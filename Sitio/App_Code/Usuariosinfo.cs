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
// ASP.NET Maker 8 Project Class (Table)
//
public partial class AspNetMaker9_ControlVehicular: System.Web.UI.Page {

	public cUsuarios Usuarios;

	// Define table class
	public class cUsuarios: cTable, IDisposable {

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

		// Constructor
		public cUsuarios(AspNetMakerPage APage) {
			m_TableName = "Usuarios";
			m_TableObjName = "Usuarios";
			m_TableObjTypeName = "cUsuarios";
			m_TableType = "TABLE";
			m_Page = APage;
			if (APage != null)
				m_ParentPage = APage.ParentPage;
			AllowAddDeleteRow = ew_AllowAddDeleteRow(); // Allow add/delete row
		}		

		// Export Return Page
		public string ExportReturnUrl {
			get {
				if (ew_NotEmpty(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_EXPORT_RETURN_URL])) {
					return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_EXPORT_RETURN_URL]);
				} else {
					return ew_CurrentPage();
				}
			}
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_EXPORT_RETURN_URL] = value; }
		}

		// Records per page
		public int RecordsPerPage {
			get { return Convert.ToInt32(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_REC_PER_PAGE]); }
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_REC_PER_PAGE] = value; }
		}

		// Start record number
		public int StartRecordNumber {
			get { return Convert.ToInt32(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_START_REC]); }
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_START_REC] = value; }
		}

		// Search Highlight Name
		public string HighlightName {
			get { return "Usuarios_Highlight"; }
		}

		// Advanced search
		public string GetAdvancedSearch(string fld) {
			return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_ADVANCED_SEARCH + "_" + fld]);
		}

		public void SetAdvancedSearch(string fld, object v) {
			if (ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_ADVANCED_SEARCH + "_" + fld] != v)
				ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_ADVANCED_SEARCH + "_" + fld] = v;
		}

		public string BasicSearchKeyword = "";

		public string BasicSearchType = "";

		// Basic search keyword	
		public string SessionBasicSearchKeyword {
			get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_BASIC_SEARCH]); }
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_BASIC_SEARCH] = value; }
		}

		// Basic Search Type		
		public string SessionBasicSearchType {
			get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_BASIC_SEARCH_TYPE]); }
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_BASIC_SEARCH_TYPE] = value; }
		}

		// Search WHERE clause		
		public string SearchWhere {
			get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_SEARCH_WHERE]); }
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_SEARCH_WHERE] = value; }
		}		

		// Multiple column sort
		public void UpdateSort(cField ofld, bool ctrl) {
			string sSortField, sLastSort, sThisSort, sOrderBy;
			if (CurrentOrder == ofld.FldName)	{
				sSortField = ofld.FldExpression;
				sLastSort = ofld.Sort;
				if (CurrentOrderType == "ASC" || CurrentOrderType == "DESC") {
					sThisSort = CurrentOrderType;
				}	else {
					sThisSort = (sLastSort == "ASC") ? "DESC" : "ASC"; 
				}
				ofld.Sort = sThisSort;
				if (ctrl)	{
					sOrderBy = SessionOrderBy;
					if (sOrderBy.Contains(sSortField + " " + sLastSort)) {
						sOrderBy = sOrderBy.Replace(sSortField + " " + sLastSort, sSortField + " " + sThisSort);
					}	else {
						if (ew_NotEmpty(sOrderBy)) sOrderBy = sOrderBy + ", "; 
						sOrderBy = sOrderBy + sSortField + " " + sThisSort;
					}
					SessionOrderBy = sOrderBy; // Save to Session
				}	else {
					SessionOrderBy = sSortField + " " + sThisSort;	// Save to Session
				}
			}	else {
				if (!ctrl)
					ofld.Sort = ""; 
			}
		}

		// Session WHERE Clause
		public string SessionWhere {
			get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_WHERE]); }
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_WHERE] = value; }
		}

		// Session ORDER BY
		public string SessionOrderBy {
			get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_ORDER_BY]); }
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_ORDER_BY] = value; }
		}

		// Session key
		public object GetKey(string fld) {
			return ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_KEY + "_" + fld];
		}

		public void SetKey(string fld, object v) {
			ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_KEY + "_" + fld] = v;
		}

		// Table level SQL
		// Select
		public string SqlSelect {
			get { return "SELECT * FROM [dbo].[Usuarios]"; }
		}

		// Where
		public string SqlWhere {
			get {
				string sWhere = "";
				TableFilter = "";
				ew_AddFilter(ref sWhere, TableFilter);
				return sWhere;
			}
		}

		// Group By
		public string SqlGroupBy {
			get { return ""; }
		}

		// Having
		public string SqlHaving {
			get { return "";	}
		}

		// Order By
		public string SqlOrderBy {
			get { return ""; }
		}

		// SQL variables			
		public string CurrentFilter = ""; // Current filter			

		public string CurrentOrder = ""; // Current order			

		public string CurrentOrderType = ""; // Current order type

		// Get SQL
		public string GetSQL(string where, string orderby) {
			return ew_BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, where, orderby);
		}

		// Table SQL
		public string SQL {
			get {
				string sFilter = CurrentFilter;
				sFilter = ApplyUserIDFilters(sFilter); // Add User ID filter
				string sSort = SessionOrderBy;
				return ew_BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, sFilter, sSort);
			}
		}

		// Return table SQL with list page filter
		public string ListSQL {
			get {
				string sSort = "";
				string sFilter = SessionWhere;
				ew_AddFilter(ref sFilter, CurrentFilter);
				sFilter = ApplyUserIDFilters(sFilter); // Add User ID filter 
				sSort = SessionOrderBy;
				return ew_BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, sFilter, sSort);
			}
		}

		// Return SQL for record count
		public string SelectCountSQL {
			get {			
				if (TableType == "TABLE" || TableType == "VIEW") {
					return "SELECT COUNT(*) FROM" + ListSQL.Substring(13);
				} else {
					return "SELECT COUNT(*) FROM (" + ListSQL + ")";
				}				
			}
		}

		// Insert
		public int Insert(ref OrderedDictionary Rs)	{
			string names = "";
			string values = "";
			cField fld;
			foreach (DictionaryEntry f in Rs) {
				fld = FieldByName((string)f.Key);
				if (fld != null) {
					names += fld.FldExpression + ",";
					values += SqlParameter(ref fld) + ",";
				}
			}
			if (names.EndsWith(",")) names = names.Remove(names.Length - 1); 
			if (values.EndsWith(",")) values = values.Remove(values.Length - 1); 
			if (ew_Empty(names)) return -1;
			string Sql = "INSERT INTO [dbo].[Usuarios] (" + names + ") VALUES (" + values + ")";
			SqlCommand command = Conn.GetCommand(Sql);
			foreach (DictionaryEntry f in Rs) {
				fld = FieldByName((string)f.Key);
				if (fld == null)
					continue;
				try {
					command.Parameters.Add(fld.FldVar, fld.FldDbType).Value = ParameterValue(ref fld, f.Value);
				} catch {
					if (EW_DEBUG_ENABLED) throw;
				}
			}
			return command.ExecuteNonQuery();
		}

		// Update
		public int Update(ref OrderedDictionary Rs)	{
			string values = "";
			cField fld;
			foreach (DictionaryEntry f in Rs) {
				fld = FieldByName((string)f.Key);
				if (fld != null)
					values += fld.FldExpression + "=" + SqlParameter(ref fld) + ",";
			}
			if (values.EndsWith(",")) values = values.Remove(values.Length - 1); 
			if (ew_Empty(values)) return -1; 
			string Sql = "UPDATE [dbo].[Usuarios] SET " + values;
			if (ew_NotEmpty(CurrentFilter)) Sql += " WHERE " + CurrentFilter; 
			SqlCommand command = Conn.GetCommand(Sql);
			foreach (DictionaryEntry f in Rs) {
				fld = FieldByName((string)f.Key);
				if (fld == null)
					continue;
				try {
					command.Parameters.Add(fld.FldVar, fld.FldDbType).Value = ParameterValue(ref fld, f.Value);
				} catch {
					if (EW_DEBUG_ENABLED) throw;
				}
			}
			return command.ExecuteNonQuery();
		}

		// Convert to parameter name for use in SQL
		public string SqlParameter(ref cField fld) {
			string sValue = EW_DB_SQLPARAM_SYMBOL;
			if (EW_DB_SQLPARAM_SYMBOL != "?")
				sValue += fld.FldVar;
			return sValue;
		}

		// Convert value to object for parameter
		public object ParameterValue(ref cField fld, object value) {
			if (EW_ENCRYPTED_PASSWORD && ew_SameStr(fld.FldName, "Password")) {
				if (EW_CASE_SENSITIVE_PASSWORD) {
					return ew_EncryptPassword(Convert.ToString(value));
				} else {
					return ew_EncryptPassword(Convert.ToString(value).ToLower());
				}
			} else {
				return value;	
			}
		}

		// Delete
		public int Delete(OrderedDictionary Rs)	{
			cField fld;
			string Sql = "DELETE FROM [dbo].[Usuarios] WHERE ";
			fld = FieldByName("IdUsuario");
			Sql += fld.FldExpression + "=" + ew_QuotedValue(Rs["IdUsuario"], EW_DATATYPE_NUMBER) + " AND ";			
			if (Sql.EndsWith(" AND ")) Sql = Sql.Remove(Sql.Length - 5); 
			if (ew_NotEmpty(CurrentFilter)) Sql += " AND " + CurrentFilter; 
			return Conn.Execute(Sql);
		}

		// Key filter WHERE clause
		private string SqlKeyFilter {
			get { return "[IdUsuario] = @IdUsuario@"; }
		}

		// Key filter
		public string KeyFilter {
			get {
				string sKeyFilter = SqlKeyFilter;
				if (!Information.IsNumeric(IdUsuario.CurrentValue))	{
					sKeyFilter = "0=1";	// Invalid key
				}
				sKeyFilter = sKeyFilter.Replace("@IdUsuario@", ew_AdjustSql(IdUsuario.CurrentValue)); // Replace key value
				return sKeyFilter;
			}
		}	

		// Return URL
		public string ReturnUrl {
			get {

				// Get referer URL automatically
				if (HttpContext.Current.Request.ServerVariables["HTTP_REFERER"] != null) {
					if (ew_ReferPage() != ew_CurrentPage() && ew_ReferPage() != "login.aspx") { // Referer not same page or login page
						string url = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
						if (url.Contains("?a=")) { // Remove action
							int p1 = url.LastIndexOf("?a=");							
							int p2 = url.IndexOf("&", p1);							
							if (p2 > -1) {
								url = url.Substring(0, p1 + 1) + url.Substring(p2 + 1);
							} else {
								url = url.Substring(0, p1); 								
							}
						}
						ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_RETURN_URL] = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]; // Save to Session
					}
				}
				if (ew_NotEmpty(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_RETURN_URL]))	{
					return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_RETURN_URL]);
				}	else {
					return "Usuarioslist.aspx";
				}
			}
		}

		// List URL
		public string ListUrl {
			get { return "Usuarioslist.aspx"; }
		}

		// View URL
		public string ViewUrl	{
			get { return KeyUrl("Usuariosview.aspx", UrlParm("")); }
		}

		// Add URL
		public string AddUrl {
			get {
				string result = "Usuariosadd.aspx";

//				string sUrlParm = UrlParm("");
//				if (ew_NotEmpty(sUrlParm)) result = result + "?" + sUrlParm; 

				return result;
			}
		}

		// Edit URL
		public string EditUrl {
			get {
				return KeyUrl("Usuariosedit.aspx", UrlParm(""));
			}
		}

		// Edit URL
		public string GetEditUrl(string parm) {
			return KeyUrl("Usuariosedit.aspx", UrlParm(parm));
		}

		// Inline edit URL
		public string InlineEditUrl	{
			get { return KeyUrl(ew_CurrentPage(), UrlParm("a=edit")); }
		}

		// Copy URL
		public string CopyUrl	{
			get {
				return KeyUrl("Usuariosadd.aspx", UrlParm(""));
			}
		}

		// Copy URL
		public string GetCopyUrl(string parm) {
			return KeyUrl("Usuariosadd.aspx", UrlParm(parm));
		}

		// Inline copy URL
		public string InlineCopyUrl	{
			get { return KeyUrl(ew_CurrentPage(), UrlParm("a=copy")); }
		}

		// Delete URL
		public string DeleteUrl	{
			get { return KeyUrl("Usuariosdelete.aspx", UrlParm("")); }
		}

		// Key URL
		public string KeyUrl(string url, string parm)	{
			string sUrl = url + "?";
			if (ew_NotEmpty(parm)) sUrl += parm + "&"; 
			if (!Convert.IsDBNull(IdUsuario.CurrentValue)) {
				sUrl += "IdUsuario=" + IdUsuario.CurrentValue;
			} else {
				return "javascript:alert(ewLanguage.Phrase('InvalidRecord'));"; 
			}
			return sUrl;
		}

		// URL parm
		public string UrlParm(string parm) {
			string OutStr = "";
			if (UseTokenInUrl)
				OutStr = "t=Usuarios";
			if (ew_NotEmpty(parm)) {
				if (ew_NotEmpty(OutStr))
					OutStr += "&"; 
				OutStr += parm;
			}
			return OutStr;
		}

		// Get record keys from POST/GET/SESSION
		public ArrayList GetRecordKeys() {
			ArrayList arKeys = new ArrayList();
			if (HttpContext.Current.Request.Form["key_m"] != null)	{	// Key in form
				string[] keys = HttpContext.Current.Request.Form.GetValues("key_m");
				arKeys.AddRange(keys);
			} else if (HttpContext.Current.Request.QueryString["key_m"] != null) {
				string[] keys = HttpContext.Current.Request.QueryString.GetValues("key_m");
				arKeys.AddRange(keys);
			} else if (HttpContext.Current.Request.QueryString != null) {
				string keys = "";
				keys = ew_Get("IdUsuario"); // IdUsuario
				arKeys.Add(keys);

				//return arKeys; // Do not return yet, so the values will also be checked by the following code
			}
			ArrayList ar = new ArrayList();

			// Check keys
			foreach (object keys in arKeys) {
				if (!Information.IsNumeric(keys))
					continue;
				ar.Add(keys);
			}
			return ar;
		}

		// Get key filter
		public string GetKeyFilter() {
			ArrayList arKeys = GetRecordKeys();
			string sKeyFilter = "";
			foreach (object keys in arKeys) {
				if (ew_NotEmpty(sKeyFilter))
					sKeyFilter += " OR ";
				IdUsuario.CurrentValue = keys;
				sKeyFilter += "(" + KeyFilter + ")";
			}
			return sKeyFilter;
		}

		// Sort URL
		public string SortUrl(cField fld)	{
			string OutStr = "";
			if (ew_NotEmpty(CurrentAction) || ew_NotEmpty(Export) || (fld.FldType == 201 || fld.FldType == 203 || fld.FldType == 205 || fld.FldType == 141)) {
				OutStr = "";
			}	else if (fld.Sortable) { 
				OutStr = ew_CurrentPage();
				string sUrlParm = UrlParm("order=" + ew_UrlEncode(fld.FldName) + "&amp;ordertype=" + fld.ReverseSort());
				OutStr += "?" + sUrlParm;
			}
			return OutStr;
		}

		// Load rows based on filter
		public SqlDataReader LoadRs(string sFilter)	{
			SqlDataReader RsRows;

			// Set up filter (SQL WHERE clause)
			CurrentFilter = sFilter;
			string sSql = SQL;
			try {
				RsRows = Conn.GetDataReader(sSql);
				if (RsRows.HasRows) {
					return RsRows;
				} else {
					RsRows.Close();
					RsRows.Dispose();
				}
			}	catch {
			}
			return null;
		}

		// Load record count based on filter
		public int LoadRecordCount(string sFilter) {

			// Set up filter (SQL WHERE clause)
			CurrentFilter = sFilter;

			// Recordset Selecting event
			Recordset_Selecting(ref CurrentFilter);
			string sSql = SQL;
			string sSqlCnt;			
			if (TableType == "TABLE" || TableType == "VIEW") {
				sSqlCnt = "SELECT COUNT(*) FROM" + sSql.Substring(13);
			} else {
				sSqlCnt = "SELECT COUNT(*) FROM (" + sSql + ")";
			}

			// Try count SQL first
			try {
				return Convert.ToInt32(Conn.ExecuteScalar(sSqlCnt));
			} catch {}

			// Loop datareader to get count
			try {
				SqlDataReader Rs = Conn.GetTempDataReader(sSql);
				int Cnt = 0;
				while (Rs.Read())
					Cnt++;
				Rs.Close();		
				return Cnt;
			} catch {
				return 0;
			}
		}

		// Load row values from recordset
		public void LoadListRowValues(ref SqlDataReader RsRow) {
			IdUsuario.DbValue = RsRow["IdUsuario"];
			Usuario.DbValue = RsRow["Usuario"];
			NombreUsuario.DbValue = RsRow["NombreUsuario"];
			Password.DbValue = RsRow["Password"];
			Correo.DbValue = RsRow["Correo"];
			IdUsuarioNivel.DbValue = RsRow["IdUsuarioNivel"];
			Activo.DbValue = (ew_ConvertToBool(RsRow["Activo"])) ? "1" : "0";
		}

		// Render list row values
		public void RenderListRow() {

		//  Common render codes
			// IdUsuario
			// Usuario
			// NombreUsuario
			// Password
			// Correo
			// IdUsuarioNivel
			// Activo
			// Row Rendering event

			Row_Rendering();

		// Render for View
			// IdUsuario

				IdUsuario.ViewValue = IdUsuario.CurrentValue;
			IdUsuario.ViewCustomAttributes = "";

			// Usuario
				Usuario.ViewValue = Usuario.CurrentValue;
			Usuario.ViewCustomAttributes = "";

			// NombreUsuario
				NombreUsuario.ViewValue = NombreUsuario.CurrentValue;
			NombreUsuario.ViewCustomAttributes = "";

			// Password
				Password.ViewValue = "********";
			Password.ViewCustomAttributes = "";

			// Correo
				Correo.ViewValue = Correo.CurrentValue;
			Correo.ViewCustomAttributes = "";

			// IdUsuarioNivel
			if ((Security.CurrentUserLevel & EW_ALLOW_ADMIN) == EW_ALLOW_ADMIN) { // System admin
			if (ew_NotEmpty(IdUsuarioNivel.CurrentValue)) {
				sFilterWrk = "[UserLevelID] = " + ew_AdjustSql(IdUsuarioNivel.CurrentValue) + "";
			sSqlWrk = "SELECT [UserLevelName] FROM [dbo].[UserLevels]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					IdUsuarioNivel.ViewValue = drWrk["UserLevelName"];
				} else {
					IdUsuarioNivel.ViewValue = IdUsuarioNivel.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				IdUsuarioNivel.ViewValue = System.DBNull.Value;
			}
			} else {
				IdUsuarioNivel.ViewValue = "********";
			}
			IdUsuarioNivel.ViewCustomAttributes = "";

			// Activo
			if (Convert.ToString(Activo.CurrentValue) == "1") {
				Activo.ViewValue = (Activo.FldTagCaption(1) != "") ? Activo.FldTagCaption(1) : "Y";
			} else {
				Activo.ViewValue = (Activo.FldTagCaption(2) != "") ? Activo.FldTagCaption(2) : "N";
			}
			Activo.ViewCustomAttributes = "";

		// Render for View Refer	
			// IdUsuario

			IdUsuario.LinkCustomAttributes = "";
			IdUsuario.HrefValue = "";
			IdUsuario.TooltipValue = "";

			// Usuario
			Usuario.LinkCustomAttributes = "";
			Usuario.HrefValue = "";
			Usuario.TooltipValue = "";

			// NombreUsuario
			NombreUsuario.LinkCustomAttributes = "";
			NombreUsuario.HrefValue = "";
			NombreUsuario.TooltipValue = "";

			// Password
			Password.LinkCustomAttributes = "";
			Password.HrefValue = "";
			Password.TooltipValue = "";

			// Correo
			Correo.LinkCustomAttributes = "";
			Correo.HrefValue = "";
			Correo.TooltipValue = "";

			// IdUsuarioNivel
			IdUsuarioNivel.LinkCustomAttributes = "";
			IdUsuarioNivel.HrefValue = "";
			IdUsuarioNivel.TooltipValue = "";

			// Activo
			Activo.LinkCustomAttributes = "";
			Activo.HrefValue = "";
			Activo.TooltipValue = "";

			// Row Rendered event
			Row_Rendered();
		}

		// Aggregate list row values
		public void AggregateListRowValues() {
		}

		// Aggregate list row (for rendering)
		public void AggregateListRow() {
		}

		// Export data in XML format
		public void ExportXmlDocument(ref cXMLDocument XmlDoc, bool HasParent, ref SqlDataReader Recordset, int StartRec, int StopRec, string ExportPageType) {
			if (Recordset == null || XmlDoc == null)
				return;
			if (!HasParent)
				XmlDoc.AddRoot(TableVar);

			// Move to first record
			int RecCnt = StartRec - 1;
			for (int i = 1; i <= StartRec - 1; i++)
				Recordset.Read();	
			while (Recordset.Read() && RecCnt < StopRec) {
				RecCnt++;
				if (RecCnt >= StartRec) {
					int RowCnt = RecCnt - StartRec + 1;
					LoadListRowValues(ref Recordset);

					// Render row
					RowType = EW_ROWTYPE_VIEW; // Render view
					ResetAttrs();
					RenderListRow();
					if (HasParent)
						XmlDoc.AddRow(TableVar, "");
					else
						XmlDoc.AddRow("", "");
					if (ExportPageType == "view") {
						XmlDoc.AddField("IdUsuario", IdUsuario.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Usuario", Usuario.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("NombreUsuario", NombreUsuario.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Password", Password.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Correo", Correo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("IdUsuarioNivel", IdUsuarioNivel.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Activo", Activo.ExportValue(Export, ExportOriginalValue));
					} else {
						XmlDoc.AddField("IdUsuario", IdUsuario.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Usuario", Usuario.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("NombreUsuario", NombreUsuario.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Password", Password.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Correo", Correo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("IdUsuarioNivel", IdUsuarioNivel.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Activo", Activo.ExportValue(Export, ExportOriginalValue));
					}
				}
			}
		}

		// Export data in HTML/CSV/Word/Excel/Email/PDF format
		public void ExportDocument(ref cExportDocument Doc, ref SqlDataReader Recordset, int StartRec, int StopRec, string ExportPageType) {
			if (Recordset == null || Doc == null)
				return;

			// Write header
			Doc.ExportTableHeader();
			if (Doc.Horizontal) { // Horizontal format, write header
				Doc.BeginExportRow();				
				if (ExportPageType == "view") {
					Doc.ExportCaption(IdUsuario);
					Doc.ExportCaption(Usuario);
					Doc.ExportCaption(NombreUsuario);
					Doc.ExportCaption(Password);
					Doc.ExportCaption(Correo);
					Doc.ExportCaption(IdUsuarioNivel);
					Doc.ExportCaption(Activo);
				} else {
					Doc.ExportCaption(IdUsuario);
					Doc.ExportCaption(Usuario);
					Doc.ExportCaption(NombreUsuario);
					Doc.ExportCaption(Password);
					Doc.ExportCaption(Correo);
					Doc.ExportCaption(IdUsuarioNivel);
					Doc.ExportCaption(Activo);
				}
				Doc.EndExportRow(Export == "pdf");
			}

			// Move to first record
			int RecCnt = StartRec - 1;
			for (int i = 1; i <= StartRec - 1; i++)
				Recordset.Read();	
			while (Recordset.Read() && RecCnt < StopRec) {
				RecCnt++;
				if (RecCnt >= StartRec) {
					int RowCnt = RecCnt - StartRec + 1;

					// Page break for PDF
					if (Export == "pdf" && ExportPageBreakCount > 0) {
						if (RowCnt > 1 && (RowCnt - 1) % ExportPageBreakCount == 0)
							Doc.ExportPageBreak();
					}
					LoadListRowValues(ref Recordset);

					// Render row
					RowType = EW_ROWTYPE_VIEW; // Render view
					ResetAttrs();
					RenderListRow();
					Doc.BeginExportRow(RowCnt); // Allow CSS styles if enabled
					if (ExportPageType == "view") {
						Doc.ExportField(IdUsuario);
						Doc.ExportField(Usuario);
						Doc.ExportField(NombreUsuario);
						Doc.ExportField(Password);
						Doc.ExportField(Correo);
						Doc.ExportField(IdUsuarioNivel);
						Doc.ExportField(Activo);
					} else {
						Doc.ExportField(IdUsuario);
						Doc.ExportField(Usuario);
						Doc.ExportField(NombreUsuario);
						Doc.ExportField(Password);
						Doc.ExportField(Correo);
						Doc.ExportField(IdUsuarioNivel);
						Doc.ExportField(Activo);
					}
					Doc.EndExportRow(false);
				}
			}
			Doc.ExportTableFooter();
		}

		// Check if Anonymous User is allowed
	  private bool AllowAnonymousUser() {
			switch (Page.PageID) {
				case "add":
				case "register":
				case "addopt":
					return false;
				case "edit":
				case "update":
					return false;
				case "delete":
					return false;
				case "view":
					return false;
				case "search":
					return false;
				default:
					return false;
			}
		}

		// Apply User ID filter
		public string ApplyUserIDFilters(string Filter) {
			string sFilter = Filter;
			if (!AllowAnonymousUser() && ew_NotEmpty(Security.CurrentUserID) && !Security.IsAdmin()) { // Not allow anonymous / Non system 
				sFilter = AddUserIDFilter(sFilter); // Add user id filter
			}
			return sFilter;
		}

		// User ID filter
		public string UserIDFilter(object userid) {
			string sUserIDFilter = "[IdUsuario] = " + ew_QuotedValue(userid, EW_DATATYPE_NUMBER);
			return sUserIDFilter;
		}

		// Add User ID filter
		public string AddUserIDFilter(string sFilter) {
			string sFilterWrk;
			if (!Security.IsAdmin()) {
				sFilterWrk = Security.UserIDList();
				if (ew_NotEmpty(sFilterWrk))
					sFilterWrk = "[IdUsuario] IN (" + sFilterWrk + ")";
				ew_AddFilter(ref sFilterWrk, sFilter);
			} else {
				sFilterWrk = sFilter;
			}
			return sFilterWrk;
		}		

		// Get User ID subquery
		public string GetUserIDSubquery(cField fld, cField masterfld)	{
			string sWrk = "";
			SqlDataReader RsUser;
			string sSql = "SELECT " + masterfld.FldExpression + " FROM [dbo].[Usuarios] WHERE " + AddUserIDFilter("");
			if (EW_USE_SUBQUERY_FOR_MASTER_USER_ID)	{	// Use subquery
				sWrk = sSql;
			}	else { // List all values
				RsUser = Conn.GetTempDataReader(sSql);
				try {
					while (RsUser.Read()) {
						if (ew_NotEmpty(sWrk)) sWrk += ","; 
						sWrk += ew_QuotedValue(RsUser[0], masterfld.FldDataType);
					}
				}	finally {
					Conn.CloseTempDataReader();
				}
			}
			if (ew_NotEmpty(sWrk))
				sWrk = fld.FldExpression + " IN (" + sWrk + ")";
			return sWrk;
		}		

		public string CurrentAction = ""; // Current action

		public string UpdateConflict = ""; // Update conflict			

		public bool EventCancelled; // Event cancelled			

		public string CancelMessage = ""; // Cancel message

		// Action
		public string LastAction = ""; // Last action

		public string CurrentMode = ""; // Current mode

		// Detail Add/Edit
		public bool DetailAdd = false; // Allow detail add

		public bool DetailEdit = false; // Allow detail edit

		public int GridAddRowCount = 5;

		public bool AllowAddDeleteRow = true; // Allow add/delete row

		//
		// Check current action
		//
		// Add
		public bool IsAdd {
			get { return CurrentAction == "add"; }
		}

		// Copy
		public bool IsCopy {
			get { return CurrentAction == "copy" || CurrentAction == "C"; }
		}

		// Edit
		public bool IsEdit {
			get { return CurrentAction == "edit"; }
		}

		// Delete
		public bool IsDelete {
			get { return CurrentAction == "D"; }
		}

		// Confirm
		public bool IsConfirm {
			get { return CurrentAction == "F"; }
		}

		// Overwrite
		public bool IsOverwrite {
			get { return CurrentAction == "overwrite"; }
		}

		// Cancel
		public bool IsCancel {
			get { return CurrentAction == "cancel"; }
		}

		// Grid add
		public bool IsGridAdd {
			get { return CurrentAction == "gridadd"; }
		}

		// Grid edit
		public bool IsGridEdit {
			get { return CurrentAction == "gridedit"; }
		}

		// - Insert
		public bool IsInsert {
			get { return CurrentAction == "insert" || CurrentAction == "A"; }
		}

		// Update
		public bool IsUpdate {
			get { return CurrentAction == "update" || CurrentAction == "U"; }
		}

		// Grid Update
		public bool IsGridUpdate {
			get { return CurrentAction == "gridupdate"; }
		}

		// Grid Insert
		public bool IsGridInsert {
			get { return CurrentAction == "gridinsert"; }
		}

		// Grid Overwrite
		public bool IsGridOverwrite {
			get { return CurrentAction == "gridoverwrite"; }
		}

		//
		// Check last action
		//
		// Cancelled
		public bool IsCanceled {
			get { return LastAction == "cancel" && CurrentAction == ""; }
		}

		// Inline Inserted
		public bool IsInlineInserted {
			get { return LastAction == "insert" && CurrentAction == ""; }
		}

		// Inline Updated
		public bool IsInlineUpdated {
			get { return LastAction == "update" && CurrentAction == ""; }
		}

		// Grid Updated
		public bool IsGridUpdated {
			get { return LastAction == "gridupdate" && CurrentAction == ""; }
		}

		// Grid Inserted
		public bool IsGridInserted {
			get { return LastAction == "gridinsert" && CurrentAction == ""; }
		}

		// Export all
		public bool ExportAll = true; 

		// Export page break count
		public int ExportPageBreakCount = 0; // Page break per every n record (PDF only)

		// IdUsuario
		private cField m_IdUsuario;

		public cField IdUsuario {
			get {
				if (m_IdUsuario == null) {
					m_IdUsuario = new cField(ref m_Page, "Usuarios", "Usuarios", "x_IdUsuario", "IdUsuario", "[IdUsuario]", 3, SqlDbType.Int, EW_DATATYPE_NUMBER,  -1, false, "FORMATTED TEXT");
					m_IdUsuario.FldDefaultErrMsg = Language.Phrase("IncorrectInteger");
					m_IdUsuario.FldForceSelection = false;
					m_IdUsuario.FldViewTag = "FORMATTED TEXT";
					Fields.Add("IdUsuario", m_IdUsuario); // Add to field list
				}				 
				return m_IdUsuario;
			}
		}

		// Usuario
		private cField m_Usuario;

		public cField Usuario {
			get {
				if (m_Usuario == null) {
					m_Usuario = new cField(ref m_Page, "Usuarios", "Usuarios", "x_Usuario", "Usuario", "[Usuario]", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Usuario.FldForceSelection = false;
					m_Usuario.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Usuario", m_Usuario); // Add to field list
				}				 
				return m_Usuario;
			}
		}

		// NombreUsuario
		private cField m_NombreUsuario;

		public cField NombreUsuario {
			get {
				if (m_NombreUsuario == null) {
					m_NombreUsuario = new cField(ref m_Page, "Usuarios", "Usuarios", "x_NombreUsuario", "NombreUsuario", "[NombreUsuario]", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_NombreUsuario.FldForceSelection = false;
					m_NombreUsuario.FldViewTag = "FORMATTED TEXT";
					Fields.Add("NombreUsuario", m_NombreUsuario); // Add to field list
				}				 
				return m_NombreUsuario;
			}
		}

		// Password
		private cField m_Password;

		public cField Password {
			get {
				if (m_Password == null) {
					m_Password = new cField(ref m_Page, "Usuarios", "Usuarios", "x_Password", "Password", "[Password]", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Password.FldForceSelection = false;
					m_Password.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Password", m_Password); // Add to field list
				}				 
				return m_Password;
			}
		}

		// Correo
		private cField m_Correo;

		public cField Correo {
			get {
				if (m_Correo == null) {
					m_Correo = new cField(ref m_Page, "Usuarios", "Usuarios", "x_Correo", "Correo", "[Correo]", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Correo.FldDefaultErrMsg = Language.Phrase("IncorrectEmail");
					m_Correo.FldForceSelection = false;
					m_Correo.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Correo", m_Correo); // Add to field list
				}				 
				return m_Correo;
			}
		}

		// IdUsuarioNivel
		private cField m_IdUsuarioNivel;

		public cField IdUsuarioNivel {
			get {
				if (m_IdUsuarioNivel == null) {
					m_IdUsuarioNivel = new cField(ref m_Page, "Usuarios", "Usuarios", "x_IdUsuarioNivel", "IdUsuarioNivel", "[IdUsuarioNivel]", 3, SqlDbType.Int, EW_DATATYPE_NUMBER,  -1, false, "FORMATTED TEXT");
					m_IdUsuarioNivel.FldDefaultErrMsg = Language.Phrase("IncorrectInteger");
					m_IdUsuarioNivel.FldForceSelection = false;
					m_IdUsuarioNivel.FldViewTag = "FORMATTED TEXT";
					Fields.Add("IdUsuarioNivel", m_IdUsuarioNivel); // Add to field list
				}				 
				return m_IdUsuarioNivel;
			}
		}

		// Activo
		private cField m_Activo;

		public cField Activo {
			get {
				if (m_Activo == null) {
					m_Activo = new cField(ref m_Page, "Usuarios", "Usuarios", "x_Activo", "Activo", "[Activo]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_Activo.FldForceSelection = false;
					m_Activo.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Activo", m_Activo); // Add to field list
				}				 
				return m_Activo;
			}
		}

		//  Get field object by name
		public cField FieldByName(string Name) {
			if (Fields.ContainsKey(Name))
				return Fields[Name]; 
			if (Name == "IdUsuario") return IdUsuario;
			if (Name == "Usuario") return Usuario;
			if (Name == "NombreUsuario") return NombreUsuario;
			if (Name == "Password") return Password;
			if (Name == "Correo") return Correo;
			if (Name == "IdUsuarioNivel") return IdUsuarioNivel;
			if (Name == "Activo") return Activo;
			return null;
		}

		// Table level events
		// Recordset Selecting event
		public void Recordset_Selecting(ref string filter) { 

			// Enter your code here	
		}

		// Recordset Selected event
		public void Recordset_Selected(DbDataReader rs) {

			//HttpContext.Current.Response.Write("Recordset Selected");
		}

		// Recordset Search Validated event
		public void Recordset_SearchValidated() {

			// Enter your code here
		}

		// Recordset Searching event
		public void Recordset_Searching(ref string filter) {

			// Enter your code here
		}

		// Row_Selecting event
		public void Row_Selecting(ref string filter) {

			// Enter your code here	
		}

		// Row Selected event
		public void Row_Selected(ref OrderedDictionary od) {

			//HttpContext.Current.Response.Write("Row Selected");
		}

		// Row Inserting event
		public bool Row_Inserting(OrderedDictionary rsold, ref OrderedDictionary rsnew) {

			// Enter your code here
			// To cancel, set return value to False and error message to CancelMessage

			return true;
		}

		// Row Inserted event
		public void Row_Inserted(OrderedDictionary rsold, OrderedDictionary rsnew) {

			//HttpContext.Current.Response.Write("Row Inserted");
		}

		// Row Updating event
		public bool Row_Updating(OrderedDictionary rsold, ref OrderedDictionary rsnew) {

			// Enter your code here
			// To cancel, set return value to False and error message to CancelMessage

			return true;
		}

		// Row Updated event
		public void Row_Updated(OrderedDictionary rsold, OrderedDictionary rsnew) {

			//HttpContext.Current.Response.Write("Row Updated");
		}

		// Row Update Conflict event
		public bool Row_UpdateConflict(OrderedDictionary rsold, ref OrderedDictionary rsnew) {

			// Enter your code here
			// To ignore conflict, set return value to false

			return true;
		}

		// Recordset Deleting event
		public bool Row_Deleting(OrderedDictionary rs) {

			// Enter your code here
			// To cancel, set return value to False and error message to CancelMessage

			return true;
		}

		// Recordset Deleted event
		public void Row_Deleted(OrderedDictionary rs) {

			//HttpContext.Current.Response.Write("Row Deleted");
		}

		// Email Sending event
		public bool Email_Sending(ref cEmail Email, Hashtable Args) {

			//HttpContext.Current.Response.Write(Email.AsString());
			//HttpContext.Current.Response.End();

			return true;
		}

		// Row Rendering event
		public void Row_Rendering() {

			// Enter your code here	
		}

		// Row Rendered event
		public void Row_Rendered() {

			// To view properties of field class, use:
			//HttpContext.Current.Response.Write(<FieldName>.AsString());

		}

		// Class terminate
		public void Dispose() {
			if (m_IdUsuario != null) m_IdUsuario.Dispose();
			if (m_Usuario != null) m_Usuario.Dispose();
			if (m_NombreUsuario != null) m_NombreUsuario.Dispose();
			if (m_Password != null) m_Password.Dispose();
			if (m_Correo != null) m_Correo.Dispose();
			if (m_IdUsuarioNivel != null) m_IdUsuarioNivel.Dispose();
			if (m_Activo != null) m_Activo.Dispose();
		}
	}
}
