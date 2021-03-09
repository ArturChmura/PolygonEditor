using GK_p1.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GK_p1.Models
{
    public class SameLengthRestriction : IRestriction
    {
        public SameLengthRestriction(Vertex bindedVertex, Edge horizontalEdgeEdge)
        {
            this.RestrictedEdge = horizontalEdgeEdge;
            this.BindedVertex = bindedVertex;
        }

        public static string Name => "SameLength";

        public Edge RestrictedEdge { get; set; }

        public Vertex BindedVertex { get; set; }

        public void MoveBindedVertex(float dx, float dy, Edge invokingEdge)
        {
            if (invokingEdge != this.RestrictedEdge && !this.BindedVertex.IsMoving)
            {
                this.BindedVertex.Move(dx, dy, this.RestrictedEdge);
            }
        }
    }
}
