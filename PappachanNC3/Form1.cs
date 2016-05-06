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

namespace PappachanNC3
{
    public partial class Form1 : Form
    {
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

            for (int i = 0; i < k; i++)
            {
                statusBox.Text += Convert.ToChar(bc[i]);
            }
            statusBox.Text += "/r/n";

        }

        // get the axis location in bytes from the x,y,z and r values
        private byte[] getLocBytes(double loc, string axis)
        {
            byte[] tempbytes = new byte[7];
            double location = loc;
            double axisLocation = 0;

            switch (axis)
            {
                case "X":
                    //case for X axis
                    if (location >= 250)
                    {
                        axisLocation = (1.8014398509474E+13 * location) + 5.404318832268880E+16;
                        //axisLocation = (1.8014398509474E+13 * location) + 5.404319552844620E+16;
                    }
                    else
                    {
                        axisLocation = (3.60287970189636000E+13 * location) + 4.9539581489556700E+16;
                    }

                    break;

                case "Y":
                    //case for Y axis
                    if (location >= 63)
                    {
                        axisLocation = (7.205759403792790000E+13 * location) + 4.50360320208648000E+16;
                    }
                    else
                    {
                        axisLocation = (1.44115188075853000E+14 * location) + 4.05324687039287000E+16;
                    }

                    break;

                case "Z":
                    //case for Z axis
                    if (location >= 125)
                    {
                        axisLocation = (3.602879701896300E+13 * location) + 4.9539595901075600E+16;
                    }
                    else
                    {
                        axisLocation = (7.2057594037925000E+13 * location) + 4.5035996273705300E+16;
                    }
                    break;

                /*case "Rx":
                    //radius for circular motion
                    //axisLocation = ((-1.8014398509483100E+13) * location) + 5.9105259494168200E+16;

                break;*/

                case "S":
                    //spindle speed equation

                    break;

                case "F":
                    //feed rate equation

                    break;

                default:
                    //default is X
                    if (location >= 250)
                    {
                        axisLocation = (1.8014398509474E+13 * location) + 5.404318832268880E+16;
                        //axisLocation = (1.8014398509474E+13 * location) + 5.404319552844620E+16;
                    }
                    else
                    {
                        axisLocation = (3.60287970189636000E+13 * location) + 4.9539581489556700E+16;
                    }
                    break;

            }
            Int64 roundAxisLoc = Convert.ToInt64(axisLocation);
            string hexOutput = String.Format("{0:X}", roundAxisLoc);

            tempbytes[0] = Convert.ToByte(Convert.ToInt32(hexOutput.Substring(12, 2), 16));
            tempbytes[1] = Convert.ToByte(Convert.ToInt32(hexOutput.Substring(10, 2), 16));
            tempbytes[2] = Convert.ToByte(Convert.ToInt32(hexOutput.Substring(8, 2), 16));
            tempbytes[3] = Convert.ToByte(Convert.ToInt32(hexOutput.Substring(6, 2), 16));
            tempbytes[4] = Convert.ToByte(Convert.ToInt32(hexOutput.Substring(4, 2), 16));
            tempbytes[5] = Convert.ToByte(Convert.ToInt32(hexOutput.Substring(2, 2), 16));
            tempbytes[6] = Convert.ToByte(Convert.ToInt32(hexOutput.Substring(0, 2), 16));

            return tempbytes;
        }

