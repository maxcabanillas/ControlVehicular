<%@ Page ClassName="RegistrosVehiculosview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="RegistrosVehiculosview.aspx.cs" Inherits="RegistrosVehiculosview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(RegistrosVehiculos.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var RegistrosVehiculos_view = new ew_Page("RegistrosVehiculos_view");

// page properties
RegistrosVehiculos_view.PageID = "view"; // page ID
RegistrosVehiculos_view.FormID = "fRegistrosVehiculosview"; // form ID 
var EW_PAGE_ID = RegistrosVehiculos_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
RegistrosVehiculos_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosVehiculos_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosVehiculos_view.ValidateRequired = false; // no JavaScript validation
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
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= RegistrosVehiculos.TableCaption %>
&nbsp;&nbsp;<% RegistrosVehiculos_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(RegistrosVehiculos.Export)) { %>
<p class="aspnetmaker">
<a href="<%= RegistrosVehiculos_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<a href="<%= RegistrosVehiculos_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% if (Security.CanEdit) { %>
<a href="<%= RegistrosVehiculos_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% if (Security.CanDelete) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= RegistrosVehiculos_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% } %>
</p>
<% RegistrosVehiculos_view.ShowPageHeader(); %>
<% RegistrosVehiculos_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (RegistrosVehiculos.IdRegistroVehiculo.Visible) { // IdRegistroVehiculo %>
	<tr id="r_IdRegistroVehiculo"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.IdRegistroVehiculo.FldCaption %></td>
		<td<%= RegistrosVehiculos.IdRegistroVehiculo.CellAttributes %>>
<div<%= RegistrosVehiculos.IdRegistroVehiculo.ViewAttributes %>><%= RegistrosVehiculos.IdRegistroVehiculo.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
	<tr id="r_IdTipoVehiculo"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.IdTipoVehiculo.FldCaption %></td>
		<td<%= RegistrosVehiculos.IdTipoVehiculo.CellAttributes %>>
<div<%= RegistrosVehiculos.IdTipoVehiculo.ViewAttributes %>><%= RegistrosVehiculos.IdTipoVehiculo.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.Placa.Visible) { // Placa %>
	<tr id="r_Placa"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.Placa.FldCaption %></td>
		<td<%= RegistrosVehiculos.Placa.CellAttributes %>>
<div<%= RegistrosVehiculos.Placa.ViewAttributes %>><%= RegistrosVehiculos.Placa.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.FechaIngreso.Visible) { // FechaIngreso %>
	<tr id="r_FechaIngreso"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.FechaIngreso.FldCaption %></td>
		<td<%= RegistrosVehiculos.FechaIngreso.CellAttributes %>>
<div<%= RegistrosVehiculos.FechaIngreso.ViewAttributes %>><%= RegistrosVehiculos.FechaIngreso.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.FechaSalida.Visible) { // FechaSalida %>
	<tr id="r_FechaSalida"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.FechaSalida.FldCaption %></td>
		<td<%= RegistrosVehiculos.FechaSalida.CellAttributes %>>
<div<%= RegistrosVehiculos.FechaSalida.ViewAttributes %>><%= RegistrosVehiculos.FechaSalida.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.Observaciones.Visible) { // Observaciones %>
	<tr id="r_Observaciones"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.Observaciones.FldCaption %></td>
		<td<%= RegistrosVehiculos.Observaciones.CellAttributes %>>
<div<%= RegistrosVehiculos.Observaciones.ViewAttributes %>><%= RegistrosVehiculos.Observaciones.ViewValue %></div>
</td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
RegistrosVehiculos_view.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(RegistrosVehiculos.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
