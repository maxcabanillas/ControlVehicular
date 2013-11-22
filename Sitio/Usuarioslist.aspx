<%@ Page ClassName="Usuarioslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Usuarioslist.aspx.cs" Inherits="Usuarioslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(Usuarios.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var Usuarios_list = new ew_Page("Usuarios_list");

// page properties
Usuarios_list.PageID = "list"; // page ID
Usuarios_list.FormID = "fUsuarioslist"; // form ID 
var EW_PAGE_ID = Usuarios_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Usuarios_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Usuarios_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Usuarios_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(Usuarios.Export) || (EW_EXPORT_MASTER_RECORD && Usuarios.Export == "print")) { %>
<% } %>
<%
	Usuarios_list.Recordset = Usuarios_list.LoadRecordset();
	Usuarios_list.StartRec = 1;
	if (Usuarios_list.DisplayRecs <= 0) // Display all records
		Usuarios_list.DisplayRecs = Usuarios_list.TotalRecs;
	if (!(Usuarios.ExportAll && ew_NotEmpty(Usuarios.Export)))
		Usuarios_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= Usuarios.TableCaption %>
&nbsp;&nbsp;<% Usuarios_list.ExportOptions.Render("body"); %>
</p>
<% Usuarios_list.ShowPageHeader(); %>
<% Usuarios_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(Usuarios.Export)) { %>
<div class="ewGridUpperPanel">
<% if (Usuarios.CurrentAction != "gridadd" && Usuarios.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (Usuarios_list.Pager == null) Usuarios_list.Pager = new cNumericPager(Usuarios_list.StartRec, Usuarios_list.DisplayRecs, Usuarios_list.TotalRecs, Usuarios_list.RecRange); %>
<% if (Usuarios_list.Pager.RecordCount > 0) { %>
	<% if (Usuarios_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= Usuarios_list.PageUrl %>start=<%= Usuarios_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (Usuarios_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= Usuarios_list.PageUrl %>start=<%= Usuarios_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in Usuarios_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= Usuarios_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (Usuarios_list.Pager.NextButton.Enabled) { %>
	<a href="<%= Usuarios_list.PageUrl %>start=<%= Usuarios_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (Usuarios_list.Pager.LastButton.Enabled) { %>
	<a href="<%= Usuarios_list.PageUrl %>start=<%= Usuarios_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (Usuarios_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= Usuarios_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= Usuarios_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= Usuarios_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (Usuarios_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= Usuarios_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<form name="fUsuarioslist" id="fUsuarioslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="Usuarios" />
<div id="gmp_Usuarios" class="ewGridMiddlePanel">
<% if (Usuarios_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= Usuarios.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		Usuarios_list.RenderListOptions();

		// Render list options (header, left)
		Usuarios_list.ListOptions.Render("header", "left");
%>
<% if (Usuarios.Usuario.Visible) { // Usuario %>
	<% if (ew_Empty(Usuarios.SortUrl(Usuarios.Usuario))) { %>
		<td><%= Usuarios.Usuario.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Usuarios.SortUrl(Usuarios.Usuario) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Usuarios.Usuario.FldCaption %></td><td style="width: 10px;"><% if (Usuarios.Usuario.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Usuarios.Usuario.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Usuarios.NombreUsuario.Visible) { // NombreUsuario %>
	<% if (ew_Empty(Usuarios.SortUrl(Usuarios.NombreUsuario))) { %>
		<td><%= Usuarios.NombreUsuario.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Usuarios.SortUrl(Usuarios.NombreUsuario) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Usuarios.NombreUsuario.FldCaption %></td><td style="width: 10px;"><% if (Usuarios.NombreUsuario.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Usuarios.NombreUsuario.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Usuarios.Password.Visible) { // Password %>
	<% if (ew_Empty(Usuarios.SortUrl(Usuarios.Password))) { %>
		<td><%= Usuarios.Password.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Usuarios.SortUrl(Usuarios.Password) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Usuarios.Password.FldCaption %></td><td style="width: 10px;"><% if (Usuarios.Password.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Usuarios.Password.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Usuarios.Correo.Visible) { // Correo %>
	<% if (ew_Empty(Usuarios.SortUrl(Usuarios.Correo))) { %>
		<td><%= Usuarios.Correo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Usuarios.SortUrl(Usuarios.Correo) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Usuarios.Correo.FldCaption %></td><td style="width: 10px;"><% if (Usuarios.Correo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Usuarios.Correo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Usuarios.IdUsuarioNivel.Visible) { // IdUsuarioNivel %>
	<% if (ew_Empty(Usuarios.SortUrl(Usuarios.IdUsuarioNivel))) { %>
		<td><%= Usuarios.IdUsuarioNivel.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Usuarios.SortUrl(Usuarios.IdUsuarioNivel) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Usuarios.IdUsuarioNivel.FldCaption %></td><td style="width: 10px;"><% if (Usuarios.IdUsuarioNivel.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Usuarios.IdUsuarioNivel.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Usuarios.Activo.Visible) { // Activo %>
	<% if (ew_Empty(Usuarios.SortUrl(Usuarios.Activo))) { %>
		<td><%= Usuarios.Activo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Usuarios.SortUrl(Usuarios.Activo) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Usuarios.Activo.FldCaption %></td><td style="width: 10px;"><% if (Usuarios.Activo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Usuarios.Activo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		Usuarios_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (Usuarios.ExportAll && ew_NotEmpty(Usuarios.Export)) {
	Usuarios_list.StopRec = Usuarios_list.TotalRecs;
} else {

	// Set the last record to display
	if (Usuarios_list.TotalRecs > Usuarios_list.StartRec + Usuarios_list.DisplayRecs - 1) {
		Usuarios_list.StopRec = Usuarios_list.StartRec + Usuarios_list.DisplayRecs - 1;
	} else {
		Usuarios_list.StopRec = Usuarios_list.TotalRecs;
	}
}
if (Usuarios_list.Recordset != null && Usuarios_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= Usuarios_list.StartRec - 1; i++) {
		if (Usuarios_list.Recordset.Read())
			Usuarios_list.RecCnt += 1;
	}		
} else if (!Usuarios.AllowAddDeleteRow && Usuarios_list.StopRec == 0) {
	Usuarios_list.StopRec = Usuarios.GridAddRowCount;
}

// Initialize Aggregate
Usuarios.RowType = EW_ROWTYPE_AGGREGATEINIT;
Usuarios.ResetAttrs();
Usuarios_list.RenderRow();
Usuarios_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (Usuarios_list.RecCnt < Usuarios_list.StopRec) {
	if (Usuarios.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !Usuarios_list.Recordset.Read();
	Usuarios_list.RecCnt += 1;
	if (Usuarios_list.RecCnt >= Usuarios_list.StartRec) {
		Usuarios_list.RowCnt += 1;

		// Set up key count
		Usuarios_list.KeyCount = ew_ConvertToInt(Usuarios_list.RowIndex);

		// Init row class and style
		Usuarios.ResetAttrs();
		Usuarios.CssClass = "";	 
		if (Usuarios.CurrentAction == "gridadd") {
		} else {
			Usuarios_list.LoadRowValues(ref Usuarios_list.Recordset); // Load row values
		}
		Usuarios.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref Usuarios.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		Usuarios_list.RenderRow();

		// Render list options
		Usuarios_list.RenderListOptions();
%>
	<tr<%= Usuarios.RowAttributes %>>
	<%

		// Render list options (body, left)
		Usuarios_list.ListOptions.Render("body", "left");
	%>
	<% if (Usuarios.Usuario.Visible) { // Usuario %>
		<td<%= Usuarios.Usuario.CellAttributes %>>
<div<%= Usuarios.Usuario.ViewAttributes %>><%= Usuarios.Usuario.ListViewValue %></div>
<a name="<%= Usuarios_list.PageObjName + "_row_" + Usuarios_list.RowCnt %>" id="<%= Usuarios_list.PageObjName + "_row_" + Usuarios_list.RowCnt %>"></a></td>
	<% } %>
	<% if (Usuarios.NombreUsuario.Visible) { // NombreUsuario %>
		<td<%= Usuarios.NombreUsuario.CellAttributes %>>
<div<%= Usuarios.NombreUsuario.ViewAttributes %>><%= Usuarios.NombreUsuario.ListViewValue %></div>
</td>
	<% } %>
	<% if (Usuarios.Password.Visible) { // Password %>
		<td<%= Usuarios.Password.CellAttributes %>>
<div<%= Usuarios.Password.ViewAttributes %>><%= Usuarios.Password.ListViewValue %></div>
</td>
	<% } %>
	<% if (Usuarios.Correo.Visible) { // Correo %>
		<td<%= Usuarios.Correo.CellAttributes %>>
<div<%= Usuarios.Correo.ViewAttributes %>><%= Usuarios.Correo.ListViewValue %></div>
</td>
	<% } %>
	<% if (Usuarios.IdUsuarioNivel.Visible) { // IdUsuarioNivel %>
		<td<%= Usuarios.IdUsuarioNivel.CellAttributes %>>
<div<%= Usuarios.IdUsuarioNivel.ViewAttributes %>><%= Usuarios.IdUsuarioNivel.ListViewValue %></div>
</td>
	<% } %>
	<% if (Usuarios.Activo.Visible) { // Activo %>
		<td<%= Usuarios.Activo.CellAttributes %>>
<% if (Convert.ToString(Usuarios.Activo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Usuarios.Activo.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Usuarios.Activo.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
<%

		// Render list options (body, right)
		Usuarios_list.ListOptions.Render("body", "right");
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
if (Usuarios_list.Recordset != null) {
	Usuarios_list.Recordset.Close();
	Usuarios_list.Recordset.Dispose();
}
%>
<% if (Usuarios_list.TotalRecs > 0) { %>
<% if (ew_Empty(Usuarios.Export)) { %>
<div class="ewGridLowerPanel">
<% if (Usuarios.CurrentAction != "gridadd" && Usuarios.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (Usuarios_list.Pager == null) Usuarios_list.Pager = new cNumericPager(Usuarios_list.StartRec, Usuarios_list.DisplayRecs, Usuarios_list.TotalRecs, Usuarios_list.RecRange); %>
<% if (Usuarios_list.Pager.RecordCount > 0) { %>
	<% if (Usuarios_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= Usuarios_list.PageUrl %>start=<%= Usuarios_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (Usuarios_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= Usuarios_list.PageUrl %>start=<%= Usuarios_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in Usuarios_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= Usuarios_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (Usuarios_list.Pager.NextButton.Enabled) { %>
	<a href="<%= Usuarios_list.PageUrl %>start=<%= Usuarios_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (Usuarios_list.Pager.LastButton.Enabled) { %>
	<a href="<%= Usuarios_list.PageUrl %>start=<%= Usuarios_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (Usuarios_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= Usuarios_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= Usuarios_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= Usuarios_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (Usuarios_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= Usuarios_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(Usuarios.Export) && ew_Empty(Usuarios.CurrentAction)) { %>
<% } %>
<%
Usuarios_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(Usuarios.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
