<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textLong = textBox1.Text;
            textLong = textLong.Replace(";\r\n",";");
            textLong = textLong.Replace("; \r\n", ";");
            textLong = textLong.Replace("\r\n", ";");
            textLong = textLong.ToUpper();
            string[] commandArray = textLong.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            int lineCount = 1;   //??? starts at 1


            foreach (string command in commandArray)
            {
                string[] commandPart = command.Split(new char[]{ ' '},StringSplitOptions.RemoveEmptyEntries);
                commandPart = moveToFront(commandPart, "MG");
                ArrayList commandPartList =new ArrayList(commandPart);

                ArrayList outputArr = new ArrayList();

                //first 2
                outputArr.Add( 0x0);
                outputArr.Add( 0x58);

                //length 
                outputArr.Add( 0x0); //!!!!!temp, will be changed at end //(outputLength - 8);
                for (int i = 0; i < 7; i++)
                    outputArr.Add(0);

                //index
                outputArr.Add((byte)lineCount);
                for (int i = 0; i < 3; i++)
                    outputArr.Add(0);

                outputArr.Add(0x0e);
                for (int i = 0; i < 3; i++)
                    outputArr.Add(0);

                outputArr.Add(0);
                outputArr.Add(0);

                //first deal with the first term, can only be G or M (others possible, but not implemented, as the use is rare(?))= 
                string firstCommand = (string) commandPartList[0];
                int GCodeCommand = -1;
                if (firstCommand[0] == 'M')
                {
                    outputArr.Add(0xDE);
                    outputArr.Add(0);

                    outputArr.Add(0);
                    outputArr.Add(0);
                    outputArr.Add(int.Parse(firstCommand.Substring(1)));
                    outputArr.Add(0);
                }
                else if (firstCommand[0] == 'G')
                {
                    GCodeCommand = int.Parse(firstCommand.Substring(1));
                }
                else if (firstCommand[0] == 'F')
                {
                    outputArr.Add(0x18);
                    outputArr.Add(0);
                    byte[] pos = feedToBinary(int.Parse(firstCommand.Substring(1, firstCommand.Length - 1)));

                    for (int i = 0; i < 8; i++)
                        outputArr.Add(pos[i]);
                }
                
                //remove now processed initial command
                commandPartList.RemoveAt(0);

                commandPart = (string[]) commandPartList.ToArray(typeof(string));

                //sort list if doing arcs


                //add mysterious paramter for G00 and G01
                if (GCodeCommand ==0 || GCodeCommand == 1 || GCodeCommand == 2 || GCodeCommand == 3)
                {
                    outputArr.Add(0x1c);
                    outputArr.Add(0);
                    outputArr.Add(0x01);
                    outputArr.Add(0);
                }

                //converts r command into IJK commands
                if (GCodeCommand == 2 || GCodeCommand == 3)
                {
                    //stores converted commands
                    ArrayList commandPartNew = new ArrayList(commandPart);

                    //nullable double values for parameters
                    double? x = null, y = null, z = null, r = null;
                    foreach (string oldCommand in commandPartNew)
                    {
                        if (oldCommand[0] == 'X')
                            x = int.Parse(oldCommand.Substring(1));

                        if (oldCommand[0] == 'Y')
                            y = int.Parse(oldCommand.Substring(1));

                        if (oldCommand[0] == 'Z')
                            z = int.Parse(oldCommand.Substring(1));

                        if (oldCommand[0] == 'R')
                        {
                            r = int.Parse(oldCommand.Substring(1));
                            commandPartNew.Remove(oldCommand);
                        }
                    }

                    if (r != null)
                    {
                        if (x != null && y != null && z == null)
                        {
                            //i,j

                        }
                        else if (x == null && y != null && z != null)
                        {

                        }
                        else if (x != null && y == null && z != null)
                        {
                        }
                        else
                            ; ///error
                    }
                }

                //sorts arcs commands into proper order
                if (GCodeCommand == 2 || GCodeCommand == 3)
                {
                    commandPart = moveToFront(commandPart, "IXJYKZ");
                }

                commandPart = moveToFront(commandPart, "F");



                foreach (string indCommand in commandPart)
                {
                   if (indCommand[0] == 'X')
                    {
                        outputArr.Add(0x1e);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'Y')
                    {
                        outputArr.Add(0x1f);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'Z')
                    {
                        outputArr.Add(0x20);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'T')
                    {
                        outputArr.Add(0xDF);
                        outputArr.Add(0);
                        outputArr.Add(0);
                        outputArr.Add(0);
                        outputArr.Add(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));
                        outputArr.Add(0);
                    }

                    else if (indCommand[0] == 'P')
                    {
                        //should only be used with G04
                        if (GCodeCommand == 4)
                        {
                            outputArr.Add(0x68);
                            outputArr.Add(0);

                            byte[] pos = spindleToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));
                            /////!!!!Using wrong formula

                            for (int i = 0; i < 8; i++)
                                outputArr.Add(pos[i]);

                        }
                    }

                    else if (indCommand[0] == 'S')
                    {
                        outputArr.Add(0x1d);
                        outputArr.Add(0);
                        outputArr.Add(0xff);
                        outputArr.Add(0xff);

                        byte[] pos = spindleToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'F')
                    {
                        outputArr.Add(0x18);
                        outputArr.Add(0);
                        byte[] pos = feedToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'I')
                    {
                        outputArr.Add(0x32);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'j')
                    {
                        outputArr.Add(0x33);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'K')
                    {
                        outputArr.Add(0x34);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                }

                //end of commands
                if (GCodeCommand != -1 && GCodeCommand != 4)
                {
                    outputArr.Add(0x64 + GCodeCommand);
                    outputArr.Add(0);
                }
                outputArr.Add(0x96);
                outputArr.Add(0);

                //replace correct length
                outputArr[2] = outputArr.Count - 8;
                for(int i=0;i<outputArr.Count;i++)
                    Console.Write("{0:X} ",outputArr[i]);
                Console.WriteLine();

                lineCount++;
            }
        }

        private string[] moveToFront(string[] commandPart, string toFront)
        {
            //stores sorted commands
            ArrayList commandPartSort = new ArrayList();

            //converts array into arrList for easier sorting
            ArrayList commandPartArrList = new ArrayList(commandPart);

            foreach (char toFrontLetter in toFront)
            {
                foreach (string unsorted in commandPartArrList)
                {
                    if (unsorted[0] == toFrontLetter)
                    {
                        commandPartSort.Add(unsorted);
                        //commandPartArrList.Remove(unsorted);
                    }
                }
            }



            for (int i = 0; i < commandPartArrList.Count; i++)
            {
                string unsorted = (string)commandPartArrList[i];
                if (toFront.Contains(unsorted[0]))
                {
                    commandPartArrList.Remove(unsorted);
                    i--;
                }

            }

            foreach (string unsorted in commandPartArrList)
            {
                commandPartSort.Add(unsorted);
            }


            commandPart = (string[])commandPartSort.ToArray(typeof(string));
            return commandPart;
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

        public string binaryToPosSpindle(int[] binaryPoint)
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
    }
    
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textLong = textBox1.Text;
            textLong = textLong.Replace(";\r\n", ";");
            textLong = textLong.Replace("; \r\n", ";");
            textLong = textLong.Replace("\r\n", ";");
            textLong = textLong.ToUpper();
            string[] commandArray = textLong.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            int lineCount = 1;   //??? starts at 1
            double? xPast = null, yPast = null, zPast = null;
            foreach (string command in commandArray)
            {
                string[] commandPart = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                ArrayList commandPartList = new ArrayList(commandPart);

                ArrayList outputArr = new ArrayList();

                //first 2
                outputArr.Add(0x0);
                outputArr.Add(0x58);

                //length 
                outputArr.Add(0x0); //!!!!!temp, will be changed at end //(outputLength - 8);
                for (int i = 0; i < 7; i++)
                    outputArr.Add(0);

                //index
                outputArr.Add((byte)lineCount);
                for (int i = 0; i < 3; i++)
                    outputArr.Add(0);

                outputArr.Add(0x0e);
                for (int i = 0; i < 3; i++)
                    outputArr.Add(0);

                outputArr.Add(0);
                outputArr.Add(0);

                //first deal with the first term, can only be G or M (others possible, but not implemented, as the use is rare(?))= 
                string firstCommand = (string)commandPartList[0];
                int GCodeCommand = -1;
                if (firstCommand[0] == 'M')
                {
                    outputArr.Add(0xDE);
                    outputArr.Add(0);

                    outputArr.Add(0);
                    outputArr.Add(0);
                    outputArr.Add(int.Parse(firstCommand.Substring(1)));
                    outputArr.Add(0);
                }
                else if (firstCommand[0] == 'G')
                {
                    GCodeCommand = int.Parse(firstCommand.Substring(1));
                }
                else if (firstCommand[0] == 'F')
                {
                    outputArr.Add(0x18);
                    outputArr.Add(0);
                    byte[] pos = feedToBinary(int.Parse(firstCommand.Substring(1, firstCommand.Length - 1)));

                    for (int i = 0; i < 8; i++)
                        outputArr.Add(pos[i]);
                }

                //remove now processed initial command
                commandPartList.RemoveAt(0);

                commandPart = (string[])commandPartList.ToArray(typeof(string));

                //sort list if doing arcs


                //add mysterious paramter for G00 and G01
                if (GCodeCommand == 0 || GCodeCommand == 1 || GCodeCommand == 2 || GCodeCommand == 3)
                {
                    outputArr.Add(0x1c);
                    outputArr.Add(0);
                    outputArr.Add(0x01);
                    outputArr.Add(0);
                }

                //converts r command into IJK commands
                if (GCodeCommand == 2 || GCodeCommand == 3)
                {
                    //stores converted commands
                    ArrayList commandPartNew = new ArrayList(commandPart);

                    //nullable double values for parameters
                    double? x = null, y = null, z = null, r = null,g = null;
                    foreach (string oldCommand in commandPartNew)
                    {
                        if (oldCommand[0] == 'X')
                            x = int.Parse(oldCommand.Substring(1));

                        if (oldCommand[0] == 'Y')
                            y = int.Parse(oldCommand.Substring(1));

                        if (oldCommand[0] == 'Z')
                            z = int.Parse(oldCommand.Substring(1));

                        if (oldCommand[0] == 'R')
                        {
                            r = int.Parse(oldCommand.Substring(1));
                            commandPartNew.Remove(oldCommand);
                        }

                        if (oldCommand[0] == 'G')
                        {
                            g = int.Parse(oldCommand.Substring(1));

                        }
                    }

                    if (r != null)
                    {
                        double[] results;
                        if (x != null && y != null && z == null)
                        {
                            if ((xPast == null) || (yPast == null))
                            {
                                //fetch current position
                            }
                            else
                            {
                               results = RtoCentre(r.Value, xPast.Value, yPast.Value, x.Value, y.Value);
                            }
                            //i,j

                        }
                        else if (x == null && y != null && z != null)
                        {
                            if ((yPast == null) || (zPast == null))
                            {
                                //fetch current position
                            }
                            else
                            {
                                
                            }
                            //j,k
                        }
                        else if (x != null && y == null && z != null)
                        {
                            if ((xPast == null) || (zPast == null))
                            {
                                //fetch current position
                            }
                            else
                            {
                               
                            }
                            //i,k
                        }
                        else
                            ; ///error
                        if (g == 02)
                        {
                            //clockwise
                            //break into four quadrants
                            //first quad upleft
                            //second quad downleft
                            //third quad downright
                            //fouth quad up right
                        }
                        else if (g == 03)
                        {
                            //counter-clockwise
                        }
                    }
                    if (g != null)
                    {
                        xPast = x;
                        yPast = y;
                        zPast = z;
                    }

                }

                //sorts arcs commands into proper order
                if (GCodeCommand == 2 || GCodeCommand == 3)
                {
                    //stores sorted commands
                    ArrayList commandPartSort = new ArrayList();

                    //converts array into arrList for easier sorting
                    ArrayList commandPartArrList = new ArrayList(commandPart);


                    foreach (string unsorted in commandPartArrList)
                    {
                        if (unsorted[0] == 'I')
                        {
                            commandPartSort.Add(unsorted);
                            //commandPartArrList.Remove(unsorted);
                        }
                    }
                    foreach (string unsorted in commandPartArrList)
                    {
                        if (unsorted[0] == 'X')
                        {
                            commandPartSort.Add(unsorted);
                            //commandPartArrList.Remove(unsorted);
                        }
                    }
                    foreach (string unsorted in commandPartArrList)
                    {
                        if (unsorted[0] == 'J')
                        {
                            commandPartSort.Add(unsorted);
                            //commandPartArrList.Remove(unsorted);
                        }
                    }
                    foreach (string unsorted in commandPartArrList)
                    {
                        if (unsorted[0] == 'Y')
                        {
                            commandPartSort.Add(unsorted);
                            //commandPartArrList.Remove(unsorted);
                        }
                    }
                    foreach (string unsorted in commandPartArrList)
                    {
                        if (unsorted[0] == 'K')
                        {
                            commandPartSort.Add(unsorted);
                            //commandPartArrList.Remove(unsorted);
                        }
                    }
                    foreach (string unsorted in commandPartArrList)
                    {
                        if (unsorted[0] == 'Z')
                        {
                            commandPartSort.Add(unsorted);
                            //commandPartArrList.Remove(unsorted);
                        }
                    }

                    for (int i = 0; i < commandPartArrList.Count; i++)
                    {
                        string unsorted = (string)commandPartArrList[i];
                        if (unsorted[0] == 'X' || unsorted[0] == 'Y' || unsorted[0] == 'Z' ||
                            unsorted[0] == 'I' || unsorted[0] == 'J' || unsorted[0] == 'K')
                        {
                            commandPartArrList.Remove(unsorted);
                            i--;
                        }

                    }

                    foreach (string unsorted in commandPartArrList)
                    {
                        commandPartSort.Add(unsorted);
                    }


                    commandPart = (string[])commandPartSort.ToArray(typeof(string));
                }

                //eleganise later (jk)
                if (true)
                {
                    //stores sorted commands
                    ArrayList commandPartSort = new ArrayList();

                    //converts array into arrList for easier sorting
                    ArrayList commandPartArrList = new ArrayList(commandPart);

                    foreach (string unsorted in commandPartArrList)
                    {
                        if (unsorted[0] == 'F')
                        {
                            commandPartSort.Add(unsorted);
                            //commandPartArrList.Remove(unsorted);
                        }
                    }

                    for (int i = 0; i < commandPartArrList.Count; i++)
                    {
                        string unsorted = (string)commandPartArrList[i];
                        if (unsorted[0] == 'F')
                        {
                            commandPartArrList.Remove(unsorted);
                            i--;
                        }

                    }

                    foreach (string unsorted in commandPartArrList)
                    {
                        commandPartSort.Add(unsorted);
                    }



                    commandPart = (string[])commandPartSort.ToArray(typeof(string));
                }



                foreach (string indCommand in commandPart)
                {
                    if (indCommand[0] == 'X')
                    {
                        outputArr.Add(0x1e);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'Y')
                    {
                        outputArr.Add(0x1f);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'Z')
                    {
                        outputArr.Add(0x20);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'T')
                    {
                        outputArr.Add(0xDF);
                        outputArr.Add(0);
                        outputArr.Add(0);
                        outputArr.Add(0);
                        outputArr.Add(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));
                        outputArr.Add(0);
                    }

                    else if (indCommand[0] == 'P')
                    {
                        //should only be used with G04
                        if (GCodeCommand == 4)
                        {
                            outputArr.Add(0x68);
                            outputArr.Add(0);

                            byte[] pos = spindleToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));
                            /////!!!!Using wrong formula

                            for (int i = 0; i < 8; i++)
                                outputArr.Add(pos[i]);

                        }
                    }

                    else if (indCommand[0] == 'S')
                    {
                        outputArr.Add(0x1d);
                        outputArr.Add(0);
                        outputArr.Add(0xff);
                        outputArr.Add(0xff);

                        byte[] pos = spindleToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'F')
                    {
                        outputArr.Add(0x18);
                        outputArr.Add(0);
                        byte[] pos = feedToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'I')
                    {
                        outputArr.Add(0x32);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'j')
                    {
                        outputArr.Add(0x33);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'K')
                    {
                        outputArr.Add(0x34);
                        outputArr.Add(0);
                        byte[] pos = posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                }

                //end of commands
                if (GCodeCommand != -1 && GCodeCommand != 4)
                {
                    outputArr.Add(0x64 + GCodeCommand);
                    outputArr.Add(0);
                }
                outputArr.Add(0x96);
                outputArr.Add(0);

                //replace correct length
                outputArr[2] = outputArr.Count - 8;
                for (int i = 0; i < outputArr.Count; i++)
                    Console.Write("{0:X} ", outputArr[i]);
                Console.WriteLine();

                lineCount++;
            }
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

        public string binaryToPosSpindle(int[] binaryPoint)
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


        public double[] quadratic(double a, double b, double c)
        {
            double[] answer = new double[2];
            answer[0] = (-b + Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / (2 * a);
            answer[1] = (-b - Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / (2 * a);

            return answer;
        }

        public double[] RtoCentre(double r, double xstart, double ystart, double xend, double yend)
        {
            double[] answer = new double[4];
            double[] xresult = new double[2];
            double[] yresult = new double[2];
            double xdiff = xend - xstart;
            double ydiff = yend - ystart;

            double yA = Math.Pow(ydiff, 2)/Math.Pow(xdiff, 2) + 1;

            double yC = (Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2)) / (2 * xdiff);

            double yB = -(yC * ydiff / xdiff);

            yresult = quadratic(yA, yB, yC);

            double xA = Math.Pow(xdiff, 2) / Math.Pow(ydiff, 2) + 1;

            double xC = (Math.Pow(ydiff, 2) + Math.Pow(xdiff, 2)) / (2 * ydiff);

            double xB = -(xC * xdiff / ydiff);

            xresult = quadratic(xA, xB, xC);


            answer[0] = xresult[0];
            answer[1] = yresult[0];
            answer[2] = xresult[1];
            answer[3] = yresult[1];  
            

            return answer;
        }


    }
}
>>>>>>> origin/master
