using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Woodshed.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    [MaxLength(50)]
    public string? DisplayName { get; set; }

    [MaxLength(1000)]
    public string? Biography { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }
}
