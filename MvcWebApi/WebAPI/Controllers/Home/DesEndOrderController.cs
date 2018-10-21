using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ModelDb;
using Com;
using System.Web;
using WebAPI.Models;
using System.Web.Http.Results;
using System;

namespace WebAPI.Controllers.Home
{
    public class DesEndOrderController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();
        public ResponseMessageResult Get(string cDingDanHao)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                try
                {
                    var order = db.GongChengCheDingDan.Where(o => o.cDingDanHao == cDingDanHao).FirstOrDefault();
                    if (order.iTWState == 0)
                    {
                        order.iTWState = 100;
                        order.cTWState = "确认";
                        model.message = "修改成功";
                        model.status_code = 200;
                        db.SaveChanges();
                    }
                    else
                    {
                        model.message = "订单状态已变化，请刷新后重试！";
                        model.status_code = 401;
                    }
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
