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

partial class TiposDocumentosadd: AspNetMaker9_ControlVehicular {

	// Page object
	public cTiposDocumentos_add TiposDocumentos_add;	

	//
	// Page Class
	//
	public class cTiposDocumentos_add: AspNetMakerPage, IDisposable {

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
				if (TiposDocumentos.UseTokenInUrl)
					Url += "t=" + TiposDocumentos.TableVar + "&"; // Add page token
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
			if (TiposDocumentos.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (TiposDocumentos.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (TiposDocumentos.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public TiposDocumentosadd AspNetPage { 
			get { return (TiposDocumentosadd)m_ParentPage; }
		}

		// TiposDocumentos	
		public cTiposDocumentos TiposDocumentos { 
			get {				
				return ParentPage.TiposDocumentos;
			}
			set {
				ParentPage.TiposDocumentos = value;	
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
		public cTiposDocumentos_add(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "add";
			m_PageObjName = "TiposDocumentos_add";
			m_PageObjTypeName = "cTiposDocumentos_add";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (TiposDocumentos == null)
				TiposDocumentos = new cTiposDocumentos(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "TiposDocumentos";
			m_Table = TiposDocumentos;
			CurrentTable = TiposDocumentos;

			//CurrentTableType = TiposDocumentos.GetType();
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
			if (!Security.CanAdd) {
				Security.SaveLastUrl();
				Page_Terminate("TiposDocumentoslist.aspx");
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
			TiposDocumentos.Dispose();
			Usuarios.Dispose();
			if (ew_NotEmpty(sRedirectUrl)) {
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.Redirect(sRedirectUrl); 
			}
		}

	public string DbMasterFilter;

	public string DbDetailFilter;

	public int Priv;

	public SqlDataReader OldRecordset = null;

	public bool CopyRecord;

	//
	// Page main processing
	//
	public void Page_Main()
	{

		// Process form if post back
		if (ew_NotEmpty(ObjForm.GetValue("a_add"))) {
			TiposDocumentos.CurrentAction = ObjForm.GetValue("a_add"); // Get form action
			CopyRecord = LoadOldRecord(); // Load old recordset
			LoadFormValues(); // Load form values

			// Validate Form
			if (!ValidateForm()) {
				TiposDocumentos.CurrentAction = "I"; // Form error, reset action
				TiposDocumentos.EventCancelled = true; // Event cancelled
				RestoreFormValues(); // Restore form values
				FailureMessage = gsFormError;
			}

		// Not post back
		} else {

			// Load key values from QueryString
			CopyRecord = true;
			if (ew_NotEmpty(ew_Get("IdTipoDocumento"))) {
				TiposDocumentos.IdTipoDocumento.QueryStringValue = ew_Get("IdTipoDocumento");
				TiposDocumentos.SetKey("IdTipoDocumento", TiposDocumentos.IdTipoDocumento.CurrentValue); // Set up key
			} else {
				TiposDocumentos.SetKey("IdTipoDocumento", ""); // Clear key
				CopyRecord = false;
			}
			if (CopyRecord) {
				TiposDocumentos.CurrentAction = "C"; // Copy Record
			} else {
				TiposDocumentos.CurrentAction = "I"; // Display Blank Record
				LoadDefaultValues(); // Load default values
			}
		}

		// Perform action based on action code
		switch (TiposDocumentos.CurrentAction) {
			case "I": // Blank record, no action required
				break;
			case "C": // Copy an existing record
				if (!LoadRow()) { // Load record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("TiposDocumentoslist.aspx"); // No matching record, return to list
				}
				break;
			case "A": // Add new record
				TiposDocumentos.SendEmail = true; // Send email on add success
				if (AddRow(Conn.GetRow(ref OldRecordset))) { // Add successful
					SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
					string sReturnUrl;
					sReturnUrl = TiposDocumentos.ReturnUrl;
					if (ew_GetPageName(sReturnUrl) == "TiposDocumentosview.aspx") sReturnUrl = TiposDocumentos.ViewUrl; // View paging, return to view page with keyurl directly
					Page_Terminate(sReturnUrl); // Clean up and return
				} else {
					TiposDocumentos.EventCancelled = true; // Event cancelled
					RestoreFormValues(); // Add failed, restore form values
				}
				break;
		}

		// Render row based on row type
		TiposDocumentos.RowType = EW_ROWTYPE_ADD; // Render add type

		// Render row
		TiposDocumentos.ResetAttrs();
		RenderRow();
	}

	// Confirm page
	public bool ConfirmPage = false;  // ASPX

	//
	// Get upload file
	//
	public void GetUploadFiles() {

		// Get upload data
		int index = ObjForm.Index; // Save form index
		ObjForm.Index = 0;
		bool confirmPage = ew_NotEmpty(ObjForm.GetValue("a_confirm"));
		ObjForm.Index = index; // Restore form index
	}

	//
	// Load default values
	//
	public void LoadDefaultValues() {
		TiposDocumentos.TipoDocumento.CurrentValue = System.DBNull.Value;
		TiposDocumentos.TipoDocumento.OldValue = TiposDocumentos.TipoDocumento.CurrentValue;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
		if (!TiposDocumentos.TipoDocumento.FldIsDetailKey) {
			TiposDocumentos.TipoDocumento.FormValue = ObjForm.GetValue("x_TipoDocumento");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadOldRecord();
		TiposDocumentos.TipoDocumento.CurrentValue = TiposDocumentos.TipoDocumento.FormValue;
		TiposDocumentos.IdTipoDocumento.CurrentValue = TiposDocumentos.IdTipoDocumento.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = TiposDocumentos.KeyFilter;

		// Row Selecting event
		TiposDocumentos.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		TiposDocumentos.CurrentFilter = sFilter;
		string sSql = TiposDocumentos.SQL;

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
		TiposDocumentos.Row_Selected(ref row);
		TiposDocumentos.IdTipoDocumento.DbValue = row["IdTipoDocumento"];
		TiposDocumentos.TipoDocumento.DbValue = row["TipoDocumento"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(TiposDocumentos.GetKey("IdTipoDocumento")))
			TiposDocumentos.IdTipoDocumento.CurrentValue = TiposDocumentos.GetKey("IdTipoDocumento"); // IdTipoDocumento
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			TiposDocumentos.CurrentFilter = TiposDocumentos.KeyFilter;
			string sSql = TiposDocumentos.SQL;			
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
		// Row Rendering event

		TiposDocumentos.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdTipoDocumento
		// TipoDocumento
		//
		//  View  Row
		//

		if (TiposDocumentos.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdTipoDocumento
				TiposDocumentos.IdTipoDocumento.ViewValue = TiposDocumentos.IdTipoDocumento.CurrentValue;
			TiposDocumentos.IdTipoDocumento.ViewCustomAttributes = "";

			// TipoDocumento
				TiposDocumentos.TipoDocumento.ViewValue = TiposDocumentos.TipoDocumento.CurrentValue;
			TiposDocumentos.TipoDocumento.ViewCustomAttributes = "";

			// View refer script
			// TipoDocumento

			TiposDocumentos.TipoDocumento.LinkCustomAttributes = "";
			TiposDocumentos.TipoDocumento.HrefValue = "";
			TiposDocumentos.TipoDocumento.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (TiposDocumentos.RowType == EW_ROWTYPE_ADD) { // Add row

			// TipoDocumento
			TiposDocumentos.TipoDocumento.EditCustomAttributes = "";
			TiposDocumentos.TipoDocumento.EditValue = ew_HtmlEncode(TiposDocumentos.TipoDocumento.CurrentValue);

			// Edit refer script
			// TipoDocumento

			TiposDocumentos.TipoDocumento.HrefValue = "";
		}
		if (TiposDocumentos.RowType == EW_ROWTYPE_ADD ||
			TiposDocumentos.RowType == EW_ROWTYPE_EDIT ||
			TiposDocumentos.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			TiposDocumentos.SetupFieldTitles();
		}

		// Row Rendered event
		if (TiposDocumentos.RowType != EW_ROWTYPE_AGGREGATEINIT)
			TiposDocumentos.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(TiposDocumentos.TipoDocumento.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + TiposDocumentos.TipoDocumento.FldCaption);

		// Return validate result
		bool Valid = (ew_Empty(gsFormError));

		// Form_CustomValidate event
		string sFormCustomError = "";
		Valid = Valid && Form_CustomValidate(ref sFormCustomError);
		ew_AddMessage(ref gsFormError, sFormCustomError);
		return Valid;
	}

	//
	// Add record
	//
	public bool AddRow(OrderedDictionary RsOld) {
		bool result = false;
		OrderedDictionary RsNew = new OrderedDictionary();
		string sSql;
		string sFilter = "";
		bool bInsertRow;
		SqlDataReader RsChk = null;
		try {

		// TipoDocumento
		TiposDocumentos.TipoDocumento.SetDbValue(ref RsNew, TiposDocumentos.TipoDocumento.CurrentValue, "", false);
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = TiposDocumentos.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				TiposDocumentos.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(TiposDocumentos.CancelMessage)) {
				FailureMessage = TiposDocumentos.CancelMessage;
				TiposDocumentos.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			TiposDocumentos.IdTipoDocumento.DbValue = Conn.GetLastInsertId();
			RsNew["IdTipoDocumento"] = TiposDocumentos.IdTipoDocumento.DbValue;
		}
		if (result) {

			// Row Inserted event
			TiposDocumentos.Row_Inserted(RsOld, RsNew);
		}
		return result;
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
		TiposDocumentos_add = new cTiposDocumentos_add(this);
		CurrentPage = TiposDocumentos_add;

		//CurrentPageType = TiposDocumentos_add.GetType();
		TiposDocumentos_add.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		TiposDocumentos_add.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (TiposDocumentos_add != null)
			TiposDocumentos_add.Dispose();
	}
}
