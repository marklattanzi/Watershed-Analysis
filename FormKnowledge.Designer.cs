namespace warmf {
	partial class FormKnowledge {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnustripKnowledge = new System.Windows.Forms.ToolStripMenuItem();
			this.miKnowEngr = new System.Windows.Forms.ToolStripMenuItem();
			this.miKnowData = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnustripKnowledge,
			this.exitToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1065, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mnustripKnowledge
			// 
			this.mnustripKnowledge.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.miKnowEngr,
			this.miKnowData});
			this.mnustripKnowledge.Name = "mnustripKnowledge";
			this.mnustripKnowledge.Size = new System.Drawing.Size(60, 20);
			this.mnustripKnowledge.Text = "&Module";
			// 
			// miKnowEngr
			// 
			this.miKnowEngr.Name = "miKnowEngr";
			this.miKnowEngr.Size = new System.Drawing.Size(180, 22);
			this.miKnowEngr.Text = "Engineering";
			this.miKnowEngr.Click += new System.EventHandler(this.miKnowEngr_Click);
			// 
			// miKnowData
			// 
			this.miKnowData.Name = "miKnowData";
			this.miKnowData.Size = new System.Drawing.Size(180, 22);
			this.miKnowData.Text = "Data";
			this.miKnowData.Click += new System.EventHandler(this.miKnowData_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// txtTitle
			// 
			this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTitle.Location = new System.Drawing.Point(349, 264);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(307, 44);
			this.txtTitle.TabIndex = 1;
			this.txtTitle.Text = "Knowledge Module";
			// 
			// frmKnowledge
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1065, 608);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmKnowledge";
			this.Text = "Knowledge Module";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnustripKnowledge;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.ToolStripMenuItem miKnowEngr;
		private System.Windows.Forms.ToolStripMenuItem miKnowData;
	}
}