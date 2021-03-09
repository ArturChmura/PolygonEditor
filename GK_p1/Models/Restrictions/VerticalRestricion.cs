using GK_p1.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GK_p1.Models
{
    public class VerticalRestricion : IRestriction
    {
        public VerticalRestricion(Vertex bindedVertex, Edge horizontalEdgeEdge)
        {
            this.RestrictedEdge = horizontalEdgeEdge;
            this.BindedVertex = bindedVertex;
        }

        public static string Name => "Vertical";

        public Edge RestrictedEdge { get; set; }

        public Vertex BindedVertex { get; set; }

        public void MoveBindedVertex(float dx, float dy, Edge invokingEdge)
        {
            if (invokingEdge != this.RestrictedEdge && !this.BindedVertex.IsMoving)
            {
                this.BindedVertex.Move(dx, 0, this.RestrictedEdge);
            }
        }
    }
}
