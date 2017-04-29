using Cross;
using System;

namespace MuGame
{
	public class GameSdkMgr
	{
		private static GameSdkMgr _instance;

		public static GameSdk_base m_sdk;

		public static void init()
		{
			bool flag = GameSdkMgr._instance == null;
			if (flag)
			{
				GameSdkMgr._instance = new GameSdkMgr();
				ENUM_SDK_PLATFORM qSMY_SDK_Index = Globle.QSMY_SDK_Index;
				if (qSMY_SDK_Index != ENUM_SDK_PLATFORM.QISJ_QUICK)
				{
					if (qSMY_SDK_Index == ENUM_SDK_PLATFORM.QISJ_RYJTDREAM)
					{
						GameSdkMgr.m_sdk = new GameSdk_ryjtDream();
					}
				}
				else
				{
					GameSdkMgr.m_sdk = new GameSdk_quick();
				}
			}
		}

		public static void Pay(rechargeData data)
		{
			bool flag = Globle.QSMY_Platform_Index != ENUM_QSMY_PLATFORM.QSPF_LINKSDK;
			if (!flag)
			{
				GameSdkMgr.m_sdk.Pay(data);
			}
		}

		public static void sharemsg(string sharemsg, string sharetype, string shareappid, string shareappkey)
		{
			GameSdkMgr.m_sdk.sharemsg(sharemsg, sharetype, shareappid, shareappkey);
		}

		public static void record_selectionSever()
		{
			GameSdkMgr.m_sdk.record_selectionSever();
		}

		public static void record_createRole(Variant data)
		{
			bool flag = Globle.QSMY_Platform_Index != ENUM_QSMY_PLATFORM.QSPF_LINKSDK;
			if (!flag)
			{
				GameSdkMgr.m_sdk.record_createRole(data);
			}
		}

		public static void record_login()
		{
			bool flag = Globle.QSMY_Platform_Index != ENUM_QSMY_PLATFORM.QSPF_LINKSDK;
			if (!flag)
			{
				GameSdkMgr.m_sdk.record_login();
			}
		}

		public static void record_LvlUp()
		{
			bool flag = Globle.QSMY_Platform_Index != ENUM_QSMY_PLATFORM.QSPF_LINKSDK;
			if (!flag)
			{
				GameSdkMgr.m_sdk.record_LvlUp();
			}
		}

		public static void record_quit()
		{
			bool flag = Globle.QSMY_Platform_Index != ENUM_QSMY_PLATFORM.QSPF_LINKSDK;
			if (!flag)
			{
				GameSdkMgr.m_sdk.record_quit();
			}
		}

		public static bool beginVoiceRecord()
		{
			return GameSdkMgr.m_sdk.beginVoiceRecord();
		}

		public static void showbalance()
		{
			GameSdkMgr.m_sdk.showbalance();
		}

		public static void cancelVoiceRecord()
		{
			GameSdkMgr.m_sdk.cancelVoiceRecord();
		}

		public static void endVoiceRecord()
		{
			GameSdkMgr.m_sdk.endVoiceRecord();
		}

		public static void playVoice(string path)
		{
			GameSdkMgr.m_sdk.playVoice(path);
		}

		public static void stopVoice(string path)
		{
			GameSdkMgr.m_sdk.stopVoice(path);
		}

		public static void clearVoices()
		{
			GameSdkMgr.m_sdk.clearVoices();
		}
	}
}
