using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ClientSideModels
{
	public class TaskItem
	{
		public Guid Id { get; set; }
		public Nullable<Guid> AssignedMemberId { get; set; }
		public bool IsComplete { get; set; }
		public string Subject { get; set; }

	}
}
