using System.Net.Mail;
using System.Net;
using System.Text;
using System;
using System.Threading.Tasks;

namespace QuickExamLibrary.Controllers
{
    public class LoginController : ControllerBase
    {
        public LoginController()
        {
            
        }

        public async Task<bool> ForgotPassword(string emailAddress)
        {
            if (await SQLConnector().EmailAddressExists(emailAddress))
            {
                string newPassword = GeneratePassword();
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(GlobalConfig.SchoolEmailAddress());
                    mail.To.Add(emailAddress);
                    mail.Subject = "QuickExams - New Password";
                    mail.Body = "This email is from QuickExams and it looks like you have forgotten your password! \n Here is a new one: " + newPassword;

                    using (SmtpClient client = new SmtpClient("smtp.live.com", 587))
                    {                      
                        client.Credentials = new NetworkCredential(GlobalConfig.SchoolEmailAddress(), GlobalConfig.SchoolEmailAddressPassword());
                        client.EnableSsl = true;
                        client.Send(mail);
                        await SQLConnector().ChangePassword(emailAddress, newPassword);
                        return true;
                    }
                }
            }
            else return false;                               
        }

        private string GeneratePassword()
        {
            int passwordLength = 16;
            const string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random random = new Random();
            while (0 < passwordLength--)
            {
                res.Append(validCharacters[random.Next(validCharacters.Length)]);
            }
            return res.ToString();
        }
    }
}
