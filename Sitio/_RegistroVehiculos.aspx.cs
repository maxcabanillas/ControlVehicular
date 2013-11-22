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
using System.Data.OleDb;

//
// ASP.NET code-behind class (Page)
//

partial class blankpage: AspNetMaker9_ControlVehicular {

    // Page object
    public ccustompage custompage;	

    //
    // Page Class
    //
    public class ccustompage: AspNetMakerPage, IDisposable {

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
        public blankpage AspNetPage { 
            get { return (blankpage)m_ParentPage; }
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
        public ccustompage(AspNetMaker9_ControlVehicular APage) {		
            m_ParentPage = APage;
            m_Page = this;
            m_PageID = "custompage";
            m_PageObjName = "custompage";
            m_PageObjTypeName = "ccustompage";

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

            // Uncomment codes below for security
            //if (!Security.IsLoggedIn()) Security.AutoLogin();
            //if (!Security.IsLoggedIn())
            //	Page_Terminate("login.aspx");
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

        //
        // Page main
        //
        public void Page_Main() {

            //SuccessMessage = "Welcome " + CurrentUserName();
            // Put your custom codes here

        }

        // Page Load event
        public void Page_Load() {

            //REvisa si se enviaron datos a guardar
            if ("" + HttpContext.Current.Request.QueryString["Placa"] != "")
            {
                HttpContext.Current.Response.Write("Se tiene la placa " + HttpContext.Current.Request.QueryString["Placa"]);

            }


                    
            

             // Debe llamar a la consulta de semestres y pegarlo en el selector de semestres
            //TipoVehiculo.DataSource = dr;
            //TipoVehiculo.DataTextField = "TipoVehiculo";
            //TipoVehiculo.DataValueField = "idTipoVehiculo";
            //TipoVehiculo.DataBind();

            //TipoVehiculo.Items.Insert(0, "seleccione uno");

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
        custompage = new ccustompage(this);
        CurrentPage = custompage;

        //CurrentPageType = custompage.GetType();
        custompage.Page_Init();

        // Set buffer/cache option
        Response.Buffer = EW_RESPONSE_BUFFER;
        ew_Header(false);

        // Page main processing
        custompage.Page_Main();
    }

    //
    // ASP.NET Page_Unload event
    //

    protected void Page_Unload(object sender, System.EventArgs e) {

        // Dispose page object
        if (custompage != null)
            custompage.Dispose();
    }

    public string ObtenerTiposVehiculos()
    {

        string Cadena = "<option value='0'>Seleccione uno</option>";

        SqlDataReader dr;

        dr = Conn.GetDataReader("select * from TiposVehiculos order by TipoVehiculo");

        while (dr.Read())
        {
            Cadena += @"<option value='" + dr["IdTipoVehiculo"] + "'>" + dr["TipoVehiculo"] + "</option>";

        }


        return Cadena;
    }
}
