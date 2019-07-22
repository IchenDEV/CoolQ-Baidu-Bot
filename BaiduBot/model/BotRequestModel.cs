using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idevlab.baidu.model
{
    public class Request
    {
        public string user_id;
        public string query;
        //request object 必需  本轮请求体。
        //+user_id    string 必需  与技能对话的用户id（如果客户端是用户未登录状态情况下对话的，也需要尽量通过其他标识（比如设备id）来唯一区分用户），方便今后在平台的日志分析模块定位分析问题、从用户维度统计分析相关对话情况。详情见【请求参数详细说明】
        //+query  string 必需  本轮请求query（用户说的话），详情见【请求参数详细说明】
        //+query_info object 可选  本轮请求query的附加信息
        //++type  string
        //enum 可选  请求信息类型，取值范围："TEXT","EVENT"。详情见【请求参数详细说明】
        //++source string
        //enum 可选  请求信息来源，可选值："ASR","KEYBOARD"。ASR为语音输入，KEYBOARD为键盘文本输入。针对ASR输入，UNIT平台内置了纠错机制，会尝试解决语音输入中的一些常见错误
        //++asr_candidates list<object>	可选 请求信息来源若为ASR，该字段为ASR候选信息。（如果调用百度语音的API会有该信息，UNIT会参考该候选信息做综合判断处理。）
        //++asr_candidates[].text string 可选  语音输入候选文本
        //++asr_candidates[].confidence   float 可选  语音输入候选置信度
        //+client_session string(json) 可选  用于在多轮中实现多选一的对话效果，具体内容见【请求参数详细说明】
        //+hyper_params kvdict of object 可选  key为技能id或机器人id（现在只实现技能id），value为控制相关技能/机器人内部行为的的超参数
        //+hyper_params{ }.bernard_level int 可选  技能自动发现不置信意图/词槽，并据此主动发起澄清确认的频率。取值范围：0(关闭)、1(低频)、2(高频)。取值越高代表技能对不置信意图/词槽的敏感度就越高，默认值=1
        //+hyper_params{}.slu_level int 可选  slu运行级别，值域1，2，3 默认值=1
        //+hyper_params{}.slu_threshold double 可选  slu阈值，值域0.0~1.0，值越高表示召回的阈值越高，避免误召回，默认值=0.5。
        //+hyper_params{}.slu_tags list<string>    可选 用于限定slu的解析范围，只在打上了指定tag的意图、或问答对的范围内执行slu
    }
    public class BotRequestModel
    {
        public string version = "2.0";
        public string service_id;
        public string log_id;
        public string session_id ;
        public Request request;
        public DialogState dialog_state=new DialogState ();
        //version string 必需 = 2.0，当前api版本对应协议版本号为2.0，固定值
        //service_id  string 可选  机器人ID，service_id 与skill_ids不能同时缺失，至少一个有值。
        //skill_ids list<string>	可选 技能ID列表。我们允许开发者指定调起哪些技能。这个列表是有序的——排在越前面的技能，优先级越高。技能优先级体现在response的排序上。具体排序规则参见【应答参数说明】
        //service_id和skill_ids可以组合使用，详见【请求参数详细说明】
        //log_id  string 必需  开发者需要在客户端生成的唯一id，用来定位请求，响应中会返回该字段。对话中每轮请求都需要一个log_id
        //session 或 session_id    string(json) 或 string 必需  session保存机器人的历史会话信息，由机器人创建，客户端从上轮应答中取出并直接传递，不需要了解其内容。如果为空，则表示清空session（开发者判断用户意图已经切换且下一轮会话不需要继承上一轮会话中的词槽信息时可以把session置空，从而进行新一轮的会话）。
        //session字段内容较多，开发者可以通过传送session_id的方式节约传输流量。具体操作方式见【请求参数详细说明】
        //以下为session内部格式，仅供参考了解
        //+service_id string 必需  机器人ID，标明该session由哪个机器人产生。
        //+session_id string 必需  session本身的ID，客户端可以使用session_id代替session，节约传输流量。
        //+skill_sessions kvdict of strings   必需 这里存储与当前对话相关的所有技能的session。key为技能ID，value为技能的session（同【UNIT对话API文档】中的bot_session)。
        //+interactions list<object>	必需 历史交互序列，即历史 request/response_list 序列，序列的每一个元素称作一次交互（interaction），随交互进行而交替插入，格式与上述不断增长直到发生清空操作。
        //+interactions[].interaction_id  string 必需  第 i 次交互的唯一标识。
        //+interactions[].timestamp   string 必需  interaction生成的时间（以interaction_id的生成时间为准）。格式：YYYY-MM-DD HH:MM:SS.fff （24小时制，精确到毫秒）
        //+interactions[].request object 必需  第 i 次交互的 request，结构参考【请求参数说明】中的request
        //+interactions[].response_list list<object>	必需 第 i 次交互的 response列表，结构参考【应答参数说明】中的response_list
        //dialog_state    object 可选  机器人对话状态。
        //+skill_states kvdict of objects   可选 技能的对话状态key为技能ID，value为技能的对话状态数据。具体数据格式后续发布。
        //+contexts json
        //object 可选  希望在多技能对话过程中贯穿的全局性上下文.
        //这里预留了一个key用于控制各技能的session记忆。详见【请求参数详细说明】

    }

    public class DialogState
    {
        public Contexts contexts=new Contexts();
    }

    public class Contexts
    {
        public List<string> SYS_REMEMBERED_SKILLS=new List<string>();
    }
}
