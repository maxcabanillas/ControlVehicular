//
// ASP.NET code-behind class (Master Record) 
//

partial class VehiculosAutorizadosmaster: System.Web.UI.UserControl {

	public AspNetMaker9_ControlVehicular ParentPage;

	public AspNetMaker9_ControlVehicular.cVehiculosAutorizados VehiculosAutorizados;	

	//
	// ASP.NET user control Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {
		if (Page is AspNetMaker9_ControlVehicular) {
			ParentPage = (AspNetMaker9_ControlVehicular)Page;			
			VehiculosAutorizados = ParentPage.VehiculosAutorizados;
		}
	}
}
