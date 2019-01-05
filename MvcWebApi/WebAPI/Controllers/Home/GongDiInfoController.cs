using System;
using System.Collections.Generic;
using System.Data;
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

namespace WebAPI.Controllers
{
    public class GongDiInfoController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();

        public ResponseMessageResult Get(string keyword=null)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var temp = from a in db.GongDiInfo
                           where (a.dTingYongRiQi==null||a.dTingYongRiQi>DateTime.Now)
                           select a;
                model.data = temp.Where(o => o.cGongDiBianMa.Contains(keyword) || o.cGongDiMingCheng.Contains(keyword)||string.IsNullOrEmpty(keyword)).ToList();

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
        // POST: api/GongDiInfo
        [ResponseType(typeof(GongDiInfo))]
        public ResponseMessageResult Post(GongDiInfo GongDiInfo)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var db_web = ContextDB.Context();
                GongDiInfo.cGongDiBianMa = db_web.QueryValue("exec PROC_GongDiCode 'GD'");
                db.GongDiInfo.Add(GongDiInfo);
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
                db_web.Dispose();
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
