using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel.Composition;
using EGIS;
using EGIS.ShapeFileLib;
using DotSpatial.Controls;
using DotSpatial.Symbology;
//using DotSpatial.Topology;
using DotSpatial.Data;

namespace warmf {
    public partial class FormMain : Form
    {
        #region Declarations
        // Versions
        public static int PROJECTFILEVERSION = 2;
        
        // Constants
        public static int LAYERCATCHMENT = 1;
        public static int LAYERRIVER = 2;
        public static int LAYERLAKE = 3;
        public static int LAYERDATA = 4;
        public static int LAYERDEM = 5;
        public static int LAYERLANDUSE = 6;
        public static int LAYERSEPTIC = 7;
        public static int LAYERSOILS = 8;
        public static int LAYERREFERENCE = 9;
        public static string FIELDNAMEWARMFID = "WARMF_ID";
        public static string FIELDNAMENAME = "Name";
        public static string FIELDNAMEFILENAME = "File Name";

        // For DotSpatial and extensions
        [Export("Shell", typeof(ContainerControl))]
        private static ContainerControl shell;

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
        private GeoAPI.Geometries.Coordinate mouseCoordinates;
        public string projectFileName;

        public LayerInfo catchmentLayer;
        public LayerInfo riverLayer;
        public LayerInfo reservoirLayer;
        public List<LayerInfo> layers = new List<LayerInfo>();
        public static List<ScenarioInfo> scenarios = new List<ScenarioInfo>();

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

        // Layers
        public bool projectChanged;
        public struct LayerInfo
        {
            public string Name;
            public string FileName;
            public int Type;
        }
        #endregion

