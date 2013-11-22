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

partial class HistoricoPeatonessrch: AspNetMaker9_ControlVehicular {

	// Page object
	public cHistoricoPeatones_search HistoricoPeatones_search;	

	//
	// Page Class
	//
	public class cHistoricoPeatones_search: AspNetMakerPage, IDisposable {

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
		public HistoricoPeatonessrch AspNetPage { 
			get { return (HistoricoPeatonessrch)m_ParentPage; }
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
		public cHistoricoPeatones_search(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "search";
			m_PageObjName = "HistoricoPeatones_search";
			m_PageObjTypeName = "cHistoricoPeatones_search";

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
				Page_Terminate("HistoricoPeatoneslist.aspx");
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
			HistoricoPeatones.Dispose();
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
			HistoricoPeatones.CurrentAction = ObjForm.GetValue("a_search");
			switch (HistoricoPeatones.CurrentAction) {
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
						sSrchStr = HistoricoPeatones.UrlParm(sSrchStr);
						Page_Terminate("HistoricoPeatoneslist.aspx" + "?" + sSrchStr); // Go to list page
					}
					break;
			}
		}

		// Restore search settings from Session
		if (ew_Empty(gsSearchError))
			LoadAdvancedSearch();

		// Render row for search
		HistoricoPeatones.RowType = EW_ROWTYPE_SEARCH;
		HistoricoPeatones.ResetAttrs();
		RenderRow();
	}

