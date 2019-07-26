using ModelDb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using WebAPI.Models;

namespace WebAPI.Controllers.App
{
    public class EditOrderbPrintController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        public HttpResponseMessage Get(long AutoID)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            string openid = HttpContext.Current.Request.Headers.GetValues("openid") == null ? "" : HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var temp = from a in db.GongChengCheDingDan
                           where a.AutoID == AutoID
                           //&& (a.bPrint == false || a.bPrint == null)
                           select a;
                var data = temp.FirstOrDefault();
                if (data != null)
                {
                    data.bPrint = true;
                    db.SaveChanges();
                    var model = new { status_code = 1, message = "修改成功" };
                    result.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.GetEncoding("UTF-8"), "application/json");
                    return result;

                }
                else
                {
                    var model = new { status_code = 0, message = "修改失败" };
                    result.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.GetEncoding("UTF-8"), "application/json");
                    return result;
                }
            }
            else
            {
                var model = new { status_code = 1, message = "微信授权失败" };
                result.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
        }
    }
}
