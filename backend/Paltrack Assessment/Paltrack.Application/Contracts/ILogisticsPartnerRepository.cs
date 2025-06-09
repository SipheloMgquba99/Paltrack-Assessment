using Paltrack.Application.Common;
using Paltrack.Domain.Entities;

namespace Paltrack.Application.Interfaces.IRepositories
{
    public interface ILogisticsPartnerRepository
    {
        Task<ServiceResult<PaginationResult<LogisticsPartner>>> GetAllAsync(LogisticsPartnerFilter filter);
        IQueryable<LogisticsPartner> GetQueryable();

    }
}
