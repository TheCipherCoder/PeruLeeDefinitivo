using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace CDatos
{
    public class Conexion
    {
        //    private readonly string _connectionString;
        //    public Conexion(IConfiguration _configuration)
        //    {
        //        _connectionString = _configuration.GetConnectionString("CadenaConexion");
        //    }

        //    public SqlConnection GetConnection()
        //    {
        //        return new SqlConnection(_connectionString);
        //    }
        public static string Cn = "Server=localhost;Database=DB_Peru_Lee;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}
