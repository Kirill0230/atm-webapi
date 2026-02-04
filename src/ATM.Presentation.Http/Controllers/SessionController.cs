using ATM.Application.Contracts.Interfaces;
using ATM.Application.Contracts.Models;
using ATM.Application.Contracts.Operations;
using ATM.Presentation.Http.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ATM.Presentation.Http.Controllers;

[ApiController]
[Route("/api/session")]
public sealed class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("createAdminSession")]
    public ActionResult<SessionDto> CreateAdminSession([FromBody] CreateAdminSessionRequest httpRequest)
    {
        var request = new CreateAdminSession.Request(httpRequest.SystemPassword);
        CreateAdminSession.Response response = _sessionService.CreateAdminSession(request);

        return response switch
        {
            CreateAdminSession.Response.Success success => Ok(success.Session),
            CreateAdminSession.Response.Failure failure => Unauthorized(failure.Message),
            _ => throw new UnreachableException(),
        };
    }

    [HttpPost("createUserSession")]
    public ActionResult<SessionDto> CreateUserSession([FromBody] CreateUserSessionRequest httpRequest)
    {
        var request = new CreateUserSession.Request(httpRequest.NumberAccount, httpRequest.Pincode);
        CreateUserSession.Response response = _sessionService.CreateUserSession(request);

        return response switch
        {
            CreateUserSession.Response.Success success => Ok(success.Session),
            CreateUserSession.Response.Failure failure => Unauthorized(failure.Message),
            _ => throw new UnreachableException(),
        };
    }
}