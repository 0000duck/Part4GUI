using System;
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

namespace PappachanNC3
{
    public partial class Form1 : Form
    {
        //global offset values
        double xOffset = 0;
        double yOffset = 0;
        double zOffset = 0;
        double toolHeightOffset = 0;
        double radiusOffset = 0;

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

        static int feedrate =0;


        // functions
        public void initialiseCNC()
        {
            //here i should send the files that is sent initially

            sendFile(b1);
            sendFile(PappachanNC3.Properties.Resources.MILL1052);

            sendFile(b3);
            sendFile(PappachanNC3.Properties.Resources.MILL105_ACC);

            sendFile(b5);
            sendFile(PappachanNC3.Properties.Resources.MILL105);

            sendFile(b7);
            //send part 8 and 9
            sendFile(PappachanNC3.Properties.Resources.MILL1053);
            sendFile(b10);

            //send file 11 (i created this file so be carefull)
            sendFile(PappachanNC3.Properties.Resources.part11);
            sendFile(b12);

            sendFile(PappachanNC3.Properties.Resources.PCMASCH0);
            sendFile(b14);
            sendFile(PappachanNC3.Properties.Resources.Mill1051);
            sendFile(PappachanNC3.Properties.Resources.lastpacket);
            //listen to the backward communication and send the lastpacket again
            //listenTCP();
            //last file to be sent here.
            //statusLabel.Text = "Done";
            //initialiseToolStripMenuItem.Enabled = false;
            //startToolStripMenuItem.Enabled = false;

            statusBox.Text += "CNC Has Been Initialised..\r\n";
            referenceToolStripMenuItem1.Enabled = true;

            statusBox.Text += "CNC Has Been Initialised..\r\n";
            referenceToolStripMenuItem1.Enabled = true;

            //declare the packets
            byte[] packet1 = { 21, 104, 0, 0, 0, 0, 10, 0 };
            byte[] packet2 = { 0, 104, 2, 0, 0, 0, 0, 0, 3, 0 };

            sendFile(packet1);
            //listenReturnCommunication();
            //sendFile(packet2);
            statusBox.Text += "Going to Reference Mode\r\n";

            stmQueue = new Queue<stmElement>();

            ThreadStart tdStartstm = new ThreadStart(posThread);
            Thread td1 = new Thread(tdStartstm);
            td1.Start();

            ThreadStart tdStartPos = new ThreadStart(displayPos);
            Thread td2 = new Thread(tdStartPos);
            td2.Start();
        }

