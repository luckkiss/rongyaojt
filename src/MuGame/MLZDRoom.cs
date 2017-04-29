using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class MLZDRoom : BaseRoomItem
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly MLZDRoom.<>c <>9 = new MLZDRoom.<>c();

			public static Action<object> <>9__7_0;

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
			MLZDRoom.isOpen = true;
			this.data = MapModel.getInstance().getMapDta(104);
			bool flag = this.data == null;
			if (flag)
			{
				this.data = new MapData();
			}
			MapModel.getInstance().AddMapDta(104, this.data);
			this.data.OnKillNumChange = delegate(int i)
			{
				bool flag6 = a3_insideui_fb.instance != null;
				if (flag6)
				{
					Variant variant3 = SvrLevelConfig.instacne.get_level_data(104u);
					foreach (Variant current6 in variant3["fin_check"]._arr)
					{
						foreach (Variant current7 in current6["km"]._arr)
						{
							int total_enemyNum = current7["cnt"];
							this.data.total_enemyNum = total_enemyNum;
						}
					}
					a3_insideui_fb.instance.SetKillNum(i, this.data.total_enemyNum);
				}
			};
			int num = 0;
			Variant variant = SvrLevelConfig.instacne.get_level_data(104u);
			foreach (Variant current in variant["diff_lvl"]._arr)
			{
				bool flag2 = current != null;
				if (flag2)
				{
					foreach (Variant current2 in current["map"]._arr)
					{
						foreach (Variant current3 in current2["trigger"]._arr)
						{
							foreach (Variant current4 in current3["addmon"]._arr)
							{
								foreach (Variant current5 in current4["m"]._arr)
								{
									uint num2 = current5["mid"];
									num++;
								}
							}
						}
					}
				}
			}
			this.data.total_enemyNum = num;
			this.startExp = ModelBase<PlayerModel>.getInstance().exp;
			bool flag3 = a3_liteMinimap.instance != null;
			if (flag3)
			{
				a3_liteMinimap.instance.updateUICseth();
			}
			bool flag4 = a3_liteMinimap.instance != null;
			if (flag4)
			{
				a3_liteMinimap.instance.refreshByUIState();
			}
			Variant variant2 = new Variant();
			variant2["curLevelId"] = MapModel.getInstance().curLevelId;
			InterfaceMgr.doCommandByLua("a3_litemap_btns.refreshByUIState", "ui/interfaces/floatui/a3_litemap_btns", new object[]
			{
				variant2
			});
			InterfaceMgr.doCommandByLua("a3_litemap.refreshByUIState", "ui/interfaces/floatui/a3_litemap", new object[]
			{
				variant2
			});
			this.entertimer = (double)muNetCleint.instance.CurServerTimeStamp;
			a3_insideui_fb.begintime = this.entertimer;
			this.enterdata = muLGClient.instance.g_levelsCT.get_curr_lvl_info();
			a3_insideui_fb.ShowInUI(a3_insideui_fb.e_room.MLZD);
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
			MLZDRoom.isOpen = false;
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
			InterfaceMgr.doCommandByLua("a3_litemap_btns.refreshByUIState", "ui/interfaces/floatui/a3_litemap_btns", new object[0]);
			InterfaceMgr.doCommandByLua("a3_litemap.refreshByUIState", "ui/interfaces/floatui/a3_litemap", new object[0]);
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
			if ((arg_F3_2 = MLZDRoom.<>c.<>9__7_0) == null)
			{
				arg_F3_2 = (MLZDRoom.<>c.<>9__7_0 = new Action<object>(MLZDRoom.<>c.<>9.<onLevelFinish>b__7_0));
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
