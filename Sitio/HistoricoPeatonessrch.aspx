<%@ Page ClassName="HistoricoPeatonessrch" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="HistoricoPeatonessrch.aspx.cs" Inherits="HistoricoPeatonessrch" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var HistoricoPeatones_search = new ew_Page("HistoricoPeatones_search");

// page properties
HistoricoPeatones_search.PageID = "search"; // page ID
HistoricoPeatones_search.FormID = "fHistoricoPeatonessearch"; // form ID 
var EW_PAGE_ID = HistoricoPeatones_search.PageID; // for backward compatibility

// extend page with validate function for search
HistoricoPeatones_search.ValidateSearch = function(fobj) {
	ew_PostAutoSuggest(fobj); 
	if (this.ValidateRequired) { 
		var infix = "";
		elm = fobj.elements["x" + infix + "_FechaIngreso"];
		if (elm && elm.type != "hidden" && !ew_CheckEuroDate(elm.value)) // skip hidden field
			return ew_OnError(this, elm, "<%= ew_JsEncode2(HistoricoPeatones.FechaIngreso.FldErrMsg) %>");
		elm = fobj.elements["x" + infix + "_FechaSalida"];
		if (elm && elm.type != "hidden" && !ew_CheckEuroDate(elm.value)) // skip hidden field
			return ew_OnError(this, elm, "<%= ew_JsEncode2(HistoricoPeatones.FechaSalida.FldErrMsg) %>");

		// Form Custom Validate event
		if (!this.Form_CustomValidate(fobj)) return false;
	} 
	for (var i=0;i<fobj.elements.length;i++) {
		var elem = fobj.elements[i];
		if (elem.name.substring(0,2) == "s_" || elem.name.substring(0,3) == "sv_")
			elem.value = "";
	}
	return true;
}

// extend page with Form_CustomValidate function
HistoricoPeatones_search.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
HistoricoPeatones_search.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
HistoricoPeatones_search.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Search") %>&nbsp;<%= Language.Phrase("TblTypeCUSTOMVIEW") %><%= HistoricoPeatones.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= HistoricoPeatones.ReturnUrl %>"><%= Language.Phrase("BackToList") %></a></p>
<% HistoricoPeatones_search.ShowPageHeader(); %>
<% HistoricoPeatones_search.ShowMessage(); %>
<form name="fHistoricoPeatonessearch" id="fHistoricoPeatonessearch" method="post" onsubmit="this.action=location.pathname;return HistoricoPeatones_search.ValidateSearch(this);">
<p />
<input type="hidden" name="t" id="t" value="HistoricoPeatones" />
<input type="hidden" name="a_search" id="a_search" value="S" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
	<tr id="r_TipoDocumento"<%= HistoricoPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoPeatones.TipoDocumento.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_TipoDocumento" id="z_TipoDocumento" value="LIKE" /></td>
		<td<%= HistoricoPeatones.TipoDocumento.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<select id="x_TipoDocumento" name="x_TipoDocumento"<%= HistoricoPeatones.TipoDocumento.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(HistoricoPeatones.TipoDocumento.EditValue)) {
	alwrk = (ArrayList)HistoricoPeatones.TipoDocumento.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], HistoricoPeatones.TipoDocumento.AdvancedSearch.SearchValue)) {
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
</span>
			</div>
		</td>
	</tr>
	<tr id="r_Documento"<%= HistoricoPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoPeatones.Documento.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Documento" id="z_Documento" value="LIKE" /></td>
		<td<%= HistoricoPeatones.Documento.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_Documento" id="x_Documento" size="30" maxlength="15" value="<%= HistoricoPeatones.Documento.EditValue %>"<%= HistoricoPeatones.Documento.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_Nombre"<%= HistoricoPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoPeatones.Nombre.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Nombre" id="z_Nombre" value="LIKE" /></td>
		<td<%= HistoricoPeatones.Nombre.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_Nombre" id="x_Nombre" size="30" maxlength="50" value="<%= HistoricoPeatones.Nombre.EditValue %>"<%= HistoricoPeatones.Nombre.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_Apellidos"<%= HistoricoPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoPeatones.Apellidos.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Apellidos" id="z_Apellidos" value="LIKE" /></td>
		<td<%= HistoricoPeatones.Apellidos.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_Apellidos" id="x_Apellidos" size="30" maxlength="50" value="<%= HistoricoPeatones.Apellidos.EditValue %>"<%= HistoricoPeatones.Apellidos.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_Area"<%= HistoricoPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoPeatones.Area.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Area" id="z_Area" value="LIKE" /></td>
		<td<%= HistoricoPeatones.Area.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<select id="x_Area" name="x_Area"<%= HistoricoPeatones.Area.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(HistoricoPeatones.Area.EditValue)) {
	alwrk = (ArrayList)HistoricoPeatones.Area.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], HistoricoPeatones.Area.AdvancedSearch.SearchValue)) {
			selwrk = " selected=\"selected\"";
			emptywrk = false;
		} else {
			selwrk = "";
		}
