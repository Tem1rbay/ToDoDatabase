using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models.Enum {

	public enum TaskPriority {
		[Display(Name = "Простая")]
		Easy,

		[Display(Name = "Важная")]
		Medium,

		[Display(Name = "Критичная")]
		Hard
    }
}

