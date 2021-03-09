using System.Windows.Forms;

namespace GK_p1
{
    public partial class MainWindow
    {
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (this.NewMode)
                {
                    this.NewMode = false;
                    if (this.selectedPolygon != null)
                    {
                        this.polygons.Remove(this.selectedPolygon);
                        this.selectedPolygon = null;
                    }

                    this.newEdge = null;
                    this.pictureBox1.Invalidate();
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (this.selectedVertex != null)
                {
                    this.polygons[this.selectedPolygonIndex].DeleteVertex(this.selectedVertexEdgeIndex);
                    this.selectedVertex = null;
                }
                else if (this.selectedPolygon != null)
                {
                    this.polygons.RemoveAt(this.selectedPolygonIndex);
                    this.selectedPolygon = null;
                }

                this.pictureBox1.Invalidate();
            }
        }
    }
}
