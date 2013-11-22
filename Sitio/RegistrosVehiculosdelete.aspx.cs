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

partial class RegistrosVehiculosdelete: AspNetMaker9_ControlVehicular {

	// Page object
	public cRegistrosVehiculos_delete RegistrosVehiculos_delete;	

	//
	// Page Class
	//
	public class cRegistrosVehiculos_delete: AspNetMakerPage, IDisposable {

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
		public RegistrosVehiculosdelete AspNetPage { 
			get { return (RegistrosVehiculosdelete)m_ParentPage; }
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
		public cRegistrosVehiculos_delete(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "delete";
			m_PageObjName = "RegistrosVehiculos_delete";
			m_PageObjTypeName = "cRegistrosVehiculos_delete";

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
			if (!Security.CanDelete) {
				Security.SaveLastUrl();
				Page_Terminate("RegistrosVehiculoslist.aspx");
			}

			// UserID_Loading event
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();

			// UserID_Loaded event
			Security.UserID_Loaded();

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

	public int TotalRecs;

	public int RecCnt;

	public ArrayList RecKeys;

	public SqlDataReader Recordset;

	//
	// Page main processing
	//
	public void Page_Main()
	{

		// Load Key Parameters
		RecKeys = RegistrosVehiculos.GetRecordKeys(); // Load record keys
		string sFilter = RegistrosVehiculos.GetKeyFilter();
		if (ew_Empty(sFilter))
			Page_Terminate("RegistrosVehiculoslist.aspx"); // Prevent SQL injection, return to list

		// Set up filter (SQL WHERE clause)
		// SQL constructor in RegistrosVehiculos class, RegistrosVehiculosinfo.aspx

		RegistrosVehiculos.CurrentFilter = sFilter;

		// Get action
		if (ew_NotEmpty(ew_Post("a_delete"))) {
			RegistrosVehiculos.CurrentAction = ew_Post("a_delete");
		} else {
			RegistrosVehiculos.CurrentAction = "D"; // Delete record directly
		}
		switch (RegistrosVehiculos.CurrentAction) {
			case "D": // Delete
				RegistrosVehiculos.SendEmail = true; // Send email on delete success
				if (DeleteRows()) {	// delete rows
					SuccessMessage = Language.Phrase("DeleteSuccess");	// Set up success message
					Page_Terminate(RegistrosVehiculos.ReturnUrl);	// Return to caller
				}
				break;
		}
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = RegistrosVehiculos.CurrentFilter;
		RegistrosVehiculos.Recordset_Selecting(ref sFilter);
		RegistrosVehiculos.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = RegistrosVehiculos.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		
		try {
			string sCntSql = RegistrosVehiculos.SelectCountSQL;

			// Write SQL for debug
			if (EW_DEBUG_ENABLED)
				ew_SetDebugMsg(sCntSql);
			TotalRecs = Convert.ToInt32(Conn.ExecuteScalar(sCntSql));
		} catch {
			TotalRecs = -1;
		}

		// Load recordset
		SqlDataReader Rs = Conn.GetDataReader(sSql);		
		if (TotalRecs < 0 && Rs != null && Rs.HasRows)	{
			TotalRecs = 0;
			while (Rs.Read())
				TotalRecs++;
			Rs.Close();
			Rs = Conn.GetDataReader(sSql);
		}

		// Recordset Selected event
		RegistrosVehiculos.Recordset_Selected(Rs);
		return Rs;
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

			// View refer script
			// IdTipoVehiculo

			RegistrosVehiculos.IdTipoVehiculo.LinkCustomAttributes = "";
			RegistrosVehiculos.IdTipoVehiculo.HrefValue = "";
			RegistrosVehiculos.IdTipoVehiculo.TooltipValue = "";

			// Placa
			RegistrosVehiculos.Placa.LinkCustomAttributes = "";
			RegistrosVehiculos.Placa.HrefValue = "";
			RegistrosVehiculos.Placa.TooltipValue = "";

			// FechaIngreso
			RegistrosVehiculos.FechaIngreso.LinkCustomAttributes = "";
			RegistrosVehiculos.FechaIngreso.HrefValue = "";
			RegistrosVehiculos.FechaIngreso.TooltipValue = "";

			// FechaSalida
			RegistrosVehiculos.FechaSalida.LinkCustomAttributes = "";
			RegistrosVehiculos.FechaSalida.HrefValue = "";
			RegistrosVehiculos.FechaSalida.TooltipValue = "";
		}

		// Row Rendered event
		if (RegistrosVehiculos.RowType != EW_ROWTYPE_AGGREGATEINIT)
			RegistrosVehiculos.Row_Rendered();
	}

	//
	// Delete records based on current filter
	//
	public bool DeleteRows() {
		bool result = true;
		string sKey = "";
		string sThisKey;
		string[] arKeyFlds;
		SqlDataReader Rs = null;
		SqlDataReader RsDelete = null;
		ArrayList RsOld;
		try {
			string sSql = RegistrosVehiculos.SQL;
			Rs = Conn.GetDataReader(sSql);
			if (Rs == null) {
				return false;
			} else if (!Rs.HasRows) {
				FailureMessage = Language.Phrase("NoRecord"); // No record found
				Rs.Close();
				Rs.Dispose();
				return false;
			}
		}	catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw; 
			FailureMessage = e.Message;
			return false;
		}
		if (!Security.CanDelete) {
			FailureMessage = Language.Phrase("NoDeletePermission"); // No delete permission
			return false;
		}

		// Clone old rows
		RsOld = Conn.GetRows(ref Rs);
		Rs.Close();
		Rs.Dispose();
		Conn.BeginTrans();

		// Call row deleting event
		if (result) {
			foreach (OrderedDictionary Row in RsOld) {
				result = RegistrosVehiculos.Row_Deleting(Row);
				if (!result)
					break;
			}
		}
		if (result) {
			sKey = "";
			foreach (OrderedDictionary Row in RsOld) {
				sThisKey = "";
				if (ew_NotEmpty(sThisKey)) sThisKey += EW_COMPOSITE_KEY_SEPARATOR;
				sThisKey += Convert.ToString(Row["IdRegistroVehiculo"]);
				try {
					RegistrosVehiculos.Delete(Row);
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message; // Set up error message
					result = false;
					break;
				}
				if (ew_NotEmpty(sKey)) sKey += ", "; 
				sKey += sThisKey;
			}
		} else {

			// Set up error message
			if (ew_NotEmpty(RegistrosVehiculos.CancelMessage)) {
				FailureMessage = RegistrosVehiculos.CancelMessage;
				RegistrosVehiculos.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("DeleteCancelled");
			}
		}
		if (result) {
			Conn.CommitTrans();	// Commit the changes

			// Row deleted event
			foreach (OrderedDictionary Row in RsOld)
				RegistrosVehiculos.Row_Deleted(Row);
		} else {
			Conn.RollbackTrans(); // Rollback changes			
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
	}

	//
	// ASP.NET Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {

		// Page init
		RegistrosVehiculos_delete = new cRegistrosVehiculos_delete(this);
		CurrentPage = RegistrosVehiculos_delete;

		//CurrentPageType = RegistrosVehiculos_delete.GetType();
		RegistrosVehiculos_delete.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		RegistrosVehiculos_delete.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (RegistrosVehiculos_delete != null)
			RegistrosVehiculos_delete.Dispose();
	}
}
