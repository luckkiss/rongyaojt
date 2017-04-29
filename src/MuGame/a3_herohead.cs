using GameFramework;
using MuGame.Qsmy.model;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_herohead : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_herohead.<>c <>9 = new a3_herohead.<>c();

			public static Action<GameObject> <>9__14_0;

			internal void <init>b__14_0(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SUMMON, null, false);
			}
		}

		public int exp_time;

		public bool isclear;

		private GameObject Buff_idfo_btn;

		private GameObject Buff_info;

		private GameObject item;

		private Transform con;

		private GameObject item_text;

		private Transform con_text;

		public static a3_herohead instance;

		private Image sum_hp;

		private Image sum_sm;

		private Image sum_cd;

		public HudunModel hudunModel = ModelBase<HudunModel>.getInstance();

		public A3_HudunProxy HudunProxy = BaseProxy<A3_HudunProxy>.getInstance();

		public uint auto_AddHudun = 0u;

		public uint waitTime_baotu = 0u;

		public uint waitTime = 0u;

		public bool do_sum_CD = false;

		public override void init()
		{
			base.alain();
			a3_herohead.instance = this;
			this.sum_hp = base.getComponentByPath<Image>("info/summon/bg/hp");
			this.sum_sm = base.getComponentByPath<Image>("info/summon/bg/sm");
			this.sum_cd = base.getComponentByPath<Image>("info/summon/bg/cd");
			BaseButton arg_77_0 = new BaseButton(base.transform.FindChild("info/summon/bg"), 1, 1);
			Action<GameObject> arg_77_1;
			if ((arg_77_1 = a3_herohead.<>c.<>9__14_0) == null)
			{
				arg_77_1 = (a3_herohead.<>c.<>9__14_0 = new Action<GameObject>(a3_herohead.<>c.<>9.<init>b__14_0));
			}
			arg_77_0.onClick = arg_77_1;
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_BUFF, null, false);
		}

		public override void onShowed()
		{
			this.refresh_sumbar();
		}

		public void Add_energy_auto(uint time, bool isAuto)
		{
			if (isAuto)
			{
				this.auto_AddHudun = time;
				base.CancelInvoke("autoTimeGo");
				base.InvokeRepeating("autoTimeGo", 0f, 1f);
			}
		}

		private void autoTimeGo()
		{
			bool is_auto = this.hudunModel.is_auto;
			if (is_auto)
			{
				bool flag = this.hudunModel.isNoAttack && this.hudunModel.Level > 0;
				if (flag)
				{
					this.auto_AddHudun -= 1u;
					bool flag2 = this.auto_AddHudun <= 0u;
					if (flag2)
					{
						this.auto_AddHudun = 0u;
						this.HudunProxy.Add_energy_auto();
						base.CancelInvoke("autoTimeGo");
						this.Add_energy_auto(this.hudunModel.auto_time, this.hudunModel.is_auto);
					}
				}
				else
				{
					this.wait_attack(this.hudunModel.noAttackTime);
					base.CancelInvoke("autoTimeGo");
				}
			}
			else
			{
				base.CancelInvoke("autoTimeGo");
			}
		}

		public void wait_attack_baotu(uint time)
		{
			this.waitTime_baotu = time;
			base.CancelInvoke("wait_Time_baotu");
			base.InvokeRepeating("wait_Time_baotu", 0f, 1f);
		}

		private void wait_Time_baotu()
		{
			this.waitTime_baotu -= 1u;
			bool flag = this.waitTime_baotu <= 0u;
			if (flag)
			{
				this.waitTime_baotu = 0u;
				base.CancelInvoke("wait_Time_baotu");
				ModelBase<FindBestoModel>.getInstance().Canfly = true;
			}
		}

		public void wait_attack(uint time)
		{
			this.waitTime = time;
			base.CancelInvoke("wait_Time");
			base.InvokeRepeating("wait_Time", 0f, 1f);
		}

		private void wait_Time()
		{
			this.waitTime -= 1u;
			bool flag = this.waitTime <= 0u;
			if (flag)
			{
				this.waitTime = 0u;
				base.CancelInvoke("wait_Time");
				this.hudunModel.isNoAttack = true;
				bool flag2 = this.hudunModel.is_auto && this.hudunModel.Level > 0;
				if (flag2)
				{
					this.HudunProxy.Add_energy_auto();
					this.Add_energy_auto(this.hudunModel.auto_time, this.hudunModel.is_auto);
				}
			}
		}

		private void onHudun(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_HUDUN, null, false);
		}

		public void refresh_sumHp(int cur, int max)
		{
			this.sum_hp.fillAmount = (float)cur / (float)max / 2f;
		}

		public void refresh_sumSm(int cur, int max)
		{
			this.sum_sm.fillAmount = (float)cur / (float)max / 2f;
		}

		public void refresh_sumbar()
		{
			bool flag = ModelBase<A3_SummonModel>.getInstance().GetSummons().Count <= 0;
			if (flag)
			{
				base.transform.FindChild("info/summon").gameObject.SetActive(false);
			}
			else
			{
				base.transform.FindChild("info/summon").gameObject.SetActive(true);
				base.transform.FindChild("info/summon/bg/icon").gameObject.SetActive(false);
				base.transform.FindChild("info/summon/bg/add").gameObject.SetActive(false);
				this.sum_cd.gameObject.SetActive(false);
				bool flag2 = ModelBase<A3_SummonModel>.getInstance().nowShowAttackID > 0u && ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(ModelBase<A3_SummonModel>.getInstance().nowShowAttackID);
				if (flag2)
				{
					a3_BagItemData a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[ModelBase<A3_SummonModel>.getInstance().nowShowAttackID];
					int tpid = a3_BagItemData.summondata.tpid;
					SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + tpid);
					base.transform.FindChild("info/summon/bg/icon").GetComponent<Image>().sprite = (Resources.Load("icon/sm_ft_head/" + sXML.getString("icon_file"), typeof(Sprite)) as Sprite);
					base.transform.FindChild("info/summon/bg/icon").gameObject.SetActive(true);
					this.refresh_sumSm(a3_BagItemData.summondata.lifespan, 100);
				}
				else
				{
					bool flag3 = ModelBase<A3_SummonModel>.getInstance().lastatkID > 0u && ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(ModelBase<A3_SummonModel>.getInstance().lastatkID);
					if (flag3)
					{
						a3_BagItemData a3_BagItemData2 = ModelBase<A3_SummonModel>.getInstance().GetSummons()[ModelBase<A3_SummonModel>.getInstance().lastatkID];
						this.refresh_sumSm(a3_BagItemData2.summondata.lifespan, 100);
						this.refresh_sumHp(0, 1);
						base.transform.FindChild("info/summon/bg/icon").gameObject.SetActive(true);
						this.sum_cd.gameObject.SetActive(true);
						this.do_sum_CD = true;
					}
					else
					{
						this.refresh_sumSm(0, 100);
						this.refresh_sumHp(0, 1);
						base.transform.FindChild("info/summon/bg/add").gameObject.SetActive(true);
					}
				}
			}
		}

		public void refresh_sumCd(float time)
		{
			this.sum_cd.fillAmount = time / 20f;
			this.sum_cd.transform.FindChild("time").GetComponent<Text>().text = ((int)time).ToString();
			bool flag = time <= 0f;
			if (flag)
			{
				this.sum_cd.gameObject.SetActive(false);
				this.do_sum_CD = false;
				BaseProxy<A3_SummonProxy>.getInstance().sendGoAttack((int)ModelBase<A3_SummonModel>.getInstance().lastatkID);
			}
		}

		private void clear()
		{
			for (int i = 0; i < this.con_text.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.con_text.GetChild(i).gameObject);
			}
		}

		private void onHpLowerSliderChange(float v)
		{
			ModelBase<AutoPlayModel>.getInstance().NHpLower = (int)v;
		}

		private void onMpLowerSliderChange(float v)
		{
			ModelBase<AutoPlayModel>.getInstance().NMpLower = (int)v;
		}

		private void Update()
		{
			bool flag = this.do_sum_CD && ModelBase<A3_SummonModel>.getInstance().getSumCds().ContainsKey((int)ModelBase<A3_SummonModel>.getInstance().lastatkID);
			if (flag)
			{
				this.refresh_sumCd(ModelBase<A3_SummonModel>.getInstance().getSumCds()[(int)ModelBase<A3_SummonModel>.getInstance().lastatkID]);
			}
			bool keyDown = Input.GetKeyDown(KeyCode.Escape);
			if (keyDown)
			{
				GameSdkMgr.record_quit();
			}
		}
	}
}
