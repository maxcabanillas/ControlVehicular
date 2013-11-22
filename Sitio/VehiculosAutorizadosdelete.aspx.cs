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
// ASP.NET code-behind class (Page)
//

partial class VehiculosAutorizadosdelete: AspNetMaker9_ControlVehicular {

	// Page object
	public cVehiculosAutorizados_delete VehiculosAutorizados_delete;	

	//
	// Page Class
	//
	public class cVehiculosAutorizados_delete: AspNetMakerPage, IDisposable {

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

		// ASP.NET page object
		public VehiculosAutorizadosdelete AspNetPage { 
			get { return (VehiculosAutorizadosdelete)m_ParentPage; }
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
		public cVehiculosAutorizados_delete(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "delete";
			m_PageObjName = "VehiculosAutorizados_delete";
			m_PageObjTypeName = "cVehiculosAutorizados_delete";

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
			CurrentTable = VehiculosAutorizados;

			//CurrentTableType = VehiculosAutorizados.GetType();
			// Initialize URLs
			// Connect to database

			if (Conn == null)
				Conn = new cConnection();
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
			if (!Security.CanDelete) {
				Security.SaveLastUrl();
				Page_Terminate("VehiculosAutorizadoslist.aspx");
			}

			// UserID_Loading event
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();

			// UserID_Loaded event
			Security.UserID_Loaded();

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

			// Page unload event, used in current page
			Page_Unload();

			// Global page unloaded event (in ewglobal*.cs)
			ParentPage.Page_Unloaded();

			// Go to URL if specified
			string sRedirectUrl = url;
			Page_Redirecting(ref sRedirectUrl);

			// Close connection
			Conn.Dispose();
			VehiculosAutorizados.Dispose();
			Personas.Dispose();
			Usuarios.Dispose();
			if (ew_NotEmpty(sRedirectUrl)) {
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.Redirect(sRedirectUrl); 
			}
		}

	public int TotalRecs;

	public int RecCnt;

	public ArrayList RecKeys;

	public SqlDataReader Recordset;

	//
	// Page main processing
	//
	public void Page_Main()
	{

		// Load Key Parameters
		RecKeys = VehiculosAutorizados.GetRecordKeys(); // Load record keys
		string sFilter = VehiculosAutorizados.GetKeyFilter();
		if (ew_Empty(sFilter))
			Page_Terminate("VehiculosAutorizadoslist.aspx"); // Prevent SQL injection, return to list

		// Set up filter (SQL WHERE clause)
		// SQL constructor in VehiculosAutorizados class, VehiculosAutorizadosinfo.aspx

		VehiculosAutorizados.CurrentFilter = sFilter;

		// Get action
		if (ew_NotEmpty(ew_Post("a_delete"))) {
			VehiculosAutorizados.CurrentAction = ew_Post("a_delete");
		} else {
			VehiculosAutorizados.CurrentAction = "D"; // Delete record directly
		}
		switch (VehiculosAutorizados.CurrentAction) {
			case "D": // Delete
				VehiculosAutorizados.SendEmail = true; // Send email on delete success
				if (DeleteRows()) {	// delete rows
					SuccessMessage = Language.Phrase("DeleteSuccess");	// Set up success message
					Page_Terminate(VehiculosAutorizados.ReturnUrl);	// Return to caller
				}
				break;
		}
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
		}

		// Row Rendered event
		if (VehiculosAutorizados.RowType != EW_ROWTYPE_AGGREGATEINIT)
			VehiculosAutorizados.Row_Rendered();
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
		Conn.BeginTrans();

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
			Conn.CommitTrans();	// Commit the changes

			// Row deleted event
			foreach (OrderedDictionary Row in RsOld)
				VehiculosAutorizados.Row_Deleted(Row);
		} else {
			Conn.RollbackTrans(); // Rollback changes			
		}
		return result;
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

		// Page init
		VehiculosAutorizados_delete = new cVehiculosAutorizados_delete(this);
		CurrentPage = VehiculosAutorizados_delete;

		//CurrentPageType = VehiculosAutorizados_delete.GetType();
		VehiculosAutorizados_delete.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		VehiculosAutorizados_delete.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (VehiculosAutorizados_delete != null)
			VehiculosAutorizados_delete.Dispose();
	}
}
