using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class GiftCardType
	{
		public int id;

		public int functp;

		public int acttm;

		public int endTm;

		public string name;

		public int golden = 0;

		public int money = 0;

		public int yinpiao = 0;

		public string desc = "";

		public List<BaseItemData> lItem;

		public void init(Variant d)
		{
			debug.Log("初始化激活码类型::" + d["tp"] + "  " + d["functp"]);
			bool flag = d.ContainsKey("crttm");
			if (flag)
			{
				this.acttm = d["crttm"];
				debug.Log("crttm::" + d["crttm"]);
			}
			this.id = d["tp"];
			this.functp = d["functp"];
			this.endTm = d["fintm"];
			this.name = d["name"];
			bool flag2 = d.ContainsKey("golden");
			if (flag2)
			{
				this.money = d["golden"];
			}
			bool flag3 = d.ContainsKey("yb");
			if (flag3)
			{
				this.golden = d["yb"];
			}
			bool flag4 = d.ContainsKey("bndyb");
			if (flag4)
			{
				this.yinpiao = d["bndyb"];
			}
			bool flag5 = d.ContainsKey("desc");
			if (flag5)
			{
				this.desc = d["desc"];
			}
			bool flag6 = d.ContainsKey("itm");
			if (flag6)
			{
				this.lItem = new List<BaseItemData>();
				List<Variant> arr = d["itm"]._arr;
				for (int i = 0; i < arr.Count; i++)
				{
					BaseItemData baseItemData = new BaseItemData();
					baseItemData.id = arr[i]["id"];
					baseItemData.num = arr[i]["cnt"];
					this.lItem.Add(baseItemData);
				}
			}
		}
	}
}
