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
// ASP.NET Maker 9 - Project Configuration
//
public partial class AspNetMaker9_ControlVehicular: System.Web.UI.Page {

	// Database connection string
	public static string EW_DB_CONNECTION_STRING = "Persist Security Info=False;Data Source=mde-db1;Initial Catalog=ControlVehicular;Trusted_Connection=Yes";

  public const string EW_DB_QUOTE_START = "["; 

	public const string EW_DB_QUOTE_END = "]"; 

	// Database SQL parameter symbol
	public const string EW_DB_SQLPARAM_SYMBOL = "@";

	// File system encoding
	public const string EW_FILE_SYSTEM_ENCODING = ""; // File system encoding

	// Database type
	public const bool EW_IS_MSACCESS = false; // Access

	public const bool EW_IS_MSSQL = true; // MS SQL

	public const bool EW_IS_MYSQL = false; // MySQL

  public const bool EW_IS_POSTGRESQL = false; // PostgreSQL

	public const bool EW_IS_ORACLE = false; // Oracle	

	// Debug flag	
	public const bool EW_DEBUG_ENABLED = false; // True to debug / False to skip

	// Response.Buffer
	public const bool EW_RESPONSE_BUFFER = true;

	// Use XPath for language object
	public const bool EW_USE_DOM_XML = false;

	// Project Name	
	public const string EW_PROJECT_NAME = "ControlVehicular";

	// Remove XSS
	public const bool EW_REMOVE_XSS = true;

	// Note: Remove accepted elements in the following array at your own risk. 
	public static string[] EW_REMOVE_XSS_KEYWORDS = new string[]{"javascript", "vbscript", "expression", "<applet", "<meta", "<xml", "<blink", "<link", "<style", "<script", "<embed", "<object", "<iframe", "<frame", "<frameset", "<ilayer", "<layer", "<bgsound", "<title", "<base", "onabort", "onactivate", "onafterprint", "onafterupdate", "onbeforeactivate", "onbeforecopy", "onbeforecut", "onbeforedeactivate", "onbeforeeditfocus", "onbeforepaste", "onbeforeprint", "onbeforeunload", "onbeforeupdate", "onblur", "onbounce", "oncellchange", "onchange", "onclick", "oncontextmenu", "oncontrolselect", "oncopy", "oncut", "ondataavailable", "ondatasetchanged", "ondatasetcomplete", "ondblclick", "ondeactivate", "ondrag", "ondragend", "ondragenter", "ondragleave", "ondragover", "ondragstart", "ondrop", "onerror", "onerrorupdate", "onfilterchange", "onfinish", "onfocus", "onfocusin", "onfocusout", "onhelp", "onkeydown", "onkeypress", "onkeyup", "onlayoutcomplete", "onload", "onlosecapture", "onmousedown", "onmouseenter", "onmouseleave", "onmousemove", "onmouseout", "onmouseover", "onmouseup", "onmousewheel", "onmove", "onmoveend", "onmovestart", "onpaste", "onpropertychange", "onreadystatechange", "onreset", "onresize", "onresizeend", "onresizestart", "onrowenter", "onrowexit", "onrowsdelete", "onrowsinserted", "onscroll", "onselect", "onselectionchange", "onselectstart", "onstart", "onstop", "onsubmit", "onunload"};	

	// Session names	
	public const string EW_SESSION_STATUS = EW_PROJECT_NAME + "_Status"; // Login status	

	public const string EW_SESSION_USER_NAME = EW_SESSION_STATUS + "_UserName";	// User name	

	public const string EW_SESSION_USER_ID = EW_SESSION_STATUS + "_UserID";	// User ID	

  public const string EW_SESSION_USER_PROFILE = EW_SESSION_STATUS + "_UserProfile"; // User Profile

	public const string EW_SESSION_USER_PROFILE_USER_NAME = EW_SESSION_USER_PROFILE + "_UserName";

	public const string EW_SESSION_USER_PROFILE_PASSWORD = EW_SESSION_USER_PROFILE + "_Password";

	public const string EW_SESSION_USER_PROFILE_LOGIN_TYPE = EW_SESSION_USER_PROFILE + "_LoginType";

	public const string EW_SESSION_USER_LEVEL_ID = EW_SESSION_STATUS + "_UserLevel"; // User level ID		

	public const string EW_SESSION_USER_LEVEL = EW_SESSION_STATUS + "_UserLevelValue"; // User level		

