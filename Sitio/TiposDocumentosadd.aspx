<%@ Page ClassName="TiposDocumentosadd" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="TiposDocumentosadd.aspx.cs" Inherits="TiposDocumentosadd" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var TiposDocumentos_add = new ew_Page("TiposDocumentos_add");

// page properties
TiposDocumentos_add.PageID = "add"; // page ID
TiposDocumentos_add.FormID = "fTiposDocumentosadd"; // form ID 
var EW_PAGE_ID = TiposDocumentos_add.PageID; // for backward compatibility

// extend page with ValidateForm function
TiposDocumentos_add.ValidateForm = function(fobj) {
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
TiposDocumentos_add.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
TiposDocumentos_add.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
TiposDocumentos_add.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Add") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= TiposDocumentos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= TiposDocumentos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% TiposDocumentos_add.ShowPageHeader(); %>
<% TiposDocumentos_add.ShowMessage(); %>
<form name="fTiposDocumentosadd" id="fTiposDocumentosadd" method="post" onsubmit="this.action=location.pathname;return TiposDocumentos_add.ValidateForm(this);">
<p />
<input type="hidden" name="t" id="t" value="TiposDocumentos" />
<input type="hidden" name="a_add" id="a_add" value="A" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
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
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("AddBtn")) %>" />
</form>
<%
TiposDocumentos_add.ShowPageFooter();
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
