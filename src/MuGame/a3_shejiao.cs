using GameFramework;
using System;
using System.Collections;
using UnityEngine;

namespace MuGame
{
	internal class a3_shejiao : Window
	{
		private TabControl tab;

		public static a3_shejiao instance;

		private Transform con;

		private BaseShejiao current = null;

		private BaseShejiao friend = null;

		private BaseShejiao legion = null;

		private BaseShejiao teamList = null;

		private BaseShejiao currentTeam = null;

		private bool Toclose = false;

		public TabControl Tab
		{
			get
			{
				return this.tab;
			}
		}

		public override void init()
		{
			this.con = base.getTransformByPath("con");
			a3_shejiao.instance = this;
			this.tab = new TabControl();
			this.tab.onClickHanle = new Action<TabControl>(this.OnSwitch);
			this.tab.create(base.getGameObjectByPath("tab"), base.gameObject, 0, 0, false);
			BaseButton baseButton = new BaseButton(base.getTransformByPath("close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnClose);
			UIClient.instance.addEventListener(17001u, new Action<GameEvent>(this.OnOpenPanel));
		}

		public override void onShowed()
		{
			bool flag = this.uiData == null;
			if (flag)
			{
				bool flag2 = this.current != null;
				if (flag2)
				{
					this.current.onShowed();
				}
				else
				{
					this.tab.setSelectedIndex(0, false);
					this.OnSwitch(this.tab);
				}
			}
			else
			{
				int index = (int)this.uiData[0];
				bool flag3 = this.uiData.Count > 1;
				if (flag3)
				{
					ModelBase<A3_LegionModel>.getInstance().showtype = (int)this.uiData[1];
				}
				this.tab.setSelectedIndex(index, false);
				this.OnSwitch(this.tab);
			}
			bool flag4 = this.teamList != null && (this.teamList == this.current || this.currentTeam == this.current);
			if (flag4)
			{
				BaseProxy<TeamProxy>.getInstance().SetTeamPanelInfo();
			}
			bool flag5 = GRMap.GAME_CAMERA != null;
			if (flag5)
			{
				GRMap.GAME_CAMERA.SetActive(false);
			}
			this.Toclose = false;
		}

		public override void onClosed()
		{
			bool flag = this.current != null;
			if (flag)
			{
				this.current.onClose();
			}
			bool flag2 = GRMap.GAME_CAMERA != null;
			if (flag2)
			{
				GRMap.GAME_CAMERA.SetActive(true);
			}
			InterfaceMgr.getInstance().itemToWin(this.Toclose, this.uiName);
		}

		public void SetIndexToFriend()
		{
			bool flag = this.friend == null;
			if (flag)
			{
				GameObject original = Resources.Load<GameObject>("prefab/a3_friend");
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
				this.friend = new a3_friend(gameObject.transform);
				this.friend.setPerent(this.con);
			}
			this.current = this.friend;
		}

		private void OnSwitch(TabControl t)
		{
			int seletedIndex = t.getSeletedIndex();
			bool flag = this.current != null;
			if (flag)
			{
				bool flag2 = this.currentTeam != null && this.teamList != null;
				if (flag2)
				{
					this.currentTeam.onClose();
					this.currentTeam.gameObject.SetActive(false);
					this.teamList.onClose();
					this.teamList.gameObject.SetActive(false);
				}
				this.current.onClose();
				this.current.gameObject.SetActive(false);
			}
			bool flag3 = seletedIndex == 0;
			if (flag3)
			{
				bool flag4 = this.legion == null;
				if (flag4)
				{
					GameObject original = Resources.Load<GameObject>("prefab/a3_legion");
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					this.legion = new a3_legion(gameObject.transform);
					this.legion.setPerent(this.con);
					this.legion.transform.localPosition = Vector3.zero;
				}
				this.current = this.legion;
			}
			else
			{
				bool flag5 = seletedIndex == 1;
				if (flag5)
				{
					this.SetIndexToFriend();
				}
				else
				{
					bool flag6 = this.currentTeam == null;
					if (flag6)
					{
						GameObject original2 = Resources.Load<GameObject>("prefab/a3_currentTeamPanel");
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2);
						this.currentTeam = new a3_currentTeamPanel(gameObject2.transform);
						this.currentTeam.setPerent(this.con);
					}
					bool flag7 = this.teamList == null;
					if (flag7)
					{
						GameObject original3 = Resources.Load<GameObject>("prefab/a3_teamPanel");
						GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(original3);
						this.teamList = new a3_teamPanel(gameObject3.transform);
						this.teamList.setPerent(this.con);
					}
					bool flag8 = BaseProxy<TeamProxy>.getInstance().joinedTeam && BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
					if (flag8)
					{
						this.current = this.currentTeam;
						this.currentTeam.gameObject.SetActive(true);
						this.teamList.gameObject.SetActive(false);
					}
					else
					{
						this.current = this.teamList;
						this.currentTeam.gameObject.SetActive(false);
						this.teamList.gameObject.SetActive(true);
					}
					BaseProxy<TeamProxy>.getInstance().SetTeamPanelInfo();
				}
			}
			bool flag9 = this.current != null;
			if (flag9)
			{
				this.current.onShowed();
				this.current.visiable = true;
			}
		}

		private void OnClose(GameObject go)
		{
			bool flag = this.currentTeam != null && this.teamList != null;
			if (flag)
			{
				this.currentTeam.onClose();
				this.currentTeam.gameObject.SetActive(false);
				this.teamList.onClose();
				this.teamList.gameObject.SetActive(false);
			}
			this.Toclose = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SHEJIAO);
		}

		public void ShowFriend(ArrayList dat)
		{
		}

		private void OnOpenPanel(GameEvent e)
		{
			int index = e.data["index"];
			this.tab.setSelectedIndex(index, false);
			this.OnSwitch(this.tab);
		}
	}
}
