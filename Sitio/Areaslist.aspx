<%@ Page ClassName="Areaslist" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Areaslist.aspx.cs" Inherits="Areaslist" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(Areas.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var Areas_list = new ew_Page("Areas_list");

// page properties
Areas_list.PageID = "list"; // page ID
Areas_list.FormID = "fAreaslist"; // form ID 
var EW_PAGE_ID = Areas_list.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Areas_list.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Areas_list.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Areas_list.ValidateRequired = false; // no JavaScript validation
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
<% if (ew_Empty(Areas.Export) || (EW_EXPORT_MASTER_RECORD && Areas.Export == "print")) { %>
<% } %>
<%
	Areas_list.Recordset = Areas_list.LoadRecordset();
	Areas_list.StartRec = 1;
	if (Areas_list.DisplayRecs <= 0) // Display all records
		Areas_list.DisplayRecs = Areas_list.TotalRecs;
	if (!(Areas.ExportAll && ew_NotEmpty(Areas.Export)))
		Areas_list.SetUpStartRec(); // Set up start record position
%>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= Areas.TableCaption %>
&nbsp;&nbsp;<% Areas_list.ExportOptions.Render("body"); %>
</p>
<% Areas_list.ShowPageHeader(); %>
<% Areas_list.ShowMessage(); %>
<br />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<% if (ew_Empty(Areas.Export)) { %>
<div class="ewGridUpperPanel">
<% if (Areas.CurrentAction != "gridadd" && Areas.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (Areas_list.Pager == null) Areas_list.Pager = new cNumericPager(Areas_list.StartRec, Areas_list.DisplayRecs, Areas_list.TotalRecs, Areas_list.RecRange); %>
<% if (Areas_list.Pager.RecordCount > 0) { %>
	<% if (Areas_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= Areas_list.PageUrl %>start=<%= Areas_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (Areas_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= Areas_list.PageUrl %>start=<%= Areas_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in Areas_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= Areas_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (Areas_list.Pager.NextButton.Enabled) { %>
	<a href="<%= Areas_list.PageUrl %>start=<%= Areas_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (Areas_list.Pager.LastButton.Enabled) { %>
	<a href="<%= Areas_list.PageUrl %>start=<%= Areas_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (Areas_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= Areas_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= Areas_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= Areas_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (Areas_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= Areas_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% if (Personas.DetailAdd && Security.AllowAdd("Personas")) { %>
<a class="ewGridLink" href="<%= Areas.AddUrl + "?" + EW_TABLE_SHOW_DETAIL + "=Personas" %>"><%= Language.Phrase("AddLink") %>&nbsp;<%= Areas.TableCaption %>/<%= Personas.TableCaption %></a>&nbsp;&nbsp;
<% } %>
<% } %>
</div>
</div>
<% } %>
<form name="fAreaslist" id="fAreaslist" class="ewForm" method="post">
<input type="hidden" name="t" id="t" value="Areas" />
<div id="gmp_Areas" class="ewGridMiddlePanel">
<% if (Areas_list.TotalRecs > 0) { %>
<table cellspacing="0" data-rowhighlightclass="ewTableHighlightRow" data-rowselectclass="ewTableSelectRow" data-roweditclass="ewTableEditRow" class="ewTable ewTableSeparate">
<%= Areas.TableCustomInnerHtml %>
<thead><!-- Table header -->
	<tr class="ewTableHeader">
<%

		// Render list options
		Areas_list.RenderListOptions();

		// Render list options (header, left)
		Areas_list.ListOptions.Render("header", "left");
%>
<% if (Areas.Area.Visible) { // Area %>
	<% if (ew_Empty(Areas.SortUrl(Areas.Area))) { %>
		<td><%= Areas.Area.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Areas.SortUrl(Areas.Area) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Areas.Area.FldCaption %></td><td style="width: 10px;"><% if (Areas.Area.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Areas.Area.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<% if (Areas.Codigo.Visible) { // Codigo %>
	<% if (ew_Empty(Areas.SortUrl(Areas.Codigo))) { %>
		<td><%= Areas.Codigo.FldCaption %></td>
	<% } else { %>
		<td><div class="ewPointer" onmousedown="ew_Sort(event,'<%= Areas.SortUrl(Areas.Codigo) %>',2);">
			<table cellspacing="0" class="ewTableHeaderBtn"><thead><tr><td><%= Areas.Codigo.FldCaption %></td><td style="width: 10px;"><% if (Areas.Codigo.Sort == "ASC") { %><img src="aspximages/sortup.gif" width="10" height="9" border="0"><% } else if (Areas.Codigo.Sort == "DESC") { %><img src="aspximages/sortdown.gif" width="10" height="9" border="0"><% } %></td></tr></thead></table>
		</div></td>		
	<% } %>
<% } %>		
<%

		// Render list options (header, right)
		Areas_list.ListOptions.Render("header", "right");
%>
	</tr>
</thead>
<tbody><!-- Table body -->
<%
if (Areas.ExportAll && ew_NotEmpty(Areas.Export)) {
	Areas_list.StopRec = Areas_list.TotalRecs;
} else {

	// Set the last record to display
	if (Areas_list.TotalRecs > Areas_list.StartRec + Areas_list.DisplayRecs - 1) {
		Areas_list.StopRec = Areas_list.StartRec + Areas_list.DisplayRecs - 1;
	} else {
		Areas_list.StopRec = Areas_list.TotalRecs;
	}
}
if (Areas_list.Recordset != null && Areas_list.Recordset.HasRows) {

	// Move to first record
	for (int i = 1; i <= Areas_list.StartRec - 1; i++) {
		if (Areas_list.Recordset.Read())
			Areas_list.RecCnt += 1;
	}		
} else if (!Areas.AllowAddDeleteRow && Areas_list.StopRec == 0) {
	Areas_list.StopRec = Areas.GridAddRowCount;
}

// Initialize Aggregate
Areas.RowType = EW_ROWTYPE_AGGREGATEINIT;
Areas.ResetAttrs();
Areas_list.RenderRow();
Areas_list.RowCnt = 0;

// Output data rows
bool Eof = false; // ASPX
while (Areas_list.RecCnt < Areas_list.StopRec) {
	if (Areas.CurrentAction != "gridadd" && !Eof) // ASPX
		Eof = !Areas_list.Recordset.Read();
	Areas_list.RecCnt += 1;
	if (Areas_list.RecCnt >= Areas_list.StartRec) {
		Areas_list.RowCnt += 1;

		// Set up key count
		Areas_list.KeyCount = ew_ConvertToInt(Areas_list.RowIndex);

		// Init row class and style
		Areas.ResetAttrs();
		Areas.CssClass = "";	 
		if (Areas.CurrentAction == "gridadd") {
		} else {
			Areas_list.LoadRowValues(ref Areas_list.Recordset); // Load row values
		}
		Areas.RowType = EW_ROWTYPE_VIEW; // Render view
		ew_SetAttr(ref Areas.RowAttrs, new string[,] {{"onmouseover","ew_MouseOver(event, this);"}, {"onmouseout","ew_MouseOut(event, this);"}, {"onclick","ew_Click(event, this);"}});

		// Render row
		Areas_list.RenderRow();

		// Render list options
		Areas_list.RenderListOptions();
%>
	<tr<%= Areas.RowAttributes %>>
	<%

		// Render list options (body, left)
		Areas_list.ListOptions.Render("body", "left");
	%>
	<% if (Areas.Area.Visible) { // Area %>
		<td<%= Areas.Area.CellAttributes %>>
<div<%= Areas.Area.ViewAttributes %>><%= Areas.Area.ListViewValue %></div>
<a name="<%= Areas_list.PageObjName + "_row_" + Areas_list.RowCnt %>" id="<%= Areas_list.PageObjName + "_row_" + Areas_list.RowCnt %>"></a></td>
	<% } %>
	<% if (Areas.Codigo.Visible) { // Codigo %>
		<td<%= Areas.Codigo.CellAttributes %>>
<div<%= Areas.Codigo.ViewAttributes %>><%= Areas.Codigo.ListViewValue %></div>
</td>
	<% } %>
<%

		// Render list options (body, right)
		Areas_list.ListOptions.Render("body", "right");
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
if (Areas_list.Recordset != null) {
	Areas_list.Recordset.Close();
	Areas_list.Recordset.Dispose();
}
%>
<% if (Areas_list.TotalRecs > 0) { %>
<% if (ew_Empty(Areas.Export)) { %>
<div class="ewGridLowerPanel">
<% if (Areas.CurrentAction != "gridadd" && Areas.CurrentAction != "gridedit") { %>
<form name="ewpagerform" id="ewpagerform" class="ewForm">
<table border="0" cellspacing="0" cellpadding="0" class="ewPager">
	<tr>
		<td>
<span class="aspnetmaker">
<% if (Areas_list.Pager == null) Areas_list.Pager = new cNumericPager(Areas_list.StartRec, Areas_list.DisplayRecs, Areas_list.TotalRecs, Areas_list.RecRange); %>
<% if (Areas_list.Pager.RecordCount > 0) { %>
	<% if (Areas_list.Pager.FirstButton.Enabled) { %>
	<a href="<%= Areas_list.PageUrl %>start=<%= Areas_list.Pager.FirstButton.Start %>"><%= Language.Phrase("PagerFirst") %></a>&nbsp;
	<% } %>
	<% if (Areas_list.Pager.PrevButton.Enabled) { %>
	<a href="<%= Areas_list.PageUrl %>start=<%= Areas_list.Pager.PrevButton.Start %>"><%= Language.Phrase("PagerPrevious") %></a>&nbsp;
	<% } %>
	<% foreach (cPagerItem PagerItem in Areas_list.Pager.Items) { %>
		<% if (PagerItem.Enabled) { %><a href="<%= Areas_list.PageUrl %>start=<%= PagerItem.Start %>"><% } %><%= PagerItem.Text %><% if (PagerItem.Enabled) { %></a><% } %>&nbsp;
	<% } %>
	<% if (Areas_list.Pager.NextButton.Enabled) { %>
	<a href="<%= Areas_list.PageUrl %>start=<%= Areas_list.Pager.NextButton.Start %>"><%= Language.Phrase("PagerNext") %></a>&nbsp;
	<% } %>
	<% if (Areas_list.Pager.LastButton.Enabled) { %>
	<a href="<%= Areas_list.PageUrl %>start=<%= Areas_list.Pager.LastButton.Start %>"><%= Language.Phrase("PagerLast") %></a>&nbsp;
	<% } %>
	<% if (Areas_list.Pager.ButtonCount > 0) { %>&nbsp;&nbsp;&nbsp;&nbsp;<%	} %>
	<%= Language.Phrase("Record") %>&nbsp;<%= Areas_list.Pager.FromIndex %>&nbsp;<%= Language.Phrase("To") %>&nbsp;<%= Areas_list.Pager.ToIndex %>&nbsp;<%= Language.Phrase("Of") %>&nbsp;<%= Areas_list.Pager.RecordCount %>
<% } else { %>
	<% if (Security.CanList) { %>
	<% if (Areas_list.SearchWhere == "0=101") { %>
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
<a class="ewGridLink" href="<%= Areas_list.AddUrl %>"><%= Language.Phrase("AddLink") %></a>&nbsp;&nbsp;
<% if (Personas.DetailAdd && Security.AllowAdd("Personas")) { %>
<a class="ewGridLink" href="<%= Areas.AddUrl + "?" + EW_TABLE_SHOW_DETAIL + "=Personas" %>"><%= Language.Phrase("AddLink") %>&nbsp;<%= Areas.TableCaption %>/<%= Personas.TableCaption %></a>&nbsp;&nbsp;
<% } %>
<% } %>
</div>
</div>
<% } %>
<% } %>
</td></tr></table>
<% if (ew_Empty(Areas.Export) && ew_Empty(Areas.CurrentAction)) { %>
<% } %>
<%
Areas_list.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(Areas.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
