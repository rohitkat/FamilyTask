using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.ViewModel
{
	public class TaskVm
	{
		public Guid Id { get; set; }
		public Nullable<Guid> AssignedMemberId { get; set; }
		public bool IsComplete { get; set; }

		[Required(ErrorMessage = "Enter task name")]
		public string Subject { get; set; }
		public MemberVm AssignedMember { get; set; }

	}
}
