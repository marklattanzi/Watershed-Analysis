namespace warmf
{
    partial class DialogSystemCoeffs
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
            this.tcSystemTabs = new System.Windows.Forms.TabControl();
            this.tpPhysicalData = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.tbElevation = new System.Windows.Forms.TextBox();
            this.tbLongitude = new System.Windows.Forms.TextBox();
            this.tbLatitude = new System.Windows.Forms.TextBox();
            this.tpLandUses = new System.Windows.Forms.TabPage();
            this.tpSnowIce = new System.Windows.Forms.TabPage();
            this.tpHeatLight = new System.Windows.Forms.TabPage();
            this.tpCanopy = new System.Windows.Forms.TabPage();
            this.tpLitter = new System.Windows.Forms.TabPage();
            this.tpSepticSystems = new System.Windows.Forms.TabPage();
            this.tpMinerals = new System.Windows.Forms.TabPage();
            this.tpSediment = new System.Windows.Forms.TabPage();
            this.tpPhytoplankton = new System.Windows.Forms.TabPage();
            this.tpPeriphyton = new System.Windows.Forms.TabPage();
            this.tpFoodWeb = new System.Windows.Forms.TabPage();
            this.tpParameters = new System.Windows.Forms.TabPage();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgvLandUseParameter = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.cbLandUseParameter = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tcSystemTabs.SuspendLayout();
            this.tpPhysicalData.SuspendLayout();
            this.tpLandUses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLandUseParameter)).BeginInit();
            this.SuspendLayout();
            // 
            // tcSystemTabs
            // 
            this.tcSystemTabs.Controls.Add(this.tpPhysicalData);
            this.tcSystemTabs.Controls.Add(this.tpLandUses);
            this.tcSystemTabs.Controls.Add(this.tpSnowIce);
            this.tcSystemTabs.Controls.Add(this.tpHeatLight);
            this.tcSystemTabs.Controls.Add(this.tpCanopy);
            this.tcSystemTabs.Controls.Add(this.tpLitter);
            this.tcSystemTabs.Controls.Add(this.tpSepticSystems);
            this.tcSystemTabs.Controls.Add(this.tpMinerals);
            this.tcSystemTabs.Controls.Add(this.tpSediment);
            this.tcSystemTabs.Controls.Add(this.tpPhytoplankton);
            this.tcSystemTabs.Controls.Add(this.tpPeriphyton);
            this.tcSystemTabs.Controls.Add(this.tpFoodWeb);
            this.tcSystemTabs.Controls.Add(this.tpParameters);
            this.tcSystemTabs.Location = new System.Drawing.Point(8, 7);
            this.tcSystemTabs.Margin = new System.Windows.Forms.Padding(4);
            this.tcSystemTabs.Multiline = true;
            this.tcSystemTabs.Name = "tcSystemTabs";
            this.tcSystemTabs.SelectedIndex = 0;
            this.tcSystemTabs.Size = new System.Drawing.Size(720, 532);
            this.tcSystemTabs.TabIndex = 0;
            // 
            // tpPhysicalData
            // 
            this.tpPhysicalData.Controls.Add(this.label4);
            this.tpPhysicalData.Controls.Add(this.label3);
            this.tpPhysicalData.Controls.Add(this.label2);
            this.tpPhysicalData.Controls.Add(this.label1);
            this.tpPhysicalData.Controls.Add(this.tbArea);
            this.tpPhysicalData.Controls.Add(this.tbElevation);
            this.tpPhysicalData.Controls.Add(this.tbLongitude);
            this.tpPhysicalData.Controls.Add(this.tbLatitude);
            this.tpPhysicalData.Location = new System.Drawing.Point(4, 46);
            this.tpPhysicalData.Margin = new System.Windows.Forms.Padding(4);
            this.tpPhysicalData.Name = "tpPhysicalData";
            this.tpPhysicalData.Padding = new System.Windows.Forms.Padding(4);
            this.tpPhysicalData.Size = new System.Drawing.Size(712, 482);
            this.tpPhysicalData.TabIndex = 0;
            this.tpPhysicalData.Text = "Physical Data";
            this.tpPhysicalData.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(146, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(205, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Watershed Area (Square Meters)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Elevation (Meters)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Longitude";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Latitude";
            // 
            // tbArea
            // 
            this.tbArea.Location = new System.Drawing.Point(358, 255);
            this.tbArea.Margin = new System.Windows.Forms.Padding(4);
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(199, 22);
            this.tbArea.TabIndex = 3;
            // 
            // tbElevation
            // 
            this.tbElevation.Location = new System.Drawing.Point(358, 224);
            this.tbElevation.Margin = new System.Windows.Forms.Padding(4);
            this.tbElevation.Name = "tbElevation";
            this.tbElevation.Size = new System.Drawing.Size(199, 22);
            this.tbElevation.TabIndex = 2;
            // 
            // tbLongitude
            // 
            this.tbLongitude.Location = new System.Drawing.Point(358, 194);
            this.tbLongitude.Margin = new System.Windows.Forms.Padding(4);
            this.tbLongitude.Name = "tbLongitude";
            this.tbLongitude.Size = new System.Drawing.Size(199, 22);
            this.tbLongitude.TabIndex = 1;
            // 
            // tbLatitude
            // 
            this.tbLatitude.Location = new System.Drawing.Point(358, 164);
            this.tbLatitude.Margin = new System.Windows.Forms.Padding(4);
            this.tbLatitude.Name = "tbLatitude";
            this.tbLatitude.Size = new System.Drawing.Size(199, 22);
            this.tbLatitude.TabIndex = 0;
            // 
            // tpLandUses
            // 
            this.tpLandUses.Controls.Add(this.button1);
            this.tpLandUses.Controls.Add(this.cbLandUseParameter);
            this.tpLandUses.Controls.Add(this.label5);
            this.tpLandUses.Controls.Add(this.dgvLandUseParameter);
            this.tpLandUses.Location = new System.Drawing.Point(4, 46);
            this.tpLandUses.Margin = new System.Windows.Forms.Padding(4);
            this.tpLandUses.Name = "tpLandUses";
            this.tpLandUses.Padding = new System.Windows.Forms.Padding(4);
            this.tpLandUses.Size = new System.Drawing.Size(712, 482);
            this.tpLandUses.TabIndex = 1;
            this.tpLandUses.Text = "Land Uses";
            this.tpLandUses.UseVisualStyleBackColor = true;
            // 
            // tpSnowIce
            // 
            this.tpSnowIce.Location = new System.Drawing.Point(4, 46);
            this.tpSnowIce.Margin = new System.Windows.Forms.Padding(4);
            this.tpSnowIce.Name = "tpSnowIce";
            this.tpSnowIce.Size = new System.Drawing.Size(712, 482);
            this.tpSnowIce.TabIndex = 2;
            this.tpSnowIce.Text = "Snow/Ice";
            this.tpSnowIce.UseVisualStyleBackColor = true;
            // 
            // tpHeatLight
            // 
            this.tpHeatLight.Location = new System.Drawing.Point(4, 46);
            this.tpHeatLight.Margin = new System.Windows.Forms.Padding(4);
            this.tpHeatLight.Name = "tpHeatLight";
            this.tpHeatLight.Size = new System.Drawing.Size(712, 482);
            this.tpHeatLight.TabIndex = 3;
            this.tpHeatLight.Text = "Heat/Light";
            this.tpHeatLight.UseVisualStyleBackColor = true;
            // 
            // tpCanopy
            // 
            this.tpCanopy.Location = new System.Drawing.Point(4, 46);
            this.tpCanopy.Margin = new System.Windows.Forms.Padding(4);
            this.tpCanopy.Name = "tpCanopy";
            this.tpCanopy.Size = new System.Drawing.Size(712, 482);
            this.tpCanopy.TabIndex = 4;
            this.tpCanopy.Text = "Canopy";
            this.tpCanopy.UseVisualStyleBackColor = true;
            // 
            // tpLitter
            // 
            this.tpLitter.Location = new System.Drawing.Point(4, 46);
            this.tpLitter.Margin = new System.Windows.Forms.Padding(4);
            this.tpLitter.Name = "tpLitter";
            this.tpLitter.Size = new System.Drawing.Size(712, 482);
            this.tpLitter.TabIndex = 5;
            this.tpLitter.Text = "Litter";
            this.tpLitter.UseVisualStyleBackColor = true;
            // 
            // tpSepticSystems
            // 
            this.tpSepticSystems.Location = new System.Drawing.Point(4, 46);
            this.tpSepticSystems.Margin = new System.Windows.Forms.Padding(4);
            this.tpSepticSystems.Name = "tpSepticSystems";
            this.tpSepticSystems.Size = new System.Drawing.Size(712, 482);
            this.tpSepticSystems.TabIndex = 6;
            this.tpSepticSystems.Text = "Septic Systems";
            this.tpSepticSystems.UseVisualStyleBackColor = true;
            // 
            // tpMinerals
            // 
            this.tpMinerals.Location = new System.Drawing.Point(4, 46);
            this.tpMinerals.Margin = new System.Windows.Forms.Padding(4);
            this.tpMinerals.Name = "tpMinerals";
            this.tpMinerals.Size = new System.Drawing.Size(712, 482);
            this.tpMinerals.TabIndex = 7;
            this.tpMinerals.Text = "Minerals";
            this.tpMinerals.UseVisualStyleBackColor = true;
            // 
            // tpSediment
            // 
            this.tpSediment.Location = new System.Drawing.Point(4, 46);
            this.tpSediment.Margin = new System.Windows.Forms.Padding(4);
            this.tpSediment.Name = "tpSediment";
            this.tpSediment.Size = new System.Drawing.Size(712, 482);
            this.tpSediment.TabIndex = 8;
            this.tpSediment.Text = "Sediment";
            this.tpSediment.UseVisualStyleBackColor = true;
            // 
            // tpPhytoplankton
            // 
            this.tpPhytoplankton.Location = new System.Drawing.Point(4, 46);
            this.tpPhytoplankton.Margin = new System.Windows.Forms.Padding(4);
            this.tpPhytoplankton.Name = "tpPhytoplankton";
            this.tpPhytoplankton.Size = new System.Drawing.Size(712, 482);
            this.tpPhytoplankton.TabIndex = 9;
            this.tpPhytoplankton.Text = "Phytoplankton";
            this.tpPhytoplankton.UseVisualStyleBackColor = true;
            // 
            // tpPeriphyton
            // 
            this.tpPeriphyton.Location = new System.Drawing.Point(4, 46);
            this.tpPeriphyton.Margin = new System.Windows.Forms.Padding(4);
            this.tpPeriphyton.Name = "tpPeriphyton";
            this.tpPeriphyton.Size = new System.Drawing.Size(712, 482);
            this.tpPeriphyton.TabIndex = 10;
            this.tpPeriphyton.Text = "Periphyton";
            this.tpPeriphyton.UseVisualStyleBackColor = true;
            // 
            // tpFoodWeb
            // 
            this.tpFoodWeb.Location = new System.Drawing.Point(4, 46);
            this.tpFoodWeb.Margin = new System.Windows.Forms.Padding(4);
            this.tpFoodWeb.Name = "tpFoodWeb";
            this.tpFoodWeb.Size = new System.Drawing.Size(712, 482);
            this.tpFoodWeb.TabIndex = 11;
            this.tpFoodWeb.Text = "Food Web";
            this.tpFoodWeb.UseVisualStyleBackColor = true;
            // 
            // tpParameters
            // 
            this.tpParameters.Location = new System.Drawing.Point(4, 46);
            this.tpParameters.Margin = new System.Windows.Forms.Padding(4);
            this.tpParameters.Name = "tpParameters";
            this.tpParameters.Size = new System.Drawing.Size(712, 482);
            this.tpParameters.TabIndex = 12;
            this.tpParameters.Text = "Parameters";
            this.tpParameters.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(483, 554);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(183, 48);
            this.btnHelp.TabIndex = 16;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(268, 554);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(183, 48);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(49, 554);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(183, 48);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dgvLandUseParameter
            // 
            this.dgvLandUseParameter.AllowUserToAddRows = false;
            this.dgvLandUseParameter.AllowUserToDeleteRows = false;
            this.dgvLandUseParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLandUseParameter.Location = new System.Drawing.Point(18, 59);
            this.dgvLandUseParameter.Name = "dgvLandUseParameter";
            this.dgvLandUseParameter.Size = new System.Drawing.Size(678, 409);
            this.dgvLandUseParameter.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Land Use Parameter";
            // 
            // cbLandUseParameter
            // 
            this.cbLandUseParameter.FormattingEnabled = true;
            this.cbLandUseParameter.Items.AddRange(new object[] {
            "Fraction Open in Winter",
            "Rainfall Detachment Factor",
            "Flow Detachment Factor",
            "Fraction Impervious",
            "Interception Storage, cm",
            "Long-Term Growth",
            "Leaf Growth Factor",
            "Productivity, kg/m2/year",
            "Active Respiration, 1/day",
            "Maintenance Respiration, 1/day",
            "Dry Collection Efficiency",
            "Wet Collection Efficiency",
            "Leaf Weight/Area, g/cm2",
            "Canopy Height, m",
            "Stomatal Resistance, s/cm",
            "Cropping Factor",
            "Leaf Area Index",
            "Annual Uptake Distribution",
            "Litter Fall Rate, kg/m2/month",
            "Exudation Rate, 1/day",
            "Leaf Composition",
            "Trunk Composition"});
            this.cbLandUseParameter.Location = new System.Drawing.Point(204, 17);
            this.cbLandUseParameter.Name = "cbLandUseParameter";
            this.cbLandUseParameter.Size = new System.Drawing.Size(269, 24);
            this.cbLandUseParameter.TabIndex = 2;
            this.cbLandUseParameter.Text = "--Select--";
            this.cbLandUseParameter.SelectedIndexChanged += new System.EventHandler(this.cbLandUseParameter_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(485, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "Edit Land Use List";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DialogSystemCoeffs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(733, 618);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tcSystemTabs);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DialogSystemCoeffs";
            this.Text = "System Coefficients";
            this.tcSystemTabs.ResumeLayout(false);
            this.tpPhysicalData.ResumeLayout(false);
            this.tpPhysicalData.PerformLayout();
            this.tpLandUses.ResumeLayout(false);
            this.tpLandUses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLandUseParameter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcSystemTabs;
        private System.Windows.Forms.TabPage tpPhysicalData;
        private System.Windows.Forms.TabPage tpLandUses;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbLatitude;
        private System.Windows.Forms.TabPage tpSnowIce;
        private System.Windows.Forms.TabPage tpHeatLight;
        private System.Windows.Forms.TabPage tpCanopy;
        private System.Windows.Forms.TabPage tpLitter;
        private System.Windows.Forms.TabPage tpSepticSystems;
        private System.Windows.Forms.TabPage tpMinerals;
        private System.Windows.Forms.TabPage tpSediment;
        private System.Windows.Forms.TabPage tpPhytoplankton;
        private System.Windows.Forms.TabPage tpPeriphyton;
        private System.Windows.Forms.TabPage tpFoodWeb;
        private System.Windows.Forms.TabPage tpParameters;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.TextBox tbElevation;
        private System.Windows.Forms.TextBox tbLongitude;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbLandUseParameter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvLandUseParameter;
    }
}