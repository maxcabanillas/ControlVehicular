using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

//
// ASP.NET code-behind class (Page)
//

partial class z_RegistroPeatoneslist: AspNetMaker9_ControlVehicular {

	// Page object
	public cz_RegistroPeatones_list z_RegistroPeatones_list;	

	//
	// Page Class
	//
	public class cz_RegistroPeatones_list: AspNetMakerPage, IDisposable {

		// Used by system generated functions
		private ArrayList RsWrk; // ArrayList of OrderedDictionary

		private DbDataReader drWrk; // DataReader

		private string sSqlWrk;

		private string sWhereWrk;

		private string sFilterWrk; 

		private string[] arwrk;

		private ArrayList alwrk;

		private OrderedDictionary odwrk;

		private string[] armultiwrk;

		// Common URLs
		public string AddUrl = "";

		public string EditUrl = "";

		public string CopyUrl = "";

		public string DeleteUrl = "";

		public string ViewUrl = "";

		public string ListUrl = "";

		// Export URLs
		public string ExportPrintUrl = "";

		public string ExportHtmlUrl = "";

		public string ExportExcelUrl = "";

		public string ExportWordUrl = "";

		public string ExportXmlUrl = "";

		public string ExportCsvUrl = "";

		public string ExportPdfUrl = "";

		// Inline URLs
		public string InlineAddUrl = "";

		public string InlineCopyUrl = "";

		public string InlineEditUrl = "";

		public string GridAddUrl = "";

		public string GridEditUrl = "";

		public string MultiDeleteUrl = "";

		public string MultiUpdateUrl = "";

//		protected string m_DebugMsg = "";
//		public string DebugMsg {
//			get { return (ew_NotEmpty(m_DebugMsg)) ? "<p>" + m_DebugMsg + "</p>" : m_DebugMsg; }
//			set {
//				if (ew_NotEmpty(m_DebugMsg))	{	// Append
//					m_DebugMsg += "<br />" + value;
//				} else {
//					m_DebugMsg = value;
//				}
//			}
//		}
		// Show Message
		public void ShowMessage() {
			string sMessage = Message;
			Message_Showing(ref sMessage, "");
			if (ew_NotEmpty(sMessage)) {
				ew_Write("<p class=\"ewMessage\">" + sMessage + "</p>");
				ew_Session[EW_SESSION_MESSAGE] = ""; // Clear message in Session
			}

			// Success message
			string sSuccessMessage = SuccessMessage;
			Message_Showing(ref sSuccessMessage, "success");
			if (ew_NotEmpty(sSuccessMessage)) { // Message in Session, display
				ew_Write("<p class=\"ewSuccessMessage\">" + sSuccessMessage + "</p>");
				ew_Session[EW_SESSION_SUCCESS_MESSAGE] = ""; // Clear message in Session
			}

			// Failure message
			string sErrorMessage = FailureMessage;
			Message_Showing(ref sErrorMessage, "failure");
			if (ew_NotEmpty(sErrorMessage)) { // Message in Session, display
				ew_Write("<p class=\"ewErrorMessage\">" + sErrorMessage + "</p>");
				ew_Session[EW_SESSION_FAILURE_MESSAGE] = ""; // Clear message in Session
			}
		}

		// Page URL
		public string PageUrl {
			get {
				string Url = ew_CurrentPage() + "?";
				if (z_RegistroPeatones.UseTokenInUrl)
					Url += "t=" + z_RegistroPeatones.TableVar + "&"; // Add page token
				return Url;
			}
		}

		public string PageHeader = "";

		public string PageFooter = "";

		// Show Page Header
		public void ShowPageHeader() {
			string sHeader = PageHeader;
			Page_DataRendering(ref sHeader);
			if (ew_NotEmpty(sHeader)) // Header exists, display
				ew_Write("<p class=\"aspnetmaker\">" + sHeader + "</p>");
		}

		// Show Page Footer
		public void ShowPageFooter() {
			string sFooter = PageFooter;
			Page_DataRendered(ref sFooter);
			if (ew_NotEmpty(sFooter)) // Fotoer exists, display
				ew_Write("<p class=\"aspnetmaker\">" + sFooter + "</p>");
		}

