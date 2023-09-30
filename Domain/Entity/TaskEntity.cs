using System;
using ToDoApplication.Models.Enum;

namespace ToDoApplication.Models.Entity {

	public class TaskEntity : BaseEntity {

		public required string Name { get; set; }

		public string? Description { get; set; }

		public TaskPriority Priority { get; set; }

		public Enum.TaskStatus Status { get; set; }
	}
}

