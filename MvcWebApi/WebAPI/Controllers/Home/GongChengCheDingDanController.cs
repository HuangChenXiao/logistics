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
    public class GongChengCheDingDanController : ApiController
    {
        //管理员订单
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();
        public ResponseMessageResult Get()
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var temp = from a in db.GongChengCheDingDan_View
                           where a.cGuanLiYuanBianMa == openid
                           select a;
                model.data = temp.Take(10).OrderByDescending(o => o.dQiYunShiJian).ToList();

                if (model.data != null)
                {
                    model.message = "查询成功";
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
            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
        }
        public ResponseMessageResult Get(string cDingDanHao)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                try
                {

                    var order = db.GongChengCheDingDan.Where(o => o.cDingDanHao == cDingDanHao).FirstOrDefault();
                    if (order.iState == 0)
                    {
                        order.iState = 110;
                        order.cState = "作废";
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
        public ResponseMessageResult Get(int page, int pagesize, string cDingDanHao = null, string cGongDiBianMa = null, string cChePaiHao = null, string js_openid = null, string cGuanLiYuanBianMa = null, string begindate = null, string enddate = null)
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
                               (a.dDanJuRiQi >= beginDate || string.IsNullOrEmpty(begindate)) &&
                               (a.dDanJuRiQi <= endDate || string.IsNullOrEmpty(enddate))
                               select a
                               ;
                    model.total = temp.Count();
                    model.data = temp.OrderByDescending(s => s.dQiYunShiJian).Skip((page - 1) * pagesize).Take(pagesize).ToList();

                    if (model.data != null)
                    {
                        model.message = "查询成功";
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
        // POST: api/Role
        [ResponseType(typeof(GongChengCheDingDan))]
        public IHttpActionResult PostGongChengCheDingDan(GongChengCheDingDan GongChengCheDingDan)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var info = db.CheLiangInfo.Where(o => o.cChePaiHao == GongChengCheDingDan.cChePaiHao).FirstOrDefault();
                GongChengCheDingDan.cShangBanBianMa = info.cShangBanBianMa;
                GongChengCheDingDan.cDingDanHao = "WX" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                GongChengCheDingDan.iState = 0;
                GongChengCheDingDan.cState = "未确认";
                GongChengCheDingDan.iTWState = 0;
                GongChengCheDingDan.cTWState = "未确认";
                GongChengCheDingDan.iBiaoShi = 1;
                GongChengCheDingDan.dDanJuRiQi = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                GongChengCheDingDan.dQiYunShiJian = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.GongChengCheDingDan.Add(GongChengCheDingDan);
                try
                {
                    db.SaveChanges();
                    model.message = "新增成功";
                    model.status_code = 200;
                    JsonModel jm = new JsonModel();
                    var result_msg = jm.GetJson(string.Format("http://localhost:59358/WeChat/TemplateAlert/TemplateMsg.ashx?openid={0}&orderno={1}", GongChengCheDingDan.openid, GongChengCheDingDan.cDingDanHao));
                    LogTextHelper.Log("消息推送返回码："+result_msg);
                }
                catch (Exception ex)
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