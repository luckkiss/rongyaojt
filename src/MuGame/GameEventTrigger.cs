using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class GameEventTrigger : TriggerHanldePoint
	{
		private static Dictionary<GameEventTrigger, int> dListeners = new Dictionary<GameEventTrigger, int>();

		public static Transform EVENT_CON;

		public new int type;

		public int curNum;

		private int targetId;

		private int maxNum;

		public static void add(GameEventTrigger listener)
		{
			GameEventTrigger.dListeners[listener] = 1;
		}

		public static void remove(GameEventTrigger listener)
		{
			listener.dispose();
			GameEventTrigger.dListeners.Remove(listener);
		}

		public static void clear()
		{
			foreach (GameEventTrigger current in GameEventTrigger.dListeners.Keys)
			{
				current.dispose();
			}
			GameEventTrigger.dListeners.Clear();
		}

		public override void onTriggerHanlde()
		{
			bool flag = GameEventTrigger.EVENT_CON == null;
			if (flag)
			{
				GameObject gameObject = new GameObject();
				GameEventTrigger.EVENT_CON = gameObject.transform;
				gameObject.name = "currentEventListener";
			}
			bool flag2 = this.type == 1;
			if (flag2)
			{
				bool flag3 = this.paramInts.Count < 2;
				if (!flag3)
				{
					this.targetId = this.paramInts[0];
					this.maxNum = this.paramInts[1];
					MonsterMgr._inst.addEventListener(MonsterMgr.EVENT_MONSTER_REMOVED, new Action<GameEvent>(this.onMonsterRemoved));
					base.transform.SetParent(GameEventTrigger.EVENT_CON);
					GameEventTrigger.add(this);
				}
			}
			else
			{
				bool flag4 = this.type == 2;
				if (flag4)
				{
					MonsterMgr._inst.addEventListener(MonsterMgr.EVENT_ROLE_BORN, new Action<GameEvent>(this.onRoleBorn));
					base.transform.SetParent(GameEventTrigger.EVENT_CON);
					GameEventTrigger.add(this);
				}
				else
				{
					bool flag5 = this.type == 3;
					if (flag5)
					{
						bool flag6 = this.paramFloat.Count < 1;
						if (!flag6)
						{
							base.Invoke("ontimeout", this.paramFloat[0]);
							base.transform.SetParent(GameEventTrigger.EVENT_CON);
							GameEventTrigger.add(this);
						}
					}
				}
			}
		}

		private void ontimeout()
		{
			base.CancelInvoke("ontimeout");
			this.doit();
			GameEventTrigger.remove(this);
		}

		private void onRoleBorn(GameEvent e)
		{
			this.doit();
			GameEventTrigger.remove(this);
		}

		private void onMonsterRemoved(GameEvent e)
		{
			MonsterRole monsterRole = (MonsterRole)e.orgdata;
			bool flag = this.targetId == 0 || this.targetId == monsterRole.monsterid;
			if (flag)
			{
				this.curNum++;
				bool flag2 = this.curNum >= this.maxNum;
				if (flag2)
				{
					this.doit();
					GameEventTrigger.remove(this);
				}
			}
		}

		private void doit()
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				TriggerHanldePoint component = base.transform.GetChild(i).GetComponent<TriggerHanldePoint>();
				bool flag = component != null;
				if (flag)
				{
					component.onTriggerHanlde();
				}
			}
		}

		public void dispose()
		{
			MonsterMgr._inst.removeEventListener(MonsterMgr.EVENT_ROLE_BORN, new Action<GameEvent>(this.onRoleBorn));
			MonsterMgr._inst.removeEventListener(MonsterMgr.EVENT_MONSTER_REMOVED, new Action<GameEvent>(this.onMonsterRemoved));
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
