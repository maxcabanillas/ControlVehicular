<%@ Page ClassName="RegistrosPeatonesdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="RegistrosPeatonesdelete.aspx.cs" Inherits="RegistrosPeatonesdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var RegistrosPeatones_delete = new ew_Page("RegistrosPeatones_delete");

// page properties
RegistrosPeatones_delete.PageID = "delete"; // page ID
RegistrosPeatones_delete.FormID = "fRegistrosPeatonesdelete"; // form ID 
var EW_PAGE_ID = RegistrosPeatones_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
RegistrosPeatones_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosPeatones_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosPeatones_delete.ValidateRequired = false; // no JavaScript validation
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
RegistrosPeatones_delete.Recordset = RegistrosPeatones_delete.LoadRecordset();
if (RegistrosPeatones_delete.TotalRecs <= 0) { // No record found, exit
	if (RegistrosPeatones_delete.Recordset != null) {
		RegistrosPeatones_delete.Recordset.Close();
		RegistrosPeatones_delete.Recordset.Dispose();
	}
	RegistrosPeatones_delete.Page_Terminate("RegistrosPeatoneslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= RegistrosPeatones.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= RegistrosPeatones.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% RegistrosPeatones_delete.ShowPageHeader(); %>
<% RegistrosPeatones_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="RegistrosPeatones" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in RegistrosPeatones_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= RegistrosPeatones.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= RegistrosPeatones.IdTipoDocumento.FldCaption %></td>
		<td valign="top"><%= RegistrosPeatones.Documento.FldCaption %></td>
		<td valign="top"><%= RegistrosPeatones.Nombre.FldCaption %></td>
		<td valign="top"><%= RegistrosPeatones.Apellidos.FldCaption %></td>
		<td valign="top"><%= RegistrosPeatones.IdArea.FldCaption %></td>
		<td valign="top"><%= RegistrosPeatones.IdPersona.FldCaption %></td>
		<td valign="top"><%= RegistrosPeatones.FechaIngreso.FldCaption %></td>
		<td valign="top"><%= RegistrosPeatones.FechaSalida.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
RegistrosPeatones_delete.RecCnt = 0;
while (RegistrosPeatones_delete.Recordset.Read()) {
	RegistrosPeatones_delete.RecCnt++;

	// Set row properties
	RegistrosPeatones.ResetAttrs();
	RegistrosPeatones.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	RegistrosPeatones_delete.LoadRowValues(ref RegistrosPeatones_delete.Recordset);

	// Render row
	RegistrosPeatones_delete.RenderRow();
%>
	<tr<%= RegistrosPeatones.RowAttributes %>>
		<td<%= RegistrosPeatones.IdTipoDocumento.CellAttributes %>>
<div<%= RegistrosPeatones.IdTipoDocumento.ViewAttributes %>><%= RegistrosPeatones.IdTipoDocumento.ListViewValue %></div>
</td>
		<td<%= RegistrosPeatones.Documento.CellAttributes %>>
<div<%= RegistrosPeatones.Documento.ViewAttributes %>><%= RegistrosPeatones.Documento.ListViewValue %></div>
</td>
		<td<%= RegistrosPeatones.Nombre.CellAttributes %>>
<div<%= RegistrosPeatones.Nombre.ViewAttributes %>><%= RegistrosPeatones.Nombre.ListViewValue %></div>
</td>
		<td<%= RegistrosPeatones.Apellidos.CellAttributes %>>
<div<%= RegistrosPeatones.Apellidos.ViewAttributes %>><%= RegistrosPeatones.Apellidos.ListViewValue %></div>
</td>
		<td<%= RegistrosPeatones.IdArea.CellAttributes %>>
<div<%= RegistrosPeatones.IdArea.ViewAttributes %>><%= RegistrosPeatones.IdArea.ListViewValue %></div>
</td>
		<td<%= RegistrosPeatones.IdPersona.CellAttributes %>>
<div<%= RegistrosPeatones.IdPersona.ViewAttributes %>><%= RegistrosPeatones.IdPersona.ListViewValue %></div>
</td>
		<td<%= RegistrosPeatones.FechaIngreso.CellAttributes %>>
<div<%= RegistrosPeatones.FechaIngreso.ViewAttributes %>><%= RegistrosPeatones.FechaIngreso.ListViewValue %></div>
</td>
		<td<%= RegistrosPeatones.FechaSalida.CellAttributes %>>
<div<%= RegistrosPeatones.FechaSalida.ViewAttributes %>><%= RegistrosPeatones.FechaSalida.ListViewValue %></div>
</td>
	</tr>
<%
}
RegistrosPeatones_delete.Recordset.Close();
RegistrosPeatones_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
RegistrosPeatones_delete.ShowPageFooter();
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
