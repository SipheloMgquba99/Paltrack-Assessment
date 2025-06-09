using Paltrack.Application.Dtos;

namespace Paltrack.Tests.Builders.Dtos
{
    public class LoginResponseDtoBuilder
    {
        private bool _success = true;
        private string _message = "Login successful";
        private string _token = "sample-token";

        public LoginResponseDtoBuilder WithSuccess(bool success)
        {
            _success = success;
            return this;
        }

        public LoginResponseDtoBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public LoginResponseDtoBuilder WithToken(string token)
        {
            _token = token;
            return this;
        }

        public LoginResponse Build()
        {
            return new LoginResponse(_success, _message, _token);
        }
    }
}
