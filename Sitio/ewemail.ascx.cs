//
// ASP.NET code-behind class (Email)
//
public partial class _ewemail: System.Web.UI.UserControl {

	public AspNetMaker9_ControlVehicular ParentPage;

	public AspNetMaker9_ControlVehicular.cLanguage Language;

	//
	// User control Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {
		if (Page is AspNetMaker9_ControlVehicular)
			ParentPage = (AspNetMaker9_ControlVehicular)Page;
		Language = ParentPage.Language;
	}
}