        private void displayPos()
        {
            while (true)
            {
                while (stmQueue.Count == 0)
                    ;

                stmElement se = stmQueue.Dequeue();

                byte[] bc = se.bc;
                int k = se.k;

                string str = "";

                for (int i = 0; i < k; i++)
                {
                    str += Convert.ToChar(bc[i]);
                }

                MethodInvoker mi = delegate
                {
                    returnBox.Text += k + ":    " + str + "\r\n";
                };
                if (InvokeRequired)
                    this.Invoke(mi);

                if (k == 26)
                {

                    if (bc[0] == 2 && bc[1] == 8)
                    {
                        //directions
                        if (bc[6] == 1)// x direction
                        {
                            int[] point = new int[8];

                            string txt = binaryToString(bc, 10);

                            MethodInvoker mi2 = delegate
                            {
                                xBox.Text = txt;
                                xOffseted.Text = (double.Parse(txt) + xOffset ).ToString();
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);

                            //dist to go

                            txt = binaryToString(bc, 18);

                            mi2 = delegate
                            {
                                xtgBox.Text = txt;
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);
                        }

                        if (bc[6] == 2)// y direction
                        {
                            int[] point = new int[8];

                            string txt = binaryToString(bc, 10);

                            MethodInvoker mi2 = delegate
                            {
                                yBox.Text = txt;
                                yOffseted.Text = (double.Parse(txt) + yOffset).ToString();
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);


                            //dist to go

                            txt = binaryToString(bc, 18);

                            mi2 = delegate
                            {
                                ytgBox.Text = txt;
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);
                        }


                        if (bc[6] == 4)// z direction
                        {
                            int[] point = new int[8];

                            string txt = binaryToString(bc, 10);

                            MethodInvoker mi2 = delegate
                            {
                                zBox.Text = txt;
                                zOffseted.Text = (double.Parse(txt) + zOffset + toolHeightOffset).ToString();
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);

                            //dist to go

                            txt = binaryToString(bc, 18);

                            mi2 = delegate
                            {
                                ztgBox.Text = txt;
                            };
                            if (InvokeRequired)
                                this.Invoke(mi2);
                        }
                    }
                    else if (bc[0] == 3 && bc[1] == 8)//spindlespeed
                    {
                        if (bc[6] == 1)  //spindle speed
                        {
                            int[] point = new int[8];
                            for (int i = 0; i < 8; i++)
                                point[i] = bc[8 + i];

                            string txt = binaryToSpindle(point);

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

                        string txt = binaryToString(bc, 10);

                        MethodInvoker mi2 = delegate
                        {
                            xBox.Text = txt;
                            xOffseted.Text = (double.Parse(txt) + xOffset).ToString();
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);


                        //dist to go

                        txt = binaryToString(bc, 18);

                        mi2 = delegate
                        {
                            xtgBox.Text = txt;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);


                        for (int i = 0; i < 8; i++)
                            point[i] = bc[28 + i];

                        txt = binaryToString(bc, 28);

                        mi2 = delegate
                        {
                            yBox.Text = txt;
                            yOffseted.Text = (double.Parse(txt) + yOffset ).ToString();
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        //dist to go

                        txt = binaryToString(bc, 36);

                        mi2 = delegate
                        {
                            ytgBox.Text = txt;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);
                    }

                    if (bc[6] == 5)//  z + x direction
                    {
                        int[] point = new int[8];

                        string txt = binaryToString(bc, 10);

                        MethodInvoker mi2 = delegate
                        {
                            xBox.Text = txt;
                            xOffseted.Text = (double.Parse(txt) + xOffset ).ToString();
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        //dist to go

                        txt = binaryToString(bc, 18);

                        mi2 = delegate
                        {
                            xtgBox.Text = txt;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        txt = binaryToString(bc, 28);

                        mi2 = delegate
                        {
                            zBox.Text = txt;
                            zOffseted.Text = (double.Parse(txt) + zOffset + toolHeightOffset).ToString();
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);


                        //dist to go

                        txt = binaryToString(bc, 36);

                        mi2 = delegate
                        {
                            ztgBox.Text = txt;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);
                    }

                    if (bc[6] == 6)// z + y direction
                    {
                        int[] point = new int[8];


                        string txt = binaryToString(bc, 10);

                        MethodInvoker mi2 = delegate
                        {
                            yBox.Text = txt;
                            yOffseted.Text = (double.Parse(txt) + yOffset ).ToString();
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);


                        //dist to go

                        txt = binaryToString(bc, 18);

                        mi2 = delegate
                        {
                            ytgBox.Text = txt;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        txt = binaryToString(bc, 28);

                        mi2 = delegate
                        {
                            zBox.Text = txt;
                            zOffseted.Text = (double.Parse(txt) + zOffset + toolHeightOffset).ToString();
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);


                        //dist to go

                        txt = binaryToString(bc, 36);

                        mi2 = delegate
                        {
                            xtgBox.Text = txt;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);
                    }

                }


                else if (k == 62)
                {
                    if (bc[6] == 7)// x + y + z direction
                    {

                        string txt = binaryToString(bc, 10);

                        MethodInvoker mi2 = delegate
                        {
                            xBox.Text = txt;
                            xOffseted.Text = (double.Parse(txt) + xOffset ).ToString();
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        //dist to go

                        txt = binaryToString(bc, 18);

                        mi2 = delegate
                        {
                            xtgBox.Text = txt;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);


                        txt = binaryToString(bc, 28);

                        mi2 = delegate
                        {
                            yBox.Text = txt;
                            yOffseted.Text = (double.Parse(txt) + yOffset ).ToString();
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);


                        //dist to go

                        txt = binaryToString(bc, 36);

                        mi2 = delegate
                        {
                            ytgBox.Text = txt;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        txt = binaryToString(bc, 46);

                        mi2 = delegate
                        {
                            zBox.Text = txt;
                            zOffseted.Text = (double.Parse(txt) + zOffset + toolHeightOffset).ToString();
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);

                        //dist to go

                        txt = binaryToString(bc, 54);

                        mi2 = delegate
                        {
                            ztgBox.Text = txt;
                        };
                        if (InvokeRequired)
                            this.Invoke(mi2);
                    }

                }

            }

        }

        public string binaryToString(byte[] byteArray, int startPos)
        {
           
            int[] point = new int[8];
            for (int i = 0; i < 8; i++)
                point[i] = byteArray[startPos + i];

            string output = binaryToPosLin(point);

            return output;
        }

        

        public byte[] feedToBinary(int pos)
        {
            // y = 1,514,611.88ln(x) + 1,055,979,950.10
            double first4 = 1514611.88 * Math.Log(pos) + 1055979950.10;
            long first4Long = Convert.ToInt64(first4);

            byte[] output = new byte[8];
            output[7] = (byte)(first4Long / (256L * 256 * 256));
            first4Long %= (256L * 256 * 256);
            output[6] = (byte)(first4Long / (256L * 256));
            first4Long %= (256L * 256);
            output[5] = (byte)(first4Long / (256L));
            first4Long %= (256L);
            output[4] = (byte)first4Long;

            return output;
        }

        public byte[] posToBinary(int pos)
        {
            //= 1,517,068.10ln(x) + 1,062,166,905.73
            double first4 = 1517068.10 * Math.Log(pos) + 1062166905.73;
            long first4Long = Convert.ToInt64(first4);

            byte[] output = new byte[8];
            output[7] = (byte)(first4Long / (256L * 256 * 256));
            first4Long %= (256L * 256 * 256);
            output[6] = (byte)(first4Long / (256L * 256));
            first4Long %= (256L * 256);
            output[5] = (byte)(first4Long / (256L));
            first4Long %= (256L);
            output[4] = (byte)first4Long;

            return output;
        }

        public byte[] spindleToBinary(double spindle)
        {
            //y= 6.507078312247120ln(x) + 4,580.272755767090000

            double first4 = 1515047.22522 * Math.Log(spindle) + 1066427853.40808;
            long first4Long = Convert.ToInt64(first4);

            byte[] output = new byte[8];
            output[7] = (byte)(first4Long / (256L * 256 * 256));
            first4Long %= (256L * 256 * 256);
            output[6] = (byte)(first4Long / (256L * 256));
            first4Long %= (256L * 256);
            output[5] = (byte)(first4Long / (256L));
            first4Long %= (256L);
            output[4] = (byte)first4Long;

            return output;
        }

        public string binaryToPosLin(int[] binaryPoint)
        {
            long intPos = (binaryPoint[4]) + (binaryPoint[5] * 256L) + (binaryPoint[6] * 256 * 256L) + (binaryPoint[7] * 256L * 256 * 256);

            double doublePos = intPos - 1062166905.73;
            doublePos = (double)(doublePos) / (double)1517068.10;
            //= 1,517,068.10ln(x) + 1,062,166,905.73
            return Math.Exp(doublePos).ToString();
        }

        public string binaryToSpindle(int[] binaryPoint)
        {
            long intPos = (binaryPoint[4]) + (binaryPoint[5] * 256L) + (binaryPoint[6] * 256 * 256L) + (binaryPoint[7] * 256L * 256 * 256);

            double doublePos = intPos - 1066427853.40808;
            doublePos = (double)(doublePos) / (double)1515047.22522;
            //y=1,515,047.22522ln(x) + 1,066,427,853.40808
            return Math.Exp(doublePos).ToString();
        }

        public string binaryToFeed(int[] feed)
        {
            long intFeed = (feed[4]) + (feed[5] * 256L) + (feed[6] * 256 * 256L) + (feed[7] * 256L * 256 * 256);

            double doubleFeed = intFeed - 1055979950.10;
            doubleFeed = (double)(doubleFeed) / (double)1514611.88;
            //y = 1,514,611.88ln(x) + 1,055,979,950.10
            return Math.Exp(doubleFeed).ToString();
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
                        statusBox.Text += "Transmitting....Part" + i.ToString() + "\r\n";
                        stm.Write(bb_temp, 0, bb_temp.Length);
                    }

                }
                else
                {
                    statusBox.Text += "Transmitting...." + "\r\n";

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

            //sendFile(PappachanNC3.Properties.Resources.lastpacket);

        }

        private void listenReturnCommunication()
        {
            Stream stm = tcpclnt.GetStream();
            byte[] bc = new byte[1000];
            int k = stm.Read(bc, 0, 1000);

            stmElement se = new stmElement();
            se.bc = bc;
            se.k = k;
            stmQueue.Enqueue(se);

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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pappachan NC Connect for EMCO Mill 105 \r\n Version 0.1.10", "Pappachan NC Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void terminateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcpclnt.Close();
            statusBox.Text += "\r\nConnection Closed\r\n";
        }

        private void referenceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //send the reference byte array
            sendFile(reference);
        }

        //jog the machine functions       
        private void moveXdown_Click_1(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 255 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(700);
            sendFile(clk2);


        }

        private void moveXup_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 1 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 0 };

            //send both bytes
            sendFile(clk1);

            System.Threading.Thread.Sleep(700);
            sendFile(clk2);

        }

        private void moveYup_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 1 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(700);
            sendFile(clk2);
        }

        private void moveYdown_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 255 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(700);
            sendFile(clk2);
        }

        private void moveZup_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 1 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(700);
            sendFile(clk2);
        }

