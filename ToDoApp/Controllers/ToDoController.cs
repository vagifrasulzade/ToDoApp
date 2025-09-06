using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data;
using ToDoApp.DTOs;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IToDoService _service;
    public ToDoController(IToDoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> Get()
    {
        return (await _service.GetToDoItemsAsync()).ToArray();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItemDTO>> Get(int id)
    {
        var item = await _service.GetToDoItemAsync(id);
        return item is not null ? item : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItemDTO>> Post([FromBody] ToDoItemCreateDTO request)
    {
        return await _service.CreateToDoAsync(request);
    }

    [HttpPatch]
    public async Task<ActionResult<ToDoItemDTO>> Patch(int id, bool isCompleted)
    {
        return await _service.ChangeToDoStatusAsync(id, isCompleted);
    }
}
