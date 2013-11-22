<%@ Page ClassName="VehiculosAutorizadosdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="VehiculosAutorizadosdelete.aspx.cs" Inherits="VehiculosAutorizadosdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var VehiculosAutorizados_delete = new ew_Page("VehiculosAutorizados_delete");

// page properties
VehiculosAutorizados_delete.PageID = "delete"; // page ID
VehiculosAutorizados_delete.FormID = "fVehiculosAutorizadosdelete"; // form ID 
var EW_PAGE_ID = VehiculosAutorizados_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
VehiculosAutorizados_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
VehiculosAutorizados_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
VehiculosAutorizados_delete.ValidateRequired = false; // no JavaScript validation
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
VehiculosAutorizados_delete.Recordset = VehiculosAutorizados_delete.LoadRecordset();
if (VehiculosAutorizados_delete.TotalRecs <= 0) { // No record found, exit
	if (VehiculosAutorizados_delete.Recordset != null) {
		VehiculosAutorizados_delete.Recordset.Close();
		VehiculosAutorizados_delete.Recordset.Dispose();
	}
	VehiculosAutorizados_delete.Page_Terminate("VehiculosAutorizadoslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= VehiculosAutorizados.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= VehiculosAutorizados.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% VehiculosAutorizados_delete.ShowPageHeader(); %>
<% VehiculosAutorizados_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="VehiculosAutorizados" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in VehiculosAutorizados_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= VehiculosAutorizados.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= VehiculosAutorizados.IdTipoVehiculo.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Placa.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Autorizado.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.PicoyPlaca.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Lunes.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Martes.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Miercoles.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Jueves.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Viernes.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Sabado.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Domingo.FldCaption %></td>
		<td valign="top"><%= VehiculosAutorizados.Marca.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
VehiculosAutorizados_delete.RecCnt = 0;
while (VehiculosAutorizados_delete.Recordset.Read()) {
	VehiculosAutorizados_delete.RecCnt++;

	// Set row properties
	VehiculosAutorizados.ResetAttrs();
	VehiculosAutorizados.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	VehiculosAutorizados_delete.LoadRowValues(ref VehiculosAutorizados_delete.Recordset);

	// Render row
	VehiculosAutorizados_delete.RenderRow();
%>
	<tr<%= VehiculosAutorizados.RowAttributes %>>
		<td<%= VehiculosAutorizados.IdTipoVehiculo.CellAttributes %>>
<div<%= VehiculosAutorizados.IdTipoVehiculo.ViewAttributes %>><%= VehiculosAutorizados.IdTipoVehiculo.ListViewValue %></div>
</td>
		<td<%= VehiculosAutorizados.Placa.CellAttributes %>>
<div<%= VehiculosAutorizados.Placa.ViewAttributes %>><%= VehiculosAutorizados.Placa.ListViewValue %></div>
</td>
		<td<%= VehiculosAutorizados.Autorizado.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		<td<%= VehiculosAutorizados.PicoyPlaca.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		<td<%= VehiculosAutorizados.Lunes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		<td<%= VehiculosAutorizados.Martes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		<td<%= VehiculosAutorizados.Miercoles.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		<td<%= VehiculosAutorizados.Jueves.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		<td<%= VehiculosAutorizados.Viernes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		<td<%= VehiculosAutorizados.Sabado.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		<td<%= VehiculosAutorizados.Domingo.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		<td<%= VehiculosAutorizados.Marca.CellAttributes %>>
<div<%= VehiculosAutorizados.Marca.ViewAttributes %>><%= VehiculosAutorizados.Marca.ListViewValue %></div>
</td>
	</tr>
<%
}
VehiculosAutorizados_delete.Recordset.Close();
VehiculosAutorizados_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
VehiculosAutorizados_delete.ShowPageFooter();
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
