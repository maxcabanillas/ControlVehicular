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

partial class UserLevelsedit: AspNetMaker9_ControlVehicular {

	// Page object
	public cUserLevels_edit UserLevels_edit;	

	//
	// Page Class
	//
	public class cUserLevels_edit: AspNetMakerPage, IDisposable {

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
		public UserLevelsedit AspNetPage { 
			get { return (UserLevelsedit)m_ParentPage; }
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
		public cUserLevels_edit(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "edit";
			m_PageObjName = "UserLevels_edit";
			m_PageObjTypeName = "cUserLevels_edit";

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

	public string DbMasterFilter = "";

	public string DbDetailFilter = ""; 

	//
	// Page main processing
	//
	public void Page_Main()
	{

		// Load key from QueryString
		if (ew_NotEmpty(ew_Get("UserLevelID"))) {
			UserLevels.UserLevelID.QueryStringValue = ew_Get("UserLevelID");
		}
		if (ew_NotEmpty(ObjForm.GetValue("a_edit"))) {
			UserLevels.CurrentAction = ObjForm.GetValue("a_edit"); // Get action code
			LoadFormValues(); // Get form values

			// Validate Form
			if (!ValidateForm()) {
				UserLevels.CurrentAction = ""; // Form error, reset action
				FailureMessage = gsFormError;
				UserLevels.EventCancelled = true; // Event cancelled 
				RestoreFormValues(); // Restore form values if validate failed
			}
		} else {
			UserLevels.CurrentAction = "I"; // Default action is display
		}

		// Check if valid key
		if (ew_Empty(UserLevels.UserLevelID.CurrentValue)) Page_Terminate("UserLevelslist.aspx"); // Invalid key, return to list
		switch (UserLevels.CurrentAction) {
			case "I": // Get a record to display
				if (!LoadRow()) { // Load Record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("UserLevelslist.aspx"); // No matching record, return to list
				}
				break;
			case "U": // Update
				UserLevels.SendEmail = true; // Send email on update success
				if (EditRow()) { // Update Record based on key
					SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success
					string sReturnUrl;
					sReturnUrl = UserLevels.ReturnUrl;
					Page_Terminate(sReturnUrl); // Return to caller
				} else {
					UserLevels.EventCancelled = true;
					RestoreFormValues(); // Restore form values if update failed
				}
				break;
		}

		// Render the record
		UserLevels.RowType = EW_ROWTYPE_EDIT; // Render as edit

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
		LoadRow();
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
		//  Edit Row
		//

		} else if (UserLevels.RowType == EW_ROWTYPE_EDIT) { // Edit row

			// UserLevelID
			UserLevels.UserLevelID.EditCustomAttributes = "";
				UserLevels.UserLevelID.EditValue = UserLevels.UserLevelID.CurrentValue;
			UserLevels.UserLevelID.ViewCustomAttributes = "";

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
	// Update record based on key values
	//
	public bool EditRow()	{
		bool result = false;
		SqlDataReader RsEdit = null;
		SqlDataReader RsChk = null;
		string sSql;
		string sSqlChk;
		string sFilterChk;
		bool bUpdateRow;
		OrderedDictionary RsOld = null;
		string sIdxErrMsg;
		OrderedDictionary RsNew = new OrderedDictionary();
		string sFilter = UserLevels.KeyFilter;
		UserLevels.CurrentFilter = sFilter;
		sSql = UserLevels.SQL;
		try {
			RsEdit = Conn.GetDataReader(sSql);
		}	catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw; 
			FailureMessage = e.Message;
			RsEdit.Close();
			return false;
		}
		if (!RsEdit.Read())	{
			RsEdit.Close();
			return false; // Update Failed
		}	else {
			try {
				RsOld = Conn.GetRow(ref RsEdit);

			//RsEdit.Close();
				// UserLevelID
				// UserLevelName

				UserLevels.UserLevelName.SetDbValue(ref RsNew, UserLevels.UserLevelName.CurrentValue, "", UserLevels.UserLevelName.ReadOnly);
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;
				RsEdit.Close();
				return false;
			}
			RsEdit.Close();

			// Row Updating event
			bUpdateRow = UserLevels.Row_Updating(RsOld, ref RsNew);
			if (bUpdateRow) {
				try {
					if (RsNew.Count > 0)
						result = UserLevels.Update(ref RsNew) > 0;
					else
						result = true; // No field to update
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message;
					return false;
				}
			}	else {
				if (ew_NotEmpty(UserLevels.CancelMessage)) {
					FailureMessage = UserLevels.CancelMessage;
					UserLevels.CancelMessage = "";
				} else {
					FailureMessage = Language.Phrase("UpdateCancelled");
				}
				result = false;
			}
		}

		// Row Updated event
		if (result)
			UserLevels.Row_Updated(RsOld, RsNew);
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
		UserLevels_edit = new cUserLevels_edit(this);
		CurrentPage = UserLevels_edit;

		//CurrentPageType = UserLevels_edit.GetType();
		UserLevels_edit.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		UserLevels_edit.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (UserLevels_edit != null)
			UserLevels_edit.Dispose();
	}
}
