﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
//using iWindow.GCodeParser;


namespace iWindow
{
    public partial class Form1 : Form
    {
        public static Form1 frm;

        public static bool matRemove = false;
        public static bool cadModel = false;
        public static bool physicalToolpath = false;
        public static bool axisDisplay = false;
        public static bool tooltipDisplay = false;
        public static bool badVolDisplay = false;

        //global offset values
        public static double xOffset = 0;
        public static double yOffset = 0;
        public static double zOffset = 0;
        public static double toolHeightOffset = 0;

        public TcpClient tcpclnt = new TcpClient();

        //Common variables
        public byte[] b1 = { 8, 64, 0, 0, 237, 18, 0, 0 };
        public byte[] b3 = { 8, 64, 6, 0, 241, 2, 0, 0 };
        public byte[] b5 = { 8, 64, 1, 0, 252, 6, 0, 0 };
        public byte[] b7 = { 8, 64, 2, 0, 4, 4, 0, 0 };
        public byte[] b10 = { 8, 64, 3, 0, 155, 0, 0, 0 };
        public byte[] b12 = { 8, 64, 4, 0, 231, 50, 0, 0 };
        public byte[] b14 = { 8, 64, 5, 0, 236, 3, 1, 0 };

        //byrte for referencing
        public byte[] reference = { 3, 104, 4, 0, 0, 0, 0, 0, 7, 0, 0, 0 };

        //for program part
        //for the jagged array 
        public byte[][] jaggedBytes = new byte[250][];
        public int codeLines = 0;

        public bool spindleOn = true;
        public bool feedOn = true;
        public double[] position = { 291.001, 125.403, 188.454 };

        Queue<stmElement> stmQueue;

        static double feedrate = 0;

        AR.render glRender;
        bool glSync = false;

        public static double? currentX = null, currentY = null, currentZ = null;
        public static double? currentX0 = null, currentY0 = null, currentZ0 = null;

        double sendLimit = 200;
        int lineNum = -1;

        public static string camIP = "169.254.6.232"; //"192.168.1.1";

        static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        public static bool offsetSet = false;


        // functions
        public void initialiseCNC()
        {

            //here i should send the files that is sent initially

            sendFile(b1);
            sendFile(iWindow.Properties.Resources.MILL1052);

            sendFile(b3);
            sendFile(iWindow.Properties.Resources.MILL105_ACC);

            sendFile(b5);
            sendFile(iWindow.Properties.Resources.MILL105);

            sendFile(b7);
            //send part 8 and 9
            sendFile(iWindow.Properties.Resources.MILL1053);
            sendFile(b10);

            //send file 11 (i created this file so be carefull)
            sendFile(iWindow.Properties.Resources.part11);
            sendFile(b12);

            sendFile(iWindow.Properties.Resources.PCMASCH0);
            sendFile(b14);
            sendFile(iWindow.Properties.Resources.Mill1051);
            sendFile(iWindow.Properties.Resources.lastpacket);
            //listen to the backward communication and send the lastpacket again
            //listenTCP();
            //last file to be sent here.
            //statusLabel.Text = "Done";
            //initialiseToolStripMenuItem.Enabled = false;
            //startToolStripMenuItem.Enabled = false;

            statusBox.Text += "CNC Has Been Initialised..\r\n";
            refButton.Enabled = true;

            //declare the packets
            byte[] packet1 = { 21, 104, 0, 0, 0, 0, 10, 0 };
            byte[] packet2 = { 0, 104, 2, 0, 0, 0, 0, 0, 3, 0 };

            sendFile(packet1);
            //listenReturnCommunication();
            //sendFile(packet2);
            statusBox.Text += "Going to Reference Mode\r\n";

            stmQueue = new Queue<stmElement>();

            renderGL();

            ThreadStart tdStartstm = new ThreadStart(posThread);
            Thread td1 = new Thread(tdStartstm);
            td1.Start();

            ThreadStart tdStartPos = new ThreadStart(displayPos);
            Thread td2 = new Thread(tdStartPos);
            td2.Start();

            ThreadStart tdStartSpd = new ThreadStart(calcSpeed);
            Thread td3 = new Thread(tdStartSpd);
            td3.Start();

        }


