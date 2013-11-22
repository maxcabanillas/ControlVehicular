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

partial class RegistrosVehiculosadd: AspNetMaker9_ControlVehicular {

	// Page object
	public cRegistrosVehiculos_add RegistrosVehiculos_add;	

	//
	// Page Class
	//
	public class cRegistrosVehiculos_add: AspNetMakerPage, IDisposable {

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
				if (RegistrosVehiculos.UseTokenInUrl)
					Url += "t=" + RegistrosVehiculos.TableVar + "&"; // Add page token
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
			if (RegistrosVehiculos.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (RegistrosVehiculos.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (RegistrosVehiculos.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public RegistrosVehiculosadd AspNetPage { 
			get { return (RegistrosVehiculosadd)m_ParentPage; }
		}

		// RegistrosVehiculos	
		public cRegistrosVehiculos RegistrosVehiculos { 
			get {				
				return ParentPage.RegistrosVehiculos;
			}
			set {
				ParentPage.RegistrosVehiculos = value;	
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
		public cRegistrosVehiculos_add(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "add";
			m_PageObjName = "RegistrosVehiculos_add";
			m_PageObjTypeName = "cRegistrosVehiculos_add";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (RegistrosVehiculos == null)
				RegistrosVehiculos = new cRegistrosVehiculos(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "RegistrosVehiculos";
			m_Table = RegistrosVehiculos;
			CurrentTable = RegistrosVehiculos;

			//CurrentTableType = RegistrosVehiculos.GetType();
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
				Page_Terminate("RegistrosVehiculoslist.aspx");
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
			RegistrosVehiculos.Dispose();
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
			RegistrosVehiculos.CurrentAction = ObjForm.GetValue("a_add"); // Get form action
			CopyRecord = LoadOldRecord(); // Load old recordset
			LoadFormValues(); // Load form values

			// Validate Form
			if (!ValidateForm()) {
				RegistrosVehiculos.CurrentAction = "I"; // Form error, reset action
				RegistrosVehiculos.EventCancelled = true; // Event cancelled
				RestoreFormValues(); // Restore form values
				FailureMessage = gsFormError;
			}

		// Not post back
		} else {

			// Load key values from QueryString
			CopyRecord = true;
			if (ew_NotEmpty(ew_Get("IdRegistroVehiculo"))) {
				RegistrosVehiculos.IdRegistroVehiculo.QueryStringValue = ew_Get("IdRegistroVehiculo");
				RegistrosVehiculos.SetKey("IdRegistroVehiculo", RegistrosVehiculos.IdRegistroVehiculo.CurrentValue); // Set up key
			} else {
				RegistrosVehiculos.SetKey("IdRegistroVehiculo", ""); // Clear key
				CopyRecord = false;
			}
			if (CopyRecord) {
				RegistrosVehiculos.CurrentAction = "C"; // Copy Record
			} else {
				RegistrosVehiculos.CurrentAction = "I"; // Display Blank Record
				LoadDefaultValues(); // Load default values
			}
		}

		// Perform action based on action code
		switch (RegistrosVehiculos.CurrentAction) {
			case "I": // Blank record, no action required
				break;
			case "C": // Copy an existing record
				if (!LoadRow()) { // Load record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("RegistrosVehiculoslist.aspx"); // No matching record, return to list
				}
				break;
			case "A": // Add new record
				RegistrosVehiculos.SendEmail = true; // Send email on add success
				if (AddRow(Conn.GetRow(ref OldRecordset))) { // Add successful
					SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
					string sReturnUrl;
					sReturnUrl = RegistrosVehiculos.AddUrl;
					if (ew_GetPageName(sReturnUrl) == "RegistrosVehiculosview.aspx") sReturnUrl = RegistrosVehiculos.ViewUrl; // View paging, return to view page with keyurl directly
					Page_Terminate(sReturnUrl); // Clean up and return
				} else {
					RegistrosVehiculos.EventCancelled = true; // Event cancelled
					RestoreFormValues(); // Add failed, restore form values
				}
				break;
		}

		// Render row based on row type
		RegistrosVehiculos.RowType = EW_ROWTYPE_ADD; // Render add type

		// Render row
		RegistrosVehiculos.ResetAttrs();
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
		RegistrosVehiculos.IdTipoVehiculo.CurrentValue = System.DBNull.Value;
		RegistrosVehiculos.IdTipoVehiculo.OldValue = RegistrosVehiculos.IdTipoVehiculo.CurrentValue;
		RegistrosVehiculos.Placa.CurrentValue = System.DBNull.Value;
		RegistrosVehiculos.Placa.OldValue = RegistrosVehiculos.Placa.CurrentValue;
		RegistrosVehiculos.Observaciones.CurrentValue = System.DBNull.Value;
		RegistrosVehiculos.Observaciones.OldValue = RegistrosVehiculos.Observaciones.CurrentValue;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
		if (!RegistrosVehiculos.IdTipoVehiculo.FldIsDetailKey) {
			RegistrosVehiculos.IdTipoVehiculo.FormValue = ObjForm.GetValue("x_IdTipoVehiculo");
		}
		if (!RegistrosVehiculos.Placa.FldIsDetailKey) {
			RegistrosVehiculos.Placa.FormValue = ObjForm.GetValue("x_Placa");
		}
		if (!RegistrosVehiculos.Observaciones.FldIsDetailKey) {
			RegistrosVehiculos.Observaciones.FormValue = ObjForm.GetValue("x_Observaciones");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadOldRecord();
		RegistrosVehiculos.IdTipoVehiculo.CurrentValue = RegistrosVehiculos.IdTipoVehiculo.FormValue;
		RegistrosVehiculos.Placa.CurrentValue = RegistrosVehiculos.Placa.FormValue;
		RegistrosVehiculos.Observaciones.CurrentValue = RegistrosVehiculos.Observaciones.FormValue;
		RegistrosVehiculos.IdRegistroVehiculo.CurrentValue = RegistrosVehiculos.IdRegistroVehiculo.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = RegistrosVehiculos.KeyFilter;

		// Row Selecting event
		RegistrosVehiculos.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		RegistrosVehiculos.CurrentFilter = sFilter;
		string sSql = RegistrosVehiculos.SQL;

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
		RegistrosVehiculos.Row_Selected(ref row);
		RegistrosVehiculos.IdRegistroVehiculo.DbValue = row["IdRegistroVehiculo"];
		RegistrosVehiculos.IdTipoVehiculo.DbValue = row["IdTipoVehiculo"];
		RegistrosVehiculos.Placa.DbValue = row["Placa"];
		RegistrosVehiculos.FechaIngreso.DbValue = row["FechaIngreso"];
		RegistrosVehiculos.FechaSalida.DbValue = row["FechaSalida"];
		RegistrosVehiculos.Observaciones.DbValue = row["Observaciones"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(RegistrosVehiculos.GetKey("IdRegistroVehiculo")))
			RegistrosVehiculos.IdRegistroVehiculo.CurrentValue = RegistrosVehiculos.GetKey("IdRegistroVehiculo"); // IdRegistroVehiculo
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			RegistrosVehiculos.CurrentFilter = RegistrosVehiculos.KeyFilter;
			string sSql = RegistrosVehiculos.SQL;			
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

		RegistrosVehiculos.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdRegistroVehiculo
		// IdTipoVehiculo
		// Placa
		// FechaIngreso
		// FechaSalida
		// Observaciones
		//
		//  View  Row
		//

		if (RegistrosVehiculos.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdRegistroVehiculo
				RegistrosVehiculos.IdRegistroVehiculo.ViewValue = RegistrosVehiculos.IdRegistroVehiculo.CurrentValue;
			RegistrosVehiculos.IdRegistroVehiculo.ViewCustomAttributes = "";

			// IdTipoVehiculo
			if (ew_NotEmpty(RegistrosVehiculos.IdTipoVehiculo.CurrentValue)) {
				sFilterWrk = "[IdTipoVehiculo] = " + ew_AdjustSql(RegistrosVehiculos.IdTipoVehiculo.CurrentValue) + "";
			sSqlWrk = "SELECT [TipoVehiculo] FROM [dbo].[TiposVehiculos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoVehiculo]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					RegistrosVehiculos.IdTipoVehiculo.ViewValue = drWrk["TipoVehiculo"];
				} else {
					RegistrosVehiculos.IdTipoVehiculo.ViewValue = RegistrosVehiculos.IdTipoVehiculo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				RegistrosVehiculos.IdTipoVehiculo.ViewValue = System.DBNull.Value;
			}
			RegistrosVehiculos.IdTipoVehiculo.ViewCustomAttributes = "";

			// Placa
				RegistrosVehiculos.Placa.ViewValue = RegistrosVehiculos.Placa.CurrentValue;
			RegistrosVehiculos.Placa.ViewCustomAttributes = "";

			// FechaIngreso
				RegistrosVehiculos.FechaIngreso.ViewValue = RegistrosVehiculos.FechaIngreso.CurrentValue;
				RegistrosVehiculos.FechaIngreso.ViewValue = ew_FormatDateTime(RegistrosVehiculos.FechaIngreso.ViewValue, 7);
			RegistrosVehiculos.FechaIngreso.ViewCustomAttributes = "";

			// FechaSalida
				RegistrosVehiculos.FechaSalida.ViewValue = RegistrosVehiculos.FechaSalida.CurrentValue;
				RegistrosVehiculos.FechaSalida.ViewValue = ew_FormatDateTime(RegistrosVehiculos.FechaSalida.ViewValue, 7);
			RegistrosVehiculos.FechaSalida.ViewCustomAttributes = "";

			// Observaciones
			RegistrosVehiculos.Observaciones.ViewValue = RegistrosVehiculos.Observaciones.CurrentValue;
			RegistrosVehiculos.Observaciones.ViewCustomAttributes = "";

			// View refer script
			// IdTipoVehiculo

			RegistrosVehiculos.IdTipoVehiculo.LinkCustomAttributes = "";
			RegistrosVehiculos.IdTipoVehiculo.HrefValue = "";
			RegistrosVehiculos.IdTipoVehiculo.TooltipValue = "";

			// Placa
			RegistrosVehiculos.Placa.LinkCustomAttributes = "";
			RegistrosVehiculos.Placa.HrefValue = "";
			RegistrosVehiculos.Placa.TooltipValue = "";

			// Observaciones
			RegistrosVehiculos.Observaciones.LinkCustomAttributes = "";
			RegistrosVehiculos.Observaciones.HrefValue = "";
			RegistrosVehiculos.Observaciones.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (RegistrosVehiculos.RowType == EW_ROWTYPE_ADD) { // Add row

			// IdTipoVehiculo
			RegistrosVehiculos.IdTipoVehiculo.EditCustomAttributes = "";
			sFilterWrk = "";
			sSqlWrk = "SELECT [IdTipoVehiculo], [TipoVehiculo] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[TiposVehiculos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoVehiculo]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			RegistrosVehiculos.IdTipoVehiculo.EditValue = alwrk;

			// Placa
			RegistrosVehiculos.Placa.EditCustomAttributes = "";
			RegistrosVehiculos.Placa.EditValue = ew_HtmlEncode(RegistrosVehiculos.Placa.CurrentValue);

			// Observaciones
			RegistrosVehiculos.Observaciones.EditCustomAttributes = "";
			RegistrosVehiculos.Observaciones.EditValue = ew_HtmlEncode(RegistrosVehiculos.Observaciones.CurrentValue);

			// Edit refer script
			// IdTipoVehiculo

			RegistrosVehiculos.IdTipoVehiculo.HrefValue = "";

			// Placa
			RegistrosVehiculos.Placa.HrefValue = "";

			// Observaciones
			RegistrosVehiculos.Observaciones.HrefValue = "";
		}
		if (RegistrosVehiculos.RowType == EW_ROWTYPE_ADD ||
			RegistrosVehiculos.RowType == EW_ROWTYPE_EDIT ||
			RegistrosVehiculos.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			RegistrosVehiculos.SetupFieldTitles();
		}

		// Row Rendered event
		if (RegistrosVehiculos.RowType != EW_ROWTYPE_AGGREGATEINIT)
			RegistrosVehiculos.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(RegistrosVehiculos.Placa.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + RegistrosVehiculos.Placa.FldCaption);

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

		// IdTipoVehiculo
		RegistrosVehiculos.IdTipoVehiculo.SetDbValue(ref RsNew, RegistrosVehiculos.IdTipoVehiculo.CurrentValue, System.DBNull.Value, false);

		// Placa
		RegistrosVehiculos.Placa.SetDbValue(ref RsNew, RegistrosVehiculos.Placa.CurrentValue, "", false);

		// Observaciones
		RegistrosVehiculos.Observaciones.SetDbValue(ref RsNew, RegistrosVehiculos.Observaciones.CurrentValue, System.DBNull.Value, false);
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = RegistrosVehiculos.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				RegistrosVehiculos.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(RegistrosVehiculos.CancelMessage)) {
				FailureMessage = RegistrosVehiculos.CancelMessage;
				RegistrosVehiculos.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			RegistrosVehiculos.IdRegistroVehiculo.DbValue = Conn.GetLastInsertId();
			RsNew["IdRegistroVehiculo"] = RegistrosVehiculos.IdRegistroVehiculo.DbValue;
		}
		if (result) {

			// Row Inserted event
			RegistrosVehiculos.Row_Inserted(RsOld, RsNew);
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
		RegistrosVehiculos_add = new cRegistrosVehiculos_add(this);
		CurrentPage = RegistrosVehiculos_add;

		//CurrentPageType = RegistrosVehiculos_add.GetType();
		RegistrosVehiculos_add.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		RegistrosVehiculos_add.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (RegistrosVehiculos_add != null)
			RegistrosVehiculos_add.Dispose();
	}
}
