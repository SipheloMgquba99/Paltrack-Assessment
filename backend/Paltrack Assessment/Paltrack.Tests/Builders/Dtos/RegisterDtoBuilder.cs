using Paltrack.Application.Dtos;

namespace Paltrack.Tests.Builders.Dtos
{
    public class RegisterDtoBuilder
    {
        private string _name = "Test User";
        private string _email = "test@example.com";
        private string _password = "Password123!";
        private string _confirmPassword = "Password123!";

        public RegisterDtoBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public RegisterDtoBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public RegisterDtoBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public RegisterDtoBuilder WithConfirmPassword(string confirmPassword)
        {
            _confirmPassword = confirmPassword;
            return this;
        }

        public RegisterUserDto Build()
        {
            return new RegisterUserDto
            {
                FullName = _name,
                Email = _email,
                Password = _password,
                ConfirmPassword = _confirmPassword
            };
        }
    }
}
