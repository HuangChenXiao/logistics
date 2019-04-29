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
        public ResponseMessageResult Get()
        {
            /*
          * 引用 DuiDui.Open
          * 初始化您的appid和appsecret
          */
            PrintConfig.AppId = "71f727c6a543";
            PrintConfig.AppSecret = "4c7b87330decb4f20744";

            //打印信息
            string strTitle = strPrintContent("厦信同美工程有限公司\n", 1, 1, 13);
            string strcDingDanHao = strPrintContent("序号：10000001", 0, 0, 13);
            string strcChePaiHao = strPrintContent("车辆：闽D15465  一车", 0, 0, 13);
            string strcGongDiBianMingCheng = strPrintContent("装点：厦门市软件园三期", 0, 0, 0);
            string strcTuWeiMingCheng = strPrintContent("卸点：厦门市软件园二期" + "\n", 0, 0, 0);
            string strcGuanLiYuanBianMingCheng = strPrintContent("登记：黄臣晓 2019-4-29 22:21:41" + "\n", 0, 0, 0);
            string strQrCode = strPrintContent("序号：10000001\n车辆：闽D15465  一车\n装点：厦门市软件园三期\n卸点：厦门市软件园二期\n登记：黄臣晓 2019-4-29 22:21:41" + "\n", 1, 0, 3, 4);
            //strQrCode += strPrintContent("\n", 1, 0, 3);
            string strLian = strPrintContent("第2联 司机" + "\n", 1, 0, 13);
            string strQianMing = strPrintContent("签名\n\n", 0, 0, 0);
            //格式详见 http://www.mstching.com/home/openapi
            string jsonContent = "[";
            jsonContent += strTitle;
            jsonContent += strcDingDanHao;
            jsonContent += strcChePaiHao;
            jsonContent += strcGongDiBianMingCheng;
            jsonContent += strcTuWeiMingCheng;
            jsonContent += strcGuanLiYuanBianMingCheng;
            jsonContent += strQrCode;
            jsonContent += strLian;
            jsonContent += strQianMing;
            jsonContent += "]";
            var result = PrintHelper.PrintContent("f9f1826e1293abc9", jsonContent, 1322725);//0 改成用户设备绑定返回的OpenUserId即可
            //查询任务状态
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            dynamic data = serializer.Deserialize<object>(result);

            model.message = data.Message;
            if (data.Code==200)
            {
                model.status_code = 200;
            }
            else
            {
                model.status_code = 401;
            }
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