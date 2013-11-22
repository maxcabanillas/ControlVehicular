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

partial class RegistrosPeatoneslist: AspNetMaker9_ControlVehicular {

	// Page object
	public cRegistrosPeatones_list RegistrosPeatones_list;	

	//
	// Page Class
	//
	public class cRegistrosPeatones_list: AspNetMakerPage, IDisposable {

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
				if (RegistrosPeatones.UseTokenInUrl)
					Url += "t=" + RegistrosPeatones.TableVar + "&"; // Add page token
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
			if (RegistrosPeatones.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (RegistrosPeatones.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (RegistrosPeatones.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public RegistrosPeatoneslist AspNetPage { 
			get { return (RegistrosPeatoneslist)m_ParentPage; }
		}

		// RegistrosPeatones	
		public cRegistrosPeatones RegistrosPeatones { 
			get {				
				return ParentPage.RegistrosPeatones;
			}
			set {
				ParentPage.RegistrosPeatones = value;	
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
		public cRegistrosPeatones_list(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "list";
			m_PageObjName = "RegistrosPeatones_list";
			m_PageObjTypeName = "cRegistrosPeatones_list";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (RegistrosPeatones == null)
				RegistrosPeatones = new cRegistrosPeatones(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "RegistrosPeatones";
			m_Table = RegistrosPeatones;
			CurrentTable = RegistrosPeatones;

			//CurrentTableType = RegistrosPeatones.GetType();
			// Initialize URLs

			ExportPrintUrl = PageUrl + "export=print";
			ExportExcelUrl = PageUrl + "export=excel";
			ExportWordUrl = PageUrl + "export=word";
			ExportHtmlUrl = PageUrl + "export=html";
			ExportXmlUrl = PageUrl + "export=xml";
			ExportCsvUrl = PageUrl + "export=csv";
			ExportPdfUrl = PageUrl + "export=pdf";
			AddUrl = "RegistrosPeatonesadd.aspx";
			InlineAddUrl = PageUrl + "a=add";
			GridAddUrl = PageUrl + "a=gridadd";
			GridEditUrl = PageUrl + "a=gridedit";
			MultiDeleteUrl = "RegistrosPeatonesdelete.aspx";
			MultiUpdateUrl = "RegistrosPeatonesupdate.aspx";

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

			// Get grid add count
			int gridaddcnt = ew_ConvertToInt(ew_Get(EW_TABLE_GRID_ADD_ROW_COUNT));
			if (gridaddcnt > 0)
				RegistrosPeatones.GridAddRowCount = gridaddcnt;

			// Set up list options
			SetupListOptions();

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
			RegistrosPeatones.Dispose();
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
		DisplayRecs = 10;
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
			if (ew_NotEmpty(RegistrosPeatones.Export) ||
				RegistrosPeatones.CurrentAction == "gridadd" ||
				RegistrosPeatones.CurrentAction == "gridedit") {
				ListOptions.HideAllOptions();
				ExportOptions.HideAllOptions();
			}

			// Set Up Sorting Order
			SetUpSortOrder();
		}

		// Restore display records
		if (RegistrosPeatones.RecordsPerPage == -1 || RegistrosPeatones.RecordsPerPage > 0) {
			DisplayRecs = RegistrosPeatones.RecordsPerPage; // Restore from Session
		} else {
			DisplayRecs = 10; // Load default
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
		RegistrosPeatones.SessionWhere = sFilter;
		RegistrosPeatones.CurrentFilter = "";
	}

	//
	// Set up Sort parameters based on Sort Links clicked
	//
	public void SetUpSortOrder() {

		// Check for an Order parameter
		if (ew_NotEmpty(ew_Get("order"))) {
			RegistrosPeatones.CurrentOrder = ew_Get("order");
			RegistrosPeatones.CurrentOrderType = ew_Get("ordertype");
			RegistrosPeatones.UpdateSort(RegistrosPeatones.IdTipoDocumento); // IdTipoDocumento
			RegistrosPeatones.UpdateSort(RegistrosPeatones.Documento); // Documento
			RegistrosPeatones.UpdateSort(RegistrosPeatones.Nombre); // Nombre
			RegistrosPeatones.UpdateSort(RegistrosPeatones.Apellidos); // Apellidos
			RegistrosPeatones.UpdateSort(RegistrosPeatones.IdArea); // IdArea
			RegistrosPeatones.UpdateSort(RegistrosPeatones.IdPersona); // IdPersona
			RegistrosPeatones.UpdateSort(RegistrosPeatones.FechaIngreso); // FechaIngreso
			RegistrosPeatones.UpdateSort(RegistrosPeatones.FechaSalida); // FechaSalida
			RegistrosPeatones.StartRecordNumber = 1; // Reset start position
		}
	}

	//
	// Load Sort Order parameters
	//
	public void LoadSortOrder()	{
		string sOrderBy = RegistrosPeatones.SessionOrderBy; // Get order by from Session
		if (ew_Empty(sOrderBy)) {
			if (ew_NotEmpty(RegistrosPeatones.SqlOrderBy)) {
				sOrderBy = RegistrosPeatones.SqlOrderBy;
				RegistrosPeatones.SessionOrderBy = sOrderBy;
				RegistrosPeatones.FechaIngreso.Sort = "ASC";
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
				RegistrosPeatones.SessionOrderBy = sOrderBy;
				RegistrosPeatones.SessionOrderByList = sOrderBy;
				RegistrosPeatones.IdTipoDocumento.Sort = "";
				RegistrosPeatones.Documento.Sort = "";
				RegistrosPeatones.Nombre.Sort = "";
				RegistrosPeatones.Apellidos.Sort = "";
				RegistrosPeatones.IdArea.Sort = "";
				RegistrosPeatones.IdPersona.Sort = "";
				RegistrosPeatones.FechaIngreso.Sort = "";
				RegistrosPeatones.FechaSalida.Sort = "";
			}

			// Reset start position
			StartRec = 1;
			RegistrosPeatones.StartRecordNumber = StartRec;
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
				RegistrosPeatones.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				RegistrosPeatones.StartRecordNumber = StartRec;
			}
		}
		StartRec = RegistrosPeatones.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			RegistrosPeatones.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			RegistrosPeatones.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			RegistrosPeatones.StartRecordNumber = StartRec;
		}
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = RegistrosPeatones.CurrentFilter;
		RegistrosPeatones.Recordset_Selecting(ref sFilter);
		RegistrosPeatones.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = RegistrosPeatones.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		
		try {
			string sCntSql = RegistrosPeatones.SelectCountSQL;

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
		RegistrosPeatones.Recordset_Selected(Rs);
		return Rs;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = RegistrosPeatones.KeyFilter;

		// Row Selecting event
		RegistrosPeatones.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		RegistrosPeatones.CurrentFilter = sFilter;
		string sSql = RegistrosPeatones.SQL;

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
		RegistrosPeatones.Row_Selected(ref row);
		RegistrosPeatones.IdRegistroPeaton.DbValue = row["IdRegistroPeaton"];
		RegistrosPeatones.IdTipoDocumento.DbValue = row["IdTipoDocumento"];
		RegistrosPeatones.Documento.DbValue = row["Documento"];
		RegistrosPeatones.Nombre.DbValue = row["Nombre"];
		RegistrosPeatones.Apellidos.DbValue = row["Apellidos"];
		RegistrosPeatones.IdArea.DbValue = row["IdArea"];
		RegistrosPeatones.IdPersona.DbValue = row["IdPersona"];
		if (row.Contains("EV__IdPersona")) {
			RegistrosPeatones.IdPersona.VirtualValue = row["EV__IdPersona"]; // Set up virtual field value
		} else {
			RegistrosPeatones.IdPersona.VirtualValue = ""; // Clear value
		}
		RegistrosPeatones.FechaIngreso.DbValue = row["FechaIngreso"];
		RegistrosPeatones.FechaSalida.DbValue = row["FechaSalida"];
		RegistrosPeatones.Observacion.DbValue = row["Observacion"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(RegistrosPeatones.GetKey("IdRegistroPeaton")))
			RegistrosPeatones.IdRegistroPeaton.CurrentValue = RegistrosPeatones.GetKey("IdRegistroPeaton"); // IdRegistroPeaton
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			RegistrosPeatones.CurrentFilter = RegistrosPeatones.KeyFilter;
			string sSql = RegistrosPeatones.SQL;			
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
		ViewUrl = RegistrosPeatones.ViewUrl;
		EditUrl = RegistrosPeatones.EditUrl;
		InlineEditUrl = RegistrosPeatones.InlineEditUrl;
		CopyUrl = RegistrosPeatones.CopyUrl;
		InlineCopyUrl = RegistrosPeatones.InlineCopyUrl;
		DeleteUrl = RegistrosPeatones.DeleteUrl;

		// Row Rendering event
		RegistrosPeatones.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdRegistroPeaton
		// IdTipoDocumento
		// Documento
		// Nombre
		// Apellidos
		// IdArea
		// IdPersona
		// FechaIngreso
		// FechaSalida
		// Observacion
		//
		//  View  Row
		//

		if (RegistrosPeatones.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdRegistroPeaton
				RegistrosPeatones.IdRegistroPeaton.ViewValue = RegistrosPeatones.IdRegistroPeaton.CurrentValue;
			RegistrosPeatones.IdRegistroPeaton.ViewCustomAttributes = "";

			// IdTipoDocumento
			if (ew_NotEmpty(RegistrosPeatones.IdTipoDocumento.CurrentValue)) {
				sFilterWrk = "[IdTipoDocumento] = " + ew_AdjustSql(RegistrosPeatones.IdTipoDocumento.CurrentValue) + "";
			sSqlWrk = "SELECT [TipoDocumento] FROM [dbo].[TiposDocumentos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoDocumento]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					RegistrosPeatones.IdTipoDocumento.ViewValue = drWrk["TipoDocumento"];
				} else {
					RegistrosPeatones.IdTipoDocumento.ViewValue = RegistrosPeatones.IdTipoDocumento.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosPeatones.IdTipoDocumento.ViewValue = System.DBNull.Value;
			}
			RegistrosPeatones.IdTipoDocumento.ViewCustomAttributes = "";

			// Documento
				RegistrosPeatones.Documento.ViewValue = RegistrosPeatones.Documento.CurrentValue;
			RegistrosPeatones.Documento.ViewCustomAttributes = "";

			// Nombre
				RegistrosPeatones.Nombre.ViewValue = RegistrosPeatones.Nombre.CurrentValue;
			RegistrosPeatones.Nombre.ViewCustomAttributes = "";

			// Apellidos
				RegistrosPeatones.Apellidos.ViewValue = RegistrosPeatones.Apellidos.CurrentValue;
			RegistrosPeatones.Apellidos.ViewCustomAttributes = "";

			// IdArea
			if (ew_NotEmpty(RegistrosPeatones.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(RegistrosPeatones.IdArea.CurrentValue) + "";
			sSqlWrk = "SELECT [Area], [Codigo] FROM [dbo].[Areas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Area]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					RegistrosPeatones.IdArea.ViewValue = drWrk["Area"];
					RegistrosPeatones.IdArea.ViewValue = String.Concat(RegistrosPeatones.IdArea.ViewValue, ew_ValueSeparator(0, 1, RegistrosPeatones.IdArea), drWrk["Codigo"]);
				} else {
					RegistrosPeatones.IdArea.ViewValue = RegistrosPeatones.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosPeatones.IdArea.ViewValue = System.DBNull.Value;
			}
			RegistrosPeatones.IdArea.ViewCustomAttributes = "";

			// IdPersona
			if (ew_NotEmpty(RegistrosPeatones.IdPersona.VirtualValue)) {
				RegistrosPeatones.IdPersona.ViewValue = RegistrosPeatones.IdPersona.VirtualValue;
			} else {
			if (ew_NotEmpty(RegistrosPeatones.IdPersona.CurrentValue)) {
				sFilterWrk = "[IdPersona] = " + ew_AdjustSql(RegistrosPeatones.IdPersona.CurrentValue) + "";
			sSqlWrk = "SELECT [IdPersona], [Persona], [Documento], [Activa] FROM [dbo].[Personas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Persona]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					RegistrosPeatones.IdPersona.ViewValue = drWrk["IdPersona"];
					RegistrosPeatones.IdPersona.ViewValue = String.Concat(RegistrosPeatones.IdPersona.ViewValue, ew_ValueSeparator(0, 1, RegistrosPeatones.IdPersona), drWrk["Persona"]);
					RegistrosPeatones.IdPersona.ViewValue = String.Concat(RegistrosPeatones.IdPersona.ViewValue, ew_ValueSeparator(0, 2, RegistrosPeatones.IdPersona), drWrk["Documento"]);
					RegistrosPeatones.IdPersona.ViewValue = String.Concat(RegistrosPeatones.IdPersona.ViewValue, ew_ValueSeparator(0, 3, RegistrosPeatones.IdPersona), drWrk["Activa"]);
				} else {
					RegistrosPeatones.IdPersona.ViewValue = RegistrosPeatones.IdPersona.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosPeatones.IdPersona.ViewValue = System.DBNull.Value;
			}
			}
			RegistrosPeatones.IdPersona.ViewCustomAttributes = "";

			// FechaIngreso
				RegistrosPeatones.FechaIngreso.ViewValue = RegistrosPeatones.FechaIngreso.CurrentValue;
				RegistrosPeatones.FechaIngreso.ViewValue = ew_FormatDateTime(RegistrosPeatones.FechaIngreso.ViewValue, 7);
			RegistrosPeatones.FechaIngreso.ViewCustomAttributes = "";

			// FechaSalida
				RegistrosPeatones.FechaSalida.ViewValue = RegistrosPeatones.FechaSalida.CurrentValue;
				RegistrosPeatones.FechaSalida.ViewValue = ew_FormatDateTime(RegistrosPeatones.FechaSalida.ViewValue, 7);
			RegistrosPeatones.FechaSalida.ViewCustomAttributes = "";

			// View refer script
			// IdTipoDocumento

			RegistrosPeatones.IdTipoDocumento.LinkCustomAttributes = "";
			RegistrosPeatones.IdTipoDocumento.HrefValue = "";
			RegistrosPeatones.IdTipoDocumento.TooltipValue = "";

			// Documento
			RegistrosPeatones.Documento.LinkCustomAttributes = "";
			RegistrosPeatones.Documento.HrefValue = "";
			RegistrosPeatones.Documento.TooltipValue = "";

			// Nombre
			RegistrosPeatones.Nombre.LinkCustomAttributes = "";
			RegistrosPeatones.Nombre.HrefValue = "";
			RegistrosPeatones.Nombre.TooltipValue = "";

			// Apellidos
			RegistrosPeatones.Apellidos.LinkCustomAttributes = "";
			RegistrosPeatones.Apellidos.HrefValue = "";
			RegistrosPeatones.Apellidos.TooltipValue = "";

			// IdArea
			RegistrosPeatones.IdArea.LinkCustomAttributes = "";
			RegistrosPeatones.IdArea.HrefValue = "";
			RegistrosPeatones.IdArea.TooltipValue = "";

			// IdPersona
			RegistrosPeatones.IdPersona.LinkCustomAttributes = "";
			RegistrosPeatones.IdPersona.HrefValue = "";
			RegistrosPeatones.IdPersona.TooltipValue = "";

			// FechaIngreso
			RegistrosPeatones.FechaIngreso.LinkCustomAttributes = "";
			RegistrosPeatones.FechaIngreso.HrefValue = "";
			RegistrosPeatones.FechaIngreso.TooltipValue = "";

			// FechaSalida
			RegistrosPeatones.FechaSalida.LinkCustomAttributes = "";
			RegistrosPeatones.FechaSalida.HrefValue = "";
			RegistrosPeatones.FechaSalida.TooltipValue = "";
		}

		// Row Rendered event
		if (RegistrosPeatones.RowType != EW_ROWTYPE_AGGREGATEINIT)
			RegistrosPeatones.Row_Rendered();
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
		RegistrosPeatones_list = new cRegistrosPeatones_list(this);
		CurrentPage = RegistrosPeatones_list;

		//CurrentPageType = RegistrosPeatones_list.GetType();
		RegistrosPeatones_list.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		RegistrosPeatones_list.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (RegistrosPeatones_list != null)
			RegistrosPeatones_list.Dispose();
	}
}
