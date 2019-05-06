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
using DuiDui.Open;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebAPI.Controllers.Home
{
    public class DuiDuiJiController : ApiController
    {
        private EBMSystemEntities db = new EBMSystemEntities();
        JsonModel model = new JsonModel();
        public ResponseMessageResult Get(string cDingDanHao, string sign)
        {
            /*
          * 引用 DuiDui.Open
          * 初始化您的appid和appsecret
          */

            PrintConfig.AppId = "71f727c6a543";
            PrintConfig.AppSecret = "4c7b87330decb4f20744";
            var th_sign = PrintConfig.AppId + "xiamen!@#" + cDingDanHao;
            if (BaseHelper.Md5Hash(th_sign) != sign)
            {
                model.status_code = 401;
                model.message = "签名错误。";
            }
            else
            {
                string openid = HttpContext.Current.Request.Headers.GetValues("openid").First().ToString();
                var mstching = db.mstching.Where(o => o.openid == openid).FirstOrDefault();
                if (mstching==null)
                {
                    model.status_code = 401;
                    model.message = "管理员未绑定打印机。";
                    return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
                }
                var order_info = db.GongChengCheDingDan_View.Where(o => o.cDingDanHao == cDingDanHao).FirstOrDefault();

                //打印信息
                string strTitle = strPrintContent("厦信同美工程有限公司\n", 1, 1, 13);
                string strcDingDanHao = strPrintContent("序号：" + order_info.cDingDanHao, 0, 0, 13);
                string strcChePaiHao = strPrintContent("车辆：" + order_info.cChePaiHao + "  一车", 0, 0, 13);
                string strcGongDiBianMingCheng = strPrintContent("装点：" + order_info.cGongDiMingCheng + "", 0, 0, 0);
                string strcTuWeiMingCheng = strPrintContent("卸点：" + order_info.cTuWeiMingCheng + "" + "\n", 0, 0, 0);
                string strcGuanLiYuanBianMingCheng = strPrintContent("登记：" + order_info.cXingMing + " " + order_info.dQiYunShiJian + "" + "\n", 0, 0, 0);
                string strQrCode = strPrintContent("序号：" + order_info.cDingDanHao + "\n车辆：" + order_info.cChePaiHao + "  一车\n装点：" + order_info.cGongDiMingCheng + "\n卸点：" + order_info.cTuWeiMingCheng + "\n登记：" + order_info.cXingMing + " " + order_info.dQiYunShiJian + "" + "\n", 1, 0, 3, 4);
                //strQrCode += strPrintContent("\n", 1, 0, 3);
                string strQianMing = strPrintContent("签名\n\n", 0, 0, 0);
                string strLine = strPrintContent("\n------------------------\n\n", 0, 0, 13);
                //格式详见 http://www.mstching.com/home/openapi
                string jsonContent = "[";
                for (int i = 1; i <= 3; i++)
                {
                    var str_name = i == 1 ? "公司" : i == 2 ? "司机" : "卸点";
                    string strLian = strPrintContent("第" + i + "联 " + str_name + "\n", 1, 0, 13);
                    //jsonContent += "[";
                    jsonContent += strTitle;
                    jsonContent += strcDingDanHao;
                    jsonContent += strcChePaiHao;
                    jsonContent += strcGongDiBianMingCheng;
                    jsonContent += strcTuWeiMingCheng;
                    jsonContent += strcGuanLiYuanBianMingCheng;
                    jsonContent += strQrCode;
                    jsonContent += strLian;
                    jsonContent += strQianMing;
                    jsonContent += strLine;
                    //jsonContent += "]";
                }
                jsonContent += "]";
                var result = PrintHelper.PrintContent(mstching.uuid, jsonContent, 1322725);//0 改成用户设备绑定返回的OpenUserId即可
                //查询任务状态
                var serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
                dynamic data = serializer.Deserialize<object>(result);

                model.message = data.Message;
                if (data.Code == 200)
                {
                    model.message ="打印请求成功，正在打印。。。";
                    model.status_code = 200;
                }
                else
                {
                    model.status_code = 401;
                }
            }
            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
        }
        public ResponseMessageResult Get(int taskId)
        {
            var result = PrintHelper.GetPrintTaskState(taskId);
            //查询任务状态
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            dynamic data = serializer.Deserialize<object>(result);
            model.message = data.Message;
            return new ResponseMessageResult(Request.CreateResponse((HttpStatusCode)model.status_code, model));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BaseText">内容的base64字符串</param>
        /// <param name="Alignment">排版方式	0居左 1居中 2居右</param>
        /// <param name="Bold">是否加粗	0不加粗 1加粗</param>
        /// <param name="FontSize">字体大小</param>
        /// <param name="PrintType">打印类型	0文本 1图片 3条码 4二维码</param>
        /// <returns></returns>
        string strPrintContent(string BaseText, int Alignment = 0, int Bold = 0, int FontSize = 0, int PrintType = 0)
        {
            return "{\"Alignment\":" + Alignment + ",\"BaseText\":\"" + Utils.StringToBase64(BaseText) + "\",\"Bold\":" + Bold + ",\"FontSize\":" + FontSize + ",\"PrintType\":" + PrintType + "},";
        }
    }
}