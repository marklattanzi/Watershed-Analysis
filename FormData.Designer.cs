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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.toolChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miDataGraph = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miDataEngr = new System.Windows.Forms.ToolStripMenuItem();
			this.miDataKnow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolGrid = new System.Windows.Forms.DataGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.gboxGraphChart = new System.Windows.Forms.GroupBox();
			this.radioChart = new System.Windows.Forms.RadioButton();
			this.radioGraph = new System.Windows.Forms.RadioButton();
			this.lblFilename = new System.Windows.Forms.Label();
			this.lblTypeOfData = new System.Windows.Forms.Label();
			this.cboxFilename = new System.Windows.Forms.ComboBox();
			this.cboxTypeOfData = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.toolChart)).BeginInit();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolGrid)).BeginInit();
			this.panel1.SuspendLayout();
			this.gboxGraphChart.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolChart
			// 
			this.toolChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			chartArea4.Name = "ChartArea1";
			this.toolChart.ChartAreas.Add(chartArea4);
			this.toolChart.DataSource = this.toolChart.Images;
			legend4.Enabled = false;
			legend4.Name = "Legend1";
			this.toolChart.Legends.Add(legend4);
			this.toolChart.Location = new System.Drawing.Point(21, 111);
			this.toolChart.Name = "toolChart";
			series4.ChartArea = "ChartArea1";
			series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
			series4.Legend = "Legend1";
			series4.Name = "data";
			this.toolChart.Series.Add(series4);
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
			this.toolGrid.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.toolGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.toolGrid.Location = new System.Drawing.Point(0, 111);
			this.toolGrid.Name = "toolGrid";
			this.toolGrid.Size = new System.Drawing.Size(1284, 518);
			this.toolGrid.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.gboxGraphChart);
			this.panel1.Controls.Add(this.lblFilename);
			this.panel1.Controls.Add(this.lblTypeOfData);
			this.panel1.Controls.Add(this.cboxFilename);
			this.panel1.Controls.Add(this.cboxTypeOfData);
			this.panel1.Location = new System.Drawing.Point(0, 27);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1309, 78);
			this.panel1.TabIndex = 3;
			// 
			// gboxGraphChart
			// 
			this.gboxGraphChart.Controls.Add(this.radioChart);
			this.gboxGraphChart.Controls.Add(this.radioGraph);
			this.gboxGraphChart.Location = new System.Drawing.Point(279, 5);
			this.gboxGraphChart.Name = "gboxGraphChart";
			this.gboxGraphChart.Size = new System.Drawing.Size(74, 61);
			this.gboxGraphChart.TabIndex = 7;
			this.gboxGraphChart.TabStop = false;
			// 
			// radioChart
			// 
			this.radioChart.AutoSize = true;
			this.radioChart.Location = new System.Drawing.Point(6, 32);
			this.radioChart.Name = "radioChart";
			this.radioChart.Size = new System.Drawing.Size(50, 17);
			this.radioChart.TabIndex = 1;
			this.radioChart.Text = "Chart";
			this.radioChart.UseVisualStyleBackColor = true;
			// 
			// radioGraph
			// 
			this.radioGraph.AutoSize = true;
			this.radioGraph.Checked = true;
			this.radioGraph.Location = new System.Drawing.Point(6, 9);
			this.radioGraph.Name = "radioGraph";
			this.radioGraph.Size = new System.Drawing.Size(54, 17);
			this.radioGraph.TabIndex = 0;
			this.radioGraph.TabStop = true;
			this.radioGraph.Text = "Graph";
			this.radioGraph.UseVisualStyleBackColor = true;
			// 
			// lblFilename
			// 
			this.lblFilename.AutoSize = true;
			this.lblFilename.Location = new System.Drawing.Point(32, 48);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(57, 13);
			this.lblFilename.TabIndex = 4;
			this.lblFilename.Text = "File Name:";
			// 
			// lblTypeOfData
			// 
			this.lblTypeOfData.AutoSize = true;
			this.lblTypeOfData.Location = new System.Drawing.Point(17, 20);
			this.lblTypeOfData.Name = "lblTypeOfData";
			this.lblTypeOfData.Size = new System.Drawing.Size(72, 13);
			this.lblTypeOfData.TabIndex = 3;
			this.lblTypeOfData.Text = "Type of Data:";
			// 
			// cboxFilename
			// 
			this.cboxFilename.FormattingEnabled = true;
			this.cboxFilename.Location = new System.Drawing.Point(105, 45);
			this.cboxFilename.Name = "cboxFilename";
			this.cboxFilename.Size = new System.Drawing.Size(153, 21);
			this.cboxFilename.TabIndex = 2;
			this.cboxFilename.SelectedIndexChanged += new System.EventHandler(this.cboxFilename_SelectedIndexChanged);
			// 
			// cboxTypeOfData
			// 
			this.cboxTypeOfData.FormattingEnabled = true;
			this.cboxTypeOfData.Location = new System.Drawing.Point(105, 12);
			this.cboxTypeOfData.Name = "cboxTypeOfData";
			this.cboxTypeOfData.Size = new System.Drawing.Size(153, 21);
			this.cboxTypeOfData.TabIndex = 1;
			this.cboxTypeOfData.SelectedIndexChanged += new System.EventHandler(this.cboxTypeOfData_SelectedIndexChanged);
			// 
			// FormData
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(1309, 670);
			this.Controls.Add(this.panel1);
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
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.gboxGraphChart.ResumeLayout(false);
			this.gboxGraphChart.PerformLayout();
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
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblFilename;
		private System.Windows.Forms.Label lblTypeOfData;
		private System.Windows.Forms.ComboBox cboxFilename;
		private System.Windows.Forms.ComboBox cboxTypeOfData;
		private System.Windows.Forms.GroupBox gboxGraphChart;
		private System.Windows.Forms.RadioButton radioChart;
		private System.Windows.Forms.RadioButton radioGraph;
	}
}