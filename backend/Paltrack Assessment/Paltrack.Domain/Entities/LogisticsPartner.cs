namespace Paltrack.Domain.Entities
{
    public class LogisticsPartner
    {
        public Guid Id { get; set; }
        public string Company { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
