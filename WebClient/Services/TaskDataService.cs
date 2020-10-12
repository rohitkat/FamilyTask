using Domain.Commands;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;
using WebClient.Shared.Models;
using Core.Extensions.ModelConversion;
using Domain.ViewModel;
using Domain.Queries;

namespace WebClient.Services
{
	public class TaskDataService : ITaskDataService
	{
		private readonly HttpClient httpClient;
		public event EventHandler TaskChanged;

		public TaskDataService(IHttpClientFactory clientFactory)
		{
			httpClient = clientFactory.CreateClient("FamilyTaskAPI");
			Tasks = new List<TaskVm>();
			if (Tasks.Count == 0)
				LoadTasks();
		}

		private async void LoadTasks()
		{
			Tasks = (await GetAllTasks()).Payload;
			TaskChanged?.Invoke(this, null);
		}

		private async Task<GetAllTaskQueryResult> GetAllTasks()
		{
			return await httpClient.GetJsonAsync<GetAllTaskQueryResult>("task");
		}


		public List<TaskVm> Tasks { get; private set; }
		public TaskVm SelectedTask { get; private set; }
		public TaskVm DraggedTask { get; set; }

		public event EventHandler TasksUpdated;
		public event EventHandler TaskSelected;

		public void SelectTask(Guid id)
		{
			SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);
			TasksUpdated?.Invoke(this, null);
		}

		public async void ToggleTask(Guid id)
		{
			TaskVm taskModel = Tasks.Where(b => b.Id == id).FirstOrDefault();
			taskModel.IsComplete = !taskModel.IsComplete;
			var result = await Update(taskModel.ToUpdateTaskCommand());
			if (result != null)
			{
				var updatedList = (await GetAllTasks()).Payload;

				if (updatedList != null)
				{
					Tasks = updatedList;
					TasksUpdated?.Invoke(this, null);
					return;
				}
			}
		}
		
		
		public async void DeleteTask(Guid id)
		{
			var result = await Delete(id);
			if (result != null)
			{
				var updatedList = (await GetAllTasks()).Payload;

				if (updatedList != null)
				{
					Tasks = updatedList;
					TasksUpdated?.Invoke(this, null);
					return;
				}
			}
		}

		public async void UpdateTask(TaskVm model)
		{			
			var result = await Update(model.ToUpdateTaskCommand());
			if (result != null)
			{
				var updatedList = (await GetAllTasks()).Payload;

				if (updatedList != null)
				{
					Tasks = updatedList;
					TasksUpdated?.Invoke(this, null);
					return;
				}
			}
		}

		public async void AddTask(TaskVm model)
		{
			var result = await Create(model.ToCreateTaskCommand());

			if (result != null)
			{
				var updatedList = (await GetAllTasks()).Payload;

				if (updatedList != null)
				{
					Tasks = updatedList;
					TasksUpdated?.Invoke(this, null);
					return;
				}
			}
		}


		private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
		{
			return await httpClient.PostJsonAsync<CreateTaskCommandResult>("task", command);
		}


		private async Task<UpdateTaskCommandResult> Update(UpdateTaskCommand command)
		{
			return await httpClient.PutJsonAsync<UpdateTaskCommandResult>($"task/{command.Id}", command);
		}
		
		private async Task<UpdateTaskCommandResult> Delete(Guid id)
		{
			return await httpClient.PostJsonAsync<UpdateTaskCommandResult>($"task/{id}",null);
		}

	}
}