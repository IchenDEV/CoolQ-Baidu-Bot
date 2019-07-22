using Native.Csharp.Sdk.Cqp;
using Native.Csharp.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Native.Csharp.App.Model;
using Native.Csharp.App.Interface;
using Idevlab.baidu;

namespace Native.Csharp.App.Event
{
    public class Event_GroupMessage : IEvent_GroupMessage
    {
        #region --公开方法--
        static Bot bot = new Bot("", "", "", "");
        /// <summary>
        /// Type=2 群消息<para/>
        /// 处理收到的群消息
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMessage(object sender, GroupMessageEventArgs e)
        {
            try
            {
                var cqat = Common.CqApi.CqCode_At(Common.CqApi.GetLoginQQ());
                if (!e.Msg.Contains(cqat)) return;
                var Msg = e.Msg.Replace(cqat,"");
                var ans = bot.getAnswer(Msg).Result;
                foreach (var item in ans.result.response_list)
                {
                    foreach (var action in item.action_list)
                    {
                        if (action.action_id == "faq_select_guide")
                        {
                            foreach (var item2 in item.schema.slots)
                            {
                                Common.CqApi.SendGroupMessage(e.FromGroup, item2.normalized_word);
                            }
                            return;
                        }
                        else if ((action.action_id.StartsWith("cx") || action.action_id.StartsWith("CX")) && action.action_id.EndsWith("satisfy"))
                        {
                            Common.CqApi.SendPrivateMessage(e.FromQQ, ans.result.response_list[0].action_list[0].say);

                            return;
                        }
                        else
                        {
                            Common.CqApi.SendGroupMessage(e.FromGroup, ans.result.response_list[0].action_list[0].say);
                            return;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Common.CqApi.SendPrivateMessage(e.FromQQ, ex.Message);

            }

            e.Handled = true;   // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=21 群私聊<para/>
        /// 处理收到的群私聊消息
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupPrivateMessage(object sender, PrivateMessageEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息
            try
            {
                var ans = bot.getAnswer(e.Msg).Result;
                foreach (var item in ans.result.response_list)
                {
                    foreach (var action in item.action_list)
                    {
                        if (action.action_id == "faq_select_guide")
                        {
                            foreach (var item2 in item.schema.slots)
                            {
                                Common.CqApi.SendPrivateMessage(e.FromQQ, item2.normalized_word);
                            }
                            e.Handled = true;
                            return;
                        }
                        else if ((action.action_id.StartsWith("cx") || action.action_id.StartsWith("CX")) && action.action_id.EndsWith("satisfy"))
                        {
                            Common.CqApi.SendPrivateMessage(e.FromQQ, ans.result.response_list[0].action_list[0].say);
                            e.Handled = true;
                            return;
                        }
                        else
                        {
                            Common.CqApi.SendPrivateMessage(e.FromQQ, ans.result.response_list[0].action_list[0].say);
                            e.Handled = true;
                            return;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Common.CqApi.SendPrivateMessage(e.FromQQ, ex.Message);

            }


            e.Handled = true;

            // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=11 群文件上传事件<para/>
        /// 处理收到的群文件上传结果
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupFileUpload(object sender, FileUploadMessageEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息
            // 关于文件信息, 触发事件时已经转换完毕, 请直接使用



            e.Handled = false;   // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=101 群事件 - 管理员增加<para/>
        /// 处理收到的群管理员增加事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupManageIncrease(object sender, GroupManageAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=101 群事件 - 管理员减少<para/>
        /// 处理收到的群管理员减少事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupManageDecrease(object sender, GroupManageAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=103 群事件 - 群成员增加 - 主动入群<para/>
        /// 处理收到的群成员增加 (主动入群) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMemberJoin(object sender, GroupMemberAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=103 群事件 - 群成员增加 - 被邀入群<para/>
        /// 处理收到的群成员增加 (被邀入群) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMemberInvitee(object sender, GroupMemberAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=102 群事件 - 群成员减少 - 成员离开<para/>
        /// 处理收到的群成员减少 (成员离开) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMemberLeave(object sender, GroupMemberAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=102 群事件 - 群成员减少 - 成员移除<para/>
        /// 处理收到的群成员减少 (成员移除) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMemberRemove(object sender, GroupMemberAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=302 群事件 - 群请求 - 申请入群<para/>
        /// 处理收到的群请求 (申请入群) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupAddApply(object sender, GroupAddRequestEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=302 群事件 - 群请求 - 被邀入群 (机器人被邀)<para/>
        /// 处理收到的群请求 (被邀入群) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupAddInvitee(object sender, GroupAddRequestEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }
        #endregion
    }
}
