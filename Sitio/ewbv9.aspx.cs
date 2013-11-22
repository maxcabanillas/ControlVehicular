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
// ASP.NET code-behind class (Blob View) 
//

partial class ewbv9: AspNetMaker9_ControlVehicular {

	//
	// ASP.NET Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e)
	{
		Response.Cache.SetCacheability(HttpCacheability.NoCache);
		string tbl = "";
		string fld = "";
		string ft;
		string fn;
		long fs;
		int width = 0;
		int height = 0;
		int interpolation;
		int idx;
		bool restoreDb;
		bool restoreDbFile;

		// Get resize parameters
		bool resize = (ew_NotEmpty(ew_Get("resize")));		
		if (ew_NotEmpty(ew_Get("width")))
			width = ew_ConvertToInt(ew_Get("width"));
		if (ew_NotEmpty(ew_Get("height")))
			height = ew_ConvertToInt(ew_Get("height"));
		if (width <= 0 && height <= 0) {
			width = EW_THUMBNAIL_DEFAULT_WIDTH;
			height = EW_THUMBNAIL_DEFAULT_HEIGHT;
		}
		if (ew_NotEmpty(ew_Get("interpolation"))) {
			interpolation = ew_ConvertToInt(ew_Get("interpolation"));
		}	else {
			interpolation = EW_THUMBNAIL_DEFAULT_INTERPOLATION;
		}

		// Resize image from physical file
		if (ew_NotEmpty(ew_Get("fn"))) {
			fn = ew_Get("fn");
			fn = Server.MapPath(fn);
			if (File.Exists(fn)) {
				string ext = Path.GetExtension(fn).Replace(".", "").ToLower();
				if (Array.IndexOf(EW_IMAGE_ALLOWED_FILE_EXT.Split(new char[] {','}), ext) > 0) {
					Response.ContentType = ew_GetImageContentType(fn);
					Response.BinaryWrite(ew_ResizeFileToBinary(fn, ref width, ref height, interpolation));
				}
			}
		}	else { // Display image from Session
			if (ew_Empty(ew_Get("tbl")) || ew_Empty(ew_Get("fld"))) Response.End();
			tbl = ew_Get("tbl");
			fld = ew_Get("fld");
			idx = ew_ConvertToInt(ew_Get("idx"));
			restoreDb = ew_NotEmpty(ew_Get("db"));
			restoreDbFile = ew_NotEmpty(ew_Get("file"));

			// Get blob field
			cUpload obj = new cUpload(tbl, fld);
			obj.Index = idx;
			if (restoreDb) {
				obj.RestoreDbFromSession();
				obj.Value = obj.DbValue;
			} else {
				obj.RestoreFromSession();
			}			
			object b = obj.Value;
			if (Convert.IsDBNull(b) || b == null)
				Response.End();				

			// Restore db file
			if (restoreDbFile) {					
				fn = ew_UploadPathEx(true, ew_Get("path")) + obj.Value; //***
				if (File.Exists(fn)) {
					string ext = Path.GetExtension(fn).Replace(".", "").ToLower();
					if (Array.IndexOf(EW_IMAGE_ALLOWED_FILE_EXT.Split(new char[] {','}), ext) > 0) {
						Response.ContentType = ew_GetImageContentType(fn);						
						Response.BinaryWrite(ew_ResizeFileToBinary(fn, ref width, ref height, interpolation));						
					}
				}		
			} else {		
				ft = obj.ContentType;
				fn = obj.FileName;
				Response.ContentType = (ew_NotEmpty(ft)) ? ft : "image/bmp";
				if (resize)
					obj.Resize(width, height, interpolation); 
				Response.BinaryWrite((byte[])obj.Value);						
			}
		}
		Response.End();
	}
}
