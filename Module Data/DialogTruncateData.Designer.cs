namespace warmf
{
    partial class TruncateData
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
            this.TruncateBeforeDate = new System.Windows.Forms.DateTimePicker();
            this.TruncateAfterDate = new System.Windows.Forms.DateTimePicker();
            this.TruncateDataBeforeCheckBox = new System.Windows.Forms.CheckBox();
            this.TruncateDataAfterCheckBox = new System.Windows.Forms.CheckBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TruncateBeforeDate
            // 
            this.TruncateBeforeDate.CustomFormat = "MM/dd/yyyy";
            this.TruncateBeforeDate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TruncateBeforeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TruncateBeforeDate.Location = new System.Drawing.Point(336, 17);
            this.TruncateBeforeDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.TruncateBeforeDate.Name = "TruncateBeforeDate";
            this.TruncateBeforeDate.ShowUpDown = true;
            this.TruncateBeforeDate.Size = new System.Drawing.Size(106, 26);
            this.TruncateBeforeDate.TabIndex = 1;
            // 
            // TruncateAfterDate
            // 
            this.TruncateAfterDate.CustomFormat = "MM/dd/yyyy";
            this.TruncateAfterDate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TruncateAfterDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TruncateAfterDate.Location = new System.Drawing.Point(336, 48);
            this.TruncateAfterDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.TruncateAfterDate.Name = "TruncateAfterDate";
            this.TruncateAfterDate.ShowUpDown = true;
            this.TruncateAfterDate.Size = new System.Drawing.Size(106, 26);
            this.TruncateAfterDate.TabIndex = 2;
            // 
            // TruncateDataBeforeCheckBox
            // 
            this.TruncateDataBeforeCheckBox.AutoSize = true;
            this.TruncateDataBeforeCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TruncateDataBeforeCheckBox.Location = new System.Drawing.Point(50, 19);
            this.TruncateDataBeforeCheckBox.Name = "TruncateDataBeforeCheckBox";
            this.TruncateDataBeforeCheckBox.Size = new System.Drawing.Size(286, 24);
            this.TruncateDataBeforeCheckBox.TabIndex = 15;
            this.TruncateDataBeforeCheckBox.Text = "Truncate all data BEFORE this date:";
            this.TruncateDataBeforeCheckBox.UseVisualStyleBackColor = true;
            // 
            // TruncateDataAfterCheckBox
            // 
            this.TruncateDataAfterCheckBox.AutoSize = true;
            this.TruncateDataAfterCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TruncateDataAfterCheckBox.Location = new System.Drawing.Point(50, 50);
            this.TruncateDataAfterCheckBox.Name = "TruncateDataAfterCheckBox";
            this.TruncateDataAfterCheckBox.Size = new System.Drawing.Size(272, 24);
            this.TruncateDataAfterCheckBox.TabIndex = 16;
            this.TruncateDataAfterCheckBox.Text = "Truncate all data AFTER this date:";
            this.TruncateDataAfterCheckBox.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(340, 87);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(137, 39);
            this.btnHelp.TabIndex = 31;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(179, 87);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 39);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(15, 87);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(137, 39);
            this.btnOK.TabIndex = 29;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // TruncateData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 143);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.TruncateDataAfterCheckBox);
            this.Controls.Add(this.TruncateDataBeforeCheckBox);
            this.Controls.Add(this.TruncateAfterDate);
            this.Controls.Add(this.TruncateBeforeDate);
            this.Name = "TruncateData";
            this.Text = "Truncate Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker TruncateBeforeDate;
        private System.Windows.Forms.DateTimePicker TruncateAfterDate;
        private System.Windows.Forms.CheckBox TruncateDataBeforeCheckBox;
        private System.Windows.Forms.CheckBox TruncateDataAfterCheckBox;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}