		// Validate page request
		public bool IsPageRequest() {
			if (z_RegistroPeatones.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (z_RegistroPeatones.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (z_RegistroPeatones.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public z_RegistroPeatoneslist AspNetPage { 
			get { return (z_RegistroPeatoneslist)m_ParentPage; }
		}

		// z_RegistroPeatones	
		public cz_RegistroPeatones z_RegistroPeatones { 
			get {				
				return ParentPage.z_RegistroPeatones;
			}
			set {
				ParentPage.z_RegistroPeatones = value;	
			}	
		}		

		// Usuarios	
		public cUsuarios Usuarios { 
			get {				
				return ParentPage.Usuarios;
			}
			set {
				ParentPage.Usuarios = value;	
			}	
		}		

		//
		//  Page class constructor
		//
		public cz_RegistroPeatones_list(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "list";
			m_PageObjName = "z_RegistroPeatones_list";
			m_PageObjTypeName = "cz_RegistroPeatones_list";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (z_RegistroPeatones == null)
				z_RegistroPeatones = new cz_RegistroPeatones(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "_RegistroPeatones";
			m_Table = z_RegistroPeatones;
			CurrentTable = z_RegistroPeatones;

			//CurrentTableType = z_RegistroPeatones.GetType();
			// Initialize URLs

			ExportPrintUrl = PageUrl + "export=print";
			ExportExcelUrl = PageUrl + "export=excel";
			ExportWordUrl = PageUrl + "export=word";
			ExportHtmlUrl = PageUrl + "export=html";
			ExportXmlUrl = PageUrl + "export=xml";
			ExportCsvUrl = PageUrl + "export=csv";
			ExportPdfUrl = PageUrl + "export=pdf";
			AddUrl = "z_RegistroPeatonesadd.aspx";
			InlineAddUrl = PageUrl + "a=add";
			GridAddUrl = PageUrl + "a=gridadd";
			GridEditUrl = PageUrl + "a=gridedit";
			MultiDeleteUrl = "z_RegistroPeatonesdelete.aspx";
			MultiUpdateUrl = "z_RegistroPeatonesupdate.aspx";

			// Connect to database
			if (Conn == null)
				Conn = new cConnection();

			// Initialize list options
			ListOptions = new cListOptions();

			// Export options
			ExportOptions = new cListOptions();
			ExportOptions.Tag = "span";
			ExportOptions.Separator = "&nbsp;&nbsp;";
		}

		//
		//  Page_Init
		//
		public void Page_Init() {
			Security = new cAdvancedSecurity(this);
			if (!Security.IsLoggedIn()) Security.AutoLogin();

			// Table Permission loading event
			Security.TablePermission_Loading();
			Security.LoadCurrentUserLevel(TableName);

			// Table Permission loaded event
			Security.TablePermission_Loaded();
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();
			Security.UserID_Loaded();

			// Get export parameters
			if (ew_NotEmpty(ew_Get("export"))) {
				z_RegistroPeatones.Export = ew_Get("export");
			} else if (ew_NotEmpty(ew_Post("exporttype"))) {
				z_RegistroPeatones.Export = ew_Post("exporttype");
			} else {
				z_RegistroPeatones.ExportReturnUrl = ew_CurrentUrl();
			}
			gsExport = z_RegistroPeatones.Export; // Get export parameter, used in header
			gsExportFile = z_RegistroPeatones.TableVar; // Get export file, used in header			
			string Charset = (ew_NotEmpty(EW_CHARSET)) ? ";charset=" + EW_CHARSET : ""; // Charset used in header

			// Write BOM 
			if (z_RegistroPeatones.Export == "excel" || z_RegistroPeatones.Export == "word" || z_RegistroPeatones.Export == "csv")
				HttpContext.Current.Response.BinaryWrite(new byte[]{0xEF, 0xBB, 0xBF});
			if (z_RegistroPeatones.Export == "excel") {
				HttpContext.Current.Response.ContentType = "application/vnd.ms-excel" + Charset;
				ew_AddHeader("Content-Disposition", "attachment; filename=" + gsExportFile + ".xls");
			}

			// Get grid add count
			int gridaddcnt = ew_ConvertToInt(ew_Get(EW_TABLE_GRID_ADD_ROW_COUNT));
			if (gridaddcnt > 0)
				z_RegistroPeatones.GridAddRowCount = gridaddcnt;

			// Set up list options
			SetupListOptions();

			// Setup export options
			SetupExportOptions();

			// Global page loading event (in ewglobal*.cs)
			ParentPage.Page_Loading();

			// Page load event, used in current page
			Page_Load();
		}

		//
		//  Class terminate
		//  - clean up page object
		//
		public void Dispose()	{
			Page_Terminate("");
		}

		//
		//  Sub Page_Terminate
		//  - called when exit page
		//  - clean up connection and objects
		//  - if URL specified, redirect to URL
		//
		public void Page_Terminate(string url) {

			// Page unload event, used in current page
			Page_Unload();

			// Global page unloaded event (in ewglobal*.cs)
			ParentPage.Page_Unloaded();

			// Go to URL if specified
			string sRedirectUrl = url;
			Page_Redirecting(ref sRedirectUrl);

			// Close connection
			Conn.Dispose();
			z_RegistroPeatones.Dispose();
			Usuarios.Dispose();
			if (ew_NotEmpty(sRedirectUrl)) {
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.Redirect(sRedirectUrl); 
			}
		}

	public int DisplayRecs; // Number of display records

	public int StartRec;

	public int StopRec;

	public int TotalRecs;

	public int RecRange;

	public string SearchWhere;

	public int RecCnt;

	public int EditRowCnt;

	public int RowCnt;

	public object RowIndex;

	public int KeyCount = 0; // Key count

	public string RowAction = ""; // Row action

	public string RowOldKey = ""; // Row old key (for copy)

	public int RecPerRow;

	public int ColCnt;		

	public string DbMasterFilter;

	public string DbDetailFilter;

	public bool MasterRecordExists;

	public cListOptions ListOptions;

	public cListOptions ExportOptions; // Export options

	public string MultiSelectKey;

	public bool RestoreSearch;

	public int x__Priv;

	public SqlDataReader Recordset;

	public SqlDataReader OldRecordset; 

	//
	// Page main processing
	//
	public void Page_Main() {
		DisplayRecs = 20;
		RecRange = EW_PAGER_RANGE;
		RecCnt = 0; // Record count

		// Search filters
		string sSrchAdvanced = "";	// Advanced search filter
		string sSrchBasic = "";	// Basic search filter
		string sFilter = "";	// Search WHERE clause

		//sDeleteConfirmMsg = "<%= Language.Phrase("DeleteConfirmMsg") %>";
		// Master/Detail

		DbMasterFilter = ""; // Master filter
		DbDetailFilter = ""; // Detail filter
		if (IsPageRequest()) { // Validate request

			// Handle reset command
			ResetCmd();

			// Hide all options
			if (ew_NotEmpty(z_RegistroPeatones.Export) ||
				z_RegistroPeatones.CurrentAction == "gridadd" ||
				z_RegistroPeatones.CurrentAction == "gridedit") {
				ListOptions.HideAllOptions();
				ExportOptions.HideAllOptions();
			}

			// Set Up Sorting Order
			SetUpSortOrder();
		}

		// Restore display records
		if (z_RegistroPeatones.RecordsPerPage == -1 || z_RegistroPeatones.RecordsPerPage > 0) {
			DisplayRecs = z_RegistroPeatones.RecordsPerPage; // Restore from Session
		} else {
			DisplayRecs = 20; // Load default
		}

		// Load Sorting Order
		LoadSortOrder();

		// Build filter
		sFilter = "";
		ew_AddFilter(ref sFilter, DbDetailFilter);
		ew_AddFilter(ref sFilter, SearchWhere);

		// Set up filter in Session
		z_RegistroPeatones.SessionWhere = sFilter;
		z_RegistroPeatones.CurrentFilter = "";

		// Export Data only
		if (z_RegistroPeatones.Export == "html" || z_RegistroPeatones.Export == "csv" || z_RegistroPeatones.Export == "word" || z_RegistroPeatones.Export == "excel" || z_RegistroPeatones.Export == "xml" || z_RegistroPeatones.Export == "pdf") {
			ExportData();
			Page_Terminate(""); // Clean up
			ew_End(); // Terminate response
		} else if (z_RegistroPeatones.Export == "email") {
			ExportData();
			ew_End();
		}
	}

	//
	// Set up Sort parameters based on Sort Links clicked
	//
	public void SetUpSortOrder() {

		// Check for Ctrl pressed
		bool bCtrl = (ew_NotEmpty(ew_Get("ctrl")));

		// Check for an Order parameter
		if (ew_NotEmpty(ew_Get("order"))) {
			z_RegistroPeatones.CurrentOrder = ew_Get("order");
			z_RegistroPeatones.CurrentOrderType = ew_Get("ordertype");
			z_RegistroPeatones.StartRecordNumber = 1; // Reset start position
		}
	}

	//
	// Load Sort Order parameters
	//
	public void LoadSortOrder()	{
		string sOrderBy = z_RegistroPeatones.SessionOrderBy; // Get order by from Session
		if (ew_Empty(sOrderBy)) {
			if (ew_NotEmpty(z_RegistroPeatones.SqlOrderBy)) {
				sOrderBy = z_RegistroPeatones.SqlOrderBy;
				z_RegistroPeatones.SessionOrderBy = sOrderBy;
			}
		}
	}

	//
	// Reset command based on querystring parameter "cmd"
	// - reset: reset search parameters
	// - resetall: reset search and master/detail parameters
	// - resetsort: reset sort parameters
	//
	public void ResetCmd() {
		string sCmd;

		// Get reset cmd
		if (ew_NotEmpty(ew_Get("cmd"))) {
			sCmd = ew_Get("cmd");

			// Reset sort criteria
			if (ew_SameText(sCmd, "resetsort")) {
				string sOrderBy = "";
				z_RegistroPeatones.SessionOrderBy = sOrderBy;
			}

			// Reset start position
			StartRec = 1;
			z_RegistroPeatones.StartRecordNumber = StartRec;
		}
	}

	//
	// Set up list options
	//
	public void SetupListOptions() {
		cListOption item;
		ListOptions_Load();
	}

	// Render list options
	public void RenderListOptions() {
		cListOption oListOpt;
		ListOptions.LoadDefault();
		string links;
		RenderListOptionsExt();
		ListOptions_Rendered();
	}

	public void RenderListOptionsExt() {
	}

	public cNumericPager Pager;

	//
	// Set up Starting Record parameters
	//
	public void SetUpStartRec()	{
		int PageNo;

		// Exit if DisplayRecs = 0
		if (DisplayRecs == 0) return;
		if (IsPageRequest()) { // Validate request

			// Check for a "start" parameter
			if (ew_NotEmpty(ew_Get(EW_TABLE_START_REC)) && Information.IsNumeric(ew_Get(EW_TABLE_START_REC)))	{
				StartRec = ew_ConvertToInt(ew_Get(EW_TABLE_START_REC));
				z_RegistroPeatones.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				z_RegistroPeatones.StartRecordNumber = StartRec;
			}
		}
		StartRec = z_RegistroPeatones.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			z_RegistroPeatones.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			z_RegistroPeatones.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			z_RegistroPeatones.StartRecordNumber = StartRec;
		}
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = z_RegistroPeatones.CurrentFilter;
		z_RegistroPeatones.Recordset_Selecting(ref sFilter);
		z_RegistroPeatones.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = z_RegistroPeatones.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		

		// Load recordset
		SqlDataReader Rs = Conn.GetDataReader(sSql);		
		if (TotalRecs < 0 && Rs != null && Rs.HasRows)	{
			TotalRecs = 0;
			while (Rs.Read())
				TotalRecs++;
			Rs.Close();
			Rs = Conn.GetDataReader(sSql);
		}

		// Recordset Selected event
		z_RegistroPeatones.Recordset_Selected(Rs);
		return Rs;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = z_RegistroPeatones.KeyFilter;

		// Row Selecting event
		z_RegistroPeatones.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		z_RegistroPeatones.CurrentFilter = sFilter;
		string sSql = z_RegistroPeatones.SQL;

		// Write SQL for debug
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); // Show SQL for debugging
		try {
			RsRow = Conn.GetTempDataReader(sSql);
			if (!RsRow.Read()) {
				return false;
			}	else {
				LoadRowValues(ref RsRow);
				return true;
			}
		}	catch {
			if (EW_DEBUG_ENABLED) throw; 
			return false;
		}	finally {
			Conn.CloseTempDataReader();
		}
	}

	//
	// Load row values from recordset
	//
	public void LoadRowValues(ref SqlDataReader dr) {
		if (dr == null)
			return;
		string sDetailFilter;

		// Call Row Selected event
		OrderedDictionary row = Conn.GetRow(ref dr);
		z_RegistroPeatones.Row_Selected(ref row);
		z_RegistroPeatones.IdRegistroPeaton.DbValue = row["IdRegistroPeaton"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;

		// Load old recordset
		if (bValidKey) {
			z_RegistroPeatones.CurrentFilter = z_RegistroPeatones.KeyFilter;
			string sSql = z_RegistroPeatones.SQL;			
			OldRecordset = Conn.GetTempDataReader(sSql);
			if (OldRecordset != null && OldRecordset.Read())
				LoadRowValues(ref OldRecordset); // Load row values
				return true;
		} else {
			OldRecordset = null;
		}
		return bValidKey;
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
		ViewUrl = z_RegistroPeatones.ViewUrl;
		EditUrl = z_RegistroPeatones.EditUrl;
		InlineEditUrl = z_RegistroPeatones.InlineEditUrl;
		CopyUrl = z_RegistroPeatones.CopyUrl;
		InlineCopyUrl = z_RegistroPeatones.InlineCopyUrl;
		DeleteUrl = z_RegistroPeatones.DeleteUrl;

		// Row Rendering event
		z_RegistroPeatones.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdRegistroPeaton
		//
		//  View  Row
		//

		if (z_RegistroPeatones.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdRegistroPeaton
				z_RegistroPeatones.IdRegistroPeaton.ViewValue = z_RegistroPeatones.IdRegistroPeaton.CurrentValue;
			z_RegistroPeatones.IdRegistroPeaton.ViewCustomAttributes = "";

			// View refer script
		}

		// Row Rendered event
		if (z_RegistroPeatones.RowType != EW_ROWTYPE_AGGREGATEINIT)
			z_RegistroPeatones.Row_Rendered();
	}

	// Set up export options
	public void SetupExportOptions() {
		cListOption item;

		// Printer friendly
		item = ExportOptions.Add("print");
		item.Body = "<a href=\"" + ExportPrintUrl + "\">" + "<img src=\"aspximages/print.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("PrinterFriendly")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("PrinterFriendly")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Export to Excel
		item = ExportOptions.Add("excel");
		item.Body = "<a href=\"" + ExportExcelUrl + "\">" + "<img src=\"aspximages/exportxls.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToExcel")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToExcel")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = true;

		// Export to Word
		item = ExportOptions.Add("word");
		item.Body = "<a href=\"" + ExportWordUrl + "\">" + "<img src=\"aspximages/exportdoc.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToWord")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToWord")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Export to Html
		item = ExportOptions.Add("html");
		item.Body = "<a href=\"" + ExportHtmlUrl + "\">" + "<img src=\"aspximages/exporthtml.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToHtml")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToHtml")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Export to Xml
		item = ExportOptions.Add("xml");
		item.Body = "<a href=\"" + ExportXmlUrl + "\">" + "<img src=\"aspximages/exportxml.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToXml")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToXml")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Export to Csv
		item = ExportOptions.Add("csv");
		item.Body = "<a href=\"" + ExportCsvUrl + "\">" + "<img src=\"aspximages/exportcsv.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToCsv")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToCsv")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Export to Pdf
		item = ExportOptions.Add("pdf");
		item.Body = "<a href=\"" + ExportPdfUrl + "\">" + "<img src=\"aspximages/exportpdf.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToPdf")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToPdf")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Export to Email
		item = ExportOptions.Add("email");
		item.Body = "<a name=\"emf_z_RegistroPeatones\" id=\"emf_z_RegistroPeatones\" href=\"javascript:void(0);\" onclick=\"ew_EmailDialogShow({lnk:'emf_z_RegistroPeatones',hdr:ewLanguage.Phrase('ExportToEmail'),f:ew_GetForm('fz_RegistroPeatoneslist'),sel:false});\">" + "<img src=\"aspximages/exportemail.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToEmail")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToEmail")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Hide options for export/action
		if (ew_NotEmpty(z_RegistroPeatones.Export) || ew_NotEmpty(z_RegistroPeatones.CurrentAction))
			ExportOptions.HideAllOptions();
	}

	// Export data in HTML/CSV/Word/Excel/XML/Email/PDF format
	public void ExportData() {
		cXMLDocument XmlDoc = null;
		cExportDocument ExportDoc = null;
		bool utf8 = ew_SameText(EW_CHARSET, "utf-8");

		// Load recordset
		SqlDataReader Rs = LoadRecordset();
		StartRec = 1;

		// Export all
		if (z_RegistroPeatones.ExportAll) {
			DisplayRecs = TotalRecs;
			StopRec = TotalRecs;
		} else { // Export one page only
			SetUpStartRec(); // Set up start record position

			// Set the last record to display
			if (DisplayRecs < 0) {
				StopRec = TotalRecs;
			} else {
				StopRec = StartRec + DisplayRecs - 1;
			}
		}
		if (Rs == null) {
			ew_AddHeader("Content-Type", ""); // Remove header
			ew_AddHeader("Content-Disposition", "");
			ShowMessage();
			return;
		}
		if (z_RegistroPeatones.Export == "xml") {
			XmlDoc = new cXMLDocument();
		} else {
			ExportDoc = new cExportDocument(this, z_RegistroPeatones, "h");
		}
		string ParentTable = "";
		if (z_RegistroPeatones.Export == "xml") {
			z_RegistroPeatones.ExportXmlDocument(ref XmlDoc, ew_NotEmpty(ParentTable), ref Rs, StartRec, StopRec, "");
		} else {
			string sHeader = PageHeader;
			Page_DataRendering(ref sHeader);
			ExportDoc.Text += sHeader;
			z_RegistroPeatones.ExportDocument(ref ExportDoc, ref Rs, StartRec, StopRec, "");
			string sFooter = PageFooter;
			Page_DataRendered(ref sFooter);
			ExportDoc.Text += sFooter;
		}

		// Close recordset
		Rs.Close();
		Rs.Dispose();

		// Export header and footer
		if (z_RegistroPeatones.Export != "xml")
			ExportDoc.ExportHeaderAndFooter();

		// Clean output buffer
		if (!EW_DEBUG_ENABLED)
			HttpContext.Current.Response.Clear();

		// Write BOM if utf-8
		if (utf8 && z_RegistroPeatones.Export != "email" && z_RegistroPeatones.Export != "xml")
			HttpContext.Current.Response.BinaryWrite(new byte[]{0xEF, 0xBB, 0xBF});

		// Write debug message if enabled
		if (EW_DEBUG_ENABLED)
			ew_Write(ew_DebugMsg());

		// Output data
		if (z_RegistroPeatones.Export == "xml") {
			ew_AddHeader("Content-Type", "text/xml");
			ew_Write(XmlDoc.XML());
		} else if (z_RegistroPeatones.Export == "pdf") {
			ExportPDF(ExportDoc.Text);
		} else {
			ew_Write(ExportDoc.Text);
		}
	}

	// PDF Export
	public void ExportPDF(string html) {
		ew_DeleteTmpImages();
		ew_Write("Missing PDF Export extension.");
		ew_End();
	}

		// Page Load event
		public void Page_Load() {

			//HttpContext.Current.Response.Write("Page Load");
		}

		// Page Unload event
		public void Page_Unload() {

			//HttpContext.Current.Response.Write("Page Unload");
		}

				// Page Redirecting event
