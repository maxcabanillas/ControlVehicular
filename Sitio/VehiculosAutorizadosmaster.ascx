<%@ Control ClassName="VehiculosAutorizadosmaster" Language="C#" AutoEventWireup="true" CodeFile="VehiculosAutorizadosmaster.ascx.cs" Inherits="VehiculosAutorizadosmaster" %>
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
	<tbody>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.IdTipoVehiculo.FldCaption %></td>
			<td<%= VehiculosAutorizados.IdTipoVehiculo.CellAttributes %>>
<div<%= VehiculosAutorizados.IdTipoVehiculo.ViewAttributes %>><%= VehiculosAutorizados.IdTipoVehiculo.ListViewValue %></div>
</td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Placa.FldCaption %></td>
			<td<%= VehiculosAutorizados.Placa.CellAttributes %>>
<div<%= VehiculosAutorizados.Placa.ViewAttributes %>><%= VehiculosAutorizados.Placa.ListViewValue %></div>
</td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Autorizado.FldCaption %></td>
			<td<%= VehiculosAutorizados.Autorizado.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Autorizado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.PicoyPlaca.FldCaption %></td>
			<td<%= VehiculosAutorizados.PicoyPlaca.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.PicoyPlaca.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Lunes.FldCaption %></td>
			<td<%= VehiculosAutorizados.Lunes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Lunes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Martes.FldCaption %></td>
			<td<%= VehiculosAutorizados.Martes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Martes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Miercoles.FldCaption %></td>
			<td<%= VehiculosAutorizados.Miercoles.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Miercoles.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Jueves.FldCaption %></td>
			<td<%= VehiculosAutorizados.Jueves.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Jueves.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Viernes.FldCaption %></td>
			<td<%= VehiculosAutorizados.Viernes.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Viernes.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Sabado.FldCaption %></td>
			<td<%= VehiculosAutorizados.Sabado.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Sabado.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Domingo.FldCaption %></td>
			<td<%= VehiculosAutorizados.Domingo.CellAttributes %>>
<% if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" checked onclick="this.form.reset();" disabled="disabled" />
<% } else { %>
<input type="checkbox" value="<%= VehiculosAutorizados.Domingo.ListViewValue %>" onclick="this.form.reset();" disabled="disabled" />
<% } %></td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= VehiculosAutorizados.Marca.FldCaption %></td>
			<td<%= VehiculosAutorizados.Marca.CellAttributes %>>
<div<%= VehiculosAutorizados.Marca.ViewAttributes %>><%= VehiculosAutorizados.Marca.ListViewValue %></div>
</td>
		</tr>
	</tbody>
</table>
</div>
</td></tr></table>
<br />
