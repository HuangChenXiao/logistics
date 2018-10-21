using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebAPI.Models
{
    public class JsonModel
    {
        public string message { get; set; }
        public int status_code { get; set; }
        public dynamic data { get; set; }
        public int total { get; set; }
        public string GetJson(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Encoding = Encoding.UTF8;
            string returnText = wc.DownloadString(url);

            return returnText;
        }
    }
}