using GK_p1.Models;
using System.Windows.Forms;

namespace GK_p1
{
    public partial class MainWindow
    {
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.NewMode)
            {
                // Pierwszy klik
                if (this.newEdge is null)
                {
                    this.newEdge = new Edge(new Vertex(e.X, e.Y), new Vertex(e.X, e.Y));
                }
                else
                {
                    if (this.selectedPolygon == null)
                    {
                        this.polygons.Add(new Polygon());
                        this.selectedPolygon = this.polygons[^1];
                    }

                    if (this.suggestCheckbox.Checked)
                    {
                        if (newEdge.IsHorizontal)
                        {
                            newEdge.MakeHorizontal();
                        }
                        
                        if (newEdge.IsVertical)
                        {
                            newEdge.MakeVertical();
                        }
                    }
                    
                    this.selectedPolygon.AddEdge(this.newEdge);
                    if (this.polygons[^1].IsComplete)
                    {
                        this.newEdge = null;
                        this.selectedPolygon = null;
                        this.NewMode = false;
                    }
                    else
                    {
                        this.newEdge = new Edge(this.newEdge.SecondVertex, new Vertex(e.X, e.Y));
                    }
                }
            }
            else
            {
                this.selectedVertex = null;
                this.SelectedEdge = null;
                this.selectedPolygon = null;
                selectedPolygonIndex = -1;
                bool foundVertex = false, foundEdge = false;

                // selecting Vertex
                for (int i = 0; i < this.polygons.Count; i++)
                {
                    Polygon polygon = this.polygons[i];
                    for (int j = 0; j < polygon.Edges.Count; j++)
                    {
                        Edge edge = polygon.Edges[j];
                        if (edge.FirstVertex.Contains(e.Location))
                        {
                            this.selectedVertex = edge.FirstVertex;
                            this.selectedVertexEdgeIndex = j;
                            this.selectedPolygonIndex = i;
                            foundVertex = true;
                            break;
                        }
                    }
                }

                if (!foundVertex)
                {
                    // selecting edge
                    for (int i = 0; i < this.polygons.Count; i++)
                    {
                        Polygon polygon = this.polygons[i];
                        for (int j = 0; j < polygon.Edges.Count; j++)
                        {
                            Edge edge = polygon.Edges[j];
                            if (edge.Contains(e.Location))
                            {
                                this.SelectedEdge = edge;
                                this.selectedVertexEdgeIndex = j;
                                this.selectedPolygonIndex = i;
                                foundEdge = true;
                                break;
                            }
                        }
                    }
                }

                if (!foundVertex && !foundEdge)
                {
                    // selecting polygon
                    for (int i = 0; i < this.polygons.Count; i++)
                    {
                        if (this.polygons[i].Contains(e.Location))
                        {
                            this.selectedPolygon = this.polygons[i];
                            this.selectedPolygonIndex = i;
                            break;
                        }
                    }
                }
                 
                this.movingMode = true;
                if(selectedPolygonIndex >= 0)
                {
                    this.oldPolygon = (Polygon)this.polygons[this.selectedPolygonIndex].Clone();
                }
            }

            this.lastMousePosition = e.Location;
            this.pictureBox1.Invalidate();
        }
    }
}
