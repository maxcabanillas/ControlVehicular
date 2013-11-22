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

	public cRegistrosVehiculos RegistrosVehiculos;

	// Define table class
	public class cRegistrosVehiculos: cTable, IDisposable {

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
		public cRegistrosVehiculos(AspNetMakerPage APage) {
			m_TableName = "RegistrosVehiculos";
			m_TableObjName = "RegistrosVehiculos";
			m_TableObjTypeName = "cRegistrosVehiculos";
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
			get { return "RegistrosVehiculos_Highlight"; }
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
			get { return "SELECT * FROM [dbo].[RegistrosVehiculos]"; }
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
			get { return "[FechaIngreso] ASC"; }
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
			string Sql = "INSERT INTO [dbo].[RegistrosVehiculos] (" + names + ") VALUES (" + values + ")";
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
			string Sql = "UPDATE [dbo].[RegistrosVehiculos] SET " + values;
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
			string Sql = "DELETE FROM [dbo].[RegistrosVehiculos] WHERE ";
			fld = FieldByName("IdRegistroVehiculo");
			Sql += fld.FldExpression + "=" + ew_QuotedValue(Rs["IdRegistroVehiculo"], EW_DATATYPE_NUMBER) + " AND ";			
			if (Sql.EndsWith(" AND ")) Sql = Sql.Remove(Sql.Length - 5); 
			if (ew_NotEmpty(CurrentFilter)) Sql += " AND " + CurrentFilter; 
			return Conn.Execute(Sql);
		}

		// Key filter WHERE clause
		private string SqlKeyFilter {
			get { return "[IdRegistroVehiculo] = @IdRegistroVehiculo@"; }
		}

		// Key filter
		public string KeyFilter {
			get {
				string sKeyFilter = SqlKeyFilter;
				if (!Information.IsNumeric(IdRegistroVehiculo.CurrentValue))	{
					sKeyFilter = "0=1";	// Invalid key
				}
				sKeyFilter = sKeyFilter.Replace("@IdRegistroVehiculo@", ew_AdjustSql(IdRegistroVehiculo.CurrentValue)); // Replace key value
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
					return "RegistrosVehiculoslist.aspx";
				}
			}
		}

		// List URL
		public string ListUrl {
			get { return "RegistrosVehiculoslist.aspx"; }
		}

		// View URL
		public string ViewUrl	{
			get { return KeyUrl("RegistrosVehiculosview.aspx", UrlParm("")); }
		}

		// Add URL
		public string AddUrl {
			get {
				string result = "RegistrosVehiculosadd.aspx";

//				string sUrlParm = UrlParm("");
//				if (ew_NotEmpty(sUrlParm)) result = result + "?" + sUrlParm; 

				return result;
			}
		}

		// Edit URL
		public string EditUrl {
			get {
				return KeyUrl("RegistrosVehiculosedit.aspx", UrlParm(""));
			}
		}

		// Edit URL
		public string GetEditUrl(string parm) {
			return KeyUrl("RegistrosVehiculosedit.aspx", UrlParm(parm));
		}

		// Inline edit URL
		public string InlineEditUrl	{
			get { return KeyUrl(ew_CurrentPage(), UrlParm("a=edit")); }
		}

		// Copy URL
		public string CopyUrl	{
			get {
				return KeyUrl("RegistrosVehiculosadd.aspx", UrlParm(""));
			}
		}

		// Copy URL
		public string GetCopyUrl(string parm) {
			return KeyUrl("RegistrosVehiculosadd.aspx", UrlParm(parm));
		}

		// Inline copy URL
		public string InlineCopyUrl	{
			get { return KeyUrl(ew_CurrentPage(), UrlParm("a=copy")); }
		}

		// Delete URL
		public string DeleteUrl	{
			get { return KeyUrl("RegistrosVehiculosdelete.aspx", UrlParm("")); }
		}

		// Key URL
		public string KeyUrl(string url, string parm)	{
			string sUrl = url + "?";
			if (ew_NotEmpty(parm)) sUrl += parm + "&"; 
			if (!Convert.IsDBNull(IdRegistroVehiculo.CurrentValue)) {
				sUrl += "IdRegistroVehiculo=" + IdRegistroVehiculo.CurrentValue;
			} else {
				return "javascript:alert(ewLanguage.Phrase('InvalidRecord'));"; 
			}
			return sUrl;
		}

		// URL parm
		public string UrlParm(string parm) {
			string OutStr = "";
			if (UseTokenInUrl)
				OutStr = "t=RegistrosVehiculos";
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
				keys = ew_Get("IdRegistroVehiculo"); // IdRegistroVehiculo
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
				IdRegistroVehiculo.CurrentValue = keys;
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
			IdRegistroVehiculo.DbValue = RsRow["IdRegistroVehiculo"];
			IdTipoVehiculo.DbValue = RsRow["IdTipoVehiculo"];
			Placa.DbValue = RsRow["Placa"];
			FechaIngreso.DbValue = RsRow["FechaIngreso"];
			FechaSalida.DbValue = RsRow["FechaSalida"];
			Observaciones.DbValue = RsRow["Observaciones"];
		}

		// Render list row values
		public void RenderListRow() {

		//  Common render codes
			// IdRegistroVehiculo
			// IdTipoVehiculo
			// Placa
			// FechaIngreso
			// FechaSalida
			// Observaciones
			// Row Rendering event

			Row_Rendering();

		// Render for View
			// IdRegistroVehiculo

				IdRegistroVehiculo.ViewValue = IdRegistroVehiculo.CurrentValue;
			IdRegistroVehiculo.ViewCustomAttributes = "";

			// IdTipoVehiculo
			if (ew_NotEmpty(IdTipoVehiculo.CurrentValue)) {
				sFilterWrk = "[IdTipoVehiculo] = " + ew_AdjustSql(IdTipoVehiculo.CurrentValue) + "";
			sSqlWrk = "SELECT [TipoVehiculo] FROM [dbo].[TiposVehiculos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoVehiculo]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					IdTipoVehiculo.ViewValue = drWrk["TipoVehiculo"];
				} else {
					IdTipoVehiculo.ViewValue = IdTipoVehiculo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				IdTipoVehiculo.ViewValue = System.DBNull.Value;
			}
			IdTipoVehiculo.ViewCustomAttributes = "";

			// Placa
				Placa.ViewValue = Placa.CurrentValue;
			Placa.ViewCustomAttributes = "";

			// FechaIngreso
				FechaIngreso.ViewValue = FechaIngreso.CurrentValue;
				FechaIngreso.ViewValue = ew_FormatDateTime(FechaIngreso.ViewValue, 7);
			FechaIngreso.ViewCustomAttributes = "";

			// FechaSalida
				FechaSalida.ViewValue = FechaSalida.CurrentValue;
				FechaSalida.ViewValue = ew_FormatDateTime(FechaSalida.ViewValue, 7);
			FechaSalida.ViewCustomAttributes = "";

			// Observaciones
			Observaciones.ViewValue = Observaciones.CurrentValue;
			Observaciones.ViewCustomAttributes = "";

		// Render for View Refer	
			// IdRegistroVehiculo

			IdRegistroVehiculo.LinkCustomAttributes = "";
			IdRegistroVehiculo.HrefValue = "";
			IdRegistroVehiculo.TooltipValue = "";

			// IdTipoVehiculo
			IdTipoVehiculo.LinkCustomAttributes = "";
			IdTipoVehiculo.HrefValue = "";
			IdTipoVehiculo.TooltipValue = "";

			// Placa
			Placa.LinkCustomAttributes = "";
			Placa.HrefValue = "";
			Placa.TooltipValue = "";

			// FechaIngreso
			FechaIngreso.LinkCustomAttributes = "";
			FechaIngreso.HrefValue = "";
			FechaIngreso.TooltipValue = "";

			// FechaSalida
			FechaSalida.LinkCustomAttributes = "";
			FechaSalida.HrefValue = "";
			FechaSalida.TooltipValue = "";

			// Observaciones
			Observaciones.LinkCustomAttributes = "";
			Observaciones.HrefValue = "";
			Observaciones.TooltipValue = "";

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
						XmlDoc.AddField("IdRegistroVehiculo", IdRegistroVehiculo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("IdTipoVehiculo", IdTipoVehiculo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Placa", Placa.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("FechaIngreso", FechaIngreso.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("FechaSalida", FechaSalida.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Observaciones", Observaciones.ExportValue(Export, ExportOriginalValue));
					} else {
						XmlDoc.AddField("IdRegistroVehiculo", IdRegistroVehiculo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("IdTipoVehiculo", IdTipoVehiculo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Placa", Placa.ExportValue(Export, ExportOriginalValue));
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
					Doc.ExportCaption(IdRegistroVehiculo);
					Doc.ExportCaption(IdTipoVehiculo);
					Doc.ExportCaption(Placa);
					Doc.ExportCaption(FechaIngreso);
					Doc.ExportCaption(FechaSalida);
					Doc.ExportCaption(Observaciones);
				} else {
					Doc.ExportCaption(IdRegistroVehiculo);
					Doc.ExportCaption(IdTipoVehiculo);
					Doc.ExportCaption(Placa);
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
						Doc.ExportField(IdRegistroVehiculo);
						Doc.ExportField(IdTipoVehiculo);
						Doc.ExportField(Placa);
						Doc.ExportField(FechaIngreso);
						Doc.ExportField(FechaSalida);
						Doc.ExportField(Observaciones);
					} else {
						Doc.ExportField(IdRegistroVehiculo);
						Doc.ExportField(IdTipoVehiculo);
						Doc.ExportField(Placa);
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

		// IdRegistroVehiculo
		private cField m_IdRegistroVehiculo;

		public cField IdRegistroVehiculo {
			get {
				if (m_IdRegistroVehiculo == null) {
					m_IdRegistroVehiculo = new cField(ref m_Page, "RegistrosVehiculos", "RegistrosVehiculos", "x_IdRegistroVehiculo", "IdRegistroVehiculo", "[IdRegistroVehiculo]", 20, SqlDbType.BigInt, EW_DATATYPE_NUMBER,  -1, false, "FORMATTED TEXT");
					m_IdRegistroVehiculo.FldDefaultErrMsg = Language.Phrase("IncorrectInteger");
					m_IdRegistroVehiculo.FldForceSelection = false;
					m_IdRegistroVehiculo.FldViewTag = "FORMATTED TEXT";
					Fields.Add("IdRegistroVehiculo", m_IdRegistroVehiculo); // Add to field list
				}				 
				return m_IdRegistroVehiculo;
			}
		}

		// IdTipoVehiculo
		private cField m_IdTipoVehiculo;

		public cField IdTipoVehiculo {
			get {
				if (m_IdTipoVehiculo == null) {
					m_IdTipoVehiculo = new cField(ref m_Page, "RegistrosVehiculos", "RegistrosVehiculos", "x_IdTipoVehiculo", "IdTipoVehiculo", "[IdTipoVehiculo]", 3, SqlDbType.Int, EW_DATATYPE_NUMBER,  -1, false, "FORMATTED TEXT");
					m_IdTipoVehiculo.FldDefaultErrMsg = Language.Phrase("IncorrectInteger");
					m_IdTipoVehiculo.FldForceSelection = false;
					m_IdTipoVehiculo.FldViewTag = "FORMATTED TEXT";
					Fields.Add("IdTipoVehiculo", m_IdTipoVehiculo); // Add to field list
				}				 
				return m_IdTipoVehiculo;
			}
		}

		// Placa
		private cField m_Placa;

		public cField Placa {
			get {
				if (m_Placa == null) {
					m_Placa = new cField(ref m_Page, "RegistrosVehiculos", "RegistrosVehiculos", "x_Placa", "Placa", "[Placa]", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Placa.FldForceSelection = false;
					m_Placa.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Placa", m_Placa); // Add to field list
				}				 
				return m_Placa;
			}
		}

		// FechaIngreso
		private cField m_FechaIngreso;

		public cField FechaIngreso {
			get {
				if (m_FechaIngreso == null) {
					m_FechaIngreso = new cField(ref m_Page, "RegistrosVehiculos", "RegistrosVehiculos", "x_FechaIngreso", "FechaIngreso", "[FechaIngreso]", 135, SqlDbType.DateTime, EW_DATATYPE_DATE,  7, false, "FORMATTED TEXT");
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
					m_FechaSalida = new cField(ref m_Page, "RegistrosVehiculos", "RegistrosVehiculos", "x_FechaSalida", "FechaSalida", "[FechaSalida]", 135, SqlDbType.DateTime, EW_DATATYPE_DATE,  7, false, "FORMATTED TEXT");
					m_FechaSalida.FldDefaultErrMsg = Language.Phrase("IncorrectDateDMY").Replace("%s", "/");
					m_FechaSalida.FldForceSelection = false;
					m_FechaSalida.FldViewTag = "FORMATTED TEXT";
					Fields.Add("FechaSalida", m_FechaSalida); // Add to field list
				}				 
				return m_FechaSalida;
			}
		}

		// Observaciones
		private cField m_Observaciones;

		public cField Observaciones {
			get {
				if (m_Observaciones == null) {
					m_Observaciones = new cField(ref m_Page, "RegistrosVehiculos", "RegistrosVehiculos", "x_Observaciones", "Observaciones", "[Observaciones]", 201, SqlDbType.Text, EW_DATATYPE_MEMO,  -1, false, "FORMATTED TEXT");
					m_Observaciones.FldForceSelection = false;
					m_Observaciones.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Observaciones", m_Observaciones); // Add to field list
				}				 
				return m_Observaciones;
			}
		}

		//  Get field object by name
		public cField FieldByName(string Name) {
			if (Fields.ContainsKey(Name))
				return Fields[Name]; 
			if (Name == "IdRegistroVehiculo") return IdRegistroVehiculo;
			if (Name == "IdTipoVehiculo") return IdTipoVehiculo;
			if (Name == "Placa") return Placa;
			if (Name == "FechaIngreso") return FechaIngreso;
			if (Name == "FechaSalida") return FechaSalida;
			if (Name == "Observaciones") return Observaciones;
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
			if (m_IdRegistroVehiculo != null) m_IdRegistroVehiculo.Dispose();
			if (m_IdTipoVehiculo != null) m_IdTipoVehiculo.Dispose();
			if (m_Placa != null) m_Placa.Dispose();
			if (m_FechaIngreso != null) m_FechaIngreso.Dispose();
			if (m_FechaSalida != null) m_FechaSalida.Dispose();
			if (m_Observaciones != null) m_Observaciones.Dispose();
		}
	}
}
