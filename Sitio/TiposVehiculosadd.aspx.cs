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

partial class TiposVehiculosadd: AspNetMaker9_ControlVehicular {

	// Page object
	public cTiposVehiculos_add TiposVehiculos_add;	

	//
	// Page Class
	//
	public class cTiposVehiculos_add: AspNetMakerPage, IDisposable {

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
				if (TiposVehiculos.UseTokenInUrl)
					Url += "t=" + TiposVehiculos.TableVar + "&"; // Add page token
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
			if (TiposVehiculos.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (TiposVehiculos.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (TiposVehiculos.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public TiposVehiculosadd AspNetPage { 
			get { return (TiposVehiculosadd)m_ParentPage; }
		}

		// TiposVehiculos	
		public cTiposVehiculos TiposVehiculos { 
			get {				
				return ParentPage.TiposVehiculos;
			}
			set {
				ParentPage.TiposVehiculos = value;	
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
		public cTiposVehiculos_add(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "add";
			m_PageObjName = "TiposVehiculos_add";
			m_PageObjTypeName = "cTiposVehiculos_add";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (TiposVehiculos == null)
				TiposVehiculos = new cTiposVehiculos(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "TiposVehiculos";
			m_Table = TiposVehiculos;
			CurrentTable = TiposVehiculos;

			//CurrentTableType = TiposVehiculos.GetType();
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
				Page_Terminate("TiposVehiculoslist.aspx");
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
			TiposVehiculos.Dispose();
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
			TiposVehiculos.CurrentAction = ObjForm.GetValue("a_add"); // Get form action
			CopyRecord = LoadOldRecord(); // Load old recordset
			LoadFormValues(); // Load form values

			// Validate Form
			if (!ValidateForm()) {
				TiposVehiculos.CurrentAction = "I"; // Form error, reset action
				TiposVehiculos.EventCancelled = true; // Event cancelled
				RestoreFormValues(); // Restore form values
				FailureMessage = gsFormError;
			}

		// Not post back
		} else {

			// Load key values from QueryString
			CopyRecord = true;
			if (ew_NotEmpty(ew_Get("IdTipoVehiculo"))) {
				TiposVehiculos.IdTipoVehiculo.QueryStringValue = ew_Get("IdTipoVehiculo");
				TiposVehiculos.SetKey("IdTipoVehiculo", TiposVehiculos.IdTipoVehiculo.CurrentValue); // Set up key
			} else {
				TiposVehiculos.SetKey("IdTipoVehiculo", ""); // Clear key
				CopyRecord = false;
			}
			if (CopyRecord) {
				TiposVehiculos.CurrentAction = "C"; // Copy Record
			} else {
				TiposVehiculos.CurrentAction = "I"; // Display Blank Record
				LoadDefaultValues(); // Load default values
			}
		}

		// Perform action based on action code
		switch (TiposVehiculos.CurrentAction) {
			case "I": // Blank record, no action required
				break;
			case "C": // Copy an existing record
				if (!LoadRow()) { // Load record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("TiposVehiculoslist.aspx"); // No matching record, return to list
				}
				break;
			case "A": // Add new record
				TiposVehiculos.SendEmail = true; // Send email on add success
				if (AddRow(Conn.GetRow(ref OldRecordset))) { // Add successful
					SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
					string sReturnUrl;
					sReturnUrl = TiposVehiculos.ReturnUrl;
					if (ew_GetPageName(sReturnUrl) == "TiposVehiculosview.aspx") sReturnUrl = TiposVehiculos.ViewUrl; // View paging, return to view page with keyurl directly
					Page_Terminate(sReturnUrl); // Clean up and return
				} else {
					TiposVehiculos.EventCancelled = true; // Event cancelled
					RestoreFormValues(); // Add failed, restore form values
				}
				break;
		}

		// Render row based on row type
		TiposVehiculos.RowType = EW_ROWTYPE_ADD; // Render add type

		// Render row
		TiposVehiculos.ResetAttrs();
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
		TiposVehiculos.TipoVehiculo.CurrentValue = System.DBNull.Value;
		TiposVehiculos.TipoVehiculo.OldValue = TiposVehiculos.TipoVehiculo.CurrentValue;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
		if (!TiposVehiculos.TipoVehiculo.FldIsDetailKey) {
			TiposVehiculos.TipoVehiculo.FormValue = ObjForm.GetValue("x_TipoVehiculo");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadOldRecord();
		TiposVehiculos.TipoVehiculo.CurrentValue = TiposVehiculos.TipoVehiculo.FormValue;
		TiposVehiculos.IdTipoVehiculo.CurrentValue = TiposVehiculos.IdTipoVehiculo.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = TiposVehiculos.KeyFilter;

		// Row Selecting event
		TiposVehiculos.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		TiposVehiculos.CurrentFilter = sFilter;
		string sSql = TiposVehiculos.SQL;

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
		TiposVehiculos.Row_Selected(ref row);
		TiposVehiculos.IdTipoVehiculo.DbValue = row["IdTipoVehiculo"];
		TiposVehiculos.TipoVehiculo.DbValue = row["TipoVehiculo"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(TiposVehiculos.GetKey("IdTipoVehiculo")))
			TiposVehiculos.IdTipoVehiculo.CurrentValue = TiposVehiculos.GetKey("IdTipoVehiculo"); // IdTipoVehiculo
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			TiposVehiculos.CurrentFilter = TiposVehiculos.KeyFilter;
			string sSql = TiposVehiculos.SQL;			
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

		TiposVehiculos.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdTipoVehiculo
		// TipoVehiculo
		//
		//  View  Row
		//

		if (TiposVehiculos.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdTipoVehiculo
				TiposVehiculos.IdTipoVehiculo.ViewValue = TiposVehiculos.IdTipoVehiculo.CurrentValue;
			TiposVehiculos.IdTipoVehiculo.ViewCustomAttributes = "";

			// TipoVehiculo
				TiposVehiculos.TipoVehiculo.ViewValue = TiposVehiculos.TipoVehiculo.CurrentValue;
			TiposVehiculos.TipoVehiculo.ViewCustomAttributes = "";

			// View refer script
			// TipoVehiculo

			TiposVehiculos.TipoVehiculo.LinkCustomAttributes = "";
			TiposVehiculos.TipoVehiculo.HrefValue = "";
			TiposVehiculos.TipoVehiculo.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (TiposVehiculos.RowType == EW_ROWTYPE_ADD) { // Add row

			// TipoVehiculo
			TiposVehiculos.TipoVehiculo.EditCustomAttributes = "";
			TiposVehiculos.TipoVehiculo.EditValue = ew_HtmlEncode(TiposVehiculos.TipoVehiculo.CurrentValue);

			// Edit refer script
			// TipoVehiculo

			TiposVehiculos.TipoVehiculo.HrefValue = "";
		}
		if (TiposVehiculos.RowType == EW_ROWTYPE_ADD ||
			TiposVehiculos.RowType == EW_ROWTYPE_EDIT ||
			TiposVehiculos.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			TiposVehiculos.SetupFieldTitles();
		}

		// Row Rendered event
		if (TiposVehiculos.RowType != EW_ROWTYPE_AGGREGATEINIT)
			TiposVehiculos.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(TiposVehiculos.TipoVehiculo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + TiposVehiculos.TipoVehiculo.FldCaption);

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

		// TipoVehiculo
		TiposVehiculos.TipoVehiculo.SetDbValue(ref RsNew, TiposVehiculos.TipoVehiculo.CurrentValue, "", false);
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = TiposVehiculos.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				TiposVehiculos.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(TiposVehiculos.CancelMessage)) {
				FailureMessage = TiposVehiculos.CancelMessage;
				TiposVehiculos.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			TiposVehiculos.IdTipoVehiculo.DbValue = Conn.GetLastInsertId();
			RsNew["IdTipoVehiculo"] = TiposVehiculos.IdTipoVehiculo.DbValue;
		}
		if (result) {

			// Row Inserted event
			TiposVehiculos.Row_Inserted(RsOld, RsNew);
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
		TiposVehiculos_add = new cTiposVehiculos_add(this);
		CurrentPage = TiposVehiculos_add;

		//CurrentPageType = TiposVehiculos_add.GetType();
		TiposVehiculos_add.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		TiposVehiculos_add.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (TiposVehiculos_add != null)
			TiposVehiculos_add.Dispose();
	}
}
