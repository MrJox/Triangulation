using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulation
{
    class Triangle
    {
        Point[] vertex;

        public Triangle(Point a, Point b, Point c)
        {
            vertex = new Point[3];
            vertex[0] = a;
            vertex[1] = b;
            vertex[2] = c;
        }
        public void Draw(Graphics Canvas)
        {
            Pen pen = new Pen(Color.Green);
            Brush brush = pen.Brush;
            Canvas.DrawPolygon(pen, vertex);
        }
        public static double Area(Point a, Point b, Point c)
        {
            return (a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)) / 2;
        }
        public static bool IsInside(Point v1, Point v2, Point v3, Point test)
        {
            bool a, b, c;

            a = Area(test, v1, v2) < 0.0 ? true : false;
            b = Area(test, v2, v3) < 0.0 ? true : false;
            c = Area(test, v3, v1) < 0.0 ? true : false;
            return ((a == b) && (a == c));
        }
    }

    class Triangulate
    {
        List<Point> points;
        List<Triangle> triangles;
        int sign = 1;

        public Triangulate(List<Point> inpoints)
        {
            points = inpoints;
            triangles = new List<Triangle>();
        }

        /* Very simple ear clipping algorithm */
        public void EarTrimming(Graphics canvas)
        {
            bool canTriangulate = true;

            //if (points[0].X < points[1].X)
            //    sign = -1;
    
            while (canTriangulate)
            {
                canTriangulate = false;
                for (int i = 1; i < points.Count - 1; i++)
                {
                    //if (sign == -1)
                    //{
                    //if (IsEar)
                        if (IsEar(i) && !IsInside(i))
                        {
                            triangles.Add(new Triangle(points[i - 1], points[i], points[i + 1]));
                            points.RemoveAt(i);
                            canTriangulate = true;
                        }
                    //}
                    //else
                    //{
                    //    if (!IsEar(i) && !IsInside(i))
                    //    {
                    //        triangles.Add(new Triangle(points[i - 1], points[i], points[i + 1]));
                    //        points.RemoveAt(i);
                    //        canTriangulate = true;
                    //    }
                    //}
                }
            }
        }

        /* Check is there any point inside the triangle */
        private bool IsInside(int index)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (i >= index - 1 && i <= index + 1) continue;
                if (Triangle.IsInside(points[index - 1], points[index], points[index + 1], points[i])) return true;
            }

            return false;
        }

        /* Check the point is an ear? */
        private bool IsEar(int index)
        {
            if (Triangle.Area(points[index - 1], points[index], points[index + 1]) <= 0.0)
                return true;
            else if (Triangle.Area(points[index - 1], points[index], points[index + 1]) >= 0.0)
                return true;
            else return false;

        }

        /* Draw the triangles */
        public void DrawTriangles(Graphics canvas)
        {
            foreach (Triangle tri in triangles)
            {
                tri.Draw(canvas);
            }
        }
    }
}
