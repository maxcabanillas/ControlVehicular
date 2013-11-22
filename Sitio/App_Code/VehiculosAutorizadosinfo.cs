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

	public cVehiculosAutorizados VehiculosAutorizados;

	// Define table class
	public class cVehiculosAutorizados: cTable, IDisposable {

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
		public cVehiculosAutorizados(AspNetMakerPage APage) {
			m_TableName = "VehiculosAutorizados";
			m_TableObjName = "VehiculosAutorizados";
			m_TableObjTypeName = "cVehiculosAutorizados";
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
			get { return "VehiculosAutorizados_Highlight"; }
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

		// Current master table name
		public string CurrentMasterTable {
			get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_MASTER_TABLE]); }
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_MASTER_TABLE] = value; }
		}

		// Session master where clause
		public string MasterFilter {
			get { // Master filter
				string sMasterFilter = "";
				if (CurrentMasterTable == "Personas") {
					if (ew_NotEmpty(IdPersona.SessionValue))
						sMasterFilter += "[IdPersona]=" + ew_QuotedValue(IdPersona.SessionValue, EW_DATATYPE_NUMBER);
					else
						return "";
				}
				return sMasterFilter;
			}
		}

		// Session detail where clause
		public string DetailFilter {
			get { // Detail filter
				string sDetailFilter = "";
				if (CurrentMasterTable == "Personas") {
					if (ew_NotEmpty(IdPersona.SessionValue))
						sDetailFilter += "[IdPersona]=" + ew_QuotedValue(IdPersona.SessionValue, EW_DATATYPE_NUMBER);
					else
						return "";
				}
				return sDetailFilter;
			}			
		}

		// Personas
		public cPersonas Personas {
			get { return ParentPage.Personas; }
			set { ParentPage.Personas = value; }
		}

		// Master filter
		public string SqlMasterFilter_Personas {
			get { return "[IdPersona]=@IdPersona@"; }
		}

		// Detail filter
		public string SqlDetailFilter_Personas {
			get { return "[IdPersona]=@IdPersona@"; }
		}

		// Detail paramters // ASPX
		public string DetailParms {
			get {
				string str = ""; 
				str += "currentaction=" + CurrentAction;
				str += "&currentmode=" + CurrentMode;
				str += "&detaileventcancelled=" + ((EventCancelled) ? "1" : "0");
				str += "&currentmastertable=" + CurrentMasterTable;								
				return str;
			}
		}

		// Load detail paramters from URL // ASPX
		public void LoadDetailParms() {
			if (ew_NotEmpty(ew_Get("currentaction")))
				CurrentAction = ew_Get("currentaction");
			if (ew_NotEmpty(ew_Get("currentmode")))
				CurrentMode = ew_Get("currentmode");
			if (ew_NotEmpty(ew_Get("detaileventcancelled")))
				EventCancelled = ew_ConvertToBool(ew_Get("detaileventcancelled"));
			if (ew_NotEmpty(ew_Get("currentmastertable")))
				CurrentMasterTable = ew_Get("currentmastertable");			
			StartRecordNumber = 1;				
		}

		// Current detail table name
		public string CurrentDetailTable {
			get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_DETAIL_TABLE]); }
			set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_DETAIL_TABLE] = value; }
		}

		// Get detail URL
		public string DetailUrl {
			get {
				string sDetailUrl = "";
				if (CurrentDetailTable == "VehiculosPicoYPlacaHoras") {
					sDetailUrl = ParentPage.VehiculosPicoYPlacaHoras.ListUrl + "?showmaster=" + TableVar;
					sDetailUrl += "&IdVehiculoAutorizado=" + IdVehiculoAutorizado.CurrentValue;
				}
				return sDetailUrl;
			}
		}

		// Table level SQL
		// Select
		public string SqlSelect {
			get { return "SELECT * FROM [dbo].[VehiculosAutorizados]"; }
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
			string Sql = "INSERT INTO [dbo].[VehiculosAutorizados] (" + names + ") VALUES (" + values + ")";
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
			string Sql = "UPDATE [dbo].[VehiculosAutorizados] SET " + values;
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
			string Sql = "DELETE FROM [dbo].[VehiculosAutorizados] WHERE ";
			fld = FieldByName("IdVehiculoAutorizado");
			Sql += fld.FldExpression + "=" + ew_QuotedValue(Rs["IdVehiculoAutorizado"], EW_DATATYPE_NUMBER) + " AND ";			
			if (Sql.EndsWith(" AND ")) Sql = Sql.Remove(Sql.Length - 5); 
			if (ew_NotEmpty(CurrentFilter)) Sql += " AND " + CurrentFilter; 
			return Conn.Execute(Sql);
		}

		// Key filter WHERE clause
		private string SqlKeyFilter {
			get { return "[IdVehiculoAutorizado] = @IdVehiculoAutorizado@"; }
		}

		// Key filter
		public string KeyFilter {
			get {
				string sKeyFilter = SqlKeyFilter;
				if (!Information.IsNumeric(IdVehiculoAutorizado.CurrentValue))	{
					sKeyFilter = "0=1";	// Invalid key
				}
				sKeyFilter = sKeyFilter.Replace("@IdVehiculoAutorizado@", ew_AdjustSql(IdVehiculoAutorizado.CurrentValue)); // Replace key value
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
					return "VehiculosAutorizadoslist.aspx";
				}
			}
		}

		// List URL
		public string ListUrl {
			get { return "VehiculosAutorizadoslist.aspx"; }
		}

		// View URL
		public string ViewUrl	{
			get { return KeyUrl("VehiculosAutorizadosview.aspx", UrlParm("")); }
		}

		// Add URL
		public string AddUrl {
			get {
				string result = "VehiculosAutorizadosadd.aspx";

//				string sUrlParm = UrlParm("");
//				if (ew_NotEmpty(sUrlParm)) result = result + "?" + sUrlParm; 

				return result;
			}
		}

		// Edit URL
		public string EditUrl {
			get {
				return KeyUrl("VehiculosAutorizadosedit.aspx", UrlParm(EW_TABLE_SHOW_DETAIL + "="));
			}
		}

		// Edit URL
		public string GetEditUrl(string parm) {
			if (ew_NotEmpty(parm))
				return KeyUrl("VehiculosAutorizadosedit.aspx", UrlParm(parm));
			else
				return KeyUrl("VehiculosAutorizadosedit.aspx", UrlParm(EW_TABLE_SHOW_DETAIL + "="));
		}

		// Inline edit URL
		public string InlineEditUrl	{
			get { return KeyUrl(ew_CurrentPage(), UrlParm("a=edit")); }
		}

		// Copy URL
		public string CopyUrl	{
			get {
				return KeyUrl("VehiculosAutorizadosadd.aspx", UrlParm(EW_TABLE_SHOW_DETAIL + "="));
			}
		}

		// Copy URL
		public string GetCopyUrl(string parm) {
			if (ew_NotEmpty(parm))
				return KeyUrl("VehiculosAutorizadosadd.aspx", UrlParm(parm));
			else
				return KeyUrl("VehiculosAutorizadosadd.aspx", UrlParm(EW_TABLE_SHOW_DETAIL + "="));
		}

		// Inline copy URL
		public string InlineCopyUrl	{
			get { return KeyUrl(ew_CurrentPage(), UrlParm("a=copy")); }
		}

		// Delete URL
		public string DeleteUrl	{
			get { return KeyUrl("VehiculosAutorizadosdelete.aspx", UrlParm("")); }
		}

		// Key URL
		public string KeyUrl(string url, string parm)	{
			string sUrl = url + "?";
			if (ew_NotEmpty(parm)) sUrl += parm + "&"; 
			if (!Convert.IsDBNull(IdVehiculoAutorizado.CurrentValue)) {
				sUrl += "IdVehiculoAutorizado=" + IdVehiculoAutorizado.CurrentValue;
			} else {
				return "javascript:alert(ewLanguage.Phrase('InvalidRecord'));"; 
			}
			return sUrl;
		}

		// URL parm
		public string UrlParm(string parm) {
			string OutStr = "";
			if (UseTokenInUrl)
				OutStr = "t=VehiculosAutorizados";
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
				keys = ew_Get("IdVehiculoAutorizado"); // IdVehiculoAutorizado
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
				IdVehiculoAutorizado.CurrentValue = keys;
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
			IdVehiculoAutorizado.DbValue = RsRow["IdVehiculoAutorizado"];
			IdTipoVehiculo.DbValue = RsRow["IdTipoVehiculo"];
			Placa.DbValue = RsRow["Placa"];
			Autorizado.DbValue = (ew_ConvertToBool(RsRow["Autorizado"])) ? "1" : "0";
			IdPersona.DbValue = RsRow["IdPersona"];
			PicoyPlaca.DbValue = (ew_ConvertToBool(RsRow["PicoyPlaca"])) ? "1" : "0";
			Lunes.DbValue = (ew_ConvertToBool(RsRow["Lunes"])) ? "1" : "0";
			Martes.DbValue = (ew_ConvertToBool(RsRow["Martes"])) ? "1" : "0";
			Miercoles.DbValue = (ew_ConvertToBool(RsRow["Miercoles"])) ? "1" : "0";
			Jueves.DbValue = (ew_ConvertToBool(RsRow["Jueves"])) ? "1" : "0";
			Viernes.DbValue = (ew_ConvertToBool(RsRow["Viernes"])) ? "1" : "0";
			Sabado.DbValue = (ew_ConvertToBool(RsRow["Sabado"])) ? "1" : "0";
			Domingo.DbValue = (ew_ConvertToBool(RsRow["Domingo"])) ? "1" : "0";
			Marca.DbValue = RsRow["Marca"];
		}

		// Render list row values
		public void RenderListRow() {

		//  Common render codes
			// IdVehiculoAutorizado
			// IdTipoVehiculo
			// Placa
			// Autorizado
			// IdPersona
			// PicoyPlaca
			// Lunes
			// Martes
			// Miercoles
			// Jueves
			// Viernes
			// Sabado
			// Domingo
			// Marca
			// Row Rendering event

			Row_Rendering();

		// Render for View
			// IdVehiculoAutorizado

				IdVehiculoAutorizado.ViewValue = IdVehiculoAutorizado.CurrentValue;
			IdVehiculoAutorizado.ViewCustomAttributes = "";

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

			// Autorizado
			if (Convert.ToString(Autorizado.CurrentValue) == "1") {
				Autorizado.ViewValue = (Autorizado.FldTagCaption(1) != "") ? Autorizado.FldTagCaption(1) : "Y";
			} else {
				Autorizado.ViewValue = (Autorizado.FldTagCaption(2) != "") ? Autorizado.FldTagCaption(2) : "N";
			}
			Autorizado.ViewCustomAttributes = "";

			// IdPersona
			IdPersona.ViewCustomAttributes = "";

			// PicoyPlaca
			if (Convert.ToString(PicoyPlaca.CurrentValue) == "1") {
				PicoyPlaca.ViewValue = (PicoyPlaca.FldTagCaption(1) != "") ? PicoyPlaca.FldTagCaption(1) : "Y";
			} else {
				PicoyPlaca.ViewValue = (PicoyPlaca.FldTagCaption(2) != "") ? PicoyPlaca.FldTagCaption(2) : "N";
			}
			PicoyPlaca.ViewCustomAttributes = "";

			// Lunes
			if (Convert.ToString(Lunes.CurrentValue) == "1") {
				Lunes.ViewValue = (Lunes.FldTagCaption(1) != "") ? Lunes.FldTagCaption(1) : "Y";
			} else {
				Lunes.ViewValue = (Lunes.FldTagCaption(2) != "") ? Lunes.FldTagCaption(2) : "N";
			}
			Lunes.ViewCustomAttributes = "";

			// Martes
			if (Convert.ToString(Martes.CurrentValue) == "1") {
				Martes.ViewValue = (Martes.FldTagCaption(1) != "") ? Martes.FldTagCaption(1) : "Y";
			} else {
				Martes.ViewValue = (Martes.FldTagCaption(2) != "") ? Martes.FldTagCaption(2) : "N";
			}
			Martes.ViewCustomAttributes = "";

			// Miercoles
			if (Convert.ToString(Miercoles.CurrentValue) == "1") {
				Miercoles.ViewValue = (Miercoles.FldTagCaption(1) != "") ? Miercoles.FldTagCaption(1) : "Y";
			} else {
				Miercoles.ViewValue = (Miercoles.FldTagCaption(2) != "") ? Miercoles.FldTagCaption(2) : "N";
			}
			Miercoles.ViewCustomAttributes = "";

			// Jueves
			if (Convert.ToString(Jueves.CurrentValue) == "1") {
				Jueves.ViewValue = (Jueves.FldTagCaption(1) != "") ? Jueves.FldTagCaption(1) : "Y";
			} else {
				Jueves.ViewValue = (Jueves.FldTagCaption(2) != "") ? Jueves.FldTagCaption(2) : "N";
			}
			Jueves.ViewCustomAttributes = "";

			// Viernes
			if (Convert.ToString(Viernes.CurrentValue) == "1") {
				Viernes.ViewValue = (Viernes.FldTagCaption(1) != "") ? Viernes.FldTagCaption(1) : "Y";
			} else {
				Viernes.ViewValue = (Viernes.FldTagCaption(2) != "") ? Viernes.FldTagCaption(2) : "N";
			}
			Viernes.ViewCustomAttributes = "";

			// Sabado
			if (Convert.ToString(Sabado.CurrentValue) == "1") {
				Sabado.ViewValue = (Sabado.FldTagCaption(1) != "") ? Sabado.FldTagCaption(1) : "Y";
			} else {
				Sabado.ViewValue = (Sabado.FldTagCaption(2) != "") ? Sabado.FldTagCaption(2) : "N";
			}
			Sabado.ViewCustomAttributes = "";

			// Domingo
			if (Convert.ToString(Domingo.CurrentValue) == "1") {
				Domingo.ViewValue = (Domingo.FldTagCaption(1) != "") ? Domingo.FldTagCaption(1) : "Y";
			} else {
				Domingo.ViewValue = (Domingo.FldTagCaption(2) != "") ? Domingo.FldTagCaption(2) : "N";
			}
			Domingo.ViewCustomAttributes = "";

			// Marca
				Marca.ViewValue = Marca.CurrentValue;
			Marca.ViewCustomAttributes = "";

		// Render for View Refer	
			// IdVehiculoAutorizado

			IdVehiculoAutorizado.LinkCustomAttributes = "";
			IdVehiculoAutorizado.HrefValue = "";
			IdVehiculoAutorizado.TooltipValue = "";

			// IdTipoVehiculo
			IdTipoVehiculo.LinkCustomAttributes = "";
			IdTipoVehiculo.HrefValue = "";
			IdTipoVehiculo.TooltipValue = "";

			// Placa
			Placa.LinkCustomAttributes = "";
			Placa.HrefValue = "";
			Placa.TooltipValue = "";

			// Autorizado
			Autorizado.LinkCustomAttributes = "";
			Autorizado.HrefValue = "";
			Autorizado.TooltipValue = "";

			// IdPersona
			IdPersona.LinkCustomAttributes = "";
			IdPersona.HrefValue = "";
			IdPersona.TooltipValue = "";

			// PicoyPlaca
			PicoyPlaca.LinkCustomAttributes = "";
			PicoyPlaca.HrefValue = "";
			PicoyPlaca.TooltipValue = "";

			// Lunes
			Lunes.LinkCustomAttributes = "";
			Lunes.HrefValue = "";
			Lunes.TooltipValue = "";

			// Martes
			Martes.LinkCustomAttributes = "";
			Martes.HrefValue = "";
			Martes.TooltipValue = "";

			// Miercoles
			Miercoles.LinkCustomAttributes = "";
			Miercoles.HrefValue = "";
			Miercoles.TooltipValue = "";

			// Jueves
			Jueves.LinkCustomAttributes = "";
			Jueves.HrefValue = "";
			Jueves.TooltipValue = "";

			// Viernes
			Viernes.LinkCustomAttributes = "";
			Viernes.HrefValue = "";
			Viernes.TooltipValue = "";

			// Sabado
			Sabado.LinkCustomAttributes = "";
			Sabado.HrefValue = "";
			Sabado.TooltipValue = "";

			// Domingo
			Domingo.LinkCustomAttributes = "";
			Domingo.HrefValue = "";
			Domingo.TooltipValue = "";

			// Marca
			Marca.LinkCustomAttributes = "";
			Marca.HrefValue = "";
			Marca.TooltipValue = "";

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
						XmlDoc.AddField("IdVehiculoAutorizado", IdVehiculoAutorizado.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("IdTipoVehiculo", IdTipoVehiculo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Placa", Placa.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Autorizado", Autorizado.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("IdPersona", IdPersona.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("PicoyPlaca", PicoyPlaca.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Lunes", Lunes.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Martes", Martes.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Miercoles", Miercoles.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Jueves", Jueves.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Viernes", Viernes.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Sabado", Sabado.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Domingo", Domingo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Marca", Marca.ExportValue(Export, ExportOriginalValue));
					} else {
						XmlDoc.AddField("IdVehiculoAutorizado", IdVehiculoAutorizado.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("IdTipoVehiculo", IdTipoVehiculo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Placa", Placa.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Autorizado", Autorizado.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("IdPersona", IdPersona.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("PicoyPlaca", PicoyPlaca.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Lunes", Lunes.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Martes", Martes.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Miercoles", Miercoles.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Jueves", Jueves.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Viernes", Viernes.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Sabado", Sabado.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Domingo", Domingo.ExportValue(Export, ExportOriginalValue));
						XmlDoc.AddField("Marca", Marca.ExportValue(Export, ExportOriginalValue));
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
					Doc.ExportCaption(IdVehiculoAutorizado);
					Doc.ExportCaption(IdTipoVehiculo);
					Doc.ExportCaption(Placa);
					Doc.ExportCaption(Autorizado);
					Doc.ExportCaption(IdPersona);
					Doc.ExportCaption(PicoyPlaca);
					Doc.ExportCaption(Lunes);
					Doc.ExportCaption(Martes);
					Doc.ExportCaption(Miercoles);
					Doc.ExportCaption(Jueves);
					Doc.ExportCaption(Viernes);
					Doc.ExportCaption(Sabado);
					Doc.ExportCaption(Domingo);
					Doc.ExportCaption(Marca);
				} else {
					Doc.ExportCaption(IdVehiculoAutorizado);
					Doc.ExportCaption(IdTipoVehiculo);
					Doc.ExportCaption(Placa);
					Doc.ExportCaption(Autorizado);
					Doc.ExportCaption(IdPersona);
					Doc.ExportCaption(PicoyPlaca);
					Doc.ExportCaption(Lunes);
					Doc.ExportCaption(Martes);
					Doc.ExportCaption(Miercoles);
					Doc.ExportCaption(Jueves);
					Doc.ExportCaption(Viernes);
					Doc.ExportCaption(Sabado);
					Doc.ExportCaption(Domingo);
					Doc.ExportCaption(Marca);
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
						Doc.ExportField(IdVehiculoAutorizado);
						Doc.ExportField(IdTipoVehiculo);
						Doc.ExportField(Placa);
						Doc.ExportField(Autorizado);
						Doc.ExportField(IdPersona);
						Doc.ExportField(PicoyPlaca);
						Doc.ExportField(Lunes);
						Doc.ExportField(Martes);
						Doc.ExportField(Miercoles);
						Doc.ExportField(Jueves);
						Doc.ExportField(Viernes);
						Doc.ExportField(Sabado);
						Doc.ExportField(Domingo);
						Doc.ExportField(Marca);
					} else {
						Doc.ExportField(IdVehiculoAutorizado);
						Doc.ExportField(IdTipoVehiculo);
						Doc.ExportField(Placa);
						Doc.ExportField(Autorizado);
						Doc.ExportField(IdPersona);
						Doc.ExportField(PicoyPlaca);
						Doc.ExportField(Lunes);
						Doc.ExportField(Martes);
						Doc.ExportField(Miercoles);
						Doc.ExportField(Jueves);
						Doc.ExportField(Viernes);
						Doc.ExportField(Sabado);
						Doc.ExportField(Domingo);
						Doc.ExportField(Marca);
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

		// IdVehiculoAutorizado
		private cField m_IdVehiculoAutorizado;

		public cField IdVehiculoAutorizado {
			get {
				if (m_IdVehiculoAutorizado == null) {
					m_IdVehiculoAutorizado = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_IdVehiculoAutorizado", "IdVehiculoAutorizado", "[IdVehiculoAutorizado]", 3, SqlDbType.Int, EW_DATATYPE_NUMBER,  -1, false, "FORMATTED TEXT");
					m_IdVehiculoAutorizado.FldDefaultErrMsg = Language.Phrase("IncorrectInteger");
					m_IdVehiculoAutorizado.FldForceSelection = false;
					m_IdVehiculoAutorizado.FldViewTag = "FORMATTED TEXT";
					Fields.Add("IdVehiculoAutorizado", m_IdVehiculoAutorizado); // Add to field list
				}				 
				return m_IdVehiculoAutorizado;
			}
		}

		// IdTipoVehiculo
		private cField m_IdTipoVehiculo;

		public cField IdTipoVehiculo {
			get {
				if (m_IdTipoVehiculo == null) {
					m_IdTipoVehiculo = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_IdTipoVehiculo", "IdTipoVehiculo", "[IdTipoVehiculo]", 3, SqlDbType.Int, EW_DATATYPE_NUMBER,  -1, false, "FORMATTED TEXT");
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
					m_Placa = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Placa", "Placa", "[Placa]", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Placa.FldForceSelection = false;
					m_Placa.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Placa", m_Placa); // Add to field list
				}				 
				return m_Placa;
			}
		}

		// Autorizado
		private cField m_Autorizado;

		public cField Autorizado {
			get {
				if (m_Autorizado == null) {
					m_Autorizado = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Autorizado", "Autorizado", "[Autorizado]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_Autorizado.FldForceSelection = false;
					m_Autorizado.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Autorizado", m_Autorizado); // Add to field list
				}				 
				return m_Autorizado;
			}
		}

		// IdPersona
		private cField m_IdPersona;

		public cField IdPersona {
			get {
				if (m_IdPersona == null) {
					m_IdPersona = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_IdPersona", "IdPersona", "[IdPersona]", 3, SqlDbType.Int, EW_DATATYPE_NUMBER,  -1, false, "FORMATTED TEXT");
					m_IdPersona.FldDefaultErrMsg = Language.Phrase("IncorrectInteger");
					m_IdPersona.FldForceSelection = false;
					m_IdPersona.FldViewTag = "FORMATTED TEXT";
					Fields.Add("IdPersona", m_IdPersona); // Add to field list
				}				 
				return m_IdPersona;
			}
		}

		// PicoyPlaca
		private cField m_PicoyPlaca;

		public cField PicoyPlaca {
			get {
				if (m_PicoyPlaca == null) {
					m_PicoyPlaca = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_PicoyPlaca", "PicoyPlaca", "[PicoyPlaca]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_PicoyPlaca.FldForceSelection = false;
					m_PicoyPlaca.FldViewTag = "FORMATTED TEXT";
					Fields.Add("PicoyPlaca", m_PicoyPlaca); // Add to field list
				}				 
				return m_PicoyPlaca;
			}
		}

		// Lunes
		private cField m_Lunes;

		public cField Lunes {
			get {
				if (m_Lunes == null) {
					m_Lunes = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Lunes", "Lunes", "[Lunes]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_Lunes.FldForceSelection = false;
					m_Lunes.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Lunes", m_Lunes); // Add to field list
				}				 
				return m_Lunes;
			}
		}

		// Martes
		private cField m_Martes;

		public cField Martes {
			get {
				if (m_Martes == null) {
					m_Martes = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Martes", "Martes", "[Martes]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_Martes.FldForceSelection = false;
					m_Martes.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Martes", m_Martes); // Add to field list
				}				 
				return m_Martes;
			}
		}

		// Miercoles
		private cField m_Miercoles;

		public cField Miercoles {
			get {
				if (m_Miercoles == null) {
					m_Miercoles = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Miercoles", "Miercoles", "[Miercoles]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_Miercoles.FldForceSelection = false;
					m_Miercoles.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Miercoles", m_Miercoles); // Add to field list
				}				 
				return m_Miercoles;
			}
		}

		// Jueves
		private cField m_Jueves;

		public cField Jueves {
			get {
				if (m_Jueves == null) {
					m_Jueves = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Jueves", "Jueves", "[Jueves]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_Jueves.FldForceSelection = false;
					m_Jueves.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Jueves", m_Jueves); // Add to field list
				}				 
				return m_Jueves;
			}
		}

		// Viernes
		private cField m_Viernes;

		public cField Viernes {
			get {
				if (m_Viernes == null) {
					m_Viernes = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Viernes", "Viernes", "[Viernes]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_Viernes.FldForceSelection = false;
					m_Viernes.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Viernes", m_Viernes); // Add to field list
				}				 
				return m_Viernes;
			}
		}

		// Sabado
		private cField m_Sabado;

		public cField Sabado {
			get {
				if (m_Sabado == null) {
					m_Sabado = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Sabado", "Sabado", "[Sabado]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_Sabado.FldForceSelection = false;
					m_Sabado.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Sabado", m_Sabado); // Add to field list
				}				 
				return m_Sabado;
			}
		}

		// Domingo
		private cField m_Domingo;

		public cField Domingo {
			get {
				if (m_Domingo == null) {
					m_Domingo = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Domingo", "Domingo", "[Domingo]", 11, SqlDbType.Bit, EW_DATATYPE_BOOLEAN,  -1, false, "FORMATTED TEXT");
					m_Domingo.FldForceSelection = false;
					m_Domingo.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Domingo", m_Domingo); // Add to field list
				}				 
				return m_Domingo;
			}
		}

		// Marca
		private cField m_Marca;

		public cField Marca {
			get {
				if (m_Marca == null) {
					m_Marca = new cField(ref m_Page, "VehiculosAutorizados", "VehiculosAutorizados", "x_Marca", "Marca", "[Marca]", 200, SqlDbType.VarChar, EW_DATATYPE_STRING,  -1, false, "FORMATTED TEXT");
					m_Marca.FldForceSelection = false;
					m_Marca.FldViewTag = "FORMATTED TEXT";
					Fields.Add("Marca", m_Marca); // Add to field list
				}				 
				return m_Marca;
			}
		}

		//  Get field object by name
		public cField FieldByName(string Name) {
			if (Fields.ContainsKey(Name))
				return Fields[Name]; 
			if (Name == "IdVehiculoAutorizado") return IdVehiculoAutorizado;
			if (Name == "IdTipoVehiculo") return IdTipoVehiculo;
			if (Name == "Placa") return Placa;
			if (Name == "Autorizado") return Autorizado;
			if (Name == "IdPersona") return IdPersona;
			if (Name == "PicoyPlaca") return PicoyPlaca;
			if (Name == "Lunes") return Lunes;
			if (Name == "Martes") return Martes;
			if (Name == "Miercoles") return Miercoles;
			if (Name == "Jueves") return Jueves;
			if (Name == "Viernes") return Viernes;
			if (Name == "Sabado") return Sabado;
			if (Name == "Domingo") return Domingo;
			if (Name == "Marca") return Marca;
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
			if (m_IdVehiculoAutorizado != null) m_IdVehiculoAutorizado.Dispose();
			if (m_IdTipoVehiculo != null) m_IdTipoVehiculo.Dispose();
			if (m_Placa != null) m_Placa.Dispose();
			if (m_Autorizado != null) m_Autorizado.Dispose();
			if (m_IdPersona != null) m_IdPersona.Dispose();
			if (m_PicoyPlaca != null) m_PicoyPlaca.Dispose();
			if (m_Lunes != null) m_Lunes.Dispose();
			if (m_Martes != null) m_Martes.Dispose();
			if (m_Miercoles != null) m_Miercoles.Dispose();
			if (m_Jueves != null) m_Jueves.Dispose();
			if (m_Viernes != null) m_Viernes.Dispose();
			if (m_Sabado != null) m_Sabado.Dispose();
			if (m_Domingo != null) m_Domingo.Dispose();
			if (m_Marca != null) m_Marca.Dispose();
		}
	}
}
