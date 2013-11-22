<%@ Page ClassName="VehiculosAutorizadosgrid" Language="C#" CodePage="65001" AutoEventWireup="true" CodeFile="VehiculosAutorizadosgrid.aspx.cs" Inherits="VehiculosAutorizadosgrid" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<% if (ew_Empty(VehiculosAutorizados.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var VehiculosAutorizados_grid = new ew_Page("VehiculosAutorizados_grid");

// page properties
VehiculosAutorizados_grid.PageID = "grid"; // page ID
VehiculosAutorizados_grid.FormID = "fVehiculosAutorizadosgrid"; // form ID 
var EW_PAGE_ID = VehiculosAutorizados_grid.PageID; // for backward compatibility

// extend page with ValidateForm function
VehiculosAutorizados_grid.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = (fobj.key_count) ? Number(fobj.key_count.value) : 1;
	var addcnt = 0;
	for (i=0; i<rowcnt; i++) {
		infix = (fobj.key_count) ? String(i+1) : "";
		var chkthisrow = true;
		if (fobj.a_list && fobj.a_list.value == "gridinsert")
			chkthisrow = !(this.EmptyRow(fobj, infix));
		else
			chkthisrow = true;
		if (chkthisrow) {
			addcnt += 1;
		elm = fobj.elements["x" + infix + "_IdTipoVehiculo"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.IdTipoVehiculo.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Placa"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Placa.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Autorizado"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Autorizado.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_PicoyPlaca"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.PicoyPlaca.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Lunes"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Lunes.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Martes"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Martes.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Miercoles"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Miercoles.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Jueves"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Jueves.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Viernes"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Viernes.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Sabado"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Sabado.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Domingo"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Domingo.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Marca"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Marca.FldCaption) %>");

		// Set up row object
		var row = {};
		row["index"] = infix;
		for (var j = 0; j < fobj.elements.length; j++) {
			var el = fobj.elements[j];
			var len = infix.length + 2;
			if (el.name.substr(0, len) == "x" + infix + "_") {
				var elname = "x_" + el.name.substr(len);
				if (ewLang.isObject(row[elname])) { // already exists
					if (ewLang.isArray(row[elname])) {
						row[elname][row[elname].length] = el; // add to array
					} else {
						row[elname] = [row[elname], el]; // convert to array
					}
				} else {
					row[elname] = el;
				}
			}
		}
		fobj.row = row;

		// Form Custom Validate event
		if (!this.Form_CustomValidate(fobj)) return false;
		} // End Grid Add checking
	}
	return true;
}

// Extend page with empty row check
VehiculosAutorizados_grid.EmptyRow = function(fobj, infix) {
	if (ew_ValueChanged(fobj, infix, "IdTipoVehiculo")) return false;
	if (ew_ValueChanged(fobj, infix, "Placa")) return false;
	if (ew_ValueChanged(fobj, infix, "Autorizado")) return false;
	if (ew_ValueChanged(fobj, infix, "PicoyPlaca")) return false;
	if (ew_ValueChanged(fobj, infix, "Lunes")) return false;
	if (ew_ValueChanged(fobj, infix, "Martes")) return false;
	if (ew_ValueChanged(fobj, infix, "Miercoles")) return false;
	if (ew_ValueChanged(fobj, infix, "Jueves")) return false;
	if (ew_ValueChanged(fobj, infix, "Viernes")) return false;
	if (ew_ValueChanged(fobj, infix, "Sabado")) return false;
	if (ew_ValueChanged(fobj, infix, "Domingo")) return false;
	if (ew_ValueChanged(fobj, infix, "Marca")) return false;
	return true;
}

// extend page with Form_CustomValidate function
VehiculosAutorizados_grid.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
VehiculosAutorizados_grid.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
VehiculosAutorizados_grid.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<% } %>
<%
if (VehiculosAutorizados.CurrentAction == "gridadd") {
	if (VehiculosAutorizados.CurrentMode == "copy") {

		// Load recordset
		VehiculosAutorizados_grid.Recordset = VehiculosAutorizados_grid.LoadRecordset();		
		VehiculosAutorizados_grid.StartRec = 1;
		VehiculosAutorizados_grid.DisplayRecs = VehiculosAutorizados_grid.TotalRecs;
	} else {
		VehiculosAutorizados.CurrentFilter = "0=1";
		VehiculosAutorizados_grid.StartRec = 1;
		VehiculosAutorizados_grid.DisplayRecs = VehiculosAutorizados.GridAddRowCount;
	}
	VehiculosAutorizados_grid.TotalRecs = VehiculosAutorizados_grid.DisplayRecs;
	VehiculosAutorizados_grid.StopRec = VehiculosAutorizados_grid.DisplayRecs;
} else {
	VehiculosAutorizados_grid.Recordset = VehiculosAutorizados_grid.LoadRecordset();
	VehiculosAutorizados_grid.StartRec = 1;
		VehiculosAutorizados_grid.DisplayRecs = VehiculosAutorizados_grid.TotalRecs; // Display all records
}
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><% if (VehiculosAutorizados.CurrentMode == "add" || VehiculosAutorizados.CurrentMode == "copy") { %><%= Language.Phrase("Add") %><% } else if (VehiculosAutorizados.CurrentMode == "edit") { %><%= Language.Phrase("Edit") %><% } %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= VehiculosAutorizados.TableCaption %>
</p>
<% VehiculosAutorizados_grid.ShowPageHeader(); %>
<% VehiculosAutorizados_grid.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if ((VehiculosAutorizados.CurrentMode == "add" || VehiculosAutorizados.CurrentMode == "copy" || VehiculosAutorizados.CurrentMode == "edit") && VehiculosAutorizados.CurrentAction != "F") { // Add/Copy/Edit mode %>
<div class="ewGridUpperPanel">
<% if (VehiculosAutorizados.AllowAddDeleteRow) { %>
<% if (Security.CanAdd) { %>
<span class="aspnetmaker">
<a href="javascript:void(0);" onclick="ew_AddGridRow(this);"><%= Language.Phrase("AddBlankRow") %></a>&nbsp;&nbsp;
</span>
<% } %>
<% } %>
</div>
<% } %>
<div id="gmp_VehiculosAutorizados" class="ewGridMiddlePanel">
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= VehiculosAutorizados.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		VehiculosAutorizados_grid.RenderListOptions();

		// Render list options (header, left)
		VehiculosAutorizados_grid.ListOptions.Render("header", "left");
