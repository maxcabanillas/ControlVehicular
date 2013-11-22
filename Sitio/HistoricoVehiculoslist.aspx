<%@ Page ClassName="HistoricoVehiculoslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="HistoricoVehiculoslist.aspx.cs" Inherits="HistoricoVehiculoslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(HistoricoVehiculos.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var HistoricoVehiculos_list = new ew_Page("HistoricoVehiculos_list");

// page properties
HistoricoVehiculos_list.PageID = "list"; // page ID
HistoricoVehiculos_list.FormID = "fHistoricoVehiculoslist"; // form ID 
var EW_PAGE_ID = HistoricoVehiculos_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
HistoricoVehiculos_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
HistoricoVehiculos_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
HistoricoVehiculos_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(HistoricoVehiculos.Export) || (EW_EXPORT_MASTER_RECORD && HistoricoVehiculos.Export == "print")) { %>
<% } %>
<%
	HistoricoVehiculos_list.Recordset = HistoricoVehiculos_list.LoadRecordset();
	HistoricoVehiculos_list.StartRec = 1;
	if (HistoricoVehiculos_list.DisplayRecs <= 0) // Display all records
		HistoricoVehiculos_list.DisplayRecs = HistoricoVehiculos_list.TotalRecs;
	if (!(HistoricoVehiculos.ExportAll && ew_NotEmpty(HistoricoVehiculos.Export)))
		HistoricoVehiculos_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeCUSTOMVIEW") %><%= HistoricoVehiculos.TableCaption %>
