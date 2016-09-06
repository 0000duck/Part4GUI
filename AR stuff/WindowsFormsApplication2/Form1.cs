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
using MjpegProcessor;



namespace WindowsFormsApplication2
{

    public partial class Form1 : Form
    {
        //class scope vars
        float[] rotateArr;
        float[] translateArr;
        float[] cameraPar;

        double xPos = 0;

        Bitmap currentBMP;

        bool bgLoaded = false;
        int BGtextureID;
        int arr1ID, arr2ID, arr3ID;

        bool loaded = false;
        bool running = false;

        int skipped = 0;

        ArrayList objects;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            //read the stl file
            objects = new ArrayList();
            StreamReader sr = new StreamReader("stl.txt");

            while (!sr.EndOfStream)
            {
                stlObj currentObj = new stlObj();
                //obj layer
                string currentLine = sr.ReadLine().ToUpper();
                if (currentLine.Contains("SOLID"))
                    currentObj.name = currentLine.Substring(currentLine.IndexOf("SOLID") + 6);  //find name
                else
                    Console.WriteLine("invalid format error");
                while (!currentLine.Contains("ENDSOLID"))
                {
                    facet curretFacet = new facet();
                    currentLine = sr.ReadLine().ToUpper();
                    if (currentLine.Contains("FACET"))
                    {
                        sr.ReadLine();//outer loop
                        //p1
                        currentLine = sr.ReadLine();
                        string[] components = currentLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                        curretFacet.points[0] = new point(components[1], components[2], components[3]);


                        currentLine = sr.ReadLine();
                        components = currentLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                        curretFacet.points[1] = new point(components[1], components[2], components[3]);


                        currentLine = sr.ReadLine();
                        components = currentLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                        curretFacet.points[2] = new point(components[1], components[2], components[3]);
                        sr.ReadLine();//end loop
                        sr.ReadLine();//endfacet

                        //this is to ensure the triangle is facing the correct direction
                    }
                    else
                        Console.WriteLine("invalid format error");

                    currentObj.facets.Add(curretFacet);
                    currentObj.facetNum++;

                }
                objects.Add(currentObj);

                loaded = true;

            }


        }

