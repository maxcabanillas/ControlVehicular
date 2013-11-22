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
// ASP.NET Maker 8 Project Class
//
public partial class AspNetMaker9_ControlVehicular: System.Web.UI.Page {

	//
	// Global variables
	//
	// Connection object
	public cConnection Conn;

	// Security object
	public cAdvancedSecurity Security;

	// Form object
	public cFormObj ObjForm;

	// Language
	public cLanguage Language;

	public static string gsLanguage; 

	// Timer
	public long StartTimer;

	// Page and Table objects
	public static AspNetMakerPage CurrentPage = null; // Main page object // C#

	public static cTable CurrentTable = null; // Main table object // C#

	public static AspNetMakerPage MasterPage = null; // Master page object // C#

	public static cTable MasterTable = null; // Master table object // C#

	// Page and Table object types // ASPX
//	public static Type CurrentPageType; // Main page type
//	public static Type CurrentTableType; // Main table type
//	public static Type MasterPageType; // Master page type
//	public static Type MasterTableType; // Master table type
	// Used by ValidateForm/ValidateSearch
	public static string gsFormError = "";

	public static string gsSearchError = "";

	// Used by *master.ascx
	public static string gsMasterReturnUrl = "";

	// Used for export checking
	public static string gsExport = "";

	public static string gsExportFile = "";

	public static string gsEmailSender = "";

	public static string gsEmailRecipient = "";

	public static string gsEmailCc = "";

	public static string gsEmailBcc = "";

	public static string gsEmailSubject = "";

	public static string gsEmailContent = "";

	public static string gsEmailContentType = "";

	public static string gsEmailErrNo = "";

	public static string gsEmailErrDesc = "";

	// Debug message
	public static string gsDebugMsg = "";

	// Keep temp images name for PDF export for delete
	public static List<string> gTmpImages = new List<string>();

	// Temp variables used by system generated functions		
	public ArrayList RsWrk; // ArrayList of OrderedDictionary

	public SqlDataReader drWrk; // DataReader

	public string sSqlWrk;

	public string sWhereWrk;

	public string jswrk;

	public string selwrk;

	public bool emptywrk;

	public static string sLookupTblFilter = "";

	public string[] arwrk;

	public ArrayList alwrk;

	public OrderedDictionary odwrk;

	public string[] armultiwrk;	

	// Global user functions
	// Page Loading event
	public void Page_Loading() {

		//HttpContext.Current.Response.Write("Page Loading");
	}

	// Page Unloaded event
	public void Page_Unloaded() {

		//HttpContext.Current.Response.Write("Page Unloaded");
	}

	public void Menu_Rendering(ref cMenu Menu) {

		// Change menu items here
	}

	public bool MenuItem_Adding(ref cMenuItem Item) {

		//HttpContext.Current.Response.Write(Item.AsString());
		// Return False if menu item not allowed

		return true;
	}
}