        private void calcSpeed()
        {
            double prevPosX, prevPosY, prevPosZ;
            double currentVelX, currentVelY, currentVelZ;
            double prevVelX, prevVelY, prevVelZ;
            double accelX, accelY, accelZ;

            while (currentX0 == null || currentY0 == null || currentZ0 == null)
            {
                Thread.Sleep(100);
            }

            //first tick
            prevPosX = currentX0.Value;
            prevPosY = currentY0.Value;
            prevPosZ = currentZ0.Value;
            
            System.DateTime timeNext = System.DateTime.Now.AddMilliseconds(500);

            //2nd tick
            while(DateTime.Now.CompareTo(timeNext)<0)
            {
                Thread.Sleep(5);
            }

            while (currentX0 == null || currentY0 == null || currentZ0 == null)
            {
                Thread.Sleep(1);
            }

            currentVelX = (prevPosX - currentX0.Value) / .5;
            currentVelY = (prevPosY - currentY0.Value) / .500;
            currentVelZ = (prevPosZ - currentZ0.Value) / .500;
            prevPosX = currentX0.Value;
            prevPosY = currentY0.Value;
            prevPosZ = currentZ0.Value;
            timeNext = timeNext.AddMilliseconds(500);

            //3rd + tick
            while (true)
            {
                while (DateTime.Now.CompareTo(timeNext) < 0)
                {
                    Thread.Sleep(5);
                }

                if (currentX0 != null && currentY0 != null && currentZ0 != null)
                {
                    
                    //assigning old velocities into placeholders
                    prevVelX = currentVelX;
                    prevVelY = currentVelY;
                    prevVelZ = currentVelZ;

                    double currentPosX = currentX0.Value;
                    double currentPosY = currentY0.Value;
                    double currentPosZ = currentZ0.Value;

                    //calculating current velocity in mm/s
                    currentVelX = (prevPosX - currentPosX) / .500 * 60;
                    currentVelY = (prevPosY - currentPosY) / .500 * 60;
                    currentVelZ = (prevPosZ - currentPosZ) / .500 * 60;


                    prevPosX = currentPosX;
                    prevPosY = currentPosY;
                    prevPosZ = currentPosZ;

                    accelX = (prevVelX - currentVelX) / .500;
                    accelY = (prevVelY - currentVelY) / .500;
                    accelZ = (prevVelZ - currentVelZ) / .500;

                    timeNext = timeNext.AddMilliseconds(500);

                    MethodInvoker mi2 = delegate
                    {
                        //line ended
                        //lineComplete.Text = (int.Parse(lineComplete.Text) + 1).ToString();
                        xVelBox.Text = String.Format("{0:0.0000}", currentVelX);
                        yVelBox.Text = String.Format("{0:0.0000}", currentVelY);
                        zVelBox.Text = String.Format("{0:0.0000}", currentVelZ);

                        xAccelBox.Text = String.Format("{0:0.0000}", accelX);
                        yAccelBox.Text = String.Format("{0:0.0000}", accelY);
                        zAccelBox.Text = String.Format("{0:0.0000}", accelZ);

                    };

                    if (InvokeRequired)
                        this.Invoke(mi2);
                }

            }
        }

        private void renderGL()
        {
            glRender = new AR.render(glControl1);
        }

