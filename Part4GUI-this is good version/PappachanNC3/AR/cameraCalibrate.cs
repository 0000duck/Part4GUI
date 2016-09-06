using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Collections;
using System.Drawing.Imaging;
using System.Xml;
using System.Xml.Serialization;

namespace PappachanNC3.AR
{
    class cameraCalibrate
    {
        //class scope vars
        public float[] rotateArr;
        public float[] translateArr;
        public float[] cameraPar;

        private void CalibrateCamera()
        {
            const int width = 5;//5 //width of chessboard no. squares in width - 1
            const int height = 5;//5 // heght of chess board no. squares in heigth - 1
            Size patternSize = new Size(width, height); //size of chess board to be detected

            MCvPoint3D32f[][] corners_object_list = new MCvPoint3D32f[6][];
            PointF[][] corners_points_list = new PointF[6][];


            for (int k = 0; k < 6; k++)
            {

                corners_object_list[k] = new MCvPoint3D32f[width * height];
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        corners_object_list[k][5 * i + j] = new MCvPoint3D32f((4 - i) * 29, (4 - j) * 29, 0);
                    }
                }
            }





            var output = new Emgu.CV.Util.VectorOfPointF();
            Size smallerPicSize = new Size(816, 612);

            for (int k = 1; k <= 6; k++)
            {
                Mat imgCam = new Mat(k + ".jpg", LoadImageType.Unchanged);//load picture of chessboard
                Mat smallerPic = new Mat();

                Size PicSize = new Size(3264, 2448);
                CvInvoke.Resize(imgCam, smallerPic, smallerPicSize);

                if (k == 1)
                    smallerPic.Save("small1.jpg");

                //CvInvoke.Imshow("small", smallerPic);

                bool found = CvInvoke.FindChessboardCorners(smallerPic, patternSize, output);//find chessboard
                Console.WriteLine("found:" + found);
                corners_points_list[k - 1] = output.ToArray();
            }

            for (int i = 0; i < output.Size; i++)
            {
                Console.WriteLine(corners_points_list[0].GetValue(i));
            }

            Mat cameraMat = new Mat();
            Mat distorCoef = new Mat();
            Mat[] rotationVec = new Mat[6];

            Mat[] translationVec = new Mat[6];
            for (int k = 0; k < 6; k++)
            {
                translationVec[k] = new Mat();
                rotationVec[k] = new Mat();
            }

            MCvTermCriteria criteria = new MCvTermCriteria();

            double rms = CvInvoke.CalibrateCamera(corners_object_list, corners_points_list, smallerPicSize, cameraMat, distorCoef, CalibType.RationalModel, criteria, out rotationVec, out translationVec);


            cameraPar = new float[9];
            double[] cameraParDouble = new Double[9];
            cameraMat.CopyTo(cameraParDouble);
            for (int i = 0; i < 9; i++)
            {
                cameraPar[i] = (float)cameraParDouble[i];
            }


            //1 by 14 array of distortion coeff, only first 8 important
            double[] distortArr = new double[14];
            distorCoef.CopyTo(distortArr);

            //1 by 3 array of rotate Matrix
            rotateArr = new float[9];
            Mat rotationMatrix = new Mat();
            //need to flip stuff
            //double[] rv = new double[3];
            //rotationVec[0].CopyTo(rv);
            //rv[1] = -1.0f * rv[1]; rv[2] = -1.0f * rv[2];
            //rotationVec[0].SetTo(rv);
            CvInvoke.Rodrigues(rotationVec[0], rotationMatrix);
            double[] rotateArrDouble = new double[9];
            rotationMatrix.CopyTo(rotateArrDouble);
            for (int i = 0; i < 9; i++)
            {
                rotateArr[i] = (float)rotateArrDouble[i];
            }


            //1 by 3 array of translate Matrix
            translateArr = new float[3];
            double[] translateArrDouble = new double[3];
            translationVec[0].CopyTo(translateArrDouble);
            for (int i = 0; i < 3; i++)
            {
                translateArr[i] = (float)translateArrDouble[i];
            }


            for (int i = 0; i < 3; i++)
                Console.WriteLine(rotateArr[i]);

            for (int i = 0; i < 3; i++)
                Console.WriteLine(translateArr[i]);

            //CvInvoke.Imshow("chessboard", imgCam);

            Console.WriteLine(rms);

            FileStorage fs = new FileStorage("cameraMat.txt", FileStorage.Mode.Write);
            fs.Write(cameraMat);
            fs.ReleaseAndGetString();
            fs = new FileStorage("distort.txt", FileStorage.Mode.Write);
            fs.Write(distorCoef);
            fs.ReleaseAndGetString();
        }

        public void loadCalibration(string file = "cameraMat.txt")
        {
            FileStorage fs = new FileStorage(file, FileStorage.Mode.Read);
            Mat cameraMat = new Mat();
            fs.GetFirstTopLevelNode().ReadMat(cameraMat);
            fs = new FileStorage("distort.txt", FileStorage.Mode.Read);
            Mat distortMat = new Mat();
            fs.GetFirstTopLevelNode().ReadMat(distortMat);

            cameraPar = new float[9];
            double[] cameraParDouble = new Double[9];
            cameraMat.CopyTo(cameraParDouble);
            for (int i = 0; i < 9; i++)
            {
                cameraPar[i] = (float)cameraParDouble[i];
            }

            const int width = 5;//5 //width of chessboard no. squares in width - 1
            const int height = 5;//5 // heght of chess board no. squares in heigth - 1
            Size patternSize = new Size(width, height); //size of chess board to be detected

            MCvPoint3D32f[] corners_object_list = new MCvPoint3D32f[width * height];
            PointF[] corners_points_list = new PointF[width * height];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    corners_object_list[5 * i + j] = new MCvPoint3D32f((4 - i) * 29, (4 - j) * 29, 0);
                }
            }


            var output = new Emgu.CV.Util.VectorOfPointF();
            Size smallerPicSize = new Size(816, 612);


            Mat imgCam = new Mat("1.jpg", LoadImageType.Unchanged);//load picture of chessboard
            Mat smallerPic = new Mat();

            Size PicSize = new Size(3264, 2448);
            CvInvoke.Resize(imgCam, smallerPic, smallerPicSize);

            bool found = CvInvoke.FindChessboardCorners(smallerPic, patternSize, output);//find chessboard
            Console.WriteLine("found:" + found);
            corners_points_list = output.ToArray();


            Mat rotationVec = new Mat();
            Mat translationVec = new Mat();

            CvInvoke.SolvePnP(corners_object_list, corners_points_list, cameraMat, distortMat, rotationVec, translationVec);

            //1 by 3 array of rotate Matrix
            rotateArr = new float[9];
            Mat rotationMatrix = new Mat();
            CvInvoke.Rodrigues(rotationVec, rotationMatrix);
            double[] rotateArrDouble = new double[9];
            rotationMatrix.CopyTo(rotateArrDouble);
            for (int i = 0; i < 9; i++)
            {
                rotateArr[i] = (float)rotateArrDouble[i];
            }

            //1 by 3 array of translate Matrix
            translateArr = new float[3];
            double[] translateArrDouble = new double[3];
            translationVec.CopyTo(translateArrDouble);
            for (int i = 0; i < 3; i++)
            {
                translateArr[i] = (float)translateArrDouble[i];
            }

        }
    }
    
}
