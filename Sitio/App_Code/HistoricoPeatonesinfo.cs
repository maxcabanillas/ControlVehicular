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

	public cHistoricoPeatones HistoricoPeatones;

	// Define table class
	public class cHistoricoPeatones: cTable, IDisposable {

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
		public cHistoricoPeatones(AspNetMakerPage APage) {
			m_TableName = "HistoricoPeatones";
			m_TableObjName = "HistoricoPeatones";
			m_TableObjTypeName = "cHistoricoPeatones";
			m_TableType = "CUSTOMVIEW";
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
			get { return "HistoricoPeatones_Highlight"; }
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

		// Single column sort
		public void UpdateSort(cField ofld)	{
			string sLastSort, sSortField, sThisSort;
			if (CurrentOrder == ofld.FldName)	{
				sSortField = ofld.FldExpression;
				sLastSort = ofld.Sort;
				if (CurrentOrderType == "ASC" || CurrentOrderType == "DESC")	{
					sThisSort = CurrentOrderType;
				}	else {
					sThisSort = (sLastSort == "ASC") ? "DESC" : "ASC"; 
				}
				ofld.Sort = sThisSort;
				SessionOrderBy = sSortField + " " + sThisSort;	// Save to Session
			}	else	{
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
			get { return "SELECT dbo.TiposDocumentos.TipoDocumento, dbo.RegistrosPeatones.Documento, dbo.RegistrosPeatones.Nombre, dbo.RegistrosPeatones.Apellidos, dbo.Areas.Area, dbo.Personas.Persona, dbo.RegistrosPeatones.FechaIngreso, dbo.RegistrosPeatones.FechaSalida, dbo.RegistrosPeatones.Observacion FROM dbo.RegistrosPeatones INNER JOIN dbo.Areas ON dbo.RegistrosPeatones.IdArea = dbo.Areas.IdArea INNER JOIN dbo.Personas ON dbo.RegistrosPeatones.IdPersona = dbo.Personas.IdPersona INNER JOIN dbo.TiposDocumentos ON dbo.RegistrosPeatones.IdTipoDocumento = dbo.TiposDocumentos.IdTipoDocumento"; }
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
			get { return "dbo.RegistrosPeatones.FechaIngreso DESC"; }
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
			string Sql = "INSERT INTO dbo.RegistrosPeatones INNER JOIN dbo.Areas ON dbo.RegistrosPeatones.IdArea = dbo.Areas.IdArea INNER JOIN dbo.Personas ON dbo.RegistrosPeatones.IdPersona = dbo.Personas.IdPersona INNER JOIN dbo.TiposDocumentos ON dbo.RegistrosPeatones.IdTipoDocumento = dbo.TiposDocumentos.IdTipoDocumento (" + names + ") VALUES (" + values + ")";
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
			string Sql = "UPDATE dbo.RegistrosPeatones INNER JOIN dbo.Areas ON dbo.RegistrosPeatones.IdArea = dbo.Areas.IdArea INNER JOIN dbo.Personas ON dbo.RegistrosPeatones.IdPersona = dbo.Personas.IdPersona INNER JOIN dbo.TiposDocumentos ON dbo.RegistrosPeatones.IdTipoDocumento = dbo.TiposDocumentos.IdTipoDocumento SET " + values;
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
			return value;	
		}

		// Delete
		public int Delete(OrderedDictionary Rs)	{
			cField fld;
			string Sql = "DELETE FROM dbo.RegistrosPeatones INNER JOIN dbo.Areas ON dbo.RegistrosPeatones.IdArea = dbo.Areas.IdArea INNER JOIN dbo.Personas ON dbo.RegistrosPeatones.IdPersona = dbo.Personas.IdPersona INNER JOIN dbo.TiposDocumentos ON dbo.RegistrosPeatones.IdTipoDocumento = dbo.TiposDocumentos.IdTipoDocumento WHERE ";
			if (Sql.EndsWith(" AND ")) Sql = Sql.Remove(Sql.Length - 5); 
			if (ew_NotEmpty(CurrentFilter)) Sql += " AND " + CurrentFilter; 
			return Conn.Execute(Sql);
		}

		// Key filter WHERE clause
		private string SqlKeyFilter {
			get { return ""; }
		}

		// Key filter
		public string KeyFilter {
			get {
				string sKeyFilter = SqlKeyFilter;
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
					return "HistoricoPeatoneslist.aspx";
				}
			}
		}

		// List URL
		public string ListUrl {
			get { return "HistoricoPeatoneslist.aspx"; }
		}

		// View URL
		public string ViewUrl	{
			get { return KeyUrl("HistoricoPeatonesview.aspx", UrlParm("")); }
		}

		// Add URL
		public string AddUrl {
			get {
				string result = "HistoricoPeatonesadd.aspx";

//				string sUrlParm = UrlParm("");
//				if (ew_NotEmpty(sUrlParm)) result = result + "?" + sUrlParm; 

				return result;
			}
		}

		// Edit URL
		public string EditUrl {
			get {
				return KeyUrl("HistoricoPeatonesedit.aspx", UrlParm(""));
			}
		}

		// Edit URL
		public string GetEditUrl(string parm) {
			return KeyUrl("HistoricoPeatonesedit.aspx", UrlParm(parm));
		}

		// Inline edit URL
		public string InlineEditUrl	{
			get { return KeyUrl(ew_CurrentPage(), UrlParm("a=edit")); }
		}

		// Copy URL
		public string CopyUrl	{
			get {
				return KeyUrl("HistoricoPeatonesadd.aspx", UrlParm(""));
			}
		}

		// Copy URL
		public string GetCopyUrl(string parm) {
			return KeyUrl("HistoricoPeatonesadd.aspx", UrlParm(parm));
		}

		// Inline copy URL
		public string InlineCopyUrl	{
			get { return KeyUrl(ew_CurrentPage(), UrlParm("a=copy")); }
		}

		// Delete URL
		public string DeleteUrl	{
			get { return KeyUrl("HistoricoPeatonesdelete.aspx", UrlParm("")); }
		}

		// Key URL
		public string KeyUrl(string url, string parm)	{
			string sUrl = url + "?";
			if (ew_NotEmpty(parm)) sUrl += parm + "&"; 
			return sUrl;
		}

		// URL parm
		public string UrlParm(string parm) {
			string OutStr = "";
			if (UseTokenInUrl)
				OutStr = "t=HistoricoPeatones";
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
				arKeys.Add(keys);

				//return arKeys; // Do not return yet, so the values will also be checked by the following code
			}
			ArrayList ar = new ArrayList();

			// Check keys
			foreach (object keys in arKeys) {
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
			TipoDocumento.DbValue = RsRow["TipoDocumento"];
			Documento.DbValue = RsRow["Documento"];
			Nombre.DbValue = RsRow["Nombre"];
			Apellidos.DbValue = RsRow["Apellidos"];
			Area.DbValue = RsRow["Area"];
			Persona.DbValue = RsRow["Persona"];
			FechaIngreso.DbValue = RsRow["FechaIngreso"];
			FechaSalida.DbValue = RsRow["FechaSalida"];
			Observacion.DbValue = RsRow["Observacion"];
		}

		// Render list row values
		public void RenderListRow() {

		//  Common render codes
			// TipoDocumento
			// Documento
			// Nombre
			// Apellidos
			// Area
			// Persona
			// FechaIngreso
			// FechaSalida
			// Observacion
			// Row Rendering event

			Row_Rendering();

		// Render for View
			// TipoDocumento

			TipoDocumento.ViewCustomAttributes = "";

			// Documento
				Documento.ViewValue = Documento.CurrentValue;
			Documento.ViewCustomAttributes = "";

			// Nombre
				Nombre.ViewValue = Nombre.CurrentValue;
			Nombre.ViewCustomAttributes = "";

			// Apellidos
				Apellidos.ViewValue = Apellidos.CurrentValue;
			Apellidos.ViewCustomAttributes = "";

			// Area
			if (ew_NotEmpty(Area.CurrentValue)) {
				sFilterWrk = "[Area] = '" + ew_AdjustSql(Area.CurrentValue) + "'";
			sSqlWrk = "SELECT [Area], [Codigo] FROM [dbo].[Areas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Area]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Area.ViewValue = drWrk["Area"];
					Area.ViewValue = String.Concat(Area.ViewValue, ew_ValueSeparator(0, 1, Area), drWrk["Codigo"]);
				} else {
					Area.ViewValue = Area.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Area.ViewValue = System.DBNull.Value;
			}
			Area.ViewCustomAttributes = "";

			// Persona
			if (ew_NotEmpty(Persona.CurrentValue)) {
				sFilterWrk = "[Persona] = '" + ew_AdjustSql(Persona.CurrentValue) + "'";
			sSqlWrk = "SELECT [Persona] FROM [dbo].[Personas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Persona]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Persona.ViewValue = drWrk["Persona"];
				} else {
					Persona.ViewValue = Persona.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Persona.ViewValue = System.DBNull.Value;
			}
			Persona.ViewCustomAttributes = "";

			// FechaIngreso
				FechaIngreso.ViewValue = FechaIngreso.CurrentValue;
				FechaIngreso.ViewValue = ew_FormatDateTime(FechaIngreso.ViewValue, 7);
			FechaIngreso.ViewCustomAttributes = "";

			// FechaSalida
				FechaSalida.ViewValue = FechaSalida.CurrentValue;
				FechaSalida.ViewValue = ew_FormatDateTime(FechaSalida.ViewValue, 7);
			FechaSalida.ViewCustomAttributes = "";

			// Observacion
			Observacion.ViewValue = Observacion.CurrentValue;
			Observacion.ViewCustomAttributes = "";

		// Render for View Refer	
			// TipoDocumento

			TipoDocumento.LinkCustomAttributes = "";
			TipoDocumento.HrefValue = "";
			TipoDocumento.TooltipValue = "";

			// Documento
			Documento.LinkCustomAttributes = "";
			Documento.HrefValue = "";
			Documento.TooltipValue = "";

			// Nombre
			Nombre.LinkCustomAttributes = "";
			Nombre.HrefValue = "";
			Nombre.TooltipValue = "";

			// Apellidos
			Apellidos.LinkCustomAttributes = "";
			Apellidos.HrefValue = "";
			Apellidos.TooltipValue = "";

			// Area
			Area.LinkCustomAttributes = "";
			Area.HrefValue = "";
			Area.TooltipValue = "";

			// Persona
			Persona.LinkCustomAttributes = "";
			Persona.HrefValue = "";
			Persona.TooltipValue = "";

			// FechaIngreso
			FechaIngreso.LinkCustomAttributes = "";
			FechaIngreso.HrefValue = "";
			FechaIngreso.TooltipValue = "";

			// FechaSalida
			FechaSalida.LinkCustomAttributes = "";
			FechaSalida.HrefValue = "";
			FechaSalida.TooltipValue = "";

			// Observacion
			Observacion.LinkCustomAttributes = "";
			Observacion.HrefValue = "";
			Observacion.TooltipValue = "";

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
						XmlDoc.AddField("TipoDocumento", TipoDocumento.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Documento", Documento.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Nombre", Nombre.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Apellidos", Apellidos.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Area", Area.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Persona", Persona.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("FechaIngreso", FechaIngreso.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("FechaSalida", FechaSalida.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Observacion", Observacion.ExportValue(Export, ExportOriginalValue));
					} else {
						XmlDoc.AddField("TipoDocumento", TipoDocumento.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Documento", Documento.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Nombre", Nombre.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Apellidos", Apellidos.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Area", Area.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Persona", Persona.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("FechaIngreso", FechaIngreso.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("FechaSalida", FechaSalida.ExportValue(Export, ExportOriginalValue));
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
					Doc.ExportCaption(TipoDocumento);
					Doc.ExportCaption(Documento);
					Doc.ExportCaption(Nombre);
					Doc.ExportCaption(Apellidos);
					Doc.ExportCaption(Area);
					Doc.ExportCaption(Persona);
					Doc.ExportCaption(FechaIngreso);
					Doc.ExportCaption(FechaSalida);
					Doc.ExportCaption(Observacion);
				} else {
					Doc.ExportCaption(TipoDocumento);
					Doc.ExportCaption(Documento);
					Doc.ExportCaption(Nombre);
					Doc.ExportCaption(Apellidos);
					Doc.ExportCaption(Area);
					Doc.ExportCaption(Persona);
					Doc.ExportCaption(FechaIngreso);
					Doc.ExportCaption(FechaSalida);
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
						Doc.ExportField(TipoDocumento);
						Doc.ExportField(Documento);
						Doc.ExportField(Nombre);
						Doc.ExportField(Apellidos);
						Doc.ExportField(Area);
						Doc.ExportField(Persona);
						Doc.ExportField(FechaIngreso);
						Doc.ExportField(FechaSalida);
						Doc.ExportField(Observacion);
					} else {
						Doc.ExportField(TipoDocumento);
						Doc.ExportField(Documento);
						Doc.ExportField(Nombre);
						Doc.ExportField(Apellidos);
						Doc.ExportField(Area);
						Doc.ExportField(Persona);
						Doc.ExportField(FechaIngreso);
						Doc.ExportField(FechaSalida);
					}
					Doc.EndExportRow(false);
				}
			}
			Doc.ExportTableFooter();
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

		// TipoDocumento
		private cField m_TipoDocumento;

		public cField TipoDocumento {
			get {
				if (m_TipoDocumento == null) {
					m_TipoDocumento = new cField(ref m_Page, "HistoricoPeatones", "HistoricoPeatones", "x_TipoDocumento", "TipoDocumento", "dbo.TiposDocumentos.TipoDocumento", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_TipoDocumento.FldForceSelection = false;
					m_TipoDocumento.FldViewTag = "FORMATTED TEXT";
					Fields.Add("TipoDocumento", m_TipoDocumento); // Add to field list
				}				 
				return m_TipoDocumento;
			}
		}

		// Documento
		private cField m_Documento;

		public cField Documento {
			get {
				if (m_Documento == null) {
					m_Documento = new cField(ref m_Page, "HistoricoPeatones", "HistoricoPeatones", "x_Documento", "Documento", "dbo.RegistrosPeatones.Documento", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Documento.FldForceSelection = false;
					m_Documento.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Documento", m_Documento); // Add to field list
				}				 
				return m_Documento;
			}
		}

		// Nombre
		private cField m_Nombre;

		public cField Nombre {
			get {
				if (m_Nombre == null) {
					m_Nombre = new cField(ref m_Page, "HistoricoPeatones", "HistoricoPeatones", "x_Nombre", "Nombre", "dbo.RegistrosPeatones.Nombre", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Nombre.FldForceSelection = false;
					m_Nombre.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Nombre", m_Nombre); // Add to field list
				}				 
				return m_Nombre;
			}
		}

		// Apellidos
		private cField m_Apellidos;

		public cField Apellidos {
			get {
				if (m_Apellidos == null) {
					m_Apellidos = new cField(ref m_Page, "HistoricoPeatones", "HistoricoPeatones", "x_Apellidos", "Apellidos", "dbo.RegistrosPeatones.Apellidos", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Apellidos.FldForceSelection = false;
					m_Apellidos.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Apellidos", m_Apellidos); // Add to field list
				}				 
				return m_Apellidos;
			}
		}

		// Area
		private cField m_Area;

		public cField Area {
			get {
				if (m_Area == null) {
					m_Area = new cField(ref m_Page, "HistoricoPeatones", "HistoricoPeatones", "x_Area", "Area", "dbo.Areas.Area", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Area.FldForceSelection = false;
					m_Area.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Area", m_Area); // Add to field list
				}				 
				return m_Area;
			}
		}

		// Persona
		private cField m_Persona;

		public cField Persona {
			get {
				if (m_Persona == null) {
					m_Persona = new cField(ref m_Page, "HistoricoPeatones", "HistoricoPeatones", "x_Persona", "Persona", "dbo.Personas.Persona", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Persona.FldForceSelection = false;
					m_Persona.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Persona", m_Persona); // Add to field list
				}				 
				return m_Persona;
			}
		}

		// FechaIngreso
		private cField m_FechaIngreso;

		public cField FechaIngreso {
			get {
				if (m_FechaIngreso == null) {
					m_FechaIngreso = new cField(ref m_Page, "HistoricoPeatones", "HistoricoPeatones", "x_FechaIngreso", "FechaIngreso", "dbo.RegistrosPeatones.FechaIngreso", 135, SqlDbType.DateTime, EW_DATATYPE_DATE,  7, false, "FORMATTED TEXT");
					m_FechaIngreso.FldDefaultErrMsg = Language.Phrase("IncorrectDateDMY").Replace("%s", "/");
					m_FechaIngreso.FldForceSelection = false;
					m_FechaIngreso.FldViewTag = "FORMATTED TEXT";
					Fields.Add("FechaIngreso", m_FechaIngreso); // Add to field list
				}				 
				return m_FechaIngreso;
			}
		}

		// FechaSalida
		private cField m_FechaSalida;

		public cField FechaSalida {
			get {
				if (m_FechaSalida == null) {
					m_FechaSalida = new cField(ref m_Page, "HistoricoPeatones", "HistoricoPeatones", "x_FechaSalida", "FechaSalida", "dbo.RegistrosPeatones.FechaSalida", 135, SqlDbType.DateTime, EW_DATATYPE_DATE,  7, false, "FORMATTED TEXT");
					m_FechaSalida.FldDefaultErrMsg = Language.Phrase("IncorrectDateDMY").Replace("%s", "/");
					m_FechaSalida.FldForceSelection = false;
					m_FechaSalida.FldViewTag = "FORMATTED TEXT";
					Fields.Add("FechaSalida", m_FechaSalida); // Add to field list
				}				 
				return m_FechaSalida;
			}
		}

		// Observacion
		private cField m_Observacion;

		public cField Observacion {
			get {
				if (m_Observacion == null) {
					m_Observacion = new cField(ref m_Page, "HistoricoPeatones", "HistoricoPeatones", "x_Observacion", "Observacion", "dbo.RegistrosPeatones.Observacion", 201, SqlDbType.Text, EW_DATATYPE_MEMO,  -1, false, "FORMATTED TEXT");
					m_Observacion.FldForceSelection = false;
					m_Observacion.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Observacion", m_Observacion); // Add to field list
				}				 
				return m_Observacion;
			}
		}

		//  Get field object by name
		public cField FieldByName(string Name) {
			if (Fields.ContainsKey(Name))
				return Fields[Name]; 
			if (Name == "TipoDocumento") return TipoDocumento;
			if (Name == "Documento") return Documento;
			if (Name == "Nombre") return Nombre;
			if (Name == "Apellidos") return Apellidos;
			if (Name == "Area") return Area;
			if (Name == "Persona") return Persona;
			if (Name == "FechaIngreso") return FechaIngreso;
			if (Name == "FechaSalida") return FechaSalida;
			if (Name == "Observacion") return Observacion;
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
			if (m_TipoDocumento != null) m_TipoDocumento.Dispose();
			if (m_Documento != null) m_Documento.Dispose();
			if (m_Nombre != null) m_Nombre.Dispose();
			if (m_Apellidos != null) m_Apellidos.Dispose();
			if (m_Area != null) m_Area.Dispose();
			if (m_Persona != null) m_Persona.Dispose();
			if (m_FechaIngreso != null) m_FechaIngreso.Dispose();
			if (m_FechaSalida != null) m_FechaSalida.Dispose();
			if (m_Observacion != null) m_Observacion.Dispose();
		}
	}
}
