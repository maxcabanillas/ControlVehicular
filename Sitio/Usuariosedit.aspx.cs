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

partial class Usuariosedit: AspNetMaker9_ControlVehicular {

	// Page object
	public cUsuarios_edit Usuarios_edit;	

	//
	// Page Class
	//
	public class cUsuarios_edit: AspNetMakerPage, IDisposable {

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
		public Usuariosedit AspNetPage { 
			get { return (Usuariosedit)m_ParentPage; }
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
		public cUsuarios_edit(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "edit";
			m_PageObjName = "Usuarios_edit";
			m_PageObjTypeName = "cUsuarios_edit";

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
			if (!Security.CanEdit) {
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

	public string DbMasterFilter = "";

	public string DbDetailFilter = ""; 

	//
	// Page main processing
	//
	public void Page_Main()
	{

		// Load key from QueryString
		if (ew_NotEmpty(ew_Get("IdUsuario"))) {
			Usuarios.IdUsuario.QueryStringValue = ew_Get("IdUsuario");
		}
		if (ew_NotEmpty(ObjForm.GetValue("a_edit"))) {
			Usuarios.CurrentAction = ObjForm.GetValue("a_edit"); // Get action code
			LoadFormValues(); // Get form values

			// Validate Form
			if (!ValidateForm()) {
				Usuarios.CurrentAction = ""; // Form error, reset action
				FailureMessage = gsFormError;
				Usuarios.EventCancelled = true; // Event cancelled 
				RestoreFormValues(); // Restore form values if validate failed
			}
		} else {
			Usuarios.CurrentAction = "I"; // Default action is display
		}

		// Check if valid key
		if (ew_Empty(Usuarios.IdUsuario.CurrentValue)) Page_Terminate("Usuarioslist.aspx"); // Invalid key, return to list
		switch (Usuarios.CurrentAction) {
			case "I": // Get a record to display
				if (!LoadRow()) { // Load Record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("Usuarioslist.aspx"); // No matching record, return to list
				}
				break;
			case "U": // Update
				Usuarios.SendEmail = true; // Send email on update success
				if (EditRow()) { // Update Record based on key
					SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success
					string sReturnUrl;
					sReturnUrl = Usuarios.ReturnUrl;
					Page_Terminate(sReturnUrl); // Return to caller
				} else {
					Usuarios.EventCancelled = true;
					RestoreFormValues(); // Restore form values if update failed
				}
				break;
		}

		// Render the record
		Usuarios.RowType = EW_ROWTYPE_EDIT; // Render as edit

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
	// Load form values
	//
	public void LoadFormValues() {
		if (!Usuarios.IdUsuario.FldIsDetailKey)
			Usuarios.IdUsuario.FormValue = ObjForm.GetValue("x_IdUsuario");
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
		LoadRow();
		Usuarios.IdUsuario.CurrentValue = Usuarios.IdUsuario.FormValue;
		Usuarios.Usuario.CurrentValue = Usuarios.Usuario.FormValue;
		Usuarios.NombreUsuario.CurrentValue = Usuarios.NombreUsuario.FormValue;
		Usuarios.Password.CurrentValue = Usuarios.Password.FormValue;
		Usuarios.Correo.CurrentValue = Usuarios.Correo.FormValue;
		Usuarios.IdUsuarioNivel.CurrentValue = Usuarios.IdUsuarioNivel.FormValue;
		Usuarios.Activo.CurrentValue = Usuarios.Activo.FormValue;
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

		//
		//  Edit Row
		//

		} else if (Usuarios.RowType == EW_ROWTYPE_EDIT) { // Edit row

			// IdUsuario
			Usuarios.IdUsuario.EditCustomAttributes = "";
				Usuarios.IdUsuario.EditValue = Usuarios.IdUsuario.CurrentValue;
			Usuarios.IdUsuario.ViewCustomAttributes = "";

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
			// IdUsuario

			Usuarios.IdUsuario.HrefValue = "";

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
		string sFilter = Usuarios.KeyFilter;
		Usuarios.CurrentFilter = sFilter;
		sSql = Usuarios.SQL;
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
				// IdUsuario
				// Usuario

				Usuarios.Usuario.SetDbValue(ref RsNew, Usuarios.Usuario.CurrentValue, "", Usuarios.Usuario.ReadOnly);

				// NombreUsuario
				Usuarios.NombreUsuario.SetDbValue(ref RsNew, Usuarios.NombreUsuario.CurrentValue, "", Usuarios.NombreUsuario.ReadOnly);

				// Password
				Usuarios.Password.SetDbValue(ref RsNew, Usuarios.Password.CurrentValue, "", Usuarios.Password.ReadOnly || (EW_ENCRYPTED_PASSWORD && RsNew["Password"] == Usuarios.Password.CurrentValue));

				// Correo
				Usuarios.Correo.SetDbValue(ref RsNew, Usuarios.Correo.CurrentValue, System.DBNull.Value, Usuarios.Correo.ReadOnly);

				// IdUsuarioNivel
				if (Security.CanAdmin) { // System admin
				Usuarios.IdUsuarioNivel.SetDbValue(ref RsNew, Usuarios.IdUsuarioNivel.CurrentValue, 0, Usuarios.IdUsuarioNivel.ReadOnly);
				}

				// Activo
				Usuarios.Activo.SetDbValue(ref RsNew, (Usuarios.Activo.CurrentValue != "" && !Convert.IsDBNull(Usuarios.Activo.CurrentValue)), false, Usuarios.Activo.ReadOnly);
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;
				RsEdit.Close();
				return false;
			}
			RsEdit.Close();

			// Row Updating event
			bUpdateRow = Usuarios.Row_Updating(RsOld, ref RsNew);
			if (bUpdateRow) {
				try {
					if (RsNew.Count > 0)
						result = Usuarios.Update(ref RsNew) > 0;
					else
						result = true; // No field to update
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message;
					return false;
				}
			}	else {
				if (ew_NotEmpty(Usuarios.CancelMessage)) {
					FailureMessage = Usuarios.CancelMessage;
					Usuarios.CancelMessage = "";
				} else {
					FailureMessage = Language.Phrase("UpdateCancelled");
				}
				result = false;
			}
		}

		// Row Updated event
		if (result)
			Usuarios.Row_Updated(RsOld, RsNew);
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
		Usuarios_edit = new cUsuarios_edit(this);
		CurrentPage = Usuarios_edit;

		//CurrentPageType = Usuarios_edit.GetType();
		Usuarios_edit.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Usuarios_edit.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (Usuarios_edit != null)
			Usuarios_edit.Dispose();
	}
}
