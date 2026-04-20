using Microsoft.AspNetCore.Identity;
using Observex.Core.Enums;

namespace Observex.Core.Entities
{
    public abstract class User : IdentityUser<Guid>
    {
        //public Guid Id { get; set; }
        public required string DisplayUserName { get; set; }
        //public required string Email { get; set; }
        public required string FullName { get; set; }
        //public UserRole Role { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
    }
}
