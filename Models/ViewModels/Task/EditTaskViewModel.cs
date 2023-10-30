using System;
using ToDoApplication.Models.Enum;

namespace ToDoApplication.Models.ViewModels.Task {

	public class EditTaskViewModel {

        public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public TaskPriority TaskPriority { get; set; }

        public void Validate() {
            if (string.IsNullOrEmpty(Name)) {
                throw new ArgumentNullException(Name, "Нужно указать название задачи");
            }
            if (string.IsNullOrEmpty(Description)) {
                throw new ArgumentNullException(Description, "Нужно указать описание задачи");
            }
        }
    }
}

