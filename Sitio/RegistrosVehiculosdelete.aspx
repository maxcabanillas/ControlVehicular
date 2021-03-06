<%@ Page ClassName="RegistrosVehiculosdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="RegistrosVehiculosdelete.aspx.cs" Inherits="RegistrosVehiculosdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var RegistrosVehiculos_delete = new ew_Page("RegistrosVehiculos_delete");

// page properties
RegistrosVehiculos_delete.PageID = "delete"; // page ID
RegistrosVehiculos_delete.FormID = "fRegistrosVehiculosdelete"; // form ID 
var EW_PAGE_ID = RegistrosVehiculos_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
RegistrosVehiculos_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosVehiculos_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosVehiculos_delete.ValidateRequired = false; // no JavaScript validation
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
RegistrosVehiculos_delete.Recordset = RegistrosVehiculos_delete.LoadRecordset();
if (RegistrosVehiculos_delete.TotalRecs <= 0) { // No record found, exit
	if (RegistrosVehiculos_delete.Recordset != null) {
		RegistrosVehiculos_delete.Recordset.Close();
		RegistrosVehiculos_delete.Recordset.Dispose();
	}
	RegistrosVehiculos_delete.Page_Terminate("RegistrosVehiculoslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= RegistrosVehiculos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= RegistrosVehiculos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% RegistrosVehiculos_delete.ShowPageHeader(); %>
<% RegistrosVehiculos_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="RegistrosVehiculos" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in RegistrosVehiculos_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= RegistrosVehiculos.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= RegistrosVehiculos.IdTipoVehiculo.FldCaption %></td>
		<td valign="top"><%= RegistrosVehiculos.Placa.FldCaption %></td>
		<td valign="top"><%= RegistrosVehiculos.FechaIngreso.FldCaption %></td>
		<td valign="top"><%= RegistrosVehiculos.FechaSalida.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
RegistrosVehiculos_delete.RecCnt = 0;
while (RegistrosVehiculos_delete.Recordset.Read()) {
	RegistrosVehiculos_delete.RecCnt++;

	// Set row properties
	RegistrosVehiculos.ResetAttrs();
	RegistrosVehiculos.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	RegistrosVehiculos_delete.LoadRowValues(ref RegistrosVehiculos_delete.Recordset);

	// Render row
	RegistrosVehiculos_delete.RenderRow();
%>
	<tr<%= RegistrosVehiculos.RowAttributes %>>
		<td<%= RegistrosVehiculos.IdTipoVehiculo.CellAttributes %>>
<div<%= RegistrosVehiculos.IdTipoVehiculo.ViewAttributes %>><%= RegistrosVehiculos.IdTipoVehiculo.ListViewValue %></div>
</td>
		<td<%= RegistrosVehiculos.Placa.CellAttributes %>>
<div<%= RegistrosVehiculos.Placa.ViewAttributes %>><%= RegistrosVehiculos.Placa.ListViewValue %></div>
</td>
		<td<%= RegistrosVehiculos.FechaIngreso.CellAttributes %>>
<div<%= RegistrosVehiculos.FechaIngreso.ViewAttributes %>><%= RegistrosVehiculos.FechaIngreso.ListViewValue %></div>
</td>
		<td<%= RegistrosVehiculos.FechaSalida.CellAttributes %>>
<div<%= RegistrosVehiculos.FechaSalida.ViewAttributes %>><%= RegistrosVehiculos.FechaSalida.ListViewValue %></div>
</td>
	</tr>
<%
}
RegistrosVehiculos_delete.Recordset.Close();
RegistrosVehiculos_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
RegistrosVehiculos_delete.ShowPageFooter();
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
