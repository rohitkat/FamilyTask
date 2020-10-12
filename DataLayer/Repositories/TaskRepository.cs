using Core.Abstractions.Repositories;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
	public class TaskRepository : BaseRepository<Guid, FamilyTask, TaskRepository>, ITaskRepository
	{
		protected readonly DbContext Context;
		protected IQueryable<FamilyTask> Query;

		public TaskRepository(FamilyTaskContext context) : base(context)
		{
			Context = context;
		}



		ITaskRepository IBaseRepository<Guid, FamilyTask, ITaskRepository>.NoTrack()
		{
			return base.NoTrack();
		}

		ITaskRepository IBaseRepository<Guid, FamilyTask, ITaskRepository>.Reset()
		{
		    Query = Context.Set<FamilyTask>().Include(v => v.AssignedMember).AsQueryable();
			var data = Query.ToList();
			return this as ITaskRepository;
		}

	}
}
