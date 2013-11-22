<%@ Page ClassName="_login" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_login" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<script type="text/javascript">
<!--
var login = new ew_Page("login");

// extend page with ValidateForm function
login.ValidateForm = function(fobj)
{
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (!ew_HasValue(fobj.username))
		return ew_OnError(this, fobj.username, ewLanguage.Phrase("EnterUid")); 
	if (!ew_HasValue(fobj.password))
		return ew_OnError(this, fobj.password, ewLanguage.Phrase("EnterPwd")); 

	// Form Custom Validate event
	if (!this.Form_CustomValidate(fobj)) return false;
	return true;
}

// extend page with Form_CustomValidate function
login.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }

// requires js validation
<% if (EW_CLIENT_VALIDATE) { %>
login.ValidateRequired = true;
<% } else { %>
login.ValidateRequired = false;
<% } %>

//-->
</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("LoginPage") %></p>
<% login.ShowPageHeader(); %>
<% login.ShowMessage(); %>
<form method="post" onsubmit="return login.ValidateForm(this);">
<table border="0" cellspacing="0" cellpadding="4">
	<tr>
		<td><span class="aspnetmaker"><%= Language.Phrase("Username") %></span></td>
		<td><span class="aspnetmaker"><input type="text" name="username" id="username" size="20" value="<%= login.sUsername %>" /></span></td>
	</tr>
	<tr>
		<td><span class="aspnetmaker"><%= Language.Phrase("Password") %></span></td>
		<td><span class="aspnetmaker"><input type="password" name="password" id="password" size="20" /></span></td>
	</tr>
	<tr>
		<td colspan="2" align="center"><span class="aspnetmaker"><input type="submit" name="submit" id="submit" value="<%= ew_BtnCaption(Language.Phrase("Login")) %>" /></span></td>
	</tr>
</table>
		<input type="hidden" name="rememberme" id="rememberme" value="" />
</form>
<br />
<p class="aspnetmaker">
</p>
<%
login.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<script language="JavaScript" type="text/javascript">
<!--

// Write your startup script here
// document.write("page loaded");
$(function(){
		$('form').wrap('<div id="FondoLogin"><div id="Login">'+
					   '</div></div>');
		$('#Login').append('<div id="LogoSofia"><img src="aspximages/parking.png" /></div>');
	});                                                                       

//-->

</script>
</asp:Content>
