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
	internal class A3_SlayDragon : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly A3_SlayDragon.<>c <>9 = new A3_SlayDragon.<>c();

			public static Action<GameObject> <>9__9_1;

			internal void <init>b__9_1(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SLAY_DRAGON);
			}
		}

		public static A3_SlayDragon Instance;

		public Transform rootDragonList;

		public Transform rootDragonOpt;

		public GameObject goDragonHelpTxt;

		private bool isOnMoveOpt = false;

		private string currentSelectedDragonName;

		private Text txtTimer;

		private GameObject goTimer;

		private bool isFrstdrgnInit = false;

		public override void init()
		{
			A3_SlayDragon.Instance = this;
			this.rootDragonList = base.transform.FindChild("bg/dragon_list/rect_mask/rect_scroll");
			for (int i = 0; i < this.rootDragonList.childCount; i++)
			{
				Transform child = this.rootDragonList.GetChild(i);
				ModelBase<A3_SlayDragonModel>.getInstance().dicDragonName[i] = child.name;
				new BaseButton(child, 1, 1).onClick = new Action<GameObject>(this.OnDragonLineClick);
			}
			this.goDragonHelpTxt = base.transform.FindChild("hp/help_txt").gameObject;
			this.goTimer = base.transform.FindChild("bg/dragon_opt/timer").gameObject;
			this.txtTimer = this.goTimer.transform.FindChild("time").GetComponent<Text>();
			new BaseButton(base.transform.FindChild("bg/dragon_opt/btn_do/Go"), 1, 1).onClick = new Action<GameObject>(this.OnGoToSlayDragon);
			new BaseButton(base.transform.FindChild("bg/dragon_opt/btn_do/Unlock"), 1, 1).onClick = new Action<GameObject>(this.OnUnlockDragon);
			new BaseButton(base.transform.FindChild("bg/dragon_opt/btn_do/Create"), 1, 1).onClick = delegate(GameObject go)
			{
				this.OnCreateDragon(go);
				this.OnGoToSlayDragon(go);
			};
			new BaseButton(base.transform.FindChild("bg/dragon_opt/proc_unlock/btn_give"), 1, 1).onClick = new Action<GameObject>(this.OnGive);
			BaseButton arg_1A7_0 = new BaseButton(base.transform.FindChild("close"), 1, 1);
			Action<GameObject> arg_1A7_1;
			if ((arg_1A7_1 = A3_SlayDragon.<>c.<>9__9_1) == null)
			{
				arg_1A7_1 = (A3_SlayDragon.<>c.<>9__9_1 = new Action<GameObject>(A3_SlayDragon.<>c.<>9.<init>b__9_1));
			}
			arg_1A7_0.onClick = arg_1A7_1;
			new BaseButton(base.transform.FindChild("hp"), 1, 1).onClick = delegate(GameObject go)
			{
				this.goDragonHelpTxt.SetActive(true);
			};
			new BaseButton(this.goDragonHelpTxt.transform.FindChild("close_area"), 1, 1).onClick = delegate(GameObject go)
			{
				this.goDragonHelpTxt.SetActive(false);
			};
			this.rootDragonOpt = base.transform.FindChild("bg/dragon_opt");
			new BaseButton(this.rootDragonOpt.FindChild("reward/reward_icon"), 1, 1).onClick = delegate(GameObject go)
			{
				uint dragonId = ModelBase<A3_SlayDragonModel>.getInstance().dicDragonData[this.currentSelectedDragonName].dragonId;
				uint rewardIdByDragonId = ModelBase<A3_SlayDragonModel>.getInstance().GetRewardIdByDragonId(dragonId);
				bool flag = rewardIdByDragonId == 0u;
				if (!flag)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(rewardIdByDragonId);
					arrayList.Add(1);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
				}
			};
			BaseProxy<A3_SlayDragonProxy>.getInstance().addEventListener(A3_SlayDragonProxy.REFRESH, new Action<GameEvent>(this.OnRefresh));
			BaseProxy<A3_SlayDragonProxy>.getInstance().addEventListener(A3_SlayDragonProxy.OPEN_LVL, new Action<GameEvent>(this.OnOpenLvl));
		}

		private void ShowFirstDragon()
		{
			this.OnDragonLineClick(this.rootDragonList.GetChild(0).gameObject);
			this.isFrstdrgnInit = true;
		}

		public override void onShowed()
		{
			A3_SlayDragon.Instance = this;
			BaseProxy<A3_SlayDragonProxy>.getInstance().addEventListener(A3_SlayDragonProxy.END_TIME, new Action<GameEvent>(this.OnRefreshTime));
			bool flag = !base.IsInvoking("RunTimer");
			if (flag)
			{
				base.InvokeRepeating("RunTimer", 0f, 1f);
			}
			base.Invoke("ShowFirstDragon", 0.2f);
			BaseProxy<A3_SlayDragonProxy>.getInstance().SendGetData();
		}

		public override void onClosed()
		{
			BaseProxy<A3_SlayDragonProxy>.getInstance().removeEventListener(A3_SlayDragonProxy.END_TIME, new Action<GameEvent>(this.OnRefreshTime));
			this.OnDragonLineClick(this.rootDragonList.GetChild(0).gameObject);
			base.CancelInvoke("RunTimer");
			this.isFrstdrgnInit = false;
			A3_SlayDragon.Instance = null;
		}

		private void OnRefreshTime(GameEvent e)
		{
			bool flag = !e.data.ContainsKey("end_time") || e.data["end_time"] == 0;
			if (!flag)
			{
				ModelBase<A3_SlayDragonModel>.getInstance().GetUnlockedDragonData().endTimeStamp = e.data["end_time"];
				bool flag2 = !base.IsInvoking("RunTimer");
				if (flag2)
				{
					base.InvokeRepeating("RunTimer", 0f, 1f);
				}
			}
		}

		private void RunTimer()
		{
			A3_SlayDragonModel expr_06 = ModelBase<A3_SlayDragonModel>.getInstance();
			DragonData dragonData = (expr_06 != null) ? expr_06.GetUnlockedDragonData() : null;
			bool flag = this.goTimer.activeSelf && dragonData != null && dragonData.endTimeStamp != 0L;
			if (flag)
			{
				long num = (long)muNetCleint.instance.CurServerTimeStamp;
				long num2 = dragonData.endTimeStamp - num;
				bool flag2 = num2 > 0L;
				if (flag2)
				{
					this.txtTimer.text = string.Format("{0:D2}:{1:D2}", num2 % 3600L / 60L, num2 % 60L);
				}
				else
				{
					this.txtTimer.text = "00:00";
				}
			}
		}

		private void OnRefresh(GameEvent e)
		{
			bool flag = !e.data.ContainsKey("tulong_lvl_ary");
			if (!flag)
			{
				ModelBase<A3_SlayDragonModel>.getInstance().SyncData(e.data);
				bool flag2 = A3_SlayDragon.Instance != null && this.currentSelectedDragonName == null;
				if (flag2)
				{
					this.OnDragonLineClick(this.rootDragonList.GetChild(0).gameObject);
				}
				List<Variant> arr = e.data["tulong_lvl_ary"]._arr;
				uint idByName = ModelBase<A3_SlayDragonModel>.getInstance().GetIdByName(this.currentSelectedDragonName);
				for (int i = 0; i < arr.Count; i++)
				{
					uint num = arr[i]["lvl_id"];
					bool flag3 = arr[i]["zhaohuan"];
					bool flag4 = (idByName != 0u && num == idByName) & flag3;
					if (flag4)
					{
						this.RefreshDragonInfo(this.currentSelectedDragonName);
					}
					string nameById = ModelBase<A3_SlayDragonModel>.getInstance().GetNameById(num);
					Transform transform = this.rootDragonList.FindChild(nameById);
					bool flag5 = arr[i]["death"];
					bool flag6 = arr[i]["open"];
					bool flag7 = arr[i]["create_tm"];
					transform.FindChild("state/killed").gameObject.SetActive(flag5);
					transform.FindChild("state/unlocked").gameObject.SetActive(flag3 && !flag5);
					transform.FindChild("state/locked").gameObject.SetActive(!flag3 && !flag5);
				}
				bool flag8 = !this.isFrstdrgnInit && InterfaceMgr.getInstance().checkWinOpened(InterfaceMgr.A3_SLAY_DRAGON);
				if (flag8)
				{
					this.ShowFirstDragon();
				}
			}
		}

		private void OnOpenLvl(GameEvent e)
		{
			bool flag = !this.isOnMoveOpt && !ModelBase<PlayerModel>.getInstance().inFb;
			if (flag)
			{
				A3_TaskOpt.Instance.ShowDragonWin();
			}
			this.isOnMoveOpt = false;
		}

		private void OnGoToSlayDragon(GameObject go)
		{
			bool flag = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc < 3 && !ModelBase<A3_SlayDragonModel>.getInstance().GetUnlockedDragonData().isOpened;
			if (flag)
			{
				flytxt.instance.fly("副本还未开启,首领或元老开启副本后才可进入", 0, default(Color), null);
			}
			else
			{
				bool flag2 = ModelBase<A3_SlayDragonModel>.getInstance().GetUnlockedDragonId() > 0u;
				if (flag2)
				{
					this.isOnMoveOpt = true;
					BaseProxy<A3_SlayDragonProxy>.getInstance().SendGo();
				}
				else
				{
					flytxt.instance.fly("请先解除封印", 0, default(Color), null);
				}
			}
		}

		private void OnUnlockDragon(GameObject go)
		{
			bool flag = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc >= 3;
			if (flag)
			{
				bool flag2 = this.currentSelectedDragonName != null;
				if (flag2)
				{
					DragonData dragonData = ModelBase<A3_SlayDragonModel>.getInstance().dicDragonData[this.currentSelectedDragonName];
					bool flag3 = !dragonData.isUnlcoked && ModelBase<A3_SlayDragonModel>.getInstance().GetUnlockedDragonId() == 0u;
					if (flag3)
					{
						BaseProxy<A3_SlayDragonProxy>.getInstance().SendUnlock(dragonData.dragonId);
					}
					else
					{
						flytxt.instance.fly("不能同时开启多条巨龙", 0, default(Color), null);
					}
				}
			}
			else
			{
				flytxt.instance.fly("只有军团领袖或元老可以召唤巨龙", 0, default(Color), null);
			}
		}

		private void OnCreateDragon(GameObject go)
		{
			uint dragonId = ModelBase<A3_SlayDragonModel>.getInstance().dicDragonData[this.currentSelectedDragonName].dragonId;
			int unlockedDiffLv = ModelBase<A3_SlayDragonModel>.getInstance().GetUnlockedDiffLv();
			BaseProxy<A3_SlayDragonProxy>.getInstance().SendCreate(dragonId, unlockedDiffLv);
		}

		private void OnDragonLineClick(GameObject go)
		{
			bool flag = !ModelBase<A3_SlayDragonModel>.getInstance().dicDragonData.ContainsKey(go.name);
			if (!flag)
			{
				for (int i = 0; i < this.rootDragonList.childCount; i++)
				{
					this.rootDragonList.GetChild(i).FindChild("select").gameObject.SetActive(false);
				}
				this.currentSelectedDragonName = go.name;
				bool isUnlcoked = ModelBase<A3_SlayDragonModel>.getInstance().dicDragonData[this.currentSelectedDragonName].isUnlcoked;
				go.transform.FindChild("select").gameObject.SetActive(true);
				bool flag2 = isUnlcoked;
				if (flag2)
				{
					go.transform.FindChild("select/unlocked").gameObject.SetActive(true);
					go.transform.FindChild("select/locked").gameObject.SetActive(false);
				}
				else
				{
					go.transform.FindChild("select/unlocked").gameObject.SetActive(false);
					go.transform.FindChild("select/locked").gameObject.SetActive(true);
				}
				this.RefreshDragonInfo(this.currentSelectedDragonName);
			}
		}

		private void RefreshDragonInfo(string dragonName)
		{
			DragonData dragonData = ModelBase<A3_SlayDragonModel>.getInstance().dicDragonData[dragonName];
			int cost = ModelBase<A3_SlayDragonModel>.getInstance().GetCost(0);
			uint dragonId = dragonData.dragonId;
			uint proc = dragonData.proc;
			bool isUnlcoked = dragonData.isUnlcoked;
			bool isOpened = dragonData.isOpened;
			bool isDead = dragonData.isDead;
			bool flag = dragonData.isCreated && !isDead;
			bool flag2 = flag | isDead;
			if (flag2)
			{
				this.goTimer.SetActive(flag);
				this.rootDragonOpt.FindChild("proc_unlock").gameObject.SetActive(false);
			}
			else
			{
				this.goTimer.SetActive(false);
				this.rootDragonOpt.FindChild("proc_unlock").gameObject.SetActive(true);
				this.rootDragonOpt.FindChild("proc_unlock/proc_text").GetComponent<Text>().text = string.Format("{0}/{1}", proc, cost);
				this.rootDragonOpt.FindChild("proc_unlock/Slider").GetComponent<Slider>().value = proc / (float)cost;
			}
			string value = dragonId.ToString();
			Transform transform = this.rootDragonOpt.FindChild("descBg/desc");
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject gameObject = transform.GetChild(i).gameObject;
				bool flag3 = !gameObject.name.Equals(value);
				if (flag3)
				{
					gameObject.SetActive(false);
				}
				else
				{
					gameObject.SetActive(true);
				}
			}
			bool flag4 = isDead;
			if (flag4)
			{
				this.rootDragonOpt.FindChild("btn_do/Create").gameObject.SetActive(false);
				this.rootDragonOpt.FindChild("btn_do/Unlock").gameObject.SetActive(false);
				this.rootDragonOpt.FindChild("btn_do/Go").GetComponent<Button>().interactable = false;
			}
			else
			{
				bool flag5 = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc < 3;
				if (flag5)
				{
					this.rootDragonOpt.FindChild("btn_do/Create").gameObject.SetActive(false);
				}
				this.rootDragonOpt.FindChild("btn_do/Go").GetComponent<Button>().interactable = true;
				bool flag6 = isUnlcoked;
				if (flag6)
				{
					bool flag7 = !isOpened;
					if (flag7)
					{
						this.rootDragonOpt.FindChild("btn_do/Unlock").gameObject.SetActive(false);
						this.rootDragonOpt.FindChild("btn_do/Create").gameObject.SetActive(!flag);
						this.rootDragonOpt.FindChild("btn_do/Create").GetComponent<Button>().interactable = ((ulong)proc >= (ulong)((long)cost));
					}
					else
					{
						this.rootDragonOpt.FindChild("btn_do/Create").gameObject.SetActive(false);
						this.rootDragonOpt.FindChild("btn_do/Unlock").gameObject.SetActive(false);
						this.rootDragonOpt.FindChild("btn_do/Go").gameObject.SetActive(flag);
					}
				}
				else
				{
					bool flag8 = !flag;
					if (flag8)
					{
						this.rootDragonOpt.FindChild("btn_do/Unlock").gameObject.SetActive(true);
						this.rootDragonOpt.FindChild("btn_do/Unlock").GetComponent<Button>().interactable = ModelBase<A3_SlayDragonModel>.getInstance().IsAbleToUnlock();
						this.rootDragonOpt.FindChild("btn_do/Go").gameObject.SetActive(false);
					}
				}
			}
		}

		private void OnGive(GameObject go)
		{
			DragonData dragonData = ModelBase<A3_SlayDragonModel>.getInstance().dicDragonData[this.currentSelectedDragonName];
			uint unlockedDragonId;
			bool flag = (unlockedDragonId = ModelBase<A3_SlayDragonModel>.getInstance().GetUnlockedDragonId()) != 0u && unlockedDragonId == dragonData.dragonId;
			if (flag)
			{
				uint dragonKeyId = ModelBase<A3_SlayDragonModel>.getInstance().GetDragonKeyId();
				bool flag2 = dragonKeyId > 0u;
				if (flag2)
				{
					bool flag3 = 0 < ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(dragonKeyId);
					if (flag3)
					{
						bool flag4 = (ulong)dragonData.proc < (ulong)((long)ModelBase<A3_SlayDragonModel>.getInstance().GetCost(0));
						if (flag4)
						{
							BaseProxy<A3_SlayDragonProxy>.getInstance().SendGive(1);
						}
						else
						{
							flytxt.instance.fly("已满足召唤条件", 0, default(Color), null);
						}
					}
					else
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add(ModelBase<a3_BagModel>.getInstance().getItemDataById(dragonKeyId));
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMLACK, arrayList, false);
					}
				}
			}
			else
			{
				flytxt.instance.fly("请先解除封印", 0, default(Color), null);
			}
		}
	}
}
