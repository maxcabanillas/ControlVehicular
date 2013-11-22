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

partial class VehiculosAutorizadospreview: AspNetMaker9_ControlVehicular {

	// Page object
	public cVehiculosAutorizados_preview VehiculosAutorizados_preview;

	// Page class for preview
	public class cVehiculosAutorizados_preview: AspNetMakerPage, IDisposable {

		public int TotalRecs;

		public int RowCount;

		public SqlDataReader Recordset;			

		// Constructor
		public cVehiculosAutorizados_preview(AspNetMaker9_ControlVehicular APage) {
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
			if (VehiculosAutorizados.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (VehiculosAutorizados.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (VehiculosAutorizados.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// VehiculosAutorizados
		public cVehiculosAutorizados VehiculosAutorizados {
			get { return ParentPage.VehiculosAutorizados; }
			set { ParentPage.VehiculosAutorizados = value; }
		}

		// Personas
		public cPersonas Personas {
			get { return ParentPage.Personas; }
			set { ParentPage.Personas = value; }
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
			if (VehiculosAutorizados == null)		
				VehiculosAutorizados = new cVehiculosAutorizados(this);
			if (Personas == null)		
				Personas = new cPersonas(this);
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
			Security.LoadCurrentUserLevel("VehiculosAutorizados");

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
			VehiculosAutorizados.Dispose();
			Personas.Dispose();
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
		VehiculosAutorizados_preview = new cVehiculosAutorizados_preview(this);
		VehiculosAutorizados_preview.Page_Main();

		// Load filter
		string filter = ew_Get("f");
		if (filter.Trim() != "") filter = cTEA.Decrypt(filter.Trim(), EW_RANDOM_KEY); 
		if (filter == "") filter = "0=1"; 

		// Load recordset
		// Call Recordset Selecting event

		VehiculosAutorizados.Recordset_Selecting(ref filter);
		VehiculosAutorizados_preview.Recordset = VehiculosAutorizados.LoadRs(filter);
		VehiculosAutorizados_preview.TotalRecs = 0;
		if (VehiculosAutorizados_preview.Recordset != null && VehiculosAutorizados_preview.Recordset.HasRows)	{
			VehiculosAutorizados_preview.TotalRecs = 0;
			while (VehiculosAutorizados_preview.Recordset.Read())
				VehiculosAutorizados_preview.TotalRecs++;
			VehiculosAutorizados_preview.Recordset.Close();
			VehiculosAutorizados_preview.Recordset = VehiculosAutorizados.LoadRs(filter);
		}

		// Recordset Selected event
		VehiculosAutorizados.Recordset_Selected(VehiculosAutorizados_preview.Recordset);
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e)	{

		// Dispose page object
		VehiculosAutorizados_preview.Dispose();
	}
}
