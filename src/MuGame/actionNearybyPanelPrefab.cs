using System;

namespace MuGame
{
	internal class actionNearybyPanelPrefab : toggleGropFriendBase
	{
		public static actionNearybyPanelPrefab _instance;

		public BaseButton btnChat;

		public BaseButton btnWatch;

		public BaseButton btnTeam;

		public BaseButton btnAdd;

		public BaseButton btnBlackList;

		public BaseButton btnCloseBg;

		public uint cid;

		public string name;

		public new void init()
		{
			actionNearybyPanelPrefab._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("itemPrefabs/actionNearybyPanel");
			this.btnChat = new BaseButton(this.root.FindChild("buttons/btnChat"), 1, 1);
			this.btnWatch = new BaseButton(this.root.FindChild("buttons/btnWatch"), 1, 1);
			this.btnTeam = new BaseButton(this.root.FindChild("buttons/btnTeam"), 1, 1);
			this.btnAdd = new BaseButton(this.root.FindChild("buttons/btnAdd"), 1, 1);
			this.btnBlackList = new BaseButton(this.root.FindChild("buttons/btnBlackList"), 1, 1);
			this.btnCloseBg = new BaseButton(this.root.FindChild("btnCloseBg"), 1, 1);
		}
	}
}
