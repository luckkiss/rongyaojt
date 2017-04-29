using System;

namespace MuGame
{
	internal class actionPanelPrefab : toggleGropFriendBase
	{
		public static actionPanelPrefab _instance;

		public BaseButton btnChat;

		public BaseButton btnWatch;

		public BaseButton btnTeam;

		public BaseButton btnDelete;

		public BaseButton btnBlackList;

		public BaseButton btnCloseBg;

		public uint cid;

		public string name;

		public uint zhuan;

		public int lvl;

		public new void init()
		{
			actionPanelPrefab._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("itemPrefabs/actionPanel");
			this.btnChat = new BaseButton(this.root.FindChild("buttons/btnChat"), 1, 1);
			this.btnWatch = new BaseButton(this.root.FindChild("buttons/btnWatch"), 1, 1);
			this.btnTeam = new BaseButton(this.root.FindChild("buttons/btnTeam"), 1, 1);
			this.btnDelete = new BaseButton(this.root.FindChild("buttons/btnDelete"), 1, 1);
			this.btnBlackList = new BaseButton(this.root.FindChild("buttons/btnBlackList"), 1, 1);
			this.btnCloseBg = new BaseButton(this.root.FindChild("btnCloseBg"), 1, 1);
		}
	}
}
