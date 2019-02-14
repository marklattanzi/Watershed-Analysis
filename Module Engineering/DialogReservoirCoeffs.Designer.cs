namespace warmf
{
    partial class DialogReservoirCoeffs
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tcReservoirTabs = new System.Windows.Forms.TabControl();
            this.tpPhysicalData = new System.Windows.Forms.TabPage();
            this.tbMaxElev = new System.Windows.Forms.TextBox();
            this.tbMinElev = new System.Windows.Forms.TextBox();
            this.tbInitElev = new System.Windows.Forms.TextBox();
            this.tbResSegID = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpStageFlow = new System.Windows.Forms.TabPage();
            this.tpReactions = new System.Windows.Forms.TabPage();
            this.tpPhytoplankton = new System.Windows.Forms.TabPage();
            this.tpHeatLight = new System.Windows.Forms.TabPage();
            this.tpDiffusion = new System.Windows.Forms.TabPage();
            this.tpSediment = new System.Windows.Forms.TabPage();
            this.tpInitTemp = new System.Windows.Forms.TabPage();
            this.tpInitConc = new System.Windows.Forms.TabPage();
            this.tpPtSrcs = new System.Windows.Forms.TabPage();
            this.tpAdsorption = new System.Windows.Forms.TabPage();
            this.tpObsData = new System.Windows.Forms.TabPage();
            this.tpStageArea = new System.Windows.Forms.TabPage();
            this.tpInOutFlow = new System.Windows.Forms.TabPage();
            this.tpMet = new System.Windows.Forms.TabPage();
            this.dgvStageArea = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.chartStageFlow = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tcReservoirTabs.SuspendLayout();
            this.tpPhysicalData.SuspendLayout();
            this.tpStageFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStageArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStageFlow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(492, 552);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(183, 48);
            this.btnHelp.TabIndex = 19;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(277, 552);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(183, 48);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(58, 552);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(183, 48);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // tcReservoirTabs
            // 
            this.tcReservoirTabs.Controls.Add(this.tpPhysicalData);
            this.tcReservoirTabs.Controls.Add(this.tpStageFlow);
            this.tcReservoirTabs.Controls.Add(this.tpReactions);
            this.tcReservoirTabs.Controls.Add(this.tpPhytoplankton);
            this.tcReservoirTabs.Controls.Add(this.tpHeatLight);
            this.tcReservoirTabs.Controls.Add(this.tpDiffusion);
            this.tcReservoirTabs.Controls.Add(this.tpSediment);
            this.tcReservoirTabs.Controls.Add(this.tpInitTemp);
            this.tcReservoirTabs.Controls.Add(this.tpInitConc);
            this.tcReservoirTabs.Controls.Add(this.tpPtSrcs);
            this.tcReservoirTabs.Controls.Add(this.tpAdsorption);
            this.tcReservoirTabs.Controls.Add(this.tpObsData);
            this.tcReservoirTabs.Controls.Add(this.tpStageArea);
            this.tcReservoirTabs.Controls.Add(this.tpInOutFlow);
            this.tcReservoirTabs.Controls.Add(this.tpMet);
            this.tcReservoirTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.tcReservoirTabs.Location = new System.Drawing.Point(8, 7);
            this.tcReservoirTabs.Multiline = true;
            this.tcReservoirTabs.Name = "tcReservoirTabs";
            this.tcReservoirTabs.SelectedIndex = 0;
            this.tcReservoirTabs.Size = new System.Drawing.Size(720, 475);
            this.tcReservoirTabs.TabIndex = 20;
            // 
            // tpPhysicalData
            // 
            this.tpPhysicalData.Controls.Add(this.tbMaxElev);
            this.tpPhysicalData.Controls.Add(this.tbMinElev);
            this.tpPhysicalData.Controls.Add(this.tbInitElev);
            this.tpPhysicalData.Controls.Add(this.tbResSegID);
            this.tpPhysicalData.Controls.Add(this.tbName);
            this.tpPhysicalData.Controls.Add(this.label5);
            this.tpPhysicalData.Controls.Add(this.label4);
            this.tpPhysicalData.Controls.Add(this.label3);
            this.tpPhysicalData.Controls.Add(this.label2);
            this.tpPhysicalData.Controls.Add(this.label1);
            this.tpPhysicalData.Location = new System.Drawing.Point(4, 46);
            this.tpPhysicalData.Name = "tpPhysicalData";
            this.tpPhysicalData.Padding = new System.Windows.Forms.Padding(3);
            this.tpPhysicalData.Size = new System.Drawing.Size(712, 425);
            this.tpPhysicalData.TabIndex = 0;
            this.tpPhysicalData.Text = "Physical Data";
            this.tpPhysicalData.UseVisualStyleBackColor = true;
            // 
            // tbMaxElev
            // 
            this.tbMaxElev.Location = new System.Drawing.Point(420, 284);
            this.tbMaxElev.Name = "tbMaxElev";
            this.tbMaxElev.ReadOnly = true;
            this.tbMaxElev.Size = new System.Drawing.Size(100, 22);
            this.tbMaxElev.TabIndex = 9;
            // 
            // tbMinElev
            // 
            this.tbMinElev.Location = new System.Drawing.Point(420, 240);
            this.tbMinElev.Name = "tbMinElev";
            this.tbMinElev.ReadOnly = true;
            this.tbMinElev.Size = new System.Drawing.Size(100, 22);
            this.tbMinElev.TabIndex = 8;
            // 
            // tbInitElev
            // 
            this.tbInitElev.Location = new System.Drawing.Point(420, 196);
            this.tbInitElev.Name = "tbInitElev";
            this.tbInitElev.Size = new System.Drawing.Size(100, 22);
            this.tbInitElev.TabIndex = 7;
            // 
            // tbResSegID
            // 
            this.tbResSegID.Location = new System.Drawing.Point(420, 152);
            this.tbResSegID.Name = "tbResSegID";
            this.tbResSegID.ReadOnly = true;
            this.tbResSegID.Size = new System.Drawing.Size(100, 22);
            this.tbResSegID.TabIndex = 6;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(265, 108);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(255, 22);
            this.tbName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Maximum Surface Elevation (m)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Minimum Surface Elevation (m)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Initial Water Surface Elevation (m)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Reservoir Segment ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tpStageFlow
            // 
            this.tpStageFlow.Controls.Add(this.chartStageFlow);
            this.tpStageFlow.Controls.Add(this.label6);
            this.tpStageFlow.Controls.Add(this.dgvStageArea);
            this.tpStageFlow.Location = new System.Drawing.Point(4, 46);
            this.tpStageFlow.Name = "tpStageFlow";
            this.tpStageFlow.Padding = new System.Windows.Forms.Padding(3);
            this.tpStageFlow.Size = new System.Drawing.Size(712, 425);
            this.tpStageFlow.TabIndex = 1;
            this.tpStageFlow.Text = "Stage-Flow";
            this.tpStageFlow.UseVisualStyleBackColor = true;
            // 
            // tpReactions
            // 
            this.tpReactions.Location = new System.Drawing.Point(4, 46);
            this.tpReactions.Name = "tpReactions";
            this.tpReactions.Size = new System.Drawing.Size(712, 425);
            this.tpReactions.TabIndex = 2;
            this.tpReactions.Text = "Reactions";
            this.tpReactions.UseVisualStyleBackColor = true;
            // 
            // tpPhytoplankton
            // 
            this.tpPhytoplankton.Location = new System.Drawing.Point(4, 46);
            this.tpPhytoplankton.Name = "tpPhytoplankton";
            this.tpPhytoplankton.Size = new System.Drawing.Size(712, 425);
            this.tpPhytoplankton.TabIndex = 3;
            this.tpPhytoplankton.Text = "Phytoplankton";
            this.tpPhytoplankton.UseVisualStyleBackColor = true;
            // 
            // tpHeatLight
            // 
            this.tpHeatLight.Location = new System.Drawing.Point(4, 46);
            this.tpHeatLight.Name = "tpHeatLight";
            this.tpHeatLight.Size = new System.Drawing.Size(712, 425);
            this.tpHeatLight.TabIndex = 4;
            this.tpHeatLight.Text = "Heat/Light";
            this.tpHeatLight.UseVisualStyleBackColor = true;
            // 
            // tpDiffusion
            // 
            this.tpDiffusion.Location = new System.Drawing.Point(4, 46);
            this.tpDiffusion.Name = "tpDiffusion";
            this.tpDiffusion.Size = new System.Drawing.Size(712, 425);
            this.tpDiffusion.TabIndex = 5;
            this.tpDiffusion.Text = "Diffusion";
            this.tpDiffusion.UseVisualStyleBackColor = true;
            // 
            // tpSediment
            // 
            this.tpSediment.Location = new System.Drawing.Point(4, 46);
            this.tpSediment.Name = "tpSediment";
            this.tpSediment.Size = new System.Drawing.Size(712, 425);
            this.tpSediment.TabIndex = 6;
            this.tpSediment.Text = "Sediment";
            this.tpSediment.UseVisualStyleBackColor = true;
            // 
            // tpInitTemp
            // 
            this.tpInitTemp.Location = new System.Drawing.Point(4, 46);
            this.tpInitTemp.Name = "tpInitTemp";
            this.tpInitTemp.Size = new System.Drawing.Size(712, 425);
            this.tpInitTemp.TabIndex = 7;
            this.tpInitTemp.Text = "Initial Temperature";
            this.tpInitTemp.UseVisualStyleBackColor = true;
            // 
            // tpInitConc
            // 
            this.tpInitConc.Location = new System.Drawing.Point(4, 46);
            this.tpInitConc.Name = "tpInitConc";
            this.tpInitConc.Size = new System.Drawing.Size(712, 425);
            this.tpInitConc.TabIndex = 8;
            this.tpInitConc.Text = "Initial Concentrations";
            this.tpInitConc.UseVisualStyleBackColor = true;
            // 
            // tpPtSrcs
            // 
            this.tpPtSrcs.Location = new System.Drawing.Point(4, 46);
            this.tpPtSrcs.Name = "tpPtSrcs";
            this.tpPtSrcs.Size = new System.Drawing.Size(712, 425);
            this.tpPtSrcs.TabIndex = 9;
            this.tpPtSrcs.Text = "Point Sources";
            this.tpPtSrcs.UseVisualStyleBackColor = true;
            // 
            // tpAdsorption
            // 
            this.tpAdsorption.Location = new System.Drawing.Point(4, 46);
            this.tpAdsorption.Name = "tpAdsorption";
            this.tpAdsorption.Size = new System.Drawing.Size(712, 425);
            this.tpAdsorption.TabIndex = 10;
            this.tpAdsorption.Text = "Adsorption";
            this.tpAdsorption.UseVisualStyleBackColor = true;
            // 
            // tpObsData
            // 
            this.tpObsData.Location = new System.Drawing.Point(4, 46);
            this.tpObsData.Name = "tpObsData";
            this.tpObsData.Size = new System.Drawing.Size(712, 425);
            this.tpObsData.TabIndex = 11;
            this.tpObsData.Text = "Observed Data";
            this.tpObsData.UseVisualStyleBackColor = true;
            // 
            // tpStageArea
            // 
            this.tpStageArea.Location = new System.Drawing.Point(4, 46);
            this.tpStageArea.Name = "tpStageArea";
            this.tpStageArea.Size = new System.Drawing.Size(712, 425);
            this.tpStageArea.TabIndex = 12;
            this.tpStageArea.Text = "Stage-Area";
            this.tpStageArea.UseVisualStyleBackColor = true;
            // 
            // tpInOutFlow
            // 
            this.tpInOutFlow.Location = new System.Drawing.Point(4, 46);
            this.tpInOutFlow.Name = "tpInOutFlow";
            this.tpInOutFlow.Size = new System.Drawing.Size(712, 425);
            this.tpInOutFlow.TabIndex = 13;
            this.tpInOutFlow.Text = "Inflow/Outflow";
            this.tpInOutFlow.UseVisualStyleBackColor = true;
            // 
            // tpMet
            // 
            this.tpMet.Location = new System.Drawing.Point(4, 46);
            this.tpMet.Name = "tpMet";
            this.tpMet.Size = new System.Drawing.Size(712, 425);
            this.tpMet.TabIndex = 14;
            this.tpMet.Text = "Meteorology";
            this.tpMet.UseVisualStyleBackColor = true;
            // 
            // dgvStageArea
            // 
            this.dgvStageArea.AllowUserToAddRows = false;
            this.dgvStageArea.AllowUserToDeleteRows = false;
            this.dgvStageArea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStageArea.Location = new System.Drawing.Point(138, 135);
            this.dgvStageArea.Name = "dgvStageArea";
            this.dgvStageArea.Size = new System.Drawing.Size(136, 254);
            this.dgvStageArea.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(136, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 53);
            this.label6.TabIndex = 1;
            this.label6.Text = "Enter up to nine (9) pairs of stage-flow data below.";
            // 
            // chartStageFlow
            // 
            chartArea1.Name = "ChartArea1";
            this.chartStageFlow.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartStageFlow.Legends.Add(legend1);
            this.chartStageFlow.Location = new System.Drawing.Point(299, 81);
            this.chartStageFlow.Name = "chartStageFlow";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartStageFlow.Series.Add(series1);
            this.chartStageFlow.Size = new System.Drawing.Size(327, 307);
            this.chartStageFlow.TabIndex = 2;
            this.chartStageFlow.Text = "chart1";
            // 
            // DialogReservoirCoeffs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 618);
            this.Controls.Add(this.tcReservoirTabs);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "DialogReservoirCoeffs";
            this.Text = "DialogReservoirCoeffs";
            this.tcReservoirTabs.ResumeLayout(false);
            this.tpPhysicalData.ResumeLayout(false);
            this.tpPhysicalData.PerformLayout();
            this.tpStageFlow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStageArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStageFlow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tcReservoirTabs;
        private System.Windows.Forms.TabPage tpPhysicalData;
        private System.Windows.Forms.TabPage tpStageFlow;
        private System.Windows.Forms.TabPage tpReactions;
        private System.Windows.Forms.TabPage tpPhytoplankton;
        private System.Windows.Forms.TabPage tpHeatLight;
        private System.Windows.Forms.TabPage tpDiffusion;
        private System.Windows.Forms.TabPage tpSediment;
        private System.Windows.Forms.TabPage tpInitTemp;
        private System.Windows.Forms.TabPage tpInitConc;
        private System.Windows.Forms.TabPage tpPtSrcs;
        private System.Windows.Forms.TabPage tpAdsorption;
        private System.Windows.Forms.TabPage tpObsData;
        private System.Windows.Forms.TabPage tpStageArea;
        private System.Windows.Forms.TabPage tpInOutFlow;
        private System.Windows.Forms.TabPage tpMet;
        private System.Windows.Forms.TextBox tbMaxElev;
        private System.Windows.Forms.TextBox tbMinElev;
        private System.Windows.Forms.TextBox tbInitElev;
        private System.Windows.Forms.TextBox tbResSegID;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvStageArea;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStageFlow;
    }
}