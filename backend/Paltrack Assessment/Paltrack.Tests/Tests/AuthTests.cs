using Microsoft.Extensions.Configuration;
using Moq;
using Paltrack.Application.Contracts;
using Paltrack.Application.Services;
using Paltrack.Domain.Entities;
using Paltrack.Tests.Builders.Dtos;

namespace Paltrack.Tests.Tests
{
    public class AuthTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IAuthRepository> _authRepository;
        private readonly Mock<IConfiguration> _configuration;

        public AuthTests()
        {
            _authRepository = new Mock<IAuthRepository>();
            _configuration = new Mock<IConfiguration>();

            _configuration.Setup(config => config["Jwt:Key"])
                .Returns("ThisIsASecureKeyWith32Bytes!!123");

            _configuration.Setup(config => config["Jwt:Issuer"])
                .Returns("TestIssuer");
            _configuration.Setup(config => config["Jwt:Audience"])
                .Returns("TestAudience");

            _authService = new AuthService(_authRepository.Object, _configuration.Object);
        }

        [Fact]
        public async Task LoginUserAsync_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FullName = "Test User",
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Password123!")
            };

            _authRepository.Setup(repo => repo.GetUserByEmailAsync(user.Email))
                .ReturnsAsync(user);

            var loginDto = new LoginDtoBuilder()
                .WithEmail(user.Email)
                .WithPassword("Password123!")
                .Build();

            // Act
            var result = await _authService.LoginUserAsync(loginDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Login successful", result.Data?.Message);
        }

        [Fact]
        public async Task LoginUserAsync_ShouldReturnFailure_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new LoginDtoBuilder()
                .WithEmail("invalid@example.com")
                .WithPassword("WrongPassword123!")
                .Build();

            _authRepository.Setup(repo => repo.GetUserByEmailAsync(loginDto.Email))
                .ReturnsAsync((ApplicationUser?)null);

            // Act
            var result = await _authService.LoginUserAsync(loginDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid email or password", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnSuccess_WhenRegistrationIsValid()
        {
            // Arrange
            var registerDto = new RegisterDtoBuilder()
                .WithEmail("newuser@example.com")
                .WithPassword("Password123!")
                .WithConfirmPassword("Password123!")
                .Build();

            _authRepository.Setup(repo => repo.GetUserByEmailAsync(registerDto.Email))
                .ReturnsAsync((ApplicationUser?)null);

            // Act
            var result = await _authService.RegisterUserAsync(registerDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Registration successful", result.Data?.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenUserAlreadyExists()
        {
            // Arrange
            var existingUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FullName = "Existing User",
                Email = "existing@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Password123!")
            };

            _authRepository.Setup(repo => repo.GetUserByEmailAsync(existingUser.Email))
                .ReturnsAsync(existingUser);

            var registerDto = new RegisterDtoBuilder()
                .WithEmail(existingUser.Email)
                .WithPassword("Password123!")
                .WithConfirmPassword("Password123!")
                .Build();

            // Act
            var result = await _authService.RegisterUserAsync(registerDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User already exists", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var registerDto = new RegisterDtoBuilder()
                .WithPassword("Password123!")
                .WithConfirmPassword("DifferentPassword123!")
                .Build();

            // Act
            var result = await _authService.RegisterUserAsync(registerDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Passwords do not match.", result.Message);
        }
    }
}