	public const string EW_SESSION_PARENT_USER_ID = EW_SESSION_STATUS + "_ParentUserID"; // Parent user ID		

	public const string EW_SESSION_SYS_ADMIN = EW_PROJECT_NAME + "_SysAdmin"; // System admin	

	public const string EW_SESSION_AR_USER_LEVEL = EW_PROJECT_NAME + "_arUserLevel"; // User level ArrayList	

	public const string EW_SESSION_AR_USER_LEVEL_PRIV = EW_PROJECT_NAME + "_arUserLevelPriv"; // User level privilege ArrayList		

	public const string EW_SESSION_SECURITY = EW_PROJECT_NAME + "_Security"; // Security array		

	public const string EW_SESSION_MESSAGE = EW_PROJECT_NAME + "_Message"; // System message	

	public const string EW_SESSION_FAILURE_MESSAGE = EW_PROJECT_NAME + "_Failure_Message"; // System error message

	public const string EW_SESSION_SUCCESS_MESSAGE = EW_PROJECT_NAME + "_Success_Message"; // System message

	public const string EW_SESSION_INLINE_MODE = EW_PROJECT_NAME + "_InlineMode"; // Inline mode

	// Paging
	public const int EW_PAGER_RANGE = 10;

	public const int EW_GRIDADD_ROWS = 10;	

	// Dynamic user level settings
	// User level definition table/field names
	public const string EW_USER_LEVEL_TABLE = "[dbo].[UserLevels]";

	public const string EW_USER_LEVEL_ID_FIELD = "[UserLevelID]";

	public const string EW_USER_LEVEL_NAME_FIELD = "[UserLevelName]";

	// User Level privileges table/field names
	public const string EW_USER_LEVEL_PRIV_TABLE = "[dbo].[UserLevelPermissions]";

	public const string EW_USER_LEVEL_PRIV_TABLE_NAME_FIELD = "[UserLevelTableName]";

	public const string EW_USER_LEVEL_PRIV_USER_LEVEL_ID_FIELD = "[UserLevelID]";

	public const string EW_USER_LEVEL_PRIV_PRIV_FIELD = "[UserLevelPermission]";

	// Delimiters/Separators
	public const string EW_RECORD_DELIMITER = "\r";

	public const string EW_FIELD_DELIMITER = "|";

	public const string EW_COMPOSITE_KEY_SEPARATOR = ","; // Composite key separator	

	public const string EW_EMAIL_KEYWORD_SEPARATOR = ""; // Email keyword separator

	public const string EW_CHARSET = "utf-8"; // Project charset

	public const string EW_EMAIL_CHARSET = EW_CHARSET; // Email charset 

	// Date format
	public const string EW_DATE_SEPARATOR = "/";

	public const short EW_UNFORMAT_YEAR = 50; // Unformat year

	public const short EW_DEFAULT_DATE_FORMAT = 7;

	// Highlight	
	public const bool EW_HIGHLIGHT_COMPARE = true; // Case-insensitive

	// Language settings
	public const string EW_LANGUAGE_FOLDER = "aspxlang/";

	public static string[][] EW_LANGUAGE_FILE = {
new string[] {"en", "", "spanish.xml"}
};

	public const string EW_LANGUAGE_DEFAULT_ID = "en";

	//public const string EW_SESSION_LANGUAGE_FILE_CACHE = EW_PROJECT_NAME + "_LanguageFile_yww3BKNesDmUQoji"; // Language File Cache
	//public const string EW_SESSION_LANGUAGE_CACHE = EW_PROJECT_NAME + "_Language_yww3BKNesDmUQoji"; // Language Cache
	public const string EW_SESSION_LANGUAGE_ID = EW_PROJECT_NAME + "_LanguageId"; // Language ID

	// CSS file name
	public const string EW_PROJECT_STYLESHEET_FILENAME = "aspxcss/ControlVehicular.css"; 	

	// Data type (DO NOT CHANGE!)
	public const short EW_DATATYPE_NUMBER = 1;

	public const short EW_DATATYPE_DATE = 2;

	public const short EW_DATATYPE_STRING = 3;

	public const short EW_DATATYPE_BOOLEAN = 4;

	public const short EW_DATATYPE_GUID = 5;

	public const short EW_DATATYPE_OTHER = 6;

	public const short EW_DATATYPE_TIME = 7;

	public const short EW_DATATYPE_BLOB = 8;

	public const short EW_DATATYPE_MEMO = 9;

	// Row types	
	public const short EW_ROWTYPE_VIEW = 1;	// Row type view	

