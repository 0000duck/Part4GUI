using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWindow
{
    class Converter
    {
        public static byte[] feedToBinary(double pos)
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

        public static byte[] posToBinary(double pos, double offset)
        {
            //= 1,517,068.10ln(x) + 1,062,166,905.73
            double first4 = 1517068.10 * Math.Log(pos-offset) + 1062166905.73;
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

        public static byte[] spindleToBinary(double spindle)
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


        public static string binaryToString(byte[] byteArray, int startPos)
        {

            int[] point = new int[8];
            for (int i = 0; i < 8; i++)
                point[i] = byteArray[startPos + i];

            string output = binaryToPosLin(point);

            return output;
        }





        public static string binaryToPosLin(int[] binaryPoint)
        {
            long intPos = (binaryPoint[4]) + (binaryPoint[5] * 256L) + (binaryPoint[6] * 256 * 256L) + (binaryPoint[7] * 256L * 256 * 256);

            double doublePos = intPos - 1062166905.73;
            doublePos = (double)(doublePos) / (double)1517068.10;
            //= 1,517,068.10ln(x) + 1,062,166,905.73
            return String.Format("{0:0.0000}", Math.Exp(doublePos));
        }

        public static string binaryToSpindle(int[] binaryPoint)
        {
            long intPos = (binaryPoint[4]) + (binaryPoint[5] * 256L) + (binaryPoint[6] * 256 * 256L) + (binaryPoint[7] * 256L * 256 * 256);

            double doublePos = intPos - 1066427853.40808;
            doublePos = (double)(doublePos) / (double)1515047.22522;
            //y=1,515,047.22522ln(x) + 1,066,427,853.40808
            return String.Format("{0:0.0000}", Math.Exp(doublePos));
        }

        public static string binaryToFeed(int[] feed)
        {
            long intFeed = (feed[4]) + (feed[5] * 256L) + (feed[6] * 256 * 256L) + (feed[7] * 256L * 256 * 256);

            double doubleFeed = intFeed - 1055979950.10;
            doubleFeed = (double)(doubleFeed) / (double)1514611.88;
            //y = 1,514,611.88ln(x) + 1,055,979,950.10
            return String.Format("{0:0.0000}", Math.Exp(doubleFeed));
        }
    }
}
