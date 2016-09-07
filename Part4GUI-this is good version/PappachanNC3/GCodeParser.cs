using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PappachanNC3
{
    class GCodeParser
    {
        static double toolHeightOffset = 0;
        static double[] toolOffsetTable = new double[21];

        public static ArrayList runGCode(string textLong)
        {
            ArrayList ouput = new ArrayList();
            textLong = textLong.Replace(";\r\n", ";");
            textLong = textLong.Replace("; \r\n", ";");
            textLong = textLong.Replace("\r\n", ";");
            textLong = textLong.ToUpper();



            string[] commandArray = textLong.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            int lineCount = 1;   //??? starts at 1
            double? xPast = null, yPast = null, zPast = null;
            char[] splitter = new char[] {'G'};
            foreach (string command in commandArray)
            {
                string noSpace = removeWhiteSpace(command);
                string[] commandPart = noSpace.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                commandPart = moveToFront(commandPart, "MG");
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

                //first deal with the first ter-m, can only be G or M (others possible, but not implemented, as the use is rare(?))= 
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
                    byte[] pos = Converter.feedToBinary(int.Parse(firstCommand.Substring(1, firstCommand.Length - 1)));

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
                        double xPrev = 0, yPrev = 0, xTo = 0, yTo = 0;
                        if (x != null && y != null && z == null)
                        {
                            if ((xPast == null) || (yPast == null))
                            {
                                //fetch current position
                            }
                            else
                            {
                                xPrev = xPast.Value;
                                yPrev = yPast.Value;
                                xTo = x.Value;
                                yTo = y.Value;

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
                                xPrev = yPast.Value;
                                yPrev = zPast.Value;
                                xTo = y.Value;
                                yTo = z.Value;
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
                                xPrev = xPast.Value;
                                yPrev = zPast.Value;
                                xTo = x.Value;
                                yTo = z.Value;
                            }

                            //i,k
                        }
                        else
                            ; ///error

                        double i = 0, j = 0;
                        double[] result = RtoCentre(r.Value, xPrev, yPrev, xTo, yTo);
                        int quad = quadrant(xPast.Value, yPast.Value, x.Value, y.Value);
                        if (GCodeCommand == 2)
                        {
                            //clockwise
                            //break into four quadrants
                            //first quad upleft
                            //second quad downleft
                            //third quad downright
                            //fouth quad up right

                            if (quad == 0)
                            {
                                i = result[1];// left
                                j = result[2];//up

                            }
                            else if (quad == 1)
                            {
                                i = result[1];//left
                                j = result[3];//down
                            }
                            else if (quad == 2)
                            {
                                i = result[0];//right
                                j = result[3];//down
                            }
                            else
                            {
                                i = result[0];//right
                                j = result[2];//up
                            }
                        }
                        else if (GCodeCommand == 03)
                        {
                            //counter-clockwise

                            if (quad == 0)
                            {
                                i = result[0];//right
                                j = result[3];//down

                            }
                            else if (quad == 1)
                            {
                                i = result[0];//right
                                j = result[2];//up
                            }
                            else if (quad == 2)
                            {
                                i = result[1];//left
                                j = result[2];//up
                            }
                            else
                            {
                                i = result[1];//left
                                j = result[3];//down
                            }
                        }
                        if (x != null && y != null && z == null)
                        {
                            commandPartNew.Add('I' + i);
                            commandPartNew.Add('J' + j);
                        }
                        else if (x == null && y != null && z != null)
                        {
                            commandPartNew.Add('J' + i);
                            commandPartNew.Add('K' + j);
                        }
                        else if (x != null && y == null && z != null)
                        {
                            commandPartNew.Add('I' + i);
                            commandPartNew.Add('K' + j);
                        }
                    }
                    if (GCodeCommand != -1)//-1 means no Gcodecommand
                    {
                        xPast = x;
                        yPast = y;
                        zPast = z;
                    }
                    commandPart = (string[])commandPartNew.ToArray(typeof(string));

                }
                else if (GCodeCommand == 43)
                {
                    /* string[] newCommandPart = commandPart.Where(str => str != "G43").ToArray();
                     newCommandPart = commandPart.Where(str => str != "H1").ToArray();*/
                    ArrayList newCommandPart = new ArrayList(commandPart);
                    newCommandPart.Remove("G43");
                    newCommandPart.Remove("H1");
                    newCommandPart.Insert(0, "G43H1");
                    commandPart = (string[])newCommandPart.ToArray(typeof(string));

                }

                

                if (GCodeCommand == 2 || GCodeCommand == 3)
                {
                    commandPart = moveToFront(commandPart, "IXJYKZ");
                }

                if (GCodeCommand == 49 || ((GCodeCommand == 43 || GCodeCommand == 44) && commandPart.Length == 0))
                    toolHeightOffset = 0;

                commandPart = moveToFront(commandPart, "F");


                foreach (string indCommand in commandPart)
                {
                    if (indCommand[0] == 'X')
                    {
                        outputArr.Add(0x1e);
                        outputArr.Add(0);
                        byte[] pos = Converter.posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)), Form1.xOffset);

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'Y')
                    {
                        outputArr.Add(0x1f);
                        outputArr.Add(0);
                        byte[] pos = Converter.posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)), Form1.yOffset);

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'Z')
                    {
                        outputArr.Add(0x20);
                        outputArr.Add(0);
                        byte[] pos = Converter.posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)), Form1.zOffset);

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

                            byte[] pos = Converter.spindleToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));
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

                        byte[] pos = Converter.spindleToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'F')
                    {
                        outputArr.Add(0x18);
                        outputArr.Add(0);
                        byte[] pos = Converter.feedToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)));

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'I')
                    {
                        outputArr.Add(0x32);
                        outputArr.Add(0);
                        byte[] pos = Converter.posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)), Form1.xOffset);

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'j')
                    {
                        outputArr.Add(0x33);
                        outputArr.Add(0);
                        byte[] pos = Converter.posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)), Form1.yOffset);

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }

                    else if (indCommand[0] == 'K')
                    {
                        outputArr.Add(0x34);
                        outputArr.Add(0);
                        byte[] pos = Converter.posToBinary(int.Parse(indCommand.Substring(1, indCommand.Length - 1)), Form1.zOffset);

                        for (int i = 0; i < 8; i++)
                            outputArr.Add(pos[i]);
                    }
                    else if (indCommand[0] == 'H')
                    {
                        //check correct order!!!
                        if (GCodeCommand == 43)
                            toolHeightOffset = toolOffsetTable[int.Parse(indCommand.Substring(1))];
                        if (GCodeCommand == 44)
                            toolHeightOffset = -toolOffsetTable[int.Parse(indCommand.Substring(1))];
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
                ouput.Add(outputArr);
            }
            return ouput;
        }

        private static string[] moveToFront(string[] commandPart, string toFront)
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



        private static double[] quadratic(double a, double b, double c)
        {
            double[] answer = new double[2];
            answer[0] = (-b + Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / (2 * a);
            answer[1] = (-b - Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / (2 * a);

            return answer;
        }

        private static double[] RtoCentre(double r, double xstart, double ystart, double xend, double yend)
        {
            double[] answer = new double[4];
            double[] xresult = new double[2];
            double[] yresult = new double[2];
            double xdiff = xend - xstart;
            double ydiff = yend - ystart;

            double yA = Math.Pow(ydiff, 2) / Math.Pow(xdiff, 2) + 1;

            double yC = (Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2)) / (2 * xdiff);

            double yB = -(yC * ydiff / xdiff);

            yresult = quadratic(yA, yB, yC);

            double xA = Math.Pow(xdiff, 2) / Math.Pow(ydiff, 2) + 1;

            double xC = (Math.Pow(ydiff, 2) + Math.Pow(xdiff, 2)) / (2 * ydiff);

            double xB = -(xC * xdiff / ydiff);

            xresult = quadratic(xA, xB, xC);


            answer[0] = xresult[0] >= xresult[1] ? xresult[0] : xresult[1];
            answer[1] = yresult[0] >= yresult[1] ? yresult[0] : yresult[1];
            answer[2] = xresult[0] >= xresult[1] ? xresult[1] : xresult[0];
            answer[3] = yresult[0] >= yresult[1] ? yresult[1] : yresult[0];


            return answer;
        }

        private static int quadrant(double xstart, double ystart, double xend, double yend)
        {
            int quadResult = 0; // 0 = first quadrant, 1 = second quadrant, 2 = third quadrant,  3 = fourth quadrant
            bool xPositive = true, yPositive = true;
            if (xstart - xend >= 0)
            {
                xPositive = true;
            }
            else
            {
                xPositive = false;
            }

            if (ystart - yend >= 0)
            {
                yPositive = true;

            }
            else
            {
                yPositive = false;
            }

            if ((yPositive == true) && (xPositive == true))
            {
                quadResult = 0;
            }
            else if ((yPositive == true) && (xPositive == false))
            {
                quadResult = 1;
            }
            else if ((yPositive == false) && (xPositive == false))
            {
                quadResult = 2;
            }
            else if ((yPositive == false) && (xPositive == true))
            {
                quadResult = 3;
            }


            return quadResult;
        }
        private static string removeWhiteSpace(string input)
        {
            return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }


    }

}
