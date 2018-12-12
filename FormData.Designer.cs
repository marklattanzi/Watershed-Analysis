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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.toolGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miDataEngr = new System.Windows.Forms.ToolStripMenuItem();
			this.miDataKnow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolDataGrid = new System.Windows.Forms.DataGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chkboxStdDev = new System.Windows.Forms.CheckBox();
			this.chkboxAverage = new System.Windows.Forms.CheckBox();
			this.tboxStdDev = new System.Windows.Forms.TextBox();
			this.tboxAverage = new System.Windows.Forms.TextBox();
			this.tboxFilename = new System.Windows.Forms.TextBox();
			this.tboxLongitude = new System.Windows.Forms.TextBox();
			this.tboxLatitude = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.lblLongitude = new System.Windows.Forms.Label();
			this.lblLatitude = new System.Windows.Forms.Label();
			this.lblData = new System.Windows.Forms.Label();
			this.cboxData = new System.Windows.Forms.ComboBox();
			this.gboxGraphChart = new System.Windows.Forms.GroupBox();
			this.radioChart = new System.Windows.Forms.RadioButton();
			this.radioGraph = new System.Windows.Forms.RadioButton();
			this.lblFilename = new System.Windows.Forms.Label();
			this.lblTypeOfData = new System.Windows.Forms.Label();
			this.cboxFilename = new System.Windows.Forms.ComboBox();
			this.cboxTypeOfFile = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.toolGraph)).BeginInit();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolDataGrid)).BeginInit();
			this.panel1.SuspendLayout();
			this.gboxGraphChart.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolGraph
			// 
			this.toolGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			chartArea5.Name = "ChartArea1";
			this.toolGraph.ChartAreas.Add(chartArea5);
			this.toolGraph.DataSource = this.toolGraph.Images;
			legend5.Enabled = false;
			legend5.Name = "Legend1";
			this.toolGraph.Legends.Add(legend5);
			this.toolGraph.Location = new System.Drawing.Point(0, 101);
			this.toolGraph.Name = "toolChart";
			series5.ChartArea = "ChartArea1";
			series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
			series5.Legend = "Legend1";
			series5.Name = "data";
			this.toolGraph.Series.Add(series5);
			this.toolGraph.Size = new System.Drawing.Size(1319, 538);
			this.toolGraph.TabIndex = 0;
			this.toolGraph.Text = "WARMF Chart";
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
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
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
			// toolDataGrid
			// 
			this.toolDataGrid.AllowUserToDeleteRows = false;
			this.toolDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.toolDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.toolDataGrid.Location = new System.Drawing.Point(0, 111);
			this.toolDataGrid.Name = "toolDataGrid";
			this.toolDataGrid.Size = new System.Drawing.Size(1284, 518);
			this.toolDataGrid.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.chkboxStdDev);
			this.panel1.Controls.Add(this.chkboxAverage);
			this.panel1.Controls.Add(this.tboxStdDev);
			this.panel1.Controls.Add(this.tboxAverage);
			this.panel1.Controls.Add(this.tboxFilename);
			this.panel1.Controls.Add(this.tboxLongitude);
			this.panel1.Controls.Add(this.tboxLatitude);
			this.panel1.Controls.Add(this.lblName);
			this.panel1.Controls.Add(this.lblLongitude);
			this.panel1.Controls.Add(this.lblLatitude);
			this.panel1.Controls.Add(this.lblData);
			this.panel1.Controls.Add(this.cboxData);
			this.panel1.Controls.Add(this.gboxGraphChart);
			this.panel1.Controls.Add(this.lblFilename);
			this.panel1.Controls.Add(this.lblTypeOfData);
			this.panel1.Controls.Add(this.cboxFilename);
			this.panel1.Controls.Add(this.cboxTypeOfFile);
			this.panel1.Location = new System.Drawing.Point(0, 27);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1309, 78);
			this.panel1.TabIndex = 3;
			// 
			// chkboxStdDev
			// 
			this.chkboxStdDev.AutoSize = true;
			this.chkboxStdDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkboxStdDev.Location = new System.Drawing.Point(1028, 35);
			this.chkboxStdDev.Name = "chkboxStdDev";
			this.chkboxStdDev.Size = new System.Drawing.Size(110, 20);
			this.chkboxStdDev.TabIndex = 20;
			this.chkboxStdDev.Text = "Std Deviation:";
			this.chkboxStdDev.UseVisualStyleBackColor = true;
			this.chkboxStdDev.CheckedChanged += new System.EventHandler(this.chkboxStdDev_CheckedChanged);
			// 
			// chkboxAverage
			// 
			this.chkboxAverage.AutoSize = true;
			this.chkboxAverage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkboxAverage.Location = new System.Drawing.Point(1028, 7);
			this.chkboxAverage.Name = "chkboxAverage";
			this.chkboxAverage.Size = new System.Drawing.Size(82, 20);
			this.chkboxAverage.TabIndex = 19;
			this.chkboxAverage.Text = "Average:";
			this.chkboxAverage.UseVisualStyleBackColor = true;
			this.chkboxAverage.CheckedChanged += new System.EventHandler(this.chkboxAverage_CheckedChanged);
			// 
			// tboxStdDev
			// 
			this.tboxStdDev.Location = new System.Drawing.Point(1144, 34);
			this.tboxStdDev.Name = "tboxStdDev";
			this.tboxStdDev.ReadOnly = true;
			this.tboxStdDev.Size = new System.Drawing.Size(118, 20);
			this.tboxStdDev.TabIndex = 18;
			// 
			// tboxAverage
			// 
			this.tboxAverage.Location = new System.Drawing.Point(1144, 7);
			this.tboxAverage.Name = "tboxAverage";
			this.tboxAverage.ReadOnly = true;
			this.tboxAverage.Size = new System.Drawing.Size(118, 20);
			this.tboxAverage.TabIndex = 17;
			// 
			// tboxFilename
			// 
			this.tboxFilename.Location = new System.Drawing.Point(840, 53);
			this.tboxFilename.Name = "tboxFilename";
			this.tboxFilename.Size = new System.Drawing.Size(118, 20);
			this.tboxFilename.TabIndex = 16;
			// 
			// tboxLongitude
			// 
			this.tboxLongitude.Location = new System.Drawing.Point(840, 28);
			this.tboxLongitude.Name = "tboxLongitude";
			this.tboxLongitude.Size = new System.Drawing.Size(118, 20);
			this.tboxLongitude.TabIndex = 15;
			// 
			// tboxLatitude
			// 
			this.tboxLatitude.Location = new System.Drawing.Point(840, 4);
			this.tboxLatitude.Name = "tboxLatitude";
			this.tboxLatitude.Size = new System.Drawing.Size(118, 20);
			this.tboxLatitude.TabIndex = 14;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(756, 54);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(73, 16);
			this.lblName.TabIndex = 12;
			this.lblName.Text = "File Name:";
			// 
			// lblLongitude
			// 
			this.lblLongitude.AutoSize = true;
			this.lblLongitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLongitude.Location = new System.Drawing.Point(756, 32);
			this.lblLongitude.Name = "lblLongitude";
			this.lblLongitude.Size = new System.Drawing.Size(70, 16);
			this.lblLongitude.TabIndex = 11;
			this.lblLongitude.Text = "Longitude:";
			// 
			// lblLatitude
			// 
			this.lblLatitude.AutoSize = true;
			this.lblLatitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLatitude.Location = new System.Drawing.Point(756, 8);
			this.lblLatitude.Name = "lblLatitude";
			this.lblLatitude.Size = new System.Drawing.Size(58, 16);
			this.lblLatitude.TabIndex = 10;
			this.lblLatitude.Text = "Latitude:";
			// 
			// lblData
			// 
			this.lblData.AutoSize = true;
			this.lblData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblData.Location = new System.Drawing.Point(402, 12);
			this.lblData.Name = "lblData";
			this.lblData.Size = new System.Drawing.Size(40, 16);
			this.lblData.TabIndex = 9;
			this.lblData.Text = "Data:";
			// 
			// cboxData
			// 
			this.cboxData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboxData.FormattingEnabled = true;
			this.cboxData.Location = new System.Drawing.Point(405, 33);
			this.cboxData.Name = "cboxData";
			this.cboxData.Size = new System.Drawing.Size(202, 24);
			this.cboxData.TabIndex = 8;
			this.cboxData.SelectedIndexChanged += new System.EventHandler(this.cboxData_SelectedIndexChanged);
			// 
			// gboxGraphChart
			// 
			this.gboxGraphChart.Controls.Add(this.radioChart);
			this.gboxGraphChart.Controls.Add(this.radioGraph);
			this.gboxGraphChart.Location = new System.Drawing.Point(634, 3);
			this.gboxGraphChart.Name = "gboxGraphChart";
			this.gboxGraphChart.Size = new System.Drawing.Size(84, 62);
			this.gboxGraphChart.TabIndex = 7;
			this.gboxGraphChart.TabStop = false;
			// 
			// radioChart
			// 
			this.radioChart.AutoSize = true;
			this.radioChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioChart.Location = new System.Drawing.Point(6, 31);
			this.radioChart.Name = "radioChart";
			this.radioChart.Size = new System.Drawing.Size(57, 20);
			this.radioChart.TabIndex = 1;
			this.radioChart.Text = "Chart";
			this.radioChart.UseVisualStyleBackColor = true;
			this.radioChart.CheckedChanged += new System.EventHandler(this.radioChart_CheckedChanged);
			// 
			// radioGraph
			// 
			this.radioGraph.AutoSize = true;
			this.radioGraph.Checked = true;
			this.radioGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioGraph.Location = new System.Drawing.Point(6, 9);
			this.radioGraph.Name = "radioGraph";
			this.radioGraph.Size = new System.Drawing.Size(63, 20);
			this.radioGraph.TabIndex = 0;
			this.radioGraph.TabStop = true;
			this.radioGraph.Text = "Graph";
			this.radioGraph.UseVisualStyleBackColor = true;
			this.radioGraph.CheckedChanged += new System.EventHandler(this.radioGraph_CheckedChanged);
			// 
			// lblFilename
			// 
			this.lblFilename.AutoSize = true;
			this.lblFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFilename.Location = new System.Drawing.Point(208, 12);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(73, 16);
			this.lblFilename.TabIndex = 4;
			this.lblFilename.Text = "File Name:";
			// 
			// lblTypeOfData
			// 
			this.lblTypeOfData.AutoSize = true;
			this.lblTypeOfData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTypeOfData.Location = new System.Drawing.Point(12, 14);
			this.lblTypeOfData.Name = "lblTypeOfData";
			this.lblTypeOfData.Size = new System.Drawing.Size(89, 16);
			this.lblTypeOfData.TabIndex = 3;
			this.lblTypeOfData.Text = "Type of Data:";
			// 
			// cboxFilename
			// 
			this.cboxFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboxFilename.FormattingEnabled = true;
			this.cboxFilename.Location = new System.Drawing.Point(211, 33);
			this.cboxFilename.Name = "cboxFilename";
			this.cboxFilename.Size = new System.Drawing.Size(156, 24);
			this.cboxFilename.TabIndex = 2;
			this.cboxFilename.SelectedIndexChanged += new System.EventHandler(this.cboxFilename_SelectedIndexChanged);
			// 
			// cboxTypeOfFile
			// 
			this.cboxTypeOfFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboxTypeOfFile.FormattingEnabled = true;
			this.cboxTypeOfFile.Location = new System.Drawing.Point(15, 33);
			this.cboxTypeOfFile.Name = "cboxTypeOfFile";
			this.cboxTypeOfFile.Size = new System.Drawing.Size(167, 24);
			this.cboxTypeOfFile.TabIndex = 1;
			this.cboxTypeOfFile.SelectedIndexChanged += new System.EventHandler(this.cboxTypeOfFile_SelectedIndexChanged);
			// 
			// FormData
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(1309, 670);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toolDataGrid);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.toolGraph);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormData";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.Text = "Data Module";
			((System.ComponentModel.ISupportInitialize)(this.toolGraph)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolDataGrid)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.gboxGraphChart.ResumeLayout(false);
			this.gboxGraphChart.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart toolGraph;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moduleToolStripMenuItem;
		private System.Windows.Forms.DataGridView toolDataGrid;
		private System.Windows.Forms.ToolStripMenuItem miDataEngr;
		private System.Windows.Forms.ToolStripMenuItem miDataKnow;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblFilename;
		private System.Windows.Forms.Label lblTypeOfData;
		private System.Windows.Forms.ComboBox cboxFilename;
		private System.Windows.Forms.ComboBox cboxTypeOfFile;
		private System.Windows.Forms.GroupBox gboxGraphChart;
		private System.Windows.Forms.RadioButton radioChart;
		private System.Windows.Forms.RadioButton radioGraph;
		private System.Windows.Forms.ComboBox cboxData;
		private System.Windows.Forms.Label lblData;
		private System.Windows.Forms.Label lblLongitude;
		private System.Windows.Forms.Label lblLatitude;
		private System.Windows.Forms.CheckBox chkboxStdDev;
		private System.Windows.Forms.CheckBox chkboxAverage;
		private System.Windows.Forms.TextBox tboxStdDev;
		private System.Windows.Forms.TextBox tboxAverage;
		private System.Windows.Forms.TextBox tboxFilename;
		private System.Windows.Forms.TextBox tboxLongitude;
		private System.Windows.Forms.TextBox tboxLatitude;
		private System.Windows.Forms.Label lblName;
	}
}