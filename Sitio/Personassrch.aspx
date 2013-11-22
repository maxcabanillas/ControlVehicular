<%@ Page ClassName="Personassrch" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Personassrch.aspx.cs" Inherits="Personassrch" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var Personas_search = new ew_Page("Personas_search");

// page properties
Personas_search.PageID = "search"; // page ID
Personas_search.FormID = "fPersonassearch"; // form ID 
var EW_PAGE_ID = Personas_search.PageID; // for backward compatibility

// extend page with validate function for search
Personas_search.ValidateSearch = function(fobj) {
	ew_PostAutoSuggest(fobj); 
	if (this.ValidateRequired) { 
		var infix = "";
		elm = fobj.elements["x" + infix + "_IdPersona"];
		if (elm && elm.type != "hidden" && !ew_CheckInteger(elm.value)) // skip hidden field
			return ew_OnError(this, elm, "<%= ew_JsEncode2(Personas.IdPersona.FldErrMsg) %>");

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
Personas_search.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Personas_search.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Personas_search.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Search") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Personas.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= Personas.ReturnUrl %>"><%= Language.Phrase("BackToList") %></a></p>
<% Personas_search.ShowPageHeader(); %>
<% Personas_search.ShowMessage(); %>
<form name="fPersonassearch" id="fPersonassearch" method="post" onsubmit="this.action=location.pathname;return Personas_search.ValidateSearch(this);">
<p />
<input type="hidden" name="t" id="t" value="Personas" />
<input type="hidden" name="a_search" id="a_search" value="S" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
	<tr id="r_IdPersona"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.IdPersona.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("=") %><input type="hidden" name="z_IdPersona" id="z_IdPersona" value="=" /></td>
		<td<%= Personas.IdPersona.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_IdPersona" id="x_IdPersona" value="<%= Personas.IdPersona.EditValue %>"<%= Personas.IdPersona.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_IdArea"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.IdArea.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("=") %><input type="hidden" name="z_IdArea" id="z_IdArea" value="=" /></td>
		<td<%= Personas.IdArea.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<select id="x_IdArea" name="x_IdArea"<%= Personas.IdArea.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdArea.EditValue)) {
	alwrk = (ArrayList)Personas.IdArea.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdArea.AdvancedSearch.SearchValue)) {
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
</span>
			</div>
		</td>
	</tr>
	<tr id="r_IdCargo"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.IdCargo.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("=") %><input type="hidden" name="z_IdCargo" id="z_IdCargo" value="=" /></td>
		<td<%= Personas.IdCargo.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<select id="x_IdCargo" name="x_IdCargo"<%= Personas.IdCargo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdCargo.EditValue)) {
	alwrk = (ArrayList)Personas.IdCargo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdCargo.AdvancedSearch.SearchValue)) {
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
	<tr id="r_Documento"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.Documento.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Documento" id="z_Documento" value="LIKE" /></td>
		<td<%= Personas.Documento.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_Documento" id="x_Documento" size="30" maxlength="50" value="<%= Personas.Documento.EditValue %>"<%= Personas.Documento.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_Persona"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.Persona.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("LIKE") %><input type="hidden" name="z_Persona" id="z_Persona" value="LIKE" /></td>
		<td<%= Personas.Persona.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<input type="text" name="x_Persona" id="x_Persona" size="50" maxlength="50" value="<%= Personas.Persona.EditValue %>"<%= Personas.Persona.EditAttributes %> />
</span>
			</div>
		</td>
	</tr>
	<tr id="r_Activa"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.Activa.FldCaption %></td>
		<td class="ewSearchOprCell"><%= Language.Phrase("=") %><input type="hidden" name="z_Activa" id="z_Activa" value="=" /></td>
		<td<%= Personas.Activa.CellAttributes %>>
			<div style="white-space: nowrap;">
				<span class="aspnetmaker">
<%
selwrk = (ew_SameStr(Personas.Activa.AdvancedSearch.SearchValue, "1")) ? " checked=\"checked\"" : "";
%>
<input type="checkbox" name="x_Activa" id="x_Activa" value="1"<%= selwrk %><%= Personas.Activa.EditAttributes %> />
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
Personas_search.ShowPageFooter();
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
