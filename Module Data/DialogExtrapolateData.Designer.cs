namespace warmf
{
    partial class ExtrapolateData
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
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.TruncateBeforeDate = new System.Windows.Forms.DateTimePicker();
            this.ExtrapolateDataDateLabel = new System.Windows.Forms.Label();
            this.toolDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.toolDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(494, 574);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(137, 39);
            this.btnHelp.TabIndex = 35;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(333, 574);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 39);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(169, 574);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(137, 39);
            this.btnOK.TabIndex = 33;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // TruncateBeforeDate
            // 
            this.TruncateBeforeDate.CustomFormat = "MM/dd/yyyy";
            this.TruncateBeforeDate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TruncateBeforeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TruncateBeforeDate.Location = new System.Drawing.Point(470, 10);
            this.TruncateBeforeDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.TruncateBeforeDate.Name = "TruncateBeforeDate";
            this.TruncateBeforeDate.ShowUpDown = true;
            this.TruncateBeforeDate.Size = new System.Drawing.Size(106, 26);
            this.TruncateBeforeDate.TabIndex = 32;
            // 
            // ExtrapolateDataDateLabel
            // 
            this.ExtrapolateDataDateLabel.AutoSize = true;
            this.ExtrapolateDataDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExtrapolateDataDateLabel.Location = new System.Drawing.Point(215, 13);
            this.ExtrapolateDataDateLabel.Name = "ExtrapolateDataDateLabel";
            this.ExtrapolateDataDateLabel.Size = new System.Drawing.Size(254, 20);
            this.ExtrapolateDataDateLabel.TabIndex = 36;
            this.ExtrapolateDataDateLabel.Text = "Extrapolate data through this date:";
            this.ExtrapolateDataDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolDataGrid
            // 
            this.toolDataGrid.AllowUserToDeleteRows = false;
            this.toolDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.toolDataGrid.Location = new System.Drawing.Point(12, 42);
            this.toolDataGrid.Name = "toolDataGrid";
            this.toolDataGrid.Size = new System.Drawing.Size(776, 515);
            this.toolDataGrid.TabIndex = 37;
            // 
            // ExtrapolateData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 630);
            this.Controls.Add(this.toolDataGrid);
            this.Controls.Add(this.ExtrapolateDataDateLabel);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.TruncateBeforeDate);
            this.Name = "ExtrapolateData";
            this.Text = "Extrapolate Data";
            ((System.ComponentModel.ISupportInitialize)(this.toolDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DateTimePicker TruncateBeforeDate;
        private System.Windows.Forms.Label ExtrapolateDataDateLabel;
        private System.Windows.Forms.DataGridView toolDataGrid;
    }
}