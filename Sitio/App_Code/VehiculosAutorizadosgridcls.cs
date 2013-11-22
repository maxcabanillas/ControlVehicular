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
	public cVehiculosAutorizados_grid VehiculosAutorizados_grid;	

	//
	// Page Class
	//
	public class cVehiculosAutorizados_grid: AspNetMakerPage, IDisposable {

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
				if (VehiculosAutorizados.UseTokenInUrl)
					Url += "t=" + VehiculosAutorizados.TableVar + "&"; // Add page token
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
			get {				
				return ParentPage.VehiculosAutorizados;
			}
			set {
				ParentPage.VehiculosAutorizados = value;	
			}	
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
		public cVehiculosAutorizados_grid(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "grid";
			m_PageObjName = "VehiculosAutorizados_grid";
			m_PageObjTypeName = "cVehiculosAutorizados_grid";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (VehiculosAutorizados == null)
				VehiculosAutorizados = new cVehiculosAutorizados(this);
			if (Personas == null)
				Personas = new cPersonas(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);

			// Table
			m_TableName = "VehiculosAutorizados";
			m_Table = VehiculosAutorizados;
			MasterTable = CurrentTable;

			//MasterTableType = CurrentTableType;
			CurrentTable = VehiculosAutorizados;

			//CurrentTableType = VehiculosAutorizados.GetType();
			VehiculosAutorizados.LoadDetailParms();
			if (ew_NotEmpty(ew_Get("confirmpage")))
				ConfirmPage = ew_ConvertToBool(ew_Get("confirmpage"));
			if (VehiculosAutorizados.CurrentMasterTable == "Personas") {
				VehiculosAutorizados.IdPersona.FldIsDetailKey = true;
				if (ew_NotEmpty(ew_Get("IdPersona"))) {
					VehiculosAutorizados.IdPersona.CurrentValue = ew_Get("IdPersona");
					VehiculosAutorizados.IdPersona.SessionValue = VehiculosAutorizados.IdPersona.CurrentValue;
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
			ObjForm.UseSession = (VehiculosAutorizados.CurrentAction != "F");

			// Get grid add count
			int gridaddcnt = ew_ConvertToInt(ew_Get(EW_TABLE_GRID_ADD_ROW_COUNT));
			if (gridaddcnt > 0)
				VehiculosAutorizados.GridAddRowCount = gridaddcnt;

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
			VehiculosAutorizados.Dispose();
			Personas.Dispose();
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
			if (ew_NotEmpty(VehiculosAutorizados.Export) ||
				VehiculosAutorizados.CurrentAction == "gridadd" ||
				VehiculosAutorizados.CurrentAction == "gridedit") {
				ListOptions.HideAllOptions();
			}

			// Show grid delete link for grid add / grid edit
			if (VehiculosAutorizados.AllowAddDeleteRow) {
				if (VehiculosAutorizados.CurrentAction == "gridadd" || VehiculosAutorizados.CurrentAction == "gridedit") {
					cListOption item = ListOptions.GetItem("griddelete");
					if (item != null)
						item.Visible = true;
				}
			}

			// Set Up Sorting Order
			SetUpSortOrder();
		}

		// Restore display records
		if (VehiculosAutorizados.RecordsPerPage == -1 || VehiculosAutorizados.RecordsPerPage > 0) {
			DisplayRecs = VehiculosAutorizados.RecordsPerPage; // Restore from Session
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
		DbMasterFilter = VehiculosAutorizados.MasterFilter; // Restore master filter
		DbDetailFilter = VehiculosAutorizados.DetailFilter; // Restore detail filter
		ew_AddFilter(ref sFilter, DbDetailFilter);
		ew_AddFilter(ref sFilter, SearchWhere);
		SqlDataReader RsMaster; 

		// Load master record
		if (VehiculosAutorizados.CurrentMode != "add" && ew_NotEmpty(VehiculosAutorizados.MasterFilter) && VehiculosAutorizados.CurrentMasterTable == "Personas") {
			RsMaster = Personas.LoadRs(DbMasterFilter);
			MasterRecordExists = RsMaster != null;
			if (!MasterRecordExists) {
			} else {
				RsMaster.Read();
				Personas.LoadListRowValues(ref RsMaster);
				Personas.RowType = EW_ROWTYPE_MASTER; // Master row
				Personas.RenderListRow();
				RsMaster.Close();
				RsMaster.Dispose();
			}
		}

		// Set up filter in Session
		VehiculosAutorizados.SessionWhere = sFilter;
		VehiculosAutorizados.CurrentFilter = "";
	}

	//
	//  Exit out of inline mode
	//
	public void ClearInlineMode() {
		VehiculosAutorizados.LastAction = VehiculosAutorizados.CurrentAction; // Save last action
		VehiculosAutorizados.CurrentAction = ""; // Clear action
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
		VehiculosAutorizados.CurrentFilter = BuildKeyFilter();
		sSql = VehiculosAutorizados.SQL;
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
						VehiculosAutorizados.CurrentFilter = VehiculosAutorizados.KeyFilter;
						bGridUpdate = DeleteRows(); // Delete this row
					} else if (!ValidateForm()) {
						bGridUpdate = false; // Form error, reset action
						FailureMessage = gsFormError;
					} else {
						if (rowaction == "insert") {
							bGridUpdate = AddRow(null); // Insert this row						
						} else {
							if (ew_NotEmpty(rowkey)) {
								VehiculosAutorizados.SendEmail = false; // Do not send email on update success
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
			VehiculosAutorizados.EventCancelled = true; // Set event cancelled
			VehiculosAutorizados.CurrentAction = "gridedit"; // Stay in gridedit mode
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
				sFilter = VehiculosAutorizados.KeyFilter;
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
			VehiculosAutorizados.IdVehiculoAutorizado.FormValue = arKeyFlds[0];
			if (!Information.IsNumeric(VehiculosAutorizados.IdVehiculoAutorizado.FormValue)) return false;
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
				VehiculosAutorizados.SendEmail = false; // Do not send email on insert success

				// Validate Form
				if (!ValidateForm()) {
					bGridInsert = false;	// Form error, reset action
					FailureMessage = gsFormError;
				}	else {
					bGridInsert = AddRow(Conn.GetRow(ref OldRecordset)); // Insert this row
				}
				if (bGridInsert) {
					if (ew_NotEmpty(sKey)) sKey += EW_COMPOSITE_KEY_SEPARATOR;
					sKey += VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue;

					// Add filter for this record
					sFilter = VehiculosAutorizados.KeyFilter;					
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
			VehiculosAutorizados.CurrentFilter = sWrkFilter;
			sSql = VehiculosAutorizados.SQL;
			RsNew = Conn.GetRows(sSql);
			ClearInlineMode(); // Clear grid add mode
		} else {
			if (addcnt == 0) { // No record inserted
				FailureMessage = Language.Phrase("NoAddRecord");
			} else if (ew_Empty(FailureMessage)) {
				FailureMessage = Language.Phrase("InsertFailed"); // Set insert failed message
			}
			VehiculosAutorizados.EventCancelled = true; // Set event cancelled
			VehiculosAutorizados.CurrentAction = "gridadd"; // Stay in gridadd mode
		}
		return bGridInsert;
	}

	public bool EmptyRow() {
		bool Empty = true;
		if (Empty && ObjForm.HasValue("x_IdTipoVehiculo") && ObjForm.HasValue("o_IdTipoVehiculo"))
			Empty = ew_SameStr(VehiculosAutorizados.IdTipoVehiculo.CurrentValue, VehiculosAutorizados.IdTipoVehiculo.OldValue);
		if (Empty && ObjForm.HasValue("x_Placa") && ObjForm.HasValue("o_Placa"))
			Empty = ew_SameStr(VehiculosAutorizados.Placa.CurrentValue, VehiculosAutorizados.Placa.OldValue);
		if (Empty && ObjForm.HasValue("x_Autorizado") && ObjForm.HasValue("o_Autorizado"))
			Empty = ew_SameStr(VehiculosAutorizados.Autorizado.CurrentValue, VehiculosAutorizados.Autorizado.OldValue);
		if (Empty && ObjForm.HasValue("x_PicoyPlaca") && ObjForm.HasValue("o_PicoyPlaca"))
			Empty = ew_SameStr(VehiculosAutorizados.PicoyPlaca.CurrentValue, VehiculosAutorizados.PicoyPlaca.OldValue);
		if (Empty && ObjForm.HasValue("x_Lunes") && ObjForm.HasValue("o_Lunes"))
			Empty = ew_SameStr(VehiculosAutorizados.Lunes.CurrentValue, VehiculosAutorizados.Lunes.OldValue);
		if (Empty && ObjForm.HasValue("x_Martes") && ObjForm.HasValue("o_Martes"))
			Empty = ew_SameStr(VehiculosAutorizados.Martes.CurrentValue, VehiculosAutorizados.Martes.OldValue);
		if (Empty && ObjForm.HasValue("x_Miercoles") && ObjForm.HasValue("o_Miercoles"))
			Empty = ew_SameStr(VehiculosAutorizados.Miercoles.CurrentValue, VehiculosAutorizados.Miercoles.OldValue);
		if (Empty && ObjForm.HasValue("x_Jueves") && ObjForm.HasValue("o_Jueves"))
			Empty = ew_SameStr(VehiculosAutorizados.Jueves.CurrentValue, VehiculosAutorizados.Jueves.OldValue);
		if (Empty && ObjForm.HasValue("x_Viernes") && ObjForm.HasValue("o_Viernes"))
			Empty = ew_SameStr(VehiculosAutorizados.Viernes.CurrentValue, VehiculosAutorizados.Viernes.OldValue);
		if (Empty && ObjForm.HasValue("x_Sabado") && ObjForm.HasValue("o_Sabado"))
			Empty = ew_SameStr(VehiculosAutorizados.Sabado.CurrentValue, VehiculosAutorizados.Sabado.OldValue);
		if (Empty && ObjForm.HasValue("x_Domingo") && ObjForm.HasValue("o_Domingo"))
			Empty = ew_SameStr(VehiculosAutorizados.Domingo.CurrentValue, VehiculosAutorizados.Domingo.OldValue);
		if (Empty && ObjForm.HasValue("x_Marca") && ObjForm.HasValue("o_Marca"))
			Empty = ew_SameStr(VehiculosAutorizados.Marca.CurrentValue, VehiculosAutorizados.Marca.OldValue);
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
			VehiculosAutorizados.CurrentOrder = ew_Get("order");
			VehiculosAutorizados.CurrentOrderType = ew_Get("ordertype");
			VehiculosAutorizados.StartRecordNumber = 1; // Reset start position
		}
	}

	//
	// Load Sort Order parameters
	//
	public void LoadSortOrder()	{
		string sOrderBy = VehiculosAutorizados.SessionOrderBy; // Get order by from Session
		if (ew_Empty(sOrderBy)) {
			if (ew_NotEmpty(VehiculosAutorizados.SqlOrderBy)) {
				sOrderBy = VehiculosAutorizados.SqlOrderBy;
				VehiculosAutorizados.SessionOrderBy = sOrderBy;
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
				VehiculosAutorizados.CurrentMasterTable = ""; // Clear master table
				DbMasterFilter = "";
				DbDetailFilter = "";
				VehiculosAutorizados.IdPersona.SessionValue = "";
			}

			// Reset sort criteria
			if (ew_SameText(sCmd, "resetsort")) {
				string sOrderBy = "";
				VehiculosAutorizados.SessionOrderBy = sOrderBy;
			}

			// Reset start position
			StartRec = 1;
			VehiculosAutorizados.StartRecordNumber = StartRec;
		}
	}

	//
	// Set up list options
	//
	public void SetupListOptions() {
		cListOption item;

		// "griddelete"
		if (VehiculosAutorizados.AllowAddDeleteRow) {
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
			if (RowAction == "insert" && VehiculosAutorizados.CurrentAction == "F" && EmptyRow())
		    MultiSelectKey += "<input type=\"hidden\" name=\"k" + RowIndex + "_blankrow\" id=\"k" + RowIndex + "_blankrow\" value=\"1\">";
		}

		// "delete"
		if (VehiculosAutorizados.AllowAddDeleteRow) {
			if (VehiculosAutorizados.CurrentMode == "add" || VehiculosAutorizados.CurrentMode == "copy" || VehiculosAutorizados.CurrentMode == "edit") {
				oListOpt = ListOptions.GetItem("griddelete");
				if (!Security.CanDelete && Information.IsNumeric(RowIndex) && (RowAction == "" || RowAction == "edit")) { // Do not allow delete existing record
					oListOpt.Body = "&nbsp;";
				} else {
					oListOpt.Body = "<a class=\"ewGridLink\" href=\"javascript:void(0);\" onclick=\"ew_DeleteGridRow(this, VehiculosAutorizados_grid, " + RowIndex + ");\">" + Language.Phrase("DeleteLink") + "</a>";
				}
			}
		}
		if (VehiculosAutorizados.CurrentMode == "edit" && Information.IsNumeric(RowIndex)) {
			MultiSelectKey += "<input type=\"hidden\" name=\"k" + RowIndex + "_key\" id=\"k" + RowIndex + "_key\" value=\"" + Convert.ToString(VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue) + "\" />";
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
		key += dr["IdVehiculoAutorizado"];
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
				VehiculosAutorizados.StartRecordNumber = StartRec;
			} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && Information.IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
				PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
				StartRec = (PageNo - 1) * DisplayRecs + 1;
				if (StartRec <= 0)	{
					StartRec = 1;
				} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
				}
				VehiculosAutorizados.StartRecordNumber = StartRec;
			}
		}
		StartRec = VehiculosAutorizados.StartRecordNumber;

		// Check if correct start record counter
		if (StartRec <= 0)	{	// Avoid invalid start record counter
			StartRec = 1;	// Reset start record counter
			VehiculosAutorizados.StartRecordNumber = StartRec;
		} else if (StartRec > TotalRecs) {	// Avoid starting record > total records
			StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to last page first record
			VehiculosAutorizados.StartRecordNumber = StartRec;
		} else if ((StartRec - 1) % DisplayRecs != 0) {
			StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1;	// Point to page boundary
			VehiculosAutorizados.StartRecordNumber = StartRec;
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
		VehiculosAutorizados.IdTipoVehiculo.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.IdTipoVehiculo.OldValue = VehiculosAutorizados.IdTipoVehiculo.CurrentValue;
		VehiculosAutorizados.Placa.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Placa.OldValue = VehiculosAutorizados.Placa.CurrentValue;
		VehiculosAutorizados.Autorizado.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Autorizado.OldValue = VehiculosAutorizados.Autorizado.CurrentValue;
		VehiculosAutorizados.PicoyPlaca.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.PicoyPlaca.OldValue = VehiculosAutorizados.PicoyPlaca.CurrentValue;
		VehiculosAutorizados.Lunes.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Lunes.OldValue = VehiculosAutorizados.Lunes.CurrentValue;
		VehiculosAutorizados.Martes.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Martes.OldValue = VehiculosAutorizados.Martes.CurrentValue;
		VehiculosAutorizados.Miercoles.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Miercoles.OldValue = VehiculosAutorizados.Miercoles.CurrentValue;
		VehiculosAutorizados.Jueves.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Jueves.OldValue = VehiculosAutorizados.Jueves.CurrentValue;
		VehiculosAutorizados.Viernes.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Viernes.OldValue = VehiculosAutorizados.Viernes.CurrentValue;
		VehiculosAutorizados.Sabado.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Sabado.OldValue = VehiculosAutorizados.Sabado.CurrentValue;
		VehiculosAutorizados.Domingo.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Domingo.OldValue = VehiculosAutorizados.Domingo.CurrentValue;
		VehiculosAutorizados.Marca.CurrentValue = System.DBNull.Value;
		VehiculosAutorizados.Marca.OldValue = VehiculosAutorizados.Marca.CurrentValue;
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
		if (!VehiculosAutorizados.IdTipoVehiculo.FldIsDetailKey) {
			VehiculosAutorizados.IdTipoVehiculo.FormValue = ObjForm.GetValue("x_IdTipoVehiculo");
		}
		if (HasFormValueFromSession("o_IdTipoVehiculo"))
			VehiculosAutorizados.IdTipoVehiculo.OldValue = ObjForm.GetValue("o_IdTipoVehiculo");
		if (!VehiculosAutorizados.Placa.FldIsDetailKey) {
			VehiculosAutorizados.Placa.FormValue = ObjForm.GetValue("x_Placa");
		}
		if (HasFormValueFromSession("o_Placa"))
			VehiculosAutorizados.Placa.OldValue = ObjForm.GetValue("o_Placa");
		if (!VehiculosAutorizados.Autorizado.FldIsDetailKey) {
			VehiculosAutorizados.Autorizado.FormValue = ObjForm.GetValue("x_Autorizado");
		}
		if (HasFormValueFromSession("o_Autorizado"))
			VehiculosAutorizados.Autorizado.OldValue = ObjForm.GetValue("o_Autorizado");
		if (!VehiculosAutorizados.PicoyPlaca.FldIsDetailKey) {
			VehiculosAutorizados.PicoyPlaca.FormValue = ObjForm.GetValue("x_PicoyPlaca");
		}
		if (HasFormValueFromSession("o_PicoyPlaca"))
			VehiculosAutorizados.PicoyPlaca.OldValue = ObjForm.GetValue("o_PicoyPlaca");
		if (!VehiculosAutorizados.Lunes.FldIsDetailKey) {
			VehiculosAutorizados.Lunes.FormValue = ObjForm.GetValue("x_Lunes");
		}
		if (HasFormValueFromSession("o_Lunes"))
			VehiculosAutorizados.Lunes.OldValue = ObjForm.GetValue("o_Lunes");
		if (!VehiculosAutorizados.Martes.FldIsDetailKey) {
			VehiculosAutorizados.Martes.FormValue = ObjForm.GetValue("x_Martes");
		}
		if (HasFormValueFromSession("o_Martes"))
			VehiculosAutorizados.Martes.OldValue = ObjForm.GetValue("o_Martes");
		if (!VehiculosAutorizados.Miercoles.FldIsDetailKey) {
			VehiculosAutorizados.Miercoles.FormValue = ObjForm.GetValue("x_Miercoles");
		}
		if (HasFormValueFromSession("o_Miercoles"))
			VehiculosAutorizados.Miercoles.OldValue = ObjForm.GetValue("o_Miercoles");
		if (!VehiculosAutorizados.Jueves.FldIsDetailKey) {
			VehiculosAutorizados.Jueves.FormValue = ObjForm.GetValue("x_Jueves");
		}
		if (HasFormValueFromSession("o_Jueves"))
			VehiculosAutorizados.Jueves.OldValue = ObjForm.GetValue("o_Jueves");
		if (!VehiculosAutorizados.Viernes.FldIsDetailKey) {
			VehiculosAutorizados.Viernes.FormValue = ObjForm.GetValue("x_Viernes");
		}
		if (HasFormValueFromSession("o_Viernes"))
			VehiculosAutorizados.Viernes.OldValue = ObjForm.GetValue("o_Viernes");
		if (!VehiculosAutorizados.Sabado.FldIsDetailKey) {
			VehiculosAutorizados.Sabado.FormValue = ObjForm.GetValue("x_Sabado");
		}
		if (HasFormValueFromSession("o_Sabado"))
			VehiculosAutorizados.Sabado.OldValue = ObjForm.GetValue("o_Sabado");
		if (!VehiculosAutorizados.Domingo.FldIsDetailKey) {
			VehiculosAutorizados.Domingo.FormValue = ObjForm.GetValue("x_Domingo");
		}
		if (HasFormValueFromSession("o_Domingo"))
			VehiculosAutorizados.Domingo.OldValue = ObjForm.GetValue("o_Domingo");
		if (!VehiculosAutorizados.Marca.FldIsDetailKey) {
			VehiculosAutorizados.Marca.FormValue = ObjForm.GetValue("x_Marca");
		}
		if (HasFormValueFromSession("o_Marca"))
			VehiculosAutorizados.Marca.OldValue = ObjForm.GetValue("o_Marca");
		if (!VehiculosAutorizados.IdVehiculoAutorizado.FldIsDetailKey && VehiculosAutorizados.CurrentAction != "gridadd" && VehiculosAutorizados.CurrentAction != "add")
			VehiculosAutorizados.IdVehiculoAutorizado.FormValue = GetFormValueFromSession("x_IdVehiculoAutorizado");
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		VehiculosAutorizados.IdTipoVehiculo.CurrentValue = VehiculosAutorizados.IdTipoVehiculo.FormValue;
		VehiculosAutorizados.Placa.CurrentValue = VehiculosAutorizados.Placa.FormValue;
		VehiculosAutorizados.Autorizado.CurrentValue = VehiculosAutorizados.Autorizado.FormValue;
		VehiculosAutorizados.PicoyPlaca.CurrentValue = VehiculosAutorizados.PicoyPlaca.FormValue;
		VehiculosAutorizados.Lunes.CurrentValue = VehiculosAutorizados.Lunes.FormValue;
		VehiculosAutorizados.Martes.CurrentValue = VehiculosAutorizados.Martes.FormValue;
		VehiculosAutorizados.Miercoles.CurrentValue = VehiculosAutorizados.Miercoles.FormValue;
		VehiculosAutorizados.Jueves.CurrentValue = VehiculosAutorizados.Jueves.FormValue;
		VehiculosAutorizados.Viernes.CurrentValue = VehiculosAutorizados.Viernes.FormValue;
		VehiculosAutorizados.Sabado.CurrentValue = VehiculosAutorizados.Sabado.FormValue;
		VehiculosAutorizados.Domingo.CurrentValue = VehiculosAutorizados.Domingo.FormValue;
		VehiculosAutorizados.Marca.CurrentValue = VehiculosAutorizados.Marca.FormValue;
		VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue = VehiculosAutorizados.IdVehiculoAutorizado.FormValue;
	}

	//
	// Load recordset
	//
	public SqlDataReader LoadRecordset() {

		// Recordset Selecting event
		string sFilter = VehiculosAutorizados.CurrentFilter;
		VehiculosAutorizados.Recordset_Selecting(ref sFilter);
		VehiculosAutorizados.CurrentFilter = sFilter;

		// Load list page SQL
		string sSql = VehiculosAutorizados.ListSQL;
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); 

		// Count
		TotalRecs = -1;		
		try {
			string sCntSql = VehiculosAutorizados.SelectCountSQL;

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
		VehiculosAutorizados.Recordset_Selected(Rs);
		return Rs;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = VehiculosAutorizados.KeyFilter;

		// Row Selecting event
		VehiculosAutorizados.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		VehiculosAutorizados.CurrentFilter = sFilter;
		string sSql = VehiculosAutorizados.SQL;

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
		VehiculosAutorizados.Row_Selected(ref row);
		VehiculosAutorizados.IdVehiculoAutorizado.DbValue = row["IdVehiculoAutorizado"];
		VehiculosAutorizados.IdTipoVehiculo.DbValue = row["IdTipoVehiculo"];
		VehiculosAutorizados.Placa.DbValue = row["Placa"];
		VehiculosAutorizados.Autorizado.DbValue = (ew_ConvertToBool(row["Autorizado"])) ? "1" : "0";
		VehiculosAutorizados.IdPersona.DbValue = row["IdPersona"];
		VehiculosAutorizados.PicoyPlaca.DbValue = (ew_ConvertToBool(row["PicoyPlaca"])) ? "1" : "0";
		VehiculosAutorizados.Lunes.DbValue = (ew_ConvertToBool(row["Lunes"])) ? "1" : "0";
		VehiculosAutorizados.Martes.DbValue = (ew_ConvertToBool(row["Martes"])) ? "1" : "0";
		VehiculosAutorizados.Miercoles.DbValue = (ew_ConvertToBool(row["Miercoles"])) ? "1" : "0";
		VehiculosAutorizados.Jueves.DbValue = (ew_ConvertToBool(row["Jueves"])) ? "1" : "0";
		VehiculosAutorizados.Viernes.DbValue = (ew_ConvertToBool(row["Viernes"])) ? "1" : "0";
		VehiculosAutorizados.Sabado.DbValue = (ew_ConvertToBool(row["Sabado"])) ? "1" : "0";
		VehiculosAutorizados.Domingo.DbValue = (ew_ConvertToBool(row["Domingo"])) ? "1" : "0";
		VehiculosAutorizados.Marca.DbValue = row["Marca"];
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		string[] arKeys = { RowOldKey };
		int cnt = arKeys.Length;
		if (cnt >= 1) {
			if (ew_NotEmpty(arKeys[0])) {
				VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue = arKeys[0]; //  IdVehiculoAutorizado
			} else {
				bValidKey = false;
			}
		} else {
			bValidKey = false;	
		}

		// Load old recordset
		if (bValidKey) {
			VehiculosAutorizados.CurrentFilter = VehiculosAutorizados.KeyFilter;
			string sSql = VehiculosAutorizados.SQL;			
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

		VehiculosAutorizados.Row_Rendering();

		//
		//  Common render codes for all row types
		//
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
		//
		//  View  Row
		//

		if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdVehiculoAutorizado
				VehiculosAutorizados.IdVehiculoAutorizado.ViewValue = VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue;
			VehiculosAutorizados.IdVehiculoAutorizado.ViewCustomAttributes = "";

			// IdTipoVehiculo
			if (ew_NotEmpty(VehiculosAutorizados.IdTipoVehiculo.CurrentValue)) {
				sFilterWrk = "[IdTipoVehiculo] = " + ew_AdjustSql(VehiculosAutorizados.IdTipoVehiculo.CurrentValue) + "";
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
					VehiculosAutorizados.IdTipoVehiculo.ViewValue = drWrk["TipoVehiculo"];
				} else {
					VehiculosAutorizados.IdTipoVehiculo.ViewValue = VehiculosAutorizados.IdTipoVehiculo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				VehiculosAutorizados.IdTipoVehiculo.ViewValue = System.DBNull.Value;
			}
			VehiculosAutorizados.IdTipoVehiculo.ViewCustomAttributes = "";

			// Placa
				VehiculosAutorizados.Placa.ViewValue = VehiculosAutorizados.Placa.CurrentValue;
			VehiculosAutorizados.Placa.ViewCustomAttributes = "";

			// Autorizado
			if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") {
				VehiculosAutorizados.Autorizado.ViewValue = (VehiculosAutorizados.Autorizado.FldTagCaption(1) != "") ? VehiculosAutorizados.Autorizado.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Autorizado.ViewValue = (VehiculosAutorizados.Autorizado.FldTagCaption(2) != "") ? VehiculosAutorizados.Autorizado.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Autorizado.ViewCustomAttributes = "";

			// IdPersona
			VehiculosAutorizados.IdPersona.ViewCustomAttributes = "";

			// PicoyPlaca
			if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") {
				VehiculosAutorizados.PicoyPlaca.ViewValue = (VehiculosAutorizados.PicoyPlaca.FldTagCaption(1) != "") ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.PicoyPlaca.ViewValue = (VehiculosAutorizados.PicoyPlaca.FldTagCaption(2) != "") ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.PicoyPlaca.ViewCustomAttributes = "";

			// Lunes
			if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") {
				VehiculosAutorizados.Lunes.ViewValue = (VehiculosAutorizados.Lunes.FldTagCaption(1) != "") ? VehiculosAutorizados.Lunes.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Lunes.ViewValue = (VehiculosAutorizados.Lunes.FldTagCaption(2) != "") ? VehiculosAutorizados.Lunes.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Lunes.ViewCustomAttributes = "";

			// Martes
			if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") {
				VehiculosAutorizados.Martes.ViewValue = (VehiculosAutorizados.Martes.FldTagCaption(1) != "") ? VehiculosAutorizados.Martes.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Martes.ViewValue = (VehiculosAutorizados.Martes.FldTagCaption(2) != "") ? VehiculosAutorizados.Martes.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Martes.ViewCustomAttributes = "";

			// Miercoles
			if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") {
				VehiculosAutorizados.Miercoles.ViewValue = (VehiculosAutorizados.Miercoles.FldTagCaption(1) != "") ? VehiculosAutorizados.Miercoles.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Miercoles.ViewValue = (VehiculosAutorizados.Miercoles.FldTagCaption(2) != "") ? VehiculosAutorizados.Miercoles.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Miercoles.ViewCustomAttributes = "";

			// Jueves
			if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") {
				VehiculosAutorizados.Jueves.ViewValue = (VehiculosAutorizados.Jueves.FldTagCaption(1) != "") ? VehiculosAutorizados.Jueves.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Jueves.ViewValue = (VehiculosAutorizados.Jueves.FldTagCaption(2) != "") ? VehiculosAutorizados.Jueves.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Jueves.ViewCustomAttributes = "";

			// Viernes
			if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") {
				VehiculosAutorizados.Viernes.ViewValue = (VehiculosAutorizados.Viernes.FldTagCaption(1) != "") ? VehiculosAutorizados.Viernes.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Viernes.ViewValue = (VehiculosAutorizados.Viernes.FldTagCaption(2) != "") ? VehiculosAutorizados.Viernes.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Viernes.ViewCustomAttributes = "";

			// Sabado
			if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") {
				VehiculosAutorizados.Sabado.ViewValue = (VehiculosAutorizados.Sabado.FldTagCaption(1) != "") ? VehiculosAutorizados.Sabado.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Sabado.ViewValue = (VehiculosAutorizados.Sabado.FldTagCaption(2) != "") ? VehiculosAutorizados.Sabado.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Sabado.ViewCustomAttributes = "";

			// Domingo
			if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") {
				VehiculosAutorizados.Domingo.ViewValue = (VehiculosAutorizados.Domingo.FldTagCaption(1) != "") ? VehiculosAutorizados.Domingo.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Domingo.ViewValue = (VehiculosAutorizados.Domingo.FldTagCaption(2) != "") ? VehiculosAutorizados.Domingo.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Domingo.ViewCustomAttributes = "";

			// Marca
				VehiculosAutorizados.Marca.ViewValue = VehiculosAutorizados.Marca.CurrentValue;
			VehiculosAutorizados.Marca.ViewCustomAttributes = "";

			// View refer script
			// IdTipoVehiculo

			VehiculosAutorizados.IdTipoVehiculo.LinkCustomAttributes = "";
			VehiculosAutorizados.IdTipoVehiculo.HrefValue = "";
			VehiculosAutorizados.IdTipoVehiculo.TooltipValue = "";

			// Placa
			VehiculosAutorizados.Placa.LinkCustomAttributes = "";
			VehiculosAutorizados.Placa.HrefValue = "";
			VehiculosAutorizados.Placa.TooltipValue = "";

			// Autorizado
			VehiculosAutorizados.Autorizado.LinkCustomAttributes = "";
			VehiculosAutorizados.Autorizado.HrefValue = "";
			VehiculosAutorizados.Autorizado.TooltipValue = "";

			// PicoyPlaca
			VehiculosAutorizados.PicoyPlaca.LinkCustomAttributes = "";
			VehiculosAutorizados.PicoyPlaca.HrefValue = "";
			VehiculosAutorizados.PicoyPlaca.TooltipValue = "";

			// Lunes
			VehiculosAutorizados.Lunes.LinkCustomAttributes = "";
			VehiculosAutorizados.Lunes.HrefValue = "";
			VehiculosAutorizados.Lunes.TooltipValue = "";

			// Martes
			VehiculosAutorizados.Martes.LinkCustomAttributes = "";
			VehiculosAutorizados.Martes.HrefValue = "";
			VehiculosAutorizados.Martes.TooltipValue = "";

			// Miercoles
			VehiculosAutorizados.Miercoles.LinkCustomAttributes = "";
			VehiculosAutorizados.Miercoles.HrefValue = "";
			VehiculosAutorizados.Miercoles.TooltipValue = "";

			// Jueves
			VehiculosAutorizados.Jueves.LinkCustomAttributes = "";
			VehiculosAutorizados.Jueves.HrefValue = "";
			VehiculosAutorizados.Jueves.TooltipValue = "";

			// Viernes
			VehiculosAutorizados.Viernes.LinkCustomAttributes = "";
			VehiculosAutorizados.Viernes.HrefValue = "";
			VehiculosAutorizados.Viernes.TooltipValue = "";

			// Sabado
			VehiculosAutorizados.Sabado.LinkCustomAttributes = "";
			VehiculosAutorizados.Sabado.HrefValue = "";
			VehiculosAutorizados.Sabado.TooltipValue = "";

			// Domingo
			VehiculosAutorizados.Domingo.LinkCustomAttributes = "";
			VehiculosAutorizados.Domingo.HrefValue = "";
			VehiculosAutorizados.Domingo.TooltipValue = "";

			// Marca
			VehiculosAutorizados.Marca.LinkCustomAttributes = "";
			VehiculosAutorizados.Marca.HrefValue = "";
			VehiculosAutorizados.Marca.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add row

			// IdTipoVehiculo
			VehiculosAutorizados.IdTipoVehiculo.EditCustomAttributes = "";

			// Placa
			VehiculosAutorizados.Placa.EditCustomAttributes = "";
			VehiculosAutorizados.Placa.EditValue = ew_HtmlEncode(VehiculosAutorizados.Placa.CurrentValue);

			// Autorizado
			VehiculosAutorizados.Autorizado.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Autorizado.FldTagCaption(1))) ? VehiculosAutorizados.Autorizado.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Autorizado.FldTagCaption(2))) ? VehiculosAutorizados.Autorizado.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Autorizado.EditValue = alwrk;

			// PicoyPlaca
			VehiculosAutorizados.PicoyPlaca.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.PicoyPlaca.FldTagCaption(1))) ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.PicoyPlaca.FldTagCaption(2))) ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.PicoyPlaca.EditValue = alwrk;

			// Lunes
			VehiculosAutorizados.Lunes.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Lunes.FldTagCaption(1))) ? VehiculosAutorizados.Lunes.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Lunes.FldTagCaption(2))) ? VehiculosAutorizados.Lunes.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Lunes.EditValue = alwrk;

			// Martes
			VehiculosAutorizados.Martes.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Martes.FldTagCaption(1))) ? VehiculosAutorizados.Martes.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Martes.FldTagCaption(2))) ? VehiculosAutorizados.Martes.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Martes.EditValue = alwrk;

			// Miercoles
			VehiculosAutorizados.Miercoles.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Miercoles.FldTagCaption(1))) ? VehiculosAutorizados.Miercoles.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Miercoles.FldTagCaption(2))) ? VehiculosAutorizados.Miercoles.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Miercoles.EditValue = alwrk;

			// Jueves
			VehiculosAutorizados.Jueves.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Jueves.FldTagCaption(1))) ? VehiculosAutorizados.Jueves.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Jueves.FldTagCaption(2))) ? VehiculosAutorizados.Jueves.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Jueves.EditValue = alwrk;

			// Viernes
			VehiculosAutorizados.Viernes.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Viernes.FldTagCaption(1))) ? VehiculosAutorizados.Viernes.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Viernes.FldTagCaption(2))) ? VehiculosAutorizados.Viernes.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Viernes.EditValue = alwrk;

			// Sabado
			VehiculosAutorizados.Sabado.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Sabado.FldTagCaption(1))) ? VehiculosAutorizados.Sabado.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Sabado.FldTagCaption(2))) ? VehiculosAutorizados.Sabado.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Sabado.EditValue = alwrk;

			// Domingo
			VehiculosAutorizados.Domingo.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Domingo.FldTagCaption(1))) ? VehiculosAutorizados.Domingo.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Domingo.FldTagCaption(2))) ? VehiculosAutorizados.Domingo.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Domingo.EditValue = alwrk;

			// Marca
			VehiculosAutorizados.Marca.EditCustomAttributes = "";
			VehiculosAutorizados.Marca.EditValue = ew_HtmlEncode(VehiculosAutorizados.Marca.CurrentValue);

			// Edit refer script
			// IdTipoVehiculo

			VehiculosAutorizados.IdTipoVehiculo.HrefValue = "";

			// Placa
			VehiculosAutorizados.Placa.HrefValue = "";

			// Autorizado
			VehiculosAutorizados.Autorizado.HrefValue = "";

			// PicoyPlaca
			VehiculosAutorizados.PicoyPlaca.HrefValue = "";

			// Lunes
			VehiculosAutorizados.Lunes.HrefValue = "";

			// Martes
			VehiculosAutorizados.Martes.HrefValue = "";

			// Miercoles
			VehiculosAutorizados.Miercoles.HrefValue = "";

			// Jueves
			VehiculosAutorizados.Jueves.HrefValue = "";

			// Viernes
			VehiculosAutorizados.Viernes.HrefValue = "";

			// Sabado
			VehiculosAutorizados.Sabado.HrefValue = "";

			// Domingo
			VehiculosAutorizados.Domingo.HrefValue = "";

			// Marca
			VehiculosAutorizados.Marca.HrefValue = "";

		//
		//  Edit Row
		//

		} else if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit row

			// IdTipoVehiculo
			VehiculosAutorizados.IdTipoVehiculo.EditCustomAttributes = "";

			// Placa
			VehiculosAutorizados.Placa.EditCustomAttributes = "";
			VehiculosAutorizados.Placa.EditValue = ew_HtmlEncode(VehiculosAutorizados.Placa.CurrentValue);

			// Autorizado
			VehiculosAutorizados.Autorizado.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Autorizado.FldTagCaption(1))) ? VehiculosAutorizados.Autorizado.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Autorizado.FldTagCaption(2))) ? VehiculosAutorizados.Autorizado.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Autorizado.EditValue = alwrk;

			// PicoyPlaca
			VehiculosAutorizados.PicoyPlaca.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.PicoyPlaca.FldTagCaption(1))) ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.PicoyPlaca.FldTagCaption(2))) ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.PicoyPlaca.EditValue = alwrk;

			// Lunes
			VehiculosAutorizados.Lunes.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Lunes.FldTagCaption(1))) ? VehiculosAutorizados.Lunes.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Lunes.FldTagCaption(2))) ? VehiculosAutorizados.Lunes.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Lunes.EditValue = alwrk;

			// Martes
			VehiculosAutorizados.Martes.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Martes.FldTagCaption(1))) ? VehiculosAutorizados.Martes.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Martes.FldTagCaption(2))) ? VehiculosAutorizados.Martes.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Martes.EditValue = alwrk;

			// Miercoles
			VehiculosAutorizados.Miercoles.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Miercoles.FldTagCaption(1))) ? VehiculosAutorizados.Miercoles.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Miercoles.FldTagCaption(2))) ? VehiculosAutorizados.Miercoles.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Miercoles.EditValue = alwrk;

			// Jueves
			VehiculosAutorizados.Jueves.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Jueves.FldTagCaption(1))) ? VehiculosAutorizados.Jueves.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Jueves.FldTagCaption(2))) ? VehiculosAutorizados.Jueves.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Jueves.EditValue = alwrk;

			// Viernes
			VehiculosAutorizados.Viernes.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Viernes.FldTagCaption(1))) ? VehiculosAutorizados.Viernes.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Viernes.FldTagCaption(2))) ? VehiculosAutorizados.Viernes.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Viernes.EditValue = alwrk;

			// Sabado
			VehiculosAutorizados.Sabado.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Sabado.FldTagCaption(1))) ? VehiculosAutorizados.Sabado.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Sabado.FldTagCaption(2))) ? VehiculosAutorizados.Sabado.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Sabado.EditValue = alwrk;

			// Domingo
			VehiculosAutorizados.Domingo.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Domingo.FldTagCaption(1))) ? VehiculosAutorizados.Domingo.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Domingo.FldTagCaption(2))) ? VehiculosAutorizados.Domingo.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Domingo.EditValue = alwrk;

			// Marca
			VehiculosAutorizados.Marca.EditCustomAttributes = "";
			VehiculosAutorizados.Marca.EditValue = ew_HtmlEncode(VehiculosAutorizados.Marca.CurrentValue);

			// Edit refer script
			// IdTipoVehiculo

			VehiculosAutorizados.IdTipoVehiculo.HrefValue = "";

			// Placa
			VehiculosAutorizados.Placa.HrefValue = "";

			// Autorizado
			VehiculosAutorizados.Autorizado.HrefValue = "";

			// PicoyPlaca
			VehiculosAutorizados.PicoyPlaca.HrefValue = "";

			// Lunes
			VehiculosAutorizados.Lunes.HrefValue = "";

			// Martes
			VehiculosAutorizados.Martes.HrefValue = "";

			// Miercoles
			VehiculosAutorizados.Miercoles.HrefValue = "";

			// Jueves
			VehiculosAutorizados.Jueves.HrefValue = "";

			// Viernes
			VehiculosAutorizados.Viernes.HrefValue = "";

			// Sabado
			VehiculosAutorizados.Sabado.HrefValue = "";

			// Domingo
			VehiculosAutorizados.Domingo.HrefValue = "";

			// Marca
			VehiculosAutorizados.Marca.HrefValue = "";
		}
		if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD ||
			VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT ||
			VehiculosAutorizados.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			VehiculosAutorizados.SetupFieldTitles();
		}

		// Row Rendered event
		if (VehiculosAutorizados.RowType != EW_ROWTYPE_AGGREGATEINIT)
			VehiculosAutorizados.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(VehiculosAutorizados.IdTipoVehiculo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.IdTipoVehiculo.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Placa.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Placa.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Autorizado.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Autorizado.FldCaption);
		if (ew_Empty(VehiculosAutorizados.PicoyPlaca.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.PicoyPlaca.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Lunes.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Lunes.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Martes.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Martes.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Miercoles.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Miercoles.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Jueves.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Jueves.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Viernes.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Viernes.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Sabado.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Sabado.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Domingo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Domingo.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Marca.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Marca.FldCaption);

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
			string sSql = VehiculosAutorizados.SQL;
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
				result = VehiculosAutorizados.Row_Deleting(Row);
				if (!result)
					break;
			}
		}
		if (result) {
			sKey = "";
			foreach (OrderedDictionary Row in RsOld) {
				sThisKey = "";
				if (ew_NotEmpty(sThisKey)) sThisKey += EW_COMPOSITE_KEY_SEPARATOR;
				sThisKey += Convert.ToString(Row["IdVehiculoAutorizado"]);
				try {
					VehiculosAutorizados.Delete(Row);
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
			if (ew_NotEmpty(VehiculosAutorizados.CancelMessage)) {
				FailureMessage = VehiculosAutorizados.CancelMessage;
				VehiculosAutorizados.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("DeleteCancelled");
			}
		}
		if (result) {

			// Row deleted event
			foreach (OrderedDictionary Row in RsOld)
				VehiculosAutorizados.Row_Deleted(Row);
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
		string sFilter = VehiculosAutorizados.KeyFilter;
		VehiculosAutorizados.CurrentFilter = sFilter;
		sSql = VehiculosAutorizados.SQL;
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
				// IdTipoVehiculo

				VehiculosAutorizados.IdTipoVehiculo.SetDbValue(ref RsNew, VehiculosAutorizados.IdTipoVehiculo.CurrentValue, 0, VehiculosAutorizados.IdTipoVehiculo.ReadOnly);

				// Placa
				VehiculosAutorizados.Placa.SetDbValue(ref RsNew, VehiculosAutorizados.Placa.CurrentValue, "", VehiculosAutorizados.Placa.ReadOnly);

				// Autorizado
				VehiculosAutorizados.Autorizado.SetDbValue(ref RsNew, (VehiculosAutorizados.Autorizado.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Autorizado.CurrentValue)), false, VehiculosAutorizados.Autorizado.ReadOnly);

				// PicoyPlaca
				VehiculosAutorizados.PicoyPlaca.SetDbValue(ref RsNew, (VehiculosAutorizados.PicoyPlaca.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.PicoyPlaca.CurrentValue)), false, VehiculosAutorizados.PicoyPlaca.ReadOnly);

				// Lunes
				VehiculosAutorizados.Lunes.SetDbValue(ref RsNew, (VehiculosAutorizados.Lunes.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Lunes.CurrentValue)), false, VehiculosAutorizados.Lunes.ReadOnly);

				// Martes
				VehiculosAutorizados.Martes.SetDbValue(ref RsNew, (VehiculosAutorizados.Martes.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Martes.CurrentValue)), false, VehiculosAutorizados.Martes.ReadOnly);

				// Miercoles
				VehiculosAutorizados.Miercoles.SetDbValue(ref RsNew, (VehiculosAutorizados.Miercoles.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Miercoles.CurrentValue)), false, VehiculosAutorizados.Miercoles.ReadOnly);

				// Jueves
				VehiculosAutorizados.Jueves.SetDbValue(ref RsNew, (VehiculosAutorizados.Jueves.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Jueves.CurrentValue)), false, VehiculosAutorizados.Jueves.ReadOnly);

				// Viernes
				VehiculosAutorizados.Viernes.SetDbValue(ref RsNew, (VehiculosAutorizados.Viernes.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Viernes.CurrentValue)), false, VehiculosAutorizados.Viernes.ReadOnly);

				// Sabado
				VehiculosAutorizados.Sabado.SetDbValue(ref RsNew, (VehiculosAutorizados.Sabado.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Sabado.CurrentValue)), false, VehiculosAutorizados.Sabado.ReadOnly);

				// Domingo
				VehiculosAutorizados.Domingo.SetDbValue(ref RsNew, (VehiculosAutorizados.Domingo.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Domingo.CurrentValue)), false, VehiculosAutorizados.Domingo.ReadOnly);

				// Marca
				VehiculosAutorizados.Marca.SetDbValue(ref RsNew, VehiculosAutorizados.Marca.CurrentValue, "", VehiculosAutorizados.Marca.ReadOnly);
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;
				RsEdit.Close();
				return false;
			}
			RsEdit.Close();

			// Row Updating event
			bUpdateRow = VehiculosAutorizados.Row_Updating(RsOld, ref RsNew);
			if (bUpdateRow) {
				try {
					if (RsNew.Count > 0)
						result = VehiculosAutorizados.Update(ref RsNew) > 0;
					else
						result = true; // No field to update
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message;
					return false;
				}
			}	else {
				if (ew_NotEmpty(VehiculosAutorizados.CancelMessage)) {
					FailureMessage = VehiculosAutorizados.CancelMessage;
					VehiculosAutorizados.CancelMessage = "";
				} else {
					FailureMessage = Language.Phrase("UpdateCancelled");
				}
				result = false;
			}
		}

		// Row Updated event
		if (result)
			VehiculosAutorizados.Row_Updated(RsOld, RsNew);
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
			if (VehiculosAutorizados.CurrentMasterTable == "Personas") {
				VehiculosAutorizados.IdPersona.CurrentValue = VehiculosAutorizados.IdPersona.SessionValue;
			}
		try {

		// IdTipoVehiculo
		VehiculosAutorizados.IdTipoVehiculo.SetDbValue(ref RsNew, VehiculosAutorizados.IdTipoVehiculo.CurrentValue, 0, false);

		// Placa
		VehiculosAutorizados.Placa.SetDbValue(ref RsNew, VehiculosAutorizados.Placa.CurrentValue, "", false);

		// Autorizado
		VehiculosAutorizados.Autorizado.SetDbValue(ref RsNew, (VehiculosAutorizados.Autorizado.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Autorizado.CurrentValue)), false, false);

		// PicoyPlaca
		VehiculosAutorizados.PicoyPlaca.SetDbValue(ref RsNew, (VehiculosAutorizados.PicoyPlaca.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.PicoyPlaca.CurrentValue)), false, false);

		// Lunes
		VehiculosAutorizados.Lunes.SetDbValue(ref RsNew, (VehiculosAutorizados.Lunes.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Lunes.CurrentValue)), false, false);

		// Martes
		VehiculosAutorizados.Martes.SetDbValue(ref RsNew, (VehiculosAutorizados.Martes.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Martes.CurrentValue)), false, false);

		// Miercoles
		VehiculosAutorizados.Miercoles.SetDbValue(ref RsNew, (VehiculosAutorizados.Miercoles.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Miercoles.CurrentValue)), false, false);

		// Jueves
		VehiculosAutorizados.Jueves.SetDbValue(ref RsNew, (VehiculosAutorizados.Jueves.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Jueves.CurrentValue)), false, false);

		// Viernes
		VehiculosAutorizados.Viernes.SetDbValue(ref RsNew, (VehiculosAutorizados.Viernes.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Viernes.CurrentValue)), false, false);

		// Sabado
		VehiculosAutorizados.Sabado.SetDbValue(ref RsNew, (VehiculosAutorizados.Sabado.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Sabado.CurrentValue)), false, false);

		// Domingo
		VehiculosAutorizados.Domingo.SetDbValue(ref RsNew, (VehiculosAutorizados.Domingo.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Domingo.CurrentValue)), false, false);

		// Marca
		VehiculosAutorizados.Marca.SetDbValue(ref RsNew, VehiculosAutorizados.Marca.CurrentValue, "", false);

		// IdPersona
		if (ew_NotEmpty(VehiculosAutorizados.IdPersona.SessionValue)) {
			RsNew["IdPersona"] = VehiculosAutorizados.IdPersona.SessionValue;
		}
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = VehiculosAutorizados.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				VehiculosAutorizados.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(VehiculosAutorizados.CancelMessage)) {
				FailureMessage = VehiculosAutorizados.CancelMessage;
				VehiculosAutorizados.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			VehiculosAutorizados.IdVehiculoAutorizado.DbValue = Conn.GetLastInsertId();
			RsNew["IdVehiculoAutorizado"] = VehiculosAutorizados.IdVehiculoAutorizado.DbValue;
		}
		if (result) {

			// Row Inserted event
			VehiculosAutorizados.Row_Inserted(RsOld, RsNew);
		}
		return result;
	}

	// Set up master/detail based on QueryString
	public void SetUpMasterParms() {

		// Hide foreign keys
		string sMasterTblVar = VehiculosAutorizados.CurrentMasterTable;
		if (sMasterTblVar == "Personas") {
			VehiculosAutorizados.IdPersona.Visible = false;
			if (ew_ConvertToBool(ew_Get("mastereventcancelled")))
				VehiculosAutorizados.EventCancelled = true;
		}
		DbMasterFilter = VehiculosAutorizados.MasterFilter; // Get master filter
		DbDetailFilter = VehiculosAutorizados.DetailFilter; // Get detail filter
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
