<%@ Page ClassName="TiposDocumentosedit" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="TiposDocumentosedit.aspx.cs" Inherits="TiposDocumentosedit" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var TiposDocumentos_edit = new ew_Page("TiposDocumentos_edit");

// page properties
TiposDocumentos_edit.PageID = "edit"; // page ID
TiposDocumentos_edit.FormID = "fTiposDocumentosedit"; // form ID 
var EW_PAGE_ID = TiposDocumentos_edit.PageID; // for backward compatibility

// extend page with ValidateForm function
TiposDocumentos_edit.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_TipoDocumento"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(TiposDocumentos.TipoDocumento.FldCaption) %>");

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
TiposDocumentos_edit.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
TiposDocumentos_edit.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
TiposDocumentos_edit.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Edit") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= TiposDocumentos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= TiposDocumentos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% TiposDocumentos_edit.ShowPageHeader(); %>
<% TiposDocumentos_edit.ShowMessage(); %>
<form name="fTiposDocumentosedit" id="fTiposDocumentosedit" method="post" onsubmit="this.action=location.pathname;return TiposDocumentos_edit.ValidateForm(this);">
<p />
<input type="hidden" name="a_table" id="a_table" value="TiposDocumentos" />
<input type="hidden" name="a_edit" id="a_edit" value="U" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (TiposDocumentos.IdTipoDocumento.Visible) { // IdTipoDocumento %>
	<tr id="r_IdTipoDocumento"<%= TiposDocumentos.RowAttributes %>>
		<td class="ewTableHeader"><%= TiposDocumentos.IdTipoDocumento.FldCaption %></td>
		<td<%= TiposDocumentos.IdTipoDocumento.CellAttributes %>><span id="el_IdTipoDocumento">
<div<%= TiposDocumentos.IdTipoDocumento.ViewAttributes %>><%= TiposDocumentos.IdTipoDocumento.EditValue %></div>
<input type="hidden" name="x_IdTipoDocumento" id="x_IdTipoDocumento" value="<%= ew_HtmlEncode(TiposDocumentos.IdTipoDocumento.CurrentValue) %>" />
</span><%= TiposDocumentos.IdTipoDocumento.CustomMsg %></td>
	</tr>
<% } %>
<% if (TiposDocumentos.TipoDocumento.Visible) { // TipoDocumento %>
	<tr id="r_TipoDocumento"<%= TiposDocumentos.RowAttributes %>>
		<td class="ewTableHeader"><%= TiposDocumentos.TipoDocumento.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= TiposDocumentos.TipoDocumento.CellAttributes %>><span id="el_TipoDocumento">
<input type="text" name="x_TipoDocumento" id="x_TipoDocumento" size="50" maxlength="50" value="<%= TiposDocumentos.TipoDocumento.EditValue %>"<%= TiposDocumentos.TipoDocumento.EditAttributes %> />
</span><%= TiposDocumentos.TipoDocumento.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("EditBtn")) %>" />
</form>
<%
TiposDocumentos_edit.ShowPageFooter();
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
