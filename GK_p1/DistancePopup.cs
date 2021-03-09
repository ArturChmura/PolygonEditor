using GK_p1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GK_p1
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class DistancePopup : Form
    {
        private readonly MainWindow mainWindow;
        private Edge handledEdge;
        private string currentText;

        public DistancePopup(MainWindow mainWindow)
        {
            this.InitializeComponent();
            this.mainWindow = mainWindow;
        }

        public void HandleEdge(Edge edge)
        {
            bool canBeRestricted = false;
            Edge p = edge.NextNeighbor;
            while (p != edge)
            {
                if (!p.Restrictions.Contains(SameLengthRestriction.Name))
                {
                    canBeRestricted = true;
                    break;
                }

                p = p.NextNeighbor;
            }

            if (!canBeRestricted)
            {
                MessageBox.Show("Nie można ustawić długości ponieważ wszystkie inne krawędzie mają nałożone ograniczenia");
                this.handledEdge = null;
                return;
            }

            this.lengthTextBox.Text = this.currentText = edge.Distance.ToString("n2");
            this.handledEdge = edge;
            this.Show();
            this.mainWindow.Enabled = false;
        }

        private void lengthTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.lengthTextBox.Text.Length > 0)
            {
                string text = this.lengthTextBox.Text.Replace('.', ',');
                bool isNumeric = float.TryParse(text, out _);

                if (isNumeric)
                {
                    this.currentText = this.lengthTextBox.Text;
                }
                else
                {
                    this.lengthTextBox.Text = this.currentText;
                    this.lengthTextBox.Select(this.lengthTextBox.Text.Length, 0);
                }
            }
        }

        private void lengthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back
               && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            float value = float.Parse(this.currentText.Replace('.', ','));
            this.handledEdge?.FixLength(value);
            this.handledEdge = null;
            this.mainWindow.Enabled = true;
            this.Hide();
            this.mainWindow.RedrawAll();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.handledEdge = null;
            this.mainWindow.Enabled = true;
            this.Hide();
        }
    }
}