        private double[] getCenterPoint(double x1, double y1, double x2, double y2, double r)
        {

            //need to check if the x values are the same, if it is the equation for the center location falls apart.
            double xc1, xc2, yc1, yc2 = 0;
            double dist = Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));

            if (x1 == x2)
            {
                double Yc = (y1 + y2) / 2;

                double a = 1;
                double b = -2 * x1;
                double c = (x1 * x1) + ((Yc - y1) * (Yc - y1)) - (r * r);

                xc1 = ((-1 * b) + Math.Sqrt((b * b) - 4 * a * c)) / (2 * a);
                xc2 = ((-1 * b) - Math.Sqrt((b * b) - 4 * a * c)) / (2 * a);

                yc1 = Yc;
                yc2 = Yc;

            }
            else
            {

                double xmid = ((x2 + x1) / 2);
                double ymid = ((y2 + y1) / 2);

                //slope of the line connecting (x1,y1) and (x2,y2)

                double m2 = (y2 - y1) / (x2 - x1);

                //substutting the values into one of the distance equation
                double a = (m2 * m2 + 1);
                double b = -1 * ((2 * y1) + (2 * m2 * (xmid + (m2 * ymid - x1))));
                double c = ((xmid + (m2 * ymid) - x1) * (xmid + (m2 * ymid) - x1)) + (y1 * y1) - (r * r);

                yc1 = ((-1 * b) + Math.Sqrt((b * b) - 4 * a * c)) / (2 * a);
                yc2 = ((-1 * b) - Math.Sqrt((b * b) - 4 * a * c)) / (2 * a);


                xc1 = (xmid + (m2 * ymid) - (m2 * yc1));
                xc2 = (xmid + (m2 * ymid) - (m2 * yc2));
            }


            double[] location = { xc1, yc1, xc2, yc2 };

            return location; //in Cx1, Cy1, Cx2,Cy2 format


        }
        //function to convert hex back to co-ordinate location
        public double getCoordinate(byte[] data, string axis)
        {
            //change the byte array to the hex value
            string locHex = String.Format("{0:X}", data[16]) + String.Format("{0:X}", data[15]) + String.Format("{0:X}", data[14]) + String.Format("{0:X}", data[13]) + String.Format("{0:X}", data[12]) + String.Format("{0:X}", data[11]) + String.Format("{0:X}", data[10]);
            double loc = Convert.ToInt64(locHex, 16);
            double coordinate = 0;
            switch (axis)
            {
                case "X":
                    //there are two equations for each of the case

                    break;
                case "Y":


                    break;
                case "Z":


                    break;
                default:
                    //default is X

                    break;
            }
            return coordinate;
        }
        //end functions

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

            double pos = axisLocationReturn("X");
            if (pos >= 0)
            {
                jogXlabel.Text = pos.ToString();
            }

        }

        private void moveXup_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 1 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 0, 0 };

            //send both bytes
            sendFile(clk1);
            sendFile(clk2);

            double pos = axisLocationReturn("X");
            if (pos >= 0)
            {
                jogXlabel.Text = pos.ToString();
            }
        }

        private void moveYup_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 1 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 0 };

            //send both bytes
            sendFile(clk1);
            sendFile(clk2);
        }

        private void moveYdown_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 255 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 1, 0 };

            //send both bytes
            sendFile(clk1);
            sendFile(clk2);
        }

        private void moveZup_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 1 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 0 };

            //send both bytes
            sendFile(clk1);
            sendFile(clk2);
        }

        private void moveZdown_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 255 };
            byte[] clk2 = { 0, 104, 2, 0, 0, 0, 0, 0, 2, 0 };

            //send both bytes
            sendFile(clk1);
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
                listenReturnCommunication();
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

        private void addLocButton_Click(object sender, EventArgs e)
        {
            try
            {
                //get the byte sequence for the x y and z first
                double dx, dy, dz = 0;
                dx = Convert.ToDouble(pgmX.Text);
                dy = Convert.ToDouble(pgmY.Text);
                dz = Convert.ToDouble(pgmZ.Text);

                if (dx != position[0] || dy != position[1] || dz != position[2])
                {

                    byte[] bX = getLocBytes(dx, "X");
                    byte[] bY = getLocBytes(dy, "Y");
                    byte[] bZ = getLocBytes(dz, "Z");

                    byte tn = Convert.ToByte(codeLines + 4);

                    if (rapidMove.Checked == true)
                    {
                        jaggedBytes[codeLines] = new byte[] { 0, 88, 50, 0, 0, 0, 0, 0, 0, 0, tn, 0, 0, 0, 14, 0, 0, 0, 0, 0, 28, 0, 1, 0, 30, 0, bX[0], bX[1], bX[2], bX[3], bX[4], bX[5], bX[6], 63, 31, 0, bY[0], bY[1], bY[2], bY[3], bY[4], bY[5], bY[6], 63, 32, 0, bZ[0], bZ[1], bZ[2], bZ[3], bZ[4], bZ[5], bZ[6], 63, 100, 0, 150, 0 };
                        codeLines++;
                        codeBox.Text += "X: " + dx.ToString() + "Y: " + dy.ToString() + "Z: " + dz.ToString() + " Rapid \r\n";
                    }
                    else
                    {
                        jaggedBytes[codeLines] = new byte[] { 0, 88, 50, 0, 0, 0, 0, 0, 0, 0, tn, 0, 0, 0, 14, 0, 0, 0, 0, 0, 28, 0, 1, 0, 30, 0, bX[0], bX[1], bX[2], bX[3], bX[4], bX[5], bX[6], 63, 31, 0, bY[0], bY[1], bY[2], bY[3], bY[4], bY[5], bY[6], 63, 32, 0, bZ[0], bZ[1], bZ[2], bZ[3], bZ[4], bZ[5], bZ[6], 63, 101, 0, 150, 0 };
                        codeLines++;
                        codeBox.Text += "X: " + dx.ToString() + "Y: " + dy.ToString() + "Z: " + dz.ToString() + "\r\n";
                    }


                    rapidMove.Checked = false;
                    position[0] = dx;
                    position[1] = dy;
                    position[2] = dz;
                }
                else
                {
                    MessageBox.Show("Check the location variables, It is the same as before");
                }
            }
            catch (Exception ex)
            {
                statusBox.Text += "\r\n" + ex.StackTrace;
            }
        }

        private void spindleOnButton_Click(object sender, EventArgs e)
        {
            int tmpSspeed = Convert.ToInt32(spindleSpeedBox.Text);

            //get the byte equivalent of spindle speed

            byte tn = Convert.ToByte(codeLines + 4);
            jaggedBytes[codeLines] = new byte[] { 0, 88, 32, 0, 0, 0, 0, 0, 0, 0, tn, 0, 0, 0, 14, 0, 0, 0, 0, 0, 222, 0, 0, 0, 3, 0, 29, 0, 255, 255, 171, 170, 170, 170, 170, 170, 48, 64, 150, 0 };
            codeLines++;
            codeBox.Text += "Spindle turned on Clockwise at " + tmpSspeed.ToString() + " \r\n";
        }

        private void SpindleOffButton_Click(object sender, EventArgs e)
        {
            //get the code for M05
            byte tn = Convert.ToByte(codeLines + 4);

            jaggedBytes[codeLines] = new byte[] { 0, 88, 20, 0, 0, 0, 0, 0, 0, 0, tn, 0, 0, 0, 14, 0, 0, 0, 0, 0, 222, 0, 0, 0, 5, 0, 150, 0 };
            codeLines++;
            codeBox.Text += "Spindle off \r\n";

        }

        private void programFeedButton_Click(object sender, EventArgs e)
        {
            byte[] clk1 = { 0, 88, 30, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 14, 0, 0, 0, 0, 0, 1, 0, 143, 138, 255, 59, 162, 66, 199, 63 , 222, 0, 0, 0, 3, 0, 150, 0 };

            //send both bytes
            sendFile(clk1);
        }

        private void circularInterpButton_Click(object sender, EventArgs e)
        {
            try
            {
                //get the byte sequence for the x y and z first
                double dx, dy, dz, dr = 0;
                dx = Convert.ToDouble(pgmX.Text);
                dy = Convert.ToDouble(pgmY.Text);
                dz = Convert.ToDouble(pgmZ.Text);
                dr = Convert.ToDouble(radiusBox.Text);

                double x1, x2, y1, y2 = 0;
                x1 = position[0];
                y1 = position[1];
                x2 = dx;
                y2 = dy;

                //check the shortest distance
                double dist = Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));

                if ((dist / 2) > dr)
                {
                    MessageBox.Show("Radius is too small to form an arc across the points given");
                }
                else
                {

                    //get the center point
                    double[] Cxy = getCenterPoint(position[0], position[1], dx, dy, dr);


                    if (dx != position[0] || dy != position[1] || dz != position[2])
                    {

                        byte[] bX = getLocBytes(dx, "X");
                        byte[] bY = getLocBytes(dy, "Y");
                        byte[] bZ = getLocBytes(dz, "Z");


                        //byte[] bR = getLocBytes(dr, "X");

                        byte tn = Convert.ToByte(codeLines + 4);
                        if (counterCircle.Checked == false)
                        {
                            byte[] bCx = getLocBytes(Cxy[2], "X");
                            byte[] bCy = getLocBytes(Cxy[3], "Y");

                            //this one for clockwise movement
                            jaggedBytes[codeLines] = new byte[] { 0, 88, 60, 0, 0, 0, 0, 0, 0, 0, tn, 0, 0, 0, 14, 0, 0, 0, 0, 0, 28, 0, 1, 0, 50, 0, bCx[0], bCx[1], bCx[2], bCx[3], bCx[4], bCx[5], bCx[6], 63, 30, 0, bX[0], bX[1], bX[2], bX[3], bX[4], bX[5], bX[6], 63, 51, 0, bCy[0], bCy[1], bCy[2], bCy[3], bCy[4], bCy[5], bCy[6], 63, 31, 0, bY[0], bY[1], bY[2], bY[3], bY[4], bY[5], bY[6], 63, 102, 0, 150, 0 };

                            codeBox.Text += "CM to X:" + Convert.ToString(dx) + " Y:" + Convert.ToString(dy) + " With a R of " + Convert.ToString(dr) + " \r\n";
                        }
                        else
                        {
                            byte[] bCx = getLocBytes(Cxy[0], "X");
                            byte[] bCy = getLocBytes(Cxy[1], "Y");

                            //this one for counterclockwise
                            jaggedBytes[codeLines] = new byte[] { 0, 88, 60, 0, 0, 0, 0, 0, 0, 0, tn, 0, 0, 0, 14, 0, 0, 0, 0, 0, 28, 0, 1, 0, 50, 0, bCx[0], bCx[1], bCx[2], bCx[3], bCx[4], bCx[5], bCx[6], 63, 30, 0, bX[0], bX[1], bX[2], bX[3], bX[4], bX[5], bX[6], 63, 51, 0, bCy[0], bCy[1], bCy[2], bCy[3], bCy[4], bCy[5], bCy[6], 63, 31, 0, bY[0], bY[1], bY[2], bY[3], bY[4], bY[5], bY[6], 63, 103, 0, 150, 0 };

                            codeBox.Text += "CCM to X:" + Convert.ToString(dx) + " Y:" + Convert.ToString(dy) + " With a R of " + Convert.ToString(dr) + " \r\n";
                        }
                        codeLines++;
                        position[0] = dx;
                        position[1] = dy;
                        //position[2] = dz;
                    }
                    else
                    {
                        MessageBox.Show("Check the location variables, It is the same as before");
                    }
                }

            }
            catch (Exception ex)
            {
                statusBox.Text += "\r\n" + ex.StackTrace;
            }
        }
        //end of program codes

        //modes
        private void referenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //declare the packets
            byte[] packet1 = { 21, 104, 0, 0, 0, 0, 10, 0 };
            byte[] packet2 = { 0, 104, 2, 0, 0, 0, 0, 0, 3, 0 };

            sendFile(packet1);
            listenReturnCommunication();
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
            listenReturnCommunication();
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
            listenReturnCommunication();
            statusBox.Text += "Going to Jog Mode\r\n";
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }
        private double axisLocationReturn(string axis)
        {
            //
            Stream stm = tcpclnt.GetStream();
            //stm.set_ReadTimeout(10);
            byte[] bc = new byte[1000];
            int k = stm.Read(bc, 0, 26);
            string hexvalue;
            double decvalue;
            double aloc = 0;

            statusBox.Text += "this works until here \r\n";

            //byte condition = 2;
            //if (bc[0] == condition)
            //{
            //get the hex value from the packet
            //byte[] hexbyteval = new byte[] { bc[16], bc[15], bc[14], bc[13], bc[12], bc[11], bc[10] };
            //hexvalue = BitConverter.ToString(hexbyteval).Replace("-",string.Empty);
            hexvalue = bc[16].ToString("X") + bc[15].ToString("X") + bc[14].ToString("X") +
    bc[13].ToString("X") + bc[12].ToString("X") + bc[11].ToString("X") + bc[10].ToString("X");

            statusBox.Text += hexvalue;
            //convert the hex value to decimal value
            decvalue = Convert.ToInt64(hexvalue, 16);
            double breakvalue;
            switch (axis)
            {
                case "X":
                    breakvalue = (1.8014398509474E+13 * 250) + 5.404318832268880E+16;
                    if (decvalue >= breakvalue)
                    {
                        //axisLocation = (1.8014398509474E+13 * location) + 5.404318832268880E+16
                     aloc = (decvalue - 5.404318832268880E+16) / 1.8014398509474E+13;
                    }
                    else
                    {
                        //axisLocation = (3.60287970189636000E+13 * location) + 4.9539581489556700E+16
                     aloc = (decvalue - 4.9539581489556700E+16) /  3.60287970189636000E+13;
                    }
                    break;
                case "Y":
                    breakvalue = (7.205759403792790000E+13 * 63) + 4.50360320208648000E+16;
                    if (decvalue >= breakvalue)
                    {
                        //axisLocation = (7.205759403792790000E+13 * location) + 4.50360320208648000E+16
                     aloc = (decvalue - 4.50360320208648000E+16) /  7.205759403792790000E+13;
                    }
                    else
                    {
                        //axisLocation = (1.44115188075853000E+14 * location) +4.05324687039287000E+16
                        aloc = (decvalue - 4.05324687039287000E+16) / 1.44115188075853000E+14;
                    }
                    break;
                case "Z":
                    breakvalue = (3.602879701896300E+13 * 125) + 4.9539595901075600E+16;
                    if (decvalue >= breakvalue)
                    {
                        //axisLocation = (3.602879701896300E+13 * location) +4.9539595901075600E+16;
                        aloc = (decvalue - 4.9539595901075600E+16) / 3.602879701896300E+13;
                    }
                    else
                    {
                        //axisLocation = (7.2057594037925000E+13 * location) +4.5035996273705300E+16
                        aloc = (decvalue - 4.5035996273705300E+16) / 7.2057594037925000E+13;
                    }
                    break;
                default:
                    //default is same as X
                    breakvalue = (1.8014398509474E+13 * 250) + 5.404318832268880E+16;
                    if (decvalue >= breakvalue)
                    {
                        //axisLocation = (1.8014398509474E+13 * location) +5.404318832268880E+16
                        aloc = (decvalue - 5.404318832268880E+16) / 1.8014398509474E+13;
                    }
                    else
                    {
                        //axisLocation = (3.60287970189636000E+13 * location) +4.9539581489556700E+16
                        aloc = (decvalue - 4.9539581489556700E+16) / 3.60287970189636000E+13;
                    }
                    break;
            }
            statusBox.Text += aloc;
            statusBox.Text += "\r\n";
            return aloc;
    }

        private void mDIModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //declare the packets
            byte[] packet1 = { 21, 104, 0, 0, 0, 0, 2, 0 };

            sendFile(packet1);
            listenReturnCommunication();
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
            byte[] clk1 = { 0, 88, 30, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 14, 0, 0, 0, 0, 0, 1, 0, 143, 138, 255, 59, 162, 66, 199, 63, 222, 0, 0, 0, 3, 0, 150, 0 };

            //send both bytes
            sendFile(clk1);
        }


        //end of modes

    }
}
