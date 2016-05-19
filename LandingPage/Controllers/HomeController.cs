using LandingPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace LandingPage.Controllers
{
    public class HomeController : Controller
    {
        private TNBIContext db = new TNBIContext();
        public ActionResult Index()
        {
            var news = db.News.OrderByDescending(s => s.PublishedDate).Take(3);
            if (news.Count() < 3)
                return View();
            else
            {
                foreach (var article in news)
                {
                    article.PublishedDate = article.PublishedDate.Date;
                    int result = article.Content.Count();
                    if (result > 88)
                    {
                        article.Content = article.Content.Substring(0, 88);
                        article.Content = article.Content.Remove(article.Content.Length - 4, 4) + "....";
                    }
                }
                return View(news);

            }
        }


        public ActionResult About()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
        public ActionResult RequestAccount(AccountRequest request)
        {
            ViewBag.message = "";
            if(!String.IsNullOrEmpty(request.Company))
            {
                SendMessage(request);
            }
            
            return View();
        }
        protected void SendMessage(AccountRequest request)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("emailadress");
            mailMessage.To.Add("reciepentemail");
            mailMessage.Subject = "Accountrequest for tbni application";
            mailMessage.Body = "Email:" + request.Email + "Namn:" + request.Contact + " Company: " + request.Company+ " Phone: " + request.Phone + " Orgnummer: " + request.OrgNr;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("emailadress", "password");
            smtpClient.Timeout = 20000;
            try
            {
                smtpClient.Send(mailMessage);
                ViewBag.message = "Email skickat!";
            }
            catch
            {
                ViewBag.message = "error occurred please fill in all fields";
            }

        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Vision()
        {
            ViewBag.Messade = "Vår vision.";
            return View();
        }


    }
}