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

partial class TiposDocumentosedit: AspNetMaker9_ControlVehicular {

	// Page object
	public cTiposDocumentos_edit TiposDocumentos_edit;	

	//
	// Page Class
	//
	public class cTiposDocumentos_edit: AspNetMakerPage, IDisposable {

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
		public TiposDocumentosedit AspNetPage { 
			get { return (TiposDocumentosedit)m_ParentPage; }
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
		public cTiposDocumentos_edit(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "edit";
			m_PageObjName = "TiposDocumentos_edit";
			m_PageObjTypeName = "cTiposDocumentos_edit";

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
			if (!Security.CanEdit) {
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

	public string DbMasterFilter = "";

	public string DbDetailFilter = ""; 

	//
	// Page main processing
	//
	public void Page_Main()
	{

		// Load key from QueryString
		if (ew_NotEmpty(ew_Get("IdTipoDocumento"))) {
			TiposDocumentos.IdTipoDocumento.QueryStringValue = ew_Get("IdTipoDocumento");
		}
		if (ew_NotEmpty(ObjForm.GetValue("a_edit"))) {
			TiposDocumentos.CurrentAction = ObjForm.GetValue("a_edit"); // Get action code
			LoadFormValues(); // Get form values

			// Validate Form
			if (!ValidateForm()) {
				TiposDocumentos.CurrentAction = ""; // Form error, reset action
				FailureMessage = gsFormError;
				TiposDocumentos.EventCancelled = true; // Event cancelled 
				RestoreFormValues(); // Restore form values if validate failed
			}
		} else {
			TiposDocumentos.CurrentAction = "I"; // Default action is display
		}

		// Check if valid key
		if (ew_Empty(TiposDocumentos.IdTipoDocumento.CurrentValue)) Page_Terminate("TiposDocumentoslist.aspx"); // Invalid key, return to list
		switch (TiposDocumentos.CurrentAction) {
			case "I": // Get a record to display
				if (!LoadRow()) { // Load Record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("TiposDocumentoslist.aspx"); // No matching record, return to list
				}
				break;
			case "U": // Update
				TiposDocumentos.SendEmail = true; // Send email on update success
				if (EditRow()) { // Update Record based on key
					SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success
					string sReturnUrl;
					sReturnUrl = TiposDocumentos.ReturnUrl;
					Page_Terminate(sReturnUrl); // Return to caller
				} else {
					TiposDocumentos.EventCancelled = true;
					RestoreFormValues(); // Restore form values if update failed
				}
				break;
		}

		// Render the record
		TiposDocumentos.RowType = EW_ROWTYPE_EDIT; // Render as edit

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
	// Load form values
	//
	public void LoadFormValues() {
		if (!TiposDocumentos.IdTipoDocumento.FldIsDetailKey)
			TiposDocumentos.IdTipoDocumento.FormValue = ObjForm.GetValue("x_IdTipoDocumento");
		if (!TiposDocumentos.TipoDocumento.FldIsDetailKey) {
			TiposDocumentos.TipoDocumento.FormValue = ObjForm.GetValue("x_TipoDocumento");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadRow();
		TiposDocumentos.IdTipoDocumento.CurrentValue = TiposDocumentos.IdTipoDocumento.FormValue;
		TiposDocumentos.TipoDocumento.CurrentValue = TiposDocumentos.TipoDocumento.FormValue;
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
			// IdTipoDocumento

			TiposDocumentos.IdTipoDocumento.LinkCustomAttributes = "";
			TiposDocumentos.IdTipoDocumento.HrefValue = "";
			TiposDocumentos.IdTipoDocumento.TooltipValue = "";

			// TipoDocumento
			TiposDocumentos.TipoDocumento.LinkCustomAttributes = "";
			TiposDocumentos.TipoDocumento.HrefValue = "";
			TiposDocumentos.TipoDocumento.TooltipValue = "";

		//
		//  Edit Row
		//

		} else if (TiposDocumentos.RowType == EW_ROWTYPE_EDIT) { // Edit row

			// IdTipoDocumento
			TiposDocumentos.IdTipoDocumento.EditCustomAttributes = "";
				TiposDocumentos.IdTipoDocumento.EditValue = TiposDocumentos.IdTipoDocumento.CurrentValue;
			TiposDocumentos.IdTipoDocumento.ViewCustomAttributes = "";

			// TipoDocumento
			TiposDocumentos.TipoDocumento.EditCustomAttributes = "";
			TiposDocumentos.TipoDocumento.EditValue = ew_HtmlEncode(TiposDocumentos.TipoDocumento.CurrentValue);

			// Edit refer script
			// IdTipoDocumento

			TiposDocumentos.IdTipoDocumento.HrefValue = "";

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
		string sFilter = TiposDocumentos.KeyFilter;
		TiposDocumentos.CurrentFilter = sFilter;
		sSql = TiposDocumentos.SQL;
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
				// IdTipoDocumento
				// TipoDocumento

				TiposDocumentos.TipoDocumento.SetDbValue(ref RsNew, TiposDocumentos.TipoDocumento.CurrentValue, "", TiposDocumentos.TipoDocumento.ReadOnly);
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;
				RsEdit.Close();
				return false;
			}
			RsEdit.Close();

			// Row Updating event
			bUpdateRow = TiposDocumentos.Row_Updating(RsOld, ref RsNew);
			if (bUpdateRow) {
				try {
					if (RsNew.Count > 0)
						result = TiposDocumentos.Update(ref RsNew) > 0;
					else
						result = true; // No field to update
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message;
					return false;
				}
			}	else {
				if (ew_NotEmpty(TiposDocumentos.CancelMessage)) {
					FailureMessage = TiposDocumentos.CancelMessage;
					TiposDocumentos.CancelMessage = "";
				} else {
					FailureMessage = Language.Phrase("UpdateCancelled");
				}
				result = false;
			}
		}

		// Row Updated event
		if (result)
			TiposDocumentos.Row_Updated(RsOld, RsNew);
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
		TiposDocumentos_edit = new cTiposDocumentos_edit(this);
		CurrentPage = TiposDocumentos_edit;

		//CurrentPageType = TiposDocumentos_edit.GetType();
		TiposDocumentos_edit.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		TiposDocumentos_edit.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (TiposDocumentos_edit != null)
			TiposDocumentos_edit.Dispose();
	}
}
