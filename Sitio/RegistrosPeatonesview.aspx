<%@ Page ClassName="RegistrosPeatonesview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="RegistrosPeatonesview.aspx.cs" Inherits="RegistrosPeatonesview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(RegistrosPeatones.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var RegistrosPeatones_view = new ew_Page("RegistrosPeatones_view");

// page properties
RegistrosPeatones_view.PageID = "view"; // page ID
RegistrosPeatones_view.FormID = "fRegistrosPeatonesview"; // form ID 
var EW_PAGE_ID = RegistrosPeatones_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
RegistrosPeatones_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosPeatones_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosPeatones_view.ValidateRequired = false; // no JavaScript validation
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
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= RegistrosPeatones.TableCaption %>
&nbsp;&nbsp;<% RegistrosPeatones_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(RegistrosPeatones.Export)) { %>
<p class="aspnetmaker">
<a href="<%= RegistrosPeatones_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<a href="<%= RegistrosPeatones_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% if (Security.CanEdit) { %>
<a href="<%= RegistrosPeatones_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% if (Security.CanDelete) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= RegistrosPeatones_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% } %>
</p>
<% RegistrosPeatones_view.ShowPageHeader(); %>
<% RegistrosPeatones_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (RegistrosPeatones.IdRegistroPeaton.Visible) { // IdRegistroPeaton %>
	<tr id="r_IdRegistroPeaton"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.IdRegistroPeaton.FldCaption %></td>
		<td<%= RegistrosPeatones.IdRegistroPeaton.CellAttributes %>>
<div<%= RegistrosPeatones.IdRegistroPeaton.ViewAttributes %>><%= RegistrosPeatones.IdRegistroPeaton.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosPeatones.IdTipoDocumento.Visible) { // IdTipoDocumento %>
	<tr id="r_IdTipoDocumento"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.IdTipoDocumento.FldCaption %></td>
		<td<%= RegistrosPeatones.IdTipoDocumento.CellAttributes %>>
<div<%= RegistrosPeatones.IdTipoDocumento.ViewAttributes %>><%= RegistrosPeatones.IdTipoDocumento.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosPeatones.Documento.Visible) { // Documento %>
	<tr id="r_Documento"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.Documento.FldCaption %></td>
		<td<%= RegistrosPeatones.Documento.CellAttributes %>>
<div<%= RegistrosPeatones.Documento.ViewAttributes %>><%= RegistrosPeatones.Documento.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosPeatones.Nombre.Visible) { // Nombre %>
	<tr id="r_Nombre"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.Nombre.FldCaption %></td>
		<td<%= RegistrosPeatones.Nombre.CellAttributes %>>
<div<%= RegistrosPeatones.Nombre.ViewAttributes %>><%= RegistrosPeatones.Nombre.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosPeatones.Apellidos.Visible) { // Apellidos %>
	<tr id="r_Apellidos"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.Apellidos.FldCaption %></td>
		<td<%= RegistrosPeatones.Apellidos.CellAttributes %>>
<div<%= RegistrosPeatones.Apellidos.ViewAttributes %>><%= RegistrosPeatones.Apellidos.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosPeatones.IdArea.Visible) { // IdArea %>
	<tr id="r_IdArea"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.IdArea.FldCaption %></td>
		<td<%= RegistrosPeatones.IdArea.CellAttributes %>>
<div<%= RegistrosPeatones.IdArea.ViewAttributes %>><%= RegistrosPeatones.IdArea.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosPeatones.IdPersona.Visible) { // IdPersona %>
	<tr id="r_IdPersona"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.IdPersona.FldCaption %></td>
		<td<%= RegistrosPeatones.IdPersona.CellAttributes %>>
<div<%= RegistrosPeatones.IdPersona.ViewAttributes %>><%= RegistrosPeatones.IdPersona.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosPeatones.FechaIngreso.Visible) { // FechaIngreso %>
	<tr id="r_FechaIngreso"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.FechaIngreso.FldCaption %></td>
		<td<%= RegistrosPeatones.FechaIngreso.CellAttributes %>>
<div<%= RegistrosPeatones.FechaIngreso.ViewAttributes %>><%= RegistrosPeatones.FechaIngreso.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosPeatones.FechaSalida.Visible) { // FechaSalida %>
	<tr id="r_FechaSalida"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.FechaSalida.FldCaption %></td>
		<td<%= RegistrosPeatones.FechaSalida.CellAttributes %>>
<div<%= RegistrosPeatones.FechaSalida.ViewAttributes %>><%= RegistrosPeatones.FechaSalida.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (RegistrosPeatones.Observacion.Visible) { // Observacion %>
	<tr id="r_Observacion"<%= RegistrosPeatones.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosPeatones.Observacion.FldCaption %></td>
		<td<%= RegistrosPeatones.Observacion.CellAttributes %>>
<div<%= RegistrosPeatones.Observacion.ViewAttributes %>><%= RegistrosPeatones.Observacion.ViewValue %></div>
</td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
RegistrosPeatones_view.ShowPageFooter();
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
