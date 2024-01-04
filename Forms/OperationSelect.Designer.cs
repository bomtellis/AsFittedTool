namespace FireAlarmTool.Forms
{
    partial class OperationSelect
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
            this.CSVGroupBox = new System.Windows.Forms.GroupBox();
            this.FilePathBtn = new System.Windows.Forms.Button();
            this.FilePathTxtBox = new System.Windows.Forms.TextBox();
            this.FilePathLabel = new System.Windows.Forms.Label();
            this.ActionGroupBox = new System.Windows.Forms.GroupBox();
            this.BlockComboBox = new System.Windows.Forms.ComboBox();
            this.BlocksLabel = new System.Windows.Forms.Label();
            this.ActionComboBox = new System.Windows.Forms.ComboBox();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.NextBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.CSVFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.CSVGroupBox.SuspendLayout();
            this.ActionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CSVGroupBox
            // 
            this.CSVGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CSVGroupBox.Controls.Add(this.FilePathBtn);
            this.CSVGroupBox.Controls.Add(this.FilePathTxtBox);
            this.CSVGroupBox.Controls.Add(this.FilePathLabel);
            this.CSVGroupBox.Location = new System.Drawing.Point(12, 12);
            this.CSVGroupBox.Name = "CSVGroupBox";
            this.CSVGroupBox.Size = new System.Drawing.Size(345, 68);
            this.CSVGroupBox.TabIndex = 0;
            this.CSVGroupBox.TabStop = false;
            this.CSVGroupBox.Text = "CSV Details";
            // 
            // FilePathBtn
            // 
            this.FilePathBtn.Location = new System.Drawing.Point(261, 30);
            this.FilePathBtn.Name = "FilePathBtn";
            this.FilePathBtn.Size = new System.Drawing.Size(75, 23);
            this.FilePathBtn.TabIndex = 3;
            this.FilePathBtn.Text = "Pick File";
            this.FilePathBtn.UseVisualStyleBackColor = true;
            this.FilePathBtn.Click += new System.EventHandler(this.FilePathBtn_Click);
            // 
            // FilePathTxtBox
            // 
            this.FilePathTxtBox.Enabled = false;
            this.FilePathTxtBox.Location = new System.Drawing.Point(9, 32);
            this.FilePathTxtBox.Name = "FilePathTxtBox";
            this.FilePathTxtBox.Size = new System.Drawing.Size(246, 20);
            this.FilePathTxtBox.TabIndex = 2;
            // 
            // FilePathLabel
            // 
            this.FilePathLabel.AutoSize = true;
            this.FilePathLabel.Location = new System.Drawing.Point(6, 16);
            this.FilePathLabel.Name = "FilePathLabel";
            this.FilePathLabel.Size = new System.Drawing.Size(51, 13);
            this.FilePathLabel.TabIndex = 1;
            this.FilePathLabel.Text = "File Path:";
            // 
            // ActionGroupBox
            // 
            this.ActionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActionGroupBox.Controls.Add(this.BlockComboBox);
            this.ActionGroupBox.Controls.Add(this.BlocksLabel);
            this.ActionGroupBox.Controls.Add(this.ActionComboBox);
            this.ActionGroupBox.Controls.Add(this.ActionLabel);
            this.ActionGroupBox.Location = new System.Drawing.Point(12, 86);
            this.ActionGroupBox.Name = "ActionGroupBox";
            this.ActionGroupBox.Size = new System.Drawing.Size(345, 112);
            this.ActionGroupBox.TabIndex = 1;
            this.ActionGroupBox.TabStop = false;
            this.ActionGroupBox.Text = "Operation";
            // 
            // BlockComboBox
            // 
            this.BlockComboBox.FormattingEnabled = true;
            this.BlockComboBox.Items.AddRange(new object[] {
            "Room Label",
            "Fire Alarm Symbols",
            "P4 Symbols"});
            this.BlockComboBox.Location = new System.Drawing.Point(9, 72);
            this.BlockComboBox.Name = "BlockComboBox";
            this.BlockComboBox.Size = new System.Drawing.Size(327, 21);
            this.BlockComboBox.TabIndex = 2;
            this.BlockComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.BlockComboBox_Validating);
            // 
            // BlocksLabel
            // 
            this.BlocksLabel.AutoSize = true;
            this.BlocksLabel.Location = new System.Drawing.Point(6, 56);
            this.BlocksLabel.Name = "BlocksLabel";
            this.BlocksLabel.Size = new System.Drawing.Size(42, 13);
            this.BlocksLabel.TabIndex = 3;
            this.BlocksLabel.Text = "Blocks:";
            // 
            // ActionComboBox
            // 
            this.ActionComboBox.FormattingEnabled = true;
            this.ActionComboBox.Items.AddRange(new object[] {
            "Sequential",
            "Bulk",
            "Update"});
            this.ActionComboBox.Location = new System.Drawing.Point(9, 32);
            this.ActionComboBox.Name = "ActionComboBox";
            this.ActionComboBox.Size = new System.Drawing.Size(327, 21);
            this.ActionComboBox.TabIndex = 2;
            this.ActionComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ActionComboBox_Validating);
            // 
            // ActionLabel
            // 
            this.ActionLabel.AutoSize = true;
            this.ActionLabel.Location = new System.Drawing.Point(6, 16);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(40, 13);
            this.ActionLabel.TabIndex = 2;
            this.ActionLabel.Text = "Action:";
            // 
            // NextBtn
            // 
            this.NextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextBtn.Location = new System.Drawing.Point(282, 222);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(75, 23);
            this.NextBtn.TabIndex = 2;
            this.NextBtn.Text = "Next";
            this.NextBtn.UseVisualStyleBackColor = true;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.Location = new System.Drawing.Point(201, 222);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // CSVFileDialog
            // 
            this.CSVFileDialog.FileName = "openFileDialog1";
            // 
            // OperationSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 257);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.NextBtn);
            this.Controls.Add(this.ActionGroupBox);
            this.Controls.Add(this.CSVGroupBox);
            this.Name = "OperationSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OperationSelect";
            this.CSVGroupBox.ResumeLayout(false);
            this.CSVGroupBox.PerformLayout();
            this.ActionGroupBox.ResumeLayout(false);
            this.ActionGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox CSVGroupBox;
        private System.Windows.Forms.TextBox FilePathTxtBox;
        private System.Windows.Forms.Label FilePathLabel;
        private System.Windows.Forms.Button FilePathBtn;
        private System.Windows.Forms.GroupBox ActionGroupBox;
        private System.Windows.Forms.ComboBox BlockComboBox;
        private System.Windows.Forms.Label BlocksLabel;
        private System.Windows.Forms.ComboBox ActionComboBox;
        private System.Windows.Forms.Label ActionLabel;
        private System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.OpenFileDialog CSVFileDialog;
    }
}