using Microsoft.AspNetCore.Mvc;
using JustDoIt.Models;
using JustDoIt.Data;
[ApiController]
[Route("[controller]")]
public class DoItController : ControllerBase
{

    private readonly ILogger<DoItController> _logger;
    private readonly JustDoItDBContext _context;

    public DoItController(ILogger<DoItController> logger, JustDoItDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IEnumerable<ToDoItem> GetToDos()
    {


    }
}
