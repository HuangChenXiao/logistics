using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAPI.WeChat;
using WeChatSDK.Helper;
using WeChatSDK.TemplateAlert;
using WeChatSDK.TemplateAlert.Model;
using WeChatSDK.WeChatLog;


namespace WeChatAPI.TemplateAlert
{
    /// <summary>
    /// TemplateMsg 的摘要说明
    /// </summary>
    public class TemplateMsg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            TemplatePurchaseNotice notice = new TemplatePurchaseNotice();
            string openid=context.Request["openid"].ToString();
            string cDingDanHao = context.Request["cDingDanHao"].ToString();
            string cGongDiMingCheng = context.Request["cGongDiMingCheng"].ToString();
            string cTuWeiMingCheng = context.Request["cTuWeiMingCheng"].ToString();
            var Msg = notice.TemplateSendMsg(GetJsonString(openid, cDingDanHao, cGongDiMingCheng, cTuWeiMingCheng));
            LogTextHelper.Log("消息推送返回码：" + Msg.errcode);
            context.Response.Write(Msg.errcode);
        }
        /// <summary>
        /// 工程车订单推送模板
        /// </summary>
        /// <returns></returns>
        public string GetJsonString(string openid, string cDingDanHao, string cGongDiMingCheng, string cTuWeiMingCheng)
        {
            TemplateOrder p = new TemplateOrder();
            //用户OPENID
            p.touser = openid;
            //模板ID
            p.template_id = "PKLyFsY0gkoqJYA_fc-as6O_zw_aolaaqJsn3W8imxg";
            p.url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx42cd9994ca8711a5&redirect_uri=https%3a%2f%2fmobile.xmxtm.cn%2fuser%2forder%3ftitle%3d%e6%88%91%e7%9a%84%e8%ae%a2%e5%8d%95%26menu_route%3duser&response_type=code&scope=snsapi_userinfo&state=1#wechat_redirect";
            p.data = new TemplateOrderMsg
            {
                first = new FirstMsg { value = "接收到新订单", color = "#173177" },
                keyword1 = new Keyword1Msg { value = cDingDanHao, color = "#173177" },
                keyword2 = new Keyword2Msg { value = "订单已下发，请尽快执行订单！", color = "#173177" },
                remark = new remarkMsg { value = "工地名称：" + cGongDiMingCheng + "\n土尾名称：" + cTuWeiMingCheng+"\n起运时间："+DateTime.Now, color = "#FF0000" },
            };
            return new JavaScriptSerializer().Serialize(p);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}