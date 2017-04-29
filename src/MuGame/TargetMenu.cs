using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class TargetMenu : Skin
	{
		public TargetMenu(Transform trans) : base(trans)
		{
			this.initUI();
			this.hide();
		}

		public void initUI()
		{
			new BaseButton(base.transform.FindChild("black"), 1, 1).onClick = delegate(GameObject g)
			{
				this.addblack();
			};
			new BaseButton(base.transform.FindChild("view"), 1, 1).onClick = delegate(GameObject g)
			{
				this.view();
			};
			new BaseButton(base.transform.FindChild("bg"), 1, 1).onClick = delegate(GameObject g)
			{
				this.hide();
			};
			new BaseButton(base.transform.FindChild("chat"), 1, 1).onClick = delegate(GameObject g)
			{
				this.chat();
			};
			new BaseButton(base.transform.FindChild("addfre"), 1, 1).onClick = delegate(GameObject g)
			{
				this.addfriend();
			};
			new BaseButton(base.transform.FindChild("team"), 1, 1).onClick = delegate(GameObject g)
			{
				this.addTeam();
			};
		}

		private void onCLick(GameObject go)
		{
			this.hide();
		}

		public void show()
		{
			this.visiable = true;
		}

		public void hide()
		{
			this.visiable = false;
		}

		private void view()
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_TARGETINFO, null, false);
			this.hide();
		}

		private void addblack()
		{
			uint unCID = SelfRole._inst.m_LockRole.m_unCID;
			string roleName = SelfRole._inst.m_LockRole.roleName;
			BaseProxy<FriendProxy>.getInstance().sendAddBlackList(unCID, roleName);
			this.hide();
		}

		private void chat()
		{
			a3_chatroom._instance.privateChat(SelfRole._inst.m_LockRole.roleName);
			this.hide();
		}

		private void addfriend()
		{
			uint unCID = SelfRole._inst.m_LockRole.m_unCID;
			string roleName = SelfRole._inst.m_LockRole.roleName;
			BaseProxy<FriendProxy>.getInstance().sendAddFriend(unCID, roleName, true);
			this.hide();
		}

		private void addTeam()
		{
			uint unCID = SelfRole._inst.m_LockRole.m_unCID;
			BaseProxy<TeamProxy>.getInstance().SendTEAM(unCID);
			BaseProxy<TeamProxy>.getInstance().trage_cid = unCID;
			this.hide();
		}
	}
}
