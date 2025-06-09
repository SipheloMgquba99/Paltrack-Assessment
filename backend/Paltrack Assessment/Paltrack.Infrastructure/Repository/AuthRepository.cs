using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Paltrack.Application.Contracts;
using Paltrack.Domain.Entities;

namespace Paltrack.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMemoryCache _cache;

        public AuthRepository(AppDbContext appDbContext, IMemoryCache cache)
        {
            _appDbContext = appDbContext;
            _cache = cache;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            var cacheKey = $"user_email_{email}";
            if (_cache.TryGetValue(cacheKey, out ApplicationUser? user))
                return user;

            user = await _appDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                _cache.Set(cacheKey, user, TimeSpan.FromMinutes(10));
            }

            return user;
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            _appDbContext.ApplicationUsers.Add(user);
            await _appDbContext.SaveChangesAsync();

            var cacheKey = $"user_email_{user.Email}";
            _cache.Set(cacheKey, user, TimeSpan.FromMinutes(10));
        }
    }
}
