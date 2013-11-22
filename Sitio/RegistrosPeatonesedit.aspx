<%@ Page ClassName="RegistrosPeatonesedit" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="RegistrosPeatonesedit.aspx.cs" Inherits="RegistrosPeatonesedit" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var RegistrosPeatones_edit = new ew_Page("RegistrosPeatones_edit");

// page properties
RegistrosPeatones_edit.PageID = "edit"; // page ID
RegistrosPeatones_edit.FormID = "fRegistrosPeatonesedit"; // form ID 
var EW_PAGE_ID = RegistrosPeatones_edit.PageID; // for backward compatibility

// extend page with ValidateForm function
RegistrosPeatones_edit.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_IdTipoDocumento"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(RegistrosPeatones.IdTipoDocumento.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Documento"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(RegistrosPeatones.Documento.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Nombre"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(RegistrosPeatones.Nombre.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Apellidos"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(RegistrosPeatones.Apellidos.FldCaption) %>");

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
	}

	// Process detail page
	var detailpage = (fobj.detailpage) ? fobj.detailpage.value : "";
	if (detailpage != "") {
		return eval(detailpage+".ValidateForm(fobj)");
	}
	return true;
}

// extend page with Form_CustomValidate function
RegistrosPeatones_edit.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosPeatones_edit.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosPeatones_edit.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Edit") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= RegistrosPeatones.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= RegistrosPeatones.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% RegistrosPeatones_edit.ShowPageHeader(); %>
<% RegistrosPeatones_edit.ShowMessage(); %>
<form name="fRegistrosPeatonesedit" id="fRegistrosPeatonesedit" method="post" onsubmit="this.action=location.pathname;return RegistrosPeatones_edit.ValidateForm(this);">
<p />
<input type="hidden" name="a_table" id="a_table" value="RegistrosPeatones" />
<input type="hidden" name="a_edit" id="a_edit" value="U" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (RegistrosPeatones.IdRegistroPeaton.Visible) { // IdRegistroPeaton %>
	<tr id="r_IdRegistroPeaton"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.IdRegistroPeaton.FldCaption %></td>
		<td<%= RegistrosPeatones.IdRegistroPeaton.CellAttributes %>><span id="el_IdRegistroPeaton">
<div<%= RegistrosPeatones.IdRegistroPeaton.ViewAttributes %>><%= RegistrosPeatones.IdRegistroPeaton.EditValue %></div>
<input type="hidden" name="x_IdRegistroPeaton" id="x_IdRegistroPeaton" value="<%= ew_HtmlEncode(RegistrosPeatones.IdRegistroPeaton.CurrentValue) %>" />
</span><%= RegistrosPeatones.IdRegistroPeaton.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.IdTipoDocumento.Visible) { // IdTipoDocumento %>
	<tr id="r_IdTipoDocumento"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.IdTipoDocumento.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= RegistrosPeatones.IdTipoDocumento.CellAttributes %>><span id="el_IdTipoDocumento">
<select id="x_IdTipoDocumento" name="x_IdTipoDocumento"<%= RegistrosPeatones.IdTipoDocumento.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(RegistrosPeatones.IdTipoDocumento.EditValue)) {
	alwrk = (ArrayList)RegistrosPeatones.IdTipoDocumento.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], RegistrosPeatones.IdTipoDocumento.CurrentValue)) {
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
%>
</select>
</span><%= RegistrosPeatones.IdTipoDocumento.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.Documento.Visible) { // Documento %>
	<tr id="r_Documento"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.Documento.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= RegistrosPeatones.Documento.CellAttributes %>><span id="el_Documento">
<input type="text" name="x_Documento" id="x_Documento" size="15" maxlength="15" value="<%= RegistrosPeatones.Documento.EditValue %>"<%= RegistrosPeatones.Documento.EditAttributes %> />
</span><%= RegistrosPeatones.Documento.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.Nombre.Visible) { // Nombre %>
	<tr id="r_Nombre"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.Nombre.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= RegistrosPeatones.Nombre.CellAttributes %>><span id="el_Nombre">
<input type="text" name="x_Nombre" id="x_Nombre" size="50" maxlength="50" value="<%= RegistrosPeatones.Nombre.EditValue %>"<%= RegistrosPeatones.Nombre.EditAttributes %> />
</span><%= RegistrosPeatones.Nombre.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.Apellidos.Visible) { // Apellidos %>
	<tr id="r_Apellidos"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.Apellidos.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= RegistrosPeatones.Apellidos.CellAttributes %>><span id="el_Apellidos">
<input type="text" name="x_Apellidos" id="x_Apellidos" size="50" maxlength="50" value="<%= RegistrosPeatones.Apellidos.EditValue %>"<%= RegistrosPeatones.Apellidos.EditAttributes %> />
</span><%= RegistrosPeatones.Apellidos.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.IdArea.Visible) { // IdArea %>
	<tr id="r_IdArea"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.IdArea.FldCaption %></td>
		<td<%= RegistrosPeatones.IdArea.CellAttributes %>><span id="el_IdArea">
<div<%= RegistrosPeatones.IdArea.ViewAttributes %>><%= RegistrosPeatones.IdArea.EditValue %></div>
<input type="hidden" name="x_IdArea" id="x_IdArea" value="<%= ew_HtmlEncode(RegistrosPeatones.IdArea.CurrentValue) %>" />
</span><%= RegistrosPeatones.IdArea.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.IdPersona.Visible) { // IdPersona %>
	<tr id="r_IdPersona"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.IdPersona.FldCaption %></td>
		<td<%= RegistrosPeatones.IdPersona.CellAttributes %>><span id="el_IdPersona">
<div<%= RegistrosPeatones.IdPersona.ViewAttributes %>><%= RegistrosPeatones.IdPersona.EditValue %></div>
<input type="hidden" name="x_IdPersona" id="x_IdPersona" value="<%= ew_HtmlEncode(RegistrosPeatones.IdPersona.CurrentValue) %>" />
</span><%= RegistrosPeatones.IdPersona.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.FechaIngreso.Visible) { // FechaIngreso %>
	<tr id="r_FechaIngreso"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.FechaIngreso.FldCaption %></td>
		<td<%= RegistrosPeatones.FechaIngreso.CellAttributes %>><span id="el_FechaIngreso">
<div<%= RegistrosPeatones.FechaIngreso.ViewAttributes %>><%= RegistrosPeatones.FechaIngreso.EditValue %></div>
<input type="hidden" name="x_FechaIngreso" id="x_FechaIngreso" value="<%= ew_HtmlEncode(RegistrosPeatones.FechaIngreso.CurrentValue) %>" />
</span><%= RegistrosPeatones.FechaIngreso.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.FechaSalida.Visible) { // FechaSalida %>
	<tr id="r_FechaSalida"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.FechaSalida.FldCaption %></td>
		<td<%= RegistrosPeatones.FechaSalida.CellAttributes %>><span id="el_FechaSalida">
<div<%= RegistrosPeatones.FechaSalida.ViewAttributes %>><%= RegistrosPeatones.FechaSalida.EditValue %></div>
<input type="hidden" name="x_FechaSalida" id="x_FechaSalida" value="<%= ew_HtmlEncode(RegistrosPeatones.FechaSalida.CurrentValue) %>" />
</span><%= RegistrosPeatones.FechaSalida.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.Observacion.Visible) { // Observacion %>
	<tr id="r_Observacion"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.Observacion.FldCaption %></td>
		<td<%= RegistrosPeatones.Observacion.CellAttributes %>><span id="el_Observacion">
<textarea name="x_Observacion" id="x_Observacion" cols="35" rows="4"<%= RegistrosPeatones.Observacion.EditAttributes %>><%= RegistrosPeatones.Observacion.EditValue %></textarea>
</span><%= RegistrosPeatones.Observacion.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("EditBtn")) %>" />
</form>
<%
RegistrosPeatones_edit.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
</asp:Content>
