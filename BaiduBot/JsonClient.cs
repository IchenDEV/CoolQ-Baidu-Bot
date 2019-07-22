using Idevlab.baidu;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Idevlab.Web
{
    public class JsonClient
    {
        string api="";
        AccessToken AT;
        public JsonClient(string Api,AccessToken at) { api = Api;AT = at; }
        public async Task<string> Post(Object data)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //序列化
                    var str = JsonConvert.SerializeObject(data);
                   
                    HttpContent content = new StringContent(str);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await client.PostAsync(api+ "?access_token="+AT.Token, content);
                    response.EnsureSuccessStatusCode();//用来抛异常的
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                    return responseBody;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    return null;
                }
            }

     
        }

    }
}
