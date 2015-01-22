using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using Microsoft.Ajax.Utilities;

namespace ASP.NET_Web_Forms_Game
{
    public class TestController : ApiController
    {
        private int Id { get; set; }
        // GET api/<controller>
        [HttpGet]
        public object Get()
        {
            var status = HttpContext.Current.Application.Get("Status");
            if (status != null)
            {
                var userName = HttpContext.Current.Application.Get("UserName");
                return new { Status = status, Message = "", Name = userName, Value = HttpContext.Current.Application.Get("Value")};
            }
            status = Status.NotStarted;
            //HttpContext.Current.Application.Set("Status", Status.NotStarted);
            return new { Status = Status.NotStarted, Message = ""};
        }

        // GET api/<controller>/5
        [HttpGet]
        public object Get(int id)
        {
            return HttpContext.Current.Application.Get("Value");
        }

        [HttpPost]
        public object Set(int value, string userName = "")
        {
            var fromApplication = HttpContext.Current.Application.Get("Value");
            var status = HttpContext.Current.Application.Get("Status");
            var userHistory = (List<string>)HttpContext.Current.Application.Get(userName);
            if (userHistory != null)
            {
                userHistory.Add(value.ToString());
            }
            else
            {
                userHistory = new List<string> { value.ToString() };
            }
            HttpContext.Current.Application.Set(userName, userHistory);

            if (status == null || status.ToString() == "Finished" )
            {
                HttpContext.Current.Application.Set("Status", Status.Started);
                HttpContext.Current.Application.Set("Value", value);
                return new { Status = Status.Starter, Message = "Поздравляем!!! Вы начали новую игру!!! :-)", More = "No", History = userHistory };
            }
            else if (fromApplication.ToString() == value.ToString())
            {
                //HttpContext.Current.Application.Remove("Value");
                HttpContext.Current.Application.Set("Status", Status.Finished);
                HttpContext.Current.Application.Set("UserName", userName);
                return new { Status = Status.Winner, Message = "Поздравляем!!! Вы угадали число!!! :-)", More = "No",
                             Value = fromApplication,
                             History = userHistory
                };
            }
            else
            {
                return (int)fromApplication > value ? new { Status = Status.Started, Message = "", More = "No", History = userHistory } :
                    new { Status = Status.Started, Message = "", More = "Yes", History = userHistory };
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}