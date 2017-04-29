using Cross;
using GameFramework;
using Lui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_expbar : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_expbar.<>c <>9 = new a3_expbar.<>c();

			public static Action<GameObject> <>9__34_2;

			public static Action<GameObject> <>9__34_3;

			public static Action<GameObject> <>9__34_4;

			public static Action<GameObject> <>9__34_5;

			public static Action<GameObject> <>9__34_6;

			public static Action<GameObject> <>9__34_7;

			public static Action<GameObject> <>9__34_8;

			public static Action<GameObject> <>9__34_9;

			public static Action<GameObject> <>9__34_10;

			public static Action <>9__34_13;

			internal void <init>b__34_2(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_18", null), 0, default(Color), null);
			}

			internal void <init>b__34_3(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_3", null), 0, default(Color), null);
			}

			internal void <init>b__34_4(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_4", null), 0, default(Color), null);
			}

			internal void <init>b__34_5(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_5", null), 0, default(Color), null);
			}

			internal void <init>b__34_6(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_6", null), 0, default(Color), null);
			}

			internal void <init>b__34_7(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_7", null), 0, default(Color), null);
			}

			internal void <init>b__34_8(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_9", null), 0, default(Color), null);
			}

			internal void <init>b__34_9(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_42", null), 0, default(Color), null);
			}

			internal void <init>b__34_10(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EVERYDAYLOGIN, null, false);
			}

			internal void <init>b__34_13()
			{
				bool mwlr_on = ModelBase<A3_ActiveModel>.getInstance().mwlr_on;
				if (mwlr_on)
				{
					bool flag = SelfRole._inst.m_moveAgent.remainingDistance < 1f;
					if (flag)
					{
						bool flag2 = !SelfRole.fsm.Autofighting;
						if (flag2)
						{
							SelfRole.fsm.StartAutofight();
							SelfRole.fsm.ChangeState(StateAttack.Instance);
						}
					}
				}
			}
		}

		public static a3_expbar instance;

		private GameObject against;

		private Animator ani;

		private Vector3 topPos;

		private Vector3 lastPos;

		private float lastPosY;

		private float lastPosX;

		private float itemChatMsgWidth;

		private RectTransform itemChatMsgPrefab;

		private Transform _chatContent;

		private Vector3 _initPos;

		private Transform lightTipTran;

		private Vector3 lightTipPos1;

		private Vector3 lightTipPos2;

		private float _initPosX;

		private float _initPosY;

		private Text txtConfig;

		private string strPk = "你正受到{0}的攻击";

		private Text txtPKInfo;

		private bool btnDwonClicked = true;

		private Dictionary<string, GameObject> lightip = new Dictionary<string, GameObject>();

		public BaseButton btnAutoOffLineExp;

		private BaseButton btnWashredname;

		private BaseButton btnNewFirendTips;

		private BaseButton btnTeamTips;

		private Text txtNewFirendTips;

		private BaseButton btn_1;

		private BaseButton btn_out;

		public static int feedTime;

		public Transform tfMHFloatBorder;

		private Queue<GameObject> itemChatMsgQuene;

		private List<ArrayList> teamInvitedListData;

		public Dictionary<string, GameObject> hint_obj;

		private bool isChatHiden
		{
			get;
			set;
		}

		public override void init()
		{
			a3_expbar.instance = this;
			this.against = base.getGameObjectByPath("Against");
			BaseButton baseButton = new BaseButton(base.transform.FindChild("Against/close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onCloseAgainst);
			this.txtPKInfo = base.transform.FindChild("Against/Text").GetComponent<Text>();
			this.lightTipTran = base.getTransformByPath("operator/LightTips");
			this.lightTipPos1 = this.lightTipTran.GetComponent<RectTransform>().anchoredPosition;
			this.lightTipPos2 = base.getTransformByPath("operator/LightTipsPart2").GetComponent<RectTransform>().anchoredPosition;
			this.ani = base.transform.GetComponent<Animator>();
			base.alain();
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("operator/btn_0"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onBtn);
			this.btn_1 = new BaseButton(base.transform.FindChild("operator/btn_1"), 1, 1);
			this.btn_1.onClick = new Action<GameObject>(this.onBtn);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("operator/btn_1_2"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onBtn);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("operator/btn_2"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onBtn);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("operator/btn_3"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onBtn);
			BaseButton expr_1DC = new BaseButton(base.transform.FindChild("operator/btn_up"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			expr_1DC.onClick = (Action<GameObject>)Delegate.Combine(expr_1DC.onClick, new Action<GameObject>(delegate(GameObject go)
			{
				this.isChatHiden = true;
				A3_BeStronger.Instance.Show = false;
			}));
			BaseButton expr_22D = new BaseButton(base.transform.FindChild("operator/btn_down"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			expr_22D.onClick = (Action<GameObject>)Delegate.Combine(expr_22D.onClick, new Action<GameObject>(delegate(GameObject go)
			{
				this.isChatHiden = false;
			}));
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("operator/btn_4"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onBtn);
			BaseButton arg_2CA_0 = new BaseButton(base.transform.FindChild("operator/btn_5"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			Action<GameObject> arg_2CA_1;
			if ((arg_2CA_1 = a3_expbar.<>c.<>9__34_2) == null)
			{
				arg_2CA_1 = (a3_expbar.<>c.<>9__34_2 = new Action<GameObject>(a3_expbar.<>c.<>9.<init>b__34_2));
			}
			arg_2CA_0.onClickFalse = arg_2CA_1;
			BaseButton arg_31E_0 = new BaseButton(base.transform.FindChild("operator/btn_6"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			Action<GameObject> arg_31E_1;
			if ((arg_31E_1 = a3_expbar.<>c.<>9__34_3) == null)
			{
				arg_31E_1 = (a3_expbar.<>c.<>9__34_3 = new Action<GameObject>(a3_expbar.<>c.<>9.<init>b__34_3));
			}
			arg_31E_0.onClickFalse = arg_31E_1;
			BaseButton arg_372_0 = new BaseButton(base.transform.FindChild("operator/btn_7"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			Action<GameObject> arg_372_1;
			if ((arg_372_1 = a3_expbar.<>c.<>9__34_4) == null)
			{
				arg_372_1 = (a3_expbar.<>c.<>9__34_4 = new Action<GameObject>(a3_expbar.<>c.<>9.<init>b__34_4));
			}
			arg_372_0.onClickFalse = arg_372_1;
			BaseButton arg_3C6_0 = new BaseButton(base.transform.FindChild("operator/btn_8"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			Action<GameObject> arg_3C6_1;
			if ((arg_3C6_1 = a3_expbar.<>c.<>9__34_5) == null)
			{
				arg_3C6_1 = (a3_expbar.<>c.<>9__34_5 = new Action<GameObject>(a3_expbar.<>c.<>9.<init>b__34_5));
			}
			arg_3C6_0.onClickFalse = arg_3C6_1;
			BaseButton arg_41A_0 = new BaseButton(base.transform.FindChild("operator/btn_9"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			Action<GameObject> arg_41A_1;
			if ((arg_41A_1 = a3_expbar.<>c.<>9__34_6) == null)
			{
				arg_41A_1 = (a3_expbar.<>c.<>9__34_6 = new Action<GameObject>(a3_expbar.<>c.<>9.<init>b__34_6));
			}
			arg_41A_0.onClickFalse = arg_41A_1;
			BaseButton arg_46E_0 = new BaseButton(base.transform.FindChild("operator/btn_10"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			Action<GameObject> arg_46E_1;
			if ((arg_46E_1 = a3_expbar.<>c.<>9__34_7) == null)
			{
				arg_46E_1 = (a3_expbar.<>c.<>9__34_7 = new Action<GameObject>(a3_expbar.<>c.<>9.<init>b__34_7));
			}
			arg_46E_0.onClickFalse = arg_46E_1;
			BaseButton baseButton7 = new BaseButton(base.transform.FindChild("operator/btn_11"), 1, 1);
			baseButton7.onClick = new Action<GameObject>(this.onBtn);
			BaseButton baseButton8 = new BaseButton(base.transform.FindChild("operator/btn_12"), 1, 1);
			baseButton8.onClick = new Action<GameObject>(this.onBtn);
			BaseButton baseButton9 = new BaseButton(base.transform.FindChild("operator/btn_13"), 1, 1);
			baseButton9.onClick = new Action<GameObject>(this.onBtn);
			baseButton9.onClickFalse = new Action<GameObject>(this.noPet);
			BaseButton arg_55D_0 = new BaseButton(base.transform.FindChild("operator/btn_14"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			Action<GameObject> arg_55D_1;
			if ((arg_55D_1 = a3_expbar.<>c.<>9__34_8) == null)
			{
				arg_55D_1 = (a3_expbar.<>c.<>9__34_8 = new Action<GameObject>(a3_expbar.<>c.<>9.<init>b__34_8));
			}
			arg_55D_0.onClickFalse = arg_55D_1;
			BaseButton baseButton10 = new BaseButton(base.transform.FindChild("operator/btn16"), 1, 1);
			baseButton10.onClick = new Action<GameObject>(this.onBtn);
			BaseButton arg_5DE_0 = new BaseButton(base.transform.FindChild("operator/btn_17"), 1, 1)
			{
				onClick = new Action<GameObject>(this.onBtn)
			};
			Action<GameObject> arg_5DE_1;
			if ((arg_5DE_1 = a3_expbar.<>c.<>9__34_9) == null)
			{
				arg_5DE_1 = (a3_expbar.<>c.<>9__34_9 = new Action<GameObject>(a3_expbar.<>c.<>9.<init>b__34_9));
			}
			arg_5DE_0.onClickFalse = arg_5DE_1;
			BaseButton baseButton11 = new BaseButton(base.getTransformByPath("operator/btn_18"), 1, 1);
			baseButton11.onClick = new Action<GameObject>(this.onBtn);
			BaseButton baseButton12 = new BaseButton(base.transform.FindChild("mail"), 1, 1);
			baseButton12.onClick = new Action<GameObject>(this.onBtn);
			this.btn_out = new BaseButton(base.transform.FindChild("btn_out"), 1, 1);
			this.btn_out.onClick = new Action<GameObject>(this.onBtnOut);
			this.btn_out.gameObject.SetActive(false);
			this.tfMHFloatBorder = base.transform.FindChild("mh_tips/a3_mhfloatBorder");
			this.btnAutoOffLineExp = new BaseButton(base.transform.FindChild("operator/LightTips/btnAuto_off_line_exp"), 1, 1);
			this.btnAutoOffLineExp.onClick = new Action<GameObject>(this.OnOfflineExp);
			bool isShowEveryDataLogin = BaseProxy<welfareProxy>.getInstance()._isShowEveryDataLogin;
			if (isShowEveryDataLogin)
			{
				base.getTransformByPath("operator/LightTips/everyDayLogin").gameObject.SetActive(true);
			}
			BaseButton arg_723_0 = new BaseButton(base.getTransformByPath("operator/LightTips/everyDayLogin"), 1, 1);
			Action<GameObject> arg_723_1;
			if ((arg_723_1 = a3_expbar.<>c.<>9__34_10) == null)
			{
				arg_723_1 = (a3_expbar.<>c.<>9__34_10 = new Action<GameObject>(a3_expbar.<>c.<>9.<init>b__34_10));
			}
			arg_723_0.onClick = arg_723_1;
			this.btnWashredname = new BaseButton(base.transform.FindChild("operator/LightTips/wash_redname"), 1, 1);
			this.btnWashredname.onClick = new Action<GameObject>(this.onWashredname);
			this.btnNewFirendTips = new BaseButton(base.transform.FindChild("operator/LightTips/btnNewFirendTips"), 1, 1);
			this.btnNewFirendTips.gameObject.SetActive(false);
			this.btnNewFirendTips.onClick = new Action<GameObject>(this.onBtnNewFirendTipsClick);
			this.txtNewFirendTips = this.btnNewFirendTips.transform.FindChild("Text").GetComponent<Text>();
			this.btnTeamTips = new BaseButton(base.transform.FindChild("operator/LightTips/btnTeamTips"), 1, 1);
			this.btnTeamTips.gameObject.SetActive(false);
			this.topPos = base.transform.FindChild("operator/chat/top").transform.position;
			BaseButton baseButton13 = new BaseButton(base.transform.FindChild("operator/chat/btnChat"), 1, 1);
			baseButton13.onClick = new Action<GameObject>(this.onBtnChatClick);
			this.lastPos = base.transform.FindChild("operator/chat/bottom").transform.position;
			this.lastPosY = this.lastPos.y;
			this.lastPosX = this.lastPos.x;
			this._chatContent = base.transform.FindChild("operator/chat/mask/scroll/content");
			this._initPos = base.transform.FindChild("operator/chat/initPos").transform.position;
			this._initPosX = this._initPos.x;
			this._initPosY = this._initPos.y;
			this.itemChatMsgPrefab = base.transform.FindChild("operator/chat/templet/itemChatMsg").GetComponent<RectTransform>();
			this.txtConfig = base.transform.FindChild("operator/chat/templet/txtConfig").GetComponent<Text>();
			RectTransform component = base.transform.FindChild("operator/chat").GetComponent<RectTransform>();
			this.itemChatMsgWidth = component.sizeDelta.x - baseButton2.transform.GetComponent<RectTransform>().sizeDelta.x - 10f;
			BaseProxy<A3_MailProxy>.getInstance().addEventListener(A3_MailProxy.MAIL_NEW_MAIL, new Action<GameEvent>(this.OnNewMail));
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_GET_PET, new Action<GameEvent>(this.OpenPet));
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_HAVE_PET, new Action<GameEvent>(this.closePet));
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_GET_LAST_TIME, new Action<GameEvent>(this.feedOpenPet));
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_NEWGET, delegate(GameEvent e)
			{
				Variant data = e.data;
				bool flag4 = data["new"];
				Transform transform = base.transform.FindChild("operator/btn_6/notice");
				bool flag5 = flag4;
				if (flag5)
				{
					transform.gameObject.SetActive(true);
				}
				else
				{
					transform.gameObject.SetActive(false);
				}
			});
			BaseProxy<A3_AuctionProxy>.getInstance().SendMyRackMsg();
			BaseProxy<FriendProxy>.getInstance().sendfriendlist(FriendProxy.FriendType.FRIEND);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_TEAMAPPLYPANEL, null, false);
			this.CheckLock();
			this.CheckLightTip();
			this.bag_Count();
			bool flag = a3_expbar.feedTime == 0 && ModelBase<PlayerModel>.getInstance().havePet;
			if (flag)
			{
				base.transform.FindChild("operator/btn_13/nofeed").gameObject.SetActive(true);
			}
			bool flag2 = !ModelBase<PlayerModel>.getInstance().havePet;
			if (flag2)
			{
				base.transform.FindChild("operator/btn_13/nofeed").gameObject.SetActive(false);
			}
			bool flag3 = a3_active_mwlr_kill.initLoginData != null && a3_active_mwlr_kill.initLoginData.Count > 0;
			if (flag3)
			{
				a3_active_mwlr_kill.Instance.ReloadData(a3_active_mwlr_kill.initLoginData);
				a3_active_mwlr_kill.initLoginData = null;
			}
			else
			{
				Transform expr_AC8 = a3_active_mwlr_kill.Instance.Bar_Mon;
				if (expr_AC8 != null)
				{
					expr_AC8.gameObject.SetActive(false);
				}
				a3_active_mwlr_kill.Instance.Clear();
			}
			int siblingOffset = this.tfMHFloatBorder.FindChild("hunt_icon_0").GetSiblingIndex();
			Action<GameObject> <>9__12;
			for (int i = 0; i < 5; i++)
			{
				BaseButton arg_B43_0 = new BaseButton(this.tfMHFloatBorder.GetChild(siblingOffset + i), 1, 1);
				Action<GameObject> arg_B43_1;
				if ((arg_B43_1 = <>9__12) == null)
				{
					arg_B43_1 = (<>9__12 = delegate(GameObject go)
					{
						Variant mwlr_map_info = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info;
						List<int> mwlr_map_id = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id;
						int num = go.transform.GetSiblingIndex() - siblingOffset;
						bool autofighting = SelfRole.fsm.Autofighting;
						if (autofighting)
						{
							SelfRole.fsm.Stop();
						}
						bool flag4 = !mwlr_map_info[num]["kill"]._bool;
						if (flag4)
						{
							ModelBase<A3_ActiveModel>.getInstance().mwlr_target_monId = mwlr_map_info[num]["target_mid"];
							ModelBase<A3_ActiveModel>.getInstance().mwlr_on = true;
							int arg_11C_0 = mwlr_map_info[num]["map_id"]._int;
							Vector3 arg_11C_1 = new Vector3(ModelBase<A3_ActiveModel>.getInstance().mwlr_mons_pos[mwlr_map_id[num]].x, SelfRole._inst.m_curModel.position.y, ModelBase<A3_ActiveModel>.getInstance().mwlr_mons_pos[mwlr_map_id[num]].z);
							Action arg_11C_2;
							if ((arg_11C_2 = a3_expbar.<>c.<>9__34_13) == null)
							{
								arg_11C_2 = (a3_expbar.<>c.<>9__34_13 = new Action(a3_expbar.<>c.<>9.<init>b__34_13));
							}
							SelfRole.WalkToMap(arg_11C_0, arg_11C_1, arg_11C_2, 0.3f);
						}
					});
				}
				arg_B43_0.onClick = arg_B43_1;
			}
			this.addIconHintImage();
		}

		public void HoldTip()
		{
			this.lightTipTran.GetComponent<RectTransform>().anchoredPosition = this.lightTipPos2;
		}

		public void DownTip()
		{
			this.lightTipTran.GetComponent<RectTransform>().anchoredPosition = this.lightTipPos1;
		}

		public void refreshExp(GameEvent e)
		{
		}

		public void OnNewMail(GameEvent e)
		{
			this.ShowLightTip("mail", delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(0);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_MAIL, arrayList, false);
				this.RemoveLightTip("mail");
			}, -1, false);
		}

		public void HideMailHint()
		{
			base.transform.FindChild("mail").gameObject.SetActive(false);
		}

		private void onBtnChatClick(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_CHATROOM, null, false);
		}

		private void onBtn(GameObject go)
		{
			string name = go.name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1633325920u)
			{
				if (num <= 665622881u)
				{
					if (num <= 615290024u)
					{
						if (num != 531401929u)
						{
							if (num == 615290024u)
							{
								if (name == "btn_13")
								{
									InterfaceMgr.getInstance().open(InterfaceMgr.A3_NEW_PET, null, false);
								}
							}
						}
						else if (name == "btn_18")
						{
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_RUNESTONE, null, false);
						}
					}
					else if (num != 632067643u)
					{
						if (num != 648845262u)
						{
							if (num == 665622881u)
							{
								if (name == "btn_10")
								{
									InterfaceMgr.getInstance().open(InterfaceMgr.A3_WIBG_SKIN, null, false);
								}
							}
						}
						else if (name == "btn_11")
						{
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_MAIL, null, false);
						}
					}
					else if (name == "btn_12")
					{
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_SYSTEM_SETTING, null, false);
					}
				}
				else if (num <= 1576214410u)
				{
					if (num != 682400500u)
					{
						if (num != 732733357u)
						{
							if (num == 1576214410u)
							{
								if (name == "btn_1_2")
								{
									InterfaceMgr.getInstance().open(InterfaceMgr.A3_BAG, null, false);
								}
							}
						}
						else if (name == "btn_14")
						{
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_HUDUN, null, false);
						}
					}
					else if (name == "btn_17")
					{
						InterfaceMgr.openByLua("a3_star_pic", null);
					}
				}
				else if (num != 1615041964u)
				{
					if (num != 1616203685u)
					{
						if (num == 1633325920u)
						{
							if (name == "btn_8")
							{
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_SUMMON, null, false);
							}
						}
					}
					else if (name == "btn_up")
					{
						this.On_Btn_Up();
					}
				}
				else if (name == "btn16")
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_YGYIWU, null, false);
				}
			}
			else if (num <= 1817879729u)
			{
				if (num <= 1767546872u)
				{
					if (num != 1650103539u)
					{
						if (num == 1767546872u)
						{
							if (name == "btn_0")
							{
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_CHATROOM, null, false);
							}
						}
					}
					else if (name == "btn_9")
					{
						InterfaceMgr.getInstance().open(InterfaceMgr.SKILL_A3, null, false);
					}
				}
				else if (num != 1784324491u)
				{
					if (num != 1801102110u)
					{
						if (num == 1817879729u)
						{
							if (name == "btn_3")
							{
								a3_role.ForceIndex = 0;
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_ROLE, null, false);
							}
						}
					}
					else if (name == "btn_2")
					{
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_TASK, null, false);
					}
				}
				else if (name == "btn_1")
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_BAG, null, false);
				}
			}
			else if (num <= 1868212586u)
			{
				if (num != 1834657348u)
				{
					if (num != 1851434967u)
					{
						if (num == 1868212586u)
						{
							if (name == "btn_6")
							{
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_AUCTION, null, false);
							}
						}
					}
					else if (name == "btn_5")
					{
						bool flag = this.can_Btn5();
						if (flag)
						{
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIP, null, false);
						}
						else
						{
							flytxt.instance.fly("至少携带一件装备哦！！！", 0, default(Color), null);
						}
					}
				}
				else if (name == "btn_4")
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, null, false);
				}
			}
			else if (num != 1884990205u)
			{
				if (num != 2949032964u)
				{
					if (num == 3968918830u)
					{
						if (name == "mail")
						{
							ArrayList arrayList = new ArrayList();
							arrayList.Add(0);
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_MAIL, arrayList, false);
							base.transform.FindChild("mail").gameObject.SetActive(false);
						}
					}
				}
				else if (name == "btn_down")
				{
					this.On_Btn_Down();
				}
			}
			else if (name == "btn_7")
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACHIEVEMENT, null, false);
			}
		}

		public void onBtnOut(GameObject go)
		{
			this.btn_out.gameObject.SetActive(false);
			this.On_Btn_Down();
		}

		public void On_Btn_Down()
		{
			bool flag = this.btnDwonClicked;
			if (!flag)
			{
				this.btnDwonClicked = true;
				this.ani.SetBool("onoff", false);
				base.getComponentByPath<Transform>("operator/btn_down").gameObject.SetActive(false);
				base.getComponentByPath<Transform>("operator/btn_up").gameObject.SetActive(true);
				bool flag2 = joystick.instance != null;
				if (flag2)
				{
					joystick.instance.onoffAni(false);
				}
				bool flag3 = skillbar.instance != null;
				if (flag3)
				{
					skillbar.instance.onoffAni(false);
				}
				InterfaceMgr.doCommandByLua("a3_litemap_btns.setToggle", "ui/interfaces/floatui/a3_litemap_btns", new object[]
				{
					true
				});
				this.btn_out.gameObject.SetActive(false);
			}
		}

		private void OnOfflineExp(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.OFFLINEEXP, null, false);
		}

		public void On_Btn_Up()
		{
			bool flag = !this.btnDwonClicked;
			if (!flag)
			{
				this.btnDwonClicked = false;
				this.ani.SetBool("onoff", true);
				base.getComponentByPath<Transform>("operator/btn_down").gameObject.SetActive(true);
				base.getComponentByPath<Transform>("operator/btn_up").gameObject.SetActive(false);
				bool flag2 = joystick.instance != null;
				if (flag2)
				{
					joystick.instance.onoffAni(true);
				}
				bool flag3 = skillbar.instance != null;
				if (flag3)
				{
					skillbar.instance.onoffAni(true);
				}
				InterfaceMgr.doCommandByLua("a3_litemap_btns.setToggle", "ui/interfaces/floatui/a3_litemap_btns", new object[]
				{
					true
				});
				this.btn_out.gameObject.SetActive(true);
			}
		}

		public void ShowAgainst(BaseRole m_CastRole)
		{
			bool flag = !this.against.activeSelf;
			if (flag)
			{
				this.against.SetActive(true);
				this.txtPKInfo.text = string.Format(this.strPk, m_CastRole.roleName);
				BaseButton baseButton = new BaseButton(base.transform.FindChild("Against/Button"), 1, 1);
				baseButton.onClick = delegate(GameObject <obj>)
				{
					this.onAgainst(m_CastRole);
				};
			}
		}

		public void CloseAgainst()
		{
			bool activeSelf = this.against.activeSelf;
			if (activeSelf)
			{
				this.against.SetActive(false);
			}
		}

		private void onCloseAgainst(GameObject go)
		{
			this.against.SetActive(false);
		}

		public void Update()
		{
			a3_active_mwlr_kill.Instance.Update();
		}

		private void PetFlyText()
		{
			bool flag = ModelBase<A3_PetModel>.getInstance().hasPet();
			if (flag)
			{
				uint feedItemTpid = ModelBase<A3_PetModel>.getInstance().GetFeedItemTpid();
				int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(feedItemTpid);
				bool auto_buy = ModelBase<A3_PetModel>.getInstance().Auto_buy;
				bool flag2 = itemNumByTpid <= 0 && !auto_buy;
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("pet_no_feed", null), 0, default(Color), null);
				}
			}
		}

		private void onAgainst(BaseRole m_CastRole)
		{
			bool flag = m_CastRole.isDead || m_CastRole == null || !OtherPlayerMgr._inst.m_mapOtherPlayer.ContainsKey(m_CastRole.m_unIID);
			if (flag)
			{
				flytxt.instance.fly("目标过远或离线", 1, default(Color), null);
				this.against.SetActive(false);
			}
			else
			{
				BaseProxy<a3_PkmodelProxy>.getInstance().sendProxy(1);
				SelfRole._inst.m_LockRole = m_CastRole;
				this.against.SetActive(false);
			}
		}

		public void setRollWord(List<msg4roll> msgrollList)
		{
			bool flag = this.itemChatMsgQuene.Count >= 2;
			if (flag)
			{
				GameObject obj = this.itemChatMsgQuene.Dequeue();
				UnityEngine.Object.Destroy(obj);
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemChatMsgPrefab.gameObject);
			LRichText component = gameObject.GetComponent<LRichText>();
			int fontSize = this.txtConfig.fontSize;
			component.font = this.txtConfig.font;
			foreach (msg4roll current in msgrollList)
			{
				component.insertElement(current.msgStr, current.color, fontSize, false, false, current.color, current.data);
			}
			a3_chatroom._instance.setLRichTextAction(component, true);
			component.reloadData();
			gameObject.transform.SetParent(this._chatContent, false);
			gameObject.transform.localScale = Vector3.one;
			gameObject.SetActive(true);
			RectTransform[] componentsInChildren = gameObject.GetComponentsInChildren<RectTransform>(false);
			float num = 0f;
			float num2 = 0f;
			bool flag2 = componentsInChildren.Length != 0;
			if (flag2)
			{
				num2 = (num = componentsInChildren[0].anchoredPosition.y);
			}
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				bool flag3 = componentsInChildren[i].anchoredPosition.y > num2;
				if (flag3)
				{
					num2 = componentsInChildren[i].anchoredPosition.y;
				}
				bool flag4 = componentsInChildren[i].anchoredPosition.y < num;
				if (flag4)
				{
					num = componentsInChildren[i].anchoredPosition.y;
				}
			}
			gameObject.GetComponent<LayoutElement>().minHeight = num2 - num;
			this.itemChatMsgQuene.Enqueue(gameObject);
		}

		private bool can_Btn5()
		{
			Dictionary<uint, a3_BagItemData> equips = ModelBase<a3_EquipModel>.getInstance().getEquips();
			Dictionary<uint, a3_BagItemData> unEquips = ModelBase<a3_BagModel>.getInstance().getUnEquips();
			bool flag = equips.Count <= 0 && unEquips.Count <= 0;
			return !flag;
		}

		private void ClickFalse(GameObject go)
		{
			flytxt.instance.fly("该功能暂未开放！", 0, default(Color), null);
		}

		private void noPet(GameObject go)
		{
			flytxt.instance.fly(ContMgr.getCont("func_limit_8", null), 0, default(Color), null);
		}

		private void CheckLightTip()
		{
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_RECEIVEAPPLYFRIEND, new Action<GameEvent>(this.onReceiveApplyFriend));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_NOTICEINVITE, new Action<GameEvent>(this.onNoticeInvite));
		}

		public void bag_Count()
		{
			int num = ModelBase<a3_BagModel>.getInstance().curi - ModelBase<a3_BagModel>.getInstance().getItems(false).Count;
			switch (num)
			{
			case 0:
				this.btn_1.gameObject.transform.FindChild("count").gameObject.SetActive(false);
				this.btn_1.gameObject.transform.FindChild("man").gameObject.SetActive(true);
				IconHintMgr.getInsatnce().showHint(IconHintMgr.TYPE_BAG, -1, num, true);
				break;
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				this.btn_1.gameObject.transform.FindChild("count").gameObject.SetActive(true);
				this.btn_1.gameObject.transform.FindChild("man").gameObject.SetActive(false);
				this.btn_1.gameObject.transform.FindChild("count/count").GetComponent<Text>().text = num.ToString();
				IconHintMgr.getInsatnce().showHint(IconHintMgr.TYPE_BAG, -1, num, false);
				break;
			default:
				this.btn_1.gameObject.transform.FindChild("count").gameObject.SetActive(false);
				this.btn_1.gameObject.transform.FindChild("man").gameObject.SetActive(false);
				IconHintMgr.getInsatnce().closeHint(IconHintMgr.TYPE_BAG, true, true);
				break;
			}
		}

		public void ShowLightTip(string iconname, Action<GameObject> OnClicked, int num = -1, bool force = false)
		{
			bool flag = !this.lightip.ContainsKey(iconname) && !force;
			GameObject gameObject;
			if (flag)
			{
				Transform transformByPath = base.getTransformByPath("operator/LightTips");
				GameObject gameObjectByPath = base.getGameObjectByPath("lt_temp/" + iconname);
				bool flag2 = gameObjectByPath == null;
				if (flag2)
				{
					return;
				}
				gameObject = UnityEngine.Object.Instantiate<GameObject>(gameObjectByPath);
				gameObject.transform.SetParent(transformByPath);
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				this.lightip[iconname] = gameObject;
			}
			else
			{
				gameObject = this.lightip[iconname];
			}
			bool flag3 = gameObject != null;
			if (flag3)
			{
				BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
				BaseButton expr_BA = baseButton;
				expr_BA.onClick = (Action<GameObject>)Delegate.Combine(expr_BA.onClick, OnClicked);
			}
		}

		public void RemoveLightTip(string iconname)
		{
			bool flag = this.lightip.ContainsKey(iconname);
			if (flag)
			{
				UnityEngine.Object.Destroy(this.lightip[iconname]);
				this.lightip.Remove(iconname);
			}
		}

		public void CheckLock()
		{
			base.transform.FindChild("operator/btn_10").gameObject.GetComponent<Button>().interactable = false;
			base.transform.FindChild("operator/btn_10/local").gameObject.SetActive(true);
			base.transform.FindChild("operator/btn_5").gameObject.GetComponent<Button>().interactable = false;
			base.transform.FindChild("operator/btn_5/local").gameObject.SetActive(true);
			base.transform.FindChild("operator/btn_6").gameObject.GetComponent<Button>().interactable = false;
			base.transform.FindChild("operator/btn_6/local").gameObject.SetActive(true);
			base.transform.FindChild("operator/btn_8").gameObject.GetComponent<Button>().interactable = false;
			base.transform.FindChild("operator/btn_8/local").gameObject.SetActive(true);
			base.transform.FindChild("operator/btn_7").gameObject.GetComponent<Button>().interactable = false;
			base.transform.FindChild("operator/btn_7/local").gameObject.SetActive(true);
			base.transform.FindChild("operator/btn_9").gameObject.GetComponent<Button>().interactable = false;
			base.transform.FindChild("operator/btn_9/local").gameObject.SetActive(true);
			base.transform.FindChild("operator/btn_13").gameObject.GetComponent<Button>().interactable = false;
			base.transform.FindChild("operator/btn_13/local").gameObject.SetActive(true);
			base.transform.FindChild("operator/btn_14").gameObject.GetComponent<Button>().interactable = false;
			base.transform.FindChild("operator/btn_14/local").gameObject.SetActive(true);
			base.transform.FindChild("operator/btn_17").gameObject.GetComponent<Button>().interactable = false;
			base.transform.FindChild("operator/btn_17/local").gameObject.SetActive(true);
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.PET_SWING, false);
			if (flag)
			{
				this.OpenSWING_PET();
			}
			bool flag2 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP, false);
			if (flag2)
			{
				this.OpenEQP();
			}
			bool flag3 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.STAR_PIC, false);
			if (flag3)
			{
				this.OpenSTAR_PIC();
			}
			bool flag4 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.HUDUN, false);
			if (flag4)
			{
				this.OpenHUDUN();
			}
			bool flag5 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.AUCTION_GUILD, false);
			if (flag5)
			{
				this.OpenAuction();
			}
			bool flag6 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.SUMMON_MONSTER, false);
			if (flag6)
			{
				this.OpenSummon();
			}
			bool flag7 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.ACHIEVEMENT, false);
			if (flag7)
			{
				this.OpenAchievement();
			}
			bool flag8 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.SKILL, false);
			if (flag8)
			{
				this.OpenSkill();
			}
			bool flag9 = ModelBase<PlayerModel>.getInstance().havePet || FunctionOpenMgr.instance.Check(FunctionOpenMgr.PET, false);
			if (flag9)
			{
				this.OpenPET();
			}
		}

		private void OpenSTAR_PIC()
		{
			base.transform.FindChild("operator/btn_17").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_17/local").gameObject.SetActive(false);
		}

		public void OpenSWING_PET()
		{
			base.transform.FindChild("operator/btn_10").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_10/local").gameObject.SetActive(false);
		}

		public void OpenHUDUN()
		{
			base.transform.FindChild("operator/btn_14").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_14/local").gameObject.SetActive(false);
		}

		public void OpenEQP()
		{
			base.transform.FindChild("operator/btn_5").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_5/local").gameObject.SetActive(false);
		}

		public void OpenAuction()
		{
			base.transform.FindChild("operator/btn_6").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_6/local").gameObject.SetActive(false);
		}

		public void OpenSummon()
		{
			base.transform.FindChild("operator/btn_8").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_8/local").gameObject.SetActive(false);
		}

		public void OpenAchievement()
		{
			base.transform.FindChild("operator/btn_7").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_7/local").gameObject.SetActive(false);
		}

		public void OpenSkill()
		{
			base.transform.FindChild("operator/btn_9").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_9/local").gameObject.SetActive(false);
		}

		public void OpenPET()
		{
			base.transform.FindChild("operator/btn_13").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_13/local").gameObject.SetActive(false);
		}

		public void OpenPet(GameEvent e)
		{
			base.transform.FindChild("operator/btn_13").gameObject.GetComponent<Button>().interactable = true;
			base.transform.FindChild("operator/btn_13/local").gameObject.SetActive(false);
		}

		private void closePet(GameEvent e)
		{
			base.transform.FindChild("operator/btn_13/nofeed").gameObject.SetActive(true);
		}

		private void feedOpenPet(GameEvent e)
		{
			base.transform.FindChild("operator/btn_13/nofeed").gameObject.SetActive(false);
		}

		public override void onShowed()
		{
			this.SetOffLineExpButton();
			this.ShowWashRed();
		}

		private void SetOffLineExpButton()
		{
			OffLineModel offLineModel = ModelBase<OffLineModel>.getInstance();
			bool flag = ModelBase<PlayerModel>.getInstance().up_lvl > 0u;
			this.btnAutoOffLineExp.gameObject.SetActive(offLineModel.CanGetExp & flag);
			Text component = this.btnAutoOffLineExp.transform.GetChild(0).GetComponent<Text>();
			bool flag2 = offLineModel.OffLineTime == OffLineModel.maxTime;
			if (flag2)
			{
				this.btnAutoOffLineExp.transform.FindChild("Image_full").gameObject.SetActive(true);
			}
			else
			{
				this.btnAutoOffLineExp.transform.FindChild("Image_full").gameObject.SetActive(false);
			}
		}

		public void ShowWashRed()
		{
			bool flag = ModelBase<PlayerModel>.getInstance().sinsNub > 15u;
			if (flag)
			{
				this.btnWashredname.gameObject.SetActive(true);
			}
			else
			{
				this.btnWashredname.gameObject.SetActive(false);
			}
		}

		private void onWashredname(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_WASHREDNAME, null, false);
		}

		private void onReceiveApplyFriend(GameEvent e)
		{
			this.btnNewFirendTips.gameObject.SetActive(true);
			int count = BaseProxy<FriendProxy>.getInstance().requestFirendList.Count;
			this.txtNewFirendTips.text = count.ToString();
		}

		private void onBtnNewFirendTipsClick(GameObject go)
		{
			this.btnNewFirendTips.gameObject.SetActive(false);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_BEREQUESTFRIEND, null, false);
		}

		private void onBtnTeamTipsClick(GameObject go)
		{
		}

		private void onNoticeInvite(GameEvent e)
		{
			Variant data = e.data;
			uint teamId = data["tid"];
			uint cid = data["cid"];
			string name = data["name"];
			uint lvl = data["lvl"];
			uint zhuan = data["zhuan"];
			uint carr = data["carr"];
			uint combpt = data["combpt"];
			ItemTeamData itemTeamData = new ItemTeamData();
			itemTeamData.teamId = teamId;
			itemTeamData.cid = cid;
			itemTeamData.name = name;
			itemTeamData.lvl = lvl;
			itemTeamData.zhuan = zhuan;
			itemTeamData.carr = carr;
			itemTeamData.combpt = (int)combpt;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(itemTeamData);
			this.teamInvitedListData.Add(arrayList);
			ArrayList data2 = this.teamInvitedListData[0];
			this.teamInvitedListData.RemoveAt(0);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_TEAMINVITEDPANEL, data2, false);
		}

		public void showBtnTeamTips(bool b)
		{
		}

		private void addIconHintImage()
		{
			IconHintMgr.getInsatnce().addHint(base.getTransformByPath("operator/btn_3"), IconHintMgr.TYPE_ROLE);
			IconHintMgr.getInsatnce().addHint(base.getTransformByPath("operator/btn_4"), IconHintMgr.TYPE_SHEJIAO);
			IconHintMgr.getInsatnce().addHint(base.getTransformByPath("operator/btn_7"), IconHintMgr.TYPE_ACHIEVEMENT);
			IconHintMgr.getInsatnce().addHint(base.getTransformByPath("operator/btn_9"), IconHintMgr.TYPE_SKILL);
			IconHintMgr.getInsatnce().addHint(base.getTransformByPath("operator/btn16"), IconHintMgr.TYPE_YGYUWU);
			IconHintMgr.getInsatnce().addHint(base.getTransformByPath("operator/btn_5"), IconHintMgr.TYPE_EQUIP);
			IconHintMgr.getInsatnce().addHint(base.getTransformByPath("operator/btn_10"), IconHintMgr.TYPE_WING_SKIN);
			IconHintMgr.getInsatnce().addHint_havenum(base.getTransformByPath("operator/btn_1_2"), IconHintMgr.TYPE_BAG);
			IconHintMgr.getInsatnce().addHint_havenum(base.getTransformByPath("operator/btn_11"), IconHintMgr.TYPE_MAIL);
		}

		public a3_expbar()
		{
			this.<isChatHiden>k__BackingField = false;
			this.itemChatMsgQuene = new Queue<GameObject>();
			this.teamInvitedListData = new List<ArrayList>();
			this.hint_obj = new Dictionary<string, GameObject>();
			base..ctor();
		}
	}
}
