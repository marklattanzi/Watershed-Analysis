using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using EGIS;
using EGIS.ShapeFileLib;

namespace warmf {
    public partial class FormMain : Form
    {
        #region Declarations
        private FormData frmData;
        private FormKnowledge frmKnow;
        private FormManager frmManager;
        private FormTMDL frmTMDL;
        private FormConsensus frmConsensus;

        // public shapefiles
        public EGIS.ShapeFileLib.ShapeFile catchShapefile;
        public EGIS.ShapeFileLib.ShapeFile riverShapefile;
        public EGIS.ShapeFileLib.ShapeFile lakeShapefile;

        // what's showing on the map
        bool showMETStations = false;
        bool showGageStations = false;
        bool showWQStations = false;
        bool showFLOStations = false;
        bool showPTSStations = false;
        bool showAIRStations = false;

        public LayerInfo catchmentLayer;
        public LayerInfo riverLayer;
        public LayerInfo reservoirLayer;
        public List<LayerInfo> layers = new List<LayerInfo>();
        public List<ScenarioInfo> scenarios = new List<ScenarioInfo>();

        // sub forms of Engineering (Main) module
        public DialogRiverCoeffs dlgRiverCoeffs;
        public DialogCatchCoeffs dlgCatchCoeffs;
        public DialogSystemCoeffs dlgSystemCoeffs;
        public DialogReservoirCoeffs dlgReservoirCoeffs;
        public DialogOutput dlgOutput;
        public DialogRunSimulation dlgRunSimulation;

        // Scenarios
        public bool scenarioChanged;
        public class ScenarioInfo
        {
            public int IsOpen;
            public int IsActive;
            public string Name;
        }

        public struct LayerInfo
        {
            public string Name;
            public string FileName;
        }
        #endregion

        public FormMain()
        {
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
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
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
            toolStrip1.Visible = false;
            // for testing - MRL
            this.Hide();
        }

        // Loads the catchment, river, and reservoir shapefiles
        private void LoadCatchmentRiverReservoirShapefiles()
        {
            try
            {
                //Add catchments shapefile (shapefile [0])
                this.frmMap.AddShapeFile(Global.DIR.SHP + catchmentLayer.FileName, catchmentLayer.Name, "");
                catchShapefile = this.frmMap[0];
                catchShapefile.RenderSettings.FieldName = catchShapefile.RenderSettings.DbfReader.GetFieldNames()[0];
                catchShapefile.RenderSettings.UseToolTip = true;
                catchShapefile.RenderSettings.ToolTipFieldName = catchShapefile.RenderSettings.FieldName;
                catchShapefile.RenderSettings.IsSelectable = false;
                catchShapefile.RenderSettings.FillColor = Color.FromArgb(224, 250, 207);
                catchShapefile.RenderSettings.OutlineColor = Color.FromArgb(178, 178, 178);
                //Add rivers shapefile (shapefile [1])
                this.frmMap.AddShapeFile(Global.DIR.SHP + riverLayer.FileName, riverLayer.Name, "");
                riverShapefile = this.frmMap[1];
                riverShapefile.RenderSettings.FieldName = catchShapefile.RenderSettings.DbfReader.GetFieldNames()[0];
                riverShapefile.RenderSettings.UseToolTip = true;
                riverShapefile.RenderSettings.ToolTipFieldName = catchShapefile.RenderSettings.FieldName;
                riverShapefile.RenderSettings.IsSelectable = false;
                riverShapefile.RenderSettings.LineType = LineType.Solid;
                riverShapefile.RenderSettings.OutlineColor = Color.FromArgb(0, 0, 255);
                //add reservoirs shapefile (shapefile [2])
                this.frmMap.AddShapeFile(Global.DIR.SHP + reservoirLayer.FileName, reservoirLayer.Name, "");
                lakeShapefile = this.frmMap[2];
                lakeShapefile.RenderSettings.FieldName = catchShapefile.RenderSettings.DbfReader.GetFieldNames()[0];
                lakeShapefile.RenderSettings.UseToolTip = true;
                lakeShapefile.RenderSettings.ToolTipFieldName = catchShapefile.RenderSettings.FieldName;
                lakeShapefile.RenderSettings.IsSelectable = false;
                lakeShapefile.RenderSettings.FillColor = Color.FromArgb(151, 219, 242);
                lakeShapefile.RenderSettings.OutlineColor = Color.FromArgb(61, 101, 235);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error : " + ex.Message);
            }
        }

        private void SetupEngrModule()
        {
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
            miModeInput.Select();
            miModeInput.BackColor = System.Drawing.SystemColors.Highlight;
            miModule.Visible = true;
            miTopScenario.Visible = true;
            miTopDocument.Visible = true;
            miTopWindow.Visible = true;

            lblLatLong.Visible = true;
            toolStrip1.Visible = true;
            frmMap.Focus();
            frmMap.ZoomToSelectedExtentWhenCtrlKeydown = false;
        }

        public void ShowForm(string name)
        {
            frmKnow.Hide();
            frmData.Hide();
            frmManager.Hide();
            frmTMDL.Hide();
            frmConsensus.Hide();

            Logger.Info("Showing form " + name);
            switch (name)
            {
                case "engineering": this.Show(); break;
                case "knowledge": frmKnow.Show(); break;
                case "data": frmData.Show(); break;
                case "manager": frmManager.Show(); break;
                case "tmdl": frmTMDL.Show(); break;
                case "consensus": frmConsensus.Show(); break;
            }
        }

