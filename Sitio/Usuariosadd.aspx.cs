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

partial class Usuariosadd: AspNetMaker9_ControlVehicular {

	// Page object
	public cUsuarios_add Usuarios_add;	

	//
	// Page Class
	//
	public class cUsuarios_add: AspNetMakerPage, IDisposable {

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
		public Usuariosadd AspNetPage { 
			get { return (Usuariosadd)m_ParentPage; }
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
		public cUsuarios_add(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "add";
			m_PageObjName = "Usuarios_add";
			m_PageObjTypeName = "cUsuarios_add";

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
			Usuarios.CurrentAction = ObjForm.GetValue("a_add"); // Get form action
			CopyRecord = LoadOldRecord(); // Load old recordset
			LoadFormValues(); // Load form values

			// Validate Form
			if (!ValidateForm()) {
				Usuarios.CurrentAction = "I"; // Form error, reset action
				Usuarios.EventCancelled = true; // Event cancelled
				RestoreFormValues(); // Restore form values
				FailureMessage = gsFormError;
			}

		// Not post back
		} else {

			// Load key values from QueryString
			CopyRecord = true;
			if (ew_NotEmpty(ew_Get("IdUsuario"))) {
				Usuarios.IdUsuario.QueryStringValue = ew_Get("IdUsuario");
				Usuarios.SetKey("IdUsuario", Usuarios.IdUsuario.CurrentValue); // Set up key
			} else {
				Usuarios.SetKey("IdUsuario", ""); // Clear key
				CopyRecord = false;
			}
			if (CopyRecord) {
				Usuarios.CurrentAction = "C"; // Copy Record
			} else {
				Usuarios.CurrentAction = "I"; // Display Blank Record
				LoadDefaultValues(); // Load default values
			}
		}

		// Perform action based on action code
		switch (Usuarios.CurrentAction) {
			case "I": // Blank record, no action required
				break;
			case "C": // Copy an existing record
				if (!LoadRow()) { // Load record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("Usuarioslist.aspx"); // No matching record, return to list
				}
				break;
			case "A": // Add new record
				Usuarios.SendEmail = true; // Send email on add success
				if (AddRow(Conn.GetRow(ref OldRecordset))) { // Add successful
					SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
					string sReturnUrl;
					sReturnUrl = Usuarios.ReturnUrl;
					if (ew_GetPageName(sReturnUrl) == "Usuariosview.aspx") sReturnUrl = Usuarios.ViewUrl; // View paging, return to view page with keyurl directly
					Page_Terminate(sReturnUrl); // Clean up and return
				} else {
					Usuarios.EventCancelled = true; // Event cancelled
					RestoreFormValues(); // Add failed, restore form values
				}
				break;
		}

		// Render row based on row type
		Usuarios.RowType = EW_ROWTYPE_ADD; // Render add type

		// Render row
		Usuarios.ResetAttrs();
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
		Usuarios.Usuario.CurrentValue = System.DBNull.Value;
		Usuarios.Usuario.OldValue = Usuarios.Usuario.CurrentValue;
		Usuarios.NombreUsuario.CurrentValue = System.DBNull.Value;
		Usuarios.NombreUsuario.OldValue = Usuarios.NombreUsuario.CurrentValue;
		Usuarios.Password.CurrentValue = System.DBNull.Value;
		Usuarios.Password.OldValue = Usuarios.Password.CurrentValue;
		Usuarios.Correo.CurrentValue = System.DBNull.Value;
		Usuarios.Correo.OldValue = Usuarios.Correo.CurrentValue;
		Usuarios.IdUsuarioNivel.CurrentValue = System.DBNull.Value;
		Usuarios.IdUsuarioNivel.OldValue = Usuarios.IdUsuarioNivel.CurrentValue;
		Usuarios.Activo.CurrentValue = System.DBNull.Value;
		Usuarios.Activo.OldValue = Usuarios.Activo.CurrentValue;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
		if (!Usuarios.Usuario.FldIsDetailKey) {
			Usuarios.Usuario.FormValue = ObjForm.GetValue("x_Usuario");
		}
		if (!Usuarios.NombreUsuario.FldIsDetailKey) {
			Usuarios.NombreUsuario.FormValue = ObjForm.GetValue("x_NombreUsuario");
		}
		if (!Usuarios.Password.FldIsDetailKey) {
			Usuarios.Password.FormValue = ObjForm.GetValue("x_Password");
		}
		if (!Usuarios.Correo.FldIsDetailKey) {
			Usuarios.Correo.FormValue = ObjForm.GetValue("x_Correo");
		}
		if (!Usuarios.IdUsuarioNivel.FldIsDetailKey) {
			Usuarios.IdUsuarioNivel.FormValue = ObjForm.GetValue("x_IdUsuarioNivel");
		}
		if (!Usuarios.Activo.FldIsDetailKey) {
			Usuarios.Activo.FormValue = ObjForm.GetValue("x_Activo");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadOldRecord();
		Usuarios.Usuario.CurrentValue = Usuarios.Usuario.FormValue;
		Usuarios.NombreUsuario.CurrentValue = Usuarios.NombreUsuario.FormValue;
		Usuarios.Password.CurrentValue = Usuarios.Password.FormValue;
		Usuarios.Correo.CurrentValue = Usuarios.Correo.FormValue;
		Usuarios.IdUsuarioNivel.CurrentValue = Usuarios.IdUsuarioNivel.FormValue;
		Usuarios.Activo.CurrentValue = Usuarios.Activo.FormValue;
		Usuarios.IdUsuario.CurrentValue = Usuarios.IdUsuario.FormValue;
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

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(Usuarios.GetKey("IdUsuario")))
			Usuarios.IdUsuario.CurrentValue = Usuarios.GetKey("IdUsuario"); // IdUsuario
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			Usuarios.CurrentFilter = Usuarios.KeyFilter;
			string sSql = Usuarios.SQL;			
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

		//
		//  Add Row
		//

		} else if (Usuarios.RowType == EW_ROWTYPE_ADD) { // Add row

			// Usuario
			Usuarios.Usuario.EditCustomAttributes = "";
			Usuarios.Usuario.EditValue = ew_HtmlEncode(Usuarios.Usuario.CurrentValue);

			// NombreUsuario
			Usuarios.NombreUsuario.EditCustomAttributes = "";
			Usuarios.NombreUsuario.EditValue = ew_HtmlEncode(Usuarios.NombreUsuario.CurrentValue);

			// Password
			Usuarios.Password.EditCustomAttributes = "";
			Usuarios.Password.EditValue = ew_HtmlEncode(Usuarios.Password.CurrentValue);

			// Correo
			Usuarios.Correo.EditCustomAttributes = "";
			Usuarios.Correo.EditValue = ew_HtmlEncode(Usuarios.Correo.CurrentValue);

			// IdUsuarioNivel
			Usuarios.IdUsuarioNivel.EditCustomAttributes = "";
			if (!Security.CanAdmin) { // System admin
				Usuarios.IdUsuarioNivel.EditValue = "********";
			} else {
			sFilterWrk = "";
			sSqlWrk = "SELECT [UserLevelID], [UserLevelName] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[UserLevels]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			Usuarios.IdUsuarioNivel.EditValue = alwrk;
			}

			// Activo
			Usuarios.Activo.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(Usuarios.Activo.FldTagCaption(1))) ? Usuarios.Activo.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(Usuarios.Activo.FldTagCaption(2))) ? Usuarios.Activo.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			Usuarios.Activo.EditValue = alwrk;

