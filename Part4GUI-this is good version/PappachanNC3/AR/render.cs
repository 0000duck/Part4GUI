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
using iWindow.AR;

namespace iWindow.AR
{
    class render
    {
        //0,0,0 position is 112.871, -17.592,57.106 is machine positions
        //to find the camera co-ordinates, subtract the above position from the current machine position
        public static double camX=-112.871, camY=17.592, camZ=-57.106;

        //class scope vars
        public float[] rotateArr;
        public float[] translateArr;
        public float[] cameraPar;

        public bool camInit = false;

        public static float Xoff, Yoff, Zoff;

        //public static float toolXoff = -0, toolYoff = -0, toolZoff = 0;
        public static float toolXoff = -43, toolYoff = -33, toolZoff = 57;

        bool bgLoaded = false;
        int BGtextureID;
        int id;

        public cameraCalibrate cc;

        public static Bitmap currentBMP;
        public static bool hasFirst;

        //ui elements
        OpenTK.GLControl glControl1;

        ArrayList badVolumes;

        ArrayList objects;
        WorkPiece wp;

        public ArrayList lineList;
        

        public double? prevX = null, prevY = null, prevZ = null;

        public render(OpenTK.GLControl gc)
        {

            glControl1 = gc;

            lineList = new ArrayList();

            readStlObj rso = new readStlObj();
            rso.readStl();
            objects = rso.objects;

            cc = new cameraCalibrate();
            cc.loadCalibration();
            cameraPar = cc.cameraPar;
            rotateArr = cc.rotateArr;
            translateArr = cc.translateArr;

            wp = new WorkPiece(-64, -36, -8, 240, 190, 8, 1.5f);

            badVolumes = new ArrayList();
            badVolumes.Add(new badVolume(-100, 00, -100, 100, -5, 40));
            badVolumes.Add(new badVolume(240, 340, -100, 100, -5, 40));
            badVolumes.Add(new badVolume(-100, 300, -100, 200, -200, -10));

            cameraFeed.loadCamera(Form1.camIP);

            int id = GL.GenTexture();
        }

        public void addLine()
        {
            if (Form1.currentX0 != null && Form1.currentY0 != null && Form1.currentZ0 != null)
            {
                if (prevX != null)
                {
                    if (Form1.currentX0 != prevX || Form1.currentY0 != prevY || Form1.currentZ0 != prevZ)
                    {
                        line temp = new line(Form1.currentX0.Value, Form1.currentY0.Value, Form1.currentZ0.Value, prevX.Value, prevY.Value, prevZ.Value);
                        lineList.Add(temp);
                    }
                }

                prevX = Form1.currentX0;
                prevY = Form1.currentY0;
                prevZ = Form1.currentZ0;

                
            }
        }

        public void checkVol()
        {
            foreach (badVolume badVol in badVolumes)
            {
                if (Form1.currentX0 != null && Form1.currentY0 != null && Form1.currentZ0 != null && Form1.offsetSet)
                {

                    if (badVol.test(Form1.currentX.Value, Form1.currentY.Value, Form1.currentZ.Value))
                    {
                        Form1.writeErr("Outside safe milling volume");
                    }

                }
            }
        }

        public void removePoint()
        {
            float rad = 5;
            if (Form1.currentX0.HasValue && Form1.currentY0.HasValue && Form1.currentZ0.HasValue)
            {
                //converts current position into camera co-ord
                double xPoint = Form1.currentX0.Value - 188 - toolXoff;
                double yPoint = Form1.currentY0.Value - 50 - toolYoff;
                double zPoint = Form1.currentZ0.Value - toolZoff;

                int maxZ = Convert.ToInt32((zPoint - wp.locZ) / wp.resolution);
                for (int k=wp.zLen-1; k > maxZ && k>=0;k--)
                {
                    for (int j =0; j < wp.yLen; j++)
                    {

                        for (int i = 0; i < wp.xLen; i++)
                        {
                            if ((i * wp.resolution + wp.locX - xPoint) * (i * wp.resolution + wp.locX - xPoint) + (j * wp.resolution + wp.locY - yPoint) * (j * wp.resolution + wp.locY - yPoint) < rad*rad)
                                wp.removePoint(i, j, k);
                        }


                    }



                }


            }

        }


