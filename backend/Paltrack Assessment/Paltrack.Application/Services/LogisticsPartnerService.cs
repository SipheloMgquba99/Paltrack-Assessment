using Paltrack.Application.Common;
using Paltrack.Application.Contracts;
using Paltrack.Application.Interfaces.IRepositories;
using Paltrack.Domain.Entities;

namespace Paltrack.Application.Services
{
    public class LogisticsPartnerService : ILogisticsPartnerService
    {
        private readonly ILogisticsPartnerRepository _logisticsPartnerRepository;

        public LogisticsPartnerService(ILogisticsPartnerRepository logisticsPartnerRepository)
        {
            _logisticsPartnerRepository = logisticsPartnerRepository;
        }

        public async Task<ServiceResult<PaginationResult<LogisticsPartner>>> GetAllAsync(LogisticsPartnerFilter filter)
        {
            return await _logisticsPartnerRepository.GetAllAsync(filter);
        }
        public IQueryable<LogisticsPartner> GetQueryable()
        {
            return _logisticsPartnerRepository.GetQueryable();
        }
    }
}
