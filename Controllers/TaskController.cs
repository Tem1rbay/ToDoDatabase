using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;
using ToDoApplication.Models.ViewModels.Task;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Controllers;

public class TaskController : Controller {
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService) {
        _taskService = taskService;
    }

    public IActionResult Index() {
        return View();
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTaskViewModel model) {
        var response = await _taskService.Create(model);
        if(response.StatusCode == Domain.Enum.StatusCode.OkResult) {
            return Ok(new { description = response.Description, id = response.Data.Id});
        }
        return BadRequest(new { description = response.Description });
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> GetTable() {

        var response = await _taskService.GetAllTasks();

        if(response.StatusCode == Domain.Enum.StatusCode.OkResult) {
            return View(response.Data);
        } else {
            return BadRequest(new { description = response.Description });
        }
    }

    [HttpGet]
    [Route("[controller]/[action]/{taskId:int}")]
    public async Task<IActionResult> TaskView(int taskId) {
        var response = await _taskService.GetTaskResponse(taskId);
        
        if(response.StatusCode == Domain.Enum.StatusCode.OkResult) {
            return View(response.Data);
        } else {
            return BadRequest(new {description = response.Description });
        }
    }
}