        int LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            // We will not upload mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // We can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Bitmap bmp = new Bitmap(filename);
            currentBMP = bmp;
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            return id;
        }

        int LoadTexture(Bitmap bmp)
        {
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            // We will not upload mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // We can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Bitmap bmt2 = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);


            bmp.UnlockBits(bmp_data);

            return id;
        }

        private void displayBackground()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0f, 816, 612.0f, 0.0f, 100.0f, 500.0f);


            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Color3(Color.White);

            /*
            if (bgLoaded == false)
            {

                //arr1ID = LoadTexture("arrow.png");
                bgLoaded = true;
            }

            else

            
            BGtextureID = LoadTexture(currentBMP);
            */

            GL.Enable(EnableCap.Texture2D);
            Bitmap bmptmp = new Bitmap("small1.jpg");
            BGtextureID = LoadTexture(currentBMP);


            GL.BindTexture(TextureTarget.Texture2D, BGtextureID);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0, 0.0); GL.Vertex3(0.0, 0.0, -499);
            GL.TexCoord2(1.0, 0.0); GL.Vertex3(800.0, 0.0, -499);
            GL.TexCoord2(1.0, 1.0); GL.Vertex3(800.0, 600.0, -499);
            GL.TexCoord2(0.0, 1.0); GL.Vertex3(0.0, 600.0, -499);
            GL.End();


        }

        private void draw()
        {
            xPos += 0.1;
            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            displayBackground();

            float far = 500, near = 100f;




            OpenTK.Matrix4 RTMat = new OpenTK.Matrix4(
            rotateArr[0], rotateArr[1], rotateArr[2], translateArr[0],
            rotateArr[3], rotateArr[4], rotateArr[5], translateArr[1],
            rotateArr[6], rotateArr[7], rotateArr[8], translateArr[2],
            0, 0, 0, 1);

            OpenTK.Matrix4 reverse = new OpenTK.Matrix4(
            1, 0, 0, 0,
            0, -1, 0, 0,
            0, 0, -1, 0,
            0, 0, 0, 1);

            //not sure if needed
            OpenTK.Matrix4 landscape = new OpenTK.Matrix4(
            0, 1, 0, 0,
            -1, 0, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

            OpenTK.Matrix4 projection = new OpenTK.Matrix4(
            2 * cameraPar[0] / 816f, 0, 1 - (2 * cameraPar[2] / 816f), 0,
            0, 2 * cameraPar[4] / 612f, -1 + (2 * cameraPar[5] + 2) / 612f, 0,
            0, 0, -(far + near) / (far - near), -2 * far * near / (far - near),
            0, 0, -1, 0);

            OpenTK.Matrix4 projT = new OpenTK.Matrix4(
            2 * cameraPar[0] / 816f, 0, 1 - (2 * cameraPar[2] / 816f), 0,
            0, 2 * cameraPar[4] / 612f, -1 + (2 * cameraPar[5] + 2) / 612f, 0,
            0, 0, -(far + near) / (far - near), -2 * far * near / (far - near),
            0, 0, -1, 0);

            projT.Transpose();
            OpenTK.Matrix4 mvMat = OpenTK.Matrix4.Mult(reverse, RTMat);
            OpenTK.Matrix4 projMat2 = OpenTK.Matrix4.Mult(landscape, projection);
            OpenTK.Matrix4 mvp = OpenTK.Matrix4.Mult(projection, mvMat);


            // render graphics

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.LoadMatrix(ref projT.Row0.X);

            mvMat.Transpose();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.LoadMatrix(ref mvMat.Row0.X);




            GL.LineWidth(2.5f);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.Color3(Color.Green);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 100);

            GL.End();

            GL.Color3(Color.Aqua);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 100, 0);


            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(100, 0, 0);

            GL.End();




            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);

            Color a = Color.FromArgb(160, 255, 0, 0);
            Color b = Color.FromArgb(160, 225, 25, 25);
            Color c = Color.FromArgb(160, 195, 50, 50);


            Color black = Color.FromArgb(0, 0, 0);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.LineWidth(1.0f);
            foreach (stlObj obj in objects)
            {
                object[] facetArray = obj.facets.ToArray();
                for (int i = 0; i < obj.facetNum - 1; i++)
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    Object fcet = (facet)facetArray[i];
                    GL.Begin(PrimitiveType.Triangles);
                    facet fct = (facet)fcet;

                    GL.Color4(a);
                    GL.Vertex3(fct.points[0].x + float.Parse(textBox1.Text) + xPos, fct.points[0].y + float.Parse(textBox2.Text), fct.points[0].z + float.Parse(textBox3.Text));
                    GL.Color4(b);
                    GL.Vertex3(fct.points[1].x + float.Parse(textBox1.Text) + xPos, fct.points[1].y + float.Parse(textBox2.Text), fct.points[1].z + float.Parse(textBox3.Text));
                    GL.Color4(c);
                    GL.Vertex3(fct.points[2].x + float.Parse(textBox1.Text) + xPos, fct.points[2].y + float.Parse(textBox2.Text), fct.points[2].z + float.Parse(textBox3.Text));

                    GL.End();

                    /*
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    GL.Color3(black);
                    GL.Begin(PrimitiveType.Triangles);
                    GL.Vertex3(fct.points[0].x , fct.points[0].y, fct.points[0].z );
                    GL.Vertex3(fct.points[1].x , fct.points[1].y, fct.points[1].z );
                    GL.Vertex3(fct.points[2].x , fct.points[2].y, fct.points[2].z );

                    GL.End();
                    */
                }


            }



            GL.Disable(EnableCap.Blend);

            glControl1.Refresh();// redraws and updates
            Bitmap gl_image = TakeScreenshot();
            gl_image.Save("screenshot.bmp");



            glControl1.SwapBuffers();
        }

        public Bitmap TakeScreenshot()
        {
            int w = glControl1.ClientSize.Width;
            int h = glControl1.ClientSize.Height;
            Bitmap bmp = new Bitmap(w, h);
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(glControl1.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, w, h, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.VSync = true;//not fired
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ;//s glControl1.VSync = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(hasFirst)
            {
                currentBMP = MD.Bitmap;
                draw();
            }
        }

        bool hasFirst = false;

        MjpegDecoder MD;

        private void button3_Click(object sender, EventArgs e)
        {

             MD = new MjpegDecoder();

            MD.FrameReady += mjpeg_FrameReady;
            System.Uri a = new Uri("http://192.168.1.1:8081/");
            MD.ParseStream(a);
            
        }

        private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {

            MjpegDecoder md = (MjpegDecoder)sender;
            hasFirst = true;
            currentBMP = MD.Bitmap;
            draw();
            md.FrameReady -= mjpeg_FrameReady;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileStorage fs = new FileStorage("cameraMat.txt", FileStorage.Mode.Read);
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

    class stlObj
    {
        public string name = "";
        public int facetNum = 0;
        public ArrayList facets = new ArrayList();
    }

    class facet
    {
        public point[] points = new point[3];
        public void addPoints(point p1, point p2, point p3)
        {
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
        }
    }

    class point
    {
        public double x, y, z;

        public point(float xIn, float yIn, float zIn)
        {
            x = xIn;
            y = yIn;
            z = zIn;
        }

        public point(string xIn, string yIn, string zIn)
        {
            x = double.Parse(xIn);
            y = double.Parse(yIn);
            z = double.Parse(zIn);
        }

        public point(double xIn, double yIn, double zIn)
        {
            x = xIn;
            y = yIn;
            z = zIn;
        }



    }



}
