using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class DoItController : ControllerBase
{

    private readonly ILogger<DoItController> _logger;

    public DoItController(ILogger<DoItController> logger)
    {
        _logger = logger;
    }

    [HttpGet("todos")]
    public IActionResult GetToDos()
    {
        return Ok();
    }
}
