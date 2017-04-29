using Cross;
using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_counterpart_gold : a3BaseActive
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_counterpart_gold.<>c <>9 = new a3_counterpart_gold.<>c();

			public static Action<GameObject> <>9__10_0;

			public static Action<GameObject> <>9__10_1;

			public static Action<GameObject> <>9__10_2;

			public static Action<GameObject> <>9__10_3;

			internal void <init>b__10_0(GameObject go)
			{
				a3_counterpart_gold.open = true;
				Variant variant = new Variant();
				variant["mapid"] = 3335;
				variant["npcid"] = 0;
				variant["ltpid"] = 102;
				variant["diff_lvl"] = 1;
				BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
			}

			internal void <init>b__10_1(GameObject go)
			{
				a3_counterpart_gold.open = true;
				Variant variant = new Variant();
				variant["mapid"] = 3335;
				variant["npcid"] = 0;
				variant["ltpid"] = 102;
				variant["diff_lvl"] = 2;
				BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
			}

			internal void <init>b__10_2(GameObject go)
			{
				a3_counterpart_gold.open = true;
				Variant variant = new Variant();
				variant["mapid"] = 3335;
				variant["npcid"] = 0;
				variant["ltpid"] = 102;
				variant["diff_lvl"] = 3;
				BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
			}

			internal void <init>b__10_3(GameObject go)
			{
				a3_counterpart_gold.open = true;
				Variant variant = new Variant();
				variant["mapid"] = 3335;
				variant["npcid"] = 0;
				variant["ltpid"] = 102;
				variant["diff_lvl"] = 4;
				BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
			}
		}

		public static bool open = false;

		private BaseButton enterbtn;

		private BaseButton enterbtn1;

		private BaseButton enterbtn2;

		private BaseButton enterbtn3;

		private int zhaun = 0;

		private int ji = 0;

		private int pzhuan = 0;

		private int pji = 0;

		public a3_counterpart_gold(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			this.refreshGold();
			this.enterbtn = new BaseButton(base.getTransformByPath("choiceDef/easy"), 1, 1);
			this.enterbtn1 = new BaseButton(base.getTransformByPath("choiceDef/normal"), 1, 1);
			this.enterbtn2 = new BaseButton(base.getTransformByPath("choiceDef/deffi"), 1, 1);
			this.enterbtn3 = new BaseButton(base.getTransformByPath("choiceDef/god"), 1, 1);
			this.pzhuan = (int)ModelBase<PlayerModel>.getInstance().up_lvl;
			this.pji = (int)ModelBase<PlayerModel>.getInstance().lvl;
			this.changeSth();
			BaseButton arg_B4_0 = this.enterbtn;
			Action<GameObject> arg_B4_1;
			if ((arg_B4_1 = a3_counterpart_gold.<>c.<>9__10_0) == null)
			{
				arg_B4_1 = (a3_counterpart_gold.<>c.<>9__10_0 = new Action<GameObject>(a3_counterpart_gold.<>c.<>9.<init>b__10_0));
			}
			arg_B4_0.onClick = arg_B4_1;
			BaseButton arg_DF_0 = this.enterbtn1;
			Action<GameObject> arg_DF_1;
			if ((arg_DF_1 = a3_counterpart_gold.<>c.<>9__10_1) == null)
			{
				arg_DF_1 = (a3_counterpart_gold.<>c.<>9__10_1 = new Action<GameObject>(a3_counterpart_gold.<>c.<>9.<init>b__10_1));
			}
			arg_DF_0.onClick = arg_DF_1;
			BaseButton arg_10A_0 = this.enterbtn2;
			Action<GameObject> arg_10A_1;
			if ((arg_10A_1 = a3_counterpart_gold.<>c.<>9__10_2) == null)
			{
				arg_10A_1 = (a3_counterpart_gold.<>c.<>9__10_2 = new Action<GameObject>(a3_counterpart_gold.<>c.<>9.<init>b__10_2));
			}
			arg_10A_0.onClick = arg_10A_1;
			BaseButton arg_135_0 = this.enterbtn3;
			Action<GameObject> arg_135_1;
			if ((arg_135_1 = a3_counterpart_gold.<>c.<>9__10_3) == null)
			{
				arg_135_1 = (a3_counterpart_gold.<>c.<>9__10_3 = new Action<GameObject>(a3_counterpart_gold.<>c.<>9.<init>b__10_3));
			}
			arg_135_0.onClick = arg_135_1;
		}

		private void refreshGold()
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(102u);
			int num = variant["daily_cnt"];
			int num2 = 0;
			bool flag = MapModel.getInstance().dFbDta.ContainsKey(102);
			if (flag)
			{
				num2 = Mathf.Min(MapModel.getInstance().dFbDta[102].cycleCount, num);
			}
			base.getTransformByPath("choiceDef/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num - num2,
				"/",
				num,
				"</color>"
			});
		}

		private void zhaunJi(int difs)
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(102u);
			this.zhaun = variant["diff_lvl"][difs]["open_zhuan"];
			this.ji = variant["diff_lvl"][difs]["open_level"];
		}

		private void changeSth()
		{
			this.zhaunJi(2);
			bool flag = this.zhaun > this.pzhuan || (this.zhaun == this.pzhuan && this.pji < this.ji);
			if (flag)
			{
				base.getGameObjectByPath("choiceDef/normal/normalText").SetActive(true);
				base.getGameObjectByPath("choiceDef/normal/enter").SetActive(false);
				base.getTransformByPath("choiceDef/normal/normalText").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.zhaun,
					"转",
					this.ji,
					"级开放"
				});
				this.enterbtn1.interactable = false;
			}
			else
			{
				bool flag2 = this.zhaun < this.pzhuan || (this.zhaun == this.pzhuan && this.pji >= this.ji);
				if (flag2)
				{
					base.getGameObjectByPath("choiceDef/normal/normalText").SetActive(false);
					base.getGameObjectByPath("choiceDef/normal/enter").SetActive(true);
					this.enterbtn1.interactable = true;
				}
			}
			this.zhaunJi(3);
			bool flag3 = this.zhaun > this.pzhuan || (this.zhaun == this.pzhuan && this.pji < this.ji);
			if (flag3)
			{
				base.getGameObjectByPath("choiceDef/deffi/deffiText").SetActive(true);
				base.getGameObjectByPath("choiceDef/deffi/enter").SetActive(false);
				base.getTransformByPath("choiceDef/deffi/deffiText").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.zhaun,
					"转",
					this.ji,
					"级开放"
				});
				this.enterbtn2.interactable = false;
			}
			else
			{
				bool flag4 = this.zhaun < this.pzhuan || (this.zhaun == this.pzhuan && this.pji >= this.ji);
				if (flag4)
				{
					base.getGameObjectByPath("choiceDef/deffi/deffiText").SetActive(false);
					base.getGameObjectByPath("choiceDef/deffi/enter").SetActive(true);
					this.enterbtn2.interactable = true;
				}
			}
			this.zhaunJi(4);
			bool flag5 = this.zhaun > this.pzhuan || (this.zhaun == this.pzhuan && this.pji < this.ji);
			if (flag5)
			{
				base.getGameObjectByPath("choiceDef/god/godText").SetActive(true);
				base.getGameObjectByPath("choiceDef/god/enter").SetActive(false);
				base.getTransformByPath("choiceDef/god/godText").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.zhaun,
					"转",
					this.ji,
					"级开放"
				});
				this.enterbtn3.interactable = false;
			}
			else
			{
				bool flag6 = this.zhaun < this.pzhuan || (this.zhaun == this.pzhuan && this.pji >= this.ji);
				if (flag6)
				{
					base.getGameObjectByPath("choiceDef/god/godText").SetActive(false);
					base.getGameObjectByPath("choiceDef/god/enter").SetActive(true);
					this.enterbtn3.interactable = true;
				}
			}
		}

		public override void onShowed()
		{
			base.onShowed();
			this.changeSth();
		}
	}
}
