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
    public class ConfirmOrderController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();
        public ResponseMessageResult Get(string cDingDanHao, string cTuWeiBianMa, string lng, string lat)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                try
                {
                    var tuwei = db.TuWeiInfo.Where(o => o.cTuWeiBianMa == cTuWeiBianMa).FirstOrDefault();
                    if (!string.IsNullOrEmpty(tuwei.longitude))//土尾经纬度不为空时判断距离
                    {
                        var db_web = ContextDB.Context();
                        var item_val = Convert.ToInt32(db_web.QueryValue("select cItemValue from SystemSet where cItemCode='002'"));
                        var lng_lat_val = Convert.ToInt32(db_web.QueryValue("select dbo.fnGetDistance(@0,@1,@2,@3)", lat, lng,tuwei.latitude,tuwei.longitude));
                        if (lng_lat_val > item_val)
                        {
                            model.message = "距离目的地较远，请稍后重试！";
                            model.status_code = 401;
                            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
                        }
                    }
                    var order = db.GongChengCheDingDan.Where(o => o.cDingDanHao == cDingDanHao).FirstOrDefault();
                    if (order.iState == 0)
                    {
                        order.iState = 100;
                        order.cState = "确认";
                        order.dQueRenShiJian = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        model.data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
