using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class a3_ygyiwuModel : ModelBase<a3_ygyiwuModel>
	{
		public Dictionary<int, yiwuInfo> Allywlist_God = new Dictionary<int, yiwuInfo>();

		public Dictionary<int, yiwuInfo> Allywlist_Pre = new Dictionary<int, yiwuInfo>();

		public Dictionary<int, yiwuInfo> yiwuList_God = new Dictionary<int, yiwuInfo>();

		public Dictionary<int, yiwuInfo> yiwuList_Pre = new Dictionary<int, yiwuInfo>();

		public int nowGod_id = -1;

		public int nowPre_id = 0;

		public int nowGodFB_id = 0;

		public int nowPreFB_id = 0;

		public int nowPre_needupLvl = 0;

		public int nowPre_needLvl = 0;

		public uint yiwuLvl = 0u;

		public uint studyTime = 0u;

		public a3_ygyiwuModel()
		{
			SXML sXML = XMLMgr.instance.GetSXML("accent_relic.relic", "carr==" + ModelBase<PlayerModel>.getInstance().profession);
			List<SXML> nodeList = sXML.GetNodeList("relic_god", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				yiwuInfo yiwuInfo = new yiwuInfo();
				yiwuInfo.id = nodeList[i].getInt("id");
				yiwuInfo.need_zdl = nodeList[i].getInt("zdl");
				yiwuInfo.isGod = true;
				yiwuInfo.name = nodeList[i].getString("name");
				yiwuInfo.des = nodeList[i].getString("des");
				yiwuInfo.needexp = nodeList[i].getInt("exp");
				yiwuInfo.place = nodeList[i].getString("place");
				yiwuInfo.awardName = nodeList[i].getString("des1");
				yiwuInfo.awardDesc = nodeList[i].getString("des2");
				yiwuInfo.iconid = nodeList[i].getInt("icon");
				yiwuInfo.awardId = nodeList[i].GetNode("award", "").getInt("id");
				yiwuInfo.awardType = nodeList[i].GetNode("award", "").getInt("type");
				yiwuInfo.eff = nodeList[i].getString("eff");
				yiwuInfo.fbBox_title = nodeList[i].getString("title");
				yiwuInfo.fbBox_dec = nodeList[i].getString("desc");
				this.Allywlist_God[yiwuInfo.id] = yiwuInfo;
			}
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("accent_relic.relic", "type==" + 2);
			SXML sXML2 = null;
			foreach (SXML current in sXMLList)
			{
				bool flag = current.getInt("carr") == ModelBase<PlayerModel>.getInstance().profession;
				if (flag)
				{
					sXML2 = current;
				}
			}
			List<SXML> nodeList2 = sXML2.GetNodeList("relic_god", "");
			for (int j = 0; j < nodeList2.Count; j++)
			{
				yiwuInfo yiwuInfo2 = new yiwuInfo();
				yiwuInfo2.id = nodeList2[j].getInt("id");
				yiwuInfo2.need_zdl = nodeList2[j].getInt("zdl");
				yiwuInfo2.isGod = false;
				yiwuInfo2.name = nodeList2[j].getString("name");
				yiwuInfo2.des = nodeList2[j].getString("des");
				yiwuInfo2.place = nodeList2[j].getString("place");
				yiwuInfo2.awardName = nodeList2[j].getString("des1");
				yiwuInfo2.awardDesc = nodeList2[j].getString("des2");
				yiwuInfo2.iconid = nodeList2[j].getInt("icon");
				yiwuInfo2.awardId = nodeList2[j].GetNode("award", "").getInt("id");
				yiwuInfo2.awardType = nodeList2[j].GetNode("award", "").getInt("type");
				yiwuInfo2.fbBox_title = nodeList2[j].getString("title");
				yiwuInfo2.fbBox_dec = nodeList2[j].getString("desc");
				yiwuInfo2.eff = nodeList2[j].getString("eff");
				yiwuInfo2.needuplvl = nodeList2[j].getInt("zhuan");
				yiwuInfo2.needlvl = nodeList2[j].getInt("level");
				this.Allywlist_Pre[yiwuInfo2.id] = yiwuInfo2;
			}
		}

		public int GetZTime(int lvl)
		{
			SXML sXML = XMLMgr.instance.GetSXML("accent_relic.relic_knowledge", "");
			return sXML.GetNode("level", "lvl==" + lvl).getInt("cost_time");
		}

		public yiwuInfo GetYiWu_God(int id)
		{
			return this.Allywlist_God[id];
		}

		public yiwuInfo GetYiWu_Pre(int id)
		{
			return this.Allywlist_Pre[id];
		}

		public void loadList()
		{
			foreach (int current in this.Allywlist_God.Keys)
			{
				bool flag = current < this.nowGod_id;
				if (flag)
				{
					bool flag2 = !this.yiwuList_God.ContainsKey(current);
					if (flag2)
					{
						this.yiwuList_God[current] = this.Allywlist_God[current];
					}
				}
			}
			foreach (int current2 in this.Allywlist_Pre.Keys)
			{
				bool flag3 = current2 < this.nowPre_id;
				if (flag3)
				{
					bool flag4 = !this.yiwuList_Pre.ContainsKey(current2);
					if (flag4)
					{
						this.yiwuList_Pre[current2] = this.Allywlist_Pre[current2];
					}
				}
			}
		}
	}
}
