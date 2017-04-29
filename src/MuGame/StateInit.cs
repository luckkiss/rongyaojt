using MuGame.Qsmy.model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MuGame
{
	internal class StateInit : StateBase
	{
		public static StateInit Instance = new StateInit();

		private Vector3 _origin;

		public float Distance = 3.40282347E+38f;

		public float DistanceNormal = 3.40282347E+38f;

		public List<Vector3> AutoPoints;

		public int RespawnTimes = 10;

		public float PKDistance;

		public float PickDistance;

		public float PickDistanceNormal;

		public int PreferedSkill;

		private long releaseTime;

		private bool lockOriPos = false;

		public Vector3 Origin
		{
			get
			{
				return this._origin;
			}
			set
			{
				bool flag = !this.LockOriPos;
				if (flag)
				{
					this._origin = value;
				}
			}
		}

		public bool LockOriPos
		{
			get
			{
				return this.lockOriPos;
			}
			set
			{
				bool flag = SelfRole.fsm.Autofighting || !value;
				if (flag)
				{
					this.lockOriPos = value;
				}
			}
		}

		public StateInit()
		{
			AutoPlayModel instance = ModelBase<AutoPlayModel>.getInstance();
			this.DistanceNormal = instance.AutoplayXml.GetNode("mdis", "").getFloat("val");
			SXML autoplayXml = ModelBase<AutoPlayModel>.getInstance().AutoplayXml;
			this.PickDistanceNormal = autoplayXml.GetNode("pickdis", "").getFloat("val");
		}

		public override void Enter()
		{
			this.Origin = SelfRole._inst.m_curModel.position;
			this.AutoPoints = new List<Vector3>();
			this.GetProperWayPoints();
			bool flag = ModelBase<AutoPlayModel>.getInstance().RespawnLimit > 0;
			if (flag)
			{
				this.RespawnTimes = ModelBase<AutoPlayModel>.getInstance().RespawnUpBound;
			}
			else
			{
				this.RespawnTimes = 2147483647;
			}
			SXML autoplayXml = ModelBase<AutoPlayModel>.getInstance().AutoplayXml;
			this.PKDistance = autoplayXml.GetNode("pkdis", "").getFloat("val");
			this.PreferedSkill = -1;
		}

		public override void Execute(float delta_time)
		{
			SelfRole.fsm.ChangeState(StateIdle.Instance);
		}

		public override void Exit()
		{
		}

		private void GetProperWayPoints()
		{
			AutoPlayModel instance = ModelBase<AutoPlayModel>.getInstance();
			Dictionary<int, List<Vector3>> mapWayPoint = instance.mapWayPoint;
			List<Vector3> list = null;
			mapWayPoint.TryGetValue(GRMap.instance.m_nCurMapID, out list);
			bool flag = list == null || list.Count == 0;
			if (!flag)
			{
				for (int i = 0; i < list.Count; i++)
				{
					float num = Vector3.Distance(this.Origin, list[i]);
					bool flag2 = num <= this.Distance;
					if (flag2)
					{
						this.AutoPoints.Add(new Vector3(list[i].x, list[i].y, list[i].z));
					}
				}
			}
		}

		public Vector3 GetNearestWayPoint()
		{
			float num = 3.40282347E+38f;
			Vector3 position = SelfRole._inst.m_curModel.position;
			Vector3 vector = position;
			foreach (Vector3 current in this.AutoPoints)
			{
				float num2 = Vector3.Distance(current, vector);
				bool flag = num2 < num;
				if (flag)
				{
					num = num2;
					vector = current;
				}
			}
			return vector;
		}

		public bool IsOutOfAutoPlayRange()
		{
			float num = Vector3.Distance(this.Origin, SelfRole._inst.m_curModel.position);
			return num > this.Distance;
		}

		public int GetSkillCanUse()
		{
			bool flag = this.PreferedSkill != -1;
			int result;
			if (flag)
			{
				long curServerTimeStampMS = muNetCleint.instance.CurServerTimeStampMS;
				bool flag2 = curServerTimeStampMS - this.releaseTime >= 2000L;
				if (flag2)
				{
					this.PreferedSkill = -1;
				}
				else
				{
					skill_a3Data skill_a3Data = ModelBase<Skill_a3Model>.getInstance().skilldic[this.PreferedSkill];
					bool flag3 = skill_a3Data.mp <= ModelBase<PlayerModel>.getInstance().mp && skill_a3Data.cdTime <= 0;
					if (flag3)
					{
						result = this.PreferedSkill;
						return result;
					}
				}
			}
			List<int> list = ModelBase<AutoPlayModel>.getInstance().Skills.ToList<int>();
			foreach (skill_a3Data current in ModelBase<Skill_a3Model>.getInstance().skilldic.Values)
			{
				bool flag4 = current.carr == ModelBase<PlayerModel>.getInstance().profession && current.skill_id != skillbar.NORNAL_SKILL_ID && current.now_lv != 0 && current.skillType2 == 1 && current.mp <= ModelBase<PlayerModel>.getInstance().mp && current.cdTime <= 0;
				if (flag4)
				{
					result = current.skill_id;
					return result;
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = i; j < list.Count; j++)
				{
					bool flag5 = ModelBase<Skill_a3Model>.getInstance().skilldic.ContainsKey(list[i]) && ModelBase<Skill_a3Model>.getInstance().skilldic.ContainsKey(list[j]);
					if (flag5)
					{
						bool flag6 = ModelBase<Skill_a3Model>.getInstance().skilldic[list[i]].cd > ModelBase<Skill_a3Model>.getInstance().skilldic[list[j]].cd;
						if (flag6)
						{
							int value = list[i];
							list[i] = list[j];
							list[j] = value;
						}
					}
				}
			}
			for (int k = list.Count - 1; k >= 0; k--)
			{
				bool flag7 = list[k] == 0;
				if (!flag7)
				{
					int num = list[k];
					skill_a3Data skill_a3Data2 = ModelBase<Skill_a3Model>.getInstance().skilldic[num];
					bool flag8 = skill_a3Data2.mp > ModelBase<PlayerModel>.getInstance().mp || skill_a3Data2.cdTime > 0;
					if (!flag8)
					{
						result = num;
						return result;
					}
				}
			}
			result = skillbar.NORNAL_SKILL_ID;
			return result;
		}

		public void PlaySkillInAutoPlay(int tpid)
		{
			this.PreferedSkill = tpid;
			this.releaseTime = muNetCleint.instance.CurServerTimeStampMS;
		}
	}
}
