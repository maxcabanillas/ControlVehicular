<%@ Page ClassName="VehiculosAutorizadoslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="VehiculosAutorizadoslist.aspx.cs" Inherits="VehiculosAutorizadoslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<%@ Register TagPrefix="ControlVehicular" TagName="MasterTable_Personas" Src="Personasmaster.ascx" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(VehiculosAutorizados.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var VehiculosAutorizados_list = new ew_Page("VehiculosAutorizados_list");

// page properties
VehiculosAutorizados_list.PageID = "list"; // page ID
VehiculosAutorizados_list.FormID = "fVehiculosAutorizadoslist"; // form ID 
var EW_PAGE_ID = VehiculosAutorizados_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
VehiculosAutorizados_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
VehiculosAutorizados_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
VehiculosAutorizados_list.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<style>

	/* styles for detail preview panel */
	#ewDetailsDiv.yui-overlay { position:absolute;background:#fff;border:2px solid orange;padding:4px;margin:10px; }
	#ewDetailsDiv.yui-overlay .hd { border:1px solid red;padding:5px; }
	#ewDetailsDiv.yui-overlay .bd { border:0px solid green;padding:5px; }
	#ewDetailsDiv.yui-overlay .ft { border:1px solid blue;padding:5px; }
</style>
<div id="ewDetailsDiv" style="visibility: hidden; z-index: 11000;" name="ewDetailsDivDiv"></div>
<script language="JavaScript" type="text/javascript">
<!--

// YUI container
var ewDetailsDiv;
var ew_AjaxDetailsTimer = null;

// init details div
function ew_InitDetailsDiv() {
	ewDetailsDiv = new ewWidget.Overlay("ewDetailsDiv", { context:null, visible:false} );
	ewDetailsDiv.render();
}

// init details div on window.load
ewEvent.addListener(window, "load", ew_InitDetailsDiv);

// show results in details div
var ew_AjaxHandleSuccess = function(o) {
	if (ewDetailsDiv && o.responseText !== undefined) {
		ewDetailsDiv.cfg.applyConfig({context:[o.argument.id,o.argument.elcorner,o.argument.ctxcorner], visible:false, constraintoviewport:true, preventcontextoverlap:true}, true);
		ewDetailsDiv.setBody(o.responseText);
		ewDetailsDiv.render();
		ew_SetupTable(ewDom.get("ewDetailsPreviewTable"));
		ewDetailsDiv.show();
	}
}

// show error in details div
var ew_AjaxHandleFailure = function(o) {
	if (ewDetailsDiv && o.responseText != "") {
		ewDetailsDiv.cfg.applyConfig({context:[o.argument.id,o.argument.elcorner,o.argument.ctxcorner], visible:false, constraintoviewport:true, preventcontextoverlap:true}, true);
		ewDetailsDiv.setBody(o.responseText);
		ewDetailsDiv.render();
		ewDetailsDiv.show();
	}
}

// show details div
function ew_AjaxShowDetails(obj, url) {
	if (ew_AjaxDetailsTimer)
		clearTimeout(ew_AjaxDetailsTimer);
	ew_AjaxDetailsTimer = setTimeout(function() { ewConnect.asyncRequest('GET', url, {cache: false, success: ew_AjaxHandleSuccess , failure: ew_AjaxHandleFailure, argument:{id: obj.id, elcorner: "tl", ctxcorner: "tr"}}) }, 200);
}

// hide details div
function ew_AjaxHideDetails(obj) {
	if (ew_AjaxDetailsTimer)
		clearTimeout(ew_AjaxDetailsTimer);
	if (ewDetailsDiv)
		ewDetailsDiv.hide();
}

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<% } %>
<% if (ew_Empty(VehiculosAutorizados.Export) || (EW_EXPORT_MASTER_RECORD && VehiculosAutorizados.Export == "print")) { %>
<%
gsMasterReturnUrl = "Personaslist.aspx";
if (ew_NotEmpty(VehiculosAutorizados_list.DbMasterFilter) && VehiculosAutorizados.CurrentMasterTable == "Personas") {
	if (VehiculosAutorizados_list.MasterRecordExists) {
		if (VehiculosAutorizados.CurrentMasterTable == VehiculosAutorizados.TableVar) gsMasterReturnUrl += "?" + EW_TABLE_SHOW_MASTER + "=";
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("MasterRecord") %><%= Personas.TableCaption %>
&nbsp;&nbsp;<% VehiculosAutorizados_list.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(VehiculosAutorizados.Export)) { %>
<p class="aspnetmaker"><a href="<%= gsMasterReturnUrl %>"><%= Language.Phrase("BackToMasterPage") %></a></p>
<% } %>
<ControlVehicular:MasterTable_Personas id="MasterTable_Personas" runat="server" />
<%
	}
}
%>
<% } %>
<%
	VehiculosAutorizados_list.Recordset = VehiculosAutorizados_list.LoadRecordset();
	VehiculosAutorizados_list.StartRec = 1;
	if (VehiculosAutorizados_list.DisplayRecs <= 0) // Display all records
		VehiculosAutorizados_list.DisplayRecs = VehiculosAutorizados_list.TotalRecs;
	if (!(VehiculosAutorizados.ExportAll && ew_NotEmpty(VehiculosAutorizados.Export)))
		VehiculosAutorizados_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= VehiculosAutorizados.TableCaption %>
