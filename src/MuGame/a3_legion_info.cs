using Cross;
using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_legion_info : a3BaseLegion
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_legion_info.<>c <>9 = new a3_legion_info.<>c();

			public static UnityAction <>9__13_1;

			public static UnityAction <>9__13_2;

			public static Action<GameObject> <>9__13_0;

			internal void <init>b__13_0(GameObject GameObject)
			{
				bool flag = ModelBase<PlayerModel>.getInstance().name == ModelBase<A3_LegionModel>.getInstance().myLegion.name;
				if (flag)
				{
					bool flag2 = ModelBase<A3_LegionModel>.getInstance().myLegion.plycnt <= 1;
					if (flag2)
					{
						MsgBoxMgr arg_6E_0 = MsgBoxMgr.getInstance();
						string arg_6E_1 = ContMgr.getCont("clan_0", null);
						UnityAction arg_6E_2;
						if ((arg_6E_2 = a3_legion_info.<>c.<>9__13_1) == null)
						{
							arg_6E_2 = (a3_legion_info.<>c.<>9__13_1 = new UnityAction(a3_legion_info.<>c.<>9.<init>b__13_1));
						}
						arg_6E_0.showConfirm(arg_6E_1, arg_6E_2, null, 0);
					}
					else
					{
						flytxt.instance.fly(ContMgr.getCont("clan_1", null), 0, default(Color), null);
					}
				}
				else
				{
					MsgBoxMgr arg_CC_0 = MsgBoxMgr.getInstance();
					string arg_CC_1 = ContMgr.getCont("clan_2", null);
					UnityAction arg_CC_2;
					if ((arg_CC_2 = a3_legion_info.<>c.<>9__13_2) == null)
					{
						arg_CC_2 = (a3_legion_info.<>c.<>9__13_2 = new UnityAction(a3_legion_info.<>c.<>9.<init>b__13_2));
					}
					arg_CC_0.showConfirm(arg_CC_1, arg_CC_2, null, 0);
				}
			}

			internal void <init>b__13_1()
			{
				BaseProxy<A3_LegionProxy>.getInstance().SendDeleteLegion();
			}

			internal void <init>b__13_2()
			{
				BaseProxy<A3_LegionProxy>.getInstance().SendQuit();
			}
		}

		public static a3_legion_info mInstance;

		private InputField edittext;

		private BaseButton edit;

		private Transform s5;

		private Transform s7;

		private BaseButton btn_quit;

		private BaseButton btn_lvup;

		private Transform helpParent;

		private Transform help;

		private Transform help2;

		private Transform jn_add;

		private bool can_jx;

		public a3_legion_info(BaseShejiao win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			a3_legion_info.mInstance = this;
			this.jn_add = base.transform.FindChild("rt/buff/icon/Image_add");
			this.helpParent = base.transform.FindChild("help");
			this.help = base.transform.FindChild("help/panel_help");
			this.help2 = base.transform.FindChild("help2");
			this.edittext = base.transform.FindChild("rt/msg").GetComponent<InputField>();
			this.edittext.enabled = false;
			this.btn_quit = new BaseButton(base.transform.FindChild("btn_quit"), 1, 1);
			BaseButton arg_D6_0 = this.btn_quit;
			Action<GameObject> arg_D6_1;
			if ((arg_D6_1 = a3_legion_info.<>c.<>9__13_0) == null)
			{
				arg_D6_1 = (a3_legion_info.<>c.<>9__13_0 = new Action<GameObject>(a3_legion_info.<>c.<>9.<init>b__13_0));
			}
			arg_D6_0.onClick = arg_D6_1;
			this.edit = new BaseButton(base.transform.FindChild("rt/Edit"), 1, 1);
			this.edit.onClick = delegate(GameObject GameObject)
			{
				bool flag = !this.edittext.interactable;
				if (flag)
				{
					bool flag2 = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc < 3;
					if (flag2)
					{
						flytxt.flyUseContId("clan_8", null, 0);
						return;
					}
					this.edittext.enabled = true;
					this.edit.transform.FindChild("Text").GetComponent<Text>().text = "确定";
					flytxt.instance.fly(ContMgr.getCont("clan_3", null), 0, default(Color), null);
				}
				else
				{
					this.edittext.enabled = false;
					this.edit.transform.FindChild("Text").GetComponent<Text>().text = "编辑";
					bool flag3 = a3_legion.mInstance.checkKeyWord(this.edittext.text, 4);
					bool flag4 = flag3;
					if (flag4)
					{
						return;
					}
					BaseProxy<A3_LegionProxy>.getInstance().SendChangeNotice(this.edittext.text);
					flytxt.instance.fly(ContMgr.getCont("clan_4", null), 0, default(Color), null);
				}
				this.edittext.interactable = !this.edittext.interactable;
			};
			this.s5 = base.main.__mainTrans.FindChild("s5");
			new BaseButton(base.transform.FindChild("btn_donation"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.s5.SetParent(this.s5.parent.parent.parent);
				this.s5.SetAsLastSibling();
				this.s5.gameObject.SetActive(true);
			};
			new BaseButton(base.transform.FindChild("help/panel_help/closeBtn"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				base.main.CloseSub(this.help, this.helpParent);
			};
			new BaseButton(base.transform.FindChild("help/panel_help/bg_0"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				base.main.CloseSub(this.help, this.helpParent);
			};
			new BaseButton(base.transform.FindChild("help2/panel_help/closeBtn"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.help2.gameObject.SetActive(false);
			};
			new BaseButton(base.transform.FindChild("help2/panel_help/bg_0"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.help2.gameObject.SetActive(false);
			};
			new BaseButton(base.main.__mainTrans.FindChild("s5/btn_close"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.s5.SetParent(base.main.__mainTrans);
				this.s5.SetAsLastSibling();
				this.s5.gameObject.SetActive(false);
			};
			InputField df = base.main.__mainTrans.FindChild("s5/InputName").GetComponent<InputField>();
			new BaseButton(base.main.__mainTrans.FindChild("s5/btn_create"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				SXML sXML = XMLMgr.instance.GetSXML("clan.clan", "clan_lvl==" + BaseProxy<A3_LegionProxy>.getInstance().lvl);
				int @int = sXML.getInt("money_limit");
				bool flag = BaseProxy<A3_LegionProxy>.getInstance().gold > @int || (ulong)int.Parse(df.text) + (ulong)((long)BaseProxy<A3_LegionProxy>.getInstance().gold) > (ulong)((long)@int);
				if (flag)
				{
					this.can_jx = false;
				}
				else
				{
					this.can_jx = true;
				}
				bool flag2 = !this.can_jx;
				if (flag2)
				{
					flytxt.instance.fly("帮派资金已经达到上限", 0, default(Color), null);
				}
				bool flag3 = this.can_jx;
				if (flag3)
				{
					BaseProxy<A3_LegionProxy>.getInstance().SendDonate((uint)int.Parse(df.text));
					this.s5.SetParent(this.main.__mainTrans);
					this.s5.SetAsLastSibling();
					df.text = "0";
					this.s5.gameObject.SetActive(false);
				}
			};
			df.onValueChange.AddListener(delegate(string str)
			{
				bool flag = str == "";
				if (flag)
				{
					str = "0";
				}
				base.main.gxdtext.text = ContMgr.getCont("clan_tip_0", new string[]
				{
					(int.Parse(str) / 1000).ToString()
				});
			});
			this.btn_lvup = new BaseButton(base.transform.FindChild("lt/btn_lvup"), 1, 1);
			this.btn_lvup.onClick = new Action<GameObject>(this.LvUpGroup);
			new BaseButton(base.transform.FindChild("btn_description"), 1, 1).onClick = new Action<GameObject>(this.Desc);
			new BaseButton(base.transform.FindChild("btn_description2"), 1, 1).onClick = delegate(GameObject g)
			{
				this.help2.gameObject.SetActive(true);
			};
			this.s7 = base.main.__mainTrans.FindChild("s7");
			new BaseButton(this.s7.transform.FindChild("raise_btn"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.s7.gameObject.SetActive(false);
			};
			new BaseButton(this.s7.transform.FindChild("close"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.s7.gameObject.SetActive(false);
			};
		}

		public override void onShowed()
		{
			base.main.CloseSub(this.help, this.helpParent);
			this.edittext.interactable = false;
			this.edit.transform.FindChild("Text").GetComponent<Text>().text = "编辑";
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.RefreshInfo));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(9u, new Action<GameEvent>(this.Quit));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(6u, new Action<GameEvent>(this.OnDonateSuccess));
			BaseProxy<A3_LegionProxy>.getInstance().SendGetInfo();
		}

		public override void onClose()
		{
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(1u, new Action<GameEvent>(this.RefreshInfo));
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(9u, new Action<GameEvent>(this.Quit));
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(6u, new Action<GameEvent>(this.OnDonateSuccess));
			this.s7.gameObject.SetActive(false);
		}

		public void RefreshInfo(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("direct_join_clan");
			if (flag)
			{
				a3_legion.mInstance.dic0.isOn = data["direct_join_clan"]._bool;
			}
			bool flag2 = ModelBase<A3_LegionModel>.getInstance().myLegion.id <= 0;
			if (flag2)
			{
				base.main.ShowS(0);
			}
			else
			{
				bool flag3 = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc < 3;
				if (flag3)
				{
					this.btn_lvup.gameObject.SetActive(false);
				}
				else
				{
					this.btn_lvup.gameObject.SetActive(true);
				}
				bool flag4 = ModelBase<PlayerModel>.getInstance().name == ModelBase<A3_LegionModel>.getInstance().myLegion.name;
				if (flag4)
				{
					this.btn_quit.transform.FindChild("Text").GetComponent<Text>().text = "解散";
				}
				else
				{
					this.btn_quit.transform.FindChild("Text").GetComponent<Text>().text = "退团";
				}
				A3_LegionData myLegion = ModelBase<A3_LegionModel>.getInstance().myLegion;
				base.transform.FindChild("lt/info/clan_name/Text/Text").GetComponent<Text>().text = myLegion.clname;
				base.transform.FindChild("lt/info/name/Text/Text").GetComponent<Text>().text = myLegion.name;
				base.transform.FindChild("lt/info/lvl/Text/Text").GetComponent<Text>().text = myLegion.lvl + "级";
				base.transform.FindChild("lt/info/exp/Text/Text").GetComponent<Text>().text = myLegion.exp + "/" + myLegion.exp_cost;
				base.transform.FindChild("lt/info/member_num/Text/Text").GetComponent<Text>().text = string.Concat(new object[]
				{
					myLegion.plycnt,
					"/",
					myLegion.member_max,
					"（",
					myLegion.ol_cnt,
					"人在线）"
				});
				base.transform.FindChild("lt/info/funds/Text/Text").GetComponent<Text>().text = myLegion.gold.ToString() + "/" + myLegion.gold_cost;
				base.transform.FindChild("lt/info/zdl/Text/Text").GetComponent<Text>().text = myLegion.combpt.ToString("N0");
				this.edittext.text = ModelBase<A3_LegionModel>.getInstance().myLegion.notice.ToString();
				this.edittext.textComponent.text = ModelBase<A3_LegionModel>.getInstance().myLegion.notice.ToString();
				Transform transform = base.transform.FindChild("rt/buff/info");
				Transform transform2 = base.transform.FindChild("rt/buff/0");
				bool flag5 = FunctionOpenMgr.instance.checkLegion(FunctionOpenMgr.LEGION, false);
				if (flag5)
				{
					base.getGameObjectByPath("btn_shop/dontopen").SetActive(false);
				}
				else
				{
					base.getGameObjectByPath("btn_shop/dontopen").SetActive(true);
				}
				Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform3 = componentsInChildren[i];
					bool flag6 = transform3.parent == transform.transform;
					if (flag6)
					{
						UnityEngine.Object.Destroy(transform3.gameObject);
					}
				}
				ModelBase<A3_LegionModel>.getInstance().SetLegionBuff(ModelBase<PlayerModel>.getInstance().clan_buff_lvl);
				foreach (int current in ModelBase<A3_LegionModel>.getInstance().myLegionbuff.buffs.Keys)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform2.gameObject);
					gameObject.transform.SetParent(transform);
					gameObject.transform.localScale = Vector3.one;
					gameObject.GetComponent<Text>().text = Globle.getAttrNameById(current) + " + " + ModelBase<A3_LegionModel>.getInstance().myLegionbuff.buffs[current];
					gameObject.SetActive(true);
				}
				base.transform.FindChild("rt/buff/icon/Text").GetComponent<Text>().text = "LV " + ModelBase<PlayerModel>.getInstance().clan_buff_lvl;
				bool flag7 = ModelBase<PlayerModel>.getInstance().clan_buff_lvl >= myLegion.lvl;
				if (flag7)
				{
					this.jn_add.gameObject.SetActive(false);
				}
				else
				{
					this.jn_add.gameObject.SetActive(true);
				}
				new BaseButton(base.transform.FindChild("rt/buff/icon"), 1, 1).onClick = delegate(GameObject oo)
				{
					bool activeSelf = this.jn_add.gameObject.activeSelf;
					if (activeSelf)
					{
						this.s7.gameObject.SetActive(true);
						BaseProxy<A3_LegionProxy>.getInstance().SendGetMember();
						BaseProxy<BagProxy>.getInstance().sendLoadItems(0);
						this.buff_up();
					}
				};
			}
		}

		public void buff_up()
		{
			Transform transform = base.transform.FindChild("rt/buff/0");
			Transform[] componentsInChildren = this.s7.transform.FindChild("newlvl/info").GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform2 = componentsInChildren[i];
				bool flag = transform2.parent == this.s7.transform.FindChild("newlvl/info").transform;
				if (flag)
				{
					UnityEngine.Object.Destroy(transform2.gameObject);
				}
			}
			Transform[] componentsInChildren2 = this.s7.transform.FindChild("oldlvl/info").GetComponentsInChildren<Transform>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform3 = componentsInChildren2[j];
				bool flag2 = transform3.parent == this.s7.transform.FindChild("oldlvl/info").transform;
				if (flag2)
				{
					UnityEngine.Object.Destroy(transform3.gameObject);
				}
			}
			ModelBase<A3_LegionModel>.getInstance().SetLegionBuff(ModelBase<PlayerModel>.getInstance().clan_buff_lvl);
			foreach (int current in ModelBase<A3_LegionModel>.getInstance().myLegionbuff.buffs.Keys)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
				gameObject.transform.SetParent(this.s7.transform.FindChild("oldlvl/info"));
				gameObject.transform.localScale = Vector3.one;
				gameObject.GetComponent<Text>().text = Globle.getAttrNameById(current) + " + " + ModelBase<A3_LegionModel>.getInstance().myLegionbuff.buffs[current];
				gameObject.SetActive(true);
			}
			this.s7.transform.FindChild("coin_change/item/haditem").GetComponent<Image>().sprite = (Resources.Load("icon/item/" + ModelBase<A3_LegionModel>.getInstance().myLegionbuff.cost_item.ToString(), typeof(Sprite)) as Sprite);
			this.s7.transform.FindChild("oldlvl/icon/Text").GetComponent<Text>().text = "LV " + ModelBase<PlayerModel>.getInstance().clan_buff_lvl;
			bool flag3 = ModelBase<A3_LegionModel>.getInstance().donate >= ModelBase<A3_LegionModel>.getInstance().myLegionbuff_cost.cost_donate;
			if (flag3)
			{
				this.s7.transform.FindChild("coin_change/coin/Text").GetComponent<Text>().text = string.Concat(new object[]
				{
					"<color=#68FB2EFF>",
					ModelBase<A3_LegionModel>.getInstance().donate,
					"</color>/<color=#FFFFFFFF>",
					ModelBase<A3_LegionModel>.getInstance().myLegionbuff_cost.cost_donate,
					"</color>"
				});
			}
			else
			{
				this.s7.transform.FindChild("coin_change/coin/Text").GetComponent<Text>().text = string.Concat(new object[]
				{
					"<color=#F32C2CFF>",
					ModelBase<A3_LegionModel>.getInstance().donate,
					"</color>/<color=#FFFFFFFF>",
					ModelBase<A3_LegionModel>.getInstance().myLegionbuff_cost.cost_donate,
					"</color>"
				});
			}
			bool flag4 = ModelBase<a3_BagModel>.getInstance().get_item_num((uint)ModelBase<A3_LegionModel>.getInstance().myLegionbuff_cost.cost_item) >= ModelBase<A3_LegionModel>.getInstance().myLegionbuff_cost.cost_num;
			if (flag4)
			{
				this.s7.transform.FindChild("coin_change/item/Text").GetComponent<Text>().text = string.Concat(new object[]
				{
					"<color=#68FB2EFF>",
					ModelBase<a3_BagModel>.getInstance().get_item_num((uint)ModelBase<A3_LegionModel>.getInstance().myLegionbuff_cost.cost_item),
					"</color>/<color=#FFFFFFFF>",
					ModelBase<A3_LegionModel>.getInstance().myLegionbuff_cost.cost_num,
					"</color>"
				});
			}
			else
			{
				this.s7.transform.FindChild("coin_change/item/Text").GetComponent<Text>().text = string.Concat(new object[]
				{
					"<color=#F32C2CFF>",
					ModelBase<a3_BagModel>.getInstance().get_item_num((uint)ModelBase<A3_LegionModel>.getInstance().myLegionbuff_cost.cost_item),
					"</color>/<color=#FFFFFFFF>",
					ModelBase<A3_LegionModel>.getInstance().myLegionbuff_cost.cost_num,
					"</color>"
				});
			}
			bool flag5 = ModelBase<PlayerModel>.getInstance().clan_buff_lvl + 1 <= 12;
			if (flag5)
			{
				ModelBase<A3_LegionModel>.getInstance().SetLegionBuff(ModelBase<PlayerModel>.getInstance().clan_buff_lvl + 1);
			}
			else
			{
				ModelBase<A3_LegionModel>.getInstance().SetLegionBuff(ModelBase<PlayerModel>.getInstance().clan_buff_lvl);
			}
			foreach (int current2 in ModelBase<A3_LegionModel>.getInstance().myLegionbuff.buffs.Keys)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
				gameObject2.transform.SetParent(this.s7.transform.FindChild("newlvl/info"));
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.GetComponent<Text>().text = Globle.getAttrNameById(current2) + " + " + ModelBase<A3_LegionModel>.getInstance().myLegionbuff.buffs[current2];
				gameObject2.SetActive(true);
			}
			bool flag6 = ModelBase<PlayerModel>.getInstance().clan_buff_lvl + 1 <= 12;
			if (flag6)
			{
				this.s7.transform.FindChild("newlvl/icon/Text").GetComponent<Text>().text = "LV " + (ModelBase<PlayerModel>.getInstance().clan_buff_lvl + 1);
			}
			else
			{
				this.s7.transform.FindChild("newlvl/icon/Text").GetComponent<Text>().text = "LV " + ModelBase<PlayerModel>.getInstance().clan_buff_lvl;
			}
			new BaseButton(this.s7.transform.FindChild("raise_btn"), 1, 1).onClick = delegate(GameObject o)
			{
				bool flag7 = ModelBase<A3_LegionModel>.getInstance().myLegionbuff.cost_num <= ModelBase<a3_BagModel>.getInstance().get_item_num((uint)ModelBase<A3_LegionModel>.getInstance().myLegionbuff.cost_item) && ModelBase<A3_LegionModel>.getInstance().donate >= ModelBase<A3_LegionModel>.getInstance().myLegionbuff.cost_donate;
				if (flag7)
				{
					BaseProxy<A3_LegionProxy>.getInstance().SendUp_Buff();
					this.s7.gameObject.SetActive(false);
					bool flag8 = ModelBase<PlayerModel>.getInstance().clan_buff_lvl < 12;
					if (flag8)
					{
						ModelBase<PlayerModel>.getInstance().clan_buff_lvl++;
					}
					bool flag9 = ModelBase<PlayerModel>.getInstance().clan_buff_lvl == 1;
					if (flag9)
					{
						a3_buff.instance.legion_buf();
					}
					else
					{
						a3_buff.instance.resh_buff();
					}
					BaseProxy<A3_LegionProxy>.getInstance().SendGetInfo();
				}
				else
				{
					flytxt.instance.fly("提升条件没有达到", 0, default(Color), null);
				}
			};
		}

		private void Quit(GameEvent e)
		{
			base.main.ShowS(0);
		}

		private void LvUpGroup(GameObject g)
		{
			bool flag = ModelBase<A3_LegionModel>.getInstance().myLegion.lvl >= ModelBase<A3_LegionModel>.getInstance().myLegion.max_lvl;
			if (flag)
			{
				flytxt.instance.fly(ContMgr.getCont("clan_5", null), 0, default(Color), null);
			}
			else
			{
				bool flag2 = ModelBase<A3_LegionModel>.getInstance().myLegion.exp >= ModelBase<A3_LegionModel>.getInstance().myLegion.exp_cost;
				if (flag2)
				{
					BaseProxy<A3_LegionProxy>.getInstance().SendLVUP();
				}
				else
				{
					flytxt.instance.fly(ContMgr.getCont("clan_6", null), 0, default(Color), null);
				}
			}
		}

		private void Desc(GameObject g)
		{
			base.main.OpenSub(this.help);
		}

		private void OnDonateSuccess(GameEvent e)
		{
			Variant data = e.data;
			int num = data["money"];
			this.s5.SetParent(base.main.__mainTrans);
			this.s5.SetAsLastSibling();
			this.s5.gameObject.SetActive(false);
			this.RefreshInfo(null);
		}
	}
}
