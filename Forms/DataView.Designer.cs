namespace FireAlarmTool.Forms
{
    partial class DataView
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
            this.DataViewGroupBox = new System.Windows.Forms.GroupBox();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.GoBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SelectAllBtn = new System.Windows.Forms.Button();
            this.UnselectAllBtn = new System.Windows.Forms.Button();
            this.AutoPlacementCheckBox = new System.Windows.Forms.CheckBox();
            this.DeselectPresentBtn = new System.Windows.Forms.Button();
            this.MapBtn = new System.Windows.Forms.Button();
            this.DataViewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DataViewGroupBox
            // 
            this.DataViewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataViewGroupBox.Controls.Add(this.DataGridView);
            this.DataViewGroupBox.Location = new System.Drawing.Point(12, 12);
            this.DataViewGroupBox.Name = "DataViewGroupBox";
            this.DataViewGroupBox.Size = new System.Drawing.Size(1360, 659);
            this.DataViewGroupBox.TabIndex = 0;
            this.DataViewGroupBox.TabStop = false;
            this.DataViewGroupBox.Text = "Drawing Items";
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AllowUserToDeleteRows = false;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView.Location = new System.Drawing.Point(3, 16);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.Size = new System.Drawing.Size(1354, 640);
            this.DataGridView.TabIndex = 0;
            this.DataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDown);
            this.DataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.DataGridView_CurrentCellDirtyStateChanged);
            // 
            // GoBtn
            // 
            this.GoBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GoBtn.Location = new System.Drawing.Point(1294, 677);
            this.GoBtn.Name = "GoBtn";
            this.GoBtn.Size = new System.Drawing.Size(75, 23);
            this.GoBtn.TabIndex = 1;
            this.GoBtn.Text = "Add to CAD";
            this.GoBtn.UseVisualStyleBackColor = true;
            this.GoBtn.Click += new System.EventHandler(this.GoBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.Location = new System.Drawing.Point(1213, 677);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SelectAllBtn
            // 
            this.SelectAllBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectAllBtn.Location = new System.Drawing.Point(93, 677);
            this.SelectAllBtn.Name = "SelectAllBtn";
            this.SelectAllBtn.Size = new System.Drawing.Size(75, 23);
            this.SelectAllBtn.TabIndex = 3;
            this.SelectAllBtn.Text = "Select All";
            this.SelectAllBtn.UseVisualStyleBackColor = true;
            this.SelectAllBtn.Click += new System.EventHandler(this.SelectAllBtn_Click);
            // 
            // UnselectAllBtn
            // 
            this.UnselectAllBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UnselectAllBtn.Location = new System.Drawing.Point(12, 677);
            this.UnselectAllBtn.Name = "UnselectAllBtn";
            this.UnselectAllBtn.Size = new System.Drawing.Size(75, 23);
            this.UnselectAllBtn.TabIndex = 4;
            this.UnselectAllBtn.Text = "Unselect All";
            this.UnselectAllBtn.UseVisualStyleBackColor = true;
            this.UnselectAllBtn.Click += new System.EventHandler(this.UnselectAllBtn_Click);
            // 
            // AutoPlacementCheckBox
            // 
            this.AutoPlacementCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoPlacementCheckBox.AutoSize = true;
            this.AutoPlacementCheckBox.Location = new System.Drawing.Point(293, 681);
            this.AutoPlacementCheckBox.Name = "AutoPlacementCheckBox";
            this.AutoPlacementCheckBox.Size = new System.Drawing.Size(271, 17);
            this.AutoPlacementCheckBox.TabIndex = 5;
            this.AutoPlacementCheckBox.Text = "Automatically place blocks where room labels match";
            this.AutoPlacementCheckBox.UseVisualStyleBackColor = true;
            // 
            // DeselectPresentBtn
            // 
            this.DeselectPresentBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeselectPresentBtn.Location = new System.Drawing.Point(174, 677);
            this.DeselectPresentBtn.Name = "DeselectPresentBtn";
            this.DeselectPresentBtn.Size = new System.Drawing.Size(113, 23);
            this.DeselectPresentBtn.TabIndex = 6;
            this.DeselectPresentBtn.Text = "Deselect Inserted";
            this.DeselectPresentBtn.UseVisualStyleBackColor = true;
            this.DeselectPresentBtn.Click += new System.EventHandler(this.DeselectPresentBtn_Click);
            // 
            // MapBtn
            // 
            this.MapBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MapBtn.Location = new System.Drawing.Point(1107, 677);
            this.MapBtn.Name = "MapBtn";
            this.MapBtn.Size = new System.Drawing.Size(100, 23);
            this.MapBtn.TabIndex = 7;
            this.MapBtn.Text = "Block Mapping";
            this.MapBtn.UseVisualStyleBackColor = true;
            this.MapBtn.Click += new System.EventHandler(this.MapBtn_Click);
            // 
            // DataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 711);
            this.Controls.Add(this.MapBtn);
            this.Controls.Add(this.DeselectPresentBtn);
            this.Controls.Add(this.AutoPlacementCheckBox);
            this.Controls.Add(this.UnselectAllBtn);
            this.Controls.Add(this.SelectAllBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.GoBtn);
            this.Controls.Add(this.DataViewGroupBox);
            this.Name = "DataView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataView";
            this.DataViewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox DataViewGroupBox;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.Button GoBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SelectAllBtn;
        private System.Windows.Forms.Button UnselectAllBtn;
        private System.Windows.Forms.CheckBox AutoPlacementCheckBox;
        private System.Windows.Forms.Button DeselectPresentBtn;
        private System.Windows.Forms.Button MapBtn;
    }
}