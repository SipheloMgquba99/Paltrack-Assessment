public class LogisticsPartnerFilter
{
    public string? Company { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Email { get; set; }
    public string? Search { get; set; } 
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
