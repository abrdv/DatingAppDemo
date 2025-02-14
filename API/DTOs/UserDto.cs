using Microsoft.Extensions.Primitives;

namespace API.DTOs
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Token { get; set; }
    }
}
