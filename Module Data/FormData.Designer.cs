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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.columnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillMissingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrapolateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.truncateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDelimitedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importHECDSSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModule = new System.Windows.Forms.ToolStripMenuItem();
            this.miEngineering = new System.Windows.Forms.ToolStripMenuItem();
            this.miKnowledge = new System.Windows.Forms.ToolStripMenuItem();
            this.miTMDL = new System.Windows.Forms.ToolStripMenuItem();
            this.miConsensus = new System.Windows.Forms.ToolStripMenuItem();
            this.miManager = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDataGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkboxStdDev = new System.Windows.Forms.CheckBox();
            this.chkboxAverage = new System.Windows.Forms.CheckBox();
            this.tboxStdDev = new System.Windows.Forms.TextBox();
            this.tboxAverage = new System.Windows.Forms.TextBox();
            this.tboxName = new System.Windows.Forms.TextBox();
            this.tboxLongitude = new System.Windows.Forms.TextBox();
            this.tboxLatitude = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.cboxData = new System.Windows.Forms.ComboBox();
            this.gboxGraphChart = new System.Windows.Forms.GroupBox();
            this.radioTable = new System.Windows.Forms.RadioButton();
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
            chartArea2.Name = "ChartArea1";
            this.toolGraph.ChartAreas.Add(chartArea2);
            this.toolGraph.DataSource = this.toolGraph.Images;
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.toolGraph.Legends.Add(legend2);
            this.toolGraph.Location = new System.Drawing.Point(0, 101);
            this.toolGraph.Name = "toolGraph";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.Name = "data";
            this.toolGraph.Series.Add(series2);
            this.toolGraph.Size = new System.Drawing.Size(1038, 538);
            this.toolGraph.TabIndex = 0;
            this.toolGraph.Text = "WARMF Chart";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuModule,
            this.miExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1028, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.columnsToolStripMenuItem,
            this.sortByToolStripMenuItem,
            this.fillMissingDataToolStripMenuItem,
            this.extrapolateToolStripMenuItem,
            this.truncateToolStripMenuItem,
            this.importDelimitedToolStripMenuItem,
            this.importHECDSSToolStripMenuItem});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(37, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // columnsToolStripMenuItem
            // 
            this.columnsToolStripMenuItem.Enabled = false;
            this.columnsToolStripMenuItem.Name = "columnsToolStripMenuItem";
            this.columnsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.columnsToolStripMenuItem.Text = "&Columns";
            this.columnsToolStripMenuItem.Click += new System.EventHandler(this.columnsToolStripMenuItem_Click);
            // 
            // sortByToolStripMenuItem
            // 
            this.sortByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateToolStripMenuItem,
            this.dataSourceToolStripMenuItem});
            this.sortByToolStripMenuItem.Enabled = false;
            this.sortByToolStripMenuItem.Name = "sortByToolStripMenuItem";
            this.sortByToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sortByToolStripMenuItem.Text = "&Sort by...";
            // 
            // dateToolStripMenuItem
            // 
            this.dateToolStripMenuItem.Name = "dateToolStripMenuItem";
            this.dateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dateToolStripMenuItem.Text = "&Date";
            this.dateToolStripMenuItem.Click += new System.EventHandler(this.dateToolStripMenuItem_Click);
            // 
            // dataSourceToolStripMenuItem
            // 
            this.dataSourceToolStripMenuItem.Name = "dataSourceToolStripMenuItem";
            this.dataSourceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dataSourceToolStripMenuItem.Text = "Data &Source";
            this.dataSourceToolStripMenuItem.Click += new System.EventHandler(this.dataSourceToolStripMenuItem_Click);
            // 
            // fillMissingDataToolStripMenuItem
            // 
            this.fillMissingDataToolStripMenuItem.Enabled = false;
            this.fillMissingDataToolStripMenuItem.Name = "fillMissingDataToolStripMenuItem";
            this.fillMissingDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fillMissingDataToolStripMenuItem.Text = "&Fill Missing Data";
            this.fillMissingDataToolStripMenuItem.Click += new System.EventHandler(this.fillMissingDataToolStripMenuItem_Click);
            // 
            // extrapolateToolStripMenuItem
            // 
            this.extrapolateToolStripMenuItem.Name = "extrapolateToolStripMenuItem";
            this.extrapolateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.extrapolateToolStripMenuItem.Text = "&Extrapolate";
            this.extrapolateToolStripMenuItem.Click += new System.EventHandler(this.extrapolateToolStripMenuItem_Click);
            // 
            // truncateToolStripMenuItem
            // 
            this.truncateToolStripMenuItem.Name = "truncateToolStripMenuItem";
            this.truncateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.truncateToolStripMenuItem.Text = "&Truncate";
            this.truncateToolStripMenuItem.Click += new System.EventHandler(this.truncateToolStripMenuItem_Click);
            // 
            // importDelimitedToolStripMenuItem
            // 
            this.importDelimitedToolStripMenuItem.Name = "importDelimitedToolStripMenuItem";
            this.importDelimitedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importDelimitedToolStripMenuItem.Text = "Import &Delimited";
            this.importDelimitedToolStripMenuItem.Click += new System.EventHandler(this.importDelimitedToolStripMenuItem_Click);
            // 
            // importHECDSSToolStripMenuItem
            // 
            this.importHECDSSToolStripMenuItem.Name = "importHECDSSToolStripMenuItem";
            this.importHECDSSToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importHECDSSToolStripMenuItem.Text = "Import &HEC-DSS";
            this.importHECDSSToolStripMenuItem.Click += new System.EventHandler(this.importHECDSSToolStripMenuItem_Click);
            // 
            // mnuModule
            // 
            this.mnuModule.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEngineering,
            this.miKnowledge,
            this.miTMDL,
            this.miConsensus,
            this.miManager});
            this.mnuModule.Name = "mnuModule";
            this.mnuModule.Size = new System.Drawing.Size(53, 20);
            this.mnuModule.Text = "Module";
            // 
            // miEngineering
            // 
            this.miEngineering.Name = "miEngineering";
            this.miEngineering.Size = new System.Drawing.Size(130, 22);
            this.miEngineering.Text = "Engineering";
            this.miEngineering.Click += new System.EventHandler(this.miEngineering_Click);
            // 
            // miKnowledge
            // 
            this.miKnowledge.Name = "miKnowledge";
            this.miKnowledge.Size = new System.Drawing.Size(130, 22);
            this.miKnowledge.Text = "Knowledge";
            this.miKnowledge.Click += new System.EventHandler(this.miKnowledge_Click);
            // 
            // miTMDL
            // 
            this.miTMDL.Name = "miTMDL";
            this.miTMDL.Size = new System.Drawing.Size(130, 22);
            this.miTMDL.Text = "TMDL";
            // 
            // miConsensus
            // 
            this.miConsensus.Name = "miConsensus";
            this.miConsensus.Size = new System.Drawing.Size(130, 22);
            this.miConsensus.Text = "Consensus";
            // 
            // miManager
            // 
            this.miManager.Name = "miManager";
            this.miManager.Size = new System.Drawing.Size(130, 22);
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
            // toolDataGrid
            // 
            this.toolDataGrid.AllowUserToDeleteRows = false;
            this.toolDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.toolDataGrid.Location = new System.Drawing.Point(0, 111);
            this.toolDataGrid.Name = "toolDataGrid";
            this.toolDataGrid.Size = new System.Drawing.Size(1003, 518);
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
            this.panel1.Controls.Add(this.tboxName);
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
            this.panel1.Size = new System.Drawing.Size(1028, 78);
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
            // tboxName
            // 
            this.tboxName.Location = new System.Drawing.Point(840, 53);
            this.tboxName.Name = "tboxName";
            this.tboxName.Size = new System.Drawing.Size(118, 20);
            this.tboxName.TabIndex = 16;
            this.tboxName.TextChanged += new System.EventHandler(this.tbox_TextChanged);
            // 
            // tboxLongitude
            // 
            this.tboxLongitude.Location = new System.Drawing.Point(840, 28);
            this.tboxLongitude.Name = "tboxLongitude";
            this.tboxLongitude.Size = new System.Drawing.Size(118, 20);
            this.tboxLongitude.TabIndex = 15;
            this.tboxLongitude.TextChanged += new System.EventHandler(this.tbox_TextChanged);
            // 
            // tboxLatitude
            // 
            this.tboxLatitude.Location = new System.Drawing.Point(840, 4);
            this.tboxLatitude.Name = "tboxLatitude";
            this.tboxLatitude.Size = new System.Drawing.Size(118, 20);
            this.tboxLatitude.TabIndex = 14;
            this.tboxLatitude.TextChanged += new System.EventHandler(this.tbox_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(756, 54);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 16);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Name:";
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
            this.gboxGraphChart.Controls.Add(this.radioTable);
            this.gboxGraphChart.Controls.Add(this.radioGraph);
            this.gboxGraphChart.Location = new System.Drawing.Point(634, 3);
            this.gboxGraphChart.Name = "gboxGraphChart";
            this.gboxGraphChart.Size = new System.Drawing.Size(84, 62);
            this.gboxGraphChart.TabIndex = 7;
            this.gboxGraphChart.TabStop = false;
            // 
            // radioTable
            // 
            this.radioTable.AutoSize = true;
            this.radioTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioTable.Location = new System.Drawing.Point(6, 31);
            this.radioTable.Name = "radioTable";
            this.radioTable.Size = new System.Drawing.Size(62, 20);
            this.radioTable.TabIndex = 1;
            this.radioTable.Text = "Table";
            this.radioTable.UseVisualStyleBackColor = true;
            this.radioTable.CheckedChanged += new System.EventHandler(this.radioTableGraph_CheckedChanged);
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
            this.radioGraph.CheckedChanged += new System.EventHandler(this.radioTableGraph_CheckedChanged);
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
            this.ClientSize = new System.Drawing.Size(1028, 670);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.toolDataGrid);
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
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem mnuModule;
		private System.Windows.Forms.DataGridView toolDataGrid;
		private System.Windows.Forms.ToolStripMenuItem miEngineering;
		private System.Windows.Forms.ToolStripMenuItem miKnowledge;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblFilename;
		private System.Windows.Forms.Label lblTypeOfData;
		private System.Windows.Forms.ComboBox cboxFilename;
		private System.Windows.Forms.ComboBox cboxTypeOfFile;
		private System.Windows.Forms.GroupBox gboxGraphChart;
		private System.Windows.Forms.RadioButton radioTable;
		private System.Windows.Forms.RadioButton radioGraph;
		private System.Windows.Forms.ComboBox cboxData;
		private System.Windows.Forms.Label lblData;
		private System.Windows.Forms.Label lblLongitude;
		private System.Windows.Forms.Label lblLatitude;
		private System.Windows.Forms.CheckBox chkboxStdDev;
		private System.Windows.Forms.CheckBox chkboxAverage;
		private System.Windows.Forms.TextBox tboxStdDev;
		private System.Windows.Forms.TextBox tboxAverage;
		private System.Windows.Forms.TextBox tboxName;
		private System.Windows.Forms.TextBox tboxLongitude;
		private System.Windows.Forms.TextBox tboxLatitude;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.ToolStripMenuItem miManager;
		private System.Windows.Forms.ToolStripMenuItem miTMDL;
		private System.Windows.Forms.ToolStripMenuItem miConsensus;
		private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem columnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillMissingDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrapolateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem truncateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDelimitedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importHECDSSToolStripMenuItem;
    }
}