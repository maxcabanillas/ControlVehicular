using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ServiceModel.Web;
using System.Web.Script.Services;
using System.Data.SqlClient;


/// <summary>
/// Descripción breve de Validaciones
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class Validaciones : System.Web.Services.WebService
{

    string Conexion = @"Persist Security Info=False;Data Source=mde-db1;Initial Catalog=ControlVehicular;Trusted_Connection=Yes"; 
    
    SqlConnection Conn = new SqlConnection();

    public Validaciones()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string ValidarVehiculo(string placa)
    {
        string autorizado = "False";
        string picoYPlaca = "False";
        string Cadena = "";

        Conn.ConnectionString = Conexion;
        Conn.Open();

        SqlCommand command = new SqlCommand("SELECT     TOP (1) IdVehiculoAutorizado, Placa, Autorizado, PicoyPlaca, Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo, Persona, Documento, Marca, Area, Cargo " +
                            " FROM         VehiculosAutorizados inner join Personas on VehiculosAutorizados.IdPersona = Personas.IdPersona inner join TiposVehiculos on VehiculosAutorizados.IdTipoVehiculo = TiposVehiculos.IdTipoVehiculo inner join Areas on Personas.IdArea = Areas.IdArea inner join Cargos on Personas.IdCargo = Cargos.IdCargo " +
                            " WHERE     (Placa = '" + placa + "' )", Conn);

        SqlDataReader dr = command.ExecuteReader();

        while (dr.Read())
        {
            Cadena = @", ""Persona"": """ + dr["Persona"].ToString() + @""", ""Documento"" :""" + dr["Documento"].ToString() + @""", ""Marca"" :""" + dr["Marca"].ToString() + @""", ""Area"" :""" + dr["Area"].ToString() + @""", ""Cargo"" :""" + dr["Cargo"].ToString() + @"""";

            if (dr["Autorizado"].ToString() == "True")
            {
                autorizado = "True";

                if (dr["PicoyPlaca"].ToString() == "True")
                {

                    System.DayOfWeek dia = DateTime.Now.DayOfWeek;

                    switch (dia)
                    {
                        case DayOfWeek.Monday:
                            {
                                if (dr["Lunes"].ToString() == "True")
                                {
                                    picoYPlaca = "True";
                                }
                                break;
                            }
                        case DayOfWeek.Tuesday:
                            {
                                if (dr["Martes"].ToString() == "True")
                                {
                                    picoYPlaca = "True";
                                }
                                break;
                            }
                        case DayOfWeek.Wednesday:
                            {
                                if (dr["Miercoles"].ToString() == "True")
                                {
                                    picoYPlaca = "True";
                                }
                                break;
                            }
                        case DayOfWeek.Thursday:
                            {
                                if (dr["Jueves"].ToString() == "True")
                                {
                                    picoYPlaca = "True";
                                }
                                break;
                            }
                        case DayOfWeek.Friday:
                            {
                                if (dr["Viernes"].ToString() == "True")
                                {
                                    picoYPlaca = "True";
                                }
                                break;
                            }
                        case DayOfWeek.Saturday:
                            {
                                if (dr["Sabados"].ToString() == "True")
                                {
                                    picoYPlaca = "True";
                                }
                                break;
                            }
                        case DayOfWeek.Sunday:
                            {
                                if (dr["Domingo"].ToString() == "True")
                                {
                                    picoYPlaca = "True";
                                }
                                break;
                            }
                    }
                }
            }
        }

        return @"{""Autorizado"": """ + autorizado + @""", ""PicoyPlaca"" :""" + picoYPlaca + @"""" + Cadena + "}";
    }



    [WebMethod]
    public void GuardarRegistroVehiculo(int idTipoVehiculo, string placa, string observacion)
    {
        SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "INSERT RegistrosVehiculos (idTipoVehiculo, placa, observaciones) VALUES (" + idTipoVehiculo.ToString() + ", '" + placa + "', '" + observacion + "')";
        cmd.Connection = Conn;

        Conn.ConnectionString = Conexion;
        Conn.Open();

        cmd.ExecuteNonQuery();
        Conn.Close();
    }

    [WebMethod]
    public void GuardarSacarVehiculo(string placa)
    {

        SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "UPDATE TOP (1) RegistrosVehiculos  SET FechaSalida = GetDate() FROM RegistrosVehiculos INNER JOIN " + 
            " (SELECT TOP (1) IdRegistroVehiculo  FROM RegistrosVehiculos AS Consulta WHERE (Placa = '" + placa + "' and FechaSalida is null) " + 
            " ORDER BY FechaIngreso DESC) AS C ON RegistrosVehiculos.IdRegistroVehiculo = C.IdRegistroVehiculo ";
                          
        cmd.Connection = Conn;

        Conn.ConnectionString = Conexion;
        Conn.Open();

        cmd.ExecuteNonQuery();
        Conn.Close();
    }

    [WebMethod]
    public string SeleccionarPersonas(int IdArea)
    {
        string Cadena = "<option value='0'>Seleccione uno</option>";

        SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select * from Personas where IdArea = " + IdArea.ToString();

        cmd.Connection = Conn;

        Conn.ConnectionString = Conexion;
        Conn.Open();


        SqlDataReader dr;

        dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            Cadena += @"<option value='" + dr["IdPersona"] + "'>" + dr["Persona"] + "</option>";
        }

        dr.Close();

        return Cadena;
    }

    [WebMethod]
    public void GuardarPeaton(int IdTipoDocumento, string Documento, string Nombre, string Apellidos, int IdArea, int IdPersona, string Observacion)
    {
        SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "INSERT RegistrosPeatones (IdTipoDocumento, Documento, Nombre, Apellidos, IdArea, IdPersona, Observacion) VALUES (" + 
            IdTipoDocumento.ToString() + ", '" + Documento + "', '" + Nombre + "', '"+ Apellidos + "', " + IdArea.ToString() + ", " + IdPersona.ToString() + ", '" + Observacion + "')";
        cmd.Connection = Conn;

        Conn.ConnectionString = Conexion;
        Conn.Open();

        cmd.ExecuteNonQuery();
        Conn.Close();
    }

    [WebMethod]
    public void GuardarSacarPeaton(string Documento)
    {

        SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "UPDATE TOP (1) RegistrosPeatones  SET FechaSalida = GetDate() FROM RegistrosPeatones INNER JOIN " +
            " (SELECT TOP (1) IdRegistroPeaton  FROM RegistrosPeatones AS Consulta WHERE (Documento = '" + Documento + "' and FechaSalida is null) " +
            " ORDER BY FechaIngreso DESC) AS C ON RegistrosPeatones.IdRegistroPeaton = C.IdRegistroPeaton ";

        cmd.Connection = Conn;

        Conn.ConnectionString = Conexion;
        Conn.Open();

        cmd.ExecuteNonQuery();
        Conn.Close();
    }

    [WebMethod]
    public string ConsultarIngresoVehiculo(string placa)
    {
        string Cadena = "";
        
        Conn.ConnectionString = Conexion;
        Conn.Open();

        SqlCommand command = new SqlCommand("SELECT TOP (1)  Placa, FechaIngreso, Observaciones  FROM RegistrosVehiculos AS Consulta WHERE (Placa = '" + placa + "' and FechaSalida is null) " +
            " ORDER BY FechaIngreso DESC", Conn);

        SqlDataReader dr = command.ExecuteReader();

        while (dr.Read())
        {
            Cadena = @"{""SeEncontro"":""True"", ""Placa"": """ + dr["Placa"].ToString() + @""", ""FechaIngreso"" :""" + dr["FechaIngreso"].ToString() + @""", ""Observaciones"" :""" + dr["Observaciones"].ToString() + @"""}";
        }

        if (Cadena == "")
        {
            Cadena = @"{""SeEncontro"":""False"", ""Placa"": """", ""FechaIngreso"" :"""", ""Observaciones"" :""""}";
        }
        
        return Cadena;
    }


    [WebMethod]
    public string ConsultarPersona(string Documento)
    {
        string Cadena = "";

        Conn.ConnectionString = Conexion;
        Conn.Open();

        SqlCommand command = new SqlCommand("SELECT TOP (1) Nombre, Apellidos FROM RegistrosPeatones AS Consulta WHERE (Documento = '" + Documento + "') " +
            " ORDER BY FechaIngreso DESC", Conn);

        SqlDataReader dr = command.ExecuteReader();

        while (dr.Read())
        {
            Cadena = @"{""SeEncontro"":""True"", ""Nombre"": """ + dr["Nombre"].ToString() + @""", ""Apellidos"" :""" + dr["Apellidos"].ToString() +  @"""}";
        }

        if (Cadena == "")
        {
            Cadena = @"{""SeEncontro"":""False""}";
        }

        return Cadena;
    }


    [WebMethod]
    public string ConsultarIngresoPeaton(string Documento)
    {
        string Cadena = "";

        Conn.ConnectionString = Conexion;
        Conn.Open();

        SqlCommand command = new SqlCommand("SELECT TOP (1) Nombre, Apellidos, FechaIngreso, Observacion FROM RegistrosPeatones AS Consulta WHERE (Documento = '" + Documento + "' and FechaSalida is null) " +
            " ORDER BY FechaIngreso DESC", Conn);

        SqlDataReader dr = command.ExecuteReader();

        while (dr.Read())
        {
            Cadena = @"{""SeEncontro"":""True"", ""Nombre"": """ + dr["Nombre"].ToString() + @""", ""Apellidos"" :""" + dr["Apellidos"].ToString() + @""", ""FechaIngreso"" :""" + dr["FechaIngreso"].ToString() + @""", ""Observacion"" :""" + dr["Observacion"].ToString() + @"""}";
        }

        if (Cadena == "")
        {
            Cadena = @"{""SeEncontro"":""False""}";
        }

        return Cadena;
    }




}
