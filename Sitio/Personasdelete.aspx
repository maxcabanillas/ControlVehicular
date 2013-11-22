<%@ Page ClassName="Personasdelete" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="Personasdelete.aspx.cs" Inherits="Personasdelete" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var Personas_delete = new ew_Page("Personas_delete");

// page properties
Personas_delete.PageID = "delete"; // page ID
Personas_delete.FormID = "fPersonasdelete"; // form ID 
var EW_PAGE_ID = Personas_delete.PageID; // for backward compatibility

// extend page with Form_CustomValidate function
Personas_delete.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
Personas_delete.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
Personas_delete.ValidateRequired = false; // no JavaScript validation
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
Personas_delete.Recordset = Personas_delete.LoadRecordset();
if (Personas_delete.TotalRecs <= 0) { // No record found, exit
	if (Personas_delete.Recordset != null) {
		Personas_delete.Recordset.Close();
		Personas_delete.Recordset.Dispose();
	}
	Personas_delete.Page_Terminate("Personaslist.aspx"); // Return to list
}
%>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Delete") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= Personas.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= Personas.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% Personas_delete.ShowPageHeader(); %>
<% Personas_delete.ShowMessage(); %>
<form method="post">
<p />
<input type="hidden" name="t" id="t" value="Personas" />
<input type="hidden" name="a_delete" id="a_delete" value="D" />
<% foreach (object key in Personas_delete.RecKeys) { %>
<% string keyvalue = Information.IsArray(key) ? String.Join(EW_COMPOSITE_KEY_SEPARATOR, (string[])key) : Convert.ToString(key); %>
<input type="hidden" name="key_m" id="key_m" value="<%= ew_HtmlEncode(keyvalue) %>" />
<% } %>
<table class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
<%= Personas.TableCustomInnerHtml %>
	<thead>
	<tr class="ewTableHeader">
		<td valign="top"><%= Personas.IdArea.FldCaption %></td>
		<td valign="top"><%= Personas.IdCargo.FldCaption %></td>
		<td valign="top"><%= Personas.Documento.FldCaption %></td>
		<td valign="top"><%= Personas.Persona.FldCaption %></td>
		<td valign="top"><%= Personas.Activa.FldCaption %></td>
	</tr>
	</thead>
	<tbody>
<%
Personas_delete.RecCnt = 0;
while (Personas_delete.Recordset.Read()) {
	Personas_delete.RecCnt++;

	// Set row properties
	Personas.ResetAttrs();
	Personas.RowType = EW_ROWTYPE_VIEW; // view

	// Get the field contents
	Personas_delete.LoadRowValues(ref Personas_delete.Recordset);

	// Render row
	Personas_delete.RenderRow();
%>
	<tr<%= Personas.RowAttributes %>>
		<td<%= Personas.IdArea.CellAttributes %>>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ListViewValue %></div>
</td>
		<td<%= Personas.IdCargo.CellAttributes %>>
<div<%= Personas.IdCargo.ViewAttributes %>><%= Personas.IdCargo.ListViewValue %></div>
</td>
		<td<%= Personas.Documento.CellAttributes %>>
<div<%= Personas.Documento.ViewAttributes %>><%= Personas.Documento.ListViewValue %></div>
</td>
		<td<%= Personas.Persona.CellAttributes %>>
<div<%= Personas.Persona.ViewAttributes %>><%= Personas.Persona.ListViewValue %></div>
</td>
		<td<%= Personas.Activa.CellAttributes %>>
<% if (Convert.ToString(Personas.Activa.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
	</tr>
<%
}
Personas_delete.Recordset.Close();
Personas_delete.Recordset.Dispose();
%>
	</tbody>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="Action" id="Action" value="<%= ew_BtnCaption(Language.Phrase("DeleteBtn")) %>" />
</form>
<%
Personas_delete.ShowPageFooter();
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
