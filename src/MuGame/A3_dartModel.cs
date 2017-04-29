using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class A3_dartModel : ModelBase<A3_dartModel>
	{
		private Dictionary<int, clans> dicClan = new Dictionary<int, clans>();

		private SXML ss = new SXML();

		private List<SXML> listXml = new List<SXML>();

		private int length;

		public uint item_id;

		public void init(uint line)
		{
			this.ss = XMLMgr.instance.GetSXML("clan_escort", "");
			this.listXml = this.ss.GetNodeList("line", "");
			this.length = this.listXml.Count;
			clans value = default(clans);
			for (int i = 0; i < this.length; i++)
			{
				value.open_lv_clan = this.listXml[i].getInt("clan_lvl");
				value.pathid = this.listXml[i].getUint("path");
				value.target_map = this.listXml[i].getUint("target_map");
				value.add_money_num = this.listXml[i].getInt("clan_money");
				value.item_id = this.listXml[i].getUint("item_id");
				value.item_num = this.listXml[i].getInt("item_num");
				bool flag = !this.dicClan.ContainsKey(this.listXml[i].getInt("id"));
				if (flag)
				{
					this.dicClan.Add(this.listXml[i].getInt("id"), value);
				}
			}
			debug.Log(line.ToString() + "is line");
			this.item_id = this.dicClan[(int)line].item_id;
		}
	}
}
