using Com;
using ModelDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using WebAPI.Models;

namespace WebAPI.Controllers.Home
{
    public class getChePaiTuWeiController : ApiController
    {
        JsonModel model = new JsonModel();
        public ResponseMessageResult Get(string cChePaiHao)
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var db = ContextDB.Context();
                var query = db.QuerySingle("select top 1 b.cTuWeiBianMa,b.cTuWeiMingCheng from GongChengCheDingDan a inner join TuWeiInfo b on a.cTuWeiBianMa=b.cTuWeiBianMa where CONVERT(nvarchar(10),dQiYunShiJian,120)=CONVERT(nvarchar(10),getdate(),120) and a.cChePaiHao=@0 order by a.autoid desc", cChePaiHao);

                if (query!=null)
                {
                    model.message = "查询成功";
                    model.status_code = 200;
                    model.data = query;
                }
                else
                {
                    model.message = "暂无数据";
                    model.status_code = 200;
                }
                db.Dispose();
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
