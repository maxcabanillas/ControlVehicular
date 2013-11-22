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
// ASP.NET code-behind class (Preview)
//

partial class Personaspreview: AspNetMaker9_ControlVehicular {

	// Page object
	public cPersonas_preview Personas_preview;

	// Page class for preview
	public class cPersonas_preview: AspNetMakerPage, IDisposable {

		public int TotalRecs;

		public int RowCount;

		public SqlDataReader Recordset;			

		// Constructor
		public cPersonas_preview(AspNetMaker9_ControlVehicular APage) {
			m_ParentPage = APage;
			m_Page = this;

			// Initialize language object
			Language = new cLanguage(this);			
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
			if (Personas.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (Personas.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (Personas.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// Personas
		public cPersonas Personas {
			get { return ParentPage.Personas; }
			set { ParentPage.Personas = value; }
		}

		// Areas
		public cAreas Areas {
			get { return ParentPage.Areas; }
			set { ParentPage.Areas = value; }
		}

		// Usuarios
		public cUsuarios Usuarios {
			get { return ParentPage.Usuarios; }
			set { ParentPage.Usuarios = value; }
		}

		//
		// Page main processing
		//
		public void Page_Main()	{

			// Open connection to the database
			Conn = new cConnection();

			// Initialize table object
			if (Personas == null)		
				Personas = new cPersonas(this);
			if (Areas == null)		
				Areas = new cAreas(this);
			if (Usuarios == null)		
				Usuarios = new cUsuarios(this);
			if (Security == null) Security = new cAdvancedSecurity(this);
			if (!Security.IsLoggedIn()) Security.AutoLogin();
			if (!Security.IsLoggedIn()) {
				ew_Write(Language.Phrase("NoPermission"));
				ew_End();
			}

			// Table Permission loading event
			Security.TablePermission_Loading();
			Security.LoadCurrentUserLevel("Personas");

			// Table Permission loaded event
			Security.TablePermission_Loaded();
			if (!Security.IsLoggedIn()) {
				Security.SaveLastUrl();
				ew_Write(Language.Phrase("NoPermission"));
				ew_End();
			}
			if (!Security.CanList) {
				ew_Write(Language.Phrase("NoPermission"));
				ew_End();
			}

			// UserID_Loading event
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();

			// UserID_Loaded event
			Security.UserID_Loaded();
		}

		//
		//  Sub Page_Terminate
		//  - called when exit page
		//  - clean up connection and objects
		//  - if URL specified, redirect to URL
		//
		public void Page_Terminate() {

			//###Security = null;
			Personas.Dispose();
			Areas.Dispose();
			Usuarios.Dispose();

			// Close connection
			Conn.Dispose();
		}

		//
		//  Class terminate
		//  - clean up page object
		//
		public void Dispose()	{			

			// Close recordset
			if (Recordset != null)	{
				Recordset.Close();
				Recordset.Dispose();
			}
			Page_Terminate();
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
		Response.Buffer = EW_RESPONSE_BUFFER;
		Response.Cache.SetCacheability(HttpCacheability.NoCache);

		// Page main processing
		Personas_preview = new cPersonas_preview(this);
		Personas_preview.Page_Main();

		// Load filter
		string filter = ew_Get("f");
		if (filter.Trim() != "") filter = cTEA.Decrypt(filter.Trim(), EW_RANDOM_KEY); 
		if (filter == "") filter = "0=1"; 

		// Load recordset
		// Call Recordset Selecting event

		Personas.Recordset_Selecting(ref filter);
		Personas_preview.Recordset = Personas.LoadRs(filter);
		Personas_preview.TotalRecs = 0;
		if (Personas_preview.Recordset != null && Personas_preview.Recordset.HasRows)	{
			Personas_preview.TotalRecs = 0;
			while (Personas_preview.Recordset.Read())
				Personas_preview.TotalRecs++;
			Personas_preview.Recordset.Close();
			Personas_preview.Recordset = Personas.LoadRs(filter);
		}

		// Recordset Selected event
		Personas.Recordset_Selected(Personas_preview.Recordset);
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e)	{

		// Dispose page object
		Personas_preview.Dispose();
	}
}
