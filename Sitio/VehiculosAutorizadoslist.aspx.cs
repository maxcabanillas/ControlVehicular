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

partial class VehiculosAutorizadoslist: AspNetMaker9_ControlVehicular {

	// Page object
	public cVehiculosAutorizados_list VehiculosAutorizados_list;	

	//
	// Page Class
	//
	public class cVehiculosAutorizados_list: AspNetMakerPage, IDisposable {

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
				if (VehiculosAutorizados.UseTokenInUrl)
					Url += "t=" + VehiculosAutorizados.TableVar + "&"; // Add page token
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
			if (VehiculosAutorizados.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (VehiculosAutorizados.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (VehiculosAutorizados.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public VehiculosAutorizadoslist AspNetPage { 
			get { return (VehiculosAutorizadoslist)m_ParentPage; }
		}

		// VehiculosAutorizados	
		public cVehiculosAutorizados VehiculosAutorizados { 
			get {				
				return ParentPage.VehiculosAutorizados;
			}
			set {
				ParentPage.VehiculosAutorizados = value;	
			}	
		}		

		// Personas	
		public cPersonas Personas { 
			get {				
				return ParentPage.Personas;
			}
			set {
				ParentPage.Personas = value;	
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

		// VehiculosPicoYPlacaHoras	
		public cVehiculosPicoYPlacaHoras VehiculosPicoYPlacaHoras { 
			get {				
				return ParentPage.VehiculosPicoYPlacaHoras;
			}
			set {
				ParentPage.VehiculosPicoYPlacaHoras = value;	
			}	
		}		

		//
		//  Page class constructor
		//
		public cVehiculosAutorizados_list(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "list";
			m_PageObjName = "VehiculosAutorizados_list";
			m_PageObjTypeName = "cVehiculosAutorizados_list";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (VehiculosAutorizados == null)
				VehiculosAutorizados = new cVehiculosAutorizados(this);
			if (Personas == null)
				Personas = new cPersonas(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);
			if (VehiculosPicoYPlacaHoras == null)
				VehiculosPicoYPlacaHoras = new cVehiculosPicoYPlacaHoras(this);

			// Table
			m_TableName = "VehiculosAutorizados";
			m_Table = VehiculosAutorizados;
			CurrentTable = VehiculosAutorizados;

			//CurrentTableType = VehiculosAutorizados.GetType();
			// Initialize URLs

			ExportPrintUrl = PageUrl + "export=print";
			ExportExcelUrl = PageUrl + "export=excel";
			ExportWordUrl = PageUrl + "export=word";
			ExportHtmlUrl = PageUrl + "export=html";
			ExportXmlUrl = PageUrl + "export=xml";
			ExportCsvUrl = PageUrl + "export=csv";
			ExportPdfUrl = PageUrl + "export=pdf";
			AddUrl = "VehiculosAutorizadosadd.aspx?" + EW_TABLE_SHOW_DETAIL + "=";
			InlineAddUrl = PageUrl + "a=add";
			GridAddUrl = PageUrl + "a=gridadd";
			GridEditUrl = PageUrl + "a=gridedit";
			MultiDeleteUrl = "VehiculosAutorizadosdelete.aspx";
			MultiUpdateUrl = "VehiculosAutorizadosupdate.aspx";

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
				VehiculosAutorizados.Export = ew_Get("export");
			} else if (ew_NotEmpty(ew_Post("exporttype"))) {
				VehiculosAutorizados.Export = ew_Post("exporttype");
			} else {
				VehiculosAutorizados.ExportReturnUrl = ew_CurrentUrl();
			}
			gsExport = VehiculosAutorizados.Export; // Get export parameter, used in header
			gsExportFile = VehiculosAutorizados.TableVar; // Get export file, used in header			
			string Charset = (ew_NotEmpty(EW_CHARSET)) ? ";charset=" + EW_CHARSET : ""; // Charset used in header

			// Write BOM 
			if (VehiculosAutorizados.Export == "excel" || VehiculosAutorizados.Export == "word" || VehiculosAutorizados.Export == "csv")
				HttpContext.Current.Response.BinaryWrite(new byte[]{0xEF, 0xBB, 0xBF});
			if (VehiculosAutorizados.Export == "excel") {
				HttpContext.Current.Response.ContentType = "application/vnd.ms-excel" + Charset;
				ew_AddHeader("Content-Disposition", "attachment; filename=" + gsExportFile + ".xls");
			}

			// Get grid add count
			int gridaddcnt = ew_ConvertToInt(ew_Get(EW_TABLE_GRID_ADD_ROW_COUNT));
			if (gridaddcnt > 0)
				VehiculosAutorizados.GridAddRowCount = gridaddcnt;

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
			VehiculosAutorizados.Dispose();
			Personas.Dispose();
			Usuarios.Dispose();
			VehiculosPicoYPlacaHoras.Dispose();
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

			// Set up master detail parameters
			SetUpMasterParms();

			// Hide all options
			if (ew_NotEmpty(VehiculosAutorizados.Export) ||
				VehiculosAutorizados.CurrentAction == "gridadd" ||
				VehiculosAutorizados.CurrentAction == "gridedit") {
				ListOptions.HideAllOptions();
				ExportOptions.HideAllOptions();
			}

			// Set Up Sorting Order
			SetUpSortOrder();
		}

		// Restore display records
		if (VehiculosAutorizados.RecordsPerPage == -1 || VehiculosAutorizados.RecordsPerPage > 0) {
			DisplayRecs = VehiculosAutorizados.RecordsPerPage; // Restore from Session
		} else {
			DisplayRecs = 20; // Load default
		}

		// Load Sorting Order
		LoadSortOrder();

		// Build filter
		sFilter = "";
		if (!Security.CanList)
			sFilter = "(0=1)"; // Filter all records

		// Restore master/detail filter
		DbMasterFilter = VehiculosAutorizados.MasterFilter; // Restore master filter
		DbDetailFilter = VehiculosAutorizados.DetailFilter; // Restore detail filter
		ew_AddFilter(ref sFilter, DbDetailFilter);
		ew_AddFilter(ref sFilter, SearchWhere);
		SqlDataReader RsMaster; 

		// Load master record
		if (VehiculosAutorizados.CurrentMode != "add" && ew_NotEmpty(VehiculosAutorizados.MasterFilter) && VehiculosAutorizados.CurrentMasterTable == "Personas") {
			RsMaster = Personas.LoadRs(DbMasterFilter);
			MasterRecordExists = RsMaster != null;
			if (!MasterRecordExists) {
				FailureMessage = Language.Phrase("NoRecord"); // Set no record found
				Page_Terminate(VehiculosAutorizados.ReturnUrl); // Return to caller
			} else {
				RsMaster.Read();
				Personas.LoadListRowValues(ref RsMaster);
				Personas.RowType = EW_ROWTYPE_MASTER; // Master row
				Personas.RenderListRow();
				RsMaster.Close();
				RsMaster.Dispose();
			}
		}

		// Set up filter in Session
		VehiculosAutorizados.SessionWhere = sFilter;
		VehiculosAutorizados.CurrentFilter = "";

		// Export Data only
		if (VehiculosAutorizados.Export == "html" || VehiculosAutorizados.Export == "csv" || VehiculosAutorizados.Export == "word" || VehiculosAutorizados.Export == "excel" || VehiculosAutorizados.Export == "xml" || VehiculosAutorizados.Export == "pdf") {
			ExportData();
			Page_Terminate(""); // Clean up
			ew_End(); // Terminate response
		} else if (VehiculosAutorizados.Export == "email") {
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
			VehiculosAutorizados.CurrentOrder = ew_Get("order");
			VehiculosAutorizados.CurrentOrderType = ew_Get("ordertype");
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.IdTipoVehiculo, bCtrl); // IdTipoVehiculo
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Placa, bCtrl); // Placa
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Autorizado, bCtrl); // Autorizado
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.PicoyPlaca, bCtrl); // PicoyPlaca
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Lunes, bCtrl); // Lunes
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Martes, bCtrl); // Martes
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Miercoles, bCtrl); // Miercoles
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Jueves, bCtrl); // Jueves
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Viernes, bCtrl); // Viernes
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Sabado, bCtrl); // Sabado
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Domingo, bCtrl); // Domingo
			VehiculosAutorizados.UpdateSort(VehiculosAutorizados.Marca, bCtrl); // Marca
			VehiculosAutorizados.StartRecordNumber = 1; // Reset start position
		}
	}

	//
	// Load Sort Order parameters
	//
	public void LoadSortOrder()	{
		string sOrderBy = VehiculosAutorizados.SessionOrderBy; // Get order by from Session
		if (ew_Empty(sOrderBy)) {
			if (ew_NotEmpty(VehiculosAutorizados.SqlOrderBy)) {
				sOrderBy = VehiculosAutorizados.SqlOrderBy;
				VehiculosAutorizados.SessionOrderBy = sOrderBy;
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

			// Reset master/detail keys
			if (ew_SameText(sCmd, "resetall")) {
				VehiculosAutorizados.CurrentMasterTable = ""; // Clear master table
				DbMasterFilter = "";
				DbDetailFilter = "";
				VehiculosAutorizados.IdPersona.SessionValue = "";
			}

			// Reset sort criteria
			if (ew_SameText(sCmd, "resetsort")) {
				string sOrderBy = "";
				VehiculosAutorizados.SessionOrderBy = sOrderBy;
				VehiculosAutorizados.IdTipoVehiculo.Sort = "";
				VehiculosAutorizados.Placa.Sort = "";
				VehiculosAutorizados.Autorizado.Sort = "";
				VehiculosAutorizados.PicoyPlaca.Sort = "";
				VehiculosAutorizados.Lunes.Sort = "";
				VehiculosAutorizados.Martes.Sort = "";
				VehiculosAutorizados.Miercoles.Sort = "";
				VehiculosAutorizados.Jueves.Sort = "";
				VehiculosAutorizados.Viernes.Sort = "";
				VehiculosAutorizados.Sabado.Sort = "";
				VehiculosAutorizados.Domingo.Sort = "";
				VehiculosAutorizados.Marca.Sort = "";
			}

			// Reset start position
			StartRec = 1;
			VehiculosAutorizados.StartRecordNumber = StartRec;
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
		item = ListOptions.Add("detail_VehiculosPicoYPlacaHoras");
		item.CssStyle = "white-space: nowrap;";
		item.Visible = Security.AllowList("VehiculosAutorizados");
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

		// "detail_VehiculosPicoYPlacaHoras"
		oListOpt = ListOptions.GetItem("detail_VehiculosPicoYPlacaHoras");
		if (Security.AllowList("VehiculosPicoYPlacaHoras")) {
			oListOpt.Body = "<img src=\"aspximages/detail.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("DetailLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("DetailLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + Language.TablePhrase("VehiculosPicoYPlacaHoras", "TblCaption");
			oListOpt.Body = "<a class=\"ewRowLink\" href=\"VehiculosPicoYPlacaHoraslist.aspx?" + EW_TABLE_SHOW_MASTER + "=VehiculosAutorizados&IdVehiculoAutorizado=" + ew_UrlEncode(Convert.ToString(VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue)) + "\">" + oListOpt.Body + "</a>";
			links = "";
			if (VehiculosPicoYPlacaHoras.DetailEdit && Security.CanEdit && Security.AllowEdit("VehiculosPicoYPlacaHoras"))
				links += "<a class=\"ewRowLink\" href=\"" + VehiculosAutorizados.GetEditUrl(EW_TABLE_SHOW_DETAIL + "=VehiculosPicoYPlacaHoras") + "\">" + "<img src=\"aspximages/edit.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>&nbsp;";
			if (ew_NotEmpty(links)) oListOpt.Body += "<br />" + links;
		}
		RenderListOptionsExt();
		ListOptions_Rendered();
	}

	public void RenderListOptionsExt() {
		string sHyperLinkParm = "";
		cListOption oListOpt; 
		string links = "";
		sSqlWrk = "[IdVehiculoAutorizado]=" + ew_AdjustSql(VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue) + "";
		sSqlWrk = cTEA.Encrypt(sSqlWrk, EW_RANDOM_KEY);
		sSqlWrk = sSqlWrk.Replace("'", "\'");
		oListOpt = ListOptions.GetItem("detail_VehiculosPicoYPlacaHoras");
		oListOpt.Body = "<img src=\"aspximages/detail.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("DetailLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("DetailLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + Language.TablePhrase("VehiculosPicoYPlacaHoras", "TblCaption");
		sHyperLinkParm = " href=\"VehiculosPicoYPlacaHoraslist.aspx?" + EW_TABLE_SHOW_MASTER + "=VehiculosAutorizados&IdVehiculoAutorizado=" + ew_UrlEncode(Convert.ToString(VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue)) + "\" name=\"dl%i_VehiculosAutorizados_VehiculosPicoYPlacaHoras\" id=\"dl%i_VehiculosAutorizados_VehiculosPicoYPlacaHoras\" onmouseover=\"ew_AjaxShowDetails(this, 'VehiculosPicoYPlacaHoraspreview.aspx?f=%s')\" onmouseout=\"ew_AjaxHideDetails(this);\"";
		sHyperLinkParm = sHyperLinkParm.Replace("%i", Convert.ToString(RowCnt));
		sHyperLinkParm = sHyperLinkParm.Replace("%s", sSqlWrk);
		oListOpt.Body = "<a class=\"ewRowLink\"" + sHyperLinkParm + ">" + oListOpt.Body + "</a>";
		links = "";
		if (VehiculosPicoYPlacaHoras.DetailEdit && Security.CanEdit && Security.AllowEdit("VehiculosPicoYPlacaHoras"))
			links += "<a class=\"ewRowLink\" href=\"" + VehiculosAutorizados.GetEditUrl(EW_TABLE_SHOW_DETAIL + "=VehiculosPicoYPlacaHoras") + "\">" +  "<img src=\"aspximages/edit.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>&nbsp;";
		if (ew_NotEmpty(links)) oListOpt.Body += "<br>" + links;
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
				VehiculosAutorizados.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				VehiculosAutorizados.StartRecordNumber = StartRec;
			}
		}
		StartRec = VehiculosAutorizados.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			VehiculosAutorizados.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			VehiculosAutorizados.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			VehiculosAutorizados.StartRecordNumber = StartRec;
		}
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = VehiculosAutorizados.CurrentFilter;
		VehiculosAutorizados.Recordset_Selecting(ref sFilter);
		VehiculosAutorizados.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = VehiculosAutorizados.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		
		try {
			string sCntSql = VehiculosAutorizados.SelectCountSQL;

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
		VehiculosAutorizados.Recordset_Selected(Rs);
		return Rs;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = VehiculosAutorizados.KeyFilter;

		// Row Selecting event
		VehiculosAutorizados.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		VehiculosAutorizados.CurrentFilter = sFilter;
		string sSql = VehiculosAutorizados.SQL;

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
		VehiculosAutorizados.Row_Selected(ref row);
		VehiculosAutorizados.IdVehiculoAutorizado.DbValue = row["IdVehiculoAutorizado"];
		VehiculosAutorizados.IdTipoVehiculo.DbValue = row["IdTipoVehiculo"];
		VehiculosAutorizados.Placa.DbValue = row["Placa"];
		VehiculosAutorizados.Autorizado.DbValue = (ew_ConvertToBool(row["Autorizado"])) ? "1" : "0";
		VehiculosAutorizados.IdPersona.DbValue = row["IdPersona"];
		VehiculosAutorizados.PicoyPlaca.DbValue = (ew_ConvertToBool(row["PicoyPlaca"])) ? "1" : "0";
		VehiculosAutorizados.Lunes.DbValue = (ew_ConvertToBool(row["Lunes"])) ? "1" : "0";
		VehiculosAutorizados.Martes.DbValue = (ew_ConvertToBool(row["Martes"])) ? "1" : "0";
		VehiculosAutorizados.Miercoles.DbValue = (ew_ConvertToBool(row["Miercoles"])) ? "1" : "0";
		VehiculosAutorizados.Jueves.DbValue = (ew_ConvertToBool(row["Jueves"])) ? "1" : "0";
		VehiculosAutorizados.Viernes.DbValue = (ew_ConvertToBool(row["Viernes"])) ? "1" : "0";
		VehiculosAutorizados.Sabado.DbValue = (ew_ConvertToBool(row["Sabado"])) ? "1" : "0";
		VehiculosAutorizados.Domingo.DbValue = (ew_ConvertToBool(row["Domingo"])) ? "1" : "0";
		VehiculosAutorizados.Marca.DbValue = row["Marca"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(VehiculosAutorizados.GetKey("IdVehiculoAutorizado")))
			VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue = VehiculosAutorizados.GetKey("IdVehiculoAutorizado"); // IdVehiculoAutorizado
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			VehiculosAutorizados.CurrentFilter = VehiculosAutorizados.KeyFilter;
			string sSql = VehiculosAutorizados.SQL;			
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
		ViewUrl = VehiculosAutorizados.ViewUrl;
		EditUrl = VehiculosAutorizados.EditUrl;
		InlineEditUrl = VehiculosAutorizados.InlineEditUrl;
		CopyUrl = VehiculosAutorizados.CopyUrl;
		InlineCopyUrl = VehiculosAutorizados.InlineCopyUrl;
		DeleteUrl = VehiculosAutorizados.DeleteUrl;

		// Row Rendering event
		VehiculosAutorizados.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdVehiculoAutorizado
		// IdTipoVehiculo
		// Placa
		// Autorizado
		// IdPersona
		// PicoyPlaca
		// Lunes
		// Martes
		// Miercoles
		// Jueves
		// Viernes
		// Sabado
		// Domingo
		// Marca
		//
		//  View  Row
		//

		if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdVehiculoAutorizado
				VehiculosAutorizados.IdVehiculoAutorizado.ViewValue = VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue;
			VehiculosAutorizados.IdVehiculoAutorizado.ViewCustomAttributes = "";

			// IdTipoVehiculo
			if (ew_NotEmpty(VehiculosAutorizados.IdTipoVehiculo.CurrentValue)) {
				sFilterWrk = "[IdTipoVehiculo] = " + ew_AdjustSql(VehiculosAutorizados.IdTipoVehiculo.CurrentValue) + "";
			sSqlWrk = "SELECT [TipoVehiculo] FROM [dbo].[TiposVehiculos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoVehiculo]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					VehiculosAutorizados.IdTipoVehiculo.ViewValue = drWrk["TipoVehiculo"];
				} else {
					VehiculosAutorizados.IdTipoVehiculo.ViewValue = VehiculosAutorizados.IdTipoVehiculo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				VehiculosAutorizados.IdTipoVehiculo.ViewValue = System.DBNull.Value;
			}
			VehiculosAutorizados.IdTipoVehiculo.ViewCustomAttributes = "";

			// Placa
				VehiculosAutorizados.Placa.ViewValue = VehiculosAutorizados.Placa.CurrentValue;
			VehiculosAutorizados.Placa.ViewCustomAttributes = "";

			// Autorizado
			if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") {
				VehiculosAutorizados.Autorizado.ViewValue = (VehiculosAutorizados.Autorizado.FldTagCaption(1) != "") ? VehiculosAutorizados.Autorizado.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Autorizado.ViewValue = (VehiculosAutorizados.Autorizado.FldTagCaption(2) != "") ? VehiculosAutorizados.Autorizado.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Autorizado.ViewCustomAttributes = "";

			// IdPersona
			VehiculosAutorizados.IdPersona.ViewCustomAttributes = "";

			// PicoyPlaca
			if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") {
				VehiculosAutorizados.PicoyPlaca.ViewValue = (VehiculosAutorizados.PicoyPlaca.FldTagCaption(1) != "") ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.PicoyPlaca.ViewValue = (VehiculosAutorizados.PicoyPlaca.FldTagCaption(2) != "") ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.PicoyPlaca.ViewCustomAttributes = "";

			// Lunes
			if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") {
				VehiculosAutorizados.Lunes.ViewValue = (VehiculosAutorizados.Lunes.FldTagCaption(1) != "") ? VehiculosAutorizados.Lunes.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Lunes.ViewValue = (VehiculosAutorizados.Lunes.FldTagCaption(2) != "") ? VehiculosAutorizados.Lunes.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Lunes.ViewCustomAttributes = "";

			// Martes
			if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") {
				VehiculosAutorizados.Martes.ViewValue = (VehiculosAutorizados.Martes.FldTagCaption(1) != "") ? VehiculosAutorizados.Martes.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Martes.ViewValue = (VehiculosAutorizados.Martes.FldTagCaption(2) != "") ? VehiculosAutorizados.Martes.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Martes.ViewCustomAttributes = "";

			// Miercoles
			if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") {
				VehiculosAutorizados.Miercoles.ViewValue = (VehiculosAutorizados.Miercoles.FldTagCaption(1) != "") ? VehiculosAutorizados.Miercoles.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Miercoles.ViewValue = (VehiculosAutorizados.Miercoles.FldTagCaption(2) != "") ? VehiculosAutorizados.Miercoles.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Miercoles.ViewCustomAttributes = "";

			// Jueves
			if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") {
				VehiculosAutorizados.Jueves.ViewValue = (VehiculosAutorizados.Jueves.FldTagCaption(1) != "") ? VehiculosAutorizados.Jueves.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Jueves.ViewValue = (VehiculosAutorizados.Jueves.FldTagCaption(2) != "") ? VehiculosAutorizados.Jueves.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Jueves.ViewCustomAttributes = "";

			// Viernes
			if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") {
				VehiculosAutorizados.Viernes.ViewValue = (VehiculosAutorizados.Viernes.FldTagCaption(1) != "") ? VehiculosAutorizados.Viernes.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Viernes.ViewValue = (VehiculosAutorizados.Viernes.FldTagCaption(2) != "") ? VehiculosAutorizados.Viernes.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Viernes.ViewCustomAttributes = "";

			// Sabado
			if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") {
				VehiculosAutorizados.Sabado.ViewValue = (VehiculosAutorizados.Sabado.FldTagCaption(1) != "") ? VehiculosAutorizados.Sabado.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Sabado.ViewValue = (VehiculosAutorizados.Sabado.FldTagCaption(2) != "") ? VehiculosAutorizados.Sabado.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Sabado.ViewCustomAttributes = "";

			// Domingo
			if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") {
				VehiculosAutorizados.Domingo.ViewValue = (VehiculosAutorizados.Domingo.FldTagCaption(1) != "") ? VehiculosAutorizados.Domingo.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Domingo.ViewValue = (VehiculosAutorizados.Domingo.FldTagCaption(2) != "") ? VehiculosAutorizados.Domingo.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Domingo.ViewCustomAttributes = "";

			// Marca
				VehiculosAutorizados.Marca.ViewValue = VehiculosAutorizados.Marca.CurrentValue;
			VehiculosAutorizados.Marca.ViewCustomAttributes = "";

			// View refer script
			// IdTipoVehiculo

			VehiculosAutorizados.IdTipoVehiculo.LinkCustomAttributes = "";
			VehiculosAutorizados.IdTipoVehiculo.HrefValue = "";
			VehiculosAutorizados.IdTipoVehiculo.TooltipValue = "";

			// Placa
			VehiculosAutorizados.Placa.LinkCustomAttributes = "";
			VehiculosAutorizados.Placa.HrefValue = "";
			VehiculosAutorizados.Placa.TooltipValue = "";

			// Autorizado
			VehiculosAutorizados.Autorizado.LinkCustomAttributes = "";
			VehiculosAutorizados.Autorizado.HrefValue = "";
			VehiculosAutorizados.Autorizado.TooltipValue = "";

			// PicoyPlaca
			VehiculosAutorizados.PicoyPlaca.LinkCustomAttributes = "";
			VehiculosAutorizados.PicoyPlaca.HrefValue = "";
			VehiculosAutorizados.PicoyPlaca.TooltipValue = "";

			// Lunes
			VehiculosAutorizados.Lunes.LinkCustomAttributes = "";
			VehiculosAutorizados.Lunes.HrefValue = "";
			VehiculosAutorizados.Lunes.TooltipValue = "";

			// Martes
			VehiculosAutorizados.Martes.LinkCustomAttributes = "";
			VehiculosAutorizados.Martes.HrefValue = "";
			VehiculosAutorizados.Martes.TooltipValue = "";

			// Miercoles
			VehiculosAutorizados.Miercoles.LinkCustomAttributes = "";
			VehiculosAutorizados.Miercoles.HrefValue = "";
			VehiculosAutorizados.Miercoles.TooltipValue = "";

			// Jueves
			VehiculosAutorizados.Jueves.LinkCustomAttributes = "";
			VehiculosAutorizados.Jueves.HrefValue = "";
			VehiculosAutorizados.Jueves.TooltipValue = "";

			// Viernes
			VehiculosAutorizados.Viernes.LinkCustomAttributes = "";
			VehiculosAutorizados.Viernes.HrefValue = "";
			VehiculosAutorizados.Viernes.TooltipValue = "";

			// Sabado
			VehiculosAutorizados.Sabado.LinkCustomAttributes = "";
			VehiculosAutorizados.Sabado.HrefValue = "";
			VehiculosAutorizados.Sabado.TooltipValue = "";

			// Domingo
			VehiculosAutorizados.Domingo.LinkCustomAttributes = "";
			VehiculosAutorizados.Domingo.HrefValue = "";
			VehiculosAutorizados.Domingo.TooltipValue = "";

			// Marca
			VehiculosAutorizados.Marca.LinkCustomAttributes = "";
			VehiculosAutorizados.Marca.HrefValue = "";
			VehiculosAutorizados.Marca.TooltipValue = "";
		}

		// Row Rendered event
		if (VehiculosAutorizados.RowType != EW_ROWTYPE_AGGREGATEINIT)
			VehiculosAutorizados.Row_Rendered();
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
		item.Body = "<a name=\"emf_VehiculosAutorizados\" id=\"emf_VehiculosAutorizados\" href=\"javascript:void(0);\" onclick=\"ew_EmailDialogShow({lnk:'emf_VehiculosAutorizados',hdr:ewLanguage.Phrase('ExportToEmail'),f:ew_GetForm('fVehiculosAutorizadoslist'),sel:false});\">" + "<img src=\"aspximages/exportemail.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToEmail")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToEmail")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Hide options for export/action
		if (ew_NotEmpty(VehiculosAutorizados.Export) || ew_NotEmpty(VehiculosAutorizados.CurrentAction))
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
		if (VehiculosAutorizados.ExportAll) {
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
		if (VehiculosAutorizados.Export == "xml") {
			XmlDoc = new cXMLDocument();
		} else {
			ExportDoc = new cExportDocument(this, VehiculosAutorizados, "h");
		}
		string ParentTable = "";

		// Export master record
		if (EW_EXPORT_MASTER_RECORD && ew_NotEmpty(VehiculosAutorizados.MasterFilter) && VehiculosAutorizados.CurrentMasterTable == "Personas") {
			SqlDataReader rsmaster = Personas.LoadRs(DbMasterFilter); // Load master record
			if (rsmaster != null && rsmaster.HasRows) {
				if (VehiculosAutorizados.Export == "xml") {
					ParentTable = "Personas";
					Personas.ExportXmlDocument(ref XmlDoc, false, ref rsmaster, 1, 1, "");
				} else {
					string ExportStyle = ExportDoc.Style;
					ExportDoc.ChangeStyle("v"); // Change to vertical
					if (VehiculosAutorizados.Export != "csv" || EW_EXPORT_MASTER_RECORD_FOR_CSV) {
						Personas.ExportDocument(ref ExportDoc, ref rsmaster, 1, 1, "");
						ExportDoc.ExportEmptyLine();
					}
					ExportDoc.ChangeStyle(ExportStyle); // Restore
				}
				rsmaster.Close();
				rsmaster.Dispose();
			}
		}
		if (VehiculosAutorizados.Export == "xml") {
			VehiculosAutorizados.ExportXmlDocument(ref XmlDoc, ew_NotEmpty(ParentTable), ref Rs, StartRec, StopRec, "");
		} else {
			string sHeader = PageHeader;
			Page_DataRendering(ref sHeader);
			ExportDoc.Text += sHeader;
			VehiculosAutorizados.ExportDocument(ref ExportDoc, ref Rs, StartRec, StopRec, "");
			string sFooter = PageFooter;
			Page_DataRendered(ref sFooter);
			ExportDoc.Text += sFooter;
		}

		// Close recordset
		Rs.Close();
		Rs.Dispose();

		// Export header and footer
		if (VehiculosAutorizados.Export != "xml")
			ExportDoc.ExportHeaderAndFooter();

		// Clean output buffer
		if (!EW_DEBUG_ENABLED)
			HttpContext.Current.Response.Clear();

		// Write BOM if utf-8
		if (utf8 && VehiculosAutorizados.Export != "email" && VehiculosAutorizados.Export != "xml")
			HttpContext.Current.Response.BinaryWrite(new byte[]{0xEF, 0xBB, 0xBF});

		// Write debug message if enabled
		if (EW_DEBUG_ENABLED)
			ew_Write(ew_DebugMsg());

		// Output data
		if (VehiculosAutorizados.Export == "xml") {
			ew_AddHeader("Content-Type", "text/xml");
			ew_Write(XmlDoc.XML());
		} else if (VehiculosAutorizados.Export == "pdf") {
			ExportPDF(ExportDoc.Text);
		} else {
			ew_Write(ExportDoc.Text);
		}
	}

	// Set up master/detail based on QueryString
	public void SetUpMasterParms() {
		bool bValidMaster = false;
		string sMasterTblVar = "";

		// Get the keys for master table
		if (ew_NotEmpty(ew_Get(EW_TABLE_SHOW_MASTER))) {
			sMasterTblVar = ew_Get(EW_TABLE_SHOW_MASTER);
			if (ew_Empty(sMasterTblVar)) {
				bValidMaster = true;
				DbMasterFilter = "";
				DbDetailFilter = "";
			}
			if (sMasterTblVar == "Personas") {
				bValidMaster = true;				
				if (ew_NotEmpty(ew_Get("IdPersona"))) {
					Personas.IdPersona.QueryStringValue = ew_Get("IdPersona");
					VehiculosAutorizados.IdPersona.QueryStringValue = Personas.IdPersona.QueryStringValue;
					VehiculosAutorizados.IdPersona.SessionValue = VehiculosAutorizados.IdPersona.QueryStringValue;
					if (!Information.IsNumeric(Personas.IdPersona.QueryStringValue)) bValidMaster = false;
				} else {
					bValidMaster = false;
				}
			}
		}
		if (bValidMaster) {

			// Save current master table
			VehiculosAutorizados.CurrentMasterTable = sMasterTblVar;

			// Reset start record counter (new master key)
			StartRec = 1;
			VehiculosAutorizados.StartRecordNumber = StartRec;

			// Clear previous master session values
			if (sMasterTblVar != "Personas") {
				if (ew_Empty(VehiculosAutorizados.IdPersona.QueryStringValue)) VehiculosAutorizados.IdPersona.SessionValue = "";
			}
		}
		DbMasterFilter = VehiculosAutorizados.MasterFilter; // Get master filter
		DbDetailFilter = VehiculosAutorizados.DetailFilter; // Get detail filter
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
		VehiculosAutorizados_list = new cVehiculosAutorizados_list(this);
		CurrentPage = VehiculosAutorizados_list;

		//CurrentPageType = VehiculosAutorizados_list.GetType();
		VehiculosAutorizados_list.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		VehiculosAutorizados_list.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (VehiculosAutorizados_list != null)
			VehiculosAutorizados_list.Dispose();
	}
}
