using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NationalCriminalDatabase.Properties;
using Font = iTextSharp.text.Font;

namespace NationalCriminalDatabase
{
    public class SearchCore
    {
        internal virtual IDb GetDb()
        {
            return new RealDb();
        }

        public IQueryable<Person> GetData(SearchParameter par)
        {
            var db = GetDb();

            if (par.Nationality == null)
                par.Nationality = new List<string>();

            DateTime? dateOfBirthFrom = null;
            DateTime? dateOfBirthBefore = null;
            if (par.AgeFrom != null && par.AgeBefore != null)
            {
                dateOfBirthFrom = DateTime.Now - TimeSpan.FromDays(365.25*(int) par.AgeBefore);
                dateOfBirthBefore = DateTime.Now - TimeSpan.FromDays(365.25*(int) par.AgeFrom);
            }

            var res = from person in db.Persons
                join n in db.Nationalities on person.Nationality equals n.id
                join s in db.Nationalities on person.Sex equals s.id
                where (par.Nationality == null || par.Nationality.Count() == 0
                            || par.Nationality.Contains(person.Nationality1.Alias)) &&
                      (par.FirstName == null || person.FirstName == par.FirstName) &&
                      (par.LastName == null || person.LastName == par.LastName) &&
                      (par.Sex == null || person.Sex1.Alias == par.Sex) &&
                      (dateOfBirthFrom == null ||
                            (person.DateOfBirth > dateOfBirthFrom && person.DateOfBirth < dateOfBirthBefore)) &&
                      (par.WeightFrom == null || par.WeightBefore == null ||
                            (person.Weight > par.WeightFrom && person.Weight < par.WeightBefore)) &&
                      (par.HeightFrom == null || par.HeightBefore == null ||
                            (person.Height > par.HeightFrom && person.Height < par.HeightBefore)) &&
                      (par.FreeText == null || person.Text.Contains(par.FreeText))
                select person;

            return res;
        }

        public void ExecuteTask(SearchTask task)
        {
            try
            {
                var persons = GetData(task.Search);
                var arr = persons.Take(task.MaxResultCount).ToArray();

                if (arr.Length != 0)
                {
                    var i = 0;
                    var attsch = new Dictionary<string, Stream>();
                    var mailCount = arr.Length/10 + (arr.Length%10 != 0 ? 1 : 0);
                    var mailNumber = 1;
                    while (i < arr.Length)
                    {
                        attsch.Add(arr[i].id + ".pdf", new MemoryStream(CreatePdf(arr[i])));
                        i++;
                        if (i%10 == 0 || i == arr.Length)
                        {
                            SendMail("smtp.yandex.ru", 465, "user.testservice@yandex.ru", "abc12345", task.Email,
                                string.Format("Search results ({0} from {1})", mailNumber++, mailCount),
                                "See attachments", attsch);
                            attsch.Clear();
                        }
                    }
                }
                else
                {
                    SendMail("smtp.yandex.ru", 465, "user.testservice@yandex.ru", "abc12345", task.Email,
                        "Search results", "Nothing found");
                }

            }
            catch (Exception e)
            {
                // Possible add exception to some log
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public interface IDb
        {
            IQueryable<Person> Persons { get; }
            IQueryable<Nationality> Nationalities { get; }
            IQueryable<Sex> Sexes { get; }
        }

        public class RealDb : IDb
        {
            internal NationalCriminalDatabaseDataContext context;

            public RealDb()
            {
                context = new NationalCriminalDatabaseDataContext(GetDbConnection());
            }

            public IQueryable<Person> Persons
            {
                get { return context.Persons; }
            }

            public IQueryable<Nationality> Nationalities
            {
                get { return context.Nationalities; }
            }

            public IQueryable<Sex> Sexes
            {
                get { return context.Sexes; }
            }

            protected virtual IDbConnection GetDbConnection()
            {
                var connectionString = Settings.Default.NationalCriminalsDatabaseConnectionString;
                return new SqlConnection(connectionString);
            }
        }

        internal void SendMail(string smtpServer, int port, string from, string password,
            string mailto, string caption, string message, Dictionary<string, Stream> attach = null)
        {
            try
            {
                var mail = new MailMessage();
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

        public byte[] CreatePdf(Person person)
        {
            Document doc = new Document(PageSize.A4);
            var output = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            doc.Open();

            Font titleFont = FontFactory.GetFont("Arial", 28, BaseColor.BLUE);
            Font textFont = FontFactory.GetFont("Arial", 14, BaseColor.BLACK);

            doc.Add(new Paragraph(string.Format("{0} {1}", person.FirstName, person.LastName), titleFont));
            doc.Add(new Paragraph(" "));

            if (person.DateOfBirth != null)
                doc.Add(new Paragraph(string.Format("DateOfBirth = {0:dd/mm/yyyy}", person.DateOfBirth), textFont));
            if (person.Sex1 != null)
                doc.Add(new Paragraph(string.Format("Sex = {0}", person.Sex1.Alias), textFont));
            if (person.Nationality1 != null)
                doc.Add(new Paragraph(string.Format("Nationality = {0}", person.Nationality1.Alias), textFont));
            if (person.Height != null)
                doc.Add(new Paragraph(string.Format("Height = {0}", person.Height), textFont));
            if (person.Weight != null)
                doc.Add(new Paragraph(string.Format("Weight = {0}", person.Weight), textFont));
            doc.Add(new Paragraph(" "));

            if (person.Text != null)
                doc.Add(new Paragraph(person.Text, textFont));

            doc.Close();
            writer.Close();

            return output.ToArray();
        }
    }
}