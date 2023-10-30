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
    [Route("[controller]/[action]/{taskId:int}/{isEditting:bool}")]
    public async Task<IActionResult> TaskView(int taskId, bool isEditting) {
        var response = await _taskService.GetTaskResponse(taskId);
        var data = response.Data;

        var model = new ViewTaskViewModel() {
            Name = data.Name,
            Priority = data.Priority,
            Description = data.Description,
            Status = data.Status,
            Id = data.Id,
        };

        if(response.StatusCode == Domain.Enum.StatusCode.OkResult) {
            ViewBag.IsEditting = isEditting;
            return View(model);
        } else {
            return BadRequest(new {description = response.Description });
        }
    }

    [HttpPut]
    [Route("[controller]/[action]/{taskId:int}")]
    public async Task<IActionResult> ExecuteTask(int taskId) {
        var response = await _taskService.GetExecuteTaskResponse(taskId);

        if(response.StatusCode == Domain.Enum.StatusCode.OkResult) {
            return View("TaskView", response.Data);
        } else {
            return BadRequest(new {description = response.Description});
        }
    }

    [HttpPut]
    [Route("[controller]/[action]/{taskId:int}")]
    public async Task<IActionResult> EditTask(int taskId) {
        var taskResponse = await _taskService.GetTaskResponse(taskId);
        var data = taskResponse.Data;

        var model = new EditTaskViewModel() {
            Id = taskId,
            Name = data.Name,
            Description = data.Description,
            TaskPriority = data.Priority
        };
        var response = await _taskService.GetEditTaskResponse(model);

        if(response.StatusCode == Domain.Enum.StatusCode.OkResult) {
            ViewBag.IsEditted = false;
            return View("TaskView", response.Data);
        } else {
            return BadRequest(new { decription = response.Description });
        }
    }
}

