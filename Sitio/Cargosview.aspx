<%@ Page ClassName="Cargosview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="Cargosview.aspx.cs" Inherits="Cargosview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(Cargos.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var Cargos_view = new ew_Page("Cargos_view");

// page properties
Cargos_view.PageID = "view"; // page ID
Cargos_view.FormID = "fCargosview"; // form ID 
var EW_PAGE_ID = Cargos_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Cargos_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Cargos_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Cargos_view.ValidateRequired = false; // no JavaScript validation
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
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Cargos.TableCaption %>
&nbsp;&nbsp;<% Cargos_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(Cargos.Export)) { %>
<p class="aspnetmaker">
<a href="<%= Cargos_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<a href="<%= Cargos_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% if (Security.CanEdit) { %>
<a href="<%= Cargos_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% if (Security.CanAdd) { %>
<a href="<%= Cargos_view.CopyUrl %>"><%= Language.Phrase("ViewPageCopyLink") %></a>&nbsp;
<% } %>
<% if (Security.CanDelete) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= Cargos_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% } %>
</p>
<% Cargos_view.ShowPageHeader(); %>
<% Cargos_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (Cargos.IdCargo.Visible) { // IdCargo %>
	<tr id="r_IdCargo"<%= Cargos.RowAttributes %>>
		<td class="ewTableHeader"><%= Cargos.IdCargo.FldCaption %></td>
		<td<%= Cargos.IdCargo.CellAttributes %>>
<div<%= Cargos.IdCargo.ViewAttributes %>><%= Cargos.IdCargo.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (Cargos.Cargo.Visible) { // Cargo %>
	<tr id="r_Cargo"<%= Cargos.RowAttributes %>>
		<td class="ewTableHeader"><%= Cargos.Cargo.FldCaption %></td>
		<td<%= Cargos.Cargo.CellAttributes %>>
<div<%= Cargos.Cargo.ViewAttributes %>><%= Cargos.Cargo.ViewValue %></div>
</td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
Cargos_view.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(Cargos.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
