namespace warmf
{
    partial class DialogRiverCoeffs
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tcRiverTabs = new System.Windows.Forms.TabControl();
            this.tpPhysicalData = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbManningsN = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbImpVolume = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbImpArea = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbDepth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbDownElevation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbUpElevation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbStreamID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpStageWidth = new System.Windows.Forms.TabPage();
            this.btnRedrawChart = new System.Windows.Forms.Button();
            this.chartStageWidth = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvStageWidth = new System.Windows.Forms.DataGridView();
            this.Stage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpDiversions = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.tbMinRiverFlow = new System.Windows.Forms.TextBox();
            this.btnToRemove = new System.Windows.Forms.Button();
            this.btnFromRemove = new System.Windows.Forms.Button();
            this.btnToAdd = new System.Windows.Forms.Button();
            this.btnFromAdd = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbDiversionsTo = new System.Windows.Forms.ListBox();
            this.lbDiversionsFrom = new System.Windows.Forms.ListBox();
            this.tpPointSources = new System.Windows.Forms.TabPage();
            this.tbNPDESNumber = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbUnspecAmbient = new System.Windows.Forms.RadioButton();
            this.rbUnspecZero = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbSourceExternal = new System.Windows.Forms.RadioButton();
            this.rbSourceInternal = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.btnRemovePTS = new System.Windows.Forms.Button();
            this.btnAddPTS = new System.Windows.Forms.Button();
            this.lbPointSources = new System.Windows.Forms.ListBox();
            this.tpReactions = new System.Windows.Forms.TabPage();
            this.tpSediment = new System.Windows.Forms.TabPage();
            this.tpInitialConcs = new System.Windows.Forms.TabPage();
            this.tpAdsorption = new System.Windows.Forms.TabPage();
            this.tpObsData = new System.Windows.Forms.TabPage();
            this.tpCEQUALW2 = new System.Windows.Forms.TabPage();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.RiverOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.dgvReactionRates = new System.Windows.Forms.DataGridView();
            this.tbAerationFactor = new System.Windows.Forms.TextBox();
            this.tbConvHeatFactor = new System.Windows.Forms.TextBox();
            this.tbPrecipitateSettling = new System.Windows.Forms.TextBox();
            this.tcRiverTabs.SuspendLayout();
            this.tpPhysicalData.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpStageWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStageWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStageWidth)).BeginInit();
            this.tpDiversions.SuspendLayout();
            this.tpPointSources.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tpReactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReactionRates)).BeginInit();
            this.SuspendLayout();
            // 
            // tcRiverTabs
            // 
            this.tcRiverTabs.Controls.Add(this.tpPhysicalData);
            this.tcRiverTabs.Controls.Add(this.tpStageWidth);
            this.tcRiverTabs.Controls.Add(this.tpDiversions);
            this.tcRiverTabs.Controls.Add(this.tpPointSources);
            this.tcRiverTabs.Controls.Add(this.tpReactions);
            this.tcRiverTabs.Controls.Add(this.tpSediment);
            this.tcRiverTabs.Controls.Add(this.tpInitialConcs);
            this.tcRiverTabs.Controls.Add(this.tpAdsorption);
            this.tcRiverTabs.Controls.Add(this.tpObsData);
            this.tcRiverTabs.Controls.Add(this.tpCEQUALW2);
            this.tcRiverTabs.Location = new System.Drawing.Point(0, 0);
            this.tcRiverTabs.Multiline = true;
            this.tcRiverTabs.Name = "tcRiverTabs";
            this.tcRiverTabs.SelectedIndex = 0;
            this.tcRiverTabs.Size = new System.Drawing.Size(675, 484);
            this.tcRiverTabs.TabIndex = 0;
            // 
            // tpPhysicalData
            // 
            this.tpPhysicalData.Controls.Add(this.groupBox4);
            this.tpPhysicalData.Controls.Add(this.groupBox3);
            this.tpPhysicalData.Controls.Add(this.groupBox2);
            this.tpPhysicalData.Controls.Add(this.groupBox1);
            this.tpPhysicalData.Controls.Add(this.tbStreamID);
            this.tpPhysicalData.Controls.Add(this.label2);
            this.tpPhysicalData.Controls.Add(this.tbName);
            this.tpPhysicalData.Controls.Add(this.label1);
            this.tpPhysicalData.Location = new System.Drawing.Point(4, 40);
            this.tpPhysicalData.Name = "tpPhysicalData";
            this.tpPhysicalData.Padding = new System.Windows.Forms.Padding(3);
            this.tpPhysicalData.Size = new System.Drawing.Size(667, 440);
            this.tpPhysicalData.TabIndex = 0;
            this.tpPhysicalData.Text = "Physical Data";
            this.tpPhysicalData.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbManningsN);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(216, 311);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(229, 57);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Kinematic Wave Routing";
            // 
            // tbManningsN
            // 
            this.tbManningsN.Location = new System.Drawing.Point(110, 24);
            this.tbManningsN.Name = "tbManningsN";
            this.tbManningsN.Size = new System.Drawing.Size(86, 20);
            this.tbManningsN.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Manning\'s N";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbImpVolume);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tbImpArea);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(96, 243);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(475, 61);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Impoundment Characteristics";
            // 
            // tbImpVolume
            // 
            this.tbImpVolume.Location = new System.Drawing.Point(316, 20);
            this.tbImpVolume.Name = "tbImpVolume";
            this.tbImpVolume.Size = new System.Drawing.Size(129, 20);
            this.tbImpVolume.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(238, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Volume, m3";
            // 
            // tbImpArea
            // 
            this.tbImpArea.Location = new System.Drawing.Point(86, 20);
            this.tbImpArea.Name = "tbImpArea";
            this.tbImpArea.Size = new System.Drawing.Size(135, 20);
            this.tbImpArea.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Area, m2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbDepth);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbLength);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(351, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 130);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Length and Depth (m)";
            // 
            // tbDepth
            // 
            this.tbDepth.Location = new System.Drawing.Point(104, 86);
            this.tbDepth.Name = "tbDepth";
            this.tbDepth.Size = new System.Drawing.Size(86, 20);
            this.tbDepth.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Depth, m";
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(104, 39);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(86, 20);
            this.tbLength.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Length, m";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbDownElevation);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbUpElevation);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(96, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 130);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stream Bed Elevation (m)";
            // 
            // tbDownElevation
            // 
            this.tbDownElevation.Location = new System.Drawing.Point(99, 86);
            this.tbDownElevation.Name = "tbDownElevation";
            this.tbDownElevation.Size = new System.Drawing.Size(86, 20);
            this.tbDownElevation.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Downstream";
            // 
            // tbUpElevation
            // 
            this.tbUpElevation.Location = new System.Drawing.Point(99, 39);
            this.tbUpElevation.Name = "tbUpElevation";
            this.tbUpElevation.Size = new System.Drawing.Size(86, 20);
            this.tbUpElevation.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Upstream";
            // 
            // tbStreamID
            // 
            this.tbStreamID.Location = new System.Drawing.Point(188, 54);
            this.tbStreamID.Name = "tbStreamID";
            this.tbStreamID.Size = new System.Drawing.Size(86, 20);
            this.tbStreamID.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Stream ID:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(188, 20);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(383, 20);
            this.tbName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tpStageWidth
            // 
            this.tpStageWidth.Controls.Add(this.btnRedrawChart);
            this.tpStageWidth.Controls.Add(this.chartStageWidth);
            this.tpStageWidth.Controls.Add(this.label10);
            this.tpStageWidth.Controls.Add(this.dgvStageWidth);
            this.tpStageWidth.Location = new System.Drawing.Point(4, 40);
            this.tpStageWidth.Name = "tpStageWidth";
            this.tpStageWidth.Padding = new System.Windows.Forms.Padding(3);
            this.tpStageWidth.Size = new System.Drawing.Size(667, 440);
            this.tpStageWidth.TabIndex = 1;
            this.tpStageWidth.Text = "Stage - Width";
            this.tpStageWidth.UseVisualStyleBackColor = true;
            // 
            // btnRedrawChart
            // 
            this.btnRedrawChart.Location = new System.Drawing.Point(57, 361);
            this.btnRedrawChart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRedrawChart.Name = "btnRedrawChart";
            this.btnRedrawChart.Size = new System.Drawing.Size(137, 39);
            this.btnRedrawChart.TabIndex = 15;
            this.btnRedrawChart.Text = "Redraw Chart";
            this.btnRedrawChart.UseVisualStyleBackColor = true;
            this.btnRedrawChart.Click += new System.EventHandler(this.btnRedrawChart_Click);
            // 
            // chartStageWidth
            // 
            chartArea1.Name = "ChartArea1";
            this.chartStageWidth.ChartAreas.Add(chartArea1);
            this.chartStageWidth.Location = new System.Drawing.Point(235, 79);
            this.chartStageWidth.Name = "chartStageWidth";
            series1.ChartArea = "ChartArea1";
            series1.Name = "SeriesStageWidth";
            this.chartStageWidth.Series.Add(series1);
            this.chartStageWidth.Size = new System.Drawing.Size(403, 355);
            this.chartStageWidth.TabIndex = 2;
            this.chartStageWidth.Text = "chart1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 67);
            this.label10.MaximumSize = new System.Drawing.Size(200, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(189, 26);
            this.label10.TabIndex = 1;
            this.label10.Text = "Enter up to 9 pairs of stage-width data below";
            // 
            // dgvStageWidth
            // 
            this.dgvStageWidth.AllowUserToAddRows = false;
            this.dgvStageWidth.AllowUserToDeleteRows = false;
            this.dgvStageWidth.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStageWidth.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Stage,
            this.width});
            this.dgvStageWidth.Location = new System.Drawing.Point(29, 96);
            this.dgvStageWidth.Name = "dgvStageWidth";
            this.dgvStageWidth.RowHeadersVisible = false;
            this.dgvStageWidth.Size = new System.Drawing.Size(200, 258);
            this.dgvStageWidth.TabIndex = 0;
            // 
            // Stage
            // 
            this.Stage.HeaderText = "Stage (m)";
            this.Stage.Name = "Stage";
            // 
            // width
            // 
            this.width.HeaderText = "Width (m)";
            this.width.Name = "width";
            // 
            // tpDiversions
            // 
            this.tpDiversions.Controls.Add(this.label13);
            this.tpDiversions.Controls.Add(this.tbMinRiverFlow);
            this.tpDiversions.Controls.Add(this.btnToRemove);
            this.tpDiversions.Controls.Add(this.btnFromRemove);
            this.tpDiversions.Controls.Add(this.btnToAdd);
            this.tpDiversions.Controls.Add(this.btnFromAdd);
            this.tpDiversions.Controls.Add(this.label12);
            this.tpDiversions.Controls.Add(this.label11);
            this.tpDiversions.Controls.Add(this.lbDiversionsTo);
            this.tpDiversions.Controls.Add(this.lbDiversionsFrom);
            this.tpDiversions.Location = new System.Drawing.Point(4, 40);
            this.tpDiversions.Name = "tpDiversions";
            this.tpDiversions.Size = new System.Drawing.Size(667, 440);
            this.tpDiversions.TabIndex = 2;
            this.tpDiversions.Text = "Diversions";
            this.tpDiversions.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(206, 393);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Minimum River Flow, cms";
            // 
            // tbMinRiverFlow
            // 
            this.tbMinRiverFlow.Location = new System.Drawing.Point(338, 390);
            this.tbMinRiverFlow.Name = "tbMinRiverFlow";
            this.tbMinRiverFlow.Size = new System.Drawing.Size(100, 20);
            this.tbMinRiverFlow.TabIndex = 8;
            // 
            // btnToRemove
            // 
            this.btnToRemove.Enabled = false;
            this.btnToRemove.Location = new System.Drawing.Point(499, 346);
            this.btnToRemove.Name = "btnToRemove";
            this.btnToRemove.Size = new System.Drawing.Size(75, 23);
            this.btnToRemove.TabIndex = 7;
            this.btnToRemove.Text = "Remove";
            this.btnToRemove.UseVisualStyleBackColor = true;
            this.btnToRemove.Click += new System.EventHandler(this.btnToRemove_Click);
            // 
            // btnFromRemove
            // 
            this.btnFromRemove.Enabled = false;
            this.btnFromRemove.Location = new System.Drawing.Point(174, 346);
            this.btnFromRemove.Name = "btnFromRemove";
            this.btnFromRemove.Size = new System.Drawing.Size(75, 23);
            this.btnFromRemove.TabIndex = 6;
            this.btnFromRemove.Text = "Remove";
            this.btnFromRemove.UseVisualStyleBackColor = true;
            this.btnFromRemove.Click += new System.EventHandler(this.btnFromRemove_Click);
            // 
            // btnToAdd
            // 
            this.btnToAdd.Location = new System.Drawing.Point(418, 346);
            this.btnToAdd.Name = "btnToAdd";
            this.btnToAdd.Size = new System.Drawing.Size(75, 23);
            this.btnToAdd.TabIndex = 5;
            this.btnToAdd.Text = "Add";
            this.btnToAdd.UseVisualStyleBackColor = true;
            this.btnToAdd.Click += new System.EventHandler(this.btnToAdd_Click);
            // 
            // btnFromAdd
            // 
            this.btnFromAdd.Location = new System.Drawing.Point(93, 346);
            this.btnFromAdd.Name = "btnFromAdd";
            this.btnFromAdd.Size = new System.Drawing.Size(75, 23);
            this.btnFromAdd.TabIndex = 4;
            this.btnFromAdd.Text = "Add";
            this.btnFromAdd.UseVisualStyleBackColor = true;
            this.btnFromAdd.Click += new System.EventHandler(this.btnFromAdd_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(335, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(148, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Diversions To River Segment:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(158, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Diversions From River Segment:";
            // 
            // lbDiversionsTo
            // 
            this.lbDiversionsTo.FormattingEnabled = true;
            this.lbDiversionsTo.Location = new System.Drawing.Point(338, 46);
            this.lbDiversionsTo.Name = "lbDiversionsTo";
            this.lbDiversionsTo.Size = new System.Drawing.Size(311, 277);
            this.lbDiversionsTo.TabIndex = 1;
            this.lbDiversionsTo.SelectedIndexChanged += new System.EventHandler(this.lbDiversionsTo_SelectedIndexChanged);
            // 
            // lbDiversionsFrom
            // 
            this.lbDiversionsFrom.FormattingEnabled = true;
            this.lbDiversionsFrom.Location = new System.Drawing.Point(19, 46);
            this.lbDiversionsFrom.Name = "lbDiversionsFrom";
            this.lbDiversionsFrom.Size = new System.Drawing.Size(311, 277);
            this.lbDiversionsFrom.TabIndex = 0;
            this.lbDiversionsFrom.SelectedIndexChanged += new System.EventHandler(this.lbDiversionsFrom_SelectedIndexChanged);
            // 
            // tpPointSources
            // 
            this.tpPointSources.Controls.Add(this.tbNPDESNumber);
            this.tpPointSources.Controls.Add(this.label14);
            this.tpPointSources.Controls.Add(this.groupBox6);
            this.tpPointSources.Controls.Add(this.groupBox5);
            this.tpPointSources.Controls.Add(this.label15);
            this.tpPointSources.Controls.Add(this.btnRemovePTS);
            this.tpPointSources.Controls.Add(this.btnAddPTS);
            this.tpPointSources.Controls.Add(this.lbPointSources);
            this.tpPointSources.Location = new System.Drawing.Point(4, 40);
            this.tpPointSources.Name = "tpPointSources";
            this.tpPointSources.Size = new System.Drawing.Size(667, 440);
            this.tpPointSources.TabIndex = 3;
            this.tpPointSources.Text = "Point Sources";
            this.tpPointSources.UseVisualStyleBackColor = true;
            // 
            // tbNPDESNumber
            // 
            this.tbNPDESNumber.Location = new System.Drawing.Point(337, 392);
            this.tbNPDESNumber.Name = "tbNPDESNumber";
            this.tbNPDESNumber.Size = new System.Drawing.Size(100, 20);
            this.tbNPDESNumber.TabIndex = 18;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(215, 395);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "NPDES Permit Number";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbUnspecAmbient);
            this.groupBox6.Controls.Add(this.rbUnspecZero);
            this.groupBox6.Location = new System.Drawing.Point(337, 303);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(170, 72);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Unspecified Constituents";
            // 
            // rbUnspecAmbient
            // 
            this.rbUnspecAmbient.AutoSize = true;
            this.rbUnspecAmbient.Location = new System.Drawing.Point(6, 42);
            this.rbUnspecAmbient.Name = "rbUnspecAmbient";
            this.rbUnspecAmbient.Size = new System.Drawing.Size(63, 17);
            this.rbUnspecAmbient.TabIndex = 14;
            this.rbUnspecAmbient.TabStop = true;
            this.rbUnspecAmbient.Text = "Ambient";
            this.rbUnspecAmbient.UseVisualStyleBackColor = true;
            // 
            // rbUnspecZero
            // 
            this.rbUnspecZero.AutoSize = true;
            this.rbUnspecZero.Location = new System.Drawing.Point(6, 19);
            this.rbUnspecZero.Name = "rbUnspecZero";
            this.rbUnspecZero.Size = new System.Drawing.Size(47, 17);
            this.rbUnspecZero.TabIndex = 13;
            this.rbUnspecZero.TabStop = true;
            this.rbUnspecZero.Text = "Zero";
            this.rbUnspecZero.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbSourceExternal);
            this.groupBox5.Controls.Add(this.rbSourceInternal);
            this.groupBox5.Location = new System.Drawing.Point(161, 302);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 73);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Water Source";
            // 
            // rbSourceExternal
            // 
            this.rbSourceExternal.AutoSize = true;
            this.rbSourceExternal.Location = new System.Drawing.Point(6, 42);
            this.rbSourceExternal.Name = "rbSourceExternal";
            this.rbSourceExternal.Size = new System.Drawing.Size(63, 17);
            this.rbSourceExternal.TabIndex = 12;
            this.rbSourceExternal.TabStop = true;
            this.rbSourceExternal.Text = "External";
            this.rbSourceExternal.UseVisualStyleBackColor = true;
            // 
            // rbSourceInternal
            // 
            this.rbSourceInternal.AutoSize = true;
            this.rbSourceInternal.Location = new System.Drawing.Point(6, 19);
            this.rbSourceInternal.Name = "rbSourceInternal";
            this.rbSourceInternal.Size = new System.Drawing.Size(60, 17);
            this.rbSourceInternal.TabIndex = 11;
            this.rbSourceInternal.TabStop = true;
            this.rbSourceInternal.Text = "Internal";
            this.rbSourceInternal.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(175, 272);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(321, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "Water source Information corresponds to the selected point source";
            // 
            // btnRemovePTS
            // 
            this.btnRemovePTS.Enabled = false;
            this.btnRemovePTS.Location = new System.Drawing.Point(337, 228);
            this.btnRemovePTS.Name = "btnRemovePTS";
            this.btnRemovePTS.Size = new System.Drawing.Size(75, 23);
            this.btnRemovePTS.TabIndex = 8;
            this.btnRemovePTS.Text = "Remove";
            this.btnRemovePTS.UseVisualStyleBackColor = true;
            this.btnRemovePTS.Click += new System.EventHandler(this.btnRemovePTS_Click);
            // 
            // btnAddPTS
            // 
            this.btnAddPTS.Location = new System.Drawing.Point(256, 228);
            this.btnAddPTS.Name = "btnAddPTS";
            this.btnAddPTS.Size = new System.Drawing.Size(75, 23);
            this.btnAddPTS.TabIndex = 7;
            this.btnAddPTS.Text = "Add";
            this.btnAddPTS.UseVisualStyleBackColor = true;
            this.btnAddPTS.Click += new System.EventHandler(this.btnAddPTS_Click);
            // 
            // lbPointSources
            // 
            this.lbPointSources.FormattingEnabled = true;
            this.lbPointSources.Location = new System.Drawing.Point(24, 23);
            this.lbPointSources.Name = "lbPointSources";
            this.lbPointSources.Size = new System.Drawing.Size(616, 199);
            this.lbPointSources.TabIndex = 0;
            this.lbPointSources.SelectedIndexChanged += new System.EventHandler(this.lbPointSources_SelectedIndexChanged);
            // 
            // tpReactions
            // 
            this.tpReactions.Controls.Add(this.tbPrecipitateSettling);
            this.tpReactions.Controls.Add(this.tbConvHeatFactor);
            this.tpReactions.Controls.Add(this.tbAerationFactor);
            this.tpReactions.Controls.Add(this.dgvReactionRates);
            this.tpReactions.Controls.Add(this.label18);
            this.tpReactions.Controls.Add(this.label17);
            this.tpReactions.Controls.Add(this.label16);
            this.tpReactions.Location = new System.Drawing.Point(4, 40);
            this.tpReactions.Name = "tpReactions";
            this.tpReactions.Size = new System.Drawing.Size(667, 440);
            this.tpReactions.TabIndex = 4;
            this.tpReactions.Text = "Reactions";
            this.tpReactions.UseVisualStyleBackColor = true;
            // 
            // tpSediment
            // 
            this.tpSediment.Location = new System.Drawing.Point(4, 40);
            this.tpSediment.Name = "tpSediment";
            this.tpSediment.Size = new System.Drawing.Size(667, 440);
            this.tpSediment.TabIndex = 5;
            this.tpSediment.Text = "Sediment";
            this.tpSediment.UseVisualStyleBackColor = true;
            // 
            // tpInitialConcs
            // 
            this.tpInitialConcs.Location = new System.Drawing.Point(4, 40);
            this.tpInitialConcs.Name = "tpInitialConcs";
            this.tpInitialConcs.Size = new System.Drawing.Size(667, 440);
            this.tpInitialConcs.TabIndex = 6;
            this.tpInitialConcs.Text = "Initial Concentrations";
            this.tpInitialConcs.UseVisualStyleBackColor = true;
            // 
            // tpAdsorption
            // 
            this.tpAdsorption.Location = new System.Drawing.Point(4, 40);
            this.tpAdsorption.Name = "tpAdsorption";
            this.tpAdsorption.Size = new System.Drawing.Size(667, 440);
            this.tpAdsorption.TabIndex = 7;
            this.tpAdsorption.Text = "Adsorption";
            this.tpAdsorption.UseVisualStyleBackColor = true;
            // 
            // tpObsData
            // 
            this.tpObsData.Location = new System.Drawing.Point(4, 40);
            this.tpObsData.Name = "tpObsData";
            this.tpObsData.Size = new System.Drawing.Size(667, 440);
            this.tpObsData.TabIndex = 8;
            this.tpObsData.Text = "Observed Data";
            this.tpObsData.UseVisualStyleBackColor = true;
            // 
            // tpCEQUALW2
            // 
            this.tpCEQUALW2.Location = new System.Drawing.Point(4, 40);
            this.tpCEQUALW2.Name = "tpCEQUALW2";
            this.tpCEQUALW2.Size = new System.Drawing.Size(667, 440);
            this.tpCEQUALW2.TabIndex = 9;
            this.tpCEQUALW2.Text = "CE-QUAL-W2";
            this.tpCEQUALW2.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(441, 502);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(137, 39);
            this.btnHelp.TabIndex = 16;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 502);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 39);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(116, 502);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(137, 39);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(434, 55);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Aeration Factor";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(434, 212);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(131, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Precipitate Settling, m/day";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(434, 128);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(120, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Convective Heat Factor";
            // 
            // dgvReactionRates
            // 
            this.dgvReactionRates.AllowUserToAddRows = false;
            this.dgvReactionRates.AllowUserToDeleteRows = false;
            this.dgvReactionRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReactionRates.Location = new System.Drawing.Point(19, 55);
            this.dgvReactionRates.Name = "dgvReactionRates";
            this.dgvReactionRates.Size = new System.Drawing.Size(393, 370);
            this.dgvReactionRates.TabIndex = 3;
            // 
            // tbAerationFactor
            // 
            this.tbAerationFactor.Location = new System.Drawing.Point(474, 89);
            this.tbAerationFactor.Name = "tbAerationFactor";
            this.tbAerationFactor.Size = new System.Drawing.Size(154, 20);
            this.tbAerationFactor.TabIndex = 4;
            // 
            // tbConvHeatFactor
            // 
            this.tbConvHeatFactor.Location = new System.Drawing.Point(474, 163);
            this.tbConvHeatFactor.Name = "tbConvHeatFactor";
            this.tbConvHeatFactor.Size = new System.Drawing.Size(154, 20);
            this.tbConvHeatFactor.TabIndex = 5;
            // 
            // tbPrecipitateSettling
            // 
            this.tbPrecipitateSettling.Location = new System.Drawing.Point(474, 252);
            this.tbPrecipitateSettling.Name = "tbPrecipitateSettling";
            this.tbPrecipitateSettling.Size = new System.Drawing.Size(154, 20);
            this.tbPrecipitateSettling.TabIndex = 6;
            // 
            // DialogRiverCoeffs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 564);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tcRiverTabs);
            this.Name = "DialogRiverCoeffs";
            this.Text = "Form1";
            this.tcRiverTabs.ResumeLayout(false);
            this.tpPhysicalData.ResumeLayout(false);
            this.tpPhysicalData.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpStageWidth.ResumeLayout(false);
            this.tpStageWidth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStageWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStageWidth)).EndInit();
            this.tpDiversions.ResumeLayout(false);
            this.tpDiversions.PerformLayout();
            this.tpPointSources.ResumeLayout(false);
            this.tpPointSources.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tpReactions.ResumeLayout(false);
            this.tpReactions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReactionRates)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcRiverTabs;
        private System.Windows.Forms.TabPage tpPhysicalData;
        private System.Windows.Forms.TabPage tpStageWidth;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpDiversions;
        private System.Windows.Forms.TabPage tpPointSources;
        private System.Windows.Forms.TabPage tpReactions;
        private System.Windows.Forms.TabPage tpSediment;
        private System.Windows.Forms.TabPage tpInitialConcs;
        private System.Windows.Forms.TabPage tpAdsorption;
        private System.Windows.Forms.TabPage tpObsData;
        private System.Windows.Forms.TabPage tpCEQUALW2;
        private System.Windows.Forms.TextBox tbUpElevation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbStreamID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbDepth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbDownElevation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbImpVolume;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbImpArea;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbManningsN;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStageWidth;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvStageWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stage;
        private System.Windows.Forms.DataGridViewTextBoxColumn width;
        private System.Windows.Forms.Button btnRedrawChart;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbMinRiverFlow;
        private System.Windows.Forms.Button btnToRemove;
        private System.Windows.Forms.Button btnFromRemove;
        private System.Windows.Forms.Button btnToAdd;
        private System.Windows.Forms.Button btnFromAdd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lbDiversionsTo;
        private System.Windows.Forms.ListBox lbDiversionsFrom;
        private System.Windows.Forms.TextBox tbNPDESNumber;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbUnspecAmbient;
        private System.Windows.Forms.RadioButton rbUnspecZero;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbSourceExternal;
        private System.Windows.Forms.RadioButton rbSourceInternal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnRemovePTS;
        private System.Windows.Forms.Button btnAddPTS;
        private System.Windows.Forms.ListBox lbPointSources;
        private System.Windows.Forms.OpenFileDialog RiverOpenFileDialog;
        private System.Windows.Forms.TextBox tbPrecipitateSettling;
        private System.Windows.Forms.TextBox tbConvHeatFactor;
        private System.Windows.Forms.TextBox tbAerationFactor;
        private System.Windows.Forms.DataGridView dgvReactionRates;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
    }
}