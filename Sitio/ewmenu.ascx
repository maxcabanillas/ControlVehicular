<%@ Control ClassName="ewmenu" Language="C#" AutoEventWireup="true" CodeFile="ewmenu.ascx.cs" Inherits="ewmenu" %>
<!-- Begin Main Menu -->
<div class="aspnetmaker">
<%
ParentPage.Menu_Rendering(ref RootMenu);
RootMenu.Render();
%>
</div>
<!-- End Main Menu -->
<script type="text/javascript">
<!--

// init the menu
var RootMenu = new YAHOO.widget.MenuBar("RootMenu", { autosubmenudisplay: true, hidedelay: 750, lazyload: true });
RootMenu.render();        

//-->
</script>
