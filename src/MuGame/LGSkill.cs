using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGSkill : lgGDBase
	{
		private Variant _skillList;

		private int _skillListInfoFlag = 1;

		private Variant m_manualSkills = null;

		private InGameSkillMsgs msgSkil
		{
			get
			{
				return this.g_mgr.g_netM.getObject("MSG_SKILL") as InGameSkillMsgs;
			}
		}

		private LGMap lgmap
		{
			get
			{
				return this.g_mgr.g_gameM.getObject("LG_MAP") as LGMap;
			}
		}

		public LGSkill(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGSkill(m as gameManager);
		}

		public override void init()
		{
		}

		public Variant get_skill_list()
		{
			return null;
		}

		public void set_skill_list(GameEvent e)
		{
			Variant data = e.data;
			Variant variant = data["skills"];
			bool flag = this._skillList == null;
			if (flag)
			{
				this._skillList = new Variant();
			}
			for (int i = 0; i < variant._arr.Count; i++)
			{
				Variant variant2 = variant[i];
				this._skillList[variant2["skid"]._int.ToString()] = variant2;
			}
			this._skillListInfoFlag = 0;
			base.dispatchEvent(GameEvent.Create(2010u, this, variant, false));
		}

		public void learn_skill(GameEvent e)
		{
			Variant data = e.data;
			this._skillList[data["skid"]._str] = data;
			base.dispatchEvent(GameEvent.Create(2011u, this, data, false));
		}

		public void decCost(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("gld_cost");
			if (flag)
			{
			}
			bool flag2 = data.ContainsKey("gld_cost");
			if (flag2)
			{
			}
			bool flag3 = data.ContainsKey("gld_cost");
			if (flag3)
			{
			}
		}

		public void setUpSkill(GameEvent e)
		{
			Variant data = e.data;
			this._skillList[data["skid"]]["sklvl"] = data["sklvl"];
		}

		public Variant GetSkillData(uint skid)
		{
			string key = skid.ToString();
			bool flag = !this._skillList.ContainsKey(key);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._skillList[key];
			}
			return result;
		}

		public bool IsManualSkill(uint sid)
		{
			bool flag = this.m_manualSkills == null;
			if (flag)
			{
			}
			bool result;
			for (int i = 0; i < this.m_manualSkills._arr.Count; i++)
			{
				bool flag2 = this.m_manualSkills[i]._uint == sid;
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public void SetSkillCD(Variant data, Dictionary<uint, Variant> cds)
		{
			bool flag = cds == null || cds.Count <= 0;
			if (!flag)
			{
				float num = (float)this.g_mgr.g_netM.CurServerTimeStampMS;
				float @float = num + data["cdtm"]._float * 100f;
				foreach (Variant current in this._skillList.Values)
				{
					bool flag2 = cds.ContainsKey(current["skid"]._uint);
					if (flag2)
					{
						current["cd"]._float = (float)(cds[current["skid"]._uint]["start_tm"] + cds[current["skid"]._uint]["cd_tm"] * 100);
					}
					else
					{
						current["cd"]._float = @float;
					}
				}
			}
		}

		public Variant GetSkillById(uint skid)
		{
			return this._skillList[skid.ToString()];
		}

		public void castSelfSkill(uint skid)
		{
		}

		public void castTargetSkill(uint skid, uint to_iid)
		{
		}

		public void castGroundSkill(uint skid, float x, float y)
		{
		}

		public void castFanSkill(uint skid, uint to_iid, int angle)
		{
		}
	}
}
