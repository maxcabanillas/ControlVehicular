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

partial class RegistrosPeatonesedit: AspNetMaker9_ControlVehicular {

	// Page object
	public cRegistrosPeatones_edit RegistrosPeatones_edit;	

	//
	// Page Class
	//
	public class cRegistrosPeatones_edit: AspNetMakerPage, IDisposable {

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
				if (RegistrosPeatones.UseTokenInUrl)
					Url += "t=" + RegistrosPeatones.TableVar + "&"; // Add page token
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
			if (RegistrosPeatones.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (RegistrosPeatones.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (RegistrosPeatones.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public RegistrosPeatonesedit AspNetPage { 
			get { return (RegistrosPeatonesedit)m_ParentPage; }
		}

		// RegistrosPeatones	
		public cRegistrosPeatones RegistrosPeatones { 
			get {				
				return ParentPage.RegistrosPeatones;
			}
			set {
				ParentPage.RegistrosPeatones = value;	
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
		public cRegistrosPeatones_edit(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "edit";
			m_PageObjName = "RegistrosPeatones_edit";
			m_PageObjTypeName = "cRegistrosPeatones_edit";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (RegistrosPeatones == null)
				RegistrosPeatones = new cRegistrosPeatones(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "RegistrosPeatones";
			m_Table = RegistrosPeatones;
			CurrentTable = RegistrosPeatones;

			//CurrentTableType = RegistrosPeatones.GetType();
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
				Page_Terminate("RegistrosPeatoneslist.aspx");
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
			RegistrosPeatones.Dispose();
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
		if (ew_NotEmpty(ew_Get("IdRegistroPeaton"))) {
			RegistrosPeatones.IdRegistroPeaton.QueryStringValue = ew_Get("IdRegistroPeaton");
		}
		if (ew_NotEmpty(ObjForm.GetValue("a_edit"))) {
			RegistrosPeatones.CurrentAction = ObjForm.GetValue("a_edit"); // Get action code
			LoadFormValues(); // Get form values

			// Validate Form
			if (!ValidateForm()) {
				RegistrosPeatones.CurrentAction = ""; // Form error, reset action
				FailureMessage = gsFormError;
				RegistrosPeatones.EventCancelled = true; // Event cancelled 
				RestoreFormValues(); // Restore form values if validate failed
			}
		} else {
			RegistrosPeatones.CurrentAction = "I"; // Default action is display
		}

		// Check if valid key
		if (ew_Empty(RegistrosPeatones.IdRegistroPeaton.CurrentValue)) Page_Terminate("RegistrosPeatoneslist.aspx"); // Invalid key, return to list
		switch (RegistrosPeatones.CurrentAction) {
			case "I": // Get a record to display
				if (!LoadRow()) { // Load Record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("RegistrosPeatoneslist.aspx"); // No matching record, return to list
				}
				break;
			case "U": // Update
				RegistrosPeatones.SendEmail = true; // Send email on update success
				if (EditRow()) { // Update Record based on key
					SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success
					string sReturnUrl;
					sReturnUrl = RegistrosPeatones.ReturnUrl;
					Page_Terminate(sReturnUrl); // Return to caller
				} else {
					RegistrosPeatones.EventCancelled = true;
					RestoreFormValues(); // Restore form values if update failed
				}
				break;
		}

		// Render the record
		RegistrosPeatones.RowType = EW_ROWTYPE_EDIT; // Render as edit

		// Render row
		RegistrosPeatones.ResetAttrs();
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
		if (!RegistrosPeatones.IdRegistroPeaton.FldIsDetailKey)
			RegistrosPeatones.IdRegistroPeaton.FormValue = ObjForm.GetValue("x_IdRegistroPeaton");
		if (!RegistrosPeatones.IdTipoDocumento.FldIsDetailKey) {
			RegistrosPeatones.IdTipoDocumento.FormValue = ObjForm.GetValue("x_IdTipoDocumento");
		}
		if (!RegistrosPeatones.Documento.FldIsDetailKey) {
			RegistrosPeatones.Documento.FormValue = ObjForm.GetValue("x_Documento");
		}
		if (!RegistrosPeatones.Nombre.FldIsDetailKey) {
			RegistrosPeatones.Nombre.FormValue = ObjForm.GetValue("x_Nombre");
		}
		if (!RegistrosPeatones.Apellidos.FldIsDetailKey) {
			RegistrosPeatones.Apellidos.FormValue = ObjForm.GetValue("x_Apellidos");
		}
		if (!RegistrosPeatones.IdArea.FldIsDetailKey) {
			RegistrosPeatones.IdArea.FormValue = ObjForm.GetValue("x_IdArea");
		}
		if (!RegistrosPeatones.IdPersona.FldIsDetailKey) {
			RegistrosPeatones.IdPersona.FormValue = ObjForm.GetValue("x_IdPersona");
		}
		if (!RegistrosPeatones.FechaIngreso.FldIsDetailKey) {
			RegistrosPeatones.FechaIngreso.FormValue = ObjForm.GetValue("x_FechaIngreso");
			if (ObjForm.HasValue("x_FechaIngreso")) 
				RegistrosPeatones.FechaIngreso.CurrentValue = ew_UnformatDateTime(RegistrosPeatones.FechaIngreso.CurrentValue, 7);	
		}
		if (!RegistrosPeatones.FechaSalida.FldIsDetailKey) {
			RegistrosPeatones.FechaSalida.FormValue = ObjForm.GetValue("x_FechaSalida");
			if (ObjForm.HasValue("x_FechaSalida")) 
				RegistrosPeatones.FechaSalida.CurrentValue = ew_UnformatDateTime(RegistrosPeatones.FechaSalida.CurrentValue, 7);	
		}
		if (!RegistrosPeatones.Observacion.FldIsDetailKey) {
			RegistrosPeatones.Observacion.FormValue = ObjForm.GetValue("x_Observacion");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadRow();
		RegistrosPeatones.IdRegistroPeaton.CurrentValue = RegistrosPeatones.IdRegistroPeaton.FormValue;
		RegistrosPeatones.IdTipoDocumento.CurrentValue = RegistrosPeatones.IdTipoDocumento.FormValue;
		RegistrosPeatones.Documento.CurrentValue = RegistrosPeatones.Documento.FormValue;
		RegistrosPeatones.Nombre.CurrentValue = RegistrosPeatones.Nombre.FormValue;
		RegistrosPeatones.Apellidos.CurrentValue = RegistrosPeatones.Apellidos.FormValue;
		RegistrosPeatones.IdArea.CurrentValue = RegistrosPeatones.IdArea.FormValue;
		RegistrosPeatones.IdPersona.CurrentValue = RegistrosPeatones.IdPersona.FormValue;
		RegistrosPeatones.FechaIngreso.CurrentValue = RegistrosPeatones.FechaIngreso.FormValue;
		RegistrosPeatones.FechaIngreso.CurrentValue = ew_UnformatDateTime(RegistrosPeatones.FechaIngreso.CurrentValue, 7);
		RegistrosPeatones.FechaSalida.CurrentValue = RegistrosPeatones.FechaSalida.FormValue;
		RegistrosPeatones.FechaSalida.CurrentValue = ew_UnformatDateTime(RegistrosPeatones.FechaSalida.CurrentValue, 7);
		RegistrosPeatones.Observacion.CurrentValue = RegistrosPeatones.Observacion.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = RegistrosPeatones.KeyFilter;

		// Row Selecting event
		RegistrosPeatones.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		RegistrosPeatones.CurrentFilter = sFilter;
		string sSql = RegistrosPeatones.SQL;

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
		RegistrosPeatones.Row_Selected(ref row);
		RegistrosPeatones.IdRegistroPeaton.DbValue = row["IdRegistroPeaton"];
		RegistrosPeatones.IdTipoDocumento.DbValue = row["IdTipoDocumento"];
		RegistrosPeatones.Documento.DbValue = row["Documento"];
		RegistrosPeatones.Nombre.DbValue = row["Nombre"];
		RegistrosPeatones.Apellidos.DbValue = row["Apellidos"];
		RegistrosPeatones.IdArea.DbValue = row["IdArea"];
		RegistrosPeatones.IdPersona.DbValue = row["IdPersona"];
		if (row.Contains("EV__IdPersona")) {
			RegistrosPeatones.IdPersona.VirtualValue = row["EV__IdPersona"]; // Set up virtual field value
		} else {
			RegistrosPeatones.IdPersona.VirtualValue = ""; // Clear value
		}
		RegistrosPeatones.FechaIngreso.DbValue = row["FechaIngreso"];
		RegistrosPeatones.FechaSalida.DbValue = row["FechaSalida"];
		RegistrosPeatones.Observacion.DbValue = row["Observacion"];
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
		// Row Rendering event

		RegistrosPeatones.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdRegistroPeaton
		// IdTipoDocumento
		// Documento
		// Nombre
		// Apellidos
		// IdArea
		// IdPersona
		// FechaIngreso
		// FechaSalida
		// Observacion
		//
		//  View  Row
		//

		if (RegistrosPeatones.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdRegistroPeaton
				RegistrosPeatones.IdRegistroPeaton.ViewValue = RegistrosPeatones.IdRegistroPeaton.CurrentValue;
			RegistrosPeatones.IdRegistroPeaton.ViewCustomAttributes = "";

			// IdTipoDocumento
			if (ew_NotEmpty(RegistrosPeatones.IdTipoDocumento.CurrentValue)) {
				sFilterWrk = "[IdTipoDocumento] = " + ew_AdjustSql(RegistrosPeatones.IdTipoDocumento.CurrentValue) + "";
			sSqlWrk = "SELECT [TipoDocumento] FROM [dbo].[TiposDocumentos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoDocumento]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					RegistrosPeatones.IdTipoDocumento.ViewValue = drWrk["TipoDocumento"];
				} else {
					RegistrosPeatones.IdTipoDocumento.ViewValue = RegistrosPeatones.IdTipoDocumento.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosPeatones.IdTipoDocumento.ViewValue = System.DBNull.Value;
			}
			RegistrosPeatones.IdTipoDocumento.ViewCustomAttributes = "";

			// Documento
				RegistrosPeatones.Documento.ViewValue = RegistrosPeatones.Documento.CurrentValue;
			RegistrosPeatones.Documento.ViewCustomAttributes = "";

			// Nombre
				RegistrosPeatones.Nombre.ViewValue = RegistrosPeatones.Nombre.CurrentValue;
			RegistrosPeatones.Nombre.ViewCustomAttributes = "";

			// Apellidos
				RegistrosPeatones.Apellidos.ViewValue = RegistrosPeatones.Apellidos.CurrentValue;
			RegistrosPeatones.Apellidos.ViewCustomAttributes = "";

			// IdArea
			if (ew_NotEmpty(RegistrosPeatones.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(RegistrosPeatones.IdArea.CurrentValue) + "";
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
					RegistrosPeatones.IdArea.ViewValue = drWrk["Area"];
					RegistrosPeatones.IdArea.ViewValue = String.Concat(RegistrosPeatones.IdArea.ViewValue, ew_ValueSeparator(0, 1, RegistrosPeatones.IdArea), drWrk["Codigo"]);
				} else {
					RegistrosPeatones.IdArea.ViewValue = RegistrosPeatones.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosPeatones.IdArea.ViewValue = System.DBNull.Value;
			}
			RegistrosPeatones.IdArea.ViewCustomAttributes = "";

			// IdPersona
			if (ew_NotEmpty(RegistrosPeatones.IdPersona.VirtualValue)) {
				RegistrosPeatones.IdPersona.ViewValue = RegistrosPeatones.IdPersona.VirtualValue;
			} else {
			if (ew_NotEmpty(RegistrosPeatones.IdPersona.CurrentValue)) {
				sFilterWrk = "[IdPersona] = " + ew_AdjustSql(RegistrosPeatones.IdPersona.CurrentValue) + "";
			sSqlWrk = "SELECT [IdPersona], [Persona], [Documento], [Activa] FROM [dbo].[Personas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Persona]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					RegistrosPeatones.IdPersona.ViewValue = drWrk["IdPersona"];
					RegistrosPeatones.IdPersona.ViewValue = String.Concat(RegistrosPeatones.IdPersona.ViewValue, ew_ValueSeparator(0, 1, RegistrosPeatones.IdPersona), drWrk["Persona"]);
					RegistrosPeatones.IdPersona.ViewValue = String.Concat(RegistrosPeatones.IdPersona.ViewValue, ew_ValueSeparator(0, 2, RegistrosPeatones.IdPersona), drWrk["Documento"]);
					RegistrosPeatones.IdPersona.ViewValue = String.Concat(RegistrosPeatones.IdPersona.ViewValue, ew_ValueSeparator(0, 3, RegistrosPeatones.IdPersona), drWrk["Activa"]);
				} else {
					RegistrosPeatones.IdPersona.ViewValue = RegistrosPeatones.IdPersona.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosPeatones.IdPersona.ViewValue = System.DBNull.Value;
			}
			}
			RegistrosPeatones.IdPersona.ViewCustomAttributes = "";

			// FechaIngreso
				RegistrosPeatones.FechaIngreso.ViewValue = RegistrosPeatones.FechaIngreso.CurrentValue;
				RegistrosPeatones.FechaIngreso.ViewValue = ew_FormatDateTime(RegistrosPeatones.FechaIngreso.ViewValue, 7);
			RegistrosPeatones.FechaIngreso.ViewCustomAttributes = "";

			// FechaSalida
				RegistrosPeatones.FechaSalida.ViewValue = RegistrosPeatones.FechaSalida.CurrentValue;
				RegistrosPeatones.FechaSalida.ViewValue = ew_FormatDateTime(RegistrosPeatones.FechaSalida.ViewValue, 7);
			RegistrosPeatones.FechaSalida.ViewCustomAttributes = "";

			// Observacion
			RegistrosPeatones.Observacion.ViewValue = RegistrosPeatones.Observacion.CurrentValue;
			RegistrosPeatones.Observacion.ViewCustomAttributes = "";

			// View refer script
			// IdRegistroPeaton

			RegistrosPeatones.IdRegistroPeaton.LinkCustomAttributes = "";
			RegistrosPeatones.IdRegistroPeaton.HrefValue = "";
			RegistrosPeatones.IdRegistroPeaton.TooltipValue = "";

			// IdTipoDocumento
			RegistrosPeatones.IdTipoDocumento.LinkCustomAttributes = "";
			RegistrosPeatones.IdTipoDocumento.HrefValue = "";
			RegistrosPeatones.IdTipoDocumento.TooltipValue = "";

			// Documento
			RegistrosPeatones.Documento.LinkCustomAttributes = "";
			RegistrosPeatones.Documento.HrefValue = "";
			RegistrosPeatones.Documento.TooltipValue = "";

			// Nombre
			RegistrosPeatones.Nombre.LinkCustomAttributes = "";
			RegistrosPeatones.Nombre.HrefValue = "";
			RegistrosPeatones.Nombre.TooltipValue = "";

			// Apellidos
			RegistrosPeatones.Apellidos.LinkCustomAttributes = "";
			RegistrosPeatones.Apellidos.HrefValue = "";
			RegistrosPeatones.Apellidos.TooltipValue = "";

			// IdArea
			RegistrosPeatones.IdArea.LinkCustomAttributes = "";
			RegistrosPeatones.IdArea.HrefValue = "";
			RegistrosPeatones.IdArea.TooltipValue = "";

			// IdPersona
			RegistrosPeatones.IdPersona.LinkCustomAttributes = "";
			RegistrosPeatones.IdPersona.HrefValue = "";
			RegistrosPeatones.IdPersona.TooltipValue = "";

			// FechaIngreso
			RegistrosPeatones.FechaIngreso.LinkCustomAttributes = "";
			RegistrosPeatones.FechaIngreso.HrefValue = "";
			RegistrosPeatones.FechaIngreso.TooltipValue = "";

			// FechaSalida
			RegistrosPeatones.FechaSalida.LinkCustomAttributes = "";
			RegistrosPeatones.FechaSalida.HrefValue = "";
			RegistrosPeatones.FechaSalida.TooltipValue = "";

			// Observacion
			RegistrosPeatones.Observacion.LinkCustomAttributes = "";
			RegistrosPeatones.Observacion.HrefValue = "";
			RegistrosPeatones.Observacion.TooltipValue = "";

		//
		//  Edit Row
		//

		} else if (RegistrosPeatones.RowType == EW_ROWTYPE_EDIT) { // Edit row

			// IdRegistroPeaton
			RegistrosPeatones.IdRegistroPeaton.EditCustomAttributes = "";
				RegistrosPeatones.IdRegistroPeaton.EditValue = RegistrosPeatones.IdRegistroPeaton.CurrentValue;
			RegistrosPeatones.IdRegistroPeaton.ViewCustomAttributes = "";

			// IdTipoDocumento
			RegistrosPeatones.IdTipoDocumento.EditCustomAttributes = "";
			sFilterWrk = "";
			sSqlWrk = "SELECT [IdTipoDocumento], [TipoDocumento] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[TiposDocumentos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoDocumento]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			RegistrosPeatones.IdTipoDocumento.EditValue = alwrk;

			// Documento
			RegistrosPeatones.Documento.EditCustomAttributes = "";
			RegistrosPeatones.Documento.EditValue = ew_HtmlEncode(RegistrosPeatones.Documento.CurrentValue);

			// Nombre
			RegistrosPeatones.Nombre.EditCustomAttributes = "";
			RegistrosPeatones.Nombre.EditValue = ew_HtmlEncode(RegistrosPeatones.Nombre.CurrentValue);

			// Apellidos
			RegistrosPeatones.Apellidos.EditCustomAttributes = "";
			RegistrosPeatones.Apellidos.EditValue = ew_HtmlEncode(RegistrosPeatones.Apellidos.CurrentValue);

			// IdArea
			RegistrosPeatones.IdArea.EditCustomAttributes = "";
			if (ew_NotEmpty(RegistrosPeatones.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(RegistrosPeatones.IdArea.CurrentValue) + "";
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
					RegistrosPeatones.IdArea.EditValue = drWrk["Area"];
					RegistrosPeatones.IdArea.EditValue = String.Concat(RegistrosPeatones.IdArea.EditValue, ew_ValueSeparator(0, 1, RegistrosPeatones.IdArea), drWrk["Codigo"]);
				} else {
					RegistrosPeatones.IdArea.EditValue = RegistrosPeatones.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosPeatones.IdArea.EditValue = System.DBNull.Value;
			}
			RegistrosPeatones.IdArea.ViewCustomAttributes = "";

			// IdPersona
			RegistrosPeatones.IdPersona.EditCustomAttributes = "";
			if (ew_NotEmpty(RegistrosPeatones.IdPersona.VirtualValue)) {
				RegistrosPeatones.IdPersona.ViewValue = RegistrosPeatones.IdPersona.VirtualValue;
			} else {
			if (ew_NotEmpty(RegistrosPeatones.IdPersona.CurrentValue)) {
				sFilterWrk = "[IdPersona] = " + ew_AdjustSql(RegistrosPeatones.IdPersona.CurrentValue) + "";
			sSqlWrk = "SELECT [IdPersona], [Persona], [Documento], [Activa] FROM [dbo].[Personas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Persona]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					RegistrosPeatones.IdPersona.EditValue = drWrk["IdPersona"];
					RegistrosPeatones.IdPersona.EditValue = String.Concat(RegistrosPeatones.IdPersona.EditValue, ew_ValueSeparator(0, 1, RegistrosPeatones.IdPersona), drWrk["Persona"]);
					RegistrosPeatones.IdPersona.EditValue = String.Concat(RegistrosPeatones.IdPersona.EditValue, ew_ValueSeparator(0, 2, RegistrosPeatones.IdPersona), drWrk["Documento"]);
					RegistrosPeatones.IdPersona.EditValue = String.Concat(RegistrosPeatones.IdPersona.EditValue, ew_ValueSeparator(0, 3, RegistrosPeatones.IdPersona), drWrk["Activa"]);
				} else {
					RegistrosPeatones.IdPersona.EditValue = RegistrosPeatones.IdPersona.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosPeatones.IdPersona.EditValue = System.DBNull.Value;
			}
			}
			RegistrosPeatones.IdPersona.ViewCustomAttributes = "";

			// FechaIngreso
			RegistrosPeatones.FechaIngreso.EditCustomAttributes = "";
				RegistrosPeatones.FechaIngreso.EditValue = RegistrosPeatones.FechaIngreso.CurrentValue;
				RegistrosPeatones.FechaIngreso.EditValue = ew_FormatDateTime(RegistrosPeatones.FechaIngreso.EditValue, 7);
			RegistrosPeatones.FechaIngreso.ViewCustomAttributes = "";

			// FechaSalida
			RegistrosPeatones.FechaSalida.EditCustomAttributes = "";
				RegistrosPeatones.FechaSalida.EditValue = RegistrosPeatones.FechaSalida.CurrentValue;
				RegistrosPeatones.FechaSalida.EditValue = ew_FormatDateTime(RegistrosPeatones.FechaSalida.EditValue, 7);
			RegistrosPeatones.FechaSalida.ViewCustomAttributes = "";

			// Observacion
			RegistrosPeatones.Observacion.EditCustomAttributes = "";
			RegistrosPeatones.Observacion.EditValue = ew_HtmlEncode(RegistrosPeatones.Observacion.CurrentValue);

			// Edit refer script
			// IdRegistroPeaton

			RegistrosPeatones.IdRegistroPeaton.HrefValue = "";

			// IdTipoDocumento
			RegistrosPeatones.IdTipoDocumento.HrefValue = "";

			// Documento
			RegistrosPeatones.Documento.HrefValue = "";

			// Nombre
			RegistrosPeatones.Nombre.HrefValue = "";

			// Apellidos
			RegistrosPeatones.Apellidos.HrefValue = "";

			// IdArea
			RegistrosPeatones.IdArea.HrefValue = "";

			// IdPersona
			RegistrosPeatones.IdPersona.HrefValue = "";

			// FechaIngreso
			RegistrosPeatones.FechaIngreso.HrefValue = "";

			// FechaSalida
			RegistrosPeatones.FechaSalida.HrefValue = "";

			// Observacion
			RegistrosPeatones.Observacion.HrefValue = "";
		}
		if (RegistrosPeatones.RowType == EW_ROWTYPE_ADD ||
			RegistrosPeatones.RowType == EW_ROWTYPE_EDIT ||
			RegistrosPeatones.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			RegistrosPeatones.SetupFieldTitles();
		}

		// Row Rendered event
		if (RegistrosPeatones.RowType != EW_ROWTYPE_AGGREGATEINIT)
			RegistrosPeatones.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(RegistrosPeatones.IdTipoDocumento.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + RegistrosPeatones.IdTipoDocumento.FldCaption);
		if (ew_Empty(RegistrosPeatones.Documento.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + RegistrosPeatones.Documento.FldCaption);
		if (ew_Empty(RegistrosPeatones.Nombre.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + RegistrosPeatones.Nombre.FldCaption);
		if (ew_Empty(RegistrosPeatones.Apellidos.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + RegistrosPeatones.Apellidos.FldCaption);

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
		string sFilter = RegistrosPeatones.KeyFilter;
		RegistrosPeatones.CurrentFilter = sFilter;
		sSql = RegistrosPeatones.SQL;
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
				// IdRegistroPeaton
				// IdTipoDocumento

				RegistrosPeatones.IdTipoDocumento.SetDbValue(ref RsNew, RegistrosPeatones.IdTipoDocumento.CurrentValue, 0, RegistrosPeatones.IdTipoDocumento.ReadOnly);

				// Documento
				RegistrosPeatones.Documento.SetDbValue(ref RsNew, RegistrosPeatones.Documento.CurrentValue, "", RegistrosPeatones.Documento.ReadOnly);

				// Nombre
				RegistrosPeatones.Nombre.SetDbValue(ref RsNew, RegistrosPeatones.Nombre.CurrentValue, "", RegistrosPeatones.Nombre.ReadOnly);

				// Apellidos
				RegistrosPeatones.Apellidos.SetDbValue(ref RsNew, RegistrosPeatones.Apellidos.CurrentValue, "", RegistrosPeatones.Apellidos.ReadOnly);

				// Observacion
				RegistrosPeatones.Observacion.SetDbValue(ref RsNew, RegistrosPeatones.Observacion.CurrentValue, System.DBNull.Value, RegistrosPeatones.Observacion.ReadOnly);
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;
				RsEdit.Close();
				return false;
			}
			RsEdit.Close();

			// Row Updating event
			bUpdateRow = RegistrosPeatones.Row_Updating(RsOld, ref RsNew);
			if (bUpdateRow) {
				try {
					if (RsNew.Count > 0)
						result = RegistrosPeatones.Update(ref RsNew) > 0;
					else
						result = true; // No field to update
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message;
					return false;
				}
			}	else {
				if (ew_NotEmpty(RegistrosPeatones.CancelMessage)) {
					FailureMessage = RegistrosPeatones.CancelMessage;
					RegistrosPeatones.CancelMessage = "";
				} else {
					FailureMessage = Language.Phrase("UpdateCancelled");
				}
				result = false;
			}
		}

		// Row Updated event
		if (result)
			RegistrosPeatones.Row_Updated(RsOld, RsNew);
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
		RegistrosPeatones_edit = new cRegistrosPeatones_edit(this);
		CurrentPage = RegistrosPeatones_edit;

		//CurrentPageType = RegistrosPeatones_edit.GetType();
		RegistrosPeatones_edit.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		RegistrosPeatones_edit.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (RegistrosPeatones_edit != null)
			RegistrosPeatones_edit.Dispose();
	}
}
