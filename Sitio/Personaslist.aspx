<%@ Page ClassName="Personaslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Personaslist.aspx.cs" Inherits="Personaslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<%@ Register TagPrefix="ControlVehicular" TagName="MasterTable_Areas" Src="Areasmaster.ascx" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(Personas.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var Personas_list = new ew_Page("Personas_list");

// page properties
Personas_list.PageID = "list"; // page ID
Personas_list.FormID = "fPersonaslist"; // form ID 
var EW_PAGE_ID = Personas_list.PageID; // for backward compatibility

// extend page with validate function for search
Personas_list.ValidateSearch = function(fobj) {
	ew_PostAutoSuggest(fobj); 
	if (this.ValidateRequired) { 
		var infix = "";

		// Form Custom Validate event
		if (!this.Form_CustomValidate(fobj)) return false;
	} 
	for (var i=0;i<fobj.elements.length;i++) {
		var elem = fobj.elements[i];
		if (elem.name.substring(0,2) == "s_" || elem.name.substring(0,3) == "sv_")
			elem.value = "";
	}
	return true;
}

// extend page with Form_CustomValidate function
Personas_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Personas_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Personas_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(Personas.Export) || (EW_EXPORT_MASTER_RECORD && Personas.Export == "print")) { %>
<%
gsMasterReturnUrl = "Areaslist.aspx";
if (ew_NotEmpty(Personas_list.DbMasterFilter) && Personas.CurrentMasterTable == "Areas") {
	if (Personas_list.MasterRecordExists) {
		if (Personas.CurrentMasterTable == Personas.TableVar) gsMasterReturnUrl += "?" + EW_TABLE_SHOW_MASTER + "=";
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("MasterRecord") %><%= Areas.TableCaption %>
&nbsp;&nbsp;<% Personas_list.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(Personas.Export)) { %>
<p class="aspnetmaker"><a href="<%= gsMasterReturnUrl %>"><%= Language.Phrase("BackToMasterPage") %></a></p>
<% } %>
<ControlVehicular:MasterTable_Areas id="MasterTable_Areas" runat="server" />
<%
	}
}
%>
<% } %>
<%
	Personas_list.Recordset = Personas_list.LoadRecordset();
	Personas_list.StartRec = 1;
	if (Personas_list.DisplayRecs <= 0) // Display all records
		Personas_list.DisplayRecs = Personas_list.TotalRecs;
	if (!(Personas.ExportAll && ew_NotEmpty(Personas.Export)))
		Personas_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= Personas.TableCaption %>
<% if (ew_Empty(Personas.CurrentMasterTable)) { %>
&nbsp;&nbsp;<% Personas_list.ExportOptions.Render("body"); %>
<% } %>
</p>
<% if (Security.CanSearch) { %>
<% if (ew_Empty(Personas.Export) && ew_Empty(Personas.CurrentAction)) { %>
<a href="javascript:ew_ToggleSearchPanel(Personas_list);" style="text-decoration: none;"><img id="Personas_list_SearchImage" src="aspximages/collapse.gif" alt="" width="9" height="9" border="0"></a><span class="aspnetmaker">&nbsp;<%= Language.Phrase("Search") %></span><br>
<div id="Personas_list_SearchPanel">
<form name="fPersonaslistsrch" id="fPersonaslistsrch" class="ewForm" onsubmit="return Personas_list.ValidateSearch(this);">
<input type="hidden" id="t" name="t" value="Personas" />
<div class="ewBasicSearch">
<%
if (ew_Empty(gsSearchError))
	Personas_list.LoadAdvancedSearch(); // Load advanced search

// Render for search
Personas.RowType = EW_ROWTYPE_SEARCH;

// Render row
Personas.ResetAttrs();
Personas_list.RenderRow();
%>
<div id="xsr_1" class="ewCssTableRow">
	<span id="xsc_IdArea" class="ewCssTableCell">
		<span class="ewSearchCaption"><%= Personas.IdArea.FldCaption %></span>
		<span class="ewSearchOprCell"><%= Language.Phrase("=") %><input type="hidden" name="z_IdArea" id="z_IdArea" value="=" /></span>
		<span class="ewSearchField">
<% if (ew_NotEmpty(Personas.IdArea.SessionValue)) { %>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ListViewValue %></div>
<input type="hidden" id="x_IdArea" name="x_IdArea" value="<%= ew_HtmlEncode(Personas.IdArea.AdvancedSearch.SearchValue) %>">
<% } else { %>
<select id="x_IdArea" name="x_IdArea"<%= Personas.IdArea.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdArea.EditValue)) {
	alwrk = (ArrayList)Personas.IdArea.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdArea.AdvancedSearch.SearchValue)) {
			selwrk = " selected=\"selected\"";
			emptywrk = false;
		} else {
			selwrk = "";
		}