        public int GetWarmfIDFieldIndex(int shapefileIndex)
        {
            string[] attributeNames = frmMap[shapefileIndex].GetAttributeFieldNames();
            int n = 0;
            while (attributeNames[n] != "WARMF_ID") //test of shapefile attributes
            {
                if (n < (attributeNames.Length - 1))
                    n++;
                else
                {
                    Debug.WriteLine("No WARMF_ID Field found in rivers attribute table");
                    return -1;
                }
            }
            return n;
        }

        #region Map Interaction Events
        
        private void frmMap_MapDoubleClick(object sender, EGIS.Controls.SFMap.MapDoubleClickedEventArgs e)
        {
            e.Cancel = true;
        }

        private void frmMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            List<int> shapefileRecord = new List<int>();
            List<int> shapefileNumber = new List<int>();
            int numShapeFiles = frmMap.ShapeFileCount;
            int CatchmentRecordIndex = frmMap.GetShapeIndexAtPixelCoord(0, e.Location, 8);
            int riverRecordIndex = frmMap.GetShapeIndexAtPixelCoord(1, e.Location, 8);
            int reservoirRecordIndex = frmMap.GetShapeIndexAtPixelCoord(2, e.Location, 8);

            if (numShapeFiles > 3) //three shapefiles are default - catchments, rivers, lakes
            {
                string fileName;
                for (int i = 3; i < numShapeFiles; i++)
                {
                    int record = frmMap.GetShapeIndexAtPixelCoord(i, e.Location, 8);
                    if (record >= 0) //record exists at this location for this shapefile
                    {
                        string[] recordAttributes = frmMap[i].GetAttributeFieldValues(record);
                        string[] attributeNames = frmMap[i].GetAttributeFieldNames();
                        for (int j = 0; j < attributeNames.Length; j++)
                        {
                            if (attributeNames[j] == "FileName")
                            {
                                fileName = recordAttributes[j];
                                fileName = fileName.Trim();
                                string fileType = fileName.Substring(fileName.Length - 3);
                                if (fileType == "MET")
                                    frmData.cboxTypeOfFile.SelectedIndex = 0;
                                else if (fileType == "AIR")
                                    frmData.cboxTypeOfFile.SelectedIndex = 1;
                                else if (fileType == "ORH" || fileType == "OLH")
                                    frmData.cboxTypeOfFile.SelectedIndex = 2;
                                else if (fileType == "ORC" || fileType == "OLC")
                                    frmData.cboxTypeOfFile.SelectedIndex = 3;
                                else if (fileType == "FLO")
                                    frmData.cboxTypeOfFile.SelectedIndex = 4;
                                else if (fileType == "PTS")
                                    frmData.cboxTypeOfFile.SelectedIndex = 5;
                                else
                                {
                                    MessageBox.Show("File extension does not match any of the recognized WARMF file extensions", "Exception/Error", MessageBoxButtons.OK);
                                    return;
                                }
                                frmData.cboxFilename.SelectedIndex =
                                    frmData.cboxFilename.FindString(fileName);
                                frmData.cboxData.SelectedIndex = 0;
                                frmData.Show();
                                return;
                            }
                        }
                    }
                }
            }

            if (riverRecordIndex >= 0) //River segment selected - River coefficients
            {
                string[] recordAttributes = frmMap[1].GetAttributeFieldValues(riverRecordIndex);
                string[] attributeNames = frmMap[1].GetAttributeFieldNames();
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
                int riverID = Int32.Parse(recordAttributes[n]);
                int ii = 0;
                while (Global.coe.rivers[ii].idNum != riverID) //test of coefficients file read
                    if (ii < Global.coe.numRivers - 1)
                        ii++;
                    else
                    {
                        Debug.WriteLine("No river found with IDnum = " + riverID);
                        return;
                    }
                if (miModeInput.BackColor == System.Drawing.SystemColors.Highlight)
                {
                    using (dlgRiverCoeffs = new DialogRiverCoeffs(this))
                    {
                        dlgRiverCoeffs.Populate(ii);
                        if (dlgRiverCoeffs.ShowDialog() == DialogResult.OK)
                            scenarioChanged = true;
                    }
                }
                else if (miModeOutput.BackColor == System.Drawing.SystemColors.Highlight)
                {
                    using (dlgOutput = new DialogOutput(this))
                    {
                        dlgOutput.Populate("River", ii);
                        dlgOutput.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Gowdy or Herr Output Selected");
                }

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
                if (miModeInput.BackColor == System.Drawing.SystemColors.Highlight)
                {
                    using (dlgReservoirCoeffs = new DialogReservoirCoeffs(this))
                    {
                        dlgReservoirCoeffs.Populate(iRes, iSeg);
                        if (dlgReservoirCoeffs.ShowDialog() == DialogResult.OK)
                            scenarioChanged = true;
                    }
                }
                else if (miModeOutput.BackColor == System.Drawing.SystemColors.Highlight)
                {
                    //need to populate the dialog - but is it the same dialog as is used for catchments and rivers?
                    using (dlgOutput = new DialogOutput(this))
                    {
                        dlgOutput.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Gowdy or Herr Output Selected");
                }
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
                if (miModeInput.BackColor == System.Drawing.SystemColors.Highlight)
                {
                    using (dlgCatchCoeffs = new DialogCatchCoeffs(this))
                    {
                        dlgCatchCoeffs.Populate(ii);
                        if (dlgCatchCoeffs.ShowDialog() == DialogResult.OK)
                            scenarioChanged = true;
                    }
                }
                else if (miModeOutput.BackColor == System.Drawing.SystemColors.Highlight)
                {
                    using (dlgOutput = new DialogOutput(this))
                    {
                        dlgOutput.Populate("Catchment", ii);
                        dlgOutput.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Gowdy or Herr Output Selected");
                }
            }

            else //System coefficients
            {
                if (miModeInput.BackColor == System.Drawing.SystemColors.Highlight)
                {
                    using (dlgSystemCoeffs = new DialogSystemCoeffs(this))
                    {
                        dlgSystemCoeffs.Populate();
                        if (dlgSystemCoeffs.ShowDialog() == DialogResult.OK)
                            scenarioChanged = true;
                    }
                }
                else if (miModeOutput.BackColor == System.Drawing.SystemColors.Highlight)
                {
                    MessageBox.Show("Display Spatial Output Dialog");
                }
                else
                {
                    MessageBox.Show("Gowdy or Herr Output Selected");
                }
            }
        }

        private void frmMap_Load(object sender, EventArgs e)
        {
            this.frmMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMap_MouseMove);
            //this.frmMap.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMap_Paint);
        }

        private void frmMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointD pt = frmMap.PixelCoordToGisPoint(e.Y, e.X);
            lblLatLong.Text = "Lat/Long: " + pt.Y + ", " + pt.X;
        }

