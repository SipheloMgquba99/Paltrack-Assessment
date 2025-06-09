namespace Paltrack.Application.Dtos
{
    public record LogisticPartnersDto(
        Guid Id,
        string CompanyName,
        string Email,
        string Phone,
        DateTime Date,
        string City,
        string Country
    );
}
