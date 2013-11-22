<%@ Page ClassName="Cargosedit" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Cargosedit.aspx.cs" Inherits="Cargosedit" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var Cargos_edit = new ew_Page("Cargos_edit");

// page properties
Cargos_edit.PageID = "edit"; // page ID
Cargos_edit.FormID = "fCargosedit"; // form ID 
var EW_PAGE_ID = Cargos_edit.PageID; // for backward compatibility

// extend page with ValidateForm function
Cargos_edit.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_Cargo"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Cargos.Cargo.FldCaption) %>");

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
Cargos_edit.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Cargos_edit.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Cargos_edit.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Edit") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Cargos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= Cargos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% Cargos_edit.ShowPageHeader(); %>
<% Cargos_edit.ShowMessage(); %>
<form name="fCargosedit" id="fCargosedit" method="post" onsubmit="this.action=location.pathname;return Cargos_edit.ValidateForm(this);">
<p />
<input type="hidden" name="a_table" id="a_table" value="Cargos" />
<input type="hidden" name="a_edit" id="a_edit" value="U" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (Cargos.IdCargo.Visible) { // IdCargo %>
	<tr id="r_IdCargo"<%= Cargos.RowAttributes %>>
		<td class="ewTableHeader"><%= Cargos.IdCargo.FldCaption %></td>
		<td<%= Cargos.IdCargo.CellAttributes %>><span id="el_IdCargo">
<div<%= Cargos.IdCargo.ViewAttributes %>><%= Cargos.IdCargo.EditValue %></div>
<input type="hidden" name="x_IdCargo" id="x_IdCargo" value="<%= ew_HtmlEncode(Cargos.IdCargo.CurrentValue) %>" />
</span><%= Cargos.IdCargo.CustomMsg %></td>
	</tr>
<% } %>
<% if (Cargos.Cargo.Visible) { // Cargo %>
	<tr id="r_Cargo"<%= Cargos.RowAttributes %>>
		<td class="ewTableHeader"><%= Cargos.Cargo.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Cargos.Cargo.CellAttributes %>><span id="el_Cargo">
<input type="text" name="x_Cargo" id="x_Cargo" size="50" maxlength="50" value="<%= Cargos.Cargo.EditValue %>"<%= Cargos.Cargo.EditAttributes %> />
</span><%= Cargos.Cargo.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("EditBtn")) %>" />
</form>
<%
Cargos_edit.ShowPageFooter();
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
