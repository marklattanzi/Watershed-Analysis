namespace warmf
{
    partial class DialogAddLayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAddLayer));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbNewLayerName = new System.Windows.Forms.TextBox();
            this.labelNewLayerName = new System.Windows.Forms.Label();
            this.cbAddLayerType = new System.Windows.Forms.ComboBox();
            this.labelAddLayerType = new System.Windows.Forms.Label();
            this.appManager1 = new DotSpatial.Controls.AppManager();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(224, 118);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(167, 44);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(60, 118);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(156, 44);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // tbNewLayerName
            // 
            this.tbNewLayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNewLayerName.Location = new System.Drawing.Point(105, 20);
            this.tbNewLayerName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbNewLayerName.Name = "tbNewLayerName";
            this.tbNewLayerName.Size = new System.Drawing.Size(331, 26);
            this.tbNewLayerName.TabIndex = 21;
            // 
            // labelNewLayerName
            // 
            this.labelNewLayerName.AutoSize = true;
            this.labelNewLayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNewLayerName.Location = new System.Drawing.Point(13, 23);
            this.labelNewLayerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNewLayerName.Name = "labelNewLayerName";
            this.labelNewLayerName.Size = new System.Drawing.Size(98, 20);
            this.labelNewLayerName.TabIndex = 20;
            this.labelNewLayerName.Text = "Layer Name:";
            // 
            // cbAddLayerType
            // 
            this.cbAddLayerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAddLayerType.FormattingEnabled = true;
            this.cbAddLayerType.Location = new System.Drawing.Point(105, 63);
            this.cbAddLayerType.Name = "cbAddLayerType";
            this.cbAddLayerType.Size = new System.Drawing.Size(331, 28);
            this.cbAddLayerType.TabIndex = 24;
            // 
            // labelAddLayerType
            // 
            this.labelAddLayerType.AutoSize = true;
            this.labelAddLayerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddLayerType.Location = new System.Drawing.Point(13, 66);
            this.labelAddLayerType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAddLayerType.Name = "labelAddLayerType";
            this.labelAddLayerType.Size = new System.Drawing.Size(90, 20);
            this.labelAddLayerType.TabIndex = 25;
            this.labelAddLayerType.Text = "Layer Type:";
            // 
            // appManager1
            // 
            this.appManager1.Directories = ((System.Collections.Generic.List<string>)(resources.GetObject("appManager1.Directories")));
            this.appManager1.DockManager = null;
            this.appManager1.HeaderControl = null;
            this.appManager1.Legend = null;
            this.appManager1.Map = null;
            this.appManager1.ProgressHandler = null;
            this.appManager1.ShowExtensionsDialogMode = DotSpatial.Controls.ShowExtensionsDialogMode.Default;
            // 
            // DialogAddLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 181);
            this.Controls.Add(this.labelAddLayerType);
            this.Controls.Add(this.cbAddLayerType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbNewLayerName);
            this.Controls.Add(this.labelNewLayerName);
            this.Name = "DialogAddLayer";
            this.Text = "Add Layer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.TextBox tbNewLayerName;
        private System.Windows.Forms.Label labelNewLayerName;
        public System.Windows.Forms.ComboBox cbAddLayerType;
        private System.Windows.Forms.Label labelAddLayerType;
        private DotSpatial.Controls.AppManager appManager1;
    }
}