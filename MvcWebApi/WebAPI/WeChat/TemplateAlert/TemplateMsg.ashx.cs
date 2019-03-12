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
        /// 购买通知模板
        /// </summary>
        /// <returns></returns>
        public string GetJsonString(string openid, string cDingDanHao, string cGongDiMingCheng, string cTuWeiMingCheng)
        {
            TemplateOrder p = new TemplateOrder();
            //用户OPENID
            p.touser = openid;
            //模板ID
            p.template_id = "PKLyFsY0gkoqJYA_fc-as6O_zw_aolaaqJsn3W8imxg";
            //p.url = "http://test.chaomafu.com/user/order?title=我的订单&menu_route=user";
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