%>
<option value="<%= ew_HtmlEncode(odwrk[0]) %>"<%= selwrk %>>
<%= odwrk[1] %><% if (ew_NotEmpty(odwrk[2])) { %><%= ew_ValueSeparator(rowcntwrk,1,HistoricoPeatones.Area) %><%= odwrk[2] %><% } %>
</option>
<%
	}
}
%>
</select>
</span>
			</div>
		</td>
	</tr>
	<tr id="r_Persona"<%= HistoricoPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoPeatones.Persona.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Persona" id="z_Persona" value="LIKE" /></td>
		<td<%= HistoricoPeatones.Persona.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<select id="x_Persona" name="x_Persona"<%= HistoricoPeatones.Persona.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(HistoricoPeatones.Persona.EditValue)) {
	alwrk = (ArrayList)HistoricoPeatones.Persona.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], HistoricoPeatones.Persona.AdvancedSearch.SearchValue)) {
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
</span>
			</div>
		</td>
	</tr>
	<tr id="r_FechaIngreso"<%= HistoricoPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoPeatones.FechaIngreso.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("BETWEEN") %><input type="hidden" name="z_FechaIngreso" id="z_FechaIngreso" value="BETWEEN" /></td>
		<td<%= HistoricoPeatones.FechaIngreso.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_FechaIngreso" id="x_FechaIngreso" value="<%= HistoricoPeatones.FechaIngreso.EditValue %>"<%= HistoricoPeatones.FechaIngreso.EditAttributes %> />
</span>
				<span class="ewSearchCond" id="btw1_FechaIngreso" name="btw1_FechaIngreso">&nbsp;<%= Language.Phrase("AND") %>&nbsp;</span>
				<span class="aspnetmaker" id="btw1_FechaIngreso" name="btw1_FechaIngreso">
<input type="text" name="y_FechaIngreso" id="y_FechaIngreso" value="<%= HistoricoPeatones.FechaIngreso.EditValue2 %>"<%= HistoricoPeatones.FechaIngreso.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_FechaSalida"<%= HistoricoPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoPeatones.FechaSalida.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("BETWEEN") %><input type="hidden" name="z_FechaSalida" id="z_FechaSalida" value="BETWEEN" /></td>
		<td<%= HistoricoPeatones.FechaSalida.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_FechaSalida" id="x_FechaSalida" value="<%= HistoricoPeatones.FechaSalida.EditValue %>"<%= HistoricoPeatones.FechaSalida.EditAttributes %> />
</span>
				<span class="ewSearchCond" id="btw1_FechaSalida" name="btw1_FechaSalida">&nbsp;<%= Language.Phrase("AND") %>&nbsp;</span>
				<span class="aspnetmaker" id="btw1_FechaSalida" name="btw1_FechaSalida">
<input type="text" name="y_FechaSalida" id="y_FechaSalida" value="<%= HistoricoPeatones.FechaSalida.EditValue2 %>"<%= HistoricoPeatones.FechaSalida.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_Observacion"<%= HistoricoPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoPeatones.Observacion.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Observacion" id="z_Observacion" value="LIKE" /></td>
		<td<%= HistoricoPeatones.Observacion.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<textarea name="x_Observacion" id="x_Observacion" cols="35" rows="4"<%= HistoricoPeatones.Observacion.EditAttributes %>><%= HistoricoPeatones.Observacion.EditValue %></textarea>
</span>
			</div>
		</td>
	</tr>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("Search")) %>" />
<input type="button" name="Reset" id="Reset" value="<%= ew_BtnCaption(Language.Phrase("Reset")) %>" onclick="ew_ClearForm(this.form);" />
</form>
<%
HistoricoPeatones_search.ShowPageFooter();
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