%>
<% if (VehiculosAutorizados.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.IdTipoVehiculo))) { %>
		<td><%= VehiculosAutorizados.IdTipoVehiculo.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.IdTipoVehiculo.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.IdTipoVehiculo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.IdTipoVehiculo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Placa.Visible) { // Placa %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Placa))) { %>
		<td><%= VehiculosAutorizados.Placa.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Placa.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Placa.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Placa.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Autorizado.Visible) { // Autorizado %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Autorizado))) { %>
		<td><%= VehiculosAutorizados.Autorizado.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Autorizado.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Autorizado.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Autorizado.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.PicoyPlaca.Visible) { // PicoyPlaca %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.PicoyPlaca))) { %>
		<td><%= VehiculosAutorizados.PicoyPlaca.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.PicoyPlaca.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.PicoyPlaca.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.PicoyPlaca.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Lunes.Visible) { // Lunes %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Lunes))) { %>
		<td><%= VehiculosAutorizados.Lunes.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Lunes.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Lunes.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Lunes.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Martes.Visible) { // Martes %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Martes))) { %>
		<td><%= VehiculosAutorizados.Martes.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Martes.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Martes.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Martes.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Miercoles.Visible) { // Miercoles %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Miercoles))) { %>
		<td><%= VehiculosAutorizados.Miercoles.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Miercoles.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Miercoles.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Miercoles.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Jueves.Visible) { // Jueves %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Jueves))) { %>
		<td><%= VehiculosAutorizados.Jueves.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Jueves.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Jueves.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Jueves.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Viernes.Visible) { // Viernes %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Viernes))) { %>
		<td><%= VehiculosAutorizados.Viernes.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Viernes.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Viernes.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Viernes.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Sabado.Visible) { // Sabado %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Sabado))) { %>
		<td><%= VehiculosAutorizados.Sabado.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Sabado.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Sabado.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Sabado.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Domingo.Visible) { // Domingo %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Domingo))) { %>
		<td><%= VehiculosAutorizados.Domingo.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Domingo.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Domingo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Domingo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Marca.Visible) { // Marca %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Marca))) { %>
		<td><%= VehiculosAutorizados.Marca.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Marca.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Marca.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Marca.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		VehiculosAutorizados_grid.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
VehiculosAutorizados_grid.StartRec = 1;
VehiculosAutorizados_grid.StopRec = VehiculosAutorizados_grid.TotalRecs; // Show all records

// Restore number of post back records
if (ObjForm != null) {
	ObjForm.Index = 0;
	if (ObjForm.HasValue("key_count") && (VehiculosAutorizados.CurrentAction == "gridadd" || VehiculosAutorizados.CurrentAction == "gridedit" || VehiculosAutorizados.CurrentAction == "F")) {
		VehiculosAutorizados_grid.KeyCount = ew_ConvertToInt(ObjForm.GetValue("key_count"));
		VehiculosAutorizados_grid.StopRec = VehiculosAutorizados_grid.KeyCount;
	}
}
if (VehiculosAutorizados_grid.Recordset != null && VehiculosAutorizados_grid.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= VehiculosAutorizados_grid.StartRec - 1; i++) {
		if (VehiculosAutorizados_grid.Recordset.Read())
			VehiculosAutorizados_grid.RecCnt += 1;
	}		
} else if (!VehiculosAutorizados.AllowAddDeleteRow && VehiculosAutorizados_grid.StopRec == 0) {
	VehiculosAutorizados_grid.StopRec = VehiculosAutorizados.GridAddRowCount;
}

