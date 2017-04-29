using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class StateMoveLine : StateBase
	{
		public static StateMoveLine Instance = new StateMoveLine();

		public List<MapLinkInfo> line;

		public Vector3 curtargetPos;

		public Vector3 pos = Vector3.zero;

		public Action handle;

		private int waitTick = 0;

		private float thinktime = 0f;

		public override void Enter()
		{
			bool flag = this.line == null;
			if (flag)
			{
				SelfRole.fsm.ChangeState(StateIdle.Instance);
			}
			else
			{
				ProfessionRole expr_26 = SelfRole._inst;
				bool flag2 = ((expr_26 != null) ? expr_26.m_moveAgent : null) == null;
				if (!flag2)
				{
					SelfRole._inst.m_moveAgent.ResetPath();
					this.refredshPos();
				}
			}
		}

		private void refredshPos()
		{
			bool flag = this.line.Count == 0;
			if (flag)
			{
				bool flag2 = this.pos != Vector3.zero;
				if (flag2)
				{
					this.curtargetPos = this.pos;
				}
				else
				{
					SelfRole._inst.m_moveAgent.ResetPath();
					SelfRole._inst.m_moveAgent.Stop();
					bool flag3 = this.handle != null;
					if (flag3)
					{
						this.handle();
					}
					SelfRole.fsm.ChangeState(StateIdle.Instance);
				}
			}
			else
			{
				bool flag4 = GRMap.instance.m_nCurMapID != this.line[0].mapid;
				if (flag4)
				{
					this.line.RemoveAt(0);
					this.curtargetPos = Vector3.zero;
					bool flag5 = this.line.Count == 0;
					if (flag5)
					{
						this.curtargetPos = this.pos;
						bool flag6 = this.curtargetPos == Vector3.zero;
						if (flag6)
						{
							return;
						}
						SelfRole._inst.SetDestPos(this.curtargetPos);
						SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
						this.waitTick = 3;
					}
				}
				bool flag7 = this.curtargetPos == Vector3.zero;
				if (flag7)
				{
					this.curtargetPos = new Vector3(this.line[0].x, 0f, this.line[0].y);
					SelfRole._inst.SetDestPos(this.curtargetPos);
					SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
					this.waitTick = 5;
				}
				else
				{
					SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
				}
			}
		}

		public override void Execute(float delta_time)
		{
			bool flag = this.waitTick > 0;
			if (flag)
			{
				this.waitTick--;
			}
			else
			{
				bool changingMap = BaseProxy<MapProxy>.getInstance().changingMap;
				if (!changingMap)
				{
					ProfessionRole expr_3A = SelfRole._inst;
					bool flag2 = ((expr_3A != null) ? expr_3A.m_moveAgent : null) == null;
					if (!flag2)
					{
						this.refredshPos();
						bool flag3 = this.waitTick > 0;
						if (flag3)
						{
							this.waitTick--;
						}
						else
						{
							Animator expr_87 = SelfRole._inst.m_curAni;
							if (expr_87 != null)
							{
								expr_87.SetBool(EnumAni.ANI_RUN, true);
							}
							bool flag4 = this.line.Count == 0;
							if (flag4)
							{
								Vector3 destination = SelfRole._inst.m_moveAgent.destination;
								float remainingDistance = SelfRole._inst.m_moveAgent.remainingDistance;
								bool flag5 = remainingDistance <= 0.3f;
								if (flag5)
								{
									ProfessionRole expr_EB = SelfRole._inst;
									if (expr_EB != null)
									{
										NavMeshAgent expr_F6 = expr_EB.m_moveAgent;
										if (expr_F6 != null)
										{
											expr_F6.ResetPath();
										}
									}
									ProfessionRole expr_107 = SelfRole._inst;
									if (expr_107 != null)
									{
										NavMeshAgent expr_112 = expr_107.m_moveAgent;
										if (expr_112 != null)
										{
											expr_112.Stop();
										}
									}
									bool flag6 = this.handle != null;
									if (flag6)
									{
										this.handle();
									}
									SelfRole.fsm.ChangeState(StateIdle.Instance);
								}
								else
								{
									this.thinktime += delta_time;
									bool flag7 = this.thinktime > 1f;
									if (flag7)
									{
										this.thinktime = 0f;
										bool flag8 = remainingDistance < 5f;
										if (flag8)
										{
											bool flag9 = Quaternion.LookRotation(SelfRole._inst.m_curModel.forward - this.pos.normalized).eulerAngles.y > 10f;
											if (flag9)
											{
												ProfessionRole expr_1D0 = SelfRole._inst;
												if (expr_1D0 != null)
												{
													expr_1D0.TurnToPos(this.pos);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		public override void Exit()
		{
			bool flag = SelfRole.fsm.previousState != this;
			if (flag)
			{
				this.handle = null;
				this.line = null;
				this.pos = Vector3.zero;
			}
			this.curtargetPos = Vector3.zero;
			ProfessionRole expr_40 = SelfRole._inst;
			bool flag2 = ((expr_40 != null) ? expr_40.m_moveAgent : null) == null;
			if (!flag2)
			{
				SelfRole._inst.m_moveAgent.ResetPath();
				SelfRole._inst.m_moveAgent.Stop();
				SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, false);
			}
		}
	}
}
