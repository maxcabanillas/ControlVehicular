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
// ASP.NET Maker 9 Project Class
// (C)2011 e.World Technology Limited. All rights reserved.
//
public partial class AspNetMaker9_ControlVehicular: System.Web.UI.Page {

	// Compare object as string
	public static bool ew_SameStr(object v1, object v2)
	{
		return string.Equals(Convert.ToString(v1).Trim(), Convert.ToString(v2).Trim());
	}

	// Compare object as string (case insensitive)
	public static bool ew_SameText(object v1, object v2)
	{
		return string.Equals(Convert.ToString(v1).Trim().ToLowerInvariant(), Convert.ToString(v2).Trim().ToLowerInvariant());
	}

	// Check if empty string
	public static bool ew_Empty(object value)
	{
		return string.Equals(Convert.ToString(value).Trim(), string.Empty);
	}

	// Check if not empty string
	public static bool ew_NotEmpty(object value)
	{
		return !ew_Empty(value);
	}

	// Convert object to integer
	public static int ew_ConvertToInt(object value)
	{
		try {
			return Convert.ToInt32(value);
		}	catch {
			return 0;
		}
	}

	// Convert object to double
	public static double ew_ConvertToDouble(object value)
	{
		try {
			return Convert.ToDouble(value);
		}	catch {
			return 0;
		}
	}

	// Convert object to bool
	public static bool ew_ConvertToBool(object value)
	{
		try {
			if (Information.IsNumeric(value)) {
				return Convert.ToBoolean(ew_ConvertToDouble(value));
			} else {
				return Convert.ToBoolean(value);
			}
		}	catch {
			return false;
		}
	}

	// Prepend CSS class name
	public static string ew_PrependClass(string attr, string classname) {
		classname = classname.Trim();
		if (ew_NotEmpty(classname)) {
			attr = attr.Trim();
			if (ew_NotEmpty(attr))
				attr = " " + attr;
			attr = classname + attr;
		}
		return attr;
	}

	// Append CSS class name
	public static string ew_AppendClass(string attr, string classname) {
		classname = classname.Trim();
		if (ew_NotEmpty(classname)) {
			attr = attr.Trim();
			if (ew_NotEmpty(attr))
				attr += " ";
			attr += classname;			
		}
		return attr;
	}

	// Add message
	public static void ew_AddMessage(ref string msg, string msgtoadd) {
		string sep = "<br />";
		if (ew_NotEmpty(msgtoadd)) {
			if (ew_NotEmpty(msg))
				msg += sep;
			msg += msgtoadd;
		}
	}

	// Add filter
	public static void ew_AddFilter(ref string filter, string newfilter) {
		if (ew_Empty(newfilter))
			return;
		if (ew_NotEmpty(filter)) {
			filter = "(" + filter + ") AND (" + newfilter + ")";
		} else {
			filter = newfilter;
		}
	}

	// Get user IP
	public static string ew_CurrentUserIP()
	{
		return HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
	}

	// Get current host name, e.g. "www.mycompany.com"
	public static string ew_CurrentHost()
	{
		return HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
	}

	// Get current date in default date format
	public static string ew_CurrentDate(int namedformat)
	{
		string DT;
		if (ew_Contains(namedformat, new int[] {5, 6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17})) {
			if (ew_Contains(namedformat, new int[] {5, 9, 12, 15})) {
				DT = ew_FormatDateTime(DateTime.Today, 5);
			} else if (ew_Contains(namedformat, new int[] {6, 10, 13, 16})) {
				DT = ew_FormatDateTime(DateTime.Today, 6);
			} else {
				DT = ew_FormatDateTime(DateTime.Today, 7);
			}
			return DT;
		} else {
			return ew_FormatDateTime(DateTime.Today, 5);
		}
	}

	public static string ew_CurrentDate()
	{
		return ew_CurrentDate(-1);
	}

	// Get current time in hh:mm:ss format
	public static string ew_CurrentTime()	{
		DateTime DT;
		DT = DateTime.Now;
		return DT.ToString("HH':'mm':'ss");
	}

	// Get current date in default date format with
	// Current time in hh:mm:ss format
	public static string ew_CurrentDateTime(int namedformat)	{
		string DT;
		if (ew_Contains(namedformat, new int[] {5, 6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17})) {
			if (ew_Contains(namedformat, new int[] {5, 9, 12, 15})) {
				DT = ew_FormatDateTime(DateTime.Now, 9);
			} else if (ew_Contains(namedformat, new int[] {6, 10, 13, 16})) {
				DT = ew_FormatDateTime(DateTime.Now, 10);
			} else {
				DT = ew_FormatDateTime(DateTime.Now, 11);
			}
			return DT;
		} else {
			return ew_FormatDateTime(DateTime.Now, 9);
		}		
	}

	// Get current date in default date format with
	// Current time in hh:mm:ss format
	public static string ew_CurrentDateTime()	{
		return ew_CurrentDateTime(-1);	
	}

	// Remove XSS
	public static object ew_RemoveXSS(object val)
	{
		string val_before;
		string pattern;
		string replacement;

		// Handle null value
		if (ew_Empty(val)) return val;

		// Remove all non-printable characters. CR(0a) and LF(0b) and TAB(9) are allowed 
		// This prevents some character re-spacing such as <java\0script> 
		// Note that you have to handle splits with \n, \r, and \t later since they *are* allowed in some inputs

		Regex regEx = new Regex("([\\x00-\\x08][\\x0b-\\x0c][\\x0e-\\x20])", RegexOptions.IgnoreCase);

		// Create regular expression.
		val = regEx.Replace(Convert.ToString(val), "");

		// Straight replacements, the user should never need these since they're normal characters 
		// This prevents like <IMG SRC=&#X40&#X61&#X76&#X61&#X73&#X63&#X72&#X69&#X70&#X74&#X3A&#X61&#X6C&#X65&#X72&#X74&#X28&#X27&#X58&#X53&#X53&#X27&#X29> 

		string search = "abcdefghijklmnopqrstuvwxyz";
		search = search + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		search = search + "1234567890!@#$%^&*()";
		search = search + "~`\";:?+/={}[]-_|'\\";
		for (int i = 0; i <= search.Length - 1; i++) {

			// ;? matches the ;, which is optional 
			// 0{0,7} matches any padded zeros, which are optional and go up to 8 chars 
			// &#x0040 @ search for the hex values

			regEx = new Regex("(&#[x|X]0{0,8}" + Conversion.Hex(Strings.Asc(search[i])) + ";?)");

			// With a ;
			val = regEx.Replace(Convert.ToString(val), Convert.ToString(search[i]));

			// &#00064 @ 0{0,7} matches '0' zero to seven times
			regEx = new Regex("(&#0{0,8}" + Strings.Asc(search[i]) + ";?)");

			// With a ;
			val = regEx.Replace(Convert.ToString(val), Convert.ToString(search[i]));
		}

		// Now the only remaining whitespace attacks are \t, \n, and \r
		bool Found = true;

		// Keep replacing as long as the previous round replaced something 
		while (Found) {
			val_before = Convert.ToString(val);
			for (int i = 0; i <= EW_REMOVE_XSS_KEYWORDS.GetUpperBound(0); i++) {
				pattern = "";
				for (int j = 0; j <= EW_REMOVE_XSS_KEYWORDS[i].Length - 1; j++) {
					if (j > 0) {
						pattern = pattern + "(";
						pattern = pattern + "(&#[x|X]0{0,8}([9][a][b]);?)?";
						pattern = pattern + "|(&#0{0,8}([9][10][13]);?)?";
						pattern = pattern + ")?";
					}
					pattern = pattern + EW_REMOVE_XSS_KEYWORDS[i][j];
				}
				replacement = EW_REMOVE_XSS_KEYWORDS[i].Substring(0, 2) + "<x>" + EW_REMOVE_XSS_KEYWORDS[i].Substring(2);

				// Add in <> to nerf the tag
				regEx = new Regex(pattern);
				val = regEx.Replace(Convert.ToString(val), replacement);

				// Filter out the hex tags
				if (ew_SameStr(val_before, val)) {

					// No replacements were made, so exit the loop
					Found = false;
				}
			}
		}
		return val;
	}

	// Adjust text for caption
	public static string ew_BtnCaption(string Caption)
	{
		int Min = 10;
		int Ln = Caption.Length;
		if (Ln < Min) {
			int Pad = Math.Abs(Conversion.Int((Min - Ln) / 2 * -1));
			return Caption.PadLeft(Ln + Pad).PadRight(Ln + Pad * 2);
		}	else {
			return Caption;
		}
	}

	// Highlight keywords
	public static string ew_Highlight(string name, object src, string bkw, string bkwtype, string akw)
	{
		string kw = "";
		string[] kwlist;
		string kwstr = "";
		string wrksrc;
		string outstr = "";
		int x;
		int y = 0;
		int xx;
		int yy;
		if (ew_NotEmpty(src) && (ew_NotEmpty(bkw) || ew_NotEmpty(akw)))	{
			string srcstr = Convert.ToString(src);
			xx = 0;
			yy = srcstr.IndexOf("<", xx);
			if (yy < 0) yy = srcstr.Length; 
			while (yy > 0) {
				if (yy > xx) {
					wrksrc = srcstr.Substring(xx, yy - xx);
					if (ew_NotEmpty(bkw)) kwstr = bkw.Trim(); 
					if (ew_NotEmpty(akw))	{
						if (kwstr.Length > 0) kwstr = kwstr + " "; 
						kwstr = kwstr + akw.Trim();
					}
					kwlist = kwstr.Split(new char[] {' '});
					x = 0;
					ew_GetKeyword(wrksrc, kwlist, x, ref y, ref kw);
					while (y >= 0) {
						outstr = outstr + wrksrc.Substring(x, y - x) + "<span id=\"" + name + "\" name=\"" + name + "\" class=\"ewHighlightSearch\">" + wrksrc.Substring(y, kw.Length) + "</span>";
						x = y + kw.Length;
						ew_GetKeyword(wrksrc, kwlist, x, ref y, ref kw);
					}
					outstr = outstr + wrksrc.Substring(x);
					xx = xx + wrksrc.Length;
				}
				if (xx < srcstr.Length) {
					yy = srcstr.IndexOf(">", xx);
					if (yy >= 0) {
						outstr = outstr + srcstr.Substring(xx, yy - xx + 1);
						xx = yy + 1;
						yy = srcstr.IndexOf("<", xx);
						if (yy < 0) yy = srcstr.Length; 
					}	else {
						outstr = outstr + srcstr.Substring(xx);
						yy = -1;
					}
				}	else {
					yy = -1;
				}
			}
		}	else {
			outstr = Convert.ToString(src);
		}
		return outstr;
	}

	// Get keyword
	public static void ew_GetKeyword(string src, string[] kwlist, int x, ref int y, ref string kw)
	{
		int wrky;
		int thisy = -1;
		string thiskw = "";
		string wrkkw = "";
		for (int i = 0; i <= kwlist.GetUpperBound(0); i++) {
			wrkkw = kwlist[i].Trim();
			if (ew_NotEmpty(wrkkw)) {
				if (EW_HIGHLIGHT_COMPARE)	{	// Case-insensitive
					wrky = src.IndexOf(wrkkw, x, StringComparison.CurrentCultureIgnoreCase);
				}	else {
					wrky = src.IndexOf(wrkkw, x);
				}
				if (wrky > -1) {
					if (thisy == -1) {
						thisy = wrky;
						thiskw = wrkkw;
					} else if (wrky < thisy) {
						thisy = wrky;
						thiskw = wrkkw;
					}
				}
			}
		}
		y = thisy;
		kw = thiskw;
	}

	// Set multiple attributes
	public static void ew_SetAttr(ref Hashtable ht, string[,] KeyValues)
	{
		if (Information.IsArray(KeyValues)) { // C#
			for (int i = 0; i <= KeyValues.GetLength(0) - 1; i++)
				ht[KeyValues[i, 0]] = KeyValues[i, 1];
		}
	}

	//
	// Security shortcut functions
	//
	// Get current user name
	public static string CurrentUserName()
	{
		return Convert.ToString(ew_Session[EW_SESSION_USER_NAME]);
	}

	// Get current user ID
	public static object CurrentUserID()
	{
		return Convert.ToString(ew_Session[EW_SESSION_USER_ID]);
	}

	// Get current parent user ID
	public static object CurrentParentUserID()
	{
		return Convert.ToString(ew_Session[EW_SESSION_PARENT_USER_ID]);
	}

	// Get current user level
	public static int CurrentUserLevel()
	{
		return Convert.ToInt32(ew_Session[EW_SESSION_USER_LEVEL_ID]);
	}

	// Get current user level list
	public string CurrentUserLevelList() {
		return (Security != null) ? Security.UserLevelList() : Convert.ToString(ew_Session[EW_SESSION_USER_LEVEL_ID]);
	}

	// Get Current user info
	public object CurrentUserInfo(string fldname) {
		if (Security != null) {
			return Security.CurrentUserInfo(fldname);
		} else if (ew_NotEmpty(ew_GetFieldValue("EW_USER_TABLE")) && !IsSysAdmin()) {
			string user = CurrentUserName();
			if (ew_NotEmpty(user))
				return ew_ExecuteScalar("SELECT " + ew_QuotedName(fldname) + " FROM " + EW_USER_TABLE + " WHERE " +
					EW_USER_NAME_FILTER.Replace("%u", ew_AdjustSql(user)));
		}
		return System.DBNull.Value;
	}

	// Get current page ID
	public static string CurrentPageID() {
		return (CurrentPage != null) ? CurrentPage.PageID : "";
	}	

	// Check if user password expired
	public static bool IsPasswordExpired()
	{
		return ew_SameStr(ew_Session[EW_SESSION_STATUS], "passwordexpired");
	}

	// Check if user is logging in (after changing password)
	public static bool IsLoggingIn()
	{
		return ew_SameStr(ew_Session[EW_SESSION_STATUS], "loggingin");
	}	

	// Is Logged In
	public static bool IsLoggedIn()
	{
		return ew_SameStr(ew_Session[EW_SESSION_STATUS], "login");
	}

	// Is System Admin
	public static bool IsSysAdmin()
	{
		return (Convert.ToInt32(ew_Session[EW_SESSION_SYS_ADMIN]) == 1);
	}

