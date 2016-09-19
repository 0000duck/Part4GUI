using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace PappachanNC3.AR
{
    class WorkPiece
    {
        public float locX, locY, locZ;

        public int xLen, yLen, zLen;

        public float resolution;

        public BitArray cubeArray;

        public Hashtable faceArray;

        public WorkPiece(float xPos, float yPos, float zPos, float xSize, float ySize, float zSize, float resolutionIn)
        {
            locX = xPos;
            locY = yPos;
            locZ = zPos;

            resolution = resolutionIn;

            xLen = Convert.ToInt32(Math.Ceiling(xSize / resolution));
            yLen = Convert.ToInt32(Math.Ceiling(ySize / resolution));
            zLen = Convert.ToInt32(Math.Ceiling(zSize / resolution));

            cubeArray = new BitArray(xLen * yLen * zLen, true);
            faceArray = new Hashtable();

            //fill the face array
            //top, bot
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    List<CubeFace> tmp = new List<CubeFace>();
                    tmp.Add(new CubeFace(locX, locY, locZ, i, j, zLen - 1, resolution, 1));
                    faceArray.Add((zLen - 1) * xLen * yLen + j * xLen + i, tmp);
                    //faceArray.Add(new CubeFace(locX, locY, locZ, i, j, 0, resolution, 2));

                }
            }
            //left
            for (int i = 0; i < yLen; i++)
            {
                for (int j = 0; j < zLen; j++)
                {
                    int pos = j * xLen * yLen + i * xLen;
                    List<CubeFace> tmp;
                    if (faceArray.Contains(pos))
                    {
                        tmp = (List<CubeFace>)faceArray[pos];
                        faceArray.Remove(pos);
                    }
                    else
                        tmp = new List<CubeFace>();
                    tmp.Add(new CubeFace(locX, locY, locZ, 0, i, j, resolution, 3));
                    faceArray.Add(pos, tmp);

                }
            }
            //right
            for (int i = 0; i < yLen; i++)
            {
                for (int j = 0; j < zLen; j++)
                {
                    int pos = j * xLen * yLen + i * xLen + xLen - 1;
                    List<CubeFace> tmp;
                    if (faceArray.Contains(pos))
                    {
                        tmp = (List<CubeFace>)faceArray[pos];
                        faceArray.Remove(pos);
                    }
                    else
                        tmp = new List<CubeFace>();
                    tmp.Add(new CubeFace(locX, locY, locZ, xLen - 1, i, j, resolution, 4));
                    faceArray.Add(pos, tmp);

                }
            }
            //front 
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < zLen; j++)
                {
                    int pos = j * xLen * yLen + i;
                    List<CubeFace> tmp;
                    if (faceArray.Contains(pos))
                    {
                        tmp = (List<CubeFace>)faceArray[pos];
                        faceArray.Remove(pos);
                    }
                    else
                        tmp = new List<CubeFace>();
                    tmp.Add(new CubeFace(locX, locY, locZ, i, 0, j, resolution, 5));
                    faceArray.Add(pos, tmp);

                    //faceArray.Add(new CubeFace(locX, locY, locZ, i, yLen - 1, j, resolution, 6));

                }
            }
            
        }

        public void removePoint(int i, int j, int k)
        {
            int pos = k * xLen * yLen + j * xLen + i;
            if (cubeArray[pos])
            {
                cubeArray[pos] = false;
                faceArray.Remove(pos);

                //bot
                if (k > 0)
                {
                    int posNew = pos - xLen * yLen;
                    if (cubeArray[posNew])
                    {
                        if (faceArray.Contains(posNew))
                            ((List<CubeFace>)faceArray[posNew]).Add(new CubeFace(locX, locY, locZ, i, j, k - 1, resolution, 1));
                        else
                        {
                            List<CubeFace> tmp = new List<CubeFace>();
                            tmp.Add(new CubeFace(locX, locY, locZ, i, j, k - 1, resolution, 1));
                            faceArray.Add(posNew, tmp);
                        }
                    }
                }
                //left
                if (i > 0)
                {
                    int posNew = pos - 1;
                    if (cubeArray[posNew])
                    {
                        if (faceArray.Contains(posNew))
                            ((List<CubeFace>)faceArray[posNew]).Add(new CubeFace(locX, locY, locZ, i - 1, j, k, resolution, 4));
                        else
                        {
                            List<CubeFace> tmp = new List<CubeFace>();
                            tmp.Add(new CubeFace(locX, locY, locZ, i - 1, j, k, resolution, 4));
                            faceArray.Add(posNew, tmp);
                        }
                    }
                }
                //right
                if (i < xLen - 1)
                {
                    int posNew = pos + 1;
                    if (cubeArray[posNew])
                    {
                        if (faceArray.Contains(posNew))
                            ((List<CubeFace>)faceArray[posNew]).Add(new CubeFace(locX, locY, locZ, i + 1, j, k, resolution, 3));
                        else
                        {
                            List<CubeFace> tmp = new List<CubeFace>();
                            tmp.Add(new CubeFace(locX, locY, locZ, i + 1, j, k, resolution, 3));
                            faceArray.Add(posNew, tmp);
                        }
                    }
                }
                //front
                if (j < yLen - 1)
                {
                    int posNew = pos + xLen;
                    if (cubeArray[posNew])
                    {
                        if (faceArray.Contains(posNew))
                            ((List<CubeFace>)faceArray[posNew]).Add(new CubeFace(locX, locY, locZ, i, j + 1, k, resolution, 5));
                        else
                        {
                            List<CubeFace> tmp = new List<CubeFace>();
                            tmp.Add(new CubeFace(locX, locY, locZ, i, j + 1, k, resolution, 5));
                            faceArray.Add(posNew, tmp);
                        }
                    }
                }

            }
            ;
        }

        public void draw()
        {
            Color a = Color.FromArgb( 255, 60, 0);
            Color b = Color.FromArgb( 255, 60, 0);
            Color c = Color.FromArgb( 245, 90, 0);
            Color d = Color.FromArgb( 245, 90, 0);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point);
            GL.Color3(Color.Purple);

            

            if (Form1.currentX0 != null && Form1.currentY0 != null && Form1.currentZ0 != null)
            {
                foreach (DictionaryEntry faceLs in faceArray)
                {
                    foreach (CubeFace face in (List<CubeFace>)faceLs.Value)
                    {

                        GL.Begin(PrimitiveType.Quads);
                        if (face.face == 1)
                        {
                            GL.Color4(a);
                            GL.Vertex3(-face.x + render.Xoff, -face.y + render.Yoff, face.z + resolution + render.Zoff);
                            GL.Color4(b);
                            GL.Vertex3(-face.x + render.Xoff, -face.y + render.Yoff - resolution, face.z + resolution + render.Zoff);
                            GL.Color4(c);
                            GL.Vertex3(-face.x + render.Xoff - resolution, -face.y + render.Yoff - resolution, face.z + resolution + render.Zoff);
                            GL.Color4(d);
                            GL.Vertex3(-face.x + render.Xoff - resolution, -face.y + render.Yoff, face.z + resolution + render.Zoff);
                        }
                        else if (face.face == 3)
                        {
                            GL.Color4(a);
                            GL.Vertex3(-face.x + render.Xoff, -face.y + render.Yoff, face.z + render.Zoff);
                            GL.Color4(b);
                            GL.Vertex3(-face.x + render.Xoff, -face.y + render.Yoff - resolution, face.z + render.Zoff);
                            GL.Color4(c);
                            GL.Vertex3(-face.x + render.Xoff, -face.y + render.Yoff - resolution, face.z + resolution + render.Zoff);
                            GL.Color4(d);
                            GL.Vertex3(-face.x + render.Xoff, -face.y + render.Yoff, face.z + resolution + render.Zoff);
                        }
                        else if (face.face == 4)
                        {
                            GL.Color4(a);
                            GL.Vertex3(-face.x + render.Xoff - resolution, -face.y + render.Yoff, face.z + render.Zoff);
                            GL.Color4(b);
                            GL.Vertex3(-face.x + render.Xoff - resolution, -face.y + render.Yoff, face.z + resolution + render.Zoff);
                            GL.Color4(c);
                            GL.Vertex3(-face.x + render.Xoff - resolution, -face.y + render.Yoff - resolution, face.z + resolution + render.Zoff);
                            GL.Color4(d);
                            GL.Vertex3(-face.x + render.Xoff - resolution, -face.y + render.Yoff - resolution, face.z + render.Zoff);
                        }
                        else if (face.face == 5)
                        {
                            GL.Color4(a);
                            GL.Vertex3(-face.x + render.Xoff, -face.y + render.Yoff, face.z + render.Zoff);
                            GL.Color4(b);
                            GL.Vertex3(-face.x + render.Xoff, -face.y + render.Yoff, face.z + resolution + render.Zoff);
                            GL.Color4(c);
                            GL.Vertex3(-face.x + render.Xoff - resolution, -face.y + render.Yoff, face.z + resolution + render.Zoff);
                            GL.Color4(d);
                            GL.Vertex3(-face.x + render.Xoff - resolution, -face.y + render.Yoff, face.z + render.Zoff);
                        }
                    }

                    GL.End();
                }


            }
            else
            {
                foreach (DictionaryEntry faceLs in faceArray)
                {
                    foreach (CubeFace face in (List<CubeFace>)faceLs.Value)
                    {

                        GL.Begin(PrimitiveType.Quads);
                        if (face.face == 1)
                        {
                            GL.Color4(a);
                            GL.Vertex3(-face.x, -face.y, face.z + resolution);
                            GL.Color4(b);
                            GL.Vertex3(-face.x, -face.y - resolution, face.z + resolution);
                            GL.Color4(c);
                            GL.Vertex3(-face.x - resolution, -face.y - resolution, face.z + resolution);
                            GL.Color4(d);
                            GL.Vertex3(-face.x - resolution, -face.y, face.z + resolution);
                        }
                        else if (face.face == 3)
                        {
                            GL.Color4(a);
                            GL.Vertex3(-face.x, -face.y, face.z);
                            GL.Color4(b);
                            GL.Vertex3(-face.x, -face.y - resolution, face.z);
                            GL.Color4(c);
                            GL.Vertex3(-face.x, -face.y - resolution, face.z + resolution);
                            GL.Color4(d);
                            GL.Vertex3(-face.x, -face.y, face.z + resolution);
                        }
                        else if (face.face == 4)
                        {
                            GL.Color4(a);
                            GL.Vertex3(-face.x - resolution, -face.y, face.z);
                            GL.Color4(b);
                            GL.Vertex3(-face.x - resolution, -face.y, face.z + resolution);
                            GL.Color4(c);
                            GL.Vertex3(-face.x - resolution, -face.y - resolution, face.z + resolution);
                            GL.Color4(d);
                            GL.Vertex3(-face.x - resolution, -face.y - resolution, face.z);
                        }
                        else if (face.face == 5)
                        {
                            GL.Color4(a);
                            GL.Vertex3(-face.x, -face.y, face.z);
                            GL.Color4(b);
                            GL.Vertex3(-face.x, -face.y, face.z + resolution);
                            GL.Color4(c);
                            GL.Vertex3(-face.x - resolution, -face.y, face.z + resolution);
                            GL.Color4(d);
                            GL.Vertex3(-face.x - resolution, -face.y, face.z);
                        }
                    }

                    GL.End();
                }
            }
        }

    }

    class CubeFace
    {
        //1-6, top, bot, left, right, front, back
        public int face = 0;
        public float x, y, z;

        public CubeFace(float xPos, float yPos, float zPos, int xIn, int yIn, int zIn, float resolution, int faceIn)
        {
            x = xIn * resolution + xPos;
            y = yIn * resolution + yPos;
            z = zIn * resolution + zPos;
            face = faceIn;
        }
    }
}
