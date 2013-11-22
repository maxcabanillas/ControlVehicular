<%@ Page ClassName="z_RegistroPeatoneslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="z_RegistroPeatoneslist.aspx.cs" Inherits="z_RegistroPeatoneslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(z_RegistroPeatones.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var z_RegistroPeatones_list = new ew_Page("z_RegistroPeatones_list");

// page properties
z_RegistroPeatones_list.PageID = "list"; // page ID
z_RegistroPeatones_list.FormID = "fz_RegistroPeatoneslist"; // form ID 
var EW_PAGE_ID = z_RegistroPeatones_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
z_RegistroPeatones_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
z_RegistroPeatones_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
z_RegistroPeatones_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(z_RegistroPeatones.Export) || (EW_EXPORT_MASTER_RECORD && z_RegistroPeatones.Export == "print")) { %>
<% } %>
<%
	z_RegistroPeatones_list.Recordset = z_RegistroPeatones_list.LoadRecordset();
	z_RegistroPeatones_list.StartRec = 1;
	if (z_RegistroPeatones_list.DisplayRecs <= 0) // Display all records
		z_RegistroPeatones_list.DisplayRecs = z_RegistroPeatones_list.TotalRecs;
	if (!(z_RegistroPeatones.ExportAll && ew_NotEmpty(z_RegistroPeatones.Export)))
		z_RegistroPeatones_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeCUSTOMVIEW") %><%= z_RegistroPeatones.TableCaption %>
