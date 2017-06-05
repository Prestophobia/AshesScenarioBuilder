namespace AshesScenarioBuilder1
{
    partial class DialogWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogWindow));
            this.EntryEditorPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SelectedKey = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectedString = new System.Windows.Forms.TextBox();
            this.DestroyBuildingDelete = new System.Windows.Forms.Button();
            this.SelectedCharacter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AudioOpen = new System.Windows.Forms.Button();
            this.SelectedAudioPath = new System.Windows.Forms.TextBox();
            this.EntryDisplayPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DeleteActionButton = new System.Windows.Forms.Button();
            this.NewEntryButton = new System.Windows.Forms.Button();
            this.SelectedPortrait = new System.Windows.Forms.PictureBox();
            this.EntryEditorPanel.SuspendLayout();
            this.EntryDisplayPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedPortrait)).BeginInit();
            this.SuspendLayout();
            // 
            // EntryEditorPanel
            // 
            this.EntryEditorPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.EntryEditorPanel.Controls.Add(this.label4);
            this.EntryEditorPanel.Controls.Add(this.label3);
            this.EntryEditorPanel.Controls.Add(this.SelectedKey);
            this.EntryEditorPanel.Controls.Add(this.label2);
            this.EntryEditorPanel.Controls.Add(this.SelectedString);
            this.EntryEditorPanel.Controls.Add(this.DestroyBuildingDelete);
            this.EntryEditorPanel.Controls.Add(this.SelectedCharacter);
            this.EntryEditorPanel.Controls.Add(this.label1);
            this.EntryEditorPanel.Controls.Add(this.AudioOpen);
            this.EntryEditorPanel.Controls.Add(this.SelectedAudioPath);
            this.EntryEditorPanel.Controls.Add(this.SelectedPortrait);
            this.EntryEditorPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.EntryEditorPanel.Location = new System.Drawing.Point(354, 0);
            this.EntryEditorPanel.Name = "EntryEditorPanel";
            this.EntryEditorPanel.Size = new System.Drawing.Size(682, 610);
            this.EntryEditorPanel.TabIndex = 0;
            this.EntryEditorPanel.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(102, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 149;
            this.label4.Text = "Character";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 148;
            this.label3.Text = "Text(Internal Key)";
            // 
            // SelectedKey
            // 
            this.SelectedKey.FormattingEnabled = true;
            this.SelectedKey.Location = new System.Drawing.Point(105, 95);
            this.SelectedKey.Name = "SelectedKey";
            this.SelectedKey.Size = new System.Drawing.Size(558, 21);
            this.SelectedKey.TabIndex = 147;
            this.SelectedKey.TextChanged += new System.EventHandler(this.SelectedKey_TextChanged);
            this.SelectedKey.Leave += new System.EventHandler(this.SelectedKey_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 146;
            this.label2.Text = "Text(Translated String)";
            // 
            // SelectedString
            // 
            this.SelectedString.Location = new System.Drawing.Point(12, 135);
            this.SelectedString.Multiline = true;
            this.SelectedString.Name = "SelectedString";
            this.SelectedString.Size = new System.Drawing.Size(651, 371);
            this.SelectedString.TabIndex = 145;
            this.SelectedString.TextChanged += new System.EventHandler(this.SelectedString_TextChanged);
            // 
            // DestroyBuildingDelete
            // 
            this.DestroyBuildingDelete.BackColor = System.Drawing.Color.Red;
            this.DestroyBuildingDelete.ForeColor = System.Drawing.Color.White;
            this.DestroyBuildingDelete.Location = new System.Drawing.Point(12, 571);
            this.DestroyBuildingDelete.Name = "DestroyBuildingDelete";
            this.DestroyBuildingDelete.Size = new System.Drawing.Size(105, 30);
            this.DestroyBuildingDelete.TabIndex = 3;
            this.DestroyBuildingDelete.Text = "DELETE";
            this.DestroyBuildingDelete.UseVisualStyleBackColor = false;
            this.DestroyBuildingDelete.Click += new System.EventHandler(this.DestroyBuildingDelete_Click);
            // 
            // SelectedCharacter
            // 
            this.SelectedCharacter.FormattingEnabled = true;
            this.SelectedCharacter.Items.AddRange(new object[] {
            "Mac",
            "Haalee",
            "Valen",
            "Samuel",
            "Athena",
            "Rygos",
            "Vexen",
            "Relias",
            "Artix",
            "Ventrix",
            "Agethon",
            "Agememnon",
            "Adrastela"});
            this.SelectedCharacter.Location = new System.Drawing.Point(161, 3);
            this.SelectedCharacter.Name = "SelectedCharacter";
            this.SelectedCharacter.Size = new System.Drawing.Size(121, 21);
            this.SelectedCharacter.TabIndex = 144;
            this.SelectedCharacter.TextChanged += new System.EventHandler(this.SelectedCharacter_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 143;
            this.label1.Text = "Audio";
            // 
            // AudioOpen
            // 
            this.AudioOpen.Location = new System.Drawing.Point(588, 522);
            this.AudioOpen.Name = "AudioOpen";
            this.AudioOpen.Size = new System.Drawing.Size(75, 23);
            this.AudioOpen.TabIndex = 142;
            this.AudioOpen.Text = "Open...";
            this.AudioOpen.UseVisualStyleBackColor = true;
            this.AudioOpen.Click += new System.EventHandler(this.AudioOpen_Click);
            // 
            // SelectedAudioPath
            // 
            this.SelectedAudioPath.Location = new System.Drawing.Point(12, 525);
            this.SelectedAudioPath.Name = "SelectedAudioPath";
            this.SelectedAudioPath.Size = new System.Drawing.Size(570, 20);
            this.SelectedAudioPath.TabIndex = 10;
            this.SelectedAudioPath.TextChanged += new System.EventHandler(this.SelectedAudioPath_TextChanged);
            // 
            // EntryDisplayPanel
            // 
            this.EntryDisplayPanel.AutoScroll = true;
            this.EntryDisplayPanel.AutoSize = true;
            this.EntryDisplayPanel.BackColor = System.Drawing.Color.Beige;
            this.EntryDisplayPanel.BackgroundImage = global::AshesScenarioBuilder1.Properties.Resources.window_border;
            this.EntryDisplayPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EntryDisplayPanel.Controls.Add(this.panel1);
            this.EntryDisplayPanel.Controls.Add(this.NewEntryButton);
            this.EntryDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntryDisplayPanel.Location = new System.Drawing.Point(0, 0);
            this.EntryDisplayPanel.Name = "EntryDisplayPanel";
            this.EntryDisplayPanel.Size = new System.Drawing.Size(354, 610);
            this.EntryDisplayPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.DeleteActionButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 558);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 52);
            this.panel1.TabIndex = 1;
            // 
            // DeleteActionButton
            // 
            this.DeleteActionButton.BackColor = System.Drawing.Color.Red;
            this.DeleteActionButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DeleteActionButton.Location = new System.Drawing.Point(12, 5);
            this.DeleteActionButton.Margin = new System.Windows.Forms.Padding(2);
            this.DeleteActionButton.Name = "DeleteActionButton";
            this.DeleteActionButton.Size = new System.Drawing.Size(318, 41);
            this.DeleteActionButton.TabIndex = 2;
            this.DeleteActionButton.Text = "DELETE DIALOG ACTION";
            this.DeleteActionButton.UseVisualStyleBackColor = false;
            this.DeleteActionButton.Click += new System.EventHandler(this.DeleteActionButton_Click);
            // 
            // NewEntryButton
            // 
            this.NewEntryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.NewEntryButton.ForeColor = System.Drawing.Color.White;
            this.NewEntryButton.Location = new System.Drawing.Point(8, 35);
            this.NewEntryButton.Name = "NewEntryButton";
            this.NewEntryButton.Size = new System.Drawing.Size(336, 23);
            this.NewEntryButton.TabIndex = 0;
            this.NewEntryButton.Text = "New Entry";
            this.NewEntryButton.UseVisualStyleBackColor = false;
            this.NewEntryButton.Click += new System.EventHandler(this.NewEntryButton_Click);
            // 
            // SelectedPortrait
            // 
            this.SelectedPortrait.Image = global::AshesScenarioBuilder1.Properties.Resources.na;
            this.SelectedPortrait.Location = new System.Drawing.Point(12, 3);
            this.SelectedPortrait.Name = "SelectedPortrait";
            this.SelectedPortrait.Size = new System.Drawing.Size(85, 110);
            this.SelectedPortrait.TabIndex = 0;
            this.SelectedPortrait.TabStop = false;
            // 
            // DialogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 610);
            this.Controls.Add(this.EntryDisplayPanel);
            this.Controls.Add(this.EntryEditorPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DialogWindow";
            this.Text = "Dialog Editor";
            this.Load += new System.EventHandler(this.DialogWindow_Load);
            this.EntryEditorPanel.ResumeLayout(false);
            this.EntryEditorPanel.PerformLayout();
            this.EntryDisplayPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SelectedPortrait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel EntryEditorPanel;
        private System.Windows.Forms.PictureBox SelectedPortrait;
        private System.Windows.Forms.TextBox SelectedAudioPath;
        private System.Windows.Forms.ComboBox SelectedCharacter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AudioOpen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SelectedKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SelectedString;
        private System.Windows.Forms.Button DestroyBuildingDelete;
        private System.Windows.Forms.Panel EntryDisplayPanel;
        private System.Windows.Forms.Button NewEntryButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button DeleteActionButton;
    }
}