// Initialize Aggregate
VehiculosAutorizados.RowType = EW_ROWTYPE_AGGREGATEINIT;
VehiculosAutorizados.ResetAttrs();
VehiculosAutorizados_grid.RenderRow();
VehiculosAutorizados_grid.RowCnt = 0;
if (VehiculosAutorizados.CurrentAction == "gridadd") VehiculosAutorizados_grid.RowIndex = 0;
if (VehiculosAutorizados.CurrentAction == "gridedit") VehiculosAutorizados_grid.RowIndex = 0;

// Output data rows
bool Eof = false; // ASPX
while (VehiculosAutorizados_grid.RecCnt < VehiculosAutorizados_grid.StopRec) {
	if ((VehiculosAutorizados.CurrentAction != "gridadd" || VehiculosAutorizados.CurrentMode == "copy") && !Eof) // ASPX
		Eof = !VehiculosAutorizados_grid.Recordset.Read();
	VehiculosAutorizados_grid.RecCnt += 1;
	if (VehiculosAutorizados_grid.RecCnt >= VehiculosAutorizados_grid.StartRec) {
		VehiculosAutorizados_grid.RowCnt += 1;
		if (VehiculosAutorizados.CurrentAction == "gridadd" || VehiculosAutorizados.CurrentAction == "gridedit" || VehiculosAutorizados.CurrentAction == "F") {
			VehiculosAutorizados_grid.RowIndex = ew_ConvertToInt(VehiculosAutorizados_grid.RowIndex) + 1;
			ObjForm.Index = ew_ConvertToInt(VehiculosAutorizados_grid.RowIndex);
			if (ObjForm.HasValue("k_action")) {
				VehiculosAutorizados_grid.RowAction = ObjForm.GetValue("k_action");
			} else if (VehiculosAutorizados.CurrentAction == "gridadd") {
				VehiculosAutorizados_grid.RowAction = "insert";
			} else {
				VehiculosAutorizados_grid.RowAction = "";
			}
		}

		// Set up key count
		VehiculosAutorizados_grid.KeyCount = ew_ConvertToInt(VehiculosAutorizados_grid.RowIndex);

		// Init row class and style
		VehiculosAutorizados.ResetAttrs();
		VehiculosAutorizados.CssClass = "";	 
		if (VehiculosAutorizados.CurrentAction == "gridadd") {
			if (VehiculosAutorizados.CurrentMode == "copy" && !Eof) { // ASPX
				VehiculosAutorizados_grid.LoadRowValues(ref VehiculosAutorizados_grid.Recordset); // Load row values
				VehiculosAutorizados_grid.SetRecordKey(ref VehiculosAutorizados_grid.RowOldKey, VehiculosAutorizados_grid.Recordset); // Set old record key
			} else {
				VehiculosAutorizados_grid.LoadDefaultValues(); // Load default values
				VehiculosAutorizados_grid.RowOldKey = ""; // Clear old key value
			}
		} else if (VehiculosAutorizados.CurrentAction == "gridedit") {
			VehiculosAutorizados_grid.LoadRowValues(ref VehiculosAutorizados_grid.Recordset); // Load row values
		}
		VehiculosAutorizados.RowType = EW_ROWTYPE_VIEW; // Render view
		if (VehiculosAutorizados.CurrentAction == "gridadd") // Grid add
			VehiculosAutorizados.RowType = EW_ROWTYPE_ADD; // Render add

		//***if (VehiculosAutorizados.CurrentAction == "gridadd" && VehiculosAutorizados.EventCancelled) // Insert failed
		if (VehiculosAutorizados.CurrentAction == "gridadd" && VehiculosAutorizados.EventCancelled && !ObjForm.HasValue("k_blankrow")) // Insert failed
			VehiculosAutorizados_grid.RestoreCurrentRowFormValues(VehiculosAutorizados_grid.RowIndex); // Restore form values
		if (VehiculosAutorizados.CurrentAction == "gridedit") { // Grid edit
			if (VehiculosAutorizados.EventCancelled)
				VehiculosAutorizados_grid.RestoreCurrentRowFormValues(VehiculosAutorizados_grid.RowIndex); // Restore form values
			if (VehiculosAutorizados_grid.RowAction == "insert")
				VehiculosAutorizados.RowType = EW_ROWTYPE_ADD; // Render add
			else
				VehiculosAutorizados.RowType = EW_ROWTYPE_EDIT; // Render edit
		}
		if (VehiculosAutorizados.CurrentAction == "gridedit" && (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT || VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) && VehiculosAutorizados.EventCancelled) // Update failed
			VehiculosAutorizados_grid.RestoreCurrentRowFormValues(VehiculosAutorizados_grid.RowIndex); // Restore form values
		if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) // Edit row
			VehiculosAutorizados_grid.EditRowCnt += 1;
		if (VehiculosAutorizados.CurrentAction == "F") // Confirm row
			VehiculosAutorizados_grid.RestoreCurrentRowFormValues(VehiculosAutorizados_grid.RowIndex); // Restore form values
		if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD || VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Add / Edit row
			if (VehiculosAutorizados.CurrentAction == "edit") {
				ew_SetAttr(ref VehiculosAutorizados.RowAttrs, null);
				VehiculosAutorizados.CssClass = "ewTableEditRow";
			} else {
				ew_SetAttr(ref VehiculosAutorizados.RowAttrs, null);
			}		 
			if (ew_NotEmpty(VehiculosAutorizados_grid.RowIndex)) {
				VehiculosAutorizados.RowAttrs["data-rowindex"] = VehiculosAutorizados_grid.RowIndex;
				VehiculosAutorizados.RowAttrs["id"] = "r" + VehiculosAutorizados_grid.RowIndex + "_VehiculosAutorizados";
			}
		} else {
			ew_SetAttr(ref VehiculosAutorizados.RowAttrs, null);
		}

		// Render row
		VehiculosAutorizados_grid.RenderRow();

		// Render list options
		VehiculosAutorizados_grid.RenderListOptions();

		// Skip delete row / empty row for confirm page
		if (VehiculosAutorizados_grid.RowAction != "delete" && VehiculosAutorizados_grid.RowAction != "insertdelete" && !(VehiculosAutorizados_grid.RowAction == "insert" && VehiculosAutorizados.CurrentAction == "F" && VehiculosAutorizados_grid.EmptyRow())) {
%>
	<tr<%= VehiculosAutorizados.RowAttributes %>>
	<%

		// Render list options (body, left)
		VehiculosAutorizados_grid.ListOptions.Render("body", "left");
	%>
	<% if (VehiculosAutorizados.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
		<td<%= VehiculosAutorizados.IdTipoVehiculo.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<select id="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" name="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo"<%= VehiculosAutorizados.IdTipoVehiculo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(VehiculosAutorizados.IdTipoVehiculo.EditValue)) {
	alwrk = (ArrayList)VehiculosAutorizados.IdTipoVehiculo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], VehiculosAutorizados.IdTipoVehiculo.CurrentValue)) {
			selwrk = " selected=\"selected\"";
			emptywrk = false;
		} else {
			selwrk = "";
		}
