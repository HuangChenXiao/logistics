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

namespace WebAPI.Controllers.User
{
    public class wechatUserController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();

        public ResponseMessageResult Get()
        {
            var HeaderOPENID = HttpContext.Current.Request.Headers.GetValues("openid");
            if (HeaderOPENID != null)
            {
                string openid = HeaderOPENID.First().ToString();
                var temp = from a in db.wechatUser select a;
                model.data = temp.Where(o => o.openid == openid).FirstOrDefault();

                if (model.data != null)
                {
                    model.message = "查询成功";
                    model.status_code = 200;
                    return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
                }
                else
                {
                    model.message = "微信授权已过期";
                    model.status_code = 402;
                }
            }
            else
            {
                model.message = "openid为空";
                model.status_code = 403;
            }
            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
        }

        // PUT: api/Role/5
        [ResponseType(typeof(void))]
        public ResponseMessageResult PutwechatUser(wechatUser wechatUser)
        {
            JwtModel jwtmodel = JwtHelper.getToken(HttpContext.Current.Request.Headers.GetValues("Authorization").First().ToString());
            if (jwtmodel.isadmin)
            {
                db.Entry(wechatUser).State = EntityState.Modified;
                try
                {
                    model.message = "修改成功";
                    model.status_code = 200;
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    model.message = ex.Message;
                    model.status_code = 401;
                }
            }
            else
            {
                model.message = "用户权限不足";
                model.status_code = 401;
            }
            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
        }

        // POST: api/Role
        [ResponseType(typeof(wechatUser))]
        public IHttpActionResult PostwechatUser(wechatUser wechatUser)
        {
            var info = db.wechatUser.Where(o => o.openid == wechatUser.openid);
            if (info.FirstOrDefault() != null)
            {
                model.message = "微信号已经授权，退出后重新进入";
                model.status_code = 401;
            }
            else
            {
                wechatUser.status = 0;
                wechatUser.audit = 0;
                wechatUser.auditdes = "未审核";
                wechatUser.addtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.wechatUser.Add(wechatUser);
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