<%@ Page ClassName="TiposVehiculoslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="TiposVehiculoslist.aspx.cs" Inherits="TiposVehiculoslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(TiposVehiculos.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var TiposVehiculos_list = new ew_Page("TiposVehiculos_list");

// page properties
TiposVehiculos_list.PageID = "list"; // page ID
TiposVehiculos_list.FormID = "fTiposVehiculoslist"; // form ID 
var EW_PAGE_ID = TiposVehiculos_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
TiposVehiculos_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
TiposVehiculos_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
TiposVehiculos_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(TiposVehiculos.Export) || (EW_EXPORT_MASTER_RECORD && TiposVehiculos.Export == "print")) { %>
<% } %>
<%
	TiposVehiculos_list.Recordset = TiposVehiculos_list.LoadRecordset();
	TiposVehiculos_list.StartRec = 1;
	if (TiposVehiculos_list.DisplayRecs <= 0) // Display all records
		TiposVehiculos_list.DisplayRecs = TiposVehiculos_list.TotalRecs;
	if (!(TiposVehiculos.ExportAll && ew_NotEmpty(TiposVehiculos.Export)))
		TiposVehiculos_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= TiposVehiculos.TableCaption %>
&nbsp;&nbsp;<% TiposVehiculos_list.ExportOptions.Render("body"); %>
</p>
<% TiposVehiculos_list.ShowPageHeader(); %>
<% TiposVehiculos_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(TiposVehiculos.Export)) { %>
<div class="ewGridUpperPanel">
<% if (TiposVehiculos.CurrentAction != "gridadd" && TiposVehiculos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (TiposVehiculos_list.Pager == null) TiposVehiculos_list.Pager = new cNumericPager(TiposVehiculos_list.StartRec, TiposVehiculos_list.DisplayRecs, TiposVehiculos_list.TotalRecs, TiposVehiculos_list.RecRange); %>
<% if (TiposVehiculos_list.Pager.RecordCount > 0) { %>
	<% if (TiposVehiculos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= TiposVehiculos_list.PageUrl %>start=<%= TiposVehiculos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (TiposVehiculos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= TiposVehiculos_list.PageUrl %>start=<%= TiposVehiculos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in TiposVehiculos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= TiposVehiculos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (TiposVehiculos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= TiposVehiculos_list.PageUrl %>start=<%= TiposVehiculos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (TiposVehiculos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= TiposVehiculos_list.PageUrl %>start=<%= TiposVehiculos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (TiposVehiculos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= TiposVehiculos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= TiposVehiculos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= TiposVehiculos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (TiposVehiculos_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= TiposVehiculos_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<form name="fTiposVehiculoslist" id="fTiposVehiculoslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="TiposVehiculos" />
<div id="gmp_TiposVehiculos" class="ewGridMiddlePanel">
<% if (TiposVehiculos_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= TiposVehiculos.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		TiposVehiculos_list.RenderListOptions();

		// Render list options (header, left)
		TiposVehiculos_list.ListOptions.Render("header", "left");
%>
<% if (TiposVehiculos.TipoVehiculo.Visible) { // TipoVehiculo %>
	<% if (ew_Empty(TiposVehiculos.SortUrl(TiposVehiculos.TipoVehiculo))) { %>
		<td><%= TiposVehiculos.TipoVehiculo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= TiposVehiculos.SortUrl(TiposVehiculos.TipoVehiculo) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= TiposVehiculos.TipoVehiculo.FldCaption %></td><td style="width: 10px;"><% if (TiposVehiculos.TipoVehiculo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (TiposVehiculos.TipoVehiculo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		TiposVehiculos_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (TiposVehiculos.ExportAll && ew_NotEmpty(TiposVehiculos.Export)) {
	TiposVehiculos_list.StopRec = TiposVehiculos_list.TotalRecs;
} else {

	// Set the last record to display
	if (TiposVehiculos_list.TotalRecs > TiposVehiculos_list.StartRec + TiposVehiculos_list.DisplayRecs - 1) {
		TiposVehiculos_list.StopRec = TiposVehiculos_list.StartRec + TiposVehiculos_list.DisplayRecs - 1;
	} else {
		TiposVehiculos_list.StopRec = TiposVehiculos_list.TotalRecs;
	}
}
if (TiposVehiculos_list.Recordset != null && TiposVehiculos_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= TiposVehiculos_list.StartRec - 1; i++) {
		if (TiposVehiculos_list.Recordset.Read())
			TiposVehiculos_list.RecCnt += 1;
	}		
} else if (!TiposVehiculos.AllowAddDeleteRow && TiposVehiculos_list.StopRec == 0) {
	TiposVehiculos_list.StopRec = TiposVehiculos.GridAddRowCount;
}

// Initialize Aggregate
TiposVehiculos.RowType = EW_ROWTYPE_AGGREGATEINIT;
TiposVehiculos.ResetAttrs();
TiposVehiculos_list.RenderRow();
TiposVehiculos_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (TiposVehiculos_list.RecCnt < TiposVehiculos_list.StopRec) {
	if (TiposVehiculos.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !TiposVehiculos_list.Recordset.Read();
	TiposVehiculos_list.RecCnt += 1;
	if (TiposVehiculos_list.RecCnt >= TiposVehiculos_list.StartRec) {
		TiposVehiculos_list.RowCnt += 1;

		// Set up key count
		TiposVehiculos_list.KeyCount = ew_ConvertToInt(TiposVehiculos_list.RowIndex);

		// Init row class and style
		TiposVehiculos.ResetAttrs();
		TiposVehiculos.CssClass = "";	 
		if (TiposVehiculos.CurrentAction == "gridadd") {
		} else {
			TiposVehiculos_list.LoadRowValues(ref TiposVehiculos_list.Recordset); // Load row values
		}
		TiposVehiculos.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref TiposVehiculos.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		TiposVehiculos_list.RenderRow();

		// Render list options
		TiposVehiculos_list.RenderListOptions();
%>
	<tr<%= TiposVehiculos.RowAttributes %>>
	<%

		// Render list options (body, left)
		TiposVehiculos_list.ListOptions.Render("body", "left");
	%>
	<% if (TiposVehiculos.TipoVehiculo.Visible) { // TipoVehiculo %>
		<td<%= TiposVehiculos.TipoVehiculo.CellAttributes %>>
<div<%= TiposVehiculos.TipoVehiculo.ViewAttributes %>><%= TiposVehiculos.TipoVehiculo.ListViewValue %></div>
<a name="<%= TiposVehiculos_list.PageObjName + "_row_" + TiposVehiculos_list.RowCnt %>" id="<%= TiposVehiculos_list.PageObjName + "_row_" + TiposVehiculos_list.RowCnt %>"></a></td>
	<% } %>
<%

		// Render list options (body, right)
		TiposVehiculos_list.ListOptions.Render("body", "right");
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
if (TiposVehiculos_list.Recordset != null) {
	TiposVehiculos_list.Recordset.Close();
	TiposVehiculos_list.Recordset.Dispose();
}
%>
<% if (TiposVehiculos_list.TotalRecs > 0) { %>
<% if (ew_Empty(TiposVehiculos.Export)) { %>
<div class="ewGridLowerPanel">
<% if (TiposVehiculos.CurrentAction != "gridadd" && TiposVehiculos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (TiposVehiculos_list.Pager == null) TiposVehiculos_list.Pager = new cNumericPager(TiposVehiculos_list.StartRec, TiposVehiculos_list.DisplayRecs, TiposVehiculos_list.TotalRecs, TiposVehiculos_list.RecRange); %>
<% if (TiposVehiculos_list.Pager.RecordCount > 0) { %>
	<% if (TiposVehiculos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= TiposVehiculos_list.PageUrl %>start=<%= TiposVehiculos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (TiposVehiculos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= TiposVehiculos_list.PageUrl %>start=<%= TiposVehiculos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in TiposVehiculos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= TiposVehiculos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (TiposVehiculos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= TiposVehiculos_list.PageUrl %>start=<%= TiposVehiculos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (TiposVehiculos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= TiposVehiculos_list.PageUrl %>start=<%= TiposVehiculos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (TiposVehiculos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= TiposVehiculos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= TiposVehiculos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= TiposVehiculos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (TiposVehiculos_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= TiposVehiculos_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(TiposVehiculos.Export) && ew_Empty(TiposVehiculos.CurrentAction)) { %>
<% } %>
<%
TiposVehiculos_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(TiposVehiculos.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
