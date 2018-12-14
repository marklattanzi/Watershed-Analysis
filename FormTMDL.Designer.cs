namespace warmf {
	partial class FormTMDL {
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
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.miEngineering = new System.Windows.Forms.ToolStripMenuItem();
			this.miTMDLEngr = new System.Windows.Forms.ToolStripMenuItem();
			this.miData = new System.Windows.Forms.ToolStripMenuItem();
			this.miKnowledge = new System.Windows.Forms.ToolStripMenuItem();
			this.miConsensus = new System.Windows.Forms.ToolStripMenuItem();
			this.miManager = new System.Windows.Forms.ToolStripMenuItem();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtTitle
			// 
			this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTitle.Location = new System.Drawing.Point(247, 203);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(307, 44);
			this.txtTitle.TabIndex = 2;
			this.txtTitle.Text = "TMDL Module";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEngineering,
            this.miExit});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// miEngineering
			// 
			this.miEngineering.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTMDLEngr,
            this.miData,
            this.miKnowledge,
            this.miConsensus,
            this.miManager});
			this.miEngineering.Name = "miEngineering";
			this.miEngineering.Size = new System.Drawing.Size(60, 20);
			this.miEngineering.Text = "&Module";
			// 
			// miTMDLEngr
			// 
			this.miTMDLEngr.Name = "miTMDLEngr";
			this.miTMDLEngr.Size = new System.Drawing.Size(180, 22);
			this.miTMDLEngr.Text = "Engineering";
			this.miTMDLEngr.Click += new System.EventHandler(this.miEngineering_Click);
			// 
			// miData
			// 
			this.miData.Name = "miData";
			this.miData.Size = new System.Drawing.Size(180, 22);
			this.miData.Text = "Data";
			this.miData.Click += new System.EventHandler(this.miData_Click);
			// 
			// miKnowledge
			// 
			this.miKnowledge.Name = "miKnowledge";
			this.miKnowledge.Size = new System.Drawing.Size(180, 22);
			this.miKnowledge.Text = "Knowledge";
			this.miKnowledge.Click += new System.EventHandler(this.miKnowledge_Click);
			// 
			// miConsensus
			// 
			this.miConsensus.Name = "miConsensus";
			this.miConsensus.Size = new System.Drawing.Size(180, 22);
			this.miConsensus.Text = "Consensus";
			this.miConsensus.Click += new System.EventHandler(this.miConsensus_Click);
			// 
			// miManager
			// 
			this.miManager.Name = "miManager";
			this.miManager.Size = new System.Drawing.Size(180, 22);
			this.miManager.Text = "Manager";
			this.miManager.Click += new System.EventHandler(this.miManager_Click);
			// 
			// miExit
			// 
			this.miExit.Name = "miExit";
			this.miExit.Size = new System.Drawing.Size(37, 20);
			this.miExit.Text = "E&xit";
			this.miExit.Click += new System.EventHandler(this.miExit_Click);
			// 
			// FormTMDL
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormTMDL";
			this.Text = "FormTMDL";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem miEngineering;
		private System.Windows.Forms.ToolStripMenuItem miTMDLEngr;
		private System.Windows.Forms.ToolStripMenuItem miData;
		private System.Windows.Forms.ToolStripMenuItem miKnowledge;
		private System.Windows.Forms.ToolStripMenuItem miManager;
		private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.ToolStripMenuItem miConsensus;
	}
}