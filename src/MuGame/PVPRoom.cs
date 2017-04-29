using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class PVPRoom : BaseRoomItem
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly PVPRoom.<>c <>9 = new PVPRoom.<>c();

			public static Action<object> <>9__11_0;

			internal void <onLevelFinish>b__11_0(object o)
			{
				MonsterMgr._inst.clear();
			}
		}

		public static bool isOpen;

		private uint startExp;

		private MapData data;

		private double entertimer;

		private Variant enterdata;

		private int Getach = 0;

		private int GetExp = 0;

		public static PVPRoom instan;

		public override void onStart(Variant svr)
		{
			base.onStart(svr);
			PVPRoom.instan = this;
			a3_insideui_fb.room = this;
			PVPRoom.isOpen = true;
			this.data = MapModel.getInstance().getMapDta(103);
			bool flag = this.data == null;
			if (flag)
			{
				this.data = new MapData();
			}
			MapModel.getInstance().AddMapDta(103, this.data);
			this.startExp = ModelBase<PlayerModel>.getInstance().exp;
			this.entertimer = (double)muNetCleint.instance.CurServerTimeStamp;
			a3_insideui_fb.begintime = this.entertimer;
			this.enterdata = muLGClient.instance.g_levelsCT.get_curr_lvl_info();
			MapModel.getInstance().curLevelId = 103u;
			InterfaceMgr.doCommandByLua("MapModel:getInstance().getcurLevelId", "model/MapModel", new object[]
			{
				103
			});
			a3_insideui_fb.ShowInUI(a3_insideui_fb.e_room.PVP);
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
			InterfaceMgr.doCommandByLua("herohead2.closethis", "ui/interfaces/floatui/herohead2", new object[0]);
			bool flag4 = tragethead.instan != null;
			if (flag4)
			{
				tragethead.instan.inFB = true;
			}
			bool flag5 = this.data != null;
			if (flag5)
			{
				this.data.cycleCount++;
			}
		}

		public override void onEnd()
		{
			base.onEnd();
			this.data.kmNum = 0;
			PVPRoom.isOpen = false;
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
			InterfaceMgr.openByLua("herohead2", null);
			bool flag3 = tragethead.instan != null;
			if (flag3)
			{
				tragethead.instan.inFB = false;
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_INSIDEUI_FB);
			BaseProxy<LevelProxy>.getInstance().sendGet_lvl_cnt_info(1, 0, 0);
		}

		public void refGet(int ach, int exp)
		{
			this.Getach = ach;
			this.GetExp = exp;
			this.getach = this.Getach;
			this.getExp = this.GetExp;
		}

		public override bool onLevelFinish(Variant msgData)
		{
			base.onLevelFinish(msgData);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_INSIDEUI_FB);
			bool flag = a3_liteMinimap.instance != null;
			if (flag)
			{
				a3_liteMinimap.instance.clear();
			}
			int delayTime = 0;
			bool flag2 = msgData.ContainsKey("win");
			if (flag2)
			{
				int num = msgData["win"];
				bool flag3 = num > 0;
				if (flag3)
				{
					delayTime = 1;
				}
			}
			bool flag4 = muNetCleint.instance.CurServerTimeStamp < this.enterdata["end_tm"] - 1;
			if (flag4)
			{
				delayTime = 1;
			}
			timersManager arg_DC_0 = new timersManager();
			int arg_DC_1 = 3;
			Action<object> arg_DC_2;
			if ((arg_DC_2 = PVPRoom.<>c.<>9__11_0) == null)
			{
				arg_DC_2 = (PVPRoom.<>c.<>9__11_0 = new Action<object>(PVPRoom.<>c.<>9.<onLevelFinish>b__11_0));
			}
			arg_DC_0.addTimer(arg_DC_1, arg_DC_2, 1, null);
			new timersManager().addTimer(delayTime, delegate(object o)
			{
				this.CollectAllDrops1();
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
			}, 1, null);
			return true;
		}

		public void onLevelFinish()
		{
		}

		public override void onGetMapMoney(int money)
		{
			FightText.play(FightText.MONEY_TEXT, lgSelfPlayer.instance.grAvatar.getHeadPos(), money, false, -1);
		}
	}
}
