using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.DAL;
using ToDoApplication.Domain.Enum;
using ToDoApplication.Models.Entity;

namespace ToDoApplication.DAL {

	public class AppDbContext : DbContext {

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			
			Database.EnsureCreated();
		}

		public DbSet<TaskEntity> Tasks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.HasSequence<int>("seq_task_id");
            base.OnModelCreating(modelBuilder);
        }
    }
}

