using GK_p1.Interfaces;
using GK_p1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace GK_p1
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class MainWindow : Form, INotifyPropertyChanged
    {
        private readonly List<Polygon> polygons = new List<Polygon>();
        private Vertex selectedVertex;
        private Edge selectedEdge;
        private Polygon selectedPolygon;
        private int selectedVertexEdgeIndex;
        private int selectedPolygonIndex;

        private Edge newEdge;
        private bool newMode = false;
        private bool movingMode = false;
        private Point lastMousePosition;

        private Polygon oldPolygon;
        private List<(string name, int x, int y)> sugestedRestrictions = new List<(string name, int x, int y)>();
        private CheckBox suggestCheckbox;

        public MainWindow()
        {
            this.polygons.Add(Predefined.GetPolygon());
            this.InitializeComponent();
            this.AddBindings();
            this.suggestCheckbox = new CheckBox()
            {
                Text = "Sugerowanie Krawędzi",
                Location = new Point(800, 10),
                Name = "suggestCheckbox",
            };
            this.Controls.Add(this.suggestCheckbox);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Edge SelectedEdge
        {
            get { return this.selectedEdge; }

            set
            {
                if (this.selectedEdge == value)
                {
                    return;
                }

                this.selectedEdge = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.SelectedEdge)));
            }
        }

        public bool NewMode
        {
            get { return this.newMode; }

            set
            {
                if (this.newMode == value)
                {
                    return;
                }

                this.newMode = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.NewMode)));
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            this.oldPolygon?.Draw(e, Brushes.Gray);

            for (int i = 0; i < this.polygons.Count; i++)
            {
                this.polygons[i].Draw(e, this.polygons[i] == this.selectedPolygon ? Brushes.Green : Brushes.Black);
            }

            this.newEdge?.Draw(e, Brushes.Green);
            this.selectedVertex?.Draw(e, Brushes.Green);
            this.SelectedEdge?.Draw(e, Brushes.Green);

            for (int i = 0; i < this.sugestedRestrictions.Count; i++)
            {
                this.DrawIcon(e, sugestedRestrictions[i].x, sugestedRestrictions[i].y, sugestedRestrictions[i].name);
            }
        }

        private void newPolygonButton_MouseClick(object sender, EventArgs e)
        {
            if (!this.NewMode)
            {
                this.SelectedEdge = null;
                this.selectedPolygon = null;
                this.selectedVertex = null;
                this.NewMode = true;
                this.RedrawAll();
            }
        }

        private void newVertexButton_Click(object sender, EventArgs e)
        {
            if (this.SelectedEdge == null)
            {
                return;
            }

            this.polygons[this.selectedPolygonIndex].AddVertex(this.selectedVertexEdgeIndex);
            this.selectedEdge = this.polygons[this.selectedPolygonIndex].Edges[this.selectedVertexEdgeIndex];
            this.RedrawAll();
        }

        private void makeVerticalButton_Click(object sender, EventArgs e)
        {
            this.selectedEdge?.MakeVertical();
            this.RedrawAll();
        }

        private void makeHorizontalButton_Click(object sender, EventArgs e)
        {
            this.selectedEdge?.MakeHorizontal();
            this.RedrawAll();
        }

        private void fixLengthButton_Click(object sender, EventArgs e)
        {
            if (this.selectedEdge == null)
            {
                return;
            }

            var distancePopup = new DistancePopup(this);
            distancePopup.HandleEdge(this.selectedEdge);
        }

        private void removeRestrictionButton_Click(object sender, EventArgs e)
        {
            this.selectedEdge?.RemoveRestrictions();
            this.RedrawAll();
        }

        public void RedrawAll()
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.SelectedEdge)));
            this.pictureBox1.Invalidate();
        }
    }
}
