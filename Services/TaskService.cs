using AutoMapper;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.DataModels;
using Domain.Queries;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TaskServiceCustom : ITaskServiceCustom
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskServiceCustom(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command)
        {
            try
            {
                var task = _mapper.Map<FamilyTask>(command);
                var persistedMember = await _taskRepository.CreateRecordAsync(task);

                var vm = _mapper.Map<TaskVm>(persistedMember);

                return new CreateTaskCommandResult()
                {
                    Payload = vm
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<GetAllTaskQueryResult> GetAllTasksQueryHandler()
        {
            try
            {
                List<TaskVm> vm = new List<TaskVm>();
                var tasks = await _taskRepository.Reset().ToListAsync();
                
                if (tasks != null && tasks.Any())
                    vm = _mapper.Map<List<TaskVm>>(tasks);
                return new GetAllTaskQueryResult()
                {
                    Payload = vm
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<UpdateTaskCommandResult> UpdateTaskCommandHandler(UpdateTaskCommand command)
        {
            try
            {
                var isSucceed = true;
                var task = await _taskRepository.ByIdAsync(command.Id);
                task.AssignedMemberId = command.AssignedMemberId;
                task.Subject = command.Subject;
                task.IsComplete = command.IsComplete;

                var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);
                if (affectedRecordsCount < 1)
                    isSucceed = false;

                return new UpdateTaskCommandResult()
                {
                    Succeed = isSucceed
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public async Task<UpdateTaskCommandResult> DeleteTaskCommandHandler(Guid id)
        {
            try
            {
                var isSucceed = true;
                var affectedRecordsCount = await _taskRepository.DeleteRecordAsync(id);
                if (affectedRecordsCount < 1)
                    isSucceed = false;
                return new UpdateTaskCommandResult()
                {
                    Succeed = isSucceed
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
