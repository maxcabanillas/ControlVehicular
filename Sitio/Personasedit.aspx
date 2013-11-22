<%@ Page ClassName="Personasedit" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Personasedit.aspx.cs" Inherits="Personasedit" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var Personas_edit = new ew_Page("Personas_edit");

// page properties
Personas_edit.PageID = "edit"; // page ID
Personas_edit.FormID = "fPersonasedit"; // form ID 
var EW_PAGE_ID = Personas_edit.PageID; // for backward compatibility

// extend page with ValidateForm function
Personas_edit.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
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
	}

	// Process detail page
	var detailpage = (fobj.detailpage) ? fobj.detailpage.value : "";
	if (detailpage != "") {
		return eval(detailpage+".ValidateForm(fobj)");
	}
	return true;
}

// extend page with Form_CustomValidate function
Personas_edit.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Personas_edit.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Personas_edit.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Edit") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Personas.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= Personas.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% Personas_edit.ShowPageHeader(); %>
<% Personas_edit.ShowMessage(); %>
<form name="fPersonasedit" id="fPersonasedit" method="post" onsubmit="this.action=location.pathname;return Personas_edit.ValidateForm(this);">
<p />
<input type="hidden" name="a_table" id="a_table" value="Personas" />
<input type="hidden" name="a_edit" id="a_edit" value="U" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (Personas.IdPersona.Visible) { // IdPersona %>
	<tr id="r_IdPersona"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.IdPersona.FldCaption %></td>
		<td<%= Personas.IdPersona.CellAttributes %>><span id="el_IdPersona">
<div<%= Personas.IdPersona.ViewAttributes %>><%= Personas.IdPersona.EditValue %></div>
<input type="hidden" name="x_IdPersona" id="x_IdPersona" value="<%= ew_HtmlEncode(Personas.IdPersona.CurrentValue) %>" />
</span><%= Personas.IdPersona.CustomMsg %></td>
	</tr>
<% } %>
<% if (Personas.IdArea.Visible) { // IdArea %>
	<tr id="r_IdArea"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.IdArea.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Personas.IdArea.CellAttributes %>><span id="el_IdArea">
<% if (ew_NotEmpty(Personas.IdArea.SessionValue)) { %>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ViewValue %></div>
<input type="hidden" id="x_IdArea" name="x_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.CurrentValue) %>">
<% } else { %>
<select id="x_IdArea" name="x_IdArea"<%= Personas.IdArea.EditAttributes %>>
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
<%= odwrk[1] %><% if (ew_NotEmpty(odwrk[2])) { %><%= ew_ValueSeparator(rowcntwrk,1,Personas.IdArea) %><%= odwrk[2] %><% } %>
</option>
<%
	}
}
%>
</select>
<% } %>
</span><%= Personas.IdArea.CustomMsg %></td>
	</tr>
<% } %>
<% if (Personas.IdCargo.Visible) { // IdCargo %>
	<tr id="r_IdCargo"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.IdCargo.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Personas.IdCargo.CellAttributes %>><span id="el_IdCargo">
<select id="x_IdCargo" name="x_IdCargo"<%= Personas.IdCargo.EditAttributes %>>
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
%>
</select>
</span><%= Personas.IdCargo.CustomMsg %></td>
	</tr>
<% } %>
<% if (Personas.Documento.Visible) { // Documento %>
	<tr id="r_Documento"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.Documento.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Personas.Documento.CellAttributes %>><span id="el_Documento">
<input type="text" name="x_Documento" id="x_Documento" size="30" maxlength="50" value="<%= Personas.Documento.EditValue %>"<%= Personas.Documento.EditAttributes %> />
</span><%= Personas.Documento.CustomMsg %></td>
	</tr>
<% } %>
<% if (Personas.Persona.Visible) { // Persona %>
	<tr id="r_Persona"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.Persona.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Personas.Persona.CellAttributes %>><span id="el_Persona">
<input type="text" name="x_Persona" id="x_Persona" size="50" maxlength="50" value="<%= Personas.Persona.EditValue %>"<%= Personas.Persona.EditAttributes %> />
</span><%= Personas.Persona.CustomMsg %></td>
	</tr>
<% } %>
<% if (Personas.Activa.Visible) { // Activa %>
	<tr id="r_Activa"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.Activa.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Personas.Activa.CellAttributes %>><span id="el_Activa">
<%
selwrk = (ew_SameStr(Personas.Activa.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Activa" id="x_Activa" value="1"<%= selwrk %><%= Personas.Activa.EditAttributes %> />
</span><%= Personas.Activa.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%

// Begin detail grid for "VehiculosAutorizados"
if (Personas.CurrentDetailTable == "VehiculosAutorizados" && VehiculosAutorizados.DetailEdit) {
%>
<br />
<%
string dtlparms = VehiculosAutorizados.DetailParms; // ASPX
dtlparms += "&confirmpage=" + ((Personas_edit.ConfirmPage) ? "1" : "0");
dtlparms += "&IdPersona=" + ew_UrlEncode(VehiculosAutorizados.IdPersona.CurrentValue);
dtlparms += "&mastereventcancelled=" + ((Personas.EventCancelled) ? "1" : "0");
Server.Execute("VehiculosAutorizadosgrid.aspx?" + dtlparms);
%>
<br />
<%
}

// End detail grid for "VehiculosAutorizados"
%>
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("EditBtn")) %>" />
</form>
<%
Personas_edit.ShowPageFooter();
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