        //private void frmMap_Paint(object sender, PaintEventArgs e){}

        #endregion

        private void pboxSplash_Click(object sender, EventArgs e)
        {
/*            catchmentLayer.FileName = "catchments.shp";
            riverLayer.FileName = "rivers.shp";
            reservoirLayer.FileName = "lakes.shp";
            LoadCatchmentRiverReservoirShapefiles();
            SetupEngrModule();  // shortcut to load SHP file --MRL

            // read in Coefficients file
            string fname = Global.DIR.COE + "Catawba_69b.coe";

            if (!Global.coe.ReadCOE(fname))
            {
                MessageBox.Show(this, "Error reading coefficients file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }*/
        }

        #region File Menu Events
        private void miFileOpen_Click(object sender, EventArgs e)
        {
            int i;

            OpenFileDialog openDialog = new OpenFileDialog
            {
                InitialDirectory = Global.DIR.ROOT,
                FileName = "",
                DefaultExt = ".wpf",
                Filter = "WARMF Project File (.wpf)|*.wpf",
                Title = "Select WARMF Project File"
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    STechStreamReader sr = new STechStreamReader(openDialog.FileName);
                    bool endOfLine = false;
                    // Read where it says "VERSION"
                    sr.ReadDelimitedField(',', ref endOfLine);
                    int version = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                    if (version >= 2)
                    {
                        // Read directory structure
                        int numDirectories = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                        for (i = 0; i < numDirectories; i++)
                        {
                            // Read the name and directory
                            string fieldString = sr.ReadDelimitedField(',', ref endOfLine);
                            string dirString = sr.ReadDelimitedField(',', ref endOfLine);

                            // Check to make sure the directory ends with a backslash
                            if (!dirString.EndsWith("\\"))
                                dirString = dirString + "\\";

                            if (fieldString.StartsWith("DATA"))
                            {
                                Global.DIR.DATA = Global.DIR.ROOT + dirString;
                            }
                            else if (fieldString.StartsWith("INPUT"))
                            {
                                Global.DIR.INPUT = Global.DIR.DATA + dirString;
                            }
                            else if (fieldString.StartsWith("OUTPUT"))
                            {
                                Global.DIR.OUTPUT = Global.DIR.DATA + dirString;
                            }
                            else if (fieldString.StartsWith("SHP"))
                            {
                                Global.DIR.SHP = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("COE"))
                            {
                                Global.DIR.COE = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("NPT"))
                            {
                                Global.DIR.NPT = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("AIR"))
                            {
                                Global.DIR.AIR = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("CPA"))
                            {
                                Global.DIR.CPA = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("FLO"))
                            {
                                Global.DIR.FLO = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("MET"))
                            {
                                Global.DIR.MET = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("OLC"))
                            {
                                Global.DIR.OLC = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("OLH"))
                            {
                                Global.DIR.OLH = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("ORC"))
                            {
                                Global.DIR.ORC = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("ORH"))
                            {
                                Global.DIR.ORH = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("PTS"))
                            {
                                Global.DIR.PTS = Global.DIR.INPUT + dirString;
                            }
                            else if (fieldString.StartsWith("CAT"))
                            {
                                Global.DIR.CAT = Global.DIR.OUTPUT + dirString;
                            }
                            else if (fieldString.StartsWith("RIV"))
                            {
                                Global.DIR.RIV = Global.DIR.OUTPUT + dirString;
                            }
                            else if (fieldString.StartsWith("LAK"))
                            {
                                Global.DIR.LAK = Global.DIR.OUTPUT + dirString;
                            }
                            else if (fieldString.StartsWith("PSM"))
                            {
                                Global.DIR.PSM = Global.DIR.OUTPUT + dirString;
                            }
                            else if (fieldString.StartsWith("QWQ"))
                            {
                                Global.DIR.QWQ = Global.DIR.OUTPUT + dirString;
                            }
                            else if (fieldString.StartsWith("WST"))
                            {
                                Global.DIR.WST = Global.DIR.OUTPUT + dirString;
                            }
                        }

                        // Read the catchment, river, and reservoir layer information
                        catchmentLayer.Name = sr.ReadDelimitedField(',', ref endOfLine);
                        catchmentLayer.FileName = sr.ReadDelimitedField(',', ref endOfLine);
                        riverLayer.Name = sr.ReadDelimitedField(',', ref endOfLine);
                        riverLayer.FileName = sr.ReadDelimitedField(',', ref endOfLine);
                        reservoirLayer.Name = sr.ReadDelimitedField(',', ref endOfLine);
                        reservoirLayer.FileName = sr.ReadDelimitedField(',', ref endOfLine);

                        LoadCatchmentRiverReservoirShapefiles();

                        // Read reference layer information
                        int numReferenceLayers = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                        layers.Clear();
                        for (i = 0; i < numReferenceLayers; i++)
                        {
                            LayerInfo referenceLayer = new LayerInfo
                            {
                                Name = sr.ReadDelimitedField(',', ref endOfLine),
                                FileName = sr.ReadDelimitedField(',', ref endOfLine)
                            };
                            layers.Add(referenceLayer);
                            miTopView.DropDownItems.Add(referenceLayer.Name);
                        }

                        // Read the information on scenarios in the project
                        int numScenarios = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                        scenarios.Clear();
                        for (i = 0; i < numScenarios; i++)
                        {
                            ScenarioInfo newScenario = new ScenarioInfo
                            {
                                IsOpen = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine)),
                                IsActive = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine)),
                                Name = sr.ReadDelimitedField(',', ref endOfLine)
                            };

