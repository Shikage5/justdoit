using Microsoft.AspNetCore.Mvc;
using JustDoIt.Models;
using JustDoIt.Data;
using JustDoIt.ViewModels;

[ApiController]
[Route("api/[controller]")]
public class DoItController : ControllerBase
{

    private readonly ILogger<DoItController> _logger;
    private readonly JustDoItDBContext _context;

    public DoItController(ILogger<DoItController> logger, JustDoItDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("get")]
    public IActionResult GetToDos()
    {
        var todos = _context.ToDoItems.ToList()!;

        return Ok(todos);

    }
    [HttpPost("new")]
    public IActionResult PostToDo([FromBody] AddToDoVm addToDo)
    {

        var todo = new ToDoItem();
        todo.Name = addToDo.Name;
        todo.Priority = addToDo.Priority;
        todo.DueDate = addToDo.DueDate;
        todo.DueTime = addToDo.DueTime;
        todo.Status = addToDo.Status;

        _context.ToDoItems.Add(todo);
        _context.SaveChanges();

        return Ok(todo);
    }
    [HttpPost("update")]
    public IActionResult UpdateToDo([FromQuery] int Id, [FromBody] AddToDoVm updateToDo)
    {
        var todo = _context.ToDoItems.FirstOrDefault(x => x.Id == Id);
        if (todo == null)
        {
            return BadRequest();
        }
        todo.Name = updateToDo.Name;
        todo.Priority = updateToDo.Priority;
        todo.DueDate = updateToDo.DueDate;
        todo.DueTime = updateToDo.DueTime;
        todo.Status = updateToDo.Status;

        _context.ToDoItems.Update(todo);
        _context.SaveChanges();

        return Ok(todo);
    }
    [HttpDelete("delete")]
    public IActionResult DeleteToDo([FromQuery] int Id)
    {

        var todo = _context.ToDoItems.FirstOrDefault(x => x.Id == Id);
        if (todo == null)
        {
            return BadRequest();
        }
        _context.ToDoItems.Remove(todo);
        _context.SaveChanges();
        return Ok();
    }
}