	public const short EW_ROWTYPE_ADD = 2; // Row type add	

	public const short EW_ROWTYPE_EDIT = 3; // Row type edit

	public const short EW_ROWTYPE_SEARCH = 4;	// Row type search

	public const short EW_ROWTYPE_MASTER = 5; // Row type master record

	public const short EW_ROWTYPE_AGGREGATEINIT = 6; // Row type aggregate init 

	public const short EW_ROWTYPE_AGGREGATE = 7; // Row type aggregate	

	// Table specific
	public const string EW_TABLE_PREFIX = "||ASPNETReportMaker||";	

	public const string EW_TABLE_REC_PER_PAGE = "recperpage"; // Records per page	

	public const string EW_TABLE_START_REC = "start"; // Start record	

	public const string EW_TABLE_PAGE_NO = "pageno"; // Page number	

	public const string EW_TABLE_BASIC_SEARCH = "psearch"; // Basic search keyword		

	public const string EW_TABLE_BASIC_SEARCH_TYPE = "psearchtype"; // Basic search type	

	public const string EW_TABLE_ADVANCED_SEARCH = "advsrch"; // Advanced search	

	public const string EW_TABLE_SEARCH_WHERE = "searchwhere"; // Search where clause	

	public const string EW_TABLE_WHERE = "where"; // Table where

	public const string EW_TABLE_WHERE_LIST = "where_list"; // Table where (list page) 	

	public const string EW_TABLE_ORDER_BY = "orderby"; // Table order by

	public const string EW_TABLE_ORDER_BY_LIST = "orderby_list"; // Table order by (list page) 	

	public const string EW_TABLE_SORT = "sort"; // Table sort	

	public const string EW_TABLE_KEY = "key"; // Table key	

	public const string EW_TABLE_SHOW_MASTER = "showmaster"; // Table show master

	public const string EW_TABLE_SHOW_DETAIL = "showdetail"; // Table show detail	

	public const string EW_TABLE_MASTER_TABLE = "mastertable"; // Master table

	public const string EW_TABLE_DETAIL_TABLE = "detailtable"; // Detail table

	public const string EW_TABLE_RETURN_URL = "return"; // Return URL

	public const string EW_TABLE_EXPORT_RETURN_URL = "exportreturn"; // Export return URL

	public const string EW_TABLE_GRID_ADD_ROW_COUNT = "gridaddcnt"; // Grid add row count

	// Audit Trail
	public const bool EW_AUDIT_TRAIL_TO_DATABASE = false; // Write audit trail to DB

	public const string EW_AUDIT_TRAIL_TABLE_NAME = ""; // Audit trail table name

	public const string EW_AUDIT_TRAIL_FIELD_NAME_DATETIME = ""; // Audit trail DateTime field name

	public const string EW_AUDIT_TRAIL_FIELD_NAME_SCRIPT = ""; // Audit trail Script field name

	public const string EW_AUDIT_TRAIL_FIELD_NAME_USER = ""; // Audit trail User field name

	public const string EW_AUDIT_TRAIL_FIELD_NAME_ACTION = ""; // Audit trail Action field name

	public const string EW_AUDIT_TRAIL_FIELD_NAME_TABLE = ""; // Audit trail Table field name

	public const string EW_AUDIT_TRAIL_FIELD_NAME_FIELD = ""; // Audit trail Field field name

	public const string EW_AUDIT_TRAIL_FIELD_NAME_KEYVALUE = ""; // Audit trail Key Value field name

	public const string EW_AUDIT_TRAIL_FIELD_NAME_OLDVALUE = ""; // Audit trail Old Value field name

	public const string EW_AUDIT_TRAIL_FIELD_NAME_NEWVALUE = ""; // Audit trail New Value field name

	// Audit trail
	public const string EW_AUDIT_TRAIL_PATH = ""; // Audit trail path	

	// Security
	public const string EW_ADMIN_USER_NAME = "admin"; // Administrator user name	

	public const string EW_ADMIN_PASSWORD = "12312"; // Administrator password	

	public const bool EW_ENCRYPTED_PASSWORD = true; // Encrypted password	

	public const bool EW_CASE_SENSITIVE_PASSWORD = false; // Case Sensitive password

	public const bool EW_USE_CUSTOM_LOGIN = true; // Use custom login

