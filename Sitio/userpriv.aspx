<%@ Page ClassName="_userpriv" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="userpriv.aspx.cs" Inherits="_userpriv" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var userpriv = new ew_Page("userpriv");

// page properties
userpriv.PageID = "userpriv"; // page ID
userpriv.FormID = "fUserLevelsuserpriv"; // form ID 
var EW_PAGE_ID = userpriv.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
userpriv.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
userpriv.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
userpriv.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("UserLevelPermission") %></p>
<p class="aspnetmaker"><a href="UserLevelslist.aspx"><%= Language.Phrase("BackToList") %></a></p>
<p class="aspnetmaker"><%= Language.Phrase("UserLevel") %><%= Security.GetUserLevelName(Convert.ToInt32(UserLevels.UserLevelID.CurrentValue)) %>(<%= UserLevels.UserLevelID.CurrentValue %>)</p>
<% userpriv.ShowMessage(); %>
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<form name="userprivform" id="userprivform" method="post">
<input type="hidden" name="t" id="t" value="UserLevels" />
<input type="hidden" name="a_edit" id="a_edit" value="U" />
<!-- hidden tag for User Level ID -->
<input type="hidden" name="x_UserLevelID" id="x_UserLevelID" value="<%= UserLevels.UserLevelID.CurrentValue %>" />
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
	<thead>
	<tr class="ewTableHeader">
		<td><%= Language.Phrase("TableOrView") %></td>
		<td><%= Language.Phrase("PermissionAddCopy") %><input type="checkbox" name="Add" id="Add" onclick="ew_SelectAll(this);"<%= userpriv.Disabled %> /></td>
		<td><%= Language.Phrase("PermissionDelete") %><input type="checkbox" name="Delete" id="Delete" onclick="ew_SelectAll(this);"<%= userpriv.Disabled %> /></td>
		<td><%= Language.Phrase("PermissionEdit") %><input type="checkbox" name="Edit" id="Edit" onclick="ew_SelectAll(this);"<%= userpriv.Disabled %> /></td>
<% if (EW_USER_LEVEL_COMPAT) { %>
		<td><%= Language.Phrase("PermissionListSearchView") %><input type="checkbox" name="List" id="List" onclick="ew_SelectAll(this);"<%= userpriv.Disabled %> /></td>
<% } else { %>
		<td><%= Language.Phrase("PermissionList") %><input type="checkbox" name="List" id="List" onclick="ew_SelectAll(this);"<%= userpriv.Disabled %> /></td>
		<td><%= Language.Phrase("PermissionView") %><input type="checkbox" name="View" id="View" onclick="ew_SelectAll(this);"<%= userpriv.Disabled %> /></td>
		<td><%= Language.Phrase("PermissionSearch") %><input type="checkbox" name="Search" id="Search" onclick="ew_SelectAll(this);"<%= userpriv.Disabled %> /></td>
<% } %>
	</tr>
	</thead>
	<tbody>
<%
for (int i = 0; i < userpriv.TableNames.Length; i++) {
	userpriv.TempPriv = Security.GetUserLevelPrivEx(userpriv.TableNames[i], ew_ConvertToInt(UserLevels.UserLevelID.CurrentValue));

	// Set row properties
	UserLevels.ResetAttrs();
	ew_SetAttr(ref UserLevels.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}}); 
%>
	<tr<%= UserLevels.RowAttributes %>>
		<td><span class="aspnetmaker"><%= userpriv.GetTableCaption(i) %></span></td>
		<td align="center"><input type="checkbox" name="Add_<%= i %>" id="Add_<%= i %>" value="1"<% if ((userpriv.TempPriv & EW_ALLOW_ADD) == EW_ALLOW_ADD) { %> checked="checked"<% } %><%= userpriv.Disabled %> /></td>
		<td align="center"><input type="checkbox" name="Delete_<%= i %>" id="Delete_<%= i %>" value="2"<% if ((userpriv.TempPriv & EW_ALLOW_DELETE) == EW_ALLOW_DELETE) { %> checked="checked"<% } %><%= userpriv.Disabled %> /></td>
		<td align="center"><input type="checkbox" name="Edit_<%= i %>" id="Edit_<%= i %>" value="4"<% if ((userpriv.TempPriv & EW_ALLOW_EDIT) == EW_ALLOW_EDIT) { %> checked="checked"<% } %><%= userpriv.Disabled %> /></td>
<% if (EW_USER_LEVEL_COMPAT) { %>
		<td align="center"><input type="checkbox" name="List_<%= i %>" id="List_<%= i %>" value="8"<% if ((userpriv.TempPriv & EW_ALLOW_LIST) == EW_ALLOW_LIST) { %> checked="checked"<% } %><%= userpriv.Disabled %> /></td>
<% } else { %>
		<td align="center"><input type="checkbox" name="List_<%= i %>" id="List_<%= i %>" value="8"<% if ((userpriv.TempPriv & EW_ALLOW_LIST) == EW_ALLOW_LIST) { %> checked="checked"<% } %><%= userpriv.Disabled %> /></td>
		<td align="center"><input type="checkbox" name="View_<%= i %>" id="View_<%= i %>" value="32"<% if ((userpriv.TempPriv & EW_ALLOW_VIEW) == EW_ALLOW_VIEW) { %> checked="checked"<% } %><%= userpriv.Disabled %> /></td>
		<td align="center"><input type="checkbox" name="Search_<%= i %>" id="Search_<%= i %>" value="64"<% if ((userpriv.TempPriv & EW_ALLOW_SEARCH) == EW_ALLOW_SEARCH) { %> checked="checked"<% } %><%= userpriv.Disabled %> /></td>
<% } %>
	</tr>
<% } %>
	</tbody>				
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="btnSubmit" id="btnSubmit" value="<%= ew_BtnCaption(Language.Phrase("Update")) %>"<%= userpriv.Disabled %> />
</form>
<script language="JavaScript" type="text/javascript">
<!--

// Write your startup script here
// document.write("page loaded");
//-->

</script>
</asp:Content>
