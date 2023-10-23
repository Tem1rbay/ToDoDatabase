using System;
using ToDoApplication.DAL.Interfaces;
using ToDoApplication.Domain.Enum;
using ToDoApplication.Domain.Response;
using ToDoApplication.Models.Entity;
using ToDoApplication.Models.ViewModels.Task;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services.Implementations {

    public class TaskService : ITaskService {

        private readonly IBaseRepository<TaskEntity> _taskRepository;
        private ILogger<TaskEntity> _logger;

        public TaskService(IBaseRepository<TaskEntity> taskRepository,
            ILogger<TaskEntity> logger) {

            _taskRepository = taskRepository;
            _logger = logger;  
        }

        public async Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model) {
            try {
                model.Validate();

                _logger.LogInformation($"Request to database with task name - {model.Name}");

                var tasks = _taskRepository.GetAll();

                var task = tasks
                    .Where(x => x.DateCreated.Date == DateTime.Today)
                    .FirstOrDefault(t => t.Name == model.Name);
                if(task != null) {
                    return new BaseResponse<TaskEntity>() {
                        Description = "Задача с таким названием уже существует в базе",
                        StatusCode = StatusCode.TaskIsHasAlready,
                    };
                }
                
                task = new TaskEntity() {
                    Name = model.Name,
                    Description = model.Description,
                    Priority = model.Priority,
                    Status = Models.Enum.TaskStatus.NotDone,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };

                await _taskRepository.Create(task);

                _logger.LogInformation("Task created {0} {1}", task.Name, task.DateCreated);
                return new BaseResponse<TaskEntity>() {
                    StatusCode = StatusCode.OkResult,
                    Description = "Задача успешно создалась",
                    Data = task
                };
            }
            catch(Exception ex) {
                _logger.LogError($"[TaskService.Create]: {ex.Message}");
                return new BaseResponse<TaskEntity>() {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"{ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<ViewTaskViewModel>>> GetAllTasks() {
            try {
                var tasks = _taskRepository.GetAll()
                    .Select(x => new ViewTaskViewModel() {
                        Id = x.Id,
                        Name = x.Name,
                        Priority = x.Priority,
                        Description = x.Description,
                        Status = x.Status,
                        CreatedDateTime = DateTime.Now
                    });

                _logger.LogInformation($"[TaskService.GetAllTasks]: Succeed in receiving all tasks");
                return new BaseResponse<IEnumerable<ViewTaskViewModel>>() {
                    Data = tasks,
                    StatusCode = StatusCode.OkResult,
                    Description = "Список задач"
                };
            } catch(Exception ex) {
                _logger.LogError($"[TaskService.GetTasks]: {ex.Message}");
                return new BaseResponse<IEnumerable<ViewTaskViewModel>>() {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"{ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<TaskEntity>> GetTaskResponse(int taskId) {
            try {
                var task = _taskRepository.GetItem(taskId);

                _logger.LogInformation($"[TaskService.GetTaskResponse]: Succeed in receiving task with Id {taskId}");
                return new BaseResponse<TaskEntity>() {
                    Data = task,
                    StatusCode = StatusCode.OkResult,
                    Description = "Субъект задачи"
                };
            } catch(Exception ex) {
                _logger.LogError($"Fail to receive task with Id: {taskId}");
                return new BaseResponse<TaskEntity>() {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"{ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<TaskEntity>> GetExecuteTaskResponse(int taskId) {
            try {
                var task = _taskRepository.GetItem(taskId);
                task.Status = Models.Enum.TaskStatus.Done;

                await _taskRepository.Update(task);

                _logger.LogInformation($"Task executed {task.Name} ({task.Id})");

                return new BaseResponse<TaskEntity>() {
                    StatusCode = StatusCode.OkResult,
                    Description = "Задача успешно выполнена",
                    Data = task
                };
            } catch(Exception ex) {
                _logger.LogError($"[TaskService.ExecuteTask]: {ex.Message}");
                return new BaseResponse<TaskEntity>() {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"{ex.Message}"
                };
            }
        }
    }
}

