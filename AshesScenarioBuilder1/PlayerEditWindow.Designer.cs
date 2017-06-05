namespace AshesScenarioBuilder1
{
    partial class PlayerEditWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerEditWindow));
            this.nameBox = new System.Windows.Forms.TextBox();
            this.factionCB = new System.Windows.Forms.ComboBox();
            this.teamNUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.startLocNud = new System.Windows.Forms.NumericUpDown();
            this.AITypeCB = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.AIDiffCB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.noSeedBox = new System.Windows.Forms.CheckBox();
            this.noEngiBox = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.colorCB = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.teamNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startLocNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(60, 6);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(121, 20);
            this.nameBox.TabIndex = 0;
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // factionCB
            // 
            this.factionCB.FormattingEnabled = true;
            this.factionCB.Items.AddRange(new object[] {
            "PHC",
            "Substrate"});
            this.factionCB.Location = new System.Drawing.Point(60, 35);
            this.factionCB.Name = "factionCB";
            this.factionCB.Size = new System.Drawing.Size(121, 21);
            this.factionCB.TabIndex = 1;
            this.factionCB.TextChanged += new System.EventHandler(this.factionCB_TextChanged);
            // 
            // teamNUD
            // 
            this.teamNUD.Location = new System.Drawing.Point(60, 64);
            this.teamNUD.Name = "teamNUD";
            this.teamNUD.Size = new System.Drawing.Size(121, 20);
            this.teamNUD.TabIndex = 2;
            this.teamNUD.ValueChanged += new System.EventHandler(this.teamNUD_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Faction";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Team";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Color";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Starting Location";
            // 
            // startLocNud
            // 
            this.startLocNud.Location = new System.Drawing.Point(106, 118);
            this.startLocNud.Name = "startLocNud";
            this.startLocNud.Size = new System.Drawing.Size(75, 20);
            this.startLocNud.TabIndex = 9;
            this.startLocNud.ValueChanged += new System.EventHandler(this.startLocNud_ValueChanged);
            // 
            // AITypeCB
            // 
            this.AITypeCB.FormattingEnabled = true;
            this.AITypeCB.Items.AddRange(new object[] {
            "Player",
            "On",
            "Off"});
            this.AITypeCB.Location = new System.Drawing.Point(60, 144);
            this.AITypeCB.Name = "AITypeCB";
            this.AITypeCB.Size = new System.Drawing.Size(121, 21);
            this.AITypeCB.TabIndex = 10;
            this.AITypeCB.TextChanged += new System.EventHandler(this.AITypeCB_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "AI Type";
            // 
            // AIDiffCB
            // 
            this.AIDiffCB.FormattingEnabled = true;
            this.AIDiffCB.Items.AddRange(new object[] {
            "Beginner",
            "Novice",
            "Easy",
            "Intermediate",
            "Normal",
            "Challenging",
            "Tough",
            "Painful",
            "Insane"});
            this.AIDiffCB.Location = new System.Drawing.Point(79, 172);
            this.AIDiffCB.Name = "AIDiffCB";
            this.AIDiffCB.Size = new System.Drawing.Size(102, 21);
            this.AIDiffCB.TabIndex = 12;
            this.AIDiffCB.TextChanged += new System.EventHandler(this.AIDiffCB_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "AI Difficulty";
            // 
            // noSeedBox
            // 
            this.noSeedBox.AutoSize = true;
            this.noSeedBox.Location = new System.Drawing.Point(46, 199);
            this.noSeedBox.Name = "noSeedBox";
            this.noSeedBox.Size = new System.Drawing.Size(73, 17);
            this.noSeedBox.TabIndex = 14;
            this.noSeedBox.Text = "No Nexus";
            this.noSeedBox.UseVisualStyleBackColor = true;
            this.noSeedBox.CheckedChanged += new System.EventHandler(this.noSeedBox_CheckedChanged);
            // 
            // noEngiBox
            // 
            this.noEngiBox.AutoSize = true;
            this.noEngiBox.Location = new System.Drawing.Point(46, 230);
            this.noEngiBox.Name = "noEngiBox";
            this.noEngiBox.Size = new System.Drawing.Size(85, 17);
            this.noEngiBox.TabIndex = 15;
            this.noEngiBox.Text = "No Engineer";
            this.noEngiBox.UseVisualStyleBackColor = true;
            this.noEngiBox.CheckedChanged += new System.EventHandler(this.noEngiBox_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AshesScenarioBuilder1.Properties.Resources.noSeed;
            this.pictureBox1.Location = new System.Drawing.Point(15, 199);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AshesScenarioBuilder1.Properties.Resources.noEngi;
            this.pictureBox2.Location = new System.Drawing.Point(15, 230);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 25);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // colorCB
            // 
            this.colorCB.FormattingEnabled = true;
            this.colorCB.Items.AddRange(new object[] {
            "Red",
            "Blue",
            "Light Brown",
            "Orange",
            "Yellow",
            "Cyan",
            "Green"});
            this.colorCB.Location = new System.Drawing.Point(60, 91);
            this.colorCB.Name = "colorCB";
            this.colorCB.Size = new System.Drawing.Size(121, 21);
            this.colorCB.TabIndex = 18;
            this.colorCB.TextChanged += new System.EventHandler(this.colorCB_TextChanged);
            // 
            // PlayerEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(200, 266);
            this.Controls.Add(this.colorCB);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.noEngiBox);
            this.Controls.Add(this.noSeedBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AIDiffCB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.AITypeCB);
            this.Controls.Add(this.startLocNud);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.teamNUD);
            this.Controls.Add(this.factionCB);
            this.Controls.Add(this.nameBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayerEditWindow";
            this.Text = "PlayerEditWindow";
            this.Load += new System.EventHandler(this.PlayerEditWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.teamNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startLocNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.ComboBox factionCB;
        private System.Windows.Forms.NumericUpDown teamNUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown startLocNud;
        private System.Windows.Forms.ComboBox AITypeCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox AIDiffCB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox noSeedBox;
        private System.Windows.Forms.CheckBox noEngiBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox colorCB;
    }
}