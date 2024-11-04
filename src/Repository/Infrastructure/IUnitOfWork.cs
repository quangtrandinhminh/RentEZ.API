using System.Security.Claims;

namespace Repository.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChange();
        Task<int> SaveChangeAsync();
    }
}
