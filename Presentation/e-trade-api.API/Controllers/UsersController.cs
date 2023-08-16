using e_trade_api.application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_trade_api.API.Controllers;

[Route("api/[controller]")]
// [Authorize]
[ApiController]
public class UsersController : ControllerBase
{
    readonly IMediator _mediator;
    readonly IMailService _mailService;

    public UsersController(IMediator mediator, IMailService mailService)
    {
        _mediator = mediator;
        _mailService = mailService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
    {
        CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
    {
        LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
        return Ok(response);
    }

    [HttpPost("googlelogin")]
    public async Task<IActionResult> GoogleLogin(
        GoogleLoginCommandRequest googleLoginCommandRequest
    )
    {
        GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> ExampleMailTest()
    {
        await _mailService.SendMessageAsync(
            "umuttsobeksk@gmail.com",
            "Örnek Mail",
            "<strong>Bu bir örnek maildir.</strong>"
        );
        return Ok();
    }

    // [HttpGet("{UserId}")] //sakın userid olarak yazma gidior
    // [Authorize(AuthenticationSchemes = "Admin")]
    // public async Task<IActionResult> GetUserBasketId(
    //     [FromRoute] GetUserBasketIdQueryRequest getUserBasketIdQueryRequest
    // )
    // {
    //     GetUserBasketIdQueryResponse response = await _mediator.Send(getUserBasketIdQueryRequest);
    //     return Ok(response);
    // }
}
