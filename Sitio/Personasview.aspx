<%@ Page ClassName="Personasview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="Personasview.aspx.cs" Inherits="Personasview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(Personas.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var Personas_view = new ew_Page("Personas_view");

// page properties
Personas_view.PageID = "view"; // page ID
Personas_view.FormID = "fPersonasview"; // form ID 
var EW_PAGE_ID = Personas_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Personas_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Personas_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Personas_view.ValidateRequired = false; // no JavaScript validation
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
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Personas.TableCaption %>
&nbsp;&nbsp;<% Personas_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(Personas.Export)) { %>
<p class="aspnetmaker">
<a href="<%= Personas_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<a href="<%= Personas_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% if (Security.CanEdit) { %>
<a href="<%= Personas_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% if (Security.CanDelete) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= Personas_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% if (Security.AllowList("VehiculosAutorizados")) { %>
<%
sSqlWrk = "[IdPersona]=" + ew_AdjustSql(Personas.IdPersona.CurrentValue) + "";
sSqlWrk = cTEA.Encrypt(sSqlWrk, EW_RANDOM_KEY);
sSqlWrk = sSqlWrk.Replace("'", "\'");
%>
<a href="VehiculosAutorizadoslist.aspx?<%= EW_TABLE_SHOW_MASTER %>=Personas&IdPersona=<%= ew_UrlEncode(Convert.ToString(Personas.IdPersona.CurrentValue)) %>" name="ew_Personas_VehiculosAutorizados_DetailLink" id="ew_Personas_VehiculosAutorizados_DetailLink" onmouseover="ew_AjaxShowDetails(this, 'VehiculosAutorizadospreview.aspx?f=<%= sSqlWrk %>')" onmouseout="ew_AjaxHideDetails(this);"><%= Language.Phrase("ViewPageDetailLink") %><%= Language.TablePhrase("VehiculosAutorizados", "TblCaption") %>
</a>
&nbsp;
<% } %>
<% } %>
</p>
<% Personas_view.ShowPageHeader(); %>
<% Personas_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (Personas.IdPersona.Visible) { // IdPersona %>
	<tr id="r_IdPersona"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.IdPersona.FldCaption %></td>
		<td<%= Personas.IdPersona.CellAttributes %>>
<div<%= Personas.IdPersona.ViewAttributes %>><%= Personas.IdPersona.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Personas.IdArea.Visible) { // IdArea %>
	<tr id="r_IdArea"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.IdArea.FldCaption %></td>
		<td<%= Personas.IdArea.CellAttributes %>>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Personas.IdCargo.Visible) { // IdCargo %>
	<tr id="r_IdCargo"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.IdCargo.FldCaption %></td>
		<td<%= Personas.IdCargo.CellAttributes %>>
<div<%= Personas.IdCargo.ViewAttributes %>><%= Personas.IdCargo.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Personas.Documento.Visible) { // Documento %>
	<tr id="r_Documento"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.Documento.FldCaption %></td>
		<td<%= Personas.Documento.CellAttributes %>>
<div<%= Personas.Documento.ViewAttributes %>><%= Personas.Documento.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Personas.Persona.Visible) { // Persona %>
	<tr id="r_Persona"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.Persona.FldCaption %></td>
		<td<%= Personas.Persona.CellAttributes %>>
<div<%= Personas.Persona.ViewAttributes %>><%= Personas.Persona.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Personas.Activa.Visible) { // Activa %>
	<tr id="r_Activa"<%= Personas.RowAttributes %>>
		<td class="ewTableHeader"><%= Personas.Activa.FldCaption %></td>
		<td<%= Personas.Activa.CellAttributes %>>
<% if (Convert.ToString(Personas.Activa.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Personas.Activa.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Personas.Activa.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
Personas_view.ShowPageFooter();
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
