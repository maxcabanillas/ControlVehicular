<%@ Page ClassName="RegistrosPeatoneslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="RegistrosPeatoneslist.aspx.cs" Inherits="RegistrosPeatoneslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(RegistrosPeatones.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var RegistrosPeatones_list = new ew_Page("RegistrosPeatones_list");

// page properties
RegistrosPeatones_list.PageID = "list"; // page ID
RegistrosPeatones_list.FormID = "fRegistrosPeatoneslist"; // form ID 
var EW_PAGE_ID = RegistrosPeatones_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
RegistrosPeatones_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosPeatones_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosPeatones_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(RegistrosPeatones.Export) || (EW_EXPORT_MASTER_RECORD && RegistrosPeatones.Export == "print")) { %>
<% } %>
<%
	RegistrosPeatones_list.Recordset = RegistrosPeatones_list.LoadRecordset();
	RegistrosPeatones_list.StartRec = 1;
	if (RegistrosPeatones_list.DisplayRecs <= 0) // Display all records
		RegistrosPeatones_list.DisplayRecs = RegistrosPeatones_list.TotalRecs;
	if (!(RegistrosPeatones.ExportAll && ew_NotEmpty(RegistrosPeatones.Export)))
		RegistrosPeatones_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= RegistrosPeatones.TableCaption %>
