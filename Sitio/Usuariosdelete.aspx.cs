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

partial class Usuariosdelete: AspNetMaker9_ControlVehicular {

	// Page object
	public cUsuarios_delete Usuarios_delete;	

	//
	// Page Class
	//
	public class cUsuarios_delete: AspNetMakerPage, IDisposable {

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
		public Usuariosdelete AspNetPage { 
			get { return (Usuariosdelete)m_ParentPage; }
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
		public cUsuarios_delete(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "delete";
			m_PageObjName = "Usuarios_delete";
			m_PageObjTypeName = "cUsuarios_delete";

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
			if (!Security.CanDelete) {
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
		RecKeys = Usuarios.GetRecordKeys(); // Load record keys
		string sFilter = Usuarios.GetKeyFilter();
		if (ew_Empty(sFilter))
			Page_Terminate("Usuarioslist.aspx"); // Prevent SQL injection, return to list

		// Set up filter (SQL WHERE clause)
		// SQL constructor in Usuarios class, Usuariosinfo.aspx

		Usuarios.CurrentFilter = sFilter;

		// Get action
		if (ew_NotEmpty(ew_Post("a_delete"))) {
			Usuarios.CurrentAction = ew_Post("a_delete");
		} else {
			Usuarios.CurrentAction = "D"; // Delete record directly
		}
		switch (Usuarios.CurrentAction) {
			case "D": // Delete
				Usuarios.SendEmail = true; // Send email on delete success
				if (DeleteRows()) {	// delete rows
					SuccessMessage = Language.Phrase("DeleteSuccess");	// Set up success message
					Page_Terminate(Usuarios.ReturnUrl);	// Return to caller
				}
				break;
		}
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = Usuarios.CurrentFilter;
		Usuarios.Recordset_Selecting(ref sFilter);
		Usuarios.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = Usuarios.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		
		try {
			string sCntSql = Usuarios.SelectCountSQL;

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
		Usuarios.Recordset_Selected(Rs);
		return Rs;
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
		}

		// Row Rendered event
		if (Usuarios.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Usuarios.Row_Rendered();
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
			string sSql = Usuarios.SQL;
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
				result = Usuarios.Row_Deleting(Row);
				if (!result)
					break;
			}
		}
		if (result) {
			sKey = "";
			foreach (OrderedDictionary Row in RsOld) {
				sThisKey = "";
				if (ew_NotEmpty(sThisKey)) sThisKey += EW_COMPOSITE_KEY_SEPARATOR;
				sThisKey += Convert.ToString(Row["IdUsuario"]);
				try {
					Usuarios.Delete(Row);
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
			if (ew_NotEmpty(Usuarios.CancelMessage)) {
				FailureMessage = Usuarios.CancelMessage;
				Usuarios.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("DeleteCancelled");
			}
		}
		if (result) {
			Conn.CommitTrans();	// Commit the changes

			// Row deleted event
			foreach (OrderedDictionary Row in RsOld)
				Usuarios.Row_Deleted(Row);
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
		Usuarios_delete = new cUsuarios_delete(this);
		CurrentPage = Usuarios_delete;

		//CurrentPageType = Usuarios_delete.GetType();
		Usuarios_delete.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Usuarios_delete.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (Usuarios_delete != null)
			Usuarios_delete.Dispose();
	}
}
