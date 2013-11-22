<%@ Page ClassName="Areasedit" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Areasedit.aspx.cs" Inherits="Areasedit" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var Areas_edit = new ew_Page("Areas_edit");

// page properties
Areas_edit.PageID = "edit"; // page ID
Areas_edit.FormID = "fAreasedit"; // form ID 
var EW_PAGE_ID = Areas_edit.PageID; // for backward compatibility

// extend page with ValidateForm function
Areas_edit.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_Area"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(Areas.Area.FldCaption) %>");

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
Areas_edit.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Areas_edit.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Areas_edit.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Edit") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Areas.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= Areas.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% Areas_edit.ShowPageHeader(); %>
<% Areas_edit.ShowMessage(); %>
<form name="fAreasedit" id="fAreasedit" method="post" onsubmit="this.action=location.pathname;return Areas_edit.ValidateForm(this);">
<p />
<input type="hidden" name="a_table" id="a_table" value="Areas" />
<input type="hidden" name="a_edit" id="a_edit" value="U" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (Areas.IdArea.Visible) { // IdArea %>
	<tr id="r_IdArea"<%= Areas.RowAttributes %>>
		<td class="ewTableHeader"><%= Areas.IdArea.FldCaption %></td>
		<td<%= Areas.IdArea.CellAttributes %>><span id="el_IdArea">
<div<%= Areas.IdArea.ViewAttributes %>><%= Areas.IdArea.EditValue %></div>
<input type="hidden" name="x_IdArea" id="x_IdArea" value="<%= ew_HtmlEncode(Areas.IdArea.CurrentValue) %>" />
</span><%= Areas.IdArea.CustomMsg %></td>
	</tr>
<% } %>
<% if (Areas.Area.Visible) { // Area %>
	<tr id="r_Area"<%= Areas.RowAttributes %>>
		<td class="ewTableHeader"><%= Areas.Area.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= Areas.Area.CellAttributes %>><span id="el_Area">
<input type="text" name="x_Area" id="x_Area" size="50" maxlength="50" value="<%= Areas.Area.EditValue %>"<%= Areas.Area.EditAttributes %> />
</span><%= Areas.Area.CustomMsg %></td>
	</tr>
<% } %>
<% if (Areas.Codigo.Visible) { // Codigo %>
	<tr id="r_Codigo"<%= Areas.RowAttributes %>>
		<td class="ewTableHeader"><%= Areas.Codigo.FldCaption %></td>
		<td<%= Areas.Codigo.CellAttributes %>><span id="el_Codigo">
<input type="text" name="x_Codigo" id="x_Codigo" size="10" maxlength="10" value="<%= Areas.Codigo.EditValue %>"<%= Areas.Codigo.EditAttributes %> />
</span><%= Areas.Codigo.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%

// Begin detail grid for "Personas"
if (Areas.CurrentDetailTable == "Personas" && Personas.DetailEdit) {
%>
<br />
<%
string dtlparms = Personas.DetailParms; // ASPX
dtlparms += "&confirmpage=" + ((Areas_edit.ConfirmPage) ? "1" : "0");
dtlparms += "&IdArea=" + ew_UrlEncode(Personas.IdArea.CurrentValue);
dtlparms += "&mastereventcancelled=" + ((Areas.EventCancelled) ? "1" : "0");
Server.Execute("Personasgrid.aspx?" + dtlparms);
%>
<br />
<%
}

// End detail grid for "Personas"
%>
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("EditBtn")) %>" />
</form>
<%
Areas_edit.ShowPageFooter();
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
