using ModelDb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using WebAPI.Models;

namespace WebAPI.Controllers.App
{
    public class CheckWechatUserController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        public HttpResponseMessage Get(string cUserCode, string cPassword)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            var temp = from a in db.wechatUser
                       where a.cUserCode == cUserCode && a.cPassword == cPassword
                       select new
                       {
                           a.openid
                       };
            var data = temp.FirstOrDefault();
            if (data != null)
            {
                var model = new { status_code = 1, message = "查询成功", openid = data.openid };
                result.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.GetEncoding("UTF-8"), "application/json"); 
                return result;

            }
            else
            {
                var model = new { status_code = 0, message = "用户名或密码" };
                result.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
        }
    }
}
