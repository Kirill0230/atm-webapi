using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Http.Models;

public class CreateAdminSessionRequest
{
    [Required]
    [NotNull]
    public string? SystemPassword { get; set; }
}