	// Current master table object
	public static cTable CurrentMasterTable()
	{
		if (CurrentPage != null && CurrentTable != null) {
			string MasterTableName = Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + CurrentTable.TableVar + "_" + EW_TABLE_MASTER_TABLE]);
			if (ew_NotEmpty(MasterTableName)) {
				PropertyInfo pi = CurrentPage.GetType().GetProperty(MasterTableName);
				if (pi != null)
					return (cTable)pi.GetValue(CurrentPage, null);
			}
		}
		return null;
	}

	// Execute SQL
	public static int ew_Execute(string Sql)
	{
		cConnection c = new cConnection();
		try {
			return c.Execute(Sql);
		}	finally {
			c.Dispose();
		}
	}

	// Execute SQL and return first value of first row
	public static object ew_ExecuteScalar(string Sql)
	{
		cConnection c = new cConnection();
		try {
			return c.ExecuteScalar(Sql);
		}	finally {
			c.Dispose();
		}
	}

	// Execute SQL and return first row
	public static OrderedDictionary ew_ExecuteRow(string Sql)
	{
		SqlDataReader dr = null;
		cConnection c = new cConnection();
		try {
			dr = c.GetDataReader(Sql);
			if (dr != null && dr.Read()) {
				return c.GetRow(ref dr);
			} else {
				return null;
			}			
		}	finally {
			if (dr != null) {
				dr.Close();
				dr.Dispose();
			}
			c.Dispose();
		}
	}

	//
	// TEA encrypt/decrypt class
	//
	public class cTEA
	{

		public static string Encrypt(string Data, string Key)
		{
			try
			{
				if (Data.Length == 0)
					throw new ArgumentException("Data must be at least 1 character in length.");
				uint[] formattedKey = FormatKey(Key);
				if (Data.Length % 2 != 0) Data += '\0'; // Make sure array is even in length.		
				byte[] dataBytes = Encoding.Unicode.GetBytes(Data);
				string cipher = string.Empty;
				uint[] tempData = new uint[2];
				for (int i=0; i<dataBytes.Length; i+=2)
				{
					tempData[0] = dataBytes[i];
					tempData[1] = dataBytes[i+1];
					code(tempData, formattedKey);
					cipher += ConvertUIntToString(tempData[0]) + ConvertUIntToString(tempData[1]);
				}
				return UrlEncode(Compress(cipher));
			} catch {
				return Data;
			}
		}

		public static string Decrypt(string Data, string Key)
		{
			try
			{
				Data = Decompress(UrlDecode(Data));
				uint[] formattedKey = FormatKey(Key);
				int x = 0;
				uint[] tempData = new uint[2];
				byte[] dataBytes = new byte[Data.Length / 8 * 2];
				for (int i=0; i<Data.Length; i+=8)
				{
					tempData[0] = ConvertStringToUInt(Data.Substring(i, 4));
					tempData[1] = ConvertStringToUInt(Data.Substring(i+4, 4));
					decode(tempData, formattedKey);
					dataBytes[x++] = (byte)tempData[0];
					dataBytes[x++] = (byte)tempData[1];
				}
				string decipheredString = Encoding.Unicode.GetString(dataBytes, 0, dataBytes.Length);
				if (decipheredString[decipheredString.Length - 1] == '\0')
					decipheredString = decipheredString.Substring(0, decipheredString.Length - 1);
				return decipheredString;
			} catch {
				return Data;
			}
		}

		public static string Compress(string text)
		{
			byte[] buffer = Encoding.Unicode.GetBytes(text);
			MemoryStream ms = new MemoryStream();
			using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, true))
			{
			 zip.Write(buffer, 0, buffer.Length);
			}
			ms.Position = 0;
			MemoryStream outStream = new MemoryStream();
			byte[] compressed = new byte[ms.Length];
			ms.Read(compressed, 0, compressed.Length);
			byte[] gzBuffer = new byte[compressed.Length + 4];
			System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
			System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
			return Convert.ToBase64String(gzBuffer);
		}

		public static string Decompress(string compressedText)
		{
			byte[] gzBuffer = Convert.FromBase64String(compressedText);
			using (MemoryStream ms = new MemoryStream())
			{
				int msgLength = BitConverter.ToInt32(gzBuffer, 0);
				ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
				byte[] buffer = new byte[msgLength];
				ms.Position = 0;
				using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress))
				{
				 zip.Read(buffer, 0, buffer.Length);
				}
				return Encoding.Unicode.GetString(buffer);
			}
		}

		private static uint[] FormatKey(string Key)
		{
			if (Key.Length == 0)
				throw new ArgumentException("Key must be between 1 and 16 characters in length");
			Key = Key.PadRight(16, ' ').Substring(0, 16); // Ensure that the key is 16 chars in length.
			uint[] formattedKey = new uint[4];

			// Get the key into the correct format for TEA usage.
			int j = 0;
			for (int i=0; i<Key.Length; i+=4)
				formattedKey[j++] = ConvertStringToUInt(Key.Substring(i, 4));
			return formattedKey;
		}

		private static void code(uint[] v, uint[] k)
		{
			uint y = v[0];
			uint z = v[1];
			uint sum = 0;
			uint delta=0x9E3779B9;
			uint n=32;
			while (n-->0)
			{
				y += (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
				sum += delta;
				z += (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
			}
			v[0]=y;
			v[1]=z;
		}

		private static void decode(uint[] v, uint[] k)
		{
			uint y=v[0];
			uint z=v[1];
			uint sum=0xC6EF3720;
			uint delta=0x9E3779B9;
			uint n=32;
			while (n-->0)
			{
				z -= (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
				sum -= delta;
				y -= (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
			}
			v[0]=y;
			v[1]=z;
		}

		private static uint ConvertStringToUInt(string Input)
		{
			uint output;
			output =  ((uint)Input[0]);
			output += ((uint)Input[1] << 8);
			output += ((uint)Input[2] << 16);
			output += ((uint)Input[3] << 24);
			return output;
		}

		private static string ConvertUIntToString(uint Input)
		{
			System.Text.StringBuilder output = new System.Text.StringBuilder();
			output.Append((char)((Input & 0xFF)));
			output.Append((char)((Input >> 8) & 0xFF));
			output.Append((char)((Input >> 16) & 0xFF));
			output.Append((char)((Input >> 24) & 0xFF));
			return output.ToString();
		}

		private static string UrlEncode(string str)
		{

			//System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();
			//str = Convert.ToBase64String(encoding.GetBytes(str));

			str = str.Replace('+', '-');
			str = str.Replace('/', '_');
			str = str.Replace('=', '.');
			return str;
		}

		private static string UrlDecode(string str)
		{
			str = str.Replace('-', '+');
			str = str.Replace('_', '/');
			str = str.Replace('.', '=');

			//byte[] dataBytes = Convert.FromBase64String(str);
			//System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();
			//return encoding.GetString(dataBytes);

			return str;
		}
	}

	// Save binary to file
	public static bool ew_SaveFile(string folder, string fn, ref byte[] filedata)
	{
		if (ew_CreateFolder(folder)) {
			try {
				FileStream fs;
				fs = new FileStream(folder + fn, FileMode.Create);
				fs.Write(filedata, 0, filedata.Length);
				fs.Close();
				return true;
			}	catch {
				if (EW_DEBUG_ENABLED) throw; 
				return false;
			}
		}
		return false;
	}

	// Get image content type
	public static string ew_GetImageContentType(string fn)
	{	
		string ext = Path.GetExtension(fn).Replace(".", "").ToLower();
		if (ext == "gif") { // gif
			return "image/gif";
		} else if (ext == "jpg" || ext == "jpeg" || ext == "jpe") { // jpg
			return "image/jpeg";
		} else if (ext == "png") { // png
			return "image/png";
		} else {
			return "image/bmp";
		}
	}

	// Read global debug message
	public static string ew_DebugMsg() {
		string msg = (ew_NotEmpty(gsDebugMsg)) ? "<p>" + gsDebugMsg + "</p>" : "";
		gsDebugMsg = "";
		return msg;
	}

	// Write global debug message
	public static void ew_SetDebugMsg(string v) {
		if (ew_NotEmpty(gsDebugMsg))
			gsDebugMsg += "<br>";
		gsDebugMsg += v;
	}

	//
	// Common base class
	//
	public class AspNetMakerBase
	{

		// Parent page (The ASP.NET page inherited from System.Web.UI.Page)
		protected AspNetMaker9_ControlVehicular m_ParentPage;

		// Page (ASP.NET Maker page, e.g. List/View/Add/Edit/Delete)
		protected AspNetMakerPage m_Page; 

		// Parent page (ASP.NET page)
		public AspNetMaker9_ControlVehicular ParentPage { 
			get { return m_ParentPage; }
			set {	m_ParentPage = value; }	
		}

		// Page
		public AspNetMakerPage Page { 
			get { return m_Page; }
			set {	m_Page = value; }						
		}

		// Connection
		public cConnection Conn {
			get { return ParentPage.Conn; }
			set { ParentPage.Conn = value; }
		}

		// Security
		public cAdvancedSecurity Security {
			get { return ParentPage.Security; }
			set { ParentPage.Security = value; }
		}

		// Form
		public cFormObj ObjForm {
			get { return ParentPage.ObjForm; }
			set { ParentPage.ObjForm = value; }
		}		

		// Language
		public cLanguage Language {
			get { return ParentPage.Language; }
			set { ParentPage.Language = value; }
		}
	}

	//
	// Common page class
	//
	public class AspNetMakerPage : AspNetMakerBase
	{

		// Page ID
		protected string m_PageID = "";

		public string PageID {
			get { return m_PageID; }
		}

		// Table name
		protected string m_TableName = "";

		public string TableName {
			get { return m_TableName; }
		}

		// Table object
		protected cTable m_Table = null;

		public cTable Table {
			get { return m_Table; }
		}

		// Page object name
		protected string m_PageObjName = "";

		public string PageObjName {
			get { return m_PageObjName; }
		}

		// Page object type name
		protected string m_PageObjTypeName = "";

		public string PageObjTypeName {
			get { return m_PageObjTypeName; }
		}

		// Page Name
		public string PageName {
			get { return ew_CurrentPage(); }
		}

		// Message
		public string Message {
			get { return Convert.ToString(ew_Session[EW_SESSION_MESSAGE]); }
			set {
				string msg = Convert.ToString(ew_Session[EW_SESSION_MESSAGE]); 
				ew_AddMessage(ref msg, value);
				ew_Session[EW_SESSION_MESSAGE] = msg;
			}
		}

		// Failure Message
		public string FailureMessage {
			get { return Convert.ToString(ew_Session[EW_SESSION_FAILURE_MESSAGE]); }
			set {
				string msg = Convert.ToString(ew_Session[EW_SESSION_FAILURE_MESSAGE]); 
				ew_AddMessage(ref msg, value);
				ew_Session[EW_SESSION_FAILURE_MESSAGE] = msg;
			}
		}

		// Success Message
		public string SuccessMessage {
			get { return Convert.ToString(ew_Session[EW_SESSION_SUCCESS_MESSAGE]); }
			set {
				string msg = Convert.ToString(ew_Session[EW_SESSION_SUCCESS_MESSAGE]); 
				ew_AddMessage(ref msg, value);
				ew_Session[EW_SESSION_SUCCESS_MESSAGE] = msg;
			}
		}
	}

	//
	//  Language class
	//
	public class cLanguage : AspNetMakerBase, IDisposable
	{
		string LanguageId;
		XmlDocument objDOM;
		StringDictionary Col;
		string LanguageFolder = EW_LANGUAGE_FOLDER;

		// Constructor
		public cLanguage(AspNetMakerPage APage) : this(APage, "", "")
		{
		}

		// Constructor
		public cLanguage(AspNetMakerPage APage, string langfolder) : this(APage, langfolder, "")
		{
		}

		// Constructor
		public cLanguage(AspNetMakerPage APage, string langfolder, string langid)
		{
			m_Page = APage;
			m_ParentPage = APage.ParentPage;
			if (ew_NotEmpty(langfolder))
				LanguageFolder = langfolder;

			// Set up file list
			LoadFileList();

			// Set up language id
			if (ew_NotEmpty(langid)) { // Set up language id
				LanguageId = langid;
				ew_Session[EW_SESSION_LANGUAGE_ID] = LanguageId;
			} else if (ew_NotEmpty(ew_Get("language"))) {
				LanguageId = ew_Get("language");
				ew_Session[EW_SESSION_LANGUAGE_ID] = LanguageId;
			} else if (ew_NotEmpty(ew_Session[EW_SESSION_LANGUAGE_ID])) {
				LanguageId = Convert.ToString(ew_Session[EW_SESSION_LANGUAGE_ID]);
			}	else {
				LanguageId = EW_LANGUAGE_DEFAULT_ID;
			}
			gsLanguage = LanguageId;
			Load(LanguageId);
		}

		// Terminate
		public void Dispose()
		{
			objDOM = null;
		}

		// Load language file list
		private void LoadFileList()
		{
			if (Information.IsArray(EW_LANGUAGE_FILE)) {
				for (int i = 0; i < EW_LANGUAGE_FILE.GetLength(0); i++)
					EW_LANGUAGE_FILE[i][1] = LoadFileDesc(ew_MapPath(LanguageFolder + EW_LANGUAGE_FILE[i][2]));
			}
		}

		// Load language file description
		private string LoadFileDesc(string File)
		{
			XmlTextReader xmlr = new XmlTextReader(File);
			xmlr.WhitespaceHandling = WhitespaceHandling.None;
			try {
				while (!xmlr.EOF) {
					xmlr.Read();
					if (xmlr.IsStartElement() && xmlr.Name == "ew-language")
						return xmlr.GetAttribute("desc");
				}
			}	finally {
				xmlr.Close();
			}
			return "";
		}

		// Load language file
		private void Load(string id)
		{
			string sFileName = GetFileName(id);
			if (ew_Empty(sFileName))
				sFileName = GetFileName(EW_LANGUAGE_DEFAULT_ID);
			if (ew_Empty(sFileName)) return; 
			if (EW_USE_DOM_XML)	{
				objDOM = new XmlDocument();
				objDOM.Load(sFileName);
			}	else {
				if (ew_Session[EW_PROJECT_NAME + "_" + sFileName] != null) {
					Col = (StringDictionary)ew_Session[EW_PROJECT_NAME + "_" + sFileName];
				}	else {
					Col = new StringDictionary();
					XmlToCollection(sFileName);
					ew_Session[EW_PROJECT_NAME + "_" + sFileName] = Col;
				}
			}
		}

		// Convert XML to Collection
		private void XmlToCollection(string File)
		{
			string Key = "/";
			string Id;
			string Name;
			int Index;
			XmlTextReader xmlr = new XmlTextReader(File);
			xmlr.WhitespaceHandling = WhitespaceHandling.None;
			try {
				while (!xmlr.EOF) {
					xmlr.Read();
					Name = xmlr.Name;
					Id = xmlr.GetAttribute("id");
					if (Name == "ew-language")
						continue; 
					switch (xmlr.NodeType) {
						case XmlNodeType.Element:
							if (xmlr.IsStartElement() && !xmlr.IsEmptyElement) {
								Key += Name + "/";
								if (Id != null)
									Key += Id + "/"; 
							}
							if (Id != null && xmlr.IsEmptyElement) {	// phrase
								Id = Name + "/" + Id;
								if (xmlr.GetAttribute("client") == "1")
									Id += "/1"; 
								if (Id != null)
									Col[Key + Id] = xmlr.GetAttribute("value"); 
							}
							break;
						case XmlNodeType.EndElement:
							Index = Key.LastIndexOf("/" + Name + "/");
							if (Index > -1)
								Key = Key.Substring(0, Index + 1); 
							break;
					}
				}
			}	finally {
				xmlr.Close();
			}
		}

		// Get language file name
		private string GetFileName(string Id)
		{
			if (Information.IsArray(EW_LANGUAGE_FILE)) {
				for (int i = 0; i < EW_LANGUAGE_FILE.GetLength(0); i++) {
					if (EW_LANGUAGE_FILE[i][0] == Id)
						return ew_MapPath(LanguageFolder + EW_LANGUAGE_FILE[i][2]);
				}
			}
			return "";
		}

		// Get node attribute
		private string GetNodeAtt(XmlNode Node, string Att)
		{
			if (Node != null)	{
				return ((XmlElement)Node).GetAttribute(Att);
			}	else {
				return "";
			}
		}

		// Get phrase
		public string Phrase(string Id)
		{
			if (EW_USE_DOM_XML)	{
				return GetNodeAtt(objDOM.SelectSingleNode("//global/phrase[@id='" + Id.ToLowerInvariant() + "']"), "value");
			}	else	{
				if (Col.ContainsKey("/global/phrase/" + Id.ToLowerInvariant()))	{
					return Col["/global/phrase/" + Id.ToLowerInvariant()];
				} else if (Col.ContainsKey("/global/phrase/" + Id.ToLowerInvariant() + "/1")) {
					return Col["/global/phrase/" + Id.ToLowerInvariant() + "/1"];
				}	else	{
					return "";
				}
			}
		}

		// Set phrase
		public void SetPhrase(string Id, string Value)
		{
			if (!EW_USE_DOM_XML) {
				if (Col.ContainsKey("/global/phrase/" + Id.ToLowerInvariant())) {
					Col["/global/phrase/" + Id.ToLowerInvariant()] = Value;
				}	else if (Col.ContainsKey("/global/phrase/" + Id.ToLowerInvariant() + "/1")) {
					Col["/global/phrase/" + Id.ToLowerInvariant() + "/1"] = Value;
				}
			}
		}

		// Get project phrase
		public string ProjectPhrase(string Id)
		{
			if (EW_USE_DOM_XML)	{
				return GetNodeAtt(objDOM.SelectSingleNode("//project/phrase[@id='" + Id.ToLowerInvariant() + "']"), "value");
			}	else {
				return Col["/project/phrase/" + Id.ToLowerInvariant()];
			}
		}

		// Set project phrase
		public void SetProjectPhrase(string Id, string Value)
		{
			if (!EW_USE_DOM_XML)
				Col["/project/phrase/" + Id.ToLowerInvariant()] = Value;
		}

		// Get menu phrase
		public string MenuPhrase(string MenuId, string Id)
		{
			if (EW_USE_DOM_XML) {
				return GetNodeAtt(objDOM.SelectSingleNode("//project/menu[@id='" + MenuId + "']/phrase[@id='" + Id.ToLowerInvariant() + "']"), "value");
			}	else	{
				return Col["/project/menu/" + MenuId + "/phrase/" + Id.ToLowerInvariant()];
			}
		}

		// Set menu phrase
		public void SetMenuPhrase(string MenuId, string Id, string Value)
		{
			if (!EW_USE_DOM_XML)
				Col["/project/menu/" + MenuId + "/phrase/" + Id.ToLowerInvariant()] = Value;
		}

		// Get table phrase
		public string TablePhrase(string TblVar, string Id)
		{
			if (EW_USE_DOM_XML)	{
				return GetNodeAtt(objDOM.SelectSingleNode("//project/table[@id='" + TblVar.ToLowerInvariant() + "']/phrase[@id='" + Id.ToLowerInvariant() + "']"), "value");
			}	else	{
				return Col["/project/table/" + TblVar.ToLowerInvariant() + "/phrase/" + Id.ToLowerInvariant()];
			}
		}

		// Set table phrase
		public void SetTablePhrase(string TblVar, string Id, string Value)
		{
			if (!EW_USE_DOM_XML)
				Col["/project/table/" + TblVar.ToLowerInvariant() + "/phrase/" + Id.ToLowerInvariant()] = Value;
		}

		// Get field phrase
		public string FieldPhrase(string TblVar, string FldVar, string Id)
		{
			if (EW_USE_DOM_XML) {
				return GetNodeAtt(objDOM.SelectSingleNode("//project/table[@id='" + TblVar.ToLowerInvariant() + "']/field[@id='" + FldVar.ToLowerInvariant() + "']/phrase[@id='" + Id.ToLowerInvariant() + "']"), "value");
			}	else	{
				return Col["/project/table/" + TblVar.ToLowerInvariant() + "/field/" + FldVar.ToLowerInvariant() + "/phrase/" + Id.ToLowerInvariant()];
			}
		}

		// Set field phrase
		public void SetFieldPhrase(string TblVar, string FldVar, string Id, string Value)
		{
			if (!EW_USE_DOM_XML)
				Col["/project/table/" + TblVar.ToLowerInvariant() + "/field/" + FldVar.ToLowerInvariant() + "/phrase/" + Id.ToLowerInvariant()] = Value;
		}

		// Output XML as JSON
		public string XmlToJSON(string XPath)
		{
			string Id;
			string Value;
			XmlNodeList NodeList = objDOM.SelectNodes(XPath);
			string Str = "{";
			foreach (XmlNode Node in NodeList) {
				Id = GetNodeAtt(Node, "id");
				Value = GetNodeAtt(Node, "value");
				Str += "\"" + ew_JsEncode2(Id) + "\":\"" + ew_JsEncode2(Value) + "\",";
			}
			if (Str.EndsWith(","))
				Str = Str.Substring(0, Str.Length - 1); 
			Str += "}\r\n";
			return Str;
		}

		// Output collection as JSON
		public string CollectionToJSON(string Prefix, string Suffix)
		{
			string Id;
			int Pos;
			string Str = "{";
			foreach (string Name in Col.Keys) {
				if (Name.StartsWith(Prefix))	{
					if (ew_NotEmpty(Suffix) && Name.EndsWith(Suffix))	{
						Pos = Name.LastIndexOf(Suffix);
						Id = Name.Substring(Prefix.Length, Pos - Prefix.Length);
					}	else	{
						Id = Name.Substring(Prefix.Length);
					}
					Str += "\"" + ew_JsEncode2(Id) + "\":\"" + ew_JsEncode2(Col[Name]) + "\",";
				}
			}
			if (Str.EndsWith(","))
				Str = Str.Substring(0, Str.Length - 1); 
			Str += "}\r\n";
			return Str;
		}

		// Output all phrases as JSON
		public string AllToJSON()
		{
			if (EW_USE_DOM_XML)	{
				return "var ewLanguage = new ew_Language(" + XmlToJSON("//global/phrase") + ");";
			}	else	{
				return "var ewLanguage = new ew_Language(" + CollectionToJSON("/global/phrase/", "") + ");";
			}
		}

		// Output client phrases as JSON
		public string ToJSON()
		{
			if (EW_USE_DOM_XML)	{
				return "var ewLanguage = new ew_Language(" + XmlToJSON("//global/phrase[@client='1']") + ");";
			}	else	{
				return "var ewLanguage = new ew_Language(" + CollectionToJSON("/global/phrase/", "/1") + ");";
			}
		}
	}

	//
	//  XML document class
	//
	public class cXMLDocument : IDisposable
	{

		public string Encoding = "";
		string RootTagName = "table";
		string SubTblName = "";
		string RowTagName = "row";
		XmlDocument XmlDoc;
		XmlElement XmlTbl;
		XmlElement XmlSubTbl;
		XmlElement XmlRow;
		XmlElement XmlFld;

		// Constructor
		public cXMLDocument()
		{
			XmlDoc = new XmlDocument();
		}

		// Add root
		public void AddRoot(string rootname)
		{
			RootTagName = ew_XmlTagName(rootname);
			XmlTbl = XmlDoc.CreateElement(RootTagName);
			XmlDoc.AppendChild(XmlTbl);
		}

		// Add row
		public void AddRow(string tablename, string rowname)
		{
			if (ew_NotEmpty(rowname))
				RowTagName = ew_XmlTagName(rowname);
			XmlRow = XmlDoc.CreateElement(RowTagName);
			if (ew_Empty(tablename)) {
				if (XmlTbl != null)
					XmlTbl.AppendChild(XmlRow);
			} else {
				if (ew_Empty(SubTblName)) {
					SubTblName = ew_XmlTagName(tablename);
					XmlSubTbl = XmlDoc.CreateElement(SubTblName);
					XmlTbl.AppendChild(XmlSubTbl);
				}
				if (XmlSubTbl != null)
					XmlSubTbl.AppendChild(XmlRow);
			}
		}

		// Add row by name
		public void AddRowEx(string Name)
		{
			XmlRow = XmlDoc.CreateElement(Name);
			XmlTbl.AppendChild(XmlRow);
		}

		// Add field
		public void AddField(string name, object value)
		{
			XmlFld = XmlDoc.CreateElement(ew_XmlTagName(name));
			XmlRow.AppendChild(XmlFld);
			XmlFld.AppendChild(XmlDoc.CreateTextNode(Convert.ToString(value)));
		}

		// XML
		public string XML()
		{
			return XmlDoc.OuterXml;
		}

		// Output
		public void Output()
		{
			if (HttpContext.Current.Response.Buffer)
				HttpContext.Current.Response.Clear(); 
			HttpContext.Current.Response.ContentType = "text/xml";
			string PI = "<?xml version=\"1.0\"";
			if (ew_NotEmpty(Encoding))
				PI += " encoding=\"" + Encoding + "\"";
			PI += " ?>";
			ew_Write(PI + XmlDoc.OuterXml);
		}

		// Output XML for debug
		public void Print()
		{
			if (HttpContext.Current.Response.Buffer)
				HttpContext.Current.Response.Clear(); 
			HttpContext.Current.Response.ContentType = "text/plain";
			ew_Write(ew_HtmlEncode(XmlDoc.OuterXml));
		}

		// Terminate
		public void Dispose()
		{
			XmlFld = null;
			XmlRow = null;
			XmlTbl = null;
			XmlDoc = null;
		}
	}

	//
	// Email class
	//
	public class cEmail
	{

		public string Sender = ""; // Sender		

		public string Recipient = ""; // Recipient		

		public string Cc = ""; // Cc		

		public string Bcc = ""; // Bcc		

		public string Subject = ""; // Subject		

		public string Format = ""; // Format		

		public string Content = ""; // Content		

		public string Charset = ""; // Charset

		public string SendErrNumber = ""; // Send error number

		public string SendErrDescription = ""; // Send error description

		// Load email from template
		public void Load(string fn)
		{
			string sHeader;
			string[] arrHeader;
			string sName;
			string sValue;
			int i, j;
			string sWrk = ew_LoadTxt(fn);

			// Load text file content
			sWrk = sWrk.Replace("\r\n", "\n");

			// Convert to Lf
			sWrk = sWrk.Replace("\r", "\n");

			// Convert to Lf
			if (ew_NotEmpty(sWrk)) {
				i = sWrk.IndexOf("\n" + "\n");

				// Locate header and mail content
				if (i > 0) {
					sHeader = sWrk.Substring(0, i + 1);
					Content = sWrk.Substring(i + 2);
					arrHeader = sHeader.Split(new char[] {'\n'});
					for (j = 0; j <= arrHeader.GetUpperBound(0); j++) {
						i = arrHeader[j].IndexOf(":");
						if (i > 0)
						{
							sName = arrHeader[j].Substring(0, i).Trim();
							sValue = arrHeader[j].Substring(i + 1).Trim();
							switch (sName.ToLowerInvariant()) {
								case "subject":
									Subject = sValue;
									break;
								case "from":
									Sender = sValue;
									break;
								case "to":
									Recipient = sValue;
									break;
								case "cc":
									Cc = sValue;
									break;
								case "bcc":
									Bcc = sValue;
									break;
								case "format":
									Format = sValue;
									break;
							}
						}
					}
				}
			}
		}

		// Replace sender
		public void ReplaceSender(string ASender)
		{
			Sender = Sender.Replace("<!--$From-->", ASender);
		}

		// Replace recipient
		public void ReplaceRecipient(string ARecipient)
		{
			Recipient = Recipient.Replace("<!--$To-->", ARecipient);
		}

		// Add cc email
		public void AddCc(string ACc)
		{
			if (ew_NotEmpty(ACc)) {
				if (ew_NotEmpty(Cc)) Cc = Cc + ";"; 
				Cc = Cc + ACc;
			}
		}

		// Add bcc email
		public void AddBcc(string ABcc)
		{
			if (ew_NotEmpty(ABcc)) {
				if (ew_NotEmpty(Bcc)) Bcc = Bcc + ";"; 
				Bcc = Bcc + ABcc;
			}
		}

		// Replace subject
		public void ReplaceSubject(string ASubject)
		{
			Subject = Subject.Replace("<!--$Subject-->", ASubject);
		}

		// Replace content
		public void ReplaceContent(string Find, string ReplaceWith)
		{
			Content = Content.Replace(Find, ReplaceWith);
		}

		// Send email
		public bool Send()
		{
			bool bSend = ew_SendEmail(Sender, Recipient, Cc, Bcc, Subject, Content, Format, Charset);
			if (!bSend)
				SendErrDescription = gsEmailErrDesc; // Send error description
			return bSend;
		}

		// Display as string
		public string AsString()
		{
			return "{Sender: " + Sender + ", Recipient: " + Recipient + ", Cc: " + Cc + ", Bcc: " + Bcc + ", Subject: " + Subject + ", Format: " + Format + ", Content: " + Content + ", Charset: " + Charset + "}";
		}
	}

	//
	// Class for Pager item
	//
	public class cPagerItem
	{

		public string Text;

		public int Start;

		public bool Enabled;

		// Constructor
		public cPagerItem(int AStart, string AText, bool AEnabled)
		{
			Text = AText;
			Start = AStart;
			Enabled = AEnabled;
		}

		// Constructor
		public cPagerItem()
		{

			// Do nothing
		}
	}

	//
	// Class for Numeric pager
	//	
	public class cNumericPager
	{

		public ArrayList Items = new ArrayList();

		public int PageSize;

		public int ToIndex;

		public int Count;

		public int FromIndex;

		public int RecordCount;

		public int Range;

		public cPagerItem LastButton;

		public cPagerItem PrevButton;

		public cPagerItem FirstButton;

		public cPagerItem NextButton;

		public int ButtonCount;

		public bool Visible;

		// Constructor
		public cNumericPager(int AFromIndex, int APageSize, int ARecordCount, int ARange)
		{
			FromIndex = AFromIndex;
			PageSize = APageSize;
			RecordCount = ARecordCount;
			Range = ARange;
			FirstButton = new cPagerItem();
			PrevButton = new cPagerItem();
			NextButton = new cPagerItem();
			LastButton = new cPagerItem();
			Visible = true;
			Init();
		}

		// Init pager
		public void Init()
		{
			if (FromIndex > RecordCount) FromIndex = RecordCount; 
			ToIndex = FromIndex + PageSize - 1;
			if (ToIndex > RecordCount) ToIndex = RecordCount; 
			Count = 0;
			SetupNumericPager();

			// Update button count
			ButtonCount = Count + 1;
			if (FirstButton.Enabled) ButtonCount = ButtonCount + 1; 
			if (PrevButton.Enabled) ButtonCount = ButtonCount + 1; 
			if (NextButton.Enabled) ButtonCount = ButtonCount + 1; 
			if (LastButton.Enabled) ButtonCount = ButtonCount + 1; 
		}

		// Add pager item
		private void AddPagerItem(int StartIndex, string Text, bool Enabled)
		{
			Items.Add(new cPagerItem(StartIndex, Text, Enabled));
			Count = Items.Count;
		}

		// Setup pager items
		private void SetupNumericPager()
		{
			bool HasPrev;
			bool NoNext;
			int dy2;
			int dx2;
			int y;
			int x;
			int dx1;
			int dy1;
			int ny;
			int TempIndex;
			if (RecordCount > PageSize)	{
				NoNext = (RecordCount < (FromIndex + PageSize));
				HasPrev = (FromIndex > 1);

				// First Button
				TempIndex = 1;
				FirstButton.Start = TempIndex;
				FirstButton.Enabled = (FromIndex > TempIndex);

				// Prev Button
				TempIndex = FromIndex - PageSize;
				if (TempIndex < 1) TempIndex = 1; 
				PrevButton.Start = TempIndex;
				PrevButton.Enabled = HasPrev;

				// Page links
				if (HasPrev | !NoNext) {
					x = 1;
					y = 1;
					dx1 = ((FromIndex - 1) / (PageSize * Range)) * PageSize * Range + 1;
					dy1 = ((FromIndex - 1) / (PageSize * Range)) * Range + 1;
					if ((dx1 + PageSize * Range - 1) > RecordCount)	{
						dx2 = (RecordCount / PageSize) * PageSize + 1;
						dy2 = (RecordCount / PageSize) + 1;
					}	else {
						dx2 = dx1 + PageSize * Range - 1;
						dy2 = dy1 + Range - 1;
					}
					while (x <= RecordCount) {
						if (x >= dx1 & x <= dx2) {
							AddPagerItem(x, Convert.ToString(y), FromIndex != x);
							x = x + PageSize;
							y = y + 1;
						}
else if (x >= (dx1 - PageSize * Range) & x <= (dx2 + PageSize * Range)) {
							if (x + Range * PageSize < RecordCount)	{
								AddPagerItem(x, y + "-" + (y + Range - 1), true);
							}	else {
								ny = (RecordCount - 1) / PageSize + 1;
								if (ny == y) {
									AddPagerItem(x, Convert.ToString(y), true);
								}	else {
									AddPagerItem(x, y + "-" + ny, true);
								}
							}
							x = x + Range * PageSize;
							y = y + Range;
						}	else {
							x = x + Range * PageSize;
							y = y + Range;
						}
					}
				}

				// Next Button
				NextButton.Start = FromIndex + PageSize;
				TempIndex = FromIndex + PageSize;
				NextButton.Start = TempIndex;
				NextButton.Enabled = !NoNext;

				// Last Button
				TempIndex = ((RecordCount - 1) / PageSize) * PageSize + 1;
				LastButton.Start = TempIndex;
				LastButton.Enabled = (FromIndex < TempIndex);
			}
		}
	}

	//
	// Class for PrevNext pager
	//
	public class cPrevNextPager
	{

		public cPagerItem NextButton;

		public cPagerItem FirstButton;

		public cPagerItem PrevButton;

		public cPagerItem LastButton;

		public int ToIndex;

		public int PageCount;

		public int CurrentPage;

		public int PageSize;

		public int FromIndex;

		public int RecordCount;

		public bool Visible;

		// Constructor
		public cPrevNextPager(int AFromIndex, int APageSize, int ARecordCount)
		{
			FromIndex = AFromIndex;
			PageSize = APageSize;
			RecordCount = ARecordCount;
			FirstButton = new cPagerItem();
			PrevButton = new cPagerItem();
			NextButton = new cPagerItem();
			LastButton = new cPagerItem();
			Visible = true;
			Init();
		}

		// Method to init pager
		public void Init()
		{
			int TempIndex;
			if (PageSize > 0) {
				CurrentPage = (FromIndex - 1) / PageSize + 1;
				PageCount = (RecordCount - 1) / PageSize + 1;
				if (FromIndex > RecordCount) FromIndex = RecordCount; 
				ToIndex = FromIndex + PageSize - 1;
				if (ToIndex > RecordCount) ToIndex = RecordCount; 

				// First Button
				TempIndex = 1;
				FirstButton.Start = TempIndex;
				FirstButton.Enabled = (TempIndex != FromIndex);

				// Prev Button
				TempIndex = FromIndex - PageSize;
				if (TempIndex < 1) TempIndex = 1; 
				PrevButton.Start = TempIndex;
				PrevButton.Enabled = (TempIndex != FromIndex);

				// Next Button
				TempIndex = FromIndex + PageSize;
				if (TempIndex > RecordCount) TempIndex = FromIndex; 
				NextButton.Start = TempIndex;
				NextButton.Enabled = (TempIndex != FromIndex);

				// Last Button
				TempIndex = ((RecordCount - 1) / PageSize) * PageSize + 1;
				LastButton.Start = TempIndex;
				LastButton.Enabled = (TempIndex != FromIndex);
			}
		}
	}

	// Table class
	public class cTable: AspNetMakerBase
	{
			protected string m_TableName = ""; // Table name
			protected string m_TableObjName = ""; // Table object name
			protected string m_TableObjTypeName = ""; // Table object name
			protected string m_TableType = ""; // Table type

			public int RowType; // Row Type

			public string CssClass = ""; // CSS class	

			public string CssStyle = ""; // CSS style		

			public Hashtable RowAttrs = new Hashtable();

			public string Export = ""; // Export						

			public bool ExportOriginalValue = EW_EXPORT_ORIGINAL_VALUE; // Export original value			

			public string TableFilter = "";		

			public Dictionary<string, cField> Fields = new Dictionary<string, cField>();			

			public bool UseTokenInUrl = EW_USE_TOKEN_IN_URL; // Define table level constants				

			public bool SendEmail; // Send email			

			public string TableCustomInnerHtml; // Custom inner HTML

			// Table name
			public string TableName {
				get { return m_TableName; }
			}

			// Table object name
			public string TableObjName {
				get { return m_TableObjName; }
			}

			// Table variable
			public string TableVar {
				get { return m_TableObjName; }
			}

			// Table object name
			public string TableObjTypeName {
				get { return m_TableObjTypeName; }
			}

			// Table type
			public string TableType {
				get {	return m_TableType; }
			}

			// Get field values
			public Hashtable GetFieldValues(string propertyname) {
				Hashtable values = new Hashtable();
				foreach (KeyValuePair<string, cField> kvp in Fields) {
					cField fld = kvp.Value;
					FieldInfo fi = fld.GetType().GetField(propertyname);
					values.Add(fld.FldName, (fi != null) ? fi.GetValue(fld) : null);
				}
				return values;
			}

			// Table caption
			public string TableCaption {
				get { return Language.TablePhrase(TableVar, "TblCaption"); }
			}

			// Page caption
			public string PageCaption(int Page) {
				string sPageCaption = Language.TablePhrase(TableVar, "TblPageCaption" + Convert.ToString(Page));
				if (ew_Empty(sPageCaption))
					sPageCaption = "Page " + Convert.ToString(Page);
				return sPageCaption;
			}

			// Row Styles
			public string RowStyles {
				get {
					string sAtt = "";
					string sStyle = CssStyle;
					if (RowAttrs.Contains("style") && ew_NotEmpty(RowAttrs["style"]))
						sStyle += " " + RowAttrs["style"];
					string sClass = CssClass;
					if (RowAttrs.Contains("class") && ew_NotEmpty(RowAttrs["class"]))
						sClass += " " + RowAttrs["class"];
					if (ew_NotEmpty(sStyle))
						sAtt += " style=\"" + sStyle.Trim() + "\"";
					if (ew_NotEmpty(sClass))
						sAtt += " class=\"" + sClass.Trim() + "\"";
					return sAtt;
				}
			}

			// Row Attribute
			public string RowAttributes {
				get {
					string sAtt = RowStyles;
					if (ew_Empty(Export)) {
						foreach (DictionaryEntry Attr in RowAttrs) {
							if (!ew_SameText(Attr.Key, "style") && !ew_SameText(Attr.Key, "class") && ew_NotEmpty(Attr.Value))
								sAtt += " " + Convert.ToString(Attr.Key) + "=\"" + Convert.ToString(Attr.Value).Trim() + "\"";
						}
					}
					return sAtt;
				}
			}

			// Reset attributes for table object
			public void ResetAttrs() {
				CssClass = "";
				CssStyle = "";
				RowAttrs.Clear();
				foreach (KeyValuePair<string, cField> kvp in Fields)
					kvp.Value.ResetAttrs();
			}

			// Setup field titles
			public void SetupFieldTitles() {
				foreach (KeyValuePair<string, cField> kvp in Fields) {
					cField fld = kvp.Value;
					if (ew_NotEmpty(fld.FldTitle)) {
						fld.EditAttrs["onmouseover"] = "ew_ShowTitle(this, '" + ew_JsEncode3(fld.FldTitle) + "');";
						fld.EditAttrs["onmouseout"] = "ew_HideTooltip();";
					}
				}
			}

			// Get field object by name
			public cField FieldByName(string Name) {
				if (Fields.ContainsKey(Name)) {
					return Fields[Name];
				} else {
					PropertyInfo pi = this.GetType().GetProperty(Name);
					if (pi != null)
						return (cField)pi.GetValue(this, null);
				}
				return null;
			}			
	}

	//
	//  Field class
	//
	public class cField: AspNetMakerBase, IDisposable
	{

		public string TblName; 

		public string TblVar; // Table var

		public string FldName; // Field name

		public string FldVar; // Field variable name

		public string FldExpression; // Field expression (used in SQL)

		public bool FldIsVirtual; // Virtual field

		public string FldVirtualExpression; // Virtual field expression (used in ListSQL)

		public bool FldForceSelection; // Autosuggest force selection

		public object VirtualValue; // Virtual field value

		public object TooltipValue; // Field tooltip value

		public int TooltipWidth; // Field tooltip width

		public int FldType; // Field type (ADO data type)

		public SqlDbType FldDbType; // Field type (.NET data type)

		public int FldDataType; // Field type (ASP.NET Maker data type)

		public string FldBlobType; // For Oracle only

		public bool Visible; // Visible

		public string FldViewTag = ""; // View Tag

		public bool FldIsDetailKey = false; // Field is detail key

		public int FldDateTimeFormat; // Date time format

		public string CssStyle = ""; // CSS style

		public string CssClass = ""; // CSS class

		public string ImageAlt = ""; // Image alt

		public int ImageWidth; // Image width

		public int ImageHeight; // Image height

		public bool ImageResize = false; // Image resize

		public int ResizeQuality = 100; // Resize quality

		public string ViewCustomAttributes = "";

		public string EditCustomAttributes = "";

		public string CellCustomAttributes = "";

		public string LinkCustomAttributes = ""; // Link custom attributes

		public string CustomMsg = ""; // Custom message

		public string CellCssClass = ""; // Cell CSS class

		public string CellCssStyle = ""; // Cell CSS style

		public string MultiUpdate = ""; // Multi update

		public object OldValue; // Old Value

		public object ConfirmValue; // Confirm Value

		public object CurrentValue; // Current value

		public object ViewValue; // View value

		public object EditValue; // Edit value

		public object EditValue2; // Edit value 2 (search)

		public object HrefValue; // Href value

		public object HrefValue2;

		public Hashtable CellAttrs = new Hashtable(); // Cell attributes

		public Hashtable EditAttrs = new Hashtable(); // Edit Attributes

		public Hashtable ViewAttrs = new Hashtable(); // View Attributes

		public Hashtable LinkAttrs = new Hashtable(); // Link custom attributes

		private string m_FormValue; // Form value

		private string m_QueryStringValue; // QueryString value

		private object m_DbValue; // Database Value		

		public bool Disabled; // Disabled

		public bool ReadOnly; // ReadOnly		

		public bool TruncateMemoRemoveHtml; // Remove Html from Memo field

		public string FldDefaultErrMsg;

		public bool Sortable; 

		public int Count; // Count		

		public double Total; // Total		

		private cAdvancedSearch m_AdvancedSearch; // Advanced Search Object		

		private cUpload m_Upload; // Upload Object

		public string UploadPath; // Upload path

		// Create new field object
		public cField(ref AspNetMakerPage APage, string atblvar, string atblname, string afldvar, string afldname, string afldexpression, int afldtype, SqlDbType aflddbtype, int aflddatatype, int aflddtformat, bool aforceselect, string afldviewtag)
		{
			m_Page = APage;
			m_ParentPage = APage.ParentPage; 
			TblVar = atblvar;
			TblName = atblname; 
			FldVar = afldvar;
			FldName = afldname;
			FldExpression = afldexpression;
			FldType = afldtype;
			FldDbType = aflddbtype;
			FldDataType = aflddatatype;
			FldDateTimeFormat = aflddtformat;
			FldForceSelection = aforceselect;
			FldViewTag = afldviewtag;
			ImageWidth = 0;
			ImageHeight = 0;
			Visible = true;
			Count = 0;
			Total = 0;
			Disabled = false;
			Sortable = true;
			TruncateMemoRemoveHtml = false;
			TooltipWidth = 0;
			FldIsVirtual = false;
		}

		public string FldCaption {	// Field caption
			get { return Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldCaption"); }
		}

		public string FldTitle {	// Field title
			get { return Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldTitle"); }
		}

		public string FldAlt { // Field alt
			get { return Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldAlt"); }
		}

		public string FldErrMsg {	// Field err msg
			get {
				string sFldErrMsg = Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldErrMsg");
				if (ew_Empty(sFldErrMsg))
					sFldErrMsg = FldDefaultErrMsg + " - " + FldCaption; 
				return sFldErrMsg;
			}
		}

		// Field tag caption
		public string FldTagCaption(int i) {
			return Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldTagCaption" + Convert.ToString(i));
		}

		// Reset attributes for field object
		public void ResetAttrs() {
			CssStyle = "";
			CssClass = "";
			CellCssStyle = "";
			CellCssClass = "";
			CellAttrs.Clear();
			EditAttrs.Clear();
			ViewAttrs.Clear();
			LinkAttrs.Clear();
		}		

		// View Attributes
		public string ViewAttributes {
			get {
				string sAtt = "";
				string sStyle = CssStyle;
				string sClass = CssClass;
				if (ViewAttrs.Contains("style") && ew_NotEmpty(ViewAttrs["style"]))
					sStyle += " " + ViewAttrs["style"];
				if (ViewAttrs.Contains("class") && ew_NotEmpty(ViewAttrs["class"]))
					sClass += " " + ViewAttrs["class"];
				if (ew_NotEmpty(sStyle))
					sAtt += " style=\"" + sStyle.Trim() + "\"";
				if (ew_NotEmpty(sClass))
					sAtt += " class=\"" + sClass.Trim() + "\"";
				if (ew_NotEmpty(ImageAlt))
					sAtt += " alt=\"" + ImageAlt.Trim() + "\"";
				if (ImageWidth > 0 && (!ImageResize || (ImageResize && ImageHeight <= 0)))
					sAtt += " width=\"" + ImageWidth + "\"";
				if (ImageHeight > 0 && (!ImageResize || (ImageResize && ImageWidth <= 0)))
					sAtt += " height=\"" + ImageHeight + "\"";
				foreach (DictionaryEntry Attr in ViewAttrs) {
					if (!ew_SameText(Attr.Key, "style") && !ew_SameText(Attr.Key, "class") && ew_NotEmpty(Attr.Value))
						sAtt += " " + Convert.ToString(Attr.Key) + "=\"" + Convert.ToString(Attr.Value).Trim() + "\"";
				}
				if (ew_NotEmpty(ViewCustomAttributes))
					sAtt += " " + ViewCustomAttributes.Trim();
				return sAtt;
			}
		}

		// Edit Attributes
		public string EditAttributes {
			get {
				string sAtt = "";
				string sStyle = CssStyle;
				string sClass = CssClass;
				if (EditAttrs.Contains("style") && ew_NotEmpty(EditAttrs["style"]))
					sStyle += " " + EditAttrs["style"];
				if (EditAttrs.Contains("class") && ew_NotEmpty(EditAttrs["class"]))
					sClass += " " + EditAttrs["class"];
				if (ew_NotEmpty(sStyle))
					sAtt += " style=\"" + sStyle.Trim() + "\"";
				if (ew_NotEmpty(sClass))
					sAtt += " class=\"" + sClass.Trim() + "\"";
				if (Disabled)
					EditAttrs["disabled"] = "disabled";
				if (ReadOnly) // For TEXT/PASSWORD/TEXTAREA only
					EditAttrs["readonly"] = "readonly";
				foreach (DictionaryEntry Attr in EditAttrs) {
					if (!ew_SameText(Attr.Key, "style") && !ew_SameText(Attr.Key, "class") && ew_NotEmpty(Attr.Value))
						sAtt += " " + Convert.ToString(Attr.Key) + "=\"" + Convert.ToString(Attr.Value).Trim() + "\"";
				}				
				if (ew_NotEmpty(EditCustomAttributes))
					sAtt += " " + EditCustomAttributes.Trim();				
				return sAtt;
			}
		}

		// Link attributes
		public string LinkAttributes {
			get {
				string sAtt = "";
				string sHref = Convert.ToString(HrefValue).Trim();
				foreach (DictionaryEntry Attr in LinkAttrs) {
					if (ew_NotEmpty(Attr.Value)) {
						if (ew_SameText(Attr.Key, "href"))
							sHref += " " + Convert.ToString(Attr.Value).Trim();
						else
							sAtt += " " + Convert.ToString(Attr.Key) + "=\"" + Convert.ToString(Attr.Value).Trim() + "\"";
					}
				}
				if (ew_NotEmpty(sHref))
					sAtt += " href=\"" + sHref.Trim() + "\"";
				if (ew_NotEmpty(LinkCustomAttributes))
					sAtt += " " + LinkCustomAttributes.Trim();
				return sAtt;
			}
		}	

		// Cell Styles
		public string CellStyles {
			get {
				string sAtt = "";
				string sStyle = CellCssStyle;
				string sClass = CellCssClass;
				if (CellAttrs.Contains("style") && ew_NotEmpty(CellAttrs["style"]))
					sStyle += " " + CellAttrs["style"];
				if (CellAttrs.Contains("class") && ew_NotEmpty(CellAttrs["class"]))
					sClass += " " + CellAttrs["class"];
				if (ew_NotEmpty(sStyle))
					sAtt += " style=\"" + sStyle.Trim() + "\"";
				if (ew_NotEmpty(sClass))
					sAtt += " class=\"" + sClass.Trim() + "\"";
				return sAtt;
			}
		}

		// Cell Attributes
		public string CellAttributes {
			get {
				string sAtt = CellStyles;
				foreach (DictionaryEntry Attr in CellAttrs) {
					if (!ew_SameText(Attr.Key, "style") && !ew_SameText(Attr.Key, "class") && ew_NotEmpty(Attr.Value))
						sAtt += " " + Convert.ToString(Attr.Key) + "=\"" + Convert.ToString(Attr.Value).Trim() + "\"";
				}
				if (ew_NotEmpty(CellCustomAttributes))
					sAtt += " " + CellCustomAttributes.Trim(); // Cell custom attributes
				return sAtt;
			}
		}

		// Sort Attributes
		public string Sort {
			get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + EW_TABLE_SORT + "_" + FldVar]); }
			set {
				if (ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + EW_TABLE_SORT + "_" + FldVar] != value)	{
					ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + EW_TABLE_SORT + "_" + FldVar] = value;
				}
			}
		}

		// List View value
		public string ListViewValue {
			get {
				if (ew_Empty(ViewValue)) {
					return "&nbsp;";
				} else {
					string Result = Convert.ToString(ViewValue);
					string Result2 = Regex.Replace(Result, "<[^img][^>]*>" , String.Empty); // Remove all except non-empty image tag
					return (Result2.Trim().Equals(String.Empty)) ? "&nbsp;" : Result;	
				}
			}
		}		

		// Export Caption
		public string ExportCaption {
			get {
				if (EW_EXPORT_FIELD_CAPTION) {
					return FldCaption;
				}	else {
					return FldName;
				}
			}
		}		

		// Export Value
		public string ExportValue(string Export, bool Original) {
			object DbVal = (Original) ? CurrentValue : ViewValue;
			string ExpVal = Convert.ToString(DbVal);
			if (Export == "xml" && Convert.IsDBNull(DbVal))
				ExpVal = "<Null>";
			if (Export == "pdf") {
				if (FldViewTag == "IMAGE")  {
					if (FldDataType == EW_DATATYPE_BLOB) {
						if (!Convert.IsDBNull(Upload.DbValue)) { // ASPX
							byte[] wrkdata = (byte[])Upload.DbValue;						
							if (ImageResize) {
								int wrkwidth = ImageWidth;
								int wrkheight = ImageHeight;
								ew_ResizeBinary(ref wrkdata, ref wrkwidth, ref wrkheight, ResizeQuality);
							}
							string imagefn = ew_TmpImage(wrkdata);
							if (ew_NotEmpty(imagefn))
								ExpVal = "<img src=\"" + imagefn + "\">";
						}
					} else {
						string wrkfile = Convert.ToString(Upload.DbValue);
						if (ew_Empty(wrkfile))
							wrkfile = Convert.ToString(CurrentValue);
						if (ew_NotEmpty(wrkfile)) {
							string imagefn = ew_UploadPathEx(true, UploadPath) + wrkfile;
							if (ImageResize) {
								int wrkwidth = ImageWidth;
								int wrkheight = ImageHeight;
								byte[] wrkdata = ew_ResizeFileToBinary(imagefn, ref wrkwidth, ref wrkheight, ResizeQuality);
								imagefn = ew_TmpImage(wrkdata);
							} else {
								imagefn = ew_TmpFile(imagefn);
							}
							if (ew_NotEmpty(imagefn))
								ExpVal = "<img src=\"" + imagefn + "\">";
						}
					}
				} else {
					ExpVal = ExpVal.Replace("<br>", "\r\n");
					ExpVal = ew_RemoveHtml(ExpVal);
					ExpVal = ExpVal.Replace("\r\n", "<br>");
				}
			} 
			return ExpVal;
		}

		public string FormValue {
			get { return m_FormValue; }
			set {
				m_FormValue = value;
				CurrentValue = m_FormValue;
			}
		}

		public string QueryStringValue {
			get { return m_QueryStringValue; }
			set {
				m_QueryStringValue = value;
				CurrentValue = m_QueryStringValue;
			}
		}

		public object DbValue {
			get { return m_DbValue; }
			set {
				m_DbValue = value;
				CurrentValue = m_DbValue;
			}
		}

		// Session Value
		public object SessionValue {
			get { return ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_SessionValue"]; }
			set { ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_SessionValue"] = value; }
		}

		public cAdvancedSearch AdvancedSearch {
			get {
				if (m_AdvancedSearch == null) m_AdvancedSearch = new cAdvancedSearch(); 
				return m_AdvancedSearch;
			}
		}

		public cUpload Upload {
			get {
				if (m_Upload == null)	{
					m_Upload = new cUpload(TblVar, FldVar);
				}
				return m_Upload;
			}
		}

		public string ReverseSort()
		{
			return (Sort == "ASC") ? "DESC" : "ASC";
		}

		// Advanced search
		public string UrlParameterName(string name)
		{
			string fldparm = FldVar.Substring(2);
			if (ew_SameText(name, "SearchValue")) {
				fldparm = "x_" + fldparm;
			} else if (ew_SameText(name, "SearchOperator")) {
				fldparm = "z_" + fldparm;
			} else if (ew_SameText(name, "SearchCondition")) {
				fldparm = "v_" + fldparm;
			} else if (ew_SameText(name, "SearchValue2")) {
				fldparm = "y_" + fldparm;
			} else if (ew_SameText(name, "SearchOperator2")) {
				fldparm = "w_" + fldparm;
			}
			return fldparm;
		}

		public object GetAdvancedSearch(string name)
		{
			string fldparm = UrlParameterName(name);
			return ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + EW_TABLE_ADVANCED_SEARCH + "_" + fldparm];
		}

		public void SetAdvancedSearch(string name, object v)
		{
			string fldparm = UrlParameterName(name);
			if (ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + EW_TABLE_ADVANCED_SEARCH + "_" + fldparm] != v)
				ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + EW_TABLE_ADVANCED_SEARCH + "_" + fldparm] = v;
		}

		// Set up database value
		public void SetDbValue(ref OrderedDictionary rs, object value, object def, bool skip)
		{
			bool bSkipUpdate = (skip || !Visible || Disabled);
			if (bSkipUpdate)
				return;
			switch (FldType) {
				case 2:
				case 3:
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:

					// Int
					if (Information.IsNumeric(value))	{
						m_DbValue = ew_Conv(value, FldType);
					}	else {
						m_DbValue = def;
					}
					break;
				case 5:
				case 6:
				case 14:
				case 131:

					// Double
					if (Information.IsNumeric(value))	{
						m_DbValue = ew_Conv(value, FldType);
					}	else {
						m_DbValue = def;
					}
					break;
				case 4:

					// Single
					if (Information.IsNumeric(value))	{
						m_DbValue = ew_Conv(value, FldType);
					}	else {
						m_DbValue = def;
					}
					break;
				case 7:
				case 133:
				case 134:
				case 135:

					// Date
					if (Information.IsDate(value))	{
						m_DbValue = Convert.ToDateTime(value);
					}	else	{
						m_DbValue = def;
					}
					break;
				case 145: // Time
					if (Information.IsDate(value)) {
						m_DbValue = DateTime.Parse(Convert.ToString(value)).TimeOfDay;
					} else {
						m_DbValue = def;
					}
					break;
				case 146: // DateTimeOffset
					if (Information.IsDate(value))	{
						m_DbValue = DateTimeOffset.Parse(Convert.ToString(value));
					} else {
						m_DbValue = def;
					}
					break;
				case 201:
				case 203:
				case 129:
				case 130:
				case 200:
				case 202:	// String
					if (EW_REMOVE_XSS) {
						m_DbValue = ew_RemoveXSS(Convert.ToString(value));
					} else {
						m_DbValue = Convert.ToString(value);
					} 
					if (Convert.ToString(m_DbValue) == "") m_DbValue = def; 
					break;
				case 141: // Xml
					if (ew_NotEmpty(value)) {
						m_DbValue = value;
					} else {
						m_DbValue = def;
					}
					break;
				case 128:
				case 204:
				case 205:

					// Binary
					if (Convert.IsDBNull(value))	{
						m_DbValue = def;
					}	else {
						m_DbValue = value;
					}
					break;
				case 72:

					// GUID
					if (ew_NotEmpty(value) && ew_CheckGUID(Convert.ToString(value).Trim()))	{
						m_DbValue = value;
					}	else {
						m_DbValue = def;
					}
					break;
				default:
					m_DbValue = value;
					break;
			}
			rs[FldName] = m_DbValue; 
		}

		// Show object as string
		public string AsString()
		{
			string AdvancedSearchAsString;
			string UploadAsString;
			if (m_AdvancedSearch != null)	{
				AdvancedSearchAsString = m_AdvancedSearch.AsString();
			}	else {
				AdvancedSearchAsString = "{Null}";
			}
			if (m_Upload != null)	{
				UploadAsString = m_Upload.AsString();
			}	else	{
				UploadAsString = "{Null}";
			}
			return "{" + "FldName: " + FldName + ", " + "FldVar: " + FldVar + ", " + "FldExpression: " + FldExpression + ", " + "FldType: " + FldType + ", " + "FldDateTimeFormat: " + FldDateTimeFormat + ", " + "CssStyle: " + CssStyle + ", " + "CssClass: " + CssClass + ", " + "ImageAlt: " + ImageAlt + ", " + "ImageWidth: " + ImageWidth + ", " + "ImageHeight: " + ImageHeight + ", " + "ViewCustomAttributes: " + ViewCustomAttributes + ", " + "EditCustomAttributes: " + EditCustomAttributes + ", " + "CellCssStyle: " + CellCssStyle + ", " + "CellCssClass: " + CellCssClass + ", " + "Sort: " + Sort + ", " + "MultiUpdate: " + MultiUpdate + ", " + "CurrentValue: " + CurrentValue + ", " + "ViewValue: " + ViewValue + ", " + "EditValue: " + Convert.ToString(EditValue) + ", " + "EditValue2: " + Convert.ToString(EditValue2) + ", " + "HrefValue: " + HrefValue + ", " + "HrefValue2: " + HrefValue2 + ", " + "FormValue: " + m_FormValue + ", " + "QueryStringValue: " + m_QueryStringValue + ", " + "DbValue: " + m_DbValue + ", " + "SessionValue: " + Convert.ToString(SessionValue) + ", " + "Count: " + Count + ", " + "Total: " + Total + ", " + "AdvancedSearch: " + AdvancedSearchAsString + ", " + "Upload: " + UploadAsString + "}";
		}

		// Class terminate
		public void Dispose()
		{
			if (m_AdvancedSearch != null)	{
				m_AdvancedSearch = null;
			}
			if (m_Upload != null)	{
				m_Upload = null;
			}
		}
	}

	//
	//  List option collection class
	//	
	public class cListOptions
	{

		public ArrayList Items;

		public string CustomItem = "";

		public string Tag = "td";

		public string Separator = "";

		public cListOptions()
		{
			Items = new ArrayList();
		}

		// Add and return a new option
		public cListOption Add(string Name)
		{
			cListOption item = new cListOption(Name, Tag, Separator);
			item.Parent = this;
			Items.Add(item);
			return item;
		}

		// Load default settings
		public void LoadDefault()
		{
			CustomItem = "";
			for (int i = 0; i <= Items.Count - 1; i++)
				((cListOption)Items[i]).Body = "";
		}

		// Hide all options
		public void HideAllOptions()
		{
			for (int i = 0; i <= Items.Count - 1; i++)
				((cListOption)Items[i]).Visible = false;
		}

		// Show all options
		public void ShowAllOptions()
		{
			for (int i = 0; i <= Items.Count - 1; i++)
				((cListOption)Items[i]).Visible = true;
		}

		// Get item by name (predefined names: view/edit/copy/delete/detail_<DetailTable>/userpermission/checkbox)
		public cListOption GetItem(string Name)
		{
			for (int i = 0; i <= Items.Count - 1; i++) {
				if (((cListOption)Items[i]).Name == Name)
					return (cListOption)Items[i];
			}
			return null;
		}

		// Move item to position
		public void MoveItem(string Name, int Pos)
		{
			int oldpos = 0;
			int newpos = Pos;
			bool bfound = false;
			if (Pos < 0) {
				Pos = 0;
			} else if (Pos >= Items.Count) {
				Pos = Items.Count - 1;
			}
			cListOption CurItem = GetItem(Name);
			if (CurItem != null) {
				bfound = true;
				oldpos = Items.IndexOf(CurItem);
			}	else {
				bfound = false;
			}
			if (bfound && Pos != oldpos) {
				Items.RemoveAt(oldpos); // Remove old item
				if (oldpos < Pos)
					newpos -= 1; // Adjust new position
				Items.Insert(newpos, CurItem); // Insert new item
			}
		}

		// Render list options
		public void Render(string Part)
		{
			Render(Part, "");
		}

		// Render list options
		public void Render(string Part, string Pos)
		{
			int cnt;
			cListOption opt = null;
			bool showTD = ew_NotEmpty(Pos);
			cListOption item;
			if (ew_NotEmpty(CustomItem))	{
				cnt = 0;
				for (int i = 0; i <= Items.Count - 1; i++) {
					item = (cListOption)Items[i];
					if (item.Visible && ShowPos(item.OnLeft, Pos))
						cnt += 1; 
					if (item.Name == CustomItem)
						opt = item; 
				}
				if (opt != null && cnt > 0) {
					if (ShowPos(opt.OnLeft, Pos)) {
						ew_Write(opt.Render(Part, cnt));
					} else {
						ew_Write(opt.Render("", cnt));
					}
				}
			} else {
				cnt = 1;
				for (int i = 0; i <= Items.Count - 1; i++) {
					item = (cListOption)Items[i];
					if (item.Visible && ShowPos(item.OnLeft, Pos))
						ew_Write(item.Render(Part, cnt)); 
				}
			}
		}

		private bool ShowPos(bool OnLeft, string Pos)
		{
			return (OnLeft && Pos == "left") || (!OnLeft && Pos == "right") || (Pos == "");
		}
	}

	//
	//  List option class
	//	
	public class cListOption
	{

		public string Name = "";

		public bool OnLeft = false;

		public string CssStyle = "";

		public bool Visible = true;

		public string Header = "";

		public string Body = "";

		public string Footer = "";

		public string Tag = "td";

		public string Separator = "";

		public cListOptions Parent;

		// Constructor
		public cListOption(string aName, string aTag, string aSeparator)
		{
			Name = aName;
			Tag = aTag;
			Separator = aSeparator;
		}

		// Move
		public void MoveTo(int Pos) {
			Parent.MoveItem(Name, Pos);
		}

		// Render
		public string Render(string Part, int ColSpan)
		{
			string value = "";
			if (Part == "header") {
				value = Header;
			} else if (Part == "body") {
				value = Body;
			} else if (Part == "footer") {
				value = Footer;
			} else {
				value = Part;
			}
			if (ew_Empty(value) && !ew_SameText(Tag, "td"))
				return "";
			string res = (ew_NotEmpty(value)) ? value : "&nbsp;";
			string tage = "</" + Tag + ">";
			string tags = "<" + Tag;
			tags += " class=\"aspnetmaker\"";
			if (ew_NotEmpty(CssStyle))
				tags += " style=\"" + CssStyle + "\"";
			if (ew_SameText(Tag, "td") && ColSpan > 1)
				tags += " colspan=\"" + ColSpan + "\"";			
			tags += ">";
			res = tags + res + tage + Separator;
			return res;
		}

		// Convert to string
		public string AsString()
		{
			return "{Name: " + Name + ", OnLeft: " + OnLeft + ", CssStyle: " + CssStyle + ", Visible: " + Visible + ", Header: " + ew_HtmlEncode(Header) + ", Body: " + ew_HtmlEncode(Body) + ", Footer: " + ew_HtmlEncode(Footer) + ", Tag: " + Tag + ", Separator: " + Separator + ", Parent: " + Convert.ToString(Parent) + "}";
		}
	}

	//
 	// Export document class
 	//
	public class cExportDocument : AspNetMakerBase {

		public cTable Table;

		public string Text;

		public string Line = "";

		public string Header = "";

		public string Style = "h"; // "v"(Vertical) or "h"(Horizontal)

		public bool Horizontal = true; // Horizontal

		public int FldCnt;

		// Constructor
		public cExportDocument(AspNetMakerPage APage, cTable tbl, string style) {
			m_Page = APage;
			m_ParentPage = APage.ParentPage;
			Table = tbl;
			ChangeStyle(style);
		}

		// Change style
		public void ChangeStyle(string style) {
			if (ew_SameText(style, "v") || ew_SameText(style, "h"))
				Style = style.ToLower();
			Horizontal = (Table.Export != "xml" && (Style != "v" || Table.Export == "csv"));
		}

		// Table Header
		public void ExportTableHeader() {
			switch (Table.Export) {
				case "email":
				case "html":
				case "word":
				case "excel":
					Text += "<table class=\"ewExportTable\">";
					break;
				case "pdf":
					Text += "<table cellspacing=\"0\" class=\"ewTablePdf\">\r\n";
					break;
				case "csv":
					Text += "";
					break;
			}
		}

		// Field Caption
		public void ExportCaption(cField fld) {
			ExportValueEx(ref fld, fld.ExportCaption);
		}

		// Field value
		public void ExportValue(ref cField fld) {
			ExportValueEx(ref fld, fld.ExportValue(Table.Export, Table.ExportOriginalValue));
		}

		// Field aggregate
		public void ExportAggregate(cField fld, string type) {
			if (Horizontal) {
				string val = "";
				if (ew_SameText(type, "TOTAL") || ew_SameText(type, "COUNT") || ew_SameText(type, "AVERAGE"))
					val = Language.Phrase(type) + ": " + fld.ExportValue(Table.Export, Table.ExportOriginalValue);
				ExportValueEx(ref fld, val);
			}
		}

		// Export a value (caption, field value, or aggregate)
		public void ExportValueEx(ref cField fld, string val, bool usestyle) {
			switch (Table.Export) {
				case "html":
				case "email":
				case "word":
				case "excel":
					Text += "<td" + ((usestyle && EW_EXPORT_CSS_STYLES) ? fld.CellStyles : "") + ">";
					if (Table.Export == "excel" && fld.FldDataType == EW_DATATYPE_STRING && Information.IsNumeric(val)) {
						Text += "=\"" + val + "\"";
					} else {
						Text += val;
					}
					Text += "</td>";
					break;
				case "csv":
					if (ew_NotEmpty(Line))
						Line += ",";
					Line += "\"" + val.Replace("\"", "\"\"") + "\"";
					break;
				case "pdf":
					Text += "<td" + ((usestyle && EW_EXPORT_CSS_STYLES) ? fld.CellStyles : "") + ">" + val + "</td>\r\n";
					break;
			}
		}

		// Export a value (caption, field value, or aggregate)
		public void ExportValueEx(ref cField fld, string val) {
			ExportValueEx(ref fld, val, true);	
		}

		// Begin a row
		public void BeginExportRow() {
			BeginExportRow(0, true);	
		}

		// Begin a row
		public void BeginExportRow(int rowcnt) {
			BeginExportRow(rowcnt, true);	
		}		

		// Begin a row
		public void BeginExportRow(int rowcnt, bool usestyle) {
			FldCnt = 0;
			if (Horizontal) {
				switch (Table.Export) {
					case "html":
					case "email":
					case "word":
					case "excel":
						if (rowcnt == -1) {
							Table.CssClass = "ewExportTableFooter";
						} else if (rowcnt == 0) {
							Table.CssClass = "ewExportTableHeader";
						} else {
							Table.CssClass = ((rowcnt % 2) == 1) ? "ewExportTableRow" : "ewExportTableAltRow";
						}
						Text += "<tr" + ((usestyle && EW_EXPORT_CSS_STYLES) ? Table.RowStyles : "") + ">";
						break;
					case "csv":
						Line = "";
						break;
					case "pdf":
						if (rowcnt == -1) {
							Table.CssClass = "ewTableFooter";
						} else if (rowcnt == 0) {
							Table.CssClass = "ewTablePdfHeader";
						} else {
							Table.CssClass = ((rowcnt % 2) == 1) ? "ewTableRow" : "ewTableAltRow";
						}
						Line = "<tr" + ((usestyle && EW_EXPORT_CSS_STYLES) ? Table.RowStyles : "") + ">";
						Text += Line;
						break;
				}
			}
		}

		// End a row
		public void EndExportRow(bool hdr) {
			if (Horizontal) {
				switch (Table.Export) {
					case "html":
					case "email":
					case "word":
					case "excel":
						Text += "</tr>";
						break;
					case "csv":
						Line += "\r\n";
						Text += Line;
						break;
					case "pdf":
						Line += "</tr>";
						Text += "</tr>";
						if (hdr)
							Header = Line;
						break;
				}
			}
		}

		// Page break
		public void ExportPageBreak() {
			if (Horizontal) {
				switch (Table.Export) {
					case "pdf":
						Text += "</table>\r\n"; // End current table
						Text += "<p class=\"ewPageBreak\"/>\r\n"; // Page break // ASPX
						Text += "<table cellspacing=\"0\" class=\"ewTablePdf\">\r\n"; // New page header // ASPX
						break;
				}
			}
		}

		// Empty line
		public void ExportEmptyLine() {
			switch (Table.Export) {
				case "html":
				case "email":
				case "word":
				case "excel":
				case "pdf":
					Text += "<br>&nbsp;";
					break;
			}
		}

		// Export a field
		public void ExportField(cField fld) {
			if (Horizontal) {
				ExportValue(ref fld);
			} else { // Vertical, export as a row
				FldCnt++;
				string tdcaption = "<td";
				switch (Table.Export) {
					case "html":
					case "email":
					case "word":
					case "excel":
						tdcaption += " class=\"ewTableHeader\"";
						break;
					case "pdf":
						tdcaption += " class=\"ewTablePdfHeader\"";
						break;
				}
				tdcaption += ">";
				fld.CellCssClass = ((FldCnt % 2) == 1) ? "ewExportTableRow" : "ewTableAltRow";
				string tdvalue = "<td" + ((EW_EXPORT_CSS_STYLES) ? fld.CellStyles : "") + ">";
				Text += "<tr>" + tdcaption + fld.ExportCaption + "</td>" + tdvalue +
					fld.ExportValue(Table.Export, Table.ExportOriginalValue) +
					"</td></tr>";
			}
		}

		// Table Footer
		public void ExportTableFooter() {
			switch (Table.Export) {
				case "html":
				case "email":
				case "word":
				case "excel":
				case "pdf":
					Text += "</table>";
					break;
			}
		}

		// Export header and footer
		public void ExportHeaderAndFooter() {
			string Charset = (ew_NotEmpty(EW_CHARSET)) ? ";charset=" + EW_CHARSET : ""; // Charset used in header
			string cssfile = (Table.Export == "pdf") ? EW_PDF_STYLESHEET_FILENAME : EW_PROJECT_STYLESHEET_FILENAME;
			switch (Table.Export) {
				case "email":
				case "html":
				case "excel":
				case "word":
				case "pdf":
					string header = "<html><head>\r\n";
					if (ew_SameText(EW_CHARSET, "utf-8")) header += "<meta charset=\"utf-8\">\r\n";
					if (EW_EXPORT_CSS_STYLES && ew_NotEmpty(cssfile))
						header += "<style" + ((Table.Export == "pdf") ? " title=\"ewExportPdf\"" : "") + ">" + ew_LoadTxt(cssfile) + "</style>\r\n";
					header += "</" + "head>\r\n<body>\r\n";
					Text = header + Text + "</body></html>";
					break;
			}
		}
	}

	//
	// CSS parser
	//
	public class cCssParser
	{

		public Dictionary<string, Dictionary<string, string>> css;

		// Constructor
		public cCssParser()
		{
			css = new Dictionary<string, Dictionary<string, string>>();
		}

		// Clear all styles
		public void Clear()
		{
			foreach (KeyValuePair<string, Dictionary<string, string>> kvp in css)
				kvp.Value.Clear();
			css.Clear();
		}

		// add a section
		public void Add(string key, string codestr)
		{
			key = key.ToLower().Trim();
			if (key == "")
				return;
			codestr = codestr.ToLower();
			if (!css.ContainsKey(key))
				css[key] = new Dictionary<string, string>();
			string[] codes = codestr.Split(new char[] { ';' });
			if (codes.Length > 0)
			{
				foreach (string code in codes)
				{
					string sCode = code.ToLower();
					string[] arCode = code.Split(new char[] { ':' });
					string codekey = arCode[0];
					string codevalue = "";
					if (arCode.Length > 1)
						codevalue = arCode[1];
					if (codekey.Length > 0)
					{
						css[key][codekey.Trim()] = codevalue.Trim();
					}
				}
			}
		}

		// explode a string into two
		private void Explode(string str, char sep, ref string str1, ref string str2)
		{
			string[] ar = str.Split(new char[] { sep });
			str1 = ar[0];
			if (ar.Length > 1)
				str2 = ar[1];
		}

		// Get a style
		public string Get(string key, string property)
		{
			key = key.ToLower();

			property = property.ToLower();
			string tag = "", subtag = "", cls = "", id = "";
			Explode(key, ':', ref tag, ref subtag);
			Explode(tag, '.', ref tag, ref cls);
			Explode(tag, '#', ref tag, ref id);
			string result = "";
			foreach (KeyValuePair<string, Dictionary<string, string>> kvp in css)
			{
				string _tag = kvp.Key;
				Dictionary<string, string> value = kvp.Value;
				string _subtag = "", _cls = "", _id = "";
				Explode(_tag, ':', ref _tag, ref _subtag);
				Explode(_tag, '.', ref _tag, ref _cls);
				Explode(_tag, '#', ref _tag, ref _id);
				bool tagmatch = (tag == _tag || _tag.Length == 0);
				bool subtagmatch = (subtag == _subtag || _subtag.Length == 0);
				bool classmatch = (cls == _cls || _cls.Length == 0);
				bool idmatch = (id == _id);
				if (tagmatch && subtagmatch && classmatch && idmatch)
				{
					string temp = _tag;
					if (temp.Length > 0 && _cls.Length > 0)
					{
						temp += "." + _cls;
					}
					else if (temp.Length == 0)
					{
						temp = "." + _cls;
					}
					if (temp.Length > 0 && _subtag.Length > 0)
					{
						temp += ":" + _subtag;
					}
					else if (temp.Length == 0)
					{
						temp = ":" + _subtag;
					}
					if (css[temp].ContainsKey(property))
						result = css[temp][property];
				}
			}
			return result;
		}

		// Get section as dictionary
		public Dictionary<string, string> GetSection(string key)
		{
			key = key.ToLower();
			string tag = "", subtag = "", cls = "", id = "";
			Explode(key, ':', ref tag, ref subtag);
			Explode(tag, '.', ref tag, ref cls);
			Explode(tag, '#', ref tag, ref id);
			Dictionary<string, string> result = new Dictionary<string, string>();
			foreach (KeyValuePair<string, Dictionary<string, string>> kvp in css)
			{
				string _tag = kvp.Key;
				Dictionary<string, string> value = kvp.Value;
				string _subtag = "", _cls = "", _id = "";
				Explode(_tag, ':', ref _tag, ref _subtag);
				Explode(_tag, '.', ref _tag, ref _cls);
				Explode(_tag, '#', ref _tag, ref _id);
				bool tagmatch = (tag == _tag || _tag.Length == 0);
				bool subtagmatch = (subtag == _subtag || _subtag.Length == 0);
				bool classmatch = (cls == _cls || _cls.Length == 0);
				bool idmatch = (id == _id);
				if (tagmatch && subtagmatch && classmatch && idmatch)
				{
					string temp = _tag;
					if (temp.Length > 0 && _cls.Length > 0)
					{
						temp += "." + _cls;
					}
					else if (temp.Length == 0)
					{
						temp = "." + _cls;
					}
					if (temp.Length > 0 && _subtag.Length > 0)
					{
						temp += ":" + _subtag;
					}
					else if (temp.Length == 0)
					{
						temp = ":" + _subtag;
					}
					foreach (KeyValuePair<string, string> kv in css[temp])
					{
						result[kv.Key] = kv.Value;
					}
				}
			}
			return result;
		}

		// Get section as string
		public string GetSectionString(string key)
		{
			Dictionary<string, string> dict = GetSection(key);
			string result = "";
			foreach (KeyValuePair<string, string> kv in dict)
				result += kv.Key + ":" + kv.Value + ";"; // no spaces
			return result;
		}

		// Parse string
		public bool ParseStr(string str)
		{
			Clear();

			// Remove comments
			str = Regex.Replace(str, @"\/\*(.*)?\*\/", "");

			// Parse the csscode
			string[] parts = str.Split(new char[] { '}' });
			if (parts.Length > 0)
			{
				foreach (string part in parts)
				{
					string keystr = "", codestr = "";
					Explode(part, '{', ref keystr, ref codestr);
					string[] keys = keystr.Split(new char[] { ',' });
					if (keys.Length > 0)
					{
						foreach (string akey in keys)
						{
							string key = akey;
							if (key.Length > 0)
							{
								key = key.Replace("\n", "");
								key = key.Replace("\\", "");
								Add(key, codestr.Trim());
							}
						}
					}
				}
			}
			return (css.Count > 0);
		}

		// Parse a stylesheet
		public bool ParseFile(string filename)
		{
			Clear();
			if (File.Exists(filename))
			{
				return ParseStr(File.ReadAllText(filename));
			}
			else
			{
				return false;
			}
		}

		// Get CSS string
		public string GetCSS()
		{
			string result = "";
			foreach (KeyValuePair<string, Dictionary<string, string>> kvp in css)
			{
				result += kvp.Key + " {\n";
				foreach (KeyValuePair<string, string> kv in kvp.Value)
				{
					result += "  " + kv.Key + ": " + kv.Value + ";\n";
				}
				result += "}\n\n";
			}
			return result;
		}
	}

	//
	// Connection object
	//
	public class cConnection : IDisposable
	{

		public string ConnectionString = EW_DB_CONNECTION_STRING;

		public SqlConnection Conn;

		public SqlTransaction Trans;

		private SqlConnection TempConn; 

		private SqlCommand TempCommand; 

		private SqlDataReader TempDataReader; 

		// Constructor
		public cConnection(string ConnStr)
		{
			ConnectionString = ConnStr;
			Database_Connecting(ref ConnectionString); 
			Conn = new SqlConnection(ConnectionString);			
			Conn.Open();
			Database_Connected();
		}

		// Constructor
		public cConnection() : this(EW_DB_CONNECTION_STRING)
		{			
		}

		// Execute SQL
		public int Execute(string Sql)
		{
			SqlCommand Cmd = GetCommand(Sql);
			return Cmd.ExecuteNonQuery();			
		}

		// Execute SQL and return first value of first row
		public object ExecuteScalar(string Sql)
		{
			SqlCommand Cmd = GetCommand(Sql);
			return Cmd.ExecuteScalar();
		}

		// Get last insert ID
		public object GetLastInsertId()
		{
			object Id = System.DBNull.Value;
			Id = ExecuteScalar("SELECT @@Identity");			
			return Id;
		}

		// Get data reader
		public SqlDataReader GetDataReader(string Sql)
		{
			try {
				SqlCommand Cmd = new SqlCommand();
				Cmd = GetCommand(Sql);
				return Cmd.ExecuteReader();
			}	catch {
				if (EW_DEBUG_ENABLED) throw; 
				return null;
			}
		}

		// Get temporary data reader
		public SqlDataReader GetTempDataReader(string Sql)
		{ 
			try {
				if (TempConn == null) {
					TempConn = new SqlConnection(ConnectionString);
					TempConn.Open();
				}
				if (TempCommand == null)
					TempCommand = new SqlCommand(Sql, TempConn);
				CloseTempDataReader();
				TempCommand.CommandText = Sql;
				TempDataReader = TempCommand.ExecuteReader();			
				return TempDataReader;
			} catch {
				if (EW_DEBUG_ENABLED) throw; 
				return null;
			}
		}

		// Close temporary data reader
		public void CloseTempDataReader()
		{
			if (TempDataReader != null)	{
				TempDataReader.Close();
				TempDataReader.Dispose();
			}			
		}

		// Get OrderedDictionary from data reader
		public OrderedDictionary GetRow(ref SqlDataReader dr)
		{
			if (dr != null) {
				OrderedDictionary od = new OrderedDictionary();
				for (int i = 0; i <= dr.FieldCount - 1; i++) {
					try {
						if (ew_NotEmpty(dr.GetName(i))) {
							od[dr.GetName(i)] = dr[i];
						} else {
							od[i] = dr[i];
						}
					} catch {}
				}
				return od;
			}
			return null;
		}

		// Get rows
		public ArrayList GetRows(ref SqlDataReader dr)
		{
			if (dr != null) {		
				ArrayList Rows = new ArrayList();
				while (dr.Read()) {
					Rows.Add(GetRow(ref dr));
				}
				return Rows;
			}
			return null;
		}

		// Get rows by SQL
		public ArrayList GetRows(string Sql)
		{
			SqlDataReader dr = GetTempDataReader(Sql);
			try {
				return GetRows(ref dr);			
			} finally {
				CloseTempDataReader();
			}
		}

		// Get dataset
		public DataSet GetDataSet(string Sql)
		{
			try {
				SqlDataAdapter Adapter = new SqlDataAdapter(Sql, Conn);
				DataSet DS = new DataSet();
				Adapter.Fill(DS);
				return DS;
			}	catch {
				if (EW_DEBUG_ENABLED) throw; 
				return null;
			}
		}

		// Get count (by dataset)
		public int GetCount(string Sql)
		{
			DataSet DS = GetDataSet(Sql);
			if (DS != null) {
				return DS.Tables[0].Rows.Count;
			} else {
				return 0;
			}
		}

		// Get command
		public SqlCommand GetCommand(string Sql)
		{
			SqlCommand Cmd = new SqlCommand(Sql, Conn);
			if (Trans != null) Cmd.Transaction = Trans; 
			return Cmd;
		}

		// Begin transaction
		public void BeginTrans()
		{
			try {
				Trans = Conn.BeginTransaction();
			}	catch {
				if (EW_DEBUG_ENABLED) throw; 
			}
		}

		// Commit transaction
		public void CommitTrans()
		{
			if (Trans != null) Trans.Commit(); 
		}

		// Rollback transaction
		public void RollbackTrans()
		{
			if (Trans != null) Trans.Rollback(); 
		}

		// Dispose
		public void Dispose()
		{
			if (Trans != null) Trans.Dispose(); 
			Conn.Close();
			Conn.Dispose();
			if (TempCommand != null) TempCommand.Dispose();
			if (TempConn != null) {
				TempConn.Close();
				TempConn.Dispose();
			}
		}

		// Database Connecting event
		public void Database_Connecting(ref string Connstr) {

			//HttpContext.Current.Response.Write("Database Connecting");
		}

		// Database Connected event
		public void Database_Connected() {

			//Execute("Your SQL");
		}
	}		

	//
	//  Advanced Search class
	//
	public class cAdvancedSearch
	{

		public object SearchValue = null; // Search value		

		public string SearchOperator = "="; // Search operator		

		public string SearchCondition = "AND"; // Search condition		

		public object SearchValue2 = null; // Search value 2		

		public string SearchOperator2 = "="; // Search operator 2

		// Show object as string
		public string AsString()
		{
			return "{" + "SearchValue: " + SearchValue + ", " + "SearchOperator: " + SearchOperator + ", " + "SearchCondition: " + SearchCondition + ", " + "SearchValue2: " + SearchValue2 + ", " + "SearchOperator2: " + SearchOperator2 + "}";
		}
	}

	//
	//  Upload class
	//
	public class cUpload
	{

		public int Index = 0; // Index to handle multiple form elements

		public string TblVar; // Table variable

		public string FldVar; // Field variable

		public object DbValue = System.DBNull.Value; // Value from database // ASPX

		private string m_Message = ""; // Error message

		private object m_Value; // Upload value

		private string m_Action = ""; // Upload action

		private string m_FileName = ""; // Upload file name

		private long m_FileSize; // Upload file size

		private string m_ContentType = ""; // File content type

		private int m_ImageWidth = -1; // Image width

		private int m_ImageHeight = -1; // Image height

		private cFormObj m_FormObj; // Page form object

		// Contructor
		public cUpload(string ATblVar, string AFldVar)
		{
			TblVar = ATblVar;
			FldVar = AFldVar;
		}

		// Form object
		public cFormObj ObjForm {
			get { return m_FormObj; }
		}

		public object Message {
			get { return m_Message; }
		}

		public object Value {
			get { return m_Value; }
			set { m_Value = value; }
		}

		public string Action {
			get { return m_Action; }
		}

		public string FileName {
			get { return m_FileName; }
		}

		public long FileSize {
			get { return m_FileSize; }
		}

		public string ContentType {
			get { return m_ContentType; }
		}

		public int ImageWidth {
			get { return m_ImageWidth; }
		}

		public int ImageHeight {
			get { return m_ImageHeight; }
		}

		// Set form object
		public void SetForm(cFormObj Obj)
		{
			m_FormObj = Obj;
			Index = Obj.Index;
		}

		// Save Db value to Session
		public void SaveDbToSession()
		{
			string sSessionID = EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_" + Index;
			ew_Session[sSessionID + "_DbValue"] = DbValue;
		}

		// Restore Db value from Session
		public void RestoreDbFromSession()
		{
			string sSessionID = EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_" + Index;
			DbValue = ew_Session[sSessionID + "_DbValue"];
			if (DbValue == null) DbValue = System.DBNull.Value; // ASPX 
		}

		// Remove Db value from Session
		public void RemoveDbFromSession()
		{
			string sSessionID = EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_" + Index;
			HttpContext.Current.Session.Contents.Remove(sSessionID + "_DbValue");
		}

		// Save Upload values to Session
		public void SaveToSession()
		{
			string sSessionID = EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_" + Index;
			ew_Session[sSessionID + "_Action"] = m_Action;
			ew_Session[sSessionID + "_FileSize"] = m_FileSize;
			ew_Session[sSessionID + "_FileName"] = m_FileName;
			ew_Session[sSessionID + "_ContentType"] = m_ContentType;
			ew_Session[sSessionID + "_ImageWidth"] = m_ImageWidth;
			ew_Session[sSessionID + "_ImageHeight"] = m_ImageHeight;
			ew_Session[sSessionID + "_Value"] = m_Value;
		}

		// Restore Upload values from Session
		public void RestoreFromSession()
		{
			string sSessionID = EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_" + Index;
			m_Action = Convert.ToString(ew_Session[sSessionID + "_Action"]);
			m_FileSize = Convert.ToInt64(ew_Session[sSessionID + "_FileSize"]);
			m_FileName = Convert.ToString(ew_Session[sSessionID + "_FileName"]);
			m_ContentType = Convert.ToString(ew_Session[sSessionID + "_ContentType"]);
			m_ImageWidth = Convert.ToInt32(ew_Session[sSessionID + "_ImageWidth"]);
			m_ImageHeight = Convert.ToInt32(ew_Session[sSessionID + "_ImageHeight"]);
			m_Value = ew_Session[sSessionID + "_Value"];
		}

		// Remove Upload values from Session
		public void RemoveFromSession()
		{
			string sSessionID = EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_" + Index;
			HttpContext.Current.Session.Contents.Remove(sSessionID + "_Action");
			HttpContext.Current.Session.Contents.Remove(sSessionID + "_FileSize");
			HttpContext.Current.Session.Contents.Remove(sSessionID + "_FileName");
			HttpContext.Current.Session.Contents.Remove(sSessionID + "_ContentType");
			HttpContext.Current.Session.Contents.Remove(sSessionID + "_ImageWidth");
			HttpContext.Current.Session.Contents.Remove(sSessionID + "_ImageHeight");
			HttpContext.Current.Session.Contents.Remove(sSessionID + "_Value");
			RemoveDbFromSession();
		}

		// Check the file type of the uploaded file
		private bool UploadAllowedFileExt(string FileName)
		{
			return ew_CheckFileType(FileName);
		}

		// Get upload file
		public bool UploadFile()
		{
			try {
				string gsFldVar = FldVar;
				string gsFldVarAction = "a" + gsFldVar.Substring(1);

				// Initialize upload value
				m_Value = System.DBNull.Value;

				// Get action
				m_Action = ObjForm.GetValue(gsFldVarAction);

				// Get and check the upload file size
				m_FileSize = ObjForm.GetUploadFileSize(gsFldVar);

				// Get and check the upload file type
				m_FileName = ObjForm.GetUploadFileName(gsFldVar);

				// Get upload file content type
				m_ContentType = ObjForm.GetUploadFileContentType(gsFldVar);

				// Get upload value
				m_Value = ObjForm.GetUploadFileData(gsFldVar); // ASPX

				// Get image width and height
				m_ImageWidth = ObjForm.GetUploadImageWidth(gsFldVar);
				m_ImageHeight = ObjForm.GetUploadImageHeight(gsFldVar);
				return true;
			}	catch {
				return false;
			}
		}

		// Resize image
		public bool Resize(int Width, int Height, int Interpolation)
		{
			bool result = false;
			int wrkWidth;
			int wrkHeight;
			if (!Convert.IsDBNull(m_Value))	{
				wrkWidth = Width;
				wrkHeight = Height;
				byte[] data = (byte[])m_Value;
				result = ew_ResizeBinary(ref data, ref wrkWidth, ref wrkHeight, Interpolation);
				if (result) {
					m_Value = data;
					m_ImageWidth = wrkWidth;
					m_ImageHeight = wrkHeight;
					m_FileSize = data.Length;
				}
			}
			return result;
		}

		// Save uploaded data to file (Path relative to application root)
		public bool SaveToFile(string Path, string NewFileName, bool Overwrite)
		{
			if (!Convert.IsDBNull(m_Value))	{
				Path = ew_UploadPathEx(true, Path);
				if (ew_Empty(NewFileName)) NewFileName = m_FileName; 
				byte[] data = (byte[])m_Value;
				if (Overwrite) {
					return ew_SaveFile(Path, NewFileName, ref data);
				}	else {
					return ew_SaveFile(Path, ew_UploadFileNameEx(Path, NewFileName), ref data);
				}
			}
			return false;
		}

		// Resize and save uploaded data to file (Path relative to application root)
		public bool ResizeAndSaveToFile(int Width, int Height, int Interpolation, string Path, string NewFileName, bool Overwrite)
		{
			if (!Convert.IsDBNull(m_Value))	{
				object OldValue = m_Value;

				// Save old values
				int OldWidth = m_ImageWidth;
				int OldHeight = m_ImageHeight;
				long OldFileSize = m_FileSize;
				try {
					Resize(Width, Height, Interpolation);
					return SaveToFile(Path, NewFileName, Overwrite);
				}	finally {
					m_Value = OldValue;

					// Restore old values
					m_ImageWidth = OldWidth;
					m_ImageHeight = OldHeight;
					m_FileSize = OldFileSize;
				}
			}
			return false;
		}

		// Show object as string
		public string AsString()
		{
			return "{Index: " + Index + ", Message: " + m_Message + ", Action: " + m_Action + ", FileName: " + m_FileName + ", FileSize: " + m_FileSize + ", ContentType: " + m_ContentType + ", ImageWidth: " + m_ImageWidth + ", ImageHeight: " + m_ImageHeight + "}";
		}
	}

	//
	// Advanced Security class
	//
	public class cAdvancedSecurity : AspNetMakerBase
	{

		private ArrayList m_ArUserLevel;

		private ArrayList m_ArUserLevelPriv;

		private int[] m_ArUserLevelID;

		// Current User Level ID / User Level
		public int CurrentUserLevelID;

		public int CurrentUserLevel;

		// Current User ID / Parent User ID / User ID array
		public object CurrentUserID;

		public object CurrentParentUserID;

		private object[] m_ArUserID;

		private cUsuarios UserTable;

		// Init
		public cAdvancedSecurity(AspNetMakerBase APage) {

			//m_Page = APage;
			m_ParentPage = APage.ParentPage;
			m_ArUserLevel = new ArrayList();
			m_ArUserLevelPriv = new ArrayList();

			// Init User Level
			CurrentUserLevelID = SessionUserLevelID;
			if (Information.IsNumeric(CurrentUserLevelID)) {
				if (CurrentUserLevelID >= -1)	{
					Array.Resize(ref m_ArUserLevelID, 1);
					m_ArUserLevelID[0] = CurrentUserLevelID;
				}
			}

			// Init User ID
			CurrentUserID = SessionUserID;
			CurrentParentUserID = SessionParentUserID;
			if (Usuarios != null) {
				UserTable = Usuarios;
			} else {
				UserTable = new cUsuarios(null); // ASPX81
			}

			// Load user level (for TablePermission_Loading event)
			LoadUserLevel();
		}		

		// User table
		public cUsuarios Usuarios { 
			get {	return ParentPage.Usuarios;	}
		}

		// Session User ID
		public object SessionUserID {
			get { return Convert.ToString(ew_Session[EW_SESSION_USER_ID]); }
			set {
				ew_Session[EW_SESSION_USER_ID] = Convert.ToString(value).Trim();
				CurrentUserID = Convert.ToString(value).Trim();
			}
		}

		// Session parent User ID
		public object SessionParentUserID {
			get { return Convert.ToString(ew_Session[EW_SESSION_PARENT_USER_ID]); }
			set {
				ew_Session[EW_SESSION_PARENT_USER_ID] = Convert.ToString(value).Trim();
				CurrentParentUserID = Convert.ToString(value).Trim();
			}
		}

		// Current user name
		public string CurrentUserName {
			get { return Convert.ToString(ew_Session[EW_SESSION_USER_NAME]); }
			set { ew_Session[EW_SESSION_USER_NAME] = value; }
		}

		// Session User Level ID		
		public int SessionUserLevelID {
			get { return Convert.ToInt32(ew_Session[EW_SESSION_USER_LEVEL_ID]); }
			set {
				ew_Session[EW_SESSION_USER_LEVEL_ID] = value;
				CurrentUserLevelID = value;
				if (Information.IsNumeric(CurrentUserLevelID)) {
					if (CurrentUserLevelID >= -1) {					
						Array.Resize(ref m_ArUserLevelID, 1);
						m_ArUserLevelID[0] = CurrentUserLevelID;
					}
				}
			}
		}

		// Session User Level value	
		public int SessionUserLevel {
			get { return Convert.ToInt32(ew_Session[EW_SESSION_USER_LEVEL]); }
			set {
				ew_Session[EW_SESSION_USER_LEVEL] = value;
				CurrentUserLevel = value;
				if (Information.IsNumeric(CurrentUserLevelID)) {
					if (CurrentUserLevelID >= -1) {
						Array.Resize(ref m_ArUserLevelID, 1);
						m_ArUserLevelID[0] = CurrentUserLevelID;
					}
				}
			}
		}

		// Can add		
		public bool CanAdd {
			get { return ((CurrentUserLevel & EW_ALLOW_ADD) == EW_ALLOW_ADD); }
			set {
				if (value) {
					CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_ADD);
				}	else	{
					CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_ADD));
				}
			}
		}

		// Can delete	
		public bool CanDelete {
			get { return ((CurrentUserLevel & EW_ALLOW_DELETE) == EW_ALLOW_DELETE); }
			set {
				if (value)	{
					CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_DELETE);
				}	else	{
					CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_DELETE));
				}
			}
		}

		// Can edit
		public bool CanEdit {
			get { return ((CurrentUserLevel & EW_ALLOW_EDIT) == EW_ALLOW_EDIT); }
			set {
				if (value)	{
					CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_EDIT);
				}	else	{
					CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_EDIT));
				}
			}
		}

		// Can view
		public bool CanView {
			get { return ((CurrentUserLevel & EW_ALLOW_VIEW) == EW_ALLOW_VIEW); }
			set {
				if (value) {
					CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_VIEW);
				}	else	{
					CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_VIEW));
				}
			}
		}

		// Can list
		public bool CanList {
			get { return ((CurrentUserLevel & EW_ALLOW_LIST) == EW_ALLOW_LIST); }
			set {
				if (value)	{
					CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_LIST);
				}	else	{
					CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_LIST));
				}
			}
		}

		// Can report		
		public bool CanReport {
			get { return ((CurrentUserLevel & EW_ALLOW_REPORT) == EW_ALLOW_REPORT); }
			set {
				if (value) {
					CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_REPORT);
				}	else	{
					CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_REPORT));
				}
			}
		}

		// Can search		
		public bool CanSearch {
			get { return ((CurrentUserLevel & EW_ALLOW_SEARCH) == EW_ALLOW_SEARCH); }
			set {
				if (value) {
					CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_SEARCH);
				}	else	{
					CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_SEARCH));
				}
			}
		}

		// Can admin		
		public bool CanAdmin {
			get { return ((CurrentUserLevel & EW_ALLOW_ADMIN) == EW_ALLOW_ADMIN); }
			set {
				if (value)	{
					CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_ADMIN);
				}	else	{
					CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_ADMIN));
				}
			}
		}

		// Last URL
		public string LastUrl {
			get { return ew_Cookie["lasturl"]; }
		}

		// Save last URL
		public void SaveLastUrl()
		{
			string s = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
			string q = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
			if (ew_NotEmpty(q)) s = s + "?" + q; 
			if (LastUrl == s) s = ""; 
			ew_Cookie["lasturl"] = s;
		}

		// Auto login
		public bool AutoLogin()
		{
			if (ew_SameStr(ew_Cookie["autologin"], "autologin"))	{
				string sUsr = ew_Cookie["username"];
				string sPwd = ew_Cookie["password"];
				sUsr = cTEA.Decrypt(sUsr, EW_RANDOM_KEY);
				sPwd = cTEA.Decrypt(sPwd, EW_RANDOM_KEY);
				bool bValid = ValidateUser(sUsr, sPwd, true);
				if (bValid) ew_WriteAuditTrailOnLogInOut(Language.Phrase("AuditTrailAutoLogin"), sUsr);
				return bValid;
			}	else	{
				return false;
			}
		}	

		// Validate user
		public bool ValidateUser(string usr, string pwd, bool autologin)
		{
			bool result = false;
			string sFilter;
			string sSql;
			SqlDataReader RsUser;
			result = false;

		// Call User Custom Validate event
		if (EW_USE_CUSTOM_LOGIN) {
			result = User_CustomValidate(ref usr, ref pwd);
			if (result)
				ew_Session[EW_SESSION_STATUS] = "login";
		}
			if (!result) {	
				if (EW_CASE_SENSITIVE_PASSWORD)	{
					result = (EW_ADMIN_USER_NAME == usr & EW_ADMIN_PASSWORD == pwd);
				}	else {
					result = (ew_SameText(EW_ADMIN_USER_NAME, usr) & ew_SameText(EW_ADMIN_PASSWORD, pwd));
				}
				if (result)	{
					ew_Session[EW_SESSION_STATUS] = "login";
					ew_Session[EW_SESSION_SYS_ADMIN] = 1;	// System Administrator
					CurrentUserName = usr; // Load user name
					SessionUserID = -1; // System Administrator
					SessionUserLevelID = -1; // System Administrator
					SetUpUserLevel();
				}
			}

			// Check other users
			if (!result) {
				sFilter = EW_USER_NAME_FILTER.Replace("%u", ew_AdjustSql(usr)); 

				// Get SQL from GetSql function in <UserTable> class, <UserTable>info.aspx
				sSql = UserTable.GetSQL(sFilter, ""); // ASPX81
				RsUser = Conn.GetDataReader(sSql);
				if (RsUser.Read()) {
					result = ew_ComparePassword(Convert.ToString(RsUser["Password"]), pwd);					
					if (result) {
						ew_Session[EW_SESSION_STATUS] = "login";
						ew_Session[EW_SESSION_SYS_ADMIN] = 0; // Non System Administrator
						CurrentUserName = Convert.ToString(RsUser["Usuario"]); // Load user name
						SessionUserID = RsUser["IdUsuario"]; // Load user id
						SessionUserLevelID = ew_ConvertToInt(RsUser["IdUsuarioNivel"]); // Load user level
						SetUpUserLevel();

						// User Validated event							
						User_Validated(RsUser);
					}
				}
				RsUser.Close();
				RsUser.Dispose();
			}
			if (!result)
				ew_Session[EW_SESSION_STATUS] = ""; // Clear login status 
			return result;
		}

		// Dynamic user level security
		// Get current user level settings from database	
		public void SetUpUserLevel() {
			SetUpUserLevelEx(); // Load all user levels

			// UserLevel_Loaded event
			UserLevel_Loaded();

			// Save the user level to session variable
			SaveUserLevel();
		}

		// Sub to get (all) user level settings from database
		public void SetUpUserLevelEx()
		{
			string sSql;

			// Get the user level definitions
			sSql = "SELECT " + EW_USER_LEVEL_ID_FIELD + ", " + EW_USER_LEVEL_NAME_FIELD + " FROM " + EW_USER_LEVEL_TABLE;
			m_ArUserLevel = Conn.GetRows(sSql);

			// Get the user level privileges
			sSql = "SELECT " + EW_USER_LEVEL_PRIV_TABLE_NAME_FIELD + ", " + EW_USER_LEVEL_PRIV_USER_LEVEL_ID_FIELD + ", " + EW_USER_LEVEL_PRIV_PRIV_FIELD + " FROM " + EW_USER_LEVEL_PRIV_TABLE;
			m_ArUserLevelPriv = Conn.GetRows(sSql);
		}

		// Add user permission
		public void AddUserPermission(string UserLevelName, string TableName, int UserPermission)
		{
			string UserLevelID = "";

			// Get user level ID from user name
			if (ew_IsArrayList(m_ArUserLevel)) {
				foreach (OrderedDictionary Row in m_ArUserLevel) {
					if (ew_SameStr(UserLevelName, Row[1])) {
						UserLevelID = Convert.ToString(Row[0]);
						break;
					}
				}
			}
			if (ew_IsArrayList(m_ArUserLevelPriv) && ew_NotEmpty(UserLevelID)) {
				foreach (OrderedDictionary Row in m_ArUserLevelPriv) {
					if (ew_SameStr(Row[0], TableName) && ew_SameStr(Row[1], UserLevelID)) {
						Row[2] = ew_ConvertToInt(Row[2]) | UserPermission;	// Add permission
						break;
					}
				}
			}
		}

		// Delete user permission
		public void DeleteUserPermission(string UserLevelName, string TableName, int UserPermission)
		{
			string UserLevelID = "";

			// Get user level ID from user name
			if (ew_IsArrayList(m_ArUserLevel)) {
				foreach (OrderedDictionary Row in m_ArUserLevel) {
					if (ew_SameStr(UserLevelName, Row[1]))	{
						UserLevelID = Convert.ToString(Row[0]);
						break;
					}
				}
			}
			if (ew_IsArrayList(m_ArUserLevelPriv) && ew_NotEmpty(UserLevelID)) {
				foreach (OrderedDictionary Row in m_ArUserLevelPriv) {
					if (ew_SameStr(Row[0], TableName) && ew_SameStr(Row[1], UserLevelID))	{
						Row[2] = ew_ConvertToInt(Row[2]) & (127 - UserPermission);	// Remove permission
						break;
					}
				}
			}
		}

		// Load current user level
		public void LoadCurrentUserLevel(string Table)
		{
			LoadUserLevel();
			SessionUserLevel = CurrentUserLevelPriv(Table);
		}

		// Get current user privilege
		private int CurrentUserLevelPriv(string TableName)
		{
			int result = 0;
			if (IsLoggedIn())	{
				result = 0;
				for (int i = 0; i <= m_ArUserLevelID.GetUpperBound(0); i++) {
					result = result | GetUserLevelPrivEx(TableName, m_ArUserLevelID[i]);
				}
			}	else	{
				return 0;
			}
			return result;
		}

		// Get user level ID by user level name
		public int GetUserLevelID(string UserLevelName)
		{
			if (ew_SameStr(UserLevelName, "Administrator"))	{
				return -1;
			} else if (ew_NotEmpty(UserLevelName)) {
				if (ew_IsArrayList(m_ArUserLevel)) {
					foreach (OrderedDictionary Row in m_ArUserLevel) {
						if (ew_SameStr(Row[1], UserLevelName)) {
							return ew_ConvertToInt(Row[0]);
						}
					}
				}
			}
			return -2;	// Unknown
		}

		// Add user level (for use with UserLevel_Loading event)
		public void AddUserLevel(string UserLevelName)
		{
			bool bFound = false;
			if (ew_Empty(UserLevelName)) return;
			int UserLevelID = GetUserLevelID(UserLevelName);
			if (!Information.IsNumeric(UserLevelID)) return;
			if (UserLevelID < -1) return;
			if (!Information.IsArray(m_ArUserLevelID))	{
				Array.Resize(ref m_ArUserLevelID, 1);
			}	else	{
				for (int i = 0; i <= m_ArUserLevelID.GetUpperBound(0); i++) {
					if (m_ArUserLevelID[i] == UserLevelID)	{
						bFound = true;
						break;
					}
				}
				if (!bFound)
					Array.Resize(ref m_ArUserLevelID, m_ArUserLevelID.Length + 1);
			}
			if (!bFound)
				m_ArUserLevelID[m_ArUserLevelID.GetUpperBound(0)] = UserLevelID;
		}

		// Delete user level (for use with UserLevel_Loading event)
		public void DeleteUserLevel(string UserLevelName)
		{
			if (ew_Empty(UserLevelName) || Convert.IsDBNull(UserLevelName)) return;
			int UserLevelID = GetUserLevelID(UserLevelName);
			if (!Information.IsNumeric(UserLevelID)) return;
			if (UserLevelID < -1) return;
			if (Information.IsArray(m_ArUserLevelID))	{
				for (int i = 0; i <= m_ArUserLevelID.GetUpperBound(0); i++) {
					if (m_ArUserLevelID[i] == UserLevelID) {
						for (int j = i + 1; j <= m_ArUserLevelID.GetUpperBound(0); j++)
							m_ArUserLevelID[j - 1] = m_ArUserLevelID[j];
						if (m_ArUserLevelID.Length > 0)
							Array.Resize(ref m_ArUserLevelID, m_ArUserLevelID.Length - 1);
						return;
					}
				}
			}
		}

		// User level list
		public string UserLevelList()
		{
			string List = "";
			if (Information.IsArray(m_ArUserLevelID))	{
				for (int i = 0; i <= m_ArUserLevelID.GetUpperBound(0); i++) {
					if (ew_NotEmpty(List)) List = List + ", "; 
					List = List + m_ArUserLevelID[i];
				}
			}
			return List;
		}

		// User level name list
		public string UserLevelNameList()
		{
			string List = "";
			if (Information.IsArray(m_ArUserLevelID)) {
				for (int i = 0; i <= m_ArUserLevelID.GetUpperBound(0); i++) {
					if (ew_NotEmpty(List)) List = List + ", "; 
					List = List + ew_QuotedValue(GetUserLevelName(m_ArUserLevelID[i]), EW_DATATYPE_STRING);
				}
			}
			return List;
		}

		// Get user privilege based on table name and user level
		public int GetUserLevelPrivEx(string TableName, int UserLevelID)
		{
			if (ew_SameStr(UserLevelID, "-1")) { // System Administrator				
				if (EW_USER_LEVEL_COMPAT)	{
					return 31;	// Use old user level values
				}	else	{
					return 127;	// Use new user level values (separate View/Search)
				}
			} else if (UserLevelID >= 0) {
				if (ew_IsArrayList(m_ArUserLevelPriv)) {
					foreach (OrderedDictionary Row in m_ArUserLevelPriv) {
						if (ew_SameStr(Row[0], TableName) && ew_SameStr(Row[1], UserLevelID)) {
							return ew_ConvertToInt(Row[2]);
						}
					}
				}
			}
			return 0;
		}

		// Get current user level name
		public string CurrentUserLevelName()
		{
			return GetUserLevelName(CurrentUserLevelID);
		}

		// Get user level name based on user level
		public string GetUserLevelName(int UserLevelID)
		{
			if (ew_SameStr(UserLevelID, "-1")) {
				return "Administrator";
			} else if (UserLevelID >= 0) {
				if (ew_IsArrayList(m_ArUserLevel)) {
					foreach (OrderedDictionary Row in m_ArUserLevel) {
						if (ew_SameStr(Row[0], UserLevelID))	{
							return Convert.ToString(Row[1]);
						}
					}
				}
			}
			return "";
		}

		// Display all the User Level settings (for debug only)
		public void ShowUserLevelInfo()
		{
			if (ew_IsArrayList(m_ArUserLevel)) {
				ew_Write("User Levels:<br>");
				ew_Write("UserLevelId, UserLevelName<br>");
				foreach (OrderedDictionary Row in m_ArUserLevel) {
					ew_Write("&nbsp;&nbsp;" + Row[0] + ", " + Row[1] + "<br>");
				}
			}	else {
				ew_Write("No User Level definitions." + "<br>");
			}
			if (ew_IsArrayList(m_ArUserLevelPriv)) {
				ew_Write("User Level Privs:<br>");
				ew_Write("TableName, UserLevelId, UserLevelPriv<br>");
				foreach (OrderedDictionary Row in m_ArUserLevelPriv) {
					ew_Write("&nbsp;&nbsp;" + Row[0] + ", " + Row[1] + ", " + Row[2] + "<br>");
				}
			}	else {
				ew_Write("No User Level privilege settings." + "<br>");
			}
			ew_Write("CurrentUserLevel = " + CurrentUserLevel + "<br>");
			ew_Write("User Levels = " + UserLevelList() + "<br>");
		}

		// Check privilege for List page (for menu items)
		public bool AllowList(string TableName)
		{
			return ew_ConvertToBool(CurrentUserLevelPriv(TableName) & EW_ALLOW_LIST);
		}

		// Check privilege for Add page (for Allow-Add / Detail-Add)
		public bool AllowAdd(string TableName)
		{
			return ew_ConvertToBool(CurrentUserLevelPriv(TableName) & EW_ALLOW_ADD);
		}

		// Check privilege for Edit page (for Detail-Edit)
		public bool AllowEdit(string TableName) {
			return ew_ConvertToBool(CurrentUserLevelPriv(TableName) & EW_ALLOW_EDIT);
		}		

		// Check if user password expired
		public bool IsPasswordExpired()
		{
			return ew_SameStr(ew_Session[EW_SESSION_STATUS], "passwordexpired");
		}

		// Check if user is logging in (after changing password)
		public bool IsLoggingIn()
		{
			return ew_SameStr(ew_Session[EW_SESSION_STATUS], "loggingin");
		}

		// Check if user is logged in
		public bool IsLoggedIn()
		{
			return ew_SameStr(ew_Session[EW_SESSION_STATUS], "login");
		}

		// Check if user is system administrator
		public bool IsSysAdmin()
		{
			return (Convert.ToInt32(ew_Session[EW_SESSION_SYS_ADMIN]) == 1);
		}

		// Check if user is administrator
		public bool IsAdmin()
		{
			bool IsAdmin = IsSysAdmin();
			if (!IsAdmin && Information.IsArray(m_ArUserLevelID))
				IsAdmin = Array.IndexOf(m_ArUserLevelID, -1) > -1;
			if (!IsAdmin)
				IsAdmin = ew_SameStr(CurrentUserID, "-1");
			if (!IsAdmin && Information.IsArray(m_ArUserID))
				IsAdmin = Array.IndexOf(m_ArUserID, -1) > -1;
			return IsAdmin;
		}

		// Save user level to session
		public void SaveUserLevel()
		{
			ew_Session[EW_SESSION_AR_USER_LEVEL] = m_ArUserLevel;
			ew_Session[EW_SESSION_AR_USER_LEVEL_PRIV] = m_ArUserLevelPriv;
		}

		// Load user level from session
		public void LoadUserLevel()
		{
			if (!ew_IsArrayList(ew_Session[EW_SESSION_AR_USER_LEVEL]) || !ew_IsArrayList(ew_Session[EW_SESSION_AR_USER_LEVEL_PRIV])) { 
				SetUpUserLevel();
				SaveUserLevel();
			}	else	{
				m_ArUserLevel = (ArrayList)ew_Session[EW_SESSION_AR_USER_LEVEL];
				m_ArUserLevelPriv = (ArrayList)ew_Session[EW_SESSION_AR_USER_LEVEL_PRIV];
			}
		}

		// Get user info
		public object CurrentUserInfo(string FieldName)
		{
			object Info = null;
			try {
				Info = GetUserInfo(FieldName, CurrentUserID);
				return Info;
			}	catch {
				if (EW_DEBUG_ENABLED) throw; 
				return Info;
			}
		}

		// Get user info
		public object GetUserInfo(string FieldName, object userid)
		{
			object Info = null;
			if (ew_Empty(userid)) return Info; 
			try {

				// Get SQL from GetSQL function in <UserTable> class, <UserTable>info.aspx
				string sFilter = EW_USER_ID_FILTER.Replace("%u", ew_AdjustSql(userid)); 
				string sSql = UserTable.GetSQL(sFilter, ""); // ASPX81
				OrderedDictionary od = ew_ExecuteRow(sSql);				
				if (od != null)
					Info = od[FieldName];
				return Info;
			}	catch {
				if (EW_DEBUG_ENABLED) throw; 
				return Info;
			}
		}

		// Get user ID value by user login name
		public object GetUserIDByUserName(string UserName)
		{
			object UserID = "";
			if (ew_NotEmpty(UserName))	{
				string sFilter = EW_USER_NAME_FILTER.Replace("%u", ew_AdjustSql(UserName)); 
				string sSql = UserTable.GetSQL(sFilter, ""); // ASPX81
				OrderedDictionary od = ew_ExecuteRow(sSql);
				if (od != null)
					UserID = od["IdUsuario"];
			}
			return UserID;
		}

		// Load user ID
		public void LoadUserID()
		{
			string sUserIDList, sCurUserIDList;
			Array.Resize(ref m_ArUserID, 0);
			if (ew_Empty(CurrentUserID)) {			

				// Add codes to handle empty User ID here
			} else if (!ew_SameStr(CurrentUserID, "-1")) { // Get first level
				AddUserID(CurrentUserID);
				string sFilter = UserTable.UserIDFilter(CurrentUserID); // ASPX81
				string sSql = UserTable.GetSQL(sFilter, ""); // ASPX81
				SqlDataReader RsUser = Conn.GetDataReader(sSql);
				while (RsUser.Read())
					AddUserID(RsUser["IdUsuario"]);
				RsUser.Close();
				RsUser.Dispose();
			}
		}

		// Add user name
		public void AddUserName(string UserName)
		{
			AddUserID(GetUserIDByUserName(UserName));
		}

		// Add user ID
		public void AddUserID(object userid)
		{
			bool bFound = false;
			if (ew_Empty(userid)) return;
			if (!Information.IsNumeric(userid)) return;
			if (!Information.IsArray(m_ArUserID))	{
				Array.Resize(ref m_ArUserID, 1);
			}	else {
				for (int i = 0; i <= m_ArUserID.GetUpperBound(0); i++) {
					if (ew_SameStr(m_ArUserID[i], userid))	{
						bFound = true;
						break;
					}
				}
				if (!bFound) {
					Array.Resize(ref m_ArUserID, m_ArUserID.Length + 1);
				}
			}
			if (!bFound) {
				m_ArUserID[m_ArUserID.GetUpperBound(0)] = userid;
			}
		}

		// Delete user name
		public void DeleteUserName(string UserName)
		{
			DeleteUserID(GetUserIDByUserName(UserName));
		}

		// Delete user ID
		public void DeleteUserID(object userid)
		{
			if (ew_Empty(userid)) return;
			if (!Information.IsNumeric(userid)) return;
			if (Information.IsArray(m_ArUserID)) {
				for (int i = 0; i <= m_ArUserID.GetUpperBound(0); i++) {
					if (ew_SameStr(m_ArUserID[i], userid)) {
						for (int j = i + 1; j <= m_ArUserID.GetUpperBound(0); j++)
							m_ArUserID[j - 1] = m_ArUserID[j];
						if (m_ArUserID.Length > 0)
							Array.Resize(ref m_ArUserID, m_ArUserID.Length - 1);
						return;
					}
				}
			}
		}

		// User ID list
		public string UserIDList()
		{
			string List = "";
			if (Information.IsArray(m_ArUserID))	{
				for (int i = 0; i <= m_ArUserID.GetUpperBound(0); i++) {
					if (ew_NotEmpty(List)) List += ", "; 
					List += ew_QuotedValue(m_ArUserID[i], EW_DATATYPE_NUMBER);
				}
			}
			return List;
		}

		// List of allowed user ids for this user
		public bool IsValidUserID(object userid)
		{
			if (IsLoggedIn())	{
				if (Information.IsArray(m_ArUserID))	{
					for (int i = 0; i <= m_ArUserID.GetUpperBound(0); i++) {
						if (ew_SameStr(m_ArUserID[i], userid))
							return true;
					}
				}
			}
			return false;
		}

		// UserID Loading event
		public void UserID_Loading() {

			//HttpContext.Current.Response.Write("UserID Loading: " + CurrentUserID + "<br>");
		}

		// UserID Loaded event
		public void UserID_Loaded() {

			//HttpContext.Current.Response.Write("UserID Loaded: " + UserIDList() + "<br>");
		}

		// User Level Loaded event
		public void UserLevel_Loaded() {

			//AddUserPermission(<UserLevelName>, <TableName>, <UserPermission>);
			//DeleteUserPermission(<UserLevelName>, <TableName>, <UserPermission>);

		}

		// Table Permission Loading event
		public void TablePermission_Loading() {

			//HttpContext.Current.Response.Write("Table Permission Loading: " + CurrentUserLevelID + "<br>");
		}

		// Table Permission Loaded event
		public void TablePermission_Loaded() {

			//HttpContext.Current.Response.Write("Table Permission Loaded: " + CurrentUserLevel + "<br>");
		}

		// User Custom Validate event
		public bool User_CustomValidate(ref string usr, ref string pwd) {

			// Return false to continue with default validation after event exits, or return true to skip default validation
			return false;
		}

		// User Validated event
		public void User_Validated(DbDataReader rs) {

			//HttpContext.Current.Session["UserEmail"] = rs["Email"];
		}

		// User PasswordExpired event
		public void User_PasswordExpired(DbDataReader rs) {

			//HttpContext.Current.Response.Write("User_PasswordExpired");
		}
	}

