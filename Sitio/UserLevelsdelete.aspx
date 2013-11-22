<%@ Page ClassName="UserLevelsdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="UserLevelsdelete.aspx.cs" Inherits="UserLevelsdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var UserLevels_delete = new ew_Page("UserLevels_delete");

// page properties
UserLevels_delete.PageID = "delete"; // page ID
UserLevels_delete.FormID = "fUserLevelsdelete"; // form ID 
var EW_PAGE_ID = UserLevels_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
UserLevels_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
UserLevels_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
UserLevels_delete.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<%

// Load records for display
UserLevels_delete.Recordset = UserLevels_delete.LoadRecordset();
if (UserLevels_delete.TotalRecs <= 0) { // No record found, exit
	if (UserLevels_delete.Recordset != null) {
		UserLevels_delete.Recordset.Close();
		UserLevels_delete.Recordset.Dispose();
	}
	UserLevels_delete.Page_Terminate("UserLevelslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= UserLevels.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= UserLevels.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% UserLevels_delete.ShowPageHeader(); %>
<% UserLevels_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="UserLevels" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in UserLevels_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= UserLevels.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= UserLevels.UserLevelID.FldCaption %></td>
		<td valign="top"><%= UserLevels.UserLevelName.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
UserLevels_delete.RecCnt = 0;
while (UserLevels_delete.Recordset.Read()) {
	UserLevels_delete.RecCnt++;

	// Set row properties
	UserLevels.ResetAttrs();
	UserLevels.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	UserLevels_delete.LoadRowValues(ref UserLevels_delete.Recordset);

	// Render row
	UserLevels_delete.RenderRow();
%>
	<tr<%= UserLevels.RowAttributes %>>
		<td<%= UserLevels.UserLevelID.CellAttributes %>>
<div<%= UserLevels.UserLevelID.ViewAttributes %>><%= UserLevels.UserLevelID.ListViewValue %></div>
</td>
		<td<%= UserLevels.UserLevelName.CellAttributes %>>
<div<%= UserLevels.UserLevelName.ViewAttributes %>><%= UserLevels.UserLevelName.ListViewValue %></div>
</td>
	</tr>
<%
}
UserLevels_delete.Recordset.Close();
UserLevels_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
UserLevels_delete.ShowPageFooter();
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