        public void draw()
        {
            if (Form1.currentX0 != null && Form1.currentY0 != null && Form1.currentZ0 != null)
            {
                Yoff = (float)Form1.currentY0 - 129.9f;
                Xoff = (float)Form1.currentX0 - 289.9f;
                Zoff = -(float)Form1.currentZ0 + 140f;
            }

            addLine();
            removePoint();
            checkVol();

            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            displayBackground();

            float far = 800, near = 50f;




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

            
            /*
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
            GL.Vertex3(0, 31, 0);


            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(31, 0, 0);
            GL.Vertex3(0, 0, 0);

            GL.End();
            */
            

            if (camInit == true)
            {

                if (Form1.axisDisplay)
                {
                    if (Form1.currentX0 != null && Form1.currentY0 != null && Form1.currentZ0 != null)
                    {

                        GL.LineWidth(5.5f);
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                        GL.Color3(.99f, 0.1f, 0.1f);
                        GL.Begin(PrimitiveType.Lines);
                        GL.Vertex3(Xoff, Yoff, Zoff);
                        GL.Vertex3(Xoff, Yoff, Zoff + 93);

                        GL.End();

                        GL.Color3(0.1f, 0.99f, 0.1f);
                        GL.Begin(PrimitiveType.Lines);
                        GL.Vertex3(Xoff, Yoff, Zoff);
                        GL.Vertex3(Xoff - 93, Yoff, Zoff);


                        GL.Color3(.1f, 0.1f, 0.99f);
                        GL.Begin(PrimitiveType.Lines);

                        GL.Vertex3(Xoff, Yoff, Zoff);
                        GL.Vertex3(Xoff, Yoff - 93, Zoff);

                        GL.End();


                    }
                }


                if (Form1.physicalToolpath)
                {
                    foreach (line ln in lineList)
                    {
                        GL.LineWidth(2.5f);
                        GL.Color3(Color.Blue);
                        GL.Begin(PrimitiveType.Lines);
                        GL.Vertex3(-ln.startX + Form1.currentX0.Value + 188 + toolXoff - 289.9, -ln.startY + Form1.currentY0.Value + toolYoff + 50 - 129.9, (ln.startZ - Form1.currentZ0.Value + 140 - toolZoff));
                        GL.Vertex3(-ln.endX + Form1.currentX0.Value + 188 + toolXoff - 289.9, -ln.endY + Form1.currentY0.Value + toolYoff + 50 - 129.9, (ln.endZ - Form1.currentZ0.Value + 140 - toolZoff));

                        GL.End();
                    }
                }

                if (Form1.tooltipDisplay)
                {
                    GL.LineWidth(20f);
                    line Drill = new line(Form1.currentX0.Value, Form1.currentY0.Value, Form1.currentZ0.Value, Form1.currentX0.Value, Form1.currentY0.Value, Form1.currentZ0.Value + 50);
                    GL.Color3(Color.Silver);
                    GL.Begin(PrimitiveType.Lines);
                    GL.Vertex3(-Drill.startX + Form1.currentX0.Value + 188 + toolXoff - 289.9, -Drill.startY + Form1.currentY0.Value + toolYoff + 50 - 129.9, (Drill.startZ - Form1.currentZ0.Value + 140 - toolZoff));
                    GL.Vertex3(-Drill.endX + Form1.currentX0.Value + 188 + toolXoff - 289.9, -Drill.endY + Form1.currentY0.Value + toolYoff + 50 - 129.9, (Drill.endZ - Form1.currentZ0.Value + 140 - toolZoff));

                    GL.End();
                }


                GL.Enable(EnableCap.DepthTest);
                GL.DepthFunc(DepthFunction.Lequal);

                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.Enable(EnableCap.Blend);

                if (Form1.badVolDisplay)
                {
                    foreach (badVolume badVol in badVolumes)
                    {

                        Color a = Color.FromArgb(160, 255, 0, 0);
                        Color b = Color.FromArgb(160, 225, 25, 25);
                        Color c = Color.FromArgb(160, 195, 50, 50);
                        Color d = Color.FromArgb(160, 225, 25, 25);

                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

                        GL.Begin(PrimitiveType.Quads);
                        GL.Color4(a);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Color4(b);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Color4(c);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.Color4(d);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.Quads);
                        GL.Color4(a);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Color4(b);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Color4(c);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.Color4(d);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.Quads);
                        GL.Color4(a);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Color4(b);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.Color4(c);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Color4(d);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.Quads);
                        GL.Color4(a);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Color4(b);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Color4(c);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.Color4(d);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.Quads);
                        GL.Color4(a);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Color4(b);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Color4(c);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.Color4(d);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.Quads);
                        GL.Color4(a);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Color4(b);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.Color4(c);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Color4(d);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.End();

