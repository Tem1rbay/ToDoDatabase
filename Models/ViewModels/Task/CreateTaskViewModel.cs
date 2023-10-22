using System;
using System.ComponentModel.DataAnnotations;
using ToDoApplication.Models.Enum;

namespace ToDoApplication.Models.ViewModels.Task {

	public class CreateTaskViewModel {

		public string Name { get; set; }

		public string Description { get; set; }

		public TaskPriority Priority { get; set; }

		public void Validate() {
			if(string.IsNullOrEmpty(Name)) {
				throw new ArgumentNullException(Name, "Нужно указать название задачи");
			}
			if(string.IsNullOrEmpty(Description)) {
				throw new ArgumentNullException(Description, "Нужно указать описание задачи");
			}
		}
	}
}

