using ModelDb;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using WebAPI.Models;
namespace WebAPI.Controllers.Home
{
    public class EditBangDingJiLuGongDiController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();

        public ResponseMessageResult Get(string cGongDiBianMa)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                try
                {
                    var info = db.BangDingJiLu.OrderByDescending(o => o.AutoID).Where(o => o.openid == openid).FirstOrDefault();
                    if (info != null)
                    {
                        info.cGongDiBianMa = cGongDiBianMa;
                    }
                    model.message = "修改成功";
                    model.status_code = 200;
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    model.message = ex.Message;
                    model.status_code = 401;
                }
            }
            else
            {
                model.message = "微信授权失败";
                model.status_code = 401;
            }
            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
        }
    }
}