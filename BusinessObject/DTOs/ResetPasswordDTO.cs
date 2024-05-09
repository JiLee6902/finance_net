using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class ResetPasswordDTO
    {
        public string Password { get; set; }
        public string UserNameOrEmail { get; set; }
        public int OTPCode { get; set; }
        public int AccountId { get; set; }
    }
}
