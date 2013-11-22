<%@ Page ClassName="VehiculosPicoYPlacaHorasgrid" Language="C#" CodePage="65001" AutoEventWireup="true" CodeFile="VehiculosPicoYPlacaHorasgrid.aspx.cs" Inherits="VehiculosPicoYPlacaHorasgrid" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<% if (ew_Empty(VehiculosPicoYPlacaHoras.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var VehiculosPicoYPlacaHoras_grid = new ew_Page("VehiculosPicoYPlacaHoras_grid");

// page properties
VehiculosPicoYPlacaHoras_grid.PageID = "grid"; // page ID
VehiculosPicoYPlacaHoras_grid.FormID = "fVehiculosPicoYPlacaHorasgrid"; // form ID 
var EW_PAGE_ID = VehiculosPicoYPlacaHoras_grid.PageID; // for backward compatibility

// extend page with ValidateForm function
VehiculosPicoYPlacaHoras_grid.ValidateForm = function(fobj) {
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
		elm = fobj.elements["x" + infix + "_HoraInicial"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosPicoYPlacaHoras.HoraInicial.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_HoraInicial"];
		if (elm && elm.type != "hidden" && !ew_CheckTime(elm.value)) // skip hidden field
			return ew_OnError(this, elm, "<%= ew_JsEncode2(VehiculosPicoYPlacaHoras.HoraInicial.FldErrMsg) %>");
		elm = fobj.elements["x" + infix + "_HoraFinal"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosPicoYPlacaHoras.HoraFinal.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_HoraFinal"];
		if (elm && elm.type != "hidden" && !ew_CheckTime(elm.value)) // skip hidden field
			return ew_OnError(this, elm, "<%= ew_JsEncode2(VehiculosPicoYPlacaHoras.HoraFinal.FldErrMsg) %>");

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
VehiculosPicoYPlacaHoras_grid.EmptyRow = function(fobj, infix) {
	if (ew_ValueChanged(fobj, infix, "HoraInicial")) return false;
	if (ew_ValueChanged(fobj, infix, "HoraFinal")) return false;
	return true;
}

// extend page with Form_CustomValidate function
VehiculosPicoYPlacaHoras_grid.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
VehiculosPicoYPlacaHoras_grid.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
VehiculosPicoYPlacaHoras_grid.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<% } %>
<%
if (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd") {
	if (VehiculosPicoYPlacaHoras.CurrentMode == "copy") {

		// Load recordset
		VehiculosPicoYPlacaHoras_grid.Recordset = VehiculosPicoYPlacaHoras_grid.LoadRecordset();		
		VehiculosPicoYPlacaHoras_grid.StartRec = 1;
		VehiculosPicoYPlacaHoras_grid.DisplayRecs = VehiculosPicoYPlacaHoras_grid.TotalRecs;
	} else {
		VehiculosPicoYPlacaHoras.CurrentFilter = "0=1";
		VehiculosPicoYPlacaHoras_grid.StartRec = 1;
		VehiculosPicoYPlacaHoras_grid.DisplayRecs = VehiculosPicoYPlacaHoras.GridAddRowCount;
	}
	VehiculosPicoYPlacaHoras_grid.TotalRecs = VehiculosPicoYPlacaHoras_grid.DisplayRecs;
	VehiculosPicoYPlacaHoras_grid.StopRec = VehiculosPicoYPlacaHoras_grid.DisplayRecs;
} else {
	VehiculosPicoYPlacaHoras_grid.Recordset = VehiculosPicoYPlacaHoras_grid.LoadRecordset();
	VehiculosPicoYPlacaHoras_grid.StartRec = 1;
		VehiculosPicoYPlacaHoras_grid.DisplayRecs = VehiculosPicoYPlacaHoras_grid.TotalRecs; // Display all records
}
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><% if (VehiculosPicoYPlacaHoras.CurrentMode == "add" || VehiculosPicoYPlacaHoras.CurrentMode == "copy") { %><%= Language.Phrase("Add") %><% } else if (VehiculosPicoYPlacaHoras.CurrentMode == "edit") { %><%= Language.Phrase("Edit") %><% } %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= VehiculosPicoYPlacaHoras.TableCaption %>
</p>
<% VehiculosPicoYPlacaHoras_grid.ShowPageHeader(); %>
<% VehiculosPicoYPlacaHoras_grid.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if ((VehiculosPicoYPlacaHoras.CurrentMode == "add" || VehiculosPicoYPlacaHoras.CurrentMode == "copy" || VehiculosPicoYPlacaHoras.CurrentMode == "edit") && VehiculosPicoYPlacaHoras.CurrentAction != "F") { // Add/Copy/Edit mode %>
<div class="ewGridUpperPanel">
</div>
<% } %>
<div id="gmp_VehiculosPicoYPlacaHoras" class="ewGridMiddlePanel">
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= VehiculosPicoYPlacaHoras.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		VehiculosPicoYPlacaHoras_grid.RenderListOptions();

		// Render list options (header, left)
		VehiculosPicoYPlacaHoras_grid.ListOptions.Render("header", "left");
%>
<% if (VehiculosPicoYPlacaHoras.HoraInicial.Visible) { // HoraInicial %>
	<% if (ew_Empty(VehiculosPicoYPlacaHoras.SortUrl(VehiculosPicoYPlacaHoras.HoraInicial))) { %>
		<td><%= VehiculosPicoYPlacaHoras.HoraInicial.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosPicoYPlacaHoras.HoraInicial.FldCaption %></td><td style="width: 10px;"><% if (VehiculosPicoYPlacaHoras.HoraInicial.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosPicoYPlacaHoras.HoraInicial.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosPicoYPlacaHoras.HoraFinal.Visible) { // HoraFinal %>
	<% if (ew_Empty(VehiculosPicoYPlacaHoras.SortUrl(VehiculosPicoYPlacaHoras.HoraFinal))) { %>
		<td><%= VehiculosPicoYPlacaHoras.HoraFinal.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosPicoYPlacaHoras.HoraFinal.FldCaption %></td><td style="width: 10px;"><% if (VehiculosPicoYPlacaHoras.HoraFinal.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosPicoYPlacaHoras.HoraFinal.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		VehiculosPicoYPlacaHoras_grid.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
VehiculosPicoYPlacaHoras_grid.StartRec = 1;
VehiculosPicoYPlacaHoras_grid.StopRec = VehiculosPicoYPlacaHoras_grid.TotalRecs; // Show all records

// Restore number of post back records
if (ObjForm != null) {
	ObjForm.Index = 0;
	if (ObjForm.HasValue("key_count") && (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd" || VehiculosPicoYPlacaHoras.CurrentAction == "gridedit" || VehiculosPicoYPlacaHoras.CurrentAction == "F")) {
		VehiculosPicoYPlacaHoras_grid.KeyCount = ew_ConvertToInt(ObjForm.GetValue("key_count"));
		VehiculosPicoYPlacaHoras_grid.StopRec = VehiculosPicoYPlacaHoras_grid.KeyCount;
	}
}
if (VehiculosPicoYPlacaHoras_grid.Recordset != null && VehiculosPicoYPlacaHoras_grid.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= VehiculosPicoYPlacaHoras_grid.StartRec - 1; i++) {
		if (VehiculosPicoYPlacaHoras_grid.Recordset.Read())
			VehiculosPicoYPlacaHoras_grid.RecCnt += 1;
	}		
} else if (!VehiculosPicoYPlacaHoras.AllowAddDeleteRow && VehiculosPicoYPlacaHoras_grid.StopRec == 0) {
	VehiculosPicoYPlacaHoras_grid.StopRec = VehiculosPicoYPlacaHoras.GridAddRowCount;
}

// Initialize Aggregate
VehiculosPicoYPlacaHoras.RowType = EW_ROWTYPE_AGGREGATEINIT;
VehiculosPicoYPlacaHoras.ResetAttrs();
VehiculosPicoYPlacaHoras_grid.RenderRow();
VehiculosPicoYPlacaHoras_grid.RowCnt = 0;
if (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd") VehiculosPicoYPlacaHoras_grid.RowIndex = 0;
if (VehiculosPicoYPlacaHoras.CurrentAction == "gridedit") VehiculosPicoYPlacaHoras_grid.RowIndex = 0;

// Output data rows
bool Eof = false; // ASPX
while (VehiculosPicoYPlacaHoras_grid.RecCnt < VehiculosPicoYPlacaHoras_grid.StopRec) {
	if ((VehiculosPicoYPlacaHoras.CurrentAction != "gridadd" || VehiculosPicoYPlacaHoras.CurrentMode == "copy") && !Eof) // ASPX
		Eof = !VehiculosPicoYPlacaHoras_grid.Recordset.Read();
	VehiculosPicoYPlacaHoras_grid.RecCnt += 1;
	if (VehiculosPicoYPlacaHoras_grid.RecCnt >= VehiculosPicoYPlacaHoras_grid.StartRec) {
		VehiculosPicoYPlacaHoras_grid.RowCnt += 1;
		if (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd" || VehiculosPicoYPlacaHoras.CurrentAction == "gridedit" || VehiculosPicoYPlacaHoras.CurrentAction == "F") {
			VehiculosPicoYPlacaHoras_grid.RowIndex = ew_ConvertToInt(VehiculosPicoYPlacaHoras_grid.RowIndex) + 1;
			ObjForm.Index = ew_ConvertToInt(VehiculosPicoYPlacaHoras_grid.RowIndex);
			if (ObjForm.HasValue("k_action")) {
				VehiculosPicoYPlacaHoras_grid.RowAction = ObjForm.GetValue("k_action");
			} else if (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd") {
				VehiculosPicoYPlacaHoras_grid.RowAction = "insert";
			} else {
				VehiculosPicoYPlacaHoras_grid.RowAction = "";
			}
		}

		// Set up key count
		VehiculosPicoYPlacaHoras_grid.KeyCount = ew_ConvertToInt(VehiculosPicoYPlacaHoras_grid.RowIndex);

		// Init row class and style
		VehiculosPicoYPlacaHoras.ResetAttrs();
		VehiculosPicoYPlacaHoras.CssClass = "";	 
		if (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd") {
			if (VehiculosPicoYPlacaHoras.CurrentMode == "copy" && !Eof) { // ASPX
				VehiculosPicoYPlacaHoras_grid.LoadRowValues(ref VehiculosPicoYPlacaHoras_grid.Recordset); // Load row values
				VehiculosPicoYPlacaHoras_grid.SetRecordKey(ref VehiculosPicoYPlacaHoras_grid.RowOldKey, VehiculosPicoYPlacaHoras_grid.Recordset); // Set old record key
			} else {
				VehiculosPicoYPlacaHoras_grid.LoadDefaultValues(); // Load default values
				VehiculosPicoYPlacaHoras_grid.RowOldKey = ""; // Clear old key value
			}
		} else if (VehiculosPicoYPlacaHoras.CurrentAction == "gridedit") {
			VehiculosPicoYPlacaHoras_grid.LoadRowValues(ref VehiculosPicoYPlacaHoras_grid.Recordset); // Load row values
		}
		VehiculosPicoYPlacaHoras.RowType = EW_ROWTYPE_VIEW; // Render view
		if (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd") // Grid add
			VehiculosPicoYPlacaHoras.RowType = EW_ROWTYPE_ADD; // Render add

		//***if (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd" && VehiculosPicoYPlacaHoras.EventCancelled) // Insert failed
		if (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd" && VehiculosPicoYPlacaHoras.EventCancelled && !ObjForm.HasValue("k_blankrow")) // Insert failed
			VehiculosPicoYPlacaHoras_grid.RestoreCurrentRowFormValues(VehiculosPicoYPlacaHoras_grid.RowIndex); // Restore form values
		if (VehiculosPicoYPlacaHoras.CurrentAction == "gridedit") { // Grid edit
			if (VehiculosPicoYPlacaHoras.EventCancelled)
				VehiculosPicoYPlacaHoras_grid.RestoreCurrentRowFormValues(VehiculosPicoYPlacaHoras_grid.RowIndex); // Restore form values
			if (VehiculosPicoYPlacaHoras_grid.RowAction == "insert")
				VehiculosPicoYPlacaHoras.RowType = EW_ROWTYPE_ADD; // Render add
			else
				VehiculosPicoYPlacaHoras.RowType = EW_ROWTYPE_EDIT; // Render edit
		}
		if (VehiculosPicoYPlacaHoras.CurrentAction == "gridedit" && (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_EDIT || VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_ADD) && VehiculosPicoYPlacaHoras.EventCancelled) // Update failed
			VehiculosPicoYPlacaHoras_grid.RestoreCurrentRowFormValues(VehiculosPicoYPlacaHoras_grid.RowIndex); // Restore form values
		if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_EDIT) // Edit row
			VehiculosPicoYPlacaHoras_grid.EditRowCnt += 1;
		if (VehiculosPicoYPlacaHoras.CurrentAction == "F") // Confirm row
			VehiculosPicoYPlacaHoras_grid.RestoreCurrentRowFormValues(VehiculosPicoYPlacaHoras_grid.RowIndex); // Restore form values
		if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_ADD || VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_EDIT) { // Add / Edit row
			if (VehiculosPicoYPlacaHoras.CurrentAction == "edit") {
				ew_SetAttr(ref VehiculosPicoYPlacaHoras.RowAttrs, null);
				VehiculosPicoYPlacaHoras.CssClass = "ewTableEditRow";
			} else {
				ew_SetAttr(ref VehiculosPicoYPlacaHoras.RowAttrs, null);
			}		 
			if (ew_NotEmpty(VehiculosPicoYPlacaHoras_grid.RowIndex)) {
				VehiculosPicoYPlacaHoras.RowAttrs["data-rowindex"] = VehiculosPicoYPlacaHoras_grid.RowIndex;
				VehiculosPicoYPlacaHoras.RowAttrs["id"] = "r" + VehiculosPicoYPlacaHoras_grid.RowIndex + "_VehiculosPicoYPlacaHoras";
			}
		} else {
			ew_SetAttr(ref VehiculosPicoYPlacaHoras.RowAttrs, null);
		}

		// Render row
		VehiculosPicoYPlacaHoras_grid.RenderRow();

		// Render list options
		VehiculosPicoYPlacaHoras_grid.RenderListOptions();

		// Skip delete row / empty row for confirm page
		if (VehiculosPicoYPlacaHoras_grid.RowAction != "delete" && VehiculosPicoYPlacaHoras_grid.RowAction != "insertdelete" && !(VehiculosPicoYPlacaHoras_grid.RowAction == "insert" && VehiculosPicoYPlacaHoras.CurrentAction == "F" && VehiculosPicoYPlacaHoras_grid.EmptyRow())) {
%>
	<tr<%= VehiculosPicoYPlacaHoras.RowAttributes %>>
	<%

		// Render list options (body, left)
		VehiculosPicoYPlacaHoras_grid.ListOptions.Render("body", "left");
	%>
	<% if (VehiculosPicoYPlacaHoras.HoraInicial.Visible) { // HoraInicial %>
		<td<%= VehiculosPicoYPlacaHoras.HoraInicial.CellAttributes %>>
<% if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<input type="text" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" size="10" value="<%= VehiculosPicoYPlacaHoras.HoraInicial.EditValue %>"<%= VehiculosPicoYPlacaHoras.HoraInicial.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" id="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraInicial.OldValue) %>" />
<% } %>
<% if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<input type="text" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" size="10" value="<%= VehiculosPicoYPlacaHoras.HoraInicial.EditValue %>"<%= VehiculosPicoYPlacaHoras.HoraInicial.EditAttributes %> />
<% } %>
<% if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<div<%= VehiculosPicoYPlacaHoras.HoraInicial.ViewAttributes %>><%= VehiculosPicoYPlacaHoras.HoraInicial.ListViewValue %></div>
<input type="hidden" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraInicial.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" id="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraInicial.OldValue) %>" />
<% } %>
<a name="<%= VehiculosPicoYPlacaHoras_grid.PageObjName + "_row_" + VehiculosPicoYPlacaHoras_grid.RowCnt %>" id="<%= VehiculosPicoYPlacaHoras_grid.PageObjName + "_row_" + VehiculosPicoYPlacaHoras_grid.RowCnt %>"></a></td>
	<% } %>
	<% if (VehiculosPicoYPlacaHoras.HoraFinal.Visible) { // HoraFinal %>
		<td<%= VehiculosPicoYPlacaHoras.HoraFinal.CellAttributes %>>
<% if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<input type="text" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" size="10" value="<%= VehiculosPicoYPlacaHoras.HoraFinal.EditValue %>"<%= VehiculosPicoYPlacaHoras.HoraFinal.EditAttributes %> />
<input type="hidden" name="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" id="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraFinal.OldValue) %>" />
<% } %>
<% if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<input type="text" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" size="10" value="<%= VehiculosPicoYPlacaHoras.HoraFinal.EditValue %>"<%= VehiculosPicoYPlacaHoras.HoraFinal.EditAttributes %> />
<% } %>
<% if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<div<%= VehiculosPicoYPlacaHoras.HoraFinal.ViewAttributes %>><%= VehiculosPicoYPlacaHoras.HoraFinal.ListViewValue %></div>
<input type="hidden" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraFinal.CurrentValue) %>" />
<input type="hidden" name="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" id="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraFinal.OldValue) %>" />
<% } %>
</td>
	<% } %>
<%

		// Render list options (body, right)
		VehiculosPicoYPlacaHoras_grid.ListOptions.Render("body", "right");
%>
	</tr>
<% if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_ADD) { %>
<% } %>
<% if (VehiculosPicoYPlacaHoras.RowType == EW_ROWTYPE_EDIT) { %>
<% } %>
<%
	}
	} // End delete row checking	
}
%>
<%
	if (VehiculosPicoYPlacaHoras.CurrentMode == "add" || VehiculosPicoYPlacaHoras.CurrentMode == "copy" || VehiculosPicoYPlacaHoras.CurrentMode == "edit") {
		VehiculosPicoYPlacaHoras_grid.RowIndex = "$rowindex$";
		VehiculosPicoYPlacaHoras_grid.LoadDefaultValues();

		// Set row properties
		VehiculosPicoYPlacaHoras.ResetAttrs();
		ew_SetAttr(ref VehiculosPicoYPlacaHoras.RowAttrs, null);
		VehiculosPicoYPlacaHoras.CssStyle = "";
		ew_SetAttr(ref VehiculosPicoYPlacaHoras.RowAttrs, null); 
		if (ew_NotEmpty(VehiculosPicoYPlacaHoras_grid.RowIndex)) {
			VehiculosPicoYPlacaHoras.RowAttrs["data-rowindex"] = VehiculosPicoYPlacaHoras_grid.RowIndex;
			VehiculosPicoYPlacaHoras.RowAttrs["id"] = "r" +  VehiculosPicoYPlacaHoras_grid.RowIndex + "_VehiculosPicoYPlacaHoras";
		}
		VehiculosPicoYPlacaHoras.RowType = EW_ROWTYPE_ADD;

		// Render row
		VehiculosPicoYPlacaHoras_grid.RenderRow();

		// Render list options
		VehiculosPicoYPlacaHoras_grid.RenderListOptions();

		// Add id and class to the template row
		VehiculosPicoYPlacaHoras.RowAttrs["id"] = "r0_VehiculosPicoYPlacaHoras";
		VehiculosPicoYPlacaHoras.RowAttrs["class"] = ew_AppendClass(Convert.ToString(VehiculosPicoYPlacaHoras.RowAttrs["class"]), "ewTemplate");
%>
	<tr<%= VehiculosPicoYPlacaHoras.RowAttributes %>>
<%

		// Render list options (body, left)
		VehiculosPicoYPlacaHoras_grid.ListOptions.Render("body", "left");
%>
	<% if (VehiculosPicoYPlacaHoras.HoraInicial.Visible) { // HoraInicial %>
		<td>
<% if (VehiculosPicoYPlacaHoras.CurrentAction != "F") { %>
<input type="text" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" size="10" value="<%= VehiculosPicoYPlacaHoras.HoraInicial.EditValue %>"<%= VehiculosPicoYPlacaHoras.HoraInicial.EditAttributes %> />
<% } else { %>
<div<%= VehiculosPicoYPlacaHoras.HoraInicial.ViewAttributes %>><%= VehiculosPicoYPlacaHoras.HoraInicial.ViewValue %></div>
<input type="hidden" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraInicial.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" id="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraInicial" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraInicial.OldValue) %>" />
</td>
	<% } %>
	<% if (VehiculosPicoYPlacaHoras.HoraFinal.Visible) { // HoraFinal %>
		<td>
<% if (VehiculosPicoYPlacaHoras.CurrentAction != "F") { %>
<input type="text" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" size="10" value="<%= VehiculosPicoYPlacaHoras.HoraFinal.EditValue %>"<%= VehiculosPicoYPlacaHoras.HoraFinal.EditAttributes %> />
<% } else { %>
<div<%= VehiculosPicoYPlacaHoras.HoraFinal.ViewAttributes %>><%= VehiculosPicoYPlacaHoras.HoraFinal.ViewValue %></div>
<input type="hidden" name="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" id="x<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraFinal.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" id="o<%= VehiculosPicoYPlacaHoras_grid.RowIndex %>_HoraFinal" value="<%= ew_HtmlEncode(VehiculosPicoYPlacaHoras.HoraFinal.OldValue) %>" />
</td>
	<% } %>
<%

		// Render list options (body, right)
		VehiculosPicoYPlacaHoras_grid.ListOptions.Render("body", "right");
%>
	</tr>
<%
}
%>
</tbody>
</table>
<% if (VehiculosPicoYPlacaHoras.CurrentMode == "add" || VehiculosPicoYPlacaHoras.CurrentMode == "copy") { %>
<input type="hidden" name="a_list" id="a_list" value="gridinsert" />
<input type="hidden" name="key_count" id="key_count" value="<%= VehiculosPicoYPlacaHoras_grid.KeyCount %>" />
<%= VehiculosPicoYPlacaHoras_grid.MultiSelectKey %>
<% } %>
<% if (VehiculosPicoYPlacaHoras.CurrentMode == "edit") { %>
<input type="hidden" name="a_list" id="a_list" value="gridupdate" />
<input type="hidden" name="key_count" id="key_count" value="<%= VehiculosPicoYPlacaHoras_grid.KeyCount %>" />
<%= VehiculosPicoYPlacaHoras_grid.MultiSelectKey %>
<% } %>
<input type="hidden" name="detailpage" id="detailpage" value="VehiculosPicoYPlacaHoras_grid">
</div>
<%

// Close recordset
if (VehiculosPicoYPlacaHoras_grid.Recordset != null) {
	VehiculosPicoYPlacaHoras_grid.Recordset.Close();
	VehiculosPicoYPlacaHoras_grid.Recordset.Dispose();
}
%>
<% if ((VehiculosPicoYPlacaHoras.CurrentMode == "add" || VehiculosPicoYPlacaHoras.CurrentMode == "copy" || VehiculosPicoYPlacaHoras.CurrentMode == "edit") && VehiculosPicoYPlacaHoras.CurrentAction != "F") { // Add/Copy/Edit mode %>
<div class="ewGridLowerPanel">
</div>
<% } %>
</td></tr></table>
<% if (ew_Empty(VehiculosPicoYPlacaHoras.Export) && ew_Empty(VehiculosPicoYPlacaHoras.CurrentAction)) { %>
<% } %>
<%
VehiculosPicoYPlacaHoras_grid.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
