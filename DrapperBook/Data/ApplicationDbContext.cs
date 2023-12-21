using Microsoft.Data.SqlClient;
using System.Data;

namespace DrapperBook.Data
{
    public class ApplicationDbContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public ApplicationDbContext(IConfiguration configuration)
        {
            configuration = configuration;
            connectionString = configuration.GetConnectionString("defaultConnection");
        }
        public IDbConnection CreateConnection()=> new SqlConnection(connectionString);
    }
}
