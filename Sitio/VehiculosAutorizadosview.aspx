<%@ Page ClassName="VehiculosAutorizadosview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="VehiculosAutorizadosview.aspx.cs" Inherits="VehiculosAutorizadosview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(VehiculosAutorizados.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var VehiculosAutorizados_view = new ew_Page("VehiculosAutorizados_view");

// page properties
VehiculosAutorizados_view.PageID = "view"; // page ID
VehiculosAutorizados_view.FormID = "fVehiculosAutorizadosview"; // form ID 
var EW_PAGE_ID = VehiculosAutorizados_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
VehiculosAutorizados_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
VehiculosAutorizados_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
VehiculosAutorizados_view.ValidateRequired = false; // no JavaScript validation
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
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= VehiculosAutorizados.TableCaption %>
&nbsp;&nbsp;<% VehiculosAutorizados_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(VehiculosAutorizados.Export)) { %>
<p class="aspnetmaker">
<a href="<%= VehiculosAutorizados_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<a href="<%= VehiculosAutorizados_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% if (Security.CanEdit) { %>
<a href="<%= VehiculosAutorizados_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% if (Security.CanDelete) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= VehiculosAutorizados_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% if (Security.AllowList("VehiculosPicoYPlacaHoras")) { %>
<%
sSqlWrk = "[IdVehiculoAutorizado]=" + ew_AdjustSql(VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue) + "";
sSqlWrk = cTEA.Encrypt(sSqlWrk, EW_RANDOM_KEY);
sSqlWrk = sSqlWrk.Replace("'", "\'");
%>
<a href="VehiculosPicoYPlacaHoraslist.aspx?<%= EW_TABLE_SHOW_MASTER %>=VehiculosAutorizados&IdVehiculoAutorizado=<%= ew_UrlEncode(Convert.ToString(VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue)) %>" name="ew_VehiculosAutorizados_VehiculosPicoYPlacaHoras_DetailLink" id="ew_VehiculosAutorizados_VehiculosPicoYPlacaHoras_DetailLink" onmouseover="ew_AjaxShowDetails(this, 'VehiculosPicoYPlacaHoraspreview.aspx?f=<%= sSqlWrk %>')" onmouseout="ew_AjaxHideDetails(this);"><%= Language.Phrase("ViewPageDetailLink") %><%= Language.TablePhrase("VehiculosPicoYPlacaHoras", "TblCaption") %>
</a>
&nbsp;
<% } %>
<% } %>
</p>
<% VehiculosAutorizados_view.ShowPageHeader(); %>
<% VehiculosAutorizados_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // IdVehiculoAutorizado %>
	<tr id="r_IdVehiculoAutorizado"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.IdVehiculoAutorizado.FldCaption %></td>
		<td<%= VehiculosAutorizados.IdVehiculoAutorizado.CellAttributes %>>
<div<%= VehiculosAutorizados.IdVehiculoAutorizado.ViewAttributes %>><%= VehiculosAutorizados.IdVehiculoAutorizado.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
	<tr id="r_IdTipoVehiculo"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.IdTipoVehiculo.FldCaption %></td>
		<td<%= VehiculosAutorizados.IdTipoVehiculo.CellAttributes %>>
<div<%= VehiculosAutorizados.IdTipoVehiculo.ViewAttributes %>><%= VehiculosAutorizados.IdTipoVehiculo.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Placa.Visible) { // Placa %>
	<tr id="r_Placa"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Placa.FldCaption %></td>
		<td<%= VehiculosAutorizados.Placa.CellAttributes %>>
<div<%= VehiculosAutorizados.Placa.ViewAttributes %>><%= VehiculosAutorizados.Placa.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Autorizado.Visible) { // Autorizado %>
	<tr id="r_Autorizado"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Autorizado.FldCaption %></td>
		<td<%= VehiculosAutorizados.Autorizado.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.IdPersona.Visible) { // IdPersona %>
	<tr id="r_IdPersona"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.IdPersona.FldCaption %></td>
		<td<%= VehiculosAutorizados.IdPersona.CellAttributes %>>
<div<%= VehiculosAutorizados.IdPersona.ViewAttributes %>><%= VehiculosAutorizados.IdPersona.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.PicoyPlaca.Visible) { // PicoyPlaca %>
	<tr id="r_PicoyPlaca"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.PicoyPlaca.FldCaption %></td>
		<td<%= VehiculosAutorizados.PicoyPlaca.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Lunes.Visible) { // Lunes %>
	<tr id="r_Lunes"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Lunes.FldCaption %></td>
		<td<%= VehiculosAutorizados.Lunes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Martes.Visible) { // Martes %>
	<tr id="r_Martes"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Martes.FldCaption %></td>
		<td<%= VehiculosAutorizados.Martes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Miercoles.Visible) { // Miercoles %>
	<tr id="r_Miercoles"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Miercoles.FldCaption %></td>
		<td<%= VehiculosAutorizados.Miercoles.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Jueves.Visible) { // Jueves %>
	<tr id="r_Jueves"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Jueves.FldCaption %></td>
		<td<%= VehiculosAutorizados.Jueves.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Viernes.Visible) { // Viernes %>
	<tr id="r_Viernes"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Viernes.FldCaption %></td>
		<td<%= VehiculosAutorizados.Viernes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Sabado.Visible) { // Sabado %>
	<tr id="r_Sabado"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Sabado.FldCaption %></td>
		<td<%= VehiculosAutorizados.Sabado.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Domingo.Visible) { // Domingo %>
	<tr id="r_Domingo"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Domingo.FldCaption %></td>
		<td<%= VehiculosAutorizados.Domingo.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
<% if (VehiculosAutorizados.Marca.Visible) { // Marca %>
	<tr id="r_Marca"<%= VehiculosAutorizados.RowAttributes %>>
		<td class="ewTableHeader"><%= VehiculosAutorizados.Marca.FldCaption %></td>
		<td<%= VehiculosAutorizados.Marca.CellAttributes %>>
<div<%= VehiculosAutorizados.Marca.ViewAttributes %>><%= VehiculosAutorizados.Marca.ViewValue %></div>
</td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
VehiculosAutorizados_view.ShowPageFooter();
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
