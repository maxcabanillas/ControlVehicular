<%@ Page ClassName="TiposVehiculosview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="TiposVehiculosview.aspx.cs" Inherits="TiposVehiculosview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(TiposVehiculos.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var TiposVehiculos_view = new ew_Page("TiposVehiculos_view");

// page properties
TiposVehiculos_view.PageID = "view"; // page ID
TiposVehiculos_view.FormID = "fTiposVehiculosview"; // form ID 
var EW_PAGE_ID = TiposVehiculos_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
TiposVehiculos_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
TiposVehiculos_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
TiposVehiculos_view.ValidateRequired = false; // no JavaScript validation
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
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= TiposVehiculos.TableCaption %>
&nbsp;&nbsp;<% TiposVehiculos_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(TiposVehiculos.Export)) { %>
<p class="aspnetmaker">
<a href="<%= TiposVehiculos_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<a href="<%= TiposVehiculos_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% if (Security.CanEdit) { %>
<a href="<%= TiposVehiculos_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% if (Security.CanDelete) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= TiposVehiculos_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% } %>
</p>
<% TiposVehiculos_view.ShowPageHeader(); %>
<% TiposVehiculos_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (TiposVehiculos.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
	<tr id="r_IdTipoVehiculo"<%= TiposVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= TiposVehiculos.IdTipoVehiculo.FldCaption %></td>
		<td<%= TiposVehiculos.IdTipoVehiculo.CellAttributes %>>
<div<%= TiposVehiculos.IdTipoVehiculo.ViewAttributes %>><%= TiposVehiculos.IdTipoVehiculo.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (TiposVehiculos.TipoVehiculo.Visible) { // TipoVehiculo %>
	<tr id="r_TipoVehiculo"<%= TiposVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= TiposVehiculos.TipoVehiculo.FldCaption %></td>
		<td<%= TiposVehiculos.TipoVehiculo.CellAttributes %>>
<div<%= TiposVehiculos.TipoVehiculo.ViewAttributes %>><%= TiposVehiculos.TipoVehiculo.ViewValue %></div>
</td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
TiposVehiculos_view.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(TiposVehiculos.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
