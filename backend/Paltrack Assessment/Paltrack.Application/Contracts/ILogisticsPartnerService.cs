using Paltrack.Application.Common;
using Paltrack.Domain.Entities;

namespace Paltrack.Application.Contracts
{
    public interface ILogisticsPartnerService
    {
        Task<ServiceResult<PaginationResult<LogisticsPartner>>> GetAllAsync(LogisticsPartnerFilter filter);
        IQueryable<LogisticsPartner> GetQueryable();
    }
}
