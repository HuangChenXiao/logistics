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
using WeChatSDK.WeChatLog;

namespace WebAPI.Controllers.Home
{
    public class EndAllOrderController : ApiController
    {
        //管理员订单
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();

        public ResponseMessageResult Get(int page, int pagesize, string cDingDanHao = null, string cGongDiBianMa = null, string cChePaiHao = null, string js_openid = null, string cGuanLiYuanBianMa = null, string cTuWeiBianMa = null, string begindate = null, string enddate = null)
        {
            try
            {
                string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
                if (!string.IsNullOrEmpty(openid))
                {
                    var beginDate = Convert.ToDateTime(begindate);
                    var endDate = Convert.ToDateTime(enddate);
                    var temp = from a in db.GongChengCheDingDan_View
                               where
                               (a.cDingDanHao.Contains(cDingDanHao) || string.IsNullOrEmpty(cDingDanHao)) &&
                               (a.cGongDiBianMa == cGongDiBianMa || string.IsNullOrEmpty(cGongDiBianMa)) &&
                               (a.cChePaiHao == cChePaiHao || string.IsNullOrEmpty(cChePaiHao)) &&
                               (a.openid == js_openid || string.IsNullOrEmpty(js_openid)) &&
                               (a.cGuanLiYuanBianMa == cGuanLiYuanBianMa || string.IsNullOrEmpty(cGuanLiYuanBianMa)) &&
                               (a.cTuWeiBianMa == cTuWeiBianMa || string.IsNullOrEmpty(cTuWeiBianMa)) &&
                               (a.dDanJuRiQi >= beginDate || string.IsNullOrEmpty(begindate)) &&
                               (a.dDanJuRiQi <= endDate || string.IsNullOrEmpty(enddate))
                               select a;
                    
                    if (model.data != null)
                    {
                        model.message = "操作成功";
                        model.status_code = 200;
                        return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
                    }
                    else
                    {
                        model.message = "暂无数据";
                        model.status_code = 200;
                    }
                }
                else
                {
                    model.message = "微信授权失败";
                    model.status_code = 401;
                }
            }
            catch (Exception ex)
            {
                model.message = ex.Message;
                model.status_code = 401;
            }

            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}