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

namespace WebAPI.Controllers.Home
{
    public class TuWeiInfoController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();

        public ResponseMessageResult Get(string keyword = null)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var temp = from a in db.TuWeiInfo
                           where (a.cTuWeiBianMa.Contains(keyword) || a.cTuWeiMingCheng.Contains(keyword) || string.IsNullOrEmpty(keyword))
                           select a;
                model.data = temp.ToList();

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
        public ResponseMessageResult Get(int page, int pagesize, string keyword = null)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var temp = from a in db.TuWeiInfo
                           where (a.cTuWeiBianMa.Contains(keyword) || a.cTuWeiMingCheng.Contains(keyword) || string.IsNullOrEmpty(keyword))
                           select a;
                model.total = temp.Count();
                model.data = temp.OrderByDescending(s => s.cTuWeiBianMa).Skip((page - 1) * pagesize).Take(pagesize).ToList();

                if (model.data.Count > 0)
                {
                    model.message = "查询成功";
                    model.status_code = 200;
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
        // POST: api/TuWeiInfo
        [ResponseType(typeof(TuWeiInfo))]
        public ResponseMessageResult Post(TuWeiInfo TuWeiInfo)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var info = db.TuWeiInfo.Where(o => o.cTuWeiBianMa == TuWeiInfo.cTuWeiBianMa);
                if (info.Count()>0)
                {
                    model.message = "土尾编码不能重复";
                    model.status_code = 401;
                    return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
                }
                db.TuWeiInfo.Add(TuWeiInfo);
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
