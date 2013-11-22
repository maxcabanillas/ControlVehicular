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

partial class HistoricoVehiculossrch: AspNetMaker9_ControlVehicular {

	// Page object
	public cHistoricoVehiculos_search HistoricoVehiculos_search;	

	//
	// Page Class
	//
	public class cHistoricoVehiculos_search: AspNetMakerPage, IDisposable {

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
		public HistoricoVehiculossrch AspNetPage { 
			get { return (HistoricoVehiculossrch)m_ParentPage; }
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
		public cHistoricoVehiculos_search(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "search";
			m_PageObjName = "HistoricoVehiculos_search";
			m_PageObjTypeName = "cHistoricoVehiculos_search";

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
			// Connect to database

			if (Conn == null)
				Conn = new cConnection();
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
			if (!Security.CanSearch) {
				Security.SaveLastUrl();
				Page_Terminate("HistoricoVehiculoslist.aspx");
			}

			// UserID_Loading event
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();

			// UserID_Loaded event
			Security.UserID_Loaded();

			// Create form object
			ObjForm = new cFormObj();

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

	//
	// Page main processing
	//
	public void Page_Main() {
		if (IsPageRequest()) {

			// Get action
			HistoricoVehiculos.CurrentAction = ObjForm.GetValue("a_search");
			switch (HistoricoVehiculos.CurrentAction) {
				case "S": // Get Search Criteria

					// Build search string for advanced search, remove blank field
					string sSrchStr;
					LoadSearchValues(); // Get search values
					if (ValidateSearch()) {
						sSrchStr = BuildAdvancedSearch();
					} else {
						sSrchStr = "";
						FailureMessage = gsSearchError;
					}
					if (ew_NotEmpty(sSrchStr)) {
						sSrchStr = HistoricoVehiculos.UrlParm(sSrchStr);
						Page_Terminate("HistoricoVehiculoslist.aspx" + "?" + sSrchStr); // Go to list page
					}
					break;
			}
		}

		// Restore search settings from Session
		if (ew_Empty(gsSearchError))
			LoadAdvancedSearch();

		// Render row for search
		HistoricoVehiculos.RowType = EW_ROWTYPE_SEARCH;
		HistoricoVehiculos.ResetAttrs();
		RenderRow();
	}

	//
	// Build advanced search
	//
	public string BuildAdvancedSearch()	{
		string sSrchUrl = "";
		BuildSearchUrl(ref sSrchUrl, HistoricoVehiculos.TipoVehiculo); // TipoVehiculo
		BuildSearchUrl(ref sSrchUrl, HistoricoVehiculos.Placa); // Placa
		BuildSearchUrl(ref sSrchUrl, HistoricoVehiculos.FechaIngreso); // FechaIngreso
		BuildSearchUrl(ref sSrchUrl, HistoricoVehiculos.FechaSalida); // FechaSalida
		BuildSearchUrl(ref sSrchUrl, HistoricoVehiculos.Observaciones); // Observaciones
		return sSrchUrl;
	}

	//
	// Build search URL
	//
	public void BuildSearchUrl(ref string Url, cField Fld) {
		bool IsValidValue;
		string sWrk = "";
		string FldParm = Fld.FldVar.Substring(2);
		string FldVal = ObjForm.GetValue("x_" + FldParm);
		string FldOpr = ObjForm.GetValue("z_" + FldParm);
		string FldCond = ObjForm.GetValue("v_" + FldParm);
		string FldVal2 = ObjForm.GetValue("y_" + FldParm);
		string FldOpr2 = ObjForm.GetValue("w_" + FldParm);
		int lFldDataType = (Fld.FldIsVirtual) ? EW_DATATYPE_STRING : Fld.FldDataType;
		if (ew_SameText(FldOpr, "BETWEEN"))	{
			IsValidValue = (lFldDataType != EW_DATATYPE_NUMBER) || (lFldDataType == EW_DATATYPE_NUMBER && Information.IsNumeric(FldVal) && Information.IsNumeric(FldVal2));
			if (ew_NotEmpty(FldVal) && ew_NotEmpty(FldVal2) && IsValidValue)	{
				sWrk = "x_" + FldParm + "=" + ew_UrlEncode(FldVal) + "&y_" + FldParm + "=" + ew_UrlEncode(FldVal2) + "&z_" + FldParm + "=" + ew_UrlEncode(FldOpr);
			}
		} else if (ew_SameText(FldOpr, "IS NULL") || ew_SameText(FldOpr, "IS NOT NULL")) {
			sWrk = "x_" + FldParm + "=" + ew_UrlEncode(FldVal) + "&z_" + FldParm + "=" + ew_UrlEncode(FldOpr);
		}	else {
			IsValidValue = (lFldDataType != EW_DATATYPE_NUMBER) || (lFldDataType == EW_DATATYPE_NUMBER && Information.IsNumeric(FldVal));
			if (ew_NotEmpty(FldVal) && IsValidValue && ew_IsValidOpr(FldOpr, lFldDataType)) {
				sWrk = "x_" + FldParm + "=" + ew_UrlEncode(FldVal) + "&z_" + FldParm + "=" + ew_UrlEncode(FldOpr);
			}
			IsValidValue = (lFldDataType != EW_DATATYPE_NUMBER) || (lFldDataType == EW_DATATYPE_NUMBER && Information.IsNumeric(FldVal2));
			if (ew_NotEmpty(FldVal2) && IsValidValue && ew_IsValidOpr(FldOpr2, lFldDataType)) {
				if (ew_NotEmpty(sWrk)) sWrk += "&v_" + FldParm + "=" + FldCond + "&"; 
				sWrk += "y_" + FldParm + "=" + ew_UrlEncode(FldVal2) + "&w_" + FldParm + "=" + ew_UrlEncode(FldOpr2);
			}
		}
		if (ew_NotEmpty(sWrk)) {
			if (ew_NotEmpty(Url)) Url += "&"; 
			Url += sWrk;
		}
	}

	//
	//  Load search values for validation
	//
	public void LoadSearchValues() {
		HistoricoVehiculos.TipoVehiculo.AdvancedSearch.SearchValue = ObjForm.GetValue("x_TipoVehiculo");
    HistoricoVehiculos.TipoVehiculo.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_TipoVehiculo");
		HistoricoVehiculos.Placa.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Placa");
    HistoricoVehiculos.Placa.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Placa");
		HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue = ObjForm.GetValue("x_FechaIngreso");
    HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_FechaIngreso");
		HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchCondition = ObjForm.GetValue("v_FechaIngreso");
   	HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue2 = ObjForm.GetValue("y_FechaIngreso");
   	HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchOperator2 = ObjForm.GetValue("w_FechaIngreso");
		HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue = ObjForm.GetValue("x_FechaSalida");
    HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_FechaSalida");
		HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchCondition = ObjForm.GetValue("v_FechaSalida");
   	HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue2 = ObjForm.GetValue("y_FechaSalida");
   	HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchOperator2 = ObjForm.GetValue("w_FechaSalida");
		HistoricoVehiculos.Observaciones.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Observaciones");
    HistoricoVehiculos.Observaciones.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Observaciones");
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
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

			// FechaSalida
			HistoricoVehiculos.FechaSalida.LinkCustomAttributes = "";
			HistoricoVehiculos.FechaSalida.HrefValue = "";
			HistoricoVehiculos.FechaSalida.TooltipValue = "";

			// Observaciones
			HistoricoVehiculos.Observaciones.LinkCustomAttributes = "";
			HistoricoVehiculos.Observaciones.HrefValue = "";
			HistoricoVehiculos.Observaciones.TooltipValue = "";

		//
		//  Search Row
		//

		} else if (HistoricoVehiculos.RowType == EW_ROWTYPE_SEARCH) { // Search row

			// TipoVehiculo
			HistoricoVehiculos.TipoVehiculo.EditCustomAttributes = "";
			sFilterWrk = "";
			sSqlWrk = "SELECT [TipoVehiculo], [TipoVehiculo] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[TiposVehiculos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoVehiculo]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			HistoricoVehiculos.TipoVehiculo.EditValue = alwrk;

			// Placa
			HistoricoVehiculos.Placa.EditCustomAttributes = "";
			HistoricoVehiculos.Placa.EditValue = ew_HtmlEncode(HistoricoVehiculos.Placa.AdvancedSearch.SearchValue);

			// FechaIngreso
			HistoricoVehiculos.FechaIngreso.EditCustomAttributes = "";
			HistoricoVehiculos.FechaIngreso.EditValue = HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue;
			HistoricoVehiculos.FechaIngreso.EditCustomAttributes = "";
			HistoricoVehiculos.FechaIngreso.EditValue2 = HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue2;

			// FechaSalida
			HistoricoVehiculos.FechaSalida.EditCustomAttributes = "";
			HistoricoVehiculos.FechaSalida.EditValue = HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue;
			HistoricoVehiculos.FechaSalida.EditCustomAttributes = "";
			HistoricoVehiculos.FechaSalida.EditValue2 = HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue2;

			// Observaciones
			HistoricoVehiculos.Observaciones.EditCustomAttributes = "";
			HistoricoVehiculos.Observaciones.EditValue = ew_HtmlEncode(HistoricoVehiculos.Observaciones.AdvancedSearch.SearchValue);
		}
		if (HistoricoVehiculos.RowType == EW_ROWTYPE_ADD ||
			HistoricoVehiculos.RowType == EW_ROWTYPE_EDIT ||
			HistoricoVehiculos.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			HistoricoVehiculos.SetupFieldTitles();
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
		if (!ew_CheckEuroDate(Convert.ToString(HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue)))
			ew_AddMessage(ref gsSearchError, HistoricoVehiculos.FechaIngreso.FldErrMsg);
		if (!ew_CheckEuroDate(Convert.ToString(HistoricoVehiculos.FechaIngreso.AdvancedSearch.SearchValue2)))
			ew_AddMessage(ref gsSearchError, HistoricoVehiculos.FechaIngreso.FldErrMsg);
		if (!ew_CheckEuroDate(Convert.ToString(HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue)))
			ew_AddMessage(ref gsSearchError, HistoricoVehiculos.FechaSalida.FldErrMsg);
		if (!ew_CheckEuroDate(Convert.ToString(HistoricoVehiculos.FechaSalida.AdvancedSearch.SearchValue2)))
			ew_AddMessage(ref gsSearchError, HistoricoVehiculos.FechaSalida.FldErrMsg);

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
	}

	//
	// ASP.NET Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {

		// Page init
		HistoricoVehiculos_search = new cHistoricoVehiculos_search(this);
		CurrentPage = HistoricoVehiculos_search;

		//CurrentPageType = HistoricoVehiculos_search.GetType();
		HistoricoVehiculos_search.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		HistoricoVehiculos_search.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (HistoricoVehiculos_search != null)
			HistoricoVehiculos_search.Dispose();
	}
}
