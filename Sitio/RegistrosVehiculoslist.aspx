<%@ Page ClassName="RegistrosVehiculoslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="RegistrosVehiculoslist.aspx.cs" Inherits="RegistrosVehiculoslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(RegistrosVehiculos.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var RegistrosVehiculos_list = new ew_Page("RegistrosVehiculos_list");

// page properties
RegistrosVehiculos_list.PageID = "list"; // page ID
RegistrosVehiculos_list.FormID = "fRegistrosVehiculoslist"; // form ID 
var EW_PAGE_ID = RegistrosVehiculos_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
RegistrosVehiculos_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosVehiculos_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosVehiculos_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(RegistrosVehiculos.Export) || (EW_EXPORT_MASTER_RECORD && RegistrosVehiculos.Export == "print")) { %>
<% } %>
<%
	RegistrosVehiculos_list.Recordset = RegistrosVehiculos_list.LoadRecordset();
	RegistrosVehiculos_list.StartRec = 1;
	if (RegistrosVehiculos_list.DisplayRecs <= 0) // Display all records
		RegistrosVehiculos_list.DisplayRecs = RegistrosVehiculos_list.TotalRecs;
	if (!(RegistrosVehiculos.ExportAll && ew_NotEmpty(RegistrosVehiculos.Export)))
		RegistrosVehiculos_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= RegistrosVehiculos.TableCaption %>
