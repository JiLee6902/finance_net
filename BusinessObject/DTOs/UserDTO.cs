using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public string? DisplayName { get; set; }
        public string? Phone { get; set; }
        public decimal Balance { get; set; }
        public DateTime Birthdate { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
