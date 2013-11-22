<%@ Page ClassName="Personasgrid" Language="C#" CodePage="65001" AutoEventWireup="true" CodeFile="Personasgrid.aspx.cs" Inherits="Personasgrid" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<% if (ew_Empty(Personas.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var Personas_grid = new ew_Page("Personas_grid");

// page properties
Personas_grid.PageID = "grid"; // page ID
Personas_grid.FormID = "fPersonasgrid"; // form ID 
var EW_PAGE_ID = Personas_grid.PageID; // for backward compatibility

// extend page with ValidateForm function
Personas_grid.ValidateForm = function(fobj) {
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
		elm = fobj.elements["x" + infix + "_IdArea"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Personas.IdArea.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_IdCargo"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Personas.IdCargo.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Documento"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Personas.Documento.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Persona"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Personas.Persona.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Activa"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Personas.Activa.FldCaption) %>");

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
Personas_grid.EmptyRow = function(fobj, infix) {
	if (ew_ValueChanged(fobj, infix, "IdArea")) return false;
	if (ew_ValueChanged(fobj, infix, "IdCargo")) return false;
	if (ew_ValueChanged(fobj, infix, "Documento")) return false;
	if (ew_ValueChanged(fobj, infix, "Persona")) return false;
	if (ew_ValueChanged(fobj, infix, "Activa")) return false;
	return true;
}

// extend page with Form_CustomValidate function
Personas_grid.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Personas_grid.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Personas_grid.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<% } %>
<%
if (Personas.CurrentAction == "gridadd") {
	if (Personas.CurrentMode == "copy") {

		// Load recordset
		Personas_grid.Recordset = Personas_grid.LoadRecordset();		
		Personas_grid.StartRec = 1;
		Personas_grid.DisplayRecs = Personas_grid.TotalRecs;
	} else {
		Personas.CurrentFilter = "0=1";
		Personas_grid.StartRec = 1;
		Personas_grid.DisplayRecs = Personas.GridAddRowCount;
	}
	Personas_grid.TotalRecs = Personas_grid.DisplayRecs;
	Personas_grid.StopRec = Personas_grid.DisplayRecs;
} else {
	Personas_grid.Recordset = Personas_grid.LoadRecordset();
	Personas_grid.StartRec = 1;
		Personas_grid.DisplayRecs = Personas_grid.TotalRecs; // Display all records
}
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><% if (Personas.CurrentMode == "add" || Personas.CurrentMode == "copy") { %><%= Language.Phrase("Add") %><% } else if (Personas.CurrentMode == "edit") { %><%= Language.Phrase("Edit") %><% } %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Personas.TableCaption %>
</p>
<% Personas_grid.ShowPageHeader(); %>
<% Personas_grid.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if ((Personas.CurrentMode == "add" || Personas.CurrentMode == "copy" || Personas.CurrentMode == "edit") && Personas.CurrentAction != "F") { // Add/Copy/Edit mode %>
<div class="ewGridUpperPanel">
<% if (Personas.AllowAddDeleteRow) { %>
<% if (Security.CanAdd) { %>
<span class="aspnetmaker">
<a href="javascript:void(0);" onclick="ew_AddGridRow(this);"><%= Language.Phrase("AddBlankRow") %></a>&nbsp;&nbsp;
</span>
<% } %>
<% } %>
</div>
<% } %>
<div id="gmp_Personas" class="ewGridMiddlePanel">
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= Personas.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		Personas_grid.RenderListOptions();

		// Render list options (header, left)
		Personas_grid.ListOptions.Render("header", "left");
%>
<% if (Personas.IdArea.Visible) { // IdArea %>
	<% if (ew_Empty(Personas.SortUrl(Personas.IdArea))) { %>
		<td><%= Personas.IdArea.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.IdArea.FldCaption %></td><td style="width: 10px;"><% if (Personas.IdArea.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.IdArea.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Personas.IdCargo.Visible) { // IdCargo %>
	<% if (ew_Empty(Personas.SortUrl(Personas.IdCargo))) { %>
		<td><%= Personas.IdCargo.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.IdCargo.FldCaption %></td><td style="width: 10px;"><% if (Personas.IdCargo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.IdCargo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Personas.Documento.Visible) { // Documento %>
	<% if (ew_Empty(Personas.SortUrl(Personas.Documento))) { %>
		<td><%= Personas.Documento.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.Documento.FldCaption %></td><td style="width: 10px;"><% if (Personas.Documento.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.Documento.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Personas.Persona.Visible) { // Persona %>
	<% if (ew_Empty(Personas.SortUrl(Personas.Persona))) { %>
		<td><%= Personas.Persona.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.Persona.FldCaption %></td><td style="width: 10px;"><% if (Personas.Persona.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.Persona.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Personas.Activa.Visible) { // Activa %>
	<% if (ew_Empty(Personas.SortUrl(Personas.Activa))) { %>
		<td><%= Personas.Activa.FldCaption %></td>
	<% } else { %>
		<td><div>
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.Activa.FldCaption %></td><td style="width: 10px;"><% if (Personas.Activa.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.Activa.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		Personas_grid.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
Personas_grid.StartRec = 1;
Personas_grid.StopRec = Personas_grid.TotalRecs; // Show all records

// Restore number of post back records
if (ObjForm != null) {
	ObjForm.Index = 0;
	if (ObjForm.HasValue("key_count") && (Personas.CurrentAction == "gridadd" || Personas.CurrentAction == "gridedit" || Personas.CurrentAction == "F")) {
		Personas_grid.KeyCount = ew_ConvertToInt(ObjForm.GetValue("key_count"));
		Personas_grid.StopRec = Personas_grid.KeyCount;
	}
}
if (Personas_grid.Recordset != null && Personas_grid.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= Personas_grid.StartRec - 1; i++) {
		if (Personas_grid.Recordset.Read())
			Personas_grid.RecCnt += 1;
	}		
} else if (!Personas.AllowAddDeleteRow && Personas_grid.StopRec == 0) {
	Personas_grid.StopRec = Personas.GridAddRowCount;
}

// Initialize Aggregate
Personas.RowType = EW_ROWTYPE_AGGREGATEINIT;
Personas.ResetAttrs();
Personas_grid.RenderRow();
Personas_grid.RowCnt = 0;
if (Personas.CurrentAction == "gridadd") Personas_grid.RowIndex = 0;
if (Personas.CurrentAction == "gridedit") Personas_grid.RowIndex = 0;

// Output data rows
bool Eof = false; // ASPX
while (Personas_grid.RecCnt < Personas_grid.StopRec) {
	if ((Personas.CurrentAction != "gridadd" || Personas.CurrentMode == "copy") && !Eof) // ASPX
		Eof = !Personas_grid.Recordset.Read();
	Personas_grid.RecCnt += 1;
	if (Personas_grid.RecCnt >= Personas_grid.StartRec) {
		Personas_grid.RowCnt += 1;
		if (Personas.CurrentAction == "gridadd" || Personas.CurrentAction == "gridedit" || Personas.CurrentAction == "F") {
			Personas_grid.RowIndex = ew_ConvertToInt(Personas_grid.RowIndex) + 1;
			ObjForm.Index = ew_ConvertToInt(Personas_grid.RowIndex);
			if (ObjForm.HasValue("k_action")) {
				Personas_grid.RowAction = ObjForm.GetValue("k_action");
			} else if (Personas.CurrentAction == "gridadd") {
				Personas_grid.RowAction = "insert";
			} else {
				Personas_grid.RowAction = "";
			}
		}

		// Set up key count
		Personas_grid.KeyCount = ew_ConvertToInt(Personas_grid.RowIndex);

		// Init row class and style
		Personas.ResetAttrs();
		Personas.CssClass = "";	 
		if (Personas.CurrentAction == "gridadd") {
			if (Personas.CurrentMode == "copy" && !Eof) { // ASPX
				Personas_grid.LoadRowValues(ref Personas_grid.Recordset); // Load row values
				Personas_grid.SetRecordKey(ref Personas_grid.RowOldKey, Personas_grid.Recordset); // Set old record key
			} else {
				Personas_grid.LoadDefaultValues(); // Load default values
				Personas_grid.RowOldKey = ""; // Clear old key value
			}
		} else if (Personas.CurrentAction == "gridedit") {
			Personas_grid.LoadRowValues(ref Personas_grid.Recordset); // Load row values
		}
		Personas.RowType = EW_ROWTYPE_VIEW; // Render view
		if (Personas.CurrentAction == "gridadd") // Grid add
			Personas.RowType = EW_ROWTYPE_ADD; // Render add

		//***if (Personas.CurrentAction == "gridadd" && Personas.EventCancelled) // Insert failed
		if (Personas.CurrentAction == "gridadd" && Personas.EventCancelled && !ObjForm.HasValue("k_blankrow")) // Insert failed
			Personas_grid.RestoreCurrentRowFormValues(Personas_grid.RowIndex); // Restore form values
		if (Personas.CurrentAction == "gridedit") { // Grid edit
			if (Personas.EventCancelled)
				Personas_grid.RestoreCurrentRowFormValues(Personas_grid.RowIndex); // Restore form values
			if (Personas_grid.RowAction == "insert")
				Personas.RowType = EW_ROWTYPE_ADD; // Render add
			else
				Personas.RowType = EW_ROWTYPE_EDIT; // Render edit
		}
		if (Personas.CurrentAction == "gridedit" && (Personas.RowType == EW_ROWTYPE_EDIT || Personas.RowType == EW_ROWTYPE_ADD) && Personas.EventCancelled) // Update failed
			Personas_grid.RestoreCurrentRowFormValues(Personas_grid.RowIndex); // Restore form values
		if (Personas.RowType == EW_ROWTYPE_EDIT) // Edit row
			Personas_grid.EditRowCnt += 1;
		if (Personas.CurrentAction == "F") // Confirm row
			Personas_grid.RestoreCurrentRowFormValues(Personas_grid.RowIndex); // Restore form values
		if (Personas.RowType == EW_ROWTYPE_ADD || Personas.RowType == EW_ROWTYPE_EDIT) { // Add / Edit row
			if (Personas.CurrentAction == "edit") {
				ew_SetAttr(ref Personas.RowAttrs, null);
				Personas.CssClass = "ewTableEditRow";
			} else {
				ew_SetAttr(ref Personas.RowAttrs, null);
			}		 
			if (ew_NotEmpty(Personas_grid.RowIndex)) {
				Personas.RowAttrs["data-rowindex"] = Personas_grid.RowIndex;
				Personas.RowAttrs["id"] = "r" + Personas_grid.RowIndex + "_Personas";
			}
		} else {
			ew_SetAttr(ref Personas.RowAttrs, null);
		}

		// Render row
		Personas_grid.RenderRow();

		// Render list options
		Personas_grid.RenderListOptions();

		// Skip delete row / empty row for confirm page
		if (Personas_grid.RowAction != "delete" && Personas_grid.RowAction != "insertdelete" && !(Personas_grid.RowAction == "insert" && Personas.CurrentAction == "F" && Personas_grid.EmptyRow())) {
%>
	<tr<%= Personas.RowAttributes %>>
	<%

		// Render list options (body, left)
		Personas_grid.ListOptions.Render("body", "left");
	%>
	<% if (Personas.IdArea.Visible) { // IdArea %>
		<td<%= Personas.IdArea.CellAttributes %>>
<% if (Personas.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<% if (ew_NotEmpty(Personas.IdArea.SessionValue)) { %>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ListViewValue %></div>
<input type="hidden" id="x<%= Personas_grid.RowIndex %>_IdArea" name="x<%= Personas_grid.RowIndex %>_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.CurrentValue) %>">
<% } else { %>
<select id="x<%= Personas_grid.RowIndex %>_IdArea" name="x<%= Personas_grid.RowIndex %>_IdArea"<%= Personas.IdArea.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdArea.EditValue)) {
	alwrk = (ArrayList)Personas.IdArea.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdArea.CurrentValue)) {
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
if (emptywrk) Personas.IdArea.OldValue = "";
%>
</select>
<% } %>
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_IdArea" id="o<%= Personas_grid.RowIndex %>_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.OldValue) %>" />
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<% if (ew_NotEmpty(Personas.IdArea.SessionValue)) { %>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ListViewValue %></div>
<input type="hidden" id="x<%= Personas_grid.RowIndex %>_IdArea" name="x<%= Personas_grid.RowIndex %>_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.CurrentValue) %>">
<% } else { %>
<select id="x<%= Personas_grid.RowIndex %>_IdArea" name="x<%= Personas_grid.RowIndex %>_IdArea"<%= Personas.IdArea.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdArea.EditValue)) {
	alwrk = (ArrayList)Personas.IdArea.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdArea.CurrentValue)) {
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
if (emptywrk) Personas.IdArea.OldValue = "";
%>
</select>
<% } %>
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ListViewValue %></div>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_IdArea" id="x<%= Personas_grid.RowIndex %>_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.CurrentValue) %>" />
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_IdArea" id="o<%= Personas_grid.RowIndex %>_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.OldValue) %>" />
<% } %>
<a name="<%= Personas_grid.PageObjName + "_row_" + Personas_grid.RowCnt %>" id="<%= Personas_grid.PageObjName + "_row_" + Personas_grid.RowCnt %>"></a>
<% if (Personas.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_IdPersona" id="o<%= Personas_grid.RowIndex %>_IdPersona" value="<%= ew_HtmlEncode(Personas.IdPersona.OldValue) %>" />
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_EDIT) { %>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_IdPersona" id="x<%= Personas_grid.RowIndex %>_IdPersona" value="<%= ew_HtmlEncode(Personas.IdPersona.CurrentValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (Personas.IdCargo.Visible) { // IdCargo %>
		<td<%= Personas.IdCargo.CellAttributes %>>
<% if (Personas.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<select id="x<%= Personas_grid.RowIndex %>_IdCargo" name="x<%= Personas_grid.RowIndex %>_IdCargo"<%= Personas.IdCargo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdCargo.EditValue)) {
	alwrk = (ArrayList)Personas.IdCargo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdCargo.CurrentValue)) {
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
if (emptywrk) Personas.IdCargo.OldValue = "";
%>
</select>
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_IdCargo" id="o<%= Personas_grid.RowIndex %>_IdCargo" value="<%= ew_HtmlEncode(Personas.IdCargo.OldValue) %>" />
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<select id="x<%= Personas_grid.RowIndex %>_IdCargo" name="x<%= Personas_grid.RowIndex %>_IdCargo"<%= Personas.IdCargo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdCargo.EditValue)) {
	alwrk = (ArrayList)Personas.IdCargo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdCargo.CurrentValue)) {
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
if (emptywrk) Personas.IdCargo.OldValue = "";
%>
</select>
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<div<%= Personas.IdCargo.ViewAttributes %>><%= Personas.IdCargo.ListViewValue %></div>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_IdCargo" id="x<%= Personas_grid.RowIndex %>_IdCargo" value="<%= ew_HtmlEncode(Personas.IdCargo.CurrentValue) %>" />
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_IdCargo" id="o<%= Personas_grid.RowIndex %>_IdCargo" value="<%= ew_HtmlEncode(Personas.IdCargo.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (Personas.Documento.Visible) { // Documento %>
		<td<%= Personas.Documento.CellAttributes %>>
<% if (Personas.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<input type="text" name="x<%= Personas_grid.RowIndex %>_Documento" id="x<%= Personas_grid.RowIndex %>_Documento" size="30" maxlength="50" value="<%= Personas.Documento.EditValue %>"<%= Personas.Documento.EditAttributes %> />
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_Documento" id="o<%= Personas_grid.RowIndex %>_Documento" value="<%= ew_HtmlEncode(Personas.Documento.OldValue) %>" />
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<input type="text" name="x<%= Personas_grid.RowIndex %>_Documento" id="x<%= Personas_grid.RowIndex %>_Documento" size="30" maxlength="50" value="<%= Personas.Documento.EditValue %>"<%= Personas.Documento.EditAttributes %> />
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<div<%= Personas.Documento.ViewAttributes %>><%= Personas.Documento.ListViewValue %></div>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_Documento" id="x<%= Personas_grid.RowIndex %>_Documento" value="<%= ew_HtmlEncode(Personas.Documento.CurrentValue) %>" />
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_Documento" id="o<%= Personas_grid.RowIndex %>_Documento" value="<%= ew_HtmlEncode(Personas.Documento.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (Personas.Persona.Visible) { // Persona %>
		<td<%= Personas.Persona.CellAttributes %>>
<% if (Personas.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<input type="text" name="x<%= Personas_grid.RowIndex %>_Persona" id="x<%= Personas_grid.RowIndex %>_Persona" size="50" maxlength="50" value="<%= Personas.Persona.EditValue %>"<%= Personas.Persona.EditAttributes %> />
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_Persona" id="o<%= Personas_grid.RowIndex %>_Persona" value="<%= ew_HtmlEncode(Personas.Persona.OldValue) %>" />
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<input type="text" name="x<%= Personas_grid.RowIndex %>_Persona" id="x<%= Personas_grid.RowIndex %>_Persona" size="50" maxlength="50" value="<%= Personas.Persona.EditValue %>"<%= Personas.Persona.EditAttributes %> />
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<div<%= Personas.Persona.ViewAttributes %>><%= Personas.Persona.ListViewValue %></div>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_Persona" id="x<%= Personas_grid.RowIndex %>_Persona" value="<%= ew_HtmlEncode(Personas.Persona.CurrentValue) %>" />
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_Persona" id="o<%= Personas_grid.RowIndex %>_Persona" value="<%= ew_HtmlEncode(Personas.Persona.OldValue) %>" />
<% } %>
</td>
	<% } %>
	<% if (Personas.Activa.Visible) { // Activa %>
		<td<%= Personas.Activa.CellAttributes %>>
<% if (Personas.RowType == EW_ROWTYPE_ADD) { // Add Record %>
<%
selwrk = (ew_SameStr(Personas.Activa.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= Personas_grid.RowIndex %>_Activa" id="x<%= Personas_grid.RowIndex %>_Activa" value="1"<%= selwrk %><%= Personas.Activa.EditAttributes %> />
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_Activa" id="o<%= Personas_grid.RowIndex %>_Activa" value="<%= ew_HtmlEncode(Personas.Activa.OldValue) %>" />
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_EDIT) { // Edit Record %>
<%
selwrk = (ew_SameStr(Personas.Activa.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= Personas_grid.RowIndex %>_Activa" id="x<%= Personas_grid.RowIndex %>_Activa" value="1"<%= selwrk %><%= Personas.Activa.EditAttributes %> />
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_VIEW) { // View Record %>
<% if (Convert.ToString(Personas.Activa.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_Activa" id="x<%= Personas_grid.RowIndex %>_Activa" value="<%= ew_HtmlEncode(Personas.Activa.CurrentValue) %>" />
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_Activa" id="o<%= Personas_grid.RowIndex %>_Activa" value="<%= ew_HtmlEncode(Personas.Activa.OldValue) %>" />
<% } %>
</td>
	<% } %>
<%

		// Render list options (body, right)
		Personas_grid.ListOptions.Render("body", "right");
%>
	</tr>
<% if (Personas.RowType == EW_ROWTYPE_ADD) { %>
<% } %>
<% if (Personas.RowType == EW_ROWTYPE_EDIT) { %>
<% } %>
<%
	}
	} // End delete row checking	
}
%>
<%
	if (Personas.CurrentMode == "add" || Personas.CurrentMode == "copy" || Personas.CurrentMode == "edit") {
		Personas_grid.RowIndex = "$rowindex$";
		Personas_grid.LoadDefaultValues();

		// Set row properties
		Personas.ResetAttrs();
		ew_SetAttr(ref Personas.RowAttrs, null);
		Personas.CssStyle = "";
		ew_SetAttr(ref Personas.RowAttrs, null); 
		if (ew_NotEmpty(Personas_grid.RowIndex)) {
			Personas.RowAttrs["data-rowindex"] = Personas_grid.RowIndex;
			Personas.RowAttrs["id"] = "r" +  Personas_grid.RowIndex + "_Personas";
		}
		Personas.RowType = EW_ROWTYPE_ADD;

		// Render row
		Personas_grid.RenderRow();

		// Render list options
		Personas_grid.RenderListOptions();

		// Add id and class to the template row
		Personas.RowAttrs["id"] = "r0_Personas";
		Personas.RowAttrs["class"] = ew_AppendClass(Convert.ToString(Personas.RowAttrs["class"]), "ewTemplate");
%>
	<tr<%= Personas.RowAttributes %>>
<%

		// Render list options (body, left)
		Personas_grid.ListOptions.Render("body", "left");
%>
	<% if (Personas.IdArea.Visible) { // IdArea %>
		<td>
<% if (Personas.CurrentAction != "F") { %>
<% if (ew_NotEmpty(Personas.IdArea.SessionValue)) { %>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ListViewValue %></div>
<input type="hidden" id="x<%= Personas_grid.RowIndex %>_IdArea" name="x<%= Personas_grid.RowIndex %>_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.CurrentValue) %>">
<% } else { %>
<select id="x<%= Personas_grid.RowIndex %>_IdArea" name="x<%= Personas_grid.RowIndex %>_IdArea"<%= Personas.IdArea.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdArea.EditValue)) {
	alwrk = (ArrayList)Personas.IdArea.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdArea.CurrentValue)) {
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
if (emptywrk) Personas.IdArea.OldValue = "";
%>
</select>
<% } %>
<% } else { %>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ViewValue %></div>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_IdArea" id="x<%= Personas_grid.RowIndex %>_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_IdArea" id="o<%= Personas_grid.RowIndex %>_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.OldValue) %>" />
</td>
	<% } %>
	<% if (Personas.IdCargo.Visible) { // IdCargo %>
		<td>
<% if (Personas.CurrentAction != "F") { %>
<select id="x<%= Personas_grid.RowIndex %>_IdCargo" name="x<%= Personas_grid.RowIndex %>_IdCargo"<%= Personas.IdCargo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdCargo.EditValue)) {
	alwrk = (ArrayList)Personas.IdCargo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdCargo.CurrentValue)) {
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
if (emptywrk) Personas.IdCargo.OldValue = "";
%>
</select>
<% } else { %>
<div<%= Personas.IdCargo.ViewAttributes %>><%= Personas.IdCargo.ViewValue %></div>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_IdCargo" id="x<%= Personas_grid.RowIndex %>_IdCargo" value="<%= ew_HtmlEncode(Personas.IdCargo.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_IdCargo" id="o<%= Personas_grid.RowIndex %>_IdCargo" value="<%= ew_HtmlEncode(Personas.IdCargo.OldValue) %>" />
</td>
	<% } %>
	<% if (Personas.Documento.Visible) { // Documento %>
		<td>
<% if (Personas.CurrentAction != "F") { %>
<input type="text" name="x<%= Personas_grid.RowIndex %>_Documento" id="x<%= Personas_grid.RowIndex %>_Documento" size="30" maxlength="50" value="<%= Personas.Documento.EditValue %>"<%= Personas.Documento.EditAttributes %> />
<% } else { %>
<div<%= Personas.Documento.ViewAttributes %>><%= Personas.Documento.ViewValue %></div>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_Documento" id="x<%= Personas_grid.RowIndex %>_Documento" value="<%= ew_HtmlEncode(Personas.Documento.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_Documento" id="o<%= Personas_grid.RowIndex %>_Documento" value="<%= ew_HtmlEncode(Personas.Documento.OldValue) %>" />
</td>
	<% } %>
	<% if (Personas.Persona.Visible) { // Persona %>
		<td>
<% if (Personas.CurrentAction != "F") { %>
<input type="text" name="x<%= Personas_grid.RowIndex %>_Persona" id="x<%= Personas_grid.RowIndex %>_Persona" size="50" maxlength="50" value="<%= Personas.Persona.EditValue %>"<%= Personas.Persona.EditAttributes %> />
<% } else { %>
<div<%= Personas.Persona.ViewAttributes %>><%= Personas.Persona.ViewValue %></div>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_Persona" id="x<%= Personas_grid.RowIndex %>_Persona" value="<%= ew_HtmlEncode(Personas.Persona.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_Persona" id="o<%= Personas_grid.RowIndex %>_Persona" value="<%= ew_HtmlEncode(Personas.Persona.OldValue) %>" />
</td>
	<% } %>
	<% if (Personas.Activa.Visible) { // Activa %>
		<td>
<% if (Personas.CurrentAction != "F") { %>
<%
selwrk = (ew_SameStr(Personas.Activa.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x<%= Personas_grid.RowIndex %>_Activa" id="x<%= Personas_grid.RowIndex %>_Activa" value="1"<%= selwrk %><%= Personas.Activa.EditAttributes %> />
<% } else { %>
<% if (Convert.ToString(Personas.Activa.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Personas.Activa.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Personas.Activa.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
<input type="hidden" name="x<%= Personas_grid.RowIndex %>_Activa" id="x<%= Personas_grid.RowIndex %>_Activa" value="<%= ew_HtmlEncode(Personas.Activa.CurrentValue) %>" />
<% } %>
<input type="hidden" name="o<%= Personas_grid.RowIndex %>_Activa" id="o<%= Personas_grid.RowIndex %>_Activa" value="<%= ew_HtmlEncode(Personas.Activa.OldValue) %>" />
</td>
	<% } %>
<%

		// Render list options (body, right)
		Personas_grid.ListOptions.Render("body", "right");
%>
	</tr>
<%
}
%>
</tbody>
</table>
<% if (Personas.CurrentMode == "add" || Personas.CurrentMode == "copy") { %>
<input type="hidden" name="a_list" id="a_list" value="gridinsert" />
<input type="hidden" name="key_count" id="key_count" value="<%= Personas_grid.KeyCount %>" />
<%= Personas_grid.MultiSelectKey %>
<% } %>
<% if (Personas.CurrentMode == "edit") { %>
<input type="hidden" name="a_list" id="a_list" value="gridupdate" />
<input type="hidden" name="key_count" id="key_count" value="<%= Personas_grid.KeyCount %>" />
<%= Personas_grid.MultiSelectKey %>
<% } %>
<input type="hidden" name="detailpage" id="detailpage" value="Personas_grid">
</div>
<%

// Close recordset
if (Personas_grid.Recordset != null) {
	Personas_grid.Recordset.Close();
	Personas_grid.Recordset.Dispose();
}
%>
<% if ((Personas.CurrentMode == "add" || Personas.CurrentMode == "copy" || Personas.CurrentMode == "edit") && Personas.CurrentAction != "F") { // Add/Copy/Edit mode %>
<div class="ewGridLowerPanel">
<% if (Personas.AllowAddDeleteRow) { %>
<% if (Security.CanAdd) { %>
<span class="aspnetmaker">
<a href="javascript:void(0);" onclick="ew_AddGridRow(this);"><%= Language.Phrase("AddBlankRow") %></a>&nbsp;&nbsp;
</span>
<% } %>
<% } %>
</div>
<% } %>
</td></tr></table>
<% if (ew_Empty(Personas.Export) && ew_Empty(Personas.CurrentAction)) { %>
<% } %>
<%
Personas_grid.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
