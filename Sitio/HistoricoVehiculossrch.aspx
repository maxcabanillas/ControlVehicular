<%@ Page ClassName="HistoricoVehiculossrch" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="HistoricoVehiculossrch.aspx.cs" Inherits="HistoricoVehiculossrch" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var HistoricoVehiculos_search = new ew_Page("HistoricoVehiculos_search");

// page properties
HistoricoVehiculos_search.PageID = "search"; // page ID
HistoricoVehiculos_search.FormID = "fHistoricoVehiculossearch"; // form ID 
var EW_PAGE_ID = HistoricoVehiculos_search.PageID; // for backward compatibility

// extend page with validate function for search
HistoricoVehiculos_search.ValidateSearch = function(fobj) {
	ew_PostAutoSuggest(fobj); 
	if (this.ValidateRequired) { 
		var infix = "";
		elm = fobj.elements["x" + infix + "_FechaIngreso"];
		if (elm && elm.type != "hidden" && !ew_CheckEuroDate(elm.value)) // skip hidden field
			return ew_OnError(this, elm, "<%= ew_JsEncode2(HistoricoVehiculos.FechaIngreso.FldErrMsg) %>");
		elm = fobj.elements["x" + infix + "_FechaSalida"];
		if (elm && elm.type != "hidden" && !ew_CheckEuroDate(elm.value)) // skip hidden field
			return ew_OnError(this, elm, "<%= ew_JsEncode2(HistoricoVehiculos.FechaSalida.FldErrMsg) %>");

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
HistoricoVehiculos_search.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
HistoricoVehiculos_search.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
HistoricoVehiculos_search.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Search") %>&nbsp;<%= Language.Phrase("TblTypeCUSTOMVIEW") %><%= HistoricoVehiculos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= HistoricoVehiculos.ReturnUrl %>"><%= Language.Phrase("BackToList") %></a></p>
<% HistoricoVehiculos_search.ShowPageHeader(); %>
<% HistoricoVehiculos_search.ShowMessage(); %>
<form name="fHistoricoVehiculossearch" id="fHistoricoVehiculossearch" method="post" onsubmit="this.action=location.pathname;return HistoricoVehiculos_search.ValidateSearch(this);">
<p />
<input type="hidden" name="t" id="t" value="HistoricoVehiculos" />
<input type="hidden" name="a_search" id="a_search" value="S" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
	<tr id="r_TipoVehiculo"<%= HistoricoVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoVehiculos.TipoVehiculo.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_TipoVehiculo" id="z_TipoVehiculo" value="LIKE" /></td>
		<td<%= HistoricoVehiculos.TipoVehiculo.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<select id="x_TipoVehiculo" name="x_TipoVehiculo"<%= HistoricoVehiculos.TipoVehiculo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(HistoricoVehiculos.TipoVehiculo.EditValue)) {
	alwrk = (ArrayList)HistoricoVehiculos.TipoVehiculo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], HistoricoVehiculos.TipoVehiculo.AdvancedSearch.SearchValue)) {
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
	<tr id="r_Placa"<%= HistoricoVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoVehiculos.Placa.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Placa" id="z_Placa" value="LIKE" /></td>
		<td<%= HistoricoVehiculos.Placa.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_Placa" id="x_Placa" size="30" maxlength="10" value="<%= HistoricoVehiculos.Placa.EditValue %>"<%= HistoricoVehiculos.Placa.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_FechaIngreso"<%= HistoricoVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoVehiculos.FechaIngreso.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("BETWEEN") %><input type="hidden" name="z_FechaIngreso" id="z_FechaIngreso" value="BETWEEN" /></td>
		<td<%= HistoricoVehiculos.FechaIngreso.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_FechaIngreso" id="x_FechaIngreso" value="<%= HistoricoVehiculos.FechaIngreso.EditValue %>"<%= HistoricoVehiculos.FechaIngreso.EditAttributes %> />
</span>
				<span class="ewSearchCond" id="btw1_FechaIngreso" name="btw1_FechaIngreso">&nbsp;<%= Language.Phrase("AND") %>&nbsp;</span>
				<span class="aspnetmaker" id="btw1_FechaIngreso" name="btw1_FechaIngreso">
<input type="text" name="y_FechaIngreso" id="y_FechaIngreso" value="<%= HistoricoVehiculos.FechaIngreso.EditValue2 %>"<%= HistoricoVehiculos.FechaIngreso.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_FechaSalida"<%= HistoricoVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoVehiculos.FechaSalida.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("BETWEEN") %><input type="hidden" name="z_FechaSalida" id="z_FechaSalida" value="BETWEEN" /></td>
		<td<%= HistoricoVehiculos.FechaSalida.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_FechaSalida" id="x_FechaSalida" value="<%= HistoricoVehiculos.FechaSalida.EditValue %>"<%= HistoricoVehiculos.FechaSalida.EditAttributes %> />
</span>
				<span class="ewSearchCond" id="btw1_FechaSalida" name="btw1_FechaSalida">&nbsp;<%= Language.Phrase("AND") %>&nbsp;</span>
				<span class="aspnetmaker" id="btw1_FechaSalida" name="btw1_FechaSalida">
<input type="text" name="y_FechaSalida" id="y_FechaSalida" value="<%= HistoricoVehiculos.FechaSalida.EditValue2 %>"<%= HistoricoVehiculos.FechaSalida.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_Observaciones"<%= HistoricoVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= HistoricoVehiculos.Observaciones.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Observaciones" id="z_Observaciones" value="LIKE" /></td>
		<td<%= HistoricoVehiculos.Observaciones.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<textarea name="x_Observaciones" id="x_Observaciones" cols="35" rows="4"<%= HistoricoVehiculos.Observaciones.EditAttributes %>><%= HistoricoVehiculos.Observaciones.EditValue %></textarea>
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
HistoricoVehiculos_search.ShowPageFooter();
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
