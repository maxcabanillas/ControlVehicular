<%@ Page ClassName="Personaspreview" Language="C#" CodePage="65001" AutoEventWireup="true" CodeFile="Personaspreview.aspx.cs" Inherits="Personaspreview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<link href="aspxcss/ControlVehicular.css" rel="stylesheet" type="text/css" />
<% Personas_preview.ShowPageHeader(); %>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= Personas.TableCaption %>&nbsp;
<% if (Personas_preview.TotalRecs > 0) { %>
(<%= Personas_preview.TotalRecs %> <%= Language.Phrase("Record") %>)
<% } else { %>
(<%= Language.Phrase("NoRecord") %>)
<% } %>
</p>
<%
if (Personas_preview.TotalRecs > 0) {
%>
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table id="ewDetailsPreviewTable" name="ewDetailsPreviewTable" cellspacing="0" class="ewTable ewTableSeparate">
	<thead><!-- Table header -->
		<tr class="ewTableHeader">
<% if (Personas.Activa.Visible) { // IdArea %>
			<td valign="top"><%= Personas.IdArea.FldCaption %></td>
<% } %>
<% if (Personas.Activa.Visible) { // IdCargo %>
			<td valign="top"><%= Personas.IdCargo.FldCaption %></td>
<% } %>
<% if (Personas.Activa.Visible) { // Documento %>
			<td valign="top"><%= Personas.Documento.FldCaption %></td>
<% } %>
<% if (Personas.Activa.Visible) { // Persona %>
			<td valign="top"><%= Personas.Persona.FldCaption %></td>
<% } %>
<% if (Personas.Activa.Visible) { // Activa %>
			<td valign="top"><%= Personas.Activa.FldCaption %></td>
<% } %>
		</tr>
	</thead>
	<tbody><!-- Table body -->
<%
	Personas_preview.RowCount = 0;
	while (Personas_preview.Recordset != null && Personas_preview.Recordset.Read()) {

		// Init row class and style
		Personas_preview.RowCount++;
		Personas.CssClass = "ewTableRow";
		Personas.CssStyle = "";
		Personas.LoadListRowValues(ref Personas_preview.Recordset);

		// Render row
		Personas.RowType = EW_ROWTYPE_PREVIEW; // Preview record
		Personas.RenderListRow();
%>
	<tr<%= Personas.RowAttributes %>>
<% if (Personas.IdArea.Visible) { // IdArea %>
		<td>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ListViewValue %></div>
</td>
<% } %>
<% if (Personas.IdCargo.Visible) { // IdCargo %>
		<td>
<div<%= Personas.IdCargo.ViewAttributes %>><%= Personas.IdCargo.ListViewValue %></div>
</td>
<% } %>
<% if (Personas.Documento.Visible) { // Documento %>
		<td>
<div<%= Personas.Documento.ViewAttributes %>><%= Personas.Documento.ListViewValue %></div>
</td>
<% } %>
<% if (Personas.Persona.Visible) { // Persona %>
		<td>
<div<%= Personas.Persona.ViewAttributes %>><%= Personas.Persona.ListViewValue %></div>
</td>
<% } %>
<% if (Personas.Activa.Visible) { // Activa %>
		<td>
<% if (Convert.ToString(Personas.Activa.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
	</tr>
<%
	}
%>
	</tbody>
</table>
</div>
</td></tr></table>
<%
Personas_preview.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<%
}
%>
