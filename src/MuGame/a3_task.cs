using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_task : Window
	{
		private Transform conTabBtn;

		private Transform conTask;

		public GameObject tabClan;

		public GameObject tabEntrust;

		private GameObject transMain;

		private GameObject transDaily;

		private GameObject transBranch;

		private GameObject transEmpty;

		private GameObject transBranchEmpty;

		private GameObject transEntrust;

		private GameObject transClanEmpty;

		private GameObject transClanTask;

		private GameObject branchBtn;

		private GameObject dailyBtn;

		private Transform container;

		private BaseButton btnClose;

		private BranchMissionText bmis;

		private A3_TaskModel tkModel;

		private Dictionary<string, TaskData> bmisDic;

		private Dictionary<string, BranchMissionObj> bmisGoDic;

		public static a3_task instance;

		public ChapterInfos mainChapterInfo;

		private List<GameObject> listSubBranch;

		private TabControl tc;

		private GameObject currentObj;

		private GameObject branchPage;

		public static int openwin = 0;

		private bool Toclose = false;

		private TaskType curTaskType;

		private Dictionary<int, GameObject> dicTaskPage = new Dictionary<int, GameObject>();

		private GameObject pageTemp;

		private Text btntextOneKeyFinish;

		private int starLevel = 0;

		private int lastAvatarId = -1;

		private GameObject monsterAvatar = null;

		private GameObject monsterCamera = null;

		public override void init()
		{
			this.tkModel = ModelBase<A3_TaskModel>.getInstance();
			this.conTabBtn = base.getTransformByPath("con_tab");
			this.conTask = base.getTransformByPath("con_task/container");
			this.transMain = base.getGameObjectByPath("con_task/mainTemp");
			this.transDaily = base.getGameObjectByPath("con_task/dailyTemp");
			this.transBranch = base.getGameObjectByPath("con_task/branchTemp");
			this.transEmpty = base.getGameObjectByPath("con_task/emptyTemp");
			this.transClanEmpty = base.getGameObjectByPath("con_task/emptyClanTemp");
			this.transBranchEmpty = base.getGameObjectByPath("con_task/emptyBranchTemp");
			this.transEntrust = base.getGameObjectByPath("con_task/entrustTemp");
			this.transClanTask = base.getGameObjectByPath("con_task/clanTaskTemp");
			this.btnClose = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			this.btnClose.onClick = new Action<GameObject>(this.OnCloseClick);
			this.InitConTabBtn();
			this.CheckLock();
			this.listSubBranch = new List<GameObject>();
			this.bmisDic = new Dictionary<string, TaskData>();
			this.bmisGoDic = new Dictionary<string, BranchMissionObj>();
			a3_task.instance = this;
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(34u, delegate(GameEvent e)
			{
				this.tabClan.SetActive(true);
			});
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(2u, delegate(GameEvent e)
			{
				this.tabClan.SetActive(true);
			});
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(19u, delegate(GameEvent e)
			{
				this.tabClan.SetActive(true);
			});
		}

		private void InitConTabBtn()
		{
			GameObject gameObjectByPath = base.getGameObjectByPath("con_tab/btnTemp");
			this.container = base.getTransformByPath("con_tab/view/con");
			List<TaskType> list = new List<TaskType>
			{
				TaskType.MAIN,
				TaskType.BRANCH,
				TaskType.DAILY,
				TaskType.ENTRUST,
				TaskType.CLAN
			};
			for (int i = 0; i < list.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(gameObjectByPath);
				gameObject.gameObject.SetActive(true);
				Text component = gameObject.transform.FindChild("Text").GetComponent<Text>();
				string text = string.Empty;
				switch (list[i])
				{
				case TaskType.MAIN:
					text = ContMgr.getCont("task_btn_name_1", null);
					break;
				case TaskType.BRANCH:
					text = ContMgr.getCont("task_btn_name_3", null);
					break;
				case TaskType.DAILY:
					text = ContMgr.getCont("task_btn_name_2", null);
					this.dailyBtn = gameObject;
					break;
				case TaskType.ENTRUST:
					text = ContMgr.getCont("task_btn_name_4", null);
					this.tabEntrust = gameObject;
					this.tabEntrust.SetActive(ModelBase<A3_TaskModel>.getInstance().GetEntrustTask() != null);
					break;
				case TaskType.CLAN:
				{
					text = ContMgr.getCont("task_btn_name_5", null);
					this.tabClan = gameObject;
					bool flag = ModelBase<A3_LegionModel>.getInstance().myLegion.id == 0;
					if (flag)
					{
						this.tabClan.SetActive(ModelBase<A3_TaskModel>.getInstance().GetClanTask() != null);
					}
					break;
				}
				}
				component.text = text;
				gameObject.name = ((int)list[i]).ToString();
				gameObject.transform.SetParent(this.container, false);
			}
			this.tc = new TabControl();
			this.tc.create(this.container.gameObject, base.gameObject, 0, 0, false);
			this.tc.onClickHanle = delegate(TabControl tb)
			{
				int seletedIndex = tb.getSeletedIndex();
				this.OnShowCurTaskTable(seletedIndex + TaskType.MAIN);
			};
		}

		private void ShowSubBranchBtn()
		{
			Dictionary<int, TaskData> dicTaskData = ModelBase<A3_TaskModel>.getInstance().GetDicTaskData();
			Dictionary<int, TaskData> dictionary = new Dictionary<int, TaskData>();
			foreach (KeyValuePair<int, TaskData> current in dicTaskData)
			{
				bool flag = current.Value.taskT == TaskType.BRANCH;
				if (flag)
				{
					dictionary.Add(current.Key, current.Value);
				}
			}
			Transform transform = base.transform.FindChild("con_tab/view/con/2");
			bool flag2 = dictionary.Count == 0;
			if (flag2)
			{
				transform.gameObject.SetActive(false);
			}
			else
			{
				GameObject gameObjectByPath = base.getGameObjectByPath("con_tab/SubBranchBtn");
				Transform transformByPath = base.getTransformByPath("con_tab/view/con");
				for (int i = 0; i < this.listSubBranch.Count; i++)
				{
					UnityEngine.Object.DestroyImmediate(this.listSubBranch[i]);
				}
				this.listSubBranch.Clear();
				List<int> list = dictionary.Keys.ToList<int>();
				this.bmisDic.Clear();
				for (int i = 0; i < list.Count; i++)
				{
					GameObject subBranchBtn = UnityEngine.Object.Instantiate<GameObject>(gameObjectByPath);
					subBranchBtn.transform.SetParent(transformByPath, false);
					subBranchBtn.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
					transform = subBranchBtn.transform;
					TaskData task = dictionary[list[i]];
					subBranchBtn.transform.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isOn)
					{
						if (isOn)
						{
							BranchMissionObj branchMissionObj = default(BranchMissionObj);
							for (int j = 0; j < this.conTask.childCount; j++)
							{
								this.conTask.GetChild(j).gameObject.SetActive(false);
							}
							subBranchBtn.transform.GetChild(0).gameObject.SetActive(true);
							subBranchBtn.transform.GetChild(1).gameObject.SetActive(false);
							bool flag5 = !a3_task.instance.bmisGoDic.ContainsKey(subBranchBtn.transform.name);
							if (flag5)
							{
								Transform expr_C9 = this.conTask.FindChild("branchPage");
								UnityEngine.Object.Destroy((expr_C9 != null) ? expr_C9.gameObject : null);
								GameObject gameObject = this.OnCreateBanchPage(this.transBranch, this.bmisDic[subBranchBtn.transform.name]);
								gameObject.name = "branchPage";
								this.bmisGoDic.Add(subBranchBtn.transform.name, branchMissionObj = new BranchMissionObj
								{
									panel = gameObject,
									btnGo = gameObject.transform.FindChild("0/btnGo").gameObject
								});
								(this.currentObj = gameObject).transform.SetParent(this.conTask);
								GameObject expr_1A1 = branchMissionObj.btnGo;
								if (expr_1A1 != null)
								{
									Button expr_1AC = expr_1A1.GetComponent<Button>();
									if (expr_1AC != null)
									{
										expr_1AC.onClick.AddListener(delegate
										{
											worldmap.Desmapimg();
											SelfRole.fsm.Stop();
											a3_task_auto.instance.RunTask(task, false, false);
											InterfaceMgr.getInstance().close(InterfaceMgr.A3_TASK);
										});
									}
								}
							}
							else
							{
								(this.currentObj = this.bmisGoDic[subBranchBtn.transform.name].panel).SetActive(true);
							}
						}
						else
						{
							subBranchBtn.transform.GetChild(0).gameObject.SetActive(false);
							subBranchBtn.transform.GetChild(1).gameObject.SetActive(true);
							bool flag6 = a3_task.instance.bmisGoDic.ContainsKey(subBranchBtn.transform.name);
							if (flag6)
							{
								UnityEngine.Object.Destroy(this.bmisGoDic[subBranchBtn.transform.name].panel);
								this.bmisGoDic.Remove(subBranchBtn.transform.name);
							}
						}
					});
					subBranchBtn.transform.name = list[i].ToString();
					this.bmisDic.Add(subBranchBtn.transform.name, dictionary[list[i]]);
					subBranchBtn.SetActive(true);
					this.listSubBranch.Add(subBranchBtn);
					Text componentInChildren = subBranchBtn.transform.GetComponentInChildren<Text>();
					componentInChildren.text = dictionary[list[i]].taskName;
				}
				bool flag3 = this.dailyBtn != null && this.dailyBtn.activeSelf;
				if (flag3)
				{
					this.dailyBtn.transform.SetAsLastSibling();
				}
				bool flag4 = !this.listSubBranch[0].transform.GetComponent<Toggle>().isOn;
				if (flag4)
				{
					this.listSubBranch[0].transform.GetComponent<Toggle>().isOn = true;
					this.bmisGoDic[this.listSubBranch[0].name].panel.SetActive(true);
					this.bmisDic[this.listSubBranch[0].name] = dictionary[int.Parse(this.listSubBranch[0].name)];
				}
			}
		}

		public void CheckLock()
		{
			Transform transform = base.transform.FindChild("con_tab/view/con/" + 3.ToString());
			bool flag = transform;
			if (flag)
			{
				transform.gameObject.SetActive(false);
			}
			bool flag2 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.DAILY_TASK, false);
			if (flag2)
			{
				this.OpenDailyTask();
			}
		}

		public void OpenDailyTask()
		{
			Transform transform = base.transform.FindChild("con_tab/view/con/" + 3.ToString());
			bool flag = transform;
			if (flag)
			{
				transform.gameObject.SetActive(true);
			}
		}

		public override void onShowed()
		{
			this.Toclose = false;
			base.onShowed();
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(3u, new Action<GameEvent>(this.OnRefreshTask));
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.OnSubmitTask));
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.OnAddNewTask));
			this.OnShowTaskPanel();
			this.setWin();
			GRMap.GAME_CAMERA.SetActive(false);
		}

		private void setWin()
		{
			bool flag = a3_task.openwin == 0;
			if (!flag)
			{
				this.tc.setSelectedIndex(this.tc.getIndexByName(a3_task.openwin.ToString()), true);
				a3_task.openwin = 0;
			}
		}

		public override void onClosed()
		{
			base.onClosed();
			BaseProxy<A3_TaskProxy>.getInstance().removeEventListener(3u, new Action<GameEvent>(this.OnRefreshTask));
			BaseProxy<A3_TaskProxy>.getInstance().removeEventListener(1u, new Action<GameEvent>(this.OnSubmitTask));
			BaseProxy<A3_TaskProxy>.getInstance().removeEventListener(2u, new Action<GameEvent>(this.OnAddNewTask));
			this.ResetTaskType();
			GRMap.GAME_CAMERA.SetActive(true);
			InterfaceMgr.getInstance().itemToWin(this.Toclose, this.uiName);
		}

		private void OnAddNewTask(GameEvent e)
		{
			List<TaskData> listAddTask = this.tkModel.listAddTask;
			foreach (TaskData current in listAddTask)
			{
				this.OnShowTaskPage(current);
			}
		}

		private void OnTaskRateRefresh(TaskData data)
		{
			bool flag = this.dicTaskPage.ContainsKey(data.taskId);
			if (flag)
			{
				GameObject page = this.dicTaskPage[data.taskId];
				switch (data.taskT)
				{
				case TaskType.MAIN:
					this.OnMainTaskDataChange(page, data);
					break;
				case TaskType.DAILY:
					this.OnDailyTaskDataChange(page, data);
					break;
				}
			}
		}

		private void OnSubmitTask(GameEvent e)
		{
			int key = e.data["id"];
			bool flag = this.dicTaskPage.ContainsKey(key);
			if (flag)
			{
				UnityEngine.Object.Destroy(this.dicTaskPage[key]);
				this.dicTaskPage.Remove(key);
			}
		}

		private void ResetTaskType()
		{
			this.curTaskType = TaskType.NULL;
			this.DisposeAvatar();
			this.DisposeCamera();
		}

		private void OnRefreshTask(GameEvent e)
		{
			Variant data = e.data;
			List<Variant> arr = data["change_task"]._arr;
			foreach (Variant current in arr)
			{
				int num = current["id"];
				TaskData taskDataById = this.tkModel.GetTaskDataById(num);
				Transform child = this.dicTaskPage[num].transform.GetChild(1);
				this.OnTaskRateRefresh(taskDataById);
			}
		}

		private void OnShowTaskPanel()
		{
			int num = 0;
			bool flag = this.uiData != null;
			if (flag)
			{
				num = (int)this.uiData[0];
			}
			TaskType taskType = TaskType.MAIN;
			bool flag2 = num != 0;
			if (flag2)
			{
				taskType = this.tkModel.GetTaskDataById(num).taskT;
			}
			TabControl arg_5E_0 = this.tc;
			TabControl arg_58_0 = this.tc;
			int num2 = (int)taskType;
			arg_5E_0.setSelectedIndex(arg_58_0.getIndexByName(num2.ToString()), true);
		}

		private void OnTaskSwichClick(GameObject go)
		{
			int type = int.Parse(go.name);
			this.OnShowCurTaskTable((TaskType)type);
		}

		private void OnShowCurTaskTable(TaskType type)
		{
			bool flag = type == this.curTaskType || type == TaskType.NULL;
			if (!flag)
			{
				this.curTaskType = type;
				this.OnCurTaskTypeChange();
				Dictionary<int, TaskData> dictionary = new Dictionary<int, TaskData>();
				dictionary = this.tkModel.GetTaskDataByTaskType(type);
				this.ShowSubBranchBtn();
				bool flag2 = type == TaskType.BRANCH;
				if (flag2)
				{
					this.ShowSubBranchBtn();
					bool flag3 = this.bmisGoDic.ContainsKey("0");
					if (flag3)
					{
						this.bmisGoDic["0"].panel.SetActive(true);
					}
				}
				else
				{
					bool flag4 = this.currentObj != null;
					if (flag4)
					{
						this.currentObj.SetActive(false);
					}
					for (int i = 0; i < this.listSubBranch.Count; i++)
					{
						UnityEngine.Object.DestroyImmediate(this.listSubBranch[i]);
					}
					this.bmisGoDic.Clear();
					bool flag5 = dictionary.Count > 0;
					if (flag5)
					{
						foreach (TaskData current in dictionary.Values)
						{
							this.OnShowTaskPage(current);
						}
					}
					else
					{
						this.OnShowTaskPage(new TaskData
						{
							taskId = 0,
							taskT = TaskType.NULL
						});
					}
				}
			}
		}

		private void OnShowTaskPage(TaskData data)
		{
			this.tkModel.curTask = data;
			int taskId = data.taskId;
			bool flag = this.dicTaskPage.ContainsKey(taskId);
			if (flag)
			{
				foreach (int current in this.dicTaskPage.Keys)
				{
					bool flag2 = current == taskId;
					this.dicTaskPage[current].SetActive(flag2);
					bool flag3 = flag2;
					if (flag3)
					{
						this.OnTaskRateRefresh(data);
					}
				}
			}
			else
			{
				foreach (int current2 in this.dicTaskPage.Keys)
				{
					this.dicTaskPage[current2].SetActive(false);
				}
				switch (data.taskT)
				{
				case TaskType.NULL:
				{
					bool flag4 = this.curTaskType == TaskType.BRANCH;
					if (flag4)
					{
						this.pageTemp = this.OnCreateEmptyPage(this.transBranchEmpty, data);
					}
					else
					{
						bool flag5 = this.curTaskType == TaskType.CLAN;
						if (flag5)
						{
							this.pageTemp = this.OnCreateEmptyPage(this.transClanEmpty, data);
						}
						else
						{
							this.pageTemp = this.OnCreateEmptyPage(this.transEmpty, data);
						}
					}
					break;
				}
				case TaskType.MAIN:
					this.pageTemp = this.OnCreateMainPage(this.transMain, data);
					break;
				case TaskType.DAILY:
					this.pageTemp = this.OnCreateDailyPage(this.transDaily, data);
					break;
				case TaskType.ENTRUST:
					this.pageTemp = this.OnCreateEntrustPage(this.transEntrust, data);
					break;
				case TaskType.CLAN:
					this.pageTemp = this.OnCreateClanTaskPage(this.transClanTask, data);
					break;
				}
				this.pageTemp.SetActive(true);
				this.dicTaskPage[taskId] = this.pageTemp;
			}
		}

		private void OnCurTaskTypeChange()
		{
		}

		private GameObject OnCreateEmptyPage(GameObject pageTemp, TaskData data)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(pageTemp);
			gameObject.transform.SetParent(this.conTask, false);
			return gameObject;
		}

		private GameObject OnCreateMainPage(GameObject pageTemp, TaskData data)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(pageTemp);
			Transform child = gameObject.transform.GetChild(0);
			Transform child2 = gameObject.transform.GetChild(1);
			this.OnTaskDescChange(child, data);
			this.OnTaskCountChange(child, data);
			this.OnTaskRateChange(child2, data);
			this.OnTaskStateChange(child2, data);
			this.ShowPanelTaskReward(child2, data);
			this.OnTaskNameChange(child2, data);
			this.ShowChapterPrize(child, data);
			gameObject.transform.SetParent(this.conTask, false);
			Transform trans = child2.FindChild("btn_move");
			this.InitMoveBtn(trans);
			return gameObject;
		}

		private void OnMainTaskDataChange(GameObject page, TaskData data)
		{
			Transform child = page.transform.GetChild(0);
			Transform child2 = page.transform.GetChild(1);
			this.OnTaskCountChange(child, data);
			this.OnTaskRateChange(child2, data);
			this.OnTaskStateChange(child2, data);
		}

		private GameObject OnCreateDailyPage(GameObject pageTemp, TaskData data)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(pageTemp);
			Transform child = gameObject.transform.GetChild(0);
			Transform child2 = gameObject.transform.GetChild(1);
			int taskId = data.taskId;
			this.OnTaskCountChange(child, data);
			this.OnTaskRateChange(child2, data);
			this.OnTaskStateChange(child2, data);
			this.OnTaskNameChange(child2, data);
			this.OnStarInfoChange(child2, data);
			this.OnOneKeyFinishCostChange(child, data);
			this.OnPrizeAndMoveBtnChange(child2, data);
			this.ShowDailyExtraPrize(child, data);
			this.ShowPanelTaskReward(child2, data);
			Transform trans = child2.FindChild("btn_move");
			this.InitMoveBtn(trans);
			Transform trans2 = child2.FindChild("star/btn_oneKey");
			this.InitOnkeUpgradeStar(trans2);
			Transform trans3 = child2.FindChild("get_reward/1");
			Transform trans4 = child2.FindChild("get_reward/2");
			this.InitGetPrizeBtn(trans3);
			this.InitGetPrizeBtn(trans4);
			Transform trans5 = child.FindChild("btn_onekey");
			this.InitOneKeyFinishTask(trans5);
			gameObject.transform.SetParent(this.conTask, false);
			child.FindChild("reward/state").GetComponent<Text>().text = ContMgr.getCont("daily_limit_tip_1", new string[]
			{
				A3_TaskModel.DAILY_TASK_LIMIT.ToString()
			});
			return gameObject;
		}

		private void OnRefreshOneKeyFinishBtnCost()
		{
			this.btntextOneKeyFinish.text = Mathf.Max(this.tkModel.GetOneKeyFinishEveryOneCost() * (this.tkModel.GetDailyMaxCount() - this.tkModel.GetTaskDataByTaskType(TaskType.DAILY).Count), 0).ToString();
		}

		private void OnStarInfoChange(Transform conState, TaskData data)
		{
			Transform transform = conState.FindChild("star/con_star");
			this.starLevel = data.taskStar;
			for (int i = 0; i < transform.childCount; i++)
			{
				bool flag = i < this.starLevel;
				if (flag)
				{
					transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
					transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
				}
				else
				{
					transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
					transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
				}
			}
			Text component = conState.FindChild("star/Text_cost").GetComponent<Text>();
			component.text = Globle.getBigText(this.tkModel.GetRefreshStarCost());
			this.ShowPanelTaskReward(conState, data);
		}

		private void OnOneKeyFinishCostChange(Transform conDesc, TaskData data)
		{
			Text component = conDesc.FindChild("btn_onekey/cost").GetComponent<Text>();
			int dailyMaxCount = this.tkModel.GetDailyMaxCount();
			int num = dailyMaxCount - data.taskCount;
			int oneKeyFinishEveryOneCost = this.tkModel.GetOneKeyFinishEveryOneCost();
			int num2 = oneKeyFinishEveryOneCost * num;
			component.text = Globle.getBigText((uint)num2);
			uint gold = ModelBase<PlayerModel>.getInstance().gold;
			bool flag = (ulong)gold < (ulong)((long)num2);
			if (flag)
			{
				component.color = Globle.getColorByQuality(7);
			}
			else
			{
				component.color = Globle.getColorByQuality(2);
			}
		}

		private void ShowPanelTaskReward(Transform conDesc, TaskData data)
		{
			List<TaskRewardData> list = (data.taskT == TaskType.CLAN) ? this.tkModel.GetClanReward(data.taskCount) : this.tkModel.GetReward(data.taskId);
			GameObject gameObject = conDesc.FindChild("reward/view/icon_bg").gameObject;
			Transform transform = conDesc.FindChild("reward/view/con");
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject gameObject2 = transform.GetChild(i).gameObject;
				UnityEngine.Object.Destroy(gameObject2);
			}
			foreach (TaskRewardData current in list)
			{
				uint tpid = 0u;
				int num = current.num;
				switch (current.type)
				{
				case 1:
					tpid = 3002u;
					break;
				case 2:
				{
					bool flag = !current.item.isEquip;
					if (flag)
					{
						continue;
					}
					tpid = (uint)current.id;
					num = -1;
					break;
				}
				case 3:
					tpid = (uint)current.id;
					break;
				}
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid);
				bool flag2 = current.type == 1;
				if (flag2)
				{
					bool flag3 = data.taskT == TaskType.CLAN;
					if (flag3)
					{
						itemDataById.file = "icon/comm/1x" + current.id;
					}
					else
					{
						itemDataById.file = "icon/comm/0x" + current.id;
					}
				}
				bool flag4 = num != -1 && data.taskT == TaskType.DAILY && this.starLevel != 0;
				if (flag4)
				{
					num *= this.starLevel;
				}
				GameObject gameObject3 = IconImageMgr.getInstance().createA3ItemIcon(itemDataById, false, num, 0.8f, false, -1, 0, false, false, false, -1, false, false);
				GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject4.transform.SetParent(gameObject3.transform);
				gameObject4.transform.SetAsFirstSibling();
				gameObject4.transform.localScale = Vector3.one;
				gameObject4.transform.localPosition = Vector3.zero;
				gameObject4.GetComponent<RectTransform>().sizeDelta /= 0.8f;
				gameObject4.SetActive(true);
				Transform transform2 = gameObject3.transform.FindChild("iconborder/equip_canequip");
				bool flag5 = transform2 != null;
				if (flag5)
				{
					transform2.gameObject.SetActive(false);
				}
				gameObject3.transform.SetParent(transform.transform, false);
				gameObject3.SetActive(true);
				gameObject3.transform.FindChild("iconborder").GetComponent<RectTransform>().sizeDelta = new Vector2(78f, 78f);
			}
		}

		private void ShowDailyExtraPrize(Transform conDesc, TaskData data)
		{
			int extraAward = data.extraAward;
			Dictionary<uint, int> extarPrizeData = this.tkModel.GetExtarPrizeData(extraAward);
			Transform transform = conDesc.FindChild("reward/view/con");
			this.OnCreatePrizeIcon(transform, extarPrizeData, 0.8f);
		}

		private void ShowChapterPrize(Transform conDesc, TaskData data)
		{
			int chapterId = data.chapterId;
			Dictionary<uint, int> chapterPrizeData = this.tkModel.GetChapterPrizeData(chapterId);
			Transform transform = conDesc.FindChild("reward/view/con");
			this.OnCreatePrizeIcon(transform, chapterPrizeData, 0.8f);
		}

		private void ShowDailyTaskPrize(Transform conState, TaskData data)
		{
			Dictionary<uint, int> valueReward = this.tkModel.GetValueReward(data.taskId);
			bool flag = valueReward == null;
			if (!flag)
			{
				Transform transform = conState.FindChild("reward/view/con");
				this.OnCreatePrizeIcon(transform, valueReward, 0.8f);
			}
		}

		private void OnDailyTaskDataChange(GameObject page, TaskData data)
		{
			Transform child = page.transform.GetChild(0);
			Transform child2 = page.transform.GetChild(1);
			this.OnOneKeyFinishCostChange(child, data);
			this.OnTaskCountChange(child, data);
			this.OnTaskRateChange(child2, data);
			this.OnTaskStateChange(child2, data);
			this.OnStarInfoChange(child2, data);
			this.OnPrizeAndMoveBtnChange(child2, data);
		}

		private GameObject OnCreateBanchPage(GameObject pageTemp, TaskData data)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(pageTemp);
			this.bmis.name = gameObject.transform.FindChild("0/title/Text").GetComponent<Text>();
			this.bmis.desc = gameObject.transform.FindChild("0/desc/Text").GetComponent<Text>();
			this.bmis.btnGo = gameObject.transform.FindChild("0/btnGo").GetComponent<Button>();
			bool flag = data.explain.Length > 0;
			if (flag)
			{
				gameObject.transform.FindChild("0/state/Text").GetComponent<Text>().text = data.explain;
			}
			this.bmis.rewardCon = gameObject.transform.FindChild("0/con_icon");
			this.bmis.name.text = data.taskName;
			this.bmis.desc.text = string.Concat(new object[]
			{
				ModelBase<A3_TaskModel>.getInstance().GetTaskDesc(data.taskId, false),
				"(",
				data.taskRate,
				"/",
				data.completion,
				")"
			});
			List<TaskRewardData> reward = ModelBase<A3_TaskModel>.getInstance().GetReward(data.taskId);
			for (int i = 0; i < this.bmis.rewardCon.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.bmis.rewardCon.GetChild(i).gameObject);
			}
			for (int j = 0; j < reward.Count; j++)
			{
				bool flag2 = reward[j].type == 1;
				GameObject gameObject2;
				if (flag2)
				{
					gameObject2 = IconImageMgr.getInstance().createMoneyIcon(reward[j].id.ToString(), 1f, reward[j].num);
				}
				else
				{
					bool flag3 = reward[j].type == 2;
					if (flag3)
					{
						gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(reward[j].item, true, -1, 1f, false);
					}
					else
					{
						gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(reward[j].item, true, reward[j].num, 1f, false);
					}
				}
				bool flag4 = gameObject2 != null;
				if (flag4)
				{
					gameObject2.transform.SetParent(this.bmis.rewardCon, false);
				}
			}
			gameObject.transform.SetParent(this.conTask, false);
			gameObject.SetActive(true);
			return gameObject;
		}

		private void ShowKillExtraPrize(Transform conDesc, TaskData data)
		{
			int extraAward = data.extraAward;
			Dictionary<uint, int> extarPrizeData = this.tkModel.GetExtarPrizeData(extraAward);
			Transform transform = conDesc.FindChild("con_icon");
			this.OnCreatePrizeIcon(transform, extarPrizeData, 0.8f);
		}

		private void CreateMonsterAvatar(int id)
		{
			bool flag = this.lastAvatarId == id;
			if (!flag)
			{
				bool flag2 = this.monsterAvatar != null;
				if (flag2)
				{
					UnityEngine.Object.Destroy(this.monsterAvatar);
				}
				SXML sXML = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + id);
				string path = string.Empty;
				bool flag3 = sXML != null;
				if (flag3)
				{
					string @string = sXML.getString("obj");
					path = "monster/" + @string;
				}
				GameObject gameObject = Resources.Load<GameObject>(path);
				bool flag4 = gameObject != null;
				if (flag4)
				{
					this.monsterAvatar = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-128.8f, 0f, 0f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
				}
			}
		}

		private void CreateMonsterCamera()
		{
			bool flag = this.monsterCamera == null;
			if (flag)
			{
				GameObject gameObject = Resources.Load<GameObject>("profession/avatar_ui/wing_ui_camera");
				bool flag2 = gameObject != null;
				if (flag2)
				{
					this.monsterCamera = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					Camera componentInChildren = this.monsterCamera.GetComponentInChildren<Camera>();
					bool flag3 = componentInChildren != null;
					if (flag3)
					{
						float orthographicSize = componentInChildren.orthographicSize * 1920f / 1080f * (float)Screen.height / (float)Screen.width;
						componentInChildren.orthographicSize = orthographicSize;
					}
				}
			}
		}

		private void DisposeAvatar()
		{
			bool flag = this.monsterAvatar != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.monsterAvatar);
				this.lastAvatarId = -1;
			}
		}

		private void DisposeCamera()
		{
			bool flag = this.monsterCamera != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.monsterCamera);
			}
		}

		private GameObject OnCreateEntrustPage(GameObject pageTemp, TaskData data)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(pageTemp);
			Transform child = gameObject.transform.GetChild(0);
			Transform child2 = gameObject.transform.GetChild(1);
			Transform transform = child.transform.FindChild("reward/view/con");
			int taskId = data.taskId;
			this.OnTaskCountChange(child, data);
			this.OnTaskRateChange(child2, data);
			this.OnTaskStateChange(child2, data);
			this.OnTaskNameChange(child2, data);
			this.ShowPanelTaskReward(child2, data);
			this.ShowPanelExtraEntTaskReward(child, ModelBase<A3_TaskModel>.getInstance().GetEntrustRewardList());
			Transform trans = child2.FindChild("btn_move");
			this.InitMoveBtn(trans);
			gameObject.transform.SetParent(this.conTask, false);
			return gameObject;
		}

		private void ShowPanelExtraEntTaskReward(Transform tfRewardRoot, List<EntrustExtraRewardData> rewardList)
		{
			Transform transform = tfRewardRoot.FindChild("reward/view/con");
			for (int i = transform.childCount; i > 0; i++)
			{
				UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject);
			}
			for (int j = 0; j < rewardList.Count; j++)
			{
				IconImageMgr.getInstance().createA3ItemIcon(rewardList[j].tpid, false, rewardList[j].num, 1f, false, -1, 0, false, false, false, false).transform.SetParent(transform, false);
			}
		}

		private GameObject OnCreateClanTaskPage(GameObject pageTemp, TaskData data)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(pageTemp);
			Transform child = gameObject.transform.GetChild(0);
			Transform child2 = gameObject.transform.GetChild(1);
			Transform transform = child.transform.FindChild("reward/view/con");
			int taskId = data.taskId;
			bool flag = data.explain.Length > 0;
			if (flag)
			{
				gameObject.transform.FindChild("0/state").GetComponent<Text>().text = data.explain;
			}
			this.OnTaskCountChange(child, data);
			this.OnTaskRateChange(child2, data);
			this.OnTaskStateChange(child2, data);
			this.OnTaskNameChange(child2, data);
			this.ShowPanelTaskReward(child2, data);
			List<SXML> nodeList = XMLMgr.instance.GetSXML("task.clan_extra", "").GetNodeList("RewardItem", "");
			Transform transform2 = gameObject.transform.FindChild("0/reward/view/con");
			for (int i = transform2.childCount; i > 0; i++)
			{
				UnityEngine.Object.DestroyImmediate(transform2.GetChild(i).gameObject);
			}
			for (int j = 0; j < nodeList.Count; j++)
			{
				IconImageMgr.getInstance().createA3ItemIcon(nodeList[j].getUint("item_id"), false, nodeList[j].getInt("value"), 1f, false, -1, 0, false, false, false, false).transform.SetParent(transform2, false);
			}
			Transform trans = child2.FindChild("btn_move");
			this.InitMoveBtn(trans);
			gameObject.transform.SetParent(this.conTask, false);
			return gameObject;
		}

		private void OnTaskDescChange(Transform conDesc, TaskData data)
		{
			Text component = conDesc.FindChild("state/Text").GetComponent<Text>();
			Text component2 = conDesc.FindChild("title/Text").GetComponent<Text>();
			ChapterInfos chapterInfosById = this.tkModel.GetChapterInfosById(data.chapterId);
			component2.text = chapterInfosById.name;
			component.text = chapterInfosById.description;
		}

		private void OnTaskCountChange(Transform conDesc, TaskData data)
		{
			Slider component = conDesc.FindChild("slider/slider").GetComponent<Slider>();
			Text component2 = conDesc.FindChild("slider/text").GetComponent<Text>();
			int taskCount = data.taskCount;
			int taskLoop = data.taskLoop;
			int num = this.tkModel.GetTaskMaxCount(data.taskId);
			bool flag = data.taskT == TaskType.ENTRUST;
			if (flag)
			{
				component.maxValue = (float)num;
				component.value = (float)(taskCount + 1);
				component2.text = string.Concat(new object[]
				{
					"(",
					taskCount % 10 + 1,
					"/",
					num,
					")"
				});
			}
			else
			{
				bool flag2 = data.taskT == TaskType.CLAN;
				if (flag2)
				{
					num = (int)A3_TaskModel.CLAN_MAX_COUNT;
					component.maxValue = (float)num;
					component.value = (float)((long)taskLoop * (long)((ulong)A3_TaskModel.CLAN_CNT_EACHLOOP) + (long)taskCount + 1L);
					component2.text = string.Concat(new object[]
					{
						"(",
						component.value,
						"/",
						num,
						")"
					});
				}
				else
				{
					component.maxValue = (float)num;
					component.value = (float)taskCount;
					component2.text = string.Concat(new object[]
					{
						"(",
						taskCount,
						"/",
						num,
						")"
					});
				}
			}
		}

		private void OnTaskRateChange(Transform conState, TaskData data)
		{
			Text component = conState.FindChild("task/Text_state/Text_count").GetComponent<Text>();
			Transform transform = conState.FindChild("task/Text_state/Text_count");
			TaskTargetType targetType = data.targetType;
			bool flag = targetType == TaskTargetType.VISIT;
			if (flag)
			{
				transform.gameObject.SetActive(false);
			}
			else
			{
				transform.gameObject.SetActive(true);
			}
			int completion = data.completion;
			int taskRate = data.taskRate;
			component.text = "";
		}

		private void OnTaskStateChange(Transform conState, TaskData data)
		{
			Text component = conState.FindChild("task/Text_state").GetComponent<Text>();
			Transform transform = conState.FindChild("task/Text_state/Text_count");
			TaskTargetType targetType = data.targetType;
			bool flag = targetType == TaskTargetType.VISIT;
			if (flag)
			{
				transform.gameObject.SetActive(false);
			}
			else
			{
				transform.gameObject.SetActive(true);
			}
			int completion = data.completion;
			int taskRate = data.taskRate;
			component.text = this.tkModel.GetTaskDesc(data.taskId, false);
			bool isComplete = data.isComplete;
			if (isComplete)
			{
				component.text = this.tkModel.GetTaskDesc(data.taskId, false) + " (已完成)";
				transform.gameObject.SetActive(true);
			}
			else
			{
				component.text = string.Concat(new object[]
				{
					this.tkModel.GetTaskDesc(data.taskId, false),
					" (",
					taskRate,
					"/",
					completion,
					")"
				});
				transform.gameObject.SetActive(true);
			}
		}

		private void OnTaskNameChange(Transform conState, TaskData data)
		{
			Text component = conState.FindChild("title/Text").GetComponent<Text>();
			component.text = data.taskName;
		}

		private void OnCreatePrizeIcon(Transform container, Dictionary<uint, int> dicItem, float iconScale)
		{
			GameObject gameObject = container.parent.FindChild("icon_bg").gameObject;
			for (int i = 0; i < container.childCount; i++)
			{
				GameObject gameObject2 = container.GetChild(i).gameObject;
				UnityEngine.Object.Destroy(gameObject2);
			}
			foreach (uint current in dicItem.Keys)
			{
				int num = dicItem[current];
				GameObject gameObject3 = IconImageMgr.getInstance().createA3ItemIcon(current, false, num, iconScale, true, -1, 0, false, false, true, false);
				GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject4.transform.SetParent(gameObject3.transform);
				gameObject4.transform.SetAsFirstSibling();
				gameObject4.transform.localScale = Vector3.one;
				gameObject4.transform.localPosition = Vector3.zero;
				gameObject4.GetComponent<RectTransform>().sizeDelta /= 0.8f;
				gameObject4.SetActive(true);
				gameObject3.transform.SetParent(container.transform, false);
				gameObject3.SetActive(true);
				gameObject3.transform.FindChild("iconborder").GetComponent<RectTransform>().sizeDelta = new Vector2(78f, 78f);
			}
		}

		private void InitMoveBtn(Transform trans)
		{
			BaseButton baseButton = new BaseButton(trans, 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnMoveClick);
		}

		private void InitGetPrizeBtn(Transform trans)
		{
			BaseButton baseButton = new BaseButton(trans, 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnGetPrizeClick);
		}

		private void InitOnkeUpgradeStar(Transform trans)
		{
			BaseButton baseButton = new BaseButton(trans, 1, 1);
			baseButton.onClick = new Action<GameObject>(this.InitOnkeyUpgradeStarClick);
		}

		private void InitOneKeyFinishTask(Transform trans)
		{
			BaseButton baseButton = new BaseButton(trans, 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnTaskOneKeyFiniskClick);
		}

		private void OnPrizeAndMoveBtnChange(Transform panel, TaskData data)
		{
			int taskId = data.taskId;
			Transform transform = panel.FindChild("btn_move");
			Transform transform2 = panel.FindChild("get_reward");
			bool isComplete = data.isComplete;
			transform.gameObject.SetActive(!isComplete);
			transform2.gameObject.SetActive(isComplete);
		}

		public void OnMoveClick(GameObject go)
		{
			A3_TaskModel a3_TaskModel = ModelBase<A3_TaskModel>.getInstance();
			a3_task_auto.instance.RunTask(a3_TaskModel.curTask, false, false);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_TASK);
		}

		private void OnTaskOneKeyFiniskClick(GameObject go)
		{
			Dictionary<int, TaskData> taskDataByTaskType = this.tkModel.GetTaskDataByTaskType(TaskType.DAILY);
			foreach (TaskData current in taskDataByTaskType.Values)
			{
				this.OnOneKeyFinishTask(current);
			}
		}

		private void OnOneKeyFinishTask(TaskData data)
		{
			int dailyMaxCount = this.tkModel.GetDailyMaxCount();
			int num = dailyMaxCount - data.taskCount;
			bool flag = num < 1;
			if (flag)
			{
				debug.Log("每日任务已完成");
			}
			else
			{
				int oneKeyFinishEveryOneCost = this.tkModel.GetOneKeyFinishEveryOneCost();
				int num2 = oneKeyFinishEveryOneCost * num;
				uint gold = ModelBase<PlayerModel>.getInstance().gold;
				bool flag2 = (ulong)gold < (ulong)((long)num2);
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getError("-1001"), 0, default(Color), null);
				}
				else
				{
					BaseProxy<A3_TaskProxy>.getInstance().OneKeyFinishTask();
				}
			}
		}

		private void OnCloseClick(GameObject go)
		{
			this.Toclose = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_TASK);
		}

		private void InitOnkeyUpgradeStarClick(GameObject go)
		{
			uint money = ModelBase<PlayerModel>.getInstance().money;
			uint refreshStarCost = this.tkModel.GetRefreshStarCost();
			int taskId = this.tkModel.curTask.taskId;
			TaskData taskDataById = this.tkModel.GetTaskDataById(taskId);
			bool flag = money < refreshStarCost;
			if (flag)
			{
				ContMgr.getError("-1002");
			}
			else
			{
				int maxStarLevel = this.tkModel.GetMaxStarLevel();
				bool flag2 = taskDataById.taskStar > maxStarLevel;
				if (flag2)
				{
					debug.Log("已达到最高星级");
				}
				else
				{
					BaseProxy<A3_TaskProxy>.getInstance().SendUpgradeStar();
				}
			}
		}

		private void OnGetPrizeClick(GameObject go)
		{
			int num = int.Parse(go.name);
			int taskId = this.tkModel.curTask.taskId;
			TaskData curTask = this.tkModel.curTask;
			bool flag = !curTask.isComplete;
			if (flag)
			{
				debug.Log("任务未完成");
			}
			else
			{
				int num2 = num;
				if (num2 != 1)
				{
					if (num2 == 2)
					{
						this.DoublePrize(num);
					}
				}
				else
				{
					this.OnePrize(num);
				}
			}
		}

		private void DoublePrize(int rate)
		{
			uint doublePrizeCost = this.tkModel.GetDoublePrizeCost();
			uint gold = ModelBase<PlayerModel>.getInstance().gold;
			bool flag = gold < doublePrizeCost;
			if (flag)
			{
				flytxt.instance.fly("钻石不足", 0, default(Color), null);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false).winComponent.transform.SetAsLastSibling();
			}
			else
			{
				BaseProxy<A3_TaskProxy>.getInstance().SendGetAward(rate);
			}
		}

		private void OnePrize(int rate)
		{
			BaseProxy<A3_TaskProxy>.getInstance().SendGetAward(0);
		}

		public static void ExcutTask(int taskId)
		{
			A3_TaskModel a3_TaskModel = ModelBase<A3_TaskModel>.getInstance();
			TaskData taskDataById = a3_TaskModel.GetTaskDataById(taskId);
		}
	}
}
