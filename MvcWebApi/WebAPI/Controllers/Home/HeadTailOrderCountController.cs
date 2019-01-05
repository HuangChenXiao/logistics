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

        public ResponseMessageResult Get(string cGongDiMingCheng="", string cTuWeiMingCheng="", string cXZDWMingCheng = "", string cXingMing = "", string cChePaiHao = "", string BegindDanJuRiQi = "", string EnddDanJuRiQi = "", string cGuanLiYuanBianMa = "")
        {
            string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
            if (!string.IsNullOrEmpty(openid))
            {
                var db = ContextDB.Context();
                model.data = db.Query("exec PROC_OrderSumCount @0,@1,@2,@3,@4,@5,@6,@7", cGongDiMingCheng, cTuWeiMingCheng, cXZDWMingCheng, cXingMing, cChePaiHao, BegindDanJuRiQi, EnddDanJuRiQi, cGuanLiYuanBianMa).ToList();

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
