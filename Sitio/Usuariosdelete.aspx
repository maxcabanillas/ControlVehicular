<%@ Page ClassName="Usuariosdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="Usuariosdelete.aspx.cs" Inherits="Usuariosdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var Usuarios_delete = new ew_Page("Usuarios_delete");

// page properties
Usuarios_delete.PageID = "delete"; // page ID
Usuarios_delete.FormID = "fUsuariosdelete"; // form ID 
var EW_PAGE_ID = Usuarios_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Usuarios_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Usuarios_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Usuarios_delete.ValidateRequired = false; // no JavaScript validation
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
Usuarios_delete.Recordset = Usuarios_delete.LoadRecordset();
if (Usuarios_delete.TotalRecs <= 0) { // No record found, exit
	if (Usuarios_delete.Recordset != null) {
		Usuarios_delete.Recordset.Close();
		Usuarios_delete.Recordset.Dispose();
	}
	Usuarios_delete.Page_Terminate("Usuarioslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Usuarios.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= Usuarios.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% Usuarios_delete.ShowPageHeader(); %>
<% Usuarios_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="Usuarios" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in Usuarios_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= Usuarios.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= Usuarios.Usuario.FldCaption %></td>
		<td valign="top"><%= Usuarios.NombreUsuario.FldCaption %></td>
		<td valign="top"><%= Usuarios.Password.FldCaption %></td>
		<td valign="top"><%= Usuarios.Correo.FldCaption %></td>
		<td valign="top"><%= Usuarios.IdUsuarioNivel.FldCaption %></td>
		<td valign="top"><%= Usuarios.Activo.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
Usuarios_delete.RecCnt = 0;
while (Usuarios_delete.Recordset.Read()) {
	Usuarios_delete.RecCnt++;

	// Set row properties
	Usuarios.ResetAttrs();
	Usuarios.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	Usuarios_delete.LoadRowValues(ref Usuarios_delete.Recordset);

	// Render row
	Usuarios_delete.RenderRow();
%>
	<tr<%= Usuarios.RowAttributes %>>
		<td<%= Usuarios.Usuario.CellAttributes %>>
<div<%= Usuarios.Usuario.ViewAttributes %>><%= Usuarios.Usuario.ListViewValue %></div>
</td>
		<td<%= Usuarios.NombreUsuario.CellAttributes %>>
<div<%= Usuarios.NombreUsuario.ViewAttributes %>><%= Usuarios.NombreUsuario.ListViewValue %></div>
</td>
		<td<%= Usuarios.Password.CellAttributes %>>
<div<%= Usuarios.Password.ViewAttributes %>><%= Usuarios.Password.ListViewValue %></div>
</td>
		<td<%= Usuarios.Correo.CellAttributes %>>
<div<%= Usuarios.Correo.ViewAttributes %>><%= Usuarios.Correo.ListViewValue %></div>
</td>
		<td<%= Usuarios.IdUsuarioNivel.CellAttributes %>>
<div<%= Usuarios.IdUsuarioNivel.ViewAttributes %>><%= Usuarios.IdUsuarioNivel.ListViewValue %></div>
</td>
		<td<%= Usuarios.Activo.CellAttributes %>>
<% if (Convert.ToString(Usuarios.Activo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Usuarios.Activo.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Usuarios.Activo.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<%
}
Usuarios_delete.Recordset.Close();
Usuarios_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
Usuarios_delete.ShowPageFooter();
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
