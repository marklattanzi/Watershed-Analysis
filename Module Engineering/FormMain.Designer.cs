namespace warmf {
	partial class FormMain {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.frmMap = new EGIS.Controls.SFMap();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.miTopFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.miFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.miFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.miFilePrinterSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.miFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miTopEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditSelectCatchments = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditSelectReservoir = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditSelectRivers = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditClearSelectedFeatures = new System.Windows.Forms.ToolStripMenuItem();
            this.miTopView = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewZoomToExtent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.miViewSelectableLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewSelectableCatchments = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewSelectableRivers = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewSelectableLakes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miViewTribConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewEntityIDs = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewEntityPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewSubwatersheds = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewMETStations = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewGagingStations = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewWQStations = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewManagedFlow = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewPointSources = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewAirQualityStations = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewScale = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewPictures = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.miTopMode = new System.Windows.Forms.ToolStripMenuItem();
            this.miModeInput = new System.Windows.Forms.ToolStripMenuItem();
            this.miModeOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.miModeFluxOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.miModeLongGowdyOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.miTopScenario = new System.Windows.Forms.ToolStripMenuItem();
            this.miScenarioRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.miScenarioManager = new System.Windows.Forms.ToolStripMenuItem();
            this.miScenarioSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miScenarioSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.miScenarioDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miScenarioExport = new System.Windows.Forms.ToolStripMenuItem();
            this.miScenarioImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.miScenarioViewCoeff = new System.Windows.Forms.ToolStripMenuItem();
            this.miScenarioCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.miScenarioFileCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.miTopDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.miDocumentModelLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miDocumentNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.miModule = new System.Windows.Forms.ToolStripMenuItem();
            this.miData = new System.Windows.Forms.ToolStripMenuItem();
            this.miKnowledge = new System.Windows.Forms.ToolStripMenuItem();
            this.miTMDL = new System.Windows.Forms.ToolStripMenuItem();
            this.miConsensus = new System.Windows.Forms.ToolStripMenuItem();
            this.miManager = new System.Windows.Forms.ToolStripMenuItem();
            this.miTopWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelpContents = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pboxSplash = new System.Windows.Forms.PictureBox();
            this.lblLatLong = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomToExtent = new System.Windows.Forms.ToolStripButton();
            this.tsbClearSelected = new System.Windows.Forms.ToolStripButton();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxSplash)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlgFileOpen
            // 
            this.dlgFileOpen.FileName = "openFileDialog1";
            // 
            // frmMap
            // 
            this.frmMap.AutoScrollMargin = new System.Drawing.Size(20, 20);
            this.frmMap.AutoSize = true;
            this.frmMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frmMap.CentrePoint2D = ((EGIS.ShapeFileLib.PointD)(resources.GetObject("frmMap.CentrePoint2D")));
            this.frmMap.Location = new System.Drawing.Point(15, 61);
            this.frmMap.MapBackColor = System.Drawing.SystemColors.Control;
            this.frmMap.Margin = new System.Windows.Forms.Padding(6);
            this.frmMap.Name = "frmMap";
            this.frmMap.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
            this.frmMap.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
            this.frmMap.Size = new System.Drawing.Size(930, 546);
            this.frmMap.TabIndex = 0;
            this.frmMap.UseMercatorProjection = false;
            this.frmMap.ZoomLevel = 1D;
            this.frmMap.ZoomToSelectedExtentWhenCtrlKeydown = false;
            this.frmMap.MapDoubleClick += new System.EventHandler<EGIS.Controls.SFMap.MapDoubleClickedEventArgs>(this.frmMap_MapDoubleClick);
            this.frmMap.Load += new System.EventHandler(this.frmMap_Load);
            this.frmMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.frmMap_MouseDoubleClick);
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTopFile,
            this.miTopEdit,
            this.miTopView,
            this.miTopMode,
            this.miTopScenario,
            this.miTopDocument,
            this.miModule,
            this.miTopWindow,
            this.miFileHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.mnuMain.Size = new System.Drawing.Size(960, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // miTopFile
            // 
            this.miTopFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFileNew,
            this.miFileOpen,
            this.miFileClose,
            this.miFileImport,
            this.miFileExport,
            this.miFileSave,
            this.miFileSaveAs,
            this.miFileSep1,
            this.miFilePrint,
            this.miFilePrintPreview,
            this.miFilePrinterSetup,
            this.toolStripSeparator3,
            this.miFileExit});
            this.miTopFile.Name = "miTopFile";
            this.miTopFile.Size = new System.Drawing.Size(37, 22);
            this.miTopFile.Text = "&File";
            // 
            // miFileNew
            // 
            this.miFileNew.Name = "miFileNew";
            this.miFileNew.Size = new System.Drawing.Size(152, 22);
            this.miFileNew.Text = "&New";
            // 
            // miFileOpen
            // 
            this.miFileOpen.Name = "miFileOpen";
            this.miFileOpen.Size = new System.Drawing.Size(152, 22);
            this.miFileOpen.Text = "&Open";
            this.miFileOpen.Click += new System.EventHandler(this.miFileOpen_Click);
            // 
            // miFileClose
            // 
            this.miFileClose.Name = "miFileClose";
            this.miFileClose.Size = new System.Drawing.Size(152, 22);
            this.miFileClose.Text = "&Close";
            // 
            // miFileImport
            // 
            this.miFileImport.Name = "miFileImport";
            this.miFileImport.Size = new System.Drawing.Size(152, 22);
            this.miFileImport.Text = "Import...";
            // 
            // miFileExport
            // 
            this.miFileExport.Name = "miFileExport";
            this.miFileExport.Size = new System.Drawing.Size(152, 22);
            this.miFileExport.Text = "Export";
            // 
            // miFileSave
            // 
            this.miFileSave.Name = "miFileSave";
            this.miFileSave.Size = new System.Drawing.Size(152, 22);
            this.miFileSave.Text = "&Save";
            // 
            // miFileSaveAs
            // 
            this.miFileSaveAs.Name = "miFileSaveAs";
            this.miFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.miFileSaveAs.Text = "Save &As";
            // 
            // miFileSep1
            // 
            this.miFileSep1.Name = "miFileSep1";
            this.miFileSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // miFilePrint
            // 
            this.miFilePrint.Name = "miFilePrint";
            this.miFilePrint.Size = new System.Drawing.Size(152, 22);
            this.miFilePrint.Text = "&Print...";
            // 
            // miFilePrintPreview
            // 
            this.miFilePrintPreview.Name = "miFilePrintPreview";
            this.miFilePrintPreview.Size = new System.Drawing.Size(152, 22);
            this.miFilePrintPreview.Text = "Print Pre&view...";
            // 
            // miFilePrinterSetup
            // 
            this.miFilePrinterSetup.Name = "miFilePrinterSetup";
            this.miFilePrinterSetup.Size = new System.Drawing.Size(152, 22);
            this.miFilePrinterSetup.Text = "Printer Setup";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // miFileExit
            // 
            this.miFileExit.Name = "miFileExit";
            this.miFileExit.Size = new System.Drawing.Size(152, 22);
            this.miFileExit.Text = "E&xit";
            this.miFileExit.Click += new System.EventHandler(this.miFileExit_Click);
            // 
            // miTopEdit
            // 
            this.miTopEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditSelectCatchments,
            this.miEditSelectReservoir,
            this.miEditSelectRivers,
            this.miEditSelectAll,
            this.miEditClearSelectedFeatures});
            this.miTopEdit.Name = "miTopEdit";
            this.miTopEdit.Size = new System.Drawing.Size(39, 22);
            this.miTopEdit.Text = "&Edit";
            // 
            // miEditSelectCatchments
            // 
            this.miEditSelectCatchments.Enabled = false;
            this.miEditSelectCatchments.Name = "miEditSelectCatchments";
            this.miEditSelectCatchments.Size = new System.Drawing.Size(211, 22);
            this.miEditSelectCatchments.Text = "Select Catchments";
            this.miEditSelectCatchments.Click += new System.EventHandler(this.miEditSelectCatchments_Click);
            // 
            // miEditSelectReservoir
            // 
            this.miEditSelectReservoir.Enabled = false;
            this.miEditSelectReservoir.Name = "miEditSelectReservoir";
            this.miEditSelectReservoir.Size = new System.Drawing.Size(211, 22);
            this.miEditSelectReservoir.Text = "Select Reservoir Segments";
            this.miEditSelectReservoir.Click += new System.EventHandler(this.miEditSelectReservoir_Click);
            // 
            // miEditSelectRivers
            // 
            this.miEditSelectRivers.Enabled = false;
            this.miEditSelectRivers.Name = "miEditSelectRivers";
            this.miEditSelectRivers.Size = new System.Drawing.Size(211, 22);
            this.miEditSelectRivers.Text = "Select Rivers";
            this.miEditSelectRivers.Click += new System.EventHandler(this.miEditSelectRivers_Click);
            // 
            // miEditSelectAll
            // 
            this.miEditSelectAll.Enabled = false;
            this.miEditSelectAll.Name = "miEditSelectAll";
            this.miEditSelectAll.Size = new System.Drawing.Size(211, 22);
            this.miEditSelectAll.Text = "Select All";
            this.miEditSelectAll.Click += new System.EventHandler(this.miEditSelectAll_Click);
            // 
            // miEditClearSelectedFeatures
            // 
            this.miEditClearSelectedFeatures.Name = "miEditClearSelectedFeatures";
            this.miEditClearSelectedFeatures.Size = new System.Drawing.Size(211, 22);
            this.miEditClearSelectedFeatures.Text = "Clear Selected Features";
            this.miEditClearSelectedFeatures.Click += new System.EventHandler(this.miEditClearSelectedFeatures_Click);
            // 
            // miTopView
            // 
            this.miTopView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miViewZoomIn,
            this.miViewZoomOut,
            this.miViewZoomToExtent,
            this.toolStripSeparator6,
            this.miViewSelectableLayer,
            this.toolStripSeparator1,
            this.miViewTribConnect,
            this.miViewEntityIDs,
            this.miViewEntityPoints,
            this.miViewSubwatersheds,
            this.miViewMETStations,
            this.miViewGagingStations,
            this.miViewWQStations,
            this.miViewManagedFlow,
            this.miViewPointSources,
            this.miViewAirQualityStations,
            this.miViewScale,
            this.miViewPictures,
            this.miViewLabels});
            this.miTopView.Name = "miTopView";
            this.miTopView.Size = new System.Drawing.Size(44, 22);
            this.miTopView.Text = "&View";
            // 
            // miViewZoomIn
            // 
            this.miViewZoomIn.Name = "miViewZoomIn";
            this.miViewZoomIn.Size = new System.Drawing.Size(195, 22);
            this.miViewZoomIn.Text = "Zoom In";
            this.miViewZoomIn.Click += new System.EventHandler(this.miViewZoomIn_Click);
            // 
            // miViewZoomOut
            // 
            this.miViewZoomOut.Name = "miViewZoomOut";
            this.miViewZoomOut.Size = new System.Drawing.Size(195, 22);
            this.miViewZoomOut.Text = "Zoom Out";
            this.miViewZoomOut.Click += new System.EventHandler(this.miViewZoomOut_Click);
            // 
            // miViewZoomToExtent
            // 
            this.miViewZoomToExtent.Name = "miViewZoomToExtent";
            this.miViewZoomToExtent.Size = new System.Drawing.Size(195, 22);
            this.miViewZoomToExtent.Text = "Zoom to Extent";
            this.miViewZoomToExtent.Click += new System.EventHandler(this.miViewZoomToExtent_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(192, 6);
            // 
            // miViewSelectableLayer
            // 
            this.miViewSelectableLayer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miViewSelectableCatchments,
            this.miViewSelectableRivers,
            this.miViewSelectableLakes});
            this.miViewSelectableLayer.Name = "miViewSelectableLayer";
            this.miViewSelectableLayer.Size = new System.Drawing.Size(195, 22);
            this.miViewSelectableLayer.Text = "Selectable Layer(s)";
            // 
            // miViewSelectableCatchments
            // 
            this.miViewSelectableCatchments.CheckOnClick = true;
            this.miViewSelectableCatchments.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewSelectableCatchments.Name = "miViewSelectableCatchments";
            this.miViewSelectableCatchments.Size = new System.Drawing.Size(180, 22);
            this.miViewSelectableCatchments.Text = "Catchments";
            this.miViewSelectableCatchments.Click += new System.EventHandler(this.miViewSelectableCatchments_Click);
            // 
            // miViewSelectableRivers
            // 
            this.miViewSelectableRivers.CheckOnClick = true;
            this.miViewSelectableRivers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewSelectableRivers.Name = "miViewSelectableRivers";
            this.miViewSelectableRivers.Size = new System.Drawing.Size(180, 22);
            this.miViewSelectableRivers.Text = "Rivers";
            this.miViewSelectableRivers.Click += new System.EventHandler(this.miViewSelectableRivers_Click);
            // 
            // miViewSelectableLakes
            // 
            this.miViewSelectableLakes.CheckOnClick = true;
            this.miViewSelectableLakes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewSelectableLakes.Name = "miViewSelectableLakes";
            this.miViewSelectableLakes.Size = new System.Drawing.Size(180, 22);
            this.miViewSelectableLakes.Text = "Lakes";
            this.miViewSelectableLakes.Click += new System.EventHandler(this.miViewSelectableLakes_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // miViewTribConnect
            // 
            this.miViewTribConnect.CheckOnClick = true;
            this.miViewTribConnect.Enabled = false;
            this.miViewTribConnect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewTribConnect.Name = "miViewTribConnect";
            this.miViewTribConnect.Size = new System.Drawing.Size(195, 22);
            this.miViewTribConnect.Text = "Tributary Connections";
            // 
            // miViewEntityIDs
            // 
            this.miViewEntityIDs.CheckOnClick = true;
            this.miViewEntityIDs.Enabled = false;
            this.miViewEntityIDs.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewEntityIDs.Name = "miViewEntityIDs";
            this.miViewEntityIDs.Size = new System.Drawing.Size(195, 22);
            this.miViewEntityIDs.Text = "Entity ID\'s";
            // 
            // miViewEntityPoints
            // 
            this.miViewEntityPoints.CheckOnClick = true;
            this.miViewEntityPoints.Enabled = false;
            this.miViewEntityPoints.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewEntityPoints.Name = "miViewEntityPoints";
            this.miViewEntityPoints.Size = new System.Drawing.Size(195, 22);
            this.miViewEntityPoints.Text = "Selected Entities Points";
            // 
            // miViewSubwatersheds
            // 
            this.miViewSubwatersheds.CheckOnClick = true;
            this.miViewSubwatersheds.Enabled = false;
            this.miViewSubwatersheds.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewSubwatersheds.Name = "miViewSubwatersheds";
            this.miViewSubwatersheds.Size = new System.Drawing.Size(195, 22);
            this.miViewSubwatersheds.Text = "Subwatersheds";
            // 
            // miViewMETStations
            // 
            this.miViewMETStations.CheckOnClick = true;
            this.miViewMETStations.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewMETStations.Name = "miViewMETStations";
            this.miViewMETStations.Size = new System.Drawing.Size(195, 22);
            this.miViewMETStations.Text = "Meteorology Stations";
            this.miViewMETStations.Click += new System.EventHandler(this.miViewMETStations_Click);
            // 
            // miViewGagingStations
            // 
            this.miViewGagingStations.CheckOnClick = true;
            this.miViewGagingStations.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewGagingStations.Name = "miViewGagingStations";
            this.miViewGagingStations.Size = new System.Drawing.Size(195, 22);
            this.miViewGagingStations.Text = "Gaging Stations";
            this.miViewGagingStations.Click += new System.EventHandler(this.miViewGagingStations_Click);
            // 
            // miViewWQStations
            // 
            this.miViewWQStations.CheckOnClick = true;
            this.miViewWQStations.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewWQStations.Name = "miViewWQStations";
            this.miViewWQStations.Size = new System.Drawing.Size(195, 22);
            this.miViewWQStations.Text = "Water Quality Stations";
            this.miViewWQStations.Click += new System.EventHandler(this.miViewWQStations_Click);
            // 
            // miViewManagedFlow
            // 
            this.miViewManagedFlow.CheckOnClick = true;
            this.miViewManagedFlow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewManagedFlow.Name = "miViewManagedFlow";
            this.miViewManagedFlow.Size = new System.Drawing.Size(195, 22);
            this.miViewManagedFlow.Text = "Managed Flow";
            this.miViewManagedFlow.Click += new System.EventHandler(this.miViewManagedFlow_Click);
            // 
            // miViewPointSources
            // 
            this.miViewPointSources.CheckOnClick = true;
            this.miViewPointSources.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewPointSources.Name = "miViewPointSources";
            this.miViewPointSources.Size = new System.Drawing.Size(195, 22);
            this.miViewPointSources.Text = "Point Sources";
            this.miViewPointSources.Click += new System.EventHandler(this.miViewPointSources_Click);
            // 
            // miViewAirQualityStations
            // 
            this.miViewAirQualityStations.CheckOnClick = true;
            this.miViewAirQualityStations.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewAirQualityStations.Name = "miViewAirQualityStations";
            this.miViewAirQualityStations.Size = new System.Drawing.Size(195, 22);
            this.miViewAirQualityStations.Text = "Air Quality Stations";
            this.miViewAirQualityStations.Click += new System.EventHandler(this.miViewAirQualityStations_Click);
            // 
            // miViewScale
            // 
            this.miViewScale.CheckOnClick = true;
            this.miViewScale.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewScale.Name = "miViewScale";
            this.miViewScale.Size = new System.Drawing.Size(195, 22);
            this.miViewScale.Text = "Scale";
            // 
            // miViewPictures
            // 
            this.miViewPictures.CheckOnClick = true;
            this.miViewPictures.Enabled = false;
            this.miViewPictures.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewPictures.Name = "miViewPictures";
            this.miViewPictures.Size = new System.Drawing.Size(195, 22);
            this.miViewPictures.Text = "Pictures";
            // 
            // miViewLabels
            // 
            this.miViewLabels.CheckOnClick = true;
            this.miViewLabels.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miViewLabels.Name = "miViewLabels";
            this.miViewLabels.Size = new System.Drawing.Size(195, 22);
            this.miViewLabels.Text = "Labels";
            // 
            // miTopMode
            // 
            this.miTopMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miModeInput,
            this.miModeOutput,
            this.miModeFluxOutput,
            this.miModeLongGowdyOutput});
            this.miTopMode.Name = "miTopMode";
            this.miTopMode.Size = new System.Drawing.Size(50, 22);
            this.miTopMode.Text = "&Mode";
            // 
            // miModeInput
            // 
            this.miModeInput.BackColor = System.Drawing.SystemColors.Highlight;
            this.miModeInput.Name = "miModeInput";
            this.miModeInput.Size = new System.Drawing.Size(230, 22);
            this.miModeInput.Text = "Input";
            this.miModeInput.Click += new System.EventHandler(this.miModeInput_Click);
            // 
            // miModeOutput
            // 
            this.miModeOutput.Name = "miModeOutput";
            this.miModeOutput.Size = new System.Drawing.Size(230, 22);
            this.miModeOutput.Text = "Output";
            this.miModeOutput.Click += new System.EventHandler(this.miModeOutput_Click);
            // 
            // miModeFluxOutput
            // 
            this.miModeFluxOutput.Name = "miModeFluxOutput";
            this.miModeFluxOutput.Size = new System.Drawing.Size(230, 22);
            this.miModeFluxOutput.Text = "Flux Output";
            this.miModeFluxOutput.Click += new System.EventHandler(this.miModeFluxOutput_Click);
            // 
            // miModeLongGowdyOutput
            // 
            this.miModeLongGowdyOutput.Name = "miModeLongGowdyOutput";
            this.miModeLongGowdyOutput.Size = new System.Drawing.Size(230, 22);
            this.miModeLongGowdyOutput.Text = "Longitudinal / Gowdy Output";
            this.miModeLongGowdyOutput.Click += new System.EventHandler(this.miModeLongGowdyOutput_Click);
            // 
            // miTopScenario
            // 
            this.miTopScenario.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miScenarioRun,
            this.toolStripSeparator4,
            this.miScenarioManager,
            this.miScenarioSave,
            this.miScenarioSaveAs,
            this.miScenarioDelete,
            this.miScenarioExport,
            this.miScenarioImport,
            this.toolStripSeparator5,
            this.miScenarioViewCoeff,
            this.miScenarioCompare,
            this.toolStripSeparator7,
            this.miScenarioFileCheck});
            this.miTopScenario.Name = "miTopScenario";
            this.miTopScenario.Size = new System.Drawing.Size(64, 22);
            this.miTopScenario.Text = "&Scenario";
            // 
            // miScenarioRun
            // 
            this.miScenarioRun.Name = "miScenarioRun";
            this.miScenarioRun.Size = new System.Drawing.Size(205, 22);
            this.miScenarioRun.Text = "Run";
            this.miScenarioRun.Click += new System.EventHandler(this.miScenarioRun_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(202, 6);
            // 
            // miScenarioManager
            // 
            this.miScenarioManager.Name = "miScenarioManager";
            this.miScenarioManager.Size = new System.Drawing.Size(205, 22);
            this.miScenarioManager.Text = "Manager";
            this.miScenarioManager.Click += new System.EventHandler(this.miScenarioManager_Click);
            // 
            // miScenarioSave
            // 
            this.miScenarioSave.Name = "miScenarioSave";
            this.miScenarioSave.Size = new System.Drawing.Size(205, 22);
            this.miScenarioSave.Text = "Save";
            this.miScenarioSave.Click += new System.EventHandler(this.miScenarioSave_Click);
            // 
            // miScenarioSaveAs
            // 
            this.miScenarioSaveAs.Name = "miScenarioSaveAs";
            this.miScenarioSaveAs.Size = new System.Drawing.Size(205, 22);
            this.miScenarioSaveAs.Text = "Save As";
            this.miScenarioSaveAs.Click += new System.EventHandler(this.miScenarioSaveAs_Click);
            // 
            // miScenarioDelete
            // 
            this.miScenarioDelete.Name = "miScenarioDelete";
            this.miScenarioDelete.Size = new System.Drawing.Size(205, 22);
            this.miScenarioDelete.Text = "Delete";
            // 
            // miScenarioExport
            // 
            this.miScenarioExport.Name = "miScenarioExport";
            this.miScenarioExport.Size = new System.Drawing.Size(205, 22);
            this.miScenarioExport.Text = "Export";
            // 
            // miScenarioImport
            // 
            this.miScenarioImport.Name = "miScenarioImport";
            this.miScenarioImport.Size = new System.Drawing.Size(205, 22);
            this.miScenarioImport.Text = "Import";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(202, 6);
            // 
            // miScenarioViewCoeff
            // 
            this.miScenarioViewCoeff.Name = "miScenarioViewCoeff";
            this.miScenarioViewCoeff.Size = new System.Drawing.Size(205, 22);
            this.miScenarioViewCoeff.Text = "View Coefficient File";
            // 
            // miScenarioCompare
            // 
            this.miScenarioCompare.Name = "miScenarioCompare";
            this.miScenarioCompare.Size = new System.Drawing.Size(205, 22);
            this.miScenarioCompare.Text = "Compare";
            this.miScenarioCompare.Click += new System.EventHandler(this.miScenarioCompare_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(202, 6);
            // 
            // miScenarioFileCheck
            // 
            this.miScenarioFileCheck.Name = "miScenarioFileCheck";
            this.miScenarioFileCheck.Size = new System.Drawing.Size(205, 22);
            this.miScenarioFileCheck.Text = "Check Scenario Run Files";
            this.miScenarioFileCheck.Click += new System.EventHandler(this.miScenarioFileCheck_Click);
            // 
            // miTopDocument
            // 
            this.miTopDocument.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDocumentModelLog,
            this.miDocumentNotes});
            this.miTopDocument.Name = "miTopDocument";
            this.miTopDocument.Size = new System.Drawing.Size(75, 22);
            this.miTopDocument.Text = "&Document";
            // 
            // miDocumentModelLog
            // 
            this.miDocumentModelLog.Name = "miDocumentModelLog";
            this.miDocumentModelLog.Size = new System.Drawing.Size(131, 22);
            this.miDocumentModelLog.Text = "Model Log";
            // 
            // miDocumentNotes
            // 
            this.miDocumentNotes.Name = "miDocumentNotes";
            this.miDocumentNotes.Size = new System.Drawing.Size(131, 22);
            this.miDocumentNotes.Text = "Notes";
            // 
            // miModule
            // 
            this.miModule.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miData,
            this.miKnowledge,
            this.miTMDL,
            this.miConsensus,
            this.miManager});
            this.miModule.Name = "miModule";
            this.miModule.Size = new System.Drawing.Size(60, 22);
            this.miModule.Text = "M&odule";
            // 
            // miData
            // 
            this.miData.Name = "miData";
            this.miData.Size = new System.Drawing.Size(133, 22);
            this.miData.Text = "Data";
            this.miData.Click += new System.EventHandler(this.miData_Click);
            // 
            // miKnowledge
            // 
            this.miKnowledge.Name = "miKnowledge";
            this.miKnowledge.Size = new System.Drawing.Size(133, 22);
            this.miKnowledge.Text = "Knowledge";
            this.miKnowledge.Click += new System.EventHandler(this.miKnowledge_Click);
            // 
            // miTMDL
            // 
            this.miTMDL.Name = "miTMDL";
            this.miTMDL.Size = new System.Drawing.Size(133, 22);
            this.miTMDL.Text = "TMDL";
            this.miTMDL.Click += new System.EventHandler(this.miTMDL_Click);
            // 
            // miConsensus
            // 
            this.miConsensus.Name = "miConsensus";
            this.miConsensus.Size = new System.Drawing.Size(133, 22);
            this.miConsensus.Text = "Consensus";
            this.miConsensus.Click += new System.EventHandler(this.miConsensus_Click);
            // 
            // miManager
            // 
            this.miManager.Name = "miManager";
            this.miManager.Size = new System.Drawing.Size(133, 22);
            this.miManager.Text = "Manager";
            this.miManager.Click += new System.EventHandler(this.miManager_Click);
            // 
            // miTopWindow
            // 
            this.miTopWindow.Name = "miTopWindow";
            this.miTopWindow.Size = new System.Drawing.Size(63, 22);
            this.miTopWindow.Text = "&Window";
            // 
            // miFileHelp
            // 
            this.miFileHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHelpContents,
            this.miHelpHelp,
            this.toolStripSeparator2,
            this.miHelpAbout});
            this.miFileHelp.Name = "miFileHelp";
            this.miFileHelp.Size = new System.Drawing.Size(44, 22);
            this.miFileHelp.Text = "&Help";
            // 
            // miHelpContents
            // 
            this.miHelpContents.Name = "miHelpContents";
            this.miHelpContents.Size = new System.Drawing.Size(144, 22);
            this.miHelpContents.Text = "Contents";
            // 
            // miHelpHelp
            // 
            this.miHelpHelp.Name = "miHelpHelp";
            this.miHelpHelp.Size = new System.Drawing.Size(144, 22);
            this.miHelpHelp.Text = "Help on Help";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(141, 6);
            // 
            // miHelpAbout
            // 
            this.miHelpAbout.Name = "miHelpAbout";
            this.miHelpAbout.Size = new System.Drawing.Size(144, 22);
            this.miHelpAbout.Text = "About";
            this.miHelpAbout.Click += new System.EventHandler(this.miHelpAbout_Click);
            // 
            // pboxSplash
            // 
            this.pboxSplash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pboxSplash.Image = ((System.Drawing.Image)(resources.GetObject("pboxSplash.Image")));
            this.pboxSplash.Location = new System.Drawing.Point(84, 87);
            this.pboxSplash.Margin = new System.Windows.Forms.Padding(2);
            this.pboxSplash.Name = "pboxSplash";
            this.pboxSplash.Size = new System.Drawing.Size(768, 399);
            this.pboxSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxSplash.TabIndex = 3;
            this.pboxSplash.TabStop = false;
            this.pboxSplash.Click += new System.EventHandler(this.pboxSplash_Click);
            // 
            // lblLatLong
            // 
            this.lblLatLong.AutoSize = true;
            this.lblLatLong.Location = new System.Drawing.Point(402, 643);
            this.lblLatLong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLatLong.Name = "lblLatLong";
            this.lblLatLong.Size = new System.Drawing.Size(54, 13);
            this.lblLatLong.TabIndex = 4;
            this.lblLatLong.Text = "Lat/Long:";
            this.lblLatLong.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbZoomIn,
            this.tsbZoomOut,
            this.tsbZoomToExtent,
            this.tsbClearSelected});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(960, 37);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.CheckOnClick = true;
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomIn.Image")));
            this.tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomIn.Margin = new System.Windows.Forms.Padding(0);
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Size = new System.Drawing.Size(34, 37);
            this.tsbZoomIn.Text = "Zoom In";
            this.tsbZoomIn.ToolTipText = "Zoom in";
            this.tsbZoomIn.Click += new System.EventHandler(this.tsbZoomIn_Click);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomOut.Image")));
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(34, 34);
            this.tsbZoomOut.Text = "Zoom Out";
            this.tsbZoomOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbZoomOut.ToolTipText = "Zoom out";
            this.tsbZoomOut.Click += new System.EventHandler(this.tsbZoomOut_Click);
            // 
            // tsbZoomToExtent
            // 
            this.tsbZoomToExtent.AutoToolTip = false;
            this.tsbZoomToExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomToExtent.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomToExtent.Image")));
            this.tsbZoomToExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomToExtent.Margin = new System.Windows.Forms.Padding(0);
            this.tsbZoomToExtent.Name = "tsbZoomToExtent";
            this.tsbZoomToExtent.Size = new System.Drawing.Size(34, 37);
            this.tsbZoomToExtent.Text = "ButtonText";
            this.tsbZoomToExtent.ToolTipText = "Zoom to watershed extent";
            this.tsbZoomToExtent.Click += new System.EventHandler(this.tsbZoomToExtent_Click);
            // 
            // tsbClearSelected
            // 
            this.tsbClearSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClearSelected.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearSelected.Image")));
            this.tsbClearSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearSelected.Name = "tsbClearSelected";
            this.tsbClearSelected.Size = new System.Drawing.Size(34, 34);
            this.tsbClearSelected.Text = "Clear Selected";
            this.tsbClearSelected.ToolTipText = "Clear selected features";
            this.tsbClearSelected.Click += new System.EventHandler(this.tsbClearSelected_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(960, 612);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lblLatLong);
            this.Controls.Add(this.pboxSplash);
            this.Controls.Add(this.frmMap);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.Text = "Watershed Analysis Risk Management Framework";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxSplash)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		public EGIS.Controls.SFMap frmMap;
		private System.Windows.Forms.MenuStrip mnuMain;
		private System.Windows.Forms.ToolStripMenuItem miTopFile;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;

		private System.Windows.Forms.ToolStripMenuItem miFileHelp;
		private System.Windows.Forms.ToolStripMenuItem miFileNew;
		private System.Windows.Forms.ToolStripMenuItem miFileOpen;
		private System.Windows.Forms.ToolStripSeparator miFileSep1;
		private System.Windows.Forms.ToolStripMenuItem miHelpContents;
		private System.Windows.Forms.ToolStripMenuItem miHelpHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem miHelpAbout;
		private System.Windows.Forms.PictureBox pboxSplash;
		private System.Windows.Forms.ToolStripMenuItem miTopEdit;
		private System.Windows.Forms.ToolStripMenuItem miTopView;
		private System.Windows.Forms.ToolStripMenuItem miTopMode;
		private System.Windows.Forms.ToolStripMenuItem miTopScenario;
		private System.Windows.Forms.ToolStripMenuItem miModule;
		private System.Windows.Forms.ToolStripMenuItem miTopWindow;
		private System.Windows.Forms.ToolStripMenuItem miTopDocument;
		private System.Windows.Forms.ToolStripMenuItem miFileClose;
		private System.Windows.Forms.ToolStripMenuItem miFileImport;
		private System.Windows.Forms.ToolStripMenuItem miFileExport;
		private System.Windows.Forms.ToolStripMenuItem miFileSave;
		private System.Windows.Forms.ToolStripMenuItem miFileSaveAs;
		private System.Windows.Forms.ToolStripMenuItem miFilePrint;
		private System.Windows.Forms.ToolStripMenuItem miFilePrintPreview;
		private System.Windows.Forms.ToolStripMenuItem miFilePrinterSetup;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem miEditSelectCatchments;
		private System.Windows.Forms.ToolStripMenuItem miEditSelectReservoir;
		private System.Windows.Forms.ToolStripMenuItem miEditSelectRivers;
		private System.Windows.Forms.ToolStripMenuItem miEditSelectAll;
		private System.Windows.Forms.ToolStripMenuItem miViewZoomIn;
		private System.Windows.Forms.ToolStripMenuItem miViewZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem miModeInput;
		private System.Windows.Forms.ToolStripMenuItem miModeOutput;
		private System.Windows.Forms.ToolStripMenuItem miModeFluxOutput;
		private System.Windows.Forms.ToolStripMenuItem miModeLongGowdyOutput;
		private System.Windows.Forms.ToolStripMenuItem miScenarioRun;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem miScenarioManager;
		private System.Windows.Forms.ToolStripMenuItem miScenarioSave;
		private System.Windows.Forms.ToolStripMenuItem miScenarioSaveAs;
		private System.Windows.Forms.ToolStripMenuItem miScenarioDelete;
		private System.Windows.Forms.ToolStripMenuItem miScenarioExport;
		private System.Windows.Forms.ToolStripMenuItem miScenarioImport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem miScenarioViewCoeff;
		private System.Windows.Forms.ToolStripMenuItem miScenarioCompare;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem miData;
		private System.Windows.Forms.ToolStripMenuItem miKnowledge;
		private System.Windows.Forms.ToolStripMenuItem miTMDL;
		private System.Windows.Forms.ToolStripMenuItem miConsensus;
		private System.Windows.Forms.ToolStripMenuItem miManager;
		private System.Windows.Forms.ToolStripMenuItem miFileExit;
		private System.Windows.Forms.ToolStripMenuItem miDocumentModelLog;
		private System.Windows.Forms.ToolStripMenuItem miDocumentNotes;
		private System.Windows.Forms.ToolStripMenuItem miViewMETStations;
		private System.Windows.Forms.Label lblLatLong;
        private System.Windows.Forms.ToolStripMenuItem miViewTribConnect;
        private System.Windows.Forms.ToolStripMenuItem miViewEntityIDs;
        private System.Windows.Forms.ToolStripMenuItem miViewEntityPoints;
        private System.Windows.Forms.ToolStripMenuItem miViewSubwatersheds;
        private System.Windows.Forms.ToolStripMenuItem miViewGagingStations;
        private System.Windows.Forms.ToolStripMenuItem miViewWQStations;
        private System.Windows.Forms.ToolStripMenuItem miViewManagedFlow;
        private System.Windows.Forms.ToolStripMenuItem miViewPointSources;
        private System.Windows.Forms.ToolStripMenuItem miViewAirQualityStations;
        private System.Windows.Forms.ToolStripMenuItem miViewScale;
        private System.Windows.Forms.ToolStripMenuItem miViewPictures;
        private System.Windows.Forms.ToolStripMenuItem miViewLabels;
        private System.Windows.Forms.ToolStripMenuItem miScenarioFileCheck;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.ToolStripButton tsbZoomToExtent;
        private System.Windows.Forms.ToolStripButton tsbClearSelected;
        private System.Windows.Forms.ToolStripMenuItem miViewSelectableLayer;
        private System.Windows.Forms.ToolStripMenuItem miViewSelectableCatchments;
        private System.Windows.Forms.ToolStripMenuItem miViewSelectableRivers;
        private System.Windows.Forms.ToolStripMenuItem miViewSelectableLakes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripMenuItem miViewZoomToExtent;
        private System.Windows.Forms.ToolStripMenuItem miEditClearSelectedFeatures;
    }
}

