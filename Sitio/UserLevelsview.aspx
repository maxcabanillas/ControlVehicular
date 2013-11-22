<%@ Page ClassName="UserLevelsview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="UserLevelsview.aspx.cs" Inherits="UserLevelsview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(UserLevels.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var UserLevels_view = new ew_Page("UserLevels_view");

// page properties
UserLevels_view.PageID = "view"; // page ID
UserLevels_view.FormID = "fUserLevelsview"; // form ID 
var EW_PAGE_ID = UserLevels_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
UserLevels_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
UserLevels_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
UserLevels_view.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<div id="ewDetailsDiv" style="visibility: hidden; z-index: 11000;" name="ewDetailsDivDiv"></div>
<script language="JavaScript" type="text/javascript">
<!--

// YUI container
var ewDetailsDiv;
var ew_AjaxDetailsTimer = null;

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<% } %>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= UserLevels.TableCaption %>
&nbsp;&nbsp;<% UserLevels_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(UserLevels.Export)) { %>
<p class="aspnetmaker">
<a href="<%= UserLevels_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<a href="<%= UserLevels_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% if (Security.CanEdit) { %>
<a href="<%= UserLevels_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% if (Security.CanDelete) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= UserLevels_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% } %>
</p>
<% UserLevels_view.ShowPageHeader(); %>
<% UserLevels_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (UserLevels.UserLevelID.Visible) { // UserLevelID %>
	<tr id="r_UserLevelID"<%= UserLevels.RowAttributes %>>
		<td class="ewTableHeader"><%= UserLevels.UserLevelID.FldCaption %></td>
		<td<%= UserLevels.UserLevelID.CellAttributes %>>
<div<%= UserLevels.UserLevelID.ViewAttributes %>><%= UserLevels.UserLevelID.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (UserLevels.UserLevelName.Visible) { // UserLevelName %>
	<tr id="r_UserLevelName"<%= UserLevels.RowAttributes %>>
		<td class="ewTableHeader"><%= UserLevels.UserLevelName.FldCaption %></td>
		<td<%= UserLevels.UserLevelName.CellAttributes %>>
<div<%= UserLevels.UserLevelName.ViewAttributes %>><%= UserLevels.UserLevelName.ViewValue %></div>
</td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
UserLevels_view.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(UserLevels.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
