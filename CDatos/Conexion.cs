using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace CDatos
{
    public class Conexion
    {
        private readonly string _connectionString;
        public Conexion(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("CadenaConexion");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
