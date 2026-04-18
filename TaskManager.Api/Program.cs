using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<TaskService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/api/tasks/{userId:int}/{taskId:int}", async (int userId, int taskId, TaskService taskService) =>
{
    try
    {
        var task = await taskService.GetTaskByIdAsync(userId, taskId);
        return Results.Ok(task);
    }
    catch (TaskValidationException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
    catch (TaskNotFoundException ex)
    {
        return Results.NotFound(new { message = ex.Message });
    }
    catch (TaskForbiddenException ex)
    {
        return Results.StatusCode(StatusCodes.Status403Forbidden);
    }
});

app.Run();


