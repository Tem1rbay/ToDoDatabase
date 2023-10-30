using System;
using ToDoApplication.Domain.Response;
using ToDoApplication.Models.Entity;
using ToDoApplication.Models.ViewModels.Task;

namespace ToDoApplication.Services.Interfaces {

	public interface ITaskService {

		Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model);
		Task<IBaseResponse<IEnumerable<ViewTaskViewModel>>> GetAllTasks();
		Task<IBaseResponse<TaskEntity>> GetExecuteTaskResponse(int taskId);
        Task<IBaseResponse<TaskEntity>> GetTaskResponse(int taskId);
		Task<IBaseResponse<TaskEntity>> GetEditTaskResponse(EditTaskViewModel model);
	}
}

