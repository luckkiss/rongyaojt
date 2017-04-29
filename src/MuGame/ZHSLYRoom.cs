using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class ZHSLYRoom : BaseRoomItem
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly ZHSLYRoom.<>c <>9 = new ZHSLYRoom.<>c();

			public static Action<int> <>9__5_0;

			public static Action<object> <>9__7_0;

			internal void <onStart>b__5_0(int i)
			{
				bool flag = a3_insideui_fb.instance != null;
				if (flag)
				{
					a3_insideui_fb.instance.SetKillNum(i, -1);
				}
			}

			internal void <onLevelFinish>b__7_0(object o)
			{
				MonsterMgr._inst.clear();
			}
		}

		public static bool isOpen;

		private MapData data;

		private uint startExp;

		private double entertimer;

		private Variant enterdata;

		public override void onStart(Variant svr)
		{
			base.onStart(svr);
			a3_insideui_fb.room = this;
			ZHSLYRoom.isOpen = true;
			this.data = MapModel.getInstance().getMapDta(105);
			bool flag = this.data == null;
			if (flag)
			{
				this.data = new MapData();
			}
			MapModel.getInstance().AddMapDta(105, this.data);
			MapData arg_79_0 = this.data;
			Action<int> arg_79_1;
			if ((arg_79_1 = ZHSLYRoom.<>c.<>9__5_0) == null)
			{
				arg_79_1 = (ZHSLYRoom.<>c.<>9__5_0 = new Action<int>(ZHSLYRoom.<>c.<>9.<onStart>b__5_0));
			}
			arg_79_0.OnKillNumChange = arg_79_1;
			this.startExp = ModelBase<PlayerModel>.getInstance().exp;
			bool flag2 = a3_liteMinimap.instance != null;
			if (flag2)
			{
				a3_liteMinimap.instance.updateUICseth();
			}
			bool flag3 = a3_liteMinimap.instance != null;
			if (flag3)
			{
				a3_liteMinimap.instance.refreshByUIState();
			}
			Variant variant = new Variant();
			variant["curLevelId"] = MapModel.getInstance().curLevelId;
			InterfaceMgr.doCommandByLua("a3_litemap_btns.refreshByUIState", "ui/interfaces/floatui/a3_litemap_btns", new object[]
			{
				variant
			});
			InterfaceMgr.doCommandByLua("a3_litemap.refreshByUIState", "ui/interfaces/floatui/a3_litemap", new object[]
			{
				variant
			});
			this.entertimer = (double)muNetCleint.instance.CurServerTimeStamp;
			a3_insideui_fb.begintime = this.entertimer;
			this.enterdata = muLGClient.instance.g_levelsCT.get_curr_lvl_info();
			a3_insideui_fb.ShowInUI(a3_insideui_fb.e_room.ZHSLY);
			bool flag4 = this.data != null;
			if (flag4)
			{
				this.data.cycleCount++;
			}
		}

		public override void onEnd()
		{
			base.onEnd();
			this.data.kmNum = 0;
			ZHSLYRoom.isOpen = false;
			bool flag = a3_liteMinimap.instance != null;
			if (flag)
			{
				a3_liteMinimap.instance.updateUICseth();
			}
			bool flag2 = a3_liteMinimap.instance != null;
			if (flag2)
			{
				a3_liteMinimap.instance.refreshByUIState();
			}
			Variant variant = new Variant();
			variant["curLevelId"] = MapModel.getInstance().curLevelId;
			InterfaceMgr.doCommandByLua("a3_litemap_btns.refreshByUIState", "ui/interfaces/floatui/a3_litemap_btns", new object[]
			{
				variant
			});
			InterfaceMgr.doCommandByLua("a3_litemap.refreshByUIState", "ui/interfaces/floatui/a3_litemap", new object[]
			{
				variant
			});
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_INSIDEUI_FB);
			BaseProxy<LevelProxy>.getInstance().sendGet_lvl_cnt_info(1, 0, 0);
		}

		public override bool onLevelFinish(Variant msgData)
		{
			base.onLevelFinish(msgData);
			base.CollectAllDrops();
			bool flag = a3_liteMinimap.instance != null;
			if (flag)
			{
				a3_liteMinimap.instance.clear();
			}
			int delayTime = 0;
			a3_insideui_fb.instance.transform.FindChild("normal/btn_quitfb").gameObject.SetActive(false);
			bool flag2 = msgData.ContainsKey("win");
			if (flag2)
			{
				int num = msgData["win"];
				bool flag3 = num > 0;
				if (flag3)
				{
					delayTime = 3;
				}
			}
			bool flag4 = muNetCleint.instance.CurServerTimeStamp < this.enterdata["end_tm"] - 1;
			if (flag4)
			{
				delayTime = 3;
			}
			timersManager arg_F3_0 = new timersManager();
			int arg_F3_1 = 3;
			Action<object> arg_F3_2;
			if ((arg_F3_2 = ZHSLYRoom.<>c.<>9__7_0) == null)
			{
				arg_F3_2 = (ZHSLYRoom.<>c.<>9__7_0 = new Action<object>(ZHSLYRoom.<>c.<>9.<onLevelFinish>b__7_0));
			}
			arg_F3_0.addTimer(arg_F3_1, arg_F3_2, 1, null);
			new timersManager().addTimer(delayTime, delegate(object o)
			{
				msgData["ltpid"] = this.enterdata["ltpid"];
				ArrayList arrayList = new ArrayList();
				arrayList.Add(msgData);
				double num2 = this.enterdata["end_tm"];
				double num3 = (double)Mathf.Min(muNetCleint.instance.CurServerTimeStamp, (int)num2) - this.entertimer;
				arrayList.Add(num3);
				arrayList.Add(this.data.kmNum);
				int num4 = (int)Mathf.Max(0f, ModelBase<PlayerModel>.getInstance().exp - this.startExp);
				arrayList.Add(num4);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_FB_FINISH, arrayList, false);
				a3_insideui_fb.instance.setAct();
			}, 1, null);
			return true;
		}

		public override void onGetMapMoney(int money)
		{
			FightText.play(FightText.MONEY_TEXT, lgSelfPlayer.instance.grAvatar.getHeadPos(), money, false, -1);
		}
	}
}
