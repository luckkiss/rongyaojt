using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class Skill_a3Model : ModelBase<Skill_a3Model>
	{
		public int[] idsgroupone;

		public int[] idsgrouptwo;

		public List<int> skillid_have = new List<int>();

		public Variant skills;

		public List<int> all_skills = new List<int>();

		public Dictionary<int, skill_a3Data> skilldic = new Dictionary<int, skill_a3Data>();

		public List<skill_a3Data> skilllst = new List<skill_a3Data>();

		public Dictionary<int, runeData> runedic = new Dictionary<int, runeData>();

		public Dictionary<int, List<RuneRequire>> runeReqDic = new Dictionary<int, List<RuneRequire>>();

		private SXML skillXML;

		public int openuplvl = 0;

		public int openlvl = 0;

		public Skill_a3Model()
		{
			this.idsgroupone = new int[4];
			this.idsgrouptwo = new int[4];
			this.skillXML = XMLMgr.instance.GetSXML("skill.skill", "");
			this.ReadXml();
			this.Readxml_rune();
		}

		public void initSkillList(List<Variant> arr)
		{
			this.skillid_have.Clear();
			foreach (Variant current in arr)
			{
				int item = current["skill_id"];
				this.skillid_have.Add(item);
			}
		}

		private void ReadXml()
		{
			skill_a3Data.itemId = XMLMgr.instance.GetSXML("skill.skill_learn_item_id", "").getUint("item_id");
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("skill.skill", "");
			bool flag = sXMLList != null;
			if (flag)
			{
				foreach (SXML current in sXMLList)
				{
					skill_a3Data skill_a3Data = new skill_a3Data();
					skill_a3Data.skill_id = current.getInt("id");
					skill_a3Data.carr = current.getInt("carr");
					skill_a3Data.skill_name = current.getString("name");
					skill_a3Data.action_tm = current.getFloat("action_tm");
					skill_a3Data.des = current.getString("descr1");
					skill_a3Data.open_zhuan = current.getInt("open_zhuan");
					skill_a3Data.open_lvl = current.getInt("open_lvl");
					skill_a3Data.xml = current;
					skill_a3Data.item_num = current.getInt("item_num");
					skill_a3Data.targetNum = current.getInt("target_num");
					skill_a3Data.range = current.getInt("range");
					skill_a3Data.skillType = current.getInt("skill_type");
					skill_a3Data.skillType2 = current.getInt("skill_type2");
					skill_a3Data.max_lvl = current.GetNodeList("skill_att", "").Count;
					skill_a3Data.eff_last = current.getFloat("eff_last");
					this.skilldic[skill_a3Data.skill_id] = skill_a3Data;
					bool flag2 = skill_a3Data.skill_id != 1;
					if (flag2)
					{
						this.skilllst.Add(skill_a3Data);
					}
				}
			}
		}

		public void skillGroups(List<Variant> skillgroup)
		{
			for (int i = 0; i < 4; i++)
			{
				this.idsgroupone[i] = skillgroup[i];
				this.idsgrouptwo[i] = skillgroup[i + 4];
			}
		}

		public void skillinfos(int skill_id, int skill_lv)
		{
			this.skilldic[skill_id].now_lv = skill_lv;
			bool flag = !this.all_skills.Contains(skill_id);
			if (flag)
			{
				this.all_skills.Add(skill_id);
				skill_a3.skills.Add(skill_id);
			}
		}

		private void Readxml_rune()
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("rune.rune", "");
			SXML sXML = XMLMgr.instance.GetSXML("rune.open", "");
			this.openuplvl = sXML.getInt("zhuan");
			this.openlvl = sXML.getInt("level");
			foreach (SXML current in sXMLList)
			{
				runeData runeData = new runeData();
				runeData.id = current.getInt("id");
				runeData.type = current.getInt("type");
				runeData.name = current.getString("name");
				runeData.desc = current.getString("desc");
				runeData.carr = current.getInt("carr");
				runeData.open_zhuan = current.getInt("open_zhuan");
				runeData.open_lv = current.getInt("open_lv");
				this.runedic[runeData.id] = runeData;
			}
			this.ReadRuneRequire(sXMLList);
		}

		private void ReadRuneRequire(List<SXML> listSxml = null)
		{
			bool flag = listSxml == null;
			if (flag)
			{
				listSxml = XMLMgr.instance.GetSXMLList("rune.rune", "");
			}
			for (int i = 0; i < listSxml.Count; i++)
			{
				List<SXML> nodeList = listSxml[i].GetNodeList("level", "");
				int @int = listSxml[i].getInt("id");
				bool flag2 = ModelBase<PlayerModel>.getInstance().profession != listSxml[i].getInt("carr") && listSxml[i].getInt("carr") != 1;
				if (!flag2)
				{
					bool flag3 = !this.runeReqDic.ContainsKey(@int);
					if (flag3)
					{
						this.runeReqDic.Add(@int, new List<RuneRequire>());
						for (int j = 0; j < nodeList.Count; j++)
						{
							List<RuneRequire> arg_155_0 = this.runeReqDic[@int];
							RuneRequire item = default(RuneRequire);
							SXML expr_FE = nodeList[j];
							item.req_role_zhuan = ((expr_FE != null) ? expr_FE.getInt("role_zhuan") : 0);
							SXML expr_11E = nodeList[j];
							item.req_role_lvl = ((expr_11E != null) ? expr_11E.getInt("role_lvl") : 0);
							SXML expr_13D = nodeList[i];
							item.req_cost = ((expr_13D != null) ? expr_13D.getInt("money_cost") : 0);
							arg_155_0.Add(item);
						}
					}
					else
					{
						Debug.LogError(string.Format("符文Id配置重复,重复id为:{0}", @int));
					}
				}
			}
		}

		public Dictionary<int, runeData> Reshreinfos(int id, int lv = -1, int time = -1)
		{
			foreach (int current in this.runedic.Keys)
			{
				bool flag = this.runedic[current].id == id;
				if (flag)
				{
					bool flag2 = lv != -1;
					if (flag2)
					{
						this.runedic[current].now_lv = lv;
					}
					bool flag3 = time != -1;
					if (flag3)
					{
						this.runedic[current].time = time;
					}
					break;
				}
			}
			return this.runedic;
		}

		public int GetCheckCount(int defaultValue = 10)
		{
			List<SXML> nodeList = XMLMgr.instance.GetSXML("skill.skill_check", "").GetNodeList("skill_check", "");
			int result;
			for (int i = 0; i < nodeList.Count; i++)
			{
				bool flag = (long)nodeList[i].getInt("check_zhuan") > (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) || ((long)nodeList[i].getInt("check_zhuan") == (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) && (long)nodeList[i].getInt("check_lv") >= (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl));
				if (flag)
				{
					result = nodeList[i].getInt("check_count");
					return result;
				}
			}
			result = defaultValue;
			return result;
		}

		public bool CheckSkillLevelupAvailable()
		{
			int num = 0;
			List<int> list = new List<int>(this.skilldic.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = this.skilldic[list[i]].now_lv <= 0;
				if (!flag)
				{
					bool flag2 = this.skilldic[list[i]].now_lv < this.skilldic[list[i]].max_lvl;
					if (flag2)
					{
						num = Mathf.Max(num, this.skilldic[list[i]].max_lvl - this.skilldic[list[i]].now_lv);
					}
				}
			}
			int num2 = Mathf.Min(this.GetCheckCount(10), num);
			bool result;
			for (int j = 0; j < list.Count; j++)
			{
				int num3 = 0;
				int num4 = 0;
				bool flag3 = false;
				int k;
				for (k = 0; k < num2; k++)
				{
					skill_a3Data expr_10B = this.skilldic[list[j]];
					bool flag4 = ((expr_10B != null) ? expr_10B.xml : null) == null || this.skilldic[list[j]].xml.m_dAtttr.ContainsKey("normal_skill");
					if (flag4)
					{
						break;
					}
					bool flag5 = ModelBase<PlayerModel>.getInstance().profession == this.skilldic[list[j]].carr;
					if (flag5)
					{
						bool flag6 = this.skilldic[list[j]].now_lv <= 0;
						if (flag6)
						{
							break;
						}
						bool flag7 = this.skilldic[list[j]].now_lv + k < this.skilldic[list[j]].max_lvl;
						if (!flag7)
						{
							break;
						}
						int @int = this.skilldic[list[j]].xml.GetNode("skill_att", "skill_lv==" + (this.skilldic[list[j]].now_lv + 1 + k)).getInt("open_zhuan");
						bool flag8 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl >= (ulong)((long)@int);
						if (!flag8)
						{
							break;
						}
						int int2 = this.skilldic[list[j]].xml.GetNode("skill_att", "skill_lv==" + (this.skilldic[list[j]].now_lv + 1 + k)).getInt("open_lvl");
						bool flag9 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)int2);
						if (!flag9)
						{
							break;
						}
						num3 += this.skilldic[list[j]].xml.GetNode("skill_att", "skill_lv==" + (this.skilldic[list[j]].now_lv + 1 + k)).getInt("money");
						num4 += this.skilldic[list[j]].xml.GetNode("skill_att", "skill_lv==" + (this.skilldic[list[j]].now_lv + 1 + k)).getInt("item_num");
						flag3 = (ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(skill_a3Data.itemId) >= num4 && (ulong)ModelBase<PlayerModel>.getInstance().money >= (ulong)((long)num3));
					}
				}
				bool flag10 = k == num2 & flag3;
				if (flag10)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public bool CheckRuneLevelupAvailable()
		{
			bool flag = this.runedic == null;
			if (flag)
			{
				this.ReadRuneRequire(null);
			}
			List<int> list = new List<int>(this.runeReqDic.Keys);
			bool result;
			for (int i = 0; i < list.Count; i++)
			{
				int now_lv = this.runedic[list[i]].now_lv;
				bool flag2 = now_lv == this.runeReqDic[list[i]].Count;
				if (!flag2)
				{
					RuneRequire runeRequire = this.runeReqDic[list[i]][now_lv + 1];
					bool flag3 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)runeRequire.req_role_lvl) && (ulong)ModelBase<PlayerModel>.getInstance().up_lvl >= (ulong)((long)runeRequire.req_role_zhuan);
					if (flag3)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}
	}
}
