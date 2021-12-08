using System;
using System.Runtime.InteropServices;

namespace TIMSDKShell
{
    public class TIMSDKImport
    {
        #region TIM

        public enum TIMNetworkStatus
        {
            kTIMConnected,       // 已连接
            kTIMDisconnected,    // 失去连接
            kTIMConnecting,      // 正在连接
            kTIMConnectFailed,   // 连接失败
        };

        public enum TRTCQuality
        {
            TRTCQuality_Unknown = 0,   ///< 未定义
            TRTCQuality_Excellent = 1,   ///< 最好
            TRTCQuality_Good = 2,   ///< 好
            TRTCQuality_Poor = 3,   ///< 一般
            TRTCQuality_Bad = 4,   ///< 差
            TRTCQuality_Vbad = 5,   ///< 很差
            TRTCQuality_Down = 6,   ///< 不可用
        };

        public enum TIMConvType
        {
            kTIMConv_Invalid, // 无效会话
            kTIMConv_C2C,     // 个人会话
            kTIMConv_Group,   // 群组会话
            kTIMConv_System,  // 系统会话
        };

        public enum TIMMsgStatus
        {
            kTIMMsg_Sending = 1,        // 消息正在发送
            kTIMMsg_SendSucc = 2,       // 消息发送成功
            kTIMMsg_SendFail = 3,       // 消息发送失败
            kTIMMsg_Deleted = 4,        // 消息已删除
            kTIMMsg_LocalImported = 5,  // 消息导入状态
            kTIMMsg_Revoked = 6,        // 消息撤回状态
        };

        /// <summary>
        /// TIM 通用回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        /// <param name="json_params"></param>
        /// <param name="user_data"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TIMCommCallback(int code, string desc, string json_params, int user_data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TIMRecvMsgCallback(IntPtr json_msg_array, int size, int user_data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TIMMsgReadedReceiptCallback(string json_msg_readed_receipt_array, int user_data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TIMKickedOfflineCallback(int user_data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TIMNetworkStatusListenerCallback(TIMNetworkStatus status, int code, IntPtr desc, int user_data);

        #endregion

        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr getVersion();

        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static int init(string config, string config1, int appId, TIMCommCallback cb, int user_data);

        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static int release();

        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static int login(string user_id, string user_sig, TIMCommCallback cb, int user_data);

        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static int logout(TIMCommCallback cb, int user_data);

        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static int createConv(string groupId, TIMCommCallback cb, int user_data);

        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static int delConv(string convId, TIMCommCallback cb, int user_data);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="convId"></param>
        /// <param name="conv_type"></param>
        /// <param name="json_msg_param"></param>
        /// <param name="cb"></param>
        /// <param name="user_data"></param>
        /// <returns></returns>
        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static int sendMsg(string convId, TIMConvType conv_type, byte[] json_msg_param, TIMCommCallback cb, int user_data);

        /// <summary>
        /// 消息回执
        /// </summary>
        /// <param name="convId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static int reportMsg(string convId, TIMConvType conv_type, string json_msg_param, TIMCommCallback cb, int user_data);

        /// <summary>
        /// 设置接收新消息回调
        /// </summary>
        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setRecvNewMsgCallback(TIMRecvMsgCallback cb, int user_data);

        /// <summary>
        /// 设置消息已读回执回调
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="user_data"></param>
        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setMsgReadedReceiptCallback(TIMMsgReadedReceiptCallback cb, int user_data);

        /// <summary>
        /// 设置被踢下线通知回调
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="user_data"></param>
        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setKickedOfflineCallback(TIMKickedOfflineCallback cb, int user_data);

        [DllImport("TIMSDKForCSharp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setNetworkStatusListenerCallback(TIMNetworkStatusListenerCallback cb, int user_data);

    }
}
