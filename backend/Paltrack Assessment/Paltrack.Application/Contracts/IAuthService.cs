using Paltrack.Application.Common;
using Paltrack.Application.Dtos;
using Paltrack.Domain.Entities;

namespace Paltrack.Application.Contracts
{
    public interface IAuthService
    {
        Task<ServiceResult<LoginResponse>> LoginUserAsync(LoginDto loginDto);
        Task<ServiceResult<RegistrationResponse>> RegisterUserAsync(RegisterUserDto registerUserDto);
    }
}
