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
// ASP.NET code-behind class (Lookup)
//

partial class ewlookup9: AspNetMaker9_ControlVehicular {

	public clookup lookup;

	// Page class for lookup
	public class clookup : AspNetMakerPage
	{

		// Constructor
		public clookup(AspNetMaker9_ControlVehicular APage)
		{
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "lookup";
			m_PageObjName = "lookup";
			m_PageObjTypeName = "clookup";
		}

		// Page URL
		public string PageUrl {
			get { return ew_CurrentPage() + "?"; }
		}

		// Main
		public void Page_Main()
		{
			if (HttpContext.Current.Request.QueryString.Count > 0) {
				string Sql = ew_Get("s");
				Sql = cTEA.Decrypt(Sql, EW_RANDOM_KEY);
				if (ew_NotEmpty(Sql)) {

					// Get the filter values (for "IN")
					string Value = ew_AdjustSql(ew_Get("f"));
					if (ew_NotEmpty(Value)) {
						string[] arValue = Value.Split(new char[] {','});
						int FldType = ew_ConvertToInt(ew_Get("lft"));	// Filter field data type
						for (int ari = 0; ari <= arValue.GetUpperBound(0); ari++) {
							arValue[ari] = ew_QuotedValue(arValue[ari], FldType);
						}
						Sql = Sql.Replace("{filter_value}", String.Join(",", arValue));
					}

					// Get the query value (for "LIKE" or "=")
					Value = ew_AdjustSql(ew_Get("q"));	
					if (ew_NotEmpty(Value)) {
						if (Sql.Contains("LIKE '{query_value}%'")) {
							Sql = Sql.Replace("LIKE '{query_value}%'", ew_Like("'" + Value + "%'"));
						} else {
							Sql = Sql.Replace("{query_value}", Value);
						}
					}

					// Get the lookup values
					if (!Sql.Contains("{filter_value}") && !Sql.Contains("{query_value}"))
						GetLookupValues(Sql);
				}
			}
		}

		// Get values from database
		private void GetLookupValues(string Sql)
		{
			ArrayList RsArr;
			string str;

			// Connect to database
			Conn = new cConnection();
			try {
				RsArr = Conn.GetRows(Sql);
			}	finally {
				Conn.Dispose();
			}

			// Output		
			foreach (OrderedDictionary Row in RsArr) {
				foreach (DictionaryEntry f in Row) {
					str = Convert.ToString(f.Value);
					str = RemoveDelimiters(str);
					ew_Write(str + EW_FIELD_DELIMITER);
				}
				ew_Write(EW_RECORD_DELIMITER);
			}
		}

		// Process values
		private string RemoveDelimiters(string str)
		{
			string wrkstr = str;
			if (wrkstr.Length > 0) {
				wrkstr = wrkstr.Replace("\r", " ");
				wrkstr = wrkstr.Replace("\n", " ");
				wrkstr = wrkstr.Replace(EW_RECORD_DELIMITER, "");
				wrkstr = wrkstr.Replace(EW_FIELD_DELIMITER, "");
			}
			return wrkstr;
		}
	}

	// ASP.NET Page_Load event
	protected void Page_Load(object sender, System.EventArgs e)
	{
		ew_Header(false);
		lookup = new clookup(this);
		lookup.Page_Main();
	}
}
