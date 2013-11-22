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

partial class Personasedit: AspNetMaker9_ControlVehicular {

	// Page object
	public cPersonas_edit Personas_edit;	

	//
	// Page Class
	//
	public class cPersonas_edit: AspNetMakerPage, IDisposable {

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
				if (Personas.UseTokenInUrl)
					Url += "t=" + Personas.TableVar + "&"; // Add page token
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
			if (Personas.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (Personas.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (Personas.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public Personasedit AspNetPage { 
			get { return (Personasedit)m_ParentPage; }
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

		// Areas	
		public cAreas Areas { 
			get {				
				return ParentPage.Areas;
			}
			set {
				ParentPage.Areas = value;	
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

		// VehiculosAutorizados_grid	
		public cVehiculosAutorizados_grid VehiculosAutorizados_grid { 
			get {				
				return ParentPage.VehiculosAutorizados_grid;
			}
			set {
				ParentPage.VehiculosAutorizados_grid = value;	
			}	
		}		

		// VehiculosAutorizados	
		public cVehiculosAutorizados VehiculosAutorizados { 
			get {				
				return ParentPage.VehiculosAutorizados;
			}
			set {
				ParentPage.VehiculosAutorizados = value;	
			}	
		}		

		//
		//  Page class constructor
		//
		public cPersonas_edit(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "edit";
			m_PageObjName = "Personas_edit";
			m_PageObjTypeName = "cPersonas_edit";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (Personas == null)
				Personas = new cPersonas(this);
			if (Areas == null)
				Areas = new cAreas(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);
			if (VehiculosAutorizados_grid == null)
				VehiculosAutorizados_grid = new cVehiculosAutorizados_grid(ParentPage);
			if (VehiculosAutorizados == null)
				VehiculosAutorizados = new cVehiculosAutorizados(this);

			// Table
			m_TableName = "Personas";
			m_Table = Personas;
			CurrentTable = Personas;

			//CurrentTableType = Personas.GetType();
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
				Page_Terminate("Personaslist.aspx");
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
			Personas.Dispose();
			Areas.Dispose();
			Usuarios.Dispose();
			VehiculosAutorizados_grid.Dispose();
			VehiculosAutorizados.Dispose();
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
		if (ew_NotEmpty(ew_Get("IdPersona"))) {
			Personas.IdPersona.QueryStringValue = ew_Get("IdPersona");
		}

		// Set up master detail parameters
		SetUpMasterParms();
		if (ew_NotEmpty(ObjForm.GetValue("a_edit"))) {
			Personas.CurrentAction = ObjForm.GetValue("a_edit"); // Get action code
			LoadFormValues(); // Get form values

			// Set up detail parameters
			SetUpDetailParms();

			// Validate Form
			if (!ValidateForm()) {
				Personas.CurrentAction = ""; // Form error, reset action
				FailureMessage = gsFormError;
				Personas.EventCancelled = true; // Event cancelled 
				RestoreFormValues(); // Restore form values if validate failed
			}
		} else {
			Personas.CurrentAction = "I"; // Default action is display
		}

		// Check if valid key
		if (ew_Empty(Personas.IdPersona.CurrentValue)) Page_Terminate("Personaslist.aspx"); // Invalid key, return to list

		// Set up detail parameters
		SetUpDetailParms();
		switch (Personas.CurrentAction) {
			case "I": // Get a record to display
				if (!LoadRow()) { // Load Record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("Personaslist.aspx"); // No matching record, return to list
				}
				break;
			case "U": // Update
				Personas.SendEmail = true; // Send email on update success
				if (EditRow()) { // Update Record based on key
					SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success
					string sReturnUrl;
					if (ew_NotEmpty(Personas.CurrentDetailTable)) // Master/detail edit
						sReturnUrl = Personas.DetailUrl;
					else
						sReturnUrl = Personas.ReturnUrl;
					Page_Terminate(sReturnUrl); // Return to caller
				} else {
					Personas.EventCancelled = true;
					RestoreFormValues(); // Restore form values if update failed
				}
				break;
		}

		// Render the record
		Personas.RowType = EW_ROWTYPE_EDIT; // Render as edit

		// Render row
		Personas.ResetAttrs();
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
		if (!Personas.IdPersona.FldIsDetailKey)
			Personas.IdPersona.FormValue = ObjForm.GetValue("x_IdPersona");
		if (!Personas.IdArea.FldIsDetailKey) {
			Personas.IdArea.FormValue = ObjForm.GetValue("x_IdArea");
		}
		if (!Personas.IdCargo.FldIsDetailKey) {
			Personas.IdCargo.FormValue = ObjForm.GetValue("x_IdCargo");
		}
		if (!Personas.Documento.FldIsDetailKey) {
			Personas.Documento.FormValue = ObjForm.GetValue("x_Documento");
		}
		if (!Personas.Persona.FldIsDetailKey) {
			Personas.Persona.FormValue = ObjForm.GetValue("x_Persona");
		}
		if (!Personas.Activa.FldIsDetailKey) {
			Personas.Activa.FormValue = ObjForm.GetValue("x_Activa");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadRow();
		Personas.IdPersona.CurrentValue = Personas.IdPersona.FormValue;
		Personas.IdArea.CurrentValue = Personas.IdArea.FormValue;
		Personas.IdCargo.CurrentValue = Personas.IdCargo.FormValue;
		Personas.Documento.CurrentValue = Personas.Documento.FormValue;
		Personas.Persona.CurrentValue = Personas.Persona.FormValue;
		Personas.Activa.CurrentValue = Personas.Activa.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = Personas.KeyFilter;

		// Row Selecting event
		Personas.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		Personas.CurrentFilter = sFilter;
		string sSql = Personas.SQL;

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
		Personas.Row_Selected(ref row);
		Personas.IdPersona.DbValue = row["IdPersona"];
		Personas.IdArea.DbValue = row["IdArea"];
		Personas.IdCargo.DbValue = row["IdCargo"];
		Personas.Documento.DbValue = row["Documento"];
		Personas.Persona.DbValue = row["Persona"];
		Personas.Activa.DbValue = (ew_ConvertToBool(row["Activa"])) ? "1" : "0";
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
		// Row Rendering event

		Personas.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdPersona
		// IdArea
		// IdCargo
		// Documento
		// Persona
		// Activa
		//
		//  View  Row
		//

		if (Personas.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdPersona
				Personas.IdPersona.ViewValue = Personas.IdPersona.CurrentValue;
			Personas.IdPersona.ViewCustomAttributes = "";

			// IdArea
			if (ew_NotEmpty(Personas.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(Personas.IdArea.CurrentValue) + "";
			sSqlWrk = "SELECT [Area], [Codigo] FROM [dbo].[Areas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Area]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Personas.IdArea.ViewValue = drWrk["Area"];
					Personas.IdArea.ViewValue = String.Concat(Personas.IdArea.ViewValue, ew_ValueSeparator(0, 1, Personas.IdArea), drWrk["Codigo"]);
				} else {
					Personas.IdArea.ViewValue = Personas.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdArea.ViewValue = System.DBNull.Value;
			}
			Personas.IdArea.ViewCustomAttributes = "";

			// IdCargo
			if (ew_NotEmpty(Personas.IdCargo.CurrentValue)) {
				sFilterWrk = "[IdCargo] = " + ew_AdjustSql(Personas.IdCargo.CurrentValue) + "";
			sSqlWrk = "SELECT [Cargo] FROM [dbo].[Cargos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Cargo]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Personas.IdCargo.ViewValue = drWrk["Cargo"];
				} else {
					Personas.IdCargo.ViewValue = Personas.IdCargo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdCargo.ViewValue = System.DBNull.Value;
			}
			Personas.IdCargo.ViewCustomAttributes = "";

			// Documento
				Personas.Documento.ViewValue = Personas.Documento.CurrentValue;
			Personas.Documento.ViewCustomAttributes = "";

			// Persona
				Personas.Persona.ViewValue = Personas.Persona.CurrentValue;
			Personas.Persona.ViewCustomAttributes = "";

			// Activa
			if (Convert.ToString(Personas.Activa.CurrentValue) == "1") {
				Personas.Activa.ViewValue = (Personas.Activa.FldTagCaption(1) != "") ? Personas.Activa.FldTagCaption(1) : "Y";
			} else {
				Personas.Activa.ViewValue = (Personas.Activa.FldTagCaption(2) != "") ? Personas.Activa.FldTagCaption(2) : "N";
			}
			Personas.Activa.ViewCustomAttributes = "";

			// View refer script
			// IdPersona

			Personas.IdPersona.LinkCustomAttributes = "";
			Personas.IdPersona.HrefValue = "";
			Personas.IdPersona.TooltipValue = "";

			// IdArea
			Personas.IdArea.LinkCustomAttributes = "";
			Personas.IdArea.HrefValue = "";
			Personas.IdArea.TooltipValue = "";

			// IdCargo
			Personas.IdCargo.LinkCustomAttributes = "";
			Personas.IdCargo.HrefValue = "";
			Personas.IdCargo.TooltipValue = "";

			// Documento
			Personas.Documento.LinkCustomAttributes = "";
			Personas.Documento.HrefValue = "";
			Personas.Documento.TooltipValue = "";

			// Persona
			Personas.Persona.LinkCustomAttributes = "";
			Personas.Persona.HrefValue = "";
			Personas.Persona.TooltipValue = "";

			// Activa
			Personas.Activa.LinkCustomAttributes = "";
			Personas.Activa.HrefValue = "";
			Personas.Activa.TooltipValue = "";

		//
		//  Edit Row
		//

		} else if (Personas.RowType == EW_ROWTYPE_EDIT) { // Edit row

			// IdPersona
			Personas.IdPersona.EditCustomAttributes = "";
				Personas.IdPersona.EditValue = Personas.IdPersona.CurrentValue;
			Personas.IdPersona.ViewCustomAttributes = "";

			// IdArea
			Personas.IdArea.EditCustomAttributes = "";
			if (ew_NotEmpty(Personas.IdArea.SessionValue)) {
				Personas.IdArea.CurrentValue = Personas.IdArea.SessionValue;
			if (ew_NotEmpty(Personas.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(Personas.IdArea.CurrentValue) + "";
			sSqlWrk = "SELECT [Area], [Codigo] FROM [dbo].[Areas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Area]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Personas.IdArea.ViewValue = drWrk["Area"];
					Personas.IdArea.ViewValue = String.Concat(Personas.IdArea.ViewValue, ew_ValueSeparator(0, 1, Personas.IdArea), drWrk["Codigo"]);
				} else {
					Personas.IdArea.ViewValue = Personas.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdArea.ViewValue = System.DBNull.Value;
			}
			Personas.IdArea.ViewCustomAttributes = "";
			} else {
			sFilterWrk = "";
			sSqlWrk = "SELECT [IdArea], [Area] AS [DispFld], [Codigo] AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[Areas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Area]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			((OrderedDictionary)alwrk[0]).Add("2", "");
			Personas.IdArea.EditValue = alwrk;
			}

			// IdCargo
			Personas.IdCargo.EditCustomAttributes = "";
			sFilterWrk = "";
			sSqlWrk = "SELECT [IdCargo], [Cargo] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[Cargos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Cargo]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			Personas.IdCargo.EditValue = alwrk;

			// Documento
			Personas.Documento.EditCustomAttributes = "";
			Personas.Documento.EditValue = ew_HtmlEncode(Personas.Documento.CurrentValue);

			// Persona
			Personas.Persona.EditCustomAttributes = "";
			Personas.Persona.EditValue = ew_HtmlEncode(Personas.Persona.CurrentValue);

			// Activa
			Personas.Activa.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(1))) ? Personas.Activa.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(2))) ? Personas.Activa.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			Personas.Activa.EditValue = alwrk;

			// Edit refer script
			// IdPersona

			Personas.IdPersona.HrefValue = "";

			// IdArea
			Personas.IdArea.HrefValue = "";

			// IdCargo
			Personas.IdCargo.HrefValue = "";

			// Documento
			Personas.Documento.HrefValue = "";

			// Persona
			Personas.Persona.HrefValue = "";

			// Activa
			Personas.Activa.HrefValue = "";
		}
		if (Personas.RowType == EW_ROWTYPE_ADD ||
			Personas.RowType == EW_ROWTYPE_EDIT ||
			Personas.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			Personas.SetupFieldTitles();
		}

		// Row Rendered event
		if (Personas.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Personas.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(Personas.IdArea.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.IdArea.FldCaption);
		if (ew_Empty(Personas.IdCargo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.IdCargo.FldCaption);
		if (ew_Empty(Personas.Documento.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.Documento.FldCaption);
		if (ew_Empty(Personas.Persona.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.Persona.FldCaption);
		if (ew_Empty(Personas.Activa.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.Activa.FldCaption);

		// Validate detail grid
		if (Personas.CurrentDetailTable == "VehiculosAutorizados" && VehiculosAutorizados.DetailEdit) {
			VehiculosAutorizados_grid = new cVehiculosAutorizados_grid(ParentPage); // Get detail page object (grid)
			VehiculosAutorizados_grid.ValidateGridForm();
			VehiculosAutorizados_grid.Dispose();
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
		string sFilter = Personas.KeyFilter;
		Personas.CurrentFilter = sFilter;
		sSql = Personas.SQL;
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
			if (ew_NotEmpty(Personas.CurrentDetailTable))
				Conn.BeginTrans();
			try {
				RsOld = Conn.GetRow(ref RsEdit);

			//RsEdit.Close();
				// IdPersona
				// IdArea

				Personas.IdArea.SetDbValue(ref RsNew, Personas.IdArea.CurrentValue, 0, Personas.IdArea.ReadOnly);

				// IdCargo
				Personas.IdCargo.SetDbValue(ref RsNew, Personas.IdCargo.CurrentValue, 0, Personas.IdCargo.ReadOnly);

				// Documento
				Personas.Documento.SetDbValue(ref RsNew, Personas.Documento.CurrentValue, "", Personas.Documento.ReadOnly);

				// Persona
				Personas.Persona.SetDbValue(ref RsNew, Personas.Persona.CurrentValue, "", Personas.Persona.ReadOnly);

				// Activa
				Personas.Activa.SetDbValue(ref RsNew, (Personas.Activa.CurrentValue != "" && !Convert.IsDBNull(Personas.Activa.CurrentValue)), false, Personas.Activa.ReadOnly);
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;
				RsEdit.Close();
				return false;
			}
			RsEdit.Close();

			// Row Updating event
			bUpdateRow = Personas.Row_Updating(RsOld, ref RsNew);
			if (bUpdateRow) {
				try {
					if (RsNew.Count > 0)
						result = Personas.Update(ref RsNew) > 0;
					else
						result = true; // No field to update

					// Update detail records
					if (result) {
						if (Personas.CurrentDetailTable == "VehiculosAutorizados" && VehiculosAutorizados.DetailEdit) {
							ParentPage.VehiculosAutorizados_grid = new cVehiculosAutorizados_grid(ParentPage); // Get detail page object (grid)			
							result = ParentPage.VehiculosAutorizados_grid.GridUpdate();
							ParentPage.VehiculosAutorizados_grid.Dispose();
						}
					}

					// Commit/Rollback transaction
					if (ew_NotEmpty(Personas.CurrentDetailTable)) {
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
				if (ew_NotEmpty(Personas.CancelMessage)) {
					FailureMessage = Personas.CancelMessage;
					Personas.CancelMessage = "";
				} else {
					FailureMessage = Language.Phrase("UpdateCancelled");
				}
				result = false;
			}
		}

		// Row Updated event
		if (result)
			Personas.Row_Updated(RsOld, RsNew);
		return result;
	}

	// Set up master/detail based on QueryString
	public void SetUpMasterParms() {
		bool bValidMaster = false;
		string sMasterTblVar = "";

		// Get the keys for master table
		if (ew_NotEmpty(ew_Get(EW_TABLE_SHOW_MASTER))) {
			sMasterTblVar = ew_Get(EW_TABLE_SHOW_MASTER);
			if (ew_Empty(sMasterTblVar)) {
				bValidMaster = true;
				DbMasterFilter = "";
				DbDetailFilter = "";
			}
			if (sMasterTblVar == "Areas") {
				bValidMaster = true;				
				if (ew_NotEmpty(ew_Get("IdArea"))) {
					Areas.IdArea.QueryStringValue = ew_Get("IdArea");
					Personas.IdArea.QueryStringValue = Areas.IdArea.QueryStringValue;
					Personas.IdArea.SessionValue = Personas.IdArea.QueryStringValue;
					if (!Information.IsNumeric(Areas.IdArea.QueryStringValue)) bValidMaster = false;
				} else {
					bValidMaster = false;
				}
			}
		}
		if (bValidMaster) {

			// Save current master table
			Personas.CurrentMasterTable = sMasterTblVar;

			// Clear previous master session values
			if (sMasterTblVar != "Areas") {
				if (ew_Empty(Personas.IdArea.QueryStringValue)) Personas.IdArea.SessionValue = "";
			}
		}
		DbMasterFilter = Personas.MasterFilter; // Get master filter
		DbDetailFilter = Personas.DetailFilter; // Get detail filter
	}

	// Set up detail parms based on QueryString
	public void SetUpDetailParms() {
		bool bValidDetail = false;
		string sDetailTblVar = "";

		// Get the keys for master table
		if (HttpContext.Current.Request.QueryString[EW_TABLE_SHOW_DETAIL] != null) {
			sDetailTblVar = ew_Get(EW_TABLE_SHOW_DETAIL);
			Personas.CurrentDetailTable = sDetailTblVar;
		} else {
			sDetailTblVar = Personas.CurrentDetailTable;
		}
		if (ew_NotEmpty(sDetailTblVar)) {
			if (sDetailTblVar == "VehiculosAutorizados") {
				if (VehiculosAutorizados == null)
					VehiculosAutorizados = new cVehiculosAutorizados(this);
				if (VehiculosAutorizados.DetailEdit) {
					VehiculosAutorizados.CurrentMode = "edit";
					VehiculosAutorizados.CurrentAction = "gridedit";

					// Save current master table to detail table
					VehiculosAutorizados.CurrentMasterTable = Personas.TableVar;
					VehiculosAutorizados.StartRecordNumber = 1;
					VehiculosAutorizados.IdPersona.FldIsDetailKey = true;
					VehiculosAutorizados.IdPersona.CurrentValue = Personas.IdPersona.CurrentValue;
					VehiculosAutorizados.IdPersona.SessionValue = VehiculosAutorizados.IdPersona.CurrentValue;
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
		Personas_edit = new cPersonas_edit(this);
		NameValueCollection fv = new NameValueCollection();
		fv.Add(Request.Form);
		Session["EW_FORM_VALUES"] = fv;
		CurrentPage = Personas_edit;

		//CurrentPageType = Personas_edit.GetType();
		Personas_edit.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Personas_edit.Page_Main();
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
		if (Personas_edit != null)
			Personas_edit.Dispose();
	}
}
