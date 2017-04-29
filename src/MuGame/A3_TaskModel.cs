using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MuGame
{
	public class A3_TaskModel : ModelBase<A3_TaskModel>
	{
		public static uint ON_NPC_TASK_STATE_CHANGE = 1u;

		public static uint DAILY_TASK_LIMIT = 10u;

		public static uint REMOVE_TOP_SHOW_TASK = 11u;

		public static uint CLAN_LOOP_LIMIT;

		public static uint CLAN_CNT_EACHLOOP;

		public static uint CLAN_MAX_COUNT;

		public bool isSubTask = false;

		public int main_task_id;

		public int main_chapter_id = -1;

		private SXML taskXML = null;

		public Dictionary<TaskType, Variant> dicTaskConfig = new Dictionary<TaskType, Variant>();

		public Dictionary<int, List<EntrustExtraRewardData>> dicEntrustExtraReward;

		public TaskData curTask = null;

		private Dictionary<int, TaskData> dicPlayerTask = new Dictionary<int, TaskData>();

		private Dictionary<int, TaskData> dicAcceptableTask = new Dictionary<int, TaskData>();

		public List<TaskData> listAddTask;

		public static readonly int REWARD_CLAN_MONEY = 1;

		public static readonly int REWARD_CLAN_EXP = 2;

		public static readonly int REWARD_CLAN_DONATE = 3;

		public static readonly int REWARD_CLAN_ACTIVE = 4;

		private SXML TaskXML
		{
			get
			{
				SXML arg_28_0;
				if ((arg_28_0 = this.taskXML) == null)
				{
					arg_28_0 = (this.taskXML = XMLMgr.instance.GetSXML("task", ""));
				}
				return arg_28_0;
			}
		}

		public Func<SXML> GetTaskXML
		{
			get
			{
				return () => this.TaskXML;
			}
		}

		public A3_TaskModel()
		{
			A3_TaskModel.DAILY_TASK_LIMIT = XMLMgr.instance.GetSXML("task", "").GetNode("limit_num", "").getUint("dailytask");
			A3_TaskModel.CLAN_CNT_EACHLOOP = XMLMgr.instance.GetSXML("task", "").GetNode("cmis_limit", "").getUint("cmis_limit");
			A3_TaskModel.CLAN_LOOP_LIMIT = XMLMgr.instance.GetSXML("task", "").GetNode("cmis_limit", "").getUint("loop_limit");
			A3_TaskModel.CLAN_MAX_COUNT = A3_TaskModel.CLAN_CNT_EACHLOOP * A3_TaskModel.CLAN_LOOP_LIMIT;
			this.dicEntrustExtraReward = new Dictionary<int, List<EntrustExtraRewardData>>();
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("task.entrust_extra_award", "");
			bool flag = sXMLList != null;
			if (flag)
			{
				for (int i = 0; i < sXMLList.Count; i++)
				{
					int @int = sXMLList[i].getInt("zhuan");
					bool flag2 = @int >= 0;
					if (flag2)
					{
						this.dicEntrustExtraReward[@int] = new List<EntrustExtraRewardData>();
						List<SXML> nodeList = sXMLList[i].GetNodeList("RewardItem", "");
						for (int j = 0; j < nodeList.Count; j++)
						{
							EntrustExtraRewardData entrustExtraRewardData = default(EntrustExtraRewardData);
							entrustExtraRewardData.tpid = nodeList[j].getUint("item_id");
							entrustExtraRewardData.num = nodeList[j].getInt("value");
							bool flag3 = entrustExtraRewardData.tpid != 0u && entrustExtraRewardData.num != 0;
							if (flag3)
							{
								this.dicEntrustExtraReward[@int].Add(entrustExtraRewardData);
							}
						}
						bool flag4 = this.dicEntrustExtraReward[@int].Count == 0;
						if (flag4)
						{
							this.dicEntrustExtraReward.Remove(@int);
						}
					}
				}
			}
		}

		public ChapterInfos GetChapterInfosById(int chapterId)
		{
			ChapterInfos chapterInfos = new ChapterInfos();
			SXML node = this.TaskXML.GetNode("Chapter", "id==" + chapterId);
			bool flag = node != null;
			if (flag)
			{
				chapterInfos.id = node.getInt("id");
				chapterInfos.name = node.getString("name");
				chapterInfos.description = node.getString("description");
			}
			return chapterInfos;
		}

		public Dictionary<int, TaskData> GetDicTaskData()
		{
			return this.dicPlayerTask;
		}

		private bool SyncXmlConf(TaskData data)
		{
			SXML node = this.TaskXML.GetNode("Task", "id==" + data.taskId);
			bool flag = node != null;
			bool flag2 = flag;
			if (flag2)
			{
				data.taskName = node.getString("name");
				data.targetType = (TaskTargetType)node.getInt("target_type");
				data.completion = node.getInt("target_param1");
				data.completionAim = node.getInt("target_param2");
				data.completionStr = node.getString("target_param2");
				data.extraAward = node.getInt("extra_award");
				data.chapterId = node.getInt("Chapter_id");
				data.taskMapid = node.getInt("tasking_map_id");
				data.transto = node.getInt("transto");
				data.guide = (node.getInt("guide") == 1);
				data.npcId = node.getInt("complete_npc_id");
				data.completeWay = node.getInt("complete_way");
				data.story_hint = node.getString("story_hint");
				data.next_step = node.hasValue("next_step");
				data.explain = node.getString("explain");
				data.topShowOnLiteminimap = node.hasValue("top_show");
				data.isComplete = (data.taskRate >= data.completion);
				data.need_tm = (node.hasValue("need_tm") ? node.getFloat("need_tm") : 0f);
				data.extraRateDesc = (node.hasValue("target_bar") ? node.getString("target_bar") : null);
				data.showMessage = node.hasValue("show_message");
			}
			return flag;
		}

		private bool isNewChapter(int taskid)
		{
			bool flag = false;
			SXML node = this.TaskXML.GetNode("Task", "id==" + (taskid - 1));
			bool flag2 = node == null;
			bool result;
			if (flag2)
			{
				result = true;
			}
			else
			{
				SXML node2 = this.TaskXML.GetNode("Task", "id==" + taskid);
				bool flag3 = node.getInt("Chapter_id") < node2.getInt("Chapter_id");
				result = (flag3 || flag);
			}
			return result;
		}

		public void OnSyncTask(Variant data)
		{
			this.dicPlayerTask.Clear();
			List<TaskData> list = new List<TaskData>();
			bool flag = data.ContainsKey("mlmis");
			if (flag)
			{
				Variant variant = data["mlmis"];
				TaskData taskData = new TaskData();
				taskData.taskId = variant["id"];
				this.main_task_id = variant["id"];
				taskData.taskRate = variant["cnt"];
				taskData.taskT = TaskType.MAIN;
				bool flag2 = variant.ContainsKey("lose_tm");
				if (flag2)
				{
					taskData.lose_tm = variant["lose_tm"];
				}
				TaskData arg_EF_0 = taskData;
				SXML expr_DD = this.TaskXML.GetNode("Task", "id==" + taskData.taskId).GetNode("m", "");
				arg_EF_0.release_tm = (long)((expr_DD != null) ? expr_DD.getInt("release_tm") : 0);
				list.Add(taskData);
			}
			bool flag3 = data.ContainsKey("dmis");
			if (flag3)
			{
				Variant variant2 = data["dmis"];
				TaskData taskData2 = new TaskData();
				taskData2.taskT = TaskType.DAILY;
				bool flag4 = variant2.ContainsKey("dmis_count");
				if (flag4)
				{
					taskData2.taskCount = variant2["dmis_count"];
				}
				bool flag5 = variant2.ContainsKey("id");
				if (flag5)
				{
					taskData2.taskId = variant2["id"];
					taskData2.taskRate = variant2["cnt"];
					taskData2.taskStar = variant2["star"];
					list.Add(taskData2);
				}
			}
			bool flag6 = data.ContainsKey("bmis");
			if (flag6)
			{
				List<Variant> list2 = new List<Variant>();
				list2 = data["bmis"]._arr;
				foreach (Variant current in list2)
				{
					TaskData taskData3 = new TaskData();
					taskData3.taskT = TaskType.BRANCH;
					taskData3.taskId = current["id"];
					taskData3.taskRate = current["cnt"];
					bool flag7 = current.ContainsKey("lose_tm");
					if (flag7)
					{
						taskData3.lose_tm = current["lose_tm"];
					}
					TaskData arg_2B7_0 = taskData3;
					SXML expr_2A5 = this.TaskXML.GetNode("Task", "id==" + taskData3.taskId).GetNode("m", "");
					arg_2B7_0.release_tm = (long)((expr_2A5 != null) ? expr_2A5.getInt("release_tm") : 0);
					this.SyncXmlConf(taskData3);
					list.Add(taskData3);
				}
			}
			bool flag8 = data.ContainsKey("emis");
			if (flag8)
			{
				Variant variant3 = data["emis"];
				TaskData taskData4 = new TaskData();
				bool flag9 = variant3.ContainsKey("id");
				if (flag9)
				{
					taskData4.taskT = TaskType.ENTRUST;
					taskData4.taskCount = variant3["emis_count"];
					taskData4.taskLoop = variant3["loop_count"];
					taskData4.taskId = variant3["id"];
					taskData4.taskRate = variant3["cnt"];
					bool flag10 = variant3.ContainsKey("lose_tm");
					if (flag10)
					{
						taskData4.lose_tm = variant3["lose_tm"];
					}
					TaskData arg_407_0 = taskData4;
					SXML expr_3F5 = this.TaskXML.GetNode("Task", "id==" + taskData4.taskId).GetNode("m", "");
					arg_407_0.release_tm = (long)((expr_3F5 != null) ? expr_3F5.getInt("release_tm") : 0);
					list.Add(taskData4);
				}
			}
			bool flag11 = data.ContainsKey("cmis");
			if (flag11)
			{
				Variant variant4 = data["cmis"];
				TaskData taskData5 = new TaskData();
				bool flag12 = variant4.ContainsKey("id");
				if (flag12)
				{
					taskData5.taskT = TaskType.CLAN;
					taskData5.taskCount = variant4["cmis_count"];
					taskData5.taskLoop = variant4["loop_count"];
					taskData5.taskId = variant4["id"];
					taskData5.taskRate = variant4["cnt"];
					bool flag13 = variant4.ContainsKey("lose_tm");
					if (flag13)
					{
						taskData5.lose_tm = variant4["lose_tm"];
					}
					TaskData arg_531_0 = taskData5;
					SXML expr_51F = this.TaskXML.GetNode("Task", "id==" + taskData5.taskId).GetNode("m", "");
					arg_531_0.release_tm = (long)((expr_51F != null) ? expr_51F.getInt("release_tm") : 0);
					list.Add(taskData5);
				}
			}
			foreach (TaskData current2 in list)
			{
				bool flag14 = !this.SyncXmlConf(current2);
				if (flag14)
				{
					debug.Log("未找到任务配置");
				}
				this.dicPlayerTask[current2.taskId] = current2;
				bool flag15 = current2.taskT == TaskType.MAIN;
				if (flag15)
				{
					this.dicPlayerTask[current2.taskId].taskCount = this.GetMainTaskIndex(current2.taskId);
				}
				this.DispatchNpcEvent(current2.taskId);
			}
		}

		public List<EntrustExtraRewardData> GetEntrustRewardList()
		{
			List<int> list = new List<int>(this.dicEntrustExtraReward.Keys);
			int num = -1;
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = (long)list[i] > (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl);
				if (flag)
				{
					break;
				}
				num = list[i];
			}
			bool flag2 = num == -1;
			List<EntrustExtraRewardData> result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = this.dicEntrustExtraReward[num];
			}
			return result;
		}

		public void OnAddTask(Variant data)
		{
			List<TaskData> list = new List<TaskData>();
			bool flag = data.ContainsKey("mlmis");
			if (flag)
			{
				Variant variant = data["mlmis"];
				TaskData taskData6 = new TaskData();
				this.main_task_id = variant["id"];
				taskData6.taskId = variant["id"];
				taskData6.taskRate = variant["cnt"];
				TaskData arg_B5_0 = taskData6;
				SXML expr_A3 = this.TaskXML.GetNode("Task", "id==" + taskData6.taskId).GetNode("m", "");
				arg_B5_0.release_tm = (long)((expr_A3 != null) ? expr_A3.getInt("release_tm") : 0);
				taskData6.taskT = TaskType.MAIN;
				list.Add(taskData6);
			}
			else
			{
				bool flag2 = data.ContainsKey("dmis");
				if (flag2)
				{
					Variant variant2 = data["dmis"];
					TaskData taskData2 = new TaskData();
					taskData2.taskT = TaskType.DAILY;
					taskData2.taskCount = variant2["dmis_count"];
					bool flag3 = variant2.ContainsKey("id");
					if (flag3)
					{
						taskData2.taskId = variant2["id"];
						taskData2.taskRate = variant2["cnt"];
						taskData2.taskStar = variant2["star"];
						TaskData arg_1BF_0 = taskData2;
						SXML expr_1AD = this.TaskXML.GetNode("Task", "id==" + taskData2.taskId).GetNode("m", "");
						arg_1BF_0.release_tm = (long)((expr_1AD != null) ? expr_1AD.getInt("release_tm") : 0);
					}
					else
					{
						bool flag4 = this.GetDailyTask() != null;
						if (flag4)
						{
							a3_liteMinimap.instance.SubmitTask(this.GetDailyTask().taskId);
						}
					}
					bool flag5 = (long)taskData2.taskCount >= (long)((ulong)A3_TaskModel.DAILY_TASK_LIMIT);
					if (flag5)
					{
						flytxt.instance.fly("您已完成今日所有日常任务。", 5, default(Color), null);
					}
					list.Add(taskData2);
				}
				else
				{
					bool flag6 = data.ContainsKey("bmis");
					if (flag6)
					{
						List<Variant> list2 = new List<Variant>();
						list2 = data["bmis"]._arr;
						foreach (Variant current in list2)
						{
							TaskData taskData3 = new TaskData();
							taskData3.taskT = TaskType.BRANCH;
							taskData3.taskId = current["id"];
							taskData3.taskRate = current["cnt"];
							TaskData arg_2FF_0 = taskData3;
							SXML expr_2ED = this.TaskXML.GetNode("Task", "id==" + taskData3.taskId).GetNode("m", "");
							arg_2FF_0.release_tm = (long)((expr_2ED != null) ? expr_2ED.getInt("release_tm") : 0);
							list.Add(taskData3);
						}
					}
					else
					{
						bool flag7 = data.ContainsKey("emis");
						if (flag7)
						{
							Variant variant3 = data["emis"];
							TaskData taskData4 = new TaskData();
							taskData4.taskT = TaskType.ENTRUST;
							taskData4.taskCount = variant3["emis_count"];
							bool flag8 = variant3.ContainsKey("id");
							if (flag8)
							{
								taskData4.taskId = variant3["id"];
								taskData4.taskRate = variant3["cnt"];
								taskData4.taskLoop = variant3["loop_count"];
								TaskData arg_421_0 = taskData4;
								SXML expr_40F = this.TaskXML.GetNode("Task", "id==" + taskData4.taskId).GetNode("m", "");
								arg_421_0.release_tm = (long)((expr_40F != null) ? expr_40F.getInt("release_tm") : 0);
							}
							a3_task expr_42C = a3_task.instance;
							if (expr_42C != null)
							{
								expr_42C.tabEntrust.SetActive(true);
							}
							list.Add(taskData4);
						}
						else
						{
							bool flag9 = data.ContainsKey("cmis");
							if (flag9)
							{
								Variant variant4 = data["cmis"];
								TaskData taskData5 = new TaskData();
								taskData5.taskT = TaskType.CLAN;
								taskData5.taskCount = variant4["cmis_count"];
								bool flag10 = variant4.ContainsKey("id");
								if (flag10)
								{
									taskData5.taskId = variant4["id"];
									taskData5.taskRate = variant4["cnt"];
									taskData5.taskLoop = variant4["loop_count"];
									TaskData arg_53D_0 = taskData5;
									SXML expr_52B = this.TaskXML.GetNode("Task", "id==" + taskData5.taskId).GetNode("m", "");
									arg_53D_0.release_tm = (long)((expr_52B != null) ? expr_52B.getInt("release_tm") : 0);
								}
								list.Add(taskData5);
							}
						}
					}
				}
			}
			this.listAddTask = new List<TaskData>();
			using (List<TaskData>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					TaskData taskData = enumerator2.Current;
					bool flag11 = !this.SyncXmlConf(taskData);
					if (flag11)
					{
						debug.Log("未找到任务配置");
					}
					this.OnProcessTaskList(taskData);
					this.dicPlayerTask[taskData.taskId] = taskData;
					this.listAddTask.Add(taskData);
					this.DispatchNpcEvent(taskData.taskId);
					bool flag12 = (taskData.taskT == TaskType.MAIN && taskData.targetType != TaskTargetType.GETEXP) || taskData.taskT == TaskType.ENTRUST || taskData.taskT == TaskType.CLAN;
					if (flag12)
					{
						bool flag13 = this.curTask == null || this.curTask.taskT == taskData.taskT;
						if (flag13)
						{
							bool flag14 = taskData.transto > 0;
							if (flag14)
							{
								SelfRole.Transmit(taskData.transto, delegate
								{
									a3_task_auto.instance.RunTask(taskData, true, false);
								}, false, true);
							}
							else
							{
								bool flag15 = !SelfRole.s_bInTransmit;
								if (flag15)
								{
									a3_task_auto.instance.RunTask(taskData, true, false);
								}
							}
						}
					}
					bool flag16 = taskData.taskT == TaskType.DAILY;
					if (flag16)
					{
						a3_task_auto.instance.RunTask(taskData, false, false);
					}
					bool flag17 = taskData.targetType == TaskTargetType.KILL;
					if (flag17)
					{
						SXML node = this.TaskXML.GetNode("Task", "id==" + taskData.taskId);
						bool flag18 = !ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.ContainsKey(taskData.taskId);
						if (flag18)
						{
							ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Add(taskData.taskId, node.getInt("target_param2"));
						}
					}
					a3_liteMinimap expr_77C = a3_liteMinimap.instance;
					if (expr_77C != null)
					{
						expr_77C.RefreshTaskPage(taskData.taskId);
					}
				}
			}
		}

		public static void onCD(cd item)
		{
			int num = (int)(cd.secCD - cd.lastCD) / 100;
			item.txt.text = "传送中 " + ((float)num / 10f).ToString();
		}

		private void OnProcessTaskList(TaskData taskData)
		{
			int taskId = taskData.taskId;
			bool flag = taskId == 0;
			if (flag)
			{
				List<int> list = new List<int>();
				foreach (TaskData current in this.dicPlayerTask.Values)
				{
					bool flag2 = current.taskT == taskData.taskT;
					if (flag2)
					{
						list.Add(current.taskId);
					}
				}
				foreach (int current2 in list)
				{
					this.removeTask(current2);
				}
				taskData.taskT = TaskType.NULL;
			}
			else
			{
				this.dicPlayerTask[taskId] = taskData;
				switch (taskData.taskT)
				{
				case TaskType.MAIN:
					this.dicPlayerTask[taskId].taskCount = this.GetMainTaskIndex(taskId);
					break;
				}
			}
		}

		public void OnSubmitTask(Variant data)
		{
			int num = data["id"];
			List<TaskRewardData> reward = this.GetReward(num);
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			foreach (TaskRewardData current in reward)
			{
				bool flag = current.type == 2 || current.type == 3;
				if (flag)
				{
					bool flag2 = !this.IsTalkWithSameNpc();
					if (flag2)
					{
						a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)current.id);
						bool flag3 = itemDataById.job_limit == ModelBase<PlayerModel>.getInstance().profession;
						if (flag3)
						{
							flytxt.instance.fly(itemDataById.item_name + "  +" + Mathf.Max(1, current.num), 5, Globle.getColorByQuality(itemDataById.quality), null);
							GameObject gameObject = IconImageMgr.getInstance().createA3ItemIconTip((uint)current.id, false, current.num, 1f, false, -1, 0, false, false, false);
							UnityEngine.Object.Destroy(gameObject.transform.FindChild("iconborder/equip_canequip").gameObject);
							UnityEngine.Object.Destroy(gameObject.transform.FindChild("num").gameObject);
							flytxt.instance.fly(null, 6, default(Color), gameObject);
						}
						bool flag4 = current.type == 3;
						if (flag4)
						{
							flytxt.instance.fly(null, 6, default(Color), IconImageMgr.getInstance().createA3ItemIconTip((uint)current.id, false, current.num, 1f, false, -1, 0, false, false, false));
						}
					}
				}
			}
			bool flag5 = list.Count > 0;
			if (flag5)
			{
				flytxt.instance.FlyQueue(list);
			}
			bool flag6 = this.dicPlayerTask.ContainsKey(num);
			if (flag6)
			{
				TaskData taskData = this.dicPlayerTask[num];
				bool flag7 = taskData.taskT == TaskType.MAIN && this.isNewChapter(taskData.taskId) && this.main_chapter_id >= 0;
				if (flag7)
				{
					a3_chapter_hint.ShowChapterHint(taskData.chapterId);
				}
				bool flag8 = taskData.taskT == TaskType.CLAN;
				if (flag8)
				{
					bool flag9 = (long)(taskData.taskCount + 1) == (long)((ulong)A3_TaskModel.CLAN_CNT_EACHLOOP);
					if (flag9)
					{
						List<SXML> nodeList = XMLMgr.instance.GetSXML("task.clan_extra", "").GetNodeList("RewardItem", "");
						List<string> list2 = new List<string>();
						for (int i = 0; i < nodeList.Count; i++)
						{
							uint @uint = nodeList[i].getUint("item_id");
							int @int = nodeList[i].getInt("value");
							a3_ItemData itemDataById2 = ModelBase<a3_BagModel>.getInstance().getItemDataById(@uint);
							bool flag10 = itemDataById2.item_name != null;
							if (flag10)
							{
								string item_name = itemDataById2.item_name;
								list2.Add(string.Format("获得了{0}×{1}", item_name, @int));
							}
						}
						bool flag11 = list2.Count != 0;
						if (flag11)
						{
							flytxt.instance.AddDelayFlytxtList(list2);
							flytxt.instance.StartDelayFly(0.2f * (float)(1 + reward.Count), 0.2f);
						}
					}
				}
			}
			this.removeTask(num);
		}

		public bool IsTalkWithSameNpc()
		{
			SXML node = this.TaskXML.GetNode("Task", "id==" + this.main_task_id);
			bool flag = node != null;
			bool result;
			if (flag)
			{
				int @int = node.getInt("follow_task_id");
				bool flag2 = @int > 0;
				if (flag2)
				{
					SXML node2 = this.TaskXML.GetNode("Task", "id==" + @int);
					bool flag3 = node2 != null;
					if (flag3)
					{
						int int2 = node2.getInt("target_type");
						bool flag4 = int2 == 22 && node2.getInt("complete_npc_id") == node.getInt("complete_npc_id");
						if (flag4)
						{
							result = true;
							return result;
						}
					}
				}
			}
			result = false;
			return result;
		}

		private void removeTask(int taskId)
		{
			bool flag = !this.dicPlayerTask.ContainsKey(taskId);
			if (!flag)
			{
				bool topShowOnLiteminimap = this.dicPlayerTask[taskId].topShowOnLiteminimap;
				if (topShowOnLiteminimap)
				{
					base.dispatchEvent(GameEvent.Create(A3_TaskModel.REMOVE_TOP_SHOW_TASK, this, null, false));
				}
				bool flag2 = this.GetTaskDataById(taskId).taskT == TaskType.ENTRUST;
				if (flag2)
				{
					a3_task expr_57 = a3_task.instance;
					if (expr_57 != null)
					{
						expr_57.tabEntrust.SetActive(false);
					}
				}
				this.dicPlayerTask.Remove(taskId);
				this.curTask = null;
				this.DispatchNpcEvent(taskId);
			}
		}

		public void RefreshTask(Variant data)
		{
			List<Variant> arr = data["change_task"]._arr;
			foreach (Variant current in arr)
			{
				int num = current["id"];
				int num2 = current["cnt"];
				bool flag = !this.dicPlayerTask.ContainsKey(num);
				if (!flag)
				{
					TaskData taskData = this.dicPlayerTask[num];
					taskData.taskRate = num2;
					bool flag2 = current.ContainsKey("star");
					if (flag2)
					{
						taskData.taskStar = current["star"];
					}
					taskData.isComplete = (num2 >= taskData.completion);
					bool flag3 = taskData.taskId == this.main_task_id && taskData.isComplete;
					if (flag3)
					{
					}
					this.dicPlayerTask[num] = taskData;
					bool flag4 = taskData.targetType == TaskTargetType.COLLECT || taskData.targetType == TaskTargetType.KILL;
					if (flag4)
					{
						SXML sXML = XMLMgr.instance.GetSXML("task.Task", "id==" + taskData.taskId);
						int @int = sXML.getInt("target_param2");
						SXML sXML2 = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + @int);
						string @string = sXML2.getString("name");
						flytxt.instance.fly(string.Concat(new object[]
						{
							(taskData.extraRateDesc != null) ? taskData.extraRateDesc : @string,
							"(",
							taskData.taskRate,
							"/",
							taskData.completion,
							")"
						}), 5, default(Color), null);
					}
					bool flag5 = taskData.targetType == TaskTargetType.KILL_MONSTER_GIVEN;
					if (flag5)
					{
						bool flag6 = current.ContainsKey("lose_tm");
						if (flag6)
						{
							taskData.lose_tm = current["lose_tm"];
						}
					}
					bool flag7 = taskData.taskRate == taskData.completion;
					if (flag7)
					{
						ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Remove(taskData.taskId);
						ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Remove(taskData.taskId);
						SelfRole.UnderTaskAutoMove = false;
					}
					bool isComplete = taskData.isComplete;
					if (isComplete)
					{
						this.DispatchNpcEvent(num);
						flytxt.instance.fly(taskData.taskName + " （已完成）", 5, default(Color), null);
					}
					bool flag8 = taskData.taskT == TaskType.MAIN && taskData.isComplete && !SelfRole.s_bInTransmit && taskData.next_step;
					if (flag8)
					{
						a3_task_auto.instance.RunTask(taskData, true, false);
					}
					bool flag9 = taskData.taskT == TaskType.MAIN && taskData.chapterId > this.main_chapter_id && taskData.isComplete;
					if (flag9)
					{
						this.main_chapter_id = taskData.chapterId;
					}
				}
			}
		}

		private void DispatchNpcEvent(int taskId)
		{
			Variant variant = new Variant();
			SXML node = this.TaskXML.GetNode("Task", "id==" + taskId);
			bool flag = node != null;
			if (flag)
			{
				variant["npcId"] = node.getInt("complete_npc_id");
				variant["taskId"] = taskId;
				variant["taskState"] = (int)this.GetTaskState(taskId);
				base.dispatchEvent(GameEvent.Create(A3_TaskModel.ON_NPC_TASK_STATE_CHANGE, this, variant, false));
			}
		}

		public TaskData GetTaskDataById(int taskId)
		{
			TaskData result = null;
			bool flag = this.dicPlayerTask.ContainsKey(taskId);
			if (flag)
			{
				result = this.dicPlayerTask[taskId];
			}
			return result;
		}

		public int GetTaskMaxCount(int taskId)
		{
			TaskData taskData = this.dicPlayerTask[taskId];
			int num = (int)A3_TaskModel.DAILY_TASK_LIMIT;
			switch (taskData.taskT)
			{
			case TaskType.MAIN:
			{
				int chapterId = taskData.chapterId;
				List<SXML> nodeList = this.TaskXML.GetNodeList("Task", "Chapter_id==" + chapterId);
				num = nodeList.Count;
				break;
			}
			case TaskType.BRANCH:
				num = 5;
				break;
			case TaskType.DAILY:
				num = (int)A3_TaskModel.DAILY_TASK_LIMIT;
				break;
			case TaskType.ENTRUST:
				num = (((num = XMLMgr.instance.GetSXML("task", "").GetNode("emis_limit", "").getInt("emis_limit")) == -1) ? num : 10);
				break;
			case TaskType.CLAN:
				num = (((num = XMLMgr.instance.GetSXML("task", "").GetNode("cmis_limit", "").getInt("cmis_limit")) == -1) ? num : 10);
				break;
			}
			return num;
		}

		public bool IsAcceptTask(int taskId)
		{
			return this.dicPlayerTask.ContainsKey(taskId);
		}

		public bool IsCompleteCount(int taskId)
		{
			TaskData taskData = this.dicPlayerTask[taskId];
			return taskData.taskCount >= this.GetTaskMaxCount(taskId);
		}

		public List<string> GetDialogkDesc(int taskId)
		{
			List<string> result = new List<string>();
			SXML node = this.TaskXML.GetNode("Task", "id==" + taskId);
			bool flag = node != null;
			if (flag)
			{
				bool isComplete = this.GetTaskDataById(taskId).isComplete;
				string text;
				if (isComplete)
				{
					text = node.getString("complete_dialog");
				}
				else
				{
					text = node.getString("target_dialog");
				}
				text = StringUtils.formatText(text);
				string[] source = text.Split(new char[]
				{
					';'
				});
				result = source.ToList<string>();
			}
			return result;
		}

		public string GetTaskDesc(int taskId, bool isComplete = false)
		{
			string text = string.Empty;
			SXML node = this.TaskXML.GetNode("Task", "id==" + taskId);
			bool flag = node != null;
			if (flag)
			{
				if (isComplete)
				{
					text = node.getString("complete_desc");
				}
				else
				{
					text = node.getString("target_desc");
				}
				text = StringUtils.formatText(text);
			}
			return text;
		}

		public NpcTaskState GetTaskState(int taskId)
		{
			NpcTaskState npcTaskState = NpcTaskState.NONE;
			bool flag = !this.dicPlayerTask.ContainsKey(taskId);
			NpcTaskState result;
			if (flag)
			{
				result = npcTaskState;
			}
			else
			{
				TaskData taskData = this.dicPlayerTask[taskId];
				bool flag2 = this.dicPlayerTask.ContainsKey(taskId);
				if (flag2)
				{
					bool isComplete = taskData.isComplete;
					if (isComplete)
					{
						npcTaskState = NpcTaskState.FINISHED;
					}
					else
					{
						npcTaskState = NpcTaskState.UNFINISHED;
					}
				}
				else
				{
					bool flag3 = this.dicAcceptableTask.ContainsKey(taskId);
					if (flag3)
					{
						npcTaskState = NpcTaskState.NONE;
					}
					else
					{
						npcTaskState = NpcTaskState.NONE;
					}
				}
				result = npcTaskState;
			}
			return result;
		}

		public string GetTaskTypeStr(TaskType type)
		{
			string result = string.Empty;
			switch (type)
			{
			case TaskType.MAIN:
				result = ContMgr.getCont("task_minimap_title_1", null);
				break;
			case TaskType.BRANCH:
				result = ContMgr.getCont("task_minimap_title_3", null);
				break;
			case TaskType.DAILY:
				result = ContMgr.getCont("task_minimap_title_2", null);
				break;
			case TaskType.ENTRUST:
				result = ContMgr.getCont("task_minimap_title_4", null);
				break;
			case TaskType.CLAN:
				result = ContMgr.getCont("task_minimap_title_5", null);
				break;
			}
			return result;
		}

		public uint GetRefreshStarCost()
		{
			uint result = 0u;
			SXML node = this.TaskXML.GetNode("refresh", "");
			bool flag = node != null;
			if (flag)
			{
				result = node.getUint("gold_cost");
			}
			return result;
		}

		public uint GetDoublePrizeCost()
		{
			return 100u;
		}

		public int GetMaxStarLevel()
		{
			return 5;
		}

		public int GetOneKeyFinishEveryOneCost()
		{
			int result = 0;
			SXML node = this.TaskXML.GetNode("quickfinish", "");
			bool flag = node != null;
			if (flag)
			{
				result = node.getInt("yb_cost");
			}
			return result;
		}

		public int GetDailyMaxCount()
		{
			return (int)A3_TaskModel.DAILY_TASK_LIMIT;
		}

		public int GetKillMaxCount()
		{
			return 5;
		}

		public Dictionary<uint, int> GetExtarPrizeData(int extarId)
		{
			Dictionary<uint, int> dictionary = new Dictionary<uint, int>();
			SXML node = this.TaskXML.GetNode("extra", "id==" + extarId);
			bool flag = node != null;
			if (flag)
			{
				List<SXML> nodeList = node.GetNodeList("RewardItem", "");
				bool flag2 = nodeList != null;
				if (flag2)
				{
					for (int i = 0; i < nodeList.Count; i++)
					{
						uint @uint = nodeList[i].getUint("item_id");
						int @int = nodeList[i].getInt("value");
						dictionary[@uint] = @int;
					}
				}
			}
			return dictionary;
		}

		public Dictionary<uint, int> GetChapterPrizeData(int chapterId)
		{
			Dictionary<uint, int> dictionary = new Dictionary<uint, int>();
			SXML node = this.TaskXML.GetNode("Cha_gift", "id==" + chapterId);
			bool flag = node != null;
			if (flag)
			{
				List<SXML> nodeList = node.GetNodeList("RewardEqp", "");
				bool flag2 = nodeList != null;
				if (flag2)
				{
					for (int i = 0; i < nodeList.Count; i++)
					{
						bool flag3 = nodeList[i].getInt("carr") == ModelBase<PlayerModel>.getInstance().profession;
						if (flag3)
						{
							uint @uint = nodeList[i].getUint("id");
							dictionary[@uint] = -1;
						}
					}
				}
				List<SXML> nodeList2 = node.GetNodeList("RewardItem", "");
				bool flag4 = nodeList2 != null;
				if (flag4)
				{
					for (int j = 0; j < nodeList2.Count; j++)
					{
						uint uint2 = nodeList2[j].getUint("item_id");
						int @int = nodeList2[j].getInt("value");
						dictionary[uint2] = @int;
					}
				}
			}
			return dictionary;
		}

		public Vector3 GetTaskTargetPos(int taskId)
		{
			Vector3 zero = Vector3.zero;
			SXML node = this.TaskXML.GetNode("Task", "id==" + taskId);
			bool flag = node == null;
			Vector3 result;
			if (flag)
			{
				result = zero;
			}
			else
			{
				float @float = node.getFloat("target_coordinate_x");
				float float2 = node.getFloat("target_coordinate_y");
				zero = new Vector3(@float, 0f, float2);
				result = zero;
			}
			return result;
		}

		public List<TaskRewardData> GetReward(int taskId)
		{
			List<TaskRewardData> list = new List<TaskRewardData>();
			SXML node = this.TaskXML.GetNode("Task", "id==" + taskId);
			FunctionOpenMgr.instance.onFinshedMainTask(ModelBase<A3_TaskModel>.getInstance().main_task_id, true, true);
			bool flag = node != null;
			if (flag)
			{
				List<SXML> nodeList = node.GetNodeList("RewardValue", "");
				bool flag2 = nodeList != null;
				if (flag2)
				{
					foreach (SXML current in nodeList)
					{
						list.Add(new TaskRewardData
						{
							type = 1,
							id = current.getInt("type"),
							num = current.getInt("value")
						});
					}
				}
				nodeList = node.GetNodeList("RewardEqp", "");
				bool flag3 = nodeList != null;
				if (flag3)
				{
					foreach (SXML current2 in nodeList)
					{
						a3_BagItemData a3_BagItemData = default(a3_BagItemData);
						a3_EquipData equipdata = default(a3_EquipData);
						uint @uint = current2.getUint("id");
						int @int = current2.getInt("carr");
						int int2 = current2.getInt("stage");
						int int3 = current2.getInt("intensify");
						bool isEquip = ModelBase<PlayerModel>.getInstance().profession == @int;
						a3_BagItemData.id = @uint;
						a3_BagItemData.isEquip = isEquip;
						equipdata.stage = int2;
						equipdata.intensify_lv = int3;
						a3_BagItemData.equipdata = equipdata;
						list.Add(new TaskRewardData
						{
							type = 2,
							id = (int)a3_BagItemData.id,
							item = a3_BagItemData
						});
					}
				}
				nodeList = node.GetNodeList("RewardItem", "");
				bool flag4 = nodeList != null;
				if (flag4)
				{
					foreach (SXML current3 in nodeList)
					{
						list.Add(new TaskRewardData
						{
							type = 3,
							id = current3.getInt("item_id"),
							num = current3.getInt("value")
						});
					}
				}
			}
			return list;
		}

		public List<TaskRewardData> GetClanReward(int taskCount)
		{
			List<TaskRewardData> list = new List<TaskRewardData>();
			SXML node = this.TaskXML.GetNode("clan_award", "count==" + (taskCount + 1));
			bool flag = node != null;
			if (flag)
			{
				SXML node2 = node.GetNode("lvl", "lv==" + ModelBase<A3_LegionModel>.getInstance().myLegion.lvl.ToString());
				bool flag2 = node2 != null;
				if (flag2)
				{
					bool flag3 = node2.hasValue("money");
					if (flag3)
					{
						list.Add(new TaskRewardData
						{
							type = 1,
							id = A3_TaskModel.REWARD_CLAN_MONEY,
							num = node2.getInt("money")
						});
					}
					bool flag4 = node2.hasValue("exp");
					if (flag4)
					{
						list.Add(new TaskRewardData
						{
							type = 1,
							id = A3_TaskModel.REWARD_CLAN_EXP,
							num = node2.getInt("exp")
						});
					}
				}
				List<SXML> nodeList = node.GetNodeList("RewardClan", "");
				for (int i = 0; i < nodeList.Count; i++)
				{
					TaskRewardData taskRewardData = default(TaskRewardData);
					taskRewardData.type = 1;
					taskRewardData.id = nodeList[i].getInt("type");
					taskRewardData.num = nodeList[i].getInt("value");
					bool flag5 = taskRewardData.id != A3_TaskModel.REWARD_CLAN_ACTIVE;
					if (flag5)
					{
						list.Add(taskRewardData);
					}
				}
			}
			return list;
		}

		public Dictionary<uint, int> GetClanRewardDic(int taskCount)
		{
			Dictionary<uint, int> dictionary = new Dictionary<uint, int>();
			SXML node = this.TaskXML.GetNode("clan_award", "count==" + (taskCount + 1));
			bool flag = node != null;
			if (flag)
			{
				SXML node2 = node.GetNode("lvl", "lv==" + ModelBase<A3_LegionModel>.getInstance().myLegion.lvl.ToString());
				bool flag2 = node2 != null;
				if (flag2)
				{
					bool flag3 = node2.hasValue("money");
					if (flag3)
					{
						dictionary.Add((uint)A3_TaskModel.REWARD_CLAN_MONEY, node2.getInt("money"));
					}
					bool flag4 = node2.hasValue("exp");
					if (flag4)
					{
						dictionary.Add((uint)A3_TaskModel.REWARD_CLAN_EXP, node2.getInt("exp"));
					}
				}
				List<SXML> nodeList = node.GetNodeList("RewardClan", "");
				for (int i = 0; i < nodeList.Count; i++)
				{
					bool flag5 = (ulong)nodeList[i].getUint("type") != (ulong)((long)A3_TaskModel.REWARD_CLAN_ACTIVE);
					if (flag5)
					{
						dictionary.Add(nodeList[i].getUint("type"), nodeList[i].getInt("value"));
					}
				}
			}
			return dictionary;
		}

		public Dictionary<uint, int> GetValueReward(int taskId)
		{
			Dictionary<uint, int> dictionary = null;
			SXML node = this.TaskXML.GetNode("Task", "id==" + taskId);
			bool flag = node != null;
			if (flag)
			{
				dictionary = new Dictionary<uint, int>();
				List<SXML> nodeList = node.GetNodeList("RewardValue", "");
				bool flag2 = nodeList != null;
				if (flag2)
				{
					foreach (SXML current in nodeList)
					{
						uint @uint = current.getUint("type");
						int @int = current.getInt("value");
						dictionary.Add(@uint, @int);
					}
				}
			}
			return dictionary;
		}

		public List<a3_BagItemData> GetEquipReward(int taskId)
		{
			List<a3_BagItemData> list = null;
			SXML node = this.TaskXML.GetNode("Task", "id==" + taskId);
			bool flag = node != null;
			if (flag)
			{
				list = new List<a3_BagItemData>();
				List<SXML> nodeList = node.GetNodeList("RewardEqp", "");
				bool flag2 = nodeList != null;
				if (flag2)
				{
					foreach (SXML current in nodeList)
					{
						a3_BagItemData item = default(a3_BagItemData);
						a3_EquipData equipdata = default(a3_EquipData);
						uint @uint = current.getUint("id");
						int @int = current.getInt("carr");
						int int2 = current.getInt("stage");
						int int3 = current.getInt("intensify");
						bool isEquip = ModelBase<PlayerModel>.getInstance().profession == @int;
						item.id = @uint;
						item.isEquip = isEquip;
						equipdata.stage = int2;
						equipdata.intensify_lv = int3;
						item.equipdata = equipdata;
						list.Add(item);
					}
				}
			}
			return list;
		}

		public Dictionary<uint, int> GetItemReward(int taskId)
		{
			Dictionary<uint, int> dictionary = null;
			SXML node = this.TaskXML.GetNode("Task", "id==" + taskId);
			bool flag = node != null;
			if (flag)
			{
				dictionary = new Dictionary<uint, int>();
				List<SXML> nodeList = node.GetNodeList("RewardItem", "");
				bool flag2 = nodeList != null;
				if (flag2)
				{
					foreach (SXML current in nodeList)
					{
						uint @uint = current.getUint("item_id");
						int @int = current.getInt("value");
						dictionary.Add(@uint, @int);
					}
				}
			}
			return dictionary;
		}

		public bool IfCurrentCollectItem(int collectorID)
		{
			bool result;
			foreach (TaskData current in this.dicPlayerTask.Values)
			{
				SXML node = this.TaskXML.GetNode("Task", "id==" + current.taskId);
				bool flag = node != null;
				if (flag)
				{
					int @int = node.getInt("target_param2");
					bool flag2 = @int == collectorID;
					if (flag2)
					{
						result = !current.isComplete;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public TaskType GetTaskTypeById(int taskId)
		{
			TaskType result = TaskType.NULL;
			bool flag = taskId >= 1 && taskId <= 20000;
			if (flag)
			{
				result = TaskType.MAIN;
			}
			bool flag2 = taskId >= 20001 && taskId <= 30000;
			if (flag2)
			{
				result = TaskType.DAILY;
			}
			bool flag3 = taskId >= 30001 && taskId <= 40000;
			if (flag3)
			{
				result = TaskType.BRANCH;
			}
			bool flag4 = taskId >= 50001 && taskId <= 60000;
			if (flag4)
			{
				result = TaskType.ENTRUST;
			}
			return result;
		}

		public Dictionary<int, TaskData> GetTaskDataByTaskType(TaskType type)
		{
			Dictionary<int, TaskData> dictionary = new Dictionary<int, TaskData>();
			foreach (int current in this.dicPlayerTask.Keys)
			{
				TaskType taskT = this.dicPlayerTask[current].taskT;
				bool flag = taskT == type;
				if (flag)
				{
					dictionary[current] = this.dicPlayerTask[current];
				}
			}
			return dictionary;
		}

		public TaskData GetDailyTask()
		{
			TaskData result;
			foreach (int current in this.dicPlayerTask.Keys)
			{
				TaskType taskT = this.dicPlayerTask[current].taskT;
				bool flag = taskT == TaskType.DAILY;
				if (flag)
				{
					result = this.dicPlayerTask[current];
					return result;
				}
			}
			result = null;
			return result;
		}

		public TaskData GetEntrustTask()
		{
			TaskData result;
			foreach (int current in this.dicPlayerTask.Keys)
			{
				TaskType taskT = this.dicPlayerTask[current].taskT;
				bool flag = taskT == TaskType.ENTRUST;
				if (flag)
				{
					result = this.dicPlayerTask[current];
					return result;
				}
			}
			result = null;
			return result;
		}

		public TaskData GetClanTask()
		{
			TaskData result;
			foreach (int current in this.dicPlayerTask.Keys)
			{
				TaskType taskT = this.dicPlayerTask[current].taskT;
				bool flag = taskT == TaskType.CLAN;
				if (flag)
				{
					result = this.dicPlayerTask[current];
					return result;
				}
			}
			result = null;
			return result;
		}

		private int GetMainTaskIndex(int taskId)
		{
			int result = 0;
			TaskData taskData = this.dicPlayerTask[taskId];
			int chapterId = taskData.chapterId;
			List<SXML> nodeList = this.TaskXML.GetNodeList("Task", "Chapter_id==" + chapterId);
			for (int i = 0; i < nodeList.Count; i++)
			{
				int @int = nodeList[i].getInt("id");
				bool flag = @int == taskId;
				if (flag)
				{
					result = i;
					break;
				}
			}
			return result;
		}
	}
}
