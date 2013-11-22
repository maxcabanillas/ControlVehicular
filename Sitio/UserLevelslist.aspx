<%@ Page ClassName="UserLevelslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="UserLevelslist.aspx.cs" Inherits="UserLevelslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(UserLevels.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var UserLevels_list = new ew_Page("UserLevels_list");

// page properties
UserLevels_list.PageID = "list"; // page ID
UserLevels_list.FormID = "fUserLevelslist"; // form ID 
var EW_PAGE_ID = UserLevels_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
UserLevels_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
UserLevels_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
UserLevels_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(UserLevels.Export) || (EW_EXPORT_MASTER_RECORD && UserLevels.Export == "print")) { %>
<% } %>
<%
	UserLevels_list.Recordset = UserLevels_list.LoadRecordset();
	UserLevels_list.StartRec = 1;
	if (UserLevels_list.DisplayRecs <= 0) // Display all records
		UserLevels_list.DisplayRecs = UserLevels_list.TotalRecs;
	if (!(UserLevels.ExportAll && ew_NotEmpty(UserLevels.Export)))
		UserLevels_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= UserLevels.TableCaption %>
&nbsp;&nbsp;<% UserLevels_list.ExportOptions.Render("body"); %>
</p>
<% UserLevels_list.ShowPageHeader(); %>
<% UserLevels_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(UserLevels.Export)) { %>
<div class="ewGridUpperPanel">
<% if (UserLevels.CurrentAction != "gridadd" && UserLevels.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (UserLevels_list.Pager == null) UserLevels_list.Pager = new cNumericPager(UserLevels_list.StartRec, UserLevels_list.DisplayRecs, UserLevels_list.TotalRecs, UserLevels_list.RecRange); %>
<% if (UserLevels_list.Pager.RecordCount > 0) { %>
	<% if (UserLevels_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= UserLevels_list.PageUrl %>start=<%= UserLevels_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (UserLevels_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= UserLevels_list.PageUrl %>start=<%= UserLevels_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in UserLevels_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= UserLevels_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (UserLevels_list.Pager.NextButton.Enabled) { %>
	<a href="<%= UserLevels_list.PageUrl %>start=<%= UserLevels_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (UserLevels_list.Pager.LastButton.Enabled) { %>
	<a href="<%= UserLevels_list.PageUrl %>start=<%= UserLevels_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (UserLevels_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= UserLevels_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= UserLevels_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= UserLevels_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (UserLevels_list.SearchWhere == "0=101") { %>
	<%= Language.Phrase("EnterSearchCriteria") %>
	<% } else { %>
	<%= Language.Phrase("NoRecord") %>
	<% } %>
	<% } else { %>
	<%= Language.Phrase("NoPermission") %>
	<% } %>
<% } %>
</span>
		</td>
	</tr>
</table>
</form>
<% } %>
<div class="aspnetmaker">
<% if (Security.CanAdd) { %>
<a class="ewGridLink" href="<%= UserLevels_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<form name="fUserLevelslist" id="fUserLevelslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="UserLevels" />
<div id="gmp_UserLevels" class="ewGridMiddlePanel">
<% if (UserLevels_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= UserLevels.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		UserLevels_list.RenderListOptions();

		// Render list options (header, left)
		UserLevels_list.ListOptions.Render("header", "left");
%>
<% if (UserLevels.UserLevelID.Visible) { // UserLevelID %>
	<% if (ew_Empty(UserLevels.SortUrl(UserLevels.UserLevelID))) { %>
		<td><%= UserLevels.UserLevelID.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= UserLevels.SortUrl(UserLevels.UserLevelID) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= UserLevels.UserLevelID.FldCaption %></td><td style="width: 10px;"><% if (UserLevels.UserLevelID.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (UserLevels.UserLevelID.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (UserLevels.UserLevelName.Visible) { // UserLevelName %>
	<% if (ew_Empty(UserLevels.SortUrl(UserLevels.UserLevelName))) { %>
		<td><%= UserLevels.UserLevelName.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= UserLevels.SortUrl(UserLevels.UserLevelName) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= UserLevels.UserLevelName.FldCaption %></td><td style="width: 10px;"><% if (UserLevels.UserLevelName.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (UserLevels.UserLevelName.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		UserLevels_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (UserLevels.ExportAll && ew_NotEmpty(UserLevels.Export)) {
	UserLevels_list.StopRec = UserLevels_list.TotalRecs;
} else {

	// Set the last record to display
	if (UserLevels_list.TotalRecs > UserLevels_list.StartRec + UserLevels_list.DisplayRecs - 1) {
		UserLevels_list.StopRec = UserLevels_list.StartRec + UserLevels_list.DisplayRecs - 1;
	} else {
		UserLevels_list.StopRec = UserLevels_list.TotalRecs;
	}
}
if (UserLevels_list.Recordset != null && UserLevels_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= UserLevels_list.StartRec - 1; i++) {
		if (UserLevels_list.Recordset.Read())
			UserLevels_list.RecCnt += 1;
	}		
} else if (!UserLevels.AllowAddDeleteRow && UserLevels_list.StopRec == 0) {
	UserLevels_list.StopRec = UserLevels.GridAddRowCount;
}

// Initialize Aggregate
UserLevels.RowType = EW_ROWTYPE_AGGREGATEINIT;
UserLevels.ResetAttrs();
UserLevels_list.RenderRow();
UserLevels_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (UserLevels_list.RecCnt < UserLevels_list.StopRec) {
	if (UserLevels.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !UserLevels_list.Recordset.Read();
	UserLevels_list.RecCnt += 1;
	if (UserLevels_list.RecCnt >= UserLevels_list.StartRec) {
		UserLevels_list.RowCnt += 1;

		// Set up key count
		UserLevels_list.KeyCount = ew_ConvertToInt(UserLevels_list.RowIndex);

		// Init row class and style
		UserLevels.ResetAttrs();
		UserLevels.CssClass = "";	 
		if (UserLevels.CurrentAction == "gridadd") {
		} else {
			UserLevels_list.LoadRowValues(ref UserLevels_list.Recordset); // Load row values
		}
		UserLevels.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref UserLevels.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		UserLevels_list.RenderRow();

		// Render list options
		UserLevels_list.RenderListOptions();
%>
	<tr<%= UserLevels.RowAttributes %>>
	<%

		// Render list options (body, left)
		UserLevels_list.ListOptions.Render("body", "left");
	%>
	<% if (UserLevels.UserLevelID.Visible) { // UserLevelID %>
		<td<%= UserLevels.UserLevelID.CellAttributes %>>
<div<%= UserLevels.UserLevelID.ViewAttributes %>><%= UserLevels.UserLevelID.ListViewValue %></div>
<a name="<%= UserLevels_list.PageObjName + "_row_" + UserLevels_list.RowCnt %>" id="<%= UserLevels_list.PageObjName + "_row_" + UserLevels_list.RowCnt %>"></a></td>
	<% } %>
	<% if (UserLevels.UserLevelName.Visible) { // UserLevelName %>
		<td<%= UserLevels.UserLevelName.CellAttributes %>>
<div<%= UserLevels.UserLevelName.ViewAttributes %>><%= UserLevels.UserLevelName.ListViewValue %></div>
</td>
	<% } %>
<%

		// Render list options (body, right)
		UserLevels_list.ListOptions.Render("body", "right");
%>
	</tr>
<%
	}
}
%>
</tbody>
</table>
<% } %>
</div>
</form>
<%

// Close recordset
if (UserLevels_list.Recordset != null) {
	UserLevels_list.Recordset.Close();
	UserLevels_list.Recordset.Dispose();
}
%>
<% if (UserLevels_list.TotalRecs > 0) { %>
<% if (ew_Empty(UserLevels.Export)) { %>
<div class="ewGridLowerPanel">
<% if (UserLevels.CurrentAction != "gridadd" && UserLevels.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (UserLevels_list.Pager == null) UserLevels_list.Pager = new cNumericPager(UserLevels_list.StartRec, UserLevels_list.DisplayRecs, UserLevels_list.TotalRecs, UserLevels_list.RecRange); %>
<% if (UserLevels_list.Pager.RecordCount > 0) { %>
	<% if (UserLevels_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= UserLevels_list.PageUrl %>start=<%= UserLevels_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (UserLevels_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= UserLevels_list.PageUrl %>start=<%= UserLevels_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in UserLevels_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= UserLevels_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (UserLevels_list.Pager.NextButton.Enabled) { %>
	<a href="<%= UserLevels_list.PageUrl %>start=<%= UserLevels_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (UserLevels_list.Pager.LastButton.Enabled) { %>
	<a href="<%= UserLevels_list.PageUrl %>start=<%= UserLevels_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (UserLevels_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= UserLevels_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= UserLevels_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= UserLevels_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (UserLevels_list.SearchWhere == "0=101") { %>
	<%= Language.Phrase("EnterSearchCriteria") %>
	<% } else { %>
	<%= Language.Phrase("NoRecord") %>
	<% } %>
	<% } else { %>
	<%= Language.Phrase("NoPermission") %>
	<% } %>
<% } %>
</span>
		</td>
	</tr>
</table>
</form>
<% } %>
<div class="aspnetmaker">
<% if (Security.CanAdd) { %>
<a class="ewGridLink" href="<%= UserLevels_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(UserLevels.Export) && ew_Empty(UserLevels.CurrentAction)) { %>
<% } %>
<%
UserLevels_list.ShowPageFooter();
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
