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
    public class GetWaJueJiOrderInfoController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        public HttpResponseMessage Get()
        {
            HttpResponseMessage result = new HttpResponseMessage();
            string openid = HttpContext.Current.Request.Headers.GetValues("openid") == null ? "" : HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var temp = from a in db.WaJueJiDingDan.ToList()
                           join b in db.GongDiInfo on a.cGongDiBianMa equals b.cGongDiBianMa
                           join d in db.wechatUser on a.cGuanLiYuanBianMa equals d.openid
                           join e in db.wechatUser on a.openid equals e.openid
                           where a.cGuanLiYuanBianMa == openid
                           && (a.bPrint == false || a.bPrint == null)
                           && a.dKaiShiShiJian != null
                           && a.dJieShuShiJian != null
                           select new
                           {
                               a.AutoID,
                               a.cDingDanHao,
                               b.cGongDiMingCheng,
                               a.cChePaiHao,
                               cGuanLiYuanMingCheng = d.cXingMing,
                               cJiaShiYuanMingCheng = e.cXingMing,
                               dKaiShiShiJian=Convert.ToDateTime(a.dKaiShiShiJian).ToString("yyyy-MM-dd HH:mm:ss"),
                               dJieShuShiJian = Convert.ToDateTime(a.dJieShuShiJian).ToString("yyyy-MM-dd HH:mm:ss")
                           };
                var data = temp.FirstOrDefault();
                if (data != null)
                {
                    var model = new { status_code = 1, message = "查询成功", data = data };
                    result.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.GetEncoding("UTF-8"), "application/json");
                    return result;

                }
                else
                {
                    var model = new { status_code = 0, message = "暂无数据" };
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
