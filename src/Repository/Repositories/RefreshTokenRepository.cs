using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models.Identity;

namespace Repository.Repositories;

public class RefreshTokenRepository(AppDbContext context)
    : BaseRepository<RefreshToken>(context), IRefreshTokenRepository;