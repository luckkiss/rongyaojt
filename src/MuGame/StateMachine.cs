using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class StateMachine
	{
		public StateBase currentState;

		public StateBase previousState;

		public StateBase globalState;

		public StateBase initState;

		public StateBase proxyState;

		public Action<bool> OnFSMStartStop = null;

		public Vector3 manBeginPos;

		private bool autofighting = false;

		private bool autoCollect = false;

		public float DistanceNowAndOri
		{
			get
			{
				return (StateInit.Instance.Origin != Vector3.zero) ? Vector3.Distance(new Vector3(StateInit.Instance.Origin.x, 0f, StateInit.Instance.Origin.z), new Vector3(SelfRole._inst.m_curModel.position.x, 0f, SelfRole._inst.m_curModel.position.z)) : 0f;
			}
		}

		public bool Autofighting
		{
			get
			{
				return this.autofighting;
			}
			private set
			{
				bool flag = this.autofighting ^ value;
				if (flag)
				{
					this.autofighting = value;
					Action<bool> expr_1B = this.OnFSMStartStop;
					if (expr_1B != null)
					{
						expr_1B(value);
					}
					StateInit.Instance.Origin = SelfRole._inst.m_curModel.position;
					bool flag2 = !value;
					if (flag2)
					{
						MonsterMgr._inst.taskMonId = null;
					}
				}
			}
		}

		public bool AutoCollect
		{
			get
			{
				return this.autoCollect;
			}
			set
			{
				this.autoCollect = value;
			}
		}

		public bool IsPause
		{
			get;
			set;
		}

		public bool CheckJoystickMoveDis(Vector3 ori, float maxDis)
		{
			return Vector3.Distance(new Vector3(this.manBeginPos.x, 0f, this.manBeginPos.z), new Vector3(ori.x, 0f, ori.z)) > maxDis;
		}

		public StateMachine()
		{
			this.IsPause = false;
			this.currentState = null;
			this.previousState = null;
			this.globalState = null;
			this.proxyState = null;
			skill_a3Data skill_a3Data = null;
			int key;
			switch (ModelBase<PlayerModel>.getInstance().profession)
			{
			case 2:
			case 4:
				IL_6A:
				key = 2001;
				goto IL_82;
			case 3:
				key = 3001;
				goto IL_82;
			case 5:
				key = 5001;
				goto IL_82;
			}
			goto IL_6A;
			IL_82:
			ModelBase<Skill_a3Model>.getInstance().skilldic.TryGetValue(key, out skill_a3Data);
			StateAttack.MinRange = (float)skill_a3Data.range / 53.333f;
		}

		public void Configure(StateBase initState, StateBase globalState, StateBase proxyState)
		{
			this.initState = initState;
			this.globalState = globalState;
			this.proxyState = proxyState;
		}

		public void Update(float delta_time)
		{
			bool flag = this.proxyState != null;
			if (flag)
			{
				this.proxyState.Execute(delta_time);
			}
			bool flag2 = this.globalState != null;
			if (flag2)
			{
				this.globalState.Execute(delta_time);
			}
			bool flag3 = this.currentState != null;
			if (flag3)
			{
				this.currentState.Execute(delta_time);
			}
			bool flag4 = this.autofighting && this.DistanceNowAndOri > StateInit.Instance.Distance;
			if (flag4)
			{
				this.MoveToOri();
			}
		}

		public void MoveToOri()
		{
			StateAutoMoveToPos.Instance.pos = StateInit.Instance.Origin;
			StateAutoMoveToPos.Instance.stopdistance = 0.3f;
			SelfRole.fsm.ChangeState(StateAutoMoveToPos.Instance);
			SelfRole._inst.m_LockRole = null;
		}

		public void RestartState(StateBase currentState)
		{
			StateInit.Instance.LockOriPos = true;
			if (currentState != null)
			{
				currentState.Exit();
			}
			if (currentState != null)
			{
				currentState.Enter();
			}
			StateInit.Instance.LockOriPos = false;
		}

		public void ChangeState(StateBase newState)
		{
			this.previousState = this.currentState;
			bool flag = this.currentState != null;
			if (flag)
			{
				this.currentState.Exit();
			}
			this.currentState = newState;
			bool flag2 = this.currentState != null;
			if (flag2)
			{
				this.currentState.Enter();
			}
		}

		public void RevertToPreviousState()
		{
			bool flag = this.previousState != null;
			if (flag)
			{
				this.ChangeState(this.previousState);
			}
		}

		public void StartAutofight()
		{
			this.Autofighting = true;
			this.IsPause = false;
			this.ChangeState(this.initState);
		}

		public void StartAutoCollect()
		{
			this.AutoCollect = true;
			this.IsPause = false;
		}

		public void Stop()
		{
			SelfRole._inst.m_LockRole = null;
			this.Autofighting = false;
			this.AutoCollect = false;
			cd.hide();
			this.ChangeState(StateIdle.Instance);
			this.ClearAutoConfig();
		}

		public void ClearAutoConfig()
		{
			SelfRole.UnderTaskAutoMove = false;
			foreach (KeyValuePair<int, int> current in ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack)
			{
				bool flag = !ModelBase<PlayerModel>.getInstance().task_monsterId.ContainsKey(current.Key);
				if (flag)
				{
					ModelBase<PlayerModel>.getInstance().task_monsterId.Add(current.Key, current.Value);
				}
			}
			ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Clear();
			StateSearch.Instance.onTaskMonsterSearch = false;
			bool onTaskSearchMon = a3_task_auto.instance.onTaskSearchMon;
			if (onTaskSearchMon)
			{
				StateSearch.Instance.onTaskMonsterSearch = false;
				a3_task_auto.instance.PauseAutoKill(-1);
			}
		}

		public void Pause()
		{
			this.IsPause = true;
			this.ChangeState(StateIdle.Instance);
		}

		public void Resume()
		{
			this.IsPause = false;
			this.ChangeState(StateIdle.Instance);
		}
	}
}
