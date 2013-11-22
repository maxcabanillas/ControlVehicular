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
// ASP.NET code-behind class (Master Page)
//

partial class MasterPage : System.Web.UI.MasterPage {

	public AspNetMaker9_ControlVehicular ParentPage;

	public string sExport = "";

	public bool NotExport = true;

	public bool PrintOrNotExport = true;

	public AspNetMaker9_ControlVehicular.cLanguage Language;

	//
	// Master page Page_Init event
	//

	protected void Page_Init(object sender, System.EventArgs e)	{
		if (this.Page is AspNetMaker9_ControlVehicular)
			ParentPage = (AspNetMaker9_ControlVehicular)this.Page;
		ParentPage.StartTimer = Environment.TickCount;
	}

	//
	// Master page Page_Load event
	//

	protected void Page_Load(object sender, EventArgs e) {
		if (ParentPage.Language == null)
			ParentPage.Language = new AspNetMaker9_ControlVehicular.cLanguage(new AspNetMaker9_ControlVehicular.AspNetMakerPage());
		Language = ParentPage.Language;
		if (!AspNetMaker9_ControlVehicular.ew_Empty(AspNetMaker9_ControlVehicular.ew_Get("export"))) {
			sExport = AspNetMaker9_ControlVehicular.ew_Get("export");
		} else if (!AspNetMaker9_ControlVehicular.ew_Empty(AspNetMaker9_ControlVehicular.ew_Post("exporttype"))) {
			sExport = AspNetMaker9_ControlVehicular.ew_Post("exporttype");
		}

		// Load Export Request
		NotExport = AspNetMaker9_ControlVehicular.ew_Empty(sExport);
		PrintOrNotExport = (AspNetMaker9_ControlVehicular.ew_Empty(sExport) || sExport == "print");
		Menu.Visible = NotExport;
	}
}
