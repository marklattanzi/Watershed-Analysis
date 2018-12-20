﻿namespace warmf {
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
			this.mnuModule = new System.Windows.Forms.ToolStripMenuItem();
			this.miEngineering = new System.Windows.Forms.ToolStripMenuItem();
			this.miData = new System.Windows.Forms.ToolStripMenuItem();
			this.miTMDL = new System.Windows.Forms.ToolStripMenuItem();
			this.miConsensus = new System.Windows.Forms.ToolStripMenuItem();
			this.miManager = new System.Windows.Forms.ToolStripMenuItem();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuModule,
            this.miExit});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1065, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mnuModule
			// 
			this.mnuModule.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEngineering,
            this.miData,
            this.miTMDL,
            this.miConsensus,
            this.miManager});
			this.mnuModule.Name = "mnuModule";
			this.mnuModule.Size = new System.Drawing.Size(60, 20);
			this.mnuModule.Text = "&Module";
			// 
			// miEngineering
			// 
			this.miEngineering.Name = "miEngineering";
			this.miEngineering.Size = new System.Drawing.Size(180, 22);
			this.miEngineering.Text = "Engineering";
			this.miEngineering.Click += new System.EventHandler(this.miEngineering_Click);
			// 
			// miData
			// 
			this.miData.Name = "miData";
			this.miData.Size = new System.Drawing.Size(180, 22);
			this.miData.Text = "Data";
			this.miData.Click += new System.EventHandler(this.miData_Click);
			// 
			// miTMDL
			// 
			this.miTMDL.Name = "miTMDL";
			this.miTMDL.Size = new System.Drawing.Size(180, 22);
			this.miTMDL.Text = "TMDL";
			this.miTMDL.Click += new System.EventHandler(this.miTMDL_Click);
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
			// txtTitle
			// 
			this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTitle.Location = new System.Drawing.Point(349, 264);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(307, 44);
			this.txtTitle.TabIndex = 1;
			this.txtTitle.Text = "Knowledge Module";
			// 
			// FormKnowledge
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1065, 608);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormKnowledge";
			this.Text = "Knowledge Module";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuModule;
		private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.ToolStripMenuItem miEngineering;
		private System.Windows.Forms.ToolStripMenuItem miData;
		private System.Windows.Forms.ToolStripMenuItem miTMDL;
		private System.Windows.Forms.ToolStripMenuItem miConsensus;
		private System.Windows.Forms.ToolStripMenuItem miManager;
	}
}