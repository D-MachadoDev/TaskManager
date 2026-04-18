using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{userId:int}/{taskId:int}")]
    public async Task<IActionResult> GetById(int userId, int taskId)
    {
        try
        {
            var task = await _taskService.GetTaskByIdAsync(userId, taskId);
            return Ok(task);
        }
        catch (TaskValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (TaskNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (TaskForbiddenException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        try
        {
            var tasks = await _taskService.GetAllByUserIdAsync(userId);
            return Ok(tasks);
        }
        catch (TaskValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody, Required] CreateTaskRequest request)
    {
        try
        {
            var createdTask = await _taskService.CreateAsync(request.Title, request.Description, request.UserId);
            return CreatedAtAction(nameof(GetById), new { userId = createdTask.UserId, taskId = createdTask.Id }, createdTask);
        }
        catch (TaskValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("{userId:int}/{taskId:int}/complete")]
    public async Task<IActionResult> CompleteTask(int userId, int taskId)
    {
        try
        {
            await _taskService.CompleteTaskAsync(taskId, userId);
            return NoContent();
        }
        catch (TaskValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (TaskNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (TaskForbiddenException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (TaskAlreadyCompletedException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{userId:int}/{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int userId, int taskId)
    {
        try
        {
            await _taskService.DeleteTaskAsync(taskId, userId);
            return NoContent();
        }
        catch (TaskValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (TaskNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (TaskForbiddenException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
    }
}

public sealed class CreateTaskRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1, int.MaxValue)]
    public int UserId { get; set; }
}
