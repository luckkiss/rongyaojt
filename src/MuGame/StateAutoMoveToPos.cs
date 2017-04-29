using System;
using UnityEngine;

namespace MuGame
{
	internal class StateAutoMoveToPos : StateBase
	{
		public static StateAutoMoveToPos Instance = new StateAutoMoveToPos();

		private int priority;

		public bool forceFaceToPos = true;

		public float stopdistance = 0.3f;

		public Vector3 pos = Vector3.zero;

		public Action handle;

		private int waitTick = 0;

		private float thinktime = 0f;

		private float stayTime = 0f;

		private Vector3 lastStay;

		public override void Enter()
		{
			ProfessionRole expr_06 = SelfRole._inst;
			if (expr_06 != null)
			{
				NavMeshAgent expr_11 = expr_06.m_moveAgent;
				if (expr_11 != null)
				{
					expr_11.ResetPath();
				}
			}
			this.priority = SelfRole._inst.m_moveAgent.avoidancePriority;
			bool flag = this.pos == Vector3.zero || Vector3.Distance(SelfRole._inst.m_curModel.position.ConvertToGamePosition(), this.pos.ConvertToGamePosition()) < this.stopdistance;
			if (flag)
			{
				NavMeshAgent expr_82 = SelfRole._inst.m_moveAgent;
				if (expr_82 != null)
				{
					expr_82.Stop();
				}
				Action expr_94 = this.handle;
				if (expr_94 != null)
				{
					expr_94();
				}
				SelfRole.fsm.ChangeState(StateIdle.Instance);
			}
			else
			{
				NavMeshHit navMeshHit;
				NavMesh.SamplePosition(this.pos, out navMeshHit, StateInit.Instance.Distance, NavmeshUtils.allARE);
				this.pos = navMeshHit.position;
				SelfRole._inst.SetDestPos(this.pos);
				SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
				this.waitTick = 3;
			}
		}

		public override void Execute(float delta_time)
		{
			try
			{
				bool flag = SelfRole._inst.m_curAni.GetBool(EnumAni.ANI_RUN) && Vector3.Distance(SelfRole._inst.m_curModel.transform.position.ConvertToGamePosition(), this.lastStay) < 0.2f;
				if (flag)
				{
					this.stayTime += delta_time;
					bool flag2 = this.stayTime > 2f;
					if (flag2)
					{
						this.stayTime = 0f;
						bool autofighting = SelfRole.fsm.Autofighting;
						if (autofighting)
						{
							SelfRole.fsm.RestartState(StateSearch.Instance);
							return;
						}
					}
				}
			}
			catch (Exception var_5_A1)
			{
				SelfRole.fsm.Stop();
			}
			bool flag3 = SelfRole._inst.m_moveAgent != null;
			if (flag3)
			{
				SelfRole._inst.m_moveAgent.avoidancePriority = 1;
			}
			bool flag4 = SelfRole._inst.m_curAni != null;
			if (flag4)
			{
				SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
			}
			bool flag5 = this.waitTick > 0;
			if (flag5)
			{
				this.waitTick--;
			}
			else
			{
				bool flag6 = SelfRole._inst.m_moveAgent == null;
				if (!flag6)
				{
					Vector3 position = SelfRole._inst.m_curModel.position;
					SelfRole._inst.m_moveAgent.SetDestination(this.pos);
					float num = Vector3.Distance(this.pos.ConvertToGamePosition(), position.ConvertToGamePosition());
					bool flag7 = num <= this.stopdistance;
					if (flag7)
					{
						ProfessionRole expr_198 = SelfRole._inst;
						if (expr_198 != null)
						{
							NavMeshAgent expr_1A3 = expr_198.m_moveAgent;
							if (expr_1A3 != null)
							{
								expr_1A3.Stop();
							}
						}
						Action expr_1B5 = this.handle;
						if (expr_1B5 != null)
						{
							expr_1B5();
						}
						bool flag8 = !SelfRole.fsm.Autofighting;
						if (flag8)
						{
							SelfRole.fsm.ChangeState(StateIdle.Instance);
						}
						else
						{
							SelfRole.fsm.ChangeState(StateAttack.Instance);
						}
					}
					else
					{
						this.thinktime += delta_time;
						bool flag9 = this.thinktime > 1f;
						if (flag9)
						{
							this.thinktime = 0f;
							bool flag10 = num < 5f;
							if (flag10)
							{
								bool flag11 = Quaternion.LookRotation(SelfRole._inst.m_curModel.forward - this.pos.normalized).eulerAngles.y > 10f;
								if (flag11)
								{
									SelfRole._inst.TurnToPos(this.pos);
								}
							}
						}
						this.lastStay = SelfRole._inst.m_curModel.transform.position.ConvertToGamePosition();
					}
				}
			}
		}

		public override void Exit()
		{
			bool flag = SelfRole._inst.m_moveAgent != null;
			if (flag)
			{
				SelfRole._inst.m_moveAgent.avoidancePriority = this.priority;
			}
			bool flag2 = SelfRole.fsm.previousState != this;
			if (flag2)
			{
				this.pos = Vector3.zero;
			}
			this.handle = null;
			try
			{
				ProfessionRole expr_59 = SelfRole._inst;
				if (expr_59 != null)
				{
					NavMeshAgent expr_64 = expr_59.m_moveAgent;
					if (expr_64 != null)
					{
						expr_64.Stop();
					}
				}
				ProfessionRole expr_75 = SelfRole._inst;
				if (expr_75 != null)
				{
					Animator expr_80 = expr_75.m_curAni;
					if (expr_80 != null)
					{
						expr_80.SetBool(EnumAni.ANI_RUN, false);
					}
				}
				this.lastStay = SelfRole._inst.m_curModel.transform.position.ConvertToGamePosition();
			}
			catch (Exception)
			{
				this.lastStay = Vector3.zero;
			}
			this.stopdistance = 0.3f;
		}
	}
}
