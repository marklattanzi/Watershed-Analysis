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
            this.miTopView = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.miEditRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewMETStations = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxSplash)).BeginInit();
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
            this.frmMap.Location = new System.Drawing.Point(15, 30);
            this.frmMap.MapBackColor = System.Drawing.SystemColors.Control;
            this.frmMap.Margin = new System.Windows.Forms.Padding(6);
            this.frmMap.Name = "frmMap";
            this.frmMap.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
            this.frmMap.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
            this.frmMap.Size = new System.Drawing.Size(917, 540);
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
            this.mnuMain.Size = new System.Drawing.Size(947, 24);
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
            this.miFileClose.Text = "Close";
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
            this.miFileSave.Text = "Save";
            // 
            // miFileSaveAs
            // 
            this.miFileSaveAs.Name = "miFileSaveAs";
            this.miFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.miFileSaveAs.Text = "Save As";
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
            this.miFilePrint.Text = "Print...";
            // 
            // miFilePrintPreview
            // 
            this.miFilePrintPreview.Name = "miFilePrintPreview";
            this.miFilePrintPreview.Size = new System.Drawing.Size(152, 22);
            this.miFilePrintPreview.Text = "Print Preview...";
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
            this.miEditSelectAll});
            this.miTopEdit.Name = "miTopEdit";
            this.miTopEdit.Size = new System.Drawing.Size(39, 22);
            this.miTopEdit.Text = "&Edit";
            // 
            // miEditSelectCatchments
            // 
            this.miEditSelectCatchments.Name = "miEditSelectCatchments";
            this.miEditSelectCatchments.Size = new System.Drawing.Size(211, 22);
            this.miEditSelectCatchments.Text = "Select Catchments";
            // 
            // miEditSelectReservoir
            // 
            this.miEditSelectReservoir.Name = "miEditSelectReservoir";
            this.miEditSelectReservoir.Size = new System.Drawing.Size(211, 22);
            this.miEditSelectReservoir.Text = "Select Reservoir Segments";
            // 
            // miEditSelectRivers
            // 
            this.miEditSelectRivers.Name = "miEditSelectRivers";
            this.miEditSelectRivers.Size = new System.Drawing.Size(211, 22);
            this.miEditSelectRivers.Text = "Select Rivers";
            // 
            // miEditSelectAll
            // 
            this.miEditSelectAll.Name = "miEditSelectAll";
            this.miEditSelectAll.Size = new System.Drawing.Size(211, 22);
            this.miEditSelectAll.Text = "Select All";
            // 
            // miTopView
            // 
            this.miTopView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miViewZoom,
            this.miEditZoomIn,
            this.miEditZoomOut,
            this.toolStripSeparator6,
            this.miEditRestore,
            this.miViewMETStations});
            this.miTopView.Name = "miTopView";
            this.miTopView.Size = new System.Drawing.Size(44, 22);
            this.miTopView.Text = "&View";
            // 
            // miViewZoom
            // 
            this.miViewZoom.Name = "miViewZoom";
            this.miViewZoom.Size = new System.Drawing.Size(187, 22);
            this.miViewZoom.Text = "Zoom";
            // 
            // miEditZoomIn
            // 
            this.miEditZoomIn.Name = "miEditZoomIn";
            this.miEditZoomIn.Size = new System.Drawing.Size(187, 22);
            this.miEditZoomIn.Text = "Zoom In";
            this.miEditZoomIn.Click += new System.EventHandler(this.miEditZoomIn_Click);
            // 
            // miEditZoomOut
            // 
            this.miEditZoomOut.Name = "miEditZoomOut";
            this.miEditZoomOut.Size = new System.Drawing.Size(187, 22);
            this.miEditZoomOut.Text = "Zoom Out";
            this.miEditZoomOut.Click += new System.EventHandler(this.miEditZoomOut_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(184, 6);
            // 
            // miEditRestore
            // 
            this.miEditRestore.Name = "miEditRestore";
            this.miEditRestore.Size = new System.Drawing.Size(187, 22);
            this.miEditRestore.Text = "Restore Map";
            // 
            // miViewMETStations
            // 
            this.miViewMETStations.Name = "miViewMETStations";
            this.miViewMETStations.Size = new System.Drawing.Size(187, 22);
            this.miViewMETStations.Text = "Meteorology Stations";
            this.miViewMETStations.Click += new System.EventHandler(this.miMETStations_Click);
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
            this.miScenarioCompare});
            this.miTopScenario.Name = "miTopScenario";
            this.miTopScenario.Size = new System.Drawing.Size(64, 22);
            this.miTopScenario.Text = "&Scenario";
            // 
            // miScenarioRun
            // 
            this.miScenarioRun.Name = "miScenarioRun";
            this.miScenarioRun.Size = new System.Drawing.Size(181, 22);
            this.miScenarioRun.Text = "Run";
            this.miScenarioRun.Click += new System.EventHandler(this.miScenarioRun_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(178, 6);
            // 
            // miScenarioManager
            // 
            this.miScenarioManager.Name = "miScenarioManager";
            this.miScenarioManager.Size = new System.Drawing.Size(181, 22);
            this.miScenarioManager.Text = "Manager";
            // 
            // miScenarioSave
            // 
            this.miScenarioSave.Name = "miScenarioSave";
            this.miScenarioSave.Size = new System.Drawing.Size(181, 22);
            this.miScenarioSave.Text = "Save";
            this.miScenarioSave.Click += new System.EventHandler(this.miScenarioSave_Click);
            // 
            // miScenarioSaveAs
            // 
            this.miScenarioSaveAs.Name = "miScenarioSaveAs";
            this.miScenarioSaveAs.Size = new System.Drawing.Size(181, 22);
            this.miScenarioSaveAs.Text = "Save As";
            // 
            // miScenarioDelete
            // 
            this.miScenarioDelete.Name = "miScenarioDelete";
            this.miScenarioDelete.Size = new System.Drawing.Size(181, 22);
            this.miScenarioDelete.Text = "Delete";
            // 
            // miScenarioExport
            // 
            this.miScenarioExport.Name = "miScenarioExport";
            this.miScenarioExport.Size = new System.Drawing.Size(181, 22);
            this.miScenarioExport.Text = "Export";
            // 
            // miScenarioImport
            // 
            this.miScenarioImport.Name = "miScenarioImport";
            this.miScenarioImport.Size = new System.Drawing.Size(181, 22);
            this.miScenarioImport.Text = "Import";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(178, 6);
            // 
            // miScenarioViewCoeff
            // 
            this.miScenarioViewCoeff.Name = "miScenarioViewCoeff";
            this.miScenarioViewCoeff.Size = new System.Drawing.Size(181, 22);
            this.miScenarioViewCoeff.Text = "View Coefficient File";
            // 
            // miScenarioCompare
            // 
            this.miScenarioCompare.Name = "miScenarioCompare";
            this.miScenarioCompare.Size = new System.Drawing.Size(181, 22);
            this.miScenarioCompare.Text = "Compare";
            this.miScenarioCompare.Click += new System.EventHandler(this.miScenarioCompare_Click);
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
            this.pboxSplash.Image = global::warmf.Properties.Resources.splash;
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(947, 575);
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
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private EGIS.Controls.SFMap frmMap;
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
		private System.Windows.Forms.ToolStripMenuItem miViewZoom;
		private System.Windows.Forms.ToolStripMenuItem miEditZoomIn;
		private System.Windows.Forms.ToolStripMenuItem miEditZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem miEditRestore;
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
	}
}

