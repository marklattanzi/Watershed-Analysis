﻿using System;
using System.Text;
using System.Windows.Forms;

namespace warmf {
	public partial class FormMain : Form {
		private FormData frmData;
		private FormKnowledge frmKnow;
		private FormManager frmManager;
		private FormTMDL frmTMDL;
		private FormConsensus frmConsensus;

		// sub forms of Engineering (Main) module
		public FormCatch frmCatch;

		public FormMain() {
			InitializeComponent();

			Logger.useTime = true;
			Logger.Info("*********************************************************************************");
			Logger.Info("Logging started");
			Logger.useTime = false;

			// module forms
			frmData = new FormData(this);
			frmKnow = new FormKnowledge(this);
			frmManager = new FormManager(this);
			frmConsensus = new FormConsensus(this);
			frmTMDL = new FormTMDL(this);

			// sub forms of engr module form
			frmCatch = new FormCatch(this); // used in Engr module to show catchment coefficients
		}

		private void FormMain_Load(object sender, EventArgs e) {
			frmMap.Hide();
			pboxSplash.Top = 100;
			pboxSplash.Left = 100;
			miFileClose.Visible = false;
			miFileImport.Visible = false;
			miFileExport.Visible = false;
			miFileSep1.Visible = false;
			miFileSave.Visible = false;
			miFileSaveAs.Visible = false;
			miFilePrint.Visible = false;
			miFilePrintPreview.Visible = false;
			miFilePrinterSetup.Visible = false;
			miTopEdit.Visible = false;
			miTopView.Visible = false;
			miTopMode.Visible = false;
			miModule.Visible = false;
			miTopScenario.Visible = false;
			miTopDocument.Visible = false;
			miTopWindow.Visible = false;

			// for testing - MRL
			this.Hide();
		}

		private void LoadDefault() {
			try {
				OpenShapeFile("Catawba.shp");
			}
			catch (Exception ex) {
				MessageBox.Show(this, "Error : " + ex.Message);
			}
		}

		private void miFileOpen_Click(object sender, EventArgs e) {
			if (dlgFileOpen.ShowDialog(this) == DialogResult.OK) {
				try {
					OpenShapeFile(dlgFileOpen.FileName);
				}
				catch (Exception ex) {
					MessageBox.Show(this, "Error : " + ex.Message);
				}
				SetupEngrModule();
			}
		}

