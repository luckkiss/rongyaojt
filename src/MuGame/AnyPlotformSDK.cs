using Cross;
using System;
using UnityEngine;

namespace MuGame
{
	public class AnyPlotformSDK
	{
		private static IPlotformSDK _inst = new PlotformBaseSDK();

		public static int isInited
		{
			get
			{
				return AnyPlotformSDK._inst.isinited;
			}
		}

		public static void InitSDK()
		{
			GameObject gameObject = new GameObject("LoadAsync");
			SceneFXMgr sceneFXMgr = gameObject.AddComponent<SceneFXMgr>();
			ProfessionRole.ROLE_LVUP_FX = Resources.Load<GameObject>("FX/comFX/FX_common_shengji");
			P2Warrior_Event.WARRIOR_B1 = Resources.Load<GameObject>("bullet/warrior/SFX_9_1");
			P3Mage_Event.MAGE_B1 = Resources.Load<GameObject>("bullet/mage/bt1/b1");
			P3Mage_Event.MAGE_B2 = Resources.Load<GameObject>("bullet/mage/bt1/b2");
			P3Mage_Event.MAGE_B3 = Resources.Load<GameObject>("bullet/mage/bt1/b3");
			P3Mage_Event.MAGE_B4_1 = Resources.Load<GameObject>("bullet/mage/bt1/b4_1");
			P3Mage_Event.MAGE_B4_2 = Resources.Load<GameObject>("bullet/mage/bt1/b4_2");
			P3Mage_Event.MAGE_B6 = Resources.Load<GameObject>("bullet/mage/bt1/b6");
			P3Mage_Event.MAGE_S3002 = Resources.Load<GameObject>("FX/mage/SFX_31");
			P3Mage_Event.MAGE_S3011 = Resources.Load<GameObject>("FX/mage/SFX_3011_heiqiu");
			P3Mage_Event.MAGE_S3011_1 = Resources.Load<GameObject>("FX/mage/SFX_3011_baodian");
			P5Assassin_Event.ASSASSIN_S1 = Resources.Load<GameObject>("FX/assa/SFX_11_4");
			P5Assassin_Event.ASSASSIN_S2 = Resources.Load<GameObject>("FX/assa/SFX_11_2");
			M0x000_Role_Event.MAGE_B1 = P3Mage_Event.MAGE_B1;
			M0x000_Role_Event.MAGE_B2 = P3Mage_Event.MAGE_B2;
			M0x000_Role_Event.MAGE_B3 = P3Mage_Event.MAGE_B3;
			M0x000_Role_Event.MAGE_B4_1 = P3Mage_Event.MAGE_B4_1;
			M0x000_Role_Event.MAGE_B4_2 = P3Mage_Event.MAGE_B4_2;
			M0x000_Role_Event.MAGE_B6 = P3Mage_Event.MAGE_B6;
			M0x000_Role_Event.MAGE_S3002 = P3Mage_Event.MAGE_S3002;
			M0x000_Role_Event.MAGE_S3011 = P3Mage_Event.MAGE_S3011;
			M0x000_Role_Event.MAGE_S3011_1 = P3Mage_Event.MAGE_S3011_1;
			M0x000_Role_Event.WARRIOR_B1 = P2Warrior_Event.WARRIOR_B1;
			M0x000_Role_Event.ASSASSIN_S1 = P5Assassin_Event.ASSASSIN_S1;
			M0x000_Role_Event.ASSASSIN_S2 = P5Assassin_Event.ASSASSIN_S2;
			P2Warrior.WARRIOR_SFX1 = Resources.Load<GameObject>("FX/warrior/SFX_4");
			P2Warrior.WARRIOR_SFX2 = Resources.Load<GameObject>("FX/warrior/SFX_101");
			P2Warrior.WARRIOR_SFX3 = Resources.Load<GameObject>("FX/warrior/SFX_9");
			P2Warrior.WARRIOR_SFX4 = Resources.Load<GameObject>("FX/warrior/SFX_9_1");
			P2Warrior.WARRIOR_SFX5 = Resources.Load<GameObject>("FX/warrior/SFX_12");
			ohterP2Warrior.WARRIOR_SFX1 = P2Warrior.WARRIOR_SFX1;
			ohterP2Warrior.WARRIOR_SFX2 = P2Warrior.WARRIOR_SFX2;
			ohterP2Warrior.WARRIOR_SFX3 = P2Warrior.WARRIOR_SFX3;
			ohterP2Warrior.WARRIOR_SFX4 = P2Warrior.WARRIOR_SFX4;
			ohterP2Warrior.WARRIOR_SFX5 = P2Warrior.WARRIOR_SFX5;
			P3Mage.P3MAGE_SFX1 = Resources.Load<GameObject>("FX/mage/FX_mage_buff_dun");
			P3Mage.P3MAGE_SFX2 = Resources.Load<GameObject>("bullet/mage/bt1/s3003");
			P3Mage.P3MAGE_SFX3 = Resources.Load<GameObject>("FX/mage/SFX_30081");
			ohterP3Mage.P3MAGE_SFX1 = P3Mage.P3MAGE_SFX1;
			ohterP3Mage.P3MAGE_SFX2 = P3Mage.P3MAGE_SFX2;
			ohterP3Mage.P3MAGE_SFX3 = P3Mage.P3MAGE_SFX3;
			P5Assassin.ASSASSIN_SFX1 = Resources.Load<GameObject>("FX/assa/SFX_5");
			P5Assassin.ASSASSIN_SFX2 = Resources.Load<GameObject>("FX/assa/SFX_9_1");
			P5Assassin.ASSASSIN_SFX3 = Resources.Load<GameObject>("FX/assa/SFX_12_1");
			ohterP5Assassin.ASSASSIN_SFX1 = P5Assassin.ASSASSIN_SFX1;
			ohterP5Assassin.ASSASSIN_SFX2 = P5Assassin.ASSASSIN_SFX2;
			ohterP5Assassin.ASSASSIN_SFX3 = P5Assassin.ASSASSIN_SFX3;
			worldmap.EFFECT_CHUANSONG1 = Resources.Load<GameObject>("FX/comFX/fx_player_cs/FX_com_player_cszhong");
			worldmap.EFFECT_CHUANSONG2 = Resources.Load<GameObject>("FX/comFX/fx_player_cs/FX_com_player_chuxian");
			GameObject[] array = Resources.LoadAll<GameObject>("FX/monsterSFX/com_monster");
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject2 = array[i];
				Monster_Base_Event.bultList[gameObject2.name] = gameObject2;
			}
			bool flag = Application.platform == RuntimePlatform.IPhonePlayer;
			if (flag)
			{
				AnyPlotformSDK._inst = new IOSPlatformSDK();
			}
			else
			{
				bool flag2 = Application.platform == RuntimePlatform.Android;
				if (flag2)
				{
					debug.Log("InitSDK");
					AnyPlotformSDK._inst = new AndroidPlotformSDK();
				}
			}
		}

		public static void FrameMove()
		{
			AnyPlotformSDK._inst.FrameMove();
		}

		public static void Add_moreCmdInfo(string info, string jstr)
		{
			AnyPlotformSDK._inst.Add_moreCmdInfo(info, jstr);
		}

		public static void Call_Cmd(string cmd, string info = null, string jstr = null, bool waiting = true)
		{
			AnyPlotformSDK._inst.Call_Cmd(cmd, info, jstr, waiting);
		}

		public static void Cmd_CallBack(Variant v)
		{
			AnyPlotformSDK._inst.Cmd_CallBack(v);
		}
	}
}
