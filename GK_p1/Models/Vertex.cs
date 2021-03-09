using GK_p1.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GK_p1.Models
{
    public class Vertex : ICloneable
    {
        private const float Radius = 5.0f;

        public Vertex()
        {
            this.X = this.Y = 0;
        }

        public Vertex(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public List<IRestriction> Restrictions { get; set; } = new List<IRestriction>();

        public float X { get; set; }

        public float Y { get; set; }

        public bool IsMoving { get; set; }

        public void Move(float dx, float dy, Edge invokingEdge)
        {
            if (dx == 0 && dy == 0)
            {
                return;
            }

            this.IsMoving = true;
            this.X += dx;
            this.Y += dy;
            for (int i = 0; i < this.Restrictions.Count; i++)
            {
                this.Restrictions[i].MoveBindedVertex(dx, dy, invokingEdge);
            }

            this.IsMoving = false;
        }

        public void Draw(PaintEventArgs e, Brush brush)
        {
            e.Graphics.FillEllipse(brush, new Rectangle((int)(this.X - Radius), (int)(this.Y - Radius), (int)(Radius * 2), (int)(Radius * 2)));
        }

        public bool Contains(Point p)
        {
            return this.Contains(new Vertex(p.X, p.Y));
        }

        public bool Contains(Vertex v)
        {
            double distance = Math.Sqrt(((this.X - v.X) * (this.X - v.X)) + ((this.Y - v.Y) * (this.Y - v.Y)));
            return distance <= Radius;
        }

        public object Clone()
        {
            var cloned = new Vertex(this.X, this.Y);

            return cloned;
        }
    }
}
