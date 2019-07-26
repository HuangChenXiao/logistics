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
    public class EditPrinterController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        public HttpResponseMessage Get(string printerid)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            string openid = HttpContext.Current.Request.Headers.GetValues("openid") == null ? "" : HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                try
                {
                    var info = db.ChuanXiangPrinter.Where(o => o.printerid == printerid).FirstOrDefault();
                    if (info!=null)
                    {

                        var model = new { status_code = 1, message = "成功" };
                        result.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.GetEncoding("UTF-8"), "application/json");
                    }
                    else
                    {
                        var model = new { status_code = 0, message = "失败" };
                        result.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.GetEncoding("UTF-8"), "application/json");
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    var model = new { status_code = 0, message = ex.Message };
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
