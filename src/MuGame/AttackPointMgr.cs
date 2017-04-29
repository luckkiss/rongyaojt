using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class AttackPointMgr
	{
		public static AttackPointMgr instacne;

		private Dictionary<BaseRole, lHurt> dHurt;

		private TickItem process;

		private int curTick = 0;

		public static AttackPointMgr init()
		{
			bool flag = AttackPointMgr.instacne == null;
			if (flag)
			{
				AttackPointMgr.instacne = new AttackPointMgr();
			}
			return AttackPointMgr.instacne;
		}

		public AttackPointMgr()
		{
			this.dHurt = new Dictionary<BaseRole, lHurt>();
			this.process = new TickItem(new Action<float>(this.onUpdate));
			TickMgr.instance.addTick(this.process);
		}

		public void setHurt(BaseRole from, BaseRole to, hurtInfo hurtD, bool useCombo = false)
		{
			bool flag = !this.dHurt.ContainsKey(from);
			if (flag)
			{
				this.dHurt[from] = new lHurt();
			}
			HurtData d = new HurtData(to, hurtD, useCombo);
			this.dHurt[from].add(d);
		}

		public void clear()
		{
			FightText.clear();
			this.dHurt.Clear();
		}

		private void onUpdate(float s)
		{
			this.curTick++;
			bool flag = this.curTick < 40;
			if (!flag)
			{
				float time = Time.time;
				BaseRole baseRole = null;
				foreach (BaseRole current in this.dHurt.Keys)
				{
					lHurt lHurt = this.dHurt[current];
					this.doAtt(lHurt);
					bool flag2 = baseRole == null && lHurt.Count == 0 && time - lHurt.lastTimer > 5f;
					if (flag2)
					{
						baseRole = current;
					}
				}
				bool flag3 = baseRole != null;
				if (flag3)
				{
					this.dHurt.Remove(baseRole);
				}
			}
		}

		private void doAtt(lHurt lHurtD)
		{
			bool flag = lHurtD.Count == 0;
			if (!flag)
			{
				float time = Time.time;
				this.curTick = 0;
				List<HurtData> l = lHurtD.l;
				for (int i = 0; i < l.Count; i++)
				{
					HurtData hurtData = l[i];
					bool flag2 = time - hurtData.timer > 2f;
					if (flag2)
					{
						bool flag3 = hurtData.play(lHurtD.maxComboNum);
						bool flag4 = flag3;
						if (flag4)
						{
							lHurtD.l.Remove(hurtData);
							i--;
						}
					}
				}
			}
		}

		public void onAttackHanle(BaseRole go, int skillid)
		{
			bool flag = !this.dHurt.ContainsKey(go);
			if (!flag)
			{
				lHurt lHurt = this.dHurt[go];
				bool flag2 = lHurt.Count == 0;
				if (!flag2)
				{
					List<HurtData> l = lHurt.l;
					for (int i = 0; i < l.Count; i++)
					{
						HurtData hurtData = l[i];
						bool flag3 = hurtData.play(lHurt.maxComboNum);
						bool flag4 = flag3;
						if (flag4)
						{
							i--;
							l.Remove(hurtData);
						}
					}
				}
			}
		}

		public void onAttackShake(float time, int count, float str)
		{
			GRMap.GAME_CAM_CAMERA.DOShakePosition(time, str, count, 90f);
		}

		public void onAttackBeginHanle(BaseRole go, int num)
		{
			bool flag = !this.dHurt.ContainsKey(go);
			if (flag)
			{
				this.dHurt[go] = new lHurt();
			}
			this.dHurt[go].maxComboNum = num;
		}
	}
}