&nbsp;&nbsp;<% RegistrosVehiculos_list.ExportOptions.Render("body"); %>
</p>
<% RegistrosVehiculos_list.ShowPageHeader(); %>
<% RegistrosVehiculos_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(RegistrosVehiculos.Export)) { %>
<div class="ewGridUpperPanel">
<% if (RegistrosVehiculos.CurrentAction != "gridadd" && RegistrosVehiculos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (RegistrosVehiculos_list.Pager == null) RegistrosVehiculos_list.Pager = new cNumericPager(RegistrosVehiculos_list.StartRec, RegistrosVehiculos_list.DisplayRecs, RegistrosVehiculos_list.TotalRecs, RegistrosVehiculos_list.RecRange); %>
<% if (RegistrosVehiculos_list.Pager.RecordCount > 0) { %>
	<% if (RegistrosVehiculos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= RegistrosVehiculos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (RegistrosVehiculos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= RegistrosVehiculos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in RegistrosVehiculos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (RegistrosVehiculos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= RegistrosVehiculos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (RegistrosVehiculos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= RegistrosVehiculos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (RegistrosVehiculos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= RegistrosVehiculos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= RegistrosVehiculos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= RegistrosVehiculos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (RegistrosVehiculos_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= RegistrosVehiculos_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<form name="fRegistrosVehiculoslist" id="fRegistrosVehiculoslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="RegistrosVehiculos" />
<div id="gmp_RegistrosVehiculos" class="ewGridMiddlePanel">
<% if (RegistrosVehiculos_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= RegistrosVehiculos.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		RegistrosVehiculos_list.RenderListOptions();

		// Render list options (header, left)
		RegistrosVehiculos_list.ListOptions.Render("header", "left");
%>
<% if (RegistrosVehiculos.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
	<% if (ew_Empty(RegistrosVehiculos.SortUrl(RegistrosVehiculos.IdTipoVehiculo))) { %>
		<td><%= RegistrosVehiculos.IdTipoVehiculo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosVehiculos.SortUrl(RegistrosVehiculos.IdTipoVehiculo) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosVehiculos.IdTipoVehiculo.FldCaption %></td><td style="width: 10px;"><% if (RegistrosVehiculos.IdTipoVehiculo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosVehiculos.IdTipoVehiculo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosVehiculos.Placa.Visible) { // Placa %>
	<% if (ew_Empty(RegistrosVehiculos.SortUrl(RegistrosVehiculos.Placa))) { %>
		<td><%= RegistrosVehiculos.Placa.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosVehiculos.SortUrl(RegistrosVehiculos.Placa) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosVehiculos.Placa.FldCaption %></td><td style="width: 10px;"><% if (RegistrosVehiculos.Placa.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosVehiculos.Placa.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosVehiculos.FechaIngreso.Visible) { // FechaIngreso %>
	<% if (ew_Empty(RegistrosVehiculos.SortUrl(RegistrosVehiculos.FechaIngreso))) { %>
		<td><%= RegistrosVehiculos.FechaIngreso.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosVehiculos.SortUrl(RegistrosVehiculos.FechaIngreso) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosVehiculos.FechaIngreso.FldCaption %></td><td style="width: 10px;"><% if (RegistrosVehiculos.FechaIngreso.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosVehiculos.FechaIngreso.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosVehiculos.FechaSalida.Visible) { // FechaSalida %>
	<% if (ew_Empty(RegistrosVehiculos.SortUrl(RegistrosVehiculos.FechaSalida))) { %>
		<td><%= RegistrosVehiculos.FechaSalida.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosVehiculos.SortUrl(RegistrosVehiculos.FechaSalida) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosVehiculos.FechaSalida.FldCaption %></td><td style="width: 10px;"><% if (RegistrosVehiculos.FechaSalida.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosVehiculos.FechaSalida.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		RegistrosVehiculos_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (RegistrosVehiculos.ExportAll && ew_NotEmpty(RegistrosVehiculos.Export)) {
	RegistrosVehiculos_list.StopRec = RegistrosVehiculos_list.TotalRecs;
} else {

	// Set the last record to display
	if (RegistrosVehiculos_list.TotalRecs > RegistrosVehiculos_list.StartRec + RegistrosVehiculos_list.DisplayRecs - 1) {
		RegistrosVehiculos_list.StopRec = RegistrosVehiculos_list.StartRec + RegistrosVehiculos_list.DisplayRecs - 1;
	} else {
		RegistrosVehiculos_list.StopRec = RegistrosVehiculos_list.TotalRecs;
	}
}
if (RegistrosVehiculos_list.Recordset != null && RegistrosVehiculos_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= RegistrosVehiculos_list.StartRec - 1; i++) {
		if (RegistrosVehiculos_list.Recordset.Read())
			RegistrosVehiculos_list.RecCnt += 1;
	}		
} else if (!RegistrosVehiculos.AllowAddDeleteRow && RegistrosVehiculos_list.StopRec == 0) {
	RegistrosVehiculos_list.StopRec = RegistrosVehiculos.GridAddRowCount;
}

// Initialize Aggregate
RegistrosVehiculos.RowType = EW_ROWTYPE_AGGREGATEINIT;
RegistrosVehiculos.ResetAttrs();
RegistrosVehiculos_list.RenderRow();
RegistrosVehiculos_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (RegistrosVehiculos_list.RecCnt < RegistrosVehiculos_list.StopRec) {
	if (RegistrosVehiculos.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !RegistrosVehiculos_list.Recordset.Read();
	RegistrosVehiculos_list.RecCnt += 1;
	if (RegistrosVehiculos_list.RecCnt >= RegistrosVehiculos_list.StartRec) {
		RegistrosVehiculos_list.RowCnt += 1;

		// Set up key count
		RegistrosVehiculos_list.KeyCount = ew_ConvertToInt(RegistrosVehiculos_list.RowIndex);

		// Init row class and style
		RegistrosVehiculos.ResetAttrs();
		RegistrosVehiculos.CssClass = "";	 
		if (RegistrosVehiculos.CurrentAction == "gridadd") {
		} else {
			RegistrosVehiculos_list.LoadRowValues(ref RegistrosVehiculos_list.Recordset); // Load row values
		}
		RegistrosVehiculos.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref RegistrosVehiculos.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		RegistrosVehiculos_list.RenderRow();

		// Render list options
		RegistrosVehiculos_list.RenderListOptions();
%>
	<tr<%= RegistrosVehiculos.RowAttributes %>>
	<%

		// Render list options (body, left)
		RegistrosVehiculos_list.ListOptions.Render("body", "left");
	%>
	<% if (RegistrosVehiculos.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
		<td<%= RegistrosVehiculos.IdTipoVehiculo.CellAttributes %>>
<div<%= RegistrosVehiculos.IdTipoVehiculo.ViewAttributes %>><%= RegistrosVehiculos.IdTipoVehiculo.ListViewValue %></div>
<a name="<%= RegistrosVehiculos_list.PageObjName + "_row_" + RegistrosVehiculos_list.RowCnt %>" id="<%= RegistrosVehiculos_list.PageObjName + "_row_" + RegistrosVehiculos_list.RowCnt %>"></a></td>
	<% } %>
	<% if (RegistrosVehiculos.Placa.Visible) { // Placa %>
		<td<%= RegistrosVehiculos.Placa.CellAttributes %>>
<div<%= RegistrosVehiculos.Placa.ViewAttributes %>><%= RegistrosVehiculos.Placa.ListViewValue %></div>
</td>
	<% } %>
	<% if (RegistrosVehiculos.FechaIngreso.Visible) { // FechaIngreso %>
		<td<%= RegistrosVehiculos.FechaIngreso.CellAttributes %>>
<div<%= RegistrosVehiculos.FechaIngreso.ViewAttributes %>><%= RegistrosVehiculos.FechaIngreso.ListViewValue %></div>
</td>
	<% } %>
	<% if (RegistrosVehiculos.FechaSalida.Visible) { // FechaSalida %>
		<td<%= RegistrosVehiculos.FechaSalida.CellAttributes %>>
<div<%= RegistrosVehiculos.FechaSalida.ViewAttributes %>><%= RegistrosVehiculos.FechaSalida.ListViewValue %></div>
</td>
	<% } %>
<%

		// Render list options (body, right)
		RegistrosVehiculos_list.ListOptions.Render("body", "right");
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
if (RegistrosVehiculos_list.Recordset != null) {
	RegistrosVehiculos_list.Recordset.Close();
	RegistrosVehiculos_list.Recordset.Dispose();
}
%>
<% if (RegistrosVehiculos_list.TotalRecs > 0) { %>
<% if (ew_Empty(RegistrosVehiculos.Export)) { %>
<div class="ewGridLowerPanel">
<% if (RegistrosVehiculos.CurrentAction != "gridadd" && RegistrosVehiculos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (RegistrosVehiculos_list.Pager == null) RegistrosVehiculos_list.Pager = new cNumericPager(RegistrosVehiculos_list.StartRec, RegistrosVehiculos_list.DisplayRecs, RegistrosVehiculos_list.TotalRecs, RegistrosVehiculos_list.RecRange); %>
<% if (RegistrosVehiculos_list.Pager.RecordCount > 0) { %>
	<% if (RegistrosVehiculos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= RegistrosVehiculos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (RegistrosVehiculos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= RegistrosVehiculos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in RegistrosVehiculos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (RegistrosVehiculos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= RegistrosVehiculos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (RegistrosVehiculos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= RegistrosVehiculos_list.PageUrl %>start=<%= RegistrosVehiculos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (RegistrosVehiculos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= RegistrosVehiculos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= RegistrosVehiculos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= RegistrosVehiculos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (RegistrosVehiculos_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= RegistrosVehiculos_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(RegistrosVehiculos.Export) && ew_Empty(RegistrosVehiculos.CurrentAction)) { %>
<script type="text/javascript">
<!--
ew_ToggleSearchPanel(RegistrosVehiculos_list); // Init search panel as collapsed 

//-->
</script>
<% } %>
<%
RegistrosVehiculos_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(RegistrosVehiculos.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