&nbsp;&nbsp;<% HistoricoVehiculos_list.ExportOptions.Render("body"); %>
</p>
<% if (Security.CanSearch) { %>
<% if (ew_Empty(HistoricoVehiculos.Export) && ew_Empty(HistoricoVehiculos.CurrentAction)) { %>
<a href="javascript:ew_ToggleSearchPanel(HistoricoVehiculos_list);" style="text-decoration: none;"><img id="HistoricoVehiculos_list_SearchImage" src="aspximages/collapse.gif" alt="" width="9" height="9" border="0"></a><span class="aspnetmaker">&nbsp;<%= Language.Phrase("Search") %></span><br>
<div id="HistoricoVehiculos_list_SearchPanel">
<form name="fHistoricoVehiculoslistsrch" id="fHistoricoVehiculoslistsrch" class="ewForm">
<input type="hidden" id="t" name="t" value="HistoricoVehiculos" />
<div class="ewBasicSearch">
<div id="xsr_1" class="ewCssTableRow">
			<input type="text" name="<%= EW_TABLE_BASIC_SEARCH %>" id="<%= EW_TABLE_BASIC_SEARCH %>" size="20" value="<%= ew_HtmlEncode(HistoricoVehiculos.SessionBasicSearchKeyword) %>" />
			<input type="Submit" name="Submit" id="Submit" value="<%= ew_BtnCaption(Language.Phrase("QuickSearchBtn")) %>" />&nbsp;
			<a href="<%= HistoricoVehiculos_list.PageUrl %>cmd=reset"><%= Language.Phrase("ShowAll") %></a>&nbsp;
			<a href="HistoricoVehiculossrch.aspx"><%= Language.Phrase("AdvancedSearch") %></a>&nbsp;
</div>
<div id="xsr_2" class="ewCssTableRow">
	<label><input type="radio" name="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" id="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" value=""<% if (ew_Empty(HistoricoVehiculos.SessionBasicSearchType)) { %> checked="checked"<% } %> /><%= Language.Phrase("ExactPhrase") %></label>&nbsp;&nbsp;<label><input type="radio" name="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" id="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" value="AND"<% if (HistoricoVehiculos.SessionBasicSearchType == "AND") { %> checked="checked"<% } %> /><%= Language.Phrase("AllWord") %></label>&nbsp;&nbsp;<label><input type="radio" name="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" id="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" value="OR"<% if (HistoricoVehiculos.SessionBasicSearchType == "OR") { %> checked="checked"<% } %> /><%= Language.Phrase("AnyWord") %></label>
</div>
</div>
</form>
</div>
<% } %>
<% } %>
<% HistoricoVehiculos_list.ShowPageHeader(); %>
<% HistoricoVehiculos_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(HistoricoVehiculos.Export)) { %>
<div class="ewGridUpperPanel">
<% if (HistoricoVehiculos.CurrentAction != "gridadd" && HistoricoVehiculos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (HistoricoVehiculos_list.Pager == null) HistoricoVehiculos_list.Pager = new cNumericPager(HistoricoVehiculos_list.StartRec, HistoricoVehiculos_list.DisplayRecs, HistoricoVehiculos_list.TotalRecs, HistoricoVehiculos_list.RecRange); %>
<% if (HistoricoVehiculos_list.Pager.RecordCount > 0) { %>
	<% if (HistoricoVehiculos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= HistoricoVehiculos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (HistoricoVehiculos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= HistoricoVehiculos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in HistoricoVehiculos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (HistoricoVehiculos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= HistoricoVehiculos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (HistoricoVehiculos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= HistoricoVehiculos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (HistoricoVehiculos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= HistoricoVehiculos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= HistoricoVehiculos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= HistoricoVehiculos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (HistoricoVehiculos_list.SearchWhere == "0=101") { %>
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
</div>
</div>
<% } %>
<form name="fHistoricoVehiculoslist" id="fHistoricoVehiculoslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="HistoricoVehiculos" />
<div id="gmp_HistoricoVehiculos" class="ewGridMiddlePanel">
<% if (HistoricoVehiculos_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= HistoricoVehiculos.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		HistoricoVehiculos_list.RenderListOptions();

		// Render list options (header, left)
		HistoricoVehiculos_list.ListOptions.Render("header", "left");
%>
<% if (HistoricoVehiculos.TipoVehiculo.Visible) { // TipoVehiculo %>
	<% if (ew_Empty(HistoricoVehiculos.SortUrl(HistoricoVehiculos.TipoVehiculo))) { %>
		<td><%= HistoricoVehiculos.TipoVehiculo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoVehiculos.SortUrl(HistoricoVehiculos.TipoVehiculo) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoVehiculos.TipoVehiculo.FldCaption %></td><td style="width: 10px;"><% if (HistoricoVehiculos.TipoVehiculo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoVehiculos.TipoVehiculo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoVehiculos.Placa.Visible) { // Placa %>
	<% if (ew_Empty(HistoricoVehiculos.SortUrl(HistoricoVehiculos.Placa))) { %>
		<td><%= HistoricoVehiculos.Placa.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoVehiculos.SortUrl(HistoricoVehiculos.Placa) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoVehiculos.Placa.FldCaption %><%= Language.Phrase("SrchLegend") %></td><td style="width: 10px;"><% if (HistoricoVehiculos.Placa.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoVehiculos.Placa.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoVehiculos.FechaIngreso.Visible) { // FechaIngreso %>
	<% if (ew_Empty(HistoricoVehiculos.SortUrl(HistoricoVehiculos.FechaIngreso))) { %>
		<td><%= HistoricoVehiculos.FechaIngreso.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoVehiculos.SortUrl(HistoricoVehiculos.FechaIngreso) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoVehiculos.FechaIngreso.FldCaption %></td><td style="width: 10px;"><% if (HistoricoVehiculos.FechaIngreso.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoVehiculos.FechaIngreso.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoVehiculos.HoraIngreso.Visible) { // HoraIngreso %>
	<% if (ew_Empty(HistoricoVehiculos.SortUrl(HistoricoVehiculos.HoraIngreso))) { %>
		<td><%= HistoricoVehiculos.HoraIngreso.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoVehiculos.SortUrl(HistoricoVehiculos.HoraIngreso) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoVehiculos.HoraIngreso.FldCaption %><%= Language.Phrase("SrchLegend") %></td><td style="width: 10px;"><% if (HistoricoVehiculos.HoraIngreso.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoVehiculos.HoraIngreso.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoVehiculos.FechaSalida.Visible) { // FechaSalida %>
	<% if (ew_Empty(HistoricoVehiculos.SortUrl(HistoricoVehiculos.FechaSalida))) { %>
		<td><%= HistoricoVehiculos.FechaSalida.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoVehiculos.SortUrl(HistoricoVehiculos.FechaSalida) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoVehiculos.FechaSalida.FldCaption %></td><td style="width: 10px;"><% if (HistoricoVehiculos.FechaSalida.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoVehiculos.FechaSalida.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoVehiculos.HoraSalida.Visible) { // HoraSalida %>
	<% if (ew_Empty(HistoricoVehiculos.SortUrl(HistoricoVehiculos.HoraSalida))) { %>
		<td><%= HistoricoVehiculos.HoraSalida.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoVehiculos.SortUrl(HistoricoVehiculos.HoraSalida) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoVehiculos.HoraSalida.FldCaption %><%= Language.Phrase("SrchLegend") %></td><td style="width: 10px;"><% if (HistoricoVehiculos.HoraSalida.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoVehiculos.HoraSalida.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoVehiculos.Observaciones.Visible) { // Observaciones %>
	<% if (ew_Empty(HistoricoVehiculos.SortUrl(HistoricoVehiculos.Observaciones))) { %>
		<td><%= HistoricoVehiculos.Observaciones.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoVehiculos.SortUrl(HistoricoVehiculos.Observaciones) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoVehiculos.Observaciones.FldCaption %><%= Language.Phrase("SrchLegend") %></td><td style="width: 10px;"><% if (HistoricoVehiculos.Observaciones.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoVehiculos.Observaciones.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		HistoricoVehiculos_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (HistoricoVehiculos.ExportAll && ew_NotEmpty(HistoricoVehiculos.Export)) {
	HistoricoVehiculos_list.StopRec = HistoricoVehiculos_list.TotalRecs;
} else {

	// Set the last record to display
	if (HistoricoVehiculos_list.TotalRecs > HistoricoVehiculos_list.StartRec + HistoricoVehiculos_list.DisplayRecs - 1) {
		HistoricoVehiculos_list.StopRec = HistoricoVehiculos_list.StartRec + HistoricoVehiculos_list.DisplayRecs - 1;
	} else {
		HistoricoVehiculos_list.StopRec = HistoricoVehiculos_list.TotalRecs;
	}
}
if (HistoricoVehiculos_list.Recordset != null && HistoricoVehiculos_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= HistoricoVehiculos_list.StartRec - 1; i++) {
		if (HistoricoVehiculos_list.Recordset.Read())
			HistoricoVehiculos_list.RecCnt += 1;
	}		
} else if (!HistoricoVehiculos.AllowAddDeleteRow && HistoricoVehiculos_list.StopRec == 0) {
	HistoricoVehiculos_list.StopRec = HistoricoVehiculos.GridAddRowCount;
}

// Initialize Aggregate
HistoricoVehiculos.RowType = EW_ROWTYPE_AGGREGATEINIT;
HistoricoVehiculos.ResetAttrs();
HistoricoVehiculos_list.RenderRow();
HistoricoVehiculos_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (HistoricoVehiculos_list.RecCnt < HistoricoVehiculos_list.StopRec) {
	if (HistoricoVehiculos.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !HistoricoVehiculos_list.Recordset.Read();
	HistoricoVehiculos_list.RecCnt += 1;
	if (HistoricoVehiculos_list.RecCnt >= HistoricoVehiculos_list.StartRec) {
		HistoricoVehiculos_list.RowCnt += 1;

		// Set up key count
		HistoricoVehiculos_list.KeyCount = ew_ConvertToInt(HistoricoVehiculos_list.RowIndex);

		// Init row class and style
		HistoricoVehiculos.ResetAttrs();
		HistoricoVehiculos.CssClass = "";	 
		if (HistoricoVehiculos.CurrentAction == "gridadd") {
		} else {
			HistoricoVehiculos_list.LoadRowValues(ref HistoricoVehiculos_list.Recordset); // Load row values
		}
		HistoricoVehiculos.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref HistoricoVehiculos.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		HistoricoVehiculos_list.RenderRow();

		// Render list options
		HistoricoVehiculos_list.RenderListOptions();
%>
	<tr<%= HistoricoVehiculos.RowAttributes %>>
	<%

		// Render list options (body, left)
		HistoricoVehiculos_list.ListOptions.Render("body", "left");
	%>
	<% if (HistoricoVehiculos.TipoVehiculo.Visible) { // TipoVehiculo %>
		<td<%= HistoricoVehiculos.TipoVehiculo.CellAttributes %>>
<div<%= HistoricoVehiculos.TipoVehiculo.ViewAttributes %>><%= HistoricoVehiculos.TipoVehiculo.ListViewValue %></div>
<a name="<%= HistoricoVehiculos_list.PageObjName + "_row_" + HistoricoVehiculos_list.RowCnt %>" id="<%= HistoricoVehiculos_list.PageObjName + "_row_" + HistoricoVehiculos_list.RowCnt %>"></a></td>
	<% } %>
	<% if (HistoricoVehiculos.Placa.Visible) { // Placa %>
		<td<%= HistoricoVehiculos.Placa.CellAttributes %>>
<div<%= HistoricoVehiculos.Placa.ViewAttributes %>><%= HistoricoVehiculos.Placa.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoVehiculos.FechaIngreso.Visible) { // FechaIngreso %>
		<td<%= HistoricoVehiculos.FechaIngreso.CellAttributes %>>
<div<%= HistoricoVehiculos.FechaIngreso.ViewAttributes %>><%= HistoricoVehiculos.FechaIngreso.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoVehiculos.HoraIngreso.Visible) { // HoraIngreso %>
		<td<%= HistoricoVehiculos.HoraIngreso.CellAttributes %>>
<div<%= HistoricoVehiculos.HoraIngreso.ViewAttributes %>><%= HistoricoVehiculos.HoraIngreso.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoVehiculos.FechaSalida.Visible) { // FechaSalida %>
		<td<%= HistoricoVehiculos.FechaSalida.CellAttributes %>>
<div<%= HistoricoVehiculos.FechaSalida.ViewAttributes %>><%= HistoricoVehiculos.FechaSalida.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoVehiculos.HoraSalida.Visible) { // HoraSalida %>
		<td<%= HistoricoVehiculos.HoraSalida.CellAttributes %>>
<div<%= HistoricoVehiculos.HoraSalida.ViewAttributes %>><%= HistoricoVehiculos.HoraSalida.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoVehiculos.Observaciones.Visible) { // Observaciones %>
		<td<%= HistoricoVehiculos.Observaciones.CellAttributes %>>
<div<%= HistoricoVehiculos.Observaciones.ViewAttributes %>><%= HistoricoVehiculos.Observaciones.ListViewValue %></div>
</td>
	<% } %>
<%

		// Render list options (body, right)
		HistoricoVehiculos_list.ListOptions.Render("body", "right");
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
if (HistoricoVehiculos_list.Recordset != null) {
	HistoricoVehiculos_list.Recordset.Close();
	HistoricoVehiculos_list.Recordset.Dispose();
}
%>
<% if (HistoricoVehiculos_list.TotalRecs > 0) { %>
<% if (ew_Empty(HistoricoVehiculos.Export)) { %>
<div class="ewGridLowerPanel">
<% if (HistoricoVehiculos.CurrentAction != "gridadd" && HistoricoVehiculos.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (HistoricoVehiculos_list.Pager == null) HistoricoVehiculos_list.Pager = new cNumericPager(HistoricoVehiculos_list.StartRec, HistoricoVehiculos_list.DisplayRecs, HistoricoVehiculos_list.TotalRecs, HistoricoVehiculos_list.RecRange); %>
<% if (HistoricoVehiculos_list.Pager.RecordCount > 0) { %>
	<% if (HistoricoVehiculos_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= HistoricoVehiculos_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (HistoricoVehiculos_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= HistoricoVehiculos_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in HistoricoVehiculos_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (HistoricoVehiculos_list.Pager.NextButton.Enabled) { %>
	<a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= HistoricoVehiculos_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (HistoricoVehiculos_list.Pager.LastButton.Enabled) { %>
	<a href="<%= HistoricoVehiculos_list.PageUrl %>start=<%= HistoricoVehiculos_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (HistoricoVehiculos_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= HistoricoVehiculos_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= HistoricoVehiculos_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= HistoricoVehiculos_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (HistoricoVehiculos_list.SearchWhere == "0=101") { %>
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
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(HistoricoVehiculos.Export) && ew_Empty(HistoricoVehiculos.CurrentAction)) { %>
<% } %>
<%
HistoricoVehiculos_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(HistoricoVehiculos.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