		private void SetupEngrModule() {
			pboxSplash.Hide();
			frmMap.Show();
			miFileClose.Visible = true;
			miFileImport.Visible = true;
			miFileExport.Visible = true;
			miFileSep1.Visible = true;
			miFileSave.Visible = true;
			miFileSaveAs.Visible = true;
			miFilePrint.Visible = true;
			miFilePrintPreview.Visible = true;
			miFilePrinterSetup.Visible = true;

			miTopEdit.Visible = true;
			miTopView.Visible = true;
			miTopMode.Visible = true;
			miModule.Visible = true;
			miTopScenario.Visible = true;
			miTopDocument.Visible = true;
			miTopWindow.Visible = true;
			frmMap.Focus();

			// read in Coefficients file
			string fname = "data\\Catawba.coe";
			//string fname = "data\\Henn.coe";
			//string fname = "data\\SanJoaquin.coe";

			if (!Global.coe.ReadFile(fname)) {
				MessageBox.Show(this, "Error reading coefficients file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			}

		}

		private void OpenShapeFile(string path) {
			Logger.Info("Opening shape file " + path);
			// clear any shapefiles the map is currently displaying
			this.frmMap.ClearShapeFiles();

			// open the shapefile passing in the path, display name of the shapefile and
			// the field name to be used when rendering the shapes (we use an empty string
			// as the field name (3rd parameter) can not be null)
			this.frmMap.AddShapeFile(path, "ShapeFile", "");

			// read the shapefile dbf field names and set the shapefiles's RenderSettings
			// to use the first field to label the shapes.
			EGIS.ShapeFileLib.ShapeFile shpFile = this.frmMap[0];
			shpFile.RenderSettings.FieldName = shpFile.RenderSettings.DbfReader.GetFieldNames()[0];
			shpFile.RenderSettings.UseToolTip = true;
			shpFile.RenderSettings.ToolTipFieldName = shpFile.RenderSettings.FieldName;
			shpFile.RenderSettings.IsSelectable = true;
		}

		/*  scroll wheel works without this.  --MRL
        private void frmMap_MouseWheel(object sender, MouseEventArgs e) {
            if (e.Delta > 0) {
                miEditZoomIn_Click(sender, e);
            }
            else {
                miEditZoomOut_Click(sender, e);
            }
        }*/

		public void ShowForm(string name) {
			//this.Hide();	// ENGR window is always visible - MRL
			frmKnow.Hide();
			frmData.Hide();
			frmManager.Hide();
			frmTMDL.Hide();
			frmConsensus.Hide();

			Logger.Info("Showing form " + name);
			switch (name) {
				case "engineering": this.Show(); break;
				case "knowledge": frmKnow.Show(); break;
				case "data": frmData.Show(); break;
				case "manager": frmManager.Show(); break;
				case "tmdl": frmTMDL.Show(); break;
				case "consensus": frmConsensus.Show(); break;
			}
		}

		private void frmMap_MouseClick(object sender, MouseEventArgs e) {
			int recordIndex = frmMap.GetShapeIndexAtPixelCoord(0, e.Location, 8);
			if (recordIndex >= 0) {
				string[] recordAttributes = frmMap[0].GetAttributeFieldValues(recordIndex);
				string[] attributeNames = frmMap[0].GetAttributeFieldNames();
				StringBuilder sb = new StringBuilder();
				int catchNum = 0;
				for (int n = 0; n < attributeNames.Length; ++n) {
					sb.Append(attributeNames[n]).Append(':').AppendLine(recordAttributes[n].Trim());
					catchNum = Int32.Parse(recordAttributes[n]);
				}
				//MessageBox.Show(this, sb.ToString(), "record attributes", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

				// test of coefficients file read
				int ii = 0;
				while (Global.coe.catchments[ii].idNum != catchNum) ii++;

				sb.AppendLine("Name:" + Global.coe.catchments[ii].name);
				sb.AppendLine("MET file:" + Global.coe.METFilename[Global.coe.catchments[ii].METFileNum]);
				sb.AppendLine("Precipitation Weighting multiplier:" + Global.coe.catchments[ii].precipMultiplier);
				sb.AppendLine("Average temperature lapse:" + Global.coe.catchments[ii].aveTempLapse);
				sb.AppendLine("Altitudinal Temp Lapse:" + Global.coe.catchments[ii].altitudeTempLapse);
				sb.AppendLine("Output to file?:" + Global.coe.catchments[ii].swOutputToFile);
				sb.AppendLine("Air/rain chemistry file num" + Global.coe.catchments[ii].airRainChemFileNum);
				sb.AppendLine("Particle/rain chemistry file num:" + Global.coe.catchments[ii].particleRainChemFileNum);
				sb.AppendLine("Num soil layers:" + Global.coe.catchments[ii].numSoilLayers);
				sb.AppendLine("Slope:" + Global.coe.catchments[ii].slope);
				sb.AppendLine("Width:" + Global.coe.catchments[ii].width);
				sb.AppendLine("Aspect:" + Global.coe.catchments[ii].aspect);
				sb.AppendLine("Manning N:" + Global.coe.catchments[ii].ManningN);
				sb.AppendLine("Detention storage:" + Global.coe.catchments[ii].detentionStorage);

				WMDialog popup = new WMDialog("Shapefile Data", sb.ToString());
				//popup.ShowDialog();
				frmCatch.Populate(ii);
				frmCatch.ShowDialog();
			}
		}

		private void miFileExit_Click(object sender, EventArgs e) {
			System.Windows.Forms.Application.Exit();
		}

		private void miEditZoomIn_Click(object sender, EventArgs e) {
			if (frmMap.ZoomLevel < 10) {
				frmMap.ZoomLevel *= 2;
			}
		}

		private void miEditZoomOut_Click(object sender, EventArgs e) {
			if (frmMap.ZoomLevel > 0) {
				frmMap.ZoomLevel /= 2;
			}
		}

		private void pboxSplash_Click(object sender, EventArgs e) {
			LoadDefault();
			SetupEngrModule();  // shortcut to load SHP file --MRL
		}

		private void miHelpAbout_Click(object sender, EventArgs e) {
			WMDialog popup = new WMDialog("About WARMF", "Watershed Analysis Risk Management Framework\nVersion 7.0\n\nCopyright 2018\nSysTech Inc.\nWalnut Creek, CA\nAll rights reserved.", false);
			popup.SetTextColor(System.Drawing.Color.Green);
			popup.ShowDialog();
		}

		private void miData_Click(object sender, EventArgs e) {
			ShowForm("data");
		}

		private void miKnowledge_Click(object sender, EventArgs e) {
			ShowForm("knowledge");
		}

		private void miManager_Click(object sender, EventArgs e) {
			ShowForm("manager");
		}

		private void miTMDL_Click(object sender, EventArgs e) {
			ShowForm("tmdl");
		}

		private void miConsensus_Click(object sender, EventArgs e) {
			ShowForm("consensus");
		}

		private void miEngineering_Click(object sender, EventArgs e) {
			ShowForm("engineering");
		}

	}
}

