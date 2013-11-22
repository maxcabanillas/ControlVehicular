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

partial class HistoricoPeatoneslist: AspNetMaker9_ControlVehicular {

	// Page object
	public cHistoricoPeatones_list HistoricoPeatones_list;	

	//
	// Page Class
	//
	public class cHistoricoPeatones_list: AspNetMakerPage, IDisposable {

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
				if (HistoricoPeatones.UseTokenInUrl)
					Url += "t=" + HistoricoPeatones.TableVar + "&"; // Add page token
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
			if (HistoricoPeatones.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (HistoricoPeatones.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (HistoricoPeatones.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public HistoricoPeatoneslist AspNetPage { 
			get { return (HistoricoPeatoneslist)m_ParentPage; }
		}

		// HistoricoPeatones	
		public cHistoricoPeatones HistoricoPeatones { 
			get {				
				return ParentPage.HistoricoPeatones;
			}
			set {
				ParentPage.HistoricoPeatones = value;	
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
		public cHistoricoPeatones_list(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "list";
			m_PageObjName = "HistoricoPeatones_list";
			m_PageObjTypeName = "cHistoricoPeatones_list";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (HistoricoPeatones == null)
				HistoricoPeatones = new cHistoricoPeatones(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "HistoricoPeatones";
			m_Table = HistoricoPeatones;
			CurrentTable = HistoricoPeatones;

			//CurrentTableType = HistoricoPeatones.GetType();
			// Initialize URLs

			ExportPrintUrl = PageUrl + "export=print";
			ExportExcelUrl = PageUrl + "export=excel";
			ExportWordUrl = PageUrl + "export=word";
			ExportHtmlUrl = PageUrl + "export=html";
			ExportXmlUrl = PageUrl + "export=xml";
			ExportCsvUrl = PageUrl + "export=csv";
			ExportPdfUrl = PageUrl + "export=pdf";
			AddUrl = "HistoricoPeatonesadd.aspx";
			InlineAddUrl = PageUrl + "a=add";
			GridAddUrl = PageUrl + "a=gridadd";
			GridEditUrl = PageUrl + "a=gridedit";
			MultiDeleteUrl = "HistoricoPeatonesdelete.aspx";
			MultiUpdateUrl = "HistoricoPeatonesupdate.aspx";

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
				HistoricoPeatones.GridAddRowCount = gridaddcnt;

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
			HistoricoPeatones.Dispose();
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

		// Master/Detail
		DbMasterFilter = ""; // Master filter
		DbDetailFilter = ""; // Detail filter
		if (IsPageRequest()) { // Validate request

			// Handle reset command
			ResetCmd();

			// Hide all options
			if (ew_NotEmpty(HistoricoPeatones.Export) ||
				HistoricoPeatones.CurrentAction == "gridadd" ||
				HistoricoPeatones.CurrentAction == "gridedit") {
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
			HistoricoPeatones.Recordset_SearchValidated();

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
		if (HistoricoPeatones.RecordsPerPage == -1 || HistoricoPeatones.RecordsPerPage > 0) {
			DisplayRecs = HistoricoPeatones.RecordsPerPage; // Restore from Session
		} else {
			DisplayRecs = 10; // Load default
		}

		// Load Sorting Order
		LoadSortOrder();

		// Build search criteria
		ew_AddFilter(ref SearchWhere, sSrchAdvanced);
		ew_AddFilter(ref SearchWhere, sSrchBasic);

		// Recordset Searching event
		HistoricoPeatones.Recordset_Searching(ref SearchWhere);

		// Save search criteria
		if (ew_NotEmpty(SearchWhere)) {
			if (ew_Empty(sSrchBasic)) ResetBasicSearchParms();
			if (ew_Empty(sSrchAdvanced)) ResetAdvancedSearchParms();
			HistoricoPeatones.SearchWhere = SearchWhere; // Save to Session
			if (RestoreSearch) {
				StartRec = 1; // Reset start record counter
				HistoricoPeatones.StartRecordNumber = StartRec;
			}
		} else {
			SearchWhere = HistoricoPeatones.SearchWhere; 
		}

		// Build filter
		sFilter = "";
		if (!Security.CanList)
			sFilter = "(0=1)"; // Filter all records
		ew_AddFilter(ref sFilter, DbDetailFilter);
		ew_AddFilter(ref sFilter, SearchWhere);

		// Set up filter in Session
		HistoricoPeatones.SessionWhere = sFilter;
		HistoricoPeatones.CurrentFilter = "";
	}

	//
	// Return Advanced Search WHERE based on QueryString parameters
	//

	string AdvancedSearchWhere() {		
		if (!Security.CanSearch) return "";
		string sWhere = "";
		BuildSearchSql(ref sWhere, HistoricoPeatones.TipoDocumento, false); // TipoDocumento
		BuildSearchSql(ref sWhere, HistoricoPeatones.Documento, false); // Documento
		BuildSearchSql(ref sWhere, HistoricoPeatones.Nombre, false); // Nombre
		BuildSearchSql(ref sWhere, HistoricoPeatones.Apellidos, false); // Apellidos
		BuildSearchSql(ref sWhere, HistoricoPeatones.Area, false); // Area
		BuildSearchSql(ref sWhere, HistoricoPeatones.Persona, false); // Persona
		BuildSearchSql(ref sWhere, HistoricoPeatones.FechaIngreso, false); // FechaIngreso
		BuildSearchSql(ref sWhere, HistoricoPeatones.FechaSalida, false); // FechaSalida
		BuildSearchSql(ref sWhere, HistoricoPeatones.Observacion, false); // Observacion

		// Set up search parm
		if (ew_NotEmpty(sWhere)) {
			SetSearchParm(HistoricoPeatones.TipoDocumento); // TipoDocumento
			SetSearchParm(HistoricoPeatones.Documento); // Documento
			SetSearchParm(HistoricoPeatones.Nombre); // Nombre
			SetSearchParm(HistoricoPeatones.Apellidos); // Apellidos
			SetSearchParm(HistoricoPeatones.Area); // Area
			SetSearchParm(HistoricoPeatones.Persona); // Persona
			SetSearchParm(HistoricoPeatones.FechaIngreso); // FechaIngreso
			SetSearchParm(HistoricoPeatones.FechaSalida); // FechaSalida
			SetSearchParm(HistoricoPeatones.Observacion); // Observacion
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
		HistoricoPeatones.SetAdvancedSearch("x_" + FldParm, Fld.AdvancedSearch.SearchValue);
		HistoricoPeatones.SetAdvancedSearch("z_" + FldParm, Fld.AdvancedSearch.SearchOperator);
		HistoricoPeatones.SetAdvancedSearch("v_" + FldParm, Fld.AdvancedSearch.SearchCondition);
		HistoricoPeatones.SetAdvancedSearch("y_" + FldParm, Fld.AdvancedSearch.SearchValue2);
		HistoricoPeatones.SetAdvancedSearch("w_" + FldParm, Fld.AdvancedSearch.SearchOperator2);
	}

	//
	// Get search parm
	//
	public void GetSearchParm(cField Fld) {
		string FldParm = Fld.FldVar.Substring(2);
		Fld.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_" + FldParm);
		Fld.AdvancedSearch.SearchOperator = HistoricoPeatones.GetAdvancedSearch("z_" + FldParm);
		Fld.AdvancedSearch.SearchCondition = HistoricoPeatones.GetAdvancedSearch("v_" + FldParm);
		Fld.AdvancedSearch.SearchValue2 = HistoricoPeatones.GetAdvancedSearch("y_" + FldParm);
		Fld.AdvancedSearch.SearchOperator2 = HistoricoPeatones.GetAdvancedSearch("w_" + FldParm);
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
			BuildBasicSearchSQL(ref sWhere, HistoricoPeatones.TipoDocumento, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoPeatones.Documento, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoPeatones.Nombre, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoPeatones.Apellidos, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoPeatones.Area, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoPeatones.Persona, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoPeatones.Observacion, Keyword); 
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
		string sSearchKeyword = HistoricoPeatones.BasicSearchKeyword;
		string sSearchType = HistoricoPeatones.BasicSearchType;
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
			HistoricoPeatones.SessionBasicSearchKeyword = sSearchKeyword;
			HistoricoPeatones.SessionBasicSearchType = sSearchType;
		}
		return sSearchStr;
	}

	//
	// Clear all search parameters
	//
	public void ResetSearchParms() {

		// Clear search where
		SearchWhere = "";
		HistoricoPeatones.SearchWhere = SearchWhere;

		// Clear basic search parameters
		ResetBasicSearchParms();

		// Clear advanced search parameters
		ResetAdvancedSearchParms();
	}

	//
	// Clear all basic search parameters
	//
	public void ResetBasicSearchParms()	{
		HistoricoPeatones.SessionBasicSearchKeyword = "";
		HistoricoPeatones.SessionBasicSearchType = "";
	}

	//
	// Clear all advanced search parameters
	//
	public void ResetAdvancedSearchParms() {
		HistoricoPeatones.SetAdvancedSearch("x_TipoDocumento", "");
		HistoricoPeatones.SetAdvancedSearch("x_Documento", "");
		HistoricoPeatones.SetAdvancedSearch("x_Nombre", "");
		HistoricoPeatones.SetAdvancedSearch("x_Apellidos", "");
		HistoricoPeatones.SetAdvancedSearch("x_Area", "");
		HistoricoPeatones.SetAdvancedSearch("x_Persona", "");
		HistoricoPeatones.SetAdvancedSearch("x_FechaIngreso", "");
		HistoricoPeatones.SetAdvancedSearch("y_FechaIngreso", "");
		HistoricoPeatones.SetAdvancedSearch("x_FechaSalida", "");
		HistoricoPeatones.SetAdvancedSearch("y_FechaSalida", "");
		HistoricoPeatones.SetAdvancedSearch("x_Observacion", "");
	}

	//
	// Restore all search parameters
	//
	public void RestoreSearchParms() {
		bool bRestore = true;
		if (ew_NotEmpty(HistoricoPeatones.BasicSearchKeyword))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.TipoDocumento.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.Documento.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.Nombre.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.Apellidos.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.Area.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.Persona.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue2))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue2))
			bRestore = false;
		if (ew_NotEmpty(HistoricoPeatones.Observacion.AdvancedSearch.SearchValue))
			bRestore = false;
		RestoreSearch = bRestore;
		if (bRestore) {

			// Restore basic search values
			HistoricoPeatones.BasicSearchKeyword = HistoricoPeatones.SessionBasicSearchKeyword;
			HistoricoPeatones.BasicSearchType = HistoricoPeatones.SessionBasicSearchType;

			// Restore advanced search values
			GetSearchParm(HistoricoPeatones.TipoDocumento);
			GetSearchParm(HistoricoPeatones.Documento);
			GetSearchParm(HistoricoPeatones.Nombre);
			GetSearchParm(HistoricoPeatones.Apellidos);
			GetSearchParm(HistoricoPeatones.Area);
			GetSearchParm(HistoricoPeatones.Persona);
			GetSearchParm(HistoricoPeatones.FechaIngreso);
			GetSearchParm(HistoricoPeatones.FechaSalida);
			GetSearchParm(HistoricoPeatones.Observacion);
		}
	}

	//
	// Set up Sort parameters based on Sort Links clicked
	//
	public void SetUpSortOrder() {

		// Check for an Order parameter
		if (ew_NotEmpty(ew_Get("order"))) {
			HistoricoPeatones.CurrentOrder = ew_Get("order");
			HistoricoPeatones.CurrentOrderType = ew_Get("ordertype");
			HistoricoPeatones.UpdateSort(HistoricoPeatones.TipoDocumento); // TipoDocumento
			HistoricoPeatones.UpdateSort(HistoricoPeatones.Documento); // Documento
			HistoricoPeatones.UpdateSort(HistoricoPeatones.Nombre); // Nombre
			HistoricoPeatones.UpdateSort(HistoricoPeatones.Apellidos); // Apellidos
			HistoricoPeatones.UpdateSort(HistoricoPeatones.Area); // Area
			HistoricoPeatones.UpdateSort(HistoricoPeatones.Persona); // Persona
			HistoricoPeatones.UpdateSort(HistoricoPeatones.FechaIngreso); // FechaIngreso
			HistoricoPeatones.UpdateSort(HistoricoPeatones.FechaSalida); // FechaSalida
			HistoricoPeatones.StartRecordNumber = 1; // Reset start position
		}
	}

	//
	// Load Sort Order parameters
	//
	public void LoadSortOrder()	{
		string sOrderBy = HistoricoPeatones.SessionOrderBy; // Get order by from Session
		if (ew_Empty(sOrderBy)) {
			if (ew_NotEmpty(HistoricoPeatones.SqlOrderBy)) {
				sOrderBy = HistoricoPeatones.SqlOrderBy;
				HistoricoPeatones.SessionOrderBy = sOrderBy;
				HistoricoPeatones.FechaIngreso.Sort = "DESC";
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

			// Reset sort criteria
			if (ew_SameText(sCmd, "resetsort")) {
				string sOrderBy = "";
				HistoricoPeatones.SessionOrderBy = sOrderBy;
				HistoricoPeatones.TipoDocumento.Sort = "";
				HistoricoPeatones.Documento.Sort = "";
				HistoricoPeatones.Nombre.Sort = "";
				HistoricoPeatones.Apellidos.Sort = "";
				HistoricoPeatones.Area.Sort = "";
				HistoricoPeatones.Persona.Sort = "";
				HistoricoPeatones.FechaIngreso.Sort = "";
				HistoricoPeatones.FechaSalida.Sort = "";
			}

			// Reset start position
			StartRec = 1;
			HistoricoPeatones.StartRecordNumber = StartRec;
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
				HistoricoPeatones.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				HistoricoPeatones.StartRecordNumber = StartRec;
			}
		}
		StartRec = HistoricoPeatones.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			HistoricoPeatones.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			HistoricoPeatones.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			HistoricoPeatones.StartRecordNumber = StartRec;
		}
	}

	//
	// Load basic search values
	//
	public void LoadBasicSearchValues() {
		HistoricoPeatones.BasicSearchKeyword = ew_Get(EW_TABLE_BASIC_SEARCH);
		HistoricoPeatones.BasicSearchType = ew_Get(EW_TABLE_BASIC_SEARCH_TYPE);
	}

	//
	//  Load search values for validation
	//
	public void LoadSearchValues() {
		HistoricoPeatones.TipoDocumento.AdvancedSearch.SearchValue = ew_Get("x_TipoDocumento");
    HistoricoPeatones.TipoDocumento.AdvancedSearch.SearchOperator = ew_Get("z_TipoDocumento");
		HistoricoPeatones.Documento.AdvancedSearch.SearchValue = ew_Get("x_Documento");
    HistoricoPeatones.Documento.AdvancedSearch.SearchOperator = ew_Get("z_Documento");
		HistoricoPeatones.Nombre.AdvancedSearch.SearchValue = ew_Get("x_Nombre");
    HistoricoPeatones.Nombre.AdvancedSearch.SearchOperator = ew_Get("z_Nombre");
		HistoricoPeatones.Apellidos.AdvancedSearch.SearchValue = ew_Get("x_Apellidos");
    HistoricoPeatones.Apellidos.AdvancedSearch.SearchOperator = ew_Get("z_Apellidos");
		HistoricoPeatones.Area.AdvancedSearch.SearchValue = ew_Get("x_Area");
    HistoricoPeatones.Area.AdvancedSearch.SearchOperator = ew_Get("z_Area");
		HistoricoPeatones.Persona.AdvancedSearch.SearchValue = ew_Get("x_Persona");
    HistoricoPeatones.Persona.AdvancedSearch.SearchOperator = ew_Get("z_Persona");
		HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue = ew_Get("x_FechaIngreso");
    HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchOperator = ew_Get("z_FechaIngreso");
		HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchCondition = ew_Get("v_FechaIngreso");
   	HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue2 = ew_Get("y_FechaIngreso");
   	HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchOperator2 = ew_Get("w_FechaIngreso");
		HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue = ew_Get("x_FechaSalida");
    HistoricoPeatones.FechaSalida.AdvancedSearch.SearchOperator = ew_Get("z_FechaSalida");
		HistoricoPeatones.FechaSalida.AdvancedSearch.SearchCondition = ew_Get("v_FechaSalida");
   	HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue2 = ew_Get("y_FechaSalida");
   	HistoricoPeatones.FechaSalida.AdvancedSearch.SearchOperator2 = ew_Get("w_FechaSalida");
		HistoricoPeatones.Observacion.AdvancedSearch.SearchValue = ew_Get("x_Observacion");
    HistoricoPeatones.Observacion.AdvancedSearch.SearchOperator = ew_Get("z_Observacion");
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = HistoricoPeatones.CurrentFilter;
		HistoricoPeatones.Recordset_Selecting(ref sFilter);
		HistoricoPeatones.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = HistoricoPeatones.ListSQL;
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
		HistoricoPeatones.Recordset_Selected(Rs);
		return Rs;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = HistoricoPeatones.KeyFilter;

		// Row Selecting event
		HistoricoPeatones.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		HistoricoPeatones.CurrentFilter = sFilter;
		string sSql = HistoricoPeatones.SQL;

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
		HistoricoPeatones.Row_Selected(ref row);
		HistoricoPeatones.TipoDocumento.DbValue = row["TipoDocumento"];
		HistoricoPeatones.Documento.DbValue = row["Documento"];
		HistoricoPeatones.Nombre.DbValue = row["Nombre"];
		HistoricoPeatones.Apellidos.DbValue = row["Apellidos"];
		HistoricoPeatones.Area.DbValue = row["Area"];
		HistoricoPeatones.Persona.DbValue = row["Persona"];
		HistoricoPeatones.FechaIngreso.DbValue = row["FechaIngreso"];
		HistoricoPeatones.FechaSalida.DbValue = row["FechaSalida"];
		HistoricoPeatones.Observacion.DbValue = row["Observacion"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;

		// Load old recordset
		if (bValidKey) {
			HistoricoPeatones.CurrentFilter = HistoricoPeatones.KeyFilter;
			string sSql = HistoricoPeatones.SQL;			
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
		ViewUrl = HistoricoPeatones.ViewUrl;
		EditUrl = HistoricoPeatones.EditUrl;
		InlineEditUrl = HistoricoPeatones.InlineEditUrl;
		CopyUrl = HistoricoPeatones.CopyUrl;
		InlineCopyUrl = HistoricoPeatones.InlineCopyUrl;
		DeleteUrl = HistoricoPeatones.DeleteUrl;

		// Row Rendering event
		HistoricoPeatones.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// TipoDocumento
		// Documento
		// Nombre
		// Apellidos
		// Area
		// Persona
		// FechaIngreso
		// FechaSalida
		// Observacion
		//
		//  View  Row
		//

		if (HistoricoPeatones.RowType == EW_ROWTYPE_VIEW) { // View row

			// TipoDocumento
			HistoricoPeatones.TipoDocumento.ViewCustomAttributes = "";

			// Documento
				HistoricoPeatones.Documento.ViewValue = HistoricoPeatones.Documento.CurrentValue;
			HistoricoPeatones.Documento.ViewCustomAttributes = "";

			// Nombre
				HistoricoPeatones.Nombre.ViewValue = HistoricoPeatones.Nombre.CurrentValue;
			HistoricoPeatones.Nombre.ViewCustomAttributes = "";

			// Apellidos
				HistoricoPeatones.Apellidos.ViewValue = HistoricoPeatones.Apellidos.CurrentValue;
			HistoricoPeatones.Apellidos.ViewCustomAttributes = "";

			// Area
			if (ew_NotEmpty(HistoricoPeatones.Area.CurrentValue)) {
				sFilterWrk = "[Area] = '" + ew_AdjustSql(HistoricoPeatones.Area.CurrentValue) + "'";
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
					HistoricoPeatones.Area.ViewValue = drWrk["Area"];
					HistoricoPeatones.Area.ViewValue = String.Concat(HistoricoPeatones.Area.ViewValue, ew_ValueSeparator(0, 1, HistoricoPeatones.Area), drWrk["Codigo"]);
				} else {
					HistoricoPeatones.Area.ViewValue = HistoricoPeatones.Area.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				HistoricoPeatones.Area.ViewValue = System.DBNull.Value;
			}
			HistoricoPeatones.Area.ViewCustomAttributes = "";

			// Persona
			if (ew_NotEmpty(HistoricoPeatones.Persona.CurrentValue)) {
				sFilterWrk = "[Persona] = '" + ew_AdjustSql(HistoricoPeatones.Persona.CurrentValue) + "'";
			sSqlWrk = "SELECT [Persona] FROM [dbo].[Personas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Persona]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					HistoricoPeatones.Persona.ViewValue = drWrk["Persona"];
				} else {
					HistoricoPeatones.Persona.ViewValue = HistoricoPeatones.Persona.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				HistoricoPeatones.Persona.ViewValue = System.DBNull.Value;
			}
			HistoricoPeatones.Persona.ViewCustomAttributes = "";

			// FechaIngreso
				HistoricoPeatones.FechaIngreso.ViewValue = HistoricoPeatones.FechaIngreso.CurrentValue;
				HistoricoPeatones.FechaIngreso.ViewValue = ew_FormatDateTime(HistoricoPeatones.FechaIngreso.ViewValue, 7);
			HistoricoPeatones.FechaIngreso.ViewCustomAttributes = "";

			// FechaSalida
				HistoricoPeatones.FechaSalida.ViewValue = HistoricoPeatones.FechaSalida.CurrentValue;
				HistoricoPeatones.FechaSalida.ViewValue = ew_FormatDateTime(HistoricoPeatones.FechaSalida.ViewValue, 7);
			HistoricoPeatones.FechaSalida.ViewCustomAttributes = "";

			// View refer script
			// TipoDocumento

			HistoricoPeatones.TipoDocumento.LinkCustomAttributes = "";
			HistoricoPeatones.TipoDocumento.HrefValue = "";
			HistoricoPeatones.TipoDocumento.TooltipValue = "";

			// Documento
			HistoricoPeatones.Documento.LinkCustomAttributes = "";
			HistoricoPeatones.Documento.HrefValue = "";
			HistoricoPeatones.Documento.TooltipValue = "";

			// Nombre
			HistoricoPeatones.Nombre.LinkCustomAttributes = "";
			HistoricoPeatones.Nombre.HrefValue = "";
			HistoricoPeatones.Nombre.TooltipValue = "";

			// Apellidos
			HistoricoPeatones.Apellidos.LinkCustomAttributes = "";
			HistoricoPeatones.Apellidos.HrefValue = "";
			HistoricoPeatones.Apellidos.TooltipValue = "";

			// Area
			HistoricoPeatones.Area.LinkCustomAttributes = "";
			HistoricoPeatones.Area.HrefValue = "";
			HistoricoPeatones.Area.TooltipValue = "";

			// Persona
			HistoricoPeatones.Persona.LinkCustomAttributes = "";
			HistoricoPeatones.Persona.HrefValue = "";
			HistoricoPeatones.Persona.TooltipValue = "";

			// FechaIngreso
			HistoricoPeatones.FechaIngreso.LinkCustomAttributes = "";
			HistoricoPeatones.FechaIngreso.HrefValue = "";
			HistoricoPeatones.FechaIngreso.TooltipValue = "";

			// FechaSalida
			HistoricoPeatones.FechaSalida.LinkCustomAttributes = "";
			HistoricoPeatones.FechaSalida.HrefValue = "";
			HistoricoPeatones.FechaSalida.TooltipValue = "";
		}

		// Row Rendered event
		if (HistoricoPeatones.RowType != EW_ROWTYPE_AGGREGATEINIT)
			HistoricoPeatones.Row_Rendered();
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
		HistoricoPeatones.TipoDocumento.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_TipoDocumento");
		HistoricoPeatones.Documento.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_Documento");
		HistoricoPeatones.Nombre.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_Nombre");
		HistoricoPeatones.Apellidos.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_Apellidos");
		HistoricoPeatones.Area.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_Area");
		HistoricoPeatones.Persona.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_Persona");
		HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_FechaIngreso");
		HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue2 = HistoricoPeatones.GetAdvancedSearch("y_FechaIngreso");
		HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_FechaSalida");
		HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue2 = HistoricoPeatones.GetAdvancedSearch("y_FechaSalida");
		HistoricoPeatones.Observacion.AdvancedSearch.SearchValue = HistoricoPeatones.GetAdvancedSearch("x_Observacion");
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
		HistoricoPeatones_list = new cHistoricoPeatones_list(this);
		CurrentPage = HistoricoPeatones_list;

		//CurrentPageType = HistoricoPeatones_list.GetType();
		HistoricoPeatones_list.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		HistoricoPeatones_list.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (HistoricoPeatones_list != null)
			HistoricoPeatones_list.Dispose();
	}
}
