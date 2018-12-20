using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using EGIS;
using EGIS.ShapeFileLib;

namespace warmf {
	public partial class FormMain : Form {
		private FormData frmData;
		private FormKnowledge frmKnow;
		private FormManager frmManager;
		private FormTMDL frmTMDL;
		private FormConsensus frmConsensus;

		// what's on the map
		bool showMETStations = false;
		
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
				OpenShapeFile(Global.DATA_DIR + "shp/Catawba.shp");
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

			lblLatLong.Visible = true;
			frmMap.Focus();

			// read in Coefficients file
			string fname = Global.DATA_DIR+"coe/Catawba.coe";
			//string fname = Global.DATA_DIR+"coe/Henn.coe";
			//string fname = Global.DATA_DIR+"coe/SanJoaquin.coe";

			if (!Global.coe.ReadFile(fname)) {
				MessageBox.Show(this, "Error reading coefficients file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			}

		}

		private void OpenShapeFile(string filename) {
			Logger.Info("Opening shape file " + filename);
			// clear any shapefiles the map is currently displaying
			this.frmMap.ClearShapeFiles();

			// open the shapefile passing in the path, display name of the shapefile and
			// the field name to be used when rendering the shapes (we use an empty string
			// as the field name (3rd parameter) can not be null)
			this.frmMap.AddShapeFile(filename, "ShapeFile", "");

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

			// for debugging
			//Point clickPt = new Point(e.X, e.Y);	clickPt is in GIS coords
			//PointD pt = frmMap.PixelCoordToGisPoint(clickPt);
			//MessageBox.Show("Pixel point = "+pt.X+", "+pt.Y+" :" + "GIS X,Y point = " + pt.X + ", " + pt.Y);
			//return;

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
				while (Global.coe.catchments[ii].idNum != catchNum)
					if (ii < Global.coe.numCatchments - 1)
						ii++;
					else {
						Debug.WriteLine("No catchment found with IDnum = " + catchNum);
						return;
					}

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

		private void DrawMarker(Graphics g, double longitude, double latitude) {
			Point pt = frmMap.GisPointToPixelCoord(longitude, latitude);
			int width = 10;

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			g.DrawLine(Pens.Red, pt.X, pt.Y - width, pt.X, pt.Y + width);
			g.DrawLine(Pens.Red, pt.X - width, pt.Y, pt.X + width, pt.Y);
			pt.Offset(-width / 2, -width / 2);
			g.FillEllipse(Brushes.Yellow, pt.X, pt.Y, width, width);
			g.DrawEllipse(Pens.Red, pt.X, pt.Y, width, width);
		}

		private void miMETStations_Click(object sender, EventArgs e) {
			//show MET stations on map
			if (showMETStations) {
				showMETStations = false;
				miViewMETStations.Text = miViewMETStations.Text.Substring(1);
				frmMap.Refresh();
				return;
			}
			showMETStations = true;
			miViewMETStations.Text = Global.checkmark + miViewMETStations.Text;
			frmMap.Refresh();
		}
			
		// need to change to creating a new shpfile layer on frmMap and show/hide it - MRL
		private void DrawMETStations(Graphics g) {
			List<METFile> mFiles = new List<METFile>();
			for (int ii = 0; ii < Global.coe.numMETFiles; ii++) {
				METFile met = new METFile("data/met/" + Global.coe.METFilename[ii]);
				met.ReadMETFile();  // for better performance, we could just read MET file headers with new method - MRL
				mFiles.Add(met);
			}

			for (int ii = 0; ii < Global.coe.numMETFiles; ii++) {
				METFile met = mFiles[ii];
				DrawMarker(g, met.longitude, met.latitude);
			}
		}

		private void frmMap_Load(object sender, EventArgs e) {
			this.frmMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMap_MouseMove);
			this.frmMap.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMap_Paint);
		}

		private void frmMap_MouseMove(object sender, MouseEventArgs e) {
			PointD pt = frmMap.PixelCoordToGisPoint(e.Y, e.X);
			lblLatLong.Text = "Lat/Long: " + pt.Y + ", " + pt.X;
		}

		private void frmMap_Paint (object sender, PaintEventArgs e) {
			if (showMETStations) {
				Graphics g = e.Graphics;
				DrawMETStations(g);
			}
				
		}
	}
}

