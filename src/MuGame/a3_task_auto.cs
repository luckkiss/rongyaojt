using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_task_auto
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_task_auto.<>c <>9 = new a3_task_auto.<>c();

			public static Action <>9__6_6;

			public static Action <>9__6_7;

			public static Action <>9__8_2;

			public static Action <>9__8_4;

			internal void <Execute>b__6_6()
			{
				SelfRole.fsm.StartAutoCollect();
			}

			internal void <Execute>b__6_7()
			{
				SelfRole.fsm.StartAutofight();
			}

			internal void <DealByType>b__8_2()
			{
				A3_TaskOpt expr_05 = A3_TaskOpt.Instance;
				if (expr_05 != null)
				{
					expr_05.ShowSubmitItem();
				}
			}

			internal void <DealByType>b__8_4()
			{
				A3_TaskOpt.Instance.ShowSubmitItem();
			}
		}

		public static a3_task_auto instance = new a3_task_auto();

		public bool stopAuto = false;

		public bool onTaskSearchMon = false;

		public TaskData executeTask = null;

		private int tarNpcId = 0;

		public void RunTask(TaskData taskData = null, bool checkNextStep = false, bool checkItem = false)
		{
			bool flag = this.stopAuto;
			if (!flag)
			{
				bool flag2 = NewbieModel.getInstance().curItem != null && NewbieModel.getInstance().curItem.showing;
				if (!flag2)
				{
					bool flag3 = taskData == null && ModelBase<A3_TaskModel>.getInstance().main_task_id > 0;
					if (flag3)
					{
						taskData = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(ModelBase<A3_TaskModel>.getInstance().main_task_id);
					}
					bool flag4 = taskData == null;
					if (!flag4)
					{
						ModelBase<A3_TaskModel>.getInstance().curTask = taskData;
						this.executeTask = taskData;
						bool flag5 = this.executeTask == null;
						if (!flag5)
						{
							this.Execute(this.executeTask, checkNextStep, checkItem);
						}
					}
				}
			}
		}

		private bool Execute(TaskData taskData, bool checkNextStep, bool checkItems)
		{
			a3_task_auto.<>c__DisplayClass6_0 <>c__DisplayClass6_ = new a3_task_auto.<>c__DisplayClass6_0();
			<>c__DisplayClass6_.<>4__this = this;
			bool flag = taskData.taskT == TaskType.CLAN && ModelBase<A3_LegionModel>.getInstance().myLegion.id == 0;
			bool result;
			if (flag)
			{
				flytxt.instance.fly("你现在还不是军团成员,无法进行该任务", 0, default(Color), null);
				result = false;
			}
			else
			{
				bool flag2 = !checkNextStep || taskData.next_step;
				bool flag3 = flag2;
				if (flag3)
				{
					bool autofighting = SelfRole.fsm.Autofighting;
					if (autofighting)
					{
						SelfRole.fsm.Stop();
						StateInit.Instance.Origin = Vector3.zero;
					}
					<>c__DisplayClass6_.npcId = 0;
					<>c__DisplayClass6_.mapId = 0;
					SXML sXML = XMLMgr.instance.GetSXML("task.Task", "id==" + taskData.taskId);
					bool flag4 = sXML == null;
					if (flag4)
					{
						debug.Log("任务Id错误::" + taskData.taskId);
						result = false;
					}
					else
					{
						bool flag5 = taskData.isComplete && taskData.taskT != TaskType.DAILY;
						if (flag5)
						{
							bool flag6 = sXML.getInt("complete_way") == 3;
							if (flag6)
							{
								BaseProxy<A3_TaskProxy>.getInstance().SendGetAward(0);
								result = true;
								return result;
							}
							<>c__DisplayClass6_.npcId = sXML.getInt("complete_npc_id");
							SXML sXML2 = XMLMgr.instance.GetSXML("npcs.npc", "id==" + <>c__DisplayClass6_.npcId);
							bool flag7 = sXML2 != null;
							if (flag7)
							{
								<>c__DisplayClass6_.mapId = sXML2.getInt("map_id");
							}
							List<string> listDialog = new List<string>();
							string text = sXML.getString("complete_dialog");
							text = StringUtils.formatText(text);
							string[] source = text.Split(new char[]
							{
								';'
							});
							listDialog = source.ToList<string>();
							this.tarNpcId = <>c__DisplayClass6_.npcId;
							InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
							{
								mapId = <>c__DisplayClass6_.mapId,
								check_beforeShow = true,
								handle_customized_afterTransmit = delegate
								{
									SelfRole.moveToNPc(<>c__DisplayClass6_.mapId, <>c__DisplayClass6_.npcId, listDialog, new Action(<>c__DisplayClass6_.<>4__this.OnTalkWithNpc));
								}
							}, false);
						}
						else
						{
							a3_task_auto.<>c__DisplayClass6_5 <>c__DisplayClass6_3 = new a3_task_auto.<>c__DisplayClass6_5();
							<>c__DisplayClass6_3.CS$<>8__locals2 = <>c__DisplayClass6_;
							<>c__DisplayClass6_3.CS$<>8__locals2.mapId = sXML.getInt("tasking_map_id");
							int @int = sXML.getInt("target_coordinate_x");
							int int2 = sXML.getInt("target_coordinate_y");
							<>c__DisplayClass6_3.pos = new Vector3((float)@int, 0f, (float)int2);
							TaskTargetType targetType = taskData.targetType;
							if (targetType <= TaskTargetType.FRIEND)
							{
								if (targetType != TaskTargetType.DODAILY)
								{
									if (targetType != TaskTargetType.FB)
									{
										if (targetType == TaskTargetType.FRIEND)
										{
											ArrayList arrayList = new ArrayList();
											arrayList.Add(1);
											InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, arrayList, false);
										}
									}
									else
									{
										int int3 = sXML.getInt("target_param2");
										bool flag8 = GRMap.instance.m_nCurMapID == <>c__DisplayClass6_3.CS$<>8__locals2.mapId || GameRoomMgr.getInstance().curRoom is PlotRoom;
										if (flag8)
										{
											int arg_AB7_0 = <>c__DisplayClass6_3.CS$<>8__locals2.mapId;
											Vector3 arg_AB7_1 = <>c__DisplayClass6_3.pos;
											Action arg_AB7_2;
											if ((arg_AB7_2 = a3_task_auto.<>c.<>9__6_7) == null)
											{
												arg_AB7_2 = (a3_task_auto.<>c.<>9__6_7 = new Action(a3_task_auto.<>c.<>9.<Execute>b__6_7));
											}
											SelfRole.moveto(arg_AB7_0, arg_AB7_1, arg_AB7_2, 2f);
										}
										else
										{
											Variant sendData = new Variant();
											sendData["npcid"] = 0;
											sendData["ltpid"] = int3;
											sendData["diff_lvl"] = 1;
											int int4 = sXML.getInt("level_info");
											SXML sXML3 = XMLMgr.instance.GetSXML("task.level_info", "id==" + int4);
											bool guide = sXML.getInt("guide") == 1;
											int int5 = sXML.getInt("level_yw");
											bool flag9 = int5 == 1;
											if (flag9)
											{
												MsgBoxMgr.getInstance().showTask_fb_confirm(sXML3.getString("title"), sXML3.getString("desc"), guide, ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).need_zdl, delegate
												{
													BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(sendData);
												}, null);
											}
											else
											{
												MsgBoxMgr.getInstance().showTask_fb_confirm(sXML3.getString("title"), sXML3.getString("desc"), guide, delegate
												{
													BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(sendData);
												}, null);
											}
										}
									}
								}
								else
								{
									ArrayList arrayList = new ArrayList();
									Dictionary<int, TaskData> dicTaskData = ModelBase<A3_TaskModel>.getInstance().GetDicTaskData();
									bool flag10 = false;
									int i = 0;
									List<int> list = new List<int>(dicTaskData.Keys);
									while (i < dicTaskData.Count)
									{
										bool flag11;
										flag10 = (flag11 = (dicTaskData[list[i]].taskT == TaskType.DAILY));
										if (flag11)
										{
											break;
										}
										i++;
									}
									bool flag12 = flag10;
									if (flag12)
									{
										List<int> list2 = dicTaskData.Keys.ToList<int>();
										for (i = 0; i < list2.Count; i++)
										{
											bool flag13 = dicTaskData[list2[i]].taskT == TaskType.DAILY;
											if (flag13)
											{
												arrayList.Add(dicTaskData[list2[i]].taskId);
											}
										}
										InterfaceMgr.getInstance().open(InterfaceMgr.A3_TASK, arrayList, false);
									}
								}
							}
							else if (targetType != TaskTargetType.WING)
							{
								switch (targetType)
								{
								case TaskTargetType.VISIT:
								{
									StateAutoMoveToPos.Instance.stopdistance = 0.3f;
									<>c__DisplayClass6_3.CS$<>8__locals2.npcId = sXML.getInt("target_param2");
									SXML sXML4 = XMLMgr.instance.GetSXML("npcs.npc", "id==" + <>c__DisplayClass6_3.CS$<>8__locals2.npcId);
									bool flag14 = sXML4 != null;
									if (flag14)
									{
										<>c__DisplayClass6_3.CS$<>8__locals2.mapId = sXML4.getInt("map_id");
									}
									List<string> listDialog = new List<string>();
									string text2 = sXML.getString("target_dialog");
									text2 = StringUtils.formatText(text2);
									string[] source2 = text2.Split(new char[]
									{
										';'
									});
									listDialog = source2.ToList<string>();
									this.tarNpcId = <>c__DisplayClass6_3.CS$<>8__locals2.npcId;
									bool flag15 = GRMap.instance != null;
									if (flag15)
									{
										InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
										{
											mapId = <>c__DisplayClass6_3.CS$<>8__locals2.mapId,
											check_beforeShow = true,
											handle_customized_afterTransmit = delegate
											{
												SelfRole.moveToNPc(<>c__DisplayClass6_3.CS$<>8__locals2.mapId, <>c__DisplayClass6_3.CS$<>8__locals2.npcId, listDialog, new Action(<>c__DisplayClass6_3.CS$<>8__locals2.<>4__this.OnTalkWithNpc));
											}
										}, false);
									}
									break;
								}
								case TaskTargetType.KILL:
								{
									SelfRole.UnderTaskAutoMove = true;
									this.onTaskSearchMon = (taskData.taskT == TaskType.MAIN);
									bool flag16 = ModelBase<PlayerModel>.getInstance().task_monsterId.ContainsKey(taskData.taskId);
									if (flag16)
									{
										bool flag17 = !ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.ContainsKey(taskData.taskId);
										if (flag17)
										{
											ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Add(taskData.taskId, ModelBase<PlayerModel>.getInstance().task_monsterId[taskData.taskId]);
										}
										ModelBase<PlayerModel>.getInstance().task_monsterId.Remove(taskData.taskId);
										int value = taskData.taskId;
									}
									else
									{
										int value = ModelBase<A3_TaskModel>.getInstance().GetTaskXML().GetNode("Task", "id==" + taskData.taskId).getInt("target_param2");
										ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Add(taskData.taskId, value);
									}
									SXML _taskXml = XMLMgr.instance.GetSXML("task.Task", "id==" + taskData.taskId);
									Action <>9__4;
									InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
									{
										mapId = <>c__DisplayClass6_3.CS$<>8__locals2.mapId,
										check_beforeShow = true,
										handle_customized_afterTransmit = delegate
										{
											Vector3 origin2 = new Vector3((float)_taskXml.getInt("target_coordinate_x"), 0.2f, (float)_taskXml.getInt("target_coordinate_y"));
											StateInit.Instance.Origin = origin2;
											int arg_7A_0 = <>c__DisplayClass6_3.CS$<>8__locals2.mapId;
											Vector3 arg_7A_1 = <>c__DisplayClass6_3.pos;
											Action arg_7A_2;
											if ((arg_7A_2 = <>9__4) == null)
											{
												arg_7A_2 = (<>9__4 = delegate
												{
													SelfRole.fsm.StartAutofight();
													MonsterMgr._inst.taskMonId = _taskXml.getInt("target_param2");
												});
											}
											SelfRole.moveto(arg_7A_0, arg_7A_1, arg_7A_2, 2f);
										}
									}, false);
									break;
								}
								case TaskTargetType.COLLECT:
									InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
									{
										mapId = <>c__DisplayClass6_3.CS$<>8__locals2.mapId,
										check_beforeShow = true,
										handle_customized_afterTransmit = new Action(<>c__DisplayClass6_3.<Execute>b__5)
									}, false);
									break;
								case TaskTargetType.GETEXP:
								{
									StateAutoMoveToPos.Instance.stopdistance = 0.3f;
									int num = int.Parse(sXML.getString("target_param2").Split(new char[]
									{
										','
									})[0]);
									int num2 = int.Parse(sXML.getString("target_param2").Split(new char[]
									{
										','
									})[1]);
									int num3 = int.Parse(sXML.getString("trigger"));
									bool flag18 = num3 == 1;
									if (flag18)
									{
										InterfaceMgr.getInstance().open(InterfaceMgr.A3_WANTLVUP, null, false);
									}
									else
									{
										bool flag19 = true;
										int profession = ModelBase<PlayerModel>.getInstance().profession;
										uint lvl = ModelBase<PlayerModel>.getInstance().lvl;
										uint up_lvl = ModelBase<PlayerModel>.getInstance().up_lvl;
										uint exp = ModelBase<PlayerModel>.getInstance().exp;
										uint needExpByCurrentZhuan = ModelBase<ResetLvLModel>.getInstance().getNeedExpByCurrentZhuan(profession, up_lvl);
										uint needLvLByCurrentZhuan = ModelBase<ResetLvLModel>.getInstance().getNeedLvLByCurrentZhuan(profession, up_lvl);
										bool flag20 = up_lvl >= 10u;
										if (!flag20)
										{
											bool flag21 = needLvLByCurrentZhuan > lvl;
											if (flag21)
											{
												flag19 = false;
											}
											bool flag22 = flag19;
											if (flag22)
											{
												<>c__DisplayClass6_3.CS$<>8__locals2.npcId = XMLMgr.instance.GetSXML("task.zhuan_npc", "").getInt("id");
												SXML sXML5 = XMLMgr.instance.GetSXML("npcs.npc", "id==" + <>c__DisplayClass6_3.CS$<>8__locals2.npcId);
												bool flag23 = sXML5 != null;
												if (flag23)
												{
													<>c__DisplayClass6_3.CS$<>8__locals2.mapId = sXML5.getInt("map_id");
												}
												List<string> listDialog = new List<string>();
												string text3 = sXML.getString("target_dialog");
												text3 = StringUtils.formatText(text3);
												string[] source3 = text3.Split(new char[]
												{
													';'
												});
												listDialog = source3.ToList<string>();
												bool flag24 = GRMap.instance != null;
												if (flag24)
												{
													InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
													{
														mapId = <>c__DisplayClass6_3.CS$<>8__locals2.mapId,
														check_beforeShow = true,
														handle_customized_afterTransmit = delegate
														{
															SelfRole.moveToNPc(<>c__DisplayClass6_3.CS$<>8__locals2.mapId, <>c__DisplayClass6_3.CS$<>8__locals2.npcId, listDialog, new Action(<>c__DisplayClass6_3.CS$<>8__locals2.<>4__this.OnTalkWithNpc));
														}
													}, false);
												}
											}
											else
											{
												InterfaceMgr.getInstance().open(InterfaceMgr.A3_WANTLVUP, null, false);
											}
										}
									}
									break;
								}
								default:
									switch (targetType)
									{
									case TaskTargetType.GET_ITEM_GIVEN:
									case TaskTargetType.KILL_MONSTER_GIVEN:
									case TaskTargetType.WAIT_POINT_GIVEN:
										this.DealByType(taskData, checkItems);
										break;
									}
									break;
								}
							}
							else
							{
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_WIBG_SKIN, null, false);
							}
							SXML sXML6 = XMLMgr.instance.GetSXML("task.Task", "id==" + taskData.taskId);
							bool flag25 = SelfRole.UnderTaskAutoMove = (taskData.targetType == TaskTargetType.KILL);
							if (flag25)
							{
								StateAutoMoveToPos.Instance.stopdistance = 2f;
								Vector3 origin = new Vector3((float)sXML6.getInt("target_coordinate_x"), 0f, (float)sXML6.getInt("target_coordinate_y"));
								StateInit.Instance.Origin = origin;
							}
							bool flag26 = taskData.targetType == TaskTargetType.KILL || taskData.targetType == TaskTargetType.DODAILY;
							if (flag26)
							{
								int int6 = sXML6.getInt("target_param2");
								bool flag27 = int6 != -1;
								if (flag27)
								{
									MonsterMgr._inst.taskMonId = int6;
								}
							}
						}
						result = true;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		private void OnTalkWithNpc()
		{
		}

		private void DealByType(TaskData taskData, bool checkItems)
		{
			A3_TaskOpt.Instance.ResetStat();
			TaskTargetType targetType = taskData.targetType;
			TaskType taskT = taskData.taskT;
			bool flag = A3_TaskOpt.Instance == null;
			if (flag)
			{
				Debug.LogError("请将A3_TaskOpt预制件默认设为Active");
			}
			else
			{
				bool flag2 = !A3_TaskOpt.Instance.taskOptElement.ContainsKey(taskData.taskId);
				if (flag2)
				{
					A3_TaskOpt.Instance.taskOptElement[taskData.taskId] = new TaskOptElement(taskData.taskId, null, null);
				}
				A3_TaskOpt.Instance.curTaskId = taskData.taskId;
				bool flag3 = targetType == TaskTargetType.WAIT_POINT_GIVEN;
				if (flag3)
				{
					Vector3 waitPosition = Vector3.zero;
					SXML pointInfo = ModelBase<A3_TaskModel>.getInstance().GetTaskXML().GetNode("Task", "id==" + taskData.taskId);
					A3_TaskOpt.Instance.LockStat = false;
					bool isWaiting = A3_TaskOpt.Instance.isWaiting;
					if (isWaiting)
					{
						A3_TaskOpt.Instance.StopCD(false);
					}
					A3_TaskOpt.Instance.BtnWait.interactable = true;
					A3_TaskOpt.Instance.waitPosition = new Vector3(pointInfo.getFloat("target_coordinate_x"), 0f, pointInfo.getFloat("target_coordinate_y"));
					waitPosition = A3_TaskOpt.Instance.waitPosition;
					A3_TaskOpt.Instance.actionImage.sprite = (Resources.Load("icon/task_action/" + pointInfo.getInt("act_icon"), typeof(Sprite)) as Sprite);
					A3_TaskOpt.Instance.transform.FindChild("wait/action_text").GetComponent<Text>().text = pointInfo.getString("act_name");
					ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack[taskData.taskId] = pointInfo.getInt("target_param2");
					InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
					{
						mapId = pointInfo.getInt("tasking_map_id"),
						check_beforeShow = true,
						handle_customized_afterTransmit = delegate
						{
							SelfRole.WalkToMap(pointInfo.getInt("tasking_map_id"), waitPosition, null, 0.3f);
						},
						targetPosition = waitPosition
					}, false);
				}
				else
				{
					bool flag4 = targetType == TaskTargetType.KILL_MONSTER_GIVEN;
					if (flag4)
					{
						A3_TaskOpt.Instance.IsOnKillMon = true;
						Vector3 waitPosition = Vector3.zero;
						SXML monInfo = ModelBase<A3_TaskModel>.getInstance().GetTaskXML().GetNode("Task", "id==" + taskData.taskId);
						bool flag5 = monInfo != null;
						if (flag5)
						{
							A3_TaskOpt.Instance.LockStat = false;
							ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Add(taskData.taskId, monInfo.getInt("target_param2"));
							A3_TaskOpt.Instance.killPosition = new Vector3(monInfo.getFloat("target_coordinate_x"), 0f, monInfo.getFloat("target_coordinate_y"));
							waitPosition = A3_TaskOpt.Instance.killPosition;
						}
						InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
						{
							mapId = monInfo.getInt("tasking_map_id"),
							check_beforeShow = true,
							handle_customized_afterTransmit = delegate
							{
								SelfRole.WalkToMap(monInfo.getInt("tasking_map_id"), waitPosition, null, 0.3f);
							},
							targetPosition = waitPosition
						}, false);
					}
					else
					{
						bool flag6 = targetType == TaskTargetType.GET_ITEM_GIVEN;
						if (flag6)
						{
							NpcShopData npcShopData = null;
							SXML node = ModelBase<A3_TaskModel>.getInstance().GetTaskXML().GetNode("Task", "id==" + taskData.taskId);
							A3_TaskOpt.Instance.taskItemId = node.getUint("target_param2");
							int num = taskData.completion - taskData.taskRate;
							int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(A3_TaskOpt.Instance.taskItemId);
							Action arg_46B_0;
							if ((arg_46B_0 = a3_task_auto.<>c.<>9__8_2) == null)
							{
								a3_task_auto.<>c.<>9__8_2 = new Action(a3_task_auto.<>c.<>9.<DealByType>b__8_2);
							}
							npcShopData = ModelBase<A3_NPCShopModel>.getInstance().GetDataByItemId(A3_TaskOpt.Instance.taskItemId);
							bool flag7 = itemNumByTpid >= num;
							if (flag7)
							{
								int @int = node.getInt("complete_npc_id");
								Vector3 npcPos = NpcMgr.instance.getRole(@int).transform.position;
								int mapId = node.getInt("tasking_map_id");
								InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
								{
									mapId = mapId,
									check_beforeShow = true,
									handle_customized_afterTransmit = delegate
									{
										int arg_31_0 = mapId;
										Vector3 arg_31_1 = npcPos;
										Action arg_31_2;
										if ((arg_31_2 = a3_task_auto.<>c.<>9__8_4) == null)
										{
											arg_31_2 = (a3_task_auto.<>c.<>9__8_4 = new Action(a3_task_auto.<>c.<>9.<DealByType>b__8_4));
										}
										SelfRole.WalkToMap(arg_31_0, arg_31_1, arg_31_2, 2f);
									}
								}, false);
							}
							else
							{
								bool flag8 = npcShopData != null;
								if (flag8)
								{
									InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
									{
										mapId = npcShopData.mapId,
										check_beforeShow = true,
										handle_customized_afterTransmit = delegate
										{
											SelfRole.moveToNPc(npcShopData.mapId, npcShopData.npc_id, null, null);
										}
									}, false);
								}
								else if (checkItems)
								{
									ArrayList arrayList = new ArrayList();
									arrayList.Add(ModelBase<a3_BagModel>.getInstance().getItemDataById(A3_TaskOpt.Instance.taskItemId));
									InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMLACK, arrayList, false);
								}
							}
						}
					}
				}
			}
		}

		public void PauseAutoKill(int taskId = -1)
		{
			bool flag = taskId <= 0;
			if (flag)
			{
				taskId = ModelBase<A3_TaskModel>.getInstance().main_task_id;
			}
			Dictionary<int, int> task_monsterId = ModelBase<PlayerModel>.getInstance().task_monsterId;
			Dictionary<int, int> task_monsterIdOnAttack = ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack;
			bool flag2 = !task_monsterId.ContainsKey(taskId) && task_monsterIdOnAttack.ContainsKey(taskId);
			if (flag2)
			{
				task_monsterId.Add(taskId, task_monsterIdOnAttack[taskId]);
				task_monsterIdOnAttack.Remove(taskId);
			}
		}
	}
}
