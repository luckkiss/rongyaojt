using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class PlotRoom : BaseRoomItem
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly PlotRoom.<>c <>9 = new PlotRoom.<>c();

			public static Action<object> <>9__8_0;

			internal void <onLevelFinish>b__8_0(object o)
			{
				MonsterMgr._inst.clear();
			}
		}

		private Variant enterdata;

		private int killnum;

		private int moneynum;

		private double entertimer;

		public override void onStart(Variant svr)
		{
			base.onStart(svr);
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
			this.enterdata = muLGClient.instance.g_levelsCT.get_curr_lvl_info();
			this.entertimer = (double)muNetCleint.instance.CurServerTimeStamp;
			a3_insideui_fb.ShowInUI(a3_insideui_fb.e_room.Normal);
			this.killnum = 0;
			this.moneynum = 0;
		}

		public override void onEnd()
		{
			base.onEnd();
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
			this.killnum = 0;
			this.moneynum = 0;
			a3_insideui_fb.CloseInUI();
		}

		public override void onMonsterDied(MonsterRole monster)
		{
			this.killnum++;
		}

		public override void onPickMoney(int num)
		{
			this.moneynum += num;
		}

		public override bool onLevelFinish(Variant msgData)
		{
			base.onLevelFinish(msgData);
			base.CollectAllDrops1();
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
			if ((arg_F3_2 = PlotRoom.<>c.<>9__8_0) == null)
			{
				arg_F3_2 = (PlotRoom.<>c.<>9__8_0 = new Action<object>(PlotRoom.<>c.<>9.<onLevelFinish>b__8_0));
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
				arrayList.Add(this.killnum);
				arrayList.Add(this.moneynum);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_FB_FINISH, arrayList, false);
			}, 1, null);
			return true;
		}
	}
}
