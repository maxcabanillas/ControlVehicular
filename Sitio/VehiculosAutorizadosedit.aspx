<%@ Page ClassName="VehiculosAutorizadosedit" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="VehiculosAutorizadosedit.aspx.cs" Inherits="VehiculosAutorizadosedit" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var VehiculosAutorizados_edit = new ew_Page("VehiculosAutorizados_edit");

// page properties
VehiculosAutorizados_edit.PageID = "edit"; // page ID
VehiculosAutorizados_edit.FormID = "fVehiculosAutorizadosedit"; // form ID 
var EW_PAGE_ID = VehiculosAutorizados_edit.PageID; // for backward compatibility

// extend page with ValidateForm function
VehiculosAutorizados_edit.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_IdTipoVehiculo"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.IdTipoVehiculo.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Placa"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Placa.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_Autorizado"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.Autorizado.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_IdPersona"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(VehiculosAutorizados.IdPersona.FldCaption) %>");
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
	}

	// Process detail page
	var detailpage = (fobj.detailpage) ? fobj.detailpage.value : "";
	if (detailpage != "") {
		return eval(detailpage+".ValidateForm(fobj)");
	}
	return true;
}

// extend page with Form_CustomValidate function
VehiculosAutorizados_edit.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
VehiculosAutorizados_edit.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
VehiculosAutorizados_edit.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Edit") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= VehiculosAutorizados.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= VehiculosAutorizados.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% VehiculosAutorizados_edit.ShowPageHeader(); %>
<% VehiculosAutorizados_edit.ShowMessage(); %>
<form name="fVehiculosAutorizadosedit" id="fVehiculosAutorizadosedit" method="post" onsubmit="this.action=location.pathname;return VehiculosAutorizados_edit.ValidateForm(this);">
<p />
<input type="hidden" name="a_table" id="a_table" value="VehiculosAutorizados" />
<input type="hidden" name="a_edit" id="a_edit" value="U" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // IdVehiculoAutorizado %>
	<tr id="r_IdVehiculoAutorizado"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.IdVehiculoAutorizado.FldCaption %></td>
		<td<%= VehiculosAutorizados.IdVehiculoAutorizado.CellAttributes %>><span id="el_IdVehiculoAutorizado">
<div<%= VehiculosAutorizados.IdVehiculoAutorizado.ViewAttributes %>><%= VehiculosAutorizados.IdVehiculoAutorizado.EditValue %></div>
<input type="hidden" name="x_IdVehiculoAutorizado" id="x_IdVehiculoAutorizado" value="<%= ew_HtmlEncode(VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue) %>" />
</span><%= VehiculosAutorizados.IdVehiculoAutorizado.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
	<tr id="r_IdTipoVehiculo"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.IdTipoVehiculo.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.IdTipoVehiculo.CellAttributes %>><span id="el_IdTipoVehiculo">
