namespace warmf.Module_Data
{
    public partial class DialogImportFileFormat
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
            this.NumberOfIgnoreLinesLabel = new System.Windows.Forms.Label();
            this.NumberOfHeaderLinesLabel = new System.Windows.Forms.Label();
            this.NumberOfLinesToIgnore = new System.Windows.Forms.TextBox();
            this.NumberOfHeaderLines = new System.Windows.Forms.TextBox();
            this.OtherDelimiter = new System.Windows.Forms.TextBox();
            this.DelimiterLabel = new System.Windows.Forms.Label();
            this.ImportFileFormatOK = new System.Windows.Forms.Button();
            this.ImportFileFormatCancel = new System.Windows.Forms.Button();
            this.ImportFileFormatHelp = new System.Windows.Forms.Button();
            this.RadioComma = new System.Windows.Forms.RadioButton();
            this.RadioTab = new System.Windows.Forms.RadioButton();
            this.RadioSpace = new System.Windows.Forms.RadioButton();
            this.RadioOther = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // NumberOfIgnoreLinesLabel
            // 
            this.NumberOfIgnoreLinesLabel.AutoSize = true;
            this.NumberOfIgnoreLinesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfIgnoreLinesLabel.Location = new System.Drawing.Point(4, 8);
            this.NumberOfIgnoreLinesLabel.Name = "NumberOfIgnoreLinesLabel";
            this.NumberOfIgnoreLinesLabel.Size = new System.Drawing.Size(185, 16);
            this.NumberOfIgnoreLinesLabel.TabIndex = 0;
            this.NumberOfIgnoreLinesLabel.Text = "Number of Lines to Ignore";
            // 
            // NumberOfHeaderLinesLabel
            // 
            this.NumberOfHeaderLinesLabel.AutoSize = true;
            this.NumberOfHeaderLinesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfHeaderLinesLabel.Location = new System.Drawing.Point(4, 33);
            this.NumberOfHeaderLinesLabel.Name = "NumberOfHeaderLinesLabel";
            this.NumberOfHeaderLinesLabel.Size = new System.Drawing.Size(176, 16);
            this.NumberOfHeaderLinesLabel.TabIndex = 1;
            this.NumberOfHeaderLinesLabel.Text = "Number of Header Lines";
            // 
            // NumberOfLinesToIgnore
            // 
            this.NumberOfLinesToIgnore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfLinesToIgnore.Location = new System.Drawing.Point(190, 5);
            this.NumberOfLinesToIgnore.Name = "NumberOfLinesToIgnore";
            this.NumberOfLinesToIgnore.Size = new System.Drawing.Size(41, 22);
            this.NumberOfLinesToIgnore.TabIndex = 2;
            this.NumberOfLinesToIgnore.TextChanged += new System.EventHandler(this.NumberOfLinesToIgnore_TextChanged);
            this.NumberOfLinesToIgnore.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOfLinesToIgnore_KeyPress);
            // 
            // NumberOfHeaderLines
            // 
            this.NumberOfHeaderLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfHeaderLines.Location = new System.Drawing.Point(190, 30);
            this.NumberOfHeaderLines.Name = "NumberOfHeaderLines";
            this.NumberOfHeaderLines.Size = new System.Drawing.Size(41, 22);
            this.NumberOfHeaderLines.TabIndex = 3;
            this.NumberOfHeaderLines.TextChanged += new System.EventHandler(this.NumberOfHeaderLines_TextChanged);
            this.NumberOfHeaderLines.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOfHeaderLines_KeyPress);
            // 
            // OtherDelimiter
            // 
            this.OtherDelimiter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OtherDelimiter.Location = new System.Drawing.Point(130, 126);
            this.OtherDelimiter.Name = "OtherDelimiter";
            this.OtherDelimiter.Size = new System.Drawing.Size(41, 22);
            this.OtherDelimiter.TabIndex = 4;
            this.OtherDelimiter.TextChanged += new System.EventHandler(this.OtherDelimiter_TextChanged);
            // 
            // DelimiterLabel
            // 
            this.DelimiterLabel.AutoSize = true;
            this.DelimiterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DelimiterLabel.Location = new System.Drawing.Point(75, 56);
            this.DelimiterLabel.Name = "DelimiterLabel";
            this.DelimiterLabel.Size = new System.Drawing.Size(74, 16);
            this.DelimiterLabel.TabIndex = 6;
            this.DelimiterLabel.Text = "Delimiter:";
            // 
            // ImportFileFormatOK
            // 
            this.ImportFileFormatOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ImportFileFormatOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFileFormatOK.Location = new System.Drawing.Point(6, 156);
            this.ImportFileFormatOK.Name = "ImportFileFormatOK";
            this.ImportFileFormatOK.Size = new System.Drawing.Size(75, 30);
            this.ImportFileFormatOK.TabIndex = 7;
            this.ImportFileFormatOK.Text = "OK";
            this.ImportFileFormatOK.UseVisualStyleBackColor = true;
            // 
            // ImportFileFormatCancel
            // 
            this.ImportFileFormatCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ImportFileFormatCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFileFormatCancel.Location = new System.Drawing.Point(87, 156);
            this.ImportFileFormatCancel.Name = "ImportFileFormatCancel";
            this.ImportFileFormatCancel.Size = new System.Drawing.Size(75, 30);
            this.ImportFileFormatCancel.TabIndex = 8;
            this.ImportFileFormatCancel.Text = "Cancel";
            this.ImportFileFormatCancel.UseVisualStyleBackColor = true;
            // 
            // ImportFileFormatHelp
            // 
            this.ImportFileFormatHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFileFormatHelp.Location = new System.Drawing.Point(168, 156);
            this.ImportFileFormatHelp.Name = "ImportFileFormatHelp";
            this.ImportFileFormatHelp.Size = new System.Drawing.Size(75, 30);
            this.ImportFileFormatHelp.TabIndex = 9;
            this.ImportFileFormatHelp.Text = "Help";
            this.ImportFileFormatHelp.UseVisualStyleBackColor = true;
            // 
            // RadioComma
            // 
            this.RadioComma.AutoSize = true;
            this.RadioComma.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioComma.Location = new System.Drawing.Point(64, 72);
            this.RadioComma.Name = "RadioComma";
            this.RadioComma.Size = new System.Drawing.Size(73, 20);
            this.RadioComma.TabIndex = 10;
            this.RadioComma.TabStop = true;
            this.RadioComma.Text = "Comma";
            this.RadioComma.UseVisualStyleBackColor = true;
            this.RadioComma.CheckedChanged += new System.EventHandler(this.RadioComma_CheckedChanged);
            // 
            // RadioTab
            // 
            this.RadioTab.AutoSize = true;
            this.RadioTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioTab.Location = new System.Drawing.Point(64, 89);
            this.RadioTab.Name = "RadioTab";
            this.RadioTab.Size = new System.Drawing.Size(51, 20);
            this.RadioTab.TabIndex = 11;
            this.RadioTab.TabStop = true;
            this.RadioTab.Text = "Tab";
            this.RadioTab.UseVisualStyleBackColor = true;
            this.RadioTab.CheckedChanged += new System.EventHandler(this.RadioTab_CheckedChanged);
            // 
            // RadioSpace
            // 
            this.RadioSpace.AutoSize = true;
            this.RadioSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioSpace.Location = new System.Drawing.Point(64, 107);
            this.RadioSpace.Name = "RadioSpace";
            this.RadioSpace.Size = new System.Drawing.Size(66, 20);
            this.RadioSpace.TabIndex = 12;
            this.RadioSpace.TabStop = true;
            this.RadioSpace.Text = "Space";
            this.RadioSpace.UseVisualStyleBackColor = true;
            this.RadioSpace.CheckedChanged += new System.EventHandler(this.RadioSpace_CheckedChanged);
            // 
            // RadioOther
            // 
            this.RadioOther.AutoSize = true;
            this.RadioOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioOther.Location = new System.Drawing.Point(64, 126);
            this.RadioOther.Name = "RadioOther";
            this.RadioOther.Size = new System.Drawing.Size(58, 20);
            this.RadioOther.TabIndex = 13;
            this.RadioOther.TabStop = true;
            this.RadioOther.Text = "Other";
            this.RadioOther.UseVisualStyleBackColor = true;
            this.RadioOther.CheckedChanged += new System.EventHandler(this.RadioOther_CheckedChanged);
            // 
            // DialogImportFileFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 193);
            this.Controls.Add(this.RadioOther);
            this.Controls.Add(this.RadioSpace);
            this.Controls.Add(this.RadioTab);
            this.Controls.Add(this.RadioComma);
            this.Controls.Add(this.ImportFileFormatHelp);
            this.Controls.Add(this.ImportFileFormatCancel);
            this.Controls.Add(this.ImportFileFormatOK);
            this.Controls.Add(this.DelimiterLabel);
            this.Controls.Add(this.OtherDelimiter);
            this.Controls.Add(this.NumberOfHeaderLines);
            this.Controls.Add(this.NumberOfLinesToIgnore);
            this.Controls.Add(this.NumberOfHeaderLinesLabel);
            this.Controls.Add(this.NumberOfIgnoreLinesLabel);
            this.Name = "DialogImportFileFormat";
            this.Text = "Import File Format";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NumberOfIgnoreLinesLabel;
        private System.Windows.Forms.Label NumberOfHeaderLinesLabel;
        private System.Windows.Forms.TextBox NumberOfLinesToIgnore;
        private System.Windows.Forms.TextBox NumberOfHeaderLines;
        private System.Windows.Forms.TextBox OtherDelimiter;
        private System.Windows.Forms.Label DelimiterLabel;
        private System.Windows.Forms.Button ImportFileFormatOK;
        private System.Windows.Forms.Button ImportFileFormatCancel;
        private System.Windows.Forms.Button ImportFileFormatHelp;
        private System.Windows.Forms.RadioButton RadioComma;
        private System.Windows.Forms.RadioButton RadioTab;
        private System.Windows.Forms.RadioButton RadioSpace;
        private System.Windows.Forms.RadioButton RadioOther;
    }
}