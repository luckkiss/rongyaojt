using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class itemBlackListPrefab : toggleGropFriendBase
	{
		public new Transform root;

		public Toggle toggle;

		public Text txtNickName;

		public Text txtLvl;

		public Text txtTeam;

		public Text txtCombat;

		public Text txtPos;

		public BaseButton btnAction;

		private Transform actionPanelPos;

		private uint cid;

		private string name;

		private uint zhuan;

		private int lvl;

		public new void init()
		{
			Transform transform = toggleGropFriendBase.mTransform.FindChild("itemPrefabs/itemBlackList");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
			this.root = gameObject.transform;
			this.toggle = this.root.FindChild("Toggle").GetComponent<Toggle>();
			this.txtNickName = this.toggle.transform.FindChild("containts/txtName").GetComponent<Text>();
			this.txtLvl = this.toggle.transform.FindChild("containts/txtLevel").GetComponent<Text>();
			this.txtTeam = this.toggle.transform.FindChild("containts/txtTeam").GetComponent<Text>();
			this.txtCombat = this.toggle.transform.FindChild("containts/txtcombat").GetComponent<Text>();
			this.txtPos = this.toggle.transform.FindChild("containts/txtpos").GetComponent<Text>();
			this.btnAction = new BaseButton(this.root.transform.FindChild("btnAction"), 1, 1);
			this.actionPanelPos = this.root.FindChild("btnAction/actionPanelPos");
			this.btnAction.onClick = new Action<GameObject>(this.onBtnActionClick);
			gameObject.SetActive(true);
			this.root.SetParent(blackList._instance.containt, false);
		}

		public void show(itemFriendData data, out Transform rootT)
		{
			this.txtNickName.text = data.name;
			this.txtLvl.text = string.Concat(new object[]
			{
				data.zhuan,
				"转",
				data.lvl,
				"级"
			});
			this.txtTeam.text = data.clan_name;
			this.txtCombat.text = data.combpt.ToString();
			this.txtPos.text = friendList._instance.getMapNameById(data.map_id);
			this.cid = data.cid;
			data.root = this.root;
			rootT = this.root;
		}

		public void set(itemFriendData data)
		{
			this.root.gameObject.SetActive(true);
			this.txtNickName.text = data.name;
			this.txtLvl.text = string.Concat(new object[]
			{
				data.zhuan,
				"转",
				data.lvl,
				"级"
			});
			this.txtTeam.text = data.clan_name;
			this.txtCombat.text = data.combpt.ToString();
			this.txtPos.text = friendList._instance.getMapNameById(data.map_id);
			this.cid = data.cid;
			data.root = this.root;
		}

		private void onBtnActionClick(GameObject go)
		{
			itemFriendData ifd = default(itemFriendData);
			ifd.cid = this.cid;
			bool flag = !actionBlackListPanelPrefab._instance.root.gameObject.activeSelf;
			if (flag)
			{
				actionBlackListPanelPrefab._instance.root.position = this.actionPanelPos.position;
				actionBlackListPanelPrefab._instance.Show(ifd);
			}
			else
			{
				bool flag2 = actionBlackListPanelPrefab._instance.root.position.y != this.actionPanelPos.position.y;
				if (flag2)
				{
					actionBlackListPanelPrefab._instance.root.position = this.actionPanelPos.position;
				}
				else
				{
					actionBlackListPanelPrefab._instance.root.gameObject.SetActive(false);
				}
			}
		}
	}
}
