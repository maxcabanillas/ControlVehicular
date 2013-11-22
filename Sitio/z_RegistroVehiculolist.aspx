<%@ Page ClassName="z_RegistroVehiculolist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="z_RegistroVehiculolist.aspx.cs" Inherits="z_RegistroVehiculolist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(z_RegistroVehiculo.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var z_RegistroVehiculo_list = new ew_Page("z_RegistroVehiculo_list");

// page properties
z_RegistroVehiculo_list.PageID = "list"; // page ID
z_RegistroVehiculo_list.FormID = "fz_RegistroVehiculolist"; // form ID 
var EW_PAGE_ID = z_RegistroVehiculo_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
z_RegistroVehiculo_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
z_RegistroVehiculo_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
z_RegistroVehiculo_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(z_RegistroVehiculo.Export) || (EW_EXPORT_MASTER_RECORD && z_RegistroVehiculo.Export == "print")) { %>
<% } %>
<%
	z_RegistroVehiculo_list.Recordset = z_RegistroVehiculo_list.LoadRecordset();
	z_RegistroVehiculo_list.StartRec = 1;
	if (z_RegistroVehiculo_list.DisplayRecs <= 0) // Display all records
		z_RegistroVehiculo_list.DisplayRecs = z_RegistroVehiculo_list.TotalRecs;
	if (!(z_RegistroVehiculo.ExportAll && ew_NotEmpty(z_RegistroVehiculo.Export)))
		z_RegistroVehiculo_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeCUSTOMVIEW") %><%= z_RegistroVehiculo.TableCaption %>
