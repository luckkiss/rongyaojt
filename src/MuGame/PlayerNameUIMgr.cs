using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class PlayerNameUIMgr
	{
		private static PlayerNameUIMgr instacne;

		private TickItem process;

		private List<PlayerNameItem> lPool;

		private Dictionary<INameObj, PlayerNameItem> dItem;

		private List<PlayerNameItem> lItem;

		private Transform playerNameLayer;

		private PlayerNameItem carItem;

		private INameObj carObj;

		private int hp_per = 100;

		private int tick = 0;

		private List<ActiveItem> lActiveItem;

		private List<ActiveItem> lActiveItemPool;

		public static PlayerNameUIMgr getInstance()
		{
			bool flag = PlayerNameUIMgr.instacne == null;
			if (flag)
			{
				PlayerNameUIMgr.instacne = new PlayerNameUIMgr();
			}
			return PlayerNameUIMgr.instacne;
		}

		public PlayerNameUIMgr()
		{
			this.lItem = new List<PlayerNameItem>();
			this.dItem = new Dictionary<INameObj, PlayerNameItem>();
			this.lPool = new List<PlayerNameItem>();
			this.playerNameLayer = GameObject.Find("playername").transform;
			this.process = new TickItem(new Action<float>(this.onUpdate));
			this.lActiveItem = new List<ActiveItem>();
			this.lActiveItemPool = new List<ActiveItem>();
			TickMgr.instance.addTick(this.process);
		}

		public void show(INameObj avatar)
		{
			bool flag = this.dItem.ContainsKey(avatar);
			if (!flag)
			{
				bool flag2 = this.lPool.Count == 0;
				PlayerNameItem playerNameItem;
				if (flag2)
				{
					GameObject original = Resources.Load("prefab/name_user") as GameObject;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					gameObject.transform.SetParent(this.playerNameLayer, false);
					playerNameItem = new PlayerNameItem(gameObject.transform);
				}
				else
				{
					playerNameItem = this.lPool[0];
					playerNameItem.visiable = true;
					this.lPool.RemoveAt(0);
				}
				playerNameItem.refresh(avatar);
				this.lItem.Add(playerNameItem);
				this.dItem[avatar] = playerNameItem;
				bool flag3 = avatar is MonsterRole;
				if (flag3)
				{
					bool flag4 = ModelBase<A3_ActiveModel>.getInstance() != null;
					if (flag4)
					{
						bool flag5 = ModelBase<A3_ActiveModel>.getInstance().mwlr_target_monId == 0 || (avatar as MonsterRole).monsterid != ModelBase<A3_ActiveModel>.getInstance().mwlr_target_monId;
						if (flag5)
						{
							playerNameItem.mhPlayerName.transform.parent.gameObject.SetActive(false);
						}
						else
						{
							playerNameItem.mhPlayerName.transform.parent.gameObject.SetActive(true);
						}
					}
					else
					{
						playerNameItem.mhPlayerName.transform.parent.gameObject.SetActive(false);
					}
				}
				else
				{
					playerNameItem.mhPlayerName.transform.parent.gameObject.SetActive(false);
				}
				bool flag6 = avatar is MDC000;
				if (flag6)
				{
					this.carObj = avatar;
					playerNameItem.setHP(((float)(avatar.curhp / avatar.maxHp) * 100f).ToString(), false);
					this.carItem = playerNameItem;
					playerNameItem.refreshHp(avatar.curhp, avatar.maxHp, avatar);
				}
				else
				{
					playerNameItem.cartxt.gameObject.SetActive(false);
					playerNameItem.refreshHp(avatar.curhp, avatar.maxHp, null);
				}
				playerNameItem.refreicon();
				playerNameItem.refreshVipLv(0u);
				bool flag7 = avatar is ProfessionRole;
				if (flag7)
				{
					bool isMain = (avatar as ProfessionRole).m_isMain;
					if (isMain)
					{
						bool istitleActive = ModelBase<PlayerModel>.getInstance().istitleActive;
						if (istitleActive)
						{
							playerNameItem.refreshTitle(a3_RankModel.now_id);
						}
						playerNameItem.refresNameColor(ModelBase<PlayerModel>.getInstance().now_nameState);
						playerNameItem.refresHitback((int)ModelBase<PlayerModel>.getInstance().hitBack, false);
						playerNameItem.refreshVipLv((uint)ModelBase<A3_VipModel>.getInstance().Level);
					}
					else
					{
						playerNameItem.refreshTitle(avatar.title_id);
						playerNameItem.refresNameColor(avatar.rednm);
						playerNameItem.refresHitback((int)avatar.hidbacktime, false);
					}
				}
				else
				{
					playerNameItem.refreshTitle(avatar.title_id);
				}
			}
		}

		public void carinfo(GameEvent e)
		{
			this.carItem.refreshHp(this.carObj.curhp - 1, this.carObj.maxHp, null);
			this.hp_per = e.data["hp_per"];
			this.carItem.hp.localScale = new Vector3((float)this.hp_per / 100f, 1f, 1f);
			bool flag = e.data["hp_per"] <= 20;
			if (flag)
			{
				this.carItem.setHP(this.hp_per.ToString(), true);
			}
			else
			{
				this.carItem.setHP(this.hp_per.ToString(), false);
			}
		}

		public void refreshTitlelv(INameObj role, int title_id)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				PlayerNameItem playerNameItem = this.dItem[role];
				playerNameItem.refreshTitle(title_id);
			}
		}

		public void refreshmapCount(INameObj role, int count, bool ismine)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				bool flag2 = !(role is ProfessionRole);
				if (!flag2)
				{
					PlayerNameItem playerNameItem = this.dItem[role];
					playerNameItem.refreshMapcount(count, ismine);
				}
			}
		}

		public void refreserialCount(INameObj role, int count)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				bool flag2 = !(role is ProfessionRole);
				if (!flag2)
				{
					PlayerNameItem playerNameItem = this.dItem[role];
					playerNameItem.refreshserial(count);
				}
			}
		}

		public void refreshNameColor(INameObj role, int rednmstate)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				PlayerNameItem playerNameItem = this.dItem[role];
				playerNameItem.refresNameColor(rednmstate);
			}
		}

		public void refresHitback(INameObj role, int time, bool ismyself = false)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				PlayerNameItem playerNameItem = this.dItem[role];
				if (ismyself)
				{
					playerNameItem.refresHitback(time, true);
				}
				else
				{
					playerNameItem.refresHitback(time, false);
				}
			}
		}

		public void hideAll()
		{
			this.playerNameLayer.gameObject.SetActive(false);
		}

		public void showAll()
		{
			this.playerNameLayer.gameObject.SetActive(true);
		}

		public void refreshVipLv(INameObj role, uint viplv)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				PlayerNameItem playerNameItem = this.dItem[role];
				playerNameItem.refreshVipLv(viplv);
			}
		}

		public void setName(INameObj role, string sumname, string mastername)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				PlayerNameItem playerNameItem = this.dItem[role];
				playerNameItem.setName(sumname, mastername);
			}
		}

		public void setDartName(INameObj role, string legionName)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				PlayerNameItem playerNameItem = this.dItem[role];
				playerNameItem.setDartName(legionName);
			}
		}

		public void seticon_forDaobao(INameObj role, int num)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				PlayerNameItem playerNameItem = this.dItem[role];
				playerNameItem.seticon_forDaobao(num);
			}
		}

		public void seticon_forMonsterHunter(INameObj role, bool hide = false)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				PlayerNameItem playerNameItem = this.dItem[role];
				playerNameItem.show_mhMark(role, hide);
			}
		}

		public void hide(INameObj role)
		{
			bool flag = !this.dItem.ContainsKey(role);
			if (!flag)
			{
				PlayerNameItem playerNameItem = this.dItem[role];
				playerNameItem.clear();
				playerNameItem.visiable = false;
				this.dItem.Remove(role);
				this.lItem.Remove(playerNameItem);
				this.lPool.Add(playerNameItem);
			}
		}

		public void refreshHp(INameObj role, Variant d)
		{
			bool flag = this.dItem.ContainsKey(role);
			if (flag)
			{
				this.dItem[role].refreshHp(d["cur"], d["max"], null);
			}
		}

		public void refreshHp(INameObj role, int cur, int max)
		{
			bool flag = this.dItem.ContainsKey(role);
			if (flag)
			{
				bool flag2 = role is MDC000;
				if (flag2)
				{
					this.dItem[role].refreshHp(cur, max, role);
				}
				else
				{
					this.dItem[role].refreshHp(cur, max, null);
				}
			}
		}

		private void onUpdate(float s)
		{
			bool flag = this.lItem.Count > 0;
			if (flag)
			{
				foreach (PlayerNameItem current in this.lItem)
				{
					current.update();
				}
			}
			bool flag2 = this.lActiveItem.Count > 0;
			if (flag2)
			{
				List<ActiveItem> list = new List<ActiveItem>();
				foreach (ActiveItem current2 in this.lActiveItem)
				{
					bool flag3 = current2.update();
					if (flag3)
					{
						list.Add(current2);
					}
				}
				foreach (ActiveItem current3 in list)
				{
					this.clearActive(current3);
				}
			}
		}

		public void clearActive(ActiveItem item)
		{
			item.clear();
			this.lActiveItem.Remove(item);
			this.lActiveItemPool.Add(item);
		}
	}
}