%>
<option value="<%= ew_HtmlEncode(odwrk[0]) %>"<%= selwrk %>>
<%= odwrk[1] %>
</option>
<%
	}
}
if (emptywrk) VehiculosAutorizados.IdTipoVehiculo.OldValue = "";
%>
</select>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" id="o<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" value="<%= ew_HtmlEncode(VehiculosAutorizados.IdTipoVehiculo.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<select id="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" name="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo"<%= VehiculosAutorizados.IdTipoVehiculo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(VehiculosAutorizados.IdTipoVehiculo.EditValue)) {
	alwrk = (ArrayList)VehiculosAutorizados.IdTipoVehiculo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], VehiculosAutorizados.IdTipoVehiculo.CurrentValue)) {
			selwrk = " selected=\"selected\"";
			emptywrk = false;
		} else {
			selwrk = "";
		}
%>
<option value="<%= ew_HtmlEncode(odwrk[0]) %>"<%= selwrk %>>
<%= odwrk[1] %>
</option>
<%
	}
}
if (emptywrk) VehiculosAutorizados.IdTipoVehiculo.OldValue = "";
%>
</select>
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<div<%= VehiculosAutorizados.IdTipoVehiculo.ViewAttributes %>><%= VehiculosAutorizados.IdTipoVehiculo.ListViewValue %></div>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" id="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" value="<%= ew_HtmlEncode(VehiculosAutorizados.IdTipoVehiculo.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" id="o<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" value="<%= ew_HtmlEncode(VehiculosAutorizados.IdTipoVehiculo.OldValue) %>" />
<% } %>
<a name="<%= VehiculosAutorizados_grid.PageObjName + "_row_" + VehiculosAutorizados_grid.RowCnt %>" id="<%= VehiculosAutorizados_grid.PageObjName + "_row_" + VehiculosAutorizados_grid.RowCnt %>"></a>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_IdVehiculoAutorizado" id="o<%= VehiculosAutorizados_grid.RowIndex %>_IdVehiculoAutorizado" value="<%= ew_HtmlEncode(VehiculosAutorizados.IdVehiculoAutorizado.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_IdVehiculoAutorizado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_IdVehiculoAutorizado" value="<%= ew_HtmlEncode(VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Placa.Visible) { // Placa %>
		<td<%= VehiculosAutorizados.Placa.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<input type="text" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" size="10" maxlength="10" value="<%= VehiculosAutorizados.Placa.EditValue %>"<%= VehiculosAutorizados.Placa.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Placa" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Placa" value="<%= ew_HtmlEncode(VehiculosAutorizados.Placa.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<input type="text" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" size="10" maxlength="10" value="<%= VehiculosAutorizados.Placa.EditValue %>"<%= VehiculosAutorizados.Placa.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<div<%= VehiculosAutorizados.Placa.ViewAttributes %>><%= VehiculosAutorizados.Placa.ListViewValue %></div>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" value="<%= ew_HtmlEncode(VehiculosAutorizados.Placa.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Placa" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Placa" value="<%= ew_HtmlEncode(VehiculosAutorizados.Placa.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Autorizado.Visible) { // Autorizado %>
		<td<%= VehiculosAutorizados.Autorizado.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Autorizado.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" value="1"<%= selwrk %><%= VehiculosAutorizados.Autorizado.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Autorizado.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Autorizado.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" value="1"<%= selwrk %><%= VehiculosAutorizados.Autorizado.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Autorizado.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Autorizado.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.PicoyPlaca.Visible) { // PicoyPlaca %>
		<td<%= VehiculosAutorizados.PicoyPlaca.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.PicoyPlaca.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" value="1"<%= selwrk %><%= VehiculosAutorizados.PicoyPlaca.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" id="o<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" value="<%= ew_HtmlEncode(VehiculosAutorizados.PicoyPlaca.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.PicoyPlaca.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" value="1"<%= selwrk %><%= VehiculosAutorizados.PicoyPlaca.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" value="<%= ew_HtmlEncode(VehiculosAutorizados.PicoyPlaca.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" id="o<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" value="<%= ew_HtmlEncode(VehiculosAutorizados.PicoyPlaca.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Lunes.Visible) { // Lunes %>
		<td<%= VehiculosAutorizados.Lunes.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Lunes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" value="1"<%= selwrk %><%= VehiculosAutorizados.Lunes.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Lunes.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Lunes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" value="1"<%= selwrk %><%= VehiculosAutorizados.Lunes.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Lunes.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Lunes.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Martes.Visible) { // Martes %>
		<td<%= VehiculosAutorizados.Martes.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Martes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" value="1"<%= selwrk %><%= VehiculosAutorizados.Martes.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Martes" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Martes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Martes.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Martes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" value="1"<%= selwrk %><%= VehiculosAutorizados.Martes.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Martes.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Martes" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Martes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Martes.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Miercoles.Visible) { // Miercoles %>
		<td<%= VehiculosAutorizados.Miercoles.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Miercoles.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" value="1"<%= selwrk %><%= VehiculosAutorizados.Miercoles.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" value="<%= ew_HtmlEncode(VehiculosAutorizados.Miercoles.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Miercoles.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" value="1"<%= selwrk %><%= VehiculosAutorizados.Miercoles.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" value="<%= ew_HtmlEncode(VehiculosAutorizados.Miercoles.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" value="<%= ew_HtmlEncode(VehiculosAutorizados.Miercoles.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Jueves.Visible) { // Jueves %>
		<td<%= VehiculosAutorizados.Jueves.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Jueves.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" value="1"<%= selwrk %><%= VehiculosAutorizados.Jueves.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" value="<%= ew_HtmlEncode(VehiculosAutorizados.Jueves.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Jueves.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" value="1"<%= selwrk %><%= VehiculosAutorizados.Jueves.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" value="<%= ew_HtmlEncode(VehiculosAutorizados.Jueves.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" value="<%= ew_HtmlEncode(VehiculosAutorizados.Jueves.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Viernes.Visible) { // Viernes %>
		<td<%= VehiculosAutorizados.Viernes.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Viernes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" value="1"<%= selwrk %><%= VehiculosAutorizados.Viernes.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Viernes.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Viernes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" value="1"<%= selwrk %><%= VehiculosAutorizados.Viernes.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Viernes.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Viernes.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Sabado.Visible) { // Sabado %>
		<td<%= VehiculosAutorizados.Sabado.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Sabado.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" value="1"<%= selwrk %><%= VehiculosAutorizados.Sabado.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Sabado.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Sabado.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" value="1"<%= selwrk %><%= VehiculosAutorizados.Sabado.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Sabado.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Sabado.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Domingo.Visible) { // Domingo %>
		<td<%= VehiculosAutorizados.Domingo.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Domingo.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" value="1"<%= selwrk %><%= VehiculosAutorizados.Domingo.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" value="<%= ew_HtmlEncode(VehiculosAutorizados.Domingo.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Domingo.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" value="1"<%= selwrk %><%= VehiculosAutorizados.Domingo.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" value="<%= ew_HtmlEncode(VehiculosAutorizados.Domingo.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" value="<%= ew_HtmlEncode(VehiculosAutorizados.Domingo.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Marca.Visible) { // Marca %>
		<td<%= VehiculosAutorizados.Marca.CellAttributes %>>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<input type="text" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" size="30" maxlength="50" value="<%= VehiculosAutorizados.Marca.EditValue %>"<%= VehiculosAutorizados.Marca.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Marca" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Marca" value="<%= ew_HtmlEncode(VehiculosAutorizados.Marca.OldValue) %>" />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<input type="text" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" size="30" maxlength="50" value="<%= VehiculosAutorizados.Marca.EditValue %>"<%= VehiculosAutorizados.Marca.EditAttributes %> />
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<div<%= VehiculosAutorizados.Marca.ViewAttributes %>><%= VehiculosAutorizados.Marca.ListViewValue %></div>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" value="<%= ew_HtmlEncode(VehiculosAutorizados.Marca.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Marca" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Marca" value="<%= ew_HtmlEncode(VehiculosAutorizados.Marca.OldValue) %>" />
<% } %>
</td>
	<% } %>
<%

		// Render list options (body, right)
		VehiculosAutorizados_grid.ListOptions.Render("body", "right");
%>
	</tr>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD) { %>
<% } %>
<% if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { %>
<% } %>
<%
	}
	} // End delete row checking	
}
%>
<%
	if (VehiculosAutorizados.CurrentMode == "add" || VehiculosAutorizados.CurrentMode == "copy" || VehiculosAutorizados.CurrentMode == "edit") {
		VehiculosAutorizados_grid.RowIndex = "$rowindex$";
		VehiculosAutorizados_grid.LoadDefaultValues();

		// Set row properties
		VehiculosAutorizados.ResetAttrs();
		ew_SetAttr(ref VehiculosAutorizados.RowAttrs, null);
		VehiculosAutorizados.CssStyle = "";
		ew_SetAttr(ref VehiculosAutorizados.RowAttrs, null); 
		if (ew_NotEmpty(VehiculosAutorizados_grid.RowIndex)) {
			VehiculosAutorizados.RowAttrs["data-rowindex"] = VehiculosAutorizados_grid.RowIndex;
			VehiculosAutorizados.RowAttrs["id"] = "r" +  VehiculosAutorizados_grid.RowIndex + "_VehiculosAutorizados";
		}
		VehiculosAutorizados.RowType = EW_ROWTYPE_ADD;

		// Render row
		VehiculosAutorizados_grid.RenderRow();

		// Render list options
		VehiculosAutorizados_grid.RenderListOptions();

		// Add id and class to the template row
		VehiculosAutorizados.RowAttrs["id"] = "r0_VehiculosAutorizados";
		VehiculosAutorizados.RowAttrs["class"] = ew_AppendClass(Convert.ToString(VehiculosAutorizados.RowAttrs["class"]), "ewTemplate");
%>
	<tr<%= VehiculosAutorizados.RowAttributes %>>
<%

		// Render list options (body, left)
		VehiculosAutorizados_grid.ListOptions.Render("body", "left");
%>
	<% if (VehiculosAutorizados.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<select id="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" name="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo"<%= VehiculosAutorizados.IdTipoVehiculo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(VehiculosAutorizados.IdTipoVehiculo.EditValue)) {
	alwrk = (ArrayList)VehiculosAutorizados.IdTipoVehiculo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], VehiculosAutorizados.IdTipoVehiculo.CurrentValue)) {
			selwrk = " selected=\"selected\"";
			emptywrk = false;
		} else {
			selwrk = "";
		}
%>
<option value="<%= ew_HtmlEncode(odwrk[0]) %>"<%= selwrk %>>
<%= odwrk[1] %>
</option>
<%
	}
}
if (emptywrk) VehiculosAutorizados.IdTipoVehiculo.OldValue = "";
%>
</select>
<% } else { %>
<div<%= VehiculosAutorizados.IdTipoVehiculo.ViewAttributes %>><%= VehiculosAutorizados.IdTipoVehiculo.ViewValue %></div>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" id="x<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" value="<%= ew_HtmlEncode(VehiculosAutorizados.IdTipoVehiculo.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" id="o<%= VehiculosAutorizados_grid.RowIndex %>_IdTipoVehiculo" value="<%= ew_HtmlEncode(VehiculosAutorizados.IdTipoVehiculo.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Placa.Visible) { // Placa %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<input type="text" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" size="10" maxlength="10" value="<%= VehiculosAutorizados.Placa.EditValue %>"<%= VehiculosAutorizados.Placa.EditAttributes %> />
<% } else { %>
<div<%= VehiculosAutorizados.Placa.ViewAttributes %>><%= VehiculosAutorizados.Placa.ViewValue %></div>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Placa" value="<%= ew_HtmlEncode(VehiculosAutorizados.Placa.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Placa" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Placa" value="<%= ew_HtmlEncode(VehiculosAutorizados.Placa.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Autorizado.Visible) { // Autorizado %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Autorizado.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" value="1"<%= selwrk %><%= VehiculosAutorizados.Autorizado.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Autorizado.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Autorizado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Autorizado.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.PicoyPlaca.Visible) { // PicoyPlaca %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.PicoyPlaca.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" value="1"<%= selwrk %><%= VehiculosAutorizados.PicoyPlaca.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" value="<%= ew_HtmlEncode(VehiculosAutorizados.PicoyPlaca.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" id="o<%= VehiculosAutorizados_grid.RowIndex %>_PicoyPlaca" value="<%= ew_HtmlEncode(VehiculosAutorizados.PicoyPlaca.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Lunes.Visible) { // Lunes %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Lunes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" value="1"<%= selwrk %><%= VehiculosAutorizados.Lunes.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Lunes.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Lunes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Lunes.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Martes.Visible) { // Martes %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Martes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" value="1"<%= selwrk %><%= VehiculosAutorizados.Martes.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Martes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Martes.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Martes" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Martes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Martes.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Miercoles.Visible) { // Miercoles %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Miercoles.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" value="1"<%= selwrk %><%= VehiculosAutorizados.Miercoles.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" value="<%= ew_HtmlEncode(VehiculosAutorizados.Miercoles.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Miercoles" value="<%= ew_HtmlEncode(VehiculosAutorizados.Miercoles.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Jueves.Visible) { // Jueves %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Jueves.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" value="1"<%= selwrk %><%= VehiculosAutorizados.Jueves.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" value="<%= ew_HtmlEncode(VehiculosAutorizados.Jueves.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Jueves" value="<%= ew_HtmlEncode(VehiculosAutorizados.Jueves.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Viernes.Visible) { // Viernes %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Viernes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" value="1"<%= selwrk %><%= VehiculosAutorizados.Viernes.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Viernes.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Viernes" value="<%= ew_HtmlEncode(VehiculosAutorizados.Viernes.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Sabado.Visible) { // Sabado %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Sabado.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" value="1"<%= selwrk %><%= VehiculosAutorizados.Sabado.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Sabado.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Sabado" value="<%= ew_HtmlEncode(VehiculosAutorizados.Sabado.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Domingo.Visible) { // Domingo %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Domingo.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" value="1"<%= selwrk %><%= VehiculosAutorizados.Domingo.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" value="<%= ew_HtmlEncode(VehiculosAutorizados.Domingo.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Domingo" value="<%= ew_HtmlEncode(VehiculosAutorizados.Domingo.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosAutorizados.Marca.Visible) { // Marca %>
		<td>
<% if (VehiculosAutorizados.CurrentAction != "F") { %>
<input type="text" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" size="30" maxlength="50" value="<%= VehiculosAutorizados.Marca.EditValue %>"<%= VehiculosAutorizados.Marca.EditAttributes %> />
<% } else { %>
<div<%= VehiculosAutorizados.Marca.ViewAttributes %>><%= VehiculosAutorizados.Marca.ViewValue %></div>
<input type="hidden" name="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" id="x<%= VehiculosAutorizados_grid.RowIndex %>_Marca" value="<%= ew_HtmlEncode(VehiculosAutorizados.Marca.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosAutorizados_grid.RowIndex %>_Marca" id="o<%= VehiculosAutorizados_grid.RowIndex %>_Marca" value="<%= ew_HtmlEncode(VehiculosAutorizados.Marca.OldValue) %>" />
</td>
	<% } %>
<%

		// Render list options (body, right)
		VehiculosAutorizados_grid.ListOptions.Render("body", "right");
%>
	</tr>
<%
}
%>
</tbody>
</table>
<% if (VehiculosAutorizados.CurrentMode == "add" || VehiculosAutorizados.CurrentMode == "copy") { %>
<input type="hidden" name="a_list" id="a_list" value="gridinsert" />
<input type="hidden" name="key_count" id="key_count" value="<%= VehiculosAutorizados_grid.KeyCount %>" />
<%= VehiculosAutorizados_grid.MultiSelectKey %>
<% } %>
<% if (VehiculosAutorizados.CurrentMode == "edit") { %>
<input type="hidden" name="a_list" id="a_list" value="gridupdate" />
<input type="hidden" name="key_count" id="key_count" value="<%= VehiculosAutorizados_grid.KeyCount %>" />
<%= VehiculosAutorizados_grid.MultiSelectKey %>
<% } %>
<input type="hidden" name="detailpage" id="detailpage" value="VehiculosAutorizados_grid">
</div>
<%

// Close recordset
if (VehiculosAutorizados_grid.Recordset != null) {
	VehiculosAutorizados_grid.Recordset.Close();
	VehiculosAutorizados_grid.Recordset.Dispose();
}
%>
<% if ((VehiculosAutorizados.CurrentMode == "add" || VehiculosAutorizados.CurrentMode == "copy" || VehiculosAutorizados.CurrentMode == "edit") && VehiculosAutorizados.CurrentAction != "F") { // Add/Copy/Edit mode %>
<div class="ewGridLowerPanel">
<% if (VehiculosAutorizados.AllowAddDeleteRow) { %>
<% if (Security.CanAdd) { %>
<span class="aspnetmaker">
<a href="javascript:void(0);" onclick="ew_AddGridRow(this);"><%= Language.Phrase("AddBlankRow") %></a>&nbsp;&nbsp;
</span>
<% } %>
<% } %>
</div>
<% } %>
</td></tr></table>
<% if (ew_Empty(VehiculosAutorizados.Export) && ew_Empty(VehiculosAutorizados.CurrentAction)) { %>
<% } %>
<%
VehiculosAutorizados_grid.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
