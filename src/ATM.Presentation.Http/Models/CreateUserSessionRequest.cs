using System.ComponentModel.DataAnnotations;

namespace ATM.Presentation.Http.Models;

public sealed class CreateUserSessionRequest
{
    [Range(minimum: 1, maximum: long.MaxValue)]
    public long NumberAccount { get; set; }

    [Range(minimum: 0, maximum: 9999)]
    public long Pincode { get; set; }
}