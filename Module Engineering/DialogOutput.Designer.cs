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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cbOutputType = new System.Windows.Forms.ComboBox();
            this.lbOutputParameters = new System.Windows.Forms.ListBox();
            this.chkShowObservations = new System.Windows.Forms.CheckBox();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnCreateTextFile = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
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
            // 
            // lbOutputParameters
            // 
            this.lbOutputParameters.FormattingEnabled = true;
            this.lbOutputParameters.Location = new System.Drawing.Point(11, 36);
            this.lbOutputParameters.Name = "lbOutputParameters";
            this.lbOutputParameters.Size = new System.Drawing.Size(200, 303);
            this.lbOutputParameters.TabIndex = 1;
            this.lbOutputParameters.SelectedIndexChanged += new System.EventHandler(this.lbOutputParameters_SelectedIndexChanged);
            // 
            // chkShowObservations
            // 
            this.chkShowObservations.AutoSize = true;
            this.chkShowObservations.Location = new System.Drawing.Point(11, 345);
            this.chkShowObservations.Name = "chkShowObservations";
            this.chkShowObservations.Size = new System.Drawing.Size(118, 17);
            this.chkShowObservations.TabIndex = 2;
            this.chkShowObservations.Text = "Show Observations";
            this.chkShowObservations.UseVisualStyleBackColor = true;
            // 
            // btnStatistics
            // 
            this.btnStatistics.Location = new System.Drawing.Point(11, 368);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(130, 34);
            this.btnStatistics.TabIndex = 3;
            this.btnStatistics.Text = "Statistics";
            this.btnStatistics.UseVisualStyleBackColor = true;
            // 
            // btnCreateTextFile
            // 
            this.btnCreateTextFile.Location = new System.Drawing.Point(15, 34);
            this.btnCreateTextFile.Name = "btnCreateTextFile";
            this.btnCreateTextFile.Size = new System.Drawing.Size(108, 29);
            this.btnCreateTextFile.TabIndex = 4;
            this.btnCreateTextFile.Text = "Create Text File";
            this.btnCreateTextFile.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(129, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(167, 20);
            this.textBox1.TabIndex = 5;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(302, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(164, 17);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "This constituent, all scenarios";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(302, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(164, 17);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "All constituents, this scenario:";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(321, 65);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(234, 21);
            this.comboBox2.TabIndex = 8;
            // 
            // chartOutput
            // 
            chartArea1.Name = "ChartArea1";
            this.chartOutput.ChartAreas.Add(chartArea1);
            this.chartOutput.Location = new System.Drawing.Point(221, 13);
            this.chartOutput.Name = "chartOutput";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsVisibleInLegend = false;
            series1.Name = "SeriesOutput";
            this.chartOutput.Series.Add(series1);
            this.chartOutput.Size = new System.Drawing.Size(569, 325);
            this.chartOutput.TabIndex = 9;
            this.chartOutput.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btnCreateTextFile);
            this.groupBox1.Location = new System.Drawing.Point(221, 349);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 94);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Timeseries Output";
            // 
            // button1
            // 
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}