        public FormMain()
        {
            InitializeComponent();

            shell = this;
            appManager.LoadExtensions();

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

            projectChanged = false;
            scenarioChanged = false;
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

        // Formats a catchment layer for the DotSpatial map
        private void FormatCatchmentLayer(int layerNumber)
        {
            MapPolygonLayer catchmentLayerPolygon = mainMap.Layers[layerNumber] as MapPolygonLayer;
            PolygonScheme catchmentLayerScheme = new PolygonScheme();
            PolygonCategory catchmentPolygonCategory = new PolygonCategory(Color.FromArgb(224, 250, 207), Color.FromArgb(178, 178, 178), 1);
            catchmentLayerScheme.AddCategory(catchmentPolygonCategory);
            catchmentLayerPolygon.Symbology = catchmentLayerScheme;
        }

        // Formats a river layer for the DotSpatial map
        private void FormatRiverLayer(int layerNumber)
        {
            MapLineLayer riverLayerLine = (MapLineLayer)this.mainMap.Layers[layerNumber];
            LineScheme riverLayerScheme = new LineScheme();
            LineCategory riverLineCategory = new LineCategory(Color.FromArgb(0, 0, 255), 1);
            riverLayerScheme.AddCategory(riverLineCategory);
            riverLayerLine.Symbology = riverLayerScheme;
        }

        // Formats a lake layer for the DotSpatial map
        private void FormatLakeLayer(int layerNumber)
        {
            MapPolygonLayer reservoirLayerPolygon = (MapPolygonLayer)this.mainMap.Layers[layerNumber];
            PolygonScheme reservoirLayerScheme = new PolygonScheme();
            PolygonCategory reservoirPolygonCategory = new PolygonCategory(Color.FromArgb(0, 0, 255), Color.FromArgb(255, 255, 255), 1);
            reservoirLayerScheme.AddCategory(reservoirPolygonCategory);
            reservoirLayerPolygon.Symbology = reservoirLayerScheme;
        }

        // Formats a data layer (points representing locations with time series data) for the DotSpatial map
        private void FormatDataLayer(int layerNumber)
        {
            MapPointLayer dataLayerPoint = this.mainMap.Layers[layerNumber] as MapPointLayer;
            if (dataLayerPoint != null)
            {
                dataLayerPoint.Projection = mainMap.Projection;
                PointScheme thePointScheme = new PointScheme();
                PointCategory thePointCategory = new PointCategory(Color.FromArgb(0, 0, 0), DotSpatial.Symbology.PointShape.Ellipse, 10);
                thePointScheme.AddCategory(thePointCategory);
                dataLayerPoint.Symbology = thePointScheme;
            }
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

                // DotSpatial map
                // Map Settings

                // Layer Settings
                for (int i = 0; i < layers.Count; i++)
                {
                    this.mainMap.AddLayer(Global.DIR.SHP + layers[i].FileName);
                    // Color schemes hardwired for now
                    if (layers[i].Type == LAYERCATCHMENT)
                    {
                        FormatCatchmentLayer(i);
                    }
                    else if (layers[i].Type == LAYERRIVER)
                    {
                        FormatRiverLayer(i);
                    }
                    else if (layers[i].Type == LAYERLAKE)
                    {
                        FormatLakeLayer(i);
                    }
                    else if (layers[i].Type == LAYERDATA)
                    {
                        FormatDataLayer(i);
                    }
                }
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
            while (attributeNames[n] != FIELDNAMEWARMFID) //test of shapefile attributes
            {
                if (n < (attributeNames.Length - 1))
                    n++;
                else
                {
                    Debug.WriteLine("No WARMF_ID Field found in river attribute table");
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
                            if (attributeNames[j] == FIELDNAMEFILENAME)
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
                while (attributeNames[n] != FIELDNAMEWARMFID) //test of shapefile attributes
                {
                    if (n < (attributeNames.Length - 1))
                        n++;
                    else
                    {
                        Debug.WriteLine("No " + FIELDNAMEWARMFID + "field found in catchments attribute table");
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
                while (attributeNames[n] != FIELDNAMEWARMFID) //test of shapefile attributes
                {
                    if (n < (attributeNames.Length - 1))
                        n++;
                    else
                    {
                        Debug.WriteLine("No " + FIELDNAMEWARMFID + " field found in reservoirs attribute table");
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
                while (attributeNames[n] != FIELDNAMEWARMFID) //test of shapefile attributes
                {
                    if (n < (attributeNames.Length - 1))
                        n++;
                    else
                    {
                        Debug.WriteLine("No " + FIELDNAMEWARMFID + " field found in catchments attribute table");
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
        // Reads the project file
        private void ReadProjectFile(string fileName)
        {
            try
            {
                STechStreamReader sr = new STechStreamReader(fileName);
                bool endOfLine = false;
                // Read where it says "VERSION"
                sr.ReadDelimitedField(',', ref endOfLine);
                int version = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                if (version >= 2)
                {
                    // Read directory structure
                    int numDirectories = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                    for (int i = 0; i < numDirectories; i++)
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
                    layers.Clear();
                    int numLayers = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                    for (int i = 0; i < numLayers; i++)
                    {
                        LayerInfo newLayer = new LayerInfo();
                        newLayer.Type = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                        newLayer.Name = sr.ReadDelimitedField(',', ref endOfLine);
                        newLayer.FileName = sr.ReadDelimitedField(',', ref endOfLine);

                        // Old hardwired layers only used with EasyGIS map
                        if (newLayer.Type == LAYERCATCHMENT)
                            catchmentLayer = newLayer;
                        if (newLayer.Type == LAYERRIVER)
                            riverLayer = newLayer;
                        if (newLayer.Type == LAYERLAKE)
                            reservoirLayer = newLayer;

                        layers.Add(newLayer);
                    }
/*                    catchmentLayer.Name = sr.ReadDelimitedField(',', ref endOfLine);
                    catchmentLayer.FileName = sr.ReadDelimitedField(',', ref endOfLine);
                    catchmentLayer.Type = LAYERCATCHMENT;
                    layers.Add(catchmentLayer);
                    riverLayer.Name = sr.ReadDelimitedField(',', ref endOfLine);
                    riverLayer.FileName = sr.ReadDelimitedField(',', ref endOfLine);
                    riverLayer.Type = LAYERRIVER;
                    layers.Add(riverLayer);
                    reservoirLayer.Name = sr.ReadDelimitedField(',', ref endOfLine);
                    reservoirLayer.FileName = sr.ReadDelimitedField(',', ref endOfLine);
                    reservoirLayer.Type = LAYERLAKE;
                    layers.Add(reservoirLayer);*/
                    LoadCatchmentRiverReservoirShapefiles();

                    // Read reference layer information
/*                    int numReferenceLayers = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                    for (int i = 0; i < numReferenceLayers; i++)
                    {
                        LayerInfo referenceLayer = new LayerInfo
                        {
                            Name = sr.ReadDelimitedField(',', ref endOfLine),
                            FileName = sr.ReadDelimitedField(',', ref endOfLine)
                        };
                        referenceLayer.Type = LAYERREFERENCE;
                        layers.Add(referenceLayer);
                        miTopView.DropDownItems.Add(referenceLayer.Name);
                    }*/

                    // Read the information on scenarios in the project
                    int numScenarios = Convert.ToInt32(sr.ReadDelimitedField(',', ref endOfLine));
                    scenarios.Clear();
                    for (int i = 0; i < numScenarios; i++)
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

                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error : " + ex.Message);
            }
        }

        // Removes the portion of a directory which coincides with the directory passed in to get a directory relative to root
        private string GetRelativePath(string inputPath, string removePath)
        {
            int rootString = inputPath.IndexOf(removePath);
            if (rootString == 0)
            {
                // If the remaining path is just a backslash, return an empty string
                if (inputPath.Length == removePath.Length + 1 && inputPath.EndsWith("\\"))
                    return "";

                // Return portion of inputPath after removePath
                return inputPath.Substring(removePath.Length);
            }

            // removePath not found at the beginning of inputPath
            return inputPath;
        }

        // Writes the project file
        private void WriteProjectFile(string fileName)
        {
            try
            {
                STechStreamWriter wr = new STechStreamWriter(fileName);
                // Write the version number
                wr.WriteLine("VERSION," + Convert.ToString(PROJECTFILEVERSION));

                // Write directory structure
                // Number of directories to be written
                wr.WriteLine("21");
                // Data directory
                wr.WriteLine("DATA," + GetRelativePath(Global.DIR.DATA, Global.DIR.ROOT));
                // Input directory
                wr.WriteLine("INPUT," + GetRelativePath(Global.DIR.INPUT, Global.DIR.DATA));
                // Output directory
                wr.WriteLine("OUTPUT," + GetRelativePath(Global.DIR.OUTPUT, Global.DIR.DATA));
                // Shapefile directory
                wr.WriteLine("SHP," + GetRelativePath(Global.DIR.SHP, Global.DIR.INPUT));
                // Coefficient file directory
                wr.WriteLine("COE," + GetRelativePath(Global.DIR.COE, Global.DIR.INPUT));
                // CE-QUAL-W2 coefficient file directory
                wr.WriteLine("NPT," + GetRelativePath(Global.DIR.NPT, Global.DIR.INPUT));
                // Air/rain chemistry file directory
                wr.WriteLine("AIR," + GetRelativePath(Global.DIR.AIR, Global.DIR.INPUT));
                // Coarse particle air quality file directory
                wr.WriteLine("CPA," + GetRelativePath(Global.DIR.CPA, Global.DIR.INPUT));
                // Managed flow file directory
                wr.WriteLine("FLO," + GetRelativePath(Global.DIR.FLO, Global.DIR.INPUT));
                // Meteorology file directory
                wr.WriteLine("MET," + GetRelativePath(Global.DIR.MET, Global.DIR.INPUT));
                // Observed lake chemistry file directory
                wr.WriteLine("OLC," + GetRelativePath(Global.DIR.OLC, Global.DIR.INPUT));
                // Observed lake hydrology file directory
                wr.WriteLine("OLH," + GetRelativePath(Global.DIR.OLH, Global.DIR.INPUT));
                // Observed river chemistry file directory
                wr.WriteLine("ORC," + GetRelativePath(Global.DIR.ORC, Global.DIR.INPUT));
                // Observed river hydrology file directory
                wr.WriteLine("ORH," + GetRelativePath(Global.DIR.ORH, Global.DIR.INPUT));
                // Point source file directory
                wr.WriteLine("PTS," + GetRelativePath(Global.DIR.PTS, Global.DIR.INPUT));
                // Catchment output file directory
                wr.WriteLine("CAT," + GetRelativePath(Global.DIR.CAT, Global.DIR.OUTPUT));
                // River output file directory
                wr.WriteLine("RIV," + GetRelativePath(Global.DIR.RIV, Global.DIR.OUTPUT));
                // Lake output file directory
                wr.WriteLine("LAK," + GetRelativePath(Global.DIR.LAK, Global.DIR.OUTPUT));
                // Loading output file directory
                wr.WriteLine("PSM," + GetRelativePath(Global.DIR.PSM, Global.DIR.OUTPUT));
                // Diverted flow/water quality file directory
                wr.WriteLine("QWQ," + GetRelativePath(Global.DIR.QWQ, Global.DIR.OUTPUT));
                // Warm start file directory
                wr.WriteLine("WST," + GetRelativePath(Global.DIR.WST, Global.DIR.OUTPUT));

                // Write the layer information
                wr.WriteLine(Convert.ToString(layers.Count));
                for (int i = 0; i < layers.Count; i++)
                    wr.WriteLine(Convert.ToString(layers[i].Type) + "," + layers[i].Name + "," + layers[i].FileName);

                // Write the information on scenarios in the project
                wr.WriteLine(Convert.ToString(scenarios.Count));
                for (int i = 0; i < scenarios.Count; i++)
                    wr.WriteLine(Convert.ToString(scenarios[i].IsOpen) + "," + Convert.ToString(scenarios[i].IsActive) + "," + scenarios[i].Name);

                wr.Close();

                projectChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error : " + ex.Message);
            }
        }

        // Called by File / New in the main map menu
        private void miFileNew_Click(object sender, EventArgs e)
        {
            // Choose the data directory
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.Description = "Select Data Directory";
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {

                Global.DIR.DATA = fbDialog.SelectedPath;
                CloseFile();
                Global.defaultCoefficients.ReadCOE(Global.DIR.ROOT + "default.coe");
                SetupEngrModule();
            }
        }

        private void miFileOpen_Click(object sender, EventArgs e)
        {
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
                projectFileName = openDialog.FileName;
                ReadProjectFile(projectFileName);
                Global.defaultCoefficients.ReadCOE(Global.DIR.ROOT + "default.coe");
                SetupEngrModule();
            }
        }

        // Actually closes a file, checking to see if it should be saved first
        private void CloseFile()
        {
            // Check if the scenario needs saving
            if (scenarioChanged == true)
            {
                DialogResult shouldSaveScenario = MessageBox.Show("Save scenario " + Path.GetFileNameWithoutExtension(GetActiveScenarioName()) + "?", "Close File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                // Cancel means don't close the file
                if (shouldSaveScenario == DialogResult.Cancel)
                    return;
                // Yes means save the scenario
                if (shouldSaveScenario == DialogResult.Yes)
                    SaveScenario();
            }

            // Check if the project needs saving
            if (projectChanged == true)
            {
                DialogResult shouldSaveProject = MessageBox.Show("Save project " + Path.GetFileName(projectFileName) + "?", "Close File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                // Cancel means don't close the file
                if (shouldSaveProject == DialogResult.Cancel)
                    return;
                // Yes means save the project
                if (shouldSaveProject == DialogResult.Yes)
                    WriteProjectFile(projectFileName);
            }

            // Clear the coefficients

            // Clear the map
            mainMap.Layers.Clear();
            layers.Clear();
        }

        private void miFileClose_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        // Called by File / Save in the main map's menu
        private void miFileSave_Click(object sender, EventArgs e)
        {
            WriteProjectFile(projectFileName);
        }

        // Called by File / Save As in the main map's menu
        private void miFileSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                InitialDirectory = Global.DIR.ROOT,
                FileName = "",
                DefaultExt = ".wpf",
                Filter = "WARMF Project File (.wpf)|*.wpf",
                Title = "Save WARMF Project"
            };
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                projectFileName = saveDialog.FileName;
                WriteProjectFile(projectFileName);
            }
        }

        private void miFileExit_Click(object sender, EventArgs e)
        {
            // Close the file, asking to save as needed
            CloseFile();

            System.Windows.Forms.Application.Exit();
        }

        #endregion

        #region Edit Menu Events
        private void SelectLayer(int layerNumber)
        {
            // Feature layer includes point, line, and polygon types
            FeatureLayer theFeatureLayer = mainMap.Layers[layerNumber] as FeatureLayer;
            if (theFeatureLayer != null)
                theFeatureLayer.SelectAll();
        }

        private void miEditSelectCatchments_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Type == LAYERCATCHMENT && i < mainMap.Layers.Count)
                {
                    SelectLayer(i);
                }
            }
            for (int i = 0; i < catchShapefile.RecordCount; i++)
            {
                catchShapefile.SelectRecord(i, true);
            }
            frmMap.Refresh(true);
        }

        private void miEditSelectReservoir_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Type == LAYERLAKE && i < mainMap.Layers.Count)
                {
                    SelectLayer(i);
                }
            }
            for (int i = 0; i < lakeShapefile.RecordCount; i++)
            {
                lakeShapefile.SelectRecord(i, true);
            }
            frmMap.Refresh(true);
        }

        private void miEditSelectRivers_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Type == LAYERRIVER && i < mainMap.Layers.Count)
                {
                    SelectLayer(i);
                }
            }
            for (int i = 0; i < riverShapefile.RecordCount; i++)
            {
                riverShapefile.SelectRecord(i, true);
            }
            frmMap.Refresh(true);
        }

