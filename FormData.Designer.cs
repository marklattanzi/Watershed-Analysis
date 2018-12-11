namespace warmf {
	partial class FormData {
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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.toolChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miDataGraph = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miDataEngr = new System.Windows.Forms.ToolStripMenuItem();
			this.miDataKnow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolGrid = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.toolChart)).BeginInit();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// toolChart
			// 
			this.toolChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			chartArea1.Name = "ChartArea1";
			this.toolChart.ChartAreas.Add(chartArea1);
			this.toolChart.DataSource = this.toolChart.Images;
			legend1.Enabled = false;
			legend1.Name = "Legend1";
			this.toolChart.Legends.Add(legend1);
			this.toolChart.Location = new System.Drawing.Point(21, 111);
			this.toolChart.Name = "toolChart";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
			series1.Legend = "Legend1";
			series1.Name = "data";
			this.toolChart.Series.Add(series1);
			this.toolChart.Size = new System.Drawing.Size(1276, 536);
			this.toolChart.TabIndex = 0;
			this.toolChart.Text = "WARMF Chart";

			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.moduleToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1309, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDataGraph});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// miDataGraph
			// 
			this.miDataGraph.Name = "miDataGraph";
			this.miDataGraph.Size = new System.Drawing.Size(106, 22);
			this.miDataGraph.Text = "Graph";
			this.miDataGraph.Click += new System.EventHandler(this.miDataGraph_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// moduleToolStripMenuItem
			// 
			this.moduleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDataEngr,
            this.miDataKnow});
			this.moduleToolStripMenuItem.Name = "moduleToolStripMenuItem";
			this.moduleToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.moduleToolStripMenuItem.Text = "Module";
			// 
			// miDataEngr
			// 
			this.miDataEngr.Name = "miDataEngr";
			this.miDataEngr.Size = new System.Drawing.Size(137, 22);
			this.miDataEngr.Text = "Engineering";
			this.miDataEngr.Click += new System.EventHandler(this.miDataEngr_Click);
			// 
			// miDataKnow
			// 
			this.miDataKnow.Name = "miDataKnow";
			this.miDataKnow.Size = new System.Drawing.Size(137, 22);
			this.miDataKnow.Text = "Knowledge";
			this.miDataKnow.Click += new System.EventHandler(this.miDataKnow_Click);
			// 
			// toolGrid
			// 
			this.toolGrid.AllowUserToDeleteRows = false;
			this.toolGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.toolGrid.Location = new System.Drawing.Point(271, 27);
			this.toolGrid.Name = "toolGrid";
			this.toolGrid.Size = new System.Drawing.Size(679, 602);
			this.toolGrid.TabIndex = 2;
			// 
			// FormData
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(1309, 670);
			this.Controls.Add(this.toolChart);
			this.Controls.Add(this.toolGrid);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormData";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.Text = "Data Module";
			((System.ComponentModel.ISupportInitialize)(this.toolChart)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolGrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart toolChart;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moduleToolStripMenuItem;
		private System.Windows.Forms.DataGridView toolGrid;
		private System.Windows.Forms.ToolStripMenuItem miDataEngr;
		private System.Windows.Forms.ToolStripMenuItem miDataKnow;
		private System.Windows.Forms.ToolStripMenuItem miDataGraph;
	}
}