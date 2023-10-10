using DomainLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces.Repository;
using System.Data;

namespace InfrastructureLayer.Data.Repository
{
    public class AppDbContext : IDbContext
    {
        private readonly ISqlServerConnectionProvider _sqlServerConnectionProvider;

        public AppDbContext(ISqlServerConnectionProvider sqlServerConnectionProvider) {
            _sqlServerConnectionProvider = sqlServerConnectionProvider;
        }

        public IDbConnection CreateConnection => _sqlServerConnectionProvider.CreateConnection();
    }   
}
