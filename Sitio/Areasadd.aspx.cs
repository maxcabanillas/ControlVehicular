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

partial class Areasadd: AspNetMaker9_ControlVehicular {

	// Page object
	public cAreas_add Areas_add;	

	//
	// Page Class
	//
	public class cAreas_add: AspNetMakerPage, IDisposable {

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
				if (Areas.UseTokenInUrl)
					Url += "t=" + Areas.TableVar + "&"; // Add page token
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
			if (Areas.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (Areas.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (Areas.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public Areasadd AspNetPage { 
			get { return (Areasadd)m_ParentPage; }
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

		// Personas_grid	
		public cPersonas_grid Personas_grid { 
			get {				
				return ParentPage.Personas_grid;
			}
			set {
				ParentPage.Personas_grid = value;	
			}	
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
		public cAreas_add(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "add";
			m_PageObjName = "Areas_add";
			m_PageObjTypeName = "cAreas_add";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (Areas == null)
				Areas = new cAreas(this);
			if (Personas_grid == null)
				Personas_grid = new cPersonas_grid(ParentPage);
			if (Personas == null)
				Personas = new cPersonas(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "Areas";
			m_Table = Areas;
			CurrentTable = Areas;

			//CurrentTableType = Areas.GetType();
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
				Page_Terminate("Areaslist.aspx");
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
			Areas.Dispose();
			Personas_grid.Dispose();
			Personas.Dispose();
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
			Areas.CurrentAction = ObjForm.GetValue("a_add"); // Get form action
			CopyRecord = LoadOldRecord(); // Load old recordset
			LoadFormValues(); // Load form values

			// Set up detail parameters
			SetUpDetailParms();

			// Validate Form
			if (!ValidateForm()) {
				Areas.CurrentAction = "I"; // Form error, reset action
				Areas.EventCancelled = true; // Event cancelled
				RestoreFormValues(); // Restore form values
				FailureMessage = gsFormError;
			}

		// Not post back
		} else {

			// Load key values from QueryString
			CopyRecord = true;
			if (ew_NotEmpty(ew_Get("IdArea"))) {
				Areas.IdArea.QueryStringValue = ew_Get("IdArea");
				Areas.SetKey("IdArea", Areas.IdArea.CurrentValue); // Set up key
			} else {
				Areas.SetKey("IdArea", ""); // Clear key
				CopyRecord = false;
			}
			if (CopyRecord) {
				Areas.CurrentAction = "C"; // Copy Record
			} else {
				Areas.CurrentAction = "I"; // Display Blank Record
				LoadDefaultValues(); // Load default values
			}
		}

		// Set up detail parameters
		SetUpDetailParms();

		// Perform action based on action code
		switch (Areas.CurrentAction) {
			case "I": // Blank record, no action required
				break;
			case "C": // Copy an existing record
				if (!LoadRow()) { // Load record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("Areaslist.aspx"); // No matching record, return to list
				}
				break;
			case "A": // Add new record
				Areas.SendEmail = true; // Send email on add success
				if (AddRow(Conn.GetRow(ref OldRecordset))) { // Add successful
					SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
					string sReturnUrl;
					if (ew_NotEmpty(Areas.CurrentDetailTable)) // Master/detail add
						sReturnUrl = Areas.DetailUrl;
					else
						sReturnUrl = Areas.ReturnUrl;
					if (ew_GetPageName(sReturnUrl) == "Areasview.aspx") sReturnUrl = Areas.ViewUrl; // View paging, return to view page with keyurl directly
					Page_Terminate(sReturnUrl); // Clean up and return
				} else {
					Areas.EventCancelled = true; // Event cancelled
					RestoreFormValues(); // Add failed, restore form values
				}
				break;
		}

		// Render row based on row type
		Areas.RowType = EW_ROWTYPE_ADD; // Render add type

		// Render row
		Areas.ResetAttrs();
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
		Areas.Area.CurrentValue = System.DBNull.Value;
		Areas.Area.OldValue = Areas.Area.CurrentValue;
		Areas.Codigo.CurrentValue = System.DBNull.Value;
		Areas.Codigo.OldValue = Areas.Codigo.CurrentValue;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
		if (!Areas.Area.FldIsDetailKey) {
			Areas.Area.FormValue = ObjForm.GetValue("x_Area");
		}
		if (!Areas.Codigo.FldIsDetailKey) {
			Areas.Codigo.FormValue = ObjForm.GetValue("x_Codigo");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadOldRecord();
		Areas.Area.CurrentValue = Areas.Area.FormValue;
		Areas.Codigo.CurrentValue = Areas.Codigo.FormValue;
		Areas.IdArea.CurrentValue = Areas.IdArea.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = Areas.KeyFilter;

		// Row Selecting event
		Areas.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		Areas.CurrentFilter = sFilter;
		string sSql = Areas.SQL;

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
		Areas.Row_Selected(ref row);
		Areas.IdArea.DbValue = row["IdArea"];
		Areas.Area.DbValue = row["Area"];
		Areas.Codigo.DbValue = row["Codigo"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(Areas.GetKey("IdArea")))
			Areas.IdArea.CurrentValue = Areas.GetKey("IdArea"); // IdArea
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			Areas.CurrentFilter = Areas.KeyFilter;
			string sSql = Areas.SQL;			
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

		Areas.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdArea
		// Area
		// Codigo
		//
		//  View  Row
		//

		if (Areas.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdArea
				Areas.IdArea.ViewValue = Areas.IdArea.CurrentValue;
			Areas.IdArea.ViewCustomAttributes = "";

			// Area
				Areas.Area.ViewValue = Areas.Area.CurrentValue;
			Areas.Area.ViewCustomAttributes = "";

			// Codigo
				Areas.Codigo.ViewValue = Areas.Codigo.CurrentValue;
			Areas.Codigo.ViewCustomAttributes = "";

			// View refer script
			// Area

			Areas.Area.LinkCustomAttributes = "";
			Areas.Area.HrefValue = "";
			Areas.Area.TooltipValue = "";

			// Codigo
			Areas.Codigo.LinkCustomAttributes = "";
			Areas.Codigo.HrefValue = "";
			Areas.Codigo.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (Areas.RowType == EW_ROWTYPE_ADD) { // Add row

			// Area
			Areas.Area.EditCustomAttributes = "";
			Areas.Area.EditValue = ew_HtmlEncode(Areas.Area.CurrentValue);

			// Codigo
			Areas.Codigo.EditCustomAttributes = "";
			Areas.Codigo.EditValue = ew_HtmlEncode(Areas.Codigo.CurrentValue);

			// Edit refer script
			// Area

			Areas.Area.HrefValue = "";

			// Codigo
			Areas.Codigo.HrefValue = "";
		}
		if (Areas.RowType == EW_ROWTYPE_ADD ||
			Areas.RowType == EW_ROWTYPE_EDIT ||
			Areas.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			Areas.SetupFieldTitles();
		}

		// Row Rendered event
		if (Areas.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Areas.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(Areas.Area.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Areas.Area.FldCaption);

		// Validate detail grid
		if (Areas.CurrentDetailTable == "Personas" && Personas.DetailAdd) {
			Personas_grid = new cPersonas_grid(ParentPage); // Get detail page object (grid)
			Personas_grid.ValidateGridForm();
			Personas_grid.Dispose();
		}

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
		if (ew_NotEmpty(Areas.Area.CurrentValue)) { // Check field with unique index
			sFilter = "([Area] = '" + ew_AdjustSql(Areas.Area.CurrentValue) + "')";
			RsChk = Areas.LoadRs(sFilter);
			if (RsChk != null) {
				FailureMessage = Language.Phrase("DupKey").Replace("%f", "Area").Replace("%v", Convert.ToString(Areas.Area.CurrentValue));
				RsChk.Close();
				RsChk.Dispose();
				return false;
			}
		}
		if (ew_NotEmpty(Areas.Codigo.CurrentValue)) { // Check field with unique index
			sFilter = "([Codigo] = '" + ew_AdjustSql(Areas.Codigo.CurrentValue) + "')";
			RsChk = Areas.LoadRs(sFilter);
			if (RsChk != null) {
				FailureMessage = Language.Phrase("DupKey").Replace("%f", "Codigo").Replace("%v", Convert.ToString(Areas.Codigo.CurrentValue));
				RsChk.Close();
				RsChk.Dispose();
				return false;
			}
		}

		// Begin transaction
		if (ew_NotEmpty(Areas.CurrentDetailTable))
			Conn.BeginTrans();
		try {

		// Area
		Areas.Area.SetDbValue(ref RsNew, Areas.Area.CurrentValue, "", false);

		// Codigo
		Areas.Codigo.SetDbValue(ref RsNew, Areas.Codigo.CurrentValue, System.DBNull.Value, false);
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = Areas.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				Areas.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(Areas.CancelMessage)) {
				FailureMessage = Areas.CancelMessage;
				Areas.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			Areas.IdArea.DbValue = Conn.GetLastInsertId();
			RsNew["IdArea"] = Areas.IdArea.DbValue;
		}

		// Add detail records
		if (result) {
			if (Areas.CurrentDetailTable == "Personas" && Personas.DetailAdd) {
			Personas.IdArea.SessionValue = Areas.IdArea.CurrentValue; // Set master key
				ParentPage.Personas_grid = new cPersonas_grid(ParentPage); // Get detail page object (grid)
				result = ParentPage.Personas_grid.GridInsert();
				ParentPage.Personas_grid.Dispose();
			}
		}

		// Commit/Rollback transaction
		if (ew_NotEmpty(Areas.CurrentDetailTable)) {
			if (result) {
				Conn.CommitTrans(); // Commit transaction
			} else {
				Conn.RollbackTrans(); // Rollback transaction
			}
		}
		if (result) {

			// Row Inserted event
			Areas.Row_Inserted(RsOld, RsNew);
		}
		return result;
	}

	// Set up detail parms based on QueryString
	public void SetUpDetailParms() {
		bool bValidDetail = false;
		string sDetailTblVar = "";

		// Get the keys for master table
		if (HttpContext.Current.Request.QueryString[EW_TABLE_SHOW_DETAIL] != null) {
			sDetailTblVar = ew_Get(EW_TABLE_SHOW_DETAIL);
			Areas.CurrentDetailTable = sDetailTblVar;
		} else {
			sDetailTblVar = Areas.CurrentDetailTable;
		}
		if (ew_NotEmpty(sDetailTblVar)) {
			if (sDetailTblVar == "Personas") {
				if (Personas == null)
					Personas = new cPersonas(this);
				if (Personas.DetailAdd) {
					if (CopyRecord)
						Personas.CurrentMode = "copy";
					else
						Personas.CurrentMode = "add";
					Personas.CurrentAction = "gridadd";

					// Save current master table to detail table
					Personas.CurrentMasterTable = Areas.TableVar;
					Personas.StartRecordNumber = 1;
					Personas.IdArea.FldIsDetailKey = true;
					Personas.IdArea.CurrentValue = Areas.IdArea.CurrentValue;
					Personas.IdArea.SessionValue = Personas.IdArea.CurrentValue;
				}
			}
		}
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
		Areas_add = new cAreas_add(this);
		NameValueCollection fv = new NameValueCollection();
		fv.Add(Request.Form);
		Session["EW_FORM_VALUES"] = fv;
		CurrentPage = Areas_add;

		//CurrentPageType = Areas_add.GetType();
		Areas_add.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Areas_add.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {
		if (Session["EW_FORM_VALUES"] != null) {
			((NameValueCollection)Session["EW_FORM_VALUES"]).Clear();
			Session.Remove("EW_FORM_VALUES");
		}

		// Dispose page object
		if (Areas_add != null)
			Areas_add.Dispose();
	}
}
