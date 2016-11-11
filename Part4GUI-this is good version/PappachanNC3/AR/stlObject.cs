using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace iWindow.AR
{
    class readStlObj
    {
        public ArrayList objects { get; } = new ArrayList();

        public void readStl(string file = "stl.txt")
        {
            //read the stl file
            StreamReader sr = new StreamReader(file);

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
