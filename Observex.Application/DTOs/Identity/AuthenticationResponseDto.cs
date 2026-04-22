namespace Observex.Application.DTOs.Identity
{
    public class AuthenticationResponseDto
    {
        public required string FullName { get; set;  }
        public required string DisplayUserName { get; set;  }
        public required string Token { get; set;  }
        public DateTime ExpirationTime { get; set; }
    }
}
