﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Collections;
using System.Drawing.Imaging;
using PappachanNC3.AR;

namespace PappachanNC3.AR
{
    class render
    {
        //class scope vars
        public float[] rotateArr;
        public float[] translateArr;
        public float[] cameraPar;

        bool bgLoaded = false;
        int BGtextureID;

        //ui elements
        TextBox textBox1, textBox2, textBox3;
        OpenTK.GLControl glControl1;
        
        ArrayList objects;

        public render(TextBox txb1, TextBox txb2, TextBox txb3,OpenTK.GLControl gc)
        {
            textBox1 = txb1;
            textBox2 = txb2;
            textBox3 = txb3;

            glControl1 = gc;

            readStlObj rso = new readStlObj();
            objects = rso.objects;

            cameraCalibrate cc = new cameraCalibrate();
            cc.loadCalibration();
            cameraPar = cc.cameraPar;
            rotateArr = cc.rotateArr;
            translateArr = cc.translateArr;
        }


        public void draw()
        {
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
                    GL.Vertex3(fct.points[0].x + float.Parse(textBox1.Text), fct.points[0].y + float.Parse(textBox2.Text), fct.points[0].z + float.Parse(textBox3.Text));
                    GL.Color4(b);
                    GL.Vertex3(fct.points[1].x + float.Parse(textBox1.Text), fct.points[1].y + float.Parse(textBox2.Text), fct.points[1].z + float.Parse(textBox3.Text));
                    GL.Color4(c);
                    GL.Vertex3(fct.points[2].x + float.Parse(textBox1.Text), fct.points[2].y + float.Parse(textBox2.Text), fct.points[2].z + float.Parse(textBox3.Text));

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

        static int LoadTexture(string filename)
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

            if (bgLoaded == false)
            {

                GL.Enable(EnableCap.Texture2D);
                BGtextureID = LoadTexture("small1.jpg");
                bgLoaded = true;
            }

            GL.BindTexture(TextureTarget.Texture2D, BGtextureID);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0, 0.0); GL.Vertex3(0.0, 0.0, -499);
            GL.TexCoord2(1.0, 0.0); GL.Vertex3(816.0, 0.0, -499);
            GL.TexCoord2(1.0, 1.0); GL.Vertex3(816.0, 612.0, -499);
            GL.TexCoord2(0.0, 1.0); GL.Vertex3(0.0, 612.0, -499);
            GL.End();


        }
    }
}
