using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class itemEnemyListPrefab : toggleGropFriendBase
	{
		public new Transform root;

		public Toggle toggle;

		public Text txtNickName;

		public Text txtLvl;

		public Text txtTeam;

		public Text txtCombat;

		public Text txthatred;

		public static Text txtPos;

		public static Text txtTimer;

		public uint cid;

		private string name;

		private uint zhuan;

		private int lvl;

		public bool isCdNow = false;

		private Transform posAction;

		public TickItem postionTime;

		public float times = 0f;

		public int i;

		public new void init()
		{
			Transform transform = toggleGropFriendBase.mTransform.FindChild("itemPrefabs/itemEnemy");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
			this.root = gameObject.transform;
			this.toggle = this.root.FindChild("Toggle").GetComponent<Toggle>();
			this.txtNickName = this.toggle.transform.FindChild("containts/txtName").GetComponent<Text>();
			this.txtLvl = this.toggle.transform.FindChild("containts/txtLevel").GetComponent<Text>();
			this.txtTeam = this.toggle.transform.FindChild("containts/txtTeam").GetComponent<Text>();
			this.txtCombat = this.toggle.transform.FindChild("containts/txtcombat").GetComponent<Text>();
			bool flag = this.toggle.transform.FindChild("containts/txthatred") != null;
			if (flag)
			{
				this.txthatred = this.toggle.transform.FindChild("containts/txthatred").GetComponent<Text>();
			}
			itemEnemyListPrefab.txtPos = this.toggle.transform.FindChild("containts/txtpos").GetComponent<Text>();
			itemEnemyListPrefab.txtTimer = this.toggle.transform.FindChild("containts/txtpos/txtTimer").GetComponent<Text>();
			BaseButton baseButton = new BaseButton(this.root.FindChild("Toggle/containts/btnAction"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnActionClick);
			gameObject.SetActive(true);
			this.root.SetParent(enemyList._instance.containt, false);
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
			this.txthatred.text = data.hatred.ToString();
			string text = string.Empty;
			text = friendList._instance.getMapNameById(data.map_id);
			itemEnemyListPrefab.txtPos.text = text;
			this.cid = data.cid;
			this.name = data.name;
			this.zhuan = data.zhuan;
			this.lvl = data.lvl;
			itemEnemyListPrefab.txtPos = this.root.FindChild("Toggle/containts/txtpos").GetComponent<Text>();
			itemEnemyListPrefab.txtTimer = this.root.FindChild("Toggle/containts/txtpos/txtTimer").GetComponent<Text>();
			this.posAction = this.root.FindChild("Toggle/containts/btnAction/actionPanelPos");
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
			this.txthatred.text = data.hatred.ToString();
			this.txtCombat.text = data.combpt.ToString();
			itemEnemyListPrefab.txtPos.text = friendList._instance.getMapNameById(data.map_id);
			this.cid = data.cid;
			this.name = data.name;
			this.zhuan = data.zhuan;
			this.lvl = data.lvl;
			itemEnemyListPrefab.txtPos = this.root.FindChild("Toggle/containts/txtpos").GetComponent<Text>();
			itemEnemyListPrefab.txtTimer = this.root.FindChild("Toggle/containts/txtpos/txtTimer").GetComponent<Text>();
			this.posAction = this.root.FindChild("Toggle/containts/btnAction/actionPanelPos");
			data.root = this.root;
		}

		public void UpdatePos(int mapId = -1)
		{
			bool flag = mapId == -1;
			if (flag)
			{
				itemEnemyListPrefab.txtPos.text = "未知";
			}
			else
			{
				itemEnemyListPrefab.txtPos.text = friendList._instance.getMapNameById(mapId);
			}
		}

		private void onUpdateEnmeyPostion(float s)
		{
			bool flag = itemEnemyListPrefab.txtPos == null || itemEnemyListPrefab.txtTimer == null;
			if (!flag)
			{
				this.times += s;
				bool flag2 = this.times >= 1f;
				if (flag2)
				{
					this.i--;
					bool flag3 = this.i <= 0;
					if (flag3)
					{
						this.i = 0;
						bool flag4 = !BaseProxy<FriendProxy>.getInstance().FriendDataList.ContainsKey(this.cid);
						if (flag4)
						{
							itemEnemyListPrefab.txtPos.text = "未知";
						}
						itemEnemyListPrefab.txtTimer.text = "";
						this.isCdNow = false;
						TickMgr.instance.removeTick(this.postionTime);
						this.postionTime = null;
					}
					else
					{
						this.isCdNow = true;
						itemEnemyListPrefab.txtTimer.text = "(" + this.i + ")";
					}
					this.times = 0f;
				}
			}
		}

		public void refresShowPostion(int time)
		{
			itemEnemyListPrefab.txtPos = (itemEnemyListPrefab.txtPos = this.root.FindChild("Toggle/containts/txtpos").GetComponent<Text>());
			itemEnemyListPrefab.txtTimer = this.root.FindChild("Toggle/containts/txtpos/txtTimer").GetComponent<Text>();
			bool flag = time <= 0;
			if (flag)
			{
				itemEnemyListPrefab.txtPos.text = "未知";
				itemEnemyListPrefab.txtTimer.text = "";
			}
			else
			{
				this.postionTime = new TickItem(new Action<float>(this.onUpdateEnmeyPostion));
				TickMgr.instance.addTick(this.postionTime);
				this.i = time;
			}
		}

		private void onBtnActionClick(GameObject go)
		{
			itemFriendData ifd = default(itemFriendData);
			ifd.cid = this.cid;
			ifd.name = this.name;
			ifd.zhuan = this.zhuan;
			ifd.lvl = this.lvl;
			actionEnemyPanel._instance.Show(ifd);
			actionEnemyPanel._instance.root.position = this.posAction.position;
		}
	}
}
