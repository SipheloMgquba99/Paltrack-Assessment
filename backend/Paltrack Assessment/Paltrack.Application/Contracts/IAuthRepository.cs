using Paltrack.Application.Dtos;
using Paltrack.Domain.Entities;

namespace Paltrack.Application.Contracts
{
    public interface IAuthRepository
    {
        Task AddUserAsync(ApplicationUser user);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
    }
}
