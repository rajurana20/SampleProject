using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SampleProject.Models
{
    public class MailJob : IJob
    {
        DBDHMEntities db = new DBDHMEntities();
        public void Execute(IJobExecutionContext context)
        {
            List<Payment> paymentlist = db.Payments.ToList();

            TimeSpan days;
            foreach (var item in paymentlist)
            {
                days = item.ValidDate.Subtract(DateTime.Today);
                double d = (double)(days.TotalDays);
                if (d == 30)
                {
                    Customer customer = db.Customers.Find(item.CId);
                    MyEmailSender(customer.Email, customer.CName, "Your service going to expire: last valid date is=" + item.ValidDate);

                }

                else if (d == 21)
                {
                    Customer customer = db.Customers.Find(item.CId);
                    MyEmailSender(customer.Email, customer.CName, "Your service going to expire: last valid date is=" + item.ValidDate);


                }
                else if (d == 14)
                {
                    Customer customer = db.Customers.Find(item.CId);
                    MyEmailSender(customer.Email, customer.CName, "Your service going to expire: last valid date is=" + item.ValidDate);

                }

                else if ((0 < d) && (d < 7))
                {
                    Customer customer = db.Customers.Find(item.CId);
                    MyEmailSender(customer.Email, customer.CName, "Your service going to expire: last valid date is=" + item.ValidDate);
                }
            }

        }

        public void MyEmailSender(string email, string name, string message)
            {
                using (MailMessage mailMessage = new MailMessage())
            {
                    mailMessage.From =
                    new MailAddress(ConfigurationManager.AppSettings["FromMail"]);
                    mailMessage.Subject = "SchedulerEmail";
                    mailMessage.Body = "Dear " + name + " , " + message;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(email));
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["Host"];
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = ConfigurationManager.AppSettings["FromMail"];
                    NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);
                }
        }

    }
}