using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_currentTeamPanel : BaseShejiao
	{
		public class ItemMemberObj : Skin
		{
			public Transform root;

			public Image iconHead;

			public Image iconCaptain;

			public Text txtName;

			public Text txtLvl;

			public Text txtCombat;

			public uint cid;

			private BaseButton btnRemoveTeam;

			private Transform noEmpty;

			private Transform empty;

			private ItemTeamData itemTeamData;

			private BaseButton btnInvite;

			public ItemMemberObj(Transform tran) : base(tran)
			{
				this.Init(tran);
			}

			private void Init(Transform trans)
			{
				this.root = trans;
				this.noEmpty = this.root.FindChild("noEmpty");
				this.empty = this.root.FindChild("empty");
				this.iconHead = this.root.FindChild("noEmpty/texts/icon/leader").GetComponent<Image>();
				this.iconCaptain = this.root.FindChild("noEmpty/iconCaptain").GetComponent<Image>();
				this.txtName = this.root.FindChild("noEmpty/texts/txtName/Text").GetComponent<Text>();
				this.txtLvl = this.root.FindChild("noEmpty/texts/txtLvl/Text").GetComponent<Text>();
				this.txtCombat = this.root.FindChild("noEmpty/texts/txtCombat/Text").GetComponent<Text>();
				this.btnRemoveTeam = new BaseButton(this.root.FindChild("noEmpty/btnRemoveTeam"), 1, 1);
				this.btnRemoveTeam.onClick = new Action<GameObject>(this.onBtnRemoveTeam);
				this.btnInvite = new BaseButton(this.root.FindChild("empty/btnInvite"), 1, 1);
				this.btnInvite.onClick = new Action<GameObject>(this.onBtnInviteClick);
			}

			public void SetInfo(ItemTeamData itd = null, bool meIsCaptain = false)
			{
				bool flag = itd != null;
				if (flag)
				{
					this.itemTeamData = itd;
					this.cid = itd.cid;
					this.txtName.text = itd.name;
					this.txtLvl.text = string.Concat(new object[]
					{
						itd.zhuan,
						"转",
						itd.lvl,
						"级"
					});
					this.txtCombat.text = itd.combpt.ToString();
					uint num = itd.carr;
					bool flag2 = num == 0u;
					if (flag2)
					{
						num = (uint)ModelBase<PlayerModel>.getInstance().profession;
					}
					bool flag3 = a3_currentTeamPanel._instance.carrSpriteDic.ContainsKey(num);
					if (flag3)
					{
						this.iconHead.sprite = a3_currentTeamPanel._instance.carrSpriteDic[num];
						this.iconHead.SetNativeSize();
					}
					bool online = itd.online;
					if (online)
					{
						this.iconHead.gameObject.GetComponent<Image>().material = null;
						this.iconCaptain.gameObject.GetComponent<Image>().material = null;
						this.txtName.transform.parent.GetComponent<Image>().material = null;
						this.txtLvl.transform.parent.GetComponent<Image>().material = null;
						this.txtCombat.transform.parent.GetComponent<Image>().material = null;
					}
					else
					{
						this.iconHead.gameObject.GetComponent<Image>().material = a3_currentTeamPanel._instance.materialGrey;
						this.iconCaptain.gameObject.GetComponent<Image>().material = a3_currentTeamPanel._instance.materialGrey;
						this.txtName.transform.parent.GetComponent<Image>().material = a3_currentTeamPanel._instance.materialGrey;
						this.txtLvl.transform.parent.GetComponent<Image>().material = a3_currentTeamPanel._instance.materialGrey;
						this.txtCombat.transform.parent.GetComponent<Image>().material = a3_currentTeamPanel._instance.materialGrey;
					}
					this.noEmpty.gameObject.SetActive(true);
					this.empty.gameObject.SetActive(false);
					this.iconCaptain.gameObject.SetActive(itd.isCaptain);
					bool flag4 = meIsCaptain && this.cid != ModelBase<PlayerModel>.getInstance().cid;
					if (flag4)
					{
						this.btnRemoveTeam.gameObject.SetActive(true);
					}
					else
					{
						this.btnRemoveTeam.gameObject.SetActive(false);
					}
				}
				else
				{
					this.empty.gameObject.SetActive(true);
					this.noEmpty.gameObject.SetActive(false);
				}
			}

			public void ClearInfo(bool showInvite = false)
			{
				this.empty.gameObject.SetActive(true);
				this.noEmpty.gameObject.SetActive(false);
				this.cid = 0u;
				this.iconHead.material = null;
				this.iconCaptain.material = null;
				this.txtName.text = string.Empty;
				this.txtLvl.text = string.Empty;
				this.txtCombat.text = string.Empty;
				this.btnInvite.gameObject.SetActive(showInvite);
			}

			public void ChangeToMeCaptain(ItemTeamData itd = null, ItemTeamMemberData itmd = null)
			{
				bool flag = itd != null;
				if (flag)
				{
					this.cid = itd.cid;
					bool flag2 = itd.cid != ModelBase<PlayerModel>.getInstance().cid;
					if (flag2)
					{
						this.btnRemoveTeam.gameObject.SetActive(true);
					}
				}
				else
				{
					bool flag3 = itmd != null;
					if (flag3)
					{
						this.btnInvite.gameObject.SetActive(true);
					}
				}
			}

			private void meIsCaptain()
			{
				bool flag = this.cid == ModelBase<PlayerModel>.getInstance().cid;
				if (flag)
				{
					this.showCaptainIcon(true);
					this.showBtnInvite(false);
					this.showBtnRemoveTeam(false);
				}
				else
				{
					this.showCaptainIcon(false);
					this.showBtnInvite(true);
					this.showBtnRemoveTeam(true);
				}
			}

			private void meIsCustom()
			{
				bool b = this.cid != ModelBase<PlayerModel>.getInstance().cid;
				this.showCaptainIcon(false);
				this.showBtnInvite(b);
			}

			private void showContentInfo(bool b)
			{
				this.noEmpty.gameObject.SetActive(b);
				this.empty.gameObject.SetActive(!b);
			}

			private void showCaptainIcon(bool b)
			{
				this.iconCaptain.gameObject.SetActive(b);
			}

			private void showBtnInvite(bool b)
			{
				this.btnInvite.gameObject.SetActive(b);
			}

			private void showBtnRemoveTeam(bool b)
			{
				this.btnRemoveTeam.gameObject.SetActive(b);
			}

			public void ChangeToMeCustom()
			{
				this.btnRemoveTeam.gameObject.SetActive(false);
			}

			private void onBtnInviteClick(GameObject go)
			{
				Variant variant = new Variant();
				variant["index"] = 1;
				UIClient.instance.dispatchEvent(GameEvent.Create(17001u, this, variant, false));
			}

			private void onBtnRemoveTeam(GameObject go)
			{
				BaseProxy<TeamProxy>.getInstance().SendKickOut(this.cid);
			}
		}

		public static a3_currentTeamPanel _instance;

		private uint cid;

		private List<a3_currentTeamPanel.ItemMemberObj> itemMemberObjList;

		private Toggle togInvite;

		private Toggle togJoin;

		public Material materialGrey;

		public Text txtTeambuff;

		public Dictionary<uint, Sprite> carrSpriteDic;

		public uint begin_index = 0u;

		public uint end_index = 20u;

		public static bool leave = false;

		public GameObject team_object;

		public Dropdown team_object_change;

		public a3_currentTeamPanel(Transform trans) : base(trans)
		{
			this.init(trans);
		}

		public void init(Transform trans)
		{
			a3_currentTeamPanel._instance = this;
			this.itemMemberObjList = new List<a3_currentTeamPanel.ItemMemberObj>();
			this.txtTeambuff = trans.FindChild("right/bottom/teambuff").GetComponent<Text>();
			Transform transform = trans.FindChild("right/main/body/contains");
			this.team_object = base.transform.FindChild("right/bottom/team_object/Dropdown").gameObject;
			this.team_object_change = this.team_object.GetComponent<Dropdown>();
			this.team_object_change.onValueChanged.AddListener(new UnityAction<int>(this.team_object_dropdownClick));
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				a3_currentTeamPanel.ItemMemberObj item = new a3_currentTeamPanel.ItemMemberObj(child);
				this.itemMemberObjList.Add(item);
			}
			this.togInvite = trans.FindChild("right/bottom/togInvite").GetComponent<Toggle>();
			this.togJoin = trans.FindChild("right/bottom/togJoin").GetComponent<Toggle>();
			this.togInvite.onValueChanged.AddListener(new UnityAction<bool>(this.onTogAgreenAddOtherClick));
			this.togJoin.onValueChanged.AddListener(new UnityAction<bool>(this.onTogAgreeOtherApplyClick));
			this.materialGrey = Resources.Load<Material>("uifx/uiGray");
			this.carrSpriteDic = new Dictionary<uint, Sprite>();
			for (int j = 0; j < 3; j++)
			{
				bool flag = j == 0;
				if (flag)
				{
					this.carrSpriteDic.Add(2u, Resources.Load<Sprite>("icon/team/warrior_team"));
				}
				bool flag2 = j == 1;
				if (flag2)
				{
					this.carrSpriteDic.Add(3u, Resources.Load<Sprite>("icon/team/mage_team"));
				}
				bool flag3 = j == 2;
				if (flag3)
				{
					this.carrSpriteDic.Add(5u, Resources.Load<Sprite>("icon/team/assassin_team"));
				}
			}
			BaseButton baseButton = new BaseButton(trans.FindChild("right/bottom/btnQuitTeam"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnQuitTeamClick);
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_CREATETEAM, new Action<GameEvent>(this.onCreateTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_AFFIRMINVITE, new Action<GameEvent>(this.onAffirminvite));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_NEWMEMBERJOIN, new Action<GameEvent>(this.onNewMemberJoin));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_KICKOUT, new Action<GameEvent>(this.onNoticeHaveMemberLeave));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_CHANGETEAMINFO, new Action<GameEvent>(this.onChangeTeamInfo));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_NOTICEHAVEMEMBERLEAVE, new Action<GameEvent>(this.onNoticeHaveMemberLeave));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_LEAVETEAM, new Action<GameEvent>(this.onLeaveTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_NOTICEONLINESTATECHANGE, new Action<GameEvent>(this.onNoticeOnlineStateChange));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_CHANGECAPTAIN, new Action<GameEvent>(this.onChangeCaptain));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_TEAMOBJECT_CHANGE, new Action<GameEvent>(this.onChangeTeamObject));
			bool flag4 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag4)
			{
				this.team_object_change.value = this.change_v((int)BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid, false);
			}
		}

		private void team_object_dropdownClick(int i)
		{
			bool flag = !ModelBase<A3_TeamModel>.getInstance().Limit_Change_Teammubiao(i);
			if (flag)
			{
				flytxt.instance.fly("您还没有开启该功能", 0, default(Color), null);
				this.team_object_change.GetComponent<Dropdown>().value = 0;
			}
			else
			{
				bool flag2 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
				if (flag2)
				{
					bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
					if (meIsCaptain)
					{
						BaseProxy<TeamProxy>.getInstance().sendobject_change(this.change_v(i, true));
					}
					else
					{
						string txt = "";
						switch (i)
						{
						case 0:
							txt = "队伍目标为：自定义";
							break;
						case 1:
							txt = "队伍目标为：挂机";
							break;
						case 2:
							txt = "队伍目标为：托维尔墓穴";
							break;
						case 3:
							txt = "队伍目标为：兽灵秘境";
							break;
						case 4:
							txt = "队伍目标为：魔物猎人";
							break;
						}
						flytxt.instance.fly(txt, 0, default(Color), null);
					}
				}
			}
		}

		public void Show(ItemTeamMemberData itm)
		{
			int count = itm.itemTeamDataList.Count;
			for (int i = 0; i < count; i++)
			{
				this.itemMemberObjList[i].SetInfo(itm.itemTeamDataList[i], itm.meIsCaptain);
			}
			this.setTeamBuffTxt();
			for (int j = count; j < 5; j++)
			{
				bool flag = itm.meIsCaptain || itm.membInv;
				if (flag)
				{
					this.itemMemberObjList[j].ClearInfo(true);
				}
				else
				{
					this.itemMemberObjList[j].ClearInfo(false);
				}
			}
			base.gameObject.SetActive(true);
			a3_teamPanel._instance.gameObject.SetActive(false);
			bool flag2 = itm.leaderCid == ModelBase<PlayerModel>.getInstance().cid;
			if (flag2)
			{
				bool flag3 = this.togInvite.isOn != itm.membInv;
				if (flag3)
				{
					this.togInvite.isOn = itm.membInv;
				}
				bool flag4 = this.togJoin.isOn != itm.dirJoin;
				if (flag4)
				{
					this.togJoin.isOn = itm.dirJoin;
				}
			}
			else
			{
				this.togInvite.gameObject.SetActive(false);
				this.togJoin.gameObject.SetActive(false);
			}
			this.cid = itm.leaderCid;
		}

		public int change_v(int i, bool b)
		{
			int result = 0;
			if (b)
			{
				switch (i)
				{
				case 0:
					result = 0;
					break;
				case 1:
					result = 1;
					break;
				case 2:
					result = 108;
					break;
				case 3:
					result = 105;
					break;
				case 4:
					result = 2;
					break;
				}
			}
			else
			{
				switch (i)
				{
				case 0:
					result = 0;
					break;
				case 1:
					result = 1;
					break;
				case 2:
					result = 4;
					break;
				default:
					if (i != 105)
					{
						if (i == 108)
						{
							result = 2;
						}
					}
					else
					{
						result = 3;
					}
					break;
				}
			}
			return result;
		}

		private void onChangeTeamObject(GameEvent e)
		{
			Variant data = e.data;
			uint i = data["ltpid"];
			this.team_object_change.value = this.change_v((int)i, false);
		}

		private void setTeamBuffTxt()
		{
			int count = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
			bool flag = count >= 2;
			if (flag)
			{
				this.txtTeambuff.text = "同仇敌忾:攻击+" + count * 6 + "%";
			}
			else
			{
				this.txtTeambuff.text = "同仇敌忾:攻击+0%";
			}
			bool flag2 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag2)
			{
				bool flag3 = !BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
				if (flag3)
				{
					this.team_object_change.enabled = false;
					this.team_object.transform.FindChild("Arrow").gameObject.SetActive(false);
				}
				else
				{
					this.team_object_change.enabled = true;
					this.team_object.transform.FindChild("Arrow").gameObject.SetActive(true);
				}
			}
		}

		private void onCreateTeam(GameEvent e)
		{
			Variant data = e.data;
			uint teamId = data["teamid"];
			uint num = data["ltpid"];
			ItemTeamData itemTeamData = new ItemTeamData();
			itemTeamData.name = ModelBase<PlayerModel>.getInstance().name;
			itemTeamData.lvl = ModelBase<PlayerModel>.getInstance().lvl;
			itemTeamData.knightage = ModelBase<PlayerModel>.getInstance().clanid.ToString();
			itemTeamData.mapId = ModelBase<PlayerModel>.getInstance().mapid;
			itemTeamData.MembCount = 1;
			itemTeamData.cid = ModelBase<PlayerModel>.getInstance().cid;
			itemTeamData.zhuan = ModelBase<PlayerModel>.getInstance().up_lvl;
			itemTeamData.combpt = ModelBase<PlayerModel>.getInstance().combpt;
			itemTeamData.teamId = teamId;
			itemTeamData.ltpid = num;
			itemTeamData.isCaptain = true;
			itemTeamData.showRemoveMemberBtn = false;
			itemTeamData.online = true;
			itemTeamData.carr = (uint)ModelBase<PlayerModel>.getInstance().profession;
			this.cid = itemTeamData.cid;
			base.gameObject.SetActive(true);
			bool isCaptain = itemTeamData.isCaptain;
			for (int i = 0; i < this.itemMemberObjList.Count; i++)
			{
				bool flag = i == 0;
				if (flag)
				{
					this.itemMemberObjList[i].SetInfo(itemTeamData, true);
				}
				else
				{
					this.itemMemberObjList[i].ClearInfo(true);
				}
			}
			this.setTeamBuffTxt();
			this.team_object_change.value = this.change_v((int)num, false);
		}

		private void onTogAgreenAddOtherClick(bool b)
		{
			BaseProxy<TeamProxy>.getInstance().SendEditorInfoMembInv(b);
		}

		private void onTogAgreeOtherApplyClick(bool b)
		{
			BaseProxy<TeamProxy>.getInstance().SendEditorInfoDirJoin(b);
		}

		private void onAffirminvite(GameEvent e)
		{
			Variant data = e.data;
			ItemTeamMemberData affirmInviteData = ModelBase<A3_TeamModel>.getInstance().AffirmInviteData;
			this.team_object_change.value = this.change_v((int)affirmInviteData.ltpid, false);
			this.Show(affirmInviteData);
		}

		private void onNewMemberJoin(GameEvent e)
		{
			Variant data = e.data;
			uint num = data["cid"];
			string name = data["name"];
			uint lvl = data["lvl"];
			uint zhuan = data["zhuan"];
			uint carr = data["carr"];
			uint combpt = data["combpt"];
			ItemTeamData itemTeamData = new ItemTeamData();
			itemTeamData.cid = num;
			itemTeamData.name = name;
			itemTeamData.lvl = lvl;
			itemTeamData.zhuan = zhuan;
			itemTeamData.carr = carr;
			itemTeamData.combpt = (int)combpt;
			itemTeamData.isCaptain = false;
			itemTeamData.online = true;
			bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
			if (meIsCaptain)
			{
				itemTeamData.showRemoveMemberBtn = true;
			}
			else
			{
				itemTeamData.showRemoveMemberBtn = false;
			}
			itemTeamData.teamId = BaseProxy<TeamProxy>.getInstance().MyTeamData.teamId;
			int count = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
			this.itemMemberObjList[count - 1].SetInfo(itemTeamData, BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain);
			this.setTeamBuffTxt();
		}

		private void onKickOut(GameEvent e)
		{
			Variant data = e.data;
			uint num = data["cid"];
			int removedIndex = (int)BaseProxy<TeamProxy>.getInstance().MyTeamData.removedIndex;
			a3_currentTeamPanel.ItemMemberObj itemMemberObj = this.itemMemberObjList[removedIndex];
			this.itemMemberObjList.RemoveAt(removedIndex);
			this.itemMemberObjList.Add(itemMemberObj);
			itemMemberObj.ClearInfo(false);
			for (int i = removedIndex; i < this.itemMemberObjList.Count; i++)
			{
				this.itemMemberObjList[i].gameObject.transform.SetSiblingIndex(i);
			}
			this.setTeamBuffTxt();
		}

		private void onChangeTeamInfo(GameEvent e)
		{
			bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
			if (meIsCaptain)
			{
				this.togInvite.gameObject.SetActive(true);
				this.togJoin.gameObject.SetActive(true);
				bool flag = e.data.ContainsKey("memb_inv") && this.togInvite.isOn != BaseProxy<TeamProxy>.getInstance().MyTeamData.membInv;
				if (flag)
				{
					this.togInvite.isOn = BaseProxy<TeamProxy>.getInstance().MyTeamData.membInv;
				}
				bool flag2 = e.data.ContainsKey("dir_join") && this.togJoin.isOn != BaseProxy<TeamProxy>.getInstance().MyTeamData.dirJoin;
				if (flag2)
				{
					this.togJoin.isOn = BaseProxy<TeamProxy>.getInstance().MyTeamData.dirJoin;
				}
			}
			else
			{
				bool membInv = BaseProxy<TeamProxy>.getInstance().MyTeamData.membInv;
				int count = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
				for (int i = count; i < this.itemMemberObjList.Count; i++)
				{
					this.itemMemberObjList[i].ClearInfo(membInv);
				}
			}
		}

		private void onNoticeHaveMemberLeave(GameEvent e)
		{
			Variant data = e.data;
			uint num = data["cid"];
			int removedIndex = (int)BaseProxy<TeamProxy>.getInstance().MyTeamData.removedIndex;
			a3_currentTeamPanel.ItemMemberObj itemMemberObj = this.itemMemberObjList[removedIndex];
			this.itemMemberObjList.RemoveAt(removedIndex);
			this.itemMemberObjList.Add(itemMemberObj);
			itemMemberObj.ClearInfo(false);
			for (int i = removedIndex; i < this.itemMemberObjList.Count; i++)
			{
				this.itemMemberObjList[i].root.SetSiblingIndex(i);
			}
			int count = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
			for (int j = removedIndex; j < count; j++)
			{
				ItemTeamData itd = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[j];
				this.itemMemberObjList[j].SetInfo(itd, BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain);
			}
			for (int k = count; k < this.itemMemberObjList.Count; k++)
			{
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain || BaseProxy<TeamProxy>.getInstance().MyTeamData.membInv;
				if (flag)
				{
					this.itemMemberObjList[k].ClearInfo(true);
				}
				else
				{
					this.itemMemberObjList[k].ClearInfo(false);
				}
			}
			bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
			if (meIsCaptain)
			{
				this.togInvite.gameObject.SetActive(true);
				this.togJoin.gameObject.SetActive(true);
				bool flag2 = this.togInvite.isOn != BaseProxy<TeamProxy>.getInstance().MyTeamData.membInv;
				if (flag2)
				{
					this.togInvite.isOn = BaseProxy<TeamProxy>.getInstance().MyTeamData.membInv;
				}
				bool flag3 = this.togJoin.isOn != BaseProxy<TeamProxy>.getInstance().MyTeamData.dirJoin;
				if (flag3)
				{
					this.togJoin.isOn = BaseProxy<TeamProxy>.getInstance().MyTeamData.dirJoin;
				}
			}
			else
			{
				this.togInvite.gameObject.SetActive(false);
				this.togJoin.gameObject.SetActive(false);
			}
			this.setTeamBuffTxt();
			ItemTeamMemberData myTeamData = BaseProxy<TeamProxy>.getInstance().MyTeamData;
			this.Show(myTeamData);
		}

		private void onLeaveTeam(GameEvent e)
		{
			a3_currentTeamPanel._instance.gameObject.SetActive(false);
			a3_teamPanel._instance.gameObject.SetActive(true);
			BaseProxy<TeamProxy>.getInstance().SendGetMapTeam(a3_teamPanel._instance.begin_index, a3_teamPanel._instance.end_index);
		}

		private void onChangeCaptain(GameEvent e)
		{
			this.changeCaptain();
			this.setTeamBuffTxt();
		}

		private void onNoticeOnlineStateChange(GameEvent e)
		{
			this.NoticeOnlineStateChange();
		}

		private void changeCaptain()
		{
			int count = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
			ItemTeamMemberData myTeamData = BaseProxy<TeamProxy>.getInstance().MyTeamData;
			List<ItemTeamData> itemTeamDataList = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList;
			int count2 = this.itemMemberObjList.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < this.itemMemberObjList.Count; j++)
				{
					bool flag = itemTeamDataList[i].cid == this.itemMemberObjList[j].cid;
					if (flag)
					{
						bool meIsCaptain = myTeamData.meIsCaptain;
						if (meIsCaptain)
						{
							this.itemMemberObjList[j].ChangeToMeCaptain(itemTeamDataList[i], null);
						}
						else
						{
							this.itemMemberObjList[j].ChangeToMeCustom();
						}
					}
				}
			}
			bool meIsCaptain2 = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
			if (meIsCaptain2)
			{
				for (int k = count; k < count2; k++)
				{
					this.itemMemberObjList[k].ChangeToMeCaptain(null, myTeamData);
				}
			}
			bool meIsCaptain3 = myTeamData.meIsCaptain;
			if (meIsCaptain3)
			{
				this.togInvite.gameObject.SetActive(true);
				this.togJoin.gameObject.SetActive(true);
				bool membInv = BaseProxy<TeamProxy>.getInstance().MyTeamData.membInv;
				this.togInvite.GetComponent<Toggle>().isOn = membInv;
				bool dirJoin = BaseProxy<TeamProxy>.getInstance().MyTeamData.dirJoin;
				this.togJoin.GetComponent<Toggle>().isOn = dirJoin;
			}
			this.changeCaptainPos();
		}

		private void changeCaptainPos()
		{
			int count = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
			ItemTeamMemberData myTeamData = BaseProxy<TeamProxy>.getInstance().MyTeamData;
			List<ItemTeamData> itemTeamDataList = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList;
			int count2 = this.itemMemberObjList.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < this.itemMemberObjList.Count; j++)
				{
					bool flag = itemTeamDataList[i].cid == this.itemMemberObjList[j].cid;
					if (flag)
					{
						bool isCaptain = itemTeamDataList[i].isCaptain;
						if (isCaptain)
						{
							a3_currentTeamPanel.ItemMemberObj item = this.itemMemberObjList[j];
							this.itemMemberObjList.RemoveAt(j);
							this.itemMemberObjList.Insert(0, item);
						}
					}
				}
			}
			for (int k = 0; k < this.itemMemberObjList.Count; k++)
			{
				this.itemMemberObjList[k].root.SetSiblingIndex(k);
			}
		}

		private void NoticeOnlineStateChange()
		{
			int count = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
			List<ItemTeamData> itemTeamDataList = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList;
			int count2 = this.itemMemberObjList.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count2; j++)
				{
					bool flag = itemTeamDataList[i].cid == this.itemMemberObjList[j].cid;
					if (flag)
					{
						this.itemMemberObjList[j].SetInfo(itemTeamDataList[i], BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain);
						a3_currentTeamPanel.ItemMemberObj item = this.itemMemberObjList[j];
						this.itemMemberObjList.RemoveAt(j);
						this.itemMemberObjList.Insert(i, item);
						this.itemMemberObjList[i].root.SetSiblingIndex(i);
					}
				}
			}
			this.setTeamBuffTxt();
		}

		private void onBtnQuitTeamClick(GameObject go)
		{
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count == 1;
			if (flag)
			{
				uint teamId = BaseProxy<TeamProxy>.getInstance().MyTeamData.teamId;
				BaseProxy<TeamProxy>.getInstance().SendDissolve(teamId);
			}
			else
			{
				BaseProxy<TeamProxy>.getInstance().SendLeaveTeam(this.cid);
			}
			Variant variant = SvrLevelConfig.instacne.get_level_data(MapModel.getInstance().curLevelId);
			bool flag2 = variant != null && variant["team"]._int == 1;
			if (!flag2)
			{
				a3_currentTeamPanel._instance.gameObject.SetActive(false);
				a3_teamPanel._instance.gameObject.SetActive(true);
				BaseProxy<A3_ActiveProxy>.getInstance().SendGiveUpHunt();
			}
		}
	}
}
