using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.WeChat
{
    public class TemplateOrder
    {
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }

        public TemplateOrderMsg data { get; set; }
    }
    public class TemplateOrderMsg
    {
        public FirstMsg first { get; set; }
        public Keyword1Msg keyword1 { get; set; }
        public Keyword2Msg keyword2 { get; set; }
        public remarkMsg remark { get; set; }
    }
    public class FirstMsg
    {
        public string value { get; set; }
        public string color { get; set; }
    }
    public class Keyword1Msg
    {
        public string value { get; set; }
        public string color { get; set; }
    }
    public class Keyword2Msg
    {
        public string value { get; set; }
        public string color { get; set; }
    }
    public class remarkMsg
    {
        public string value { get; set; }
        public string color { get; set; }
    }
}