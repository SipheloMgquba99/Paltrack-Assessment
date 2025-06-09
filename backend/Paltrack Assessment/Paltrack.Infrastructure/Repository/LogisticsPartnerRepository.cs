using Microsoft.EntityFrameworkCore;
using Paltrack.Application.Common;
using Paltrack.Application.Interfaces.IRepositories;
using Paltrack.Domain.Entities;

public class LogisticsPartnerRepository : ILogisticsPartnerRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<LogisticsPartner> _dbSet;

    public LogisticsPartnerRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<LogisticsPartner>();
    }

    public async Task<ServiceResult<PaginationResult<LogisticsPartner>>> GetAllAsync(LogisticsPartnerFilter filter)
    {
        try
        {
            var query = ApplyFilters(_dbSet.AsNoTracking(), filter);
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var pagedResult = new PaginationResult<LogisticsPartner>
            {
                Items = items.AsQueryable(),
                TotalCount = totalCount,
                PageNumber = filter.PageIndex,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize)
            };

            return ServiceResult<PaginationResult<LogisticsPartner>>.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return ServiceResult<PaginationResult<LogisticsPartner>>.Failure($"An error occurred while retrieving logistics partners: {ex.Message}");
        }
    }
    public IQueryable<LogisticsPartner> GetQueryable()
    {
        return _dbSet.AsNoTracking();
    }
    private static IQueryable<LogisticsPartner> ApplyFilters(IQueryable<LogisticsPartner> query, LogisticsPartnerFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(lp =>
                lp.Company.Contains(filter.Search) ||
                lp.City.Contains(filter.Search) ||
                lp.Country.Contains(filter.Search) ||
                lp.Email.Contains(filter.Search));
        }

        if (!string.IsNullOrWhiteSpace(filter.Company))
            query = query.Where(lp => lp.Company.Contains(filter.Company));

        if (!string.IsNullOrWhiteSpace(filter.City))
            query = query.Where(lp => lp.City.Contains(filter.City));

        if (!string.IsNullOrWhiteSpace(filter.Country))
            query = query.Where(lp => lp.Country.Contains(filter.Country));

        if (!string.IsNullOrWhiteSpace(filter.Email))
            query = query.Where(lp => lp.Email.Contains(filter.Email));

        return query;
    }

}