public void Page_Redirecting(ref string url) {
	   url = "_RegistroPeatones.aspx";    
}                             

		// Message Showing event
		// type = ""|"success"|"failure"
		public void Message_Showing(ref string msg, string type) {

			//msg = newmsg;
		}

		// Page Data Rendering event
		public void Page_DataRendering(ref string header) {

			// Example:
			//header = "your header";

		}

		// Page Data Rendered event
		public void Page_DataRendered(ref string footer) {

			// Example:
			//footer = "your footer";

		}

		// Form Custom Validate event
		public bool Form_CustomValidate(ref string CustomError) {

			//Return error message in CustomError
			return true;
		}

		// ListOptions Load event
		public void ListOptions_Load() {

			//Example: 
			//cListOption opt = ListOptions.Add("new");
		//opt.Header = "xxx";
			//opt.OnLeft = true; // Link on left
			//opt.MoveTo(0); // Move to first column

		}

		// ListOptions Rendered event
		public void ListOptions_Rendered() {

			//Example: 
			//ListOptions.GetItem("new").Body = "xxx";

		}
	}

	//
	// ASP.NET Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {

		// Page init
		z_RegistroPeatones_list = new cz_RegistroPeatones_list(this);
		CurrentPage = z_RegistroPeatones_list;

		//CurrentPageType = z_RegistroPeatones_list.GetType();
		z_RegistroPeatones_list.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		z_RegistroPeatones_list.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (z_RegistroPeatones_list != null)
			z_RegistroPeatones_list.Dispose();
	}
}
