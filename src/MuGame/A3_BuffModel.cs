using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class A3_BuffModel : ModelBase<A3_BuffModel>
	{
		public Dictionary<uint, BuffInfo> BuffCd = new Dictionary<uint, BuffInfo>();

		public List<BUFF_TYPE> Buff_type_list = new List<BUFF_TYPE>();

		public void addBuffList(Variant data)
		{
			BuffInfo buffInfo = new BuffInfo();
			buffInfo.id = data["id"];
			buffInfo.par = data["par"];
			buffInfo.start_time = data["start_tm"];
			buffInfo.end_time = data["end_tm"];
			XMLMgr expr_64 = XMLMgr.instance;
			SXML sXML = (expr_64 != null) ? expr_64.GetSXML("skill.state", "id==" + buffInfo.id) : null;
			SXML sXML2 = (sXML != null) ? sXML.GetNode("s", null) : null;
			bool flag = sXML2 != null;
			if (flag)
			{
				int @int = sXML2.getInt("tp");
				if (@int != 1)
				{
					if (@int != 6)
					{
						if (@int != 7)
						{
							buffInfo.buff_type = BUFF_TYPE.NULL;
						}
						else
						{
							SelfRole._inst.can_buff_move = false;
							SelfRole._inst.can_buff_skill = false;
							SelfRole._inst.can_buff_ani = false;
							SelfRole._inst.m_curAni.enabled = false;
							buffInfo.buff_type = BUFF_TYPE.CANT_MOVE_SKILL;
						}
					}
					else
					{
						SelfRole._inst.can_buff_skill = false;
						buffInfo.buff_type = BUFF_TYPE.CANT_SKILL;
					}
				}
				else
				{
					buffInfo.buff_type = BUFF_TYPE.CANT_MOVE;
					SelfRole._inst.can_buff_move = false;
				}
				bool flag2 = buffInfo.buff_type != BUFF_TYPE.NULL && !this.Buff_type_list.Contains(buffInfo.buff_type);
				if (flag2)
				{
					this.Buff_type_list.Add(buffInfo.buff_type);
				}
			}
			buffInfo.icon = ((sXML != null) ? sXML.getString("icon") : null);
			buffInfo.name = ((sXML != null) ? sXML.getString("name") : null);
			this.dele_buff(data);
			this.BuffCd[buffInfo.id] = buffInfo;
			buffInfo.doCD();
			a3_buff expr_1C7 = a3_buff.instance;
			if (expr_1C7 != null)
			{
				expr_1C7.resh_buff();
			}
		}

		public void dele_buff(Variant data)
		{
			for (uint num = 9998u; num > 9995u; num -= 1u)
			{
				bool flag = data["id"] > num;
				if (flag)
				{
					bool flag2 = this.BuffCd.ContainsKey(num);
					if (flag2)
					{
						this.BuffCd.Remove(num);
					}
				}
			}
		}

		public void RemoveBuff(uint id)
		{
			bool flag = !this.BuffCd.ContainsKey(id);
			if (!flag)
			{
				BUFF_TYPE buff_type = this.BuffCd[id].buff_type;
				BUFF_TYPE bUFF_TYPE = buff_type;
				if (bUFF_TYPE != BUFF_TYPE.CANT_MOVE)
				{
					if (bUFF_TYPE != BUFF_TYPE.CANT_SKILL)
					{
						if (bUFF_TYPE == BUFF_TYPE.CANT_MOVE_SKILL)
						{
							SelfRole._inst.can_buff_move = true;
							SelfRole._inst.can_buff_skill = true;
							SelfRole._inst.can_buff_ani = true;
							SelfRole._inst.m_curAni.enabled = true;
						}
					}
					else
					{
						SelfRole._inst.can_buff_skill = true;
					}
				}
				else
				{
					SelfRole._inst.can_buff_move = true;
				}
				bool flag2 = SelfRole._inst is P3Mage;
				if (flag2)
				{
					XMLMgr expr_A4 = XMLMgr.instance;
					SXML sXML = (expr_A4 != null) ? expr_A4.GetSXML("skill.state", "id==" + id) : null;
					bool flag3 = sXML.getInt("skill_id") == 3008;
					if (flag3)
					{
						SelfRole._inst.PlaySkill(30081);
					}
				}
				bool flag4 = buff_type != BUFF_TYPE.NULL && this.Buff_type_list.Contains(buff_type);
				if (flag4)
				{
					this.Buff_type_list.Remove(this.BuffCd[id].buff_type);
				}
				this.BuffCd.Remove(id);
				a3_buff expr_138 = a3_buff.instance;
				if (expr_138 != null)
				{
					expr_138.resh_buff();
				}
			}
		}

		public void addOtherBuff(BaseRole role, uint id)
		{
			SXML sXML = XMLMgr.instance.GetSXML("skill.state", "id==" + id);
			bool flag = sXML != null;
			if (flag)
			{
				SXML node = sXML.GetNode("s", null);
				bool flag2 = node != null;
				if (flag2)
				{
					int @int = node.getInt("tp");
					if (@int == 7)
					{
						role.m_curAni.enabled = false;
					}
				}
			}
		}

		public void removeOtherBuff(BaseRole role, uint id)
		{
			SXML sXML = XMLMgr.instance.GetSXML("skill.state", "id==" + id);
			bool flag = sXML != null;
			if (flag)
			{
				SXML node = sXML.GetNode("s", null);
				bool flag2 = node != null;
				if (flag2)
				{
					int @int = node.getInt("tp");
					if (@int == 7)
					{
						role.m_curAni.enabled = true;
					}
				}
			}
			bool flag3 = role is P3Mage || role is ohterP3Mage;
			if (flag3)
			{
				bool flag4 = sXML.getInt("skill_id") == 3008;
				if (flag4)
				{
					SelfRole._inst.PlaySkill(30081);
				}
			}
		}
	}
}