%>
<option value="<%= ew_HtmlEncode(odwrk[0]) %>"<%= selwrk %>>
<%= odwrk[1] %><% if (ew_NotEmpty(odwrk[2])) { %><%= ew_ValueSeparator(rowcntwrk,1,Personas.IdArea) %><%= odwrk[2] %><% } %>
</option>
<%
	}
}
%>
</select>
<% } %>
</span>
		</span>
</div>
<div id="xsr_2" class="ewCssTableRow">
	<span id="xsc_IdCargo" class="ewCssTableCell">
		<span class="ewSearchCaption"><%= Personas.IdCargo.FldCaption %></span>
		<span class="ewSearchOprCell"><%= Language.Phrase("=") %><input type="hidden" name="z_IdCargo" id="z_IdCargo" value="=" /></span>
		<span class="ewSearchField">
<select id="x_IdCargo" name="x_IdCargo"<%= Personas.IdCargo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(Personas.IdCargo.EditValue)) {
	alwrk = (ArrayList)Personas.IdCargo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], Personas.IdCargo.AdvancedSearch.SearchValue)) {
			selwrk = " selected=\"selected\"";
			emptywrk = false;
		} else {
			selwrk = "";
		}
%>
<option value="<%= ew_HtmlEncode(odwrk[0]) %>"<%= selwrk %>>
<%= odwrk[1] %>
</option>
<%
	}
}
%>
</select>
</span>
		</span>
</div>
<div id="xsr_3" class="ewCssTableRow">
			<input type="text" name="<%= EW_TABLE_BASIC_SEARCH %>" id="<%= EW_TABLE_BASIC_SEARCH %>" size="20" value="<%= ew_HtmlEncode(Personas.SessionBasicSearchKeyword) %>" />
			<input type="Submit" name="Submit" id="Submit" value="<%= ew_BtnCaption(Language.Phrase("QuickSearchBtn")) %>" />&nbsp;
			<a href="<%= Personas_list.PageUrl %>cmd=reset"><%= Language.Phrase("ShowAll") %></a>&nbsp;
			<a href="Personassrch.aspx"><%= Language.Phrase("AdvancedSearch") %></a>&nbsp;
</div>
<div id="xsr_4" class="ewCssTableRow">
	<label><input type="radio" name="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" id="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" value=""<% if (ew_Empty(Personas.SessionBasicSearchType)) { %> checked="checked"<% } %> /><%= Language.Phrase("ExactPhrase") %></label>&nbsp;&nbsp;<label><input type="radio" name="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" id="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" value="AND"<% if (Personas.SessionBasicSearchType == "AND") { %> checked="checked"<% } %> /><%= Language.Phrase("AllWord") %></label>&nbsp;&nbsp;<label><input type="radio" name="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" id="<%= EW_TABLE_BASIC_SEARCH_TYPE %>" value="OR"<% if (Personas.SessionBasicSearchType == "OR") { %> checked="checked"<% } %> /><%= Language.Phrase("AnyWord") %></label>
