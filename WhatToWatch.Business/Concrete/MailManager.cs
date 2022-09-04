using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Core.Entities;
using WhatToWatch.Entities.Dtos.Movie;

namespace WhatToWatch.Business.Concrete
{
    public class MailManager : IMailService
    {
        private readonly EmailConfiguration _emailConfig;
        public MailManager(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public void SendMail(string email,string userName, MovieDto movieDto)
        {
            try
            {
                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress(_emailConfig.From);
                ePosta.To.Add(email);
                ePosta.Subject = "Film Tavsiye";
                ePosta.Body =@$"Merhaba size {userName} adlı kullanıcıdan, {movieDto.Title} adlı film tavsiye edildi.  Film Hakkında:{movieDto.Overview}";
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(_emailConfig.From, _emailConfig.Password);
                smtp.Port = _emailConfig.Port;
                smtp.Host = _emailConfig.SmtpServer;
                smtp.EnableSsl = true;
                smtp.Send(ePosta);
            }
            catch (Exception ex)
            {
                //log
            }
        }
    }
}
