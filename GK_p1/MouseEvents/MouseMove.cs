using GK_p1.Interfaces;
using GK_p1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GK_p1
{
    public partial class MainWindow
    {
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.sugestedRestrictions.Clear();
            if (this.newEdge != null)
            {
                this.newEdge.SecondVertex.Move(e.Location.X - this.lastMousePosition.X, e.Location.Y - this.lastMousePosition.Y, this.newEdge);
                if (suggestCheckbox.Checked)
                {
                    if (this.newEdge.IsVertical)
                    {
                        int x = (int)((this.newEdge.FirstVertex.X + this.newEdge.SecondVertex.X) / 2);
                        int y = (int)((this.newEdge.FirstVertex.Y + this.newEdge.SecondVertex.Y) / 2);
                        this.sugestedRestrictions.Add((VerticalRestricion.Name, x, y));
                    }
                    else if (this.newEdge.IsHorizontal)
                    {
                        int x = (int)((this.newEdge.FirstVertex.X + this.newEdge.SecondVertex.X) / 2);
                        int y = (int)((this.newEdge.FirstVertex.Y + this.newEdge.SecondVertex.Y) / 2);
                        this.sugestedRestrictions.Add((HorizontalRestriction.Name, x, y));
                    }
                }

                this.pictureBox1.Invalidate();
            }

            if (this.movingMode)
            {
                this.selectedPolygon?.Move(e.Location.X - this.lastMousePosition.X, e.Location.Y - this.lastMousePosition.Y);
                this.SelectedEdge?.Move(e.Location.X - this.lastMousePosition.X, e.Location.Y - this.lastMousePosition.Y);

                if (this.selectedVertex != null)
                {
                    this.selectedVertex?.Move(e.Location.X - this.lastMousePosition.X, e.Location.Y - this.lastMousePosition.Y, null);
                    if (this.suggestCheckbox.Checked)
                    {
                        Edge nextEdge = this.polygons[this.selectedPolygonIndex].Edges[this.selectedVertexEdgeIndex];
                        Edge prevEdge = this.polygons[this.selectedPolygonIndex].Edges[this.selectedVertexEdgeIndex == 0 ? this.polygons[this.selectedPolygonIndex].Edges.Count - 1 : this.selectedVertexEdgeIndex - 1];
                        if (nextEdge.IsVertical)
                        {
                            int x = (int)((nextEdge.FirstVertex.X + nextEdge.SecondVertex.X) / 2);
                            int y = (int)((nextEdge.FirstVertex.Y + nextEdge.SecondVertex.Y) / 2);
                            this.sugestedRestrictions.Add((VerticalRestricion.Name, x, y));
                        }
                        else if (nextEdge.IsHorizontal)
                        {
                            int x = (int)((nextEdge.FirstVertex.X + nextEdge.SecondVertex.X) / 2);
                            int y = (int)((nextEdge.FirstVertex.Y + nextEdge.SecondVertex.Y) / 2);
                            this.sugestedRestrictions.Add((HorizontalRestriction.Name, x, y));
                        }

                        if (prevEdge.IsVertical)
                        {
                            int x = (int)((prevEdge.FirstVertex.X + prevEdge.SecondVertex.X) / 2);
                            int y = (int)((prevEdge.FirstVertex.Y + prevEdge.SecondVertex.Y) / 2);
                            this.sugestedRestrictions.Add((VerticalRestricion.Name, x, y));
                        }
                        else if (prevEdge.IsHorizontal)
                        {
                            int x = (int)((prevEdge.FirstVertex.X + prevEdge.SecondVertex.X) / 2);
                            int y = (int)((prevEdge.FirstVertex.Y + prevEdge.SecondVertex.Y) / 2);
                            this.sugestedRestrictions.Add((HorizontalRestriction.Name, x, y));
                        }
                    }
                }


                this.pictureBox1.Invalidate();
            }

            this.lastMousePosition = e.Location;
        }
    }
}
