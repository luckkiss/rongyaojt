using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_getGoldWay : Window
	{
		public static a3_getGoldWay _instance;

		private BaseButton btn_close;

		private Text cs_rotine;

		private Text cs_goldfb;

		private Text cs_getMoney;

		private int map_x;

		private int map_y;

		private int map_id;

		public override void init()
		{
			this.btn_close = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			this.cs_rotine = base.getTransformByPath("cells/scroll/content/routine/name/dj").GetComponent<Text>();
			this.cs_goldfb = base.getTransformByPath("cells/scroll/content/goldfb/name/dj").GetComponent<Text>();
			this.cs_getMoney = base.getTransformByPath("cells/scroll/content/dianjin/name/dj").GetComponent<Text>();
			BaseButton expr_66 = this.btn_close;
			expr_66.onClick = (Action<GameObject>)Delegate.Combine(expr_66.onClick, new Action<GameObject>(this.onBtnCloseClick));
			new BaseButton(base.getTransformByPath("cells/scroll/content/routine/go"), 1, 1).onClick = new Action<GameObject>(this.routine_go);
			new BaseButton(base.getTransformByPath("cells/scroll/content/goldfb/go"), 1, 1).onClick = new Action<GameObject>(this.goldfb_go);
			new BaseButton(base.getTransformByPath("cells/scroll/content/guaji/go"), 1, 1).onClick = new Action<GameObject>(this.guaji_go);
			new BaseButton(base.getTransformByPath("cells/scroll/content/dianjin/go"), 1, 1).onClick = new Action<GameObject>(this.dianjin_go);
		}

		public override void onShowed()
		{
			this.btn_close.addEvent();
			this.want_to();
			this.refresh();
		}

		public override void onClosed()
		{
			this.btn_close.removeAllListener();
		}

		public void onBtnCloseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_GETGOLDWAY);
		}

		private void refresh()
		{
			TaskData dailyTask = ModelBase<A3_TaskModel>.getInstance().GetDailyTask();
			bool flag = dailyTask != null;
			if (flag)
			{
				this.cs_rotine.text = string.Concat(new object[]
				{
					"(",
					ModelBase<A3_TaskModel>.getInstance().GetTaskMaxCount(dailyTask.taskId) - dailyTask.taskCount,
					"/",
					ModelBase<A3_TaskModel>.getInstance().GetTaskMaxCount(dailyTask.taskId),
					")"
				});
				base.transform.FindChild("cells/scroll/content/routine").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("cells/scroll/content/routine").gameObject.SetActive(false);
			}
			Variant variant = SvrLevelConfig.instacne.get_level_data(102u);
			int num = variant["daily_cnt"];
			int num2 = 0;
			bool flag2 = MapModel.getInstance().dFbDta.ContainsKey(102);
			if (flag2)
			{
				num2 = Mathf.Min(MapModel.getInstance().dFbDta[102].cycleCount, num);
			}
			bool flag3 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.GOLD_DUNGEON, false) && num != num2;
			if (flag3)
			{
				this.cs_goldfb.text = string.Concat(new object[]
				{
					"(",
					num - num2,
					"/",
					num,
					")"
				});
				base.transform.FindChild("cells/scroll/content/goldfb").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("cells/scroll/content/goldfb").gameObject.SetActive(false);
			}
			bool flag4 = ModelBase<A3_VipModel>.getInstance().Level > 0;
			int num3;
			if (flag4)
			{
				num3 = ModelBase<A3_VipModel>.getInstance().vip_exchange_num(3);
			}
			else
			{
				num3 = 10;
			}
			ExchangeModel instance = ModelBase<ExchangeModel>.getInstance();
			bool flag5 = (long)num3 - (long)((ulong)instance.Count) > 0L;
			if (flag5)
			{
				this.cs_getMoney.text = string.Concat(new object[]
				{
					"(",
					(long)num3 - (long)((ulong)instance.Count),
					"/",
					num3,
					")"
				});
				base.transform.FindChild("cells/scroll/content/dianjin").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("cells/scroll/content/dianjin").gameObject.SetActive(false);
			}
		}

		private void want_to()
		{
			this.map_x = 0;
			this.map_y = 0;
			this.map_id = 1;
			SXML sXML = XMLMgr.instance.GetSXML("god_light", "");
			List<SXML> nodeList = sXML.GetNodeList("player_info", "");
			bool flag = nodeList != null;
			if (flag)
			{
				foreach (SXML current in nodeList)
				{
					bool flag2 = (long)current.getInt("zhuan") < (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl);
					if (!flag2)
					{
						bool flag3 = (long)current.getInt("zhuan") == (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl);
						if (!flag3)
						{
							break;
						}
						bool flag4 = (long)current.getInt("lv") > (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl);
						if (flag4)
						{
							break;
						}
					}
					this.map_id = current.getInt("map_id");
					this.map_x = current.getInt("map_x");
					this.map_y = current.getInt("map_y");
				}
			}
		}

		private void routine_go(GameObject go)
		{
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.DAILY_TASK, true);
			if (flag)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(20005);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_TASK, arrayList, false);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_GETGOLDWAY);
			}
		}

		private void goldfb_go(GameObject go)
		{
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.GOLD_DUNGEON, true);
			if (flag)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, null, false);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_GETGOLDWAY);
			}
		}

		private void guaji_go(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_GETGOLDWAY);
			SelfRole.moveto(this.map_id, new Vector3((float)this.map_x, 0f, (float)this.map_y), null, 0.3f);
		}

		private void dianjin_go(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_GETGOLDWAY);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
		}
	}
}
