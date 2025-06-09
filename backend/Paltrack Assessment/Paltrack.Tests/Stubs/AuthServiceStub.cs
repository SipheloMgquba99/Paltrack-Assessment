using Paltrack.Application.Contracts;
using Paltrack.Application.Dtos;
using Paltrack.Application.Common;

namespace Paltrack.Tests.Stubs
{
    public class AuthServiceStub : IAuthService
    {
        public Task<ServiceResult<LoginResponse>> LoginUserAsync(LoginDto loginDto)
        {
            return Task.FromResult(ServiceResult<LoginResponse>.Success(new LoginResponse(true, "Stub login successful", "stub-token")));
        }

        public Task<ServiceResult<RegistrationResponse>> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            return Task.FromResult(ServiceResult<RegistrationResponse>.Success(new RegistrationResponse(true, "Stub registration successful")));
        }
    }
}
