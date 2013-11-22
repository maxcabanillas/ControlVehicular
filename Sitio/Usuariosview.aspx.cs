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

partial class Usuariosview: AspNetMaker9_ControlVehicular {

	// Page object
	public cUsuarios_view Usuarios_view;	

	//
	// Page Class
	//
	public class cUsuarios_view: AspNetMakerPage, IDisposable {

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
				if (Usuarios.UseTokenInUrl)
					Url += "t=" + Usuarios.TableVar + "&"; // Add page token
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
			if (Usuarios.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (Usuarios.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (Usuarios.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public Usuariosview AspNetPage { 
			get { return (Usuariosview)m_ParentPage; }
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
		public cUsuarios_view(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "view";
			m_PageObjName = "Usuarios_view";
			m_PageObjTypeName = "cUsuarios_view";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "Usuarios";
			m_Table = Usuarios;
			CurrentTable = Usuarios;

			//CurrentTableType = Usuarios.GetType();
			// Initialize URLs

			string KeyUrl = "";
			if (ew_NotEmpty(ew_Get("IdUsuario"))) {
				RecKey["IdUsuario"] = ew_Get("IdUsuario");
				KeyUrl += "&IdUsuario=" + ew_UrlEncode(RecKey["IdUsuario"]);
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
				Page_Terminate("Usuarioslist.aspx");
			}

			// UserID_Loading event
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();

			// UserID_Loaded event
			Security.UserID_Loaded();
			if (Security.IsLoggedIn() && ew_Empty(Security.CurrentUserID)) {
				ew_Session[EW_SESSION_FAILURE_MESSAGE] = Language.Phrase("NoPermission");
				Page_Terminate("Usuarioslist.aspx");
			}

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
			if (ew_NotEmpty(ew_Get("IdUsuario"))) {
				Usuarios.IdUsuario.QueryStringValue = ew_Get("IdUsuario");
				RecKey["IdUsuario"] = Usuarios.IdUsuario.QueryStringValue;
			} else {
				sReturnUrl = "Usuarioslist.aspx"; // Return to list
			}

			// Get action
			Usuarios.CurrentAction = "I"; // Display form
			switch (Usuarios.CurrentAction) {
				case "I": // Get a record to display
					if (!LoadRow()) { // Load record based on key
						if (ew_Empty(SuccessMessage) && ew_Empty(FailureMessage))
							FailureMessage = Language.Phrase("NoRecord"); // Set no record message
						sReturnUrl = "Usuarioslist.aspx"; // No matching record, return to list
					}
					break;
			}
		} else {
			sReturnUrl = "Usuarioslist.aspx"; // Not page request, return to list
		}
		if (ew_NotEmpty(sReturnUrl)) Page_Terminate(sReturnUrl);

		// Render row
		Usuarios.RowType = EW_ROWTYPE_VIEW;
		Usuarios.ResetAttrs();
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
				Usuarios.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				Usuarios.StartRecordNumber = StartRec;
			}
		}
		StartRec = Usuarios.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			Usuarios.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			Usuarios.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			Usuarios.StartRecordNumber = StartRec;
		}
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = Usuarios.KeyFilter;

		// Row Selecting event
		Usuarios.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		Usuarios.CurrentFilter = sFilter;
		string sSql = Usuarios.SQL;

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
		Usuarios.Row_Selected(ref row);
		Usuarios.IdUsuario.DbValue = row["IdUsuario"];
		Usuarios.Usuario.DbValue = row["Usuario"];
		Usuarios.NombreUsuario.DbValue = row["NombreUsuario"];
		Usuarios.Password.DbValue = row["Password"];
		Usuarios.Correo.DbValue = row["Correo"];
		Usuarios.IdUsuarioNivel.DbValue = row["IdUsuarioNivel"];
		Usuarios.Activo.DbValue = (ew_ConvertToBool(row["Activo"])) ? "1" : "0";
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
		AddUrl = Usuarios.AddUrl;
		EditUrl = Usuarios.EditUrl;
		CopyUrl = Usuarios.CopyUrl;
		DeleteUrl = Usuarios.DeleteUrl;
		ListUrl = Usuarios.ListUrl;

		// Row Rendering event
		Usuarios.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdUsuario
		// Usuario
		// NombreUsuario
		// Password
		// Correo
		// IdUsuarioNivel
		// Activo
		//
		//  View  Row
		//

		if (Usuarios.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdUsuario
				Usuarios.IdUsuario.ViewValue = Usuarios.IdUsuario.CurrentValue;
			Usuarios.IdUsuario.ViewCustomAttributes = "";

			// Usuario
				Usuarios.Usuario.ViewValue = Usuarios.Usuario.CurrentValue;
			Usuarios.Usuario.ViewCustomAttributes = "";

			// NombreUsuario
				Usuarios.NombreUsuario.ViewValue = Usuarios.NombreUsuario.CurrentValue;
			Usuarios.NombreUsuario.ViewCustomAttributes = "";

			// Password
				Usuarios.Password.ViewValue = "********";
			Usuarios.Password.ViewCustomAttributes = "";

			// Correo
				Usuarios.Correo.ViewValue = Usuarios.Correo.CurrentValue;
			Usuarios.Correo.ViewCustomAttributes = "";

			// IdUsuarioNivel
			if ((Security.CurrentUserLevel & EW_ALLOW_ADMIN) == EW_ALLOW_ADMIN) { // System admin
			if (ew_NotEmpty(Usuarios.IdUsuarioNivel.CurrentValue)) {
				sFilterWrk = "[UserLevelID] = " + ew_AdjustSql(Usuarios.IdUsuarioNivel.CurrentValue) + "";
			sSqlWrk = "SELECT [UserLevelName] FROM [dbo].[UserLevels]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Usuarios.IdUsuarioNivel.ViewValue = drWrk["UserLevelName"];
				} else {
					Usuarios.IdUsuarioNivel.ViewValue = Usuarios.IdUsuarioNivel.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Usuarios.IdUsuarioNivel.ViewValue = System.DBNull.Value;
			}
			} else {
				Usuarios.IdUsuarioNivel.ViewValue = "********";
			}
			Usuarios.IdUsuarioNivel.ViewCustomAttributes = "";

			// Activo
			if (Convert.ToString(Usuarios.Activo.CurrentValue) == "1") {
				Usuarios.Activo.ViewValue = (Usuarios.Activo.FldTagCaption(1) != "") ? Usuarios.Activo.FldTagCaption(1) : "Y";
			} else {
				Usuarios.Activo.ViewValue = (Usuarios.Activo.FldTagCaption(2) != "") ? Usuarios.Activo.FldTagCaption(2) : "N";
			}
			Usuarios.Activo.ViewCustomAttributes = "";

			// View refer script
			// IdUsuario

			Usuarios.IdUsuario.LinkCustomAttributes = "";
			Usuarios.IdUsuario.HrefValue = "";
			Usuarios.IdUsuario.TooltipValue = "";

			// Usuario
			Usuarios.Usuario.LinkCustomAttributes = "";
			Usuarios.Usuario.HrefValue = "";
			Usuarios.Usuario.TooltipValue = "";

			// NombreUsuario
			Usuarios.NombreUsuario.LinkCustomAttributes = "";
			Usuarios.NombreUsuario.HrefValue = "";
			Usuarios.NombreUsuario.TooltipValue = "";

			// Password
			Usuarios.Password.LinkCustomAttributes = "";
			Usuarios.Password.HrefValue = "";
			Usuarios.Password.TooltipValue = "";

			// Correo
			Usuarios.Correo.LinkCustomAttributes = "";
			Usuarios.Correo.HrefValue = "";
			Usuarios.Correo.TooltipValue = "";

			// IdUsuarioNivel
			Usuarios.IdUsuarioNivel.LinkCustomAttributes = "";
			Usuarios.IdUsuarioNivel.HrefValue = "";
			Usuarios.IdUsuarioNivel.TooltipValue = "";

			// Activo
			Usuarios.Activo.LinkCustomAttributes = "";
			Usuarios.Activo.HrefValue = "";
			Usuarios.Activo.TooltipValue = "";
		}

		// Row Rendered event
		if (Usuarios.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Usuarios.Row_Rendered();
	}

	//
	// Show link optionally based on user ID
	//
	public bool ShowOptionLink() {		
		if (Security.IsLoggedIn() && !Security.IsAdmin()) {
			return Security.IsValidUserID(Usuarios.IdUsuario.CurrentValue);
		}
		return true;
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
		Usuarios_view = new cUsuarios_view(this);
		CurrentPage = Usuarios_view;

		//CurrentPageType = Usuarios_view.GetType();
		Usuarios_view.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Usuarios_view.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (Usuarios_view != null)
			Usuarios_view.Dispose();
	}
}
