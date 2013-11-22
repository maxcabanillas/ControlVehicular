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

partial class RegistrosVehiculosview: AspNetMaker9_ControlVehicular {

	// Page object
	public cRegistrosVehiculos_view RegistrosVehiculos_view;	

	//
	// Page Class
	//
	public class cRegistrosVehiculos_view: AspNetMakerPage, IDisposable {

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
				if (RegistrosVehiculos.UseTokenInUrl)
					Url += "t=" + RegistrosVehiculos.TableVar + "&"; // Add page token
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
			if (RegistrosVehiculos.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (RegistrosVehiculos.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (RegistrosVehiculos.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public RegistrosVehiculosview AspNetPage { 
			get { return (RegistrosVehiculosview)m_ParentPage; }
		}

		// RegistrosVehiculos	
		public cRegistrosVehiculos RegistrosVehiculos { 
			get {				
				return ParentPage.RegistrosVehiculos;
			}
			set {
				ParentPage.RegistrosVehiculos = value;	
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
		public cRegistrosVehiculos_view(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "view";
			m_PageObjName = "RegistrosVehiculos_view";
			m_PageObjTypeName = "cRegistrosVehiculos_view";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (RegistrosVehiculos == null)
				RegistrosVehiculos = new cRegistrosVehiculos(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "RegistrosVehiculos";
			m_Table = RegistrosVehiculos;
			CurrentTable = RegistrosVehiculos;

			//CurrentTableType = RegistrosVehiculos.GetType();
			// Initialize URLs

			string KeyUrl = "";
			if (ew_NotEmpty(ew_Get("IdRegistroVehiculo"))) {
				RecKey["IdRegistroVehiculo"] = ew_Get("IdRegistroVehiculo");
				KeyUrl += "&IdRegistroVehiculo=" + ew_UrlEncode(RecKey["IdRegistroVehiculo"]);
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
				Page_Terminate("RegistrosVehiculoslist.aspx");
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
			RegistrosVehiculos.Dispose();
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
			if (ew_NotEmpty(ew_Get("IdRegistroVehiculo"))) {
				RegistrosVehiculos.IdRegistroVehiculo.QueryStringValue = ew_Get("IdRegistroVehiculo");
				RecKey["IdRegistroVehiculo"] = RegistrosVehiculos.IdRegistroVehiculo.QueryStringValue;
			} else {
				sReturnUrl = "RegistrosVehiculoslist.aspx"; // Return to list
			}

			// Get action
			RegistrosVehiculos.CurrentAction = "I"; // Display form
			switch (RegistrosVehiculos.CurrentAction) {
				case "I": // Get a record to display
					if (!LoadRow()) { // Load record based on key
						if (ew_Empty(SuccessMessage) && ew_Empty(FailureMessage))
							FailureMessage = Language.Phrase("NoRecord"); // Set no record message
						sReturnUrl = "RegistrosVehiculoslist.aspx"; // No matching record, return to list
					}
					break;
			}
		} else {
			sReturnUrl = "RegistrosVehiculoslist.aspx"; // Not page request, return to list
		}
		if (ew_NotEmpty(sReturnUrl)) Page_Terminate(sReturnUrl);

		// Render row
		RegistrosVehiculos.RowType = EW_ROWTYPE_VIEW;
		RegistrosVehiculos.ResetAttrs();
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
				RegistrosVehiculos.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				RegistrosVehiculos.StartRecordNumber = StartRec;
			}
		}
		StartRec = RegistrosVehiculos.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			RegistrosVehiculos.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			RegistrosVehiculos.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			RegistrosVehiculos.StartRecordNumber = StartRec;
		}
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = RegistrosVehiculos.KeyFilter;

		// Row Selecting event
		RegistrosVehiculos.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		RegistrosVehiculos.CurrentFilter = sFilter;
		string sSql = RegistrosVehiculos.SQL;

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
		RegistrosVehiculos.Row_Selected(ref row);
		RegistrosVehiculos.IdRegistroVehiculo.DbValue = row["IdRegistroVehiculo"];
		RegistrosVehiculos.IdTipoVehiculo.DbValue = row["IdTipoVehiculo"];
		RegistrosVehiculos.Placa.DbValue = row["Placa"];
		RegistrosVehiculos.FechaIngreso.DbValue = row["FechaIngreso"];
		RegistrosVehiculos.FechaSalida.DbValue = row["FechaSalida"];
		RegistrosVehiculos.Observaciones.DbValue = row["Observaciones"];
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
		AddUrl = RegistrosVehiculos.AddUrl;
		EditUrl = RegistrosVehiculos.EditUrl;
		CopyUrl = RegistrosVehiculos.CopyUrl;
		DeleteUrl = RegistrosVehiculos.DeleteUrl;
		ListUrl = RegistrosVehiculos.ListUrl;

		// Row Rendering event
		RegistrosVehiculos.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdRegistroVehiculo
		// IdTipoVehiculo
		// Placa
		// FechaIngreso
		// FechaSalida
		// Observaciones
		//
		//  View  Row
		//

		if (RegistrosVehiculos.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdRegistroVehiculo
				RegistrosVehiculos.IdRegistroVehiculo.ViewValue = RegistrosVehiculos.IdRegistroVehiculo.CurrentValue;
			RegistrosVehiculos.IdRegistroVehiculo.ViewCustomAttributes = "";

			// IdTipoVehiculo
			if (ew_NotEmpty(RegistrosVehiculos.IdTipoVehiculo.CurrentValue)) {
				sFilterWrk = "[IdTipoVehiculo] = " + ew_AdjustSql(RegistrosVehiculos.IdTipoVehiculo.CurrentValue) + "";
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
					RegistrosVehiculos.IdTipoVehiculo.ViewValue = drWrk["TipoVehiculo"];
				} else {
					RegistrosVehiculos.IdTipoVehiculo.ViewValue = RegistrosVehiculos.IdTipoVehiculo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosVehiculos.IdTipoVehiculo.ViewValue = System.DBNull.Value;
			}
			RegistrosVehiculos.IdTipoVehiculo.ViewCustomAttributes = "";

			// Placa
				RegistrosVehiculos.Placa.ViewValue = RegistrosVehiculos.Placa.CurrentValue;
			RegistrosVehiculos.Placa.ViewCustomAttributes = "";

			// FechaIngreso
				RegistrosVehiculos.FechaIngreso.ViewValue = RegistrosVehiculos.FechaIngreso.CurrentValue;
				RegistrosVehiculos.FechaIngreso.ViewValue = ew_FormatDateTime(RegistrosVehiculos.FechaIngreso.ViewValue, 7);
			RegistrosVehiculos.FechaIngreso.ViewCustomAttributes = "";

			// FechaSalida
				RegistrosVehiculos.FechaSalida.ViewValue = RegistrosVehiculos.FechaSalida.CurrentValue;
				RegistrosVehiculos.FechaSalida.ViewValue = ew_FormatDateTime(RegistrosVehiculos.FechaSalida.ViewValue, 7);
			RegistrosVehiculos.FechaSalida.ViewCustomAttributes = "";

			// Observaciones
			RegistrosVehiculos.Observaciones.ViewValue = RegistrosVehiculos.Observaciones.CurrentValue;
			RegistrosVehiculos.Observaciones.ViewCustomAttributes = "";

			// View refer script
			// IdRegistroVehiculo

			RegistrosVehiculos.IdRegistroVehiculo.LinkCustomAttributes = "";
			RegistrosVehiculos.IdRegistroVehiculo.HrefValue = "";
			RegistrosVehiculos.IdRegistroVehiculo.TooltipValue = "";

			// IdTipoVehiculo
			RegistrosVehiculos.IdTipoVehiculo.LinkCustomAttributes = "";
			RegistrosVehiculos.IdTipoVehiculo.HrefValue = "";
			RegistrosVehiculos.IdTipoVehiculo.TooltipValue = "";

			// Placa
			RegistrosVehiculos.Placa.LinkCustomAttributes = "";
			RegistrosVehiculos.Placa.HrefValue = "";
			RegistrosVehiculos.Placa.TooltipValue = "";

			// FechaIngreso
			RegistrosVehiculos.FechaIngreso.LinkCustomAttributes = "";
			RegistrosVehiculos.FechaIngreso.HrefValue = "";
			RegistrosVehiculos.FechaIngreso.TooltipValue = "";

			// FechaSalida
			RegistrosVehiculos.FechaSalida.LinkCustomAttributes = "";
			RegistrosVehiculos.FechaSalida.HrefValue = "";
			RegistrosVehiculos.FechaSalida.TooltipValue = "";

			// Observaciones
			RegistrosVehiculos.Observaciones.LinkCustomAttributes = "";
			RegistrosVehiculos.Observaciones.HrefValue = "";
			RegistrosVehiculos.Observaciones.TooltipValue = "";
		}

		// Row Rendered event
		if (RegistrosVehiculos.RowType != EW_ROWTYPE_AGGREGATEINIT)
			RegistrosVehiculos.Row_Rendered();
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
		RegistrosVehiculos_view = new cRegistrosVehiculos_view(this);
		CurrentPage = RegistrosVehiculos_view;

		//CurrentPageType = RegistrosVehiculos_view.GetType();
		RegistrosVehiculos_view.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		RegistrosVehiculos_view.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (RegistrosVehiculos_view != null)
			RegistrosVehiculos_view.Dispose();
	}
}
