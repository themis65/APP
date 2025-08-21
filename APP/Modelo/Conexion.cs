using MongoDB.Driver;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Modelo
{
    internal class Conexion
    {
        private readonly string cadenaConexionMongo = "mongodb+srv://themis:themis@bdd.vyum2ot.mongodb.net/?retryWrites=true&w=majority&appName=reservas_medicas";

        public IMongoDatabase ObtenerConexionMongo()
        {
            var cliente = new MongoClient(cadenaConexionMongo);
            return cliente.GetDatabase("reservas_medicas");
        }

        private string cadenaConexionXampp = "server=localhost;user=root;password=;database=reservas_medicas";

        //private string cadenaConexionXampp = "server=192.168.145.138;user=usuario_remoto;password=medusa;database=reservas_medicas";

        public MySqlConnection ObtenerConexionXampp()
        {
            return new MySqlConnection(cadenaConexionXampp);
        }


    }
}