        private void miEditSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < layers.Count; i++)
                SelectLayer(i);

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

        // De-selects all features in all layers on the map
        private void ClearAllSelections()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                FeatureLayer theFeatureLayer = mainMap.Layers[i] as FeatureLayer;
                if (theFeatureLayer != null)
                    theFeatureLayer.ClearSelection();
            }
        }

        private void miEditClearSelectedFeatures_Click(object sender, EventArgs e)
        {
            ClearAllSelections();
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
            mainMap.ZoomIn();
        }

        private void miViewZoomOut_Click(object sender, EventArgs e)
        {
            frmMap.ZoomLevel /= 1.5;
            mainMap.ZoomOut();
        }

        private void miViewZoomToExtent_Click(object sender, EventArgs e)
        {
            frmMap.ZoomToFullExtent();
            frmMap.ZoomLevel /= 1.05;
            mainMap.ZoomToMaxExtent();
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

        // Returns the index of the first instance of a layer name precisely matching the specified name
        private int GetLayerNumberFromName(string layerName)
        {
            for (int i = 0; i < layers.Count; i++)
                if (layers[i].Name == layerName)
                    return i;

            return -1;
        }

        // Returns the index of the first instance of a layer matching the specified type
        private int GetLayerNumberFromType(int layerType)
        {
            for (int i = 0; i < layers.Count; i++)
                if (layers[i].Type == layerType)
                    return i;

            return -1;
        }

        private bool RemoveLayer(int layerNumber)
        {
            if (layerNumber >= 0)
            {
                layers.RemoveAt(layerNumber);
                if (layerNumber < mainMap.Layers.Count)
                {
                    mainMap.Layers.RemoveAt(layerNumber);
                    return true;
                }
            }

            return false;
        }

        private bool RemoveLayer(string layerName)
        {
            return RemoveLayer(GetLayerNumberFromName(layerName));
        }

        private void AddDataLayer(string name, string fileName)
        {
            // Copy the projection file
            if (layers.Count > 0)
            {
                string existingProjectionFile = Path.GetFileNameWithoutExtension(layers[0].FileName) + ".prj";
                string newProjectionFile = Path.GetFileNameWithoutExtension(fileName) + ".prj";
                if (File.Exists(Global.DIR.SHP + existingProjectionFile))
                    File.Copy(Global.DIR.SHP + existingProjectionFile, Global.DIR.SHP + newProjectionFile, true);
            }

            // Add the new layer to the map
            MapPointLayer thePointLayer = (MapPointLayer)mainMap.Layers.Add(Global.DIR.SHP + fileName);
            FormatDataLayer(mainMap.Layers.Count - 1);
            
            // Update record keeping
            LayerInfo newLayerInfo;
            newLayerInfo.Name = name;
            newLayerInfo.FileName = fileName;
            newLayerInfo.Type = LAYERDATA;
            layers.Add(newLayerInfo);
        }

        private void miViewMETStations_Click(object sender, EventArgs e)
        {
            // Remove meteorology stations if they are present
            if (RemoveLayer("Meteorology Stations"))
                return;

            if (STechShapes.CreateShapeFile("MET", Global.DIR.MET, Global.coe.METFilename))
                AddDataLayer("Meteorology Stations", "MET.shp");
        }

        private void miViewGagingStations_Click(object sender, EventArgs e)
        {
            // Remove gaging stations if present on the map
            if (RemoveLayer("Gaging Stations"))
                return;

            if (STechShapes.CreateShapeFile("ORH", Global.DIR.ORH, Global.coe.GetAllObservedHydrologyFiles()))
                AddDataLayer("Gaging Stations", "ORH.shp");
        }

        private void miViewWQStations_Click(object sender, EventArgs e)
        {
            // Remove water quality stations if present on the map
            if (RemoveLayer("Water Quality Stations"))
                return;

            if (STechShapes.CreateShapeFile("ORC", Global.DIR.ORC, Global.coe.GetAllObservedWaterQualityFiles()))
                AddDataLayer("Water Quality Stations", "ORC.shp");
        }

        private void miViewManagedFlow_Click(object sender, EventArgs e)
        {
            // Remove managed flow if present on the map
            if (RemoveLayer("Managed Flow"))
                return;

            if (STechShapes.CreateShapeFile("FLO", Global.DIR.FLO, Global.coe.GetAllManagedFlowFiles()))
                AddDataLayer("Managed Flow", "FLO.shp");
        }

        private void miViewPointSources_Click(object sender, EventArgs e)
        {
            // Remove point sources if present on the map
            if (RemoveLayer("Point Sources"))
                return;

            if (STechShapes.CreateShapeFile("PTS", Global.DIR.PTS, Global.coe.PTSFilename))
                AddDataLayer("Point Sources", "PTS.shp");
        }

        private void miViewAirQualityStations_Click(object sender, EventArgs e)
        {
            // Remove air quality stations if present on the map
            if (RemoveLayer("Air Quality Stations"))
                return;

            if (STechShapes.CreateShapeFile("AIR", Global.DIR.AIR, Global.coe.AIRFilename))
                AddDataLayer("Air Quality Stations", "AIR.shp");
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
        // Actually saves a scenario
        private void SaveScenario()
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
        private void miScenarioSave_Click(object sender, EventArgs e)
        {
            SaveScenario();
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

                scenarioChanged = false;
                projectChanged = true;
            }
        }

        private void miScenarioViewCoeff_Click(object sender, EventArgs e)
        {
            int activeScenario = GetActiveScenario();
            // Launches the default editor for a coefficient file
            if (activeScenario >= 0 && activeScenario < scenarios.Count)
                Process.Start(Global.DIR.COE + scenarios[activeScenario].Name);
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
                projectChanged = true;
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

        // Returns the name of the active scenario
        private string GetActiveScenarioName()
        {
            int theActiveScenario = GetActiveScenario();
            if (theActiveScenario < 0 || theActiveScenario >= scenarios.Count)
                return "";

            return scenarios[theActiveScenario].Name;
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

                projectChanged = true;
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
            mainMap.ZoomToMaxExtent();
        }

        private void tsbClearSelected_Click(object sender, EventArgs e)
        {
            ClearAllSelections();
            catchShapefile.ClearSelectedRecords();
            riverShapefile.ClearSelectedRecords();
            lakeShapefile.ClearSelectedRecords();
            frmMap.Refresh(true);
        }

        private void tsbZoomIn_Click(object sender, EventArgs e)
        {
            frmMap.ZoomLevel *= 1.25;
            mainMap.ZoomIn();
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            frmMap.ZoomLevel /= 1.25;
            mainMap.ZoomOut();
        }


        #endregion

        private void mainMap_Load(object sender, EventArgs e)
        {

        }

        private void tsbSelect_Click(object sender, EventArgs e)
        {
            this.mainMap.FunctionMode = FunctionMode.Select;
        }

        private void tsbPan_Click(object sender, EventArgs e)
        {
            this.mainMap.FunctionMode = FunctionMode.Pan;
        }

        private void tsbPointer_Click(object sender, EventArgs e)
        {
            this.mainMap.FunctionMode = FunctionMode.None;
        }

        private void mainMap_MouseMove(object sender, MouseEventArgs e)
        {
            mouseCoordinates = mainMap.PixelToProj(e.Location); 
            lblLatLong.Text = "Lat/Long: " + mouseCoordinates.Y + ", " + mouseCoordinates.X;
        }

        private void mainMap_MouseHover(object sender, EventArgs e)
        {

            lblLatLong.Text = "Lat/Long: " + mouseCoordinates.Y + ", " + mouseCoordinates.X;
        }

        // Determines the distance between two points assuming lat / long projection
        private double GetCoordinateToCoordinateDistance(GeoAPI.Geometries.Coordinate point1, GeoAPI.Geometries.Coordinate theCoordinate)
        {
            double latitudeDistance = (point1.Y - theCoordinate.Y) * Math.Cos(point1.Y * Math.PI / 180);
            double longitudeDistance = point1.X - theCoordinate.X;
            return Math.Sqrt(Math.Pow(latitudeDistance, 2) + Math.Pow(longitudeDistance, 2));
        }

        // Determines the shortest distance between a point and a line segment
        private double GetPointToLineSegmentDistance(GeoAPI.Geometries.Coordinate theCoordinate, GeoAPI.Geometries.Coordinate lineCoordinate1, GeoAPI.Geometries.Coordinate lineCoordinate2)
        {
            // First check the length to each end of the segment
            double minimumDistance = GetCoordinateToCoordinateDistance(theCoordinate, lineCoordinate1);
            minimumDistance = Math.Min(minimumDistance, GetCoordinateToCoordinateDistance(theCoordinate, lineCoordinate2));

            // Get the shortest distance to the line segment between the points
            // Infinte slope
            if (lineCoordinate1.X == lineCoordinate2.X)
            {
                // Coordinate is within the Y values of the line coordinates
                if ((theCoordinate.Y - lineCoordinate1.Y) * (theCoordinate.Y - lineCoordinate2.Y) <= 0)
                    return Math.Abs(theCoordinate.X - lineCoordinate1.X);
            }
            // Zero slope
            else if (lineCoordinate1.Y == lineCoordinate2.Y)
            {
                // Coordinate is within the X values of the line coordinates
                if ((theCoordinate.X - lineCoordinate1.X) * (theCoordinate.X - lineCoordinate2.X) <= 0)
                    return Math.Abs(theCoordinate.Y - lineCoordinate1.Y);
            }
            // Not horizontal or vertical line
            else
            {
                double lineSlope = (lineCoordinate2.Y - lineCoordinate1.Y) / (lineCoordinate2.X - lineCoordinate1.X);
                double lineIntercept = lineCoordinate1.Y - lineSlope * lineCoordinate1.X;

                // Shortest distance between point and line is along line perpendicular to the line
                double coordinateSlope = -1 / lineSlope;
                double coordinateIntercept = theCoordinate.Y - coordinateSlope * theCoordinate.X;

                // Solve for x at intersection
                double intersectionX = (coordinateIntercept - lineIntercept) / (lineSlope - coordinateSlope);
                
                // Check if the intersection X value is within the line X values
                if ((intersectionX - lineCoordinate1.X) * (intersectionX - lineCoordinate2.X) < 0)
                {
                    double intersectionY = coordinateSlope * intersectionX + coordinateIntercept;
                    return GetCoordinateToCoordinateDistance(new GeoAPI.Geometries.Coordinate(intersectionX, intersectionY), theCoordinate);
                }
            }

            return minimumDistance;
        }

        // Determines the shortest distance between a point and a polyline
        private double GetPointToLineDistance(GeoAPI.Geometries.Coordinate theCoordinate, IList <GeoAPI.Geometries.Coordinate> lineCoordinates)
        {
            double minimumDistance = 999999999999;
            for (int i = 0; i < lineCoordinates.Count - 1; i++)
            {
                minimumDistance = Math.Min(minimumDistance, GetPointToLineSegmentDistance(theCoordinate, lineCoordinates[i], lineCoordinates[i + 1]));
            }
            return minimumDistance;
        }

        // Determines if one number is between two others, inclusive of the end range points
        private bool IsBetween(double testNumber, double range1, double range2)
        {
            return ((testNumber - range1) * (testNumber - range2) <= 0);
        }

        // Determines if there is overlap in values between two sets (1 and 2) of points
        private bool RangesOverlap(double value1A, double value1B, double value2A, double value2B)
        {
            return IsBetween(value1A, value2A, value2B) || IsBetween(value1B, value2A, value2B) ||
                   IsBetween(value2A, value1A, value1B) || IsBetween(value2B, value1A, value1B);
        }

        // Determines if two lines each defined by two points cross each other between the points
        private bool LinesIntersect(GeoAPI.Geometries.Coordinate line1Point1, GeoAPI.Geometries.Coordinate line1Point2, GeoAPI.Geometries.Coordinate line2Point1, GeoAPI.Geometries.Coordinate line2Point2)
        {
            // Check for overlap in x and y values for each line - without overlap in both X and Y they can't intersect
            if (!RangesOverlap(line1Point1.X, line1Point2.X, line2Point1.X, line2Point2.X))
                return false;
            if (!RangesOverlap(line1Point1.Y, line1Point2.Y, line2Point1.Y, line2Point2.Y))
                return false;

            // Check for vertical lines (infinite slope)
            bool line1Vertical = (line1Point1.X == line1Point2.X);
            bool line2Vertical = (line2Point1.X == line2Point2.X);

            // Determine slopes and intercepts
            double slope1 = 9999999;
            double intercept1 = 0;
            if (!line1Vertical)
            {
                slope1 = (line1Point2.Y - line1Point1.Y) / (line1Point2.X - line1Point1.X);
                intercept1 = line1Point1.Y - slope1 * line1Point1.X;
            }
            double slope2 = 9999999;
            double intercept2 = 0;
            if (!line2Vertical)
            {
                slope2 = (line2Point2.Y - line2Point1.Y) / (line2Point2.X - line2Point1.X);
                intercept2 = line2Point1.Y - slope2 * line2Point1.X;
            }

            // Both lines are vertical and we already know their values overlap
            if (line1Vertical && line2Vertical)
                return true;

            // Line 1 is vertical but line 2 is not
            if (line1Vertical)
            {
                // Find the y-value for line 2 at line 1's constant X value
                double yValue = slope2 * line1Point1.X + intercept2;

                // Lines intersect if line 2 y value is in the range of line 1 y value
                return IsBetween(yValue, line1Point1.Y, line1Point2.Y);
            }

            // Line 2 is vertical but line 2 is not
            if (line2Vertical)
            {
                // Find the y-value for line 2 at line 1's constant X value
                double yValue = slope1 * line2Point1.X + intercept1;

                // Lines intersect if line 2 y value is in the range of line 1 y value
                return IsBetween(yValue, line2Point1.Y, line2Point2.Y);
            }

            // Parallel
            if (slope1 == slope2)
                return (intercept1 == intercept2);

            // Two non-vertical, non-parallel lines: solve for x at intersection
            double intersectionX = (intercept2 - intercept1) / (slope1 - slope2);

            // Lines intersect if intersection is within the endpoints of both of the lines
            return (IsBetween(intersectionX, line1Point1.X, line1Point2.X) && IsBetween(intersectionX, line2Point1.X, line2Point2.X));
        }

        // Checks to see if a point is within a polygon using robust ray crossing method
        // Polygon vertices are in a single array of the form (x1, y1, x2, y2, x3, y3, ...)
        private bool CoordinateInPolygon(GeoAPI.Geometries.Coordinate theCoordinate, double[] polygonVertices)
        {
            // Polygon has no vertices(
            if (polygonVertices.Length < 2)
                return false;

            // Get bounding box of polygon
            double minX = polygonVertices[0];
            double maxX = polygonVertices[0];
            double minY = polygonVertices[1];
            double maxY = polygonVertices[1];
            for (int i = 2; i < polygonVertices.Length; i += 2)
            {
                minX = Math.Min(minX, polygonVertices[i]);
                maxX = Math.Max(maxX, polygonVertices[i]);
                minY = Math.Min(minY, polygonVertices[i + 1]);
                maxY = Math.Max(maxY, polygonVertices[i + 1]);
            }

            // Get coordinate outside the bounding box
            GeoAPI.Geometries.Coordinate outsideCoordinate = new GeoAPI.Geometries.Coordinate(2 * maxX - minX, 2.001 * maxY - minY);

            // Check how many times a line between the coordinates crosses lines between polygon vertices
            int numCrosses = 0;
            for (int i = 0; i < polygonVertices.Length; i += 2)
            {
                GeoAPI.Geometries.Coordinate polygonCoordinate1 = new GeoAPI.Geometries.Coordinate(polygonVertices[i], polygonVertices[i + 1]);

                // Second point is the previous point unless this is the first point, in which case the second point is the last vertex
                GeoAPI.Geometries.Coordinate polygonCoordinate2 = new GeoAPI.Geometries.Coordinate();
                if (i >= 2)
                {
                    polygonCoordinate2.X = polygonVertices[i - 2];
                    polygonCoordinate2.Y = polygonVertices[i - 1];
                }
                else
                {
                    polygonCoordinate2.X = polygonVertices[polygonVertices.Length - 2];
                    polygonCoordinate2.Y = polygonVertices[polygonVertices.Length - 1];
                }

                if (LinesIntersect(theCoordinate, outsideCoordinate, polygonCoordinate1, polygonCoordinate2) == true)
                    numCrosses++;
            }

            // Odd number of crosses means the point is in the polygon
            return (numCrosses % 2 == 1);
        }
        private bool CoordinateInPolygon(GeoAPI.Geometries.Coordinate theCoordinate, IList<GeoAPI.Geometries.Coordinate> polygonVertices)
        {
            // Polygon has no vertices
            if (polygonVertices.Count < 2)
                return false;
            
            // Get bounding box of polygon
            double minX = polygonVertices[0].X;
            double maxX = polygonVertices[0].X;
            double minY = polygonVertices[0].Y;
            double maxY = polygonVertices[0].Y;
            for (int i = 1; i < polygonVertices.Count; i++)
            {
                minX = Math.Min(minX, polygonVertices[i].X);
                maxX = Math.Max(maxX, polygonVertices[i].X);
                minY = Math.Min(minY, polygonVertices[i].Y);
                maxY = Math.Max(maxY, polygonVertices[i].Y);
            }

            // Coordinate outside bounding box
            if (!IsBetween(theCoordinate.X, minX, maxX) || !IsBetween(theCoordinate.Y, minY, maxY))
                return false;

            // Get a coordinate definitely outside the bounding box
            GeoAPI.Geometries.Coordinate outsideCoordinate = new GeoAPI.Geometries.Coordinate(2 * maxX - minX, 2.001 * maxY - minY);

            // Check how many times a line between the coordinates crosses lines between polygon vertices
            int numCrosses = 0;
            for (int i = 0; i < polygonVertices.Count; i++)
            {
                GeoAPI.Geometries.Coordinate polygonCoordinate1 = new GeoAPI.Geometries.Coordinate(polygonVertices[i].X, polygonVertices[i].Y);

                // Second point is the previous point unless this is the first point, in which case the second point is the last vertex
                GeoAPI.Geometries.Coordinate polygonCoordinate2 = new GeoAPI.Geometries.Coordinate();
                if (i >= 1)
                {
                    polygonCoordinate2.X = polygonVertices[i - 1].X;
                    polygonCoordinate2.Y = polygonVertices[i - 1].Y;
                }
                else
                {
                    polygonCoordinate2.X = polygonVertices[polygonVertices.Count - 1].X;
                    polygonCoordinate2.Y = polygonVertices[polygonVertices.Count - 1].Y;
                }

                if (LinesIntersect(theCoordinate, outsideCoordinate, polygonCoordinate1, polygonCoordinate2))
                    numCrosses++;
            }

            // Odd number of crosses means the point is in the polygon
            return (numCrosses % 2 == 1);
        }

        private bool GetShapeAndLayerFromCoordinates(GeoAPI.Geometries.Coordinate theCoordinates, ref int layerNumber, ref int featureNumber)
        {
            // Check layers from front to back to find the first which has been clicked upon
            for (layerNumber = mainMap.Layers.Count - 1; layerNumber >= 0; layerNumber--)
            {
                // Check if the layer is polygons
                MapPolygonLayer thePolygonLayer = mainMap.Layers[layerNumber] as MapPolygonLayer;
                if (thePolygonLayer != null)
                {
                    DotSpatial.Data.IFeatureSet theFeatureSet = thePolygonLayer.FeatureSet;
                    for (featureNumber = 0; featureNumber < theFeatureSet.Features.Count; featureNumber++)
                    {
                        GeoAPI.Geometries.IPolygon thePolygon = theFeatureSet.Features[featureNumber].Geometry as GeoAPI.Geometries.IPolygon;
                        if (thePolygon != null)
                        {
                            if (CoordinateInPolygon(new GeoAPI.Geometries.Coordinate(theCoordinates.X, theCoordinates.Y), thePolygon.Coordinates) == true)
                                return true;
//                            if (thePolygon.Contains(new DotSpatial.Topology.Point(theCoordinates)))
//                                return true;
                        }
                        else
                        {
                            // Check for multi-polygon
                            GeoAPI.Geometries.IMultiPolygon theMultiPolygon = theFeatureSet.Features[featureNumber].Geometry as GeoAPI.Geometries.IMultiPolygon;
                            if (theMultiPolygon != null)
                            {
                                for (int i = 0; i < theMultiPolygon.Geometries.Length; i++)
                                {
                                    GeoAPI.Geometries.IPolygon thePolygonPiece = theMultiPolygon.Geometries[i] as GeoAPI.Geometries.IPolygon;
                                    if (thePolygonPiece != null)
                                    {
                                        if (CoordinateInPolygon(new GeoAPI.Geometries.Coordinate(theCoordinates.X, theCoordinates.Y), thePolygonPiece.Coordinates) == true)
                                            return true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // If the layer isn't polygons (lines or points), figure out whether it is selected by
                    // creating a circular polygon around the double-click coordinate and finding the intersection with a layer

                    // Get the longitude distance for 2 pixels
                    System.Drawing.Point pixelCoordinates = mainMap.ProjToPixel(theCoordinates);
                    pixelCoordinates.Y += 2;
                    double latitudeRadius = Math.Abs(mainMap.PixelToProj(pixelCoordinates).Y - theCoordinates.Y);

                    // Check if the layer is of point type, and check if a point in the layer is within the double-click circle.
                    MapPointLayer thePointLayer = mainMap.Layers[layerNumber] as MapPointLayer;
                    if (thePointLayer != null)
                    {
                        DotSpatial.Data.IFeatureList theFeatureList = thePointLayer.DataSet.Features;
                        for (featureNumber = 0; featureNumber < theFeatureList.Count; featureNumber++)
                        {
                            GeoAPI.Geometries.IPoint thePoint = theFeatureList[featureNumber].Geometry as GeoAPI.Geometries.IPoint;
                            // Determine if the distance between the feature point and the mouse double-click point is less than the radius
                            if (GetCoordinateToCoordinateDistance(new GeoAPI.Geometries.Coordinate(thePoint.X, thePoint.Y), theCoordinates) <= latitudeRadius)
                                return true;
                        }
                    }

                    // Check if the layer is of line type, and check if a line is within the radius
                    MapLineLayer theLineLayer = mainMap.Layers[layerNumber] as MapLineLayer;
                    if (theLineLayer != null)
                    {
                        DotSpatial.Data.IFeatureSet theFeatureSet = theLineLayer.FeatureSet;
                        for (featureNumber = 0; featureNumber < theFeatureSet.Features.Count; featureNumber++)
                        {

                            if (GetPointToLineDistance(theCoordinates, theFeatureSet.Features[featureNumber].Geometry.Coordinates) < latitudeRadius)
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        // Searches for a given string in the field names and returns a string for the corresponding field
        private string GetFieldFromAttributes(string targetName, string [] fieldNames, string [] theFields)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                if (fieldNames[i] == targetName && i < theFields.Length)
                    return theFields[i];

            return "";
        }

        // Searches for the WARMF_ID field and returns the ID number corresponding to that field number
        private int GetWARMF_IDFromAttributes(string[] fieldNames, string [] theFields)
        {
            string idField = GetFieldFromAttributes(FIELDNAMEWARMFID, fieldNames, theFields);
            if (idField.Length > 0)
                return Convert.ToInt32(idField);

            return -1;
        }

        private void mainMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int layerNumber = -1;
            int featureNumber = -1;
            mouseCoordinates = mainMap.PixelToProj(e.Location);
            if (GetShapeAndLayerFromCoordinates(mouseCoordinates, ref layerNumber, ref featureNumber))
            {
                if (layerNumber < layers.Count)
                {
                    // Get the fields from the shapefile
                    ShapeFile theShapeFile = new ShapeFile(Global.DIR.SHP + layers[layerNumber].FileName);
                    string[] fieldNames = theShapeFile.GetAttributeFieldNames();
                    string[] theFields = theShapeFile.GetAttributeFieldValues(featureNumber);

                    // Catchment
                    if (layers[layerNumber].Type == LAYERCATCHMENT)
                    {
                        int warmfID = GetWARMF_IDFromAttributes(fieldNames, theFields);

                        if (warmfID < 0)
                        {
                            Debug.WriteLine("No " + FIELDNAMEWARMFID + " field found in catchment attribute table");
                            return;
                        }

                        int catchIndex = Global.coe.GetCatchmentNumberFromID(warmfID);
                        if (catchIndex < 0)
                        {
                            Debug.WriteLine("No catchment found with IDnum = " + warmfID);
                            return;
                        }
                        if (miModeInput.BackColor == System.Drawing.SystemColors.Highlight)
                        {
                            // Select the catchment
                            MapPolygonLayer thePolygonLayer = mainMap.Layers[layerNumber] as MapPolygonLayer;
                            if (thePolygonLayer != null && featureNumber < thePolygonLayer.FeatureSet.Features.Count)
                                thePolygonLayer.Select(featureNumber);

                            using (dlgCatchCoeffs = new DialogCatchCoeffs(this))
                            {
                                // Open the catchment dialog
                                dlgCatchCoeffs.Populate(catchIndex);
                                if (dlgCatchCoeffs.ShowDialog() == DialogResult.OK)
                                    scenarioChanged = true;
                            }

                            // Clear selection
                            if (thePolygonLayer != null)
                                thePolygonLayer.ClearSelection();
                        }
                        else if (miModeOutput.BackColor == System.Drawing.SystemColors.Highlight)
                        {
                            using (dlgOutput = new DialogOutput(this))
                            {
                                dlgOutput.Populate("Catchment", catchIndex);
                                dlgOutput.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gowdy or Herr Output Selected");
                        }
                    }

                    // River
                    if (layers[layerNumber].Type == LAYERRIVER)
                    {
                        int warmfID = GetWARMF_IDFromAttributes(fieldNames, theFields);

                        if (warmfID < 0)
                        {
                            Debug.WriteLine("No " + FIELDNAMEWARMFID + " field found in river attribute table");
                            return;
                        }

                        int riverIndex = Global.coe.GetRiverNumberFromID(warmfID);
                        if (riverIndex < 0)
                        {
                            Debug.WriteLine("No river found with IDnum = " + warmfID);
                            return;
                        }
                        if (miModeInput.BackColor == System.Drawing.SystemColors.Highlight)
                        {
                            // Select the river
                            MapLineLayer theLineLayer = mainMap.Layers[layerNumber] as MapLineLayer;
                            if (theLineLayer != null && featureNumber < theLineLayer.FeatureSet.Features.Count)
                                theLineLayer.Select(featureNumber);

                            using (dlgRiverCoeffs = new DialogRiverCoeffs(this))
                            {
                                dlgRiverCoeffs.Populate(riverIndex);
                                if (dlgRiverCoeffs.ShowDialog() == DialogResult.OK)
                                    scenarioChanged = true;
                            }

                            // Clear selection
                            if (theLineLayer != null)
                                theLineLayer.ClearSelection();
                        }
                        else if (miModeOutput.BackColor == System.Drawing.SystemColors.Highlight)
                        {
                            using (dlgOutput = new DialogOutput(this))
                            {
                                dlgOutput.Populate("River", riverIndex);
                                dlgOutput.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gowdy or Herr Output Selected");
                        }
                    }

                    // Reservoir / Lake
                    if (layers[layerNumber].Type == LAYERLAKE)
                    {
                        int segmentID = GetWARMF_IDFromAttributes(fieldNames, theFields);

                        if (segmentID < 0)
                        {
                            Debug.WriteLine("No " + FIELDNAMEWARMFID + " field found in lake attribute table");
                            return;
                        }

                        List<int> reservoirAndSegmentIndices = Global.coe.GetReservoirAndSegmentNumberFromID(segmentID);
                        if (reservoirAndSegmentIndices.Count < 2 || reservoirAndSegmentIndices[0] < 0 || reservoirAndSegmentIndices[1] < 0)
                        {
                            Debug.WriteLine("No reservoir segment found with IDnum = " + segmentID);
                            return;
                        }
                        if (miModeInput.BackColor == System.Drawing.SystemColors.Highlight)
                        {
                            // Select the reservoir segment
                            MapPolygonLayer thePolygonLayer = mainMap.Layers[layerNumber] as MapPolygonLayer;
                            if (thePolygonLayer != null && featureNumber < thePolygonLayer.FeatureSet.Features.Count)
                                thePolygonLayer.Select(featureNumber);

                            using (dlgReservoirCoeffs = new DialogReservoirCoeffs(this))
                            {
                                dlgReservoirCoeffs.Populate(reservoirAndSegmentIndices[0], reservoirAndSegmentIndices[1]);
                                if (dlgReservoirCoeffs.ShowDialog() == DialogResult.OK)
                                    scenarioChanged = true;
                            }

                            // Clear selection
                            if (thePolygonLayer != null)
                                thePolygonLayer.ClearSelection();
                        }
                        else if (miModeOutput.BackColor == System.Drawing.SystemColors.Highlight)
                        {
                            //need to populate the dialog - but is it the same dialog as is used for catchments and rivers?
                            using (dlgOutput = new DialogOutput(this))
                            {
                                dlgOutput.Populate("Lake", segmentID);
                                dlgOutput.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gowdy or Herr Output Selected");
                        }
                    }

                    if (layers[layerNumber].Type == LAYERDATA)
                    {
                        string dataFileName = GetFieldFromAttributes(FIELDNAMEFILENAME, fieldNames, theFields).Trim();

                        // DotSpatial way of getting fields
                        MapPointLayer thePointLayer = mainMap.Layers[layerNumber] as MapPointLayer;
                        if (thePointLayer != null)
                        {
                            DotSpatial.Data.IFeatureSet theFeatureSet = thePointLayer.FeatureSet;
                            dataFileName = theFeatureSet.Features[featureNumber].DataRow[1].ToString();
                        }
                        string extension = Path.GetExtension(dataFileName);
                        if (extension.IndexOf("MET", StringComparison.CurrentCultureIgnoreCase) >= 0)
                            frmData.cboxTypeOfFile.SelectedIndex = 0;
                        else if (extension.IndexOf("AIR", StringComparison.CurrentCultureIgnoreCase) >= 0)
                            frmData.cboxTypeOfFile.SelectedIndex = 1;
                        else if (extension.IndexOf("ORH", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                 extension.IndexOf("OLH", StringComparison.CurrentCultureIgnoreCase) >= 0)
                            frmData.cboxTypeOfFile.SelectedIndex = 2;
                        else if (extension.IndexOf("ORC", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                 extension.IndexOf("OLC", StringComparison.CurrentCultureIgnoreCase) >= 0)
                            frmData.cboxTypeOfFile.SelectedIndex = 3;
                        else if (extension.IndexOf("FLO", StringComparison.CurrentCultureIgnoreCase) >= 0)
                            frmData.cboxTypeOfFile.SelectedIndex = 4;
                        else if (extension.IndexOf("PTS", StringComparison.CurrentCultureIgnoreCase) >= 0)
                            frmData.cboxTypeOfFile.SelectedIndex = 5;
                        else
                        {
                            MessageBox.Show("File extension does not match any of the recognized WARMF file extensions", "Exception/Error", MessageBoxButtons.OK);
                            return;
                        }
                        frmData.cboxFilename.SelectedIndex =
                            frmData.cboxFilename.FindString(dataFileName);
                        frmData.cboxData.SelectedIndex = 0;
                        frmData.Show();
                    }
                }
            }
            // System coefficients dialog or spatial output
            else
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

            }
        }

        public List<int> GetFieldLinkages(List<string> warmfFields, string [] shapefileFields)
        {
            // Initialize the linkages to 
            List<int> theLinkages = new List<int>();
            for (int i = 0; i < warmfFields.Count; i++)
                theLinkages.Add(-1);

            DialogGridView theDialog = new DialogGridView();
            theDialog.Text = "Select Shapefile Fields for WARMF Parameters";
            theDialog.Populate(warmfFields, shapefileFields);
            if (theDialog.ShowDialog() == DialogResult.OK)
            {

            }
                return theLinkages;
        }

        private void layersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogEditLayers layersDialog = new DialogEditLayers(ref layers);

            if (layersDialog.ShowDialog() == DialogResult.OK)
            {
                int originalNumLayers = layers.Count;
                List<int> foundLayers = new List<int>();
                // Add new layers to the master
                for (int i = 0; i < layersDialog.changedLayers.Count; i++)
                {
                    int currentLayerNumber = GetLayerNumberFromName(layersDialog.changedLayers[i].Name);
                    if (currentLayerNumber >= 0)
                        foundLayers.Add(currentLayerNumber);
                    else
                    {
                        string newFileName = Global.DIR.SHP + Path.GetFileName(layersDialog.changedLayers[i].FileName);
                        //var grp = new DotSpatial.Data.Rasters.GdalExtension.GdalRasterProvider();

                        try
                        {
                            // Add the new layer to the map
                            //                            mainMap.AddLayer(Global.DIR.SHP + newFileName);
                            // Color schemes hardwired for now
                            if (layersDialog.changedLayers[i].Type == LAYERCATCHMENT)
                            {
                                mainMap.AddLayer(newFileName);
                                FormatCatchmentLayer(mainMap.Layers.Count - 1);

                                // Give default model coefficients to each catchment
                                // Get the fields from the shapefile
                                ShapeFile theShapeFile = new ShapeFile(newFileName);
                                string[] fieldNames = theShapeFile.GetAttributeFieldNames();
                                // Get the ID, slope, name, and aspect fields
                                List<string> catchmentFields = new List<string>();
                                catchmentFields.Add("WARMF_ID");
                                catchmentFields.Add("Area");
                                catchmentFields.Add("Slope");
                                catchmentFields.Add("Aspect");
                                List<int> fieldNumbers = GetFieldLinkages(catchmentFields, theShapeFile.GetAttributeFieldNames());
                                FeatureLayer theFeatureLayer = mainMap.Layers[mainMap.Layers.Count - 1] as FeatureLayer;
                                if (theFeatureLayer != null && Global.defaultCoefficients.catchments.Count > 0)
                                {
                                    int highestID = Global.coe.GetHighestCatchmentID();
                                    DotSpatial.Data.IFeatureSet theFeatureSet = theFeatureLayer.FeatureSet;
                                    for (int featureNumber = 0; featureNumber < theFeatureSet.Features.Count; featureNumber++)
                                    {
                                        Catchment newCatchment = new Catchment();
                                        newCatchment = Global.defaultCoefficients.catchments[0];
                                        newCatchment.idNum = highestID + featureNumber;
                                    }
                                }
                            }
                            else if (layersDialog.changedLayers[i].Type == LAYERRIVER)
                            {
                                mainMap.AddLayer(newFileName);
                                FormatRiverLayer(mainMap.Layers.Count - 1);
                            }
                            else if (layersDialog.changedLayers[i].Type == LAYERLAKE)
                            {
                                mainMap.AddLayer(newFileName);
                                FormatLakeLayer(mainMap.Layers.Count - 1);
                            }
                            else if (layersDialog.changedLayers[i].Type == LAYERDATA)
                            {
                                MapPointLayer thePointLayer = mainMap.AddLayer(newFileName) as MapPointLayer;
                                FormatDataLayer(mainMap.Layers.Count - 1);
                            }
                            else if (layersDialog.changedLayers[i].Type == LAYERDEM)
                            {
//                                MapImageLayer theImageLayer = mainMap.AddLayer(newFileName) as MapImageLayer;
//                                if (theImageLayer != null)
                                {
                                    // Reproject the layer
//                                    theImageLayer.Reproject(mainMap.Projection);
//                                    mainMap.Layers.Move(mainMap.Layers[mainMap.Layers.Count - 1], 0);

                                    // Overlay image points with catchment boundaries
                                }
                                //                                IRaster theRaster = Raster.Open(Global.DIR.SHP + layersDialog.changedLayers[i].FileName);
                                //                                IRasterLayer theRasterLayer = mainMap.Layers.Add(theRaster);
                                //IRaster theRaster = grp.Open(Global.DIR.SHP + layersDialog.changedLayers[i].FileName);
                                //IRasterLayer theRasterLayer = mainMap.AddLayer(Global.DIR.SHP + layersDialog.changedLayers[i].FileName) as IRasterLayer;
                                                                //mainMap.AddLayer(Global.DIR.SHP + newFileName);
                                                                mainMap.Layers.Move(mainMap.Layers[mainMap.Layers.Count - 1], 0);
                                //                                IMapRasterLayer rasterLayer = mainMap.Layers[0] as IMapRasterLayer;
                                /*                                if (theRasterLayer != null)
                                                                {
                                                                    theRasterLayer.Projection = mainMap.Projection;
                                                                    // Overlay DEM with catchments to calculate slope and aspect

                                                                    // Set up color scheme for display
                                                                }*/
                            }
                            else if (layersDialog.changedLayers[i].Type == LAYERLANDUSE)
                            {
//                                MapImageLayer theImageLayer = mainMap.AddLayer(newFileName) as MapImageLayer;
                                //                                MapImageLayer theImageLayer = MapImageLayer.OpenFile(newFileName) as MapImageLayer;
                                //                                mainMap.AddLayer(newFileName);
//                                if (theImageLayer != null)
                                {
                                    // Get the catchment layer
//                                    int catchmentLayerNumber = GetLayerNumberFromType(LAYERCATCHMENT);
//                                    if (catchmentLayerNumber >= 0 && catchmentLayerNumber < mainMap.Layers.Count)
                                    {
                                        // Copy the catchment layer and project it
                                        /*                                        MapPolygonLayer projectedCatchmentLayer = new MapPolygonLayer((mainMap.Layers[catchmentLayerNumber] as MapPolygonLayer).FeatureSet);
                                                                                if (projectedCatchmentLayer != null)
                                                                                {
                                                                                    projectedCatchmentLayer.Reproject(theImageLayer.Projection);
                                        //                                            mainMap.Layers.Add(projectedCatchmentLayer);
                                                                                    // Check each point in the image file to see if it is within a catchment
                                                                                    for (int columnNumber = 0; columnNumber < theImageLayer.DataSet.Bounds.X; columnNumber++)
                                                                                    {
                                                                                        for (int rowNumber = 0; rowNumber < theImageLayer.DataSet.Bounds.Y; rowNumber++)
                                                                                        {

                                                                                        }
                                                                                    }
                                                                                }*/
                                    }

                                    // Convert the map and other layers to this projection
//                                    for (int layerCount = 0; layerCount < mainMap.Layers.Count - 1; layerCount++)
                                        //    mainMap.Layers[layerCount].Reproject(mainMap.Layers[mainMap.Layers.Count - 1].Projection);
//                                        mainMap.Layers[layerCount].Reproject(theImageLayer.Projection);
                                    //                                    mainMap.Projection = mainMap.Layers[mainMap.Layers.Count - 1].Projection;
//                                    mainMap.Projection = theImageLayer.Projection;
                                    //                                    mainMap.Layers.RemoveAt(mainMap.Layers.Count - 1);

                                    /*                                    Raster theRaster = new Raster();
                                                                        theRaster.Filename = newFileName;
                                                                        theRaster.Open();
                                                                        RasterLayer theRasterLayer = new RasterLayer(theRaster);
                                                                        if (theRasterLayer != null)
                                                                        {
                                                                            Coordinate bottomLeft = theRasterLayer.DataSet.Bounds.CellCenter_ToProj(0, 0);
                                                                        }*/
                                    //                                    mainMap.Layers.Move(mainMap.Layers[mainMap.Layers.Count - 1], 0);

                                    /*                                    for (int columnCount = 0; columnCount < theImageLayer.DataSet.Bounds.NumColumns; columnCount++)
                                                                        {
                                                                            for (int rowCount = 0; rowCount < theImageLayer.DataSet.Bounds.NumRows; rowCount++)
                                                                            {
                                                                                Coordinate theCoordinate = 
                                                                            }
                                                                        }

                                                                        // Reproject the layer
                                                                        theImageLayer.Reproject(mainMap.Projection);*/
  //                                  mainMap.Layers.Move(mainMap.Layers[mainMap.Layers.Count - 1], 0);
                                    //                                    mainMap.Layers.RemoveAt(mainMap.Layers.Count - 1);

                                    // Overlay image points with catchment boundaries
                                }
                                /*                                IRaster theRaster = Raster.Open(Global.DIR.SHP + layersDialog.changedLayers[i].FileName);
                                                                DotSpatial.Projections.ProjectionInfo dest = default(DotSpatial.Projections.ProjectionInfo);
                                                                //                                dest = DotSpatial.Projections.ProjectionInfo.FromEpsgCode(4326);
                                                                dest = mainMap.Projection;
                                                                theRaster.Projection = dest;
                                                                IMapRasterLayer myLayer = mainMap.Layers.Add(theRaster);*/
                                //                                IMapRasterLayer rasterLayer = mainMap.Layers[0] as IMapRasterLayer;
                                //                                if (rasterLayer != null)
                                {
                                    //                                    rasterLayer.Projection = mainMap.Projection;
                                    // Overlay land use with catchments to calculate percent of each land use in each catchment

                                    // Set up color scheme for display
                                }
                            }
                            else if (layersDialog.changedLayers[i].Type == LAYERREFERENCE)
                            {
                                mainMap.AddLayer(newFileName);
                            }

                            // Update record keeping
                            layers.Add(layersDialog.changedLayers[i]);
                            projectChanged = true;
                        }
                        catch
                        {
                            MessageBox.Show("Unable to read spatial data file: " + newFileName, "Data File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

                // Remove layers from the master list
                for (int i = 0; i < originalNumLayers; i++)
                    if (foundLayers.IndexOf(i) < 0)
                        RemoveLayer(i);
            }
        }
    }
}

