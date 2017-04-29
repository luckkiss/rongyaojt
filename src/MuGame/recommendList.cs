using System;
using UnityEngine;

namespace MuGame
{
	internal class recommendList : toggleGropFriendBase
	{
		public static recommendList _instance;

		public Transform containt;

		public new void init()
		{
			recommendList._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("mainBody/myFriendsPanel/hidPanels/personalsPanel");
			this.containt = this.root.FindChild("main/scroll/containts");
		}
	}
}
