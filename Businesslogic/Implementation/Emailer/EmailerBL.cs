using BusinessLogic.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BusinessLogic.Implementation
{
    public class EmailerBL : IEmailerBL
    {
        #region Global Fields

        private const string imageurl = @"images\2019 GWH Logo.png";
        private readonly IHostingEnvironment hostingEnv;
        private IConfiguration Configuration { get; }
        #endregion

        #region Constructors
        public EmailerBL(IHostingEnvironment hostingEnv, IConfiguration configuration)
        {
            this.hostingEnv = hostingEnv;
            this.Configuration = configuration;
        }

        #endregion

        #region Methods



        public Dictionary<bool, string> SendEmail(DataAccess.QueuedEmail emailRecord)
        {
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

          

            SmtpClient SmtpServer = new SmtpClient();

            var credential = new NetworkCredential
            {
                UserName = emailRecord.EmailAccount.Username,
                Password = emailRecord.EmailAccount.Password,
            };

            try
            {


                SmtpServer.Credentials = credential;
                SmtpServer.Host = string.Format("{0}", emailRecord.EmailAccount.Host);
                MailMessage mail = new MailMessage();

                var portNumber = emailRecord.EmailAccount.Port;

                if (portNumber != 0)
                {
                    SmtpServer.Port = portNumber;
                    SmtpServer.EnableSsl = true;
                }


                mail.From = new MailAddress(emailRecord.From);
                mail.To.Add(emailRecord.To);
                mail.Subject = emailRecord.Subject;

                mail.IsBodyHtml = true;

                #region Embedded Image
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(emailRecord.Body, null, MediaTypeNames.Text.Html);

                // Create a LinkedResource object for each embedded image
                var imagePath = GetImage();
                LinkedResource pic1 = new LinkedResource(imagePath, MediaTypeNames.Image.Jpeg)
                {
                    ContentId = "Logo"
                };
                avHtml.LinkedResources.Add(pic1);
                mail.AlternateViews.Add(avHtml);
                #endregion

                if (emailRecord.FileByte != null && emailRecord.FileByte.Length > 0)
                {
                    //GenerateEmailAttachment(emailRecord.FileByte, mail);
                }
                if (!SmtpServer.EnableSsl)
                {
                    SendToLocalFolder(SmtpServer);
                }

                SmtpServer.Send(mail);
                dictionary.Add(true, string.Empty);
                return dictionary;
            }
            catch (Exception exception)
            {
                dictionary.Add(false, exception.ToString());
   
                return dictionary;
            }
        }


        public string GetImage()
        {
            var path = Path.Combine(hostingEnv.WebRootPath, imageurl);

            return path;
        }


        #region Private Methods


        /// <summary>
        /// Dumps generated email into local machine
        /// </summary>

        private void SendToLocalFolder(SmtpClient SmtpServer)
        {
#if DEBUG
            string path = Configuration.GetSection("EmailSetting")["EmailPickitUpDirectory"];
             
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            SmtpServer.PickupDirectoryLocation = path;

#endif

        }

        #endregion

        #endregion

    }
}
