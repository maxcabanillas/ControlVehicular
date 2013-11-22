<%@ Page ClassName="Areasdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="Areasdelete.aspx.cs" Inherits="Areasdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var Areas_delete = new ew_Page("Areas_delete");

// page properties
Areas_delete.PageID = "delete"; // page ID
Areas_delete.FormID = "fAreasdelete"; // form ID 
var EW_PAGE_ID = Areas_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Areas_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Areas_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Areas_delete.ValidateRequired = false; // no JavaScript validation
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
Areas_delete.Recordset = Areas_delete.LoadRecordset();
if (Areas_delete.TotalRecs <= 0) { // No record found, exit
	if (Areas_delete.Recordset != null) {
		Areas_delete.Recordset.Close();
		Areas_delete.Recordset.Dispose();
	}
	Areas_delete.Page_Terminate("Areaslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Areas.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= Areas.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% Areas_delete.ShowPageHeader(); %>
<% Areas_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="Areas" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in Areas_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= Areas.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= Areas.Area.FldCaption %></td>
		<td valign="top"><%= Areas.Codigo.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
Areas_delete.RecCnt = 0;
while (Areas_delete.Recordset.Read()) {
	Areas_delete.RecCnt++;

	// Set row properties
	Areas.ResetAttrs();
	Areas.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	Areas_delete.LoadRowValues(ref Areas_delete.Recordset);

	// Render row
	Areas_delete.RenderRow();
%>
	<tr<%= Areas.RowAttributes %>>
		<td<%= Areas.Area.CellAttributes %>>
<div<%= Areas.Area.ViewAttributes %>><%= Areas.Area.ListViewValue %></div>
</td>
		<td<%= Areas.Codigo.CellAttributes %>>
<div<%= Areas.Codigo.ViewAttributes %>><%= Areas.Codigo.ListViewValue %></div>
</td>
	</tr>
<%
}
Areas_delete.Recordset.Close();
Areas_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
Areas_delete.ShowPageFooter();
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
