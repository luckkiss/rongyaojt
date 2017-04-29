using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class StateAttack : StateBase
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly StateAttack.<>c <>9 = new StateAttack.<>c();

			public static Predicate<ItemTeamData> <>9__10_0;

			internal bool <Execute>b__10_0(ItemTeamData m)
			{
				return m.cid == SelfRole._inst.m_LockRole.m_unCID;
			}
		}

		public static StateAttack Instance = new StateAttack();

		private int mon = 0;

		private float exe_action_tm = 0f;

		private float xml_action_tm;

		public static float MinRange = 2f;

		public float StateTimeover
		{
			get;
			set;
		}

		public override void Enter()
		{
			bool flag = this.exe_action_tm < this.xml_action_tm || SelfRole._inst.isPlayingSkill;
			if (!flag)
			{
				BaseProxy<BattleProxy>.getInstance().addEventListener(BattleProxy.EVENT_SELF_KILL_MON, new Action<GameEvent>(this.OnKillMon));
				this.xml_action_tm = 0f;
				while (true)
				{
					this.CheckPK();
					bool flag2 = !SelfRole.UnderPlayerAttack;
					if (flag2)
					{
						break;
					}
					bool flag3 = SelfRole.LastAttackPlayer != null;
					if (flag3)
					{
						goto Block_11;
					}
					SelfRole.UnderPlayerAttack = false;
				}
				bool flag4 = ModelBase<PlayerModel>.getInstance().pk_state > PK_TYPE.PK_PEACE;
				if (flag4)
				{
					BaseRole baseRole = OtherPlayerMgr._inst.FindNearestEnemyOne(SelfRole._inst.m_curModel.transform.position, false, new Func<ProfessionRole, bool>(this.EnemySelector), false, ModelBase<PlayerModel>.getInstance().pk_state);
					bool flag5 = baseRole != null;
					if (flag5)
					{
						SelfRole._inst.m_LockRole = baseRole;
						return;
					}
				}
				bool flag6 = (SelfRole._inst.m_LockRole is ProfessionRole && ModelBase<PlayerModel>.getInstance().pk_state == PK_TYPE.PK_PEACE) || SelfRole._inst.m_LockRole == null;
				if (flag6)
				{
					MonsterRole monsterRole = MonsterMgr._inst.FindNearestMonster(SelfRole._inst.m_curModel.position, null, false, PK_TYPE.PK_PEACE, false);
					bool flag7 = monsterRole == null;
					if (flag7)
					{
						SelfRole.fsm.ChangeState(StateIdle.Instance);
					}
					else
					{
						SelfRole._inst.m_LockRole = monsterRole;
					}
				}
				else
				{
					bool flag8 = SelfRole.fsm.Autofighting && Vector3.Distance(SelfRole._inst.m_LockRole.m_curModel.position.ConvertToGamePosition(), SelfRole._inst.m_curModel.position.ConvertToGamePosition()) > StateInit.Instance.Distance;
					if (flag8)
					{
						SelfRole._inst.m_LockRole = null;
						SelfRole.fsm.ChangeState(StateIdle.Instance);
					}
				}
				return;
				Block_11:
				SelfRole._inst.m_LockRole = SelfRole.LastAttackPlayer;
			}
		}

		public override void Execute(float delta_time)
		{
			this.exe_action_tm += delta_time;
			bool flag = this.exe_action_tm < this.xml_action_tm || SelfRole._inst.isPlayingSkill;
			if (!flag)
			{
				bool flag2 = this.mon > 0;
				if (flag2)
				{
					this.mon--;
					SelfRole.fsm.ChangeState(StatePick.Instance);
				}
				else
				{
					bool flag3 = SelfRole._inst.m_LockRole != null;
					if (flag3)
					{
						bool flag4 = SelfRole._inst.m_LockRole is CollectRole;
						if (flag4)
						{
							SelfRole._inst.m_LockRole = null;
						}
						else
						{
							bool flag5 = ModelBase<PlayerModel>.getInstance().pk_state == PK_TYPE.PK_PEACE;
							if (flag5)
							{
								bool flag6 = SelfRole._inst.m_LockRole is ProfessionRole;
								if (flag6)
								{
									SelfRole._inst.m_LockRole = null;
								}
							}
							else
							{
								bool flag7 = ModelBase<PlayerModel>.getInstance().pk_state == PK_TYPE.PK_TEAM && BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
								if (flag7)
								{
									List<ItemTeamData> arg_132_0 = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList;
									Predicate<ItemTeamData> arg_132_1;
									if ((arg_132_1 = StateAttack.<>c.<>9__10_0) == null)
									{
										arg_132_1 = (StateAttack.<>c.<>9__10_0 = new Predicate<ItemTeamData>(StateAttack.<>c.<>9.<Execute>b__10_0));
									}
									bool flag8 = arg_132_0.Exists(arg_132_1);
									if (flag8)
									{
										SelfRole._inst.m_LockRole = null;
									}
								}
							}
						}
					}
					bool flag9 = SelfRole._inst.m_LockRole == null;
					if (flag9)
					{
						bool flag10 = ModelBase<PlayerModel>.getInstance().pk_state > PK_TYPE.PK_PEACE;
						if (flag10)
						{
							BaseRole baseRole = OtherPlayerMgr._inst.FindNearestEnemyOne(SelfRole._inst.m_curModel.transform.position, false, new Func<ProfessionRole, bool>(this.EnemySelector), false, ModelBase<PlayerModel>.getInstance().pk_state);
							bool flag11 = baseRole != null;
							if (flag11)
							{
								SelfRole._inst.m_LockRole = baseRole;
							}
						}
						bool flag12 = SelfRole._inst.m_LockRole == null;
						if (flag12)
						{
							SelfRole._inst.m_LockRole = MonsterMgr._inst.FindNearestMonster(SelfRole._inst.m_curModel.transform.position, null, false, PK_TYPE.PK_PEACE, false);
							MonsterRole monsterRole = SelfRole._inst.m_LockRole as MonsterRole;
							try
							{
								bool flag13 = ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Count != 0 && !ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.ContainsValue(monsterRole.monsterid);
								if (flag13)
								{
									SelfRole._inst.m_LockRole = null;
									SelfRole.fsm.ChangeState(StateIdle.Instance);
									return;
								}
							}
							catch (Exception)
							{
								SelfRole._inst.m_LockRole = null;
								SelfRole.fsm.ChangeState(StateIdle.Instance);
								return;
							}
						}
					}
					BaseRole lockRole = SelfRole._inst.m_LockRole;
					try
					{
						Vector3 position = SelfRole._inst.m_LockRole.m_curModel.position;
					}
					catch (Exception)
					{
						SelfRole._inst.m_LockRole = null;
						SelfRole.fsm.RestartState(this);
						return;
					}
					bool flag14 = lockRole == null || lockRole.isDead;
					if (flag14)
					{
						this.StateTimeover += delta_time;
						bool flag15 = this.StateTimeover > 1.5f;
						if (flag15)
						{
							this.StateTimeover = 0f;
							SelfRole.fsm.MoveToOri();
						}
						else
						{
							SelfRole._inst.m_LockRole = null;
							bool flag16 = BaseRoomItem.instance.dDropItem_own.Count != 0;
							if (flag16)
							{
								SelfRole.fsm.ChangeState(StatePick.Instance);
							}
							else
							{
								SelfRole.fsm.RestartState(this);
							}
						}
					}
					else
					{
						this.StateTimeover = 0f;
						int skillCanUse = StateInit.Instance.GetSkillCanUse();
						skill_a3Data skill_a3Data = null;
						ModelBase<Skill_a3Model>.getInstance().skilldic.TryGetValue(skillCanUse, out skill_a3Data);
						float num = (float)skill_a3Data.range / 53.333f;
						bool flag17 = skill_a3Data.skillType != 0 && Vector3.Distance(lockRole.m_curModel.position, SelfRole._inst.m_curModel.position) > StateAttack.MinRange;
						if (flag17)
						{
							bool flag18 = num < Vector3.Distance(SelfRole._inst.m_curModel.position, lockRole.m_curModel.position);
							if (flag18)
							{
								StateAutoMoveToPos.Instance.stopdistance = 2f;
								StateAutoMoveToPos.Instance.pos = SelfRole._inst.m_LockRole.m_curModel.position;
								SelfRole.fsm.ChangeState(StateAutoMoveToPos.Instance);
								return;
							}
						}
						SelfRole._inst.TurnToPos(SelfRole._inst.m_LockRole.m_curModel.position);
						this.exe_action_tm = 0f;
						int num2 = skillCanUse;
						if (num2 <= 3001)
						{
							if (num2 != 2001 && num2 != 3001)
							{
								goto IL_4BE;
							}
						}
						else if (num2 != 4001 && num2 != 5001)
						{
							goto IL_4BE;
						}
						this.xml_action_tm = 0f;
						goto IL_4D2;
						IL_4BE:
						this.xml_action_tm = skill_a3Data.action_tm * 0.1f;
						IL_4D2:
						bool flag19 = skillbar.instance.playSkillById(skillCanUse, false);
						bool flag20 = flag19;
						if (flag20)
						{
							bool flag21 = StateInit.Instance.PreferedSkill == skillCanUse;
							if (flag21)
							{
								StateInit.Instance.PreferedSkill = -1;
							}
						}
						else
						{
							this.xml_action_tm = 0f;
						}
					}
				}
			}
		}

		public override void Exit()
		{
			BaseProxy<BattleProxy>.getInstance().removeEventListener(BattleProxy.EVENT_SELF_KILL_MON, new Action<GameEvent>(this.OnKillMon));
		}

		private void CheckPK()
		{
			ProfessionRole lastAttackPlayer = SelfRole.LastAttackPlayer;
			bool flag = lastAttackPlayer == null;
			if (!flag)
			{
				switch (SelfRole._inst.m_ePK_Type)
				{
				case PK_TYPE.PK_PKALL:
					SelfRole._inst.m_LockRole = lastAttackPlayer;
					SelfRole.LastAttackPlayer = lastAttackPlayer;
					return;
				case PK_TYPE.PK_TEAM:
				{
					bool flag2 = lastAttackPlayer.m_unTeamID != SelfRole._inst.m_unTeamID || !ModelBase<PlayerModel>.getInstance().IsInATeam;
					if (flag2)
					{
						SelfRole._inst.m_LockRole = lastAttackPlayer;
						SelfRole.LastAttackPlayer = lastAttackPlayer;
					}
					else
					{
						SelfRole.UnderPlayerAttack = false;
						SelfRole._inst.m_LockRole = null;
						SelfRole.LastAttackPlayer = null;
					}
					return;
				}
				}
				SelfRole.UnderPlayerAttack = false;
				SelfRole._inst.m_LockRole = null;
				SelfRole.LastAttackPlayer = null;
			}
		}

		private void OnKillMon(GameEvent e)
		{
			this.mon++;
		}

		private bool EnemySelector(ProfessionRole p)
		{
			bool flag = p == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = ModelBase<PlayerModel>.getInstance().pk_state == PK_TYPE.PK_PKALL;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = ModelBase<PlayerModel>.getInstance().pk_state == PK_TYPE.PK_TEAM;
					if (flag3)
					{
						bool flag4 = BaseProxy<TeamProxy>.getInstance().MyTeamData == null || BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList == null;
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Exists((ItemTeamData m) => m.cid == p.m_unCID);
							if (flag5)
							{
								bool flag6 = SelfRole._inst.m_LockRole == p;
								if (flag6)
								{
									SelfRole._inst.m_LockRole = null;
								}
								result = true;
							}
							else
							{
								result = false;
							}
						}
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}
	}
}
