namespace Restaurant.DTO
{
    public class token
    {
        public record AccessResponse(
            string Message,
            string AccessToken
        );
    }
}