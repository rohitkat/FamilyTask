using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Queries
{
	public class GetAllTaskQueryResult
	{
        public List<TaskVm> Payload { get; set; }
	}
}
