using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

//
// ASP.NET code-behind class (Page)
//

partial class VehiculosPicoYPlacaHorasgrid: AspNetMaker9_ControlVehicular {

	//
	// ASP.NET Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {

		// Page init
		VehiculosPicoYPlacaHoras_grid = new cVehiculosPicoYPlacaHoras_grid(this);
		MasterPage = CurrentPage;

		//MasterPageType = CurrentPageType;
		CurrentPage = VehiculosPicoYPlacaHoras_grid;

		//CurrentPageType = VehiculosPicoYPlacaHoras_grid.GetType();
		VehiculosPicoYPlacaHoras_grid.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;

		// Page main processing
		VehiculosPicoYPlacaHoras_grid.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {

		// Dispose page object
		if (VehiculosPicoYPlacaHoras_grid != null)
			VehiculosPicoYPlacaHoras_grid.Dispose();
		CurrentPage = MasterPage;

		//CurrentPageType = MasterPageType;
	}
}
