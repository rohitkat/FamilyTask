using Domain.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.ViewModel
{
    public class MemberVm
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Enter last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Enter email")]
        [EmailAddress]
        public string Email { get; set; }
        public string Roles { get; set; }

        public string Avatar { get; set; }

        public UpdateMemberCommand ToUpdateCommand()
        {
            throw new NotImplementedException();
        }
    }
}
