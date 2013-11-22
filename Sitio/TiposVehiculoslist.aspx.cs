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

partial class TiposVehiculoslist: AspNetMaker9_ControlVehicular {

	// Page object
	public cTiposVehiculos_list TiposVehiculos_list;	

	//
	// Page Class
	//
	public class cTiposVehiculos_list: AspNetMakerPage, IDisposable {

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
				if (TiposVehiculos.UseTokenInUrl)
					Url += "t=" + TiposVehiculos.TableVar + "&"; // Add page token
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
			if (TiposVehiculos.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (TiposVehiculos.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (TiposVehiculos.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public TiposVehiculoslist AspNetPage { 
			get { return (TiposVehiculoslist)m_ParentPage; }
		}

		// TiposVehiculos	
		public cTiposVehiculos TiposVehiculos { 
			get {				
				return ParentPage.TiposVehiculos;
			}
			set {
				ParentPage.TiposVehiculos = value;	
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
		public cTiposVehiculos_list(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "list";
			m_PageObjName = "TiposVehiculos_list";
			m_PageObjTypeName = "cTiposVehiculos_list";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (TiposVehiculos == null)
				TiposVehiculos = new cTiposVehiculos(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "TiposVehiculos";
			m_Table = TiposVehiculos;
			CurrentTable = TiposVehiculos;

			//CurrentTableType = TiposVehiculos.GetType();
			// Initialize URLs

			ExportPrintUrl = PageUrl + "export=print";
			ExportExcelUrl = PageUrl + "export=excel";
			ExportWordUrl = PageUrl + "export=word";
			ExportHtmlUrl = PageUrl + "export=html";
			ExportXmlUrl = PageUrl + "export=xml";
			ExportCsvUrl = PageUrl + "export=csv";
			ExportPdfUrl = PageUrl + "export=pdf";
			AddUrl = "TiposVehiculosadd.aspx";
			InlineAddUrl = PageUrl + "a=add";
			GridAddUrl = PageUrl + "a=gridadd";
			GridEditUrl = PageUrl + "a=gridedit";
			MultiDeleteUrl = "TiposVehiculosdelete.aspx";
			MultiUpdateUrl = "TiposVehiculosupdate.aspx";

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
			if (!Security.IsLoggedIn()) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
			}

			// Table Permission loading event
			Security.TablePermission_Loading();
			Security.LoadCurrentUserLevel(TableName);

			// Table Permission loaded event
			Security.TablePermission_Loaded();
			if (!Security.IsLoggedIn()) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
			}
			if (!Security.CanList) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
			}

			// UserID_Loading event
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();

			// UserID_Loaded event
			Security.UserID_Loaded();

			// Get export parameters
			if (ew_NotEmpty(ew_Get("export"))) {
				TiposVehiculos.Export = ew_Get("export");
			} else if (ew_NotEmpty(ew_Post("exporttype"))) {
				TiposVehiculos.Export = ew_Post("exporttype");
			} else {
				TiposVehiculos.ExportReturnUrl = ew_CurrentUrl();
			}
			gsExport = TiposVehiculos.Export; // Get export parameter, used in header
			gsExportFile = TiposVehiculos.TableVar; // Get export file, used in header			
			string Charset = (ew_NotEmpty(EW_CHARSET)) ? ";charset=" + EW_CHARSET : ""; // Charset used in header

			// Write BOM 
			if (TiposVehiculos.Export == "excel" || TiposVehiculos.Export == "word" || TiposVehiculos.Export == "csv")
				HttpContext.Current.Response.BinaryWrite(new byte[]{0xEF, 0xBB, 0xBF});
			if (TiposVehiculos.Export == "excel") {
				HttpContext.Current.Response.ContentType = "application/vnd.ms-excel" + Charset;
				ew_AddHeader("Content-Disposition", "attachment; filename=" + gsExportFile + ".xls");
			}

			// Get grid add count
			int gridaddcnt = ew_ConvertToInt(ew_Get(EW_TABLE_GRID_ADD_ROW_COUNT));
			if (gridaddcnt > 0)
				TiposVehiculos.GridAddRowCount = gridaddcnt;

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
			TiposVehiculos.Dispose();
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
			if (ew_NotEmpty(TiposVehiculos.Export) ||
				TiposVehiculos.CurrentAction == "gridadd" ||
				TiposVehiculos.CurrentAction == "gridedit") {
				ListOptions.HideAllOptions();
				ExportOptions.HideAllOptions();
			}

			// Set Up Sorting Order
			SetUpSortOrder();
		}

		// Restore display records
		if (TiposVehiculos.RecordsPerPage == -1 || TiposVehiculos.RecordsPerPage > 0) {
			DisplayRecs = TiposVehiculos.RecordsPerPage; // Restore from Session
		} else {
			DisplayRecs = 20; // Load default
		}

		// Load Sorting Order
		LoadSortOrder();

		// Build filter
		sFilter = "";
		if (!Security.CanList)
			sFilter = "(0=1)"; // Filter all records
		ew_AddFilter(ref sFilter, DbDetailFilter);
		ew_AddFilter(ref sFilter, SearchWhere);

		// Set up filter in Session
		TiposVehiculos.SessionWhere = sFilter;
		TiposVehiculos.CurrentFilter = "";

		// Export Data only
		if (TiposVehiculos.Export == "html" || TiposVehiculos.Export == "csv" || TiposVehiculos.Export == "word" || TiposVehiculos.Export == "excel" || TiposVehiculos.Export == "xml" || TiposVehiculos.Export == "pdf") {
			ExportData();
			Page_Terminate(""); // Clean up
			ew_End(); // Terminate response
		} else if (TiposVehiculos.Export == "email") {
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
			TiposVehiculos.CurrentOrder = ew_Get("order");
			TiposVehiculos.CurrentOrderType = ew_Get("ordertype");
			TiposVehiculos.UpdateSort(TiposVehiculos.TipoVehiculo, bCtrl); // TipoVehiculo
			TiposVehiculos.StartRecordNumber = 1; // Reset start position
		}
	}

	//
	// Load Sort Order parameters
	//
	public void LoadSortOrder()	{
		string sOrderBy = TiposVehiculos.SessionOrderBy; // Get order by from Session
		if (ew_Empty(sOrderBy)) {
			if (ew_NotEmpty(TiposVehiculos.SqlOrderBy)) {
				sOrderBy = TiposVehiculos.SqlOrderBy;
				TiposVehiculos.SessionOrderBy = sOrderBy;
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
				TiposVehiculos.SessionOrderBy = sOrderBy;
				TiposVehiculos.TipoVehiculo.Sort = "";
			}

			// Reset start position
			StartRec = 1;
			TiposVehiculos.StartRecordNumber = StartRec;
		}
	}

	//
	// Set up list options
	//
	public void SetupListOptions() {
		cListOption item;
		item = ListOptions.Add("view");
		item.CssStyle = "white-space: nowrap;";
		item.Visible = Security.CanView;
		item.OnLeft = true;
		item = ListOptions.Add("edit");
		item.CssStyle = "white-space: nowrap;";
		item.Visible = Security.CanEdit;
		item.OnLeft = true;
		item = ListOptions.Add("delete");
		item.CssStyle = "white-space: nowrap;";
		item.Visible = Security.CanDelete;
		item.OnLeft = true;
		ListOptions_Load();
	}

	// Render list options
	public void RenderListOptions() {
		cListOption oListOpt;
		ListOptions.LoadDefault();
		string links;
		if (Security.CanView && ListOptions.GetItem("view").Visible)
			ListOptions.GetItem("view").Body = "<a class=\"ewRowLink\" href=\"" + ViewUrl + "\">" + "<img src=\"aspximages/view.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ViewLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ViewLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		if (Security.CanEdit && ListOptions.GetItem("edit").Visible) {
			oListOpt = ListOptions.GetItem("edit");
			oListOpt.Body = "<a class=\"ewRowLink\" href=\"" + EditUrl + "\">" + "<img src=\"aspximages/edit.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		}
		if (Security.CanDelete && ListOptions.GetItem("delete").Visible)
			ListOptions.GetItem("delete").Body = "<a class=\"ewRowLink\"" + " onclick=\"ew_ClickDelete(this);return ew_ConfirmDelete(ewLanguage.Phrase('DeleteConfirmMsg'), this);\"" + " href=\"" + DeleteUrl + "\">" + "<img src=\"aspximages/delete.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("DeleteLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("DeleteLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
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
				TiposVehiculos.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				TiposVehiculos.StartRecordNumber = StartRec;
			}
		}
		StartRec = TiposVehiculos.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			TiposVehiculos.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			TiposVehiculos.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			TiposVehiculos.StartRecordNumber = StartRec;
		}
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = TiposVehiculos.CurrentFilter;
		TiposVehiculos.Recordset_Selecting(ref sFilter);
		TiposVehiculos.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = TiposVehiculos.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		
		try {
			string sCntSql = TiposVehiculos.SelectCountSQL;

			// Write SQL for debug
			if (EW_DEBUG_ENABLED)
				ew_SetDebugMsg(sCntSql);
			TotalRecs = Convert.ToInt32(Conn.ExecuteScalar(sCntSql));
		} catch {
			TotalRecs = -1;
		}

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
		TiposVehiculos.Recordset_Selected(Rs);
		return Rs;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = TiposVehiculos.KeyFilter;

		// Row Selecting event
		TiposVehiculos.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		TiposVehiculos.CurrentFilter = sFilter;
		string sSql = TiposVehiculos.SQL;

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
		TiposVehiculos.Row_Selected(ref row);
		TiposVehiculos.IdTipoVehiculo.DbValue = row["IdTipoVehiculo"];
		TiposVehiculos.TipoVehiculo.DbValue = row["TipoVehiculo"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(TiposVehiculos.GetKey("IdTipoVehiculo")))
			TiposVehiculos.IdTipoVehiculo.CurrentValue = TiposVehiculos.GetKey("IdTipoVehiculo"); // IdTipoVehiculo
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			TiposVehiculos.CurrentFilter = TiposVehiculos.KeyFilter;
			string sSql = TiposVehiculos.SQL;			
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
		ViewUrl = TiposVehiculos.ViewUrl;
		EditUrl = TiposVehiculos.EditUrl;
		InlineEditUrl = TiposVehiculos.InlineEditUrl;
		CopyUrl = TiposVehiculos.CopyUrl;
		InlineCopyUrl = TiposVehiculos.InlineCopyUrl;
		DeleteUrl = TiposVehiculos.DeleteUrl;

		// Row Rendering event
		TiposVehiculos.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdTipoVehiculo
		// TipoVehiculo
		//
		//  View  Row
		//

		if (TiposVehiculos.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdTipoVehiculo
				TiposVehiculos.IdTipoVehiculo.ViewValue = TiposVehiculos.IdTipoVehiculo.CurrentValue;
			TiposVehiculos.IdTipoVehiculo.ViewCustomAttributes = "";

			// TipoVehiculo
				TiposVehiculos.TipoVehiculo.ViewValue = TiposVehiculos.TipoVehiculo.CurrentValue;
			TiposVehiculos.TipoVehiculo.ViewCustomAttributes = "";

			// View refer script
			// TipoVehiculo

			TiposVehiculos.TipoVehiculo.LinkCustomAttributes = "";
			TiposVehiculos.TipoVehiculo.HrefValue = "";
			TiposVehiculos.TipoVehiculo.TooltipValue = "";
		}

		// Row Rendered event
		if (TiposVehiculos.RowType != EW_ROWTYPE_AGGREGATEINIT)
			TiposVehiculos.Row_Rendered();
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
		item.Body = "<a name=\"emf_TiposVehiculos\" id=\"emf_TiposVehiculos\" href=\"javascript:void(0);\" onclick=\"ew_EmailDialogShow({lnk:'emf_TiposVehiculos',hdr:ewLanguage.Phrase('ExportToEmail'),f:ew_GetForm('fTiposVehiculoslist'),sel:false});\">" + "<img src=\"aspximages/exportemail.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToEmail")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToEmail")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Hide options for export/action
		if (ew_NotEmpty(TiposVehiculos.Export) || ew_NotEmpty(TiposVehiculos.CurrentAction))
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
		if (TiposVehiculos.ExportAll) {
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
		if (TiposVehiculos.Export == "xml") {
			XmlDoc = new cXMLDocument();
		} else {
			ExportDoc = new cExportDocument(this, TiposVehiculos, "h");
		}
		string ParentTable = "";
		if (TiposVehiculos.Export == "xml") {
			TiposVehiculos.ExportXmlDocument(ref XmlDoc, ew_NotEmpty(ParentTable), ref Rs, StartRec, StopRec, "");
		} else {
			string sHeader = PageHeader;
			Page_DataRendering(ref sHeader);
			ExportDoc.Text += sHeader;
			TiposVehiculos.ExportDocument(ref ExportDoc, ref Rs, StartRec, StopRec, "");
			string sFooter = PageFooter;
			Page_DataRendered(ref sFooter);
			ExportDoc.Text += sFooter;
		}

		// Close recordset
		Rs.Close();
		Rs.Dispose();

		// Export header and footer
		if (TiposVehiculos.Export != "xml")
			ExportDoc.ExportHeaderAndFooter();

		// Clean output buffer
		if (!EW_DEBUG_ENABLED)
			HttpContext.Current.Response.Clear();

		// Write BOM if utf-8
		if (utf8 && TiposVehiculos.Export != "email" && TiposVehiculos.Export != "xml")
			HttpContext.Current.Response.BinaryWrite(new byte[]{0xEF, 0xBB, 0xBF});

		// Write debug message if enabled
		if (EW_DEBUG_ENABLED)
			ew_Write(ew_DebugMsg());

		// Output data
		if (TiposVehiculos.Export == "xml") {
			ew_AddHeader("Content-Type", "text/xml");
			ew_Write(XmlDoc.XML());
		} else if (TiposVehiculos.Export == "pdf") {
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

			//url = newurl;
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
		TiposVehiculos_list = new cTiposVehiculos_list(this);
		CurrentPage = TiposVehiculos_list;

		//CurrentPageType = TiposVehiculos_list.GetType();
		TiposVehiculos_list.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		TiposVehiculos_list.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (TiposVehiculos_list != null)
			TiposVehiculos_list.Dispose();
	}
}
