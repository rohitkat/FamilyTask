
using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Pages
{
	public class ManageTaskBase : ComponentBase
	{

		[Inject]
		public ITaskDataService TaskDataService { get; set; }
		protected bool isAllTaskShow;

		protected void onTaskAdd(TaskVm memberTask)
		{
			TaskDataService.AddTask(memberTask);
		}

	}
}