	//
	// Build advanced search
	//
	public string BuildAdvancedSearch()	{
		string sSrchUrl = "";
		BuildSearchUrl(ref sSrchUrl, HistoricoPeatones.TipoDocumento); // TipoDocumento
		BuildSearchUrl(ref sSrchUrl, HistoricoPeatones.Documento); // Documento
		BuildSearchUrl(ref sSrchUrl, HistoricoPeatones.Nombre); // Nombre
		BuildSearchUrl(ref sSrchUrl, HistoricoPeatones.Apellidos); // Apellidos
		BuildSearchUrl(ref sSrchUrl, HistoricoPeatones.Area); // Area
		BuildSearchUrl(ref sSrchUrl, HistoricoPeatones.Persona); // Persona
		BuildSearchUrl(ref sSrchUrl, HistoricoPeatones.FechaIngreso); // FechaIngreso
		BuildSearchUrl(ref sSrchUrl, HistoricoPeatones.FechaSalida); // FechaSalida
		BuildSearchUrl(ref sSrchUrl, HistoricoPeatones.Observacion); // Observacion
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
		HistoricoPeatones.TipoDocumento.AdvancedSearch.SearchValue = ObjForm.GetValue("x_TipoDocumento");
    HistoricoPeatones.TipoDocumento.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_TipoDocumento");
		HistoricoPeatones.Documento.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Documento");
    HistoricoPeatones.Documento.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Documento");
		HistoricoPeatones.Nombre.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Nombre");
    HistoricoPeatones.Nombre.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Nombre");
		HistoricoPeatones.Apellidos.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Apellidos");
    HistoricoPeatones.Apellidos.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Apellidos");
		HistoricoPeatones.Area.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Area");
    HistoricoPeatones.Area.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Area");
		HistoricoPeatones.Persona.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Persona");
    HistoricoPeatones.Persona.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Persona");
		HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue = ObjForm.GetValue("x_FechaIngreso");
    HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_FechaIngreso");
		HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchCondition = ObjForm.GetValue("v_FechaIngreso");
   	HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue2 = ObjForm.GetValue("y_FechaIngreso");
   	HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchOperator2 = ObjForm.GetValue("w_FechaIngreso");
		HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue = ObjForm.GetValue("x_FechaSalida");
    HistoricoPeatones.FechaSalida.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_FechaSalida");
		HistoricoPeatones.FechaSalida.AdvancedSearch.SearchCondition = ObjForm.GetValue("v_FechaSalida");
   	HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue2 = ObjForm.GetValue("y_FechaSalida");
   	HistoricoPeatones.FechaSalida.AdvancedSearch.SearchOperator2 = ObjForm.GetValue("w_FechaSalida");
		HistoricoPeatones.Observacion.AdvancedSearch.SearchValue = ObjForm.GetValue("x_Observacion");
    HistoricoPeatones.Observacion.AdvancedSearch.SearchOperator = ObjForm.GetValue("z_Observacion");
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
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

			// Observacion
			HistoricoPeatones.Observacion.ViewValue = HistoricoPeatones.Observacion.CurrentValue;
			HistoricoPeatones.Observacion.ViewCustomAttributes = "";

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

			// Observacion
			HistoricoPeatones.Observacion.LinkCustomAttributes = "";
			HistoricoPeatones.Observacion.HrefValue = "";
			HistoricoPeatones.Observacion.TooltipValue = "";

		//
		//  Search Row
		//

		} else if (HistoricoPeatones.RowType == EW_ROWTYPE_SEARCH) { // Search row

			// TipoDocumento
			HistoricoPeatones.TipoDocumento.EditCustomAttributes = "";

			// Documento
			HistoricoPeatones.Documento.EditCustomAttributes = "";
			HistoricoPeatones.Documento.EditValue = ew_HtmlEncode(HistoricoPeatones.Documento.AdvancedSearch.SearchValue);

			// Nombre
			HistoricoPeatones.Nombre.EditCustomAttributes = "";
			HistoricoPeatones.Nombre.EditValue = ew_HtmlEncode(HistoricoPeatones.Nombre.AdvancedSearch.SearchValue);

			// Apellidos
			HistoricoPeatones.Apellidos.EditCustomAttributes = "";
			HistoricoPeatones.Apellidos.EditValue = ew_HtmlEncode(HistoricoPeatones.Apellidos.AdvancedSearch.SearchValue);

			// Area
			HistoricoPeatones.Area.EditCustomAttributes = "";
			sFilterWrk = "";
			sSqlWrk = "SELECT [Area], [Area] AS [DispFld], [Codigo] AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[Areas]";
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
			HistoricoPeatones.Area.EditValue = alwrk;

			// Persona
			HistoricoPeatones.Persona.EditCustomAttributes = "";
			sFilterWrk = "";
			sSqlWrk = "SELECT [Persona], [Persona] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[Personas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Persona]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			HistoricoPeatones.Persona.EditValue = alwrk;

			// FechaIngreso
			HistoricoPeatones.FechaIngreso.EditCustomAttributes = "";
			HistoricoPeatones.FechaIngreso.EditValue = HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue;
			HistoricoPeatones.FechaIngreso.EditCustomAttributes = "";
			HistoricoPeatones.FechaIngreso.EditValue2 = HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue2;

			// FechaSalida
			HistoricoPeatones.FechaSalida.EditCustomAttributes = "";
			HistoricoPeatones.FechaSalida.EditValue = HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue;
			HistoricoPeatones.FechaSalida.EditCustomAttributes = "";
			HistoricoPeatones.FechaSalida.EditValue2 = HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue2;

			// Observacion
			HistoricoPeatones.Observacion.EditCustomAttributes = "";
			HistoricoPeatones.Observacion.EditValue = ew_HtmlEncode(HistoricoPeatones.Observacion.AdvancedSearch.SearchValue);
		}
		if (HistoricoPeatones.RowType == EW_ROWTYPE_ADD ||
			HistoricoPeatones.RowType == EW_ROWTYPE_EDIT ||
			HistoricoPeatones.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			HistoricoPeatones.SetupFieldTitles();
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
		if (!ew_CheckEuroDate(Convert.ToString(HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue)))
			ew_AddMessage(ref gsSearchError, HistoricoPeatones.FechaIngreso.FldErrMsg);
		if (!ew_CheckEuroDate(Convert.ToString(HistoricoPeatones.FechaIngreso.AdvancedSearch.SearchValue2)))
			ew_AddMessage(ref gsSearchError, HistoricoPeatones.FechaIngreso.FldErrMsg);
		if (!ew_CheckEuroDate(Convert.ToString(HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue)))
			ew_AddMessage(ref gsSearchError, HistoricoPeatones.FechaSalida.FldErrMsg);
		if (!ew_CheckEuroDate(Convert.ToString(HistoricoPeatones.FechaSalida.AdvancedSearch.SearchValue2)))
			ew_AddMessage(ref gsSearchError, HistoricoPeatones.FechaSalida.FldErrMsg);

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
		HistoricoPeatones_search = new cHistoricoPeatones_search(this);
		CurrentPage = HistoricoPeatones_search;

		//CurrentPageType = HistoricoPeatones_search.GetType();
		HistoricoPeatones_search.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		HistoricoPeatones_search.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (HistoricoPeatones_search != null)
			HistoricoPeatones_search.Dispose();
	}
}
