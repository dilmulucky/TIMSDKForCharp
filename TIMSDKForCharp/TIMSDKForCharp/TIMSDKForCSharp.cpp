#include "framework.h"
#include "TIMCloud.h"


//TIM_SUCC
//0
//接口调用成功

//TIM_ERR_SDKUNINIT
//- 1
//接口调用失败，IM SDK 未初始化

//TIM_ERR_NOTLOGIN
//- 2
//接口调用失败，用户未登录

//TIM_ERR_JSON
//- 3
//接口调用失败，错误的 JSON 格式或 JSON Key

//TIM_ERR_PARAM
//- 4
//接口调用成功，参数错误

//TIM_ERR_CONV
//- 5
//接口调用成功，无效的会话

//TIM_ERR_GROUP
//- 6
//接口调用成功，无效的群组

extern "C" __declspec(dllexport) const char* getVersion()
{
	return TIMGetSDKVersion();
}

extern "C" __declspec(dllexport) int init(char* config, char* config1, int appId, TIMCommCallback cb, const void* user_data)
{
	//TIMSetConfig(config, cb, user_data);

	return TIMInit(appId, config1);
}

extern "C" __declspec(dllexport) int release()
{
	return TIMUninit();
}

extern "C" __declspec(dllexport) int login(const char* user_id, const char* user_sig, TIMCommCallback cb, const void* user_data)
{
	return TIMLogin(user_id, user_sig, cb, user_data);
}

extern "C" __declspec(dllexport) int logout(TIMCommCallback cb, const void* user_data)
{
	return TIMLogout(cb, user_data);
}

extern "C" __declspec(dllexport) int createConv(const char* groupId, TIMCommCallback cb, const void* user_data)
{
	return TIMConvCreate(groupId, kTIMConv_Group, cb, user_data);
}

extern "C" __declspec(dllexport) int delConv(const char* convId, TIMCommCallback cb, const void* user_data)
{
	return TIMConvDelete(convId, kTIMConv_Group, cb, user_data);
}

//获取本地缓存的会话列表。
//TIM_DECL int TIMConvGetConvList(TIMCommCallback cb, const void* user_data);


extern "C" __declspec(dllexport) int sendMsg(const char* convId, enum TIMConvType conv_type, const char* json_msg_param, TIMCommCallback cb, const void* user_data)
{
	return TIMMsgSendNewMsg(convId, conv_type, json_msg_param, cb, user_data);

}

// 消息回执
extern "C" __declspec(dllexport) int reportMsg(const char* convId, enum TIMConvType conv_type, const char* json_msg_param, TIMCommCallback cb, const void* user_data)
{
	return TIMMsgReportReaded(convId, conv_type, json_msg_param, cb, user_data);
}


typedef void(*TIMRecvMsgCallback)(const char* json_msg_array, int size, const void* user_data);

TIMRecvMsgCallback RecvMsgCallback;

void MsgCallBack(const char* json_msg_array, const void* user_data)
{
	RecvMsgCallback(json_msg_array, strlen(json_msg_array), user_data);
}

// 设置接收新消息回调。
extern "C" __declspec(dllexport) void setRecvNewMsgCallback(TIMRecvMsgCallback cb, const void* user_data)
{
	RecvMsgCallback = cb;
	TIMAddRecvNewMsgCallback(MsgCallBack, user_data);
}

// 设置消息已读回执回调。
extern "C" __declspec(dllexport) void setMsgReadedReceiptCallback(TIMMsgReadedReceiptCallback cb, const void* user_data)
{
	TIMSetMsgReadedReceiptCallback(cb, user_data);
}

// 设置被踢下线通知回调。
extern "C" __declspec(dllexport) void setKickedOfflineCallback(TIMKickedOfflineCallback cb, const void* user_data)
{
	TIMSetKickedOfflineCallback(cb, user_data);
}


//typedef void(*NetworkCallback)(TIMNetworkStatus status);
//
//NetworkCallback networkCallback;
//
//void timNetworkStatusListenerCallback(enum TIMNetworkStatus status, int32_t code, const char* desc, const void* user_data)
//{
//	networkCallback(status);
//}

// 设置网络连接状态监听回调。
extern "C" __declspec(dllexport) void setNetworkStatusListenerCallback(TIMNetworkStatusListenerCallback cb, const void* user_data)
{
	//networkCallback = cb;
	// 
	TIMSetNetworkStatusListenerCallback(cb, user_data);
}