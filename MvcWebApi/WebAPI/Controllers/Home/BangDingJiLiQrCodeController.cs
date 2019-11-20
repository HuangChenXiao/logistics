using ModelDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using WebAPI.Models;
using WeChatSDK.WeChatLog;

namespace WebAPI.Controllers.Home
{
    public class BangDingJiLiQrCodeController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();
        public ResponseMessageResult Get(int iBangDingLeiXing, string cChePaiHao = null, string cShangBanBianMa = null, string cDiZhi = null, string cGongDiBianMa = null)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                //查询现在车辆绑定记录
                var up_jilu = db.BangDingJiLu.Where(o => o.cChePaiHao == cChePaiHao).OrderByDescending(o=>o.AutoID).First();
                if(up_jilu!=null)
                {
                    BangDingJiLu ad_jilu = new BangDingJiLu();
                    ad_jilu.iBangDingLeiXing = 0;
                    ad_jilu.openid = up_jilu.openid;
                    ad_jilu.iFangShi = 0;
                    ad_jilu.cChePaiHao = cChePaiHao;
                    ad_jilu.cDiZhi = cDiZhi;
                    ad_jilu.cGongDiBianMa = cGongDiBianMa;
                    ad_jilu.cShangBanBianMa = null;
                    ad_jilu.dShiJian = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    db.BangDingJiLu.Add(ad_jilu);
                    var user = db.wechatUser.Where(o => o.openid == up_jilu.openid).FirstOrDefault();
                    user.status = 0;
                }

                BangDingJiLu jilu = new BangDingJiLu();
                jilu.iBangDingLeiXing = iBangDingLeiXing;
                jilu.openid = openid;
                jilu.iFangShi = 0;
                jilu.cChePaiHao = cChePaiHao;
                jilu.cDiZhi = cDiZhi;
                jilu.cGongDiBianMa = cGongDiBianMa;
                jilu.cShangBanBianMa = iBangDingLeiXing == 1 ? cShangBanBianMa : null;
                jilu.dShiJian = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.BangDingJiLu.Add(jilu);
                var wechat = db.wechatUser.Where(o => o.openid == openid).FirstOrDefault();
                wechat.status = iBangDingLeiXing;
                LogTextHelper.Log("openid:" + openid + ";车牌号：" + cChePaiHao);
                //判断是否下班 车牌号为空时
                if (iBangDingLeiXing == 0 && string.IsNullOrEmpty(cChePaiHao))
                {
                    var driver_list = db.CheLiangInfo.Where(o => o.openid == openid).ToList();
                    driver_list.ForEach(o =>
                    {
                        o.openid = null;
                    });
                }
                if (!string.IsNullOrEmpty(cChePaiHao))
                {
                    var driver = db.CheLiangInfo.Where(o => o.cChePaiHao == cChePaiHao).FirstOrDefault();
                    if (driver != null)
                    {
                        if (iBangDingLeiXing == 1)
                        {
                            driver.openid = openid;
                        }
                        else
                        {
                            driver.openid = null;
                        }
                        driver.cShangBanBianMa = iBangDingLeiXing == 1 ? cShangBanBianMa : null; ;
                    }
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
