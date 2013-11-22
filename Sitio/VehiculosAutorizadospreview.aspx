<%@ Page ClassName="VehiculosAutorizadospreview" Language="C#" CodePage="65001" AutoEventWireup="true" CodeFile="VehiculosAutorizadospreview.aspx.cs" Inherits="VehiculosAutorizadospreview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<link href="aspxcss/ControlVehicular.css" rel="stylesheet" type="text/css" />
<% VehiculosAutorizados_preview.ShowPageHeader(); %>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= VehiculosAutorizados.TableCaption %>&nbsp;
<% if (VehiculosAutorizados_preview.TotalRecs > 0) { %>
(<%= VehiculosAutorizados_preview.TotalRecs %> <%= Language.Phrase("Record") %>)
<% } else { %>
(<%= Language.Phrase("NoRecord") %>)
<% } %>
</p>
<%
if (VehiculosAutorizados_preview.TotalRecs > 0) {
%>
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table id="ewDetailsPreviewTable" name="ewDetailsPreviewTable" cellspacing="0" class="ewTable ewTableSeparate">
	<thead><!-- Table header -->
		<tr class="ewTableHeader">
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // IdTipoVehiculo %>
			<td valign="top"><%= VehiculosAutorizados.IdTipoVehiculo.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Placa %>
			<td valign="top"><%= VehiculosAutorizados.Placa.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Autorizado %>
			<td valign="top"><%= VehiculosAutorizados.Autorizado.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // PicoyPlaca %>
			<td valign="top"><%= VehiculosAutorizados.PicoyPlaca.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Lunes %>
			<td valign="top"><%= VehiculosAutorizados.Lunes.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Martes %>
			<td valign="top"><%= VehiculosAutorizados.Martes.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Miercoles %>
			<td valign="top"><%= VehiculosAutorizados.Miercoles.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Jueves %>
			<td valign="top"><%= VehiculosAutorizados.Jueves.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Viernes %>
			<td valign="top"><%= VehiculosAutorizados.Viernes.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Sabado %>
			<td valign="top"><%= VehiculosAutorizados.Sabado.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Domingo %>
			<td valign="top"><%= VehiculosAutorizados.Domingo.FldCaption %></td>
<% } %>
<% if (VehiculosAutorizados.IdVehiculoAutorizado.Visible) { // Marca %>
			<td valign="top"><%= VehiculosAutorizados.Marca.FldCaption %></td>
<% } %>
		</tr>
	</thead>
	<tbody><!-- Table body -->
<%
	VehiculosAutorizados_preview.RowCount = 0;
	while (VehiculosAutorizados_preview.Recordset != null && VehiculosAutorizados_preview.Recordset.Read()) {

		// Init row class and style
		VehiculosAutorizados_preview.RowCount++;
		VehiculosAutorizados.CssClass = "ewTableRow";
		VehiculosAutorizados.CssStyle = "";
		VehiculosAutorizados.LoadListRowValues(ref VehiculosAutorizados_preview.Recordset);

		// Render row
		VehiculosAutorizados.RowType = EW_ROWTYPE_PREVIEW; // Preview record
		VehiculosAutorizados.RenderListRow();
%>
	<tr<%= VehiculosAutorizados.RowAttributes %>>
<% if (VehiculosAutorizados.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
		<td>
<div<%= VehiculosAutorizados.IdTipoVehiculo.ViewAttributes %>><%= VehiculosAutorizados.IdTipoVehiculo.ListViewValue %></div>
</td>
<% } %>
<% if (VehiculosAutorizados.Placa.Visible) { // Placa %>
		<td>
<div<%= VehiculosAutorizados.Placa.ViewAttributes %>><%= VehiculosAutorizados.Placa.ListViewValue %></div>
</td>
<% } %>
<% if (VehiculosAutorizados.Autorizado.Visible) { // Autorizado %>
		<td>
<% if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
<% if (VehiculosAutorizados.PicoyPlaca.Visible) { // PicoyPlaca %>
		<td>
<% if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
<% if (VehiculosAutorizados.Lunes.Visible) { // Lunes %>
		<td>
<% if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
<% if (VehiculosAutorizados.Martes.Visible) { // Martes %>
		<td>
<% if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
<% if (VehiculosAutorizados.Miercoles.Visible) { // Miercoles %>
		<td>
<% if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
<% if (VehiculosAutorizados.Jueves.Visible) { // Jueves %>
		<td>
<% if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
<% if (VehiculosAutorizados.Viernes.Visible) { // Viernes %>
		<td>
<% if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
<% if (VehiculosAutorizados.Sabado.Visible) { // Sabado %>
		<td>
<% if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
<% if (VehiculosAutorizados.Domingo.Visible) { // Domingo %>
		<td>
<% if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
<% } %>
<% if (VehiculosAutorizados.Marca.Visible) { // Marca %>
		<td>
<div<%= VehiculosAutorizados.Marca.ViewAttributes %>><%= VehiculosAutorizados.Marca.ListViewValue %></div>
</td>
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
VehiculosAutorizados_preview.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<%
}
%>
