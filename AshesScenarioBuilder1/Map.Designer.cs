namespace AshesScenarioBuilder1
{
    partial class Map
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Map));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MapDisplay = new System.Windows.Forms.PictureBox();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.MapDisplay);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(896, 755);
            this.MainPanel.TabIndex = 1;
            // 
            // MapDisplay
            // 
            this.MapDisplay.BackColor = System.Drawing.Color.Transparent;
            this.MapDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapDisplay.ErrorImage = global::AshesScenarioBuilder1.Properties.Resources.xTiny;
            this.MapDisplay.Location = new System.Drawing.Point(0, 0);
            this.MapDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.MapDisplay.Name = "MapDisplay";
            this.MapDisplay.Size = new System.Drawing.Size(896, 755);
            this.MapDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MapDisplay.TabIndex = 0;
            this.MapDisplay.TabStop = false;
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 755);
            this.Controls.Add(this.MainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Map";
            this.Text = "Map";
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MapDisplay;
        private System.Windows.Forms.Panel MainPanel;
    }
}