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
	internal class A3_TaskOpt : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly A3_TaskOpt.<>c <>9 = new A3_TaskOpt.<>c();

			public static Action<GameObject> <>9__37_1;

			public static Action<GameObject> <>9__37_3;

			internal void <init>b__37_1(GameObject go)
			{
				go.transform.parent.gameObject.SetActive(false);
			}

			internal void <init>b__37_3(GameObject go)
			{
				uint unlockedDragonId = ModelBase<A3_SlayDragonModel>.getInstance().GetUnlockedDragonId();
				int unlockedDiffLv = ModelBase<A3_SlayDragonModel>.getInstance().GetUnlockedDiffLv();
				BaseProxy<A3_SlayDragonProxy>.getInstance().SendGo();
			}
		}

		public int curTaskId;

		private static float waitThresholdDistance = 3f;

		private static float killThresholdDistance = 1.5f;

		private uint submitItemIId;

		private Transform tfParentWait;

		public Transform tfSubmitItem;

		private Transform tfSubmitItemCon;

		private Transform tfFocus;

		private bool haveEnteredWaitPosition;

		public uint taskItemId;

		private Image imgProcess;

		private long timeWaitTerminal;

		private GameObject winKillMon;

		private GameObject winKillDragon;

		public static A3_TaskOpt Instance;

		public bool isWaiting;

		public Vector3 waitPosition;

		public Vector3 killPosition;

		public BaseButton BtnWait;

		public Dictionary<int, TaskOptElement> taskOptElement;

		private Vector3 scaleIcon;

		public Image actionImage;

		private List<a3_BagItemData> bagItem = new List<a3_BagItemData>();

		private float waitTime = 5f;

		public bool LockStat = false;

		private float timeCD
		{
			get
			{
				return ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(this.curTaskId).need_tm;
			}
		}

		public bool IsOnTaskWait
		{
			get;
			set;
		}

		public bool IsOnKillMon
		{
			get;
			set;
		}

		public bool IsOnTaskGet
		{
			get;
			set;
		}

		public override void init()
		{
			A3_TaskOpt.Instance = this;
			this.tfParentWait = base.transform.FindChild("wait");
			this.tfParentWait.gameObject.SetActive(false);
			this.imgProcess = this.tfParentWait.FindChild("waitBG").GetComponent<Image>();
			(this.BtnWait = new BaseButton(this.tfParentWait.FindChild("waitBG/btnDoWait"), 1, 1)).onClick = new Action<GameObject>(this.OnWaitBtnClick);
			this.actionImage = this.tfParentWait.FindChild("waitBG/btnDoWait").GetComponent<Image>();
			this.winKillMon = base.transform.FindChild("killmon").gameObject;
			this.winKillMon.SetActive(false);
			this.winKillDragon = base.transform.FindChild("killDragon").gameObject;
			this.winKillDragon.SetActive(false);
			this.tfSubmitItem = base.transform.FindChild("submitItem");
			this.tfSubmitItemCon = this.tfSubmitItem.FindChild("mask/scrollview/con");
			this.tfFocus = this.tfSubmitItem.FindChild("focus");
			this.tfFocus.gameObject.SetActive(false);
			this.tfSubmitItem.gameObject.SetActive(false);
			Transform trans = this.winKillMon.transform.FindChild("btnStart");
			Transform trans2 = this.winKillMon.transform.FindChild("btnDontStart");
			new BaseButton(trans, 1, 1).onClick = new Action<GameObject>(this.OnStartBtnClick);
			new BaseButton(trans2, 1, 1).onClick = new Action<GameObject>(this.OnCancelBtnClick);
			new BaseButton(base.transform.FindChild("submitItem/closeBtn"), 1, 1).onClick = delegate(GameObject btnClose)
			{
				this.tfSubmitItem.gameObject.SetActive(false);
			};
			BaseButton arg_20E_0 = new BaseButton(base.transform.FindChild("killmon/closeArea"), 1, 1);
			Action<GameObject> arg_20E_1;
			if ((arg_20E_1 = A3_TaskOpt.<>c.<>9__37_1) == null)
			{
				arg_20E_1 = (A3_TaskOpt.<>c.<>9__37_1 = new Action<GameObject>(A3_TaskOpt.<>c.<>9.<init>b__37_1));
			}
			arg_20E_0.onClick = arg_20E_1;
			new BaseButton(this.tfSubmitItem.FindChild("btnOK"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag4 = this.submitItemIId != 0u && this.curTaskId != 0;
				if (flag4)
				{
					BaseProxy<A3_TaskProxy>.getInstance().SendSubmit(this.curTaskId, this.submitItemIId);
				}
				this.tfSubmitItem.gameObject.SetActive(false);
			};
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(3u, new Action<GameEvent>(this.OnCheck));
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.OnCheck));
			this.taskOptElement = new Dictionary<int, TaskOptElement>();
			Dictionary<int, TaskData> dicTaskData = ModelBase<A3_TaskModel>.getInstance().GetDicTaskData();
			List<int> list = new List<int>(dicTaskData.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				int num = list[i];
				bool flag = dicTaskData[num].release_tm > 0L;
				if (flag)
				{
					bool flag2 = dicTaskData[num].lose_tm > (long)muNetCleint.instance.CurServerTimeStamp;
					if (flag2)
					{
						Dictionary<int, TaskOptElement> arg_2FC_0 = this.taskOptElement;
						int arg_2FC_1 = num;
						int arg_2F7_0 = num;
						bool? isKeepingKillMon = new bool?(true);
						arg_2FC_0[arg_2FC_1] = new TaskOptElement(arg_2F7_0, new bool?(true), isKeepingKillMon);
						TaskOptElement arg_336_0 = this.taskOptElement[num];
						Transform expr_31B = a3_liteMinimap.instance.GetTaskPage(num);
						arg_336_0.InitUi((expr_31B != null) ? expr_31B.transform.FindChild("name/timer").GetComponent<Text>() : null);
					}
				}
			}
			A3_TaskOpt.Instance.name = "A3_TaskOpt";
			A3_TaskOpt.waitThresholdDistance = XMLMgr.instance.GetSXML("task.range", "").getFloat("action_range") / 53.333f;
			Transform expr_3A1 = base.transform.FindChild("submitItem/iconConfig");
			this.scaleIcon = ((expr_3A1 != null) ? expr_3A1.localScale : Vector3.zero);
			BaseButton arg_3F1_0 = new BaseButton(this.winKillDragon.transform.FindChild("btnStart"), 1, 1);
			Action<GameObject> arg_3F1_1;
			if ((arg_3F1_1 = A3_TaskOpt.<>c.<>9__37_3) == null)
			{
				arg_3F1_1 = (A3_TaskOpt.<>c.<>9__37_3 = new Action<GameObject>(A3_TaskOpt.<>c.<>9.<init>b__37_3));
			}
			arg_3F1_0.onClick = arg_3F1_1;
			new BaseButton(this.winKillDragon.transform.FindChild("btnNope"), 1, 1).onClick = delegate(GameObject go)
			{
				this.winKillDragon.SetActive(false);
			};
			bool flag3 = !base.IsInvoking("RunTimer");
			if (flag3)
			{
				base.InvokeRepeating("RunTimer", 0f, 1f);
			}
		}

		public void ShowDragonWin()
		{
			GameObject expr_07 = this.winKillDragon;
			if (expr_07 != null)
			{
				expr_07.SetActive(true);
			}
		}

		private void OnCheck(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data == null;
			if (!flag)
			{
				bool flag2 = data.ContainsKey("change_task") && data["change_task"].Length > 0;
				if (flag2)
				{
					Variant variant = data["change_task"][0];
					bool flag3 = variant.ContainsKey("id");
					if (flag3)
					{
						int @int = variant["id"]._int;
						TaskData taskDataById = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(@int);
						bool flag4 = this.taskOptElement.ContainsKey(@int) && variant.ContainsKey("cnt") && variant["cnt"]._int >= taskDataById.completion;
						if (flag4)
						{
							TaskOptElement arg_F0_0 = this.taskOptElement[@int];
							bool? isKeepingKillMon = new bool?(false);
							arg_F0_0.Set(new bool?(false), isKeepingKillMon, new long?(-1L));
							bool flag5 = this.taskOptElement.ContainsKey(@int);
							if (flag5)
							{
								TaskOptElement arg_131_0 = this.taskOptElement[@int];
								bool? arg_131_1 = new bool?(false);
								isKeepingKillMon = null;
								arg_131_0.Set(arg_131_1, isKeepingKillMon, null);
								GameObject expr_14E = this.taskOptElement[@int].liteMinimapTaskTimer.gameObject;
								if (expr_14E != null)
								{
									expr_14E.SetActive(false);
								}
								this.taskOptElement.Remove(@int);
							}
						}
						else
						{
							bool flag6 = taskDataById.targetType == TaskTargetType.KILL_MONSTER_GIVEN;
							if (flag6)
							{
								uint num = variant.ContainsKey("lose_tm") ? variant["lose_tm"]._uint : 0u;
								TaskOptElement arg_1DD_0 = this.taskOptElement[@int];
								bool? isKeepingKillMon = new bool?(true);
								arg_1DD_0.Set(new bool?(taskDataById.release_tm > (long)((ulong)num - (ulong)((long)muNetCleint.instance.CurServerTimeStamp))), isKeepingKillMon, new long?((long)((ulong)num)));
							}
						}
					}
				}
				else
				{
					int num2 = 0;
					Variant variant2 = null;
					TaskTargetType taskTargetType = (TaskTargetType)0;
					bool flag7 = data.ContainsKey("mlmis");
					if (flag7)
					{
						taskTargetType = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(num2 = (variant2 = data["mlmis"])["id"]._int).targetType;
					}
					bool flag8 = data.ContainsKey("bmis");
					if (flag8)
					{
						taskTargetType = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(num2 = (variant2 = data["bmis"])["id"]._int).targetType;
					}
					bool flag9 = data.ContainsKey("dmis");
					if (flag9)
					{
						taskTargetType = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(num2 = (variant2 = data["dmis"])["id"]._int).targetType;
					}
					bool flag10 = data.ContainsKey("emis");
					if (flag10)
					{
						taskTargetType = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(num2 = (variant2 = data["emis"])["id"]._int).targetType;
					}
					bool flag11 = data.ContainsKey("cmis");
					if (flag11)
					{
						taskTargetType = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(num2 = (variant2 = data["cmis"])["id"]._int).targetType;
					}
					bool flag12 = taskTargetType == TaskTargetType.KILL_MONSTER_GIVEN;
					if (flag12)
					{
						bool flag13 = variant2.ContainsKey("cnt") && variant2["cnt"] < ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(num2).completion && variant2.ContainsKey("lose_tm");
						long value;
						if (flag13)
						{
							value = variant2["lose_tm"]._int64;
						}
						else
						{
							value = 0L;
						}
						TaskOptElement arg_3D3_0 = this.taskOptElement[num2];
						bool? isKeepingKillMon = new bool?(true);
						arg_3D3_0.Set(new bool?(false), isKeepingKillMon, new long?(value));
					}
					else
					{
						bool flag14 = taskTargetType == TaskTargetType.GET_ITEM_GIVEN;
						if (flag14)
						{
							this.taskItemId = (uint)ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(num2).completionAim;
						}
					}
				}
				bool flag15 = !base.IsInvoking("RunTimer");
				if (flag15)
				{
					base.InvokeRepeating("RunTimer", 0f, 1f);
				}
			}
		}

		private void RunTimer()
		{
			Dictionary<int, TaskData> dicTaskData = ModelBase<A3_TaskModel>.getInstance().GetDicTaskData();
			List<int> list = new List<int>(dicTaskData.Keys);
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				bool flag2 = !this.taskOptElement.ContainsKey(list[i]);
				if (!flag2)
				{
					TaskOptElement taskOptElement = this.taskOptElement[list[i]];
					float num = (float)dicTaskData[list[i]].lose_tm;
					float num2 = (float)muNetCleint.instance.CurServerTimeStamp;
					float num3 = (float)dicTaskData[list[i]].release_tm;
					TaskData taskData = dicTaskData[list[i]];
					bool flag3 = this.taskOptElement.ContainsKey(list[i]);
					if (flag3)
					{
						long num4 = taskData.lose_tm - (long)muNetCleint.instance.CurServerTimeStamp;
						bool flag4 = TaskTargetType.KILL_MONSTER_GIVEN == taskData.targetType && taskData.taskCount < taskData.completion && num4 > 0L && taskData.release_tm > num4;
						bool flag5 = flag4;
						if (flag5)
						{
							taskOptElement.liteMinimapTaskTimer.gameObject.SetActive(true);
							taskOptElement.liteMinimapTaskTimer.text = this.GetSecByTime(taskData.lose_tm);
							this.taskOptElement[taskOptElement.taskId].Set(new bool?(true), null, null);
						}
						else
						{
							this.taskOptElement[taskOptElement.taskId].Set(new bool?(false), null, null);
							taskOptElement.liteMinimapTaskTimer.gameObject.SetActive(false);
						}
						flag |= flag4;
					}
				}
			}
			bool flag6 = this.waitTime < 0f;
			if (flag6)
			{
				base.CancelInvoke("RunTimer");
				this.waitTime = 5f;
			}
			else
			{
				bool flag7 = !flag;
				if (flag7)
				{
					this.waitTime -= 1f;
				}
			}
		}

		public void ResetStat()
		{
			this.IsOnKillMon = false;
			this.IsOnTaskWait = false;
		}

		private void Update()
		{
			bool moveing = joystick.instance.moveing;
			if (moveing)
			{
				bool flag = this.isWaiting;
				if (flag)
				{
					this.StopCD(false);
				}
				this.Reset(true);
			}
			else
			{
				TaskData curTask = ModelBase<A3_TaskModel>.getInstance().curTask;
				bool flag2 = curTask == null;
				if (!flag2)
				{
					bool flag3 = !this.LockStat;
					if (flag3)
					{
						this.IsOnKillMon = (!curTask.isComplete && curTask.targetType == TaskTargetType.KILL_MONSTER_GIVEN);
						this.IsOnTaskWait = (!curTask.isComplete && curTask.targetType == TaskTargetType.WAIT_POINT_GIVEN);
					}
					bool flag4 = this.IsOnKillMon || this.IsOnTaskWait;
					if (flag4)
					{
						Vector3 a = new Vector3(SelfRole._inst.m_curModel.position.x, 0f, SelfRole._inst.m_curModel.position.z);
						bool isOnTaskWait = this.IsOnTaskWait;
						if (isOnTaskWait)
						{
							bool flag5 = this.waitPosition == Vector3.zero;
							if (flag5)
							{
								this.waitPosition = ModelBase<A3_TaskModel>.getInstance().GetTaskTargetPos(curTask.taskId);
							}
							bool flag6 = this.waitPosition != Vector3.zero;
							if (flag6)
							{
								bool flag7 = Vector3.Distance(a, this.waitPosition) < A3_TaskOpt.waitThresholdDistance;
								if (flag7)
								{
									this.LockStat = true;
									this.IsOnTaskWait = false;
									this.tfParentWait.gameObject.SetActive(true);
									SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, false);
									bool isActiveAndEnabled = SelfRole._inst.m_moveAgent.isActiveAndEnabled;
									if (isActiveAndEnabled)
									{
										SelfRole._inst.m_moveAgent.Stop();
									}
									this.haveEnteredWaitPosition = true;
								}
								else
								{
									bool flag8 = this.haveEnteredWaitPosition;
									if (flag8)
									{
										this.Reset(true);
										this.haveEnteredWaitPosition = false;
									}
								}
							}
						}
						else
						{
							bool flag9 = this.IsOnKillMon && this.killPosition != Vector3.zero;
							if (flag9)
							{
								bool flag10 = Vector3.Distance(a, this.killPosition) < A3_TaskOpt.killThresholdDistance;
								if (flag10)
								{
									TaskData expr_227 = a3_task_auto.instance.executeTask;
									int key = (expr_227 != null) ? expr_227.taskId : ModelBase<A3_TaskModel>.getInstance().curTask.taskId;
									SelfRole.fsm.Stop();
									bool flag11 = this.taskOptElement.ContainsKey(key);
									if (flag11)
									{
										this.LockStat = true;
										this.IsOnKillMon = false;
										bool flag12 = !this.taskOptElement[key].isTaskMonsterAlive;
										if (flag12)
										{
											this.winKillMon.SetActive(true);
										}
										else
										{
											bool flag13 = !SelfRole.fsm.Autofighting;
											if (flag13)
											{
												SelfRole.fsm.StartAutofight();
											}
										}
									}
								}
								else
								{
									this.winKillMon.SetActive(false);
								}
							}
						}
					}
				}
			}
		}

		private string GetSecByTime(long sec)
		{
			long num = (long)muNetCleint.instance.CurServerTimeStamp;
			long num2 = sec - num;
			bool flag = num2 > 0L;
			string result;
			if (flag)
			{
				result = "[" + num2.ToString() + "]";
			}
			else
			{
				result = "";
			}
			return result;
		}

		public override void onShowed()
		{
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.OnStopTimer));
			base.onShowed();
		}

		private void OnStopTimer(GameEvent e)
		{
			bool flag = e.data.ContainsKey("id");
			if (flag)
			{
				int key = e.data["id"];
				bool flag2 = this.taskOptElement.ContainsKey(key);
				if (flag2)
				{
					this.taskOptElement[key].Set(new bool?(false), null, null);
					GameObject expr_81 = this.taskOptElement[key].liteMinimapTaskTimer.gameObject;
					if (expr_81 != null)
					{
						expr_81.SetActive(false);
					}
					this.taskOptElement.Remove(key);
				}
			}
		}

		private void OnWaitBtnClick(GameObject go)
		{
			SelfRole.fsm.Stop();
			this.timeWaitTerminal = (long)((int)this.timeCD * 1000) + NetClient.instance.CurServerTimeStampMS;
			this.BtnWait.interactable = !(this.isWaiting = true);
			BaseProxy<A3_TaskProxy>.getInstance().SendWaitStart(this.curTaskId);
			bool flag = this.timeCD > 0f;
			if (flag)
			{
				TaskData taskDataById = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(this.curTaskId);
				base.StartCoroutine(this.RunCD());
			}
		}

		private void OnStartBtnClick(GameObject go)
		{
			bool flag = this.curTaskId != 0 && this.taskOptElement[this.curTaskId].isTaskMonsterAlive;
			if (!flag)
			{
				BaseProxy<A3_TaskProxy>.getInstance().SendCallMonster((uint)this.curTaskId);
				this.winKillMon.SetActive(false);
				SelfRole.fsm.StartAutofight();
			}
		}

		private void OnCancelBtnClick(GameObject go)
		{
			this.Reset(true);
		}

		public void ShowSubmitItem()
		{
			bool flag = this.tfSubmitItemCon == null;
			if (!flag)
			{
				this.tfSubmitItem.gameObject.SetActive(true);
				this.tfFocus.SetParent(this.tfSubmitItem, false);
				this.tfFocus.gameObject.SetActive(false);
				Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
				int i;
				for (i = this.tfSubmitItemCon.childCount; i > 0; i--)
				{
					UnityEngine.Object.DestroyImmediate(this.tfSubmitItemCon.GetChild(i - 1).gameObject);
				}
				List<uint> list = new List<uint>();
				List<uint> list2 = new List<uint>(items.Keys);
				while (i < list2.Count)
				{
					bool flag2 = items[list2[i]].tpid == this.taskItemId;
					if (flag2)
					{
						Transform transform;
						(transform = IconImageMgr.getInstance().createA3ItemIcon(items[list2[i]].tpid, false, items[list2[i]].num, 1f, false, -1, 0, false, false, false, true).transform).SetParent(this.tfSubmitItemCon, false);
						transform.localScale = this.scaleIcon;
						uint itemIId = items[list2[i]].id;
						list.Add(itemIId);
						new BaseButton(transform.GetComponentInChildren<Button>().transform, 1, 1).onClick = delegate(GameObject go)
						{
							this.tfFocus.SetParent(go.transform, false);
							this.tfFocus.gameObject.SetActive(true);
							this.submitItemIId = itemIId;
						};
					}
					i++;
				}
				bool flag3 = this.tfSubmitItemCon.childCount > 0;
				if (flag3)
				{
					this.tfFocus.SetParent(this.tfSubmitItemCon.GetChild(0), false);
					this.tfFocus.gameObject.SetActive(true);
					this.submitItemIId = items[list[0]].id;
				}
			}
		}

		public void Reset(bool alsoHideGameObject = false)
		{
			this.waitPosition = Vector3.zero;
			this.haveEnteredWaitPosition = false;
			this.killPosition = Vector3.zero;
			if (alsoHideGameObject)
			{
				this.tfParentWait.gameObject.SetActive(false);
				this.winKillMon.SetActive(false);
			}
		}

		public void HideSubmitItem()
		{
			bool activeSelf = this.tfSubmitItem.gameObject.activeSelf;
			if (activeSelf)
			{
				this.tfSubmitItem.gameObject.SetActive(false);
			}
		}

		private IEnumerator RunCD()
		{
			while (!SelfRole._inst.m_curAni.GetBool(EnumAni.ANI_RUN) && !SelfRole.s_bInTransmit)
			{
				long curServerTimeStampMS = NetClient.instance.CurServerTimeStampMS;
				bool flag = this.timeWaitTerminal < curServerTimeStampMS;
				if (flag)
				{
					this.tfParentWait.gameObject.SetActive(false);
					this.StopCD(true);
					bool showMessage = ModelBase<A3_TaskModel>.getInstance().curTask.showMessage;
					if (showMessage)
					{
						string strMsg = string.Format(ModelBase<A3_TaskModel>.getInstance().curTask.completionStr, ModelBase<A3_LegionModel>.getInstance().myLegion.clname);
						a3_chatroom._instance.SendMsg(strMsg, ChatToType.Nearby);
						strMsg = null;
					}
					yield break;
				}
				this.imgProcess.fillAmount = (this.timeCD * 1000f - (float)(this.timeWaitTerminal - curServerTimeStampMS)) / (this.timeCD * 1000f);
				yield return null;
			}
			this.imgProcess.fillAmount = 0f;
			yield break;
		}

		public void StopCD(bool isFinish = false)
		{
			this.BtnWait.interactable = !(this.isWaiting = false);
			this.imgProcess.fillAmount = 0f;
			base.StopCoroutine(this.RunCD());
			if (isFinish)
			{
				BaseProxy<A3_TaskProxy>.getInstance().SendWaitEnd(this.curTaskId);
			}
		}
	}
}
