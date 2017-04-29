using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class npctasktalk : npctalk
	{
		private Transform conIcon;

		private Transform conOption;

		private GameObject optionTemp;

		private GameObject iconTemp;

		public new List<Transform> lUiPos = new List<Transform>();

		private BaseButton btnNext;

		private BaseButton btnExit;

		private Transform transTalk;

		private A3_TaskModel tkModel;

		private Transform bg_task;

		private Image headicon;

		private bool hasWait = false;

		private int npc_id;

		private TaskData curTask
		{
			get
			{
				bool flag = a3_task_auto.instance.executeTask != null;
				TaskData result;
				if (flag)
				{
					result = a3_task_auto.instance.executeTask;
				}
				else
				{
					result = ModelBase<A3_TaskModel>.getInstance().curTask;
				}
				return result;
			}
			set
			{
				ModelBase<A3_TaskModel>.getInstance().curTask = value;
			}
		}

		public override void OnInit()
		{
			this.headicon = base.getTransformByPath("talk/npc_head/icon").GetComponent<Image>();
			this.bg_task = base.getTransformByPath("talk/bg_task");
			this.tkModel = ModelBase<A3_TaskModel>.getInstance();
			this.lUiPos.Add(base.getTransformByPath("con1"));
			this.lUiPos.Add(base.getTransformByPath("con0"));
			this.conIcon = base.getTransformByPath("talk/con_icon");
			this.conOption = base.getTransformByPath("talk/bg_taskselect/con_options");
			this.optionTemp = base.getGameObjectByPath("talk/optionTemp");
			this.iconTemp = base.getGameObjectByPath("talk/iconTemp");
			this.transTalk = base.getTransformByPath("talk");
			this.btnNext = new BaseButton(base.getTransformByPath("talk/bg_task/next"), 1, 1);
			this.btnExit = new BaseButton(base.getTransformByPath("talk/close"), 1, 1);
			this.btnExit.onClick = new Action<GameObject>(this.OnCloseDialog);
			new BaseButton(base.getTransformByPath("talk/fg"), 1, 1).onClick = new Action<GameObject>(this.OnCloseDialog);
			base.OnInit();
		}

		public override void refreshView()
		{
			EventTriggerListener.Get(this.fg).clearAllListener();
			this.txtDesc.text = dialog.curDesc;
			bool flag = dialog.curType == "2";
			if (flag)
			{
				this.headicon.sprite = (Resources.Load("icon/npctask/" + ModelBase<PlayerModel>.getInstance().profession, typeof(Sprite)) as Sprite);
			}
			else
			{
				uint @uint = XMLMgr.instance.GetSXML("npcs.npc", "id==" + dialog.m_npc.id).getUint("head_icon");
				Sprite sprite = Resources.Load("icon/npctask/" + @uint, typeof(Sprite)) as Sprite;
				bool flag2 = sprite == null;
				if (flag2)
				{
					sprite = (Resources.Load("icon/npctask/" + 101, typeof(Sprite)) as Sprite);
				}
				this.headicon.sprite = sprite;
			}
			bool flag3 = dialog.curType == "-1";
			if (flag3)
			{
				this.OnShowOptionUi();
				this.ShowNextBtn();
			}
			else
			{
				this.OnShowTaskUi();
			}
		}

		public override void refreshPos()
		{
			bool flag = dialog.curType == "2";
			if (flag)
			{
				this.txtName.text = ModelBase<PlayerModel>.getInstance().name;
			}
			else
			{
				this.txtName.text = dialog.m_npc.name;
			}
		}

		public override void OnClosedProcess()
		{
			bool flag = this.conIcon.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < this.conIcon.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.conIcon.GetChild(i).gameObject);
				}
			}
			bool flag2 = this.conOption.childCount > 0;
			if (flag2)
			{
				for (int j = 0; j < this.conOption.childCount; j++)
				{
					UnityEngine.Object.Destroy(this.conOption.GetChild(j).gameObject);
				}
			}
			BaseProxy<A3_TaskProxy>.getInstance().removeEventListener(3u, new Action<GameEvent>(this.OnRefreshTask));
			base.OnClosedProcess();
		}

		private void OnCloseDialog(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.DIALOG);
			A3_TaskOpt.Instance.HideSubmitItem();
		}

		private void OnShowTaskUi()
		{
			bool isComplete = this.curTask.isComplete;
			if (isComplete)
			{
				this.ShowTask();
			}
			else
			{
				this.conIcon.gameObject.SetActive(true);
				this.OnShowAwardInfo();
				bool flag = this.curTask.targetType == TaskTargetType.VISIT;
				if (flag)
				{
					BaseProxy<A3_TaskProxy>.getInstance().addEventListener(3u, new Action<GameEvent>(this.OnRefreshTask));
					BaseProxy<A3_TaskProxy>.getInstance().SendTalkWithNpc(dialog.m_npc.id);
				}
				bool flag2 = this.curTask.targetType == TaskTargetType.GETEXP;
				if (flag2)
				{
					SXML sXML = XMLMgr.instance.GetSXML("task.Task", "id==" + this.curTask.taskId);
					int num = int.Parse(sXML.getString("trigger"));
					bool flag3 = num == 2;
					if (flag3)
					{
						this.btnNext.interactable = true;
						this.btnNext.transform.FindChild("Text").GetComponent<Text>().text = "转生";
						this.btnNext.onClick = new Action<GameObject>(this.OnOpenZhuan);
					}
				}
				bool flag4 = this.curTask.targetType == TaskTargetType.GET_ITEM_GIVEN;
				if (flag4)
				{
					A3_TaskOpt.Instance.taskItemId = (uint)this.curTask.completionAim;
					this.btnNext.transform.FindChild("Text").GetComponent<Text>().text = "上交物品";
					BaseButton expr_17B = this.btnNext;
					expr_17B.onClick = (Action<GameObject>)Delegate.Combine(expr_17B.onClick, new Action<GameObject>(delegate(GameObject go)
					{
						int num2 = this.curTask.completion - this.curTask.taskRate;
						int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(A3_TaskOpt.Instance.taskItemId);
						bool flag5 = itemNumByTpid < num2;
						if (flag5)
						{
							InterfaceMgr.getInstance().close(InterfaceMgr.DIALOG);
							ArrayList arrayList = new ArrayList();
							arrayList.Add(ModelBase<a3_BagModel>.getInstance().getItemDataById(A3_TaskOpt.Instance.taskItemId));
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMLACK, arrayList, false);
							A3_TaskOpt.Instance.HideSubmitItem();
						}
						else
						{
							A3_TaskOpt expr_92 = A3_TaskOpt.Instance;
							if (expr_92 != null)
							{
								expr_92.ShowSubmitItem();
							}
						}
					}));
				}
			}
		}

		private void OnRefreshTask(GameEvent e)
		{
			bool isComplete = this.curTask.isComplete;
			if (isComplete)
			{
				this.ShowTask();
			}
		}

		private void ShowTask()
		{
			this.conIcon.gameObject.SetActive(true);
			this.conOption.parent.gameObject.SetActive(false);
			this.bg_task.gameObject.SetActive(false);
			bool isLastDesc = dialog.isLastDesc;
			if (isLastDesc)
			{
				this.OnShowAwardInfo();
				this.ShowCompleteBtn();
			}
			else
			{
				this.OnShowAwardInfo();
				this.ShowNextBtn();
			}
		}

		private void ShowCompleteBtn()
		{
			int taskId = this.curTask.taskId;
			Text component = this.btnNext.transform.FindChild("Text").GetComponent<Text>();
			switch (this.tkModel.GetTaskState(taskId))
			{
			case NpcTaskState.UNREACHED:
				component.text = "领取任务";
				this.btnNext.interactable = false;
				break;
			case NpcTaskState.REACHED:
				component.text = "领取任务";
				this.btnNext.interactable = true;
				this.btnNext.onClick = new Action<GameObject>(this.OnAcceptTask);
				break;
			case NpcTaskState.UNFINISHED:
				component.text = "提交任务";
				this.btnNext.interactable = false;
				break;
			case NpcTaskState.FINISHED:
				component.text = "提交任务";
				this.btnNext.interactable = true;
				this.btnNext.onClick = new Action<GameObject>(this.OnSubmitTask);
				break;
			}
		}

		private void ShowNextBtn()
		{
			this.btnNext.transform.FindChild("Text").GetComponent<Text>().text = "继续";
			this.btnNext.onClick = new Action<GameObject>(base.onClick);
			this.btnNext.interactable = true;
		}

		private void OnShowAwardInfo()
		{
			this.bg_task.gameObject.SetActive(true);
			this.conOption.parent.gameObject.SetActive(false);
			TaskData curTask = this.curTask;
			TaskType taskT = curTask.taskT;
			Dictionary<uint, int> dictionary;
			if (taskT != TaskType.CLAN)
			{
				dictionary = this.tkModel.GetValueReward(curTask.taskId);
			}
			else
			{
				dictionary = this.tkModel.GetClanRewardDic(curTask.taskCount);
			}
			Dictionary<uint, int> itemReward = this.tkModel.GetItemReward(curTask.taskId);
			List<a3_BagItemData> equipReward = this.tkModel.GetEquipReward(curTask.taskId);
			bool guide = curTask.guide;
			if (guide)
			{
				this.btnNext.transform.FindChild("guide_task_info").gameObject.SetActive(true);
			}
			else
			{
				this.btnNext.transform.FindChild("guide_task_info").gameObject.SetActive(false);
			}
			bool flag = dictionary != null;
			if (flag)
			{
				bool flag2 = this.conIcon.childCount > 0;
				if (flag2)
				{
					for (int i = 0; i < this.conIcon.childCount; i++)
					{
						UnityEngine.Object.Destroy(this.conIcon.GetChild(i).gameObject);
					}
				}
				foreach (uint current in dictionary.Keys)
				{
					a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(current);
					TaskType taskT2 = curTask.taskT;
					if (taskT2 != TaskType.CLAN)
					{
						itemDataById.file = "icon/comm/0x" + current;
					}
					else
					{
						itemDataById.file = "icon/comm/1x" + current;
					}
					GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(itemDataById, false, dictionary[current], 0.8f, false, -1, 0, false, false, false, -1, false, false);
					Transform transform = gameObject.transform.FindChild("iconbor");
					bool flag3 = transform != null;
					if (flag3)
					{
						transform.gameObject.SetActive(false);
					}
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.iconTemp);
					gameObject.transform.SetParent(gameObject2.transform.FindChild("icon"), false);
					gameObject2.transform.SetParent(this.conIcon, false);
					gameObject2.name = itemDataById.tpid.ToString();
					gameObject2.SetActive(true);
					Image component = gameObject.GetComponent<Image>();
					bool flag4 = component != null;
					if (flag4)
					{
						component.enabled = false;
					}
				}
			}
			bool flag5 = equipReward != null;
			if (flag5)
			{
				foreach (a3_BagItemData current2 in equipReward)
				{
					bool flag6 = !current2.isEquip;
					if (!flag6)
					{
						GameObject gameObject3 = IconImageMgr.getInstance().createA3ItemIcon(current2.id, false, -1, 0.8f, false, -1, 0, false, false, false, false);
						Transform transform2 = gameObject3.transform.FindChild("iconborder");
						bool flag7 = transform2 != null;
						if (flag7)
						{
							transform2.gameObject.SetActive(false);
						}
						GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.iconTemp);
						gameObject3.transform.SetParent(gameObject4.transform.FindChild("icon"), false);
						gameObject4.transform.SetParent(this.conIcon, false);
						UnityEngine.Object arg_375_0 = gameObject4;
						uint tpid = current2.tpid;
						arg_375_0.name = tpid.ToString();
						gameObject4.SetActive(true);
						Image component2 = gameObject3.GetComponent<Image>();
						bool flag8 = component2 != null;
						if (flag8)
						{
							component2.enabled = false;
						}
					}
				}
			}
			bool flag9 = itemReward != null;
			if (flag9)
			{
				foreach (uint current3 in itemReward.Keys)
				{
					int num = itemReward[current3];
					GameObject gameObject5 = IconImageMgr.getInstance().createA3ItemIcon(current3, true, num, 0.8f, false, -1, 0, false, false, false, false);
					Transform transform3 = gameObject5.transform.FindChild("iconborder");
					bool flag10 = transform3 != null;
					if (flag10)
					{
						transform3.gameObject.SetActive(false);
					}
					GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.iconTemp);
					gameObject5.transform.SetParent(gameObject6.transform.FindChild("icon"), false);
					gameObject6.transform.SetParent(this.conIcon, false);
					gameObject6.name = current3.ToString();
					gameObject6.SetActive(true);
					Image component3 = gameObject5.GetComponent<Image>();
					bool flag11 = component3 != null;
					if (flag11)
					{
						component3.enabled = false;
					}
				}
			}
		}

		private void OnSubmitTask(GameObject go)
		{
			this.btnNext.interactable = false;
			BaseProxy<A3_TaskProxy>.getInstance().SendGetAward(0);
			bool flag = !this.curTask.story_hint.Equals("null");
			if (flag)
			{
				flytxt.instance.fly(this.curTask.story_hint, 8, default(Color), null);
			}
			dialog.next();
		}

		private void OnAcceptTask(GameObject go)
		{
			this.btnNext.interactable = false;
			debug.Log("领取任务");
			dialog.next();
		}

		private void OnOpenZhuan(GameObject go)
		{
			this.btnNext.interactable = false;
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_RESETLVL, null, false);
			InterfaceMgr.getInstance().close(InterfaceMgr.DIALOG);
			dialog.next();
		}

		private void OnShowOptionUi()
		{
			this.conIcon.gameObject.SetActive(false);
			this.conOption.parent.gameObject.SetActive(true);
			this.bg_task.gameObject.SetActive(false);
			NpcRole npc = dialog.m_npc;
			List<int> listTaskId = npc.listTaskId;
			bool flag = listTaskId != null;
			if (flag)
			{
				for (int i = 0; i < listTaskId.Count; i++)
				{
					int taskId = listTaskId[i];
					string taskName = this.tkModel.GetTaskDataById(taskId).taskName;
					bool flag2 = this.tkModel.GetTaskDataById(taskId).taskT == TaskType.MAIN;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.optionTemp);
					switch (this.tkModel.GetTaskDataById(taskId).taskT)
					{
					case TaskType.MAIN:
						gameObject.transform.FindChild("sign/main").gameObject.SetActive(true);
						break;
					case TaskType.BRANCH:
						gameObject.transform.FindChild("sign/branch").gameObject.SetActive(true);
						break;
					case TaskType.ENTRUST:
						gameObject.transform.FindChild("sign/entrust").gameObject.SetActive(true);
						break;
					case TaskType.CLAN:
						gameObject.transform.FindChild("sign/clan").gameObject.SetActive(true);
						break;
					}
					gameObject.transform.FindChild("Text").GetComponent<Text>().text = taskName;
					gameObject.transform.SetParent(this.conOption, false);
					gameObject.SetActive(true);
					BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
					baseButton.onClick = new Action<GameObject>(this.OnOptionBtnClick);
					baseButton.gameObject.name = taskId.ToString();
				}
			}
			bool flag3 = npc.openid != "";
			if (flag3)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.optionTemp);
				gameObject2.transform.FindChild("sign/func").gameObject.SetActive(true);
				bool flag4 = npc.openid == "a3_warehouse";
				if (flag4)
				{
					gameObject2.transform.FindChild("Text").GetComponent<Text>().text = "仓库";
				}
				bool flag5 = npc.openid == "A3_FindBesto";
				if (flag5)
				{
					gameObject2.transform.FindChild("Text").GetComponent<Text>().text = "藏宝图兑换";
				}
				bool flag6 = npc.openid == "a3_resetlvl";
				if (flag6)
				{
					gameObject2.transform.FindChild("Text").GetComponent<Text>().text = "转生";
				}
				bool flag7 = npc.openid == "A3_Smithy";
				if (flag7)
				{
					gameObject2.transform.FindChild("Text").GetComponent<Text>().text = "铁匠铺";
				}
				bool flag8 = npc.openid == "a3_npc_shop";
				if (flag8)
				{
					gameObject2.transform.FindChild("Text").GetComponent<Text>().text = "NPC商店";
					this.npc_id = npc.id;
				}
				bool flag9 = npc.openid == "a3_legion_dart";
				if (flag9)
				{
					gameObject2.transform.FindChild("Text").GetComponent<Text>().text = "军团运镖";
				}
				gameObject2.transform.SetParent(this.conOption, false);
				gameObject2.SetActive(true);
				BaseButton baseButton2 = new BaseButton(gameObject2.transform, 1, 1);
				baseButton2.onClick = new Action<GameObject>(this.OnOptionBtnClick);
				baseButton2.gameObject.name = npc.openid;
			}
		}

		private void OnOptionBtnClick(GameObject go)
		{
			dialog.next();
			int taskId = 0;
			bool flag = int.TryParse(go.name, out taskId);
			if (flag)
			{
				List<string> dialogkDesc = this.tkModel.GetDialogkDesc(taskId);
				this.tkModel.curTask = this.tkModel.GetTaskDataById(taskId);
				dialog.showTalk(dialogkDesc, null, dialog.m_npc, false);
			}
			else
			{
				string name = go.name;
				bool flag2 = name == "a3_npc_shop";
				if (flag2)
				{
					List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("npc_shop.npc_shop", "npc_id==" + this.npc_id);
					ModelBase<A3_NPCShopModel>.getInstance().listNPCShop.Clear();
					ModelBase<A3_NPCShopModel>.getInstance().listNPCShop = sXMLList;
					BaseProxy<A3_NPCShopProxy>.getInstance().sendShowFloat((uint)sXMLList[0].getInt("shop_id"));
				}
				bool flag3 = name == "a3_legion_dart";
				if (flag3)
				{
					bool flag4 = ModelBase<A3_LegionModel>.getInstance().myLegion.id == 0;
					if (flag4)
					{
						flytxt.instance.fly("您当前没有军团", 0, default(Color), null);
						return;
					}
				}
				InterfaceMgr.getInstance().open(name, null, false);
			}
		}
	}
}
