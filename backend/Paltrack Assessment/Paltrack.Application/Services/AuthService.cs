using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Paltrack.Application.Contracts;
using Paltrack.Application.Dtos;
using Paltrack.Application.Common;
using Paltrack.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Paltrack.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<ServiceResult<LoginResponse>> LoginUserAsync(LoginDto loginDto)
        {
            var validationResult = ValidateUserInput(loginDto.Email, loginDto.Password);
            if (!validationResult.IsSuccess)
            {
                return ServiceResult<LoginResponse>.Failure(validationResult.Message);
            }

            var user = await _authRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null || user.Password != loginDto.Password)
            {
                return ServiceResult<LoginResponse>.Failure("Invalid email or password");
            }

            string token = GenerateJWTToken(user);
            return ServiceResult<LoginResponse>.Success(new LoginResponse(true, "Login successful", token));
        }

        public async Task<ServiceResult<RegistrationResponse>> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var validationResult = ValidateUserInput(registerUserDto.Email, registerUserDto.Password);
            if (!validationResult.IsSuccess)
            {
                return ServiceResult<RegistrationResponse>.Failure(validationResult.Message);
            }

            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                return ServiceResult<RegistrationResponse>.Failure("Passwords do not match.");
            }

            var existingUser = await _authRepository.GetUserByEmailAsync(registerUserDto.Email);
            if (existingUser != null)
            {
                return ServiceResult<RegistrationResponse>.Failure("User already exists");
            }

            var newUser = new ApplicationUser
            {
                FullName = registerUserDto.FullName,
                Email = registerUserDto.Email,
                Password = registerUserDto.Password
            };

            await _authRepository.AddUserAsync(newUser);
            return ServiceResult<RegistrationResponse>.Success(new RegistrationResponse(true, "Registration successful"));
        }

        private ServiceResult ValidateUserInput(string email, string password)
        {
            if (!new EmailAddressAttribute().IsValid(email))
            {
                return ServiceResult.Failure("Invalid email format.");
            }

            var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$");
            if (!passwordRegex.IsMatch(password))
            {
                return ServiceResult.Failure("Password must be at least 6 characters long and include an uppercase letter, a lowercase letter, a number, and a special character.");
            }

            return ServiceResult.Success();
        }

        private string GenerateJWTToken(ApplicationUser user)
        {
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("JWT Key is not configured.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
