using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_washredname : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_washredname.<>c <>9 = new a3_washredname.<>c();

			public static Action<GameObject> <>9__5_0;

			internal void <init>b__5_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_WASHREDNAME);
			}
		}

		private GameObject rule;

		private Text now_point;

		private Text des;

		private Text needMoney;

		public static a3_washredname _instance;

		public override void init()
		{
			a3_washredname._instance = this;
			this.rule = base.transform.FindChild("rule_bg").gameObject;
			this.now_point = base.getComponentByPath<Text>("bg_top/txt/now_point");
			this.des = base.getComponentByPath<Text>("bg_top/des");
			this.needMoney = base.getComponentByPath<Text>("bg_downleft/num");
			BaseButton baseButton = new BaseButton(base.transform.FindChild("closeBtn"), 1, 1);
			BaseButton arg_8D_0 = baseButton;
			Action<GameObject> arg_8D_1;
			if ((arg_8D_1 = a3_washredname.<>c.<>9__5_0) == null)
			{
				arg_8D_1 = (a3_washredname.<>c.<>9__5_0 = new Action<GameObject>(a3_washredname.<>c.<>9.<init>b__5_0));
			}
			arg_8D_0.onClick = arg_8D_1;
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("bg_top/rule_btn"), 1, 1);
			baseButton2.onClick = delegate(GameObject go)
			{
				this.rule.SetActive(true);
			};
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("rule_bg/rule/close"), 1, 1);
			baseButton3.onClick = delegate(GameObject go)
			{
				this.rule.SetActive(false);
			};
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("bg_downleft/Button"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.cleanpoint);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("bg_downright/Button"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.cleanpoint1);
		}

		public override void onShowed()
		{
			this.point();
		}

		public override void onClosed()
		{
		}

		public void point()
		{
			this.now_point.text = ModelBase<PlayerModel>.getInstance().sinsNub.ToString();
			this.refreshpoint(ModelBase<PlayerModel>.getInstance().sinsNub);
			this.needMoney.text = "消耗：" + ModelBase<PlayerModel>.getInstance().sinsNub * 2u + "个钻石";
		}

		private void refreshpoint(uint point)
		{
			bool flag = point <= 15u;
			if (flag)
			{
				this.des.text = "死亡后不掉落装备";
			}
			else
			{
				bool flag2 = point > 15u && point <= 90u;
				if (flag2)
				{
					this.des.text = "死亡后小几率掉落装备";
				}
				else
				{
					bool flag3 = point > 90u && point <= 150u;
					if (flag3)
					{
						this.des.text = "死亡后中几率掉落装备";
					}
					else
					{
						bool flag4 = point > 150u;
						if (flag4)
						{
							this.des.text = "死亡后大几率掉落装备";
						}
					}
				}
			}
		}

		private void cleanpoint(GameObject go)
		{
			BaseProxy<a3_PkmodelProxy>.getInstance().sendWashredname(1);
		}

		private void cleanpoint1(GameObject go)
		{
			BaseProxy<a3_PkmodelProxy>.getInstance().sendWashredname(2);
		}
	}
}