                            // Add open scenarios to the bottom of the Scenario menu
                            if (newScenario.IsOpen > 0)
                            {
                                string scenarioName = Path.GetFileNameWithoutExtension(newScenario.Name);
                                miTopScenario.DropDownItems.Add(scenarioName);
                            }

                            // Read the active scenario and check it in the scenario menu
                            if (newScenario.IsActive > 0)
                            {
                                string coeFileName = Global.DIR.COE + newScenario.Name;
                                Global.coe.ReadCOE(coeFileName);

                                int itemNumber = miTopScenario.DropDownItems.Count - 1;
                                if (itemNumber >= 0)
                                    ((ToolStripMenuItem)miTopScenario.DropDownItems[itemNumber]).Checked = true;
                            }

                            // Add the scenario to the master list of scenarios in the project
                            scenarios.Add(newScenario);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error : " + ex.Message);
                }
                SetupEngrModule();
            }
        }

        private void miFileExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        #endregion

        #region Edit Menu Events
        private void miEditSelectCatchments_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < catchShapefile.RecordCount; i++)
            {
                catchShapefile.SelectRecord(i, true);
            }
            frmMap.Refresh(true);
        }

        private void miEditSelectReservoir_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lakeShapefile.RecordCount; i++)
            {
                lakeShapefile.SelectRecord(i, true);
            }
            frmMap.Refresh(true);
        }

        private void miEditSelectRivers_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < riverShapefile.RecordCount; i++)
            {
                riverShapefile.SelectRecord(i, true);
            }
            frmMap.Refresh(true);
        }

        private void miEditSelectAll_Click(object sender, EventArgs e)
        {
            if (catchShapefile.IsSelectable)
            {
                for (int i = 0; i < catchShapefile.RecordCount; i++)
                {
                    catchShapefile.SelectRecord(i, true);
                }
            }
            if (riverShapefile.IsSelectable)
            {
                for (int i = 0; i < riverShapefile.RecordCount; i++)
                {
                    riverShapefile.SelectRecord(i, true);
                }
            }
            if (lakeShapefile.IsSelectable)
            {
                for (int i = 0; i < lakeShapefile.RecordCount; i++)
                {
                    lakeShapefile.SelectRecord(i, true);
                }
            }
            frmMap.Refresh(true);
        }

        private void miEditClearSelectedFeatures_Click(object sender, EventArgs e)
        {
            catchShapefile.ClearSelectedRecords();
            riverShapefile.ClearSelectedRecords();
            lakeShapefile.ClearSelectedRecords();
            frmMap.Refresh(true);
        }
        #endregion

        #region View Menu Events
        private void miViewZoomIn_Click(object sender, EventArgs e)
        {
            frmMap.ZoomLevel *= 1.5;
        }

        private void miViewZoomOut_Click(object sender, EventArgs e)
        {
            frmMap.ZoomLevel /= 1.5;
        }

        private void miViewZoomToExtent_Click(object sender, EventArgs e)
        {
            frmMap.ZoomToFullExtent();
            frmMap.ZoomLevel /= 1.05;
        }

        private void miViewSelectableCatchments_Click(object sender, EventArgs e)
        { 
            if (miViewSelectableCatchments.Checked)
            {
                catchShapefile.RenderSettings.IsSelectable = true;
                miViewSelectableCatchments.Checked = true;
                miEditSelectCatchments.Enabled = true;
                if (miViewSelectableLakes.Checked || miViewSelectableRivers.Checked)
                {
                    miEditSelectAll.Enabled = true;
                }
            }
            else
            {
                catchShapefile.ClearSelectedRecords();
                catchShapefile.RenderSettings.IsSelectable = false;
                miViewSelectableCatchments.Checked = false;
                miEditSelectCatchments.Enabled = false;
                if (miViewSelectableLakes.Checked == false || miViewSelectableRivers.Checked == false)
                {
                    miEditSelectAll.Enabled = false;
                }
                frmMap.Refresh(true);
            }
        }

        private void miViewSelectableRivers_Click(object sender, EventArgs e)
        {
            if (miViewSelectableRivers.Checked)
            {
                riverShapefile.RenderSettings.IsSelectable = true;
                miViewSelectableRivers.Checked = true;
                miEditSelectRivers.Enabled = true;
                if (miViewSelectableCatchments.Checked || miViewSelectableLakes.Checked)
                {
                    miEditSelectAll.Enabled = true;
                }
            }
            else
            {
                riverShapefile.ClearSelectedRecords();
                riverShapefile.RenderSettings.IsSelectable = false;
                miViewSelectableRivers.Checked = false;
                miEditSelectRivers.Enabled = false;
                if (miViewSelectableCatchments.Checked == false || miViewSelectableLakes.Checked == false)
                {
                    miEditSelectAll.Enabled = false;
                }
                frmMap.Refresh(true);
            }
        }

        private void miViewSelectableLakes_Click(object sender, EventArgs e)
        {
            if (miViewSelectableLakes.Checked)
            {
                lakeShapefile.RenderSettings.IsSelectable = true;
                miViewSelectableLakes.Checked = true;
                miEditSelectReservoir.Enabled = true;
                if (miViewSelectableRivers.Checked || miViewSelectableCatchments.Checked)
                {
                    miEditSelectAll.Enabled = true;
                }
                miEditSelectAll.Enabled = true;
            }
            else
            {
                lakeShapefile.ClearSelectedRecords();
                lakeShapefile.RenderSettings.IsSelectable = false;
                miViewSelectableLakes.Checked = false;
                miEditSelectReservoir.Enabled = false;
                if (miViewSelectableRivers.Checked == false || miViewSelectableCatchments.Checked == false)
                {
                    miEditSelectAll.Enabled = false;
                }
                frmMap.Refresh(true);
            }
        }

        private void miViewMETStations_Click(object sender, EventArgs e)
        {
            if (showMETStations) //remove points from map
            {
                showMETStations = false;
                for (int i = 0; i < frmMap.ShapeFileCount; i++)
                {
                    if (frmMap[i].Name == "Meteorology Stations")
                    {
                        frmMap.RemoveShapeFile(frmMap[i]);
                    }
                }
                return;
            }
            else //add points to map
            {
                if (STechShapes.CreateShapeFile("MET"))
                {
                    this.frmMap.AddShapeFile(Global.DIR.SHP + "MET.shp", "Meteorology Stations", "Name");
                    showMETStations = true;
                    frmMap.ZoomLevel /= 1.05;
                }
            }
        }

        private void miViewGagingStations_Click(object sender, EventArgs e)
        {
            if (showGageStations) //remove points from map
            {
                showGageStations = false;
                for (int i = 0; i < frmMap.ShapeFileCount; i++)
                {
                    if (frmMap[i].Name == "Gaging Stations")
                    {
                        frmMap.RemoveShapeFile(frmMap[i]);
                    }
                }
                return;
            }
            else //add points to map
            {
                if (STechShapes.CreateShapeFile("ORH"))
                {
                    this.frmMap.AddShapeFile(Global.DIR.SHP + "ORH.shp", "Gaging Stations", "Name");
                    showGageStations = true;
                    frmMap.ZoomLevel /= 1.05;
                }
            }
        }

        private void miViewWQStations_Click(object sender, EventArgs e)
        {
            if (showWQStations) //remove points from map
            {
                showWQStations = false;
                for (int i = 0; i < frmMap.ShapeFileCount; i++)
                {
                    if (frmMap[i].Name == "Water Quality Stations")
                    {
                        frmMap.RemoveShapeFile(frmMap[i]);
                    }
                }
                return;
            }
            else //add points to map
            {
                if (STechShapes.CreateShapeFile("ORC"))
                {
                    this.frmMap.AddShapeFile(Global.DIR.SHP + "ORC.shp", "Water Quality Stations", "Name");
                    showWQStations = true;
                    frmMap.ZoomLevel /= 1.05;
                }
            }
        }

        private void miViewManagedFlow_Click(object sender, EventArgs e)
        {
            if (showFLOStations) //remove points from map
            {
                showFLOStations = false;
                for (int i = 0; i < frmMap.ShapeFileCount; i++)
                {
                    if (frmMap[i].Name == "Managed Flow")
                    {
                        frmMap.RemoveShapeFile(frmMap[i]);
                    }
                }
                frmMap.Refresh();
                return;
            }
            else //add points to map
            {
                if (STechShapes.CreateShapeFile("FLO"))
                {
                    this.frmMap.AddShapeFile(Global.DIR.SHP + "FLO.shp", "Managed Flow", "Name");
                    showFLOStations = true;
                    frmMap.ZoomLevel /= 1.05;
                }
            }
        }

        private void miViewPointSources_Click(object sender, EventArgs e)
        {
            if (showPTSStations) //remove points from map
            {
                showPTSStations = false;
                for (int i = 0; i < frmMap.ShapeFileCount; i++)
                {
                    if (frmMap[i].Name == "Point Sources")
                    {
                        frmMap.RemoveShapeFile(frmMap[i]);
                    }
                }
                return;
            }
            else //add points to map
            {
                if (STechShapes.CreateShapeFile("PTS"))
                {
                    this.frmMap.AddShapeFile(Global.DIR.SHP + "PTS.shp", "Point Sources", "Name");
                    showPTSStations = true;
                    frmMap.ZoomLevel /= 1.05;
                }
                
            }
        }

        private void miViewAirQualityStations_Click(object sender, EventArgs e)
        {
            if (showAIRStations) //remove points from map
            {
                showAIRStations = false;
                for (int i = 0; i < frmMap.ShapeFileCount; i++)
                {
                    if (frmMap[i].Name == "Air Quality Stations")
                    {
                        frmMap.RemoveShapeFile(frmMap[i]);
                    }
                }
                return;
            }
            else //add points to map
            {
                if (STechShapes.CreateShapeFile("AIR"))
                {
                    this.frmMap.AddShapeFile(Global.DIR.SHP + "AIR.shp", "Air Quality Stations", "Name");
                    showAIRStations = true;
                    frmMap.ZoomLevel /= 1.05;
                }
            }
        }
        #endregion

        #region Mode Menu Events
        private void miModeOutput_Click(object sender, EventArgs e)
        {
            miModeInput.BackColor = System.Drawing.SystemColors.Control;
            miModeFluxOutput.BackColor = System.Drawing.SystemColors.Control;
            miModeLongGowdyOutput.BackColor = System.Drawing.SystemColors.Control;
            miModeOutput.BackColor = System.Drawing.SystemColors.Highlight;
        }

        private void miModeInput_Click(object sender, EventArgs e)
        {
            miModeInput.BackColor = System.Drawing.SystemColors.Highlight;
            miModeFluxOutput.BackColor = System.Drawing.SystemColors.Control;
            miModeLongGowdyOutput.BackColor = System.Drawing.SystemColors.Control;
            miModeOutput.BackColor = System.Drawing.SystemColors.Control;
        }

        private void miModeFluxOutput_Click(object sender, EventArgs e)
        {
            miModeInput.BackColor = System.Drawing.SystemColors.Control;
            miModeFluxOutput.BackColor = System.Drawing.SystemColors.Highlight;
            miModeLongGowdyOutput.BackColor = System.Drawing.SystemColors.Control;
            miModeOutput.BackColor = System.Drawing.SystemColors.Control;
        }

        private void miModeLongGowdyOutput_Click(object sender, EventArgs e)
        {
            miModeInput.BackColor = System.Drawing.SystemColors.Control;
            miModeFluxOutput.BackColor = System.Drawing.SystemColors.Control;
            miModeLongGowdyOutput.BackColor = System.Drawing.SystemColors.Highlight;
            miModeOutput.BackColor = System.Drawing.SystemColors.Control;
        }
        #endregion

        #region Scenario Menu Events
        private void miScenarioSave_Click(object sender, EventArgs e)
        {
            int activeScenario = GetActiveScenario();
            if (activeScenario >= 0)
            {
                if (Global.coe.WriteCOE(Global.DIR.COE + scenarios[activeScenario].Name, -1))
                    scenarioChanged = false;
            }
            else
                MessageBox.Show("There is no active scenario to save.");
        }

        public static bool ScenarioSaveAs(ref string newScenario)
        {
            // Get the new coefficient file
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                InitialDirectory = Global.DIR.COE,
                FileName = "",
                DefaultExt = ".coe",
                Filter = "WARMF Coefficient File (.coe)|*.coe"
            };
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                newScenario = saveDialog.FileName;

                // Replace the output file names
                Global.coe.SetOutputFileNames(newScenario);

                // Save the coefficients in memory to the new coefficient file
                Global.coe.WriteCOE(newScenario, -1);

                return true;
            }

            return false;
        }

        private void miScenarioSaveAs_Click(object sender, EventArgs e)
        {
            // Get the new scenario name
            string newScenario = "";
            if (ScenarioSaveAs(ref newScenario))
            {
                // Set the name of the active scenario to the new name
                int activeScenario = GetActiveScenario();
                if (activeScenario >= 0 && activeScenario < scenarios.Count)
                    scenarios[activeScenario].Name = Path.GetFileName(newScenario);
                else
                {
                    ScenarioInfo newScenarioInfo = new ScenarioInfo
                    {
                        Name = newScenario,
                        IsOpen = 1,
                        IsActive = 0
                    };
                    scenarios.Add(newScenarioInfo);
                    activeScenario = scenarios.Count - 1;
                    SetActiveScenario(scenarios.Count - 1);
                }

                // Change the scenario name in the list at the bottom of the menu
                int firstScenario = miTopScenario.DropDownItems.Count - GetNumberOfOpenScenarios();
                for (int i = firstScenario; i < miTopScenario.DropDownItems.Count; i++)
                {
                    // Set the name of the active scenario to the new name
                    if (((ToolStripMenuItem)miTopScenario.DropDownItems[i]).Checked)
                        miTopScenario.DropDownItems[i].Text = Path.GetFileNameWithoutExtension(scenarios[activeScenario].Name);
                }
            }
        }

        private void miScenarioCompare_Click(object sender, EventArgs e)
        {
            int activeScenario = GetActiveScenario();
            string firstCOEFileName = "";
            if (activeScenario >= 0)
                firstCOEFileName = Global.DIR.COE + scenarios[activeScenario].Name;
            else
            {
                if (!OpenExistingCOEFile(ref firstCOEFileName))
                    return;
            }

            string secondCOEFileName = "";
            if (!OpenExistingCOEFile(ref secondCOEFileName))
                return;

            // Check to see if both files are the same
            if (firstCOEFileName == secondCOEFileName)
            {
                MessageBox.Show("Can not compare one scenario against itself");
                return;
            }

            // Open the files for comparison
            try
            {
                StreamReader newCOE = new StreamReader(secondCOEFileName);
                StreamReader oldCOE = new StreamReader(firstCOEFileName);
                string compareFile = Global.DIR.COE + "compare.txt";
                StreamWriter swErrors = new StreamWriter(compareFile);
                string newCOEline, oldCOEline, newCOElineTrim;
                int result, i;
                char[] trimChars = { ' ' };

                i = 1;
                int differences = 0;
                newCOEline = newCOE.ReadLine();
                while (newCOEline != null)
                {
                    newCOElineTrim = newCOEline.TrimEnd(trimChars);
                    oldCOEline = oldCOE.ReadLine().TrimEnd(trimChars);
                    result = string.Compare(newCOElineTrim, oldCOEline);
                    if (result != 0)
                    {
                        // Check to see if the difference is just the output file names, which is not a real difference
                        if (!oldCOEline.StartsWith("FILES") || !newCOElineTrim.StartsWith("FILES"))
                        {
                            swErrors.WriteLine("Line " + i.ToString());
                            swErrors.WriteLine(Path.GetFileName(firstCOEFileName) + ":\n" + oldCOEline);
                            swErrors.WriteLine(Path.GetFileName(secondCOEFileName) + ":\n" + newCOElineTrim);
                            swErrors.WriteLine();

                            differences++;
                        }
                    }
                    i++;
                    newCOEline = newCOE.ReadLine();
                }
                swErrors.WriteLine("Lines Reviewed: " + i.ToString() + "; differences found: " + differences.ToString());
                swErrors.Close();
                newCOE.Close();
                oldCOE.Close();
                Process.Start(compareFile);
            }
            catch
            {
                MessageBox.Show("Unable to compare " + Path.GetFileName(firstCOEFileName) + " and " + Path.GetFileName(secondCOEFileName));
            }
        }

        private void miScenarioRun_Click(object sender, EventArgs e)
        {
            using (dlgRunSimulation = new DialogRunSimulation(this))
            {
                dlgRunSimulation.Populate();
                dlgRunSimulation.ShowDialog();
            }

        }

        private void miScenarioFileCheck_Click(object sender, EventArgs e)
        {
            Global.coe.RunFilesCrosscheck();
        }

        private void miScenarioManager_Click(object sender, EventArgs e)
        {
            DialogScenarioManager smDialog = new DialogScenarioManager(scenarios);

            if (smDialog.ShowDialog() == DialogResult.OK)
            {
                // Save the old active scenario name in case it is closed or removed from the project
                int oldActiveScenario = GetActiveScenario();
                string oldScenarioName = "";
                if (oldActiveScenario >= 0)
                    oldScenarioName = scenarios[oldActiveScenario].Name;

                // Clear the scenarios from the bottom of the scenario menu
                int firstScenario = miTopScenario.DropDownItems.Count - GetNumberOfOpenScenarios();
                for (int i = miTopScenario.DropDownItems.Count - 1; i >= firstScenario; i--)
                    miTopScenario.DropDownItems.RemoveAt(i);

                // Retrieve the new scenario info from the dialog
                smDialog.GetScenarioInfo(ref scenarios);

                // Put the open scenarios at the bottom of the scenario menu
                for (int i = 0; i < scenarios.Count; i++)
                    if (scenarios[i].IsOpen > 0)
                    {
                        miTopScenario.DropDownItems.Add(Path.GetFileNameWithoutExtension(scenarios[i].Name));
                        miTopScenario.DropDownItems[miTopScenario.DropDownItems.Count - 1].Click += new System.EventHandler(this.miScenarioSelected_Click);
                    }

                // Determine if the active scenario is among those in the list of open scenarios
                int newActiveScenario = GetScenarioNumberFromName(oldScenarioName);
                if (newActiveScenario < 0 || scenarios[newActiveScenario].IsOpen == 0)
                {
                    // Provide the opportunity to save changes to old scenario
                    if (scenarioChanged)
                    {
                        if (MessageBox.Show("Save changes to model coefficients?", "Scenario " + oldScenarioName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            Global.coe.WriteCOE(Global.DIR.COE + oldScenarioName, -1);

                        scenarioChanged = false;
                    }

                    // Previous active scenario is no longer an open scenario
                    // Set the first open scenario to be active and read its coefficient file
                    newActiveScenario = GetOpenScenario(0);
                    //                    Global.coe.ReadCOE(Global.DIR.COE + scenarios[0].Name);
                }

                SetActiveScenario(newActiveScenario);
            }
        }

        // Called when the user selects one of the listed scenarios at the bottom of the Scenario menu
        public void miScenarioSelected_Click(object sender, EventArgs e)
        {
            // Get which scenario was chosen
            int scenarioSelected = GetScenarioNumberFromName(sender.ToString() + ".coe");
            SetActiveScenario(scenarioSelected);
        }

        // Returns the active scenario
        private int GetActiveScenario()
        {
            for (int i = 0; i < scenarios.Count; i++)
                if (scenarios[i].IsActive > 0)
                    return i;

            // For some reason there is no active scenario
            return -1;
        }

        // Returns the number of the scenario in the master list from the name
        private int GetScenarioNumberFromName(string ScenarioName)
        {
            for (int i = 0; i < scenarios.Count; i++)
                if (ScenarioName.ToLower() == scenarios[i].Name.ToLower())
                    return i;

            return -1;
        }

        // Returns the "Number"th open scenario in the master list
        private int GetOpenScenario(int Number)
        {
            int counter = 0;
            for (int i = 0; i < scenarios.Count; i++)
                if (scenarios[i].IsOpen > 0)
                {
                    if (counter == Number)
                        return i;
                    counter++;
                }
            return -1;
        }

        // Returns the number of scenarios in the master list which are open
        private int GetNumberOfOpenScenarios()
        {
            int numOpen = 0;
            for (int i = 0; i < scenarios.Count; i++)
                if (scenarios[i].IsOpen > 0)
                    numOpen++;

            return numOpen;
        }

        // Changes the active scenario
        private void SetActiveScenario(int NewActiveScenario)
        {
            int oldActiveScenario = GetActiveScenario();
            // If new active scenario is the same as the old, do nothing
            if (oldActiveScenario != NewActiveScenario)
            {
                // Provide the opportunity to save changes to old scenario
                if (oldActiveScenario >= 0 && scenarioChanged)
                {
                    if (MessageBox.Show("Save changes to model coefficients?", "Scenario " + scenarios[oldActiveScenario].Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Global.coe.WriteCOE(Global.DIR.COE + scenarios[oldActiveScenario].Name, -1);
                    scenarioChanged = false;
                }

                if (NewActiveScenario >= 0)
                {
                    string fileName = Global.DIR.COE + scenarios[NewActiveScenario].Name;
                    if (Global.coe.ReadCOE(fileName))
                    {
                        // Mark the new scenario as active
                        scenarios[NewActiveScenario].IsActive = 1;
                        // Find new and old active scenarios in the Scenario menu
                        string newScenario = Path.GetFileNameWithoutExtension(scenarios[NewActiveScenario].Name);
                        string oldScenario = "";
                        if (oldActiveScenario >= 0)
                        {
                            oldScenario = Path.GetFileNameWithoutExtension(scenarios[oldActiveScenario].Name);
                            // Mark the old scenario as no longer active
                            scenarios[oldActiveScenario].IsActive = 0;
                        }
                        // Search through items at the bottom of the Scenario menu, uncheck the old and check the new
                        for (int i = miTopScenario.DropDownItems.Count - GetNumberOfOpenScenarios(); i < miTopScenario.DropDownItems.Count; i++)
                        {
                            if (miTopScenario.DropDownItems[i].ToString() == newScenario)
                                ((ToolStripMenuItem)miTopScenario.DropDownItems[i]).Checked = true;
                            else
                                ((ToolStripMenuItem)miTopScenario.DropDownItems[i]).Checked = false;
                        }
                    }
                    else
                        MessageBox.Show("Unable to read coefficient file " + fileName);
                }
            }

        }

        // Gets the user to choose the name of an existing coefficient file to open
        public static bool OpenExistingCOEFile(ref string FileName)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                InitialDirectory = Global.DIR.COE,
                FileName = "",
                DefaultExt = ".coe",
                Filter = "WARMF Coefficient File (.coe)|*.coe"
            };
            bool result = (openDialog.ShowDialog() == DialogResult.OK);
            if (result)
                FileName = openDialog.FileName;

            return result;
        }
        #endregion

        #region Document Menu Events

        #endregion

        #region Module Menu Events
        private void miData_Click(object sender, EventArgs e)
        {
            ShowForm("data");
        }

        private void miKnowledge_Click(object sender, EventArgs e)
        {
            ShowForm("knowledge");
        }

        private void miManager_Click(object sender, EventArgs e)
        {
            ShowForm("manager");
        }

        private void miTMDL_Click(object sender, EventArgs e)
        {
            ShowForm("tmdl");
        }

        private void miConsensus_Click(object sender, EventArgs e)
        {
            ShowForm("consensus");
        }

        private void miEngineering_Click(object sender, EventArgs e)
        {
            ShowForm("engineering");
        }

        #endregion

        #region Window Menu Events

        #endregion

        #region Help Menu Events
        private void miHelpAbout_Click(object sender, EventArgs e)
        {
            WMDialog popup = new WMDialog("About WARMF", "Watershed Analysis Risk Management Framework\nVersion 7.0\n\nCopyright 2018\nSystech Inc.\nWalnut Creek, CA\nAll rights reserved.", false);
            popup.SetTextColor(System.Drawing.Color.Green);
            popup.ShowDialog();
        }

        #endregion

        #region Tool Strip Buttons
        private void tsbZoomToExtent_Click(object sender, EventArgs e)
        {
            frmMap.ZoomToFullExtent();
            frmMap.ZoomLevel /= 1.05;
        }

        private void tsbClearSelected_Click(object sender, EventArgs e)
        {
            catchShapefile.ClearSelectedRecords();
            riverShapefile.ClearSelectedRecords();
            lakeShapefile.ClearSelectedRecords();
            frmMap.Refresh(true);
        }

        private void tsbZoomIn_Click(object sender, EventArgs e)
        {
            frmMap.ZoomLevel *= 1.25;
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            frmMap.ZoomLevel /= 1.25;
        }

        #endregion

        
    }
}

