namespace warmf
{
    partial class DialogOutput
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cbOutputType = new System.Windows.Forms.ComboBox();
            this.lbOutputParameters = new System.Windows.Forms.ListBox();
            this.chkShowObservations = new System.Windows.Forms.CheckBox();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnCreateTextFile = new System.Windows.Forms.Button();
            this.tbTextFileName = new System.Windows.Forms.TextBox();
            this.rbThisConstituent = new System.Windows.Forms.RadioButton();
            this.rbAllConstituents = new System.Windows.Forms.RadioButton();
            this.cbTextFileScenario = new System.Windows.Forms.ComboBox();
            this.chartOutput = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartOutput)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbOutputType
            // 
            this.cbOutputType.FormattingEnabled = true;
            this.cbOutputType.Location = new System.Drawing.Point(11, 12);
            this.cbOutputType.Name = "cbOutputType";
            this.cbOutputType.Size = new System.Drawing.Size(200, 21);
            this.cbOutputType.TabIndex = 0;
            this.cbOutputType.SelectionChangeCommitted += new System.EventHandler(this.cbOutputType_SelectionChangeCommitted);
            // 
            // lbOutputParameters
            // 
            this.lbOutputParameters.FormattingEnabled = true;
            this.lbOutputParameters.Location = new System.Drawing.Point(11, 36);
            this.lbOutputParameters.Name = "lbOutputParameters";
            this.lbOutputParameters.Size = new System.Drawing.Size(200, 303);
            this.lbOutputParameters.TabIndex = 1;
            // 
            // chkShowObservations
            // 
            this.chkShowObservations.AutoSize = true;
            this.chkShowObservations.Checked = true;
            this.chkShowObservations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowObservations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowObservations.Location = new System.Drawing.Point(11, 345);
            this.chkShowObservations.Name = "chkShowObservations";
            this.chkShowObservations.Size = new System.Drawing.Size(143, 20);
            this.chkShowObservations.TabIndex = 2;
            this.chkShowObservations.Text = "Show Observations";
            this.chkShowObservations.UseVisualStyleBackColor = true;
            this.chkShowObservations.CheckedChanged += new System.EventHandler(this.chkShowObservations_CheckedChanged);
            // 
            // btnStatistics
            // 
            this.btnStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatistics.Location = new System.Drawing.Point(11, 368);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(130, 34);
            this.btnStatistics.TabIndex = 3;
            this.btnStatistics.Text = "Statistics";
            this.btnStatistics.UseVisualStyleBackColor = true;
            // 
            // btnCreateTextFile
            // 
            this.btnCreateTextFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateTextFile.Location = new System.Drawing.Point(92, 26);
            this.btnCreateTextFile.Name = "btnCreateTextFile";
            this.btnCreateTextFile.Size = new System.Drawing.Size(135, 29);
            this.btnCreateTextFile.TabIndex = 4;
            this.btnCreateTextFile.Text = "Export CSV File";
            this.btnCreateTextFile.UseVisualStyleBackColor = true;
            this.btnCreateTextFile.Click += new System.EventHandler(this.btnCreateTextFile_Click);
            // 
            // tbTextFileName
            // 
            this.tbTextFileName.Location = new System.Drawing.Point(6, 66);
            this.tbTextFileName.Name = "tbTextFileName";
            this.tbTextFileName.Size = new System.Drawing.Size(291, 22);
            this.tbTextFileName.TabIndex = 5;
            // 
            // rbThisConstituent
            // 
            this.rbThisConstituent.AutoSize = true;
            this.rbThisConstituent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbThisConstituent.Location = new System.Drawing.Point(357, 19);
            this.rbThisConstituent.Name = "rbThisConstituent";
            this.rbThisConstituent.Size = new System.Drawing.Size(200, 20);
            this.rbThisConstituent.TabIndex = 6;
            this.rbThisConstituent.TabStop = true;
            this.rbThisConstituent.Text = "This constituent, all scenarios";
            this.rbThisConstituent.UseVisualStyleBackColor = true;
            this.rbThisConstituent.CheckedChanged += new System.EventHandler(this.rbThisConstituent_CheckedChanged);
            // 
            // rbAllConstituents
            // 
            this.rbAllConstituents.AutoSize = true;
            this.rbAllConstituents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAllConstituents.Location = new System.Drawing.Point(357, 42);
            this.rbAllConstituents.Name = "rbAllConstituents";
            this.rbAllConstituents.Size = new System.Drawing.Size(198, 20);
            this.rbAllConstituents.TabIndex = 7;
            this.rbAllConstituents.TabStop = true;
            this.rbAllConstituents.Text = "All constituents, this scenario:";
            this.rbAllConstituents.UseVisualStyleBackColor = true;
            this.rbAllConstituents.CheckedChanged += new System.EventHandler(this.rbAllConstituents_CheckedChanged);
            // 
            // cbTextFileScenario
            // 
            this.cbTextFileScenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextFileScenario.FormattingEnabled = true;
            this.cbTextFileScenario.Location = new System.Drawing.Point(303, 65);
            this.cbTextFileScenario.Name = "cbTextFileScenario";
            this.cbTextFileScenario.Size = new System.Drawing.Size(300, 24);
            this.cbTextFileScenario.TabIndex = 8;
            this.cbTextFileScenario.SelectedIndexChanged += new System.EventHandler(this.cbTextFileScenario_SelectedIndexChanged);
            // 
            // chartOutput
            // 
            chartArea7.Name = "ChartArea1";
            this.chartOutput.ChartAreas.Add(chartArea7);
            legend7.Alignment = System.Drawing.StringAlignment.Center;
            legend7.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend7.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend7.Name = "Legend1";
            this.chartOutput.Legends.Add(legend7);
            this.chartOutput.Location = new System.Drawing.Point(221, 13);
            this.chartOutput.Name = "chartOutput";
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series13.Legend = "Legend1";
            series13.Name = "SeriesOutput";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series14.Legend = "Legend1";
            series14.MarkerBorderColor = System.Drawing.Color.Red;
            series14.MarkerColor = System.Drawing.Color.White;
            series14.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series14.Name = "SeriesObserved";
            this.chartOutput.Series.Add(series13);
            this.chartOutput.Series.Add(series14);
            this.chartOutput.Size = new System.Drawing.Size(569, 325);
            this.chartOutput.TabIndex = 9;
            this.chartOutput.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbTextFileScenario);
            this.groupBox1.Controls.Add(this.rbAllConstituents);
            this.groupBox1.Controls.Add(this.rbThisConstituent);
            this.groupBox1.Controls.Add(this.tbTextFileName);
            this.groupBox1.Controls.Add(this.btnCreateTextFile);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(170, 349);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 94);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Time Series Output";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(11, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 34);
            this.button1.TabIndex = 11;
            this.button1.Text = "Help";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DialogOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chartOutput);
            this.Controls.Add(this.btnStatistics);
            this.Controls.Add(this.chkShowObservations);
            this.Controls.Add(this.lbOutputParameters);
            this.Controls.Add(this.cbOutputType);
            this.Name = "DialogOutput";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chartOutput)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbOutputType;
        private System.Windows.Forms.ListBox lbOutputParameters;
        private System.Windows.Forms.CheckBox chkShowObservations;
        private System.Windows.Forms.Button btnStatistics;
        private System.Windows.Forms.Button btnCreateTextFile;
        private System.Windows.Forms.TextBox tbTextFileName;
        private System.Windows.Forms.RadioButton rbThisConstituent;
        private System.Windows.Forms.RadioButton rbAllConstituents;
        private System.Windows.Forms.ComboBox cbTextFileScenario;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}