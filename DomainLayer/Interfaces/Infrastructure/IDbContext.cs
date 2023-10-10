using System.Data;

namespace DomainLayer.Interfaces.Infrastructure
{
    public interface IDbContext
    {
        IDbConnection CreateConnection { get; }
    }
}
