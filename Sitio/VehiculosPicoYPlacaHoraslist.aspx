<%@ Page ClassName="VehiculosPicoYPlacaHoraslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="VehiculosPicoYPlacaHoraslist.aspx.cs" Inherits="VehiculosPicoYPlacaHoraslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<%@ Register TagPrefix="ControlVehicular" TagName="MasterTable_VehiculosAutorizados" Src="VehiculosAutorizadosmaster.ascx" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(VehiculosPicoYPlacaHoras.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var VehiculosPicoYPlacaHoras_list = new ew_Page("VehiculosPicoYPlacaHoras_list");

// page properties
VehiculosPicoYPlacaHoras_list.PageID = "list"; // page ID
VehiculosPicoYPlacaHoras_list.FormID = "fVehiculosPicoYPlacaHoraslist"; // form ID 
var EW_PAGE_ID = VehiculosPicoYPlacaHoras_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
VehiculosPicoYPlacaHoras_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
VehiculosPicoYPlacaHoras_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
VehiculosPicoYPlacaHoras_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(VehiculosPicoYPlacaHoras.Export) || (EW_EXPORT_MASTER_RECORD && VehiculosPicoYPlacaHoras.Export == "print")) { %>
<%
gsMasterReturnUrl = "VehiculosAutorizadoslist.aspx";
if (ew_NotEmpty(VehiculosPicoYPlacaHoras_list.DbMasterFilter) && VehiculosPicoYPlacaHoras.CurrentMasterTable == "VehiculosAutorizados") {
	if (VehiculosPicoYPlacaHoras_list.MasterRecordExists) {
		if (VehiculosPicoYPlacaHoras.CurrentMasterTable == VehiculosPicoYPlacaHoras.TableVar) gsMasterReturnUrl += "?" + EW_TABLE_SHOW_MASTER + "=";
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("MasterRecord") %><%= VehiculosAutorizados.TableCaption %>
&nbsp;&nbsp;<% VehiculosPicoYPlacaHoras_list.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(VehiculosPicoYPlacaHoras.Export)) { %>
<p class="aspnetmaker"><a href="<%= gsMasterReturnUrl %>"><%= Language.Phrase("BackToMasterPage") %></a></p>
<% } %>
<ControlVehicular:MasterTable_VehiculosAutorizados id="MasterTable_VehiculosAutorizados" runat="server" />
<%
	}
}
%>
<% } %>
<%
	VehiculosPicoYPlacaHoras_list.Recordset = VehiculosPicoYPlacaHoras_list.LoadRecordset();
	VehiculosPicoYPlacaHoras_list.StartRec = 1;
	if (VehiculosPicoYPlacaHoras_list.DisplayRecs <= 0) // Display all records
		VehiculosPicoYPlacaHoras_list.DisplayRecs = VehiculosPicoYPlacaHoras_list.TotalRecs;
	if (!(VehiculosPicoYPlacaHoras.ExportAll && ew_NotEmpty(VehiculosPicoYPlacaHoras.Export)))
		VehiculosPicoYPlacaHoras_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= VehiculosPicoYPlacaHoras.TableCaption %>
