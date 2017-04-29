using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class toggleGropFriend : toggleGropFriendBase
	{
		public Toggle togFriend;

		public static toggleGropFriend _instance;

		private Toggle togBlackList;

		private Toggle togEnemy;

		private Toggle togNearby;

		public new void init()
		{
			toggleGropFriend._instance = this;
			this.togFriend = toggleGropFriendBase.mTransform.FindChild("mainBody/left/toggleGroup/togFriend").GetComponent<Toggle>();
			this.togBlackList = toggleGropFriendBase.mTransform.FindChild("mainBody/left/toggleGroup/togBlacklist").GetComponent<Toggle>();
			this.togEnemy = toggleGropFriendBase.mTransform.FindChild("mainBody/left/toggleGroup/togEnemy").GetComponent<Toggle>();
			this.togNearby = toggleGropFriendBase.mTransform.FindChild("mainBody/left/toggleGroup/togNearby").GetComponent<Toggle>();
			this.togFriend.onValueChanged.AddListener(new UnityAction<bool>(this.onTogFriendClick));
			this.togBlackList.onValueChanged.AddListener(new UnityAction<bool>(this.onTogBlackListClick));
			this.togEnemy.onValueChanged.AddListener(new UnityAction<bool>(this.onTogEnemyListClick));
			this.togNearby.onValueChanged.AddListener(new UnityAction<bool>(this.onTogNearbyListClick));
		}

		private void onTogFriendClick(bool b)
		{
			bool flag = !b;
			if (!flag)
			{
				bool flag2 = this.togFriendType == ToggleFriend.Friend;
				if (!flag2)
				{
					this.togFriendType = ToggleFriend.Friend;
					toggleGropFriendBase.mFriendList.show();
					bool isActive = toggleGropFriendBase.mBlackList.isActive;
					if (isActive)
					{
						toggleGropFriendBase.mBlackList.close();
					}
					bool isActive2 = toggleGropFriendBase.mEnemyList.isActive;
					if (isActive2)
					{
						toggleGropFriendBase.mEnemyList.close();
					}
					bool isActive3 = toggleGropFriendBase.mNeighborList.isActive;
					if (isActive3)
					{
						toggleGropFriendBase.mNeighborList.close();
					}
					friendList._instance.hidActionPanel();
				}
			}
		}

		private void onTogBlackListClick(bool b)
		{
			bool flag = !b;
			if (!flag)
			{
				bool flag2 = this.togFriendType == ToggleFriend.BlackList;
				if (!flag2)
				{
					this.togFriendType = ToggleFriend.BlackList;
					toggleGropFriendBase.mBlackList.show();
					bool isActive = toggleGropFriendBase.mFriendList.isActive;
					if (isActive)
					{
						toggleGropFriendBase.mFriendList.close();
					}
					bool isActive2 = toggleGropFriendBase.mEnemyList.isActive;
					if (isActive2)
					{
						toggleGropFriendBase.mEnemyList.close();
					}
					bool isActive3 = toggleGropFriendBase.mNeighborList.isActive;
					if (isActive3)
					{
						toggleGropFriendBase.mNeighborList.close();
					}
					friendList._instance.hidActionPanel();
				}
			}
		}

		private void onTogEnemyListClick(bool b)
		{
			bool flag = !b;
			if (!flag)
			{
				bool flag2 = this.togFriendType == ToggleFriend.Enemy;
				if (!flag2)
				{
					this.togFriendType = ToggleFriend.Enemy;
					toggleGropFriendBase.mEnemyList.show();
					bool isActive = toggleGropFriendBase.mFriendList.isActive;
					if (isActive)
					{
						toggleGropFriendBase.mFriendList.close();
					}
					bool isActive2 = toggleGropFriendBase.mBlackList.isActive;
					if (isActive2)
					{
						toggleGropFriendBase.mBlackList.close();
					}
					bool isActive3 = toggleGropFriendBase.mNeighborList.isActive;
					if (isActive3)
					{
						toggleGropFriendBase.mNeighborList.close();
					}
					friendList._instance.hidActionPanel();
				}
			}
		}

		public void onTogNearbyListClick(bool b)
		{
			bool flag = !b;
			if (!flag)
			{
				bool flag2 = this.togFriendType == ToggleFriend.Nearby;
				if (!flag2)
				{
					this.togFriendType = ToggleFriend.Nearby;
					toggleGropFriendBase.mNeighborList.show();
					bool isActive = toggleGropFriendBase.mFriendList.isActive;
					if (isActive)
					{
						toggleGropFriendBase.mFriendList.close();
					}
					bool isActive2 = toggleGropFriendBase.mBlackList.isActive;
					if (isActive2)
					{
						toggleGropFriendBase.mBlackList.close();
					}
					bool isActive3 = toggleGropFriendBase.mEnemyList.isActive;
					if (isActive3)
					{
						toggleGropFriendBase.mEnemyList.close();
					}
					friendList._instance.hidActionPanel();
				}
			}
		}
	}
}
