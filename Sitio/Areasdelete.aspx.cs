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

partial class Areasdelete: AspNetMaker9_ControlVehicular {

	// Page object
	public cAreas_delete Areas_delete;	

	//
	// Page Class
	//
	public class cAreas_delete: AspNetMakerPage, IDisposable {

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
		public Areasdelete AspNetPage { 
			get { return (Areasdelete)m_ParentPage; }
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

		//
		//  Page class constructor
		//
		public cAreas_delete(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "delete";
			m_PageObjName = "Areas_delete";
			m_PageObjTypeName = "cAreas_delete";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (Areas == null)
				Areas = new cAreas(this);
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
			if (!Security.CanDelete) {
				Security.SaveLastUrl();
				Page_Terminate("Areaslist.aspx");
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
			Areas.Dispose();
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
		RecKeys = Areas.GetRecordKeys(); // Load record keys
		string sFilter = Areas.GetKeyFilter();
		if (ew_Empty(sFilter))
			Page_Terminate("Areaslist.aspx"); // Prevent SQL injection, return to list

		// Set up filter (SQL WHERE clause)
		// SQL constructor in Areas class, Areasinfo.aspx

		Areas.CurrentFilter = sFilter;

		// Get action
		if (ew_NotEmpty(ew_Post("a_delete"))) {
			Areas.CurrentAction = ew_Post("a_delete");
		} else {
			Areas.CurrentAction = "D"; // Delete record directly
		}
		switch (Areas.CurrentAction) {
			case "D": // Delete
				Areas.SendEmail = true; // Send email on delete success
				if (DeleteRows()) {	// delete rows
					SuccessMessage = Language.Phrase("DeleteSuccess");	// Set up success message
					Page_Terminate(Areas.ReturnUrl);	// Return to caller
				}
				break;
		}
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = Areas.CurrentFilter;
		Areas.Recordset_Selecting(ref sFilter);
		Areas.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = Areas.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		
		try {
			string sCntSql = Areas.SelectCountSQL;

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
		Areas.Recordset_Selected(Rs);
		return Rs;
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
			// Area

			Areas.Area.LinkCustomAttributes = "";
			Areas.Area.HrefValue = "";
			Areas.Area.TooltipValue = "";

			// Codigo
			Areas.Codigo.LinkCustomAttributes = "";
			Areas.Codigo.HrefValue = "";
			Areas.Codigo.TooltipValue = "";
		}

		// Row Rendered event
		if (Areas.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Areas.Row_Rendered();
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
			string sSql = Areas.SQL;
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
				result = Areas.Row_Deleting(Row);
				if (!result)
					break;
			}
		}
		if (result) {
			sKey = "";
			foreach (OrderedDictionary Row in RsOld) {
				sThisKey = "";
				if (ew_NotEmpty(sThisKey)) sThisKey += EW_COMPOSITE_KEY_SEPARATOR;
				sThisKey += Convert.ToString(Row["IdArea"]);
				try {
					Areas.Delete(Row);
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
			if (ew_NotEmpty(Areas.CancelMessage)) {
				FailureMessage = Areas.CancelMessage;
				Areas.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("DeleteCancelled");
			}
		}
		if (result) {
			Conn.CommitTrans();	// Commit the changes

			// Row deleted event
			foreach (OrderedDictionary Row in RsOld)
				Areas.Row_Deleted(Row);
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
		Areas_delete = new cAreas_delete(this);
		CurrentPage = Areas_delete;

		//CurrentPageType = Areas_delete.GetType();
		Areas_delete.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Areas_delete.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (Areas_delete != null)
			Areas_delete.Dispose();
	}
}
