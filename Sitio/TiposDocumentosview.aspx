<%@ Page ClassName="TiposDocumentosview" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="TiposDocumentosview.aspx.cs" Inherits="TiposDocumentosview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<% if (ew_Empty(TiposDocumentos.Export)) { %>
<script type="text/javascript">
<!--

// Create page object
var TiposDocumentos_view = new ew_Page("TiposDocumentos_view");

// page properties
TiposDocumentos_view.PageID = "view"; // page ID
TiposDocumentos_view.FormID = "fTiposDocumentosview"; // form ID 
var EW_PAGE_ID = TiposDocumentos_view.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
TiposDocumentos_view.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
TiposDocumentos_view.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
TiposDocumentos_view.ValidateRequired = false; // no JavaScript validation
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
<p class="aspnetmaker ewTitle"><%= Language.Phrase("View") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= TiposDocumentos.TableCaption %>
&nbsp;&nbsp;<% TiposDocumentos_view.ExportOptions.Render("body"); %>
</p>
<% if (ew_Empty(TiposDocumentos.Export)) { %>
<p class="aspnetmaker">
<a href="<%= TiposDocumentos_view.ListUrl %>"><%= Language.Phrase("BackToList") %></a>&nbsp;
<% if (Security.CanAdd) { %>
<a href="<%= TiposDocumentos_view.AddUrl %>"><%= Language.Phrase("ViewPageAddLink") %></a>&nbsp;
<% } %>
<% if (Security.CanEdit) { %>
<a href="<%= TiposDocumentos_view.EditUrl %>"><%= Language.Phrase("ViewPageEditLink") %></a>&nbsp;
<% } %>
<% if (Security.CanDelete) { %>
<a onclick="return ew_Confirm(ewLanguage.Phrase('DeleteConfirmMsg'));" href="<%= TiposDocumentos_view.DeleteUrl %>"><%= Language.Phrase("ViewPageDeleteLink") %></a>&nbsp;
<% } %>
<% } %>
</p>
<% TiposDocumentos_view.ShowPageHeader(); %>
<% TiposDocumentos_view.ShowMessage(); %>
<p />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (TiposDocumentos.IdTipoDocumento.Visible) { // IdTipoDocumento %>
	<tr id="r_IdTipoDocumento"<%= TiposDocumentos.RowAttributes %>>
		<td class="ewTableHeader"><%= TiposDocumentos.IdTipoDocumento.FldCaption %></td>
		<td<%= TiposDocumentos.IdTipoDocumento.CellAttributes %>>
<div<%= TiposDocumentos.IdTipoDocumento.ViewAttributes %>><%= TiposDocumentos.IdTipoDocumento.ViewValue %></div>
</td>
	</tr>
<% } %>
<% if (TiposDocumentos.TipoDocumento.Visible) { // TipoDocumento %>
	<tr id="r_TipoDocumento"<%= TiposDocumentos.RowAttributes %>>
		<td class="ewTableHeader"><%= TiposDocumentos.TipoDocumento.FldCaption %></td>
		<td<%= TiposDocumentos.TipoDocumento.CellAttributes %>>
<div<%= TiposDocumentos.TipoDocumento.ViewAttributes %>><%= TiposDocumentos.TipoDocumento.ViewValue %></div>
</td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<%
TiposDocumentos_view.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<% if (ew_Empty(TiposDocumentos.Export)) { %>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
<% } %>
</asp:Content>
