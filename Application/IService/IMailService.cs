using BusinessObject.DTOs;
using BusinessObject.ServiceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IMailService
    {
        public string GetMailBodyToConfirmMail(string url, AccountDTO accountDTO);
        public string GetMailBodyToChangePassword(int otpCode);
        public Task<string> SendMail(MailClass mailClass);
    }
}
