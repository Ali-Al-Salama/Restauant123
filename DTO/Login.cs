namespace Restaurant.DTO
{
        public record LoginRequest(
        string Email,
        string PasswordHash
    );
    public record LoginResponse(
        string Message,
        string Email,
        string Name,
        string Phone,
        string Address,
        string AccessToken,
        string RefreshToken
    );
}