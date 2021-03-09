using GK_p1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GK_p1.Interfaces
{
    public interface IRestriction
    {
        Edge RestrictedEdge { get; set; }

        Vertex BindedVertex { get; set; }

        void MoveBindedVertex(float dx, float dy, Edge invokingEdge);
    }
}
