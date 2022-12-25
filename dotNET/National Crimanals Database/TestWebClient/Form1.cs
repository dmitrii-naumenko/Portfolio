using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestWebClient.ServiceReference1;
using System.Net;
using System.Net.Mail;

namespace TestWebClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var par = new SearchParameter
            {
                AgeFrom = 40,
                AgeBefore = 60,
                Nationality = new [] { "<Undefined>", "Belgian", "English" }
            };

            //var par = new SearchParameter() {FirstName = "David"};

            using (var service = new NcSearchServiceClient())
                service.Search(par, "user.testservice@yandex.ru", 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMail("smtp.yandex.ru", 465, "user.testservice@yandex.ru", "abc12345", "dima-post2009@yandex.ru", "Тема письма", "Тело письма", null);
        }

        /// <summary>
        /// Отправка письма на почтовый ящик C# mail send
        /// </summary>
        /// <param name="smtpServer">Имя SMTP-сервера</param>
        /// <param name="from">Адрес отправителя</param>
        /// <param name="password">пароль к почтовому ящику отправителя</param>
        /// <param name="mailto">Адрес получателя</param>
        /// <param name="caption">Тема письма</param>
        /// <param name="message">Сообщение</param>
        /// <param name="attachFile">Присоединенный файл</param>
        public static void SendMail(string smtpServer, int port, string from, string password,
            string mailto, string caption, string message, Dictionary<string, Stream> attach = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;

                if (attach != null)
                    foreach (var att in attach)
                        mail.Attachments.Add((new Attachment(att.Value, att.Key)));
                
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var ms1 = new MemoryStream();
            ms1.WriteByte(1);
            ms1.Position = 0;

            var ms2 = new MemoryStream();
            ms2.WriteByte(1);
            ms2.Position = 0;

            var res = ms1.ToArray().Equals(ms2.ToArray());

        }
    }
}
