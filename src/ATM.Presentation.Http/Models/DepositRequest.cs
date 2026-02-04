namespace ATM.Presentation.Http.Models;

public class DepositRequest
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }
}