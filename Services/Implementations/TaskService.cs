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
    }
}

