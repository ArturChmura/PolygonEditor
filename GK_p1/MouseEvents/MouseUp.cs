using GK_p1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GK_p1
{
    public partial class MainWindow
    {
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(movingMode && selectedVertex != null && this.suggestCheckbox.Checked)
            {
                Edge nextEdge = this.polygons[this.selectedPolygonIndex].Edges[this.selectedVertexEdgeIndex];
                Edge prevEdge = this.polygons[this.selectedPolygonIndex].Edges[this.selectedVertexEdgeIndex == 0 ? this.polygons[this.selectedPolygonIndex].Edges.Count - 1 : this.selectedVertexEdgeIndex - 1];
                if (nextEdge.IsVertical)
                {
                    nextEdge.MakeVertical();
                }
                else if (nextEdge.IsHorizontal)
                {

                    nextEdge.MakeHorizontal();
                }

                if (prevEdge.IsVertical)
                {

                    nextEdge.MakeVertical();
                }
                else if (prevEdge.IsHorizontal)
                {

                    prevEdge.MakeHorizontal();
                }
            }


            this.movingMode = false;
            this.oldPolygon = null;
            

            this.pictureBox1.Invalidate();
        }
    }
}