&nbsp;&nbsp;<% z_RegistroPeatones_list.ExportOptions.Render("body"); %>
</p>
<% z_RegistroPeatones_list.ShowPageHeader(); %>
<% z_RegistroPeatones_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(z_RegistroPeatones.Export)) { %>
<div class="ewGridUpperPanel">
<% if (z_RegistroPeatones.CurrentAction != "gridadd" && z_RegistroPeatones.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (z_RegistroPeatones_list.Pager == null) z_RegistroPeatones_list.Pager = new cNumericPager(z_RegistroPeatones_list.StartRec, z_RegistroPeatones_list.DisplayRecs, z_RegistroPeatones_list.TotalRecs, z_RegistroPeatones_list.RecRange); %>
<% if (z_RegistroPeatones_list.Pager.RecordCount > 0) { %>
	<% if (z_RegistroPeatones_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= z_RegistroPeatones_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroPeatones_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= z_RegistroPeatones_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in z_RegistroPeatones_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (z_RegistroPeatones_list.Pager.NextButton.Enabled) { %>
	<a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= z_RegistroPeatones_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroPeatones_list.Pager.LastButton.Enabled) { %>
	<a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= z_RegistroPeatones_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroPeatones_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= z_RegistroPeatones_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= z_RegistroPeatones_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= z_RegistroPeatones_list.Pager.RecordCount %>
<% } else { %>
	<% if (z_RegistroPeatones_list.SearchWhere == "0=101") { %>
	<%= Language.Phrase("EnterSearchCriteria") %>
	<% } else { %>
	<%= Language.Phrase("NoRecord") %>
	<% } %>
<% } %>
</span>
		</td>
	</tr>
</table>
</form>
<% } %>
<div class="aspnetmaker">
</div>
</div>
<% } %>
<form name="fz_RegistroPeatoneslist" id="fz_RegistroPeatoneslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="z_RegistroPeatones" />
<div id="gmp_z_RegistroPeatones" class="ewGridMiddlePanel">
<% if (z_RegistroPeatones_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= z_RegistroPeatones.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		z_RegistroPeatones_list.RenderListOptions();

		// Render list options (header, left)
		z_RegistroPeatones_list.ListOptions.Render("header", "left");
%>
<%

		// Render list options (header, right)
		z_RegistroPeatones_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (z_RegistroPeatones.ExportAll && ew_NotEmpty(z_RegistroPeatones.Export)) {
	z_RegistroPeatones_list.StopRec = z_RegistroPeatones_list.TotalRecs;
} else {

	// Set the last record to display
	if (z_RegistroPeatones_list.TotalRecs > z_RegistroPeatones_list.StartRec + z_RegistroPeatones_list.DisplayRecs - 1) {
		z_RegistroPeatones_list.StopRec = z_RegistroPeatones_list.StartRec + z_RegistroPeatones_list.DisplayRecs - 1;
	} else {
		z_RegistroPeatones_list.StopRec = z_RegistroPeatones_list.TotalRecs;
	}
}
if (z_RegistroPeatones_list.Recordset != null && z_RegistroPeatones_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= z_RegistroPeatones_list.StartRec - 1; i++) {
		if (z_RegistroPeatones_list.Recordset.Read())
			z_RegistroPeatones_list.RecCnt += 1;
	}		
} else if (!z_RegistroPeatones.AllowAddDeleteRow && z_RegistroPeatones_list.StopRec == 0) {
	z_RegistroPeatones_list.StopRec = z_RegistroPeatones.GridAddRowCount;
}

// Initialize Aggregate
z_RegistroPeatones.RowType = EW_ROWTYPE_AGGREGATEINIT;
z_RegistroPeatones.ResetAttrs();
z_RegistroPeatones_list.RenderRow();
z_RegistroPeatones_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (z_RegistroPeatones_list.RecCnt < z_RegistroPeatones_list.StopRec) {
	if (z_RegistroPeatones.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !z_RegistroPeatones_list.Recordset.Read();
	z_RegistroPeatones_list.RecCnt += 1;
	if (z_RegistroPeatones_list.RecCnt >= z_RegistroPeatones_list.StartRec) {
		z_RegistroPeatones_list.RowCnt += 1;

		// Set up key count
		z_RegistroPeatones_list.KeyCount = ew_ConvertToInt(z_RegistroPeatones_list.RowIndex);

		// Init row class and style
		z_RegistroPeatones.ResetAttrs();
		z_RegistroPeatones.CssClass = "";	 
		if (z_RegistroPeatones.CurrentAction == "gridadd") {
		} else {
			z_RegistroPeatones_list.LoadRowValues(ref z_RegistroPeatones_list.Recordset); // Load row values
		}
		z_RegistroPeatones.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref z_RegistroPeatones.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		z_RegistroPeatones_list.RenderRow();

		// Render list options
		z_RegistroPeatones_list.RenderListOptions();
%>
	<tr<%= z_RegistroPeatones.RowAttributes %>>
	<%

		// Render list options (body, left)
		z_RegistroPeatones_list.ListOptions.Render("body", "left");
	%>
<%

		// Render list options (body, right)
		z_RegistroPeatones_list.ListOptions.Render("body", "right");
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
if (z_RegistroPeatones_list.Recordset != null) {
	z_RegistroPeatones_list.Recordset.Close();
	z_RegistroPeatones_list.Recordset.Dispose();
}
%>
<% if (z_RegistroPeatones_list.TotalRecs > 0) { %>
<% if (ew_Empty(z_RegistroPeatones.Export)) { %>
<div class="ewGridLowerPanel">
<% if (z_RegistroPeatones.CurrentAction != "gridadd" && z_RegistroPeatones.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (z_RegistroPeatones_list.Pager == null) z_RegistroPeatones_list.Pager = new cNumericPager(z_RegistroPeatones_list.StartRec, z_RegistroPeatones_list.DisplayRecs, z_RegistroPeatones_list.TotalRecs, z_RegistroPeatones_list.RecRange); %>
<% if (z_RegistroPeatones_list.Pager.RecordCount > 0) { %>
	<% if (z_RegistroPeatones_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= z_RegistroPeatones_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroPeatones_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= z_RegistroPeatones_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in z_RegistroPeatones_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (z_RegistroPeatones_list.Pager.NextButton.Enabled) { %>
	<a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= z_RegistroPeatones_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroPeatones_list.Pager.LastButton.Enabled) { %>
	<a href="<%= z_RegistroPeatones_list.PageUrl %>start=<%= z_RegistroPeatones_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroPeatones_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= z_RegistroPeatones_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= z_RegistroPeatones_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= z_RegistroPeatones_list.Pager.RecordCount %>
<% } else { %>
	<% if (z_RegistroPeatones_list.SearchWhere == "0=101") { %>
	<%= Language.Phrase("EnterSearchCriteria") %>
	<% } else { %>
	<%= Language.Phrase("NoRecord") %>
	<% } %>
<% } %>
</span>
		</td>
	</tr>
</table>
</form>
<% } %>
<div class="aspnetmaker">
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(z_RegistroPeatones.Export) && ew_Empty(z_RegistroPeatones.CurrentAction)) { %>
<% } %>
<%
z_RegistroPeatones_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(z_RegistroPeatones.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
