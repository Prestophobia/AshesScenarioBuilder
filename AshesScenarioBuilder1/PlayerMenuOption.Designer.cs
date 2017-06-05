namespace AshesScenarioBuilder1
{
    partial class PlayerMenuOption
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
            this.iconBox = new System.Windows.Forms.PictureBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.Label();
            this.difficulty = new System.Windows.Forms.Label();
            this.noSeedDisplay = new System.Windows.Forms.PictureBox();
            this.noEngiDisplay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noSeedDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noEngiDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // iconBox
            // 
            this.iconBox.Image = global::AshesScenarioBuilder1.Properties.Resources.phcRed;
            this.iconBox.Location = new System.Drawing.Point(3, 3);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(44, 44);
            this.iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconBox.TabIndex = 0;
            this.iconBox.TabStop = false;
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.Red;
            this.deleteButton.Location = new System.Drawing.Point(333, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "DELETE 💣";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.BackColor = System.Drawing.Color.White;
            this.name.Location = new System.Drawing.Point(53, 3);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(41, 13);
            this.name.TabIndex = 2;
            this.name.Text = "Name: ";
            // 
            // difficulty
            // 
            this.difficulty.AutoSize = true;
            this.difficulty.BackColor = System.Drawing.Color.White;
            this.difficulty.Location = new System.Drawing.Point(53, 34);
            this.difficulty.Name = "difficulty";
            this.difficulty.Size = new System.Drawing.Size(56, 13);
            this.difficulty.TabIndex = 3;
            this.difficulty.Text = "Difficulty:  ";
            // 
            // noSeedDisplay
            // 
            this.noSeedDisplay.Image = global::AshesScenarioBuilder1.Properties.Resources.noSeed;
            this.noSeedDisplay.Location = new System.Drawing.Point(358, 25);
            this.noSeedDisplay.Name = "noSeedDisplay";
            this.noSeedDisplay.Size = new System.Drawing.Size(22, 22);
            this.noSeedDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.noSeedDisplay.TabIndex = 4;
            this.noSeedDisplay.TabStop = false;
            this.noSeedDisplay.Visible = false;
            // 
            // noEngiDisplay
            // 
            this.noEngiDisplay.Image = global::AshesScenarioBuilder1.Properties.Resources.noEngi;
            this.noEngiDisplay.Location = new System.Drawing.Point(386, 25);
            this.noEngiDisplay.Name = "noEngiDisplay";
            this.noEngiDisplay.Size = new System.Drawing.Size(22, 22);
            this.noEngiDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.noEngiDisplay.TabIndex = 5;
            this.noEngiDisplay.TabStop = false;
            this.noEngiDisplay.Visible = false;
            // 
            // PlayerMenuOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.noEngiDisplay);
            this.Controls.Add(this.noSeedDisplay);
            this.Controls.Add(this.difficulty);
            this.Controls.Add(this.name);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.iconBox);
            this.Name = "PlayerMenuOption";
            this.Size = new System.Drawing.Size(414, 54);
            this.Click += new System.EventHandler(this.PlayerMenuOption_Click);
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noSeedDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noEngiDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iconBox;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label difficulty;
        private System.Windows.Forms.PictureBox noSeedDisplay;
        private System.Windows.Forms.PictureBox noEngiDisplay;
    }
}
