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

partial class Personassrch: AspNetMaker9_ControlVehicular {

	// Page object
	public cPersonas_search Personas_search;	

	//
	// Page Class
	//
	public class cPersonas_search: AspNetMakerPage, IDisposable {

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
		public Personassrch AspNetPage { 
			get { return (Personassrch)m_ParentPage; }
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

		//
		//  Page class constructor
		//
		public cPersonas_search(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "search";
			m_PageObjName = "Personas_search";
			m_PageObjTypeName = "cPersonas_search";

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

			// Table
			m_TableName = "Personas";
			m_Table = Personas;
			CurrentTable = Personas;

			//CurrentTableType = Personas.GetType();
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
				Page_Terminate("Personaslist.aspx");
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
			Personas.Dispose();
			Areas.Dispose();
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
			Personas.CurrentAction = ObjForm.GetValue("a_search");
			switch (Personas.CurrentAction) {
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
						sSrchStr = Personas.UrlParm(sSrchStr);
						Page_Terminate("Personaslist.aspx" + "?" + sSrchStr); // Go to list page
					}
					break;
			}
		}

		// Restore search settings from Session
		if (ew_Empty(gsSearchError))
			LoadAdvancedSearch();

		// Render row for search
		Personas.RowType = EW_ROWTYPE_SEARCH;
		Personas.ResetAttrs();
		RenderRow();
	}

	//
	// Build advanced search
	//
	public string BuildAdvancedSearch()	{
		string sSrchUrl = "";
		BuildSearchUrl(ref sSrchUrl, Personas.IdPersona); // IdPersona
		BuildSearchUrl(ref sSrchUrl, Personas.IdArea); // IdArea
		BuildSearchUrl(ref sSrchUrl, Personas.IdCargo); // IdCargo
		BuildSearchUrl(ref sSrchUrl, Personas.Documento); // Documento
		BuildSearchUrl(ref sSrchUrl, Personas.Persona); // Persona
		BuildSearchUrl(ref sSrchUrl, Personas.Activa); // Activa
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
		Personas.IdPersona.AdvancedSearch.SearchValue = ObjForm.GetValue("x_IdPersona");
    Personas.IdPersona.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_IdPersona");
		Personas.IdArea.AdvancedSearch.SearchValue = ObjForm.GetValue("x_IdArea");
    Personas.IdArea.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_IdArea");
		Personas.IdCargo.AdvancedSearch.SearchValue = ObjForm.GetValue("x_IdCargo");
    Personas.IdCargo.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_IdCargo");
		Personas.Documento.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Documento");
    Personas.Documento.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Documento");
		Personas.Persona.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Persona");
    Personas.Persona.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Persona");
		Personas.Activa.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Activa");
    Personas.Activa.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Activa");
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
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
			// IdPersona

			Personas.IdPersona.LinkCustomAttributes = "";
			Personas.IdPersona.HrefValue = "";
			Personas.IdPersona.TooltipValue = "";

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

			// IdPersona
			Personas.IdPersona.EditCustomAttributes = "";
			Personas.IdPersona.EditValue = ew_HtmlEncode(Personas.IdPersona.AdvancedSearch.SearchValue);

			// IdArea
			Personas.IdArea.EditCustomAttributes = "";
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
		if (!ew_CheckInteger(Convert.ToString(Personas.IdPersona.AdvancedSearch.SearchValue)))
			ew_AddMessage(ref gsSearchError, Personas.IdPersona.FldErrMsg);

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
		Personas_search = new cPersonas_search(this);
		CurrentPage = Personas_search;

		//CurrentPageType = Personas_search.GetType();
		Personas_search.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Personas_search.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (Personas_search != null)
			Personas_search.Dispose();
	}
}
