using System;
using System.Collections.Generic;

namespace MuGame
{
	public class ResetLvLModel : ModelBase<ResetLvLModel>
	{
		public uint getExpByResetLvL(int profession, uint zhuan, uint lvl)
		{
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl.carr", "carr==" + profession);
			SXML node = sXML.GetNode("zhuanshen", "zhuan==" + zhuan);
			SXML node2 = node.GetNode("carr", "lvl==" + lvl);
			return (node2 == null) ? 0u : node2.getUint("exp");
		}

		public uint getNeedLvLByCurrentZhuan(int profession, uint zhuan)
		{
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl.carr", "carr==" + profession);
			bool flag = sXML == null;
			uint result;
			if (flag)
			{
				result = 4294967295u;
			}
			else
			{
				SXML node = sXML.GetNode("zhuanshen", "zhuan==" + zhuan);
				List<SXML> nodeList = node.GetNodeList("carr", null);
				result = nodeList[nodeList.Count - 1].getUint("lvl");
			}
			return result;
		}

		public uint getNeedExpByCurrentZhuan(int profession, uint zhuan)
		{
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl.carr", "carr==" + profession);
			bool flag = sXML == null;
			uint result;
			if (flag)
			{
				result = 4294967295u;
			}
			else
			{
				SXML node = sXML.GetNode("zhuanshen", "zhuan==" + zhuan);
				List<SXML> nodeList = node.GetNodeList("carr", null);
				result = nodeList[nodeList.Count - 1].getUint("exp");
			}
			return result;
		}

		public uint getAwardAttrPointByZhuan(int profession, uint zhuan)
		{
			zhuan += 1u;
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl.carr", "carr==" + profession);
			SXML node = sXML.GetNode("zhuanshen", "zhuan==" + zhuan);
			return (node == null) ? 0u : node.getUint("att_pt");
		}

		public uint getNextLvLByZhuan(int profession, uint zhuan, uint exp)
		{
			uint result = 0u;
			zhuan += 1u;
			uint num = 0u;
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl.carr", "carr==" + profession);
			SXML node = sXML.GetNode("zhuanshen", "zhuan==" + zhuan);
			List<SXML> nodeList = node.GetNodeList("carr", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				num += nodeList[i].getUint("exp");
				bool flag = exp < num;
				if (flag)
				{
					result = nodeList[i].getUint("lvl");
					break;
				}
			}
			return result;
		}

		public uint getNeedGoldsByZhuan(int profession, uint zhuan)
		{
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl.carr", "carr==" + profession);
			SXML node = sXML.GetNode("zhuanshen", "zhuan==" + zhuan);
			return (node == null) ? 0u : node.getUint("money_cost");
		}

		public uint getAllExpByZhuan(int profession, uint zhuan)
		{
			zhuan += 1u;
			uint num = 0u;
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl.carr", "carr==" + profession);
			SXML node = sXML.GetNode("zhuanshen", "zhuan==" + zhuan);
			uint num2 = node.getUint("exp_pool_level");
			num2 -= 1u;
			List<SXML> nodeList = node.GetNodeList("carr", null);
			for (int i = 0; i < nodeList.Count; i++)
			{
				bool flag = nodeList[i].getUint("lvl") <= num2;
				if (flag)
				{
					num += nodeList[i].getUint("exp");
				}
			}
			return num;
		}
	}
}
