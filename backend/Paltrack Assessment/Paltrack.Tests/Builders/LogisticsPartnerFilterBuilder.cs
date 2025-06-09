namespace Paltrack.Tests.Builders.Filters
{
    public class LogisticsPartnerFilterBuilder
    {
        private readonly LogisticsPartnerFilter _filter;

        public LogisticsPartnerFilterBuilder()
        {
            _filter = new LogisticsPartnerFilter
            {
                PageIndex = 1,
                PageSize = 10,
                Search = null,
                Company = null,
                City = null,
                Country = null,
                Email = null
            };
        }

        public LogisticsPartnerFilterBuilder WithPageIndex(int pageIndex)
        {
            _filter.PageIndex = pageIndex;
            return this;
        }

        public LogisticsPartnerFilterBuilder WithPageSize(int pageSize)
        {
            _filter.PageSize = pageSize;
            return this;
        }

        public LogisticsPartnerFilter Build()
        {
            return _filter;
        }
    }
}
