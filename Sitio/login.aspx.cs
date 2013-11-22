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

partial class _login: AspNetMaker9_ControlVehicular {

	// Page object
	public clogin login;	

	//
	// Page Class
	//
	public class clogin: AspNetMakerPage, IDisposable {

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
		public _login AspNetPage { 
			get { return (_login)m_ParentPage; }
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
		public clogin(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "login";
			m_PageObjName = "login";
			m_PageObjTypeName = "clogin";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
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

	public string sUsername = "";

	public string sLoginType;

	//
	// Page main processing
	//
	public void Page_Main()	{
		bool bValidate;
		bool bValidPwd;
		string sPassword = "";
		string sLastUrl = Security.LastUrl;	// Get last URL
		if (ew_Empty(sLastUrl))
			sLastUrl = "default.aspx";
		if (Security.IsLoggingIn()) {
			sUsername = Convert.ToString(ew_Session[EW_SESSION_USER_PROFILE_USER_NAME]);
			sPassword = Convert.ToString(ew_Session[EW_SESSION_USER_PROFILE_PASSWORD]);
			sLoginType = Convert.ToString(ew_Session[EW_SESSION_USER_PROFILE_LOGIN_TYPE]);
			bValidPwd = Security.ValidateUser(sUsername, sPassword, false);
			if (bValidPwd) {
				ew_Session[EW_SESSION_USER_PROFILE_USER_NAME] = "";
				ew_Session[EW_SESSION_USER_PROFILE_PASSWORD] = "";
				ew_Session[EW_SESSION_USER_PROFILE_LOGIN_TYPE] = "";
			}
		} else {
			if (!Security.IsLoggedIn())
				Security.AutoLogin();
			Security.LoadUserLevel(); // Load user level
			if (HttpContext.Current.Request.RequestType == "POST") {

				// Setup variables
				sUsername = Convert.ToString(ew_RemoveXSS(ew_Post("Username")));
				sPassword = ew_Post("Password");
				sLoginType = ew_Post("rememberme").ToLower();
				bValidate = ValidateForm(sUsername, sPassword);
				if (!bValidate)
					FailureMessage = gsFormError;
				ew_Session[EW_SESSION_USER_PROFILE_USER_NAME] = sUsername; // Save login user name
				ew_Session[EW_SESSION_USER_PROFILE_LOGIN_TYPE] = sLoginType; // Save login type
			}	else	{
				if (Security.IsLoggedIn()) {
					if (ew_Empty(FailureMessage))
						Page_Terminate(sLastUrl); // Return to last accessed page
				}
				bValidate = false;

				// Restore settings
				sUsername = ew_Cookie["username"];
				if (ew_SameStr(ew_Cookie["checksum"], CRC32(MD5(EW_RANDOM_KEY))))
					sUsername = cTEA.Decrypt(ew_Cookie["username"], EW_RANDOM_KEY);
				if (ew_Cookie["autologin"] == "autologin")	{
					sLoginType = "a";
				} else if (ew_Cookie["autologin"] == "rememberusername") {
					sLoginType = "u";
				}	else	{
					sLoginType = "";
				}
			}
			bValidPwd = false; 
			if (bValidate) {
				bValidPwd = false;

				// Logging in event
				bValidate = User_LoggingIn(sUsername, sPassword);
				if (bValidate) {
					bValidPwd = Security.ValidateUser(sUsername, sPassword, false);
					if (!bValidPwd) {
						if (ew_Empty(FailureMessage))
							FailureMessage = Language.Phrase("InvalidUidPwd"); // Invalid user id/password
					}
				} else {
					if (ew_Empty(FailureMessage))
						FailureMessage = Language.Phrase("LoginCancelled"); // Login cancelled 
				}
			}
		}

		// Write cookies
		if (bValidPwd) {
			if (sLoginType == "a")	{		// Auto login
				ew_Cookie["autologin"] ="autologin";	// Set up autologin cookies
				ew_Cookie["username"] = cTEA.Encrypt(sUsername, EW_RANDOM_KEY);	// Set up user name cookies
				ew_Cookie["password"] = cTEA.Encrypt(sPassword, EW_RANDOM_KEY);	// Set up password cookies
				ew_Cookie["checksum"] = Convert.ToString(CRC32(MD5(EW_RANDOM_KEY)));				
			} else if (sLoginType == "u") {	// Remember user name
				ew_Cookie["autologin"] = "rememberusername";	// Set up remember user name cookies
				ew_Cookie["username"] = cTEA.Encrypt(sUsername, EW_RANDOM_KEY);	// Set up user name cookies
				ew_Cookie["checksum"] = Convert.ToString(CRC32(MD5(EW_RANDOM_KEY)));				
			}	else	{
				ew_Cookie["autologin"] = "";		// Clear autologin cookies
			}

			// User_LoggedIn event
			User_LoggedIn(sUsername);
			ew_WriteAuditTrailOnLogInOut(Language.Phrase("AuditTrailLogin"), sUsername);
			Page_Terminate(sLastUrl); // Return to last accessed URL
		} else if (ew_NotEmpty(sUsername) && ew_NotEmpty(sPassword)) {

			// User login error event
			User_LoginError(sUsername, sPassword);
		}
	}

	//
	// Validate form
	//
	public bool ValidateForm(string usr, string pwd) {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return true; // Skip
		if (ew_Empty(usr))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterUid"));
		if (ew_Empty(pwd))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterPwd"));

		// Return validate result
		bool result = (ew_Empty(gsFormError));

		// Form_CustomValidate event
		string sFormCustomError = "";
		result = result & Form_CustomValidate(ref sFormCustomError);
		if (ew_NotEmpty(sFormCustomError))
			ew_AddMessage(ref gsFormError, sFormCustomError);
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

		// Page Data Rendered event
		public void Page_DataRendered(ref string footer) {

			// Example:
			//footer = "your footer";

		}

	// User Logging In event
	public bool User_LoggingIn(string usr, string pwd) {

		// Enter your code here
		// To cancel, set return value to False

		return true;
	}

	// User Logged In event
	public void User_LoggedIn(string usr) {

		//HttpContext.Current.Response.Write("User Logged In");
	}

	// User Login Error event
	public void User_LoginError(string usr, string pwd) {

		//HttpContext.Current.Response.Write("User Login Error");
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
		login = new clogin(this);
		CurrentPage = login;

		//CurrentPageType = login.GetType();
		login.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		login.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (login != null)
			login.Dispose();
	}
}