	// Dynamic user level table
	public static string[] EW_USER_LEVEL_TABLE_NAME = new string[] {"Areas",
"Personas",
"sysdiagrams",
"TiposVehiculos",
"Usuarios",
"VehiculosAutorizados",
"VehiculosPicoYPlacaHoras",
"TiposDocumentos",
"RegistrosPeatones",
"RegistrosVehiculos",
"HistoricoVehiculos",
"HistoricoPeatones",
"_RegistroVehiculo",
"_RegistroPeatones",
"Cargos"};

	public static string[] EW_USER_LEVEL_TABLE_CAPTION = new string[] {"Areas",
"Personas",
"sysdiagrams",
"Tipos Vehículos",
"Usuarios",
"Vehículos Autorizados",
"Horas Pico y Placa",
"Tipos Documentos",
"Registros Peatones",
"Registros Vehículos",
"Histórico Vehículos",
"Histórico Peatones",
"Registro Vehículos",
"Registro Peatones",
"Cargos"};

	public static string[] EW_USER_LEVEL_TABLE_VAR = new string[] {"Areas",
"Personas",
"sysdiagrams",
"TiposVehiculos",
"Usuarios",
"VehiculosAutorizados",
"VehiculosPicoYPlacaHoras",
"TiposDocumentos",
"RegistrosPeatones",
"RegistrosVehiculos",
"HistoricoVehiculos",
"HistoricoPeatones",
"z_RegistroVehiculo",
"z_RegistroPeatones",
"Cargos"}; 

	// Use old user level values
	public const bool EW_USER_LEVEL_COMPAT = true; // Use old user level values

	public const short EW_ALLOW_ADD = 1; // Add		

	public const short EW_ALLOW_DELETE = 2; // Delete			

	public const short EW_ALLOW_EDIT = 4; // Edit				

	public const short EW_ALLOW_LIST = 8; // List		

	public const int EW_ALLOW_VIEW = 8; // View (for EW_USER_LEVEL_COMPAT = True)		

	public const int EW_ALLOW_SEARCH = 8; // Search (for EW_USER_LEVEL_COMPAT = True)

	//Public Const EW_ALLOW_VIEW As Integer = 32 ' View (for EW_USER_LEVEL_COMPAT = False)
	//Public Const EW_ALLOW_SEARCH As Integer = 64 ' Search (for EW_USER_LEVEL_COMPAT = False)		
	public const short EW_ALLOW_REPORT = 8; // Report		

	public const short EW_ALLOW_ADMIN = 16; // Admin	

	// Hierarchical User ID		
	public const bool EW_USER_ID_IS_HIERARCHICAL = true; // True to show all level / False to show 1 level 

	// Use subquery for master/detail user id checking
	public const bool EW_USE_SUBQUERY_FOR_MASTER_USER_ID = true; // True to use subquery / False to skip

	// User table filters
	public const string EW_USER_TABLE = "[Usuarios]";

	public const string EW_USER_NAME_FILTER = "([Usuario] = '%u')";

	public const string EW_USER_ID_FILTER = "([IdUsuario] = %u)";

	public const string EW_USER_EMAIL_FILTER = "";

	public const string EW_USER_ACTIVATE_FILTER = "";

	public const string EW_USER_PROFILE_FIELD_NAME = "";

	// User Profile Constants
	public const string EW_USER_PROFILE_KEY_SEPARATOR = "=";

	public const string EW_USER_PROFILE_FIELD_SEPARATOR = ",";

	public const string EW_USER_PROFILE_SESSION_ID = "SessionID";

	public const string EW_USER_PROFILE_LAST_ACCESSED_DATE_TIME = "LastAccessedDateTime";

	public const int EW_USER_PROFILE_SESSION_TIMEOUT = 20;

	public const string EW_USER_PROFILE_LOGIN_RETRY_COUNT = "LoginRetryCount";

	public const string EW_USER_PROFILE_LAST_BAD_LOGIN_DATE_TIME = "LastBadLoginDateTime";

	public const int EW_USER_PROFILE_MAX_RETRY = 3;

	public const int EW_USER_PROFILE_RETRY_LOCKOUT = 20;

	public const string EW_USER_PROFILE_LAST_PASSWORD_CHANGED_DATE = "LastPasswordChangedDate";

	public const int EW_USER_PROFILE_PASSWORD_EXPIRE = 90;

	// Email
	public const string EW_SMTP_SERVER = "localhost"; // SMTP server	

	public const int EW_SMTP_SERVER_PORT = 25; // SMTP server port	

	public const string EW_SMTP_SERVER_USERNAME = ""; // SMTP server user name	

