namespace warmf
{
    partial class DialogRunSimulation
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
            this.dtpBeginDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudTimeStepsPerDay = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbxWaterQuality = new System.Windows.Forms.CheckBox();
            this.chbxSediment = new System.Windows.Forms.CheckBox();
            this.chbxLandApplication = new System.Windows.Forms.CheckBox();
            this.chbxPointSources = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbSubwatersheds = new System.Windows.Forms.ListBox();
            this.chbxHydrologyAutocalibration = new System.Windows.Forms.CheckBox();
            this.lblNumLoops = new System.Windows.Forms.Label();
            this.nudLoops = new System.Windows.Forms.NumericUpDown();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chbxLoadingData = new System.Windows.Forms.CheckBox();
            this.chbxWarmStart = new System.Windows.Forms.CheckBox();
            this.lblWarmStartFile = new System.Windows.Forms.Label();
            this.btnSelectWst = new System.Windows.Forms.Button();
            this.tbWarmStartFile = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeStepsPerDay)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoops)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpBeginDate
            // 
            this.dtpBeginDate.CustomFormat = "MM/dd/yyyy";
            this.dtpBeginDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginDate.Location = new System.Drawing.Point(111, 20);
            this.dtpBeginDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpBeginDate.Name = "dtpBeginDate";
            this.dtpBeginDate.ShowUpDown = true;
            this.dtpBeginDate.Size = new System.Drawing.Size(116, 21);
            this.dtpBeginDate.TabIndex = 0;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(111, 51);
            this.dtpEndDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowUpDown = true;
            this.dtpEndDate.Size = new System.Drawing.Size(116, 21);
            this.dtpEndDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Beginning Date: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ending Date: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Time steps per day: ";
            // 
            // nudTimeStepsPerDay
            // 
            this.nudTimeStepsPerDay.Location = new System.Drawing.Point(175, 82);
            this.nudTimeStepsPerDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimeStepsPerDay.Name = "nudTimeStepsPerDay";
            this.nudTimeStepsPerDay.Size = new System.Drawing.Size(52, 21);
            this.nudTimeStepsPerDay.TabIndex = 5;
            this.nudTimeStepsPerDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudTimeStepsPerDay);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.dtpBeginDate);
            this.groupBox1.Location = new System.Drawing.Point(43, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 117);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // chbxWaterQuality
            // 
            this.chbxWaterQuality.AutoSize = true;
            this.chbxWaterQuality.Location = new System.Drawing.Point(11, 25);
            this.chbxWaterQuality.Name = "chbxWaterQuality";
            this.chbxWaterQuality.Size = new System.Drawing.Size(98, 19);
            this.chbxWaterQuality.TabIndex = 7;
            this.chbxWaterQuality.Text = "Water Quality";
            this.chbxWaterQuality.UseVisualStyleBackColor = true;
            // 
            // chbxSediment
            // 
            this.chbxSediment.AutoSize = true;
            this.chbxSediment.Location = new System.Drawing.Point(11, 50);
            this.chbxSediment.Name = "chbxSediment";
            this.chbxSediment.Size = new System.Drawing.Size(79, 19);
            this.chbxSediment.TabIndex = 8;
            this.chbxSediment.Text = "Sediment";
            this.chbxSediment.UseVisualStyleBackColor = true;
            // 
            // chbxLandApplication
            // 
            this.chbxLandApplication.AutoSize = true;
            this.chbxLandApplication.Location = new System.Drawing.Point(11, 75);
            this.chbxLandApplication.Name = "chbxLandApplication";
            this.chbxLandApplication.Size = new System.Drawing.Size(116, 19);
            this.chbxLandApplication.TabIndex = 9;
            this.chbxLandApplication.Text = "Land Application";
            this.chbxLandApplication.UseVisualStyleBackColor = true;
            // 
            // chbxPointSources
            // 
            this.chbxPointSources.AutoSize = true;
            this.chbxPointSources.Location = new System.Drawing.Point(10, 100);
            this.chbxPointSources.Name = "chbxPointSources";
            this.chbxPointSources.Size = new System.Drawing.Size(103, 19);
            this.chbxPointSources.TabIndex = 10;
            this.chbxPointSources.Text = "Point Sources";
            this.chbxPointSources.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chbxPointSources);
            this.groupBox2.Controls.Add(this.chbxLandApplication);
            this.groupBox2.Controls.Add(this.chbxSediment);
            this.groupBox2.Controls.Add(this.chbxWaterQuality);
            this.groupBox2.Location = new System.Drawing.Point(43, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 129);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Simulate Hydrology and...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Subwatersheds:";
            // 
            // lbSubwatersheds
            // 
            this.lbSubwatersheds.FormattingEnabled = true;
            this.lbSubwatersheds.ItemHeight = 15;
            this.lbSubwatersheds.Location = new System.Drawing.Point(303, 51);
            this.lbSubwatersheds.Name = "lbSubwatersheds";
            this.lbSubwatersheds.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbSubwatersheds.Size = new System.Drawing.Size(355, 94);
            this.lbSubwatersheds.TabIndex = 13;
            // 
            // chbxHydrologyAutocalibration
            // 
            this.chbxHydrologyAutocalibration.AutoSize = true;
            this.chbxHydrologyAutocalibration.Location = new System.Drawing.Point(17, 18);
            this.chbxHydrologyAutocalibration.Name = "chbxHydrologyAutocalibration";
            this.chbxHydrologyAutocalibration.Size = new System.Drawing.Size(163, 19);
            this.chbxHydrologyAutocalibration.TabIndex = 14;
            this.chbxHydrologyAutocalibration.Text = "Hydrology Autocalibration";
            this.chbxHydrologyAutocalibration.UseVisualStyleBackColor = true;
            this.chbxHydrologyAutocalibration.CheckedChanged += new System.EventHandler(this.chbxHydrologyAutocalibration_CheckedChanged);
            // 
            // lblNumLoops
            // 
            this.lblNumLoops.AutoSize = true;
            this.lblNumLoops.Enabled = false;
            this.lblNumLoops.Location = new System.Drawing.Point(14, 45);
            this.lblNumLoops.Name = "lblNumLoops";
            this.lblNumLoops.Size = new System.Drawing.Size(105, 15);
            this.lblNumLoops.TabIndex = 15;
            this.lblNumLoops.Text = "Number of loops: ";
            // 
            // nudLoops
            // 
            this.nudLoops.Enabled = false;
            this.nudLoops.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudLoops.Location = new System.Drawing.Point(125, 43);
            this.nudLoops.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudLoops.Name = "nudLoops";
            this.nudLoops.Size = new System.Drawing.Size(120, 21);
            this.nudLoops.TabIndex = 16;
            this.nudLoops.ThousandsSeparator = true;
            this.nudLoops.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(366, 155);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(108, 25);
            this.btnSelectAll.TabIndex = 17;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(480, 155);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(108, 25);
            this.btnClearSelection.TabIndex = 18;
            this.btnClearSelection.Text = "Clear Selection";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nudLoops);
            this.groupBox3.Controls.Add(this.lblNumLoops);
            this.groupBox3.Controls.Add(this.chbxHydrologyAutocalibration);
            this.groupBox3.Location = new System.Drawing.Point(304, 183);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(354, 80);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            // 
            // chbxLoadingData
            // 
            this.chbxLoadingData.AutoSize = true;
            this.chbxLoadingData.Location = new System.Drawing.Point(304, 271);
            this.chbxLoadingData.Name = "chbxLoadingData";
            this.chbxLoadingData.Size = new System.Drawing.Size(154, 19);
            this.chbxLoadingData.TabIndex = 20;
            this.chbxLoadingData.Text = "Generate Loading Data";
            this.chbxLoadingData.UseVisualStyleBackColor = true;
            // 
            // chbxWarmStart
            // 
            this.chbxWarmStart.AutoSize = true;
            this.chbxWarmStart.Location = new System.Drawing.Point(14, 30);
            this.chbxWarmStart.Name = "chbxWarmStart";
            this.chbxWarmStart.Size = new System.Drawing.Size(268, 19);
            this.chbxWarmStart.TabIndex = 21;
            this.chbxWarmStart.Text = "Initial conditions from warm start file (*.WST)";
            this.chbxWarmStart.UseVisualStyleBackColor = true;
            this.chbxWarmStart.CheckedChanged += new System.EventHandler(this.chbxWarmStart_CheckedChanged);
            // 
            // lblWarmStartFile
            // 
            this.lblWarmStartFile.AutoSize = true;
            this.lblWarmStartFile.Enabled = false;
            this.lblWarmStartFile.Location = new System.Drawing.Point(13, 66);
            this.lblWarmStartFile.Name = "lblWarmStartFile";
            this.lblWarmStartFile.Size = new System.Drawing.Size(92, 15);
            this.lblWarmStartFile.TabIndex = 22;
            this.lblWarmStartFile.Text = "Warm start file: ";
            // 
            // btnSelectWst
            // 
            this.btnSelectWst.Enabled = false;
            this.btnSelectWst.Location = new System.Drawing.Point(298, 26);
            this.btnSelectWst.Name = "btnSelectWst";
            this.btnSelectWst.Size = new System.Drawing.Size(137, 25);
            this.btnSelectWst.TabIndex = 23;
            this.btnSelectWst.Text = "Select WST File";
            this.btnSelectWst.UseVisualStyleBackColor = true;
            // 
            // tbWarmStartFile
            // 
            this.tbWarmStartFile.Enabled = false;
            this.tbWarmStartFile.Location = new System.Drawing.Point(111, 63);
            this.tbWarmStartFile.Name = "tbWarmStartFile";
            this.tbWarmStartFile.Size = new System.Drawing.Size(373, 21);
            this.tbWarmStartFile.TabIndex = 24;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbWarmStartFile);
            this.groupBox4.Controls.Add(this.btnSelectWst);
            this.groupBox4.Controls.Add(this.lblWarmStartFile);
            this.groupBox4.Controls.Add(this.chbxWarmStart);
            this.groupBox4.Location = new System.Drawing.Point(43, 310);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(506, 103);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Initial Simulation Conditions";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(441, 431);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(137, 39);
            this.btnHelp.TabIndex = 28;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 431);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 39);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(116, 431);
            this.btnRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(137, 39);
            this.btnRun.TabIndex = 26;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // DialogRunSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 486);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.chbxLoadingData);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnClearSelection);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.lbSubwatersheds);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DialogRunSimulation";
            this.Text = "Simulation Control";
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeStepsPerDay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoops)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpBeginDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudTimeStepsPerDay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbxWaterQuality;
        private System.Windows.Forms.CheckBox chbxSediment;
        private System.Windows.Forms.CheckBox chbxLandApplication;
        private System.Windows.Forms.CheckBox chbxPointSources;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbSubwatersheds;
        private System.Windows.Forms.CheckBox chbxHydrologyAutocalibration;
        private System.Windows.Forms.Label lblNumLoops;
        private System.Windows.Forms.NumericUpDown nudLoops;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClearSelection;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chbxLoadingData;
        private System.Windows.Forms.CheckBox chbxWarmStart;
        private System.Windows.Forms.Label lblWarmStartFile;
        private System.Windows.Forms.Button btnSelectWst;
        private System.Windows.Forms.TextBox tbWarmStartFile;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRun;
    }
}