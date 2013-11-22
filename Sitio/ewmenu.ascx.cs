//
// ASP.NET code-behind class (Menu)
//

partial class ewmenu: System.Web.UI.UserControl {

	public AspNetMaker9_ControlVehicular ParentPage;

	public AspNetMaker9_ControlVehicular.cMenu RootMenu;

	//
	// User control Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {
		if (Page is AspNetMaker9_ControlVehicular) {
			ParentPage = (AspNetMaker9_ControlVehicular)Page;			
			RootMenu = new AspNetMaker9_ControlVehicular.cMenu("RootMenu", true);
			RootMenu.ParentPage = ParentPage;
			if (ParentPage.Language == null)
				ParentPage.Language = new AspNetMaker9_ControlVehicular.cLanguage(new AspNetMaker9_ControlVehicular.AspNetMakerPage());
			AspNetMaker9_ControlVehicular.cLanguage Language = ParentPage.Language;

			// Generate menu items
			RootMenu.AddMenuItem(19, Language.MenuPhrase("19", "MenuText"), "z_RegistroVehiculolist.aspx", -1, "", true, false); 
			RootMenu.AddMenuItem(20, Language.MenuPhrase("20", "MenuText"), "z_RegistroPeatoneslist.aspx", -1, "", true, false); 
			RootMenu.AddMenuItem(15, Language.MenuPhrase("15", "MenuText"), "", -1, "", (AspNetMaker9_ControlVehicular.IsLoggedIn()), false); 
			RootMenu.AddMenuItem(17, Language.MenuPhrase("17", "MenuText"), "HistoricoVehiculoslist.aspx", 15, "", (ParentPage.AllowList("HistoricoVehiculos")), false); 
			RootMenu.AddMenuItem(18, Language.MenuPhrase("18", "MenuText"), "HistoricoPeatoneslist.aspx", 15, "", (ParentPage.AllowList("HistoricoPeatones")), false); 
			RootMenu.AddMenuItem(14, Language.MenuPhrase("14", "MenuText"), "", -1, "", (AspNetMaker9_ControlVehicular.IsLoggedIn()), false); 
			RootMenu.AddMenuItem(1, Language.MenuPhrase("1", "MenuText"), "Areaslist.aspx", 14, "", (ParentPage.AllowList("Areas")), false); 
			RootMenu.AddMenuItem(21, Language.MenuPhrase("21", "MenuText"), "Cargoslist.aspx", 14, "", (ParentPage.AllowList("Cargos")), false); 
			RootMenu.AddMenuItem(3, Language.MenuPhrase("3", "MenuText"), "Personaslist.aspx?cmd=resetall", 14, "", (ParentPage.AllowList("Personas")), false); 
			RootMenu.AddMenuItem(2, Language.MenuPhrase("2", "MenuText"), "TiposVehiculoslist.aspx", 14, "", (ParentPage.AllowList("TiposVehiculos")), false); 
			RootMenu.AddMenuItem(10, Language.MenuPhrase("10", "MenuText"), "TiposDocumentoslist.aspx", 14, "", (ParentPage.AllowList("TiposDocumentos")), false); 
			RootMenu.AddMenuItem(9, Language.MenuPhrase("9", "MenuText"), "UserLevelslist.aspx", 14, "", (ParentPage.IsAdmin()), false); 
			RootMenu.AddMenuItem(4, Language.MenuPhrase("4", "MenuText"), "Usuarioslist.aspx", 14, "", (ParentPage.AllowList("Usuarios")), false); 
			RootMenu.AddMenuItem(-1, Language.Phrase("Logout"), "logout.aspx", -1, "", AspNetMaker9_ControlVehicular.IsLoggedIn(), false); 
			RootMenu.AddMenuItem(-1, Language.Phrase("Login"), "login.aspx", -1, "", !AspNetMaker9_ControlVehicular.IsLoggedIn() && !Request.ServerVariables["URL"].EndsWith("/login.aspx"), false); 
		}
	}
}
