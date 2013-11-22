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

partial class Personaslist: AspNetMaker9_ControlVehicular {

	// Page object
	public cPersonas_list Personas_list;	

	//
	// Page Class
	//
	public class cPersonas_list: AspNetMakerPage, IDisposable {

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
				if (Personas.UseTokenInUrl)
					Url += "t=" + Personas.TableVar + "&"; // Add page token
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
			if (Personas.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (Personas.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (Personas.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public Personaslist AspNetPage { 
			get { return (Personaslist)m_ParentPage; }
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

		// Areas	
		public cAreas Areas { 
			get {				
				return ParentPage.Areas;
			}
			set {
				ParentPage.Areas = value;	
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

		// VehiculosAutorizados	
		public cVehiculosAutorizados VehiculosAutorizados { 
			get {				
				return ParentPage.VehiculosAutorizados;
			}
			set {
				ParentPage.VehiculosAutorizados = value;	
			}	
		}		

		//
		//  Page class constructor
		//
		public cPersonas_list(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "list";
			m_PageObjName = "Personas_list";
			m_PageObjTypeName = "cPersonas_list";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (Personas == null)
				Personas = new cPersonas(this);
			if (Areas == null)
				Areas = new cAreas(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);
			if (VehiculosAutorizados == null)
				VehiculosAutorizados = new cVehiculosAutorizados(this);

			// Table
			m_TableName = "Personas";
			m_Table = Personas;
			CurrentTable = Personas;

			//CurrentTableType = Personas.GetType();
			// Initialize URLs

			ExportPrintUrl = PageUrl + "export=print";
			ExportExcelUrl = PageUrl + "export=excel";
			ExportWordUrl = PageUrl + "export=word";
			ExportHtmlUrl = PageUrl + "export=html";
			ExportXmlUrl = PageUrl + "export=xml";
			ExportCsvUrl = PageUrl + "export=csv";
			ExportPdfUrl = PageUrl + "export=pdf";
			AddUrl = "Personasadd.aspx?" + EW_TABLE_SHOW_DETAIL + "=";
			InlineAddUrl = PageUrl + "a=add";
			GridAddUrl = PageUrl + "a=gridadd";
			GridEditUrl = PageUrl + "a=gridedit";
			MultiDeleteUrl = "Personasdelete.aspx";
			MultiUpdateUrl = "Personasupdate.aspx";

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
				Personas.Export = ew_Get("export");
			} else if (ew_NotEmpty(ew_Post("exporttype"))) {
				Personas.Export = ew_Post("exporttype");
			} else {
				Personas.ExportReturnUrl = ew_CurrentUrl();
			}
			gsExport = Personas.Export; // Get export parameter, used in header
			gsExportFile = Personas.TableVar; // Get export file, used in header			
			string Charset = (ew_NotEmpty(EW_CHARSET)) ? ";charset=" + EW_CHARSET : ""; // Charset used in header

			// Write BOM 
			if (Personas.Export == "excel" || Personas.Export == "word" || Personas.Export == "csv")
				HttpContext.Current.Response.BinaryWrite(new byte[]{0xEF, 0xBB, 0xBF});
			if (Personas.Export == "excel") {
				HttpContext.Current.Response.ContentType = "application/vnd.ms-excel" + Charset;
				ew_AddHeader("Content-Disposition", "attachment; filename=" + gsExportFile + ".xls");
			}

			// Get grid add count
			int gridaddcnt = ew_ConvertToInt(ew_Get(EW_TABLE_GRID_ADD_ROW_COUNT));
			if (gridaddcnt > 0)
				Personas.GridAddRowCount = gridaddcnt;

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
			Personas.Dispose();
			Areas.Dispose();
			Usuarios.Dispose();
			VehiculosAutorizados.Dispose();
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
			if (ew_NotEmpty(Personas.Export) ||
				Personas.CurrentAction == "gridadd" ||
				Personas.CurrentAction == "gridedit") {
				ListOptions.HideAllOptions();
				ExportOptions.HideAllOptions();
			}

			// Get basic search values
			LoadBasicSearchValues();

			// Get advanced search criteria
			LoadSearchValues();
			if (!ValidateSearch())				
				FailureMessage = gsSearchError;

			// Restore search parms from Session
			RestoreSearchParms();

			// Call Recordset SearchValidated event
			Personas.Recordset_SearchValidated();

			// Set Up Sorting Order
			SetUpSortOrder();

			// Get basic search criteria
			if (ew_Empty(gsSearchError))
				sSrchBasic = BasicSearchWhere();

			// Get search criteria for advanced search
			if (ew_Empty(gsSearchError))
				sSrchAdvanced = AdvancedSearchWhere();
		}

		// Restore display records
		if (Personas.RecordsPerPage == -1 || Personas.RecordsPerPage > 0) {
			DisplayRecs = Personas.RecordsPerPage; // Restore from Session
		} else {
			DisplayRecs = 20; // Load default
		}

		// Load Sorting Order
		LoadSortOrder();

		// Build search criteria
		ew_AddFilter(ref SearchWhere, sSrchAdvanced);
		ew_AddFilter(ref SearchWhere, sSrchBasic);

		// Recordset Searching event
		Personas.Recordset_Searching(ref SearchWhere);

		// Save search criteria
		if (ew_NotEmpty(SearchWhere)) {
			if (ew_Empty(sSrchBasic)) ResetBasicSearchParms();
			if (ew_Empty(sSrchAdvanced)) ResetAdvancedSearchParms();
			Personas.SearchWhere = SearchWhere; // Save to Session
			if (RestoreSearch) {
				StartRec = 1; // Reset start record counter
				Personas.StartRecordNumber = StartRec;
			}
		} else {
			SearchWhere = Personas.SearchWhere; 
		}

		// Build filter
		sFilter = "";
		if (!Security.CanList)
			sFilter = "(0=1)"; // Filter all records

		// Restore master/detail filter
		DbMasterFilter = Personas.MasterFilter; // Restore master filter
		DbDetailFilter = Personas.DetailFilter; // Restore detail filter
		ew_AddFilter(ref sFilter, DbDetailFilter);
		ew_AddFilter(ref sFilter, SearchWhere);
		SqlDataReader RsMaster; 

		// Load master record
		if (Personas.CurrentMode != "add" && ew_NotEmpty(Personas.MasterFilter) && Personas.CurrentMasterTable == "Areas") {
			RsMaster = Areas.LoadRs(DbMasterFilter);
			MasterRecordExists = RsMaster != null;
			if (!MasterRecordExists) {
				FailureMessage = Language.Phrase("NoRecord"); // Set no record found
				Page_Terminate(Personas.ReturnUrl); // Return to caller
			} else {
				RsMaster.Read();
				Areas.LoadListRowValues(ref RsMaster);
				Areas.RowType = EW_ROWTYPE_MASTER; // Master row
				Areas.RenderListRow();
				RsMaster.Close();
				RsMaster.Dispose();
			}
		}

		// Set up filter in Session
		Personas.SessionWhere = sFilter;
		Personas.CurrentFilter = "";

		// Export Data only
		if (Personas.Export == "html" || Personas.Export == "csv" || Personas.Export == "word" || Personas.Export == "excel" || Personas.Export == "xml" || Personas.Export == "pdf") {
			ExportData();
			Page_Terminate(""); // Clean up
			ew_End(); // Terminate response
		} else if (Personas.Export == "email") {
			ExportData();
			ew_End();
		}
	}

	//
	// Return Advanced Search WHERE based on QueryString parameters
	//

	string AdvancedSearchWhere() {		
		if (!Security.CanSearch) return "";
		string sWhere = "";
		BuildSearchSql(ref sWhere, Personas.IdPersona, false); // IdPersona
		BuildSearchSql(ref sWhere, Personas.IdArea, false); // IdArea
		BuildSearchSql(ref sWhere, Personas.IdCargo, false); // IdCargo
		BuildSearchSql(ref sWhere, Personas.Documento, false); // Documento
		BuildSearchSql(ref sWhere, Personas.Persona, false); // Persona
		BuildSearchSql(ref sWhere, Personas.Activa, false); // Activa

		// Set up search parm
		if (ew_NotEmpty(sWhere)) {
			SetSearchParm(Personas.IdPersona); // IdPersona
			SetSearchParm(Personas.IdArea); // IdArea
			SetSearchParm(Personas.IdCargo); // IdCargo
			SetSearchParm(Personas.Documento); // Documento
			SetSearchParm(Personas.Persona); // Persona
			SetSearchParm(Personas.Activa); // Activa
		}
		return sWhere;
	}

	//
	// Build search SQL
	//
	public void BuildSearchSql(ref string Where, cField Fld, bool MultiValue)
	{
		string FldParm = Fld.FldVar.Substring(2);
		string FldVal = Convert.ToString(Fld.AdvancedSearch.SearchValue);
		string FldOpr = Fld.AdvancedSearch.SearchOperator;
		string FldCond = Fld.AdvancedSearch.SearchCondition;
		string FldVal2 = Convert.ToString(Fld.AdvancedSearch.SearchValue2);
		string FldOpr2 = Fld.AdvancedSearch.SearchOperator2;
		string sWrk = "";
		FldOpr = FldOpr.Trim().ToUpper();
		if (ew_Empty(FldOpr)) FldOpr = "="; 
		FldOpr2 = FldOpr2.Trim().ToUpper();
		if (ew_Empty(FldOpr2)) FldOpr2 = "="; 
		if (EW_SEARCH_MULTI_VALUE_OPTION == 1) MultiValue = false; 
		if (FldOpr != "LIKE") MultiValue = false; 
		if (FldOpr2 != "LIKE" && ew_NotEmpty(FldVal2)) MultiValue = false; 
		if (MultiValue)	{
			string sWrk1;
			string sWrk2;

			// Field value 1
			if (ew_NotEmpty(FldVal))	{
				sWrk1 = ew_GetMultiSearchSql(ref Fld, FldVal);
			}	else	{
				sWrk1 = "";
			}

			// Field value 2
			if (ew_NotEmpty(FldVal2) && ew_NotEmpty(FldCond)) {
				sWrk2 = ew_GetMultiSearchSql(ref Fld, FldVal2);
			}	else	{
				sWrk2 = "";
			}

			// Build final SQL
			sWrk = sWrk1;
			if (ew_NotEmpty(sWrk2)) {
				if (ew_NotEmpty(sWrk)) {
					sWrk = "(" + sWrk + ") " + FldCond + " (" + sWrk2 + ")";
				}	else	{
					sWrk = sWrk2;
				}
			}
		}	else {
			FldVal = ConvertSearchValue(ref Fld, FldVal);
			FldVal2 = ConvertSearchValue(ref Fld, FldVal2);
			sWrk = ew_GetSearchSql(ref Fld, FldVal, FldOpr, FldCond, FldVal2, FldOpr2);
		}
		ew_AddFilter(ref Where, sWrk);
	}

	//
	// Set search parm
	//
	public void SetSearchParm(cField Fld)	{
		string FldParm = Fld.FldVar.Substring(2); 
		Personas.SetAdvancedSearch("x_" + FldParm, Fld.AdvancedSearch.SearchValue);
		Personas.SetAdvancedSearch("z_" + FldParm, Fld.AdvancedSearch.SearchOperator);
		Personas.SetAdvancedSearch("v_" + FldParm, Fld.AdvancedSearch.SearchCondition);
		Personas.SetAdvancedSearch("y_" + FldParm, Fld.AdvancedSearch.SearchValue2);
		Personas.SetAdvancedSearch("w_" + FldParm, Fld.AdvancedSearch.SearchOperator2);
	}

	//
	// Get search parm
	//
	public void GetSearchParm(cField Fld) {
		string FldParm = Fld.FldVar.Substring(2);
		Fld.AdvancedSearch.SearchValue = Personas.GetAdvancedSearch("x_" + FldParm);
		Fld.AdvancedSearch.SearchOperator = Personas.GetAdvancedSearch("z_" + FldParm);
		Fld.AdvancedSearch.SearchCondition = Personas.GetAdvancedSearch("v_" + FldParm);
		Fld.AdvancedSearch.SearchValue2 = Personas.GetAdvancedSearch("y_" + FldParm);
		Fld.AdvancedSearch.SearchOperator2 = Personas.GetAdvancedSearch("w_" + FldParm);
	}

	//
	// Convert search value
	//
	public string ConvertSearchValue(ref cField Fld, string FldVal)	{
		if (Fld.FldDataType == EW_DATATYPE_BOOLEAN)	{
			if (ew_NotEmpty(FldVal)) return (FldVal == "1" || ew_SameText(FldVal, "y") || ew_SameText(FldVal, "t")) ? "1" : "0"; 
		} else if (Fld.FldDataType == EW_DATATYPE_DATE) {
			if (ew_NotEmpty(FldVal)) return ew_UnformatDateTime(FldVal, Fld.FldDateTimeFormat); 
		}
		return FldVal;
	}	

	//
	// Return Basic Search SQL
	//
	public string BasicSearchSQL(string Keyword) {
		string sWhere = "";
			BuildBasicSearchSQL(ref sWhere, Personas.Documento, Keyword); 
			BuildBasicSearchSQL(ref sWhere, Personas.Persona, Keyword); 
		return sWhere;
	}

	//
	// Build basic search SQL
	//
	public void BuildBasicSearchSQL(ref string Where, cField Fld, string Keyword)	{
		string sFldExpression;
		string sWrk;
		if (ew_NotEmpty(Fld.FldVirtualExpression))	{
			sFldExpression = Fld.FldVirtualExpression;
		}	else {
			sFldExpression = Fld.FldExpression;
		}
		int lFldDataType = Fld.FldDataType;
		if (Fld.FldIsVirtual)
			lFldDataType = EW_DATATYPE_STRING; 
		if (lFldDataType == EW_DATATYPE_NUMBER)	{
			sWrk = sFldExpression + " = " + ew_QuotedValue(Keyword, lFldDataType);
		}	else {
			sWrk = sFldExpression + ew_Like(ew_QuotedValue("%" + Keyword + "%", lFldDataType));
		}
		if (ew_NotEmpty(Where))
			Where += " OR ";
		Where += sWrk;
	}

	//
	// Return Basic Search WHERE based on search keyword and type
	//
	public string BasicSearchWhere() {
		string sSearchStr = "";
		string[] arKeyword;		
		if (!Security.CanSearch) return "";
		string sSearchKeyword = Personas.BasicSearchKeyword;
		string sSearchType = Personas.BasicSearchType;
		if (ew_NotEmpty(sSearchKeyword))	{
			string sSearch = sSearchKeyword.Trim();
			if (ew_NotEmpty(sSearchType)) {
				while (sSearch.Contains("  "))
					sSearch = sSearch.Replace("  ", " ");
				arKeyword = sSearch.Trim().Split(new char[] {' '});
				foreach (string sKeyword in arKeyword) {
					if (ew_NotEmpty(sSearchStr)) sSearchStr += " " + sSearchType + " "; 
					sSearchStr += "(" + BasicSearchSQL(sKeyword) + ")";
				}
			}	else {
				sSearchStr = BasicSearchSQL(sSearch);
			}
		}
		if (ew_NotEmpty(sSearchKeyword)) {
			Personas.SessionBasicSearchKeyword = sSearchKeyword;
			Personas.SessionBasicSearchType = sSearchType;
		}
		return sSearchStr;
	}

	//
	// Clear all search parameters
	//
	public void ResetSearchParms() {

		// Clear search where
		SearchWhere = "";
		Personas.SearchWhere = SearchWhere;

		// Clear basic search parameters
		ResetBasicSearchParms();

		// Clear advanced search parameters
		ResetAdvancedSearchParms();
	}

	//
	// Clear all basic search parameters
	//
	public void ResetBasicSearchParms()	{
		Personas.SessionBasicSearchKeyword = "";
		Personas.SessionBasicSearchType = "";
	}

	//
	// Clear all advanced search parameters
	//
	public void ResetAdvancedSearchParms() {
		Personas.SetAdvancedSearch("x_IdPersona", "");
		Personas.SetAdvancedSearch("x_IdArea", "");
		Personas.SetAdvancedSearch("x_IdCargo", "");
		Personas.SetAdvancedSearch("x_Documento", "");
		Personas.SetAdvancedSearch("x_Persona", "");
		Personas.SetAdvancedSearch("x_Activa", "");
	}

	//
	// Restore all search parameters
	//
	public void RestoreSearchParms() {
		bool bRestore = true;
		if (ew_NotEmpty(Personas.BasicSearchKeyword))
			bRestore = false;
		if (ew_NotEmpty(Personas.IdPersona.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(Personas.IdArea.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(Personas.IdCargo.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(Personas.Documento.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(Personas.Persona.AdvancedSearch.SearchValue))
			bRestore = false;
		if (!ew_SameStr(Personas.Activa.AdvancedSearch.SearchValue, Personas.Activa.GetAdvancedSearch("SearchValue")))
			bRestore = false;
		RestoreSearch = bRestore;
		if (bRestore) {

			// Restore basic search values
			Personas.BasicSearchKeyword = Personas.SessionBasicSearchKeyword;
			Personas.BasicSearchType = Personas.SessionBasicSearchType;

			// Restore advanced search values
			GetSearchParm(Personas.IdPersona);
			GetSearchParm(Personas.IdArea);
			GetSearchParm(Personas.IdCargo);
			GetSearchParm(Personas.Documento);
			GetSearchParm(Personas.Persona);
			GetSearchParm(Personas.Activa);
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
			Personas.CurrentOrder = ew_Get("order");
			Personas.CurrentOrderType = ew_Get("ordertype");
			Personas.UpdateSort(Personas.IdArea, bCtrl); // IdArea
			Personas.UpdateSort(Personas.IdCargo, bCtrl); // IdCargo
			Personas.UpdateSort(Personas.Documento, bCtrl); // Documento
			Personas.UpdateSort(Personas.Persona, bCtrl); // Persona
			Personas.UpdateSort(Personas.Activa, bCtrl); // Activa
			Personas.StartRecordNumber = 1; // Reset start position
		}
	}

	//
	// Load Sort Order parameters
	//
	public void LoadSortOrder()	{
		string sOrderBy = Personas.SessionOrderBy; // Get order by from Session
		if (ew_Empty(sOrderBy)) {
			if (ew_NotEmpty(Personas.SqlOrderBy)) {
				sOrderBy = Personas.SqlOrderBy;
				Personas.SessionOrderBy = sOrderBy;
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

			// Reset search criteria
			if (ew_SameText(sCmd, "reset") || ew_SameText(sCmd, "resetall")) {
				ResetSearchParms();
			}

			// Reset master/detail keys
			if (ew_SameText(sCmd, "resetall")) {
				Personas.CurrentMasterTable = ""; // Clear master table
				DbMasterFilter = "";
				DbDetailFilter = "";
				Personas.IdArea.SessionValue = "";
			}

			// Reset sort criteria
			if (ew_SameText(sCmd, "resetsort")) {
				string sOrderBy = "";
				Personas.SessionOrderBy = sOrderBy;
				Personas.IdArea.Sort = "";
				Personas.IdCargo.Sort = "";
				Personas.Documento.Sort = "";
				Personas.Persona.Sort = "";
				Personas.Activa.Sort = "";
			}

			// Reset start position
			StartRec = 1;
			Personas.StartRecordNumber = StartRec;
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
		item = ListOptions.Add("detail_VehiculosAutorizados");
		item.CssStyle = "white-space: nowrap;";
		item.Visible = Security.AllowList("Personas");
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

		// "detail_VehiculosAutorizados"
		oListOpt = ListOptions.GetItem("detail_VehiculosAutorizados");
		if (Security.AllowList("VehiculosAutorizados")) {
			oListOpt.Body = "<img src=\"aspximages/detail.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("DetailLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("DetailLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + Language.TablePhrase("VehiculosAutorizados", "TblCaption");
			oListOpt.Body = "<a class=\"ewRowLink\" href=\"VehiculosAutorizadoslist.aspx?" + EW_TABLE_SHOW_MASTER + "=Personas&IdPersona=" + ew_UrlEncode(Convert.ToString(Personas.IdPersona.CurrentValue)) + "\">" + oListOpt.Body + "</a>";
			links = "";
			if (VehiculosAutorizados.DetailEdit && Security.CanEdit && Security.AllowEdit("VehiculosAutorizados"))
				links += "<a class=\"ewRowLink\" href=\"" + Personas.GetEditUrl(EW_TABLE_SHOW_DETAIL + "=VehiculosAutorizados") + "\">" + "<img src=\"aspximages/edit.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>&nbsp;";
			if (ew_NotEmpty(links)) oListOpt.Body += "<br />" + links;
		}
		RenderListOptionsExt();
		ListOptions_Rendered();
	}

	public void RenderListOptionsExt() {
		string sHyperLinkParm = "";
		cListOption oListOpt; 
		string links = "";
		sSqlWrk = "[IdPersona]=" + ew_AdjustSql(Personas.IdPersona.CurrentValue) + "";
		sSqlWrk = cTEA.Encrypt(sSqlWrk, EW_RANDOM_KEY);
		sSqlWrk = sSqlWrk.Replace("'", "\'");
		oListOpt = ListOptions.GetItem("detail_VehiculosAutorizados");
		oListOpt.Body = "<img src=\"aspximages/detail.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("DetailLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("DetailLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + Language.TablePhrase("VehiculosAutorizados", "TblCaption");
		sHyperLinkParm = " href=\"VehiculosAutorizadoslist.aspx?" + EW_TABLE_SHOW_MASTER + "=Personas&IdPersona=" + ew_UrlEncode(Convert.ToString(Personas.IdPersona.CurrentValue)) + "\" name=\"dl%i_Personas_VehiculosAutorizados\" id=\"dl%i_Personas_VehiculosAutorizados\" onmouseover=\"ew_AjaxShowDetails(this, 'VehiculosAutorizadospreview.aspx?f=%s')\" onmouseout=\"ew_AjaxHideDetails(this);\"";
		sHyperLinkParm = sHyperLinkParm.Replace("%i", Convert.ToString(RowCnt));
		sHyperLinkParm = sHyperLinkParm.Replace("%s", sSqlWrk);
		oListOpt.Body = "<a class=\"ewRowLink\"" + sHyperLinkParm + ">" + oListOpt.Body + "</a>";
		links = "";
		if (VehiculosAutorizados.DetailEdit && Security.CanEdit && Security.AllowEdit("VehiculosAutorizados"))
			links += "<a class=\"ewRowLink\" href=\"" + Personas.GetEditUrl(EW_TABLE_SHOW_DETAIL + "=VehiculosAutorizados") + "\">" +  "<img src=\"aspximages/edit.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("EditLink")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>&nbsp;";
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
				Personas.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				Personas.StartRecordNumber = StartRec;
			}
		}
		StartRec = Personas.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			Personas.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			Personas.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			Personas.StartRecordNumber = StartRec;
		}
	}

	//
	// Load basic search values
	//
	public void LoadBasicSearchValues() {
		Personas.BasicSearchKeyword = ew_Get(EW_TABLE_BASIC_SEARCH);
		Personas.BasicSearchType = ew_Get(EW_TABLE_BASIC_SEARCH_TYPE);
	}

	//
	//  Load search values for validation
	//
	public void LoadSearchValues() {
		Personas.IdPersona.AdvancedSearch.SearchValue = ew_Get("x_IdPersona");
    Personas.IdPersona.AdvancedSearch.SearchOperator = ew_Get("z_IdPersona");
		Personas.IdArea.AdvancedSearch.SearchValue = ew_Get("x_IdArea");
    Personas.IdArea.AdvancedSearch.SearchOperator = ew_Get("z_IdArea");
		Personas.IdCargo.AdvancedSearch.SearchValue = ew_Get("x_IdCargo");
    Personas.IdCargo.AdvancedSearch.SearchOperator = ew_Get("z_IdCargo");
		Personas.Documento.AdvancedSearch.SearchValue = ew_Get("x_Documento");
    Personas.Documento.AdvancedSearch.SearchOperator = ew_Get("z_Documento");
		Personas.Persona.AdvancedSearch.SearchValue = ew_Get("x_Persona");
    Personas.Persona.AdvancedSearch.SearchOperator = ew_Get("z_Persona");
		Personas.Activa.AdvancedSearch.SearchValue = ew_Get("x_Activa");
    Personas.Activa.AdvancedSearch.SearchOperator = ew_Get("z_Activa");
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = Personas.CurrentFilter;
		Personas.Recordset_Selecting(ref sFilter);
		Personas.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = Personas.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		
		try {
			string sCntSql = Personas.SelectCountSQL;

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
		Personas.Recordset_Selected(Rs);
		return Rs;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = Personas.KeyFilter;

		// Row Selecting event
		Personas.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		Personas.CurrentFilter = sFilter;
		string sSql = Personas.SQL;

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
		Personas.Row_Selected(ref row);
		Personas.IdPersona.DbValue = row["IdPersona"];
		Personas.IdArea.DbValue = row["IdArea"];
		Personas.IdCargo.DbValue = row["IdCargo"];
		Personas.Documento.DbValue = row["Documento"];
		Personas.Persona.DbValue = row["Persona"];
		Personas.Activa.DbValue = (ew_ConvertToBool(row["Activa"])) ? "1" : "0";
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(Personas.GetKey("IdPersona")))
			Personas.IdPersona.CurrentValue = Personas.GetKey("IdPersona"); // IdPersona
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			Personas.CurrentFilter = Personas.KeyFilter;
			string sSql = Personas.SQL;			
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
		ViewUrl = Personas.ViewUrl;
		EditUrl = Personas.EditUrl;
		InlineEditUrl = Personas.InlineEditUrl;
		CopyUrl = Personas.CopyUrl;
		InlineCopyUrl = Personas.InlineCopyUrl;
		DeleteUrl = Personas.DeleteUrl;

		// Row Rendering event
		Personas.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdPersona
		// IdArea
		// IdCargo
		// Documento
		// Persona
		// Activa
		//
		//  View  Row
		//

		if (Personas.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdPersona
				Personas.IdPersona.ViewValue = Personas.IdPersona.CurrentValue;
			Personas.IdPersona.ViewCustomAttributes = "";

			// IdArea
			if (ew_NotEmpty(Personas.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(Personas.IdArea.CurrentValue) + "";
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
					Personas.IdArea.ViewValue = drWrk["Area"];
					Personas.IdArea.ViewValue = String.Concat(Personas.IdArea.ViewValue, ew_ValueSeparator(0, 1, Personas.IdArea), drWrk["Codigo"]);
				} else {
					Personas.IdArea.ViewValue = Personas.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdArea.ViewValue = System.DBNull.Value;
			}
			Personas.IdArea.ViewCustomAttributes = "";

			// IdCargo
			if (ew_NotEmpty(Personas.IdCargo.CurrentValue)) {
				sFilterWrk = "[IdCargo] = " + ew_AdjustSql(Personas.IdCargo.CurrentValue) + "";
			sSqlWrk = "SELECT [Cargo] FROM [dbo].[Cargos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Cargo]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Personas.IdCargo.ViewValue = drWrk["Cargo"];
				} else {
					Personas.IdCargo.ViewValue = Personas.IdCargo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdCargo.ViewValue = System.DBNull.Value;
			}
			Personas.IdCargo.ViewCustomAttributes = "";

			// Documento
				Personas.Documento.ViewValue = Personas.Documento.CurrentValue;
			Personas.Documento.ViewCustomAttributes = "";

			// Persona
				Personas.Persona.ViewValue = Personas.Persona.CurrentValue;
			Personas.Persona.ViewCustomAttributes = "";

			// Activa
			if (Convert.ToString(Personas.Activa.CurrentValue) == "1") {
				Personas.Activa.ViewValue = (Personas.Activa.FldTagCaption(1) != "") ? Personas.Activa.FldTagCaption(1) : "Y";
			} else {
				Personas.Activa.ViewValue = (Personas.Activa.FldTagCaption(2) != "") ? Personas.Activa.FldTagCaption(2) : "N";
			}
			Personas.Activa.ViewCustomAttributes = "";

			// View refer script
			// IdArea

			Personas.IdArea.LinkCustomAttributes = "";
			Personas.IdArea.HrefValue = "";
			Personas.IdArea.TooltipValue = "";

			// IdCargo
			Personas.IdCargo.LinkCustomAttributes = "";
			Personas.IdCargo.HrefValue = "";
			Personas.IdCargo.TooltipValue = "";

			// Documento
			Personas.Documento.LinkCustomAttributes = "";
			Personas.Documento.HrefValue = "";
			Personas.Documento.TooltipValue = "";

			// Persona
			Personas.Persona.LinkCustomAttributes = "";
			Personas.Persona.HrefValue = "";
			Personas.Persona.TooltipValue = "";

			// Activa
			Personas.Activa.LinkCustomAttributes = "";
			Personas.Activa.HrefValue = "";
			Personas.Activa.TooltipValue = "";

		//
		//  Search Row
		//

		} else if (Personas.RowType == EW_ROWTYPE_SEARCH) { // Search row

			// IdArea
			Personas.IdArea.EditCustomAttributes = "";
			if (ew_NotEmpty(Personas.IdArea.SessionValue)) {
				Personas.IdArea.AdvancedSearch.SearchValue = Personas.IdArea.SessionValue;
			if (ew_NotEmpty(Personas.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(Personas.IdArea.CurrentValue) + "";
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
					Personas.IdArea.ViewValue = drWrk["Area"];
					Personas.IdArea.ViewValue = String.Concat(Personas.IdArea.ViewValue, ew_ValueSeparator(0, 1, Personas.IdArea), drWrk["Codigo"]);
				} else {
					Personas.IdArea.ViewValue = Personas.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdArea.ViewValue = System.DBNull.Value;
			}
			Personas.IdArea.ViewCustomAttributes = "";
			} else {
			sFilterWrk = "";
			sSqlWrk = "SELECT [IdArea], [Area] AS [DispFld], [Codigo] AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[Areas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Area]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			((OrderedDictionary)alwrk[0]).Add("2", "");
			Personas.IdArea.EditValue = alwrk;
			}

			// IdCargo
			Personas.IdCargo.EditCustomAttributes = "";
			sFilterWrk = "";
			sSqlWrk = "SELECT [IdCargo], [Cargo] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[Cargos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Cargo]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			Personas.IdCargo.EditValue = alwrk;

			// Documento
			Personas.Documento.EditCustomAttributes = "";
			Personas.Documento.EditValue = ew_HtmlEncode(Personas.Documento.AdvancedSearch.SearchValue);

			// Persona
			Personas.Persona.EditCustomAttributes = "";
			Personas.Persona.EditValue = ew_HtmlEncode(Personas.Persona.AdvancedSearch.SearchValue);

			// Activa
			Personas.Activa.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(1))) ? Personas.Activa.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(2))) ? Personas.Activa.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			Personas.Activa.EditValue = alwrk;
		}
		if (Personas.RowType == EW_ROWTYPE_ADD ||
			Personas.RowType == EW_ROWTYPE_EDIT ||
			Personas.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			Personas.SetupFieldTitles();
		}

		// Row Rendered event
		if (Personas.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Personas.Row_Rendered();
	}

	//
	// Validate search
	//
	public bool ValidateSearch() {

		// Initialize
		gsSearchError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return true; 

		// Return validate result
		bool Valid = ew_Empty(gsSearchError);

		// Form_CustomValidate event
		string sFormCustomError = "";
		Valid = Valid && Form_CustomValidate(ref sFormCustomError);
		ew_AddMessage(ref gsSearchError, sFormCustomError);
		return Valid;
	}

	//
	// Load advanced search
	//
	public void LoadAdvancedSearch() {
		Personas.IdPersona.AdvancedSearch.SearchValue = Personas.GetAdvancedSearch("x_IdPersona");
		Personas.IdArea.AdvancedSearch.SearchValue = Personas.GetAdvancedSearch("x_IdArea");
		Personas.IdCargo.AdvancedSearch.SearchValue = Personas.GetAdvancedSearch("x_IdCargo");
		Personas.Documento.AdvancedSearch.SearchValue = Personas.GetAdvancedSearch("x_Documento");
		Personas.Persona.AdvancedSearch.SearchValue = Personas.GetAdvancedSearch("x_Persona");
		Personas.Activa.AdvancedSearch.SearchValue = Personas.GetAdvancedSearch("x_Activa");
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
		item.Body = "<a name=\"emf_Personas\" id=\"emf_Personas\" href=\"javascript:void(0);\" onclick=\"ew_EmailDialogShow({lnk:'emf_Personas',hdr:ewLanguage.Phrase('ExportToEmail'),f:ew_GetForm('fPersonaslist'),sel:false});\">" + "<img src=\"aspximages/exportemail.gif\" alt=\"" + ew_HtmlEncode(Language.Phrase("ExportToEmail")) + "\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToEmail")) + "\" width=\"16\" height=\"16\" border=\"0\" />" + "</a>";
		item.Visible = false;

		// Hide options for export/action
		if (ew_NotEmpty(Personas.Export) || ew_NotEmpty(Personas.CurrentAction))
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
		if (Personas.ExportAll) {
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
		if (Personas.Export == "xml") {
			XmlDoc = new cXMLDocument();
		} else {
			ExportDoc = new cExportDocument(this, Personas, "h");
		}
		string ParentTable = "";

		// Export master record
		if (EW_EXPORT_MASTER_RECORD && ew_NotEmpty(Personas.MasterFilter) && Personas.CurrentMasterTable == "Areas") {
			SqlDataReader rsmaster = Areas.LoadRs(DbMasterFilter); // Load master record
			if (rsmaster != null && rsmaster.HasRows) {
				if (Personas.Export == "xml") {
					ParentTable = "Areas";
					Areas.ExportXmlDocument(ref XmlDoc, false, ref rsmaster, 1, 1, "");
				} else {
					string ExportStyle = ExportDoc.Style;
					ExportDoc.ChangeStyle("v"); // Change to vertical
					if (Personas.Export != "csv" || EW_EXPORT_MASTER_RECORD_FOR_CSV) {
						Areas.ExportDocument(ref ExportDoc, ref rsmaster, 1, 1, "");
						ExportDoc.ExportEmptyLine();
					}
					ExportDoc.ChangeStyle(ExportStyle); // Restore
				}
				rsmaster.Close();
				rsmaster.Dispose();
			}
		}
		if (Personas.Export == "xml") {
			Personas.ExportXmlDocument(ref XmlDoc, ew_NotEmpty(ParentTable), ref Rs, StartRec, StopRec, "");
		} else {
			string sHeader = PageHeader;
			Page_DataRendering(ref sHeader);
			ExportDoc.Text += sHeader;
			Personas.ExportDocument(ref ExportDoc, ref Rs, StartRec, StopRec, "");
			string sFooter = PageFooter;
			Page_DataRendered(ref sFooter);
			ExportDoc.Text += sFooter;
		}

		// Close recordset
		Rs.Close();
		Rs.Dispose();

		// Export header and footer
		if (Personas.Export != "xml")
			ExportDoc.ExportHeaderAndFooter();

		// Clean output buffer
		if (!EW_DEBUG_ENABLED)
			HttpContext.Current.Response.Clear();

		// Write BOM if utf-8
		if (utf8 && Personas.Export != "email" && Personas.Export != "xml")
			HttpContext.Current.Response.BinaryWrite(new byte[]{0xEF, 0xBB, 0xBF});

		// Write debug message if enabled
		if (EW_DEBUG_ENABLED)
			ew_Write(ew_DebugMsg());

		// Output data
		if (Personas.Export == "xml") {
			ew_AddHeader("Content-Type", "text/xml");
			ew_Write(XmlDoc.XML());
		} else if (Personas.Export == "pdf") {
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
			if (sMasterTblVar == "Areas") {
				bValidMaster = true;				
				if (ew_NotEmpty(ew_Get("IdArea"))) {
					Areas.IdArea.QueryStringValue = ew_Get("IdArea");
					Personas.IdArea.QueryStringValue = Areas.IdArea.QueryStringValue;
					Personas.IdArea.SessionValue = Personas.IdArea.QueryStringValue;
					if (!Information.IsNumeric(Areas.IdArea.QueryStringValue)) bValidMaster = false;
				} else {
					bValidMaster = false;
				}
			}
		}
		if (bValidMaster) {

			// Save current master table
			Personas.CurrentMasterTable = sMasterTblVar;

			// Reset start record counter (new master key)
			StartRec = 1;
			Personas.StartRecordNumber = StartRec;

			// Clear previous master session values
			if (sMasterTblVar != "Areas") {
				if (ew_Empty(Personas.IdArea.QueryStringValue)) Personas.IdArea.SessionValue = "";
			}
		}
		DbMasterFilter = Personas.MasterFilter; // Get master filter
		DbDetailFilter = Personas.DetailFilter; // Get detail filter
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
		Personas_list = new cPersonas_list(this);
		CurrentPage = Personas_list;

		//CurrentPageType = Personas_list.GetType();
		Personas_list.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Personas_list.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (Personas_list != null)
			Personas_list.Dispose();
	}
}
