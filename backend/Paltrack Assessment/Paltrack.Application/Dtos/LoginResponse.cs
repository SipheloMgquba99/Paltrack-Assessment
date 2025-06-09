namespace Paltrack.Application.Dtos
{
    public record LoginResponse(bool Flag, string Message = "", string Token = "");

}
