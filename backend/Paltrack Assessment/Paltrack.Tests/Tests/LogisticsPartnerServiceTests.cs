using Moq;
using Paltrack.Application.Common;
using Paltrack.Application.Interfaces.IRepositories;
using Paltrack.Application.Services;
using Paltrack.Domain.Entities;
using Paltrack.Tests.Builders.Entities;
using Paltrack.Tests.Builders.Filters;

namespace Paltrack.Tests.Tests
{
    public class LogisticsPartnerServiceTests
    {
        private readonly LogisticsPartnerService _logisticsPartnerService;
        private readonly Mock<ILogisticsPartnerRepository> _logisticsPartnerRepository;

        public LogisticsPartnerServiceTests()
        {
            _logisticsPartnerRepository = new Mock<ILogisticsPartnerRepository>();
            _logisticsPartnerService = new LogisticsPartnerService(_logisticsPartnerRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
        {
            // Arrange
            var filter = new LogisticsPartnerFilterBuilder()
                .WithPageIndex(1)
                .WithPageSize(10)
                .Build();

            var logisticsPartners = new List<LogisticsPartner>
            {
                new LogisticsPartnerBuilder()
                    .WithCompany("Company A")
                    .WithCity("City A")
                    .WithCountry("Country A")
                    .WithEmail("emailA@example.com")
                    .Build(),
                new LogisticsPartnerBuilder()
                    .WithCompany("Company B")
                    .WithCity("City B")
                    .WithCountry("Country B")
                    .WithEmail("emailB@example.com")
                    .Build()
            };

            var paginationResult = new PaginationResult<LogisticsPartner>
            {
                Items = logisticsPartners.AsQueryable(),
                TotalCount = logisticsPartners.Count,
                PageNumber = filter.PageIndex,
                PageSize = filter.PageSize,
                TotalPages = 1
            };

            _logisticsPartnerRepository.Setup(repo => repo.GetAllAsync(filter))
                .ReturnsAsync(ServiceResult<PaginationResult<LogisticsPartner>>.Success(paginationResult));

            // Act
            var result = await _logisticsPartnerService.GetAllAsync(filter);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(logisticsPartners.Count, result.Data.TotalCount);
        }

        [Fact]
        public void GetQueryable_ShouldReturnQueryable_WhenCalled()
        {
            // Arrange
            var logisticsPartners = new List<LogisticsPartner>
            {
                new LogisticsPartnerBuilder()
                    .WithCompany("Company A")
                    .WithCity("City A")
                    .WithCountry("Country A")
                    .WithEmail("emailA@example.com")
                    .Build(),
                new LogisticsPartnerBuilder()
                    .WithCompany("Company B")
                    .WithCity("City B")
                    .WithCountry("Country B")
                    .WithEmail("emailB@example.com")
                    .Build()
            }.AsQueryable();

            _logisticsPartnerRepository.Setup(repo => repo.GetQueryable())
                .Returns(logisticsPartners);

            // Act
            var queryableResult = _logisticsPartnerService.GetQueryable();

            // Assert
            Assert.NotNull(queryableResult);
            Assert.Equal(logisticsPartners.Count(), queryableResult.Count());
        }
    }
}

