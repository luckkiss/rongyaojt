using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class SkillModel : ModelBase<SkillModel>
	{
		public static uint EVENT_INIT_INFO = 0u;

		public static uint EVENT_CHANGE_INFO = 1u;

		protected Variant _skillArr = null;

		protected Variant _clientConf = null;

		public Dictionary<uint, SkillXmlData> skillXMl;

		public Dictionary<uint, SkillHitedXml> skillHitXmls;

		public Dictionary<uint, SkillData> skillDatas;

		public List<SkillData> lSkill;

		public SkillModel()
		{
			this.initXml();
		}

		public void initXml()
		{
			this.skillHitXmls = new Dictionary<uint, SkillHitedXml>();
			SXML sXML = XMLMgr.instance.GetSXML("skill.hited", null);
			bool flag = sXML == null;
			if (flag)
			{
				Debug.Log("config [skill.hited] is missing");
			}
			else
			{
				do
				{
					SkillHitedXml skillHitedXml = new SkillHitedXml();
					string[] array = sXML.getString("rim_Color").Split(new string[]
					{
						","
					}, StringSplitOptions.None);
					bool flag2 = array.Length == 3;
					if (flag2)
					{
						Color rimColor = new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
						array = sXML.getString("main_Color").Split(new string[]
						{
							","
						}, StringSplitOptions.None);
						bool flag3 = array.Length == 3;
						if (flag3)
						{
							Color mainColor = new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
							skillHitedXml.rimColor = rimColor;
							skillHitedXml.mainColor = mainColor;
							this.skillHitXmls[sXML.getUint("id")] = skillHitedXml;
						}
					}
				}
				while (sXML.nextOne());
				this.skillXMl = new Dictionary<uint, SkillXmlData>();
				sXML = XMLMgr.instance.GetSXML("skill.skill", null);
				do
				{
					uint @uint = sXML.getUint("id");
					SkillXmlData skillXmlData = default(SkillXmlData);
					skillXmlData.id = @uint;
					skillXmlData.skill_name = sXML.getString("skill_name");
					skillXmlData.eff = sXML.getString("eff");
					skillXmlData.eff_female = sXML.getString("eff_female");
					skillXmlData.target_type = sXML.getInt("skill_targettype");
					SXML node = sXML.GetNode("jump", null);
					skillXmlData.useJump = false;
					skillXmlData.hitfall = (sXML.getInt("hitfall") == 1);
					bool flag4 = node != null;
					if (flag4)
					{
						skillXmlData.useJump = true;
						skillXmlData.jump_canying = node.getString("canying");
					}
					uint uint2 = sXML.getUint("hide_id");
					bool flag5 = this.skillHitXmls.ContainsKey(uint2);
					if (flag5)
					{
						skillXmlData.hitxml = this.skillHitXmls[uint2];
					}
					skillXmlData.lv = new Dictionary<uint, SkillLvXmlData>();
					node = sXML.GetNode("Level", null);
					bool flag6 = node != null;
					if (flag6)
					{
						do
						{
							SkillLvXmlData skillLvXmlData = default(SkillLvXmlData);
							uint uint3 = node.getUint("level");
							skillLvXmlData.range = (float)(node.getInt("range") + 32) / 53.333f;
							skillLvXmlData.range_gezi = (int)skillLvXmlData.range / 32;
							skillLvXmlData.cd = node.getUint("cd_time");
							skillLvXmlData.desc = node.getString("description");
							skillLvXmlData.pvp_param = node.getInt("pvp_param");
							skillLvXmlData.needMoney = node.getInt("need_money");
							skillLvXmlData.attr = node.getInt("skill_attribute");
							skillLvXmlData.needExp = node.getInt("need_exp");
							skillXmlData.lv[uint3] = skillLvXmlData;
						}
						while (node.nextOne());
					}
					this.skillXMl[@uint] = skillXmlData;
				}
				while (sXML.nextOne());
			}
		}

		public void changeSkillList(Variant data)
		{
			uint num = data["skill_id"];
			uint lv = data["skill_level"];
			bool flag = num > 1009u;
			if (!flag)
			{
				SkillData skillData = this.getSkillData(num);
				bool flag2 = skillData == null;
				if (flag2)
				{
					skillData = new SkillData();
				}
				skillData.id = num;
				skillData.lv = lv;
				skillData.xml = this.skillXMl[num];
				skillData.range_gezi = skillData.xml.lv[skillData.lv].range_gezi;
				skillData.range = skillData.xml.lv[skillData.lv].range;
				skillData.maxCd = skillData.xml.lv[skillData.lv].cd * 100u;
				bool flag3 = !this.skillDatas.ContainsKey(num);
				if (flag3)
				{
					this.skillDatas[num] = skillData;
					this.lSkill.Add(skillData);
				}
				base.dispatchEvent(GameEvent.Create(SkillModel.EVENT_CHANGE_INFO, this, null, false));
			}
		}

		public void initSkillList(List<Variant> arr)
		{
			bool flag = this.skillDatas == null;
			if (flag)
			{
				this.skillDatas = new Dictionary<uint, SkillData>();
			}
			bool flag2 = this.lSkill == null;
			if (flag2)
			{
				this.lSkill = new List<SkillData>();
			}
			foreach (Variant current in arr)
			{
				bool flag3 = current["skill_id"] < 1010;
				if (flag3)
				{
					SkillData skillData = new SkillData();
					uint num = current["skill_id"];
					skillData.id = num;
					skillData.lv = current["skill_level"];
					bool flag4 = this.skillXMl == null;
					if (!flag4)
					{
						bool flag5 = this.skillXMl.ContainsKey(num);
						if (flag5)
						{
							skillData.xml = this.skillXMl[num];
							skillData.range_gezi = skillData.xml.lv[skillData.lv].range_gezi;
							skillData.range = skillData.xml.lv[skillData.lv].range;
							skillData.maxCd = skillData.xml.lv[skillData.lv].cd * 100u;
							this.skillDatas[skillData.id] = skillData;
							this.lSkill.Add(skillData);
						}
					}
				}
			}
			base.dispatchEvent(GameEvent.Create(SkillModel.EVENT_INIT_INFO, this, null, false));
		}

		public SkillData getSkillData(uint id)
		{
			bool flag = this.skillDatas.ContainsKey(id);
			SkillData result;
			if (flag)
			{
				result = this.skillDatas[id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public SkillXmlData getSkillXml(uint id)
		{
			bool flag = this.skillXMl.ContainsKey(id);
			SkillXmlData result;
			if (flag)
			{
				result = this.skillXMl[id];
			}
			else
			{
				result = this.skillXMl[1001u];
			}
			return result;
		}
	}
}
