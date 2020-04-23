namespace warmf.Module_Data
{
    using System;
    using System.Windows.Forms;

    partial class DialogImportDelimitedFile
    {
        int NumHeaderLines;
        
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
            this.ImportDelimitedDataGrid = new System.Windows.Forms.DataGridView();
            this.FileNameSuffix = new System.Windows.Forms.TextBox();
            this.FileNameSuffixLabel = new System.Windows.Forms.Label();
            this.ImportDelimitedFileHelp = new System.Windows.Forms.Button();
            this.ImportDelimitedFileCancel = new System.Windows.Forms.Button();
            this.ImportDelimitedFileOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImportDelimitedDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ImportDelimitedDataGrid
            // 
            this.ImportDelimitedDataGrid.AllowUserToDeleteRows = false;
            this.ImportDelimitedDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportDelimitedDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ImportDelimitedDataGrid.Location = new System.Drawing.Point(8, 8);
            this.ImportDelimitedDataGrid.Name = "ImportDelimitedDataGrid";
            this.ImportDelimitedDataGrid.Size = new System.Drawing.Size(785, 368);
            this.ImportDelimitedDataGrid.TabIndex = 3;
            this.ImportDelimitedDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ImportDelimitedDataGrid_CellContentClick);
            this.ImportDelimitedDataGrid.CellValueChanged += new DataGridViewCellEventHandler(ImportDelimitedDataGrid_CellValueChanged);
            this.ImportDelimitedDataGrid.CurrentCellDirtyStateChanged += new EventHandler(ImportDelimitedDataGrid_CurrentCellDirtyStateChanged);
            this.ImportDelimitedDataGrid.DataError += new DataGridViewDataErrorEventHandler(ImportDelimitedDataGrid_DataError);
            // 
            // FileNameSuffix
            // 
            this.FileNameSuffix.Location = new System.Drawing.Point(395, 382);
            this.FileNameSuffix.Name = "FileNameSuffix";
            this.FileNameSuffix.Size = new System.Drawing.Size(100, 20);
            this.FileNameSuffix.TabIndex = 4;
            // 
            // FileNameSuffixLabel
            // 
            this.FileNameSuffixLabel.AutoSize = true;
            this.FileNameSuffixLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileNameSuffixLabel.Location = new System.Drawing.Point(290, 385);
            this.FileNameSuffixLabel.Name = "FileNameSuffixLabel";
            this.FileNameSuffixLabel.Size = new System.Drawing.Size(104, 16);
            this.FileNameSuffixLabel.TabIndex = 5;
            this.FileNameSuffixLabel.Text = "File Name Suffix";
            this.FileNameSuffixLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ImportDelimitedFileHelp
            // 
            this.ImportDelimitedFileHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportDelimitedFileHelp.Location = new System.Drawing.Point(444, 413);
            this.ImportDelimitedFileHelp.Name = "ImportDelimitedFileHelp";
            this.ImportDelimitedFileHelp.Size = new System.Drawing.Size(75, 30);
            this.ImportDelimitedFileHelp.TabIndex = 12;
            this.ImportDelimitedFileHelp.Text = "Help";
            this.ImportDelimitedFileHelp.UseVisualStyleBackColor = true;
            this.ImportDelimitedFileHelp.Click += new System.EventHandler(this.ImportFileFormatHelp_Click);
            // 
            // ImportDelimitedFileCancel
            // 
            this.ImportDelimitedFileCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ImportDelimitedFileCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportDelimitedFileCancel.Location = new System.Drawing.Point(363, 413);
            this.ImportDelimitedFileCancel.Name = "ImportDelimitedFileCancel";
            this.ImportDelimitedFileCancel.Size = new System.Drawing.Size(75, 30);
            this.ImportDelimitedFileCancel.TabIndex = 11;
            this.ImportDelimitedFileCancel.Text = "Cancel";
            this.ImportDelimitedFileCancel.UseVisualStyleBackColor = true;
            // 
            // ImportDelimitedFileOK
            // 
            this.ImportDelimitedFileOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ImportDelimitedFileOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportDelimitedFileOK.Location = new System.Drawing.Point(282, 413);
            this.ImportDelimitedFileOK.Name = "ImportDelimitedFileOK";
            this.ImportDelimitedFileOK.Size = new System.Drawing.Size(75, 30);
            this.ImportDelimitedFileOK.TabIndex = 10;
            this.ImportDelimitedFileOK.Text = "OK";
            this.ImportDelimitedFileOK.UseVisualStyleBackColor = true;
            // 
            // DialogImportDelimitedFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ImportDelimitedFileHelp);
            this.Controls.Add(this.ImportDelimitedFileCancel);
            this.Controls.Add(this.ImportDelimitedFileOK);
            this.Controls.Add(this.FileNameSuffixLabel);
            this.Controls.Add(this.FileNameSuffix);
            this.Controls.Add(this.ImportDelimitedDataGrid);
            this.Name = "DialogImportDelimitedFile";
            this.Text = "Import Delimited Data";
            ((System.ComponentModel.ISupportInitialize)(this.ImportDelimitedDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ImportDelimitedDataGrid;
        private System.Windows.Forms.TextBox FileNameSuffix;
        private System.Windows.Forms.Label FileNameSuffixLabel;
        private System.Windows.Forms.Button ImportDelimitedFileHelp;
        private System.Windows.Forms.Button ImportDelimitedFileCancel;
        private System.Windows.Forms.Button ImportDelimitedFileOK;
    }
}