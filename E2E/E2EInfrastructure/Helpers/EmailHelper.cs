using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace E2EInfrastructure.Helpers
{
    public static class EmailHelper
    {
        private static string smtpHost = ConfigurationManager.AppSettings["SMTPHost"] != null ? ConfigurationManager.AppSettings["SMTPHost"].ToString() : "";
        private static int smtpPort = ConfigurationManager.AppSettings["SMTPPort"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString()) : 587;
        private static bool smtpEnableSsl = ConfigurationManager.AppSettings["SMTPEnableSsl"] != null ? Convert.ToBoolean(ConfigurationManager.AppSettings["SMTPEnableSsl"].ToString()) : true;
        private static string smtpUserName = ConfigurationManager.AppSettings["SMTPUserName"] != null ? ConfigurationManager.AppSettings["SMTPUserName"].ToString() : "";
        private static string smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"] != null ? ConfigurationManager.AppSettings["SMTPPassword"].ToString() : "";

        public static bool SendEmail(string From, string To, string Subject, string Body, Stream file, string FileName, bool IsBodyHtml)
        {
            bool isSuccess = false;
            using (MailMessage mm = new MailMessage(From, To))
            {
                mm.Subject = Subject;
                mm.Body = Body;
                if (file != null)
                {
                    mm.Attachments.Add(new Attachment(file, FileName));
                }
                mm.IsBodyHtml = IsBodyHtml;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpHost;
                smtp.EnableSsl = smtpEnableSsl;
                NetworkCredential NetworkCred = new NetworkCredential(smtpUserName, smtpPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = smtpPort;
                try
                {
                    smtp.Send(mm);
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }

                return isSuccess;
            }
        }
    }
}
