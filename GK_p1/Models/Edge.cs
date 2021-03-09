using GK_p1.Interfaces;
using GK_p1.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace GK_p1.Models
{
    public class Edge : ICloneable
    {
        public Edge()
        {
        }

        public Edge(Vertex v1, Vertex v2)
        {
            this.FirstVertex = v1;
            this.SecondVertex = v2;
        }

        public Vertex FirstVertex { get; set; }

        public Vertex SecondVertex { get; set; }

        public Edge PrevNeighbor { get; set; }

        public Edge NextNeighbor { get; set; }

        public bool IsRestricted
        {
          get { return this.Restrictions.Count > 0; }
        }

        public bool IsVertical
        {
            get
            {
                if (this.FirstVertex.X == this.SecondVertex.X)
                {
                    return true;
                }

                var tang = Math.Abs((this.SecondVertex.Y - this.FirstVertex.Y) / (this.SecondVertex.X - this.FirstVertex.X));

                return tang > 100;
            }
        }

        public bool IsHorizontal
        {
            get
            {
                if (this.FirstVertex.X == this.SecondVertex.X)
                {
                    return false;
                }

                var tang = Math.Abs((this.SecondVertex.Y - this.FirstVertex.Y) / (this.SecondVertex.X - this.FirstVertex.X));

                return tang < 0.01;
            }
        }

        public List<string> Restrictions { get; set; } = new List<string>();

        public double Distance =>
            Math.Sqrt(((this.FirstVertex.X - this.SecondVertex.X) * (this.FirstVertex.X - this.SecondVertex.X)) +
                ((this.FirstVertex.Y - this.SecondVertex.Y) * (this.FirstVertex.Y - this.SecondVertex.Y)));

        public bool MakeVertical()
        {
            if (this.IsRestricted || 
                (this.PrevNeighbor != null ? this.PrevNeighbor.Restrictions.Contains(VerticalRestricion.Name): false) || 
                (this.NextNeighbor != null ? this.NextNeighbor.Restrictions.Contains(VerticalRestricion.Name) : false))
            {
                return false;
            }

            bool canBeVaerical = false;
            Edge p = this.NextNeighbor;
            while (p != this)
            {
                if (p == null || !p.Restrictions.Contains(SameLengthRestriction.Name))
                {
                    canBeVaerical = true;
                    break;
                }

                p = p.NextNeighbor;
            }

            if (!canBeVaerical)
            {
                MessageBox.Show("Nie można ustawić krawędzi jako pionowa");
                return false;
            }

            float x = (this.FirstVertex.X + this.SecondVertex.X) / 2;
            this.FirstVertex.Move(x - this.FirstVertex.X, 0, this);
            this.SecondVertex.Move(x - this.SecondVertex.X, 0, this);

            this.FirstVertex.Restrictions.Add(new VerticalRestricion(this.SecondVertex, this));
            this.SecondVertex.Restrictions.Add(new VerticalRestricion(this.FirstVertex, this));
            this.Restrictions.Add(VerticalRestricion.Name);

            return true;
        }

        public bool MakeHorizontal()
        {
            if (this.IsRestricted ||
                (this.PrevNeighbor != null ? this.PrevNeighbor.Restrictions.Contains(HorizontalRestriction.Name) : false) ||
                (this.NextNeighbor != null ? this.NextNeighbor.Restrictions.Contains(HorizontalRestriction.Name) : false))
            {
                return false;
            }

            bool canBeHorizontal = false;
            Edge p = this.NextNeighbor;
            while (p != this)
            {
                if (p == null || !p.Restrictions.Contains(SameLengthRestriction.Name))
                {
                    canBeHorizontal = true;
                    break;
                }

                p = p.NextNeighbor;
            }

            if (!canBeHorizontal)
            {
                MessageBox.Show("Nie można ustawić krawędzi jako pozioma");
                return false;
            }

            float y = (this.FirstVertex.Y + this.SecondVertex.Y) / 2;
            this.FirstVertex.Move(0, y - this.FirstVertex.Y, this);
            this.SecondVertex.Move(0, y - this.SecondVertex.Y, this);

            this.FirstVertex.Restrictions.Add(new HorizontalRestriction(this.SecondVertex, this));
            this.SecondVertex.Restrictions.Add(new HorizontalRestriction(this.FirstVertex, this));

            this.Restrictions.Add(HorizontalRestriction.Name);

            return true;
        }

        public bool FixLength(double distance)
        {
            if (this.IsRestricted)
            {
                return false;
            }

            double delta = distance - this.Distance;
            int xSign = this.FirstVertex.X < this.SecondVertex.X ? 1 : -1;
            int ySign = this.FirstVertex.Y < this.SecondVertex.Y ? 1 : -1;
            this.FirstVertex.Restrictions.Add(new SameLengthRestriction(this.SecondVertex, this));
            this.SecondVertex.Restrictions.Add(new SameLengthRestriction(this.FirstVertex, this));
            this.Restrictions.Add(SameLengthRestriction.Name);

            if (this.SecondVertex.X == this.FirstVertex.X)
            {
                this.FirstVertex.Move(0, -1 * ySign * (int)delta / 2, this);
                this.SecondVertex.Move(0, ySign * (int)delta / 2, this);
                return true;
            }

            double tang = ((double)(this.SecondVertex.Y - this.FirstVertex.Y)) / ((double)(this.SecondVertex.X - this.FirstVertex.X));

            float dx = (float)(Math.Sign(delta) * Math.Sqrt(delta * delta / 4 / (1 + (tang * tang))));
            float dy = (float)(Math.Sign(delta) * Math.Abs(dx * tang));
            this.FirstVertex.Move(-1 * dx * xSign, -1 * dy * ySign, this);
            this.SecondVertex.Move(dx * xSign, dy * ySign, this);

            return true;
        }

        public bool RemoveRestrictions()
        {
            if (!this.IsRestricted)
            {
                return false;
            }

            for (int i = this.FirstVertex.Restrictions.Count - 1; i >= 0; i--)
            {
                if (this.FirstVertex.Restrictions[i].RestrictedEdge == this)
                {
                    this.FirstVertex.Restrictions.RemoveAt(i);
                }
            }

            for (int i = this.SecondVertex.Restrictions.Count - 1; i >= 0; i--)
            {
                if (this.SecondVertex.Restrictions[i].RestrictedEdge == this)
                {
                    this.SecondVertex.Restrictions.RemoveAt(i);
                }
            }

            this.Restrictions.Clear();
            return true;
        }

        private void PutPixel(int x, int y, PaintEventArgs e, Brush brush)
        {
            e.Graphics.FillRectangle(brush, x, y, 1, 1);
        }

        public bool Contains(Point p)
        {
            float distance;

            float l2 = this.DistanceSquared(this.FirstVertex.X, this.FirstVertex.Y, this.SecondVertex.X, this.SecondVertex.Y);
            if (l2 == 0.0)
            {
                distance = this.DistanceSquared(this.FirstVertex.X, this.FirstVertex.Y, p.X, p.Y);
            }
            else
            {
                var t = (((p.X - this.FirstVertex.X) * (this.SecondVertex.X - this.FirstVertex.X)) + ((p.Y - this.FirstVertex.Y) * (this.SecondVertex.Y - this.FirstVertex.Y))) / l2;

                t = Math.Max(0, Math.Min(1, t));
                distance = this.DistanceSquared(p.X, p.Y, this.FirstVertex.X + (t * (this.SecondVertex.X - this.FirstVertex.X)), this.FirstVertex.Y + (t * (this.SecondVertex.Y - this.FirstVertex.Y)));
            }

            return distance <= 9;
        }

        private float DistanceSquared(float x1, float y1, float x2, float y2)
        {
            return ((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2));
        }

        public void Move(int dx, int dy)
        {
            if (dx == 0 && dy == 0)
            {
                return;
            }

            this.FirstVertex.Move(dx, dy, this);
            this.SecondVertex.Move(dx, dy, this);
        }

        private void DrawIcons(PaintEventArgs e)
        {
            int x = (int)((this.FirstVertex.X + this.SecondVertex.X) / 2);
            int y = (int)((this.FirstVertex.Y + this.SecondVertex.Y) / 2);
            for (int i = 0; i < this.Restrictions.Count; i++)
            {
                var restrictionName = this.Restrictions[i];
                if (restrictionName == VerticalRestricion.Name)
                {
                    e.Graphics.DrawIcon(Resources.vertical, x, y - (Resources.vertical.Height / 2));
                }
                else if (restrictionName == HorizontalRestriction.Name)
                {
                    e.Graphics.DrawIcon(Resources.horizontal, x - (Resources.horizontal.Width / 2), y);
                }
                else if (restrictionName == SameLengthRestriction.Name)
                {
                    e.Graphics.DrawIcon(Resources._fixed, x - (Resources._fixed.Width / 2), y - (Resources._fixed.Height / 2));
                }
            }
        }

        public void Draw(PaintEventArgs e, Brush brush)
        {
            this.DrawIcons(e);
            int d, dx, dy, ai, bi, xi, yi, x1 = (int)this.FirstVertex.X, x2 = (int)this.SecondVertex.X, y1 = (int)this.FirstVertex.Y, y2 = (int)this.SecondVertex.Y;
            int x = x1, y = y1;
            if ((int)this.FirstVertex.X < (int)this.SecondVertex.X)
            {
                xi = 1;
                dx = x2 - x1;
            }
            else
            {
                xi = -1;
                dx = x1 - x2;
            }

            if ((int)this.FirstVertex.Y < (int)this.SecondVertex.Y)
            {
                yi = 1;
                dy = y2 - y1;
            }
            else
            {
                yi = -1;
                dy = y1 - y2;
            }

            this.PutPixel(x, y, e, brush);

            if (dx > dy)
            {
                ai = (dy - dx) * 2;
                bi = dy * 2;
                d = bi - dx;

                while (x != x2)
                {
                    if (d >= 0)
                    {
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                    }

                    x += xi;
                    this.PutPixel(x, y, e, brush);
                }
            }
            else
            {
                ai = (dx - dy) * 2;
                bi = dx * 2;
                d = bi - dy;
                while (y != y2)
                {
                    if (d >= 0)
                    {
                        x += xi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                    }

                    y += yi;
                    this.PutPixel(x, y, e, brush);
                }
            }
        }

        public object Clone()
        {
            var cloned = new Edge((Vertex)this.FirstVertex.Clone(), (Vertex)this.SecondVertex.Clone());
            return cloned;
        }
    }
}
