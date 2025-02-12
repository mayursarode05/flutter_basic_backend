using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserDto
    {
        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string? Password { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? CreatedBy { get; set; } = DateTime.Now;
    }

}
