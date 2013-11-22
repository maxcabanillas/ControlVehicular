<%@ Page ClassName="TiposVehiculosadd" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="TiposVehiculosadd.aspx.cs" Inherits="TiposVehiculosadd" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var TiposVehiculos_add = new ew_Page("TiposVehiculos_add");

// page properties
TiposVehiculos_add.PageID = "add"; // page ID
TiposVehiculos_add.FormID = "fTiposVehiculosadd"; // form ID 
var EW_PAGE_ID = TiposVehiculos_add.PageID; // for backward compatibility

// extend page with ValidateForm function
TiposVehiculos_add.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_TipoVehiculo"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(TiposVehiculos.TipoVehiculo.FldCaption) %>");

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
TiposVehiculos_add.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
TiposVehiculos_add.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
TiposVehiculos_add.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Add") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= TiposVehiculos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= TiposVehiculos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% TiposVehiculos_add.ShowPageHeader(); %>
<% TiposVehiculos_add.ShowMessage(); %>
<form name="fTiposVehiculosadd" id="fTiposVehiculosadd" method="post" onsubmit="this.action=location.pathname;return TiposVehiculos_add.ValidateForm(this);">
<p />
<input type="hidden" name="t" id="t" value="TiposVehiculos" />
<input type="hidden" name="a_add" id="a_add" value="A" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (TiposVehiculos.TipoVehiculo.Visible) { // TipoVehiculo %>
	<tr id="r_TipoVehiculo"<%= TiposVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= TiposVehiculos.TipoVehiculo.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= TiposVehiculos.TipoVehiculo.CellAttributes %>><span id="el_TipoVehiculo">
<input type="text" name="x_TipoVehiculo" id="x_TipoVehiculo" size="50" maxlength="50" value="<%= TiposVehiculos.TipoVehiculo.EditValue %>"<%= TiposVehiculos.TipoVehiculo.EditAttributes %> />
</span><%= TiposVehiculos.TipoVehiculo.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("AddBtn")) %>" />
</form>
<%
TiposVehiculos_add.ShowPageFooter();
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
