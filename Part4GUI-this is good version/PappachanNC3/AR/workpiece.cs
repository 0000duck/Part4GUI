using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OpenTK.Graphics.OpenGL;

namespace PappachanNC3.AR
{
    class WorkPiece
    {
        public float locX, locY, locZ;

        public int xLen, yLen, zLen;

        public float resolution;

        public BitArray cubeArray;

        public ArrayList faceArray;

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
            faceArray = new ArrayList();

            //fill the face array
            //top, bot
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    faceArray.Add(new CubeFace(locX, locY, locZ,i, j, 0,resolution, 1));
                    faceArray.Add(new CubeFace(locX, locY, locZ, i, j, zLen - 1, resolution, 2));

                }
            }
            //left,right
            for (int i = 0; i < yLen; i++)
            {
                for (int j = 0; j < zLen; j++)
                {
                    faceArray.Add(new CubeFace(locX, locY, locZ, 0, i, j, resolution, 3));
                    faceArray.Add(new CubeFace(locX, locY, locZ, xLen -1, i, j, resolution, 4));

                }
            }
            //front 
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < zLen; j++)
                {
                    faceArray.Add(new CubeFace(locX, locY, locZ, i, 0, j, resolution, 5));
                    faceArray.Add(new CubeFace(locX, locY, locZ, i, yLen-1,j, resolution, 6));

                }
            }
        }

        public void draw()
        {
            foreach (CubeFace face in faceArray)
            {
                GL.Begin(PrimitiveType.Quads);
                if (face.face == 1)
                {
                    GL.Vertex3(face.x, face.y,face.z);
                    GL.Vertex3(face.x + resolution, face.y, face.z);
                    GL.Vertex3(face.x + resolution, face.y + resolution, face.z);
                    GL.Vertex3(face.x, face.y + resolution, face.z);
                }
                else if (face.face == 3)
                {
                    GL.Vertex3(face.x, face.y, face.z);
                    GL.Vertex3(face.x, face.y + resolution, face.z);
                    GL.Vertex3(face.x, face.y + resolution, face.z + resolution);
                    GL.Vertex3(face.x, face.y, face.z + resolution);
                }
                else if (face.face == 4)
                {
                    GL.Vertex3(face.x + resolution, face.y, face.z);
                    GL.Vertex3(face.x + resolution, face.y, face.z + resolution);
                    GL.Vertex3(face.x + resolution, face.y + resolution, face.z + resolution);
                    GL.Vertex3(face.x + resolution, face.y + resolution, face.z);
                }
                else if (face.face == 5)
                {
                    GL.Vertex3(face.x, face.y, face.z);
                    GL.Vertex3(face.x, face.y, face.z + resolution);
                    GL.Vertex3(face.x + resolution, face.y, face.z + resolution);
                    GL.Vertex3(face.x + resolution, face.y, face.z);
                }

                GL.End();
            }
        }

    }

    class CubeFace
    {
        //1-6, top, bot, left, right, front, back
        public int face = 0;
        public float x, y, z;

        public CubeFace(float xPos, float yPos, float zPos,int xIn, int yIn, int zIn, float resolution,int faceIn)
        {
            x = xIn*resolution + xPos;
            y = yIn * resolution + yPos;
            z = zIn * resolution + zPos;
            face = faceIn;
        }
    }
}