&nbsp;&nbsp;<% RegistrosPeatones_list.ExportOptions.Render("body"); %>
</p>
<% RegistrosPeatones_list.ShowPageHeader(); %>
<% RegistrosPeatones_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(RegistrosPeatones.Export)) { %>
<div class="ewGridUpperPanel">
<% if (RegistrosPeatones.CurrentAction != "gridadd" && RegistrosPeatones.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (RegistrosPeatones_list.Pager == null) RegistrosPeatones_list.Pager = new cNumericPager(RegistrosPeatones_list.StartRec, RegistrosPeatones_list.DisplayRecs, RegistrosPeatones_list.TotalRecs, RegistrosPeatones_list.RecRange); %>
<% if (RegistrosPeatones_list.Pager.RecordCount > 0) { %>
	<% if (RegistrosPeatones_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= RegistrosPeatones_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (RegistrosPeatones_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= RegistrosPeatones_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in RegistrosPeatones_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (RegistrosPeatones_list.Pager.NextButton.Enabled) { %>
	<a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= RegistrosPeatones_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (RegistrosPeatones_list.Pager.LastButton.Enabled) { %>
	<a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= RegistrosPeatones_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (RegistrosPeatones_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= RegistrosPeatones_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= RegistrosPeatones_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= RegistrosPeatones_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (RegistrosPeatones_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= RegistrosPeatones_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<form name="fRegistrosPeatoneslist" id="fRegistrosPeatoneslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="RegistrosPeatones" />
<div id="gmp_RegistrosPeatones" class="ewGridMiddlePanel">
<% if (RegistrosPeatones_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= RegistrosPeatones.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		RegistrosPeatones_list.RenderListOptions();

		// Render list options (header, left)
		RegistrosPeatones_list.ListOptions.Render("header", "left");
%>
<% if (RegistrosPeatones.IdTipoDocumento.Visible) { // IdTipoDocumento %>
	<% if (ew_Empty(RegistrosPeatones.SortUrl(RegistrosPeatones.IdTipoDocumento))) { %>
		<td><%= RegistrosPeatones.IdTipoDocumento.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosPeatones.SortUrl(RegistrosPeatones.IdTipoDocumento) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosPeatones.IdTipoDocumento.FldCaption %></td><td style="width: 10px;"><% if (RegistrosPeatones.IdTipoDocumento.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosPeatones.IdTipoDocumento.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosPeatones.Documento.Visible) { // Documento %>
	<% if (ew_Empty(RegistrosPeatones.SortUrl(RegistrosPeatones.Documento))) { %>
		<td><%= RegistrosPeatones.Documento.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosPeatones.SortUrl(RegistrosPeatones.Documento) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosPeatones.Documento.FldCaption %></td><td style="width: 10px;"><% if (RegistrosPeatones.Documento.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosPeatones.Documento.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosPeatones.Nombre.Visible) { // Nombre %>
	<% if (ew_Empty(RegistrosPeatones.SortUrl(RegistrosPeatones.Nombre))) { %>
		<td><%= RegistrosPeatones.Nombre.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosPeatones.SortUrl(RegistrosPeatones.Nombre) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosPeatones.Nombre.FldCaption %></td><td style="width: 10px;"><% if (RegistrosPeatones.Nombre.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosPeatones.Nombre.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosPeatones.Apellidos.Visible) { // Apellidos %>
	<% if (ew_Empty(RegistrosPeatones.SortUrl(RegistrosPeatones.Apellidos))) { %>
		<td><%= RegistrosPeatones.Apellidos.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosPeatones.SortUrl(RegistrosPeatones.Apellidos) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosPeatones.Apellidos.FldCaption %></td><td style="width: 10px;"><% if (RegistrosPeatones.Apellidos.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosPeatones.Apellidos.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosPeatones.IdArea.Visible) { // IdArea %>
	<% if (ew_Empty(RegistrosPeatones.SortUrl(RegistrosPeatones.IdArea))) { %>
		<td><%= RegistrosPeatones.IdArea.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosPeatones.SortUrl(RegistrosPeatones.IdArea) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosPeatones.IdArea.FldCaption %></td><td style="width: 10px;"><% if (RegistrosPeatones.IdArea.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosPeatones.IdArea.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosPeatones.IdPersona.Visible) { // IdPersona %>
	<% if (ew_Empty(RegistrosPeatones.SortUrl(RegistrosPeatones.IdPersona))) { %>
		<td><%= RegistrosPeatones.IdPersona.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosPeatones.SortUrl(RegistrosPeatones.IdPersona) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosPeatones.IdPersona.FldCaption %></td><td style="width: 10px;"><% if (RegistrosPeatones.IdPersona.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosPeatones.IdPersona.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosPeatones.FechaIngreso.Visible) { // FechaIngreso %>
	<% if (ew_Empty(RegistrosPeatones.SortUrl(RegistrosPeatones.FechaIngreso))) { %>
		<td><%= RegistrosPeatones.FechaIngreso.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosPeatones.SortUrl(RegistrosPeatones.FechaIngreso) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosPeatones.FechaIngreso.FldCaption %></td><td style="width: 10px;"><% if (RegistrosPeatones.FechaIngreso.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosPeatones.FechaIngreso.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (RegistrosPeatones.FechaSalida.Visible) { // FechaSalida %>
	<% if (ew_Empty(RegistrosPeatones.SortUrl(RegistrosPeatones.FechaSalida))) { %>
		<td><%= RegistrosPeatones.FechaSalida.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= RegistrosPeatones.SortUrl(RegistrosPeatones.FechaSalida) %>',1);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= RegistrosPeatones.FechaSalida.FldCaption %></td><td style="width: 10px;"><% if (RegistrosPeatones.FechaSalida.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (RegistrosPeatones.FechaSalida.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		RegistrosPeatones_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (RegistrosPeatones.ExportAll && ew_NotEmpty(RegistrosPeatones.Export)) {
	RegistrosPeatones_list.StopRec = RegistrosPeatones_list.TotalRecs;
} else {

	// Set the last record to display
	if (RegistrosPeatones_list.TotalRecs > RegistrosPeatones_list.StartRec + RegistrosPeatones_list.DisplayRecs - 1) {
		RegistrosPeatones_list.StopRec = RegistrosPeatones_list.StartRec + RegistrosPeatones_list.DisplayRecs - 1;
	} else {
		RegistrosPeatones_list.StopRec = RegistrosPeatones_list.TotalRecs;
	}
}
if (RegistrosPeatones_list.Recordset != null && RegistrosPeatones_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= RegistrosPeatones_list.StartRec - 1; i++) {
		if (RegistrosPeatones_list.Recordset.Read())
			RegistrosPeatones_list.RecCnt += 1;
	}		
} else if (!RegistrosPeatones.AllowAddDeleteRow && RegistrosPeatones_list.StopRec == 0) {
	RegistrosPeatones_list.StopRec = RegistrosPeatones.GridAddRowCount;
}

// Initialize Aggregate
RegistrosPeatones.RowType = EW_ROWTYPE_AGGREGATEINIT;
RegistrosPeatones.ResetAttrs();
RegistrosPeatones_list.RenderRow();
RegistrosPeatones_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (RegistrosPeatones_list.RecCnt < RegistrosPeatones_list.StopRec) {
	if (RegistrosPeatones.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !RegistrosPeatones_list.Recordset.Read();
	RegistrosPeatones_list.RecCnt += 1;
	if (RegistrosPeatones_list.RecCnt >= RegistrosPeatones_list.StartRec) {
		RegistrosPeatones_list.RowCnt += 1;

		// Set up key count
		RegistrosPeatones_list.KeyCount = ew_ConvertToInt(RegistrosPeatones_list.RowIndex);

		// Init row class and style
		RegistrosPeatones.ResetAttrs();
		RegistrosPeatones.CssClass = "";	 
		if (RegistrosPeatones.CurrentAction == "gridadd") {
		} else {
			RegistrosPeatones_list.LoadRowValues(ref RegistrosPeatones_list.Recordset); // Load row values
		}
		RegistrosPeatones.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref RegistrosPeatones.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		RegistrosPeatones_list.RenderRow();

		// Render list options
		RegistrosPeatones_list.RenderListOptions();
%>
	<tr<%= RegistrosPeatones.RowAttributes %>>
	<%

		// Render list options (body, left)
		RegistrosPeatones_list.ListOptions.Render("body", "left");
	%>
	<% if (RegistrosPeatones.IdTipoDocumento.Visible) { // IdTipoDocumento %>
		<td<%= RegistrosPeatones.IdTipoDocumento.CellAttributes %>>
<div<%= RegistrosPeatones.IdTipoDocumento.ViewAttributes %>><%= RegistrosPeatones.IdTipoDocumento.ListViewValue %></div>
<a name="<%= RegistrosPeatones_list.PageObjName + "_row_" + RegistrosPeatones_list.RowCnt %>" id="<%= RegistrosPeatones_list.PageObjName + "_row_" + RegistrosPeatones_list.RowCnt %>"></a></td>
	<% } %>
	<% if (RegistrosPeatones.Documento.Visible) { // Documento %>
		<td<%= RegistrosPeatones.Documento.CellAttributes %>>
<div<%= RegistrosPeatones.Documento.ViewAttributes %>><%= RegistrosPeatones.Documento.ListViewValue %></div>
</td>
	<% } %>
	<% if (RegistrosPeatones.Nombre.Visible) { // Nombre %>
		<td<%= RegistrosPeatones.Nombre.CellAttributes %>>
<div<%= RegistrosPeatones.Nombre.ViewAttributes %>><%= RegistrosPeatones.Nombre.ListViewValue %></div>
</td>
	<% } %>
	<% if (RegistrosPeatones.Apellidos.Visible) { // Apellidos %>
		<td<%= RegistrosPeatones.Apellidos.CellAttributes %>>
<div<%= RegistrosPeatones.Apellidos.ViewAttributes %>><%= RegistrosPeatones.Apellidos.ListViewValue %></div>
</td>
	<% } %>
	<% if (RegistrosPeatones.IdArea.Visible) { // IdArea %>
		<td<%= RegistrosPeatones.IdArea.CellAttributes %>>
<div<%= RegistrosPeatones.IdArea.ViewAttributes %>><%= RegistrosPeatones.IdArea.ListViewValue %></div>
</td>
	<% } %>
	<% if (RegistrosPeatones.IdPersona.Visible) { // IdPersona %>
		<td<%= RegistrosPeatones.IdPersona.CellAttributes %>>
<div<%= RegistrosPeatones.IdPersona.ViewAttributes %>><%= RegistrosPeatones.IdPersona.ListViewValue %></div>
</td>
	<% } %>
	<% if (RegistrosPeatones.FechaIngreso.Visible) { // FechaIngreso %>
		<td<%= RegistrosPeatones.FechaIngreso.CellAttributes %>>
<div<%= RegistrosPeatones.FechaIngreso.ViewAttributes %>><%= RegistrosPeatones.FechaIngreso.ListViewValue %></div>
</td>
	<% } %>
	<% if (RegistrosPeatones.FechaSalida.Visible) { // FechaSalida %>
		<td<%= RegistrosPeatones.FechaSalida.CellAttributes %>>
<div<%= RegistrosPeatones.FechaSalida.ViewAttributes %>><%= RegistrosPeatones.FechaSalida.ListViewValue %></div>
</td>
	<% } %>
<%

		// Render list options (body, right)
		RegistrosPeatones_list.ListOptions.Render("body", "right");
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
if (RegistrosPeatones_list.Recordset != null) {
	RegistrosPeatones_list.Recordset.Close();
	RegistrosPeatones_list.Recordset.Dispose();
}
%>
<% if (RegistrosPeatones_list.TotalRecs > 0) { %>
<% if (ew_Empty(RegistrosPeatones.Export)) { %>
<div class="ewGridLowerPanel">
<% if (RegistrosPeatones.CurrentAction != "gridadd" && RegistrosPeatones.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (RegistrosPeatones_list.Pager == null) RegistrosPeatones_list.Pager = new cNumericPager(RegistrosPeatones_list.StartRec, RegistrosPeatones_list.DisplayRecs, RegistrosPeatones_list.TotalRecs, RegistrosPeatones_list.RecRange); %>
<% if (RegistrosPeatones_list.Pager.RecordCount > 0) { %>
	<% if (RegistrosPeatones_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= RegistrosPeatones_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (RegistrosPeatones_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= RegistrosPeatones_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in RegistrosPeatones_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (RegistrosPeatones_list.Pager.NextButton.Enabled) { %>
	<a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= RegistrosPeatones_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (RegistrosPeatones_list.Pager.LastButton.Enabled) { %>
	<a href="<%= RegistrosPeatones_list.PageUrl %>start=<%= RegistrosPeatones_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (RegistrosPeatones_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= RegistrosPeatones_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= RegistrosPeatones_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= RegistrosPeatones_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (RegistrosPeatones_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= RegistrosPeatones_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(RegistrosPeatones.Export) && ew_Empty(RegistrosPeatones.CurrentAction)) { %>
<script type="text/javascript">
<!--
ew_ToggleSearchPanel(RegistrosPeatones_list); // Init search panel as collapsed 

//-->
</script>
<% } %>
<%
RegistrosPeatones_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(RegistrosPeatones.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
