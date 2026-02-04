using ATM.Application.Contracts.Interfaces;
using ATM.Application.Contracts.Models;
using ATM.Application.Contracts.Operations;
using ATM.Presentation.Http.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ATM.Presentation.Http.Controllers;

[ApiController]
[Route("/api/account")]
public sealed class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("сreateAccount")]
    public ActionResult<AccountDto> CreateAccount([FromBody] CreateAccountRequest httpRequest)
    {
        var request = new CreateAccount.Request(httpRequest.Id, httpRequest.AccountNumber, httpRequest.Pincode);
        CreateAccount.Response response = _accountService.CreateUserAccount(request);

        return response switch
        {
            CreateAccount.Response.Success success => Ok(success.AccountDto),
            CreateAccount.Response.BadRequest failure => BadRequest(failure.Message),
            CreateAccount.Response.Unauthorized failure => Unauthorized(failure.Message),
            _ => throw new UnreachableException(),
        };
    }

    [HttpPost("deposit")]
    public ActionResult Deposit([FromBody] DepositRequest httpRequest)
    {
        var request = new Deposit.Request(httpRequest.Id, httpRequest.Amount);
        Deposit.Response response = _accountService.Deposit(request);

        return response switch
        {
            Deposit.Response.Success success => Ok(),
            Deposit.Response.BadRequest failure => BadRequest(failure.Message),
            Deposit.Response.Unauthorized failure => Unauthorized(failure.Message),
            _ => throw new UnreachableException(),
        };
    }

    [HttpPost("withdraw")]
    public ActionResult Withdraw([FromBody] WithdrawRequest httpRequest)
    {
        var request = new Withdraw.Request(httpRequest.Id, httpRequest.Amount);
        Withdraw.Response response = _accountService.Withdraw(request);

        return response switch
        {
            Withdraw.Response.Success success => Ok(),
            Withdraw.Response.BadRequest failure => BadRequest(failure.Message),
            Withdraw.Response.Unauthorized failure => Unauthorized(failure.Message),
            _ => throw new UnreachableException(),
        };
    }

    [HttpPost("viewingBalance")]
    public ActionResult<BalanceDto> ViewingViewing([FromBody] ViewingBalanceRequest httpRequest)
    {
        var request = new ViewingBalance.Request(httpRequest.Id);
        ViewingBalance.Response response = _accountService.ViewingBalance(request);

        return response switch
        {
            ViewingBalance.Response.Success success => Ok(success.BalanceDto),
            ViewingBalance.Response.BadRequest failure => BadRequest(failure.Message),
            ViewingBalance.Response.Unauthorized failure => Unauthorized(failure.Message),
            _ => throw new UnreachableException(),
        };
    }

    [HttpPost("getHistoryOperation")]
    public ActionResult<IEnumerable<OperationDto>> GetHistoryOperation([FromBody] GetHistoryOperationRequest httpRequest)
    {
        var request = new GetHistoryOperation.Request(httpRequest.Id);
        GetHistoryOperation.Response response = _accountService.GetHistoryOperation(request);

        return response switch
        {
            GetHistoryOperation.Response.Success success => Ok(success.OperationDtos),
            GetHistoryOperation.Response.Unauthorized failure => Unauthorized(failure.Message),
            _ => throw new UnreachableException(),
        };
    }
}