	public const string EW_SMTP_SERVER_PASSWORD = ""; // SMTP server password	

	public const string EW_SENDER_EMAIL = ""; // Sender email	

	public const string EW_RECIPIENT_EMAIL = ""; // Recipient email

	public const int EW_MAX_EMAIL_RECIPIENT = 3;

	public const int EW_MAX_EMAIL_SENT_COUNT = 3;

	public const string EW_EXPORT_EMAIL_COUNTER = EW_SESSION_STATUS + "_EmailCounter";

	// File upload
	public const string EW_UPLOAD_DEST_PATH = "~/App_Upload/"; // Upload destination path	

	public const string EW_UPLOAD_ALLOWED_FILE_EXT = "gif,jpg,jpeg,bmp,png,doc,xls,pdf,zip,docx,xlsx"; // Allowed file extensions

	public const string EW_IMAGE_ALLOWED_FILE_EXT = "gif,jpg,png,bmp"; // Allowed file extensions for images	

	public const int EW_MAX_FILE_SIZE = 2000000; // Max file size	

	public const short EW_THUMBNAIL_DEFAULT_WIDTH = 0; // Thumbnail default width

	public const short EW_THUMBNAIL_DEFAULT_HEIGHT = 0; // Thumbnail default height

	public const short EW_THUMBNAIL_DEFAULT_INTERPOLATION = 1; // Thumbnail default interpolation

	// Export
	public const bool EW_EXPORT_ALL = true; // Export all records

	public const bool EW_EXPORT_ORIGINAL_VALUE = false; // True to export original value

	public const bool EW_EXPORT_FIELD_CAPTION = false; // True to export field caption

	public const bool EW_EXPORT_CSS_STYLES = true; // True to export css styles

	public const bool EW_EXPORT_MASTER_RECORD = true; // TRUE to export master record

	public const bool EW_EXPORT_MASTER_RECORD_FOR_CSV = false; // TRUE to export master record for CSV

	// Use token in URL (reserved only)	
	public const bool EW_USE_TOKEN_IN_URL = false; // Do not use token in URL	

	// public const bool EW_USE_TOKEN_IN_URL = true; // Use token in URL
	// Use ILIKE for PostgreSql
	public const bool EW_USE_ILIKE_FOR_POSTGRESQL = false;

	// Use collation for MySQL
	public const string EW_LIKE_COLLATION_FOR_MYSQL = "";

	// Null / Not Null values
	public const string EW_NULL_VALUE = "##null##";

	public const string EW_NOT_NULL_VALUE = "##notnull##";

	// Search multi value option
	// 1 - no multi value
	// 2 - AND all multi values
	// 3 - OR all multi values	
	public const short EW_SEARCH_MULTI_VALUE_OPTION = 3;

	// Validate option
	public const bool EW_CLIENT_VALIDATE = true;	

	public const bool EW_SERVER_VALIDATE = false;

	// Blob field byte count for Hash value calculation
	public const int EW_BLOB_FIELD_BYTE_COUNT = 200;

	// Auto suggest max entries
	public const int EW_AUTO_SUGGEST_MAX_ENTRIES = 10;

	// Cookie expiry time
	public static DateTime EW_COOKIE_EXPIRY_TIME = DateTime.Today.AddDays(365);

	// Random key
	public const string EW_RANDOM_KEY = "bJW1S1Y84BU5kl3E";

	// Checkbox/RadioButton template/table
	public const string EW_ITEM_TEMPLATE_CLASSNAME = "ewTemplate";	

	public const string EW_ITEM_TABLE_CLASSNAME = "ewItemTable";

	// StyleSheet
	public const string EW_PROJECT_CSSFILE = "aspxcss/ControlVehicular.css";	

	public const int EW_ROWTYPE_PREVIEW = 11; // Preview record

	public const string EW_MENUBAR_CLASSNAME = "yuimenubar yuimenubarnav";

	public const string EW_MENUBAR_ITEM_CLASSNAME = "yuimenubaritem";

	public const string EW_MENUBAR_ITEM_LABEL_CLASSNAME = "yuimenubaritemlabel";

	public const string EW_MENU_CLASSNAME = "yuimenu";

	public const string EW_MENU_ITEM_CLASSNAME = "yuimenuitem"; // Vertical

	public const string EW_MENU_ITEM_LABEL_CLASSNAME = "yuimenuitemlabel"; // Vertical

	// PDF
	public const string EW_PDF_STYLESHEET_FILENAME = ""; // export PDF CSS styles
}
