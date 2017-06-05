namespace AshesScenarioBuilder1
{
    partial class TriggerMenuOption
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
            this.triggerIcon = new System.Windows.Forms.PictureBox();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.triggerName = new System.Windows.Forms.Label();
            this.triggerDetails = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.triggerIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // triggerIcon
            // 
            this.triggerIcon.BackColor = System.Drawing.Color.Transparent;
            this.triggerIcon.Image = global::AshesScenarioBuilder1.Properties.Resources.timer;
            this.triggerIcon.Location = new System.Drawing.Point(109, 7);
            this.triggerIcon.Name = "triggerIcon";
            this.triggerIcon.Size = new System.Drawing.Size(56, 52);
            this.triggerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.triggerIcon.TabIndex = 0;
            this.triggerIcon.TabStop = false;
            // 
            // upButton
            // 
            this.upButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.upButton.BackColor = System.Drawing.Color.Lime;
            this.upButton.Location = new System.Drawing.Point(16, 7);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(87, 23);
            this.upButton.TabIndex = 1;
            this.upButton.Text = "Move Up ▲";
            this.upButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.upButton.UseVisualStyleBackColor = false;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downButton.BackColor = System.Drawing.Color.Lime;
            this.downButton.Location = new System.Drawing.Point(16, 36);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(87, 23);
            this.downButton.TabIndex = 2;
            this.downButton.Text = "Move Down ▼";
            this.downButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.downButton.UseVisualStyleBackColor = false;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.BackColor = System.Drawing.Color.Aquamarine;
            this.editButton.Location = new System.Drawing.Point(617, 36);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(87, 23);
            this.editButton.TabIndex = 3;
            this.editButton.Text = "Edit     🖉";
            this.editButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.BackColor = System.Drawing.Color.Red;
            this.deleteButton.Location = new System.Drawing.Point(617, 7);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(87, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "DELETE 💣";
            this.deleteButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // triggerName
            // 
            this.triggerName.AutoSize = true;
            this.triggerName.Location = new System.Drawing.Point(186, 7);
            this.triggerName.Name = "triggerName";
            this.triggerName.Size = new System.Drawing.Size(35, 13);
            this.triggerName.TabIndex = 5;
            this.triggerName.Text = "Name";
            // 
            // triggerDetails
            // 
            this.triggerDetails.Location = new System.Drawing.Point(186, 25);
            this.triggerDetails.Name = "triggerDetails";
            this.triggerDetails.Size = new System.Drawing.Size(425, 34);
            this.triggerDetails.TabIndex = 6;
            this.triggerDetails.Text = "Details";
            // 
            // TriggerMenuOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.triggerDetails);
            this.Controls.Add(this.triggerName);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.triggerIcon);
            this.Name = "TriggerMenuOption";
            this.Size = new System.Drawing.Size(720, 69);
            ((System.ComponentModel.ISupportInitialize)(this.triggerIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox triggerIcon;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label triggerName;
        private System.Windows.Forms.Label triggerDetails;
    }
}
