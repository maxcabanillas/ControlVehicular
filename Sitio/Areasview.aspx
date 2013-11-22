<%@ Page ClassName="Areasview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="Areasview.aspx.cs" Inherits="Areasview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(Areas.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var Areas_view = new ew_Page("Areas_view");

// page properties
Areas_view.PageID = "view"; // page ID
Areas_view.FormID = "fAreasview"; // form ID 
var EW_PAGE_ID = Areas_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Areas_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Areas_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Areas_view.ValidateRequired = false; // no JavaScript validation
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
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Areas.TableCaption %>
&nbsp;&nbsp;<% Areas_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(Areas.Export)) { %>
<p class="aspnetmaker">
<a href="<%= Areas_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<a href="<%= Areas_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% if (Security.CanEdit) { %>
<a href="<%= Areas_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% if (Security.CanDelete) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= Areas_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% if (Security.AllowList("Personas")) { %>
<%
sSqlWrk = "[IdArea]=" + ew_AdjustSql(Areas.IdArea.CurrentValue) + "";
sSqlWrk = cTEA.Encrypt(sSqlWrk, EW_RANDOM_KEY);
sSqlWrk = sSqlWrk.Replace("'", "\'");
%>
<a href="Personaslist.aspx?<%= EW_TABLE_SHOW_MASTER %>=Areas&IdArea=<%= ew_UrlEncode(Convert.ToString(Areas.IdArea.CurrentValue)) %>" name="ew_Areas_Personas_DetailLink" id="ew_Areas_Personas_DetailLink" onmouseover="ew_AjaxShowDetails(this, 'Personaspreview.aspx?f=<%= sSqlWrk %>')" onmouseout="ew_AjaxHideDetails(this);"><%= Language.Phrase("ViewPageDetailLink") %><%= Language.TablePhrase("Personas", "TblCaption") %>
</a>
&nbsp;
<% } %>
<% } %>
</p>
<% Areas_view.ShowPageHeader(); %>
<% Areas_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (Areas.IdArea.Visible) { // IdArea %>
	<tr id="r_IdArea"<%= Areas.RowAttributes %>>
		<td class="ewTableHeader"><%= Areas.IdArea.FldCaption %></td>
		<td<%= Areas.IdArea.CellAttributes %>>
<div<%= Areas.IdArea.ViewAttributes %>><%= Areas.IdArea.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Areas.Area.Visible) { // Area %>
	<tr id="r_Area"<%= Areas.RowAttributes %>>
		<td class="ewTableHeader"><%= Areas.Area.FldCaption %></td>
		<td<%= Areas.Area.CellAttributes %>>
<div<%= Areas.Area.ViewAttributes %>><%= Areas.Area.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Areas.Codigo.Visible) { // Codigo %>
	<tr id="r_Codigo"<%= Areas.RowAttributes %>>
		<td class="ewTableHeader"><%= Areas.Codigo.FldCaption %></td>
		<td<%= Areas.Codigo.CellAttributes %>>
<div<%= Areas.Codigo.ViewAttributes %>><%= Areas.Codigo.ViewValue %></div>
</td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
Areas_view.ShowPageFooter();
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