			// Edit refer script
			// Usuario

			Usuarios.Usuario.HrefValue = "";

			// NombreUsuario
			Usuarios.NombreUsuario.HrefValue = "";

			// Password
			Usuarios.Password.HrefValue = "";

			// Correo
			Usuarios.Correo.HrefValue = "";

			// IdUsuarioNivel
			Usuarios.IdUsuarioNivel.HrefValue = "";

			// Activo
			Usuarios.Activo.HrefValue = "";
		}
		if (Usuarios.RowType == EW_ROWTYPE_ADD ||
			Usuarios.RowType == EW_ROWTYPE_EDIT ||
			Usuarios.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			Usuarios.SetupFieldTitles();
		}

		// Row Rendered event
		if (Usuarios.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Usuarios.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(Usuarios.Usuario.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Usuarios.Usuario.FldCaption);
		if (ew_Empty(Usuarios.NombreUsuario.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Usuarios.NombreUsuario.FldCaption);
		if (ew_Empty(Usuarios.Password.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Usuarios.Password.FldCaption);
		if (!ew_CheckEmail(Usuarios.Correo.FormValue))
			ew_AddMessage(ref gsFormError, Usuarios.Correo.FldErrMsg);
		if (ew_Empty(Usuarios.IdUsuarioNivel.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Usuarios.IdUsuarioNivel.FldCaption);
		if (ew_Empty(Usuarios.Activo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Usuarios.Activo.FldCaption);

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

		// Usuario
		Usuarios.Usuario.SetDbValue(ref RsNew, Usuarios.Usuario.CurrentValue, "", false);

		// NombreUsuario
		Usuarios.NombreUsuario.SetDbValue(ref RsNew, Usuarios.NombreUsuario.CurrentValue, "", false);

		// Password
		Usuarios.Password.SetDbValue(ref RsNew, Usuarios.Password.CurrentValue, "", false);

		// Correo
		Usuarios.Correo.SetDbValue(ref RsNew, Usuarios.Correo.CurrentValue, System.DBNull.Value, false);

		// IdUsuarioNivel
		if (Security.CanAdmin) { // System admin
		Usuarios.IdUsuarioNivel.SetDbValue(ref RsNew, Usuarios.IdUsuarioNivel.CurrentValue, 0, false);
		}

		// Activo
		Usuarios.Activo.SetDbValue(ref RsNew, (Usuarios.Activo.CurrentValue != "" && !Convert.IsDBNull(Usuarios.Activo.CurrentValue)), false, false);

		// IdUsuario
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = Usuarios.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				Usuarios.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(Usuarios.CancelMessage)) {
				FailureMessage = Usuarios.CancelMessage;
				Usuarios.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			Usuarios.IdUsuario.DbValue = Conn.GetLastInsertId();
			RsNew["IdUsuario"] = Usuarios.IdUsuario.DbValue;
		}
		if (result) {

			// Row Inserted event
			Usuarios.Row_Inserted(RsOld, RsNew);
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
		Usuarios_add = new cUsuarios_add(this);
		CurrentPage = Usuarios_add;

		//CurrentPageType = Usuarios_add.GetType();
		Usuarios_add.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Usuarios_add.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (Usuarios_add != null)
			Usuarios_add.Dispose();
	}
}