<select id="x_IdTipoVehiculo" name="x_IdTipoVehiculo"<%= VehiculosAutorizados.IdTipoVehiculo.EditAttributes %>>
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
%>
</select>
</span><%= VehiculosAutorizados.IdTipoVehiculo.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Placa.Visible) { // Placa %>
	<tr id="r_Placa"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Placa.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Placa.CellAttributes %>><span id="el_Placa">
<input type="text" name="x_Placa" id="x_Placa" size="10" maxlength="10" value="<%= VehiculosAutorizados.Placa.EditValue %>"<%= VehiculosAutorizados.Placa.EditAttributes %> />
</span><%= VehiculosAutorizados.Placa.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Autorizado.Visible) { // Autorizado %>
	<tr id="r_Autorizado"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Autorizado.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Autorizado.CellAttributes %>><span id="el_Autorizado">
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Autorizado.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Autorizado" id="x_Autorizado" value="1"<%= selwrk %><%= VehiculosAutorizados.Autorizado.EditAttributes %> />
</span><%= VehiculosAutorizados.Autorizado.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.IdPersona.Visible) { // IdPersona %>
	<tr id="r_IdPersona"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.IdPersona.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.IdPersona.CellAttributes %>><span id="el_IdPersona">
<% if (ew_NotEmpty(VehiculosAutorizados.IdPersona.SessionValue)) { %>
<div<%= VehiculosAutorizados.IdPersona.ViewAttributes %>><%= VehiculosAutorizados.IdPersona.ViewValue %></div>
<input type="hidden" id="x_IdPersona" name="x_IdPersona" value="<%= ew_HtmlEncode(VehiculosAutorizados.IdPersona.CurrentValue) %>">
<% } else { %>
<select id="x_IdPersona" name="x_IdPersona"<%= VehiculosAutorizados.IdPersona.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(VehiculosAutorizados.IdPersona.EditValue)) {
	alwrk = (ArrayList)VehiculosAutorizados.IdPersona.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], VehiculosAutorizados.IdPersona.CurrentValue)) {
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
</span><%= VehiculosAutorizados.IdPersona.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.PicoyPlaca.Visible) { // PicoyPlaca %>
	<tr id="r_PicoyPlaca"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.PicoyPlaca.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.PicoyPlaca.CellAttributes %>><span id="el_PicoyPlaca">
<%
selwrk = (ew_SameStr(VehiculosAutorizados.PicoyPlaca.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_PicoyPlaca" id="x_PicoyPlaca" value="1"<%= selwrk %><%= VehiculosAutorizados.PicoyPlaca.EditAttributes %> />
</span><%= VehiculosAutorizados.PicoyPlaca.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Lunes.Visible) { // Lunes %>
	<tr id="r_Lunes"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Lunes.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Lunes.CellAttributes %>><span id="el_Lunes">
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Lunes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Lunes" id="x_Lunes" value="1"<%= selwrk %><%= VehiculosAutorizados.Lunes.EditAttributes %> />
</span><%= VehiculosAutorizados.Lunes.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Martes.Visible) { // Martes %>
	<tr id="r_Martes"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Martes.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Martes.CellAttributes %>><span id="el_Martes">
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Martes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Martes" id="x_Martes" value="1"<%= selwrk %><%= VehiculosAutorizados.Martes.EditAttributes %> />
</span><%= VehiculosAutorizados.Martes.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Miercoles.Visible) { // Miercoles %>
	<tr id="r_Miercoles"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Miercoles.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Miercoles.CellAttributes %>><span id="el_Miercoles">
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Miercoles.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Miercoles" id="x_Miercoles" value="1"<%= selwrk %><%= VehiculosAutorizados.Miercoles.EditAttributes %> />
</span><%= VehiculosAutorizados.Miercoles.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Jueves.Visible) { // Jueves %>
	<tr id="r_Jueves"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Jueves.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Jueves.CellAttributes %>><span id="el_Jueves">
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Jueves.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Jueves" id="x_Jueves" value="1"<%= selwrk %><%= VehiculosAutorizados.Jueves.EditAttributes %> />
</span><%= VehiculosAutorizados.Jueves.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Viernes.Visible) { // Viernes %>
	<tr id="r_Viernes"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Viernes.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Viernes.CellAttributes %>><span id="el_Viernes">
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Viernes.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Viernes" id="x_Viernes" value="1"<%= selwrk %><%= VehiculosAutorizados.Viernes.EditAttributes %> />
</span><%= VehiculosAutorizados.Viernes.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Sabado.Visible) { // Sabado %>
	<tr id="r_Sabado"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Sabado.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Sabado.CellAttributes %>><span id="el_Sabado">
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Sabado.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Sabado" id="x_Sabado" value="1"<%= selwrk %><%= VehiculosAutorizados.Sabado.EditAttributes %> />
</span><%= VehiculosAutorizados.Sabado.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Domingo.Visible) { // Domingo %>
	<tr id="r_Domingo"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Domingo.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Domingo.CellAttributes %>><span id="el_Domingo">
<%
selwrk = (ew_SameStr(VehiculosAutorizados.Domingo.CurrentValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Domingo" id="x_Domingo" value="1"<%= selwrk %><%= VehiculosAutorizados.Domingo.EditAttributes %> />
</span><%= VehiculosAutorizados.Domingo.CustomMsg %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Marca.Visible) { // Marca %>
	<tr id="r_Marca"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Marca.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= VehiculosAutorizados.Marca.CellAttributes %>><span id="el_Marca">
<input type="text" name="x_Marca" id="x_Marca" size="30" maxlength="50" value="<%= VehiculosAutorizados.Marca.EditValue %>"<%= VehiculosAutorizados.Marca.EditAttributes %> />
</span><%= VehiculosAutorizados.Marca.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%

// Begin detail grid for "VehiculosPicoYPlacaHoras"
if (VehiculosAutorizados.CurrentDetailTable == "VehiculosPicoYPlacaHoras" && VehiculosPicoYPlacaHoras.DetailEdit) {
%>
<br />
<%
string dtlparms = VehiculosPicoYPlacaHoras.DetailParms; // ASPX
dtlparms += "&confirmpage=" + ((VehiculosAutorizados_edit.ConfirmPage) ? "1" : "0");
dtlparms += "&IdVehiculoAutorizado=" + ew_UrlEncode(VehiculosPicoYPlacaHoras.IdVehiculoAutorizado.CurrentValue);
dtlparms += "&mastereventcancelled=" + ((VehiculosAutorizados.EventCancelled) ? "1" : "0");
Server.Execute("VehiculosPicoYPlacaHorasgrid.aspx?" + dtlparms);
%>
<br />
<%
}

// End detail grid for "VehiculosPicoYPlacaHoras"
%>
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("EditBtn")) %>" />
</form>
<%
VehiculosAutorizados_edit.ShowPageFooter();
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
