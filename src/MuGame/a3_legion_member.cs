using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_legion_member : a3BaseLegion
	{
		private Dictionary<GameObject, A3_LegionMember> gos = new Dictionary<GameObject, A3_LegionMember>();

		private Dictionary<GameObject, BaseButton> gbtn = new Dictionary<GameObject, BaseButton>();

		private Transform select;

		private Transform content;

		private GameObject selectedMember;

		private BaseButton btn_leader;

		private BaseButton btn_promotion;

		private BaseButton btn_demotion;

		private BaseButton btn_remove;

		private BaseButton btn_invitation;

		private Transform s6;

		private InputField df;

		private uint inviteNum;

		public a3_legion_member(BaseShejiao win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			this.content = base.transform.FindChild("cells/scroll/content");
			this.select = base.transform.FindChild("cells/scroll/select");
			this.btn_leader = new BaseButton(base.transform.FindChild("btn_leader"), 1, 1);
			this.btn_promotion = new BaseButton(base.transform.FindChild("btn_promotion"), 1, 1);
			this.btn_demotion = new BaseButton(base.transform.FindChild("btn_demotion"), 1, 1);
			this.btn_remove = new BaseButton(base.transform.FindChild("btn_remove"), 1, 1);
			this.btn_invitation = new BaseButton(base.transform.FindChild("btn_invitation"), 1, 1);
			this.btn_leader.onClick = new Action<GameObject>(this.PromoteToBeLeader);
			this.btn_promotion.onClick = new Action<GameObject>(this.Promotion);
			this.btn_demotion.onClick = new Action<GameObject>(this.Demotion);
			this.btn_remove.onClick = new Action<GameObject>(this.RemoveOne);
			this.btn_invitation.onClick = new Action<GameObject>(this.InviteMember);
			this.s6 = base.main.__mainTrans.FindChild("s6");
			new BaseButton(base.main.__mainTrans.FindChild("s6/btn_close"), 1, 1).onClick = delegate(GameObject g)
			{
				this.df.text = "";
				this.s6.gameObject.SetActive(false);
				this.inviteNum = 0u;
			};
			new BaseButton(base.main.__mainTrans.FindChild("s6/btn_cancel"), 1, 1).onClick = delegate(GameObject g)
			{
				this.df.text = "";
				this.s6.gameObject.SetActive(false);
				this.inviteNum = 0u;
			};
			this.df = base.main.__mainTrans.FindChild("s6/InputName").GetComponent<InputField>();
			new BaseButton(base.main.__mainTrans.FindChild("s6/btn_invite"), 1, 1).onClick = delegate(GameObject g)
			{
				BaseProxy<PlayerInfoProxy>.getInstance().SendGetPlayerFromName(this.df.text);
				bool flag = this.inviteNum > 0u;
				if (flag)
				{
					BaseProxy<A3_LegionProxy>.getInstance().SendInvite(this.inviteNum);
				}
				this.s6.gameObject.SetActive(false);
			};
			new BaseButton(base.main.__mainTrans.FindChild("s6/btn_search"), 1, 1).onClick = delegate(GameObject g)
			{
				BaseProxy<PlayerInfoProxy>.getInstance().SendGetPlayerFromName(this.df.text);
			};
			base.main.__mainTrans.FindChild("s6/sa").gameObject.SetActive(false);
		}

		public override void onShowed()
		{
			this.inviteNum = 0u;
			this.select.gameObject.SetActive(false);
			this.selectedMember = null;
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(14u, new Action<GameEvent>(this.RefreshMembersList));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(18u, new Action<GameEvent>(this.OnInviteSuccess));
			BaseProxy<PlayerInfoProxy>.getInstance().addEventListener(PlayerInfoProxy.EVENT_ONGETPLAYERINFO, new Action<GameEvent>(this.OnSearchMember));
			BaseProxy<A3_LegionProxy>.getInstance().SendGetMember();
			base.main.__mainTrans.FindChild("s6/sa").gameObject.SetActive(false);
			bool flag = ModelBase<A3_LegionModel>.getInstance().members != null;
			if (flag)
			{
				foreach (int current in ModelBase<A3_LegionModel>.getInstance().members.Keys)
				{
					bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().cid == (ulong)((long)ModelBase<A3_LegionModel>.getInstance().members[current].cid);
					if (flag2)
					{
						bool flag3 = ModelBase<A3_LegionModel>.getInstance().members[current].clanc > 2;
						if (flag3)
						{
							this.btn_leader.gameObject.SetActive(true);
							this.btn_promotion.gameObject.SetActive(true);
							this.btn_demotion.gameObject.SetActive(true);
							this.btn_remove.gameObject.SetActive(true);
						}
						else
						{
							this.btn_leader.gameObject.SetActive(false);
							this.btn_promotion.gameObject.SetActive(false);
							this.btn_demotion.gameObject.SetActive(false);
							this.btn_remove.gameObject.SetActive(false);
						}
					}
				}
			}
		}

		public override void onClose()
		{
			this.inviteNum = 0u;
			this.select.gameObject.SetActive(false);
			this.selectedMember = null;
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(14u, new Action<GameEvent>(this.RefreshMembersList));
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(18u, new Action<GameEvent>(this.OnInviteSuccess));
			BaseProxy<PlayerInfoProxy>.getInstance().removeEventListener(PlayerInfoProxy.EVENT_ONGETPLAYERINFO, new Action<GameEvent>(this.OnSearchMember));
		}

		private void RefreshMembersList(GameEvent e)
		{
			this.selectedMember = null;
			this.select.gameObject.SetActive(false);
			this.select.SetParent(this.content.parent);
			Transform transform = base.transform.FindChild("cells/scroll/0");
			int num = 0;
			List<A3_LegionMember> list = new List<A3_LegionMember>(ModelBase<A3_LegionModel>.getInstance().members.Values);
			int num2 = list.Count;
			Transform[] componentsInChildren = this.content.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform2 = componentsInChildren[i];
				bool flag = transform2.parent == this.content.transform;
				if (flag)
				{
					bool flag2 = num < list.Count;
					if (flag2)
					{
						this.SetLine(transform2, list[num]);
						bool flag3 = list[num].lastlogoff > 0;
						if (flag3)
						{
							num2--;
						}
						List<A3_LegionMember> tp = new List<A3_LegionMember>();
						tp.Add(list[num]);
						BaseButton baseButton = new BaseButton(transform2.transform, 1, 1);
						baseButton.onClick = delegate(GameObject g)
						{
							debug.Log(tp[0].name);
							this.select.SetParent(g.transform);
							this.select.localPosition = Vector3.zero;
							this.select.localScale = Vector3.one;
							this.select.gameObject.SetActive(true);
							this.selectedMember = g;
						};
						this.gbtn[transform2.gameObject] = baseButton;
						transform2.gameObject.SetActive(true);
						num++;
					}
					else
					{
						bool flag4 = this.gbtn.ContainsKey(transform2.gameObject);
						if (flag4)
						{
							this.gbtn[transform2.gameObject] = null;
						}
						transform2.gameObject.SetActive(false);
					}
				}
			}
			bool flag5 = list.Count >= this.gos.Count;
			if (flag5)
			{
				for (int j = this.gos.Count; j < list.Count; j++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
					gameObject.transform.SetParent(this.content);
					gameObject.transform.localScale = Vector3.one;
					gameObject.SetActive(true);
					this.SetLine(gameObject.transform, list[j]);
					bool flag6 = list[num].lastlogoff > 0;
					if (flag6)
					{
						num2--;
					}
				}
			}
			this.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, this.content.GetComponent<GridLayoutGroup>().cellSize.y * (float)ModelBase<A3_LegionModel>.getInstance().members.Count);
			base.transform.FindChild("zx").GetComponent<Text>().text = string.Concat(new object[]
			{
				list.Count,
				"/",
				ModelBase<A3_LegionModel>.getInstance().myLegion.member_max,
				"(",
				num2,
				"在线)"
			});
		}

		private void SetLine(Transform go, A3_LegionMember alm)
		{
			go.name = alm.cid.ToString();
			go.FindChild("zy/job2").gameObject.SetActive(false);
			go.FindChild("zy/job3").gameObject.SetActive(false);
			go.FindChild("zy/job5").gameObject.SetActive(false);
			string text = "";
			switch (alm.clanc)
			{
			case 0:
				text = "新人";
				break;
			case 1:
				text = "会员";
				break;
			case 2:
				text = "精英";
				break;
			case 3:
				text = "元老";
				break;
			case 4:
				text = "领袖";
				break;
			}
			int lastlogoff = alm.lastlogoff;
			string text2;
			if (lastlogoff != 0)
			{
				text2 = "离线";
			}
			else
			{
				text2 = "在线";
			}
			go.FindChild("hyd").GetComponent<Text>().text = alm.huoyue.ToString();
			go.FindChild("name").GetComponent<Text>().text = alm.name;
			go.FindChild("jj").GetComponent<Text>().text = text;
			go.FindChild("zy/job" + alm.carr).gameObject.SetActive(true);
			go.FindChild("zt").GetComponent<Text>().text = text2;
			go.FindChild("dj").GetComponent<Text>().text = alm.zhuan.ToString() + "转" + alm.lvl.ToString() + "级";
			go.FindChild("zdl").GetComponent<Text>().text = alm.combpt.ToString();
			go.FindChild("gxd").GetComponent<Text>().text = alm.donate.ToString();
			this.gos[go.gameObject] = alm;
		}

		private void PromoteToBeLeader(GameObject go)
		{
			bool flag = this.selectedMember != null && this.gos.ContainsKey(this.selectedMember);
			if (flag)
			{
				A3_LegionMember a3_LegionMember = this.gos[this.selectedMember];
				bool flag2 = a3_LegionMember.cid == (int)ModelBase<PlayerModel>.getInstance().cid;
				if (flag2)
				{
					flytxt.flyUseContId("clan_7", null, 0);
				}
				else
				{
					bool flag3 = a3_LegionMember.clanc < 3;
					if (flag3)
					{
						flytxt.flyUseContId("clan_15", null, 0);
					}
					else
					{
						BaseProxy<A3_LegionProxy>.getInstance().SendBeLeader((uint)a3_LegionMember.cid);
					}
				}
			}
		}

		private void Promotion(GameObject go)
		{
			bool flag = this.selectedMember != null && this.gos.ContainsKey(this.selectedMember);
			if (flag)
			{
				A3_LegionMember a3_LegionMember = this.gos[this.selectedMember];
				bool flag2 = a3_LegionMember.cid == (int)ModelBase<PlayerModel>.getInstance().cid;
				if (flag2)
				{
					flytxt.flyUseContId("clan_7", null, 0);
				}
				else
				{
					bool flag3 = a3_LegionMember.clanc == 3;
					if (flag3)
					{
						flytxt.flyUseContId("clan_16", null, 0);
					}
					else
					{
						BaseProxy<A3_LegionProxy>.getInstance().PromotionOrDemotion((uint)a3_LegionMember.cid, 1u);
					}
				}
			}
		}

		private void Demotion(GameObject go)
		{
			bool flag = this.selectedMember != null && this.gos.ContainsKey(this.selectedMember);
			if (flag)
			{
				A3_LegionMember a3_LegionMember = this.gos[this.selectedMember];
				bool flag2 = a3_LegionMember.cid == (int)ModelBase<PlayerModel>.getInstance().cid;
				if (flag2)
				{
					flytxt.flyUseContId("clan_7", null, 0);
				}
				else
				{
					bool flag3 = a3_LegionMember.clanc == 1;
					if (flag3)
					{
						flytxt.flyUseContId("clan_13", null, 0);
					}
					else
					{
						BaseProxy<A3_LegionProxy>.getInstance().PromotionOrDemotion((uint)a3_LegionMember.cid, 0u);
					}
				}
			}
		}

		private void RemoveOne(GameObject go)
		{
			bool flag = this.selectedMember != null && this.gos.ContainsKey(this.selectedMember);
			if (flag)
			{
				A3_LegionMember a3_LegionMember = this.gos[this.selectedMember];
				bool flag2 = a3_LegionMember.cid == (int)ModelBase<PlayerModel>.getInstance().cid;
				if (flag2)
				{
					flytxt.flyUseContId("clan_7", null, 0);
				}
				else
				{
					BaseProxy<A3_LegionProxy>.getInstance().SendRemove((uint)a3_LegionMember.cid);
				}
			}
		}

		private void InviteMember(GameObject go)
		{
			this.s6.gameObject.SetActive(true);
			this.s6.FindChild("sa").gameObject.SetActive(false);
		}

		private void OnSearchMember(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("res");
			if (flag)
			{
				bool flag2 = data["res"] < 0;
				if (flag2)
				{
					return;
				}
			}
			uint num = data["cid"];
			this.inviteNum = num;
			int num2 = data["combpt"];
			int i = data["carr"];
			int num3 = data["zhuan"];
			int num4 = data["lvl"];
			string text = data["name"];
			int num5 = data["clid"];
			bool flag3 = data["online"];
			this.s6.FindChild("sa").gameObject.SetActive(true);
			this.s6.FindChild("sa/Text").GetComponent<Text>().text = string.Concat(new object[]
			{
				"ID:",
				num,
				"       昵称：",
				text,
				"     职业：",
				A3_LegionModel.GetCarr(i),
				"      等级：",
				num3,
				"转",
				num4,
				"级"
			});
			bool flag4 = num5 != 0;
			if (flag4)
			{
				flytxt.instance.fly("玩家已有军团", 0, default(Color), null);
			}
		}

		private void OnInviteSuccess(GameEvent e)
		{
			this.df.text = "";
			this.s6.gameObject.SetActive(false);
		}
	}
}
