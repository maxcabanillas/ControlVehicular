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

partial class _default: AspNetMaker9_ControlVehicular {

	// Page object
	public cindex index;	

	//
	// Page Class
	//
	public class cindex: AspNetMakerPage, IDisposable {

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

		// ASP.NET page object
		public _default AspNetPage { 
			get { return (_default)m_ParentPage; }
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
		public cindex(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "index";
			m_PageObjName = "index";
			m_PageObjTypeName = "cindex";

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

		// Page main processing
		public void Page_Main() {
			if (!Security.IsLoggedIn()) Security.AutoLogin();
			Security.LoadUserLevel();
				Page_Terminate("z_RegistroVehiculolist.aspx"); // Exit and go to default page
			if (Security.AllowList("Areas"))
				Page_Terminate("Areaslist.aspx");
			if (Security.AllowList("Personas"))
				Page_Terminate("Personaslist.aspx");
			if (Security.AllowList("TiposVehiculos"))
				Page_Terminate("TiposVehiculoslist.aspx");
			if (Security.AllowList("UserLevels"))
				Page_Terminate("UserLevelslist.aspx");
			if (Security.AllowList("Usuarios"))
				Page_Terminate("Usuarioslist.aspx");
			if (Security.AllowList("VehiculosAutorizados"))
				Page_Terminate("VehiculosAutorizadoslist.aspx");
			if (Security.AllowList("VehiculosPicoYPlacaHoras"))
				Page_Terminate("VehiculosPicoYPlacaHoraslist.aspx");
			if (Security.AllowList("TiposDocumentos"))
				Page_Terminate("TiposDocumentoslist.aspx");
			if (Security.AllowList("HistoricoVehiculos"))
				Page_Terminate("HistoricoVehiculoslist.aspx");
			if (Security.AllowList("HistoricoPeatones"))
				Page_Terminate("HistoricoPeatoneslist.aspx");
				Page_Terminate("z_RegistroPeatoneslist.aspx");
			if (Security.AllowList("Cargos"))
				Page_Terminate("Cargoslist.aspx");
			if (Security.IsLoggedIn()) {
				FailureMessage = Language.Phrase("NoPermission") + "<br /><br /><a href=\"logout.aspx\">" + Language.Phrase("BackToLogin") + "</a>";
			} else {
				Page_Terminate("login.aspx"); // Exit and go to login page
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
	}

	//
	// ASP.NET Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {

		// Page init
		index = new cindex(this);
		CurrentPage = index;

		//CurrentPageType = index.GetType();
		index.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		index.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (index != null)
			index.Dispose();
	}
}
