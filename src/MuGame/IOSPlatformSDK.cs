using Cross;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MuGame
{
	public class IOSPlatformSDK : IPlotformSDK
	{
		private static bool m_bInCalling = false;

		private static int m_nResetAppCount = 0;

		public static bool m_bLogined = false;

		public static Action<Variant> LOGIN_ACTION;

		private int _inited = -1;

		public int isinited
		{
			get
			{
				return this._inited;
			}
		}

		[DllImport("__Internal")]
		public static extern void CallSDKFunc(string type, string jsonpara);

		public void FrameMove()
		{
			bool flag = IOSPlatformSDK.m_nResetAppCount > 0;
			if (flag)
			{
				IOSPlatformSDK.m_nResetAppCount--;
				bool flag2 = IOSPlatformSDK.m_nResetAppCount == 0;
				if (flag2)
				{
					Debug.Log("重启游戏APP");
					AnyPlotformSDK.Call_Cmd("resetapp", null, null, true);
				}
			}
		}

		public void Add_moreCmdInfo(string info, string jstr)
		{
			bool flag = Application.platform != RuntimePlatform.IPhonePlayer;
			if (!flag)
			{
				bool flag2 = Globle.QSMY_Platform_Index != ENUM_QSMY_PLATFORM.QSPF_LINKSDK;
				if (!flag2)
				{
					IOSPlatformSDK.CallSDKFunc(info, jstr);
				}
			}
		}

		public void Call_Cmd(string cmd, string info = null, string jstr = null, bool waiting = true)
		{
			Debug.Log(string.Concat(new string[]
			{
				"Call Cmd ",
				cmd,
				"  ",
				info,
				"  ",
				jstr,
				"\n"
			}));
			bool flag = Application.platform != RuntimePlatform.IPhonePlayer;
			if (!flag)
			{
				bool flag2 = Globle.QSMY_Platform_Index != ENUM_QSMY_PLATFORM.QSPF_LINKSDK;
				if (!flag2)
				{
					if (waiting)
					{
						InterfaceMgr.getInstance().open(InterfaceMgr.SDK_LOADING, null, false);
					}
					IOSPlatformSDK.m_bInCalling = true;
					bool flag3 = info != null && jstr != null;
					if (flag3)
					{
						IOSPlatformSDK.CallSDKFunc(info, jstr);
					}
					IOSPlatformSDK.CallSDKFunc(cmd, "");
				}
			}
		}

		public void Cmd_CallBack(Variant v)
		{
			debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!Cmd_CallBack!!!!!");
			InterfaceMgr.getInstance().close(InterfaceMgr.SDK_LOADING);
			IOSPlatformSDK.m_bInCalling = false;
			int num = v["result"];
			debug.Log(num.ToString());
			bool flag = num < 3;
			if (flag)
			{
				this._inited = num;
			}
			else
			{
				bool flag2 = num == 11 || num == 12 || num == 13;
				if (flag2)
				{
					bool flag3 = IOSPlatformSDK.m_bLogined && num == 11;
					if (flag3)
					{
						IOSPlatformSDK.m_nResetAppCount = 1;
					}
					else
					{
						IOSPlatformSDK.LOGIN_ACTION(v);
					}
				}
				else
				{
					bool flag4 = num == 42;
					if (flag4)
					{
						GameSdkMgr.record_quit();
						AnyPlotformSDK.Call_Cmd("close", null, null, true);
					}
					else
					{
						bool flag5 = num == 21;
						if (flag5)
						{
							GameSdkMgr.record_quit();
							IOSPlatformSDK.m_nResetAppCount = 1;
						}
					}
				}
			}
		}
	}
}
