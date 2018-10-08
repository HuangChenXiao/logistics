using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ModelDb;
using System.Collections;
using System.Web;
using WebAPI.Models;
using System.Web.Http.Results;
using Com;
using WebAPI;

namespace WebAPI.Controllers.Agent
{
    public class AgentBusinessBasincController : ApiController
    {

        private NiuChaoEntities db = new NiuChaoEntities();

        JsonModel model = new JsonModel();
        /// <summary>
        /// 获取代理商的商户
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(sy_agent))]
        [WebApiActionDebugFilter]
        public ResponseMessageResult Get(int merchantid)
        {
            JwtModel jwtmodel = JwtHelper.getToken(HttpContext.Current.Request.Headers.GetValues("Authorization").First().ToString());
            var sy_merchant = from a in db.fa_business_basic
                              join b in db.sy_merchant on a.merchantid equals b.id
                              join c in db.sy_agent on b.agid equals c.id
                              where b.agid == jwtmodel.userid
                              && (b.id == merchantid||merchantid==0)
                              select new
                              {
                                  a.id,
                                  a.code,
                                  a.password,
                                  a.merchantid,
                                  a.merchantName,
                                  a.shortName,
                                  a.handleType,
                                  a.city,
                                  a.merchantAddress,
                                  a.servicePhone,
                                  a.orgCode,
                                  a.merchantType,
                                  a.category,
                                  a.corpmanName,
                                  a.corpmanId,
                                  a.corpmanPhone,
                                  a.corpmanMobile,
                                  a.corpmanEmail,
                                  a.bankCode,
                                  a.bankaccountNo,
                                  a.bankName,
                                  a.bankaccountName,
                                  a.autoCus,
                                  a.remark,
                                  a.licenseNo,
                                  a.taxRegisterNo,
                                  a.agencyId,
                                  a.appId,
                                  a.appSecret,
                                  a.status,
                                  a.balance,
                                  a.addtime,
                                  a.adduser,
                                  a.updatetime,
                                  a.updateuser,
                                  mername = b.name,
                                  agname = c.name
                              };
            if (sy_merchant == null)
            {
                model.message = "暂无数据";
                model.status_code = 200;
            }
            else
            {
                model.data = sy_merchant.ToList();
                model.message = "查询成功";
                model.status_code = 200;
            }
            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
        }
        /// <summary>
        /// 获取代理商的商户
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(sy_agent))]
        [WebApiActionDebugFilter]
        public ResponseMessageResult Get(int page, int pagesize, string code)
        {
            JwtModel jwtmodel = JwtHelper.getToken(HttpContext.Current.Request.Headers.GetValues("Authorization").First().ToString());
            var sy_merchant = from a in db.fa_business_basic
                              join b in db.sy_merchant on a.merchantid equals b.id
                              join c in db.sy_agent on b.agid equals c.id
                              where b.agid == jwtmodel.userid
                              && (a.shortName.Contains(code) || string.IsNullOrEmpty(code))
                              select new
                              {
                                  a.id,
                                  a.code,
                                  a.password,
                                  a.merchantid,
                                  a.merchantName,
                                  a.shortName,
                                  a.handleType,
                                  a.city,
                                  a.merchantAddress,
                                  a.servicePhone,
                                  a.orgCode,
                                  a.merchantType,
                                  a.category,
                                  a.corpmanName,
                                  a.corpmanId,
                                  a.corpmanPhone,
                                  a.corpmanMobile,
                                  a.corpmanEmail,
                                  a.bankCode,
                                  a.bankaccountNo,
                                  a.bankName,
                                  a.bankaccountName,
                                  a.autoCus,
                                  a.remark,
                                  a.licenseNo,
                                  a.taxRegisterNo,
                                  a.agencyId,
                                  a.appId,
                                  a.appSecret,
                                  a.status,
                                  a.balance,
                                  a.addtime,
                                  a.adduser,
                                  a.updatetime,
                                  a.updateuser,
                                  mername = b.name,
                                  agname = c.name
                              };
            if (sy_merchant == null)
            {
                model.message = "暂无数据";
                model.status_code = 200;
            }
            else
            {
                model.total = sy_merchant.Count();
                model.data = sy_merchant.OrderByDescending(s => s.id).Skip((page - 1) * pagesize).Take(pagesize).ToList();
                model.message = "查询成功";
                model.status_code = 200;
            }
            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
        }
    }
}