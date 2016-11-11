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
            double offsetPos = pos - offset;
            double first4;

            if (offsetPos <= 8)
                first4 = 1062224726.12274 * Math.Pow(offsetPos, 0.00140);
            else if (offsetPos <= 16)
                first4 = 132539.98333 * offsetPos + 1064321976.20000;
            else if (offsetPos <= 31)
                first4 = 67108.86618 * offsetPos + 1065353215.45735;
            else if (offsetPos <= 63)
                first4 = 33554.43349 * offsetPos + 1066401283.01994;
            else if (offsetPos <= 125)
                first4 = 16777.21649 * offsetPos + 1067450367.45910;
            else if (offsetPos <= 252)
                first4 = 8384.05963 * offsetPos + 1068499702.57274;
            else
                first4 = 4194.30443 * offsetPos + 1069547519.36565;


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
            double intPos = (binaryPoint[4]) + (binaryPoint[5] * 256L) + (binaryPoint[6] * 256 * 256L) + (binaryPoint[7] * 256L * 256 * 256);

            double posOut;

            if (intPos < 1065378381)
            {
                posOut = intPos / 1062224726.12274;
                posOut = Math.Pow(intPos,1.0/0.00140);
            }
            else if (intPos < 1066426957)
                posOut = (intPos - 1064321976.20000) / 132539.98333;
            else if (intPos < 1067433590)
                posOut = (intPos - 1065353215.45735) / 67108.86618;
            else if (intPos < 1068507332)
                posOut = (intPos - 1066401283.01994) / 33554.43349;
            else if (intPos < 1069547520)
                posOut = (intPos - 1067450367.45910) / 16777.21649;
            else if (intPos < 1070604484)
                posOut = (intPos - 1068499702.57274) / 8384.05963;
            else
                posOut = (intPos - 1069547519.36565) / 4194.30443;

            
            //= 1,517,068.10ln(x) + 1,062,166,905.73
            return String.Format("{0:0.0000}", posOut);
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
