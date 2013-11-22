<%@ Page ClassName="Usuariosview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="Usuariosview.aspx.cs" Inherits="Usuariosview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(Usuarios.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var Usuarios_view = new ew_Page("Usuarios_view");

// page properties
Usuarios_view.PageID = "view"; // page ID
Usuarios_view.FormID = "fUsuariosview"; // form ID 
var EW_PAGE_ID = Usuarios_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Usuarios_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Usuarios_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Usuarios_view.ValidateRequired = false; // no JavaScript validation
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
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Usuarios.TableCaption %>
&nbsp;&nbsp;<% Usuarios_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(Usuarios.Export)) { %>
<p class="aspnetmaker">
<a href="<%= Usuarios_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<% if (Usuarios_view.ShowOptionLink()) { %>
<a href="<%= Usuarios_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% } %>
<% if (Security.CanEdit) { %>
<% if (Usuarios_view.ShowOptionLink()) { %>
<a href="<%= Usuarios_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% } %>
<% if (Security.CanDelete) { %>
<% if (Usuarios_view.ShowOptionLink()) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= Usuarios_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% } %>
<% } %>
</p>
<% Usuarios_view.ShowPageHeader(); %>
<% Usuarios_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (Usuarios.IdUsuario.Visible) { // IdUsuario %>
	<tr id="r_IdUsuario"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.IdUsuario.FldCaption %></td>
		<td<%= Usuarios.IdUsuario.CellAttributes %>>
<div<%= Usuarios.IdUsuario.ViewAttributes %>><%= Usuarios.IdUsuario.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Usuarios.Usuario.Visible) { // Usuario %>
	<tr id="r_Usuario"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.Usuario.FldCaption %></td>
		<td<%= Usuarios.Usuario.CellAttributes %>>
<div<%= Usuarios.Usuario.ViewAttributes %>><%= Usuarios.Usuario.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Usuarios.NombreUsuario.Visible) { // NombreUsuario %>
	<tr id="r_NombreUsuario"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.NombreUsuario.FldCaption %></td>
		<td<%= Usuarios.NombreUsuario.CellAttributes %>>
<div<%= Usuarios.NombreUsuario.ViewAttributes %>><%= Usuarios.NombreUsuario.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Usuarios.Password.Visible) { // Password %>
	<tr id="r_Password"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.Password.FldCaption %></td>
		<td<%= Usuarios.Password.CellAttributes %>>
<div<%= Usuarios.Password.ViewAttributes %>><%= Usuarios.Password.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Usuarios.Correo.Visible) { // Correo %>
	<tr id="r_Correo"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.Correo.FldCaption %></td>
		<td<%= Usuarios.Correo.CellAttributes %>>
<div<%= Usuarios.Correo.ViewAttributes %>><%= Usuarios.Correo.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Usuarios.IdUsuarioNivel.Visible) { // IdUsuarioNivel %>
	<tr id="r_IdUsuarioNivel"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.IdUsuarioNivel.FldCaption %></td>
		<td<%= Usuarios.IdUsuarioNivel.CellAttributes %>>
<div<%= Usuarios.IdUsuarioNivel.ViewAttributes %>><%= Usuarios.IdUsuarioNivel.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Usuarios.Activo.Visible) { // Activo %>
	<tr id="r_Activo"<%= Usuarios.RowAttributes %>>
		<td class="ewTableHeader"><%= Usuarios.Activo.FldCaption %></td>
		<td<%= Usuarios.Activo.CellAttributes %>>
<% if (Convert.ToString(Usuarios.Activo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Usuarios.Activo.ViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Usuarios.Activo.ViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
Usuarios_view.ShowPageFooter();
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
