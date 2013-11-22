<%@ Page ClassName="HistoricoPeatoneslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="HistoricoPeatoneslist.aspx.cs" Inherits="HistoricoPeatoneslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(HistoricoPeatones.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var HistoricoPeatones_list = new ew_Page("HistoricoPeatones_list");

// page properties
HistoricoPeatones_list.PageID = "list"; // page ID
HistoricoPeatones_list.FormID = "fHistoricoPeatoneslist"; // form ID 
var EW_PAGE_ID = HistoricoPeatones_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
HistoricoPeatones_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
HistoricoPeatones_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
HistoricoPeatones_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(HistoricoPeatones.Export) || (EW_EXPORT_MASTER_RECORD && HistoricoPeatones.Export == "print")) { %>
<% } %>
<%
	HistoricoPeatones_list.Recordset = HistoricoPeatones_list.LoadRecordset();
	HistoricoPeatones_list.StartRec = 1;
	if (HistoricoPeatones_list.DisplayRecs <= 0) // Display all records
		HistoricoPeatones_list.DisplayRecs = HistoricoPeatones_list.TotalRecs;
	if (!(HistoricoPeatones.ExportAll && ew_NotEmpty(HistoricoPeatones.Export)))
		HistoricoPeatones_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeCUSTOMVIEW") %><%= HistoricoPeatones.TableCaption %>
