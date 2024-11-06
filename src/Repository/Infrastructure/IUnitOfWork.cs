using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Repository.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChange();
        Task<int> SaveChangeAsync(IHttpContextAccessor accessor = null);
    }
}
