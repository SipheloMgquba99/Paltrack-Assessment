using Paltrack.Domain.Entities;

namespace Paltrack.Tests.Builders.Entities
{
    public class LogisticsPartnerBuilder
    {
        private readonly LogisticsPartner _logisticsPartner;

        public LogisticsPartnerBuilder()
        {
            _logisticsPartner = new LogisticsPartner
            {
                Id = Guid.NewGuid(),
                Company = "Default Company",
                Email = "default@example.com",
                Phone = "1234567890",
                Date = DateTime.UtcNow,
                City = "Default City",
                Country = "Default Country"
            };
        }

        public LogisticsPartnerBuilder WithCompany(string company)
        {
            _logisticsPartner.Company = company;
            return this;
        }

        public LogisticsPartnerBuilder WithEmail(string email)
        {
            _logisticsPartner.Email = email;
            return this;
        }

        public LogisticsPartnerBuilder WithCity(string city)
        {
            _logisticsPartner.City = city;
            return this;
        }

        public LogisticsPartnerBuilder WithCountry(string country)
        {
            _logisticsPartner.Country = country;
            return this;
        }

        public LogisticsPartner Build()
        {
            return _logisticsPartner;
        }
    }
}
