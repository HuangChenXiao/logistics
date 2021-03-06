﻿using System.Data.Entity;
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
    public class WaJueJiDingDanController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();
        public ResponseMessageResult Get()
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var temp = from a in db.WaJueJiDingDan_View
                           where a.cGuanLiYuanBianMa == openid
                           select a;
                model.data = temp.Take(10).OrderByDescending(o => o.dDanJuRiQi).ToList();

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
                    var order = db.WaJueJiDingDan.Where(o => o.cDingDanHao == cDingDanHao).FirstOrDefault();
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
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var beginDate = Convert.ToDateTime(begindate);
                var endDate = Convert.ToDateTime(enddate);
                var temp = from a in db.WaJueJiDingDan_View
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
                model.data = temp.OrderByDescending(s => s.dDanJuRiQi).Skip((page - 1) * pagesize).Take(pagesize).ToList();

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
        // POST: api/Role
        [ResponseType(typeof(WaJueJiDingDan))]
        public IHttpActionResult PostWaJueJiDingDan(WaJueJiDingDan WaJueJiDingDan)
        {

            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var info = db.CheLiangInfo.Where(o => o.cChePaiHao == WaJueJiDingDan.cChePaiHao).FirstOrDefault();
                WaJueJiDingDan.cShangBanBianMa = info.cShangBanBianMa;
                //WaJueJiDingDan.cDingDanHao = "WX" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                WaJueJiDingDan.iState = 0;
                WaJueJiDingDan.cState = "未确认";
                WaJueJiDingDan.iBiaoShi = 1;
                WaJueJiDingDan.dDanJuRiQi = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                WaJueJiDingDan.dKaiShiShiJian = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.WaJueJiDingDan.Add(WaJueJiDingDan);
                try
                {
                    db.SaveChanges();
                    model.message = "新增成功";
                    model.status_code = 200;

                    //JsonModel jm = new JsonModel();
                    //var result_msg = jm.GetJson(string.Format("http://test.chaomafu.com/WeChat/TemplateAlert/TemplateMsg.ashx?openid={0}&cDingDanHao={1}", WaJueJiDingDan.openid, WaJueJiDingDan.cDingDanHao));
                    //LogTextHelper.Log("消息推送返回码：" + result_msg);
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