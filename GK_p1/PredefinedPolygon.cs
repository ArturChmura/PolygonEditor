using GK_p1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GK_p1
{
    internal static class Predefined
    {
        public static Polygon GetPolygon()
        {
            var polygon = new Polygon();

            var vertexList = new List<Vertex>() { new Vertex(10, 10), new Vertex(10, 400), new Vertex(350, 500), new Vertex(450, 300), new Vertex(490, 400), new Vertex(215, 180), new Vertex(200, 40) };
            var edgesList = new List<Edge>();
            for (int i = 0; i < vertexList.Count; i++)
            {
                edgesList.Add(new Edge(vertexList[i], vertexList[(i + 1) % vertexList.Count]));
            }

            for (int i = 0; i < edgesList.Count; i++)
            {
                polygon.AddEdge(edgesList[i]);
            }

            edgesList[0].MakeVertical();
            edgesList[1].MakeHorizontal();
            edgesList[2].FixLength(edgesList[2].Distance);
            edgesList[^1].FixLength(edgesList[^1].Distance);
            return polygon;
        }
    }
}
