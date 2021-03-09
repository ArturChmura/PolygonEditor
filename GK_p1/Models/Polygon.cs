using GK_p1.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace GK_p1.Models
{
    public class Polygon : ICloneable
    {
        public Polygon()
        {
            this.Edges = new List<Edge>();
        }

        public List<Edge> Edges { get; set; }

        public bool IsComplete => this.Edges.Count > 2 && this.Edges[0].FirstVertex == this.Edges[^1].SecondVertex;

        public bool AddEdge(Edge edge)
        {
            if (this.IsComplete || (this.Edges.Count > 0 && edge.FirstVertex != this.Edges[^1].SecondVertex))
            {
                return false;
            }

            if (this.Edges.Count >= 2 && this.Edges[0].FirstVertex.Contains(edge.SecondVertex))
            {
                edge.SecondVertex = this.Edges[0].FirstVertex;
                this.Edges[0].PrevNeighbor = edge;
                edge.NextNeighbor = this.Edges[0];
            }

            this.Edges.Add(edge);
            if (this.Edges.Count > 1)
            {
                edge.PrevNeighbor = this.Edges[^2];
                this.Edges[^2].NextNeighbor = edge;
            }

            return true;
        }

        public void Draw(PaintEventArgs e, Brush brush)
        {
            for (int i = 0; i < this.Edges.Count; i++)
            {
                this.Edges[i].Draw(e, brush);
                this.Edges[i].FirstVertex.Draw(e, brush);
            }
        }

        public bool DeleteVertex(int index)
        {
            if (index >= this.Edges.Count || index < 0 || this.Edges.Count <= 3)
            {
                return false;
            }

            this.Edges[index].RemoveRestrictions();
            this.Edges.RemoveAt(index);
            if (index >= this.Edges.Count)
            {
                index = 0;
            }

            int prevIndex = index == 0 ? this.Edges.Count - 1 : index - 1;
            this.Edges[prevIndex].RemoveRestrictions();
            this.Edges[prevIndex].SecondVertex = this.Edges[index].FirstVertex;
            this.Edges[prevIndex].NextNeighbor = this.Edges[index];
            this.Edges[index].PrevNeighbor = this.Edges[prevIndex];

            return true;
        }

        public bool AddVertex(int index)
        {
            if (index >= this.Edges.Count || index < 0)
            {
                return false;
            }

            var edge = this.Edges[index];

            float x = (edge.FirstVertex.X + edge.SecondVertex.X) / 2;
            float y = (edge.FirstVertex.Y + edge.SecondVertex.Y) / 2;

            Vertex middleVertex = new Vertex(x, y);

            var newEdgeFirst = new Edge(edge.FirstVertex, middleVertex);
            var newEdgeSecond = new Edge(middleVertex, edge.SecondVertex);

            edge.PrevNeighbor.NextNeighbor = newEdgeFirst;
            edge.NextNeighbor.PrevNeighbor = newEdgeSecond;
            newEdgeFirst.PrevNeighbor = edge.PrevNeighbor;
            newEdgeFirst.NextNeighbor = newEdgeSecond;
            newEdgeSecond.PrevNeighbor = newEdgeFirst;
            newEdgeSecond.NextNeighbor = edge.NextNeighbor;

            for (int i = edge.FirstVertex.Restrictions.Count - 1; i >= 0; i--)
            {
                if (edge.FirstVertex.Restrictions[i].RestrictedEdge == edge)
                {
                    edge.FirstVertex.Restrictions.RemoveAt(i);
                }
            }

            for (int i = edge.SecondVertex.Restrictions.Count - 1; i >= 0; i--)
            {
                if (edge.SecondVertex.Restrictions[i].RestrictedEdge == edge)
                {
                    edge.SecondVertex.Restrictions.RemoveAt(i);
                }
            }

            this.Edges.RemoveAt(index);
            this.Edges.Insert(index, newEdgeSecond);
            this.Edges.Insert(index, newEdgeFirst);

            return true;
        }

        public bool Contains(Point v)
        {
            int sum = 0;

            for (int i = 0; i < this.Edges.Count; i++)
            {
                Edge e;
                if (this.Edges[i].FirstVertex.X > this.Edges[i].SecondVertex.X)
                {
                    e = new Edge(this.Edges[i].SecondVertex, this.Edges[i].FirstVertex);
                }
                else
                {
                    e = this.Edges[i];
                }

                if (e.FirstVertex.X < v.X && e.SecondVertex.X >= v.X)
                {
                    if (this.VectorProduct(e.FirstVertex, e.SecondVertex, v) < 0)
                    {
                        sum++;
                    }
                }

                // vertical line above v
                if (e.FirstVertex.X == e.SecondVertex.X && e.FirstVertex.X == v.X)
                {
                    Edge prev, next, p = e;
                    while (p.SecondVertex.X == v.X)
                    {
                        p = e.NextNeighbor;
                    }

                    next = p;
                    p = e;
                    while (p.FirstVertex.X == v.X)
                    {
                        p = e.PrevNeighbor;
                    }

                    prev = p;

                    bool isPrevLeft = prev.FirstVertex.X < v.X;
                    bool isNextLeft = next.SecondVertex.X < v.X;
                    if ((isNextLeft && !isPrevLeft) || (!isNextLeft && isPrevLeft))
                    {
                        sum++;
                    }
                }
            }

            return (sum & 1) == 1;
        }

        private float VectorProduct(Vertex p0, Vertex p1, Point p2)
        {
            return ((p1.X - p0.X) * (p2.Y - p0.Y)) - ((p2.X - p0.X) * (p1.Y - p0.Y));
        }

        public void Move(int dx, int dy)
        {
            if (dx == 0 && dy == 0)
            {
                return;
            }

            for (int i = 0; i < this.Edges.Count; i++)
            {
                this.Edges[i].FirstVertex.X += dx;
                this.Edges[i].SecondVertex.Y += dy;
            }
        }

        public object Clone()
        {
            var cloned = new Polygon();
            var edgesList = new List<Edge>();
            for (int i = 0; i < this.Edges.Count; i++)
            {
                edgesList.Add((Edge)this.Edges[i].Clone());
            }

            int n = edgesList.Count;
            for (int i = 1; i < this.Edges.Count-1; i++)
            {
                edgesList[i].NextNeighbor = edgesList[(i + 1)];
                edgesList[i].PrevNeighbor = edgesList[(i - 1)];
            }

            edgesList[0].NextNeighbor = edgesList[1];
            edgesList[0].PrevNeighbor = edgesList[n-1];

            edgesList[n-1].NextNeighbor = edgesList[0];
            edgesList[n-1].PrevNeighbor = edgesList[n-2];

            cloned.Edges = edgesList;

            return cloned;
        }
    }
}
