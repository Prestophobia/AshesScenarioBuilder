namespace AshesScenarioBuilder1
{
    partial class TriggerXMLPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TriggerXMLPreview));
            this.previewText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // previewText
            // 
            this.previewText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewText.Location = new System.Drawing.Point(0, 0);
            this.previewText.Multiline = true;
            this.previewText.Name = "previewText";
            this.previewText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.previewText.Size = new System.Drawing.Size(574, 506);
            this.previewText.TabIndex = 0;
            // 
            // TriggerXMLPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 506);
            this.Controls.Add(this.previewText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TriggerXMLPreview";
            this.Text = "TriggerXMLPreview";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox previewText;

    }
}