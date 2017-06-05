namespace AshesScenarioBuilder1
{
    partial class DialogMenuOption
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Button();
            this.portrait = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.portrait)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.ForeColor = System.Drawing.Color.Aqua;
            this.nameLabel.Image = global::AshesScenarioBuilder1.Properties.Resources.ChatBG;
            this.nameLabel.Location = new System.Drawing.Point(74, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(261, 85);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "label";
            this.nameLabel.UseVisualStyleBackColor = true;
            this.nameLabel.Click += new System.EventHandler(this.nameLabel_Click);
            // 
            // portrait
            // 
            this.portrait.Image = global::AshesScenarioBuilder1.Properties.Resources.na;
            this.portrait.Location = new System.Drawing.Point(3, 3);
            this.portrait.Name = "portrait";
            this.portrait.Size = new System.Drawing.Size(65, 85);
            this.portrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.portrait.TabIndex = 0;
            this.portrait.TabStop = false;
            // 
            // DialogMenuOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.portrait);
            this.Name = "DialogMenuOption";
            this.Size = new System.Drawing.Size(339, 90);
            ((System.ComponentModel.ISupportInitialize)(this.portrait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox portrait;
        private System.Windows.Forms.Button nameLabel;
    }
}
