﻿using System;
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
		public DialogCatchCoeffs dlgCatchCoeffs;
        public DialogSystemCoeffs dlgSystemCoeffs;
        public DialogReservoirCoeffs dlgReservoirCoeffs;

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

            // dialogs called from within the Engineering Module (River Coefficients,
            // Catchment Coefficients, System Coefficients, Reservoir Coefficients)
			dlgCatchCoeffs = new DialogCatchCoeffs(this); // used in Engineering module to show catchment coefficients
            dlgSystemCoeffs = new DialogSystemCoeffs(this); //used in Engineering module to show the system coefficients
            dlgReservoirCoeffs = new DialogReservoirCoeffs(this); //used in Engineering module to show the reservoir coefficients
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
                //Add catchments shapefile (shapefile [0])
                this.frmMap.AddShapeFile(Global.DIR.DATA + "shp\\Catchments.shp", "ShapeFile", "");
                EGIS.ShapeFileLib.ShapeFile catchShapefile = this.frmMap[0];
                catchShapefile.RenderSettings.FieldName = catchShapefile.RenderSettings.DbfReader.GetFieldNames()[0];
                catchShapefile.RenderSettings.UseToolTip = true;
                catchShapefile.RenderSettings.ToolTipFieldName = catchShapefile.RenderSettings.FieldName;
                catchShapefile.RenderSettings.IsSelectable = true;
                catchShapefile.RenderSettings.FillColor = Color.FromArgb(224,250,207);
                catchShapefile.RenderSettings.OutlineColor = Color.FromArgb(178, 178, 178);
                ////Add rivers shapefile (shapefile [1])
                this.frmMap.AddShapeFile(Global.DIR.DATA + "shp\\Rivers.shp", "ShapeFile", "");
                EGIS.ShapeFileLib.ShapeFile riverShapefile = this.frmMap[1];
                riverShapefile.RenderSettings.FieldName = catchShapefile.RenderSettings.DbfReader.GetFieldNames()[0];
                riverShapefile.RenderSettings.UseToolTip = true;
                riverShapefile.RenderSettings.ToolTipFieldName = catchShapefile.RenderSettings.FieldName;
                riverShapefile.RenderSettings.IsSelectable = true;
                riverShapefile.RenderSettings.LineType = LineType.Solid;
                riverShapefile.RenderSettings.OutlineColor = Color.FromArgb(0, 0, 255);
                //add reservoirs shapefile (shapefile [2])
                this.frmMap.AddShapeFile(Global.DIR.DATA + "shp\\Lakes.shp", "ShapeFile", "");
                EGIS.ShapeFileLib.ShapeFile lakeShapefile = this.frmMap[2];
                lakeShapefile.RenderSettings.FieldName = catchShapefile.RenderSettings.DbfReader.GetFieldNames()[0];
                lakeShapefile.RenderSettings.UseToolTip = true;
                lakeShapefile.RenderSettings.ToolTipFieldName = catchShapefile.RenderSettings.FieldName;
                lakeShapefile.RenderSettings.IsSelectable = true;
                lakeShapefile.RenderSettings.FillColor = Color.FromArgb(151, 219, 242);
                lakeShapefile.RenderSettings.OutlineColor = Color.FromArgb(61, 101, 235);
            }
			catch (Exception ex) {
				MessageBox.Show(this, "Error : " + ex.Message);
			}
		}

        private void miFileOpen_Click(object sender, EventArgs e)
        {
            if (dlgFileOpen.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    //OpenShapeFile(dlgFileOpen.FileName);
                }
                catch (Exception ex)
                {
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
			string fname = Global.DIR.DATA+"coe\\Catawba.coe";
			//string fname = Global.DIR_DATA+"coe/Henn.coe";
			//string fname = Global.DIR_DATA+"coe/SanJoaquin.coe";

			if (!Global.coe.ReadFile(fname)) {
				MessageBox.Show(this, "Error reading coefficients file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			}

		}

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

        private void frmMap_MapDoubleClick(object sender, EGIS.Controls.SFMap.MapDoubleClickedEventArgs e)
        {
            e.Cancel = true;
        }

        private void frmMap_MouseDoubleClick(object sender, MouseEventArgs e) {

		int CatchmentRecordIndex = frmMap.GetShapeIndexAtPixelCoord(0, e.Location, 8);
        int riverRecordIndex = frmMap.GetShapeIndexAtPixelCoord(1, e.Location, 8);
        int reservoirRecordIndex = frmMap.GetShapeIndexAtPixelCoord(2, e.Location, 8);

        if (riverRecordIndex >= 0) //River segment selected - River coefficients
        {
            MessageBox.Show("River segment selected");
        }
        else if (reservoirRecordIndex >= 0) //Reservoir segment selected - Reservoir coefficients
        {
            string[] recordAttributes = frmMap[2].GetAttributeFieldValues(reservoirRecordIndex);
            string[] attributeNames = frmMap[2].GetAttributeFieldNames();
            int n = 0;
            while (attributeNames[n] != "WARMF_ID") //test of shapefile attributes
            {
                if (n < (attributeNames.Length - 1))
                    n++;
                else
                {
                    Debug.WriteLine("No WARMF_ID Field found in reservoirs attribute table");
                    return;
                }
            }
            int reservoirID = Int32.Parse(recordAttributes[n]);

            int iRes = 0;
            int iSeg = 0;
            while (Global.coe.reservoirs[iRes].reservoirSegs[iSeg].idNum != reservoirID) //test of coefficients file read
                if (iSeg < Global.coe.reservoirs[iRes].numSegments - 1)
                    iSeg++;
                else
                    if (iRes < Global.coe.numReservoirs - 1)
                        iRes++;
                    else
                    {
                    Debug.WriteLine("No reservoir found with IDnum = " + reservoirID);
                    return;
                    }
            dlgReservoirCoeffs.Populate(iRes, iSeg);
            dlgReservoirCoeffs.ShowDialog();
        }
        else if (CatchmentRecordIndex >= 0) //Catchment selected - Catchment coefficients
        {
            string[] recordAttributes = frmMap[0].GetAttributeFieldValues(CatchmentRecordIndex);
            string[] attributeNames = frmMap[0].GetAttributeFieldNames();
            int n = 0;
            while (attributeNames[n] != "WARMF_ID") //test of shapefile attributes
            {
                if (n < (attributeNames.Length - 1))
                    n++;
                else
                {
                    Debug.WriteLine("No WARMF_ID Field found in catchments attribute table");
                    return;
                }
            }
            int catchID = Int32.Parse(recordAttributes[n]);
            int ii = 0;
            while (Global.coe.catchments[ii].idNum != catchID) //test of coefficients file read
                if (ii < Global.coe.numCatchments - 1)
                    ii++;
                else
                {
                    Debug.WriteLine("No catchment found with IDnum = " + catchID);
                    return;
                }
            dlgCatchCoeffs.Populate(ii);
            dlgCatchCoeffs.ShowDialog();
        }
            
        else //System coefficients
        {
            dlgSystemCoeffs.Populate();
            dlgSystemCoeffs.ShowDialog();
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

