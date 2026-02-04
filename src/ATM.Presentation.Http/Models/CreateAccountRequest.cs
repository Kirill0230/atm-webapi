using System.ComponentModel.DataAnnotations;

namespace ATM.Presentation.Http.Models;

public class CreateAccountRequest
{
    public Guid Id { get; set; }

    [Range(minimum: 0, maximum: long.MaxValue)]
    public long AccountNumber { get; set; }

    [Range(minimum: 0, maximum: 9999)]
    public long Pincode { get; set; }
}