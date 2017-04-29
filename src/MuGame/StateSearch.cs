using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class StateSearch : StateBase
	{
		public static StateSearch Instance = new StateSearch();

		private float thinktm = 0f;

		public bool onTaskMonsterSearch
		{
			get
			{
				return a3_task_auto.instance.onTaskSearchMon;
			}
			set
			{
				a3_task_auto.instance.onTaskSearchMon = value;
			}
		}

		private bool isOutOfAutoPlayRange
		{
			get
			{
				return StateInit.Instance.IsOutOfAutoPlayRange();
			}
		}

		public override void Enter()
		{
			this.thinktm = 1000f;
		}

		public override void Execute(float delta_time)
		{
			this.thinktm += delta_time;
			bool flag = this.thinktm < 0.25f;
			if (!flag)
			{
				this.thinktm = 0f;
				bool isOutOfAutoPlayRange = this.isOutOfAutoPlayRange;
				if (isOutOfAutoPlayRange)
				{
					StateAutoMoveToPos.Instance.pos = StateInit.Instance.Origin;
					StateAutoMoveToPos.Instance.stopdistance = 0.3f;
					SelfRole.fsm.ChangeState(StateAutoMoveToPos.Instance);
					SelfRole._inst.m_LockRole = null;
				}
				else
				{
					Vector3 position = SelfRole._inst.m_curModel.position;
					MonsterRole monsterRole = MonsterMgr._inst.FindNearestMonster(position, null, false, PK_TYPE.PK_PEACE, this.onTaskMonsterSearch);
					bool flag2 = monsterRole == null;
					if (flag2)
					{
						Vector3 vector;
						bool flag3 = this.FindRandomPropAutoPoint(position, out vector);
						bool flag4 = flag3 && Vector2.Distance(new Vector2(StateInit.Instance.Origin.x, StateInit.Instance.Origin.z), new Vector2(vector.x, vector.z)) < StateInit.Instance.Distance;
						if (flag4)
						{
							bool flag5 = Vector3.Distance(vector, position) <= 2f;
							if (!flag5)
							{
								StateAutoMoveToPos.Instance.pos = vector;
								StateAutoMoveToPos.Instance.stopdistance = 2f;
								SelfRole.fsm.ChangeState(StateAutoMoveToPos.Instance);
							}
						}
						else
						{
							SelfRole.fsm.ChangeState(StatePick.Instance);
						}
					}
					else
					{
						SelfRole._inst.m_LockRole = monsterRole;
						SelfRole.fsm.ChangeState(StateAttack.Instance);
					}
				}
			}
		}

		public override void Exit()
		{
		}

		private bool FindRandomPropAutoPoint(Vector3 curpos, out Vector3 autopoint)
		{
			List<Vector3> autoPoints = StateInit.Instance.AutoPoints;
			bool flag = autoPoints.Count == 0;
			bool result;
			if (flag)
			{
				autopoint = Vector3.zero;
				result = false;
			}
			else
			{
				System.Random random = new System.Random();
				int index = random.Next(autoPoints.Count);
				autopoint = new Vector3(autoPoints[index].x, autoPoints[index].y, autoPoints[index].z);
				result = true;
			}
			return result;
		}
	}
}