&nbsp;&nbsp;<% HistoricoPeatones_list.ExportOptions.Render("body"); %>
</p>
<% if (Security.CanSearch) { %>
<% if (ew_Empty(HistoricoPeatones.Export) && ew_Empty(HistoricoPeatones.CurrentAction)) { %>
<a href="javascript:ew_ToggleSearchPanel(HistoricoPeatones_list);" style="text-decoration: none;"><img id="HistoricoPeatones_list_SearchImage" src="aspximages/collapse.gif" alt="" width="9" height="9" border="0"></a><span class="aspnetmaker">&nbsp;<%= Language.Phrase("Search") %></span><br>
<div id="HistoricoPeatones_list_SearchPanel">
<form name="fHistoricoPeatoneslistsrch" id="fHistoricoPeatoneslistsrch" class="ewForm">
<input type="hidden" id="t" name="t" value="HistoricoPeatones" />
<div class="ewBasicSearch">
<div id="xsr_1" class="ewCssTableRow">
			<input type="text" name="<%= EW_TABLE_BASIC_SEARCH %>" id="<%= EW_TABLE_BASIC_SEARCH %>" size="20" value="<%= ew_HtmlEncode(HistoricoPeatones.SessionBasicSearchKeyword) %>" />
			<input type="Submit" name="Submit" id="Submit" value="<%= ew_BtnCaption(Language.Phrase("QuickSearchBtn")) %>" />&nbsp;
			<a href="<%= HistoricoPeatones_list.PageUrl %>cmd=reset"><%= Language.Phrase("ShowAll") %></a>&nbsp;
			<a href="HistoricoPeatonessrch.aspx"><%= Language.Phrase("AdvancedSearch") %></a>&nbsp;
</div>
<div id="xsr_2" class="ewCssTableRow">
	<label><input type="radio" name="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" id="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" value=""<% if (ew_Empty(HistoricoPeatones.SessionBasicSearchType)) { %> checked="checked"<% } %> /><%= Language.Phrase("ExactPhrase") %></label>&nbsp;&nbsp;<label><input type="radio" name="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" id="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" value="AND"<% if (HistoricoPeatones.SessionBasicSearchType == "AND") { %> checked="checked"<% } %> /><%= Language.Phrase("AllWord") %></label>&nbsp;&nbsp;<label><input type="radio" name="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" id="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" value="OR"<% if (HistoricoPeatones.SessionBasicSearchType == "OR") { %> checked="checked"<% } %> /><%= Language.Phrase("AnyWord") %></label>
</div>
</div>
</form>
</div>
<% } %>
<% } %>
<% HistoricoPeatones_list.ShowPageHeader(); %>
<% HistoricoPeatones_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(HistoricoPeatones.Export)) { %>
<div class="ewGridUpperPanel">
<% if (HistoricoPeatones.CurrentAction != "gridadd" && HistoricoPeatones.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (HistoricoPeatones_list.Pager == null) HistoricoPeatones_list.Pager = new cNumericPager(HistoricoPeatones_list.StartRec, HistoricoPeatones_list.DisplayRecs, HistoricoPeatones_list.TotalRecs, HistoricoPeatones_list.RecRange); %>
<% if (HistoricoPeatones_list.Pager.RecordCount > 0) { %>
	<% if (HistoricoPeatones_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= HistoricoPeatones_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (HistoricoPeatones_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= HistoricoPeatones_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in HistoricoPeatones_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (HistoricoPeatones_list.Pager.NextButton.Enabled) { %>
	<a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= HistoricoPeatones_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (HistoricoPeatones_list.Pager.LastButton.Enabled) { %>
	<a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= HistoricoPeatones_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (HistoricoPeatones_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= HistoricoPeatones_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= HistoricoPeatones_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= HistoricoPeatones_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (HistoricoPeatones_list.SearchWhere == "0=101") { %>
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
<form name="fHistoricoPeatoneslist" id="fHistoricoPeatoneslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="HistoricoPeatones" />
<div id="gmp_HistoricoPeatones" class="ewGridMiddlePanel">
<% if (HistoricoPeatones_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= HistoricoPeatones.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		HistoricoPeatones_list.RenderListOptions();

		// Render list options (header, left)
		HistoricoPeatones_list.ListOptions.Render("header", "left");
%>
<% if (HistoricoPeatones.TipoDocumento.Visible) { // TipoDocumento %>
	<% if (ew_Empty(HistoricoPeatones.SortUrl(HistoricoPeatones.TipoDocumento))) { %>
		<td><%= HistoricoPeatones.TipoDocumento.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoPeatones.SortUrl(HistoricoPeatones.TipoDocumento) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoPeatones.TipoDocumento.FldCaption %></td><td style="width: 10px;"><% if (HistoricoPeatones.TipoDocumento.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoPeatones.TipoDocumento.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoPeatones.Documento.Visible) { // Documento %>
	<% if (ew_Empty(HistoricoPeatones.SortUrl(HistoricoPeatones.Documento))) { %>
		<td><%= HistoricoPeatones.Documento.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoPeatones.SortUrl(HistoricoPeatones.Documento) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoPeatones.Documento.FldCaption %><%= Language.Phrase("SrchLegend") %></td><td style="width: 10px;"><% if (HistoricoPeatones.Documento.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoPeatones.Documento.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoPeatones.Nombre.Visible) { // Nombre %>
	<% if (ew_Empty(HistoricoPeatones.SortUrl(HistoricoPeatones.Nombre))) { %>
		<td><%= HistoricoPeatones.Nombre.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoPeatones.SortUrl(HistoricoPeatones.Nombre) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoPeatones.Nombre.FldCaption %><%= Language.Phrase("SrchLegend") %></td><td style="width: 10px;"><% if (HistoricoPeatones.Nombre.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoPeatones.Nombre.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoPeatones.Apellidos.Visible) { // Apellidos %>
	<% if (ew_Empty(HistoricoPeatones.SortUrl(HistoricoPeatones.Apellidos))) { %>
		<td><%= HistoricoPeatones.Apellidos.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoPeatones.SortUrl(HistoricoPeatones.Apellidos) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoPeatones.Apellidos.FldCaption %><%= Language.Phrase("SrchLegend") %></td><td style="width: 10px;"><% if (HistoricoPeatones.Apellidos.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoPeatones.Apellidos.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoPeatones.Area.Visible) { // Area %>
	<% if (ew_Empty(HistoricoPeatones.SortUrl(HistoricoPeatones.Area))) { %>
		<td><%= HistoricoPeatones.Area.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoPeatones.SortUrl(HistoricoPeatones.Area) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoPeatones.Area.FldCaption %></td><td style="width: 10px;"><% if (HistoricoPeatones.Area.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoPeatones.Area.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoPeatones.Persona.Visible) { // Persona %>
	<% if (ew_Empty(HistoricoPeatones.SortUrl(HistoricoPeatones.Persona))) { %>
		<td><%= HistoricoPeatones.Persona.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoPeatones.SortUrl(HistoricoPeatones.Persona) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoPeatones.Persona.FldCaption %></td><td style="width: 10px;"><% if (HistoricoPeatones.Persona.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoPeatones.Persona.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoPeatones.FechaIngreso.Visible) { // FechaIngreso %>
	<% if (ew_Empty(HistoricoPeatones.SortUrl(HistoricoPeatones.FechaIngreso))) { %>
		<td><%= HistoricoPeatones.FechaIngreso.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoPeatones.SortUrl(HistoricoPeatones.FechaIngreso) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoPeatones.FechaIngreso.FldCaption %></td><td style="width: 10px;"><% if (HistoricoPeatones.FechaIngreso.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoPeatones.FechaIngreso.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (HistoricoPeatones.FechaSalida.Visible) { // FechaSalida %>
	<% if (ew_Empty(HistoricoPeatones.SortUrl(HistoricoPeatones.FechaSalida))) { %>
		<td><%= HistoricoPeatones.FechaSalida.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= HistoricoPeatones.SortUrl(HistoricoPeatones.FechaSalida) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= HistoricoPeatones.FechaSalida.FldCaption %></td><td style="width: 10px;"><% if (HistoricoPeatones.FechaSalida.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (HistoricoPeatones.FechaSalida.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		HistoricoPeatones_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (HistoricoPeatones.ExportAll && ew_NotEmpty(HistoricoPeatones.Export)) {
	HistoricoPeatones_list.StopRec = HistoricoPeatones_list.TotalRecs;
} else {

	// Set the last record to display
	if (HistoricoPeatones_list.TotalRecs > HistoricoPeatones_list.StartRec + HistoricoPeatones_list.DisplayRecs - 1) {
		HistoricoPeatones_list.StopRec = HistoricoPeatones_list.StartRec + HistoricoPeatones_list.DisplayRecs - 1;
	} else {
		HistoricoPeatones_list.StopRec = HistoricoPeatones_list.TotalRecs;
	}
}
if (HistoricoPeatones_list.Recordset != null && HistoricoPeatones_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= HistoricoPeatones_list.StartRec - 1; i++) {
		if (HistoricoPeatones_list.Recordset.Read())
			HistoricoPeatones_list.RecCnt += 1;
	}		
} else if (!HistoricoPeatones.AllowAddDeleteRow && HistoricoPeatones_list.StopRec == 0) {
	HistoricoPeatones_list.StopRec = HistoricoPeatones.GridAddRowCount;
}

// Initialize Aggregate
HistoricoPeatones.RowType = EW_ROWTYPE_AGGREGATEINIT;
HistoricoPeatones.ResetAttrs();
HistoricoPeatones_list.RenderRow();
HistoricoPeatones_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (HistoricoPeatones_list.RecCnt < HistoricoPeatones_list.StopRec) {
	if (HistoricoPeatones.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !HistoricoPeatones_list.Recordset.Read();
	HistoricoPeatones_list.RecCnt += 1;
	if (HistoricoPeatones_list.RecCnt >= HistoricoPeatones_list.StartRec) {
		HistoricoPeatones_list.RowCnt += 1;

		// Set up key count
		HistoricoPeatones_list.KeyCount = ew_ConvertToInt(HistoricoPeatones_list.RowIndex);

		// Init row class and style
		HistoricoPeatones.ResetAttrs();
		HistoricoPeatones.CssClass = "";	 
		if (HistoricoPeatones.CurrentAction == "gridadd") {
		} else {
			HistoricoPeatones_list.LoadRowValues(ref HistoricoPeatones_list.Recordset); // Load row values
		}
		HistoricoPeatones.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref HistoricoPeatones.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		HistoricoPeatones_list.RenderRow();

		// Render list options
		HistoricoPeatones_list.RenderListOptions();
%>
	<tr<%= HistoricoPeatones.RowAttributes %>>
	<%

		// Render list options (body, left)
		HistoricoPeatones_list.ListOptions.Render("body", "left");
	%>
	<% if (HistoricoPeatones.TipoDocumento.Visible) { // TipoDocumento %>
		<td<%= HistoricoPeatones.TipoDocumento.CellAttributes %>>
<div<%= HistoricoPeatones.TipoDocumento.ViewAttributes %>><%= HistoricoPeatones.TipoDocumento.ListViewValue %></div>
<a name="<%= HistoricoPeatones_list.PageObjName + "_row_" + HistoricoPeatones_list.RowCnt %>" id="<%= HistoricoPeatones_list.PageObjName + "_row_" + HistoricoPeatones_list.RowCnt %>"></a></td>
	<% } %>
	<% if (HistoricoPeatones.Documento.Visible) { // Documento %>
		<td<%= HistoricoPeatones.Documento.CellAttributes %>>
<div<%= HistoricoPeatones.Documento.ViewAttributes %>><%= HistoricoPeatones.Documento.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoPeatones.Nombre.Visible) { // Nombre %>
		<td<%= HistoricoPeatones.Nombre.CellAttributes %>>
<div<%= HistoricoPeatones.Nombre.ViewAttributes %>><%= HistoricoPeatones.Nombre.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoPeatones.Apellidos.Visible) { // Apellidos %>
		<td<%= HistoricoPeatones.Apellidos.CellAttributes %>>
<div<%= HistoricoPeatones.Apellidos.ViewAttributes %>><%= HistoricoPeatones.Apellidos.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoPeatones.Area.Visible) { // Area %>
		<td<%= HistoricoPeatones.Area.CellAttributes %>>
<div<%= HistoricoPeatones.Area.ViewAttributes %>><%= HistoricoPeatones.Area.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoPeatones.Persona.Visible) { // Persona %>
		<td<%= HistoricoPeatones.Persona.CellAttributes %>>
<div<%= HistoricoPeatones.Persona.ViewAttributes %>><%= HistoricoPeatones.Persona.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoPeatones.FechaIngreso.Visible) { // FechaIngreso %>
		<td<%= HistoricoPeatones.FechaIngreso.CellAttributes %>>
<div<%= HistoricoPeatones.FechaIngreso.ViewAttributes %>><%= HistoricoPeatones.FechaIngreso.ListViewValue %></div>
</td>
	<% } %>
	<% if (HistoricoPeatones.FechaSalida.Visible) { // FechaSalida %>
		<td<%= HistoricoPeatones.FechaSalida.CellAttributes %>>
<div<%= HistoricoPeatones.FechaSalida.ViewAttributes %>><%= HistoricoPeatones.FechaSalida.ListViewValue %></div>
</td>
	<% } %>
<%

		// Render list options (body, right)
		HistoricoPeatones_list.ListOptions.Render("body", "right");
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
if (HistoricoPeatones_list.Recordset != null) {
	HistoricoPeatones_list.Recordset.Close();
	HistoricoPeatones_list.Recordset.Dispose();
}
%>
<% if (HistoricoPeatones_list.TotalRecs > 0) { %>
<% if (ew_Empty(HistoricoPeatones.Export)) { %>
<div class="ewGridLowerPanel">
<% if (HistoricoPeatones.CurrentAction != "gridadd" && HistoricoPeatones.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (HistoricoPeatones_list.Pager == null) HistoricoPeatones_list.Pager = new cNumericPager(HistoricoPeatones_list.StartRec, HistoricoPeatones_list.DisplayRecs, HistoricoPeatones_list.TotalRecs, HistoricoPeatones_list.RecRange); %>
<% if (HistoricoPeatones_list.Pager.RecordCount > 0) { %>
	<% if (HistoricoPeatones_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= HistoricoPeatones_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (HistoricoPeatones_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= HistoricoPeatones_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in HistoricoPeatones_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (HistoricoPeatones_list.Pager.NextButton.Enabled) { %>
	<a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= HistoricoPeatones_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (HistoricoPeatones_list.Pager.LastButton.Enabled) { %>
	<a href="<%= HistoricoPeatones_list.PageUrl %>start=<%= HistoricoPeatones_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (HistoricoPeatones_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= HistoricoPeatones_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= HistoricoPeatones_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= HistoricoPeatones_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (HistoricoPeatones_list.SearchWhere == "0=101") { %>
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
<% if (ew_Empty(HistoricoPeatones.Export) && ew_Empty(HistoricoPeatones.CurrentAction)) { %>
<% } %>
<%
HistoricoPeatones_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(HistoricoPeatones.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
