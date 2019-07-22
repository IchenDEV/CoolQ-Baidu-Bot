using Idevlab.baidu;
using Idevlab.baidu.model;
using Idevlab.Web;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Idevlab.baidu
{

    public class Bot
    {
        const string api = "https://aip.baidubce.com/rpc/2.0/unit/service/chat";
        private string UserId ;
        string session="";
        private string ServiceId;
        string SkillId;
        AccessToken Token ;
        JsonClient client;
        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId;
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret;
        public Bot(string id,string secret,string serviceId,string skillId="0",string userid = "Public")
        {
            SkillId = skillId;
            clientId = id;clientSecret = secret;
            UserId = userid;ServiceId = serviceId;
            Token = new AccessToken(clientId, clientSecret);
            client = new JsonClient(api, Token);
        }

        public async Task<BotResponseModel> getAnswer(string say)
        {
            BotRequestModel data = new BotRequestModel()
            {
                request = new Request()
                {
                    query = say,
                    user_id = UserId
                },
                log_id = Guid.NewGuid().ToString(),
                session_id = session,
                service_id = ServiceId
            };
            data.dialog_state.contexts.SYS_REMEMBERED_SKILLS.Add(SkillId);
            var str = await client.Post(data);
            BotResponseModel res=JsonConvert.DeserializeObject(str, typeof(BotResponseModel)) as BotResponseModel;
            session = res.result.session_id;
            return res;
        }

    }
}
