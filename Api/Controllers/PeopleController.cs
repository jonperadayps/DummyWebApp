using System.Globalization;
using Application.Data.People.Commands;
using Application.Models.Dtos.People;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/people")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;
    
    // GET
    public PeopleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var dto = new CreatePersonDto
        {
            Name = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
        };

        return Ok(await _mediator.Send(new CreatePersonCommand(dto), cancellationToken));
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreatePost(CancellationToken cancellationToken)
    {
        var dto = new CreatePersonDto
        {
            Name = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
        };

        return Ok(await _mediator.Send(new CreatePersonCommand(dto), cancellationToken));
    }
}