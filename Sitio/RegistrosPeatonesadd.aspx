<%@ Page ClassName="RegistrosPeatonesadd" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="RegistrosPeatonesadd.aspx.cs" Inherits="RegistrosPeatonesadd" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var RegistrosPeatones_add = new ew_Page("RegistrosPeatones_add");

// page properties
RegistrosPeatones_add.PageID = "add"; // page ID
RegistrosPeatones_add.FormID = "fRegistrosPeatonesadd"; // form ID 
var EW_PAGE_ID = RegistrosPeatones_add.PageID; // for backward compatibility

// extend page with ValidateForm function
RegistrosPeatones_add.ValidateForm = function(fobj) {
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
		elm = fobj.elements["x" + infix + "_IdArea"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(RegistrosPeatones.IdArea.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_IdPersona"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(RegistrosPeatones.IdPersona.FldCaption) %>");

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
RegistrosPeatones_add.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosPeatones_add.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosPeatones_add.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Add") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= RegistrosPeatones.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= RegistrosPeatones.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% RegistrosPeatones_add.ShowPageHeader(); %>
<% RegistrosPeatones_add.ShowMessage(); %>
<form name="fRegistrosPeatonesadd" id="fRegistrosPeatonesadd" method="post" onsubmit="this.action=location.pathname;return RegistrosPeatones_add.ValidateForm(this);">
<p />
<input type="hidden" name="t" id="t" value="RegistrosPeatones" />
<input type="hidden" name="a_add" id="a_add" value="A" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
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
		<td class="ewTableHeader"><%= RegistrosPeatones.IdArea.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= RegistrosPeatones.IdArea.CellAttributes %>><span id="el_IdArea">
<select id="x_IdArea" name="x_IdArea"<%= RegistrosPeatones.IdArea.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(RegistrosPeatones.IdArea.EditValue)) {
	alwrk = (ArrayList)RegistrosPeatones.IdArea.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], RegistrosPeatones.IdArea.CurrentValue)) {
			selwrk = " selected=\"selected\"";
			emptywrk = false;
		} else {
			selwrk = "";
		}
%>
<option value="<%= ew_HtmlEncode(odwrk[0]) %>"<%= selwrk %>>
<%= odwrk[1] %><% if (ew_NotEmpty(odwrk[2])) { %><%= ew_ValueSeparator(rowcntwrk,1,RegistrosPeatones.IdArea) %><%= odwrk[2] %><% } %>
</option>
<%
	}
}
%>
</select>
</span><%= RegistrosPeatones.IdArea.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosPeatones.IdPersona.Visible) { // IdPersona %>
	<tr id="r_IdPersona"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.IdPersona.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= RegistrosPeatones.IdPersona.CellAttributes %>><span id="el_IdPersona">
<select id="x_IdPersona" name="x_IdPersona"<%= RegistrosPeatones.IdPersona.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(RegistrosPeatones.IdPersona.EditValue)) {
	alwrk = (ArrayList)RegistrosPeatones.IdPersona.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], RegistrosPeatones.IdPersona.CurrentValue)) {
			selwrk = " selected=\"selected\"";
			emptywrk = false;
		} else {
			selwrk = "";
		}
%>
<option value="<%= ew_HtmlEncode(odwrk[0]) %>"<%= selwrk %>>
<%= odwrk[1] %><% if (ew_NotEmpty(odwrk[2])) { %><%= ew_ValueSeparator(rowcntwrk,1,RegistrosPeatones.IdPersona) %><%= odwrk[2] %><% } %><% if (ew_NotEmpty(odwrk[3])) { %><%= ew_ValueSeparator(rowcntwrk,2,RegistrosPeatones.IdPersona) %><%= odwrk[3] %><% } %><% if (ew_NotEmpty(odwrk[4])) { %><%= ew_ValueSeparator(rowcntwrk,3,RegistrosPeatones.IdPersona) %><%= odwrk[4] %><% } %>
</option>
<%
	}
}
%>
</select>
<%
sSqlWrk = "SELECT [IdPersona], [IdPersona] AS [DispFld], [Persona] AS [Disp2Fld], [Documento] AS [Disp3Fld], [Activa] AS [Disp4Fld] FROM [dbo].[Personas]";
sWhereWrk = "";
if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
sSqlWrk += " ORDER BY [Persona]";
sSqlWrk = cTEA.Encrypt(sSqlWrk, EW_RANDOM_KEY);
%>
<input type="hidden" name="s_x_IdPersona" id="s_x_IdPersona" value="<%= sSqlWrk %>">
<input type="hidden" name="lft_x_IdPersona" id="lft_x_IdPersona" value="">
</span><%= RegistrosPeatones.IdPersona.CustomMsg %></td>
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
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("AddBtn")) %>" />
</form>
<script language="JavaScript" type="text/javascript">
<!--
ew_UpdateOpts([['x_IdPersona','x_IdPersona',false]]);

//-->
</script>
<%
RegistrosPeatones_add.ShowPageFooter();
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