<% if (ew_Empty(VehiculosAutorizados.CurrentMasterTable)) { %>
&nbsp;&nbsp;<% VehiculosAutorizados_list.ExportOptions.Render("body"); %>
<% } %>
</p>
<% VehiculosAutorizados_list.ShowPageHeader(); %>
<% VehiculosAutorizados_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(VehiculosAutorizados.Export)) { %>
<div class="ewGridUpperPanel">
<% if (VehiculosAutorizados.CurrentAction != "gridadd" && VehiculosAutorizados.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (VehiculosAutorizados_list.Pager == null) VehiculosAutorizados_list.Pager = new cNumericPager(VehiculosAutorizados_list.StartRec, VehiculosAutorizados_list.DisplayRecs, VehiculosAutorizados_list.TotalRecs, VehiculosAutorizados_list.RecRange); %>
<% if (VehiculosAutorizados_list.Pager.RecordCount > 0) { %>
	<% if (VehiculosAutorizados_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= VehiculosAutorizados_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (VehiculosAutorizados_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= VehiculosAutorizados_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in VehiculosAutorizados_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (VehiculosAutorizados_list.Pager.NextButton.Enabled) { %>
	<a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= VehiculosAutorizados_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (VehiculosAutorizados_list.Pager.LastButton.Enabled) { %>
	<a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= VehiculosAutorizados_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (VehiculosAutorizados_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= VehiculosAutorizados_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= VehiculosAutorizados_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= VehiculosAutorizados_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (VehiculosAutorizados_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= VehiculosAutorizados_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% if (VehiculosPicoYPlacaHoras.DetailAdd && Security.AllowAdd("VehiculosPicoYPlacaHoras")) { %>
<a class="ewGridLink" href="<%= VehiculosAutorizados.AddUrl + "?" + EW_TABLE_SHOW_DETAIL + "=VehiculosPicoYPlacaHoras" %>"><%= Language.Phrase("AddLink") %>&nbsp;<%= VehiculosAutorizados.TableCaption %>/<%= VehiculosPicoYPlacaHoras.TableCaption %></a>&nbsp;&nbsp;
<% } %>
<% } %>
</div>
</div>
<% } %>
<form name="fVehiculosAutorizadoslist" id="fVehiculosAutorizadoslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="VehiculosAutorizados" />
<div id="gmp_VehiculosAutorizados" class="ewGridMiddlePanel">
<% if (VehiculosAutorizados_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= VehiculosAutorizados.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		VehiculosAutorizados_list.RenderListOptions();

		// Render list options (header, left)
		VehiculosAutorizados_list.ListOptions.Render("header", "left");
%>
<% if (VehiculosAutorizados.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.IdTipoVehiculo))) { %>
		<td><%= VehiculosAutorizados.IdTipoVehiculo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.IdTipoVehiculo) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.IdTipoVehiculo.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.IdTipoVehiculo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.IdTipoVehiculo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Placa.Visible) { // Placa %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Placa))) { %>
		<td><%= VehiculosAutorizados.Placa.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Placa) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Placa.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Placa.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Placa.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Autorizado.Visible) { // Autorizado %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Autorizado))) { %>
		<td><%= VehiculosAutorizados.Autorizado.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Autorizado) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Autorizado.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Autorizado.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Autorizado.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.PicoyPlaca.Visible) { // PicoyPlaca %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.PicoyPlaca))) { %>
		<td><%= VehiculosAutorizados.PicoyPlaca.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.PicoyPlaca) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.PicoyPlaca.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.PicoyPlaca.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.PicoyPlaca.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Lunes.Visible) { // Lunes %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Lunes))) { %>
		<td><%= VehiculosAutorizados.Lunes.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Lunes) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Lunes.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Lunes.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Lunes.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Martes.Visible) { // Martes %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Martes))) { %>
		<td><%= VehiculosAutorizados.Martes.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Martes) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Martes.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Martes.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Martes.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Miercoles.Visible) { // Miercoles %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Miercoles))) { %>
		<td><%= VehiculosAutorizados.Miercoles.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Miercoles) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Miercoles.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Miercoles.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Miercoles.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Jueves.Visible) { // Jueves %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Jueves))) { %>
		<td><%= VehiculosAutorizados.Jueves.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Jueves) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Jueves.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Jueves.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Jueves.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Viernes.Visible) { // Viernes %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Viernes))) { %>
		<td><%= VehiculosAutorizados.Viernes.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Viernes) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Viernes.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Viernes.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Viernes.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Sabado.Visible) { // Sabado %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Sabado))) { %>
		<td><%= VehiculosAutorizados.Sabado.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Sabado) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Sabado.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Sabado.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Sabado.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Domingo.Visible) { // Domingo %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Domingo))) { %>
		<td><%= VehiculosAutorizados.Domingo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Domingo) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Domingo.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Domingo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Domingo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (VehiculosAutorizados.Marca.Visible) { // Marca %>
	<% if (ew_Empty(VehiculosAutorizados.SortUrl(VehiculosAutorizados.Marca))) { %>
		<td><%= VehiculosAutorizados.Marca.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= VehiculosAutorizados.SortUrl(VehiculosAutorizados.Marca) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= VehiculosAutorizados.Marca.FldCaption %></td><td style="width: 10px;"><% if (VehiculosAutorizados.Marca.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (VehiculosAutorizados.Marca.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		VehiculosAutorizados_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (VehiculosAutorizados.ExportAll && ew_NotEmpty(VehiculosAutorizados.Export)) {
	VehiculosAutorizados_list.StopRec = VehiculosAutorizados_list.TotalRecs;
} else {

	// Set the last record to display
	if (VehiculosAutorizados_list.TotalRecs > VehiculosAutorizados_list.StartRec + VehiculosAutorizados_list.DisplayRecs - 1) {
		VehiculosAutorizados_list.StopRec = VehiculosAutorizados_list.StartRec + VehiculosAutorizados_list.DisplayRecs - 1;
	} else {
		VehiculosAutorizados_list.StopRec = VehiculosAutorizados_list.TotalRecs;
	}
}
if (VehiculosAutorizados_list.Recordset != null && VehiculosAutorizados_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= VehiculosAutorizados_list.StartRec - 1; i++) {
		if (VehiculosAutorizados_list.Recordset.Read())
			VehiculosAutorizados_list.RecCnt += 1;
	}		
} else if (!VehiculosAutorizados.AllowAddDeleteRow && VehiculosAutorizados_list.StopRec == 0) {
	VehiculosAutorizados_list.StopRec = VehiculosAutorizados.GridAddRowCount;
}

// Initialize Aggregate
VehiculosAutorizados.RowType = EW_ROWTYPE_AGGREGATEINIT;
VehiculosAutorizados.ResetAttrs();
VehiculosAutorizados_list.RenderRow();
VehiculosAutorizados_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (VehiculosAutorizados_list.RecCnt < VehiculosAutorizados_list.StopRec) {
	if (VehiculosAutorizados.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !VehiculosAutorizados_list.Recordset.Read();
	VehiculosAutorizados_list.RecCnt += 1;
	if (VehiculosAutorizados_list.RecCnt >= VehiculosAutorizados_list.StartRec) {
		VehiculosAutorizados_list.RowCnt += 1;

		// Set up key count
		VehiculosAutorizados_list.KeyCount = ew_ConvertToInt(VehiculosAutorizados_list.RowIndex);

		// Init row class and style
		VehiculosAutorizados.ResetAttrs();
		VehiculosAutorizados.CssClass = "";	 
		if (VehiculosAutorizados.CurrentAction == "gridadd") {
		} else {
			VehiculosAutorizados_list.LoadRowValues(ref VehiculosAutorizados_list.Recordset); // Load row values
		}
		VehiculosAutorizados.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref VehiculosAutorizados.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		VehiculosAutorizados_list.RenderRow();

		// Render list options
		VehiculosAutorizados_list.RenderListOptions();
%>
	<tr<%= VehiculosAutorizados.RowAttributes %>>
	<%

		// Render list options (body, left)
		VehiculosAutorizados_list.ListOptions.Render("body", "left");
	%>
	<% if (VehiculosAutorizados.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
		<td<%= VehiculosAutorizados.IdTipoVehiculo.CellAttributes %>>
<div<%= VehiculosAutorizados.IdTipoVehiculo.ViewAttributes %>><%= VehiculosAutorizados.IdTipoVehiculo.ListViewValue %></div>
<a name="<%= VehiculosAutorizados_list.PageObjName + "_row_" + VehiculosAutorizados_list.RowCnt %>" id="<%= VehiculosAutorizados_list.PageObjName + "_row_" + VehiculosAutorizados_list.RowCnt %>"></a></td>
	<% } %>
	<% if (VehiculosAutorizados.Placa.Visible) { // Placa %>
		<td<%= VehiculosAutorizados.Placa.CellAttributes %>>
<div<%= VehiculosAutorizados.Placa.ViewAttributes %>><%= VehiculosAutorizados.Placa.ListViewValue %></div>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Autorizado.Visible) { // Autorizado %>
		<td<%= VehiculosAutorizados.Autorizado.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.PicoyPlaca.Visible) { // PicoyPlaca %>
		<td<%= VehiculosAutorizados.PicoyPlaca.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Lunes.Visible) { // Lunes %>
		<td<%= VehiculosAutorizados.Lunes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Martes.Visible) { // Martes %>
		<td<%= VehiculosAutorizados.Martes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Miercoles.Visible) { // Miercoles %>
		<td<%= VehiculosAutorizados.Miercoles.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Jueves.Visible) { // Jueves %>
		<td<%= VehiculosAutorizados.Jueves.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Viernes.Visible) { // Viernes %>
		<td<%= VehiculosAutorizados.Viernes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Sabado.Visible) { // Sabado %>
		<td<%= VehiculosAutorizados.Sabado.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Domingo.Visible) { // Domingo %>
		<td<%= VehiculosAutorizados.Domingo.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
	<% if (VehiculosAutorizados.Marca.Visible) { // Marca %>
		<td<%= VehiculosAutorizados.Marca.CellAttributes %>>
<div<%= VehiculosAutorizados.Marca.ViewAttributes %>><%= VehiculosAutorizados.Marca.ListViewValue %></div>
</td>
	<% } %>
<%

		// Render list options (body, right)
		VehiculosAutorizados_list.ListOptions.Render("body", "right");
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
if (VehiculosAutorizados_list.Recordset != null) {
	VehiculosAutorizados_list.Recordset.Close();
	VehiculosAutorizados_list.Recordset.Dispose();
}
%>
<% if (VehiculosAutorizados_list.TotalRecs > 0) { %>
<% if (ew_Empty(VehiculosAutorizados.Export)) { %>
<div class="ewGridLowerPanel">
<% if (VehiculosAutorizados.CurrentAction != "gridadd" && VehiculosAutorizados.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (VehiculosAutorizados_list.Pager == null) VehiculosAutorizados_list.Pager = new cNumericPager(VehiculosAutorizados_list.StartRec, VehiculosAutorizados_list.DisplayRecs, VehiculosAutorizados_list.TotalRecs, VehiculosAutorizados_list.RecRange); %>
<% if (VehiculosAutorizados_list.Pager.RecordCount > 0) { %>
	<% if (VehiculosAutorizados_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= VehiculosAutorizados_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (VehiculosAutorizados_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= VehiculosAutorizados_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in VehiculosAutorizados_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (VehiculosAutorizados_list.Pager.NextButton.Enabled) { %>
	<a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= VehiculosAutorizados_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (VehiculosAutorizados_list.Pager.LastButton.Enabled) { %>
	<a href="<%= VehiculosAutorizados_list.PageUrl %>start=<%= VehiculosAutorizados_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (VehiculosAutorizados_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= VehiculosAutorizados_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= VehiculosAutorizados_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= VehiculosAutorizados_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (VehiculosAutorizados_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= VehiculosAutorizados_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% if (VehiculosPicoYPlacaHoras.DetailAdd && Security.AllowAdd("VehiculosPicoYPlacaHoras")) { %>
<a class="ewGridLink" href="<%= VehiculosAutorizados.AddUrl + "?" + EW_TABLE_SHOW_DETAIL + "=VehiculosPicoYPlacaHoras" %>"><%= Language.Phrase("AddLink") %>&nbsp;<%= VehiculosAutorizados.TableCaption %>/<%= VehiculosPicoYPlacaHoras.TableCaption %></a>&nbsp;&nbsp;
<% } %>
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(VehiculosAutorizados.Export) && ew_Empty(VehiculosAutorizados.CurrentAction)) { %>
<% } %>
<%
VehiculosAutorizados_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(VehiculosAutorizados.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
