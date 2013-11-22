<%@ Control ClassName="Personasmaster" Language="C#" AutoEventWireup="true" CodeFile="Personasmaster.ascx.cs" Inherits="Personasmaster" %>
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
	<tbody>
		<tr>
			<td class="ewTableHeader"><%= Personas.IdArea.FldCaption %></td>
			<td<%= Personas.IdArea.CellAttributes %>>
<div<%= Personas.IdArea.ViewAttributes %>><%= Personas.IdArea.ListViewValue %></div>
</td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= Personas.IdCargo.FldCaption %></td>
			<td<%= Personas.IdCargo.CellAttributes %>>
<div<%= Personas.IdCargo.ViewAttributes %>><%= Personas.IdCargo.ListViewValue %></div>
</td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= Personas.Documento.FldCaption %></td>
			<td<%= Personas.Documento.CellAttributes %>>
<div<%= Personas.Documento.ViewAttributes %>><%= Personas.Documento.ListViewValue %></div>
</td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= Personas.Persona.FldCaption %></td>
			<td<%= Personas.Persona.CellAttributes %>>
<div<%= Personas.Persona.ViewAttributes %>><%= Personas.Persona.ListViewValue %></div>
</td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= Personas.Activa.FldCaption %></td>
			<td<%= Personas.Activa.CellAttributes %>>
<% if (Convert.ToString(Personas.Activa.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= Personas.Activa.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
	</tbody>
</table>
</div>
</td></tr></table>
<br />
