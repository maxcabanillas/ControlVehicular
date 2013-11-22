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

partial class RegistrosPeatonesadd: AspNetMaker9_ControlVehicular {

	// Page object
	public cRegistrosPeatones_add RegistrosPeatones_add;	

	//
	// Page Class
	//
	public class cRegistrosPeatones_add: AspNetMakerPage, IDisposable {

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
		public RegistrosPeatonesadd AspNetPage { 
			get { return (RegistrosPeatonesadd)m_ParentPage; }
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
		public cRegistrosPeatones_add(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "add";
			m_PageObjName = "RegistrosPeatones_add";
			m_PageObjTypeName = "cRegistrosPeatones_add";

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
			if (!Security.CanAdd) {
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
			RegistrosPeatones.CurrentAction = ObjForm.GetValue("a_add"); // Get form action
			CopyRecord = LoadOldRecord(); // Load old recordset
			LoadFormValues(); // Load form values

			// Validate Form
			if (!ValidateForm()) {
				RegistrosPeatones.CurrentAction = "I"; // Form error, reset action
				RegistrosPeatones.EventCancelled = true; // Event cancelled
				RestoreFormValues(); // Restore form values
				FailureMessage = gsFormError;
			}

		// Not post back
		} else {

			// Load key values from QueryString
			CopyRecord = true;
			if (ew_NotEmpty(ew_Get("IdRegistroPeaton"))) {
				RegistrosPeatones.IdRegistroPeaton.QueryStringValue = ew_Get("IdRegistroPeaton");
				RegistrosPeatones.SetKey("IdRegistroPeaton", RegistrosPeatones.IdRegistroPeaton.CurrentValue); // Set up key
			} else {
				RegistrosPeatones.SetKey("IdRegistroPeaton", ""); // Clear key
				CopyRecord = false;
			}
			if (CopyRecord) {
				RegistrosPeatones.CurrentAction = "C"; // Copy Record
			} else {
				RegistrosPeatones.CurrentAction = "I"; // Display Blank Record
				LoadDefaultValues(); // Load default values
			}
		}

		// Perform action based on action code
		switch (RegistrosPeatones.CurrentAction) {
			case "I": // Blank record, no action required
				break;
			case "C": // Copy an existing record
				if (!LoadRow()) { // Load record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("RegistrosPeatoneslist.aspx"); // No matching record, return to list
				}
				break;
			case "A": // Add new record
				RegistrosPeatones.SendEmail = true; // Send email on add success
				if (AddRow(Conn.GetRow(ref OldRecordset))) { // Add successful
					SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
					string sReturnUrl;
					sReturnUrl = RegistrosPeatones.ReturnUrl;
					if (ew_GetPageName(sReturnUrl) == "RegistrosPeatonesview.aspx") sReturnUrl = RegistrosPeatones.ViewUrl; // View paging, return to view page with keyurl directly
					Page_Terminate(sReturnUrl); // Clean up and return
				} else {
					RegistrosPeatones.EventCancelled = true; // Event cancelled
					RestoreFormValues(); // Add failed, restore form values
				}
				break;
		}

		// Render row based on row type
		RegistrosPeatones.RowType = EW_ROWTYPE_ADD; // Render add type

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
	// Load default values
	//
	public void LoadDefaultValues() {
		RegistrosPeatones.IdTipoDocumento.CurrentValue = System.DBNull.Value;
		RegistrosPeatones.IdTipoDocumento.OldValue = RegistrosPeatones.IdTipoDocumento.CurrentValue;
		RegistrosPeatones.Documento.CurrentValue = System.DBNull.Value;
		RegistrosPeatones.Documento.OldValue = RegistrosPeatones.Documento.CurrentValue;
		RegistrosPeatones.Nombre.CurrentValue = System.DBNull.Value;
		RegistrosPeatones.Nombre.OldValue = RegistrosPeatones.Nombre.CurrentValue;
		RegistrosPeatones.Apellidos.CurrentValue = System.DBNull.Value;
		RegistrosPeatones.Apellidos.OldValue = RegistrosPeatones.Apellidos.CurrentValue;
		RegistrosPeatones.IdArea.CurrentValue = System.DBNull.Value;
		RegistrosPeatones.IdArea.OldValue = RegistrosPeatones.IdArea.CurrentValue;
		RegistrosPeatones.IdPersona.CurrentValue = System.DBNull.Value;
		RegistrosPeatones.IdPersona.OldValue = RegistrosPeatones.IdPersona.CurrentValue;
		RegistrosPeatones.Observacion.CurrentValue = System.DBNull.Value;
		RegistrosPeatones.Observacion.OldValue = RegistrosPeatones.Observacion.CurrentValue;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
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
		if (!RegistrosPeatones.Observacion.FldIsDetailKey) {
			RegistrosPeatones.Observacion.FormValue = ObjForm.GetValue("x_Observacion");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadOldRecord();
		RegistrosPeatones.IdTipoDocumento.CurrentValue = RegistrosPeatones.IdTipoDocumento.FormValue;
		RegistrosPeatones.Documento.CurrentValue = RegistrosPeatones.Documento.FormValue;
		RegistrosPeatones.Nombre.CurrentValue = RegistrosPeatones.Nombre.FormValue;
		RegistrosPeatones.Apellidos.CurrentValue = RegistrosPeatones.Apellidos.FormValue;
		RegistrosPeatones.IdArea.CurrentValue = RegistrosPeatones.IdArea.FormValue;
		RegistrosPeatones.IdPersona.CurrentValue = RegistrosPeatones.IdPersona.FormValue;
		RegistrosPeatones.Observacion.CurrentValue = RegistrosPeatones.Observacion.FormValue;
		RegistrosPeatones.IdRegistroPeaton.CurrentValue = RegistrosPeatones.IdRegistroPeaton.FormValue;
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

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(RegistrosPeatones.GetKey("IdRegistroPeaton")))
			RegistrosPeatones.IdRegistroPeaton.CurrentValue = RegistrosPeatones.GetKey("IdRegistroPeaton"); // IdRegistroPeaton
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			RegistrosPeatones.CurrentFilter = RegistrosPeatones.KeyFilter;
			string sSql = RegistrosPeatones.SQL;			
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

			// Observacion
			RegistrosPeatones.Observacion.LinkCustomAttributes = "";
			RegistrosPeatones.Observacion.HrefValue = "";
			RegistrosPeatones.Observacion.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (RegistrosPeatones.RowType == EW_ROWTYPE_ADD) { // Add row

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
			RegistrosPeatones.IdArea.EditValue = alwrk;

			// IdPersona
			RegistrosPeatones.IdPersona.EditCustomAttributes = "";
			if (Convert.ToString(RegistrosPeatones.IdPersona.CurrentValue) == "") {
				sFilterWrk = "0=1";
			} else {
				sFilterWrk = "[IdPersona] = " + ew_AdjustSql(RegistrosPeatones.IdPersona.CurrentValue) + "";
			}
			sSqlWrk = "SELECT [IdPersona], [IdPersona] AS [DispFld], [Persona] AS [Disp2Fld], [Documento] AS [Disp3Fld], [Activa] AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[Personas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Persona]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			((OrderedDictionary)alwrk[0]).Add("2", "");
			((OrderedDictionary)alwrk[0]).Add("3",  "");
			((OrderedDictionary)alwrk[0]).Add("4",  "");
			RegistrosPeatones.IdPersona.EditValue = alwrk;

			// Observacion
			RegistrosPeatones.Observacion.EditCustomAttributes = "";
			RegistrosPeatones.Observacion.EditValue = ew_HtmlEncode(RegistrosPeatones.Observacion.CurrentValue);

			// Edit refer script
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
		if (ew_Empty(RegistrosPeatones.IdArea.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + RegistrosPeatones.IdArea.FldCaption);
		if (ew_Empty(RegistrosPeatones.IdPersona.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + RegistrosPeatones.IdPersona.FldCaption);

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

		// IdTipoDocumento
		RegistrosPeatones.IdTipoDocumento.SetDbValue(ref RsNew, RegistrosPeatones.IdTipoDocumento.CurrentValue, 0, false);

		// Documento
		RegistrosPeatones.Documento.SetDbValue(ref RsNew, RegistrosPeatones.Documento.CurrentValue, "", false);

		// Nombre
		RegistrosPeatones.Nombre.SetDbValue(ref RsNew, RegistrosPeatones.Nombre.CurrentValue, "", false);

		// Apellidos
		RegistrosPeatones.Apellidos.SetDbValue(ref RsNew, RegistrosPeatones.Apellidos.CurrentValue, "", false);

		// IdArea
		RegistrosPeatones.IdArea.SetDbValue(ref RsNew, RegistrosPeatones.IdArea.CurrentValue, 0, false);

		// IdPersona
		RegistrosPeatones.IdPersona.SetDbValue(ref RsNew, RegistrosPeatones.IdPersona.CurrentValue, 0, false);

		// Observacion
		RegistrosPeatones.Observacion.SetDbValue(ref RsNew, RegistrosPeatones.Observacion.CurrentValue, System.DBNull.Value, false);
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = RegistrosPeatones.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				RegistrosPeatones.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(RegistrosPeatones.CancelMessage)) {
				FailureMessage = RegistrosPeatones.CancelMessage;
				RegistrosPeatones.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			RegistrosPeatones.IdRegistroPeaton.DbValue = Conn.GetLastInsertId();
			RsNew["IdRegistroPeaton"] = RegistrosPeatones.IdRegistroPeaton.DbValue;
		}
		if (result) {

			// Row Inserted event
			RegistrosPeatones.Row_Inserted(RsOld, RsNew);
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
		RegistrosPeatones_add = new cRegistrosPeatones_add(this);
		CurrentPage = RegistrosPeatones_add;

		//CurrentPageType = RegistrosPeatones_add.GetType();
		RegistrosPeatones_add.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		RegistrosPeatones_add.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (RegistrosPeatones_add != null)
			RegistrosPeatones_add.Dispose();
	}
}