// User Profile Class
	public class cUserProfile : AspNetMakerBase
	{

		private Hashtable Profile = new Hashtable();

		private string KeySep;

		private string FldSep;

		private int TimeoutTime;

		private int MaxRetryCount;

		private int RetryLockoutTime;

		private int PasswordExpiryTime;

		private cUsuarios UserTable; // ASPX81

		// Constructor
		public cUserProfile(AspNetMakerPage APage)
		{
			m_Page = APage;
			m_ParentPage = APage.ParentPage;
			UserTable = Usuarios; // ASPX81 
			InitProfile();
		}

		// User table
		public cUsuarios Usuarios
		{
			get { return ParentPage.Usuarios; }
		}

		// Initialize profile object
		private void InitProfile() {
			KeySep = EW_USER_PROFILE_KEY_SEPARATOR;
			FldSep = EW_USER_PROFILE_FIELD_SEPARATOR;
		}

		public string GetValue(string Name)
		{
			if (Profile.ContainsKey(Name)) {
				return Convert.ToString(Profile[Name]);
			} else {
				return "";
			}
		}

		public bool SetValue(string Name, string Value)
		{
			if (Profile.ContainsKey(Name)) {
				Profile[Name] = Value;
				return true;
			} else {
				return false;
			}
		}

		public bool LoadProfileFromDatabase(string usr)
		{
			if (ew_Empty(usr) || usr == EW_ADMIN_USER_NAME)
				return false; 

			// Ignore hard code admin
			string sFilter = EW_USER_NAME_FILTER.Replace("%u", ew_AdjustSql(usr));

			// Get SQL from GetSql method in <UserTable> class, <UserTable>info.*
			string sSql = UserTable.GetSQL(sFilter, ""); // ASPX81
			OrderedDictionary od = ew_ExecuteRow(sSql);
			if (od != null) {
				string p = Convert.ToString(od[EW_USER_PROFILE_FIELD_NAME]);
				LoadProfile(p);
				return true;
			} else {
				return false;
			}
		}

		public void LoadProfile(string str)
		{
			string name;
			string value;
			if (ew_NotEmpty(str)) {
				Hashtable ar = SplitProfile(str);
				foreach (DictionaryEntry p in ar) {
					name = Convert.ToString(p.Key);
					value = Convert.ToString(p.Value);
					if (Profile.ContainsKey(name))
						Profile[name] = value;
				}
			}
		}

		public void WriteProfile()
		{
			foreach (DictionaryEntry p in Profile)
				HttpContext.Current.Response.Write("Name: " + Convert.ToString(p.Key) + ", Value: " + Convert.ToString(p.Value) + "<br />");
		}

		public void ClearProfile()
		{
			Profile.Clear();
		}

		public void SaveProfileToDatabase(string usr)
		{
			if (ew_Empty(usr) || usr == EW_ADMIN_USER_NAME) // Ignore hard code admin
				return;			
			string sFilter = EW_USER_NAME_FILTER.Replace("%u", ew_AdjustSql(usr));
			OrderedDictionary RsProfile = new OrderedDictionary();
			RsProfile.Add(EW_USER_PROFILE_FIELD_NAME, ProfileToString());

			// Update Profile
			UserTable.CurrentFilter = sFilter; // ASPX81
			UserTable.Update(ref RsProfile); // ASPX81

			//ew_Execute("UPDATE " + EW_USER_TABLE + " SET " + EW_USER_PROFILE_FIELD_NAME + "='" + ew_AdjustSql(ProfileToString()) + "' WHERE " + sFilter);
			ew_Session[EW_SESSION_USER_PROFILE] = ProfileToString();
		}

		public string ProfileToString()
		{
			string name;
			string value;
			string sProfileStr = "";
			foreach (DictionaryEntry p in Profile) {
				name = Convert.ToString(p.Key);
				value = Convert.ToString(p.Value);
				if (ew_NotEmpty(value)) {
					if (ew_NotEmpty(sProfileStr)) sProfileStr += FldSep;
					sProfileStr += EncodeStr(name) + KeySep + EncodeStr(value);					
				}
			}
			return sProfileStr;
		}

		// Split profile
		private Hashtable SplitProfile(string pstr)
		{
			Hashtable ar = new Hashtable();
			string field;
			int pos1 = 0;
			int pos2 = LocateStr(pos1, pstr, FldSep);
			while (pos2 != -1) {
				field = pstr.Substring(pos1, pos2 - pos1);
				AddProfileItem(ref ar, field);
				pos1 = pos2 + 1;
				pos2 = LocateStr(pos1, pstr, FldSep);
			}
			if (pos1 < pstr.Length - 1)
				AddProfileItem(ref ar, pstr.Substring(pos1));
			return ar;
		}

		// Add profile item
		private void AddProfileItem(ref Hashtable ar, string field)
		{
			int pos = LocateStr(0, field, KeySep);
			if (pos > 0) {
				string name = DecodeStr(field.Substring(0, pos - 1));
				string value = DecodeStr(field.Substring(pos + 1));
				ar.Add(name, value);
			}
		}

		// Locate string from separator (skip escaped value)
		private int LocateStr(int pos, string str, string sep)
		{
			int wrkpos = str.IndexOf(sep, pos);
			while (wrkpos != -1) {
				if (wrkpos <= 0) {
					return wrkpos;
				} else if (str.Substring(wrkpos, 1) == "\\") { // Escaped?
					wrkpos = str.IndexOf(sep, wrkpos + 1); // Continue to next character
				} else {
					return wrkpos;
				}
			}
			return -1; // Not found
		}

		// Encode value ...,...=... as "...\,...\=..."
		private string EncodeStr(string val)
		{
			string wrkstr = val.Replace("\\", "\\\\");
			wrkstr = wrkstr.Replace(KeySep, "\\" + KeySep);
			wrkstr = "\"" + wrkstr.Replace(FldSep, "\\" + FldSep) + "\"";
			return wrkstr;
		}

		// Decode value "...\,...\=..." to ...,...=...
		private string DecodeStr(string val)
		{
			string wrkstr = val;
			if (wrkstr.StartsWith("\""))
				wrkstr = wrkstr.Substring(1); 
			if (wrkstr.EndsWith("\""))
				wrkstr = wrkstr.Substring(0, wrkstr.Length - 1); 
			wrkstr = wrkstr.Replace("\\" + FldSep, FldSep);
			wrkstr = wrkstr.Replace("\\" + KeySep, KeySep);
			wrkstr = wrkstr.Replace("\\\\", "\\");
			return wrkstr;
		}
	}

	// Return multi-value search SQL
	public static string ew_GetMultiSearchSql(ref cField Fld, string FldVal)
	{
		string sSql;
		string sVal;
		string sWrk = "";
		string[] arVal = FldVal.Split(new char[] {','});
		for (int i = 0; i <= arVal.GetUpperBound(0); i++) {
			sVal = arVal[i].Trim();
			if (arVal.GetUpperBound(0) == 0 || EW_SEARCH_MULTI_VALUE_OPTION == 3)	{
				sSql = Fld.FldExpression + " = '" + ew_AdjustSql(sVal) + "' OR " + ew_GetMultiSearchSqlPart(ref Fld, sVal);
			}	else	{
				sSql = ew_GetMultiSearchSqlPart(ref Fld, sVal);
			}
			if (ew_NotEmpty(sWrk)) {
				if (EW_SEARCH_MULTI_VALUE_OPTION == 2)	{
					sWrk = sWrk + " AND ";
				} else if (EW_SEARCH_MULTI_VALUE_OPTION == 3) {
					sWrk = sWrk + " OR ";
				}
			}
			sWrk = sWrk + "(" + sSql + ")";
		}
		return sWrk;
	}

	// Get multi search SQL part
	public static string ew_GetMultiSearchSqlPart(ref cField Fld, string FldVal)
	{
		return Fld.FldExpression + ew_Like("'" + ew_AdjustSql(FldVal) + ",%'") + " OR " +
			Fld.FldExpression + ew_Like("'%," + ew_AdjustSql(FldVal) + ",%'") + " OR " +
			Fld.FldExpression + ew_Like("'%," + ew_AdjustSql(FldVal) + "'");
	}

	// Get search SQL
	public static string ew_GetSearchSql(ref cField Fld, string FldVal, string FldOpr, string FldCond, string FldVal2, string FldOpr2)
	{
		bool IsValidValue;
		string sSql = "";
		string sFldExpression;
		if (Fld.FldIsVirtual && !Fld.FldForceSelection) {
			sFldExpression = Fld.FldVirtualExpression;
		} else {
			sFldExpression = Fld.FldExpression;
		}
		int FldDataType = Fld.FldDataType;
		if (Fld.FldIsVirtual && !Fld.FldForceSelection)
			FldDataType = EW_DATATYPE_STRING;
		if (FldDataType == EW_DATATYPE_NUMBER) { // Fix wrong operator
			if (FldOpr == "LIKE" || FldOpr == "STARTS WITH") {
				FldOpr = "=";
			} else if (FldOpr == "NOT LIKE") {
				FldOpr = "<>";
			}
			if (FldOpr2 == "LIKE" || FldOpr2 == "STARTS WITH") {
				FldOpr2 = "=";
			} else if (FldOpr2 == "NOT LIKE") {
				FldOpr2 = "<>";
			}	
		} 
		if (FldOpr == "BETWEEN")	{
			IsValidValue = (FldDataType != EW_DATATYPE_NUMBER) || (FldDataType == EW_DATATYPE_NUMBER && Information.IsNumeric(FldVal) && Information.IsNumeric(FldVal2));
			if (ew_NotEmpty(FldVal) && ew_NotEmpty(FldVal2) && IsValidValue)	{
				sSql = sFldExpression + " BETWEEN " + ew_QuotedValue(FldVal, FldDataType) + " AND " + ew_QuotedValue(FldVal2, FldDataType);
			}
		} else if (FldVal == EW_NULL_VALUE || FldOpr == "IS NULL") {
			sSql = sFldExpression + " IS NULL";
		} else if (FldVal == EW_NOT_NULL_VALUE || FldOpr == "IS NOT NULL") {
			sSql = sFldExpression + " IS NOT NULL";
		}	else {
			IsValidValue = (FldDataType != EW_DATATYPE_NUMBER) || (FldDataType == EW_DATATYPE_NUMBER && Information.IsNumeric(FldVal));
			if (ew_NotEmpty(FldVal) && IsValidValue && ew_IsValidOpr(FldOpr, FldDataType))	{
				sSql = sFldExpression + ew_SearchString(FldOpr, FldVal, FldDataType);
				if (FldDataType == EW_DATATYPE_BOOLEAN && FldVal == "0" && FldOpr == "=")
					sSql = "(" + sSql + " OR " + sFldExpression + " IS NULL)";
			}
			IsValidValue = (FldDataType != EW_DATATYPE_NUMBER) || (FldDataType == EW_DATATYPE_NUMBER && Information.IsNumeric(FldVal2));
			if (ew_NotEmpty(FldVal2) && IsValidValue && ew_IsValidOpr(FldOpr2, FldDataType))	{
				string sSql2 = sFldExpression + ew_SearchString(FldOpr2, FldVal2, FldDataType);
				if (FldDataType == EW_DATATYPE_BOOLEAN && FldVal2 == "0" && FldOpr2 == "=")
					sSql2 = "(" + sSql2 + " OR " + sFldExpression + " IS NULL)";
				if (ew_NotEmpty(sSql)) {
					sSql = "(" + sSql + " " + ((FldCond == "OR") ? "OR" : "AND") + " " + sSql2 + ")";
				} else {
					sSql = sSql2;
				}
			}
		}
		return sSql;
	}

	// Return search string
	public static string ew_SearchString(string FldOpr, string FldVal, int FldType)
	{
		if (FldOpr == "LIKE")	{
			return ew_Like(ew_QuotedValue("%" + FldVal + "%", FldType));
		} else if (FldOpr == "NOT LIKE") {
			return " NOT " + ew_Like(ew_QuotedValue("%" + FldVal + "%", FldType));
		} else if (FldOpr == "STARTS WITH") {
			return ew_Like(ew_QuotedValue(FldVal + "%", FldType));
		} else {
			return " " + FldOpr + " " + ew_QuotedValue(FldVal, FldType);
		}
	}

	// Check if valid operator
	public static bool ew_IsValidOpr(string Opr, int FldType)
	{
		bool Valid = (Opr == "=" || Opr == "<" || Opr == "<=" || Opr == ">" || Opr == ">=" || Opr == "<>");
		if (FldType == EW_DATATYPE_STRING || FldType == EW_DATATYPE_MEMO)	{
			Valid = Valid || Opr == "LIKE" || Opr == "NOT LIKE" || Opr == "STARTS WITH";
		}
		return Valid;
	}

	// Quoted name for table/field
	public static string ew_QuotedName(string Name)
	{
		return EW_DB_QUOTE_START + Name.Replace(EW_DB_QUOTE_END, EW_DB_QUOTE_END + EW_DB_QUOTE_END) + EW_DB_QUOTE_END;
	}

	// Quoted value for field type
	public static string ew_QuotedValue(object Value, int FldType)
	{
		switch (FldType) {
			case EW_DATATYPE_STRING:
			case EW_DATATYPE_MEMO:
				return "'" + ew_AdjustSql(Value) + "'";
			case EW_DATATYPE_GUID:
				if (EW_IS_MSACCESS)	{
					if (Convert.ToString(Value).StartsWith("{"))	{
						return Convert.ToString(Value);
					} else {
						return "{" + ew_AdjustSql(Value) + "}";
					}
				} else {
					return "'" + ew_AdjustSql(Value) + "'";
				}
				break;
			case EW_DATATYPE_DATE:
			case EW_DATATYPE_TIME:
				if (EW_IS_MSACCESS)	{
					return "#" + ew_AdjustSql(Value) + "#";
				} else if (EW_IS_ORACLE) { 
					return "TO_DATE('" + ew_AdjustSql(Value) + "', 'YYYY/MM/DD HH24:MI:SS')"; 
				} else {
					return "'" + ew_AdjustSql(Value) + "'";
				}
				break;
			default:
				return Convert.ToString(Value);
		}
	}

	// Pad zeros before number
	public static string ew_ZeroPad(object m, int t)
	{
		return Convert.ToString(m).PadLeft(t, '0');
	}

	// Append like operator
	public static string ew_Like(string pat) {
		return " LIKE " + pat;
	}

	// Get script name
	public static string ew_ScriptName() {
		string sn = ew_ServerVar("SCRIPT_NAME");
		if (ew_Empty(sn)) sn = ew_ServerVar("PATH_INFO");
		if (ew_Empty(sn)) sn = ew_ServerVar("URL");
		if (ew_Empty(sn)) sn = "UNKNOWN";
		return sn;
	}

	// Get server variable by name
	public static string ew_ServerVar(string Name) {
		string str = HttpContext.Current.Request.ServerVariables[Name];
		if (ew_Empty(str))
			str = "";
		return str;
	}

	// Convert numeric value
	public static object ew_Conv(object v, int t)
	{
		if (Convert.IsDBNull(v)) return System.DBNull.Value; 
		switch (t) {
			case 20: // adBigInt
				return Convert.ToInt64(v);
			case 21: // adUnsignedBigInt
				return Convert.ToUInt64(v);
			case 2:
			case 16: // adSmallInt/adTinyInt
				return Convert.ToInt16(v);
			case 3: // adInteger
				return Convert.ToInt32(v);
			case 17:
			case 18: // adUnsignedTinyInt/adUnsignedSmallInt
				return Convert.ToUInt16(v);
			case 19: // adUnsignedInt
				return Convert.ToUInt32(v);
			case 4: // adSingle
				return Convert.ToSingle(v);
			case 5:
			case 6:
			case 131:
			case 139: // adDouble/adCurrency/adNumeric/adVarNumeric
				return Convert.ToDouble(v);
			default:
				return v;
		}
	}

	// Trace (for debug only)
	public static void ew_Trace(object Msg)
	{
		try {
			string FileName = ew_MapPath("debug.txt");
			StreamWriter sw = File.AppendText(FileName);
			sw.WriteLine(Convert.ToString(Msg));
			sw.Close();
		}	catch {
			if (EW_DEBUG_ENABLED) throw; 
		}
	}

	// Calculate elapsed time
	public static string ew_CalcElapsedTime(long tm)
	{
		double endTimer = Environment.TickCount;
		return "<div>page processing time: " + Convert.ToString((endTimer - tm) / 1000) + " seconds</div>";
	}

	// Compare values with special handling for null values
	public static bool ew_CompareValue(object v1, object v2)
	{
		if (Convert.IsDBNull(v1) && Convert.IsDBNull(v2)) {
			return true;
		} else if (Convert.IsDBNull(v1) || Convert.IsDBNull(v2)) {
			return false;
		}	else {
			return ew_SameStr(v1, v2);
		}
	}

	// Adjust SQL for special characters
	public static string ew_AdjustSql(object value)
	{
		string sWrk = Convert.ToString(value).Trim();
		sWrk = sWrk.Replace("'", "''");	// Adjust for single quote
		sWrk = sWrk.Replace("[", "[[]"); // Adjust for open square bracket
		return sWrk;
	}

	// Build SELECT SQL based on different SQL part
	public static string ew_BuildSelectSql(string sSelect, string sWhere, string sGroupBy, string sHaving, string sOrderBy, string sFilter, string sSort)
	{
		string sDbWhere = sWhere;
		ew_AddFilter(ref sDbWhere, sFilter);
		string sDbOrderBy = sOrderBy;
		if (ew_NotEmpty(sSort))
			sDbOrderBy = sSort;
		string sSql = sSelect;
		if (ew_NotEmpty(sDbWhere))
			sSql += " WHERE " + sDbWhere;
		if (ew_NotEmpty(sGroupBy))
			sSql += " GROUP BY " + sGroupBy;
		if (ew_NotEmpty(sHaving))
			sSql += " HAVING " + sHaving;
		if (ew_NotEmpty(sDbOrderBy))
			sSql += " ORDER BY " + sDbOrderBy;
		return sSql;
	}

	// Load a text file
	public static string ew_LoadTxt(string fn)
	{
		string sTxt = "";
		if (ew_NotEmpty(fn)) {
			if (!ew_FileExists("", fn)) fn = ew_MapPath("~/" + fn);
			StreamReader sw = File.OpenText(fn);
			sTxt = sw.ReadToEnd();
			sw.Close();
		}
		return sTxt;
	}

	// Write audit trail (login/logout)
	public static void ew_WriteAuditTrailOnLogInOut(string logtype, string username)
	{
		try {
			string field = "";
			object oldvalue = "";
			object keyvalue = "";
			object newvalue = "";
			string filePfx = "log";
			DateTime dt = DateTime.Now;
			string curDateTime = dt.ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss");
			string table = ew_CurrentUserIP();
			string id = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
			ew_WriteAuditTrail(filePfx, curDateTime, id, username, logtype, table, field, keyvalue, oldvalue, newvalue);
		}	catch {
			if (EW_DEBUG_ENABLED) throw; 
		}
	}

	// Write audit trail (insert/update/delete)
	public static void ew_WriteAuditTrail(string pfx, string curDateTime, string scrpt, string user, string action, string table, string field, object keyvalue, object oldvalue, object newvalue)
	{
		try {
			string userwrk = user;
			if (ew_Empty(userwrk))
				userwrk = "-1"; // assume Administrator if no user
			if (!EW_AUDIT_TRAIL_TO_DATABASE) { // Write audit trail to log file
				string sHeader = "date/time" + "\t" + "script" + "\t" + "user" + "\t" + "action" + "\t" + "table" + "\t" + "field" + "\t" + "key value" + "\t" + "old value" + "\t" + "new value";
				string sMsg = curDateTime + "\t" + scrpt + "\t" + userwrk + "\t" + action + "\t" + table + "\t" + field + "\t" + Convert.ToString(keyvalue) + "\t" + Convert.ToString(oldvalue) + "\t" + Convert.ToString(newvalue);
				string sFolder = EW_AUDIT_TRAIL_PATH;
				string sFn = pfx + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
				bool bWriteHeader = !File.Exists(ew_UploadPathEx(true, sFolder) + sFn);
				StreamWriter sw = File.AppendText(ew_UploadPathEx(true, sFolder) + sFn);
				if (bWriteHeader) sw.WriteLine(sHeader); 
				sw.WriteLine(sMsg);
				sw.Close();
			} else {
				string sAuditSql = "INSERT INTO " + ew_QuotedName(EW_AUDIT_TRAIL_TABLE_NAME) +
					" (" + ew_QuotedName(EW_AUDIT_TRAIL_FIELD_NAME_DATETIME) + ", " +
					ew_QuotedName(EW_AUDIT_TRAIL_FIELD_NAME_SCRIPT) + ", " +
					ew_QuotedName(EW_AUDIT_TRAIL_FIELD_NAME_USER) + ", " +
					ew_QuotedName(EW_AUDIT_TRAIL_FIELD_NAME_ACTION) + ", " +
					ew_QuotedName(EW_AUDIT_TRAIL_FIELD_NAME_TABLE) + ", " +
					ew_QuotedName(EW_AUDIT_TRAIL_FIELD_NAME_FIELD) + ", " +
					ew_QuotedName(EW_AUDIT_TRAIL_FIELD_NAME_KEYVALUE) + ", " +
					ew_QuotedName(EW_AUDIT_TRAIL_FIELD_NAME_OLDVALUE) + ", " +
					ew_QuotedName(EW_AUDIT_TRAIL_FIELD_NAME_NEWVALUE) + ") " +
					" VALUES (" +
					ew_QuotedValue(curDateTime, EW_DATATYPE_DATE) + ", " +
					ew_QuotedValue(scrpt, EW_DATATYPE_STRING) + ", " +
					ew_QuotedValue(userwrk, EW_DATATYPE_STRING) + ", " +
					ew_QuotedValue(action, EW_DATATYPE_STRING) + ", " +
					ew_QuotedValue(table, EW_DATATYPE_STRING) + ", " +
					ew_QuotedValue(field, EW_DATATYPE_STRING) + ", " +
					ew_QuotedValue(Convert.ToString(keyvalue), EW_DATATYPE_STRING) + ", " +
					ew_QuotedValue(Convert.ToString(oldvalue), EW_DATATYPE_STRING) + ", " +
					ew_QuotedValue(Convert.ToString(newvalue), EW_DATATYPE_STRING) + ")";

				// HttpContext.Current.Response.Write(sAuditSql) // uncomment to debug
				ew_Execute(sAuditSql);
			}
		} catch {
			if (EW_DEBUG_ENABLED) throw; 
		}
	}

	// Functions for default date format
	// ANamedFormat = 0-8, where 0-4 same as VBScript
	// 5 = "yyyy/mm/dd"
	// 6 = "mm/dd/yyyy"
	// 7 = "dd/mm/yyyy"
	// 8 = Short Date + Short Time
	// 9 = "yyyy/mm/dd HH:MM:SS"
	// 10 = "mm/dd/yyyy HH:MM:SS"
	// 11 = "dd/mm/yyyy HH:MM:SS"
	// 12 - Short Date - 2 digit year (yy/mm/dd)
	// 13 - Short Date - 2 digit year (mm/dd/yy)
	// 14 - Short Date - 2 digit year (dd/mm/yy)
	// 15 - Short Date (yy/mm/dd) + Short Time (hh:mm:ss)
	// 16 - Short Date (mm/dd/yy) + Short Time (hh:mm:ss)
	// 17 - Short Date (dd/mm/yy) + Short Time (hh:mm:ss)
	// Format date time based on format type
	public static string ew_FormatDateTime(object ADate, int ANamedFormat)
	{
		string sDT;
		if (Information.IsDate(ADate)) {
			DateTime DT = Convert.ToDateTime(ADate);
			if (ANamedFormat >= 0 && ANamedFormat <= 4)	{
				sDT = Strings.FormatDateTime(DT, (DateFormat)Enum.ToObject(typeof(DateFormat), ANamedFormat));
			} else if (ANamedFormat == 5 || ANamedFormat == 9) {
				sDT = DT.ToString("yyyy'" + EW_DATE_SEPARATOR + "'MM'" + EW_DATE_SEPARATOR + "'dd");
			} else if (ANamedFormat == 6 || ANamedFormat == 10) {
				sDT = DT.ToString("MM'" + EW_DATE_SEPARATOR + "'dd'" + EW_DATE_SEPARATOR + "'yyyy");
			} else if (ANamedFormat == 7 || ANamedFormat == 11) {
				sDT = DT.ToString("dd'" + EW_DATE_SEPARATOR + "'MM'" + EW_DATE_SEPARATOR + "'yyyy");
			} else if (ANamedFormat == 8) {
				sDT = Strings.FormatDateTime(DT, (DateFormat)Enum.ToObject(typeof(DateFormat), 2));
				if (DT.Hour != 0 || DT.Minute != 0 || DT.Second != 0)
					sDT += " " + DT.ToString("HH':'mm':'ss");
			} else if (ANamedFormat == 12 || ANamedFormat == 15) {
				sDT = DT.ToString("yy'" + EW_DATE_SEPARATOR + "'MM'" + EW_DATE_SEPARATOR + "'dd");
			} else if (ANamedFormat == 13 || ANamedFormat == 16) {
				sDT = DT.ToString("MM'" + EW_DATE_SEPARATOR + "'dd'" + EW_DATE_SEPARATOR + "'yy");
			} else if (ANamedFormat == 14 || ANamedFormat == 17) {
				sDT = DT.ToString("dd'" + EW_DATE_SEPARATOR + "'MM'" + EW_DATE_SEPARATOR + "'yy");	
			}	else	{
				return DT.ToString();
			}
			if ((ANamedFormat >= 9 && ANamedFormat <= 11) || (ANamedFormat >= 15 && ANamedFormat <= 17))
				sDT += " " + DT.ToString("HH':'mm':'ss");
			return sDT;
		}	else	{
			return Convert.ToString(ADate);
		}
	}

	// Unformat date time based on format type
	public static string ew_UnformatDateTime(object ADate, int ANamedFormat)
	{
		string[] arDateTime, arDatePt;
		DateTime d;
		string sDT;
		string sDate = Convert.ToString(ADate).Trim();
		while (sDate.Contains("  "))
			sDate = sDate.Replace("  ", " ");
		if (Regex.IsMatch(sDate, @"^([0-9]{4})/([0][1-9]|[1][0-2])/([0][1-9]|[1|2][0-9]|[3][0|1])( (0[0-9]|1[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]))?$")) // ASPX
			return sDate;		
		arDateTime = sDate.Split(new char[] {' '});
		if (ANamedFormat == 0 && Information.IsDate(sDate))	{
			d = Convert.ToDateTime(arDateTime[0]);
			sDT = d.ToString("yyyy'/'MM'/'dd");
			if (arDateTime.Length > 0)	{
				for (int i = 1; i < arDateTime.Length; i++)
					sDT = sDT + " " + arDateTime[i];
			}
			return sDT;
		}	else	{
			arDatePt = arDateTime[0].Split(new char[] { Convert.ToChar(EW_DATE_SEPARATOR) });
			if (arDatePt.Length == 3)	{
				switch (ANamedFormat) {
					case 5:
					case 9: //yyyymmdd
						if (ew_CheckDate(arDateTime[0])) {
							sDT = arDatePt[0] + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[2].PadLeft(2, '0');							
							break;
						} else {
							return sDate;
						}
					case 6:
					case 10: //mmddyyyy
						if (ew_CheckUSDate(arDateTime[0])) {
							sDT = arDatePt[2].PadLeft(2, '0') + "/" + arDatePt[0].PadLeft(2, '0') + "/" + arDatePt[1];
							break;
						} else {
							return sDate;
						}
					case 7:
					case 11: //ddmmyyyy
						if (ew_CheckEuroDate(arDateTime[0])) {
							sDT = arDatePt[2].PadLeft(2, '0') + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[0];
							break;
						} else {
							return sDate;
						}
					case 12:
					case 15: //yymmdd
						if (ew_CheckShortDate(arDateTime[0])) {
							arDatePt[0] = ew_UnformatYear(arDatePt[0]);
							sDT = arDatePt[0] + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[2].PadLeft(2, '0');							
							break;
						} else {
							return sDate;
						}
					case 13:
					case 16: //mmddyy
						if (ew_CheckShortUSDate(arDateTime[0])) {
							arDatePt[2] = ew_UnformatYear(arDatePt[2]);
							sDT = arDatePt[2] + "/" + arDatePt[0].PadLeft(2, '0') + "/" + arDatePt[1].PadLeft(2, '0');
							break;
						} else {
							return sDate;
						}
					case 14:
					case 17: //ddmmyy
						if (ew_CheckShortEuroDate(arDateTime[0])) {
							arDatePt[2] = ew_UnformatYear(arDatePt[2]);
							sDT = arDatePt[2] + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[0].PadLeft(2, '0');
							break;
						} else {
							return sDate;
						}
					default:
						return sDate;
				}				
				if (arDateTime.Length > 1)	{
					if (Information.IsDate(arDateTime[1])) // Is time
						sDT += " " + arDateTime[1];
				}
				return sDT;
			}	else {
				return sDate;
			}
		}
	}

	// Unformat date time based on format type // C#
	public static string ew_UnFormatDateTime(object ADate, int ANamedFormat)
	{
		return ew_UnformatDateTime(ADate, ANamedFormat);
	}

	// Format currency
	public static string ew_FormatCurrency(object Expression, int NumDigitsAfterDecimal, Microsoft.VisualBasic.TriState IncludeLeadingDigit, Microsoft.VisualBasic.TriState UseParensForNegativeNumbers, Microsoft.VisualBasic.TriState GroupDigits)
	{
		if (!Information.IsNumeric(Expression)) return Convert.ToString(Expression);
		if (Convert.IsDBNull(Expression)) return String.Empty;
		return Strings.FormatCurrency(Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits);
	}

	// Format number
	public static string ew_FormatNumber(object Expression, int NumDigitsAfterDecimal, Microsoft.VisualBasic.TriState IncludeLeadingDigit, Microsoft.VisualBasic.TriState UseParensForNegativeNumbers, Microsoft.VisualBasic.TriState GroupDigits)
	{
		if (!Information.IsNumeric(Expression)) return Convert.ToString(Expression);
		if (Convert.IsDBNull(Expression)) return String.Empty;
		return Strings.FormatNumber(Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits);
	}

	// Format percent
	public static string ew_FormatPercent(object Expression, int NumDigitsAfterDecimal, Microsoft.VisualBasic.TriState IncludeLeadingDigit, Microsoft.VisualBasic.TriState UseParensForNegativeNumbers, Microsoft.VisualBasic.TriState GroupDigits)
	{
		if (!Information.IsNumeric(Expression)) return Convert.ToString(Expression);
		if (Convert.IsDBNull(Expression)) return String.Empty;
		return Strings.FormatPercent(Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits);
	}

	// Output SCRIPT tag
	public static void ew_AddClientScript(string src, Hashtable attrs) {
		Hashtable atts = new Hashtable();
		atts.Add("type", "text/javascript");
		atts.Add("src", src);
		if (attrs != null) {
			foreach (DictionaryEntry de in attrs)
				atts.Add(de.Key, de.Value);
		}
		ew_Write(ew_HtmlElement("script", atts, "", true) + "\n");
	}

	public static void ew_AddClientScript(string src) {
		ew_AddClientScript(src, null);	
	}

	// Output LINK tag
	public static void ew_AddStylesheet(string href, Hashtable attrs) {
		Hashtable atts = new Hashtable();
		atts.Add("rel", "stylesheet");
		atts.Add("type", "text/css");
		atts.Add("href", href);
		if (attrs != null) {
			foreach (DictionaryEntry de in attrs)
				atts.Add(de.Key, de.Value);
		}
		ew_Write(ew_HtmlElement("link", atts, "", false) + "\n");
	}

	public static void ew_AddStylesheet(string href) {
		ew_AddStylesheet(href, null);	
	}

	// Build HTML element
	public static string ew_HtmlElement(string tagname, Hashtable attrs, string innerhtml, bool endtag) {
		string html = "<" + tagname;
		if (attrs != null) {
			foreach (DictionaryEntry de in attrs) {
				if (ew_NotEmpty(de.Value))
					html += " " + de.Key + "=\"" + ew_HtmlEncode(de.Value) + "\"";
			}
		}
		html += ">";
		if (ew_NotEmpty(innerhtml))
			html += innerhtml;
		if (endtag)
			html += "</" + tagname + ">";
		return html;
	}

	// XML tag name
	public static string ew_XmlTagName(string name) {
		name = name.Replace(" ", "_");
		Regex regEx = new Regex(@"^(?!XML)[a-z][\w-]*$", RegexOptions.IgnoreCase);
		if (!regEx.IsMatch(name))
			name = "_" + name;
		return name;
	}

	// Encode HTML
	public static string ew_HtmlEncode(object Expression)
	{
		return HttpContext.Current.Server.HtmlEncode(Convert.ToString(Expression));
	}

	// Encode URL
	public static string ew_UrlEncode(object Expression)
	{
		return HttpContext.Current.Server.UrlEncode(Convert.ToString(Expression));
	}

	// Decode URL
	public static string ew_UrlDecode(string Str)
	{
		return HttpContext.Current.Server.UrlDecode(Str);
	}

	// Encode value for single-quoted JavaScript string
	public static string ew_JsEncode(object val)
	{
		string outstr = Convert.ToString(val).Replace("\\", "\\\\");
		outstr = outstr.Replace("'", "\\'");
		outstr = outstr.Replace("\r\n", "<br>");
		outstr = outstr.Replace("\r", "<br>");
		outstr = outstr.Replace("\n", "<br>");
		return outstr;		
	}

	// Encode value for double-quoted JavaScript string
	public static string ew_JsEncode2(object val)
	{
		string outstr = Convert.ToString(val).Replace("\\", "\\\\");
		outstr = outstr.Replace("\"", "\\\"");
		outstr = outstr.Replace("\r\n", "<br>");
		outstr = outstr.Replace("\r", "<br>");
		outstr = outstr.Replace("\n", "<br>");
		return outstr;
	}	

	// Encode value to single-quoted Javascript string for HTML attributes
	public static string ew_JsEncode3(object val)
	{
		string outstr = Convert.ToString(val).Replace("\\", "\\\\");
		outstr = outstr.Replace("'", "\\'");
		outstr = outstr.Replace("\"", "&quot;");
		return outstr;
	}

	// Convert array to JSON for HTML attributes
	public static string ew_ArrayToJsonAttr(Hashtable ht)
	{
		string Str = "{";
		foreach (DictionaryEntry Item in ht) {
			Str += Convert.ToString(Item.Key) + ":'" + ew_JsEncode3(Item.Value) + "',";
		}
		if (Str.EndsWith(","))
			Str = Str.Substring(0, Str.Length - 1); 
		Str += "}";
		return Str;
	}

	// Generate Value Separator based on current row index
	// rowidx - zero based row index
	// dispidx - zero based display index
	// fld - field object
	public static string ew_ValueSeparator(int rowidx, int dispidx, cField fld)
	{
		return ", ";
	}

	// Generate View Option Separator based on current option count (Multi-Select / CheckBox)
	// optidx - zero based option index
	public static string ew_ViewOptionSeparator(int optidx)
	{
		return ", ";
	}

	// Render repeat column table
	// rowcnt - zero based row count
	public static string ew_RepeatColumnTable(int totcnt, int rowcnt, int repeatcnt, int rendertype)
	{
		string sWrk = "";
		if (rendertype == 1)	{	// Render control start
			if (rowcnt == 0)
				sWrk += "<table class=\"" + EW_ITEM_TABLE_CLASSNAME + "\">"; 
			if (rowcnt % repeatcnt == 0)
				sWrk += "<tr>"; 
			sWrk += "<td>";
		} else if (rendertype == 2) {	// Render control end
			sWrk += "</td>";
			if (rowcnt % repeatcnt == repeatcnt - 1)	{
				sWrk += "</tr>";
			} else if (rowcnt == totcnt) {
				for (int i = rowcnt % repeatcnt + 1; i <= repeatcnt - 1; i++)
					sWrk += "<td>&nbsp;</td>";
				sWrk += "</tr>";
			}
			if (rowcnt == totcnt)
				sWrk += "</table>"; 
		}
		return sWrk;
	}

	// Truncate memo field based on specified length, string truncated to nearest space or CrLf
	public static string ew_TruncateMemo(string memostr, int ln, bool removehtml)
	{
		int i, j, k;
		string str;
		if (removehtml) {
			str = ew_RemoveHtml(memostr); // Remove HTML
		} else {
			str = memostr;
		}
		if (str.Length > 0 && str.Length > ln)	{
			k = 0;
			while (k >= 0 && k < str.Length) {
				i = str.IndexOf(" ", k);
				j = str.IndexOf("\r\n", k);
				if (i < 0 && j < 0)	{	// Unable to truncate
					return str;
				}	else	{	// Get nearest space or CrLf
					if (i > 0 && j > 0)	{
						k = (i < j) ? i : j; 
					} else if (i > 0) {
						k = i;
					} else if (j > 0) {
						k = j;
					}

					// Get truncated text
					if (k >= ln) {
						return str.Substring(0, k) + "...";
					}	else {
						k = k + 1;
					}
				}
			}
		}
		return str;
	}	

	// Remove Html tags from text
	public static string ew_RemoveHtml(string str)
	{
		return Regex.Replace(str, "<[^>]*>", string.Empty);
	}

	// Send email
	public static bool ew_SendEmail(string sFrEmail, string sToEmail, string sCcEmail, string sBccEmail, string sSubject, string sMail, string sFormat, string sCharset, string sAttachmentFileName, string sAttachmentContent)
	{
		System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
		if (ew_NotEmpty(sFrEmail))
			mail.From = new System.Net.Mail.MailAddress(sFrEmail);
		if (ew_NotEmpty(sToEmail)) {
			sToEmail = sToEmail.Replace(',', ';');
			string[] arTo = sToEmail.Split(new char[] {';'});
			foreach (string strTo in arTo)
				mail.To.Add(strTo);
		}
		if (ew_NotEmpty(sCcEmail)) {
			sCcEmail = sCcEmail.Replace(',', ';');
			string[] arCC = sCcEmail.Split(new char[] {';'});
			foreach (string strCC in arCC)
				mail.CC.Add(strCC);
		}
		if (ew_NotEmpty(sBccEmail)) {
			sBccEmail = sBccEmail.Replace(',', ';');
			string[] arBcc = sBccEmail.Split(new char[] {';'});
			foreach (string strBcc in arBcc)
				mail.Bcc.Add(strBcc);
		}
		mail.Subject = sSubject;
		mail.Body = sMail;
		mail.IsBodyHtml = ew_SameText(sFormat, "html");
		if (ew_NotEmpty(sCharset) && !ew_SameText(sCharset, "iso-8859-1"))
			mail.BodyEncoding = Encoding.GetEncoding(sCharset); 
		System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
		if (ew_NotEmpty(EW_SMTP_SERVER)) {
			smtp.Host = EW_SMTP_SERVER;
		} else {
			smtp.Host = "localhost";
		}
		if (EW_SMTP_SERVER_PORT > 0)
			smtp.Port = EW_SMTP_SERVER_PORT;
		if (ew_NotEmpty(EW_SMTP_SERVER_USERNAME) && ew_NotEmpty(EW_SMTP_SERVER_PASSWORD)) {
			System.Net.NetworkCredential smtpuser = new System.Net.NetworkCredential();
			smtpuser.UserName = EW_SMTP_SERVER_USERNAME;
			smtpuser.Password = EW_SMTP_SERVER_PASSWORD;
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = smtpuser;
		}
		if (ew_NotEmpty(sAttachmentFileName) && ew_NotEmpty(sAttachmentContent)) { // HTML
			byte[] arByte = mail.BodyEncoding.GetBytes(sAttachmentContent);
			MemoryStream stream = new MemoryStream(arByte);
			Attachment data = new Attachment(stream, new ContentType(MediaTypeNames.Text.Html));
			ContentDisposition disposition = data.ContentDisposition;
			disposition.FileName = sAttachmentFileName;
			mail.Attachments.Add(data);
		} else if (ew_NotEmpty(sAttachmentFileName)) { //URL
			Attachment data = new Attachment(sAttachmentFileName, new ContentType(MediaTypeNames.Text.Html));
			mail.Attachments.Add(data);
		}
		try {
			smtp.Send(mail);
			return true;
		}	catch (Exception e) {
			gsEmailErrDesc = e.ToString();
			if (EW_DEBUG_ENABLED) throw; 
			return false;
		}
	}

	// Send email
	public static bool ew_SendEmail(string sFrEmail, string sToEmail, string sCcEmail, string sBccEmail, string sSubject, string sMail, string sFormat, string sCharset)
	{
		return ew_SendEmail(sFrEmail, sToEmail, sCcEmail, sBccEmail, sSubject, sMail, sFormat, sCharset, "", "");
	}

	// Return path of the uploaded file
	//	Parameter: If PhyPath is true(1), return physical path on the server
	//	           If PhyPath is false(0), return relative URL
	public static string ew_UploadPathEx(bool PhyPath, string DestPath)
	{
		string Path;
		if (DestPath.StartsWith("~/")) DestPath = DestPath.Substring(2); 
		if (PhyPath) {
			Path = HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
			Path = ew_PathCombine(Path, DestPath.Replace("/", "\\"), PhyPath); 
		}	else	{
			Path = ew_AppPath();
			Path = ew_PathCombine(Path, DestPath, PhyPath); 
		}
		return ew_IncludeTrailingDelimiter(Path, PhyPath);
	}

	// Change the file name of the uploaded file
	public static string ew_UploadFileNameEx(string folder, string FileName)
	{
		string OutFileName;

		// By default, ewUniqueFileName() is used to get an unique file name.
		// Amend your logic here

		OutFileName = ew_UniqueFileName(folder, FileName);

		// Return computed output file name
		return OutFileName;
	}

	// Return path of the uploaded file
	// returns global upload folder, for backward compatibility only
	public static string ew_UploadPath(bool PhyPath)
	{
		return ew_UploadPathEx(PhyPath, EW_UPLOAD_DEST_PATH);
	}

	// Change the file name of the uploaded file
	// use global upload folder, for backward compatibility only
	public static string ew_UploadFileName(string FileName)
	{
		return ew_UploadFileNameEx(ew_UploadPath(true), FileName);
	}

	// Generate an unique file name (filename(n).ext)
	public static string ew_UniqueFileName(string folder, string FileName)
	{
		if (ew_Empty(FileName)) FileName = ew_DefaultFileName(); 
		if (FileName == ".") throw new Exception("Invalid file name: " + FileName); 
		if (ew_Empty(folder)) throw new Exception("Unspecified folder"); 
		string Ext = Path.GetExtension(FileName);
		string Name = Path.GetFileNameWithoutExtension(FileName);
		folder = ew_IncludeTrailingDelimiter(folder, true);
		if (!Directory.Exists(folder) && !ew_CreateFolder(folder)) {
			throw new Exception("Folder does not exist: " + folder);
		}
		int Index = 0;
		string Suffix = "";

		// Check to see if filename exists
		while (File.Exists(folder + Name + Suffix + Ext)) {
			Index = Index + 1;
			Suffix = "(" + Index + ")";
		}

		// Return unique file name
		return Name + Suffix + Ext;
	}

	// Generate an unique file name (filename(n).ext)
	public static string ew_UniqueFilename(string folder, string FileName)
	{
		return ew_UniqueFileName(folder, FileName);
	}

	// Create a default file name (yyyymmddhhmmss.bin)
	public static string ew_DefaultFileName()
	{
		DateTime DT = DateTime.Now;
		return DT.ToString("yyyyMMddHHmmss") + ".bin";
	}

	// Shortcut to Server.MapPath
	public static string ew_MapPath(string Path)
	{
		return HttpContext.Current.Server.MapPath(Path);
	}

	// Get path relative to application root
	public static string ew_ServerMapPath(string Path)
	{
		return ew_PathCombine(HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"], Path, true);
	}

	// Get path relative to a base path
	public static string ew_PathCombine(string BasePath, string RelPath, bool PhyPath)
	{
		int p2;
		int p1;
		string Path2;
		string Path;
		string Delimiter;
		BasePath = ew_RemoveTrailingDelimiter(BasePath, PhyPath);
		if (PhyPath) {
			Delimiter = "\\";
			RelPath = RelPath.Replace("/", "\\");
		}	else	{
			Delimiter = "/";
			RelPath = RelPath.Replace("\\", "/");
		}
		if (RelPath == "." | RelPath == "..") RelPath = RelPath + Delimiter; 
		p1 = RelPath.IndexOf(Delimiter);
		Path2 = "";
		while (p1 > -1) {
			Path = RelPath.Substring(0, p1 + 1);
			if (Path == Delimiter || Path == "." + Delimiter)	{

					// Skip
			} else if (Path == ".." + Delimiter) {
				p2 = BasePath.LastIndexOf(Delimiter);
				if (p2 > -1) BasePath = BasePath.Substring(0, p2); 
			}	else {
				Path2 += Path;
			}
			RelPath = RelPath.Substring(p1 + 1);
			p1 = RelPath.IndexOf(Delimiter);
		}
		return ew_IncludeTrailingDelimiter(BasePath, PhyPath) + Path2 + RelPath; 
	}

	// Remove the last delimiter for a path
	public static string ew_RemoveTrailingDelimiter(string Path, bool PhyPath)
	{
		string Delimiter;
		if (PhyPath) Delimiter = "\\"; 		else Delimiter = "/"; 
		while (Path.EndsWith(Delimiter)) {
			Path = Path.Substring(0, Path.Length - 1);
		}
		return Path;
	}

	// Include the last delimiter for a path
	public static string ew_IncludeTrailingDelimiter(string Path, bool PhyPath)
	{
		string Delimiter;
		Path = ew_RemoveTrailingDelimiter(Path, PhyPath);
		 Delimiter = (PhyPath) ? "\\" : "/"; 
		return Path + Delimiter;
	}

	// Write the paths for config/debug only
	public static void ew_WriteUploadPaths()
	{
		ew_Write("APPL_PHYSICAL_PATH = " + HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"] + "<br>");
		ew_Write("APPL_MD_PATH = " + HttpContext.Current.Request.ServerVariables["APPL_MD_PATH"] + "<br>");
	}

	// Get current page name
	public static string ew_CurrentPage()
	{
		return ew_GetPageName(HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"]);
	}

	// Get refer page name
	public static string ew_ReferPage()
	{
		return ew_GetPageName(HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
	}

	// Get page name
	public static string ew_GetPageName(string url)
	{
		if (ew_NotEmpty(url)) {
			if (url.Contains("?")) {
				url = url.Substring(0, url.LastIndexOf("?"));

				// Remove querystring first
			}
			return url.Substring(url.LastIndexOf("/") + 1);

			// Remove path
		}	else	{
			return "";
		}
	}

	// Get full URL
	public static string ew_FullUrl()
	{
		return ew_DomainUrl() + HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
	}

	// Third party files host // ASPX
	public static string ew_LibHost(bool local, string path) {
		if (local) { // Use local files
			return path; 
		} else { // Use files online
			if (ew_IsHttps()) {
				return "https://ajax.googleapis.com/ajax/libs/" + path;
			} else {
				return "http://ajax.googleapis.com/ajax/libs/" + path;
			}
		}
	}

	// YUI files host
	public static string ew_YuiHost() {
		return ew_LibHost(true, "yui290/"); // Use local files
	}

	// Check if HTTPS
	public static bool ew_IsHttps() {
		return !ew_SameText(HttpContext.Current.Request.ServerVariables["HTTPS"], "off") &&
			ew_NotEmpty(HttpContext.Current.Request.ServerVariables["HTTPS"]);
	}

	// Get domain URL
	public static string ew_DomainUrl()
	{
		bool bSSL = ew_IsHttps();
		string sUrl = (bSSL) ? "https": "http";
		string sPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
		string defPort = (bSSL) ? "443" : "80";
		sPort = (sPort == defPort) ? "" : ":" + sPort; 
		return sUrl + "://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + sPort;
	}

	// Get current URL
	public static string ew_CurrentUrl()
	{
		string s = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
		string q = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
		if (ew_NotEmpty(q))
			s += "?" + q;
		return s;
	}

	// Get application root path (relative to domain)
	public static string ew_AppPath()
	{
		string Path = HttpContext.Current.Request.ServerVariables["APPL_MD_PATH"];
		int pos = Path.IndexOf("Root", StringComparison.InvariantCultureIgnoreCase);
		if (pos > 0)
			Path = Path.Substring(pos + 4); 
		return Path;
	}

	// Convert to full URL
	public static string ew_ConvertFullUrl(string url)
	{
		if (ew_Empty(url))	{
			return "";
		} else if (url.Contains("://")) {
			return url;
		}	else	{
			string sUrl = ew_FullUrl();
			return sUrl.Substring(0, sUrl.LastIndexOf("/") + 1) + url;
		}
	}

	// Check if folder exists
	public static bool ew_FolderExists(string folder)
	{
		return Directory.Exists(folder);
	}

	// Check if file exists
	public static bool ew_FileExists(string folder, string fn)
	{
		return File.Exists(folder + fn);
	}

	// Delete file
	public static void ew_DeleteFile(string FilePath)
	{
		File.Delete(FilePath);
	}

	// Rename file
	public static void ew_RenameFile(string OldFile, string NewFile)
	{
		File.Move(OldFile, NewFile);
	}

	// Copy file
	public static void ew_CopyFile(string SrcFile, string DestFile)
	{
		File.Copy(SrcFile, DestFile);
	}

	// Create folder
	public static bool ew_CreateFolder(string folder)
	{
		try {
			DirectoryInfo di = Directory.CreateDirectory(folder);
			return (di != null);
		}	catch {
			return false;
		}
	}

	// Check if field exists in a data reader
	public static bool ew_InDataReader(ref SqlDataReader dr, string FldName)
	{
		try {
			return (dr.GetOrdinal(FldName) >= 0);
		} catch {
			return false;
		}
	}

	// Calculate Hash for recordset // ASP.NET
	public static string ew_GetFldHash(object Value, int FldType) 
	{
		return MD5(ew_GetFldValueAsString(Value, FldType));
	}

	// Get field value as string
	public static string ew_GetFldValueAsString(object Value, int FldType)
	{
		if (Convert.IsDBNull(Value)) {
			return "";
		} else if (FldType == EW_DATATYPE_BLOB) {
			return ew_ByteToString((byte[])Value, EW_BLOB_FIELD_BYTE_COUNT);
		} else {
			return Convert.ToString(Value);
		}
	}

	// Convert byte to string
	public static string ew_ByteToString(object b, int l)
	{
		if (EW_BLOB_FIELD_BYTE_COUNT > 0) {
			return BitConverter.ToString((byte[])b, 0, EW_BLOB_FIELD_BYTE_COUNT);
		} else {
			return BitConverter.ToString((byte[])b);
		}
	}

	// Create temp image file from binary data
	public static string ew_TmpImage(byte[] filedata) {
		string folder = ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH);
		string f = folder + Path.GetRandomFileName();
		MemoryStream ms = new MemoryStream(filedata);
		System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
		if (img.RawFormat.Equals(ImageFormat.Gif)) {
			f = Path.ChangeExtension(f, ".gif");
		} else if (img.RawFormat.Equals(ImageFormat.Jpeg)) {
			f = Path.ChangeExtension(f, ".jpg");
		} else if (img.RawFormat.Equals(ImageFormat.Png)) {
			f = Path.ChangeExtension(f, ".png");
		} else if (img.RawFormat.Equals(ImageFormat.Bmp)) {
			f = Path.ChangeExtension(f, ".bmp");
		} else {
			return "";
		}
		img.Save(f);
		gTmpImages.Add(f);
		return f;
	}

	// Delete temp images
	public static void ew_DeleteTmpImages() {
		foreach (string tmpimage in gTmpImages)
			ew_DeleteFile(tmpimage);
	}

	// Create temp file
	public static string ew_TmpFile(string afile) {
		if (File.Exists(afile)) { // Copy only
			string folder = ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH);
			string f = folder + Path.GetRandomFileName();
			string ext = Path.GetExtension(afile);
			if (ew_NotEmpty(ext))
				f += ext;
			ew_CopyFile(afile, f);
			string tmpimage = Path.GetFileName(f);
			gTmpImages.Add(tmpimage);
			return EW_UPLOAD_DEST_PATH + tmpimage;
		} else {
			return "";
		}
	}

	//
	//  Form object class
	//
	public class cFormObj
	{		

		public int Index = 0; // Index to handle multiple form elements

		public bool UseSession = false; // ASPX

		private NameValueCollection fvs = null; // ASPX

		// Constructor // ASPX
		public cFormObj()
		{
			if (HttpContext.Current.Session["EW_FORM_VALUES"] != null)
				fvs = (NameValueCollection)HttpContext.Current.Session["EW_FORM_VALUES"];	
		}

		// Get form element name based on index
		public string GetIndexedName(string name)
		{
			int Pos;
			if (Index <= 0)	{
				return name;
			}	else {
				Pos = name.IndexOf("_");
				if (Pos == 1 || Pos == 2)	{
					return name.Substring(0, Pos) + Index + name.Substring(Pos);
				}	else {
					return name;
				}
			}
		}

		// Has value for form element
		public bool HasValue(string name) {
			string wrkname = GetIndexedName(name);
			if (UseSession) { // ASPX
				if (fvs != null) {
					return (fvs[wrkname] != null);
				} else {
					return false;
				}
			} else {
				return (HttpContext.Current.Request.Form[wrkname] != null);
			}	
		}

		// Get value for form element
		public string GetValue(string name)
		{
			string wrkname = GetIndexedName(name);
			if (UseSession) { // ASPX
				if (fvs != null) {
					return (fvs[wrkname] != null) ? fvs[wrkname] : "";
				} else {
					return "";
				}
			} else {
				return ew_Post(wrkname);
			}
		}

		// Get upload file size
		public long GetUploadFileSize(string name)
		{
			string wrkname = GetIndexedName(name);
			if (HttpContext.Current.Request.Files[wrkname] != null)	{
				return HttpContext.Current.Request.Files[wrkname].ContentLength;
			}	else	{
				return -1;
			}
		}

		// Get upload file name
		public string GetUploadFileName(string name)
		{
			string wrkname = GetIndexedName(name);
			if (HttpContext.Current.Request.Files[wrkname] != null) {
				string FileName = HttpContext.Current.Request.Files[wrkname].FileName;
				return Path.GetFileName(FileName);
			}	else	{
				return "";
			}
		}

		// Get file content type
		public string GetUploadFileContentType(string name)
		{
			string wrkname = GetIndexedName(name);
			if (HttpContext.Current.Request.Files[wrkname] != null) {
				return HttpContext.Current.Request.Files[wrkname].ContentType;
			}	else {
				return "";
			}
		}

		// Get upload file data
		public object GetUploadFileData(string name)
		{
			string wrkname = GetIndexedName(name);
			HttpPostedFile file = HttpContext.Current.Request.Files[wrkname];
			if (file != null && file.ContentLength > 0)	{
				int filelength = file.ContentLength;
				byte[] bindata = new byte[filelength];
				Stream fs = file.InputStream;
				fs.Seek(0, SeekOrigin.Begin);
				fs.Read(bindata, 0, filelength);				
				return bindata;
			}	else {
				return System.DBNull.Value;
			}
		}

		// Get file image width
		public int GetUploadImageWidth(string name)
		{
			string wrkname = GetIndexedName(name);
			HttpPostedFile file = HttpContext.Current.Request.Files[wrkname];
			if (file != null && file.ContentLength > 0) {
				try {
					System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
					return Convert.ToInt32(img.PhysicalDimension.Width);
				}	catch {
					return -1;
				}
			}	else {
				return -1;
			}
		}

		// Get file image height
		public int GetUploadImageHeight(string name)
		{
			string wrkname = GetIndexedName(name);
			HttpPostedFile file = HttpContext.Current.Request.Files[wrkname];
			if (file != null && file.ContentLength > 0) {
				try {
					System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
					return Convert.ToInt32(img.PhysicalDimension.Height);
				}	catch {
					return -1;
				}
			}	else	{
				return -1;
			}
		}
	}

	// Resize binary to thumbnail
	public static bool ew_ResizeBinary(ref byte[] filedata, ref int width, ref int height, int interpolation)
	{
		return true; // No resize
	}

	// Resize file to thumbnail file
	public static bool ew_ResizeFile(string fn, string tn, ref int width, ref int height, int interpolation)
	{
		try {
			if (File.Exists(fn)) {
				File.Copy(fn, tn); // Copy only
				return true;
			}
			return false;
		}	catch {
			if (EW_DEBUG_ENABLED) throw; 
			return false;
		}
	}

	// Resize file to binary
	public static byte[] ew_ResizeFileToBinary(string fn, ref int width, ref int height, int interpolation)
	{
		try {
			if (File.Exists(fn)) {
				FileInfo oFile = new FileInfo(fn);
				FileStream fs = oFile.OpenRead();
				long lBytes = fs.Length;
				if (lBytes > 0)	{
					byte[] fileData = new byte[lBytes];
					fs.Read(fileData, 0, (int)lBytes); // Read the file into a byte array
					fs.Close();
					fs.Dispose();
					return fileData;
				}
			}
			return null;
		}	catch {
			if (EW_DEBUG_ENABLED) throw; 
			return null;
		}
	}

	// Menu class
	public class cMenu : AspNetMakerBase
	{

		public object Id;

		public bool IsRoot;

		public ArrayList ItemData = new ArrayList(); // ArrayList of cMenuItem

		// Constructor
		public cMenu(object aId, bool aRoot)
		{
			Id = aId;
			IsRoot = aRoot;
		}

		// Add a menu item
		public void AddMenuItem(int aId, string aText, string aUrl, int aParentId, string aSrc, bool aAllowed, bool aGroupTitle)
		{
			cMenuItem oParentMenu = null;
			cMenuItem item = new cMenuItem(aId, aText, aUrl, aParentId, aSrc, aAllowed, aGroupTitle);
			item.Page = m_Page;
			if (!ParentPage.MenuItem_Adding(ref item))
				return;
			if (item.ParentId < 0) {
				AddItem(ref item);
			}	else {
				if (FindItem(item.ParentId, ref oParentMenu))
					oParentMenu.AddItem(ref item);
			}
		}

		// Get menu item count
		public int Count()
		{
			return ItemData.Count;
		}

		// Add item to internal ArrayList
		public void AddItem(ref cMenuItem item)
		{
			ItemData.Add(item);
		}

		// Clear all menu items
		public void Clear() {
			ItemData.Clear();
		}

		// Find item
		public bool FindItem(int id, ref cMenuItem outitem)
		{
			bool result = false;
			cMenuItem item;
			result = false;
			for (int i = 0; i <= ItemData.Count - 1; i++) {
				item = (cMenuItem)ItemData[i];
				if (item.Id == id) {
					outitem = item;
					return true;
				} else if (item.SubMenu != null) {
					if (item.SubMenu.FindItem(id, ref outitem))
						return true;
				}
			}
			return result;
		}

		// Move item to position
		public void MoveItem(string Text, int Pos)
		{
			int oldpos = 0;
			int newpos = Pos;
			bool bfound = false;
			if (Pos < 0) {
				Pos = 0;
			} else if (Pos >= ItemData.Count) {
				Pos = ItemData.Count - 1;
			}
			cMenuItem CurItem = null;
			foreach (cMenuItem item in ItemData) {
				if (ew_SameStr(item.Text, Text)) {
					CurItem = item;
					break;
				}
			}
			if (CurItem != null) {
				bfound = true;
				oldpos = ItemData.IndexOf(CurItem);
			}	else {
				bfound = false;
			}
			if (bfound && Pos != oldpos) {
				ItemData.RemoveAt(oldpos); // Remove old item
				if (oldpos < Pos)
					newpos -= 1; // Adjust new position
				ItemData.Insert(newpos, CurItem); // Insert new item
			}
		}

		// Check if a menu item should be shown
		public bool RenderItem(cMenuItem item) {
			if (item.SubMenu != null) {
				foreach (cMenuItem subitem in item.SubMenu.ItemData) {
					if (item.SubMenu.RenderItem(subitem))
						return true;
				}
			}
			return (item.Allowed && ew_NotEmpty(item.Url));
		}

		// Check if this menu should be rendered
		public bool RenderMenu() {
			foreach (cMenuItem item in ItemData) {
				if (RenderItem(item))
					return true;
			}
			return false;
		}

		// Render the menu
		public void Render() {
			Render(false);
		}

		// Render the menu
		public string Render(bool ret) {
			if (!RenderMenu())
				return "";
			string str = "<div";
			if (ew_NotEmpty(Id))	{
				if (Information.IsNumeric(Id))	{
					str += " id=\"menu_" + Id + "\"";
				}	else {
					str += " id=\"" + Id + "\"";
				}
			}
			str += " class=\"" + ((IsRoot) ? EW_MENUBAR_CLASSNAME : EW_MENU_CLASSNAME) + "\">";
			str += "<div class=\"bd" + ((IsRoot) ? " first-of-type" : "") + "\">\n";
			bool gopen = false; // Group open status
			int gcnt = 0; // Group count
			int i = 0; // Menu item count
			string classfot = " class=\"first-of-type\"";
			foreach (cMenuItem item in ItemData) {
				if (RenderItem(item)) {	
					i++;

					// Begin a group
					if (i == 1 && !item.GroupTitle) {
						gcnt++;
						str += "<ul " + classfot + ">\n";
						gopen = true;
					}
					string aclass = (IsRoot) ? EW_MENUBAR_ITEM_LABEL_CLASSNAME : EW_MENU_ITEM_LABEL_CLASSNAME;
					string liclass = (IsRoot) ? EW_MENUBAR_ITEM_CLASSNAME : EW_MENU_ITEM_CLASSNAME;
					if (item.GroupTitle && ew_NotEmpty(EW_MENU_ITEM_CLASSNAME)) { // Group title
						gcnt++;
						if (i > 1 && gopen) {
							str += "</ul>\n"; // End last group
							gopen = false;
						}

						// Begin a new group with title
						if (ew_NotEmpty(item.Text))
							str += "<h6" + ((gcnt == 1) ? classfot : "") + ">" + item.Text + "</h6>\n";
						str += "<ul" + ((gcnt == 1) ? classfot : "") + ">\n";
						gopen = true;
						if (item.SubMenu != null) {
							foreach (cMenuItem subitem in item.SubMenu.ItemData) {
								if (RenderItem(subitem))
									str += subitem.Render(aclass, liclass) + "\n"; // Create <LI>
							}
						}
						str += "</ul>\n"; // End the group
						gopen = false;
					} else { // Menu item
						if (!gopen) { // Begin a group if no opened group
							gcnt++;
							str += "<ul" + ((gcnt == 1) ? classfot : "") + ">\n";
							gopen = true;
						}
						if (IsRoot && i == 1) // For horizontal menu
							liclass += " first-of-type";
						str += item.Render(aclass, liclass) + "\n"; // Create <LI>
					}
				}
			}
			if (gopen)
				str += "</ul>\n"; // End last group
			str += "</div></div>\n";
			if (ret) // Return as string
				return str;
			ew_Write(str); // Output
			return "";
		}
	}

	// Menu item class
	public class cMenuItem : AspNetMakerBase
	{

		public int Id;

		public string Text;

		public string Url;

		public int ParentId;

		public cMenu SubMenu = null;

		public string Source = "";

		public bool Allowed = true;

		public string Target = "";

		public bool GroupTitle = false;

		// Constructor
		public cMenuItem(int aId, string aText, string aUrl, int aParentId, string aSource, bool aAllowed, bool aGroupTitle)
		{
			Id = aId;
			Text = aText;
			Url = aUrl;
			ParentId = aParentId;
			Source = aSource;
			Allowed = aAllowed;
			GroupTitle = aGroupTitle;			
		}

		// Add submenu item
		public void AddItem(ref cMenuItem item)
		{
			if (SubMenu == null)	{
				SubMenu = new cMenu(Id, false);
				SubMenu.ParentPage = this.ParentPage;
			}
			SubMenu.AddItem(ref item);
		}

		// Render
		public string Render(string aclass, string liclass) {

			// Create <A>
			Hashtable attrs = new Hashtable(); 
			attrs.Add("class", aclass);
			attrs.Add("href", Url);
			attrs.Add("target", Target);
			string innerhtml = ew_HtmlElement("a", attrs, Text, true);
			if (SubMenu != null)
				innerhtml += SubMenu.Render(true);

			// Create <LI>
			attrs.Clear();
			attrs.Add("class", liclass);
			return ew_HtmlElement("li", attrs, innerhtml, true);
		}

		// Output as string
		public string AsString()	{
			string OutStr = "{ Id: " + Id + ", Text: " + Text + ", Url: " + Url + ", ParentId: " + ParentId;
			if (SubMenu == null)	{
				OutStr += ", SubMenu: (Null)";
			}	else	{
				OutStr += ", SubMenu: (Object)";
			}
			OutStr += ", Source: " + Source;
			OutStr += ", Target: " + Target; 
			OutStr += ", Allowed: " + Allowed;
			OutStr += ", GroupTitle: " + GroupTitle;
			return OutStr + " }" + "<br />";
		}
	}

	// Is Admin
	public bool IsAdmin()
	{
		if (Security != null)	{
			return Security.IsAdmin();
		} else {
			return (ew_ConvertToInt(ew_Session[EW_SESSION_USER_LEVEL_ID]) == -1 || ew_ConvertToInt(ew_Session[EW_SESSION_SYS_ADMIN]) == 1);
		}
	}

	// Allow list
	public bool AllowList(string TableName)
	{
		if (Security != null)	{
			return Security.AllowList(TableName);
		}	else {
			return true;
		}
	}

	// Allow add
	public bool AllowAdd(string TableName)
	{
		if (Security != null)	{
			return Security.AllowAdd(TableName);
		}	else	{
			return true;
		}
	}	

	//		
	// Validation functions
	//
	// Check date format
	// format: std/stdshort/us/usshort/euro/euroshort
	public static bool ew_CheckDateEx(string value, string format, string sep)
	{
		if (ew_Empty(value)) return true; 
		while (value.Contains("  "))
			value = value.Replace("  ", " ");
		value = value.Trim();
		string[] arDT;
		string[] arD;
		string pattern = "";
		string sYear = "";
		string sMonth = "";
		string sDay = "";
		arDT = value.Split(new char[] {' '});
		if (arDT.Length > 0)	{
			Match m;
			if ((m = Regex.Match(arDT[0], @"^([0-9]{4})/([0][1-9]|[1][0-2])/([0][1-9]|[1|2][0-9]|[3][0|1])$")) != null && m.Success) { // Accept yyyy/mm/dd
				sYear = m.Groups[1].Value;
				sMonth = m.Groups[2].Value;
				sDay = m.Groups[3].Value;
			} else {
				string wrksep = "\\" + sep;
				switch (format) {
					case "std":
						pattern = "^([0-9]{4})" + wrksep + "([0]?[1-9]|[1][0-2])" + wrksep + "([0]?[1-9]|[1|2][0-9]|[3][0|1])$";
						break;
					case "us":
						pattern = "^([0]?[1-9]|[1][0-2])" + wrksep + "([0]?[1-9]|[1|2][0-9]|[3][0|1])" + wrksep + "([0-9]{4})$";
						break;
					case "euro":
						pattern = "^([0]?[1-9]|[1|2][0-9]|[3][0|1])" + wrksep + "([0]?[1-9]|[1][0-2])" + wrksep + "([0-9]{4})$";
						break;
				}
				Regex re = new Regex(pattern);
				if (!re.IsMatch(arDT[0])) return false; 
				arD = arDT[0].Split(new char[] {Convert.ToChar(sep)});
				switch (format) {
					case "std":
					case "stdshort":
						sYear = ew_UnformatYear(arD[0]);
						sMonth = arD[1];
						sDay = arD[2];
						break;
					case "us":
					case "usshort":
						sYear = ew_UnformatYear(arD[2]);
						sMonth = arD[0];
						sDay = arD[1];
						break;
					case "euro":
					case "euroshort":
						sYear = ew_UnformatYear(arD[2]);
						sMonth = arD[1];
						sDay = arD[0];
						break;
				}
			}
			if (!ew_CheckDay(ew_ConvertToInt(sYear), ew_ConvertToInt(sMonth), ew_ConvertToInt(sDay))) return false; 
		}
		if (arDT.Length > 1 && !ew_CheckTime(arDT[1])) return false; 
		return true;
	}

	// Unformat 2 digit year to 4 digit year
	public static string ew_UnformatYear(object year) {
		string yr = Convert.ToString(year);
		if (Information.IsNumeric(yr) && yr.Length == 2) {
			if (ew_ConvertToInt(yr) > EW_UNFORMAT_YEAR)
				return "19" + yr;
			else
				return "20" + yr;
		} else {
			return yr;
		}
	}

	// Check Date format (yyyy/mm/dd)
	public static bool ew_CheckDate(string value)
	{
		return ew_CheckDateEx(value, "std", EW_DATE_SEPARATOR);
	}

	// Check Date format (yy/mm/dd)
	public static bool ew_CheckShortDate(string value) {
		return ew_CheckDateEx(value, "stdshort", EW_DATE_SEPARATOR);
	}

	// Check US Date format (mm/dd/yyyy)
	public static bool ew_CheckUSDate(string value)
	{
		return ew_CheckDateEx(value, "us", EW_DATE_SEPARATOR);
	}

	// Check US Date format (mm/dd/yy)
	public static bool ew_CheckShortUSDate(string value) {
		return ew_CheckDateEx(value, "usshort", EW_DATE_SEPARATOR);
	}

	// Check Euro Date format (dd/mm/yyyy)
	public static bool ew_CheckEuroDate(string value)
	{
		return ew_CheckDateEx(value, "euro", EW_DATE_SEPARATOR);
	}

	// Check Euro Date format (dd/mm/yy)
	public static bool ew_CheckShortEuroDate(string value) {
		return ew_CheckDateEx(value, "euroshort", EW_DATE_SEPARATOR);
	}

	// Check day
	public static bool ew_CheckDay(int checkYear, int checkMonth, int checkDay)
	{
		int maxDay = 31;
		if (checkMonth == 4 || checkMonth == 6 || checkMonth == 9 || checkMonth == 11) {
			maxDay = 30;
		} else if (checkMonth == 2) {
			if (checkYear % 4 > 0)	{
				maxDay = 28;
			} else if (checkYear % 100 == 0 && checkYear % 400 > 0) {
				maxDay = 28;
			}	else	{
				maxDay = 29;
			}
		}
		return ew_CheckRange(Convert.ToString(checkDay), 1, maxDay);
	}

	// Check integer
	public static bool ew_CheckInteger(string value)
	{
		if (ew_Empty(value)) return true; 
		Regex re = new Regex("^\\-?\\+?[0-9]+$");
		return re.IsMatch(value);
	}

	// Check number range
	public static bool ew_NumberRange(string value, object min, object max)
	{
		if ((min != null && Convert.ToDouble(value) < Convert.ToDouble(min)) || (max != null && Convert.ToDouble(value) > Convert.ToDouble(max)))	{
			return false;
		}
		return true;
	}

	// Check number
	public static bool ew_CheckNumber(string value)
	{
		if (ew_Empty(value)) return true; 
		return Information.IsNumeric(Strings.Trim(value));
	}

	// Check range
	public static bool ew_CheckRange(string value, object min, object max)
	{
		if (ew_Empty(value)) return true; 
		if (!ew_CheckNumber(value)) return false; 
		return ew_NumberRange(value, min, max);
	}

	// Check time
	public static bool ew_CheckTime(string value)
	{
		if (ew_Empty(value)) return true;
		string[] Values = value.Split(new Char[] {'.', ' '}); 
		Regex re = new Regex("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]");
		return re.IsMatch(Values[0]);
	}

	// Check US phone number
	public static bool ew_CheckPhone(string value)
	{
		if (ew_Empty(value)) return true; 
		Regex re = new Regex("^\\(\\d{3}\\) ?\\d{3}( |-)?\\d{4}|^\\d{3}( |-)?\\d{3}( |-)?\\d{4}");
		return re.IsMatch(value);
	}

	// Check US zip code
	public static bool ew_CheckZip(string value)
	{
		if (ew_Empty(value)) return true; 
		Regex re = new Regex("^\\d{5}|^\\d{5}-\\d{4}");
		return re.IsMatch(value);
	}

	// Check credit card
	public static bool ew_CheckCreditCard(string value)
	{
		if (ew_Empty(value)) return true; 
		Hashtable creditcard = new Hashtable();
		creditcard.Add("visa", "^4\\d{3}[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}");
		creditcard.Add("mastercard", "^5[1-5]\\d{2}[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}");
		creditcard.Add("discover", "^6011[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}");
		creditcard.Add("amex", "^3[4,7]\\d{13}");
		creditcard.Add("diners", "^3[0,6,8]\\d{12}");
		creditcard.Add("bankcard", "^5610[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}");
		creditcard.Add("jcb", "^[3088|3096|3112|3158|3337|3528]\\d{12}");
		creditcard.Add("enroute", "^[2014|2149]\\d{11}");
		creditcard.Add("switch", "^[4903|4911|4936|5641|6333|6759|6334|6767]\\d{12}");
		Regex re;
		foreach (DictionaryEntry de in creditcard) {
			re = new Regex(Convert.ToString(de.Value));
			if (re.IsMatch(value))
				return ew_CheckSum(value);
		}
		return false;
	}

	// Check sum
	public static bool ew_CheckSum(string value)
	{
		int checksum;
		byte digit;
		value = value.Replace("-", "");
		value = value.Replace(" ", "");
		checksum = 0;
		for (int i = 2 - (value.Length % 2); i <= value.Length; i += 2) {
			checksum = checksum + Convert.ToByte(value[i - 1]);
		}
		for (int i = (value.Length % 2) + 1; i <= value.Length; i += 2) {
			digit = Convert.ToByte(Convert.ToByte(value[i - 1]) * 2);
			checksum = checksum + ((digit < 10) ? digit : (digit - 9));
		}
		return (checksum % 10 == 0);
	}

	// Check US social security number
	public static bool ew_CheckSSC(string value)
	{
		if (ew_Empty(value)) return true; 
		Regex re = new Regex("^(?!000)([0-6]\\d{2}|7([0-6]\\d|7[012]))([ -]?)(?!00)\\d\\d\\3(?!0000)\\d{4}");
		return re.IsMatch(value);
	}

	// Check email
	public static bool ew_CheckEmail(string value)
	{
		if (ew_Empty(value)) return true; 
		Regex re = new Regex("^[A-Za-z0-9\\._\\-+]+@[A-Za-z0-9_\\-+]+(\\.[A-Za-z0-9_\\-+]+)+");
		return re.IsMatch(value);
	}	

	// Check emails
	public static bool ew_CheckEmailList(string value, int cnt)
	{
		if (ew_Empty(value)) return true; 
		string emailList = value.Replace(",", ";");
		string[] arEmails = emailList.Split(new char[] {';'});
		if (arEmails.Length > cnt && cnt > 0)
			return false;
		foreach (string email in arEmails) {
			if (!ew_CheckEmail(email))
				return false;
		}
		return true;
	}

	// Check GUID
	public static bool ew_CheckGUID(string value)
	{
		if (ew_Empty(value)) return true; 
		Regex re1 = new Regex("^{{1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}}{1}");
		Regex re2 = new Regex("^([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}");
		return re1.IsMatch(value) || re2.IsMatch(value);
	}

	// Check file extension
	public static bool ew_CheckFileType(string value)
	{
		if (ew_Empty(value) || ew_Empty(EW_UPLOAD_ALLOWED_FILE_EXT)) return true; 
		string extension = Path.GetExtension(value).Substring(1);
		string[] allowExt = EW_UPLOAD_ALLOWED_FILE_EXT.Split(new char[] {','});
		foreach (string ext in allowExt) {
			if (ew_SameText(ext, extension)) return true; 
		}
		return false;
	}

	// Check by regular expression
	public static bool ew_CheckByRegEx(string value, string pattern)
	{
		if (ew_Empty(value)) return true; 
		return Regex.IsMatch(value, pattern);
	}

	// Check by regular expression
	public static bool ew_CheckByRegEx(string value, string pattern, RegexOptions options)
	{
		if (ew_Empty(value)) return true; 
		return Regex.IsMatch(value, pattern, options);
	}

	// Encrypt password
	public static string ew_EncryptPassword(string input) {
		return MD5(input);
	}

	// Compare password
	public static bool ew_ComparePassword(string pwd, string input) {
		if (EW_CASE_SENSITIVE_PASSWORD) {
			if (EW_ENCRYPTED_PASSWORD) {
				return (pwd == ew_EncryptPassword(input));
			} else {
				return ew_SameStr(pwd, input);
			}
		} else {
			if (EW_ENCRYPTED_PASSWORD) {
				return (pwd == ew_EncryptPassword(input.ToLower()));
			} else {
				return ew_SameText(pwd, input);
			}
		}
	}	

	// MD5
	public static string MD5(string InputStr)
	{
		MD5CryptoServiceProvider Md5Hasher = new MD5CryptoServiceProvider();
		byte[] Data = Md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(InputStr));
		StringBuilder sBuilder = new StringBuilder();
		for (int i = 0; i <= Data.Length - 1; i++) {
			sBuilder.Append(Data[i].ToString("x2"));
		}
		return sBuilder.ToString();
	}

	// CRC32
	public static uint CRC32(string InputStr) {
		byte[] bytes = Encoding.Unicode.GetBytes(InputStr);
		uint crc = 0xffffffff;
		uint poly	=	0xedb88320;
		uint[] table = new uint[256];
		uint temp = 0;
		for (uint i = 0; i < table.Length; ++i) {
			temp = i;
			for (int j = 8; j > 0; --j) {
				if ((temp & 1) == 1) {
					temp = (uint)((temp >> 1) ^ poly);
				} else {
					temp >>= 1;
				}
			}
			table[i] = temp;
		}
		for (int i = 0; i < bytes.Length; ++i) {
			byte index = (byte)(((crc) & 0xff) ^ bytes[i]);
			crc = (uint)((crc >> 8) ^ table[index]);
		}
		return ~crc;
	}

	// Invoke method with no parameter
	public object ew_InvokeMethod(string name, object[] parameters)
	{
		MethodInfo mi = typeof(AspNetMaker9_ControlVehicular).GetMethod(name);
		if (mi != null)	{
			return mi.Invoke(this, parameters);
		}	else {
			return false;
		}
	}	

	// Get static/constant field value
	public static object ew_GetFieldValue(string name)
	{
		FieldInfo fi = typeof(AspNetMaker9_ControlVehicular).GetField(name);
		return (fi != null) ? fi.GetValue(null) : null;		
	}

	// Get table names
	public static void GetTableNames(ref string[] TableNames)
	{
		TableNames = (string[])EW_USER_LEVEL_TABLE_NAME.Clone();
		object arRptTblName = ew_GetFieldValue("EWRPT_USER_LEVEL_TABLE_NAME");		
		if (arRptTblName != null && Information.IsArray(arRptTblName)) {
			string[] arwrk = (string[])((string[])arRptTblName).Clone();
			int arwrkcnt = arwrk.Length;
			int len = TableNames.Length;			
			Array.Resize(ref TableNames, len + arwrk.Length);
			arwrk.CopyTo(TableNames, len); 		
		}
	}

	// Get table captions
	public static void GetTableCaptions(ref string[] TableCaptions)
	{
		TableCaptions = (string[])EW_USER_LEVEL_TABLE_CAPTION.Clone();
		object arRptTblCaption = ew_GetFieldValue("EWRPT_USER_LEVEL_TABLE_CAPTION");		
		if (arRptTblCaption != null && Information.IsArray(arRptTblCaption)) {
			string[] arwrk = (string[])((string[])arRptTblCaption).Clone();
			int len = TableCaptions.Length;
			Array.Resize(ref TableCaptions, len + arwrk.Length);
			arwrk.CopyTo(TableCaptions, len); 		
		}
	}

	// Get table vars
	public static void GetTableVars(ref string[] TableVars)
	{
		TableVars = (string[])EW_USER_LEVEL_TABLE_VAR.Clone();
		object arRptTblVar = ew_GetFieldValue("EWRPT_USER_LEVEL_TABLE_VAR");		
		if (arRptTblVar != null && Information.IsArray(arRptTblVar)) {
			string[] arwrk = (string[])((string[])arRptTblVar).Clone();
			int len = TableVars.Length;			
			Array.Resize(ref TableVars, len + arwrk.Length);
			arwrk.CopyTo(TableVars, len); 		
		}
	}

	// Check if object is ArrayList
	public static bool ew_IsArrayList(object obj)
	{
		return (obj != null) && (obj.GetType().ToString() == "System.Collections.ArrayList");
	}

	// check if a value is in a string array
	public static bool ew_Contains(string str, string[] ar)
	{
		return (new List<string>(ar)).Contains(str); 
	}

	// check if a value is in an integer array
	public static bool ew_Contains(int i, int[] ar)
	{
		return (new List<int>(ar)).Contains(i); 
	}

	// Global random
	private static Random GlobalRandom = new Random();

	// Get a random number
	public static int ew_Random()
	{	
		lock (GlobalRandom) {
			Random NewRandom = new Random(GlobalRandom.Next());
			return NewRandom.Next();
		}
	}

	// Get query string value
	public static string ew_Get(string name)
	{
		if (HttpContext.Current.Request.QueryString[name] != null)	{
			return HttpContext.Current.Request.QueryString[name];
		}	else	{
			return "";
		}
	}

	// Get form value
	public static string ew_Post(string name)
	{
		if (HttpContext.Current.Request.Form[name] != null)	{
			return HttpContext.Current.Request.Form[name];
		}	else	{
			return "";
		}
	}

	// Get/set session values
	public static cSession ew_Session = new cSession();

	public class cSession
	{

		public object this[string name] {
			get { return HttpContext.Current.Session[name]; }
			set { HttpContext.Current.Session[name] = value; }
		}
	}

	// Get/set project cookie
	public static cCookie ew_Cookie = new cCookie();

	public class cCookie
	{

		public string this[string name] {
			get {
				if (HttpContext.Current.Request.Cookies[EW_PROJECT_NAME] != null)		{
					return HttpContext.Current.Request.Cookies[EW_PROJECT_NAME][name];
				}	else	{
					return "";
				}
			}
			set {
				HttpCookie c;
				if (HttpContext.Current.Request.Cookies[EW_PROJECT_NAME] != null)	{
					c = HttpContext.Current.Request.Cookies[EW_PROJECT_NAME];
				}	else {
					c = new HttpCookie(EW_PROJECT_NAME);
				}
				c.Values[name] = value;
				c.Path = ew_AppPath();
				c.Expires = EW_COOKIE_EXPIRY_TIME;
				HttpContext.Current.Response.Cookies.Add(c);				
			}
		}
	}

	// Response.Write
	public static void ew_Write(object value) {
		HttpContext.Current.Response.Write(value);
	}

	// Response.End
	public static void ew_End() {
		HttpContext.Current.Response.End();
	}

	// Write HTTP header
	public static void ew_Header(bool cache) {
		if (cache || !cache && ew_IsHttps() && ew_NotEmpty(gsExport) && gsExport != "print") { // Allow cache
			ew_AddHeader("Cache-Control", "private, must-revalidate"); // HTTP/1.1
		} else { // No cache
			HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
		}

		// Note: Charset not required. ASP.NET will output the header according to page directive.
//		if (ew_NotEmpty(charset)) // Charset
//			ew_AddHeader("Content-Type", "text/html; charset=" + charset);

	}

	// Add HTTP header
	public static void ew_AddHeader(string name, string value) {
		if (ew_NotEmpty(name)) // value can be empty
			HttpContext.Current.Response.AddHeader(name, value);
	}

	// check if allow add/delete row
	public static bool ew_AllowAddDeleteRow() {
		string[] ua = ew_UserAgent();
		return (ua.Length >= 2 && (ua[0] != "IE" || ew_ConvertToInt(ua[1]) > 5));
	}

	// Get browser type and version
	public static string[] ew_UserAgent() {
		string browser, browser_version; 
		string useragent = ew_ServerVar("HTTP_USER_AGENT");
		Match m;		
		if ((m = Regex.Match(useragent, @"MSIE ([0-9].[0-9]{1,2})")) != null && m.Success) {
			browser = "IE";
			browser_version = m.Groups[1].Value;
		} else if ((m = Regex.Match(useragent, @"Firefox/([0-9\.]+)")) != null && m.Success) {
			browser = "Firefox";
			browser_version = m.Groups[1].Value;
		} else if ((m = Regex.Match(useragent, @"Opera/([0-9].[0-9]{1,2})")) != null && m.Success) {
			browser = "Opera";
			browser_version = m.Groups[1].Value;
		} else if ((m = Regex.Match(useragent, @"Chrome/([0-9\.]+)")) != null && m.Success) {
			browser = "Chrome";
			browser_version = m.Groups[1].Value;
		} else if ((m = Regex.Match(useragent, @"Version/([0-9\.]+)")) != null && m.Success && Regex.IsMatch(useragent, @"Safari/")) {
			browser = "Safari";
			browser_version = m.Groups[1].Value;
		} else { // browser not recognized
			browser = "Other";
			browser_version = "0";
		}
		string[] ver = browser_version.Split(new char[] { '.' });
		string[] ua = new string[ver.Length + 1];
		ua[0] = browser;
		ver.CopyTo(ua, 1);
		return ua;
	}
}
