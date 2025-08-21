using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace APP.Modelo
{
    internal class ConexionMySQL
    {
        private readonly string cadenaConexion = "server=0.0.0.0;user=Your_User;password=Your_Password;database=Your_DataBase;port=3306";
    

    public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}
