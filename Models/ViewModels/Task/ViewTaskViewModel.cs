using System;
using System.ComponentModel.DataAnnotations;
using ToDoApplication.Models.Enum;

namespace ToDoApplication.Models.ViewModels.Task {

	public class ViewTaskViewModel {

		[Display(Name = "Id заявки")]
		public int Id { get; set; }

		[Display(Name = "Название задачи")]
		public string Name { get; set; }

		[Display(Name = "Приоритет задачи")]
		public TaskPriority Priority { get; set; }

		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Статус задачи")]
		public Enum.TaskStatus Status { get; set; }

		[Display(Name = "Дата создания")]
		public DateTime CreatedDateTime { get; set; }

		public bool IsReadonly { get; set; }
    }
}

