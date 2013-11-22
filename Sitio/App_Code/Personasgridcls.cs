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
// ASP.NET Maker 9 Project Class
// (C)2011 e.World Technology Limited. All rights reserved.
//
public partial class AspNetMaker9_ControlVehicular: System.Web.UI.Page {

	// Page object
	public cPersonas_grid Personas_grid;	

	//
	// Page Class
	//
	public class cPersonas_grid: AspNetMakerPage, IDisposable {

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
				if (Personas.UseTokenInUrl)
					Url += "t=" + Personas.TableVar + "&"; // Add page token
				return Url;
			}
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
			get {				
				return ParentPage.Personas;
			}
			set {
				ParentPage.Personas = value;	
			}	
		}		

		// Areas	
		public cAreas Areas { 
			get {				
				return ParentPage.Areas;
			}
			set {
				ParentPage.Areas = value;	
			}	
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
		public cPersonas_grid(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "grid";
			m_PageObjName = "Personas_grid";
			m_PageObjTypeName = "cPersonas_grid";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (Personas == null)
				Personas = new cPersonas(this);
			if (Areas == null)
				Areas = new cAreas(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "Personas";
			m_Table = Personas;
			MasterTable = CurrentTable;

			//MasterTableType = CurrentTableType;
			CurrentTable = Personas;

			//CurrentTableType = Personas.GetType();
			Personas.LoadDetailParms();
			if (ew_NotEmpty(ew_Get("confirmpage")))
				ConfirmPage = ew_ConvertToBool(ew_Get("confirmpage"));
			if (Personas.CurrentMasterTable == "Areas") {
				Personas.IdArea.FldIsDetailKey = true;
				if (ew_NotEmpty(ew_Get("IdArea"))) {
					Personas.IdArea.CurrentValue = ew_Get("IdArea");
					Personas.IdArea.SessionValue = Personas.IdArea.CurrentValue;
				}
			}

			// Initialize URLs
			// Connect to database

			if (Conn == null)
				Conn = new cConnection();

			// Initialize list options
			ListOptions = new cListOptions();
		}

		//
		//  Page_Init
		//
		public void Page_Init() {
			Security = new cAdvancedSecurity(this);
			if (!Security.IsLoggedIn()) Security.AutoLogin();
			if (!Security.IsLoggedIn()) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
			}

			// Table Permission loading event
			Security.TablePermission_Loading();
			Security.LoadCurrentUserLevel(TableName);

			// Table Permission loaded event
			Security.TablePermission_Loaded();
			if (!Security.IsLoggedIn()) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
			}
			if (!Security.CanList) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
			}

			// UserID_Loading event
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();

			// UserID_Loaded event
			Security.UserID_Loaded();

			// Create form object
			ObjForm = new cFormObj();
			ObjForm.UseSession = (Personas.CurrentAction != "F");

			// Get grid add count
			int gridaddcnt = ew_ConvertToInt(ew_Get(EW_TABLE_GRID_ADD_ROW_COUNT));
			if (gridaddcnt > 0)
				Personas.GridAddRowCount = gridaddcnt;

			// Set up list options
			SetupListOptions();

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
			CurrentTable = MasterTable;

			//CurrentTableType = MasterTableType;
			if (ew_Empty(url))
				return;

			// Global page unloaded event (in ewglobal*.cs)
			ParentPage.Page_Unloaded();

			// Go to URL if specified
			string sRedirectUrl = url;
			Page_Redirecting(ref sRedirectUrl);
			Personas.Dispose();
			Areas.Dispose();
			Usuarios.Dispose();
			if (ew_NotEmpty(sRedirectUrl)) {
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.Redirect(sRedirectUrl); 
			}
		}

	public int DisplayRecs; // Number of display records

	public int StartRec;

	public int StopRec;

	public int TotalRecs;

	public int RecRange;

	public string SearchWhere;

	public int RecCnt;

	public int EditRowCnt;

	public int RowCnt;

	public object RowIndex;

	public int KeyCount = 0; // Key count

	public string RowAction = ""; // Row action

	public string RowOldKey = ""; // Row old key (for copy)

	public int RecPerRow;

	public int ColCnt;		

	public string DbMasterFilter;

	public string DbDetailFilter;

	public bool MasterRecordExists;

	public cListOptions ListOptions;

	public cListOptions ExportOptions; // Export options

	public string MultiSelectKey;

	public bool RestoreSearch;

	public int x__Priv;

	public SqlDataReader Recordset;

	public SqlDataReader OldRecordset; 

	//
	// Page main processing
	//
	public void Page_Main() {
		DisplayRecs = 20;
		RecRange = EW_PAGER_RANGE;
		RecCnt = 0; // Record count

		// Search filters
		string sSrchAdvanced = "";	// Advanced search filter
		string sSrchBasic = "";	// Basic search filter
		string sFilter = "";	// Search WHERE clause

		//sDeleteConfirmMsg = "<%= Language.Phrase("DeleteConfirmMsg") %>";
		// Master/Detail

		DbMasterFilter = ""; // Master filter
		DbDetailFilter = ""; // Detail filter
		if (IsPageRequest()) { // Validate request

			// Handle reset command
			ResetCmd();

			// Set up master detail parameters
			SetUpMasterParms();

			// Hide all options
			if (ew_NotEmpty(Personas.Export) ||
				Personas.CurrentAction == "gridadd" ||
				Personas.CurrentAction == "gridedit") {
				ListOptions.HideAllOptions();
			}

			// Show grid delete link for grid add / grid edit
			if (Personas.AllowAddDeleteRow) {
				if (Personas.CurrentAction == "gridadd" || Personas.CurrentAction == "gridedit") {
					cListOption item = ListOptions.GetItem("griddelete");
					if (item != null)
						item.Visible = true;
				}
			}

			// Set Up Sorting Order
			SetUpSortOrder();
		}

		// Restore display records
		if (Personas.RecordsPerPage == -1 || Personas.RecordsPerPage > 0) {
			DisplayRecs = Personas.RecordsPerPage; // Restore from Session
		} else {
			DisplayRecs = 20; // Load default
		}

		// Load Sorting Order
		LoadSortOrder();

		// Build filter
		sFilter = "";
		if (!Security.CanList)
			sFilter = "(0=1)"; // Filter all records

		// Restore master/detail filter
		DbMasterFilter = Personas.MasterFilter; // Restore master filter
		DbDetailFilter = Personas.DetailFilter; // Restore detail filter
		ew_AddFilter(ref sFilter, DbDetailFilter);
		ew_AddFilter(ref sFilter, SearchWhere);
		SqlDataReader RsMaster; 

		// Load master record
		if (Personas.CurrentMode != "add" && ew_NotEmpty(Personas.MasterFilter) && Personas.CurrentMasterTable == "Areas") {
			RsMaster = Areas.LoadRs(DbMasterFilter);
			MasterRecordExists = RsMaster != null;
			if (!MasterRecordExists) {
			} else {
				RsMaster.Read();
				Areas.LoadListRowValues(ref RsMaster);
				Areas.RowType = EW_ROWTYPE_MASTER; // Master row
				Areas.RenderListRow();
				RsMaster.Close();
				RsMaster.Dispose();
			}
		}

		// Set up filter in Session
		Personas.SessionWhere = sFilter;
		Personas.CurrentFilter = "";
	}

	//
	//  Exit out of inline mode
	//
	public void ClearInlineMode() {
		Personas.LastAction = Personas.CurrentAction; // Save last action
		Personas.CurrentAction = ""; // Clear action
		ew_Session[EW_SESSION_INLINE_MODE] = ""; // Clear inline mode
	}

	//
	// Switch to Grid Add Mode
	//
	public void GridAddMode() {
		ew_Session[EW_SESSION_INLINE_MODE] = "gridadd"; // Enabled grid add
	}

	//
	// Switch to Grid Edit Mode
	//
	public void GridEditMode() {
		ew_Session[EW_SESSION_INLINE_MODE] = "gridedit"; // Enabled grid edit
	}

	//
	// Peform update to grid
	//
	public bool GridUpdate() {
		int rowindex = 1;
		bool bGridUpdate = true;
		string sKey = "";
		string sSql;
		ArrayList RsOld, RsNew;

		// Get old recordset
		Personas.CurrentFilter = BuildKeyFilter();
		sSql = Personas.SQL;
		RsOld = Conn.GetRows(sSql);

		// Update row index and get row key
		ObjForm.Index = 0;
		int rowcnt = ew_ConvertToInt(ObjForm.GetValue("key_count"));

		// Update all rows based on key
		for (rowindex = 1; rowindex <= rowcnt; rowindex++) {
			ObjForm.Index = rowindex;
			string rowkey = ObjForm.GetValue("k_key");
			string rowaction = ObjForm.GetValue("k_action");

			// Load all values and keys
			if (rowaction != "insertdelete") { // Skip insert then deleted rows
				LoadFormValues(); // Get form values
				if (ew_Empty(rowaction) || rowaction == "edit" || rowaction == "delete") {
					bGridUpdate = SetupKeyValues(rowkey); // Set up key values
				} else {
					bGridUpdate = true;
				}

				// Skip empty row
				if (rowaction == "insert" && EmptyRow()) {

					// No action required
				// Validate form and insert/update/delete record

				} else if (bGridUpdate) {

					// Validate Form
					if (rowaction == "delete") {
						Personas.CurrentFilter = Personas.KeyFilter;
						bGridUpdate = DeleteRows(); // Delete this row
					} else if (!ValidateForm()) {
						bGridUpdate = false; // Form error, reset action
						FailureMessage = gsFormError;
					} else {
						if (rowaction == "insert") {
							bGridUpdate = AddRow(null); // Insert this row						
						} else {
							if (ew_NotEmpty(rowkey)) {
								Personas.SendEmail = false; // Do not send email on update success
								bGridUpdate = EditRow(); // Update this row
							}
						}	
					}
				}
				if (bGridUpdate) {
					if (ew_NotEmpty(sKey)) sKey += ", ";
					sKey += rowkey;
				} else {
					break;
				}
			}
		}
		if (bGridUpdate) {

			// Get new recordset
			RsNew = Conn.GetRows(sSql);
			ClearInlineMode(); // Clear inline edit mode
		} else {
			if (ew_Empty(FailureMessage))
				FailureMessage = Language.Phrase("UpdateFailed"); // Set update failed message
			Personas.EventCancelled = true; // Set event cancelled
			Personas.CurrentAction = "gridedit"; // Stay in gridedit mode
		}
		return bGridUpdate;
	}

	//
	//  Build filter for all keys
	//
	public string BuildKeyFilter() {
		int rowindex = 1;
		string sThisKey;
		string sKey = "";
		string sWrkFilter = "";
		string sFilter;

		// Update row index and get row key
		ObjForm.Index = rowindex;
		sThisKey = ObjForm.GetValue("k_key");
		while (ew_NotEmpty(sThisKey)) {
			if (SetupKeyValues(sThisKey))	{
				sFilter = Personas.KeyFilter;
				if (ew_NotEmpty(sWrkFilter)) sWrkFilter += " OR "; 
				sWrkFilter += sFilter;
			}	else	{
				sWrkFilter = "0=1";
				break;
			}

			// Update row index and get row key
			rowindex++;

			// next row
			ObjForm.Index = rowindex;
			sThisKey = ObjForm.GetValue("k_key");
		}
		return sWrkFilter;
	}

	//
	// Set up key values
	//
	public bool SetupKeyValues(string key) {
		string[] arKeyFlds;
		arKeyFlds = key.Split(new char[] {Convert.ToChar(EW_COMPOSITE_KEY_SEPARATOR)});
		if (arKeyFlds.Length >= 1) {
			Personas.IdPersona.FormValue = arKeyFlds[0];
			if (!Information.IsNumeric(Personas.IdPersona.FormValue)) return false;
			return true;
		}
		return false;
	}

	// Grid Insert
	public bool GridInsert() {
		int addcnt = 0;
		int rowcnt = 0;
		int rowindex = 1;
		bool bGridInsert = false;
		string sSql;
		string sWrkFilter = "";
		string sFilter;
		string sKey = "";
		ArrayList RsNew;

		// Get row count
		ObjForm.Index = 0;
		if (Information.IsNumeric(ObjForm.GetValue("key_count")))
			rowcnt = Convert.ToInt32(ObjForm.GetValue("key_count"));
		for (rowindex = 1; rowindex <= rowcnt; rowindex++) {	// Load current row values
			ObjForm.Index = rowindex;
			string rowaction = ObjForm.GetValue("k_action");
			if (ew_NotEmpty(rowaction) && rowaction != "insert")
				continue; // Skip
			if (rowaction == "insert") {
				RowOldKey = Convert.ToString(ObjForm.GetValue("k_oldkey"));
				LoadOldRecord(); // Load old recordset
			}
			LoadFormValues(); // Get form values
			if (!EmptyRow()) {
				addcnt++;
				Personas.SendEmail = false; // Do not send email on insert success

				// Validate Form
				if (!ValidateForm()) {
					bGridInsert = false;	// Form error, reset action
					FailureMessage = gsFormError;
				}	else {
					bGridInsert = AddRow(Conn.GetRow(ref OldRecordset)); // Insert this row
				}
				if (bGridInsert) {
					if (ew_NotEmpty(sKey)) sKey += EW_COMPOSITE_KEY_SEPARATOR;
					sKey += Personas.IdPersona.CurrentValue;

					// Add filter for this record
					sFilter = Personas.KeyFilter;					
					if (ew_NotEmpty(sWrkFilter)) sWrkFilter += " OR "; 
					sWrkFilter += sFilter;
				}	else	{
					break;
				}
			}
		}
		if (addcnt == 0) { // No record inserted
			ClearInlineMode(); // Clear grid add mode and return
			return true;
		}
		if (bGridInsert) {

			// Get new recordset
			Personas.CurrentFilter = sWrkFilter;
			sSql = Personas.SQL;
			RsNew = Conn.GetRows(sSql);
			ClearInlineMode(); // Clear grid add mode
		} else {
			if (addcnt == 0) { // No record inserted
				FailureMessage = Language.Phrase("NoAddRecord");
			} else if (ew_Empty(FailureMessage)) {
				FailureMessage = Language.Phrase("InsertFailed"); // Set insert failed message
			}
			Personas.EventCancelled = true; // Set event cancelled
			Personas.CurrentAction = "gridadd"; // Stay in gridadd mode
		}
		return bGridInsert;
	}

	public bool EmptyRow() {
		bool Empty = true;
		if (Empty && ObjForm.HasValue("x_IdArea") && ObjForm.HasValue("o_IdArea"))
			Empty = ew_SameStr(Personas.IdArea.CurrentValue, Personas.IdArea.OldValue);
		if (Empty && ObjForm.HasValue("x_IdCargo") && ObjForm.HasValue("o_IdCargo"))
			Empty = ew_SameStr(Personas.IdCargo.CurrentValue, Personas.IdCargo.OldValue);
		if (Empty && ObjForm.HasValue("x_Documento") && ObjForm.HasValue("o_Documento"))
			Empty = ew_SameStr(Personas.Documento.CurrentValue, Personas.Documento.OldValue);
		if (Empty && ObjForm.HasValue("x_Persona") && ObjForm.HasValue("o_Persona"))
			Empty = ew_SameStr(Personas.Persona.CurrentValue, Personas.Persona.OldValue);
		if (Empty && ObjForm.HasValue("x_Activa") && ObjForm.HasValue("o_Activa"))
			Empty = ew_SameStr(Personas.Activa.CurrentValue, Personas.Activa.OldValue);
		return Empty;
	}

	// Validate grid form
	public bool ValidateGridForm() {

		// Get row count
		ObjForm.Index = 0;
		int rowcnt = ew_ConvertToInt(ObjForm.GetValue("key_count"));

		// Validate all records
		for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {

			// Load current row values
			ObjForm.Index = rowindex;
			string rowaction = ObjForm.GetValue("k_action");
			if (rowaction != "delete" && rowaction != "insertdelete") {
				LoadFormValues(); // Get form values
				if (rowaction == "insert" && EmptyRow()) {

					// Ignore
				} else if (!ValidateForm()) {
					return false;
				}
			}
		}
		return true;
	}

	//
	// Restore form values for current row
	//
	public void RestoreCurrentRowFormValues(object index)	{
		string sKey = "";
		string[] arKeyFlds;		

		// Get row based on current index
		ObjForm.Index = ew_ConvertToInt(index); // ASPX
		LoadFormValues(); // Load form values
	}

	//
	// Set up Sort parameters based on Sort Links clicked
	//
	public void SetUpSortOrder() {

		// Check for an Order parameter
		if (ew_NotEmpty(ew_Get("order"))) {
			Personas.CurrentOrder = ew_Get("order");
			Personas.CurrentOrderType = ew_Get("ordertype");
			Personas.StartRecordNumber = 1; // Reset start position
		}
	}

	//
	// Load Sort Order parameters
	//
	public void LoadSortOrder()	{
		string sOrderBy = Personas.SessionOrderBy; // Get order by from Session
		if (ew_Empty(sOrderBy)) {
			if (ew_NotEmpty(Personas.SqlOrderBy)) {
				sOrderBy = Personas.SqlOrderBy;
				Personas.SessionOrderBy = sOrderBy;
			}
		}
	}

	//
	// Reset command based on querystring parameter "cmd"
	// - reset: reset search parameters
	// - resetall: reset search and master/detail parameters
	// - resetsort: reset sort parameters
	//
	public void ResetCmd() {
		string sCmd;

		// Get reset cmd
		if (ew_NotEmpty(ew_Get("cmd"))) {
			sCmd = ew_Get("cmd");

			// Reset master/detail keys
			if (ew_SameText(sCmd, "resetall")) {
				Personas.CurrentMasterTable = ""; // Clear master table
				DbMasterFilter = "";
				DbDetailFilter = "";
				Personas.IdArea.SessionValue = "";
			}

			// Reset sort criteria
			if (ew_SameText(sCmd, "resetsort")) {
				string sOrderBy = "";
				Personas.SessionOrderBy = sOrderBy;
			}

			// Reset start position
			StartRec = 1;
			Personas.StartRecordNumber = StartRec;
		}
	}

	//
	// Set up list options
	//
	public void SetupListOptions() {
		cListOption item;

		// "griddelete"
		if (Personas.AllowAddDeleteRow) {
			item = ListOptions.Add("griddelete");
			item.CssStyle = "white-space: nowrap;";
			item.OnLeft = true;
			item.Visible = false; // Default hidden
		}
	}

	// Render list options
	public void RenderListOptions() {
		cListOption oListOpt;
		ListOptions.LoadDefault();
		string links;

		// Set up row action and key
		if (Information.IsNumeric(RowIndex)) {
			ObjForm.Index = ew_ConvertToInt(RowIndex);
			if (ew_NotEmpty(RowAction))
				MultiSelectKey += "<input type=\"hidden\" name=\"k" + RowIndex + "_action\" id=\"k" + RowIndex + "_action\" value=\"" + RowAction + "\" />";
			if (ObjForm.HasValue("k_oldkey"))
				RowOldKey = ObjForm.GetValue("k_oldkey");
			if (ew_NotEmpty(RowOldKey))
				MultiSelectKey += "<input type=\"hidden\" name=\"k" + RowIndex + "_oldkey\" id=\"k" + RowIndex + "_oldkey\" value=\"" + ew_HtmlEncode(RowOldKey) + "\">";
			if (RowAction == "delete") {
				string rowkey = ObjForm.GetValue("k_key");
				SetupKeyValues(rowkey);
			}
			if (RowAction == "insert" && Personas.CurrentAction == "F" && EmptyRow())
		    MultiSelectKey += "<input type=\"hidden\" name=\"k" + RowIndex + "_blankrow\" id=\"k" + RowIndex + "_blankrow\" value=\"1\">";
		}

		// "delete"
		if (Personas.AllowAddDeleteRow) {
			if (Personas.CurrentMode == "add" || Personas.CurrentMode == "copy" || Personas.CurrentMode == "edit") {
				oListOpt = ListOptions.GetItem("griddelete");
				if (!Security.CanDelete && Information.IsNumeric(RowIndex) && (RowAction == "" || RowAction == "edit")) { // Do not allow delete existing record
					oListOpt.Body = "&nbsp;";
				} else {
					oListOpt.Body = "<a class=\"ewGridLink\" href=\"javascript:void(0);\" onclick=\"ew_DeleteGridRow(this, Personas_grid, " + RowIndex + ");\">" + Language.Phrase("DeleteLink") + "</a>";
				}
			}
		}
		if (Personas.CurrentMode == "edit" && Information.IsNumeric(RowIndex)) {
			MultiSelectKey += "<input type=\"hidden\" name=\"k" + RowIndex + "_key\" id=\"k" + RowIndex + "_key\" value=\"" + Convert.ToString(Personas.IdPersona.CurrentValue) + "\" />";
		}
		RenderListOptionsExt();
	}

	// Set record key
	public void SetRecordKey(ref string key, SqlDataReader dr) {
		if (dr == null)
			return;
		key = "";
		if (ew_NotEmpty(key))
			key += EW_COMPOSITE_KEY_SEPARATOR;
		key += dr["IdPersona"];
	}

	public void RenderListOptionsExt() {
	}

	public cNumericPager Pager;

	//
	// Set up Starting Record parameters
	//
	public void SetUpStartRec()	{
		int PageNo;

		// Exit if DisplayRecs = 0
		if (DisplayRecs == 0) return;
		if (IsPageRequest()) { // Validate request

			// Check for a "start" parameter
			if (ew_NotEmpty(ew_Get(EW_TABLE_START_REC)) && Information.IsNumeric(ew_Get(EW_TABLE_START_REC)))	{
				StartRec = ew_ConvertToInt(ew_Get(EW_TABLE_START_REC));
				Personas.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				Personas.StartRecordNumber = StartRec;
			}
		}
		StartRec = Personas.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			Personas.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			Personas.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			Personas.StartRecordNumber = StartRec;
		}
	}

	// Confirm page
	public bool ConfirmPage = false; // ASPX

	//
	// Get upload file
	//
	public void GetUploadFiles() {

		// Get upload data
		int index = ObjForm.Index; // Save form index
		ObjForm.Index = 0;
		bool confirmPage = ew_NotEmpty(ObjForm.GetValue("a_confirm"));
		ObjForm.Index = index; // Restore form index
	}

	//
	// Load default values
	//
	public void LoadDefaultValues() {
		Personas.IdArea.CurrentValue = System.DBNull.Value;
		Personas.IdArea.OldValue = Personas.IdArea.CurrentValue;
		Personas.IdCargo.CurrentValue = System.DBNull.Value;
		Personas.IdCargo.OldValue = Personas.IdCargo.CurrentValue;
		Personas.Documento.CurrentValue = System.DBNull.Value;
		Personas.Documento.OldValue = Personas.Documento.CurrentValue;
		Personas.Persona.CurrentValue = System.DBNull.Value;
		Personas.Persona.OldValue = Personas.Persona.CurrentValue;
		Personas.Activa.CurrentValue = System.DBNull.Value;
		Personas.Activa.OldValue = Personas.Activa.CurrentValue;
	}

	// Load saved form value from session
	public string GetFormValueFromSession(string name) {
		if (ew_Session["EW_FORM_VALUES"] != null) {
			NameValueCollection fv = (NameValueCollection)ew_Session["EW_FORM_VALUES"];
			string wrkname = ObjForm.GetIndexedName(name);
			if (fv[wrkname] != null)
				return fv[wrkname];
		}
		return "";
	}

	// Has saved form value from session
	public bool HasFormValueFromSession(string name) {
		if (ew_Session["EW_FORM_VALUES"] != null) {
			NameValueCollection fv = (NameValueCollection)ew_Session["EW_FORM_VALUES"];
			string wrkname = ObjForm.GetIndexedName(name);
			return fv[wrkname] != null;
		}
		return false;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
		if (!Personas.IdArea.FldIsDetailKey) {
			Personas.IdArea.FormValue = ObjForm.GetValue("x_IdArea");
		}
		if (HasFormValueFromSession("o_IdArea"))
			Personas.IdArea.OldValue = ObjForm.GetValue("o_IdArea");
		if (!Personas.IdCargo.FldIsDetailKey) {
			Personas.IdCargo.FormValue = ObjForm.GetValue("x_IdCargo");
		}
		if (HasFormValueFromSession("o_IdCargo"))
			Personas.IdCargo.OldValue = ObjForm.GetValue("o_IdCargo");
		if (!Personas.Documento.FldIsDetailKey) {
			Personas.Documento.FormValue = ObjForm.GetValue("x_Documento");
		}
		if (HasFormValueFromSession("o_Documento"))
			Personas.Documento.OldValue = ObjForm.GetValue("o_Documento");
		if (!Personas.Persona.FldIsDetailKey) {
			Personas.Persona.FormValue = ObjForm.GetValue("x_Persona");
		}
		if (HasFormValueFromSession("o_Persona"))
			Personas.Persona.OldValue = ObjForm.GetValue("o_Persona");
		if (!Personas.Activa.FldIsDetailKey) {
			Personas.Activa.FormValue = ObjForm.GetValue("x_Activa");
		}
		if (HasFormValueFromSession("o_Activa"))
			Personas.Activa.OldValue = ObjForm.GetValue("o_Activa");
		if (!Personas.IdPersona.FldIsDetailKey && Personas.CurrentAction != "gridadd" && Personas.CurrentAction != "add")
			Personas.IdPersona.FormValue = GetFormValueFromSession("x_IdPersona");
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		Personas.IdArea.CurrentValue = Personas.IdArea.FormValue;
		Personas.IdCargo.CurrentValue = Personas.IdCargo.FormValue;
		Personas.Documento.CurrentValue = Personas.Documento.FormValue;
		Personas.Persona.CurrentValue = Personas.Persona.FormValue;
		Personas.Activa.CurrentValue = Personas.Activa.FormValue;
		Personas.IdPersona.CurrentValue = Personas.IdPersona.FormValue;
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = Personas.CurrentFilter;
		Personas.Recordset_Selecting(ref sFilter);
		Personas.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = Personas.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		
		try {
			string sCntSql = Personas.SelectCountSQL;

			// Write SQL for debug
			if (EW_DEBUG_ENABLED)
				ew_SetDebugMsg(sCntSql);
			TotalRecs = Convert.ToInt32(Conn.ExecuteScalar(sCntSql));
		} catch {
			TotalRecs = -1;
		}

		// Load recordset
		SqlDataReader Rs = Conn.GetDataReader(sSql);		
		if (TotalRecs < 0 && Rs != null && Rs.HasRows)	{
			TotalRecs = 0;
			while (Rs.Read())
				TotalRecs++;
			Rs.Close();
			Rs = Conn.GetDataReader(sSql);
		}

		// Recordset Selected event
		Personas.Recordset_Selected(Rs);
		return Rs;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = Personas.KeyFilter;

		// Row Selecting event
		Personas.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		Personas.CurrentFilter = sFilter;
		string sSql = Personas.SQL;

		// Write SQL for debug
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); // Show SQL for debugging
		try {
			RsRow = Conn.GetTempDataReader(sSql);
			if (!RsRow.Read()) {
				return false;
			}	else {
				LoadRowValues(ref RsRow);
				return true;
			}
		}	catch {
			if (EW_DEBUG_ENABLED) throw; 
			return false;
		}	finally {
			Conn.CloseTempDataReader();
		}
	}

	//
	// Load row values from recordset
	//
	public void LoadRowValues(ref SqlDataReader dr) {
		if (dr == null)
			return;
		string sDetailFilter;

		// Call Row Selected event
		OrderedDictionary row = Conn.GetRow(ref dr);
		Personas.Row_Selected(ref row);
		Personas.IdPersona.DbValue = row["IdPersona"];
		Personas.IdArea.DbValue = row["IdArea"];
		Personas.IdCargo.DbValue = row["IdCargo"];
		Personas.Documento.DbValue = row["Documento"];
		Personas.Persona.DbValue = row["Persona"];
		Personas.Activa.DbValue = (ew_ConvertToBool(row["Activa"])) ? "1" : "0";
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		string[] arKeys = { RowOldKey };
		int cnt = arKeys.Length;
		if (cnt >= 1) {
			if (ew_NotEmpty(arKeys[0])) {
				Personas.IdPersona.CurrentValue = arKeys[0]; //  IdPersona
			} else {
				bValidKey = false;
			}
		} else {
			bValidKey = false;	
		}

		// Load old recordset
		if (bValidKey) {
			Personas.CurrentFilter = Personas.KeyFilter;
			string sSql = Personas.SQL;			
			OldRecordset = Conn.GetTempDataReader(sSql);
			if (OldRecordset != null && OldRecordset.Read())
				LoadRowValues(ref OldRecordset); // Load row values
				return true;
		} else {
			OldRecordset = null;
		}
		return bValidKey;
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
		// Row Rendering event

		Personas.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdPersona
		// IdArea
		// IdCargo
		// Documento
		// Persona
		// Activa
		//
		//  View  Row
		//

		if (Personas.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdPersona
				Personas.IdPersona.ViewValue = Personas.IdPersona.CurrentValue;
			Personas.IdPersona.ViewCustomAttributes = "";

			// IdArea
			if (ew_NotEmpty(Personas.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(Personas.IdArea.CurrentValue) + "";
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
					Personas.IdArea.ViewValue = drWrk["Area"];
					Personas.IdArea.ViewValue = String.Concat(Personas.IdArea.ViewValue, ew_ValueSeparator(0, 1, Personas.IdArea), drWrk["Codigo"]);
				} else {
					Personas.IdArea.ViewValue = Personas.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdArea.ViewValue = System.DBNull.Value;
			}
			Personas.IdArea.ViewCustomAttributes = "";

			// IdCargo
			if (ew_NotEmpty(Personas.IdCargo.CurrentValue)) {
				sFilterWrk = "[IdCargo] = " + ew_AdjustSql(Personas.IdCargo.CurrentValue) + "";
			sSqlWrk = "SELECT [Cargo] FROM [dbo].[Cargos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Cargo]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Personas.IdCargo.ViewValue = drWrk["Cargo"];
				} else {
					Personas.IdCargo.ViewValue = Personas.IdCargo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdCargo.ViewValue = System.DBNull.Value;
			}
			Personas.IdCargo.ViewCustomAttributes = "";

			// Documento
				Personas.Documento.ViewValue = Personas.Documento.CurrentValue;
			Personas.Documento.ViewCustomAttributes = "";

			// Persona
				Personas.Persona.ViewValue = Personas.Persona.CurrentValue;
			Personas.Persona.ViewCustomAttributes = "";

			// Activa
			if (Convert.ToString(Personas.Activa.CurrentValue) == "1") {
				Personas.Activa.ViewValue = (Personas.Activa.FldTagCaption(1) != "") ? Personas.Activa.FldTagCaption(1) : "Y";
			} else {
				Personas.Activa.ViewValue = (Personas.Activa.FldTagCaption(2) != "") ? Personas.Activa.FldTagCaption(2) : "N";
			}
			Personas.Activa.ViewCustomAttributes = "";

			// View refer script
			// IdArea

			Personas.IdArea.LinkCustomAttributes = "";
			Personas.IdArea.HrefValue = "";
			Personas.IdArea.TooltipValue = "";

			// IdCargo
			Personas.IdCargo.LinkCustomAttributes = "";
			Personas.IdCargo.HrefValue = "";
			Personas.IdCargo.TooltipValue = "";

			// Documento
			Personas.Documento.LinkCustomAttributes = "";
			Personas.Documento.HrefValue = "";
			Personas.Documento.TooltipValue = "";

			// Persona
			Personas.Persona.LinkCustomAttributes = "";
			Personas.Persona.HrefValue = "";
			Personas.Persona.TooltipValue = "";

			// Activa
			Personas.Activa.LinkCustomAttributes = "";
			Personas.Activa.HrefValue = "";
			Personas.Activa.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (Personas.RowType == EW_ROWTYPE_ADD) { // Add row

			// IdArea
			Personas.IdArea.EditCustomAttributes = "";
			if (ew_NotEmpty(Personas.IdArea.SessionValue)) {
				Personas.IdArea.CurrentValue = Personas.IdArea.SessionValue;
				Personas.IdArea.OldValue = Personas.IdArea.CurrentValue;
			if (ew_NotEmpty(Personas.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(Personas.IdArea.CurrentValue) + "";
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
					Personas.IdArea.ViewValue = drWrk["Area"];
					Personas.IdArea.ViewValue = String.Concat(Personas.IdArea.ViewValue, ew_ValueSeparator(0, 1, Personas.IdArea), drWrk["Codigo"]);
				} else {
					Personas.IdArea.ViewValue = Personas.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdArea.ViewValue = System.DBNull.Value;
			}
			Personas.IdArea.ViewCustomAttributes = "";
			} else {
			}

			// IdCargo
			Personas.IdCargo.EditCustomAttributes = "";

			// Documento
			Personas.Documento.EditCustomAttributes = "";
			Personas.Documento.EditValue = ew_HtmlEncode(Personas.Documento.CurrentValue);

			// Persona
			Personas.Persona.EditCustomAttributes = "";
			Personas.Persona.EditValue = ew_HtmlEncode(Personas.Persona.CurrentValue);

			// Activa
			Personas.Activa.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(1))) ? Personas.Activa.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(2))) ? Personas.Activa.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			Personas.Activa.EditValue = alwrk;

			// Edit refer script
			// IdArea

			Personas.IdArea.HrefValue = "";

			// IdCargo
			Personas.IdCargo.HrefValue = "";

			// Documento
			Personas.Documento.HrefValue = "";

			// Persona
			Personas.Persona.HrefValue = "";

			// Activa
			Personas.Activa.HrefValue = "";

		//
		//  Edit Row
		//

		} else if (Personas.RowType == EW_ROWTYPE_EDIT) { // Edit row

			// IdArea
			Personas.IdArea.EditCustomAttributes = "";
			if (ew_NotEmpty(Personas.IdArea.SessionValue)) {
				Personas.IdArea.CurrentValue = Personas.IdArea.SessionValue;
				Personas.IdArea.OldValue = Personas.IdArea.CurrentValue;
			if (ew_NotEmpty(Personas.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(Personas.IdArea.CurrentValue) + "";
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
					Personas.IdArea.ViewValue = drWrk["Area"];
					Personas.IdArea.ViewValue = String.Concat(Personas.IdArea.ViewValue, ew_ValueSeparator(0, 1, Personas.IdArea), drWrk["Codigo"]);
				} else {
					Personas.IdArea.ViewValue = Personas.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdArea.ViewValue = System.DBNull.Value;
			}
			Personas.IdArea.ViewCustomAttributes = "";
			} else {
			}

			// IdCargo
			Personas.IdCargo.EditCustomAttributes = "";

			// Documento
			Personas.Documento.EditCustomAttributes = "";
			Personas.Documento.EditValue = ew_HtmlEncode(Personas.Documento.CurrentValue);

			// Persona
			Personas.Persona.EditCustomAttributes = "";
			Personas.Persona.EditValue = ew_HtmlEncode(Personas.Persona.CurrentValue);

			// Activa
			Personas.Activa.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(1))) ? Personas.Activa.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(2))) ? Personas.Activa.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			Personas.Activa.EditValue = alwrk;

			// Edit refer script
			// IdArea

			Personas.IdArea.HrefValue = "";

			// IdCargo
			Personas.IdCargo.HrefValue = "";

			// Documento
			Personas.Documento.HrefValue = "";

			// Persona
			Personas.Persona.HrefValue = "";

			// Activa
			Personas.Activa.HrefValue = "";
		}
		if (Personas.RowType == EW_ROWTYPE_ADD ||
			Personas.RowType == EW_ROWTYPE_EDIT ||
			Personas.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			Personas.SetupFieldTitles();
		}

		// Row Rendered event
		if (Personas.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Personas.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(Personas.IdArea.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.IdArea.FldCaption);
		if (ew_Empty(Personas.IdCargo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.IdCargo.FldCaption);
		if (ew_Empty(Personas.Documento.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.Documento.FldCaption);
		if (ew_Empty(Personas.Persona.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.Persona.FldCaption);
		if (ew_Empty(Personas.Activa.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.Activa.FldCaption);

		// Return validate result
		bool Valid = (ew_Empty(gsFormError));

		// Form_CustomValidate event
		string sFormCustomError = "";
		Valid = Valid && Form_CustomValidate(ref sFormCustomError);
		ew_AddMessage(ref gsFormError, sFormCustomError);
		return Valid;
	}

	//
	// Delete records based on current filter
	//
	public bool DeleteRows() {
		bool result = true;
		string sKey = "";
		string sThisKey;
		string[] arKeyFlds;
		SqlDataReader Rs = null;
		SqlDataReader RsDelete = null;
		ArrayList RsOld;
		try {
			string sSql = Personas.SQL;
			Rs = Conn.GetDataReader(sSql);
			if (Rs == null) {
				return false;
			} else if (!Rs.HasRows) {
				FailureMessage = Language.Phrase("NoRecord"); // No record found
				Rs.Close();
				Rs.Dispose();
				return false;
			}
		}	catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw; 
			FailureMessage = e.Message;
			return false;
		}
		if (!Security.CanDelete) {
			FailureMessage = Language.Phrase("NoDeletePermission"); // No delete permission
			return false;
		}

		// Clone old rows
		RsOld = Conn.GetRows(ref Rs);
		Rs.Close();
		Rs.Dispose();

		// Call row deleting event
		if (result) {
			foreach (OrderedDictionary Row in RsOld) {
				result = Personas.Row_Deleting(Row);
				if (!result)
					break;
			}
		}
		if (result) {
			sKey = "";
			foreach (OrderedDictionary Row in RsOld) {
				sThisKey = "";
				if (ew_NotEmpty(sThisKey)) sThisKey += EW_COMPOSITE_KEY_SEPARATOR;
				sThisKey += Convert.ToString(Row["IdPersona"]);
				try {
					Personas.Delete(Row);
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message; // Set up error message
					result = false;
					break;
				}
				if (ew_NotEmpty(sKey)) sKey += ", "; 
				sKey += sThisKey;
			}
		} else {

			// Set up error message
			if (ew_NotEmpty(Personas.CancelMessage)) {
				FailureMessage = Personas.CancelMessage;
				Personas.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("DeleteCancelled");
			}
		}
		if (result) {

			// Row deleted event
			foreach (OrderedDictionary Row in RsOld)
				Personas.Row_Deleted(Row);
		} else {
			Conn.RollbackTrans(); // Rollback changes			
		}
		return result;
	}

	//
	// Update record based on key values
	//
	public bool EditRow()	{
		bool result = false;
		SqlDataReader RsEdit = null;
		SqlDataReader RsChk = null;
		string sSql;
		string sSqlChk;
		string sFilterChk;
		bool bUpdateRow;
		OrderedDictionary RsOld = null;
		string sIdxErrMsg;
		OrderedDictionary RsNew = new OrderedDictionary();
		string sFilter = Personas.KeyFilter;
		Personas.CurrentFilter = sFilter;
		sSql = Personas.SQL;
		try {
			RsEdit = Conn.GetDataReader(sSql);
		}	catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw; 
			FailureMessage = e.Message;
			RsEdit.Close();
			return false;
		}
		if (!RsEdit.Read())	{
			RsEdit.Close();
			return false; // Update Failed
		}	else {
			try {
				RsOld = Conn.GetRow(ref RsEdit);

			//RsEdit.Close();
				// IdArea

				Personas.IdArea.SetDbValue(ref RsNew, Personas.IdArea.CurrentValue, 0, Personas.IdArea.ReadOnly);

				// IdCargo
				Personas.IdCargo.SetDbValue(ref RsNew, Personas.IdCargo.CurrentValue, 0, Personas.IdCargo.ReadOnly);

				// Documento
				Personas.Documento.SetDbValue(ref RsNew, Personas.Documento.CurrentValue, "", Personas.Documento.ReadOnly);

				// Persona
				Personas.Persona.SetDbValue(ref RsNew, Personas.Persona.CurrentValue, "", Personas.Persona.ReadOnly);

				// Activa
				Personas.Activa.SetDbValue(ref RsNew, (Personas.Activa.CurrentValue != "" && !Convert.IsDBNull(Personas.Activa.CurrentValue)), false, Personas.Activa.ReadOnly);
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;
				RsEdit.Close();
				return false;
			}
			RsEdit.Close();

			// Row Updating event
			bUpdateRow = Personas.Row_Updating(RsOld, ref RsNew);
			if (bUpdateRow) {
				try {
					if (RsNew.Count > 0)
						result = Personas.Update(ref RsNew) > 0;
					else
						result = true; // No field to update
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message;
					return false;
				}
			}	else {
				if (ew_NotEmpty(Personas.CancelMessage)) {
					FailureMessage = Personas.CancelMessage;
					Personas.CancelMessage = "";
				} else {
					FailureMessage = Language.Phrase("UpdateCancelled");
				}
				result = false;
			}
		}

		// Row Updated event
		if (result)
			Personas.Row_Updated(RsOld, RsNew);
		return result;
	}

	//
	// Add record
	//
	public bool AddRow(OrderedDictionary RsOld) {
		bool result = false;
		OrderedDictionary RsNew = new OrderedDictionary();
		string sSql;
		string sFilter = "";
		bool bInsertRow;
		SqlDataReader RsChk = null;

		// Set up foreign key field value from Session
			if (Personas.CurrentMasterTable == "Areas") {
				Personas.IdArea.CurrentValue = Personas.IdArea.SessionValue;
			}
		try {

		// IdArea
		Personas.IdArea.SetDbValue(ref RsNew, Personas.IdArea.CurrentValue, 0, false);

		// IdCargo
		Personas.IdCargo.SetDbValue(ref RsNew, Personas.IdCargo.CurrentValue, 0, false);

		// Documento
		Personas.Documento.SetDbValue(ref RsNew, Personas.Documento.CurrentValue, "", false);

		// Persona
		Personas.Persona.SetDbValue(ref RsNew, Personas.Persona.CurrentValue, "", false);

		// Activa
		Personas.Activa.SetDbValue(ref RsNew, (Personas.Activa.CurrentValue != "" && !Convert.IsDBNull(Personas.Activa.CurrentValue)), false, false);
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = Personas.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				Personas.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(Personas.CancelMessage)) {
				FailureMessage = Personas.CancelMessage;
				Personas.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			Personas.IdPersona.DbValue = Conn.GetLastInsertId();
			RsNew["IdPersona"] = Personas.IdPersona.DbValue;
		}
		if (result) {

			// Row Inserted event
			Personas.Row_Inserted(RsOld, RsNew);
		}
		return result;
	}

	// Set up master/detail based on QueryString
	public void SetUpMasterParms() {

		// Hide foreign keys
		string sMasterTblVar = Personas.CurrentMasterTable;
		if (sMasterTblVar == "Areas") {
			Personas.IdArea.Visible = false;
			if (ew_ConvertToBool(ew_Get("mastereventcancelled")))
				Personas.EventCancelled = true;
		}
		DbMasterFilter = Personas.MasterFilter; // Get master filter
		DbDetailFilter = Personas.DetailFilter; // Get detail filter
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

		// Form Custom Validate event
		public bool Form_CustomValidate(ref string CustomError) {

			//Return error message in CustomError
			return true;
		}
	}
}
