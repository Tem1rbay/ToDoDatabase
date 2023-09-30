using System;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.Models.Entity;

namespace ToDoApplication.DAL {

	public class AppDbContext : DbContext {

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			Database.EnsureCreated();
		}

		public DbSet<TaskEntity> Tasks { get; set; }
	}
}

