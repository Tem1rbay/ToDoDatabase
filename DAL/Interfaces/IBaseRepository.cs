using System;
using ToDoApplication.Models.Entity;

namespace ToDoApplication.DAL.Interfaces {

	public interface IBaseRepository<T>	{

		Task Create(T entity);

		IQueryable<T> GetAll();

		Task<T> Update(T entity);

		Task Delete(T entity);
	}
}

