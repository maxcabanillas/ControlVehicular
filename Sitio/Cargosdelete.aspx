<%@ Page ClassName="Cargosdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="Cargosdelete.aspx.cs" Inherits="Cargosdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var Cargos_delete = new ew_Page("Cargos_delete");

// page properties
Cargos_delete.PageID = "delete"; // page ID
Cargos_delete.FormID = "fCargosdelete"; // form ID 
var EW_PAGE_ID = Cargos_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Cargos_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Cargos_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Cargos_delete.ValidateRequired = false; // no JavaScript validation
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
Cargos_delete.Recordset = Cargos_delete.LoadRecordset();
if (Cargos_delete.TotalRecs <= 0) { // No record found, exit
	if (Cargos_delete.Recordset != null) {
		Cargos_delete.Recordset.Close();
		Cargos_delete.Recordset.Dispose();
	}
	Cargos_delete.Page_Terminate("Cargoslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Cargos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= Cargos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% Cargos_delete.ShowPageHeader(); %>
<% Cargos_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="Cargos" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in Cargos_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= Cargos.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= Cargos.Cargo.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
Cargos_delete.RecCnt = 0;
while (Cargos_delete.Recordset.Read()) {
	Cargos_delete.RecCnt++;

	// Set row properties
	Cargos.ResetAttrs();
	Cargos.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	Cargos_delete.LoadRowValues(ref Cargos_delete.Recordset);

	// Render row
	Cargos_delete.RenderRow();
%>
	<tr<%= Cargos.RowAttributes %>>
		<td<%= Cargos.Cargo.CellAttributes %>>
<div<%= Cargos.Cargo.ViewAttributes %>><%= Cargos.Cargo.ListViewValue %></div>
</td>
	</tr>
<%
}
Cargos_delete.Recordset.Close();
Cargos_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
Cargos_delete.ShowPageFooter();
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
