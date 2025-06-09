using Paltrack.Application.Dtos;

namespace Paltrack.Tests.Builders.Dtos
{
    public class LoginDtoBuilder
    {
        private string _email = "test@example.com";
        private string _password = "Password123!";

        public LoginDtoBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public LoginDtoBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public LoginDto Build()
        {
            return new LoginDto
            {
                Email = _email,
                Password = _password
            };
        }
    }
}
