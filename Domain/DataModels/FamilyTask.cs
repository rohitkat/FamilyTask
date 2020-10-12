using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.DataModels
{
	public class FamilyTask
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public Guid? AssignedMemberId { get; set; }
		public bool IsComplete { get; set; }
		public string Subject { get; set; }

		public virtual Member AssignedMember { get; set; }
	}
}
