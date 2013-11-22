<%@ Page ClassName="Usuariosadd" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Usuariosadd.aspx.cs" Inherits="Usuariosadd" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var Usuarios_add = new ew_Page("Usuarios_add");

// page properties
Usuarios_add.PageID = "add"; // page ID
Usuarios_add.FormID = "fUsuariosadd"; // form ID 
var EW_PAGE_ID = Usuarios_add.PageID; // for backward compatibility

// extend page with ValidateForm function
Usuarios_add.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_Usuario"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Usuarios.Usuario.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_NombreUsuario"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Usuarios.NombreUsuario.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Password"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Usuarios.Password.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Correo"];
		if (elm && elm.type != "hidden" && !ew_CheckEmail(elm.value)) // skip hidden field
			return ew_OnError(this, elm, "<%= ew_JsEncode2(Usuarios.Correo.FldErrMsg) %>");
		elm = fobj.elements["x" + infix + "_IdUsuarioNivel"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Usuarios.IdUsuarioNivel.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Activo"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Usuarios.Activo.FldCaption) %>");

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
Usuarios_add.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Usuarios_add.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Usuarios_add.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Add") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Usuarios.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= Usuarios.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% Usuarios_add.ShowPageHeader(); %>
<% Usuarios_add.ShowMessage(); %>
<form name="fUsuariosadd" id="fUsuariosadd" method="post" onsubmit="this.action=location.pathname;return Usuarios_add.ValidateForm(this);">
<p />
<input type="hidden" name="t" id="t" value="Usuarios" />
<input type="hidden" name="a_add" id="a_add" value="A" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (Usuarios.Usuario.Visible) { // Usuario %>
	<tr id="r_Usuario"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.Usuario.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Usuarios.Usuario.CellAttributes %>><span id="el_Usuario">
<input type="text" name="x_Usuario" id="x_Usuario" size="30" maxlength="50" value="<%= Usuarios.Usuario.EditValue %>"<%= Usuarios.Usuario.EditAttributes %> />
</span><%= Usuarios.Usuario.CustomMsg %></td>
	</tr>
<% } %>
<% if (Usuarios.NombreUsuario.Visible) { // NombreUsuario %>
	<tr id="r_NombreUsuario"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.NombreUsuario.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Usuarios.NombreUsuario.CellAttributes %>><span id="el_NombreUsuario">
<input type="text" name="x_NombreUsuario" id="x_NombreUsuario" size="50" maxlength="50" value="<%= Usuarios.NombreUsuario.EditValue %>"<%= Usuarios.NombreUsuario.EditAttributes %> />
</span><%= Usuarios.NombreUsuario.CustomMsg %></td>
	</tr>
<% } %>
<% if (Usuarios.Password.Visible) { // Password %>
	<tr id="r_Password"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.Password.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Usuarios.Password.CellAttributes %>><span id="el_Password">
<input type="password" name="x_Password" id="x_Password" size="32" maxlength="32"<%= Usuarios.Password.EditAttributes %> />
</span><%= Usuarios.Password.CustomMsg %></td>
	</tr>
<% } %>
<% if (Usuarios.Correo.Visible) { // Correo %>
	<tr id="r_Correo"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.Correo.FldCaption %></td>
		<td<%= Usuarios.Correo.CellAttributes %>><span id="el_Correo">
<input type="text" name="x_Correo" id="x_Correo" size="30" maxlength="100" value="<%= Usuarios.Correo.EditValue %>"<%= Usuarios.Correo.EditAttributes %> />
</span><%= Usuarios.Correo.CustomMsg %></td>
	</tr>
<% } %>
<% if (Usuarios.IdUsuarioNivel.Visible) { // IdUsuarioNivel %>
	<tr id="r_IdUsuarioNivel"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.IdUsuarioNivel.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Usuarios.IdUsuarioNivel.CellAttributes %>><span id="el_IdUsuarioNivel">
<% if (!Security.IsAdmin() && Security.IsLoggedIn()) { // Non system admin %>
<div<%= Usuarios.IdUsuarioNivel.ViewAttributes %>><%= Usuarios.IdUsuarioNivel.EditValue %></div>
<% } else { %>
<select id="x_IdUsuarioNivel" name="x_IdUsuarioNivel"<%= Usuarios.IdUsuarioNivel.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Usuarios.IdUsuarioNivel.EditValue)) {
	alwrk = (ArrayList)Usuarios.IdUsuarioNivel.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Usuarios.IdUsuarioNivel.CurrentValue)) {
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
<% } %>
</span><%= Usuarios.IdUsuarioNivel.CustomMsg %></td>
	</tr>
<% } %>
<% if (Usuarios.Activo.Visible) { // Activo %>
	<tr id="r_Activo"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.Activo.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Usuarios.Activo.CellAttributes %>><span id="el_Activo">
<%
selwrk = (ew_SameStr(Usuarios.Activo.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Activo" id="x_Activo" value="1"<%= selwrk %><%= Usuarios.Activo.EditAttributes %> />
</span><%= Usuarios.Activo.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("AddBtn")) %>" />
</form>
<%
Usuarios_add.ShowPageFooter();
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