        private void moveZdown_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 255 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 0 };

            //send both bytes
            sendFile(clk1);
            System.Threading.Thread.Sleep(700);
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
                byte[] number = spindleToBinary(double.Parse(M03Param.Text));

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

            byte[] commandMid = new byte[length - 20 +4];
            commandMid[0] = 0x1c;
            commandMid[1] = 0;
            commandMid[2] = 0x01;
            commandMid[3] = 0;
            int commandMidPos = 4;
            if (xIn.Text != "")
            {
                commandMid[commandMidPos+0] = 0x1e;
                commandMid[commandMidPos+1] = 0;
                byte[] pos = posToBinary(int.Parse(xIn.Text));
                for (int i = 0; i < 8; i++)
                    commandMid[commandMidPos+2 + i] = pos[i];
                commandMidPos += 10;
            }
            if (yIn.Text != "")
            {
                commandMid[commandMidPos + 0] = 0x1f;
                commandMid[commandMidPos + 1] = 0;
                byte[] pos = posToBinary(int.Parse(yIn.Text));
                for (int i = 0; i < 8; i++)
                    commandMid[commandMidPos + 2 + i] = pos[i];
                commandMidPos += 10;
            }
            if (zIn.Text != "")
            {
                commandMid[commandMidPos + 0] = 0x20;
                commandMid[commandMidPos + 1] = 0;
                byte[] pos = posToBinary(int.Parse(zIn.Text));
                for (int i = 0; i < 8; i++)
                    commandMid[commandMidPos + 2 + i] = pos[i];
                commandMidPos += 10;
            }


