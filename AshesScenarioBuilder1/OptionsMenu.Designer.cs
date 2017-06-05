namespace AshesScenarioBuilder1
{
    partial class OptionsMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsMenu));
            this.label1 = new System.Windows.Forms.Label();
            this.AssetsPathTextBox = new System.Windows.Forms.TextBox();
            this.AssetOpenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ashes of the Singularity Assets Path";
            // 
            // AssetsPathTextBox
            // 
            this.AssetsPathTextBox.Location = new System.Drawing.Point(194, 16);
            this.AssetsPathTextBox.Name = "AssetsPathTextBox";
            this.AssetsPathTextBox.Size = new System.Drawing.Size(438, 20);
            this.AssetsPathTextBox.TabIndex = 1;
            this.AssetsPathTextBox.TextChanged += new System.EventHandler(this.AssetsPathTextBox_TextChanged);
            // 
            // AssetOpenButton
            // 
            this.AssetOpenButton.Location = new System.Drawing.Point(638, 14);
            this.AssetOpenButton.Name = "AssetOpenButton";
            this.AssetOpenButton.Size = new System.Drawing.Size(75, 23);
            this.AssetOpenButton.TabIndex = 2;
            this.AssetOpenButton.Text = "Open...";
            this.AssetOpenButton.UseVisualStyleBackColor = true;
            this.AssetOpenButton.Click += new System.EventHandler(this.AssetOpenButton_Click);
            // 
            // OptionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 56);
            this.Controls.Add(this.AssetOpenButton);
            this.Controls.Add(this.AssetsPathTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsMenu";
            this.Text = "OptionsMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AssetsPathTextBox;
        private System.Windows.Forms.Button AssetOpenButton;
    }
}