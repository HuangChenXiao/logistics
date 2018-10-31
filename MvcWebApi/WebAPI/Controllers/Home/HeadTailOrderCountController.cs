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
    public class HeadTailOrderCountController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();

        public ResponseMessageResult Get(string begindate, string enddate, string cGongDiBianMa = null, string cTuWeiBianMa = null)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var beginDate = Convert.ToDateTime(begindate);
                var endDate = Convert.ToDateTime(enddate);
                var temp = from a in db.GongChengCheDingDan_View
                           where a.cGuanLiYuanBianMa == openid &&
                           (a.cGongDiBianMa == cGongDiBianMa || string.IsNullOrEmpty(cGongDiBianMa)) &&
                           (a.cTuWeiBianMa == cTuWeiBianMa || string.IsNullOrEmpty(cTuWeiBianMa)) &&
                           (a.dDanJuRiQi >= beginDate || string.IsNullOrEmpty(begindate)) &&
                           (a.dDanJuRiQi <= endDate || string.IsNullOrEmpty(enddate))
                           select a;
                model.total = temp.Count();

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
    }
}
