using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_wantlvup : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_wantlvup.<>c <>9 = new a3_wantlvup.<>c();

			public static Action<GameObject> <>9__3_0;

			internal void <init>b__3_0(GameObject g)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_WANTLVUP);
			}
		}

		private Text cs_rotine;

		private Text cs_expfb;

		private Text cs_sczghd;

		private int map_x;

		private int map_y;

		private int map_id;

		public override void init()
		{
			this.cs_rotine = base.getTransformByPath("cells/scroll/content/routine/name/dj").GetComponent<Text>();
			this.cs_expfb = base.getTransformByPath("cells/scroll/content/expfb/name/dj").GetComponent<Text>();
			this.cs_sczghd = base.getTransformByPath("cells/scroll/content/sczghd/name/dj").GetComponent<Text>();
			BaseButton arg_74_0 = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			Action<GameObject> arg_74_1;
			if ((arg_74_1 = a3_wantlvup.<>c.<>9__3_0) == null)
			{
				arg_74_1 = (a3_wantlvup.<>c.<>9__3_0 = new Action<GameObject>(a3_wantlvup.<>c.<>9.<init>b__3_0));
			}
			arg_74_0.onClick = arg_74_1;
			new BaseButton(base.getTransformByPath("cells/scroll/content/routine/go"), 1, 1).onClick = new Action<GameObject>(this.routine_go);
			new BaseButton(base.getTransformByPath("cells/scroll/content/expfb/go"), 1, 1).onClick = new Action<GameObject>(this.expfb_go);
			new BaseButton(base.getTransformByPath("cells/scroll/content/sczghd/go"), 1, 1).onClick = new Action<GameObject>(this.sczghd_go);
			new BaseButton(base.getTransformByPath("cells/scroll/content/guaji/go"), 1, 1).onClick = new Action<GameObject>(this.sczghd_go);
			new BaseButton(base.getTransformByPath("cells/scroll/content/entrustTask/go"), 1, 1).onClick = new Action<GameObject>(this.entrust_go);
		}

		public override void onShowed()
		{
			this.refresh();
		}

		public override void onClosed()
		{
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
			Variant variant = SvrLevelConfig.instacne.get_level_data(101u);
			int num = variant["daily_cnt"];
			int num2 = 0;
			bool flag2 = MapModel.getInstance().dFbDta.ContainsKey(101);
			if (flag2)
			{
				num2 = Mathf.Min(MapModel.getInstance().dFbDta[101].cycleCount, num);
			}
			bool flag3 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EXP_DUNGEON, false);
			if (flag3)
			{
				this.cs_expfb.text = string.Concat(new object[]
				{
					"(",
					num - num2,
					"/",
					num,
					")"
				});
				base.transform.FindChild("cells/scroll/content/expfb").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("cells/scroll/content/expfb").gameObject.SetActive(false);
			}
			bool flag4 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.ENTRUST_TASK, false);
			if (flag4)
			{
				TaskData entrustTask;
				bool flag5 = (entrustTask = ModelBase<A3_TaskModel>.getInstance().GetEntrustTask()) != null;
				if (flag5)
				{
					int num3 = XMLMgr.instance.GetSXML("task.emis_limit", "").getInt("emis_limit") * XMLMgr.instance.GetSXML("task.emis_limit", "").getInt("loop_limit");
					int num4 = num3 - (entrustTask.taskCount + entrustTask.taskLoop * XMLMgr.instance.GetSXML("task.emis_limit", "").getInt("emis_limit"));
					num4 = ((num4 > 0) ? num4 : 0);
					base.transform.FindChild("cells/scroll/content/entrustTask").gameObject.SetActive(true);
					base.transform.FindChild("cells/scroll/content/entrustTask/name/dj").GetComponent<Text>().text = string.Format("{0}/{1}", num4, num3);
				}
				else
				{
					base.transform.FindChild("cells/scroll/content/entrustTask").gameObject.SetActive(false);
				}
			}
			else
			{
				base.transform.FindChild("cells/scroll/content/entrustTask").gameObject.SetActive(false);
			}
			bool flag6 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.AUTO_PLAY, false);
			if (flag6)
			{
				bool active_open = BaseProxy<GeneralProxy>.getInstance().active_open;
				if (active_open)
				{
					base.getTransformByPath("cells/scroll/content/sczghd/go").GetComponent<Button>().interactable = true;
					base.transform.FindChild("cells/scroll/content/sczghd").gameObject.SetActive(true);
					base.transform.FindChild("cells/scroll/content/guaji").gameObject.SetActive(false);
				}
				else
				{
					base.transform.FindChild("cells/scroll/content/sczghd").gameObject.SetActive(false);
					base.transform.FindChild("cells/scroll/content/guaji").gameObject.SetActive(true);
				}
			}
			else
			{
				base.getTransformByPath("cells/scroll/content/sczghd/go").GetComponent<Button>().interactable = false;
				base.transform.FindChild("cells/scroll/content/sczghd").gameObject.SetActive(false);
				base.transform.FindChild("cells/scroll/content/guaji").gameObject.SetActive(false);
			}
			this.cs_sczghd.text = "";
			this.want_to();
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
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_WANTLVUP);
			}
		}

		private void expfb_go(GameObject go)
		{
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EXP_DUNGEON, true);
			if (flag)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, null, false);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_WANTLVUP);
			}
		}

		private void sczghd_go(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_WANTLVUP);
			SelfRole.moveto(this.map_id, new Vector3((float)this.map_x, 0f, (float)this.map_y), null, 0.3f);
		}

		private void entrust_go(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_WANTLVUP);
			TaskData entrustTask = ModelBase<A3_TaskModel>.getInstance().GetEntrustTask();
			bool flag = entrustTask != null;
			if (flag)
			{
				a3_task_auto.instance.RunTask(entrustTask, false, false);
			}
		}
	}
}
