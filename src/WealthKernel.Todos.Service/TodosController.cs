using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Net;
using System.Threading.Tasks;
using WealthKernel.Todos.Data;
using WealthKernel.Todos.Data.Models;

namespace WealthKernel.Todos.Service;

[ApiController]
public class TodosController : ControllerBase
{
    // defined in Program with Ioc 
    private readonly InMemoryTodosRepository _repository;

    public TodosController(InMemoryTodosRepository respository)
    {
        _repository = respository;
    }

    [HttpGet("api/todo")]
    public async Task<ActionResult<Todo>> GetTodo([FromQuery] string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest();

        try
        {
            var todo = await _repository.GetTodo(id);
            if (todo == null)
                return NotFound("Empty todo");

            return Ok(todo);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    
    [HttpPost("api/addtodo")]
    public async Task<ActionResult> AddTodo([FromBody] Todo todo)
    {
        if (todo == null )
            return BadRequest();

        try
        {
            await _repository.AddTodo(todo);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
        
    }
}


