using Domain.Commands;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
	public interface ITaskServiceCustom
	{
		Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);
		Task<GetAllTaskQueryResult> GetAllTasksQueryHandler();
		Task<UpdateTaskCommandResult> UpdateTaskCommandHandler(UpdateTaskCommand command);
		Task<UpdateTaskCommandResult> DeleteTaskCommandHandler(Guid id);

	}
}