        private void displayPos()
        {
            while (true)
            {
                while (stmQueue.Count == 0)
                    Thread.Sleep(5);


                stmElement se = null;

                semaphore.Wait();
                se = stmQueue.Dequeue();
                semaphore.Release();

                byte[] bc = se.bc;
                int k = se.k;

                string str = "";

                for (int i = 0; i < k; i++)
                {
                    str += Convert.ToChar(bc[i]);
                }

                /*
                MethodInvoker mi = delegate
                {
                    returnBox.Text += k + ":    " + str + "\r\n";
                };
                if (InvokeRequired)
                    this.Invoke(mi);
                    */

                if (k == 8)
                {
                    if (bc[0] == 0x0a && bc[1] == 0x08)
                    {
                        string feedPercent = "";
                        if (bc[6] == 0xb0 && bc[7] == 0x04)
                            feedPercent = "120%";
                        else if (bc[6] == 0x4c && bc[7] == 0x04)
                            feedPercent = "110%";
                        else if (bc[6] == 0xe8 && bc[7] == 0x03)
                            feedPercent = "100%";
                        else if (bc[6] == 0x84 && bc[7] == 0x03)
                            feedPercent = "90%";
                        else if (bc[6] == 0x20 && bc[7] == 0x03)
                            feedPercent = "80%";
                        else if (bc[6] == 0xbc && bc[7] == 0x02)
                            feedPercent = "70%";
                        else if (bc[6] == 0x58 && bc[7] == 0x02)
                            feedPercent = "60%";
                        else if (bc[6] == 0xf4 && bc[7] == 0x01)
                            feedPercent = "50%";
                        else if (bc[6] == 0x90 && bc[7] == 0x01)
                            feedPercent = "40%";
                        else if (bc[6] == 0x2c && bc[7] == 0x01)
                            feedPercent = "30%";
                        else if (bc[6] == 0xc8 && bc[7] == 0x00)
                            feedPercent = "20%";
                        else if (bc[6] == 0x64 && bc[7] == 0x00)
                            feedPercent = "10%";
                        else if (bc[6] == 0x32 && bc[7] == 0x00)
                            feedPercent = "5%";
                        else if (bc[6] == 0x14 && bc[7] == 0x00)
                            feedPercent = "2%";
                        else if (bc[6] == 0x08 && bc[7] == 0x00)
                            feedPercent = "1%";
                        else if (bc[6] == 0x00 && bc[7] == 0x00)
                            feedPercent = "0%";

                        MethodInvoker mi2 = delegate
                        {
                            feedRatePercentTxtbox.Text = feedPercent;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);
                    }
                }

                if (k == 12)
                {

                    if (bc[0] == 0x0f)
                    {
                        if (bc[1] == 0x08 && bc[2] == 0x04)
                        {
                            if(lineNum!= -1)
                                lineNum++;
                            sendLimit += 1.00;

                            if (lineNum == GCodeParser.commandStringArr.Length)
                            {
                                lineNum = -1;
                                runGCode.Enabled = true;
                            }

                            MethodInvoker mi2 = delegate
                            {
                                //line ended
                                //lineComplete.Text = (int.Parse(lineComplete.Text) + 1).ToString();
                                if (lineNum != -1)
                                {
                                    lineNumberBox.Text = lineNum.ToString();
                                    currentLineBox.Text = GCodeParser.commandStringArr[lineNum-1];
                                }
                            };

                            if (InvokeRequired)
                                this.Invoke(mi2);
                        }
                    }
                }

                if (k == 26)
                {

                    if (bc[0] == 2 && bc[1] == 8)
                    {
                        //directions
                        if (bc[6] == 1)// x direction
                        {
                            int[] point = new int[8];

                            string txt = Converter.binaryToString(bc, 10);

                            MethodInvoker mi2 = delegate
                            {
                                xBox.Text = txt;
                                xOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + xOffset));
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);

                            currentX0 = double.Parse(txt);
                            currentX = currentX0 + xOffset;

                            //dist to go

                            txt = Converter.binaryToString(bc, 18);
                            
                        }

                        if (bc[6] == 2)// y direction
                        {
                            int[] point = new int[8];

                            string txt = Converter.binaryToString(bc, 10);

                            MethodInvoker mi2 = delegate
                            {
                                yBox.Text = txt;
                                yOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + yOffset));
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);

                            currentY0 = double.Parse(txt);
                            currentY = currentY0 + yOffset;

                            //dist to go

                            txt = Converter.binaryToString(bc, 18);
                            
                        }


                        if (bc[6] == 4)// z direction
                        {
                            int[] point = new int[8];

                            string txt = Converter.binaryToString(bc, 10);

                            MethodInvoker mi2 = delegate
                            {
                                zBox.Text = txt;
                                zOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + zOffset + toolHeightOffset));
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);

                            currentZ0 = double.Parse(txt);
                            currentZ = currentZ0 + zOffset + toolHeightOffset;

                            //dist to go

