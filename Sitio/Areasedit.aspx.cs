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

partial class Areasedit: AspNetMaker9_ControlVehicular {

	// Page object
	public cAreas_edit Areas_edit;	

	//
	// Page Class
	//
	public class cAreas_edit: AspNetMakerPage, IDisposable {

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
		public Areasedit AspNetPage { 
			get { return (Areasedit)m_ParentPage; }
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
		public cAreas_edit(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "edit";
			m_PageObjName = "Areas_edit";
			m_PageObjTypeName = "cAreas_edit";

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
			if (!Security.CanEdit) {
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

	public string DbMasterFilter = "";

	public string DbDetailFilter = ""; 

	//
	// Page main processing
	//
	public void Page_Main()
	{

		// Load key from QueryString
		if (ew_NotEmpty(ew_Get("IdArea"))) {
			Areas.IdArea.QueryStringValue = ew_Get("IdArea");
		}
		if (ew_NotEmpty(ObjForm.GetValue("a_edit"))) {
			Areas.CurrentAction = ObjForm.GetValue("a_edit"); // Get action code
			LoadFormValues(); // Get form values

			// Set up detail parameters
			SetUpDetailParms();

			// Validate Form
			if (!ValidateForm()) {
				Areas.CurrentAction = ""; // Form error, reset action
				FailureMessage = gsFormError;
				Areas.EventCancelled = true; // Event cancelled 
				RestoreFormValues(); // Restore form values if validate failed
			}
		} else {
			Areas.CurrentAction = "I"; // Default action is display
		}

		// Check if valid key
		if (ew_Empty(Areas.IdArea.CurrentValue)) Page_Terminate("Areaslist.aspx"); // Invalid key, return to list

		// Set up detail parameters
		SetUpDetailParms();
		switch (Areas.CurrentAction) {
			case "I": // Get a record to display
				if (!LoadRow()) { // Load Record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("Areaslist.aspx"); // No matching record, return to list
				}
				break;
			case "U": // Update
				Areas.SendEmail = true; // Send email on update success
				if (EditRow()) { // Update Record based on key
					SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success
					string sReturnUrl;
					if (ew_NotEmpty(Areas.CurrentDetailTable)) // Master/detail edit
						sReturnUrl = Areas.DetailUrl;
					else
						sReturnUrl = Areas.ReturnUrl;
					Page_Terminate(sReturnUrl); // Return to caller
				} else {
					Areas.EventCancelled = true;
					RestoreFormValues(); // Restore form values if update failed
				}
				break;
		}

		// Render the record
		Areas.RowType = EW_ROWTYPE_EDIT; // Render as edit

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
	// Load form values
	//
	public void LoadFormValues() {
		if (!Areas.IdArea.FldIsDetailKey)
			Areas.IdArea.FormValue = ObjForm.GetValue("x_IdArea");
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
		LoadRow();
		Areas.IdArea.CurrentValue = Areas.IdArea.FormValue;
		Areas.Area.CurrentValue = Areas.Area.FormValue;
		Areas.Codigo.CurrentValue = Areas.Codigo.FormValue;
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
			// IdArea

			Areas.IdArea.LinkCustomAttributes = "";
			Areas.IdArea.HrefValue = "";
			Areas.IdArea.TooltipValue = "";

			// Area
			Areas.Area.LinkCustomAttributes = "";
			Areas.Area.HrefValue = "";
			Areas.Area.TooltipValue = "";

			// Codigo
			Areas.Codigo.LinkCustomAttributes = "";
			Areas.Codigo.HrefValue = "";
			Areas.Codigo.TooltipValue = "";

		//
		//  Edit Row
		//

		} else if (Areas.RowType == EW_ROWTYPE_EDIT) { // Edit row

			// IdArea
			Areas.IdArea.EditCustomAttributes = "";
				Areas.IdArea.EditValue = Areas.IdArea.CurrentValue;
			Areas.IdArea.ViewCustomAttributes = "";

			// Area
			Areas.Area.EditCustomAttributes = "";
			Areas.Area.EditValue = ew_HtmlEncode(Areas.Area.CurrentValue);

			// Codigo
			Areas.Codigo.EditCustomAttributes = "";
			Areas.Codigo.EditValue = ew_HtmlEncode(Areas.Codigo.CurrentValue);

			// Edit refer script
			// IdArea

			Areas.IdArea.HrefValue = "";

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
		if (Areas.CurrentDetailTable == "Personas" && Personas.DetailEdit) {
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
		string sFilter = Areas.KeyFilter;
		if (ew_NotEmpty(Areas.Area.CurrentValue)) { // Check field with unique index
			sFilterChk = "([Area] = '" + ew_AdjustSql(Areas.Area.CurrentValue) + "')";
			sFilterChk = sFilterChk + " AND NOT (" + sFilter + ")";
			Areas.CurrentFilter = sFilterChk;
			sSqlChk = Areas.SQL;
			try {
				RsChk = Conn.GetDataReader(sSqlChk);
				if (RsChk.Read()) {
					sIdxErrMsg = Language.Phrase("DupIndex").Replace("%f", Areas.Area.FldCaption); 
					sIdxErrMsg = sIdxErrMsg.Replace("%v", Convert.ToString(Areas.Area.CurrentValue));
					FailureMessage = sIdxErrMsg;
					return false;
				}
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw; 
				FailureMessage = e.Message;
				return false;
			} finally {
				if (RsChk != null) { 
					RsChk.Close();
					RsChk.Dispose();
				} 
			}
		}
		if (ew_NotEmpty(Areas.Codigo.CurrentValue)) { // Check field with unique index
			sFilterChk = "([Codigo] = '" + ew_AdjustSql(Areas.Codigo.CurrentValue) + "')";
			sFilterChk = sFilterChk + " AND NOT (" + sFilter + ")";
			Areas.CurrentFilter = sFilterChk;
			sSqlChk = Areas.SQL;
			try {
				RsChk = Conn.GetDataReader(sSqlChk);
				if (RsChk.Read()) {
					sIdxErrMsg = Language.Phrase("DupIndex").Replace("%f", Areas.Codigo.FldCaption); 
					sIdxErrMsg = sIdxErrMsg.Replace("%v", Convert.ToString(Areas.Codigo.CurrentValue));
					FailureMessage = sIdxErrMsg;
					return false;
				}
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw; 
				FailureMessage = e.Message;
				return false;
			} finally {
				if (RsChk != null) { 
					RsChk.Close();
					RsChk.Dispose();
				} 
			}
		}
		Areas.CurrentFilter = sFilter;
		sSql = Areas.SQL;
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

			// Begin transaction
			if (ew_NotEmpty(Areas.CurrentDetailTable))
				Conn.BeginTrans();
			try {
				RsOld = Conn.GetRow(ref RsEdit);

			//RsEdit.Close();
				// IdArea
				// Area

				Areas.Area.SetDbValue(ref RsNew, Areas.Area.CurrentValue, "", Areas.Area.ReadOnly);

				// Codigo
				Areas.Codigo.SetDbValue(ref RsNew, Areas.Codigo.CurrentValue, System.DBNull.Value, Areas.Codigo.ReadOnly);
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;
				RsEdit.Close();
				return false;
			}
			RsEdit.Close();

			// Row Updating event
			bUpdateRow = Areas.Row_Updating(RsOld, ref RsNew);
			if (bUpdateRow) {
				try {
					if (RsNew.Count > 0)
						result = Areas.Update(ref RsNew) > 0;
					else
						result = true; // No field to update

					// Update detail records
					if (result) {
						if (Areas.CurrentDetailTable == "Personas" && Personas.DetailEdit) {
							ParentPage.Personas_grid = new cPersonas_grid(ParentPage); // Get detail page object (grid)			
							result = ParentPage.Personas_grid.GridUpdate();
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
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message;
					return false;
				}
			}	else {
				if (ew_NotEmpty(Areas.CancelMessage)) {
					FailureMessage = Areas.CancelMessage;
					Areas.CancelMessage = "";
				} else {
					FailureMessage = Language.Phrase("UpdateCancelled");
				}
				result = false;
			}
		}

		// Row Updated event
		if (result)
			Areas.Row_Updated(RsOld, RsNew);
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
				if (Personas.DetailEdit) {
					Personas.CurrentMode = "edit";
					Personas.CurrentAction = "gridedit";

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
		Areas_edit = new cAreas_edit(this);
		NameValueCollection fv = new NameValueCollection();
		fv.Add(Request.Form);
		Session["EW_FORM_VALUES"] = fv;
		CurrentPage = Areas_edit;

		//CurrentPageType = Areas_edit.GetType();
		Areas_edit.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Areas_edit.Page_Main();
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
		if (Areas_edit != null)
			Areas_edit.Dispose();
	}
}
