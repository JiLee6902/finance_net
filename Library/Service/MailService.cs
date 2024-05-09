using Application.IService;
using BusinessObject.ServiceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DTOs;

namespace Library.Service
{
    public class MailService : IMailService
    {
        public string GetMailBodyToConfirmMail(string url, AccountDTO accountDTO)
        {
            url = url + accountDTO.UserName;

            return string.Format(@"<div style='text-align:center;'>
                                    <h1 style='color: #4D61B9'>Hi {1}, welcome to RECTEM</h1>
                                    <h3>Click below button for verify your Email Address</h3>
                                    <form method='post' action='{0}' style='display: inline;'>
                                      <button type = 'submit' style=' display: block;
                                                                    text-align: center;
                                                                    font-weight: bold;
                                                                    background-color: #008CBA;
                                                                    font-size: 16px;
                                                                    border-radius: 10px;
                                                                    color:#ffffff;
                                                                    cursor:pointer;
                                                                    width:100%;
                                                                    padding:10px;'>
                                        Confirm Mail
                                      </button>
                                    </form>
                                </div>", url, accountDTO.UserName);
        }

        public string GetMailBodyToChangePassword(int otpCode)
        {
            return string.Format(@"<div>
                                    <h1 style='text-align:center; color: #4D61B9'>Welcome to RECTEM</h1>
                                    <p>Here is your OTP Code to change your Password</p>
                                    <h2>{0}</h2>
                                </div>", otpCode);
        }

        public async Task<string> SendMail(MailClass mailClass)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(mailClass.FromMailId);
                    mailClass.ToMailIds.ForEach(x =>
                    {
                        mail.To.Add(x);
                    });
                    mail.Subject = mailClass.Subject;
                    mail.Body = mailClass.Body;
                    mail.IsBodyHtml = mailClass.IsBodyHtml;
                    mailClass.Attachments.ForEach(x =>
                    {
                        mail.Attachments.Add(new Attachment(x));
                    });
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(mailClass.FromMailId, mailClass.FromMailIdPassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                        return "Mail is sent successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
       
    }
}
