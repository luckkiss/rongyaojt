using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	public class BeStrongerBtn
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly BeStrongerBtn.<>c <>9 = new BeStrongerBtn.<>c();

			public static Action<GameObject> <>9__5_0;

			public static Action<GameObject> <>9__5_1;

			public static Action<GameObject> <>9__5_2;

			public static Action<GameObject> <>9__5_3;

			public static Action<GameObject> <>9__5_4;

			public static Action<GameObject> <>9__5_5;

			public static Action<GameObject> <>9__5_6;

			public static Action<GameObject> <>9__5_7;

			public static Action<GameObject> <>9__5_8;

			internal void <initBtnFunc>b__5_0(GameObject go)
			{
				A3_BeStronger.Instance.HideShownPanel();
				a3_role.ForceIndex = 1;
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ROLE, null, false);
			}

			internal void <initBtnFunc>b__5_1(GameObject go)
			{
				A3_BeStronger.Instance.HideShownPanel();
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIP, null, false);
				a3_equip.instance.tabIndex = 5;
			}

			internal void <initBtnFunc>b__5_2(GameObject go)
			{
				A3_BeStronger.Instance.HideShownPanel();
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIP, null, false);
				a3_equip.instance.tabIndex = 4;
			}

			internal void <initBtnFunc>b__5_3(GameObject go)
			{
				A3_BeStronger.Instance.HideShownPanel();
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIP, null, false);
				bool flag = a3_equip.instance != null;
				if (flag)
				{
					a3_equip.instance.tabIndex = 0;
				}
			}

			internal void <initBtnFunc>b__5_4(GameObject go)
			{
				A3_BeStronger.Instance.HideShownPanel();
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_WIBG_SKIN, null, false);
			}

			internal void <initBtnFunc>b__5_5(GameObject go)
			{
				A3_BeStronger.Instance.HideShownPanel();
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_PET_SKIN, null, false);
			}

			internal void <initBtnFunc>b__5_6(GameObject go)
			{
				A3_BeStronger.Instance.HideShownPanel();
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_HUDUN, null, false);
			}

			internal void <initBtnFunc>b__5_7(GameObject go)
			{
				A3_BeStronger.Instance.HideShownPanel();
				InterfaceMgr.getInstance().open(InterfaceMgr.SKILL_A3, null, false);
			}

			internal void <initBtnFunc>b__5_8(GameObject go)
			{
				A3_BeStronger.Instance.HideShownPanel();
				ArrayList arrayList = new ArrayList();
				arrayList.Add(1);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACHIEVEMENT, arrayList, false);
			}
		}

		private HideOrShown currentState;

		public Transform button;

		public HideOrShown State
		{
			get
			{
				return this.currentState;
			}
			set
			{
				this.currentState = value;
			}
		}

		public void initBtnFunc(Title_BtnStronger title)
		{
			Action<GameObject> onClick = null;
			switch (title)
			{
			case Title_BtnStronger.Player_Attribute:
			{
				bool flag = this.button.childCount == 0;
				if (flag)
				{
					Transform transform = A3_BeStronger.Instance.template.FindChild("stronger_att");
					transform.SetParent(this.button, false);
					transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
					this.button.name = "stronger_att_btn";
				}
				Action<GameObject> arg_B0_0;
				if ((arg_B0_0 = BeStrongerBtn.<>c.<>9__5_0) == null)
				{
					arg_B0_0 = (BeStrongerBtn.<>c.<>9__5_0 = new Action<GameObject>(BeStrongerBtn.<>c.<>9.<initBtnFunc>b__5_0));
				}
				onClick = arg_B0_0;
				break;
			}
			case Title_BtnStronger.Equipment_Add:
			{
				bool flag2 = this.button.childCount == 0;
				if (flag2)
				{
					Transform transform2 = A3_BeStronger.Instance.template.FindChild("stronger_add");
					transform2.SetParent(this.button, false);
					transform2.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				}
				Action<GameObject> arg_122_0;
				if ((arg_122_0 = BeStrongerBtn.<>c.<>9__5_1) == null)
				{
					arg_122_0 = (BeStrongerBtn.<>c.<>9__5_1 = new Action<GameObject>(BeStrongerBtn.<>c.<>9.<initBtnFunc>b__5_1));
				}
				onClick = arg_122_0;
				break;
			}
			case Title_BtnStronger.Equipment_Gem:
			{
				bool flag3 = this.button.childCount == 0;
				if (flag3)
				{
					Transform transform3 = A3_BeStronger.Instance.template.FindChild("stronger_gem");
					transform3.SetParent(this.button, false);
					transform3.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				}
				Action<GameObject> arg_194_0;
				if ((arg_194_0 = BeStrongerBtn.<>c.<>9__5_2) == null)
				{
					arg_194_0 = (BeStrongerBtn.<>c.<>9__5_2 = new Action<GameObject>(BeStrongerBtn.<>c.<>9.<initBtnFunc>b__5_2));
				}
				onClick = arg_194_0;
				break;
			}
			case Title_BtnStronger.Equipment_Intensify:
			{
				bool flag4 = this.button.childCount == 0;
				if (flag4)
				{
					Transform transform4 = A3_BeStronger.Instance.template.FindChild("stronger_intensify");
					transform4.SetParent(this.button, false);
					transform4.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				}
				Action<GameObject> arg_206_0;
				if ((arg_206_0 = BeStrongerBtn.<>c.<>9__5_3) == null)
				{
					arg_206_0 = (BeStrongerBtn.<>c.<>9__5_3 = new Action<GameObject>(BeStrongerBtn.<>c.<>9.<initBtnFunc>b__5_3));
				}
				onClick = arg_206_0;
				break;
			}
			case Title_BtnStronger.Wings:
			{
				bool flag5 = this.button.childCount == 0;
				if (flag5)
				{
					Transform transform5 = A3_BeStronger.Instance.template.FindChild("stronger_wing");
					transform5.SetParent(this.button, false);
					transform5.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				}
				Action<GameObject> arg_278_0;
				if ((arg_278_0 = BeStrongerBtn.<>c.<>9__5_4) == null)
				{
					arg_278_0 = (BeStrongerBtn.<>c.<>9__5_4 = new Action<GameObject>(BeStrongerBtn.<>c.<>9.<initBtnFunc>b__5_4));
				}
				onClick = arg_278_0;
				break;
			}
			case Title_BtnStronger.Pet:
			{
				bool flag6 = this.button.childCount == 0;
				if (flag6)
				{
					Transform transform6 = A3_BeStronger.Instance.template.FindChild("stronger_pet");
					transform6.SetParent(this.button, false);
					transform6.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				}
				Action<GameObject> arg_2EA_0;
				if ((arg_2EA_0 = BeStrongerBtn.<>c.<>9__5_5) == null)
				{
					arg_2EA_0 = (BeStrongerBtn.<>c.<>9__5_5 = new Action<GameObject>(BeStrongerBtn.<>c.<>9.<initBtnFunc>b__5_5));
				}
				onClick = arg_2EA_0;
				break;
			}
			case Title_BtnStronger.Shield:
			{
				bool flag7 = this.button.childCount == 0;
				if (flag7)
				{
					Transform transform7 = A3_BeStronger.Instance.template.FindChild("stronger_shield");
					transform7.SetParent(this.button, false);
					transform7.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				}
				Action<GameObject> arg_35C_0;
				if ((arg_35C_0 = BeStrongerBtn.<>c.<>9__5_6) == null)
				{
					arg_35C_0 = (BeStrongerBtn.<>c.<>9__5_6 = new Action<GameObject>(BeStrongerBtn.<>c.<>9.<initBtnFunc>b__5_6));
				}
				onClick = arg_35C_0;
				break;
			}
			case Title_BtnStronger.Skill_LevelUp:
			{
				bool flag8 = this.button.childCount == 0;
				if (flag8)
				{
					Transform transform8 = A3_BeStronger.Instance.template.FindChild("stronger_skill");
					transform8.SetParent(this.button, false);
					transform8.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				}
				Action<GameObject> arg_3CE_0;
				if ((arg_3CE_0 = BeStrongerBtn.<>c.<>9__5_7) == null)
				{
					arg_3CE_0 = (BeStrongerBtn.<>c.<>9__5_7 = new Action<GameObject>(BeStrongerBtn.<>c.<>9.<initBtnFunc>b__5_7));
				}
				onClick = arg_3CE_0;
				break;
			}
			case Title_BtnStronger.Title:
			{
				bool flag9 = this.button.childCount == 0;
				if (flag9)
				{
					Transform transform9 = A3_BeStronger.Instance.template.FindChild("stronger_title");
					transform9.SetParent(this.button, false);
					transform9.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				}
				Action<GameObject> arg_43D_0;
				if ((arg_43D_0 = BeStrongerBtn.<>c.<>9__5_8) == null)
				{
					arg_43D_0 = (BeStrongerBtn.<>c.<>9__5_8 = new Action<GameObject>(BeStrongerBtn.<>c.<>9.<initBtnFunc>b__5_8));
				}
				onClick = arg_43D_0;
				break;
			}
			}
			new BaseButton(this.button, 1, 1).onClick = onClick;
		}

		public static implicit operator BeStrongerBtn(GameObject go)
		{
			return new BeStrongerBtn
			{
				button = (go != null) ? go.transform : null
			};
		}

		public static implicit operator GameObject(BeStrongerBtn btnObj)
		{
			Transform expr_06 = btnObj.button;
			return (expr_06 != null) ? expr_06.gameObject : null;
		}

		public override bool Equals(object obj)
		{
			return this.button.Equals((obj as BeStrongerBtn).button);
		}

		public override int GetHashCode()
		{
			return this.button.GetHashCode();
		}
	}
}
