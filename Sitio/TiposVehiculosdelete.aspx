<%@ Page ClassName="TiposVehiculosdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="TiposVehiculosdelete.aspx.cs" Inherits="TiposVehiculosdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var TiposVehiculos_delete = new ew_Page("TiposVehiculos_delete");

// page properties
TiposVehiculos_delete.PageID = "delete"; // page ID
TiposVehiculos_delete.FormID = "fTiposVehiculosdelete"; // form ID 
var EW_PAGE_ID = TiposVehiculos_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
TiposVehiculos_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
TiposVehiculos_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
TiposVehiculos_delete.ValidateRequired = false; // no JavaScript validation
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
TiposVehiculos_delete.Recordset = TiposVehiculos_delete.LoadRecordset();
if (TiposVehiculos_delete.TotalRecs <= 0) { // No record found, exit
	if (TiposVehiculos_delete.Recordset != null) {
		TiposVehiculos_delete.Recordset.Close();
		TiposVehiculos_delete.Recordset.Dispose();
	}
	TiposVehiculos_delete.Page_Terminate("TiposVehiculoslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= TiposVehiculos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= TiposVehiculos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% TiposVehiculos_delete.ShowPageHeader(); %>
<% TiposVehiculos_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="TiposVehiculos" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in TiposVehiculos_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= TiposVehiculos.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= TiposVehiculos.TipoVehiculo.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
TiposVehiculos_delete.RecCnt = 0;
while (TiposVehiculos_delete.Recordset.Read()) {
	TiposVehiculos_delete.RecCnt++;

	// Set row properties
	TiposVehiculos.ResetAttrs();
	TiposVehiculos.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	TiposVehiculos_delete.LoadRowValues(ref TiposVehiculos_delete.Recordset);

	// Render row
	TiposVehiculos_delete.RenderRow();
%>
	<tr<%= TiposVehiculos.RowAttributes %>>
		<td<%= TiposVehiculos.TipoVehiculo.CellAttributes %>>
<div<%= TiposVehiculos.TipoVehiculo.ViewAttributes %>><%= TiposVehiculos.TipoVehiculo.ListViewValue %></div>
</td>
	</tr>
<%
}
TiposVehiculos_delete.Recordset.Close();
TiposVehiculos_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
TiposVehiculos_delete.ShowPageFooter();
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