<% if (ew_Empty(VehiculosPicoYPlacaHoras.CurrentMasterTable)) { %>
&nbsp;&nbsp;<% VehiculosPicoYPlacaHoras_list.ExportOptions.Render("body"); %>
<% } %>
</p>
<% VehiculosPicoYPlacaHoras_list.ShowPageHeader(); %>
<% VehiculosPicoYPlacaHoras_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(VehiculosPicoYPlacaHoras.Export)) { %>
<div class="ewGridUpperPanel">
<% if (VehiculosPicoYPlacaHoras.CurrentAction != "gridadd" && VehiculosPicoYPlacaHoras.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (VehiculosPicoYPlacaHoras_list.Pager == null) VehiculosPicoYPlacaHoras_list.Pager = new cNumericPager(VehiculosPicoYPlacaHoras_list.StartRec, VehiculosPicoYPlacaHoras_list.DisplayRecs, VehiculosPicoYPlacaHoras_list.TotalRecs, VehiculosPicoYPlacaHoras_list.RecRange); %>
<% if (VehiculosPicoYPlacaHoras_list.Pager.RecordCount > 0) { %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= VehiculosPicoYPlacaHoras_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= VehiculosPicoYPlacaHoras_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in VehiculosPicoYPlacaHoras_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.NextButton.Enabled) { %>
	<a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= VehiculosPicoYPlacaHoras_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.LastButton.Enabled) { %>
	<a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= VehiculosPicoYPlacaHoras_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= VehiculosPicoYPlacaHoras_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= VehiculosPicoYPlacaHoras_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= VehiculosPicoYPlacaHoras_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (VehiculosPicoYPlacaHoras_list.SearchWhere == "0=101") { %>
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
<form name="fVehiculosPicoYPlacaHoraslist" id="fVehiculosPicoYPlacaHoraslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="VehiculosPicoYPlacaHoras" />
<div id="gmp_VehiculosPicoYPlacaHoras" class="ewGridMiddlePanel">
<% if (VehiculosPicoYPlacaHoras_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= VehiculosPicoYPlacaHoras.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		VehiculosPicoYPlacaHoras_list.RenderListOptions();

		// Render list options (header, left)
		VehiculosPicoYPlacaHoras_list.ListOptions.Render("header", "left");
%>
<% if (VehiculosPicoYPlacaHoras.HoraInicial.Visible) { // HoraInicial %>
	<% if (ew_Empty(VehiculosPicoYPlacaHoras.SortUrl(VehiculosPicoYPlacaHoras.HoraInicial))) { %>
		<td><%= VehiculosPicoYPlacaHoras.HoraInicial.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosPicoYPlacaHoras.SortUrl(VehiculosPicoYPlacaHoras.HoraInicial) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosPicoYPlacaHoras.HoraInicial.FldCaption %></td><td style="width: 10px;"><% if (VehiculosPicoYPlacaHoras.HoraInicial.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosPicoYPlacaHoras.HoraInicial.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosPicoYPlacaHoras.HoraFinal.Visible) { // HoraFinal %>
	<% if (ew_Empty(VehiculosPicoYPlacaHoras.SortUrl(VehiculosPicoYPlacaHoras.HoraFinal))) { %>
		<td><%= VehiculosPicoYPlacaHoras.HoraFinal.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosPicoYPlacaHoras.SortUrl(VehiculosPicoYPlacaHoras.HoraFinal) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosPicoYPlacaHoras.HoraFinal.FldCaption %></td><td style="width: 10px;"><% if (VehiculosPicoYPlacaHoras.HoraFinal.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosPicoYPlacaHoras.HoraFinal.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		VehiculosPicoYPlacaHoras_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (VehiculosPicoYPlacaHoras.ExportAll && ew_NotEmpty(VehiculosPicoYPlacaHoras.Export)) {
	VehiculosPicoYPlacaHoras_list.StopRec = VehiculosPicoYPlacaHoras_list.TotalRecs;
} else {

	// Set the last record to display
	if (VehiculosPicoYPlacaHoras_list.TotalRecs > VehiculosPicoYPlacaHoras_list.StartRec + VehiculosPicoYPlacaHoras_list.DisplayRecs - 1) {
		VehiculosPicoYPlacaHoras_list.StopRec = VehiculosPicoYPlacaHoras_list.StartRec + VehiculosPicoYPlacaHoras_list.DisplayRecs - 1;
	} else {
		VehiculosPicoYPlacaHoras_list.StopRec = VehiculosPicoYPlacaHoras_list.TotalRecs;
	}
}
if (VehiculosPicoYPlacaHoras_list.Recordset != null && VehiculosPicoYPlacaHoras_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= VehiculosPicoYPlacaHoras_list.StartRec - 1; i++) {
		if (VehiculosPicoYPlacaHoras_list.Recordset.Read())
			VehiculosPicoYPlacaHoras_list.RecCnt += 1;
	}		
} else if (!VehiculosPicoYPlacaHoras.AllowAddDeleteRow && VehiculosPicoYPlacaHoras_list.StopRec == 0) {
	VehiculosPicoYPlacaHoras_list.StopRec = VehiculosPicoYPlacaHoras.GridAddRowCount;
}

// Initialize Aggregate
VehiculosPicoYPlacaHoras.RowType = EW_ROWTYPE_AGGREGATEINIT;
VehiculosPicoYPlacaHoras.ResetAttrs();
VehiculosPicoYPlacaHoras_list.RenderRow();
VehiculosPicoYPlacaHoras_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (VehiculosPicoYPlacaHoras_list.RecCnt < VehiculosPicoYPlacaHoras_list.StopRec) {
	if (VehiculosPicoYPlacaHoras.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !VehiculosPicoYPlacaHoras_list.Recordset.Read();
	VehiculosPicoYPlacaHoras_list.RecCnt += 1;
	if (VehiculosPicoYPlacaHoras_list.RecCnt >= VehiculosPicoYPlacaHoras_list.StartRec) {
		VehiculosPicoYPlacaHoras_list.RowCnt += 1;

		// Set up key count
		VehiculosPicoYPlacaHoras_list.KeyCount = ew_ConvertToInt(VehiculosPicoYPlacaHoras_list.RowIndex);

		// Init row class and style
		VehiculosPicoYPlacaHoras.ResetAttrs();
		VehiculosPicoYPlacaHoras.CssClass = "";	 
		if (VehiculosPicoYPlacaHoras.CurrentAction == "gridadd") {
		} else {
			VehiculosPicoYPlacaHoras_list.LoadRowValues(ref VehiculosPicoYPlacaHoras_list.Recordset); // Load row values
		}
		VehiculosPicoYPlacaHoras.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref VehiculosPicoYPlacaHoras.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		VehiculosPicoYPlacaHoras_list.RenderRow();

		// Render list options
		VehiculosPicoYPlacaHoras_list.RenderListOptions();
%>
	<tr<%= VehiculosPicoYPlacaHoras.RowAttributes %>>
	<%

		// Render list options (body, left)
		VehiculosPicoYPlacaHoras_list.ListOptions.Render("body", "left");
	%>
	<% if (VehiculosPicoYPlacaHoras.HoraInicial.Visible) { // HoraInicial %>
		<td<%= VehiculosPicoYPlacaHoras.HoraInicial.CellAttributes %>>
<div<%= VehiculosPicoYPlacaHoras.HoraInicial.ViewAttributes %>><%= VehiculosPicoYPlacaHoras.HoraInicial.ListViewValue %></div>
<a name="<%= VehiculosPicoYPlacaHoras_list.PageObjName + "_row_" + VehiculosPicoYPlacaHoras_list.RowCnt %>" id="<%= VehiculosPicoYPlacaHoras_list.PageObjName + "_row_" + VehiculosPicoYPlacaHoras_list.RowCnt %>"></a></td>
	<% } %>
	<% if (VehiculosPicoYPlacaHoras.HoraFinal.Visible) { // HoraFinal %>
		<td<%= VehiculosPicoYPlacaHoras.HoraFinal.CellAttributes %>>
<div<%= VehiculosPicoYPlacaHoras.HoraFinal.ViewAttributes %>><%= VehiculosPicoYPlacaHoras.HoraFinal.ListViewValue %></div>
</td>
	<% } %>
<%

		// Render list options (body, right)
		VehiculosPicoYPlacaHoras_list.ListOptions.Render("body", "right");
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
if (VehiculosPicoYPlacaHoras_list.Recordset != null) {
	VehiculosPicoYPlacaHoras_list.Recordset.Close();
	VehiculosPicoYPlacaHoras_list.Recordset.Dispose();
}
%>
<% if (VehiculosPicoYPlacaHoras_list.TotalRecs > 0) { %>
<% if (ew_Empty(VehiculosPicoYPlacaHoras.Export)) { %>
<div class="ewGridLowerPanel">
<% if (VehiculosPicoYPlacaHoras.CurrentAction != "gridadd" && VehiculosPicoYPlacaHoras.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (VehiculosPicoYPlacaHoras_list.Pager == null) VehiculosPicoYPlacaHoras_list.Pager = new cNumericPager(VehiculosPicoYPlacaHoras_list.StartRec, VehiculosPicoYPlacaHoras_list.DisplayRecs, VehiculosPicoYPlacaHoras_list.TotalRecs, VehiculosPicoYPlacaHoras_list.RecRange); %>
<% if (VehiculosPicoYPlacaHoras_list.Pager.RecordCount > 0) { %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= VehiculosPicoYPlacaHoras_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= VehiculosPicoYPlacaHoras_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in VehiculosPicoYPlacaHoras_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.NextButton.Enabled) { %>
	<a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= VehiculosPicoYPlacaHoras_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.LastButton.Enabled) { %>
	<a href="<%= VehiculosPicoYPlacaHoras_list.PageUrl %>start=<%= VehiculosPicoYPlacaHoras_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (VehiculosPicoYPlacaHoras_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= VehiculosPicoYPlacaHoras_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= VehiculosPicoYPlacaHoras_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= VehiculosPicoYPlacaHoras_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (VehiculosPicoYPlacaHoras_list.SearchWhere == "0=101") { %>
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
<% if (ew_Empty(VehiculosPicoYPlacaHoras.Export) && ew_Empty(VehiculosPicoYPlacaHoras.CurrentAction)) { %>
<% } %>
<%
VehiculosPicoYPlacaHoras_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(VehiculosPicoYPlacaHoras.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
