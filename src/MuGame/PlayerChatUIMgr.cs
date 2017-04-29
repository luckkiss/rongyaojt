using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class PlayerChatUIMgr
	{
		private static PlayerChatUIMgr instacne;

		private TickItem process;

		private List<PlayerChatItem> lPool;

		private Dictionary<INameObj, PlayerChatItem> dItem;

		private List<PlayerChatItem> lItem;

		private Transform playerChatLayer;

		private int tick = 0;

		private List<ActiveChatItem> lActiveItem;

		private List<ActiveChatItem> lActiveItemPool;

		public static PlayerChatUIMgr getInstance()
		{
			bool flag = PlayerChatUIMgr.instacne == null;
			if (flag)
			{
				PlayerChatUIMgr.instacne = new PlayerChatUIMgr();
			}
			return PlayerChatUIMgr.instacne;
		}

		public PlayerChatUIMgr()
		{
			this.lItem = new List<PlayerChatItem>();
			this.dItem = new Dictionary<INameObj, PlayerChatItem>();
			this.lPool = new List<PlayerChatItem>();
			this.playerChatLayer = GameObject.Find("playerChat").transform;
			this.process = new TickItem(new Action<float>(this.onUpdate));
			this.lActiveItem = new List<ActiveChatItem>();
			this.lActiveItemPool = new List<ActiveChatItem>();
			TickMgr.instance.addTick(this.process);
		}

		public void show(INameObj avatar, string msg)
		{
			bool flag = !this.dItem.ContainsKey(avatar);
			if (flag)
			{
				bool flag2 = this.lPool.Count == 0;
				PlayerChatItem playerChatItem;
				if (flag2)
				{
					GameObject original = Resources.Load("prefab/chat_user") as GameObject;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					gameObject.transform.SetParent(this.playerChatLayer, false);
					playerChatItem = new PlayerChatItem(gameObject.transform);
				}
				else
				{
					playerChatItem = this.lPool[0];
					playerChatItem.visiable = true;
					this.lPool.RemoveAt(0);
				}
				playerChatItem.refresh(avatar, msg);
				this.lItem.Add(playerChatItem);
				this.dItem[avatar] = playerChatItem;
				bool flag3 = avatar is ProfessionRole;
				if (flag3)
				{
					playerChatItem.refresShowChat(3, false);
				}
			}
			else
			{
				this.dItem[avatar].refresh(avatar, msg);
				this.dItem[avatar].refresShowChat(3, false);
			}
		}

		public void hideAll()
		{
			this.playerChatLayer.gameObject.SetActive(false);
		}

		public void showAll()
		{
			this.playerChatLayer.gameObject.SetActive(true);
		}

		public void hide(INameObj role)
		{
			bool flag = role.roleName == ModelBase<PlayerModel>.getInstance().name;
			if (flag)
			{
				debug.Log("a");
			}
			bool flag2 = !this.dItem.ContainsKey(role);
			if (!flag2)
			{
				PlayerChatItem playerChatItem = this.dItem[role];
				playerChatItem.clear();
				playerChatItem.visiable = false;
				this.dItem.Remove(role);
				this.lItem.Remove(playerChatItem);
				this.lPool.Add(playerChatItem);
			}
		}

		private void onUpdate(float s)
		{
			bool flag = this.lItem.Count > 0;
			if (flag)
			{
				foreach (PlayerChatItem current in this.lItem)
				{
					current.update();
				}
			}
			bool flag2 = this.lActiveItem.Count > 0;
			if (flag2)
			{
				List<ActiveChatItem> list = new List<ActiveChatItem>();
				foreach (ActiveChatItem current2 in this.lActiveItem)
				{
					bool flag3 = current2.update();
					if (flag3)
					{
						list.Add(current2);
					}
				}
				foreach (ActiveChatItem current3 in list)
				{
					this.clearActive(current3);
				}
			}
		}

		public void clearActive(ActiveChatItem item)
		{
			item.clear();
			this.lActiveItem.Remove(item);
			this.lActiveItemPool.Add(item);
		}
	}
}
