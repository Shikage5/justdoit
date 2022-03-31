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
    public IActionResult GetToDos([FromQuery] string projectName)
    {
        var todos = _context.ToDoItems.Where(i => i.ProjectName == projectName).ToList();

        return Ok(todos);

    }

    [HttpPost("new")]
    public IActionResult PostToDo([FromBody] ToDoItem addToDo)
    {

        var todo = new ToDoItem();
        todo.ProjectName = addToDo.ProjectName;
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
    public IActionResult UpdateToDo([FromBody] ToDoItem updateToDo)
    {
        var todo = _context.ToDoItems.FirstOrDefault(x => x.Id == updateToDo.Id);
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
    [HttpGet("getProjectNames")]
    public IActionResult GetProjectNames()
    {
        var projectNames = _context.ToDoItems.Select(i => i.ProjectName).Distinct().ToList();

        return Ok(projectNames);

    }
    [HttpDelete("deleteProject")]
    public IActionResult DeleteProject([FromQuery] string projectName)
    {
        var projectToDos = _context.ToDoItems.Where(i=>i.ProjectName==projectName).ToArray();
        if (projectToDos == null)
        {
            return BadRequest();
        }
        foreach (var item in projectToDos)
        {
            _context.ToDoItems.Remove(item);
        }
        _context.SaveChanges();
        return Ok();
    }
}