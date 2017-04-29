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
	internal class A3_EliteMonster : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly A3_EliteMonster.<>c <>9 = new A3_EliteMonster.<>c();

			public static Action<bool> <>9__39_0;

			public static Action<bool> <>9__39_1;

			public static Action<bool> <>9__39_2;

			internal void ctor>b__39_0(bool show)
			{
				if (show)
				{
					bool flag = !A3_EliteMonster.Instance.IsInvoking("GetFirstEliteSelected");
					if (flag)
					{
						A3_EliteMonster.Instance.InvokeRepeating("GetFirstEliteSelected", 0f, 0.2f);
					}
				}
				bool flag2 = ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo.Count > 0;
				if (flag2)
				{
					uint num = new List<uint>(ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo.Keys)[0];
					EliteMonsterPage.Instance.CreateModel(XMLMgr.instance.GetSXML("monsters.monsters", "id==" + ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo[num].MonId).getString("elite_obj"), num);
				}
				A3_EliteMonster.Instance.tabWild.FindChild("isOn").gameObject.SetActive(show);
				EliteMonsterPage.Instance.owner.SetActive(show);
				EliteMonsterPage.Instance.HideOrShowModel(show);
				GameObject expr_F9 = A3_EliteMonster.Instance.background;
				if (expr_F9 != null)
				{
					expr_F9.SetActive(false);
				}
				Transform expr_110 = A3_EliteMonster.Instance.tfRewardParent;
				if (expr_110 != null)
				{
					expr_110.gameObject.SetActive(true);
				}
			}

			internal void ctor>b__39_1(bool show)
			{
				if (show)
				{
					bool flag = !A3_EliteMonster.Instance.IsInvoking("GetFirstBossSelected");
					if (flag)
					{
						A3_EliteMonster.Instance.InvokeRepeating("GetFirstBossSelected", 0f, 0.2f);
					}
				}
				A3_EliteMonster.Instance.tabBoss.FindChild("isOn").gameObject.SetActive(show);
				BossPage.Instance.OnShowed();
				BossPage.Instance.owner.SetActive(show);
				BossPage.Instance.InitModel(show);
				BossPage.Instance.HideOrShowModel(show);
				GameObject expr_94 = A3_EliteMonster.Instance.background;
				if (expr_94 != null)
				{
					expr_94.SetActive(false);
				}
				Transform expr_AB = A3_EliteMonster.Instance.tfRewardParent;
				if (expr_AB != null)
				{
					expr_AB.gameObject.SetActive(true);
				}
			}

			internal void ctor>b__39_2(bool show)
			{
				A3_EliteMonster.Instance.tabReport.FindChild("isOn").gameObject.SetActive(show);
				ReportMessagePage.Instance.owner.SetActive(show);
				GameObject expr_3C = A3_EliteMonster.Instance.background;
				if (expr_3C != null)
				{
					expr_3C.SetActive(true);
				}
				Transform expr_53 = A3_EliteMonster.Instance.tfRewardParent;
				if (expr_53 != null)
				{
					expr_53.gameObject.SetActive(false);
				}
			}
		}

		private Transform tabWild;

		private Transform tabBoss;

		private Transform tabReport;

		private static A3_EliteMonster instance;

		private Toggle toggleWild;

		private Toggle toggleCave;

		private Toggle toggleReport;

		private Toggle[] toggle;

		private GameObject ig_block;

		private GameObject background;

		private GameObject prefabRewardItemIcon;

		private bool switchStatu;

		private Transform tfRewardShow;

		private Transform tfRewardParent;

		private Action<bool>[] fnShow;

		private bool Toclose;

		public uint CurrentSelectedMonsterId
		{
			get;
			set;
		}

		public static A3_EliteMonster Instance
		{
			get
			{
				bool flag = A3_EliteMonster.instance == null;
				if (flag)
				{
					A3_EliteMonster.instance = new A3_EliteMonster();
					A3_EliteMonster.instance.init();
				}
				return A3_EliteMonster.instance;
			}
			set
			{
				A3_EliteMonster.instance = value;
			}
		}

		private bool switchEltmonRspnTmn
		{
			set
			{
				bool flag = this.switchStatu == value;
				if (!flag)
				{
					this.switchStatu = value;
					if (value)
					{
						base.InvokeRepeating("TimingEltMonRspnTm", 0f, 1f);
					}
					else
					{
						base.CancelInvoke("TimingEltMonRspnTm");
					}
				}
			}
		}

		public void TurnOnEliteMonTimer()
		{
			this.switchEltmonRspnTmn = true;
		}

		public void TurnOffEliteMonTimer()
		{
			this.switchEltmonRspnTmn = false;
		}

		public override void init()
		{
			A3_EliteMonster.Instance = this;
			new BaseButton(base.transform.FindChild("btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.Toclose = true;
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_ELITEMON);
			};
			ReportMessagePage reportMessagePage = ReportMessagePage.Instance;
			EliteMonsterPage eliteMonsterPage = EliteMonsterPage.Instance;
			BossPage bossPage = BossPage.Instance;
			(this.ig_block = base.transform.FindChild("ig_bg_bg").gameObject).SetActive(false);
			this.background = base.transform.FindChild("background").gameObject;
			this.prefabRewardItemIcon = base.transform.FindChild("Template/rewardItemIcon").gameObject;
			this.tfRewardParent = base.transform.FindChild("rewardItem");
			this.tfRewardShow = base.transform.FindChild("rewardItem/itemMask/items");
			(this.toggleWild = (this.tabWild = base.transform.FindChild("con_tab/view/con/wild")).transform.GetComponent<Toggle>()).onValueChanged.AddListener(delegate(bool isOn)
			{
				this.fnShow[0](isOn);
			});
			(this.toggleCave = (this.tabBoss = base.transform.FindChild("con_tab/view/con/cave")).transform.GetComponent<Toggle>()).onValueChanged.AddListener(delegate(bool isOn)
			{
				this.fnShow[1](isOn);
			});
			(this.toggleReport = (this.tabReport = base.transform.FindChild("con_tab/view/con/report")).transform.GetComponent<Toggle>()).onValueChanged.AddListener(delegate(bool isOn)
			{
				this.fnShow[2](isOn);
			});
			this.toggle = new Toggle[]
			{
				this.toggleWild,
				this.toggleCave,
				this.toggleReport
			};
		}

		private void OnBossStatuRefresh(GameEvent e)
		{
			Variant expr_11 = e.data["boss_status"];
			List<Variant> list = (expr_11 != null) ? expr_11._arr : null;
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = list[i].ContainsKey("index") && list[i].ContainsKey("status");
				if (flag)
				{
					int num = list[i]["index"];
					int num2 = list[i]["status"];
					bool flag2 = BossPage.Instance.btnBoss1_transmit.gameObject && BossPage.Instance.textBoss1_RspnLftTm.gameObject;
					if (flag2)
					{
						switch (num)
						{
						case 1:
						{
							bool flag3 = num2 == 1;
							if (flag3)
							{
								BossPage.Instance.btnBoss1_transmit.gameObject.SetActive(true);
								BossPage.Instance.textBoss1_RspnLftTm.gameObject.SetActive(false);
							}
							else
							{
								BossPage.Instance.btnBoss1_transmit.gameObject.SetActive(false);
								BossPage.Instance.textBoss1_RspnLftTm.gameObject.SetActive(true);
							}
							break;
						}
						case 2:
						{
							bool flag4 = num2 == 1;
							if (flag4)
							{
								BossPage.Instance.btnBoss2_transmit.gameObject.SetActive(true);
								BossPage.Instance.textBoss2_RspnLftTm.gameObject.SetActive(false);
							}
							else
							{
								BossPage.Instance.btnBoss2_transmit.gameObject.SetActive(false);
								BossPage.Instance.textBoss2_RspnLftTm.gameObject.SetActive(true);
							}
							break;
						}
						case 3:
						{
							bool flag5 = num2 == 1;
							if (flag5)
							{
								BossPage.Instance.btnBoss3_transmit.gameObject.SetActive(true);
								BossPage.Instance.textBoss3_RspnLftTm.gameObject.SetActive(false);
							}
							else
							{
								BossPage.Instance.btnBoss3_transmit.gameObject.SetActive(false);
								BossPage.Instance.textBoss3_RspnLftTm.gameObject.SetActive(true);
							}
							break;
						}
						}
					}
				}
			}
		}

		public override void onShowed()
		{
			this.Toclose = false;
			A3_EliteMonster.Instance = this;
			BaseProxy<EliteMonsterProxy>.getInstance().addEventListener(EliteMonsterProxy.EVENT_ELITEMONSTER, new Action<GameEvent>(this.OnEliteMonInfoRefresh));
			BaseProxy<EliteMonsterProxy>.getInstance().SendProxy();
			BaseProxy<A3_ActiveProxy>.getInstance().addEventListener(EliteMonsterProxy.EVENT_BOSSOPSUCCESS, new Action<GameEvent>(this.OnBossStatuRefresh));
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			GRMap.GAME_CAMERA.SetActive(false);
			bool flag = this.uiData != null;
			if (flag)
			{
				this.fnShow[(int)this.uiData[0]](this.toggle[(int)this.uiData[0]].isOn = true);
				this.uiData = null;
			}
			else
			{
				this.fnShow[0](this.toggleWild.isOn = true);
				bool flag2 = ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo.Count > 0;
				if (flag2)
				{
					bool flag3 = base.IsInvoking("GetFirstEliteSelected");
					if (flag3)
					{
						base.InvokeRepeating("GetFirstEliteSelected", 0f, 0.2f);
					}
					uint num = ModelBase<A3_EliteMonsterModel>.getInstance().GetSortedMonInfoIdList()[0];
					EliteMonsterPage.Instance.CreateModel(XMLMgr.instance.GetSXML("monsters.monsters", "id==" + ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo[num].MonId).getString("elite_obj"), num);
				}
			}
		}

		private void GetFirstEliteSelected()
		{
			bool flag = EliteMonsterPage.Instance.goMonInfoScroll.transform.childCount > 0;
			if (flag)
			{
				GameObject expr_2A = EliteMonsterPage.Instance.curSelected;
				if (expr_2A != null)
				{
					expr_2A.SetActive(false);
				}
				(EliteMonsterPage.Instance.curSelected = EliteMonsterPage.Instance.goMonInfoScroll.transform.GetChild(0).FindChild("bg/selected").gameObject).SetActive(true);
				List<uint> sortedMonInfoIdList = ModelBase<A3_EliteMonsterModel>.getInstance().GetSortedMonInfoIdList();
				bool flag2 = sortedMonInfoIdList.Count > 0;
				if (flag2)
				{
					EliteMonsterPage.Instance.ShowReward(this.CurrentSelectedMonsterId = sortedMonInfoIdList[0]);
				}
				base.CancelInvoke("GetFirstEliteSelected");
			}
		}

		private void GetFirstBossSelected()
		{
			GameObject expr_0B = BossPage.Instance.pboss1;
			bool flag = expr_0B != null && expr_0B.activeSelf;
			if (flag)
			{
				GameObject expr_26 = BossPage.Instance.curSelected;
				if (expr_26 != null)
				{
					expr_26.SetActive(false);
				}
				(BossPage.Instance.curSelected = BossPage.Instance.btnBoss[0].transform.FindChild("selected").gameObject).SetActive(true);
				EliteMonsterPage.Instance.ShowReward(this.CurrentSelectedMonsterId = BossPage.Instance.FirstBossId);
				base.CancelInvoke("GetFirstBossSelected");
			}
		}

		public void Update()
		{
			BossPage.Instance.Update();
		}

		public override void onClosed()
		{
			BossPage.Instance.OnClosed();
			BaseProxy<EliteMonsterProxy>.getInstance().removeEventListener(EliteMonsterProxy.EVENT_ELITEMONSTER, new Action<GameEvent>(this.OnEliteMonInfoRefresh));
			this.TurnOffEliteMonTimer();
			EliteMonsterPage.Instance.DestroyModel();
			BossPage.Instance.DestroyModel();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			A3_EliteMonster.Instance = null;
			InterfaceMgr.getInstance().itemToWin(this.Toclose, this.uiName);
			bool flag = a3_getJewelryWay.instance && a3_getJewelryWay.instance.closeWin != null && this.Toclose;
			if (flag)
			{
				InterfaceMgr.getInstance().open(a3_getJewelryWay.instance.closeWin, null, false);
				a3_getJewelryWay.instance.closeWin = null;
			}
			BaseProxy<A3_ActiveProxy>.getInstance().removeEventListener(EliteMonsterProxy.EVENT_BOSSOPSUCCESS, new Action<GameEvent>(this.OnBossStatuRefresh));
		}

		private void OnEliteMonInfoRefresh(GameEvent e)
		{
			bool flag = e.data.ContainsKey("elite_mon");
			if (flag)
			{
				List<Variant> arr = e.data["elite_mon"]._arr;
				EliteMonsterInfo monInfo;
				for (int i = 0; i < arr.Count; i++)
				{
					uint monId = arr[i]["mid"]._uint;
					monInfo = ModelBase<A3_EliteMonsterModel>.getInstance().AddData(arr[i]);
					bool flag2 = arr[i].ContainsKey("killer_name");
					if (flag2)
					{
						bool flag3 = arr[i]["killer_name"]._str.Length == 0;
						if (flag3)
						{
							EliteMonsterPage.Instance.RefreshMonInfo(monId, 0u);
						}
						else
						{
							ReportMessagePage.Instance.AddReportMessage(monInfo.date, monInfo.killerName, XMLMgr.instance.GetSXML("monsters.monsters", "id==" + monId).getString("name"), () => ReportMessagePage.Instance.AddReportInfo(monId, monInfo), true);
							EliteMonsterPage.Instance.RefreshMonInfo(monId, monInfo.unRespawnTime);
						}
					}
				}
			}
		}

		private void TimingEltMonRspnTm()
		{
			EliteMonsterPage expr_05 = EliteMonsterPage.Instance;
			if (expr_05 != null)
			{
				expr_05.ShowLeftTime();
			}
		}

		public void ShowRewardItemIcon(uint? monId = null)
		{
			bool flag = this.tfRewardShow != null;
			if (flag)
			{
				bool flag2 = !monId.HasValue;
				if (flag2)
				{
					monId = new uint?(this.CurrentSelectedMonsterId);
				}
				this.ClearRewardItemIcon();
				for (int i = 0; i < EliteMonsterInfo.poolItemReward[monId.Value].Count; i++)
				{
					List<uint> list = EliteMonsterInfo.poolItemReward[monId.Value];
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRewardItemIcon);
					GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(list[i], true, -1, 1f, false, -1, 0, false, false, true, false);
					this.AddFnShowRewardTip(list[i], gameObject.transform.FindChild("btn").gameObject);
					gameObject2.transform.SetParent(gameObject.transform, false);
					gameObject2.transform.SetSiblingIndex(gameObject.transform.FindChild("btn").GetSiblingIndex());
					gameObject.transform.SetParent(this.tfRewardShow, false);
					gameObject.SetActive(true);
				}
			}
			else
			{
				Debug.LogError("预制件不存在或已经更改");
			}
		}

		private void AddFnShowRewardTip(uint itemId, GameObject btn)
		{
			new BaseButton(btn.transform, 1, 1).onClick = delegate(GameObject go)
			{
				SXML sXML = XMLMgr.instance.GetSXML("worldboss.mdrop", "mid==" + this.CurrentSelectedMonsterId);
				bool flag = sXML != null;
				if (flag)
				{
					SXML node = sXML.GetNode("item", "id==" + itemId);
					int @int = node.getInt("type");
					RewardDescText? rewardDescText = null;
					bool flag2 = @int == 3;
					if (flag2)
					{
						bool flag3 = sXML != null;
						if (flag3)
						{
							SXML node2 = sXML.GetNode("item", "id==" + itemId);
							rewardDescText = new RewardDescText?(new RewardDescText
							{
								strItemName = node2.GetNode("item_name", "").getString("tip"),
								strTipDesc = "",
								strCarrLimit = node2.GetNode("carr_limit", "").getString("tip"),
								strBaseAttr = node2.GetNode("desc1", "").getString("tip"),
								strAddAttr = node2.GetNode("desc2", "").getString("tip"),
								strExtraDesc1 = node2.GetNode("random_tip1", "").getString("tip"),
								strExtraDesc2 = node2.GetNode("random_tip2", "").getString("tip"),
								strExtraDesc3 = node2.GetNode("random_tip3", "").getString("tip")
							});
						}
					}
					ArrayList arrayList = new ArrayList();
					arrayList.Add(itemId);
					arrayList.Add(@int);
					arrayList.Add(rewardDescText);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
				}
			};
		}

		private void ClearRewardItemIcon()
		{
			bool flag = this.tfRewardShow != null;
			if (flag)
			{
				for (int i = this.tfRewardShow.childCount - 1; i >= 0; i--)
				{
					UnityEngine.Object.Destroy(this.tfRewardShow.GetChild(i).gameObject);
				}
			}
		}

		public A3_EliteMonster()
		{
			Action<bool>[] expr_07 = new Action<bool>[3];
			int arg_28_1 = 0;
			Action<bool> arg_28_2;
			if ((arg_28_2 = A3_EliteMonster.<>c.<>9__39_0) == null)
			{
				arg_28_2 = (A3_EliteMonster.<>c.<>9__39_0 = new Action<bool>(A3_EliteMonster.<>c.<>9.<.ctor>b__39_0));
			}
			expr_07[arg_28_1] = arg_28_2;
			int arg_4A_1 = 1;
			Action<bool> arg_4A_2;
			if ((arg_4A_2 = A3_EliteMonster.<>c.<>9__39_1) == null)
			{
				arg_4A_2 = (A3_EliteMonster.<>c.<>9__39_1 = new Action<bool>(A3_EliteMonster.<>c.<>9.<.ctor>b__39_1));
			}
			expr_07[arg_4A_1] = arg_4A_2;
			int arg_6C_1 = 2;
			Action<bool> arg_6C_2;
			if ((arg_6C_2 = A3_EliteMonster.<>c.<>9__39_2) == null)
			{
				arg_6C_2 = (A3_EliteMonster.<>c.<>9__39_2 = new Action<bool>(A3_EliteMonster.<>c.<>9.<.ctor>b__39_2));
			}
			expr_07[arg_6C_1] = arg_6C_2;
			this.fnShow = expr_07;
			this.Toclose = false;
			base..ctor();
		}
	}
}
