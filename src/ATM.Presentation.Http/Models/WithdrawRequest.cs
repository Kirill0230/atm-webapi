namespace ATM.Presentation.Http.Models;

public class WithdrawRequest
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }
}