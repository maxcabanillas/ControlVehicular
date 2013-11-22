<%@ Page ClassName="VehiculosPicoYPlacaHoraspreview" Language="C#" CodePage="65001" AutoEventWireup="true" CodeFile="VehiculosPicoYPlacaHoraspreview.aspx.cs" Inherits="VehiculosPicoYPlacaHoraspreview" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<link href="aspxcss/ControlVehicular.css" rel="stylesheet" type="text/css" />
<% VehiculosPicoYPlacaHoras_preview.ShowPageHeader(); %>
<p class="aspnetmaker ewTitle" style="white-space: nowrap;"><%= Language.Phrase("TblTypeTABLE") %><%= VehiculosPicoYPlacaHoras.TableCaption %>&nbsp;
<% if (VehiculosPicoYPlacaHoras_preview.TotalRecs > 0) { %>
(<%= VehiculosPicoYPlacaHoras_preview.TotalRecs %> <%= Language.Phrase("Record") %>)
<% } else { %>
(<%= Language.Phrase("NoRecord") %>)
<% } %>
</p>
<%
if (VehiculosPicoYPlacaHoras_preview.TotalRecs > 0) {
%>
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table id="ewDetailsPreviewTable" name="ewDetailsPreviewTable" cellspacing="0" class="ewTable ewTableSeparate">
	<thead><!-- Table header -->
		<tr class="ewTableHeader">
<% if (VehiculosPicoYPlacaHoras.HoraFinal.Visible) { // HoraInicial %>
			<td valign="top"><%= VehiculosPicoYPlacaHoras.HoraInicial.FldCaption %></td>
<% } %>
<% if (VehiculosPicoYPlacaHoras.HoraFinal.Visible) { // HoraFinal %>
			<td valign="top"><%= VehiculosPicoYPlacaHoras.HoraFinal.FldCaption %></td>
<% } %>
		</tr>
	</thead>
	<tbody><!-- Table body -->
<%
	VehiculosPicoYPlacaHoras_preview.RowCount = 0;
	while (VehiculosPicoYPlacaHoras_preview.Recordset != null && VehiculosPicoYPlacaHoras_preview.Recordset.Read()) {

		// Init row class and style
		VehiculosPicoYPlacaHoras_preview.RowCount++;
		VehiculosPicoYPlacaHoras.CssClass = "ewTableRow";
		VehiculosPicoYPlacaHoras.CssStyle = "";
		VehiculosPicoYPlacaHoras.LoadListRowValues(ref VehiculosPicoYPlacaHoras_preview.Recordset);

		// Render row
		VehiculosPicoYPlacaHoras.RowType = EW_ROWTYPE_PREVIEW; // Preview record
		VehiculosPicoYPlacaHoras.RenderListRow();
%>
	<tr<%= VehiculosPicoYPlacaHoras.RowAttributes %>>
<% if (VehiculosPicoYPlacaHoras.HoraInicial.Visible) { // HoraInicial %>
		<td>
<div<%= VehiculosPicoYPlacaHoras.HoraInicial.ViewAttributes %>><%= VehiculosPicoYPlacaHoras.HoraInicial.ListViewValue %></div>
</td>
<% } %>
<% if (VehiculosPicoYPlacaHoras.HoraFinal.Visible) { // HoraFinal %>
		<td>
<div<%= VehiculosPicoYPlacaHoras.HoraFinal.ViewAttributes %>><%= VehiculosPicoYPlacaHoras.HoraFinal.ListViewValue %></div>
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
VehiculosPicoYPlacaHoras_preview.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<%
}
%>
