using Microsoft.AspNetCore.Mvc;
using Observex.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Observex.Application.DTOs.Identity
{
    public class RegisterDto
    {
        [Required]
        // ViewFeatures NuGet Package
        [Remote(action: "IsUserAlreadyRegisterd", controller: "Account", ErrorMessage = "Username is already in use.")]
        public required string UserName { get; set; }
        [Required]
        public required string FullName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        public required string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }

    }
}
