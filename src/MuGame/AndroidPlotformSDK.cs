using Cross;
using System;
using UnityEngine;

namespace MuGame
{
	public class AndroidPlotformSDK : IPlotformSDK
	{
		public static Action<string> ANDROID_PLOTFORM_SDK_CALL;

		public static Action<string, string> ANDROID_PLOTFORM_SDK_INFO_CALL;

		private static bool m_bInCalling = false;

		private static int m_nResetAppCount = 0;

		public static bool m_bLogined = false;

		public static Action<Variant> LOGIN_ACTION;

		public int isinited
		{
			get
			{
				return 1;
			}
		}

		public void FrameMove()
		{
			bool flag = AndroidPlotformSDK.m_nResetAppCount > 0;
			if (flag)
			{
				AndroidPlotformSDK.m_nResetAppCount--;
				bool flag2 = AndroidPlotformSDK.m_nResetAppCount == 0;
				if (flag2)
				{
					Debug.Log("重启游戏APP");
					AnyPlotformSDK.Call_Cmd("resetapp", null, null, true);
				}
			}
		}

		public void Add_moreCmdInfo(string info, string jstr)
		{
			bool flag = Application.platform != RuntimePlatform.Android;
			if (!flag)
			{
				bool flag2 = Globle.QSMY_Platform_Index != ENUM_QSMY_PLATFORM.QSPF_LINKSDK;
				if (!flag2)
				{
					AndroidPlotformSDK.ANDROID_PLOTFORM_SDK_INFO_CALL(info, jstr);
				}
			}
		}

		public void Call_Cmd(string cmd, string info = null, string jstr = null, bool waiting = true)
		{
			debug.Log(string.Concat(new object[]
			{
				"Call_Cmd::",
				Application.platform,
				" ",
				Globle.QSMY_Platform_Index
			}));
			bool flag = Application.platform != RuntimePlatform.Android;
			if (!flag)
			{
				if (waiting)
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.SDK_LOADING, null, false);
				}
				AndroidPlotformSDK.m_bInCalling = true;
				bool flag2 = info != null && jstr != null;
				if (flag2)
				{
					AndroidPlotformSDK.ANDROID_PLOTFORM_SDK_INFO_CALL(info, jstr);
				}
				AndroidPlotformSDK.ANDROID_PLOTFORM_SDK_CALL(cmd);
				debug.Log("ANDROID_PLOTFORM_SDK_CALL::");
			}
		}

		public void Cmd_CallBack(Variant v)
		{
			debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!Cmd_CallBack!!!!!" + v.dump());
			int num = v["result"];
			bool flag = num != 85;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.SDK_LOADING);
			}
			AndroidPlotformSDK.m_bInCalling = false;
			bool flag2 = num == 11 || num == 12 || num == 13;
			if (flag2)
			{
				bool flag3 = AndroidPlotformSDK.m_bLogined && num == 11;
				if (flag3)
				{
					AndroidPlotformSDK.m_nResetAppCount = 1;
				}
				else
				{
					AndroidPlotformSDK.LOGIN_ACTION(v);
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
						AndroidPlotformSDK.m_nResetAppCount = 1;
					}
					else
					{
						bool flag6 = num == 81;
						if (flag6)
						{
							Variant variant = v["data"];
							GameSdkMgr.m_sdk.voiceRecordHanlde("begin", "", 0);
						}
						else
						{
							bool flag7 = num == 82;
							if (flag7)
							{
								Variant variant2 = v["data"];
								bool flag8 = variant2.ContainsKey("error");
								if (flag8)
								{
									GameSdkMgr.m_sdk.voiceRecordHanlde("error", variant2["error"], 0);
								}
							}
							else
							{
								bool flag9 = num == 83;
								if (flag9)
								{
									Variant variant3 = v["data"];
									GameSdkMgr.m_sdk.voiceRecordHanlde("end", variant3["url"], variant3["seconds"]);
								}
								else
								{
									bool flag10 = num == 84;
									if (flag10)
									{
										Variant variant4 = v["data"];
										bool flag11 = variant4.ContainsKey("error");
										if (flag11)
										{
											GameSdkMgr.m_sdk.voiceRecordHanlde("error", variant4["error"], 0);
										}
									}
									else
									{
										bool flag12 = num == 91;
										if (flag12)
										{
											GameSdkMgr.m_sdk.voicePlayedHanlde("error");
										}
										else
										{
											bool flag13 = num == 92;
											if (flag13)
											{
												GameSdkMgr.m_sdk.voicePlayedHanlde("played");
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
