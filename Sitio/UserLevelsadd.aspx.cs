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

partial class UserLevelsadd: AspNetMaker9_ControlVehicular {

	// Page object
	public cUserLevels_add UserLevels_add;	

	//
	// Page Class
	//
	public class cUserLevels_add: AspNetMakerPage, IDisposable {

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
				if (UserLevels.UseTokenInUrl)
					Url += "t=" + UserLevels.TableVar + "&"; // Add page token
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
			if (UserLevels.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (UserLevels.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (UserLevels.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public UserLevelsadd AspNetPage { 
			get { return (UserLevelsadd)m_ParentPage; }
		}

		// UserLevels	
		public cUserLevels UserLevels { 
			get {				
				return ParentPage.UserLevels;
			}
			set {
				ParentPage.UserLevels = value;	
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
		public cUserLevels_add(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "add";
			m_PageObjName = "UserLevels_add";
			m_PageObjTypeName = "cUserLevels_add";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (UserLevels == null)
				UserLevels = new cUserLevels(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "UserLevels";
			m_Table = UserLevels;
			CurrentTable = UserLevels;

			//CurrentTableType = UserLevels.GetType();
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
			if (!Security.CanAdmin) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
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
			UserLevels.Dispose();
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
			UserLevels.CurrentAction = ObjForm.GetValue("a_add"); // Get form action
			CopyRecord = LoadOldRecord(); // Load old recordset
			LoadFormValues(); // Load form values

			// Load values for user privileges
			int x__AllowAdd = ew_ConvertToInt(ObjForm.GetValue("x__AllowAdd"));
			int x__AllowEdit = ew_ConvertToInt(ObjForm.GetValue("x__AllowEdit"));
			int x__AllowDelete = ew_ConvertToInt(ObjForm.GetValue("x__AllowDelete"));
			int x__AllowList = ew_ConvertToInt(ObjForm.GetValue("x__AllowList"));
			if (EW_USER_LEVEL_COMPAT)	{
				Priv = x__AllowAdd + x__AllowEdit + x__AllowDelete + x__AllowList;
			}	else {
				int x__AllowView = ew_ConvertToInt(ObjForm.GetValue("x__AllowView"));
				int x__AllowSearch = ew_ConvertToInt(ObjForm.GetValue("x__AllowSearch"));
				Priv = x__AllowAdd + x__AllowEdit + x__AllowDelete + x__AllowList + x__AllowView + x__AllowSearch;
			}

			// Validate Form
			if (!ValidateForm()) {
				UserLevels.CurrentAction = "I"; // Form error, reset action
				UserLevels.EventCancelled = true; // Event cancelled
				RestoreFormValues(); // Restore form values
				FailureMessage = gsFormError;
			}

		// Not post back
		} else {

			// Load key values from QueryString
			CopyRecord = true;
			if (ew_NotEmpty(ew_Get("UserLevelID"))) {
				UserLevels.UserLevelID.QueryStringValue = ew_Get("UserLevelID");
				UserLevels.SetKey("UserLevelID", UserLevels.UserLevelID.CurrentValue); // Set up key
			} else {
				UserLevels.SetKey("UserLevelID", ""); // Clear key
				CopyRecord = false;
			}
			if (CopyRecord) {
				UserLevels.CurrentAction = "C"; // Copy Record
			} else {
				UserLevels.CurrentAction = "I"; // Display Blank Record
				LoadDefaultValues(); // Load default values
			}
		}

		// Perform action based on action code
		switch (UserLevels.CurrentAction) {
			case "I": // Blank record, no action required
				break;
			case "C": // Copy an existing record
				if (!LoadRow()) { // Load record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("UserLevelslist.aspx"); // No matching record, return to list
				}
				break;
			case "A": // Add new record
				UserLevels.SendEmail = true; // Send email on add success
				if (AddRow(Conn.GetRow(ref OldRecordset))) { // Add successful
					SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
					string sReturnUrl;
					sReturnUrl = UserLevels.ReturnUrl;
					if (ew_GetPageName(sReturnUrl) == "UserLevelsview.aspx") sReturnUrl = UserLevels.ViewUrl; // View paging, return to view page with keyurl directly
					Page_Terminate(sReturnUrl); // Clean up and return
				} else {
					UserLevels.EventCancelled = true; // Event cancelled
					RestoreFormValues(); // Add failed, restore form values
				}
				break;
		}

		// Render row based on row type
		UserLevels.RowType = EW_ROWTYPE_ADD; // Render add type

		// Render row
		UserLevels.ResetAttrs();
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
		UserLevels.UserLevelID.CurrentValue = System.DBNull.Value;
		UserLevels.UserLevelID.OldValue = UserLevels.UserLevelID.CurrentValue;
		UserLevels.UserLevelName.CurrentValue = System.DBNull.Value;
		UserLevels.UserLevelName.OldValue = UserLevels.UserLevelName.CurrentValue;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
		if (!UserLevels.UserLevelID.FldIsDetailKey) {
			UserLevels.UserLevelID.FormValue = ObjForm.GetValue("x_UserLevelID");
		}
		if (!UserLevels.UserLevelName.FldIsDetailKey) {
			UserLevels.UserLevelName.FormValue = ObjForm.GetValue("x_UserLevelName");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadOldRecord();
		UserLevels.UserLevelID.CurrentValue = UserLevels.UserLevelID.FormValue;
		UserLevels.UserLevelName.CurrentValue = UserLevels.UserLevelName.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = UserLevels.KeyFilter;

		// Row Selecting event
		UserLevels.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		UserLevels.CurrentFilter = sFilter;
		string sSql = UserLevels.SQL;

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
		UserLevels.Row_Selected(ref row);
		UserLevels.UserLevelID.DbValue = row["UserLevelID"];
		UserLevels.UserLevelID.CurrentValue = ew_ConvertToInt(UserLevels.UserLevelID.CurrentValue);
		UserLevels.UserLevelName.DbValue = row["UserLevelName"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(UserLevels.GetKey("UserLevelID")))
			UserLevels.UserLevelID.CurrentValue = UserLevels.GetKey("UserLevelID"); // UserLevelID
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			UserLevels.CurrentFilter = UserLevels.KeyFilter;
			string sSql = UserLevels.SQL;			
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

		UserLevels.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// UserLevelID
		// UserLevelName
		//
		//  View  Row
		//

		if (UserLevels.RowType == EW_ROWTYPE_VIEW) { // View row

			// UserLevelID
				UserLevels.UserLevelID.ViewValue = UserLevels.UserLevelID.CurrentValue;
			UserLevels.UserLevelID.ViewCustomAttributes = "";

			// UserLevelName
				UserLevels.UserLevelName.ViewValue = UserLevels.UserLevelName.CurrentValue;
			UserLevels.UserLevelName.ViewCustomAttributes = "";

			// View refer script
			// UserLevelID

			UserLevels.UserLevelID.LinkCustomAttributes = "";
			UserLevels.UserLevelID.HrefValue = "";
			UserLevels.UserLevelID.TooltipValue = "";

			// UserLevelName
			UserLevels.UserLevelName.LinkCustomAttributes = "";
			UserLevels.UserLevelName.HrefValue = "";
			UserLevels.UserLevelName.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (UserLevels.RowType == EW_ROWTYPE_ADD) { // Add row

			// UserLevelID
			UserLevels.UserLevelID.EditCustomAttributes = "";
			UserLevels.UserLevelID.EditValue = ew_HtmlEncode(UserLevels.UserLevelID.CurrentValue);

			// UserLevelName
			UserLevels.UserLevelName.EditCustomAttributes = "";
			UserLevels.UserLevelName.EditValue = ew_HtmlEncode(UserLevels.UserLevelName.CurrentValue);

			// Edit refer script
			// UserLevelID

			UserLevels.UserLevelID.HrefValue = "";

			// UserLevelName
			UserLevels.UserLevelName.HrefValue = "";
		}
		if (UserLevels.RowType == EW_ROWTYPE_ADD ||
			UserLevels.RowType == EW_ROWTYPE_EDIT ||
			UserLevels.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			UserLevels.SetupFieldTitles();
		}

		// Row Rendered event
		if (UserLevels.RowType != EW_ROWTYPE_AGGREGATEINIT)
			UserLevels.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(UserLevels.UserLevelID.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + UserLevels.UserLevelID.FldCaption);
		if (!ew_CheckInteger(UserLevels.UserLevelID.FormValue))
			ew_AddMessage(ref gsFormError, UserLevels.UserLevelID.FldErrMsg);
		if (ew_Empty(UserLevels.UserLevelName.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + UserLevels.UserLevelName.FldCaption);

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
		if (ew_Empty(UserLevels.UserLevelID.CurrentValue)) {
			FailureMessage = Language.Phrase("MissingUserLevelID");
		} else if (ew_Empty(UserLevels.UserLevelName.CurrentValue)) {
			FailureMessage = Language.Phrase("MissingUserLevelName");
		} else if (!Information.IsNumeric(UserLevels.UserLevelID.CurrentValue)) {
			FailureMessage = Language.Phrase("UserLevelIDInteger");
		} else if (ew_ConvertToInt(UserLevels.UserLevelID.CurrentValue) < -1) {
			FailureMessage = Language.Phrase("UserLevelIDIncorrect");
		} else if (ew_ConvertToInt(UserLevels.UserLevelID.CurrentValue) == 0 && !ew_SameText(UserLevels.UserLevelName.CurrentValue, "default")) {
			FailureMessage = Language.Phrase("UserLevelDefaultName");
		} else if (ew_ConvertToInt(UserLevels.UserLevelID.CurrentValue) == -1 && !ew_SameText(UserLevels.UserLevelName.CurrentValue, "administrator")) {
			FailureMessage = Language.Phrase("UserLevelAdministratorName");
		} else if (ew_ConvertToInt(UserLevels.UserLevelID.CurrentValue) > 0 && (ew_SameText(UserLevels.UserLevelName.CurrentValue, "administrator") || ew_SameText(UserLevels.UserLevelName.CurrentValue, "default"))) {
			FailureMessage = Language.Phrase("UserLevelNameIncorrect");
		}
		if (ew_NotEmpty(Message)) return false;

		// Check if key value entered
		if (ew_Empty(UserLevels.UserLevelID.CurrentValue) && ew_Empty(UserLevels.UserLevelID.SessionValue)) {
			FailureMessage = Language.Phrase("InvalidKeyValue");
			return false;
		}

		// Check for duplicate key
		bool bCheckKey = true;
		sFilter = UserLevels.KeyFilter;
		if (bCheckKey) {
			RsChk = UserLevels.LoadRs(sFilter);
			if (RsChk != null) {
				FailureMessage = Language.Phrase("DupKey").Replace("%f", sFilter);
				RsChk.Close();
				RsChk.Dispose();
				return false;
			}
		}
		if (ew_NotEmpty(UserLevels.UserLevelID.CurrentValue)) { // Check field with unique index
			sFilter = "([UserLevelID] = " + ew_AdjustSql(UserLevels.UserLevelID.CurrentValue) + ")";
			RsChk = UserLevels.LoadRs(sFilter);
			if (RsChk != null) {
				FailureMessage = Language.Phrase("DupKey").Replace("%f", "UserLevelID").Replace("%v", Convert.ToString(UserLevels.UserLevelID.CurrentValue));
				RsChk.Close();
				RsChk.Dispose();
				return false;
			}
		}
		try {

		// UserLevelID
		UserLevels.UserLevelID.SetDbValue(ref RsNew, UserLevels.UserLevelID.CurrentValue, 0, false);

		// UserLevelName
		UserLevels.UserLevelName.SetDbValue(ref RsNew, UserLevels.UserLevelName.CurrentValue, "", false);
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = UserLevels.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				UserLevels.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(UserLevels.CancelMessage)) {
				FailureMessage = UserLevels.CancelMessage;
				UserLevels.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
		}
		if (result) {

			// Row Inserted event
			UserLevels.Row_Inserted(RsOld, RsNew);
		}

		// Add user level priv
		string[] TableNames = new string[0];
		GetTableNames(ref TableNames);		
		if (Priv > 0 && Information.IsArray(TableNames)) {
			for (int i = 0; i < TableNames.Length; i++) {
				sSql = "INSERT INTO " + EW_USER_LEVEL_PRIV_TABLE + " (" +
					EW_USER_LEVEL_PRIV_TABLE_NAME_FIELD + ", " +
					EW_USER_LEVEL_PRIV_USER_LEVEL_ID_FIELD + ", " +
					EW_USER_LEVEL_PRIV_PRIV_FIELD + ") VALUES ('" +
					ew_AdjustSql(TableNames[i]) +
					"', " + UserLevels.UserLevelID.CurrentValue + ", " + Priv + ")";
				Conn.Execute(sSql);
			}
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
		UserLevels_add = new cUserLevels_add(this);
		CurrentPage = UserLevels_add;

		//CurrentPageType = UserLevels_add.GetType();
		UserLevels_add.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		UserLevels_add.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (UserLevels_add != null)
			UserLevels_add.Dispose();
	}
}
