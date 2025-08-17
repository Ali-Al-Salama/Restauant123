
namespace Restaurant.DTO
{
    public record ResetRequest(
        long Code,
        string NewPassword
    );
}