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

partial class HistoricoVehiculoslist: AspNetMaker9_ControlVehicular {

	// Page object
	public cHistoricoVehiculos_list HistoricoVehiculos_list;	

	//
	// Page Class
	//
	public class cHistoricoVehiculos_list: AspNetMakerPage, IDisposable {

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
				if (HistoricoVehiculos.UseTokenInUrl)
					Url += "t=" + HistoricoVehiculos.TableVar + "&"; // Add page token
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
			if (HistoricoVehiculos.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (HistoricoVehiculos.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (HistoricoVehiculos.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public HistoricoVehiculoslist AspNetPage { 
			get { return (HistoricoVehiculoslist)m_ParentPage; }
		}

		// HistoricoVehiculos	
		public cHistoricoVehiculos HistoricoVehiculos { 
			get {				
				return ParentPage.HistoricoVehiculos;
			}
			set {
				ParentPage.HistoricoVehiculos = value;	
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
		public cHistoricoVehiculos_list(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "list";
			m_PageObjName = "HistoricoVehiculos_list";
			m_PageObjTypeName = "cHistoricoVehiculos_list";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (HistoricoVehiculos == null)
				HistoricoVehiculos = new cHistoricoVehiculos(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "HistoricoVehiculos";
			m_Table = HistoricoVehiculos;
			CurrentTable = HistoricoVehiculos;

			//CurrentTableType = HistoricoVehiculos.GetType();
			// Initialize URLs

			ExportPrintUrl = PageUrl + "export=print";
			ExportExcelUrl = PageUrl + "export=excel";
			ExportWordUrl = PageUrl + "export=word";
			ExportHtmlUrl = PageUrl + "export=html";
			ExportXmlUrl = PageUrl + "export=xml";
			ExportCsvUrl = PageUrl + "export=csv";
			ExportPdfUrl = PageUrl + "export=pdf";
			AddUrl = "HistoricoVehiculosadd.aspx";
			InlineAddUrl = PageUrl + "a=add";
			GridAddUrl = PageUrl + "a=gridadd";
			GridEditUrl = PageUrl + "a=gridedit";
			MultiDeleteUrl = "HistoricoVehiculosdelete.aspx";
			MultiUpdateUrl = "HistoricoVehiculosupdate.aspx";

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
				HistoricoVehiculos.GridAddRowCount = gridaddcnt;

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
			HistoricoVehiculos.Dispose();
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
			if (ew_NotEmpty(HistoricoVehiculos.Export) ||
				HistoricoVehiculos.CurrentAction == "gridadd" ||
				HistoricoVehiculos.CurrentAction == "gridedit") {
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
			HistoricoVehiculos.Recordset_SearchValidated();

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
		if (HistoricoVehiculos.RecordsPerPage == -1 || HistoricoVehiculos.RecordsPerPage > 0) {
			DisplayRecs = HistoricoVehiculos.RecordsPerPage; // Restore from Session
		} else {
			DisplayRecs = 10; // Load default
		}

		// Load Sorting Order
		LoadSortOrder();

		// Build search criteria
		ew_AddFilter(ref SearchWhere, sSrchAdvanced);
		ew_AddFilter(ref SearchWhere, sSrchBasic);

		// Recordset Searching event
		HistoricoVehiculos.Recordset_Searching(ref SearchWhere);

		// Save search criteria
		if (ew_NotEmpty(SearchWhere)) {
			if (ew_Empty(sSrchBasic)) ResetBasicSearchParms();
			if (ew_Empty(sSrchAdvanced)) ResetAdvancedSearchParms();
			HistoricoVehiculos.SearchWhere = SearchWhere; // Save to Session
			if (RestoreSearch) {
				StartRec = 1; // Reset start record counter
				HistoricoVehiculos.StartRecordNumber = StartRec;
			}
		} else {
			SearchWhere = HistoricoVehiculos.SearchWhere; 
		}

		// Build filter
		sFilter = "";
		if (!Security.CanList)
			sFilter = "(0=1)"; // Filter all records
		ew_AddFilter(ref sFilter, DbDetailFilter);
		ew_AddFilter(ref sFilter, SearchWhere);

		// Set up filter in Session
		HistoricoVehiculos.SessionWhere = sFilter;
		HistoricoVehiculos.CurrentFilter = "";
	}

	//
	// Return Advanced Search WHERE based on QueryString parameters
	//

	string AdvancedSearchWhere() {		
		if (!Security.CanSearch) return "";
		string sWhere = "";
		BuildSearchSql(ref sWhere, HistoricoVehiculos.TipoVehiculo, false); // TipoVehiculo
		BuildSearchSql(ref sWhere, HistoricoVehiculos.Placa, false); // Placa
		BuildSearchSql(ref sWhere, HistoricoVehiculos.FechaIngreso, false); // FechaIngreso
		BuildSearchSql(ref sWhere, HistoricoVehiculos.FechaSalida, false); // FechaSalida
		BuildSearchSql(ref sWhere, HistoricoVehiculos.Observaciones, false); // Observaciones

		// Set up search parm
		if (ew_NotEmpty(sWhere)) {
			SetSearchParm(HistoricoVehiculos.TipoVehiculo); // TipoVehiculo
			SetSearchParm(HistoricoVehiculos.Placa); // Placa
			SetSearchParm(HistoricoVehiculos.FechaIngreso); // FechaIngreso
			SetSearchParm(HistoricoVehiculos.FechaSalida); // FechaSalida
			SetSearchParm(HistoricoVehiculos.Observaciones); // Observaciones
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
		HistoricoVehiculos.SetAdvancedSearch("x_" + FldParm, Fld.AdvancedSearch.SearchValue);
		HistoricoVehiculos.SetAdvancedSearch("z_" + FldParm, Fld.AdvancedSearch.SearchOperator);
		HistoricoVehiculos.SetAdvancedSearch("v_" + FldParm, Fld.AdvancedSearch.SearchCondition);
		HistoricoVehiculos.SetAdvancedSearch("y_" + FldParm, Fld.AdvancedSearch.SearchValue2);
		HistoricoVehiculos.SetAdvancedSearch("w_" + FldParm, Fld.AdvancedSearch.SearchOperator2);
	}

	//
	// Get search parm
	//
	public void GetSearchParm(cField Fld) {
		string FldParm = Fld.FldVar.Substring(2);
		Fld.AdvancedSearch.SearchValue = HistoricoVehiculos.GetAdvancedSearch("x_" + FldParm);
		Fld.AdvancedSearch.SearchOperator = HistoricoVehiculos.GetAdvancedSearch("z_" + FldParm);
		Fld.AdvancedSearch.SearchCondition = HistoricoVehiculos.GetAdvancedSearch("v_" + FldParm);
		Fld.AdvancedSearch.SearchValue2 = HistoricoVehiculos.GetAdvancedSearch("y_" + FldParm);
		Fld.AdvancedSearch.SearchOperator2 = HistoricoVehiculos.GetAdvancedSearch("w_" + FldParm);
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
			BuildBasicSearchSQL(ref sWhere, HistoricoVehiculos.TipoVehiculo, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoVehiculos.Placa, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoVehiculos.HoraIngreso, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoVehiculos.HoraSalida, Keyword); 
			BuildBasicSearchSQL(ref sWhere, HistoricoVehiculos.Observaciones, Keyword); 
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
		string sSearchKeyword = HistoricoVehiculos.BasicSearchKeyword;
		string sSearchType = HistoricoVehiculos.BasicSearchType;
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
			HistoricoVehiculos.SessionBasicSearchKeyword = sSearchKeyword;
			HistoricoVehiculos.SessionBasicSearchType = sSearchType;
		}
		return sSearchStr;
	}

	//
	// Clear all search parameters
	//
	public void ResetSearchParms() {

		// Clear search where
		SearchWhere = "";
		HistoricoVehiculos.SearchWhere = SearchWhere;

		// Clear basic search parameters
		ResetBasicSearchParms();

		// Clear advanced search parameters
		ResetAdvancedSearchParms();
	}

	//
	// Clear all basic search parameters
	//
	public void ResetBasicSearchParms()	{
		HistoricoVehiculos.SessionBasicSearchKeyword = "";
		HistoricoVehiculos.SessionBasicSearchType = "";
	}

	//
	// Clear all advanced search parameters
	//
	public void ResetAdvancedSearchParms() {
		HistoricoVehiculos.SetAdvancedSearch("x_TipoVehiculo", "");
		HistoricoVehiculos.SetAdvancedSearch("x_Placa", "");
		HistoricoVehiculos.SetAdvancedSearch("x_FechaIngreso", "");
		HistoricoVehiculos.SetAdvancedSearch("y_FechaIngreso", "");
		HistoricoVehiculos.SetAdvancedSearch("x_FechaSalida", "");
		HistoricoVehiculos.SetAdvancedSearch("y_FechaSalida", "");
		HistoricoVehiculos.SetAdvancedSearch("x_Observaciones", "");
	}

	//
	// Restore all search parameters
	//
	public void RestoreSearchParms() {
		bool bRestore = true;
		if (ew_NotEmpty(HistoricoVehiculos.BasicSearchKeyword))
			bRestore = false;
		if (ew_NotEmpty(HistoricoVehiculos.TipoVehiculo.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoVehiculos.Placa.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue2))
			bRestore = false;
		if (ew_NotEmpty(HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue))
			bRestore = false;
		if (ew_NotEmpty(HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue2))
			bRestore = false;
		if (ew_NotEmpty(HistoricoVehiculos.Observaciones.AdvancedSearch.SearchValue))
			bRestore = false;
		RestoreSearch = bRestore;
		if (bRestore) {

			// Restore basic search values
			HistoricoVehiculos.BasicSearchKeyword = HistoricoVehiculos.SessionBasicSearchKeyword;
			HistoricoVehiculos.BasicSearchType = HistoricoVehiculos.SessionBasicSearchType;

			// Restore advanced search values
			GetSearchParm(HistoricoVehiculos.TipoVehiculo);
			GetSearchParm(HistoricoVehiculos.Placa);
			GetSearchParm(HistoricoVehiculos.FechaIngreso);
			GetSearchParm(HistoricoVehiculos.FechaSalida);
			GetSearchParm(HistoricoVehiculos.Observaciones);
		}
	}

	//
	// Set up Sort parameters based on Sort Links clicked
	//
	public void SetUpSortOrder() {

		// Check for an Order parameter
		if (ew_NotEmpty(ew_Get("order"))) {
			HistoricoVehiculos.CurrentOrder = ew_Get("order");
			HistoricoVehiculos.CurrentOrderType = ew_Get("ordertype");
			HistoricoVehiculos.UpdateSort(HistoricoVehiculos.TipoVehiculo); // TipoVehiculo
			HistoricoVehiculos.UpdateSort(HistoricoVehiculos.Placa); // Placa
			HistoricoVehiculos.UpdateSort(HistoricoVehiculos.FechaIngreso); // FechaIngreso
			HistoricoVehiculos.UpdateSort(HistoricoVehiculos.HoraIngreso); // HoraIngreso
			HistoricoVehiculos.UpdateSort(HistoricoVehiculos.FechaSalida); // FechaSalida
			HistoricoVehiculos.UpdateSort(HistoricoVehiculos.HoraSalida); // HoraSalida
			HistoricoVehiculos.UpdateSort(HistoricoVehiculos.Observaciones); // Observaciones
			HistoricoVehiculos.StartRecordNumber = 1; // Reset start position
		}
	}

	//
	// Load Sort Order parameters
	//
	public void LoadSortOrder()	{
		string sOrderBy = HistoricoVehiculos.SessionOrderBy; // Get order by from Session
		if (ew_Empty(sOrderBy)) {
			if (ew_NotEmpty(HistoricoVehiculos.SqlOrderBy)) {
				sOrderBy = HistoricoVehiculos.SqlOrderBy;
				HistoricoVehiculos.SessionOrderBy = sOrderBy;
				HistoricoVehiculos.FechaIngreso.Sort = "DESC";
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
				HistoricoVehiculos.SessionOrderBy = sOrderBy;
				HistoricoVehiculos.TipoVehiculo.Sort = "";
				HistoricoVehiculos.Placa.Sort = "";
				HistoricoVehiculos.FechaIngreso.Sort = "";
				HistoricoVehiculos.HoraIngreso.Sort = "";
				HistoricoVehiculos.FechaSalida.Sort = "";
				HistoricoVehiculos.HoraSalida.Sort = "";
				HistoricoVehiculos.Observaciones.Sort = "";
			}

			// Reset start position
			StartRec = 1;
			HistoricoVehiculos.StartRecordNumber = StartRec;
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
				HistoricoVehiculos.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				HistoricoVehiculos.StartRecordNumber = StartRec;
			}
		}
		StartRec = HistoricoVehiculos.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			HistoricoVehiculos.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			HistoricoVehiculos.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			HistoricoVehiculos.StartRecordNumber = StartRec;
		}
	}

	//
	// Load basic search values
	//
	public void LoadBasicSearchValues() {
		HistoricoVehiculos.BasicSearchKeyword = ew_Get(EW_TABLE_BASIC_SEARCH);
		HistoricoVehiculos.BasicSearchType = ew_Get(EW_TABLE_BASIC_SEARCH_TYPE);
	}

	//
	//  Load search values for validation
	//
	public void LoadSearchValues() {
		HistoricoVehiculos.TipoVehiculo.AdvancedSearch.SearchValue = ew_Get("x_TipoVehiculo");
    HistoricoVehiculos.TipoVehiculo.AdvancedSearch.SearchOperator = ew_Get("z_TipoVehiculo");
		HistoricoVehiculos.Placa.AdvancedSearch.SearchValue = ew_Get("x_Placa");
    HistoricoVehiculos.Placa.AdvancedSearch.SearchOperator = ew_Get("z_Placa");
		HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue = ew_Get("x_FechaIngreso");
    HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchOperator = ew_Get("z_FechaIngreso");
		HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchCondition = ew_Get("v_FechaIngreso");
   	HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue2 = ew_Get("y_FechaIngreso");
   	HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchOperator2 = ew_Get("w_FechaIngreso");
		HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue = ew_Get("x_FechaSalida");
    HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchOperator = ew_Get("z_FechaSalida");
		HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchCondition = ew_Get("v_FechaSalida");
   	HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue2 = ew_Get("y_FechaSalida");
   	HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchOperator2 = ew_Get("w_FechaSalida");
		HistoricoVehiculos.Observaciones.AdvancedSearch.SearchValue = ew_Get("x_Observaciones");
    HistoricoVehiculos.Observaciones.AdvancedSearch.SearchOperator = ew_Get("z_Observaciones");
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = HistoricoVehiculos.CurrentFilter;
		HistoricoVehiculos.Recordset_Selecting(ref sFilter);
		HistoricoVehiculos.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = HistoricoVehiculos.ListSQL;
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
		HistoricoVehiculos.Recordset_Selected(Rs);
		return Rs;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = HistoricoVehiculos.KeyFilter;

		// Row Selecting event
		HistoricoVehiculos.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		HistoricoVehiculos.CurrentFilter = sFilter;
		string sSql = HistoricoVehiculos.SQL;

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
		HistoricoVehiculos.Row_Selected(ref row);
		HistoricoVehiculos.TipoVehiculo.DbValue = row["TipoVehiculo"];
		HistoricoVehiculos.Placa.DbValue = row["Placa"];
		HistoricoVehiculos.FechaIngreso.DbValue = row["FechaIngreso"];
		HistoricoVehiculos.HoraIngreso.DbValue = row["HoraIngreso"];
		HistoricoVehiculos.FechaSalida.DbValue = row["FechaSalida"];
		HistoricoVehiculos.HoraSalida.DbValue = row["HoraSalida"];
		HistoricoVehiculos.Observaciones.DbValue = row["Observaciones"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;

		// Load old recordset
		if (bValidKey) {
			HistoricoVehiculos.CurrentFilter = HistoricoVehiculos.KeyFilter;
			string sSql = HistoricoVehiculos.SQL;			
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
		ViewUrl = HistoricoVehiculos.ViewUrl;
		EditUrl = HistoricoVehiculos.EditUrl;
		InlineEditUrl = HistoricoVehiculos.InlineEditUrl;
		CopyUrl = HistoricoVehiculos.CopyUrl;
		InlineCopyUrl = HistoricoVehiculos.InlineCopyUrl;
		DeleteUrl = HistoricoVehiculos.DeleteUrl;

		// Row Rendering event
		HistoricoVehiculos.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// TipoVehiculo
		// Placa
		// FechaIngreso
		// HoraIngreso
		// FechaSalida
		// HoraSalida
		// Observaciones
		//
		//  View  Row
		//

		if (HistoricoVehiculos.RowType == EW_ROWTYPE_VIEW) { // View row

			// TipoVehiculo
			if (ew_NotEmpty(HistoricoVehiculos.TipoVehiculo.CurrentValue)) {
				sFilterWrk = "[TipoVehiculo] = '" + ew_AdjustSql(HistoricoVehiculos.TipoVehiculo.CurrentValue) + "'";
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
					HistoricoVehiculos.TipoVehiculo.ViewValue = drWrk["TipoVehiculo"];
				} else {
					HistoricoVehiculos.TipoVehiculo.ViewValue = HistoricoVehiculos.TipoVehiculo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				HistoricoVehiculos.TipoVehiculo.ViewValue = System.DBNull.Value;
			}
			HistoricoVehiculos.TipoVehiculo.ViewCustomAttributes = "";

			// Placa
				HistoricoVehiculos.Placa.ViewValue = HistoricoVehiculos.Placa.CurrentValue;
			HistoricoVehiculos.Placa.ViewCustomAttributes = "";

			// FechaIngreso
				HistoricoVehiculos.FechaIngreso.ViewValue = HistoricoVehiculos.FechaIngreso.CurrentValue;
				HistoricoVehiculos.FechaIngreso.ViewValue = ew_FormatDateTime(HistoricoVehiculos.FechaIngreso.ViewValue, 7);
			HistoricoVehiculos.FechaIngreso.ViewCustomAttributes = "";

			// HoraIngreso
				HistoricoVehiculos.HoraIngreso.ViewValue = HistoricoVehiculos.HoraIngreso.CurrentValue;
			HistoricoVehiculos.HoraIngreso.ViewCustomAttributes = "";

			// FechaSalida
				HistoricoVehiculos.FechaSalida.ViewValue = HistoricoVehiculos.FechaSalida.CurrentValue;
				HistoricoVehiculos.FechaSalida.ViewValue = ew_FormatDateTime(HistoricoVehiculos.FechaSalida.ViewValue, 7);
			HistoricoVehiculos.FechaSalida.ViewCustomAttributes = "";

			// HoraSalida
				HistoricoVehiculos.HoraSalida.ViewValue = HistoricoVehiculos.HoraSalida.CurrentValue;
			HistoricoVehiculos.HoraSalida.ViewCustomAttributes = "";

			// Observaciones
			HistoricoVehiculos.Observaciones.ViewValue = HistoricoVehiculos.Observaciones.CurrentValue;
			HistoricoVehiculos.Observaciones.ViewCustomAttributes = "";

			// View refer script
			// TipoVehiculo

			HistoricoVehiculos.TipoVehiculo.LinkCustomAttributes = "";
			HistoricoVehiculos.TipoVehiculo.HrefValue = "";
			HistoricoVehiculos.TipoVehiculo.TooltipValue = "";

			// Placa
			HistoricoVehiculos.Placa.LinkCustomAttributes = "";
			HistoricoVehiculos.Placa.HrefValue = "";
			HistoricoVehiculos.Placa.TooltipValue = "";

			// FechaIngreso
			HistoricoVehiculos.FechaIngreso.LinkCustomAttributes = "";
			HistoricoVehiculos.FechaIngreso.HrefValue = "";
			HistoricoVehiculos.FechaIngreso.TooltipValue = "";

			// HoraIngreso
			HistoricoVehiculos.HoraIngreso.LinkCustomAttributes = "";
			HistoricoVehiculos.HoraIngreso.HrefValue = "";
			HistoricoVehiculos.HoraIngreso.TooltipValue = "";

			// FechaSalida
			HistoricoVehiculos.FechaSalida.LinkCustomAttributes = "";
			HistoricoVehiculos.FechaSalida.HrefValue = "";
			HistoricoVehiculos.FechaSalida.TooltipValue = "";

			// HoraSalida
			HistoricoVehiculos.HoraSalida.LinkCustomAttributes = "";
			HistoricoVehiculos.HoraSalida.HrefValue = "";
			HistoricoVehiculos.HoraSalida.TooltipValue = "";

			// Observaciones
			HistoricoVehiculos.Observaciones.LinkCustomAttributes = "";
			HistoricoVehiculos.Observaciones.HrefValue = "";
			HistoricoVehiculos.Observaciones.TooltipValue = "";
		}

		// Row Rendered event
		if (HistoricoVehiculos.RowType != EW_ROWTYPE_AGGREGATEINIT)
			HistoricoVehiculos.Row_Rendered();
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
		HistoricoVehiculos.TipoVehiculo.AdvancedSearch.SearchValue = HistoricoVehiculos.GetAdvancedSearch("x_TipoVehiculo");
		HistoricoVehiculos.Placa.AdvancedSearch.SearchValue = HistoricoVehiculos.GetAdvancedSearch("x_Placa");
		HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue = HistoricoVehiculos.GetAdvancedSearch("x_FechaIngreso");
		HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue2 = HistoricoVehiculos.GetAdvancedSearch("y_FechaIngreso");
		HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue = HistoricoVehiculos.GetAdvancedSearch("x_FechaSalida");
		HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue2 = HistoricoVehiculos.GetAdvancedSearch("y_FechaSalida");
		HistoricoVehiculos.Observaciones.AdvancedSearch.SearchValue = HistoricoVehiculos.GetAdvancedSearch("x_Observaciones");
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
		HistoricoVehiculos_list = new cHistoricoVehiculos_list(this);
		CurrentPage = HistoricoVehiculos_list;

		//CurrentPageType = HistoricoVehiculos_list.GetType();
		HistoricoVehiculos_list.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		HistoricoVehiculos_list.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (HistoricoVehiculos_list != null)
			HistoricoVehiculos_list.Dispose();
	}
}
