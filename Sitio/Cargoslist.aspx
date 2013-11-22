<%@ Page ClassName="Cargoslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Cargoslist.aspx.cs" Inherits="Cargoslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(Cargos.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var Cargos_list = new ew_Page("Cargos_list");

// page properties
Cargos_list.PageID = "list"; // page ID
Cargos_list.FormID = "fCargoslist"; // form ID 
var EW_PAGE_ID = Cargos_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Cargos_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Cargos_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Cargos_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(Cargos.Export) || (EW_EXPORT_MASTER_RECORD && Cargos.Export == "print")) { %>
<% } %>
<%
	Cargos_list.Recordset = Cargos_list.LoadRecordset();
	Cargos_list.StartRec = 1;
	if (Cargos_list.DisplayRecs <= 0) // Display all records
		Cargos_list.DisplayRecs = Cargos_list.TotalRecs;
	if (!(Cargos.ExportAll && ew_NotEmpty(Cargos.Export)))
		Cargos_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= Cargos.TableCaption %>
&nbsp;&nbsp;<% Cargos_list.ExportOptions.Render("body"); %>
</p>
<% Cargos_list.ShowPageHeader(); %>
<% Cargos_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(Cargos.Export)) { %>
<div class="ewGridUpperPanel">
<% if (Cargos.CurrentAction != "gridadd" && Cargos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (Cargos_list.Pager == null) Cargos_list.Pager = new cNumericPager(Cargos_list.StartRec, Cargos_list.DisplayRecs, Cargos_list.TotalRecs, Cargos_list.RecRange); %>
<% if (Cargos_list.Pager.RecordCount > 0) { %>
	<% if (Cargos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= Cargos_list.PageUrl %>start=<%= Cargos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (Cargos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= Cargos_list.PageUrl %>start=<%= Cargos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in Cargos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= Cargos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (Cargos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= Cargos_list.PageUrl %>start=<%= Cargos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (Cargos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= Cargos_list.PageUrl %>start=<%= Cargos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (Cargos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= Cargos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= Cargos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= Cargos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (Cargos_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= Cargos_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<form name="fCargoslist" id="fCargoslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="Cargos" />
<div id="gmp_Cargos" class="ewGridMiddlePanel">
<% if (Cargos_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= Cargos.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		Cargos_list.RenderListOptions();

		// Render list options (header, left)
		Cargos_list.ListOptions.Render("header", "left");
%>
<% if (Cargos.Cargo.Visible) { // Cargo %>
	<% if (ew_Empty(Cargos.SortUrl(Cargos.Cargo))) { %>
		<td><%= Cargos.Cargo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Cargos.SortUrl(Cargos.Cargo) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Cargos.Cargo.FldCaption %></td><td style="width: 10px;"><% if (Cargos.Cargo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Cargos.Cargo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		Cargos_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (Cargos.ExportAll && ew_NotEmpty(Cargos.Export)) {
	Cargos_list.StopRec = Cargos_list.TotalRecs;
} else {

	// Set the last record to display
	if (Cargos_list.TotalRecs > Cargos_list.StartRec + Cargos_list.DisplayRecs - 1) {
		Cargos_list.StopRec = Cargos_list.StartRec + Cargos_list.DisplayRecs - 1;
	} else {
		Cargos_list.StopRec = Cargos_list.TotalRecs;
	}
}
if (Cargos_list.Recordset != null && Cargos_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= Cargos_list.StartRec - 1; i++) {
		if (Cargos_list.Recordset.Read())
			Cargos_list.RecCnt += 1;
	}		
} else if (!Cargos.AllowAddDeleteRow && Cargos_list.StopRec == 0) {
	Cargos_list.StopRec = Cargos.GridAddRowCount;
}

// Initialize Aggregate
Cargos.RowType = EW_ROWTYPE_AGGREGATEINIT;
Cargos.ResetAttrs();
Cargos_list.RenderRow();
Cargos_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (Cargos_list.RecCnt < Cargos_list.StopRec) {
	if (Cargos.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !Cargos_list.Recordset.Read();
	Cargos_list.RecCnt += 1;
	if (Cargos_list.RecCnt >= Cargos_list.StartRec) {
		Cargos_list.RowCnt += 1;

		// Set up key count
		Cargos_list.KeyCount = ew_ConvertToInt(Cargos_list.RowIndex);

		// Init row class and style
		Cargos.ResetAttrs();
		Cargos.CssClass = "";	 
		if (Cargos.CurrentAction == "gridadd") {
		} else {
			Cargos_list.LoadRowValues(ref Cargos_list.Recordset); // Load row values
		}
		Cargos.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref Cargos.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		Cargos_list.RenderRow();

		// Render list options
		Cargos_list.RenderListOptions();
%>
	<tr<%= Cargos.RowAttributes %>>
	<%

		// Render list options (body, left)
		Cargos_list.ListOptions.Render("body", "left");
	%>
	<% if (Cargos.Cargo.Visible) { // Cargo %>
		<td<%= Cargos.Cargo.CellAttributes %>>
<div<%= Cargos.Cargo.ViewAttributes %>><%= Cargos.Cargo.ListViewValue %></div>
<a name="<%= Cargos_list.PageObjName + "_row_" + Cargos_list.RowCnt %>" id="<%= Cargos_list.PageObjName + "_row_" + Cargos_list.RowCnt %>"></a></td>
	<% } %>
<%

		// Render list options (body, right)
		Cargos_list.ListOptions.Render("body", "right");
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
if (Cargos_list.Recordset != null) {
	Cargos_list.Recordset.Close();
	Cargos_list.Recordset.Dispose();
}
%>
<% if (Cargos_list.TotalRecs > 0) { %>
<% if (ew_Empty(Cargos.Export)) { %>
<div class="ewGridLowerPanel">
<% if (Cargos.CurrentAction != "gridadd" && Cargos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (Cargos_list.Pager == null) Cargos_list.Pager = new cNumericPager(Cargos_list.StartRec, Cargos_list.DisplayRecs, Cargos_list.TotalRecs, Cargos_list.RecRange); %>
<% if (Cargos_list.Pager.RecordCount > 0) { %>
	<% if (Cargos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= Cargos_list.PageUrl %>start=<%= Cargos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (Cargos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= Cargos_list.PageUrl %>start=<%= Cargos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in Cargos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= Cargos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (Cargos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= Cargos_list.PageUrl %>start=<%= Cargos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (Cargos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= Cargos_list.PageUrl %>start=<%= Cargos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (Cargos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= Cargos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= Cargos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= Cargos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (Cargos_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= Cargos_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(Cargos.Export) && ew_Empty(Cargos.CurrentAction)) { %>
<% } %>
<%
Cargos_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(Cargos.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
