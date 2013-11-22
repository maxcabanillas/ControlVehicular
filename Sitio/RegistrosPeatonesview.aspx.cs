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

partial class RegistrosPeatonesview: AspNetMaker9_ControlVehicular {

	// Page object
	public cRegistrosPeatones_view RegistrosPeatones_view;	

	//
	// Page Class
	//
	public class cRegistrosPeatones_view: AspNetMakerPage, IDisposable {

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
		public RegistrosPeatonesview AspNetPage { 
			get { return (RegistrosPeatonesview)m_ParentPage; }
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
		public cRegistrosPeatones_view(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "view";
			m_PageObjName = "RegistrosPeatones_view";
			m_PageObjTypeName = "cRegistrosPeatones_view";

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

			string KeyUrl = "";
			if (ew_NotEmpty(ew_Get("IdRegistroPeaton"))) {
				RecKey["IdRegistroPeaton"] = ew_Get("IdRegistroPeaton");
				KeyUrl += "&IdRegistroPeaton=" + ew_UrlEncode(RecKey["IdRegistroPeaton"]);
			}
			ExportPrintUrl = PageUrl + "export=print" + KeyUrl;
			ExportHtmlUrl = PageUrl + "export=html" + KeyUrl;
			ExportExcelUrl = PageUrl + "export=excel" + KeyUrl;
			ExportWordUrl = PageUrl + "export=word" + KeyUrl;
			ExportXmlUrl = PageUrl + "export=xml" + KeyUrl;
			ExportCsvUrl = PageUrl + "export=csv" + KeyUrl;
			ExportPdfUrl = PageUrl + "export=pdf" + KeyUrl;

			// Connect to database
			if (Conn == null)
				Conn = new cConnection();

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
			if (!Security.CanView) {
				Security.SaveLastUrl();
				Page_Terminate("RegistrosPeatoneslist.aspx");
			}

			// UserID_Loading event
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();

			// UserID_Loaded event
			Security.UserID_Loaded();

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

	public int RecCnt;

	public Hashtable RecKey = new Hashtable();

	public string SearchWhere = ""; 

	public string DbMasterFilter;

	public string DbDetailFilter;

	public bool MasterRecordExists;

	public cListOptions ExportOptions; // Export options

	//
	// Page main processing
	//
	public void Page_Main() {
		string sReturnUrl = "";
		bool bMatchRecord = false;
		if (IsPageRequest()) {
			if (ew_NotEmpty(ew_Get("IdRegistroPeaton"))) {
				RegistrosPeatones.IdRegistroPeaton.QueryStringValue = ew_Get("IdRegistroPeaton");
				RecKey["IdRegistroPeaton"] = RegistrosPeatones.IdRegistroPeaton.QueryStringValue;
			} else {
				sReturnUrl = "RegistrosPeatoneslist.aspx"; // Return to list
			}

			// Get action
			RegistrosPeatones.CurrentAction = "I"; // Display form
			switch (RegistrosPeatones.CurrentAction) {
				case "I": // Get a record to display
					if (!LoadRow()) { // Load record based on key
						if (ew_Empty(SuccessMessage) && ew_Empty(FailureMessage))
							FailureMessage = Language.Phrase("NoRecord"); // Set no record message
						sReturnUrl = "RegistrosPeatoneslist.aspx"; // No matching record, return to list
					}
					break;
			}
		} else {
			sReturnUrl = "RegistrosPeatoneslist.aspx"; // Not page request, return to list
		}
		if (ew_NotEmpty(sReturnUrl)) Page_Terminate(sReturnUrl);

		// Render row
		RegistrosPeatones.RowType = EW_ROWTYPE_VIEW;
		RegistrosPeatones.ResetAttrs();
		RenderRow();
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

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
		AddUrl = RegistrosPeatones.AddUrl;
		EditUrl = RegistrosPeatones.EditUrl;
		CopyUrl = RegistrosPeatones.CopyUrl;
		DeleteUrl = RegistrosPeatones.DeleteUrl;
		ListUrl = RegistrosPeatones.ListUrl;

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

			// Observacion
			RegistrosPeatones.Observacion.ViewValue = RegistrosPeatones.Observacion.CurrentValue;
			RegistrosPeatones.Observacion.ViewCustomAttributes = "";

			// View refer script
			// IdRegistroPeaton

			RegistrosPeatones.IdRegistroPeaton.LinkCustomAttributes = "";
			RegistrosPeatones.IdRegistroPeaton.HrefValue = "";
			RegistrosPeatones.IdRegistroPeaton.TooltipValue = "";

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

			// Observacion
			RegistrosPeatones.Observacion.LinkCustomAttributes = "";
			RegistrosPeatones.Observacion.HrefValue = "";
			RegistrosPeatones.Observacion.TooltipValue = "";
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
	}

	//
	// ASP.NET Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {

		// Page init
		RegistrosPeatones_view = new cRegistrosPeatones_view(this);
		CurrentPage = RegistrosPeatones_view;

		//CurrentPageType = RegistrosPeatones_view.GetType();
		RegistrosPeatones_view.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		RegistrosPeatones_view.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (RegistrosPeatones_view != null)
			RegistrosPeatones_view.Dispose();
	}
}
