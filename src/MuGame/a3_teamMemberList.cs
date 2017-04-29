using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_teamMemberList : Window
	{
		private class itemTeamMember
		{
			public Transform root;

			private Text txtName;

			private Text txtLvl;

			private Text txtKnightage;

			private Text txtMap;

			private Image iconCaptain;

			private Toggle toggle;

			private Image iconCarr;

			public itemTeamMember(Transform trans)
			{
				this.Init(trans);
			}

			private void Init(Transform trans)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(trans.gameObject);
				this.root = gameObject.transform;
				this.txtName = this.root.FindChild("texts/txtName").GetComponent<Text>();
				this.txtLvl = this.root.FindChild("texts/txtLvl").GetComponent<Text>();
				this.txtKnightage = this.root.FindChild("texts/txtKnightage").GetComponent<Text>();
				this.txtMap = this.root.FindChild("texts/txtMap").GetComponent<Text>();
				this.iconCaptain = this.root.FindChild("texts/txtName/icon").GetComponent<Image>();
				this.iconCarr = this.root.FindChild("texts/txtName/iconCarr").GetComponent<Image>();
				this.toggle = this.root.FindChild("Toggle").GetComponent<Toggle>();
			}

			public void Show(ItemTeamData itd)
			{
				this.root.SetParent(a3_teamMemberList.contant);
				ToggleGroup component = a3_teamMemberList.contant.GetComponent<ToggleGroup>();
				this.toggle.GetComponent<Toggle>().group = component;
				this.root.localScale = Vector3.one;
				this.root.SetAsLastSibling();
				this.txtName.text = itd.name;
				this.txtLvl.text = string.Concat(new object[]
				{
					itd.zhuan,
					"转",
					itd.lvl,
					"级"
				});
				this.txtKnightage.text = itd.knightage;
				this.txtMap.text = ((SvrMapConfig.instance.getSingleMapConf(itd.mapId) == null) ? "" : SvrMapConfig.instance.getSingleMapConf(itd.mapId)["map_name"]._str);
				this.iconCaptain.gameObject.SetActive(itd.isCaptain);
				this.iconCarr.sprite = a3_teamMemberList._instance.iconSpriteDic[itd.carr];
				this.root.gameObject.SetActive(true);
			}
		}

		public static a3_teamMemberList _instance;

		private Transform itemMemberPrefab;

		private static Transform contant;

		private List<a3_teamMemberList.itemTeamMember> itemTeamMemberList;

		public Dictionary<uint, Sprite> iconSpriteDic;

		public override void init()
		{
			a3_teamMemberList._instance = this;
			this.itemTeamMemberList = new List<a3_teamMemberList.itemTeamMember>();
			BaseButton baseButton = new BaseButton(base.transform.FindChild("main/title/btnClose"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnCloseClick);
			this.itemMemberPrefab = base.transform.FindChild("main/itemPrefab/itemTeamMember");
			a3_teamMemberList.contant = base.transform.FindChild("main/body/main/Scroll/content");
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("main/bottom/btnJoin"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onBtnJoinClick);
			this.getProfessionSprite();
		}

		public override void onShowed()
		{
			bool flag = this.uiData != null;
			if (flag)
			{
				this.itemTeamMemberList.Clear();
				List<ItemTeamData> list = (List<ItemTeamData>)this.uiData[0];
				for (int i = 0; i < list.Count; i++)
				{
					a3_teamMemberList.itemTeamMember itemTeamMember = new a3_teamMemberList.itemTeamMember(this.itemMemberPrefab);
					itemTeamMember.Show(list[i]);
					this.itemTeamMemberList.Add(itemTeamMember);
				}
			}
		}

		private void onBtnCloseClick(GameObject g)
		{
			for (int i = 0; i < this.itemTeamMemberList.Count; i++)
			{
				UnityEngine.Object.Destroy(this.itemTeamMemberList[i].root.gameObject);
			}
			this.itemTeamMemberList.Clear();
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_TEAMMEMBERLIST);
		}

		private void onBtnJoinClick(GameObject go)
		{
			uint num = ModelBase<PlayerModel>.getInstance().up_lvl * 100u + ModelBase<PlayerModel>.getInstance().lvl;
			bool flag = num >= TeamProxy.WatchTeamId_limited;
			if (flag)
			{
				BaseProxy<TeamProxy>.getInstance().SendApplyJoinTeam(TeamProxy.wantedWatchTeamId);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_TEAMMEMBERLIST);
			}
			else
			{
				flytxt.instance.fly("未到可加入等级！", 0, default(Color), null);
			}
		}

		private void getProfessionSprite()
		{
			this.iconSpriteDic = new Dictionary<uint, Sprite>();
			for (int i = 2; i <= 5; i++)
			{
				this.iconSpriteDic.Add((uint)i, Resources.Load<Sprite>(this.getProfession(i)));
			}
		}

		private string getProfession(int profession)
		{
			string result = string.Empty;
			switch (profession)
			{
			case 2:
				result = "icon/job_icon/h2";
				break;
			case 3:
				result = "icon/job_icon/h3";
				break;
			case 4:
				result = "icon/job_icon/h4";
				break;
			case 5:
				result = "icon/job_icon/h5";
				break;
			}
			return result;
		}

		public override void onClosed()
		{
			for (int i = 0; i < this.itemTeamMemberList.Count; i++)
			{
				UnityEngine.Object.Destroy(this.itemTeamMemberList[i].root.gameObject);
			}
			this.itemTeamMemberList.Clear();
		}
	}
}