&nbsp;&nbsp;<% z_RegistroVehiculo_list.ExportOptions.Render("body"); %>
</p>
<% z_RegistroVehiculo_list.ShowPageHeader(); %>
<% z_RegistroVehiculo_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(z_RegistroVehiculo.Export)) { %>
<div class="ewGridUpperPanel">
<% if (z_RegistroVehiculo.CurrentAction != "gridadd" && z_RegistroVehiculo.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (z_RegistroVehiculo_list.Pager == null) z_RegistroVehiculo_list.Pager = new cNumericPager(z_RegistroVehiculo_list.StartRec, z_RegistroVehiculo_list.DisplayRecs, z_RegistroVehiculo_list.TotalRecs, z_RegistroVehiculo_list.RecRange); %>
<% if (z_RegistroVehiculo_list.Pager.RecordCount > 0) { %>
	<% if (z_RegistroVehiculo_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= z_RegistroVehiculo_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroVehiculo_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= z_RegistroVehiculo_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in z_RegistroVehiculo_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (z_RegistroVehiculo_list.Pager.NextButton.Enabled) { %>
	<a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= z_RegistroVehiculo_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroVehiculo_list.Pager.LastButton.Enabled) { %>
	<a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= z_RegistroVehiculo_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroVehiculo_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= z_RegistroVehiculo_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= z_RegistroVehiculo_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= z_RegistroVehiculo_list.Pager.RecordCount %>
<% } else { %>
	<% if (z_RegistroVehiculo_list.SearchWhere == "0=101") { %>
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
<form name="fz_RegistroVehiculolist" id="fz_RegistroVehiculolist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="z_RegistroVehiculo" />
<div id="gmp_z_RegistroVehiculo" class="ewGridMiddlePanel">
<% if (z_RegistroVehiculo_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= z_RegistroVehiculo.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		z_RegistroVehiculo_list.RenderListOptions();

		// Render list options (header, left)
		z_RegistroVehiculo_list.ListOptions.Render("header", "left");
%>
<% if (z_RegistroVehiculo.Placa.Visible) { // Placa %>
	<% if (ew_Empty(z_RegistroVehiculo.SortUrl(z_RegistroVehiculo.Placa))) { %>
		<td><%= z_RegistroVehiculo.Placa.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= z_RegistroVehiculo.SortUrl(z_RegistroVehiculo.Placa) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= z_RegistroVehiculo.Placa.FldCaption %></td><td style="width: 10px;"><% if (z_RegistroVehiculo.Placa.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (z_RegistroVehiculo.Placa.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (z_RegistroVehiculo.FechaIngreso.Visible) { // FechaIngreso %>
	<% if (ew_Empty(z_RegistroVehiculo.SortUrl(z_RegistroVehiculo.FechaIngreso))) { %>
		<td><%= z_RegistroVehiculo.FechaIngreso.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= z_RegistroVehiculo.SortUrl(z_RegistroVehiculo.FechaIngreso) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= z_RegistroVehiculo.FechaIngreso.FldCaption %></td><td style="width: 10px;"><% if (z_RegistroVehiculo.FechaIngreso.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (z_RegistroVehiculo.FechaIngreso.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		z_RegistroVehiculo_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (z_RegistroVehiculo.ExportAll && ew_NotEmpty(z_RegistroVehiculo.Export)) {
	z_RegistroVehiculo_list.StopRec = z_RegistroVehiculo_list.TotalRecs;
} else {

	// Set the last record to display
	if (z_RegistroVehiculo_list.TotalRecs > z_RegistroVehiculo_list.StartRec + z_RegistroVehiculo_list.DisplayRecs - 1) {
		z_RegistroVehiculo_list.StopRec = z_RegistroVehiculo_list.StartRec + z_RegistroVehiculo_list.DisplayRecs - 1;
	} else {
		z_RegistroVehiculo_list.StopRec = z_RegistroVehiculo_list.TotalRecs;
	}
}
if (z_RegistroVehiculo_list.Recordset != null && z_RegistroVehiculo_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= z_RegistroVehiculo_list.StartRec - 1; i++) {
		if (z_RegistroVehiculo_list.Recordset.Read())
			z_RegistroVehiculo_list.RecCnt += 1;
	}		
} else if (!z_RegistroVehiculo.AllowAddDeleteRow && z_RegistroVehiculo_list.StopRec == 0) {
	z_RegistroVehiculo_list.StopRec = z_RegistroVehiculo.GridAddRowCount;
}

// Initialize Aggregate
z_RegistroVehiculo.RowType = EW_ROWTYPE_AGGREGATEINIT;
z_RegistroVehiculo.ResetAttrs();
z_RegistroVehiculo_list.RenderRow();
z_RegistroVehiculo_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (z_RegistroVehiculo_list.RecCnt < z_RegistroVehiculo_list.StopRec) {
	if (z_RegistroVehiculo.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !z_RegistroVehiculo_list.Recordset.Read();
	z_RegistroVehiculo_list.RecCnt += 1;
	if (z_RegistroVehiculo_list.RecCnt >= z_RegistroVehiculo_list.StartRec) {
		z_RegistroVehiculo_list.RowCnt += 1;

		// Set up key count
		z_RegistroVehiculo_list.KeyCount = ew_ConvertToInt(z_RegistroVehiculo_list.RowIndex);

		// Init row class and style
		z_RegistroVehiculo.ResetAttrs();
		z_RegistroVehiculo.CssClass = "";	 
		if (z_RegistroVehiculo.CurrentAction == "gridadd") {
		} else {
			z_RegistroVehiculo_list.LoadRowValues(ref z_RegistroVehiculo_list.Recordset); // Load row values
		}
		z_RegistroVehiculo.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref z_RegistroVehiculo.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		z_RegistroVehiculo_list.RenderRow();

		// Render list options
		z_RegistroVehiculo_list.RenderListOptions();
%>
	<tr<%= z_RegistroVehiculo.RowAttributes %>>
	<%

		// Render list options (body, left)
		z_RegistroVehiculo_list.ListOptions.Render("body", "left");
	%>
	<% if (z_RegistroVehiculo.Placa.Visible) { // Placa %>
		<td<%= z_RegistroVehiculo.Placa.CellAttributes %>>
<div<%= z_RegistroVehiculo.Placa.ViewAttributes %>><%= z_RegistroVehiculo.Placa.ListViewValue %></div>
<a name="<%= z_RegistroVehiculo_list.PageObjName + "_row_" + z_RegistroVehiculo_list.RowCnt %>" id="<%= z_RegistroVehiculo_list.PageObjName + "_row_" + z_RegistroVehiculo_list.RowCnt %>"></a></td>
	<% } %>
	<% if (z_RegistroVehiculo.FechaIngreso.Visible) { // FechaIngreso %>
		<td<%= z_RegistroVehiculo.FechaIngreso.CellAttributes %>>
<div<%= z_RegistroVehiculo.FechaIngreso.ViewAttributes %>><%= z_RegistroVehiculo.FechaIngreso.ListViewValue %></div>
</td>
	<% } %>
<%

		// Render list options (body, right)
		z_RegistroVehiculo_list.ListOptions.Render("body", "right");
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
if (z_RegistroVehiculo_list.Recordset != null) {
	z_RegistroVehiculo_list.Recordset.Close();
	z_RegistroVehiculo_list.Recordset.Dispose();
}
%>
<% if (z_RegistroVehiculo_list.TotalRecs > 0) { %>
<% if (ew_Empty(z_RegistroVehiculo.Export)) { %>
<div class="ewGridLowerPanel">
<% if (z_RegistroVehiculo.CurrentAction != "gridadd" && z_RegistroVehiculo.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (z_RegistroVehiculo_list.Pager == null) z_RegistroVehiculo_list.Pager = new cNumericPager(z_RegistroVehiculo_list.StartRec, z_RegistroVehiculo_list.DisplayRecs, z_RegistroVehiculo_list.TotalRecs, z_RegistroVehiculo_list.RecRange); %>
<% if (z_RegistroVehiculo_list.Pager.RecordCount > 0) { %>
	<% if (z_RegistroVehiculo_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= z_RegistroVehiculo_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroVehiculo_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= z_RegistroVehiculo_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in z_RegistroVehiculo_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (z_RegistroVehiculo_list.Pager.NextButton.Enabled) { %>
	<a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= z_RegistroVehiculo_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroVehiculo_list.Pager.LastButton.Enabled) { %>
	<a href="<%= z_RegistroVehiculo_list.PageUrl %>start=<%= z_RegistroVehiculo_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (z_RegistroVehiculo_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= z_RegistroVehiculo_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= z_RegistroVehiculo_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= z_RegistroVehiculo_list.Pager.RecordCount %>
<% } else { %>
	<% if (z_RegistroVehiculo_list.SearchWhere == "0=101") { %>
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
<% if (ew_Empty(z_RegistroVehiculo.Export) && ew_Empty(z_RegistroVehiculo.CurrentAction)) { %>
<% } %>
<%
z_RegistroVehiculo_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(z_RegistroVehiculo.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
