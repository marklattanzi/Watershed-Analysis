namespace warmf
{
    partial class DialogAddSepticType
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
            this.tbNewSepticSystemType = new System.Windows.Forms.TextBox();
            this.labelNewSepticSystemType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbNewSepticSystemType
            // 
            this.tbNewSepticSystemType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNewSepticSystemType.Location = new System.Drawing.Point(17, 50);
            this.tbNewSepticSystemType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbNewSepticSystemType.Name = "tbNewSepticSystemType";
            this.tbNewSepticSystemType.Size = new System.Drawing.Size(331, 26);
            this.tbNewSepticSystemType.TabIndex = 17;
            // 
            // labelNewSepticSystemType
            // 
            this.labelNewSepticSystemType.AutoSize = true;
            this.labelNewSepticSystemType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNewSepticSystemType.Location = new System.Drawing.Point(13, 23);
            this.labelNewSepticSystemType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNewSepticSystemType.Name = "labelNewSepticSystemType";
            this.labelNewSepticSystemType.Size = new System.Drawing.Size(335, 20);
            this.labelNewSepticSystemType.TabIndex = 16;
            this.labelNewSepticSystemType.Text = "Enter the name of the new septic system type:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(182, 97);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(167, 44);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(18, 97);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(156, 44);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // DialogAddSepticType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 161);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbNewSepticSystemType);
            this.Controls.Add(this.labelNewSepticSystemType);
            this.Name = "DialogAddSepticType";
            this.Text = "New Septic System Type";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbNewSepticSystemType;
        private System.Windows.Forms.Label labelNewSepticSystemType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}