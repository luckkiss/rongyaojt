using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class Friend_searchProxy : BaseProxy<Friend_searchProxy>
	{
		public static uint HAVE_FRIEND = 0u;

		public static uint ON_FRIEND = 1u;

		public static uint RECOMMENTFRIEND = 2u;

		public static uint ADDFRIEND = 3u;

		public static uint LOOKFRIEND = 11u;

		public Friend_searchProxy()
		{
			this.addProxyListener(172u, new Action<Variant>(this.onLoadSearchFriend));
			this.addProxyListener(175u, new Action<Variant>(this.onLoadrecommentFriend));
			this.addProxyListener(170u, new Action<Variant>(this.onAddfriendButton));
		}

		public void sendSearchFriend(string name)
		{
			Variant variant = new Variant();
			variant["name"] = name;
			this.sendRPC(172u, variant);
		}

		public void sendrecommentFriend()
		{
			this.sendRPC(175u, null);
		}

		public void sendaddfriendButton(int id)
		{
			Variant variant = new Variant();
			variant["cid"] = id;
			this.sendRPC(170u, variant);
		}

		public void sendgetplayerinfo(uint cid)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 11;
			variant["cid"] = cid;
			this.sendRPC(170u, variant);
		}

		public void onLoadSearchFriend(Variant data)
		{
			debug.Log("搜索好友：" + data.dump());
			int num = data["res"];
			bool flag = num == 1;
			if (flag)
			{
				base.dispatchEvent(GameEvent.Create(Friend_searchProxy.HAVE_FRIEND, this, data, false));
			}
			else
			{
				base.dispatchEvent(GameEvent.Create(Friend_searchProxy.ON_FRIEND, this, data, false));
			}
		}

		public void onLoadrecommentFriend(Variant data)
		{
			debug.Log("推荐好友：" + data.dump());
			base.dispatchEvent(GameEvent.Create(Friend_searchProxy.RECOMMENTFRIEND, this, data, false));
		}

		public void onAddfriendButton(Variant data)
		{
			debug.Log("好友操作：" + data.dump());
			int num = data["res"];
			bool flag = num == 11;
			if (flag)
			{
				base.dispatchEvent(GameEvent.Create(Friend_searchProxy.LOOKFRIEND, this, data, false));
			}
		}
	}
}
