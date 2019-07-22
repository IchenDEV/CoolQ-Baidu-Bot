using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;

namespace Idevlab.baidu
{
    public class AccessToken
    {
        public AccessToken(string clientid,string scret)
        {
            clientId = clientid;
            clientSecret = scret;
            getAccessToken();
        }

        AccessTokenResponse ATR;

        private string _token;

        public String Token
        {
            get
            {
                if (ATR != null)
                {
                    if (ATR.express_date > DateTime.Now)
                    {
                        return _token;
                    }
                }
                _token= getAccessToken();
                return _token;
            }
            set
            {
                _token = value;
            }
        }


        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId ;
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret ;

        public String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            ATR= JsonConvert.DeserializeObject(result,typeof(AccessTokenResponse)) as AccessTokenResponse;
            ATR.express_date = DateTime.Now.AddSeconds(ATR.expires_in);
            Token = ATR.access_token;
            return ATR.access_token;
        }
    }
    public class AccessTokenResponse
    {
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public DateTime express_date { get; set; }
        public string scope { get; set; }
        public string session_key { get; set; }
        public string access_token { get; set; }
        public string session_secret { get; set; }
    }

}
