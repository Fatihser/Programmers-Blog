using Microsoft.Extensions.Options;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class MailManager : IMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public IResult Send(EmailSendDto emailSendDto)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail),
                To = { new MailAddress(emailSendDto.Email) },
                Subject = emailSendDto.Subject,
                IsBodyHtml = true,
                Body = emailSendDto.Message
            };
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtpClient.Send(message);

            return new Result(ResultStatus.Success, $"E-postaniz basariyla gonderilmistir");
        }

        public IResult SendContactEmail(EmailSendDto emailSendDto)
        {
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail),
                To = { new MailAddress("erfatihsuleyman@gmail.com") },
                Subject=emailSendDto.Subject,
                IsBodyHtml=true,
                Body=$"Gonderen Kisi: {emailSendDto.Name}, Gonderen E-posta Adresi:{emailSendDto.Email} <br/>{emailSendDto.Message}"
            };
            client.Port = 587;
            client.DeliveryMethod= SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(_smtpSettings.Username,
                _smtpSettings.Password);
            client.Credentials = credentials;
            client.Send(message);
            //SmtpClient smtpClient = new SmtpClient
            //{
            //    Host = _smtpSettings.Server,
            //    Port = _smtpSettings.Port,
            //    EnableSsl = true,
            //    UseDefaultCredentials = false,
            //    Credentials=new NetworkCredential(_smtpSettings.Username,_smtpSettings.Password),
            //    DeliveryMethod=SmtpDeliveryMethod.Network
            //};
            //smtpClient.Send(message);

            return new Result(ResultStatus.Success, $"E-postaniz basariyla gonderilmistir");
        }
    }
}
