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

partial class _userpriv: AspNetMaker9_ControlVehicular {

	// Page object
	public cuserpriv userpriv;	

	//
	// Page Class
	//
	public class cuserpriv: AspNetMakerPage, IDisposable {

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
				return Url;
			}
		}

		public string PageHeader = "";

		public string PageFooter = "";

		// Show Page Header
		public void ShowPageHeader() {
			string sHeader = PageHeader;
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
			return true;
		}

		// ASP.NET page object
		public _userpriv AspNetPage { 
			get { return (_userpriv)m_ParentPage; }
		}

		// UserLevels	
		public cUserLevels UserLevels { 
			get {				
				return ParentPage.UserLevels;
			}
			set {
				ParentPage.UserLevels = value;	
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
		public cuserpriv(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "userpriv";
			m_PageObjName = "userpriv";
			m_PageObjTypeName = "cuserpriv";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (UserLevels == null)
				UserLevels = new cUserLevels(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

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
			Security.LoadCurrentUserLevel("UserLevels");

			// Table Permission loaded event
			Security.TablePermission_Loaded();
			if (!Security.CanAdmin) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
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
			UserLevels.Dispose();
			Usuarios.Dispose();
			if (ew_NotEmpty(sRedirectUrl)) {
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.Redirect(sRedirectUrl); 
			}
		}

	public int TempPriv;

	public string Disabled;

	public int[] Privileges;	

	public string[] TableNames;	

	public string[] TableCaptions;

	public string[] TableVars;

	public cLanguage ReportLanguage;

	//
	// Page main processing
	//
	public void Page_Main() {

		// Try to load ASP.NET Report Maker language file
		// Note: The langauge IDs must be the same in both projects

		if (ew_NotEmpty(ew_GetFieldValue("EWRPT_LANGUAGE_FOLDER")))
			ReportLanguage = new cLanguage(this, Convert.ToString(ew_GetFieldValue("EWRPT_LANGUAGE_FOLDER"))); // ASPX
		GetTableNames(ref TableNames);
		GetTableCaptions(ref TableCaptions);
		GetTableVars(ref TableVars);
		if (!Information.IsArray(TableNames)) {
			FailureMessage = Language.Phrase("NoTableGenerated"); 
			Page_Terminate("UserLevelslist.aspx"); // Return to list
		}
		Array.Resize(ref Privileges, TableNames.Length);

		// Get action
		if (ew_Empty(ew_Post("a_edit"))) {
			UserLevels.CurrentAction = "I"; // Display with input box

			// Load key from QueryString
			if (ew_NotEmpty(ew_Get("UserLevelID"))) {
				UserLevels.UserLevelID.QueryStringValue = ew_Get("UserLevelID");
			} else {
				Page_Terminate("UserLevelslist.aspx"); // Return to list
			}
			if (UserLevels.UserLevelID.QueryStringValue == "-1") {
				Disabled = " disabled=\"disabled\"";
			} else {
				Disabled = "";
			}
		} else {
			UserLevels.CurrentAction = ew_Post("a_edit");

			// Get fields from form
			UserLevels.UserLevelID.FormValue = ew_Post("x_UserLevelID");
			for (int i = 0; i < TableNames.Length; i++) {
				if (EW_USER_LEVEL_COMPAT)	{
					Privileges[i] = ew_ConvertToInt(ew_Post("Add_" + i)) + ew_ConvertToInt(ew_Post("Delete_" + i)) + ew_ConvertToInt(ew_Post("Edit_" + i)) + ew_ConvertToInt(ew_Post("List_" + i));
				}	else {
					Privileges[i] = ew_ConvertToInt(ew_Post("Add_" + i)) + ew_ConvertToInt(ew_Post("Delete_" + i)) + ew_ConvertToInt(ew_Post("Edit_" + i)) + ew_ConvertToInt(ew_Post("List_" + i)) + ew_ConvertToInt(ew_Post("View_" + i)) + ew_ConvertToInt(ew_Post("Search_" + i));
				}
			}
		}
		switch (UserLevels.CurrentAction) {
			case "I":	// Display
				Security.SetUpUserLevelEx(); // Get all user level info
				break;
			case "U":	// Update
				if (EditRow()) { // Update Record based on key
					SuccessMessage = Language.Phrase("UpdateSuccess"); // Set update success message 

					// Alternatively, comment out the following line to go back to this page
					Page_Terminate("UserLevelslist.aspx"); // Return to list
				}
				break;
		}
	}

	//
	// Update privileges
	//
	public bool EditRow()	{
		string Sql;
		int lRowCnt;
		for (int i = 0; i < TableNames.Length; i++) {
			Sql = "UPDATE " + EW_USER_LEVEL_PRIV_TABLE + " SET " + EW_USER_LEVEL_PRIV_PRIV_FIELD + " = " + Privileges[i] + " WHERE " +
				EW_USER_LEVEL_PRIV_TABLE_NAME_FIELD + " = '" + ew_AdjustSql(TableNames[i]) + "' AND " +
				EW_USER_LEVEL_PRIV_USER_LEVEL_ID_FIELD + " = " + UserLevels.UserLevelID.CurrentValue;
			lRowCnt = Conn.Execute(Sql);
			if (lRowCnt == 0) {
				Sql = "INSERT INTO " + EW_USER_LEVEL_PRIV_TABLE + " (" + EW_USER_LEVEL_PRIV_TABLE_NAME_FIELD + ", " + EW_USER_LEVEL_PRIV_USER_LEVEL_ID_FIELD + ", " + EW_USER_LEVEL_PRIV_PRIV_FIELD + ") VALUES ('" + ew_AdjustSql(TableNames[i]) + "', " + UserLevels.UserLevelID.CurrentValue + ", " + Privileges[i] + ")";
				lRowCnt = Conn.Execute(Sql);
			}
		}
		return true;
	}

	//
	// Get table caption
	//
	public string GetTableCaption(int i) {
		string str = "";
		if (i < TableNames.Length) {
			if (i < TableVars.Length)
				str = Convert.ToString(Language.TablePhrase(TableVars[i], "TblCaption")) + "";				
			bool report = TableNames[i].StartsWith(EW_TABLE_PREFIX);
      if (report && ReportLanguage != null)
				str = ReportLanguage.TablePhrase(TableVars[i], "TblCaption");
			if (ew_Empty(str) && i < TableCaptions.Length)
				str = TableCaptions[i];
			if (ew_Empty(str)) {
				str = TableNames[i];
				if (report)
					str = str.Substring(EW_TABLE_PREFIX.Length);
			}
			if (report)
				str += "&nbsp;(" + Language.Phrase("Report") + ")";
		} else {
			str = "";
		}
		return str;
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
		userpriv = new cuserpriv(this);
		CurrentPage = userpriv;

		//CurrentPageType = userpriv.GetType();
		userpriv.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		userpriv.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (userpriv != null)
			userpriv.Dispose();
	}
}
