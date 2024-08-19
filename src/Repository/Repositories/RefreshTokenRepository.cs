using BusinessObject.Entities.Identity;
using Repository.Base;
using Repository.Interfaces;

namespace Repository.Repositories;

public class RefreshTokenRepository(AppDbContext context)
    : BaseRepository<RefreshToken>(context), IRefreshTokenRepository;