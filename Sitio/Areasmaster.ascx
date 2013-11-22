<%@ Control ClassName="Areasmaster" Language="C#" AutoEventWireup="true" CodeFile="Areasmaster.ascx.cs" Inherits="Areasmaster" %>
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable ewTableSeparate">
	<tbody>
		<tr>
			<td class="ewTableHeader"><%= Areas.Area.FldCaption %></td>
			<td<%= Areas.Area.CellAttributes %>>
<div<%= Areas.Area.ViewAttributes %>><%= Areas.Area.ListViewValue %></div>
</td>
		</tr>
		<tr>
			<td class="ewTableHeader"><%= Areas.Codigo.FldCaption %></td>
			<td<%= Areas.Codigo.CellAttributes %>>
<div<%= Areas.Codigo.ViewAttributes %>><%= Areas.Codigo.ListViewValue %></div>
</td>
		</tr>
	</tbody>
</table>
</div>
</td></tr></table>
<br />
