<%@ Page ClassName="UserLevelsadd" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="UserLevelsadd.aspx.cs" Inherits="UserLevelsadd" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var UserLevels_add = new ew_Page("UserLevels_add");

// page properties
UserLevels_add.PageID = "add"; // page ID
UserLevels_add.FormID = "fUserLevelsadd"; // form ID 
var EW_PAGE_ID = UserLevels_add.PageID; // for backward compatibility

// extend page with ValidateForm function
UserLevels_add.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_UserLevelID"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(UserLevels.UserLevelID.FldCaption) %>");
		elm = fobj.elements["x" + infix + "_UserLevelID"];
		if (elm && elm.type != "hidden" && !ew_CheckInteger(elm.value)) // skip hidden field
			return ew_OnError(this, elm, "<%= ew_JsEncode2(UserLevels.UserLevelID.FldErrMsg) %>");
		elm = fobj.elements["x" + infix + "_UserLevelName"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(UserLevels.UserLevelName.FldCaption) %>");
		elmId = fobj.elements["x" + infix + "_UserLevelID"];
		elmName = fobj.elements["x" + infix + "_UserLevelName"];
		if (elmId && elmName) {
			elmId.value = elmId.value.replace(/^\s+|\s+$/, '');
			elmName.value = elmName.value.replace(/^\s+|\s+$/, '');
			if (elmId && !ew_CheckInteger(elmId.value))
				return ew_OnError(this, elmId, ewLanguage.Phrase("UserLevelIDInteger")); 
			var level = parseInt(elmId.value);
			if (level == 0) {
				if (elmName.value.toLowerCase() != "default")
					return ew_OnError(this, elmName, ewLanguage.Phrase("UserLevelDefaultName")); 
			} else if (level == -1) { 
				if (elmName.value.toLowerCase() != "administrator")
					return ew_OnError(this, elmName, ewLanguage.Phrase("UserLevelAdministratorName")); 
			} else if (level < -1) {
				return ew_OnError(this, elmId, ewLanguage.Phrase("UserLevelIDIncorrect")); 
			} else if (level > 0) { 
				if (elmName.value.toLowerCase() == "administrator" || elmName.value.toLowerCase() == "default")
					return ew_OnError(this, elmName, ewLanguage.Phrase("UserLevelNameIncorrect")); 
			}
		}

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
UserLevels_add.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
UserLevels_add.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
UserLevels_add.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Add") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= UserLevels.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= UserLevels.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% UserLevels_add.ShowPageHeader(); %>
<% UserLevels_add.ShowMessage(); %>
<form name="fUserLevelsadd" id="fUserLevelsadd" method="post" onsubmit="this.action=location.pathname;return UserLevels_add.ValidateForm(this);">
<p />
<input type="hidden" name="t" id="t" value="UserLevels" />
<input type="hidden" name="a_add" id="a_add" value="A" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (UserLevels.UserLevelID.Visible) { // UserLevelID %>
	<tr id="r_UserLevelID"<%= UserLevels.RowAttributes %>>
		<td class="ewTableHeader"><%= UserLevels.UserLevelID.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= UserLevels.UserLevelID.CellAttributes %>><span id="el_UserLevelID">
<input type="text" name="x_UserLevelID" id="x_UserLevelID" size="30" value="<%= UserLevels.UserLevelID.EditValue %>"<%= UserLevels.UserLevelID.EditAttributes %> />
</span><%= UserLevels.UserLevelID.CustomMsg %></td>
	</tr>
<% } %>
<% if (UserLevels.UserLevelName.Visible) { // UserLevelName %>
	<tr id="r_UserLevelName"<%= UserLevels.RowAttributes %>>
		<td class="ewTableHeader"><%= UserLevels.UserLevelName.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= UserLevels.UserLevelName.CellAttributes %>><span id="el_UserLevelName">
<input type="text" name="x_UserLevelName" id="x_UserLevelName" size="30" maxlength="255" value="<%= UserLevels.UserLevelName.EditValue %>"<%= UserLevels.UserLevelName.EditAttributes %> />
</span><%= UserLevels.UserLevelName.CustomMsg %></td>
	</tr>
<% } %>
	<!-- row for permission values -->
	<tr id="rp_permission">
		<td class="ewTableHeader"><%= Language.Phrase("Permission") %></td>
		<td>
<label><input type="checkbox" name="x__AllowAdd" id="Add" value="<%= EW_ALLOW_ADD %>" /><%= Language.Phrase("PermissionAddCopy") %></label>
<label><input type="checkbox" name="x__AllowDelete" id="Delete" value="<%= EW_ALLOW_DELETE %>" /><%= Language.Phrase("PermissionDelete") %></label>
<label><input type="checkbox" name="x__AllowEdit" id="Edit" value="<%= EW_ALLOW_EDIT %>" /><%= Language.Phrase("PermissionEdit") %></label>
<% if (EW_USER_LEVEL_COMPAT) { %>
<label><input type="checkbox" name="x__AllowList" id="List" value="<%= EW_ALLOW_LIST %>" /><%= Language.Phrase("PermissionListSearchView") %></label>
<% } else { %>
<label><input type="checkbox" name="x__AllowList" id="List" value="<%= EW_ALLOW_LIST %>" /><%= Language.Phrase("PermissionList") %></label>
<label><input type="checkbox" name="x__AllowView" id="View" value="<%= EW_ALLOW_VIEW %>" /><%= Language.Phrase("PermissionView") %></label>
<label><input type="checkbox" name="x__AllowSearch" id="Search" value="<%= EW_ALLOW_SEARCH %>" /><%= Language.Phrase("PermissionSearch") %></label>
<% } %>
		</td>
	</tr>	
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("AddBtn")) %>" />
</form>
<%
UserLevels_add.ShowPageFooter();
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
