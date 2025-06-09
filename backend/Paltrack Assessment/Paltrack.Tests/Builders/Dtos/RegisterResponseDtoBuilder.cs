using Paltrack.Application.Dtos;

namespace Paltrack.Tests.Builders.Dtos
{
    public class RegisterResponseDtoBuilder
    {
        private bool _success = true;
        private string _message = "Registration successful";

        public RegisterResponseDtoBuilder WithSuccess(bool success)
        {
            _success = success;
            return this;
        }

        public RegisterResponseDtoBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public RegistrationResponse Build()
        {
            return new RegistrationResponse(_success, _message);
        }
    }
}

