using MuGame;
using System;
using UnityEngine;

public class RoleStateHelper
{
	private ProfessionRole m_insOperate;

	private float timer = 0f;

	private RoleStateHelper()
	{
	}

	public RoleStateHelper(ProfessionRole insUnderOperate) : this()
	{
		this.m_insOperate = insUnderOperate;
	}

	public void SetNavMeshInfo(int avoidancePriority)
	{
		bool flag = this.m_insOperate.m_moveAgent == null;
		if (!flag)
		{
			this.m_insOperate.m_moveAgent.avoidancePriority = avoidancePriority;
		}
	}

	public void SetNavMeshInfo(float radius, float height)
	{
		bool flag = this.m_insOperate.m_moveAgent == null;
		if (!flag)
		{
			this.m_insOperate.m_moveAgent.radius = radius;
			this.m_insOperate.m_moveAgent.height = height;
		}
	}

	public void SetNavMeshInfo(int avoidancePriority, float radius, float height)
	{
		this.SetNavMeshInfo(avoidancePriority);
		this.SetNavMeshInfo(radius, height);
	}

	public bool CheckMoveAgent(float deltaTime, float maxAllowedStayTime = 0.5f, bool resetImmediately = false)
	{
		bool flag = this.m_insOperate.m_moveAgent.velocity.z != 0f;
		if (flag)
		{
			this.timer += deltaTime;
		}
		else
		{
			this.timer = Mathf.Clamp(this.timer - deltaTime, 0f, this.timer);
		}
		bool flag2 = this.timer > maxAllowedStayTime;
		bool flag3 = resetImmediately & flag2;
		if (flag3)
		{
			try
			{
				this.ResetAutoFight(null);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.Message);
			}
		}
		return !flag2;
	}

	public void ResetAutoFight(StateMachine stateMachine = null)
	{
		this.timer = 0f;
		bool flag = stateMachine == null;
		if (flag)
		{
			bool flag2 = SelfRole.fsm != null;
			if (!flag2)
			{
				throw new Exception("StateMachineNotFoundException");
			}
			SelfRole.fsm.Stop();
			SelfRole.fsm.StartAutofight();
		}
		else
		{
			stateMachine.Stop();
			stateMachine.StartAutofight();
		}
	}
}