</div>
</div>
</form>
</div>
<% } %>
<% } %>
<% Personas_list.ShowPageHeader(); %>
<% Personas_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(Personas.Export)) { %>
<div class="ewGridUpperPanel">
<% if (Personas.CurrentAction != "gridadd" && Personas.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (Personas_list.Pager == null) Personas_list.Pager = new cNumericPager(Personas_list.StartRec, Personas_list.DisplayRecs, Personas_list.TotalRecs, Personas_list.RecRange); %>
<% if (Personas_list.Pager.RecordCount > 0) { %>
	<% if (Personas_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= Personas_list.PageUrl %>start=<%= Personas_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (Personas_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= Personas_list.PageUrl %>start=<%= Personas_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in Personas_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= Personas_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (Personas_list.Pager.NextButton.Enabled) { %>
	<a href="<%= Personas_list.PageUrl %>start=<%= Personas_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (Personas_list.Pager.LastButton.Enabled) { %>
	<a href="<%= Personas_list.PageUrl %>start=<%= Personas_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (Personas_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= Personas_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= Personas_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= Personas_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (Personas_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= Personas_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% if (VehiculosAutorizados.DetailAdd && Security.AllowAdd("VehiculosAutorizados")) { %>
<a class="ewGridLink" href="<%= Personas.AddUrl + "?" + EW_TABLE_SHOW_DETAIL + "=VehiculosAutorizados" %>"><%= Language.Phrase("AddLink") %>&nbsp;<%= Personas.TableCaption %>/<%= VehiculosAutorizados.TableCaption %></a>&nbsp;&nbsp;
<% } %>
<% } %>
</div>
</div>
<% } %>
<form name="fPersonaslist" id="fPersonaslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="Personas" />
<div id="gmp_Personas" class="ewGridMiddlePanel">
<% if (Personas_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= Personas.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		Personas_list.RenderListOptions();

		// Render list options (header, left)
		Personas_list.ListOptions.Render("header", "left");
%>
<% if (Personas.IdArea.Visible) { // IdArea %>
	<% if (ew_Empty(Personas.SortUrl(Personas.IdArea))) { %>
		<td><%= Personas.IdArea.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Personas.SortUrl(Personas.IdArea) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.IdArea.FldCaption %></td><td style="width: 10px;"><% if (Personas.IdArea.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.IdArea.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Personas.IdCargo.Visible) { // IdCargo %>
	<% if (ew_Empty(Personas.SortUrl(Personas.IdCargo))) { %>
		<td><%= Personas.IdCargo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Personas.SortUrl(Personas.IdCargo) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.IdCargo.FldCaption %></td><td style="width: 10px;"><% if (Personas.IdCargo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.IdCargo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Personas.Documento.Visible) { // Documento %>
	<% if (ew_Empty(Personas.SortUrl(Personas.Documento))) { %>
		<td><%= Personas.Documento.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Personas.SortUrl(Personas.Documento) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.Documento.FldCaption %><%= Language.Phrase("SrchLegend") %></td><td style="width: 10px;"><% if (Personas.Documento.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.Documento.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Personas.Persona.Visible) { // Persona %>
	<% if (ew_Empty(Personas.SortUrl(Personas.Persona))) { %>
		<td><%= Personas.Persona.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Personas.SortUrl(Personas.Persona) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.Persona.FldCaption %><%= Language.Phrase("SrchLegend") %></td><td style="width: 10px;"><% if (Personas.Persona.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.Persona.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Personas.Activa.Visible) { // Activa %>
	<% if (ew_Empty(Personas.SortUrl(Personas.Activa))) { %>
		<td><%= Personas.Activa.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Personas.SortUrl(Personas.Activa) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Personas.Activa.FldCaption %></td><td style="width: 10px;"><% if (Personas.Activa.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Personas.Activa.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		Personas_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (Personas.ExportAll && ew_NotEmpty(Personas.Export)) {
	Personas_list.StopRec = Personas_list.TotalRecs;
} else {

	// Set the last record to display
	if (Personas_list.TotalRecs > Personas_list.StartRec + Personas_list.DisplayRecs - 1) {
		Personas_list.StopRec = Personas_list.StartRec + Personas_list.DisplayRecs - 1;
	} else {
		Personas_list.StopRec = Personas_list.TotalRecs;
	}
}
if (Personas_list.Recordset != null && Personas_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= Personas_list.StartRec - 1; i++) {
		if (Personas_list.Recordset.Read())
			Personas_list.RecCnt += 1;
	}		
} else if (!Personas.AllowAddDeleteRow && Personas_list.StopRec == 0) {
	Personas_list.StopRec = Personas.GridAddRowCount;
}

// Initialize Aggregate
Personas.RowType = EW_ROWTYPE_AGGREGATEINIT;
Personas.ResetAttrs();
Personas_list.RenderRow();
Personas_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (Personas_list.RecCnt < Personas_list.StopRec) {
	if (Personas.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !Personas_list.Recordset.Read();
	Personas_list.RecCnt += 1;
	if (Personas_list.RecCnt >= Personas_list.StartRec) {
		Personas_list.RowCnt += 1;

		// Set up key count
		Personas_list.KeyCount = ew_ConvertToInt(Personas_list.RowIndex);

		// Init row class and style
		Personas.ResetAttrs();
		Personas.CssClass = "";	 
		if (Personas.CurrentAction == "gridadd") {
		} else {
			Personas_list.LoadRowValues(ref Personas_list.Recordset); // Load row values
		}
		Personas.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref Personas.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		Personas_list.RenderRow();

		// Render list options
		Personas_list.RenderListOptions();
%>
	<tr<%= Personas.RowAttributes %>>
	<%

		// Render list options (body, left)
		Personas_list.ListOptions.Render("body", "left");
	%>
	<% if (Personas.IdArea.Visible) { // IdArea %>
		<td<%= Personas.IdArea.CellAttributes %>>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ListViewValue %></div>
<a name="<%= Personas_list.PageObjName + "_row_" + Personas_list.RowCnt %>" id="<%= Personas_list.PageObjName + "_row_" + Personas_list.RowCnt %>"></a></td>
	<% } %>
	<% if (Personas.IdCargo.Visible) { // IdCargo %>
		<td<%= Personas.IdCargo.CellAttributes %>>
<div<%= Personas.IdCargo.ViewAttributes %>><%= Personas.IdCargo.ListViewValue %></div>
</td>
	<% } %>
	<% if (Personas.Documento.Visible) { // Documento %>
		<td<%= Personas.Documento.CellAttributes %>>
<div<%= Personas.Documento.ViewAttributes %>><%= Personas.Documento.ListViewValue %></div>
</td>
	<% } %>
	<% if (Personas.Persona.Visible) { // Persona %>
		<td<%= Personas.Persona.CellAttributes %>>
<div<%= Personas.Persona.ViewAttributes %>><%= Personas.Persona.ListViewValue %></div>
</td>
	<% } %>
	<% if (Personas.Activa.Visible) { // Activa %>
		<td<%= Personas.Activa.CellAttributes %>>
<% if (Convert.ToString(Personas.Activa.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %>
</td>
	<% } %>
<%

		// Render list options (body, right)
		Personas_list.ListOptions.Render("body", "right");
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
if (Personas_list.Recordset != null) {
	Personas_list.Recordset.Close();
	Personas_list.Recordset.Dispose();
}
%>
<% if (Personas_list.TotalRecs > 0) { %>
<% if (ew_Empty(Personas.Export)) { %>
<div class="ewGridLowerPanel">
<% if (Personas.CurrentAction != "gridadd" && Personas.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (Personas_list.Pager == null) Personas_list.Pager = new cNumericPager(Personas_list.StartRec, Personas_list.DisplayRecs, Personas_list.TotalRecs, Personas_list.RecRange); %>
<% if (Personas_list.Pager.RecordCount > 0) { %>
	<% if (Personas_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= Personas_list.PageUrl %>start=<%= Personas_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (Personas_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= Personas_list.PageUrl %>start=<%= Personas_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in Personas_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= Personas_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (Personas_list.Pager.NextButton.Enabled) { %>
	<a href="<%= Personas_list.PageUrl %>start=<%= Personas_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (Personas_list.Pager.LastButton.Enabled) { %>
	<a href="<%= Personas_list.PageUrl %>start=<%= Personas_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (Personas_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= Personas_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= Personas_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= Personas_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (Personas_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= Personas_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% if (VehiculosAutorizados.DetailAdd && Security.AllowAdd("VehiculosAutorizados")) { %>
<a class="ewGridLink" href="<%= Personas.AddUrl + "?" + EW_TABLE_SHOW_DETAIL + "=VehiculosAutorizados" %>"><%= Language.Phrase("AddLink") %>&nbsp;<%= Personas.TableCaption %>/<%= VehiculosAutorizados.TableCaption %></a>&nbsp;&nbsp;
<% } %>
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(Personas.Export) && ew_Empty(Personas.CurrentAction)) { %>
<% } %>
<%
Personas_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(Personas.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
