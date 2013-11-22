<%@ Page ClassName="TiposDocumentoslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="TiposDocumentoslist.aspx.cs" Inherits="TiposDocumentoslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(TiposDocumentos.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var TiposDocumentos_list = new ew_Page("TiposDocumentos_list");

// page properties
TiposDocumentos_list.PageID = "list"; // page ID
TiposDocumentos_list.FormID = "fTiposDocumentoslist"; // form ID 
var EW_PAGE_ID = TiposDocumentos_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
TiposDocumentos_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
TiposDocumentos_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
TiposDocumentos_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(TiposDocumentos.Export) || (EW_EXPORT_MASTER_RECORD && TiposDocumentos.Export == "print")) { %>
<% } %>
<%
	TiposDocumentos_list.Recordset = TiposDocumentos_list.LoadRecordset();
	TiposDocumentos_list.StartRec = 1;
	if (TiposDocumentos_list.DisplayRecs <= 0) // Display all records
		TiposDocumentos_list.DisplayRecs = TiposDocumentos_list.TotalRecs;
	if (!(TiposDocumentos.ExportAll && ew_NotEmpty(TiposDocumentos.Export)))
		TiposDocumentos_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= TiposDocumentos.TableCaption %>
&nbsp;&nbsp;<% TiposDocumentos_list.ExportOptions.Render("body"); %>
</p>
<% TiposDocumentos_list.ShowPageHeader(); %>
<% TiposDocumentos_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(TiposDocumentos.Export)) { %>
<div class="ewGridUpperPanel">
<% if (TiposDocumentos.CurrentAction != "gridadd" && TiposDocumentos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (TiposDocumentos_list.Pager == null) TiposDocumentos_list.Pager = new cNumericPager(TiposDocumentos_list.StartRec, TiposDocumentos_list.DisplayRecs, TiposDocumentos_list.TotalRecs, TiposDocumentos_list.RecRange); %>
<% if (TiposDocumentos_list.Pager.RecordCount > 0) { %>
	<% if (TiposDocumentos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= TiposDocumentos_list.PageUrl %>start=<%= TiposDocumentos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (TiposDocumentos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= TiposDocumentos_list.PageUrl %>start=<%= TiposDocumentos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in TiposDocumentos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= TiposDocumentos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (TiposDocumentos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= TiposDocumentos_list.PageUrl %>start=<%= TiposDocumentos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (TiposDocumentos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= TiposDocumentos_list.PageUrl %>start=<%= TiposDocumentos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (TiposDocumentos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= TiposDocumentos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= TiposDocumentos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= TiposDocumentos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (TiposDocumentos_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= TiposDocumentos_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<form name="fTiposDocumentoslist" id="fTiposDocumentoslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="TiposDocumentos" />
<div id="gmp_TiposDocumentos" class="ewGridMiddlePanel">
<% if (TiposDocumentos_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= TiposDocumentos.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		TiposDocumentos_list.RenderListOptions();

		// Render list options (header, left)
		TiposDocumentos_list.ListOptions.Render("header", "left");
%>
<% if (TiposDocumentos.TipoDocumento.Visible) { // TipoDocumento %>
	<% if (ew_Empty(TiposDocumentos.SortUrl(TiposDocumentos.TipoDocumento))) { %>
		<td><%= TiposDocumentos.TipoDocumento.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= TiposDocumentos.SortUrl(TiposDocumentos.TipoDocumento) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= TiposDocumentos.TipoDocumento.FldCaption %></td><td style="width: 10px;"><% if (TiposDocumentos.TipoDocumento.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (TiposDocumentos.TipoDocumento.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		TiposDocumentos_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (TiposDocumentos.ExportAll && ew_NotEmpty(TiposDocumentos.Export)) {
	TiposDocumentos_list.StopRec = TiposDocumentos_list.TotalRecs;
} else {

	// Set the last record to display
	if (TiposDocumentos_list.TotalRecs > TiposDocumentos_list.StartRec + TiposDocumentos_list.DisplayRecs - 1) {
		TiposDocumentos_list.StopRec = TiposDocumentos_list.StartRec + TiposDocumentos_list.DisplayRecs - 1;
	} else {
		TiposDocumentos_list.StopRec = TiposDocumentos_list.TotalRecs;
	}
}
if (TiposDocumentos_list.Recordset != null && TiposDocumentos_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= TiposDocumentos_list.StartRec - 1; i++) {
		if (TiposDocumentos_list.Recordset.Read())
			TiposDocumentos_list.RecCnt += 1;
	}		
} else if (!TiposDocumentos.AllowAddDeleteRow && TiposDocumentos_list.StopRec == 0) {
	TiposDocumentos_list.StopRec = TiposDocumentos.GridAddRowCount;
}

// Initialize Aggregate
TiposDocumentos.RowType = EW_ROWTYPE_AGGREGATEINIT;
TiposDocumentos.ResetAttrs();
TiposDocumentos_list.RenderRow();
TiposDocumentos_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (TiposDocumentos_list.RecCnt < TiposDocumentos_list.StopRec) {
	if (TiposDocumentos.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !TiposDocumentos_list.Recordset.Read();
	TiposDocumentos_list.RecCnt += 1;
	if (TiposDocumentos_list.RecCnt >= TiposDocumentos_list.StartRec) {
		TiposDocumentos_list.RowCnt += 1;

		// Set up key count
		TiposDocumentos_list.KeyCount = ew_ConvertToInt(TiposDocumentos_list.RowIndex);

		// Init row class and style
		TiposDocumentos.ResetAttrs();
		TiposDocumentos.CssClass = "";	 
		if (TiposDocumentos.CurrentAction == "gridadd") {
		} else {
			TiposDocumentos_list.LoadRowValues(ref TiposDocumentos_list.Recordset); // Load row values
		}
		TiposDocumentos.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref TiposDocumentos.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		TiposDocumentos_list.RenderRow();

		// Render list options
		TiposDocumentos_list.RenderListOptions();
%>
	<tr<%= TiposDocumentos.RowAttributes %>>
	<%

		// Render list options (body, left)
		TiposDocumentos_list.ListOptions.Render("body", "left");
	%>
	<% if (TiposDocumentos.TipoDocumento.Visible) { // TipoDocumento %>
		<td<%= TiposDocumentos.TipoDocumento.CellAttributes %>>
<div<%= TiposDocumentos.TipoDocumento.ViewAttributes %>><%= TiposDocumentos.TipoDocumento.ListViewValue %></div>
<a name="<%= TiposDocumentos_list.PageObjName + "_row_" + TiposDocumentos_list.RowCnt %>" id="<%= TiposDocumentos_list.PageObjName + "_row_" + TiposDocumentos_list.RowCnt %>"></a></td>
	<% } %>
<%

		// Render list options (body, right)
		TiposDocumentos_list.ListOptions.Render("body", "right");
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
if (TiposDocumentos_list.Recordset != null) {
	TiposDocumentos_list.Recordset.Close();
	TiposDocumentos_list.Recordset.Dispose();
}
%>
<% if (TiposDocumentos_list.TotalRecs > 0) { %>
<% if (ew_Empty(TiposDocumentos.Export)) { %>
<div class="ewGridLowerPanel">
<% if (TiposDocumentos.CurrentAction != "gridadd" && TiposDocumentos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (TiposDocumentos_list.Pager == null) TiposDocumentos_list.Pager = new cNumericPager(TiposDocumentos_list.StartRec, TiposDocumentos_list.DisplayRecs, TiposDocumentos_list.TotalRecs, TiposDocumentos_list.RecRange); %>
<% if (TiposDocumentos_list.Pager.RecordCount > 0) { %>
	<% if (TiposDocumentos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= TiposDocumentos_list.PageUrl %>start=<%= TiposDocumentos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (TiposDocumentos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= TiposDocumentos_list.PageUrl %>start=<%= TiposDocumentos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in TiposDocumentos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= TiposDocumentos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (TiposDocumentos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= TiposDocumentos_list.PageUrl %>start=<%= TiposDocumentos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (TiposDocumentos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= TiposDocumentos_list.PageUrl %>start=<%= TiposDocumentos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (TiposDocumentos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= TiposDocumentos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= TiposDocumentos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= TiposDocumentos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (TiposDocumentos_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= TiposDocumentos_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(TiposDocumentos.Export) && ew_Empty(TiposDocumentos.CurrentAction)) { %>
<% } %>
<%
TiposDocumentos_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(TiposDocumentos.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
