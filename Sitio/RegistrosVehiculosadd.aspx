<%@ Page ClassName="RegistrosVehiculosadd" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="RegistrosVehiculosadd.aspx.cs" Inherits="RegistrosVehiculosadd" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var RegistrosVehiculos_add = new ew_Page("RegistrosVehiculos_add");

// page properties
RegistrosVehiculos_add.PageID = "add"; // page ID
RegistrosVehiculos_add.FormID = "fRegistrosVehiculosadd"; // form ID 
var EW_PAGE_ID = RegistrosVehiculos_add.PageID; // for backward compatibility

// extend page with ValidateForm function
RegistrosVehiculos_add.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_Placa"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(RegistrosVehiculos.Placa.FldCaption) %>");

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
RegistrosVehiculos_add.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosVehiculos_add.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosVehiculos_add.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Add") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= RegistrosVehiculos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= RegistrosVehiculos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% RegistrosVehiculos_add.ShowPageHeader(); %>
<% RegistrosVehiculos_add.ShowMessage(); %>
<form name="fRegistrosVehiculosadd" id="fRegistrosVehiculosadd" method="post" onsubmit="this.action=location.pathname;return RegistrosVehiculos_add.ValidateForm(this);">
<p />
<input type="hidden" name="t" id="t" value="RegistrosVehiculos" />
<input type="hidden" name="a_add" id="a_add" value="A" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (RegistrosVehiculos.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
	<tr id="r_IdTipoVehiculo"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.IdTipoVehiculo.FldCaption %></td>
		<td<%= RegistrosVehiculos.IdTipoVehiculo.CellAttributes %>><span id="el_IdTipoVehiculo">
<select id="x_IdTipoVehiculo" name="x_IdTipoVehiculo"<%= RegistrosVehiculos.IdTipoVehiculo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(RegistrosVehiculos.IdTipoVehiculo.EditValue)) {
	alwrk = (ArrayList)RegistrosVehiculos.IdTipoVehiculo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], RegistrosVehiculos.IdTipoVehiculo.CurrentValue)) {
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
</span><%= RegistrosVehiculos.IdTipoVehiculo.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.Placa.Visible) { // Placa %>
	<tr id="r_Placa"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.Placa.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= RegistrosVehiculos.Placa.CellAttributes %>><span id="el_Placa">
<input type="text" name="x_Placa" id="x_Placa" size="10" maxlength="10" value="<%= RegistrosVehiculos.Placa.EditValue %>"<%= RegistrosVehiculos.Placa.EditAttributes %> />
</span><%= RegistrosVehiculos.Placa.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.Observaciones.Visible) { // Observaciones %>
	<tr id="r_Observaciones"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.Observaciones.FldCaption %></td>
		<td<%= RegistrosVehiculos.Observaciones.CellAttributes %>><span id="el_Observaciones">
<textarea name="x_Observaciones" id="x_Observaciones" cols="35" rows="4"<%= RegistrosVehiculos.Observaciones.EditAttributes %>><%= RegistrosVehiculos.Observaciones.EditValue %></textarea>
</span><%= RegistrosVehiculos.Observaciones.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("AddBtn")) %>" />
</form>
<%
RegistrosVehiculos_add.ShowPageFooter();
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
