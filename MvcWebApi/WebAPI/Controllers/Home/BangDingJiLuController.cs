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
    public class BangDingJiLuController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();
        public ResponseMessageResult Get()
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var temp = from a in db.BangDingJiLu
                           join b in db.wechatUser on a.openid equals b.openid
                           where a.openid == openid
                           select a;
                model.data = temp.OrderByDescending(o => o.dShiJian).FirstOrDefault().cChePaiHao;

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
        public ResponseMessageResult Get(int iBangDingLeiXing, string cChePaiHao = null, string cDiZhi = null, string cShangBanBianMa = null)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                BangDingJiLu jilu = new BangDingJiLu();
                jilu.iBangDingLeiXing = iBangDingLeiXing;
                jilu.openid = openid;
                jilu.iFangShi = 0;
                jilu.cChePaiHao = cChePaiHao;
                jilu.cDiZhi = cDiZhi;
                jilu.cShangBanBianMa = cShangBanBianMa;
                jilu.dShiJian = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.BangDingJiLu.Add(jilu);
                var wechat = db.wechatUser.Where(o => o.openid == openid).FirstOrDefault();
                wechat.status = iBangDingLeiXing;
                LogTextHelper.Log("openid:" + openid + ";车牌号：" + cChePaiHao);
                if (!string.IsNullOrEmpty(cChePaiHao))
                {
                    var driver = db.CheLiangInfo.Where(o => o.cChePaiHao == cChePaiHao).FirstOrDefault();
                    if (!string.IsNullOrEmpty(driver.openid) && iBangDingLeiXing == 1)
                    {
                        model.message = "车辆已经被其他驾驶员绑定，请重新选择";
                        model.status_code = 401;
                        return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
                    }
                    if (iBangDingLeiXing == 1)
                    {
                        driver.openid = openid;
                    }
                    else
                    {
                        driver.openid = null;
                    }
                    driver.cShangBanBianMa = cShangBanBianMa;
                }
                try
                {
                    db.SaveChanges();
                    model.message = "新增成功";
                    model.status_code = 200;
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
    }
}
