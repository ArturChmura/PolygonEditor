namespace GK_p1
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.newPolygonButton = new System.Windows.Forms.Button();
            this.newVertexButton = new System.Windows.Forms.Button();
            this.makeVerticalButton = new System.Windows.Forms.Button();
            this.fixLengthButton = new System.Windows.Forms.Button();
            this.makeHorizontalButton = new System.Windows.Forms.Button();
            this.removeRestrictionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1393, 603);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // newPolygonButton
            // 
            this.newPolygonButton.Location = new System.Drawing.Point(0, 9);
            this.newPolygonButton.Name = "newPolygonButton";
            this.newPolygonButton.Size = new System.Drawing.Size(120, 23);
            this.newPolygonButton.TabIndex = 1;
            this.newPolygonButton.Text = "Nowy Wielokąt";
            this.newPolygonButton.UseVisualStyleBackColor = true;
            this.newPolygonButton.Click += new System.EventHandler(this.newPolygonButton_MouseClick);
            // 
            // newVertexButton
            // 
            this.newVertexButton.Location = new System.Drawing.Point(127, 9);
            this.newVertexButton.Name = "newVertexButton";
            this.newVertexButton.Size = new System.Drawing.Size(124, 23);
            this.newVertexButton.TabIndex = 2;
            this.newVertexButton.Text = "Nowy wierzchołek";
            this.newVertexButton.UseVisualStyleBackColor = true;
            this.newVertexButton.Click += new System.EventHandler(this.newVertexButton_Click);
            // 
            // makeVerticalButton
            // 
            this.makeVerticalButton.Location = new System.Drawing.Point(258, 9);
            this.makeVerticalButton.Name = "makeVerticalButton";
            this.makeVerticalButton.Size = new System.Drawing.Size(99, 23);
            this.makeVerticalButton.TabIndex = 3;
            this.makeVerticalButton.Text = "Pionowa";
            this.makeVerticalButton.UseVisualStyleBackColor = true;
            this.makeVerticalButton.Click += new System.EventHandler(this.makeVerticalButton_Click);
            // 
            // fixLengthButton
            // 
            this.fixLengthButton.Location = new System.Drawing.Point(497, 9);
            this.fixLengthButton.Name = "fixLengthButton";
            this.fixLengthButton.Size = new System.Drawing.Size(127, 23);
            this.fixLengthButton.TabIndex = 4;
            this.fixLengthButton.Text = "Blokuj długość";
            this.fixLengthButton.UseVisualStyleBackColor = true;
            this.fixLengthButton.Click += new System.EventHandler(this.fixLengthButton_Click);
            // 
            // makeHorizontalButton
            // 
            this.makeHorizontalButton.Location = new System.Drawing.Point(363, 9);
            this.makeHorizontalButton.Name = "makeHorizontalButton";
            this.makeHorizontalButton.Size = new System.Drawing.Size(128, 23);
            this.makeHorizontalButton.TabIndex = 5;
            this.makeHorizontalButton.Text = "Pozioma";
            this.makeHorizontalButton.UseVisualStyleBackColor = true;
            this.makeHorizontalButton.Click += new System.EventHandler(this.makeHorizontalButton_Click);
            // 
            // removeRestrictionButton
            // 
            this.removeRestrictionButton.Location = new System.Drawing.Point(631, 9);
            this.removeRestrictionButton.Name = "removeRestrictionButton";
            this.removeRestrictionButton.Size = new System.Drawing.Size(165, 23);
            this.removeRestrictionButton.TabIndex = 6;
            this.removeRestrictionButton.Text = "Usuń ograniczenia";
            this.removeRestrictionButton.UseVisualStyleBackColor = true;
            this.removeRestrictionButton.Click += new System.EventHandler(this.removeRestrictionButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1393, 641);
            this.Controls.Add(this.removeRestrictionButton);
            this.Controls.Add(this.makeHorizontalButton);
            this.Controls.Add(this.fixLengthButton);
            this.Controls.Add(this.makeVerticalButton);
            this.Controls.Add(this.newVertexButton);
            this.Controls.Add(this.newPolygonButton);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.Name = "MainWindow";
            this.Text = "Polygon Editor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button newPolygonButton;
        private System.Windows.Forms.Button newVertexButton;
        private System.Windows.Forms.Button makeVerticalButton;
        private System.Windows.Forms.Button fixLengthButton;
        private System.Windows.Forms.Button makeHorizontalButton;
        private System.Windows.Forms.Button removeRestrictionButton;
    }
}

