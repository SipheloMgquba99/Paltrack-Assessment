using Moq;
using Paltrack.Application.Contracts;
using Paltrack.Domain.Entities;

namespace Paltrack.Tests.Builders
{
    public class AuthRepositoryBuilder
    {
        private List<ApplicationUser> _users = new();

        public AuthRepositoryBuilder WithUser(ApplicationUser user)
        {
            _users.Add(user);
            return this;
        }

        public IAuthRepository Build()
        {
            var mockRepository = new Mock<IAuthRepository>();

            mockRepository.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => _users.FirstOrDefault(u => u.Email == email));

            mockRepository.Setup(repo => repo.AddUserAsync(It.IsAny<ApplicationUser>()))
                .Callback<ApplicationUser>(user => _users.Add(user))
                .Returns(Task.CompletedTask);

            return mockRepository.Object;
        }
    }
}