                        Color black = Color.FromArgb(0, 0, 0);
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                        GL.Color4(black);
                        GL.LineWidth(1.2f);


                        GL.Begin(PrimitiveType.LineLoop);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.LineLoop);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.LineLoop);
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.LineLoop);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.LineLoop);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Vertex3(badVol.xMax+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.End();

                        GL.Begin(PrimitiveType.LineLoop);
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMin+Zoff );
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMin+Zoff );
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMax+Yoff-50, badVol.zMax+Zoff );
                        GL.Vertex3(badVol.xMin+Xoff- 180, badVol.yMin+Yoff-50, badVol.zMax+Zoff );
                        GL.End();

                        GL.End();

                        }

                    }


                
                
  
                
                GL.LineWidth(1.8f);



                if (Form1.matRemove)
                {
                    wp.draw();
                    wp.draw2();
                }



                if (Form1.cadModel)
                {

                    Color black = Color.FromArgb(0, 0, 0);


                    Color a = Color.FromArgb(160, 119, 136, 153);
                    Color b = Color.FromArgb(160, 112, 138, 144);
                    Color c = Color.FromArgb(160, 49, 79, 79);



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

                            if (Form1.currentX0 != null && Form1.currentY0 != null && Form1.currentZ0 != null)
                            {
                                GL.Color4(a);
                                GL.Vertex3(-fct.points[0].x + Xoff, -fct.points[0].y + Yoff, fct.points[0].z + Zoff);
                                GL.Color4(b);
                                GL.Vertex3(-fct.points[1].x + Xoff, -fct.points[1].y + Yoff, fct.points[1].z + Zoff);
                                GL.Color4(c);
                                GL.Vertex3(-fct.points[2].x + Xoff, -fct.points[2].y + Yoff, fct.points[2].z + Zoff);

                                GL.End();
                            }
                            else
                            {
                                GL.Color4(a);
                                GL.Vertex3(-fct.points[0].x, -fct.points[0].y, fct.points[0].z);
                                GL.Color4(b);
                                GL.Vertex3(-fct.points[1].x, -fct.points[1].y, fct.points[1].z);
                                GL.Color4(c);
                                GL.Vertex3(-fct.points[2].x, -fct.points[2].y, fct.points[2].z);

                                GL.End();
                            }

                            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                            GL.Color4(black);
                            GL.Begin(PrimitiveType.LineLoop);

                            GL.Vertex3(-fct.points[0].x + Xoff, -fct.points[0].y + Yoff, fct.points[0].z + Zoff);
                            GL.Vertex3(-fct.points[1].x + Xoff, -fct.points[1].y + Yoff, fct.points[1].z + Zoff);
                            GL.Vertex3(-fct.points[2].x + Xoff, -fct.points[2].y + Yoff, fct.points[2].z + Zoff);

                            GL.End();

                        }


                    }

                }



                GL.Disable(EnableCap.Blend);
            }

            glControl1.Refresh();// redraws and updates



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

            GL.BindTexture(TextureTarget.Texture2D, id);

            // We will not upload mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // We can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);


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
            GL.Ortho(0.0f, 816, 612.0f, 0.0f, 050f, 800);


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
            BGtextureID = LoadTexture(currentBMP);


            GL.BindTexture(TextureTarget.Texture2D, BGtextureID);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0, 0.0); GL.Vertex3(0.0, 0.0, -799);
            GL.TexCoord2(1.0, 0.0); GL.Vertex3(800.0, 0.0, -799);
            GL.TexCoord2(1.0, 1.0); GL.Vertex3(800.0, 600.0, -799);
            GL.TexCoord2(0.0, 1.0); GL.Vertex3(0.0, 600.0, -799);
            GL.End();


        }
    }

    class line
    {
        public double startX, startY, startZ, endX, endY, endZ;

        public line(double xIn1, double yIn1, double zIn1, double xIn2, double yIn2, double zIn2)
        {
            startX = xIn2;
            startY = yIn2;
            startZ = zIn2;

            endX = xIn1;
            endY = yIn1;
            endZ = zIn1;
        }
    }

    

    
}

