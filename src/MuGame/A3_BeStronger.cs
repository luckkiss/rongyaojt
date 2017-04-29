using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class A3_BeStronger : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly A3_BeStronger.<>c <>9 = new A3_BeStronger.<>c();

			public static EventTriggerListener.VoidDelegate <>9__24_1;

			public static UnityAction <>9__24_2;

			internal void cctor>b__18_0(GameObject go)
			{
				bool arg_23_0;
				if (go == null)
				{
					arg_23_0 = false;
				}
				else
				{
					object arg_1E_0 = go.transform;
					A3_BeStronger expr_12 = A3_BeStronger.Instance;
					arg_23_0 = arg_1E_0.Equals((expr_12 != null) ? expr_12.upBtn : null);
				}
				bool flag = arg_23_0;
				if (!flag)
				{
					A3_BeStronger expr_2E = A3_BeStronger.Instance;
					bool flag2 = expr_2E != null && expr_2E.ContentShown.gameObject.activeSelf;
					if (flag2)
					{
						A3_BeStronger.Instance.ContentShown.gameObject.SetActive(false);
						A3_BeStronger.Instance.ClickPanel.gameObject.SetActive(false);
					}
				}
			}

			internal void <init>b__24_1(GameObject go)
			{
				A3_BeStronger.handler(go);
			}

			internal void <init>b__24_2()
			{
				A3_BeStronger.handler(null);
			}
		}

		private ScrollRect scrollRect_contentShown;

		public Transform ClickPanel;

		public static A3_BeStronger Instance;

		public static Transform ContentHiden;

		public Dictionary<Title_BtnStronger, BeStrongerBtn> btnInContent = new Dictionary<Title_BtnStronger, BeStrongerBtn>();

		public Transform template;

		public RectTransform ContentShown2D;

		public Transform upBtn;

		public GameObject up_sub_btnPrefab;

		public Transform ContentShown;

		private static Action<GameObject> handler;

		public Transform Owner
		{
			get
			{
				return base.transform.parent;
			}
			set
			{
				base.transform.SetParent(value, false);
			}
		}

		public int ShownItemNum
		{
			get
			{
				Transform expr_06 = this.ContentShown;
				return (expr_06 != null) ? expr_06.childCount : 0;
			}
		}

		public bool Show
		{
			set
			{
				this.ContentShown.gameObject.SetActive(value);
			}
		}

		static A3_BeStronger()
		{
			A3_BeStronger.handler = new Action<GameObject>(A3_BeStronger.<>c.<>9.<.cctor>b__18_0);
			BaseButton.Handler = A3_BeStronger.handler;
		}

		private A3_BeStronger()
		{
		}

		private void Btn_RefreshOrCreate(bool b_check, Title_BtnStronger buttonTitle)
		{
			if (b_check)
			{
				bool flag = !this.btnInContent.ContainsKey(buttonTitle);
				if (flag)
				{
					this.CreateButton(buttonTitle, UnityEngine.Object.Instantiate<GameObject>(this.up_sub_btnPrefab).transform);
				}
				this.btnInContent[buttonTitle].State = HideOrShown.Shown;
			}
			else
			{
				bool flag2 = this.btnInContent.ContainsKey(buttonTitle);
				if (flag2)
				{
					this.btnInContent[buttonTitle].State = HideOrShown.Hide;
				}
			}
		}

		private void OnUpBtnClick(GameObject go)
		{
			bool activeSelf = this.ContentShown.gameObject.activeSelf;
			if (activeSelf)
			{
				this.ContentShown.gameObject.SetActive(false);
			}
			else
			{
				this.CheckUpItem();
				this.ContentShown.gameObject.SetActive(true);
				this.ClickPanel.gameObject.SetActive(true);
				skillbar.instance.transform.SetAsFirstSibling();
			}
		}

		public bool CheckUpItem()
		{
			bool inFb = ModelBase<PlayerModel>.getInstance().inFb;
			bool result;
			if (inFb)
			{
				result = false;
			}
			else
			{
				PlayerModel expr_1C = ModelBase<PlayerModel>.getInstance();
				this.Btn_RefreshOrCreate(expr_1C != null && expr_1C.pt_att > 0, Title_BtnStronger.Player_Attribute);
				a3_EquipModel.EquipStrengthOption equipStrengthOption = ModelBase<a3_EquipModel>.getInstance().CheckEquipStrengthAvailable()[true];
				bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_ENCHANT, false);
				if (flag)
				{
					this.Btn_RefreshOrCreate((equipStrengthOption & a3_EquipModel.EquipStrengthOption.Add) > a3_EquipModel.EquipStrengthOption.None, Title_BtnStronger.Equipment_Add);
				}
				bool flag2 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_MOUNTING, false);
				if (flag2)
				{
					this.Btn_RefreshOrCreate((equipStrengthOption & a3_EquipModel.EquipStrengthOption.Gem) > a3_EquipModel.EquipStrengthOption.None, Title_BtnStronger.Equipment_Gem);
				}
				bool flag3 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_ENHANCEMENT, false);
				if (flag3)
				{
					this.Btn_RefreshOrCreate((equipStrengthOption & a3_EquipModel.EquipStrengthOption.Intensify) > a3_EquipModel.EquipStrengthOption.None, Title_BtnStronger.Equipment_Intensify);
				}
				bool flag4 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.PET_SWING, false);
				if (flag4)
				{
					A3_WingModel expr_C9 = ModelBase<A3_WingModel>.getInstance();
					this.Btn_RefreshOrCreate(expr_C9 != null && expr_C9.CheckLevelupAvailable(), Title_BtnStronger.Wings);
				}
				bool flag5 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.SKILL, false);
				if (flag5)
				{
					Skill_a3Model expr_F8 = ModelBase<Skill_a3Model>.getInstance();
					this.Btn_RefreshOrCreate(expr_F8 != null && expr_F8.CheckSkillLevelupAvailable(), Title_BtnStronger.Skill_LevelUp);
				}
				bool flag6 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.ACHIEVEMENT, false);
				if (flag6)
				{
					a3_RankModel expr_127 = ModelBase<a3_RankModel>.getInstance();
					this.Btn_RefreshOrCreate(expr_127 != null && expr_127.CheckTitleLevelupAvailable(), Title_BtnStronger.Title);
				}
				this.RefreshView();
				bool flag7 = this.ShownItemNum > 0;
				if (flag7)
				{
					Transform expr_158 = this.upBtn;
					if (expr_158 != null)
					{
						expr_158.gameObject.SetActive(true);
					}
					bool flag8 = !base.gameObject.activeSelf;
					if (flag8)
					{
						base.gameObject.SetActive(true);
					}
					result = true;
				}
				else
				{
					Transform expr_196 = this.upBtn;
					if (expr_196 != null)
					{
						expr_196.gameObject.SetActive(false);
					}
					bool activeSelf = base.gameObject.activeSelf;
					if (activeSelf)
					{
						base.gameObject.SetActive(false);
					}
					result = false;
				}
			}
			return result;
		}

		public void CreateButton(Title_BtnStronger title, Transform btn)
		{
			BeStrongerBtn beStrongerBtn = new BeStrongerBtn
			{
				button = btn,
				State = HideOrShown.Shown
			};
			bool flag = this.btnInContent.ContainsKey(title);
			if (flag)
			{
				this.btnInContent[title] = beStrongerBtn;
			}
			else
			{
				this.btnInContent.Add(title, beStrongerBtn);
			}
			beStrongerBtn.initBtnFunc(title);
		}

		public override void init()
		{
			A3_BeStronger.Instance = this;
			this.upBtn = base.transform.FindChild("upbtn");
			A3_BeStronger.ContentHiden = this.upBtn.transform.FindChild("HideView");
			this.ContentShown = this.upBtn.transform.FindChild("ShowMask/ShowView");
			this.ClickPanel = this.upBtn.transform.FindChild("ClickPanel");
			this.ContentShown2D = this.ContentShown.GetComponent<RectTransform>();
			this.scrollRect_contentShown = this.ContentShown.GetComponent<ScrollRect>();
			new BaseButton(this.upBtn, 1, 1).onClick = new Action<GameObject>(this.OnUpBtnClick);
			this.template = this.upBtn.FindChild("template");
			this.up_sub_btnPrefab = this.template.FindChild("sub_btn").gameObject;
			this.Owner = skillbar.instance.transform.FindChild("combat");
			base.transform.SetAsLastSibling();
			new BaseButton(this.ClickPanel, 1, 1).onClick = delegate(GameObject go)
			{
				Transform expr_07 = this.ContentShown;
				if (expr_07 != null)
				{
					expr_07.gameObject.SetActive(false);
				}
				this.ClickPanel.gameObject.SetActive(false);
			};
			EventTriggerListener expr_134 = EventTriggerListener.Get(joystick.instance.Stick);
			Delegate arg_159_0 = expr_134.onDown;
			EventTriggerListener.VoidDelegate arg_159_1;
			if ((arg_159_1 = A3_BeStronger.<>c.<>9__24_1) == null)
			{
				arg_159_1 = (A3_BeStronger.<>c.<>9__24_1 = new EventTriggerListener.VoidDelegate(A3_BeStronger.<>c.<>9.<init>b__24_1));
			}
			expr_134.onDown = (EventTriggerListener.VoidDelegate)Delegate.Combine(arg_159_0, arg_159_1);
			Button[] componentsInChildren = skillbar.instance.GetComponentsInChildren<Button>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				UnityEvent arg_19F_0 = componentsInChildren[i].onClick;
				UnityAction arg_19F_1;
				if ((arg_19F_1 = A3_BeStronger.<>c.<>9__24_2) == null)
				{
					arg_19F_1 = (A3_BeStronger.<>c.<>9__24_2 = new UnityAction(A3_BeStronger.<>c.<>9.<init>b__24_2));
				}
				arg_19F_0.AddListener(arg_19F_1);
			}
		}

		public override void onShowed()
		{
			this.CheckUpItem();
		}

		public void RefreshView()
		{
			List<Title_BtnStronger> list = new List<Title_BtnStronger>(this.btnInContent.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				HideOrShown state = this.btnInContent[list[i]].State;
				if (state != HideOrShown.Shown)
				{
					if (state != HideOrShown.Hide)
					{
					}
					this.btnInContent[list[i]].button.SetParent(A3_BeStronger.ContentHiden, false);
				}
				else
				{
					this.btnInContent[list[i]].button.SetParent(this.ContentShown, false);
				}
			}
		}

		public void HideShownPanel()
		{
			Transform expr_07 = this.ContentShown;
			if (expr_07 != null)
			{
				expr_07.gameObject.SetActive(false);
			}
			Transform expr_1F = this.ClickPanel;
			if (expr_1F != null)
			{
				expr_1F.gameObject.SetActive(false);
			}
		}

		public static implicit operator bool(A3_BeStronger any)
		{
			return A3_BeStronger.Instance != null;
		}
	}
}
