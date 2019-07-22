using System;
using System.Collections.Generic;
using System.Text;

namespace Idevlab.baidu.model
{

    public class BotResponseModel
    {
        public Result result { get; set; }
        public int error_code { get; set; }
    }
    public class Result
    {
        public string version { get; set; }
        public string timestamp { get; set; }
        public string service_id { get; set; }
        public string log_id { get; set; }
        public string session_id { get; set; }
        public string interaction_id { get; set; }
        public Response_List[] response_list { get; set; }
       
    }
    public class Response_List
    {
        public int status { get; set; }
        public string msg { get; set; }
        public string origin { get; set; }
        public Schema schema { get; set; }
        public Action_List[] action_list { get; set; }
    }

    public class Schema
    {
        public float intent_confidence { get; set; }
        public string intent { get; set; }
        public Slot[] slots { get; set; }
        public float domain_confidence { get; set; }
        public object[] slu_tags { get; set; }
    }

    public class Slot
    {
        public string word_type { get; set; }
        public int father_idx { get; set; }
        public float confidence { get; set; }
        public int length { get; set; }
        public string name { get; set; }
        public string original_word { get; set; }
        public int session_offset { get; set; }
        public int begin { get; set; }
        public string normalized_word { get; set; }
        public string merge_method { get; set; }
    }

    public class Action_List
    {
        public string action_id { get; set; }
        public Refine_Detail refine_detail { get; set; }
        public float confidence { get; set; }
        public string custom_reply { get; set; }
        public string say { get; set; }
        public string type { get; set; }
    }

    public class Refine_Detail
    {
        public object[] option_list { get; set; }
        public string interact { get; set; }
        public string clarify_reason { get; set; }
    }

}