            byte[] commandEnd = { };
            byte[] feedCommand = { };

            if (sender == G00Button)
            {
                commandEnd = new byte[]{ 0x64,0,0x96,0 };
            }
            else if (sender == G01Button)
            {
                if(int.Parse(fIn.Text) != feedrate)
                {
                    length += 10;
                    feedrate = int.Parse(fIn.Text);
                    byte[] feedSpeed = feedToBinary(feedrate);
                    feedCommand = new byte[] { 0x18, 0, feedSpeed[0], feedSpeed[1], feedSpeed[2], feedSpeed[3], feedSpeed[4], feedSpeed[5], feedSpeed[6], feedSpeed[7] };
                }
                commandEnd = new byte[] { 0x65, 0, 0x96,0};
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

          

            byte[] command= new byte[length + 8];

            for (int i = 0; i < commandStart.Length; i++)
                command[i] = commandStart[i];
            if (feedCommand.Length ==0)
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
                    command[i + commandStart.Length+feedCommand.Length] = commandMid[i];
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

        private void setOffsetButton_Click(object sender, EventArgs e)
        {
            xOffset = double.Parse(xOffseted.Text) - double.Parse(xBox.Text);
            yOffset = double.Parse(yOffseted.Text) - double.Parse(yBox.Text);
            zOffset = double.Parse(zOffseted.Text) - double.Parse(zBox.Text) - toolHeightOffset;
        }




        //end of modes

    }

    public class stmElement
    {
        public byte[] bc;
        public int k;
    }
}