                            txt = Converter.binaryToString(bc, 18);
                            
                        }
                    }
                    else if (bc[0] == 3 && bc[1] == 8)//spindlespeed
                    {
                        if (bc[6] == 1)  //spindle speed
                        {
                            int[] point = new int[8];
                            for (int i = 0; i < 8; i++)
                                point[i] = bc[8 + i];

                            string txt = Converter.binaryToSpindle(point);

                            MethodInvoker mi2 = delegate
                            {
                                sBox.Text = txt;
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);
                        }
                    }

                }

                else if (k == 44)
                {
                    if (bc[6] == 3)// x + y direction
                    {
                        int[] point = new int[8];

                        string txt = Converter.binaryToString(bc, 10);

                        MethodInvoker mi2 = delegate
                        {
                            xBox.Text = txt;
                            xOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + xOffset));
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        currentX0 = double.Parse(txt);
                        currentX = currentX0 + xOffset;

                        //dist to go

                        txt = Converter.binaryToString(bc, 18);
                        


                        for (int i = 0; i < 8; i++)
                            point[i] = bc[28 + i];

                        txt = Converter.binaryToString(bc, 28);

                        mi2 = delegate
                        {
                            yBox.Text = txt;
                            yOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + yOffset));
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        currentY0 = double.Parse(txt);
                        currentY = currentY0 + xOffset;

                        //dist to go

                        txt = Converter.binaryToString(bc, 36);
                        
                    }

                    if (bc[6] == 5)//  z + x direction
                    {
                        int[] point = new int[8];

                        string txt = Converter.binaryToString(bc, 10);

                        MethodInvoker mi2 = delegate
                        {
                            xBox.Text = txt;
                            xOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + xOffset));
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        currentX0 = double.Parse(txt);
                        currentX = currentX0 + xOffset;
                        //dist to go

                        txt = Converter.binaryToString(bc, 18);
                        

                        txt = Converter.binaryToString(bc, 28);

                        mi2 = delegate
                        {
                            zBox.Text = txt;
                            zOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + zOffset + toolHeightOffset));
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        currentZ0 = double.Parse(txt);
                        currentZ = currentZ0 + zOffset + toolHeightOffset;


                        //dist to go

                        txt = Converter.binaryToString(bc, 36);
                        
                    }

                    if (bc[6] == 6)// z + y direction
                    {
                        int[] point = new int[8];


                        string txt = Converter.binaryToString(bc, 10);

                        MethodInvoker mi2 = delegate
                        {
                            yBox.Text = txt;
                            yOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + yOffset));
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        currentY0 = double.Parse(txt);
                        currentY = currentY0 + xOffset;
                        //dist to go

                        txt = Converter.binaryToString(bc, 18);
                        

                        txt = Converter.binaryToString(bc, 28);

                        mi2 = delegate
                        {
                            zBox.Text = txt;
                            zOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + zOffset + toolHeightOffset));
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        currentZ0 = double.Parse(txt);
                        currentZ = currentZ0 + zOffset + toolHeightOffset;


                        //dist to go

                        txt = Converter.binaryToString(bc, 36);
                        
                    }

                }


                else if (k == 62)
                {
                    if (bc[6] == 7)// x + y + z direction
                    {

                        string txt = Converter.binaryToString(bc, 10);

                        MethodInvoker mi2 = delegate
                        {
                            xBox.Text = txt;
                            xOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + xOffset));
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        currentX0 = double.Parse(txt);
                        currentX = currentX0 + xOffset;

                        //dist to go

                        txt = Converter.binaryToString(bc, 18);
                        


                        txt = Converter.binaryToString(bc, 28);

                        mi2 = delegate
                        {
                            yBox.Text = txt;
                            yOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + yOffset));
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        currentY0 = double.Parse(txt);
                        currentY = currentY0 + xOffset;


                        //dist to go

                        txt = Converter.binaryToString(bc, 36);
                        

                        txt = Converter.binaryToString(bc, 46);

                        mi2 = delegate
                        {
                            zBox.Text = txt;
                            zOffseted.Text = String.Format("{0:0.0000}", (double.Parse(txt) + zOffset + toolHeightOffset));
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        currentZ0 = double.Parse(txt);
                        currentZ = currentZ0+ zOffset + toolHeightOffset;

                        //dist to go

                        txt = Converter.binaryToString(bc, 54);
                        
                    }

                }
                if (double.IsInfinity(currentX.GetValueOrDefault()))
                    currentX = null;

                if (double.IsInfinity(currentY.GetValueOrDefault()))
                    currentY = null;

                if (double.IsInfinity(currentZ.GetValueOrDefault()))
                    currentZ = null;

                if (double.IsInfinity(currentX0.GetValueOrDefault()))
                    currentX0 = null;

                if (double.IsInfinity(currentY0.GetValueOrDefault()))
                    currentY0 = null;

                if (double.IsInfinity(currentZ0.GetValueOrDefault()))
                    currentZ0 = null;

            }

        }



        public void posThread()
        {
            while (true)
            {
                listenReturnCommunication();
            }
        }

        //sends the byte array to the remote location
        public void sendFile(byte[] bb)
        {
            //code here for sending files
            try
            {
                //String str = System.IO.File.ReadAllText(file);
                NetworkStream stm = tcpclnt.GetStream();
                tcpclnt.SendBufferSize = 512;
                tcpclnt.NoDelay = true;

                //ASCIIEncoding asen = new ASCIIEncoding();
                //byte[] ba = asen.GetBytes(str);

                if (bb.Length > 512)
                {
                    int count = bb.Length / 512;

                    for (int i = 0; i <= count; i++)
                    {

                        int length = 0;
                        byte[] bb_temp = null;
                        if (i == count)
                        {

                            length = bb.Length - (i * 512);
                            bb_temp = new byte[length];
                        }
                        else
                        {
                            bb_temp = new byte[512];
                            length = 512;
                        }
                        Array.ConstrainedCopy(bb, i * 512, bb_temp, 0, length);
                        //statusBox.Text += "Transmitting....Part" + i.ToString() + "\r\n";
                        stm.Write(bb_temp, 0, bb_temp.Length);
                    }

                }
                else
                {
                    //statusBox.Text += "Transmitting...." + "\r\n";

                    stm.Write(bb, 0, bb.Length);

                    //read the returning file
                    /* byte[] bc = new byte[1000];
                     int k = stm.Read(bc, 0, 1000);

                     for (int i = 0; i < k; i++)
                     {
                         statusBox.Text += Convert.ToChar(bc[i]);
                     }

                    */
                }
            }
            catch (Exception ex)
            {
                statusBox.Text += "Error \r\n" + ex.StackTrace;
            }

        }

        private void listenTCP()
        {
            NetworkStream stm = tcpclnt.GetStream();
            byte[] bc = new byte[1000];
            int k = stm.Read(bc, 0, 1000);

            /*
            for (int i = 0; i < k; i++)
            {
                statusBox.Text += Convert.ToChar(bc[i]);
            }
             * */

            //sendFile(iWindow.Properties.Resources.lastpacket);

        }

        private void listenReturnCommunication()
        {
            Stream stm = tcpclnt.GetStream();
            byte[] bc = new byte[1000];
            int k = stm.Read(bc, 0, 1000);

            stmElement se = new stmElement();
            se.bc = bc;
            se.k = k;
            semaphore.Wait();
            stmQueue.Enqueue(se);
            semaphore.Release();

        }


        public Form1()
        {
            InitializeComponent();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //TcpClient tcpclnt = new TcpClient();
                statusBox.Text += "Connecting....\r\n";

                //get the ip address and the port from the textboxes
                String ipaddr = "192.168.10.12";
                int portaddr = 1122;

                tcpclnt.Connect(ipaddr, portaddr);

                statusBox.Text += "Connected\r\n";
                statusBox.Text += "You can start transmission now\r\n";

                //transferButton.Enabled = true;
                //initialiseToolStripMenuItem.Enabled = true;
                terminateToolStripMenuItem.Enabled = true;

                // enable jog.. see if it works
                moveXup.Enabled = true;
                moveXdown.Enabled = true;

                moveYup.Enabled = true;
                moveYdown.Enabled = true;

                moveZdown.Enabled = true;
                moveZup.Enabled = true;

                //read the returning file
                Stream stm = tcpclnt.GetStream();
                byte[] bc = new byte[1000];
                int k = stm.Read(bc, 0, 1000);

                for (int i = 0; i < k; i++)
                {
                    statusBox.Text += Convert.ToChar(bc[i]);
                }
                initialiseCNC();

                startToolStripMenuItem.Enabled = false;
            }
            catch (Exception ex)
            {
                statusBox.Text += "Error \r\n" + ex.StackTrace;
            }
        }
        

        private void terminateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcpclnt.Close();
            statusBox.Text += "\r\nConnection Closed\r\n";
        }

        //jog the machine functions       
        private void moveXdown_Click_1(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 255 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(300);
            sendFile(clk2);


        }

        private void moveXup_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 1 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 0 };

            //send both bytes
            sendFile(clk1);

            System.Threading.Thread.Sleep(300);
            sendFile(clk2);

        }

        private void moveYup_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 1 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(300);
            sendFile(clk2);
        }

        private void moveYdown_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 255 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(300);
            sendFile(clk2);
        }

        private void moveZup_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 1 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(300);
            sendFile(clk2);
        }

        private void moveZdown_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 255 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(300);
            sendFile(clk2);
        }
        //end of jog modes

        //code for creating and sending program
        private void sendProgramButton_Click(object sender, EventArgs e)
        {
            try
            {
                //declare the packets
                byte[] packet1 = { 4, 104, 0, 0, 0, 0, 0, 0 };
                //send the request
                sendFile(packet1);

                //listen to the acceptence
                //listenReturnCommunication();
                //listenReturnCommunication();

                //declare the packets to send
                //G57
                byte[] program1 = { 0, 88, 24, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 14, 0, 0, 0, 0, 0, 1, 0, 103, 156, 134, 168, 194, 159, 210, 63, 150, 0 };
                //Spindle on 
                byte[] program2 = { 0, 88, 32, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 14, 0, 0, 0, 0, 0, 222, 0, 0, 0, 3, 0, 29, 0, 255, 255, 171, 170, 170, 170, 170, 170, 48, 64, 150, 0 };
                //absolute programming and feed rate
                byte[] program3 = { 0, 88, 24, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 14, 0, 0, 0, 0, 0, 24, 0, 123, 20, 174, 71, 225, 122, 116, 63, 150, 0 };

                //send the first 3 packets
                sendFile(program1);
                sendFile(program2);
                sendFile(program3);
                //use loop to send the program bytes
                for (int i = 0; i < codeLines; i++)
                {
                    sendFile(jaggedBytes[i]);

                }
                byte tn = Convert.ToByte(codeLines + 4);

                //end of loop
                //send the m30 packet
                byte[] program4 = { 0, 88, 20, 0, 0, 0, 0, 0, 0, 0, tn, 0, 0, 0, 14, 0, 0, 0, 0, 0, 222, 0, 0, 0, 30, 0, 150, 0 };
                sendFile(program4);
            }
            catch (Exception ex)
            {
                statusBox.Text += "Error \r\n" + ex.StackTrace;
            }
        }



        private void programFeedButton_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 88, 30, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 14, 0, 0, 0, 0, 0, 1, 0, 143, 138, 255, 59, 162, 66, 199, 63, 222, 0, 0, 0, 3, 0, 150, 0 };

            //send both bytes
            sendFile(clk1);
        }


        //modes
        private void referenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //declare the packets
            byte[] packet1 = { 21, 104, 0, 0, 0, 0, 10, 0 };
            byte[] packet2 = { 0, 104, 2, 0, 0, 0, 0, 0, 3, 0 };

            sendFile(packet1);
            //listenReturnCommunication();
            //sendFile(packet2);
            statusBox.Text += "Going to Reference Mode\r\n";
        }

        private void memoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //declare memory mode packets
            byte[] packet1 = { 21, 104, 0, 0, 0, 0, 4, 0 };
            byte[] packet2 = { 16, 104, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //send the packet
            sendFile(packet1);
            //listen for the return communication
            //listenReturnCommunication();
            //send the second packet
            //sendFile(packet2);
            //listen for the return
            //listenReturnCommunication();
            statusBox.Text += "Going to Memory Mode Mode\r\n";
        }

        private void jogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //declare the jogmode file
            byte[] packet1 = { 21, 104, 0, 0, 0, 0, 0, 0 };

            sendFile(packet1);
            //listenReturnCommunication();
            statusBox.Text += "Going to Jog Mode\r\n";
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void mDIModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //declare the packets
            byte[] packet1 = { 21, 104, 0, 0, 0, 0, 2, 0 };

            sendFile(packet1);
            //listenReturnCommunication();
            //sendFile(packet2);
            statusBox.Text += "Going to Reference Mode\r\n";
        }

        private void M05Button_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 88, 30, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 14, 0, 0, 0, 0, 0, 1, 0, 143, 138, 255, 59, 162, 66, 199, 63, 222, 0, 0, 0, 5, 0, 150, 0 };

            //send both bytes
            sendFile(clk1);
        }

        private void M03Button_Click(object sender, EventArgs e)
        {
            if (M03Param.Text == "")
            {
                byte[] clk1 = { 0, 88, 30, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 14, 0, 0, 0, 0, 0, 1, 0, 143, 138, 255, 59, 162, 66, 199, 63, 222, 0, 0, 0, 3, 0, 150, 0 };

                //send both bytes
                sendFile(clk1);
            }
            else
            {
                byte[] letterS = { 0x1d, 0x00, 0xff, 0xff };
                byte[] number = Converter.spindleToBinary(double.Parse(M03Param.Text));

                byte[] clk1 = { 0, 0x58, 0x20, 0, 0, 0, 0, 0, 0, 0, 0xb6, 0, 0, 0, 0x0e, 0, 0, 0, 0, 0, 0xde, 0, 0, 0, 3, 0, letterS[0], letterS[1], letterS[2], letterS[3], number[0], number[1], number[2], number[3], number[4], number[5], number[6], number[7], 150, 0 };

                //send both bytes
                sendFile(clk1);
            }
        }

        private void ModeControl_Click(object sender, EventArgs e)
        {
            TabControl tab = (TabControl)sender;
            string tabName = tab.SelectedTab.Name;
            if (tabName == "referenceTab")
            {
                //declare the packets
                byte[] packet1 = { 21, 104, 0, 0, 0, 0, 10, 0 };
                byte[] packet2 = { 0, 104, 2, 0, 0, 0, 0, 0, 3, 0 };

                sendFile(packet1);
                //listenReturnCommunication();
                //sendFile(packet2);
                statusBox.Text += "Going to Reference Mode\r\n";
            }
            else if (tabName == "memoryTab")
            {
                //declare memory mode packets
                byte[] packet1 = { 21, 104, 0, 0, 0, 0, 4, 0 };
                byte[] packet2 = { 16, 104, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                //send the packet
                sendFile(packet1);
                //listen for the return communication
                //listenReturnCommunication();
                //send the second packet
                //sendFile(packet2);
                //listen for the return
                ////listenReturnCommunication();
                statusBox.Text += "Going to Memory Mode Mode\r\n";
            }
            else if (tabName == "referenceTab")
            {

            }
            else if (tabName == "jogTab")
            {
                //declare the jogmode file
                byte[] packet1 = { 21, 104, 0, 0, 0, 0, 0, 0 };

                sendFile(packet1);
                //listenReturnCommunication();
                statusBox.Text += "Going to Jog Mode\r\n";
            }
            else if (tabName == "mdiTab")
            {
                //declare the packets
                byte[] packet1 = { 21, 104, 0, 0, 0, 0, 2, 0 };

                sendFile(packet1);
                //listenReturnCommunication();
                //sendFile(packet2);
                statusBox.Text += "Going to MDI Mode\r\n";
            }

        }

        private void g00Button_Click(object sender, EventArgs e)
        {
            byte length = 20;
            byte lineNum = 1;
            if (xIn.Text != "")
            {
                length += 10;
            }
            if (yIn.Text != "")
            {
                length += 10;
            }
            if (zIn.Text != "")
            {
                length += 10;
            }

            if (length == 20)
            {
                statusBox.Text += "error: G00 no position \r\n";
                return;
            }

            byte[] commandMid = new byte[length - 20 + 4];
            commandMid[0] = 0x1c;
            commandMid[1] = 0;
            commandMid[2] = 0x01;
            commandMid[3] = 0;
            int commandMidPos = 4;
            if (xIn.Text != "")
            {
                commandMid[commandMidPos + 0] = 0x1e;
                commandMid[commandMidPos + 1] = 0;
                byte[] pos = Converter.posToBinary(double.Parse(xIn.Text),xOffset);
                for (int i = 0; i < 8; i++)
                    commandMid[commandMidPos + 2 + i] = pos[i];
                commandMidPos += 10;
            }
            if (yIn.Text != "")
            {
                commandMid[commandMidPos + 0] = 0x1f;
                commandMid[commandMidPos + 1] = 0;
                byte[] pos = Converter.posToBinary(double.Parse(yIn.Text),yOffset);
                for (int i = 0; i < 8; i++)
                    commandMid[commandMidPos + 2 + i] = pos[i];
                commandMidPos += 10;
            }
            if (zIn.Text != "")
            {
                commandMid[commandMidPos + 0] = 0x20;
                commandMid[commandMidPos + 1] = 0;
                byte[] pos = Converter.posToBinary(double.Parse(zIn.Text),zOffset);
                for (int i = 0; i < 8; i++)
                    commandMid[commandMidPos + 2 + i] = pos[i];
                commandMidPos += 10;
            }


            byte[] commandEnd = { };
            byte[] feedCommand = { };

            if (sender == G00Button)
            {
                commandEnd = new byte[] { 0x64, 0, 0x96, 0 };
            }
            else if (sender == G01Button)
            {
                if (double.Parse(fIn.Text) != feedrate)
                {
                    length += 10;
                    feedrate = double.Parse(fIn.Text);
                    byte[] feedSpeed = Converter.feedToBinary(feedrate);
                    feedCommand = new byte[] { 0x18, 0, feedSpeed[0], feedSpeed[1], feedSpeed[2], feedSpeed[3], feedSpeed[4], feedSpeed[5], feedSpeed[6], feedSpeed[7] };
                }
                commandEnd = new byte[] { 0x65, 0, 0x96, 0 };
            }

            byte[] commandStart = {
            0, 0x58,
            length, 0, 0, 0, 0, 0, 0, 0,
            lineNum, 0,0,0 , 0x0e, 0, 0, 0,
            0,0
            };

            if (sender == G00Button)
            {
                byte[] commandEndTmp =
                {
                    0x64,0,0x96,0
                };
                commandEnd = commandEndTmp;
            }



            byte[] command = new byte[length + 8];

            for (int i = 0; i < commandStart.Length; i++)
                command[i] = commandStart[i];
            if (feedCommand.Length == 0)
            {
                for (int i = 0; i < commandMid.Length; i++)
                    command[i + commandStart.Length] = commandMid[i];
                for (int i = 0; i < commandEnd.Length; i++)
                    command[i + commandStart.Length + commandMid.Length] = commandEnd[i];
            }
            else
            {
                for (int i = 0; i < feedCommand.Length; i++)
                    command[i + commandStart.Length] = feedCommand[i];
                for (int i = 0; i < commandMid.Length; i++)
                    command[i + commandStart.Length + feedCommand.Length] = commandMid[i];
                for (int i = 0; i < commandEnd.Length; i++)
                    command[i + commandStart.Length + commandMid.Length + feedCommand.Length] = commandEnd[i];
            }

            sendFile(command);
        }

        private void SendM06_Click(object sender, EventArgs e)
        {
            byte i = (byte)M06Number.Value;

            byte[] clk1 = { 0, 0x58, 0x24, 0, 0, 0, 0, 0, 0, 0, 0x01, 0, 0, 0, 0x0E, 0, 0x05, 0, 0, 0, 0x01, 0, 0x8f, 0xc2, 0xf5, 0x28, 0x5c, 0x8f, 0xca, 0x3f, 0xde, 0, 0, 0, 0x06, 0, 0xdf, 0, 0, 0, i, 0, 0x96, 0 };

            //send both bytes
            sendFile(clk1);
        }

        private void refBtn_Click(object sender, EventArgs e)
        {
            //send the reference byte array
            sendFile(reference);
        }

        private void jogTab_Click(object sender, EventArgs e)
        {

        }

        private void runGCode_Click(object sender, EventArgs e)
        {
            lineNum = 0;
            sendLimit = 200;
            Button button = sender as Button;

            MethodInvoker mi2 = delegate
            {
                button.Enabled = false;
            };
            if (InvokeRequired)
                this.Invoke(mi2);

            ArrayList outputs = GCodeParser.runGCode(GCodeTxt.Text);
            Thread thd = new Thread(() => sendGCode_thd(outputs));
            thd.Start();

        }

        private void sendGCode_thd(ArrayList outputs)
        {
            foreach (ArrayList outputLine in outputs)
            {
                while (sendLimit <= 0)
                {
                    Thread.Sleep(50);
                    //sendLimit++;
                }

                object[] a = outputLine.ToArray();
                byte[] byteArr = new byte[a.Length];
                for (int i = 0; i < a.Length; i++)
                    byteArr[i] = Convert.ToByte(a[i]);
                sendFile(byteArr);
                sendLimit--;
            }
            //runGCode.Enabled = true;
            
        }

        private void offsetButton_Click(object sender, EventArgs e)
        {
            
            xOffset = double.Parse(xOffseted.Text) - double.Parse(xBox.Text);
            yOffset = double.Parse(yOffseted.Text) - double.Parse(yBox.Text);
            zOffset = double.Parse(zOffseted.Text) - double.Parse(zBox.Text) - toolHeightOffset;
            currentX = double.Parse(xOffseted.Text);
            currentY = double.Parse(yOffseted.Text);
            currentZ = double.Parse(zOffseted.Text);
            offsetSet = true;
        }

        private void feedMinusButton_Click(object sender, EventArgs e)
        {
            sendFile(new byte[8] { 0x01, 0x68, 0, 0, 0, 0, 0xff, 0xff });
        }

        private void feedPlusButton_Click(object sender, EventArgs e)
        {
            sendFile(new byte[8] { 0x01, 0x68, 0, 0, 0, 0, 0x00, 0x80 });
        }

        private void calibrateButton_Click(object sender, EventArgs e)
        {
            glRender.cc.loadCalibrationNow();

            glRender.camInit = true;

            glRender.rotateArr = glRender.cc.rotateArr;
            glRender.translateArr = glRender.cc.translateArr;
        }

        private void matRemoveButton_Click(object sender, EventArgs e)
        {
            matRemove = !matRemove;
        }

        private void loadCalib_Click(object sender, EventArgs e)
        {
            glRender.cc.loadCalibrationAll();

            glRender.camInit = true;

            glRender.rotateArr = glRender.cc.rotateArr;
            glRender.translateArr = glRender.cc.translateArr;
        }

        private void refButton_Click(object sender, EventArgs e)
        {
            //send the reference byte array
            sendFile(reference);
        }

        private void changeIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            camIP = Microsoft.VisualBasic.Interaction.InputBox("Enter Ip", "IP", camIP);
        }

        private void cadToggle_Click(object sender, EventArgs e)
        {
            cadModel = !cadModel;
        }

        private void toolpathToggle_Click(object sender, EventArgs e)
        {
            physicalToolpath = !physicalToolpath;
        }

        private void axisToggle_Click(object sender, EventArgs e)
        {
            axisDisplay = !axisDisplay;
        }

        private void drillTipToggle_Click(object sender, EventArgs e)
        {
            tooltipDisplay = !tooltipDisplay;
        }

        private void zBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.VSync = true;//not fired
            glSync = true;
        }

        private void toggleBadVolume_Click(object sender, EventArgs e)
        {
            badVolDisplay = !badVolDisplay;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            frm = this;
        }

        private void clearLine_Click(object sender, EventArgs e)
        {
            glRender.lineList = new ArrayList();
            glRender.prevX = null;
            glRender.prevY = null;
            glRender.prevZ = null;

        }


        private void alarmBtn_Click(object sender, EventArgs e)
        {
            sendFile(new byte[8] { 0x09, 0x60, 0, 0, 0, 0, 0, 0 });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (AR.render.hasFirst && glSync)
            {
                AR.render.currentBMP = AR.cameraFeed.MD.Bitmap;
                glRender.draw();
            }
        }

        public static void writeErr(string strIn)
        {
            frm.ErrorBox.Visible = true;
            frm.ErrorBox.Text = strIn;
        }

        public bool checkGcodeButton()
        {
            return runGCode.Enabled;
        }


        //end of modes

    }

    public class stmElement
    {
        public byte[] bc;
        public int k;
    }
}
