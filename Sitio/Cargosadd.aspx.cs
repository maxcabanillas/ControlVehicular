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

partial class Cargosadd: AspNetMaker9_ControlVehicular {

	// Page object
	public cCargos_add Cargos_add;	

	//
	// Page Class
	//
	public class cCargos_add: AspNetMakerPage, IDisposable {

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
				if (Cargos.UseTokenInUrl)
					Url += "t=" + Cargos.TableVar + "&"; // Add page token
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
			if (Cargos.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (Cargos.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (Cargos.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public Cargosadd AspNetPage { 
			get { return (Cargosadd)m_ParentPage; }
		}

		// Cargos	
		public cCargos Cargos { 
			get {				
				return ParentPage.Cargos;
			}
			set {
				ParentPage.Cargos = value;	
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
		public cCargos_add(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "add";
			m_PageObjName = "Cargos_add";
			m_PageObjTypeName = "cCargos_add";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (Cargos == null)
				Cargos = new cCargos(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "Cargos";
			m_Table = Cargos;
			CurrentTable = Cargos;

			//CurrentTableType = Cargos.GetType();
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
				Page_Terminate("Cargoslist.aspx");
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
			Cargos.Dispose();
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
			Cargos.CurrentAction = ObjForm.GetValue("a_add"); // Get form action
			CopyRecord = LoadOldRecord(); // Load old recordset
			LoadFormValues(); // Load form values

			// Validate Form
			if (!ValidateForm()) {
				Cargos.CurrentAction = "I"; // Form error, reset action
				Cargos.EventCancelled = true; // Event cancelled
				RestoreFormValues(); // Restore form values
				FailureMessage = gsFormError;
			}

		// Not post back
		} else {

			// Load key values from QueryString
			CopyRecord = true;
			if (ew_NotEmpty(ew_Get("IdCargo"))) {
				Cargos.IdCargo.QueryStringValue = ew_Get("IdCargo");
				Cargos.SetKey("IdCargo", Cargos.IdCargo.CurrentValue); // Set up key
			} else {
				Cargos.SetKey("IdCargo", ""); // Clear key
				CopyRecord = false;
			}
			if (CopyRecord) {
				Cargos.CurrentAction = "C"; // Copy Record
			} else {
				Cargos.CurrentAction = "I"; // Display Blank Record
				LoadDefaultValues(); // Load default values
			}
		}

		// Perform action based on action code
		switch (Cargos.CurrentAction) {
			case "I": // Blank record, no action required
				break;
			case "C": // Copy an existing record
				if (!LoadRow()) { // Load record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("Cargoslist.aspx"); // No matching record, return to list
				}
				break;
			case "A": // Add new record
				Cargos.SendEmail = true; // Send email on add success
				if (AddRow(Conn.GetRow(ref OldRecordset))) { // Add successful
					SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
					string sReturnUrl;
					sReturnUrl = Cargos.ReturnUrl;
					if (ew_GetPageName(sReturnUrl) == "Cargosview.aspx") sReturnUrl = Cargos.ViewUrl; // View paging, return to view page with keyurl directly
					Page_Terminate(sReturnUrl); // Clean up and return
				} else {
					Cargos.EventCancelled = true; // Event cancelled
					RestoreFormValues(); // Add failed, restore form values
				}
				break;
		}

		// Render row based on row type
		Cargos.RowType = EW_ROWTYPE_ADD; // Render add type

		// Render row
		Cargos.ResetAttrs();
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
		Cargos.Cargo.CurrentValue = System.DBNull.Value;
		Cargos.Cargo.OldValue = Cargos.Cargo.CurrentValue;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
		if (!Cargos.Cargo.FldIsDetailKey) {
			Cargos.Cargo.FormValue = ObjForm.GetValue("x_Cargo");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadOldRecord();
		Cargos.Cargo.CurrentValue = Cargos.Cargo.FormValue;
		Cargos.IdCargo.CurrentValue = Cargos.IdCargo.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = Cargos.KeyFilter;

		// Row Selecting event
		Cargos.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		Cargos.CurrentFilter = sFilter;
		string sSql = Cargos.SQL;

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
		Cargos.Row_Selected(ref row);
		Cargos.IdCargo.DbValue = row["IdCargo"];
		Cargos.Cargo.DbValue = row["Cargo"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(Cargos.GetKey("IdCargo")))
			Cargos.IdCargo.CurrentValue = Cargos.GetKey("IdCargo"); // IdCargo
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			Cargos.CurrentFilter = Cargos.KeyFilter;
			string sSql = Cargos.SQL;			
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

		Cargos.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdCargo
		// Cargo
		//
		//  View  Row
		//

		if (Cargos.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdCargo
				Cargos.IdCargo.ViewValue = Cargos.IdCargo.CurrentValue;
			Cargos.IdCargo.ViewCustomAttributes = "";

			// Cargo
				Cargos.Cargo.ViewValue = Cargos.Cargo.CurrentValue;
			Cargos.Cargo.ViewCustomAttributes = "";

			// View refer script
			// Cargo

			Cargos.Cargo.LinkCustomAttributes = "";
			Cargos.Cargo.HrefValue = "";
			Cargos.Cargo.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (Cargos.RowType == EW_ROWTYPE_ADD) { // Add row

			// Cargo
			Cargos.Cargo.EditCustomAttributes = "";
			Cargos.Cargo.EditValue = ew_HtmlEncode(Cargos.Cargo.CurrentValue);

			// Edit refer script
			// Cargo

			Cargos.Cargo.HrefValue = "";
		}
		if (Cargos.RowType == EW_ROWTYPE_ADD ||
			Cargos.RowType == EW_ROWTYPE_EDIT ||
			Cargos.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			Cargos.SetupFieldTitles();
		}

		// Row Rendered event
		if (Cargos.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Cargos.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(Cargos.Cargo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Cargos.Cargo.FldCaption);

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

		// Cargo
		Cargos.Cargo.SetDbValue(ref RsNew, Cargos.Cargo.CurrentValue, "", false);
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = Cargos.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				Cargos.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(Cargos.CancelMessage)) {
				FailureMessage = Cargos.CancelMessage;
				Cargos.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			Cargos.IdCargo.DbValue = Conn.GetLastInsertId();
			RsNew["IdCargo"] = Cargos.IdCargo.DbValue;
		}
		if (result) {

			// Row Inserted event
			Cargos.Row_Inserted(RsOld, RsNew);
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
		Cargos_add = new cCargos_add(this);
		CurrentPage = Cargos_add;

		//CurrentPageType = Cargos_add.GetType();
		Cargos_add.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Cargos_add.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (Cargos_add != null)
			Cargos_add.Dispose();
	}
}
