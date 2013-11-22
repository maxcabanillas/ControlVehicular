<%@ Page ClassName="TiposDocumentosdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="TiposDocumentosdelete.aspx.cs" Inherits="TiposDocumentosdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var TiposDocumentos_delete = new ew_Page("TiposDocumentos_delete");

// page properties
TiposDocumentos_delete.PageID = "delete"; // page ID
TiposDocumentos_delete.FormID = "fTiposDocumentosdelete"; // form ID 
var EW_PAGE_ID = TiposDocumentos_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
TiposDocumentos_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
TiposDocumentos_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
TiposDocumentos_delete.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<%

// Load records for display
TiposDocumentos_delete.Recordset = TiposDocumentos_delete.LoadRecordset();
if (TiposDocumentos_delete.TotalRecs <= 0) { // No record found, exit
	if (TiposDocumentos_delete.Recordset != null) {
		TiposDocumentos_delete.Recordset.Close();
		TiposDocumentos_delete.Recordset.Dispose();
	}
	TiposDocumentos_delete.Page_Terminate("TiposDocumentoslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= TiposDocumentos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= TiposDocumentos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% TiposDocumentos_delete.ShowPageHeader(); %>
<% TiposDocumentos_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="TiposDocumentos" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in TiposDocumentos_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= TiposDocumentos.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= TiposDocumentos.TipoDocumento.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
TiposDocumentos_delete.RecCnt = 0;
while (TiposDocumentos_delete.Recordset.Read()) {
	TiposDocumentos_delete.RecCnt++;

	// Set row properties
	TiposDocumentos.ResetAttrs();
	TiposDocumentos.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	TiposDocumentos_delete.LoadRowValues(ref TiposDocumentos_delete.Recordset);

	// Render row
	TiposDocumentos_delete.RenderRow();
%>
	<tr<%= TiposDocumentos.RowAttributes %>>
		<td<%= TiposDocumentos.TipoDocumento.CellAttributes %>>
<div<%= TiposDocumentos.TipoDocumento.ViewAttributes %>><%= TiposDocumentos.TipoDocumento.ListViewValue %></div>
</td>
	</tr>
<%
}
TiposDocumentos_delete.Recordset.Close();
TiposDocumentos_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
TiposDocumentos_delete.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
</asp:Content>
