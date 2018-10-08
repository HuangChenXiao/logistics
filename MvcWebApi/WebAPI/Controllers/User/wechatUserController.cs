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
    [WebApiActionDebugFilter]
    public class wechatUserController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();

        // GET: api/Role
        public ResponseMessageResult GetwechatUser()
        {
            JwtModel jwtmodel = JwtHelper.getToken(HttpContext.Current.Request.Headers.GetValues("Authorization").First().ToString());
            if (jwtmodel.isadmin)
            {
                var temp = from a in db.wechatUser select a;
                model.data = temp.ToList();

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
                model.message = "用户权限不足";
                model.status_code = 401;
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
            JwtModel jwtmodel = JwtHelper.getToken(HttpContext.Current.Request.Headers.GetValues("Authorization").First().ToString());
            if (jwtmodel.isadmin)
            {
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