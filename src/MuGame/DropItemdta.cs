using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class DropItemdta
	{
		public int x;

		public int y;

		public uint ownerId;

		public uint dpid;

		public uint tp;

		public uint owner_tm;

		public bool no_owner = false;

		public int tpid;

		public long left_tm;

		public int count = 1;

		public int intensify_lv;

		public int add_level;

		public int stage;

		public SXML itemXml;

		public long initedTimer;

		public int num;

		public void init(Variant v, long timer)
		{
			this.no_owner = false;
			this.dpid = v["dpid"];
			bool flag = v.ContainsKey("tp");
			if (flag)
			{
				this.tp = v["tp"];
			}
			else
			{
				this.tp = 0u;
			}
			this.initedTimer = timer;
			this.left_tm = v["left_tm"] * 1000L + timer;
			bool flag2 = v.ContainsKey("eqp");
			if (flag2)
			{
				Variant variant = v["eqp"];
				this.tpid = variant["tpid"];
				this.intensify_lv = variant["intensify_lv"];
				this.add_level = variant["add_level"];
				this.stage = variant["stage"];
				this.itemXml = ModelBase<a3_BagModel>.getInstance().getItemXml(this.tpid);
			}
			else
			{
				bool flag3 = v.ContainsKey("itm");
				if (flag3)
				{
					Variant variant2 = v["itm"];
					this.tpid = variant2["id"];
					bool flag4 = variant2.ContainsKey("cnt");
					if (flag4)
					{
						this.count = variant2["cnt"];
					}
					this.itemXml = ModelBase<a3_BagModel>.getInstance().getItemXml(this.tpid);
				}
				else
				{
					bool flag5 = v.ContainsKey("gold");
					if (flag5)
					{
						this.tpid = 0;
						this.count = v["gold"];
					}
				}
			}
			bool flag6 = v.ContainsKey("owner") && this.tp != 3u;
			if (flag6)
			{
				this.ownerId = v["owner"]._uint;
			}
			else
			{
				this.ownerId = 99999u;
			}
			bool flag7 = v.ContainsKey("owner_tm");
			if (flag7)
			{
				this.owner_tm = v["owner_tm"]._uint;
			}
			else
			{
				this.owner_tm = (uint)(NetClient.instance.CurServerTimeStamp + 1000);
			}
		}

		public string getName()
		{
			bool flag = this.tpid == 0;
			string result;
			if (flag)
			{
				result = "<color=#ffd800>" + ContMgr.getCont("comm_money", new string[]
				{
					this.count.ToString()
				}) + "</color>";
			}
			else
			{
				result = Globle.getColorStrByQuality(this.itemXml.getString("item_name"), this.itemXml.getInt("quality"));
			}
			return result;
		}

		public string getDropItemName()
		{
			bool flag = this.tpid == 0;
			string result;
			if (flag)
			{
				result = ContMgr.getCont("comm_money", new string[]
				{
					this.count.ToString()
				});
			}
			else
			{
				result = Globle.getColorStrByQuality(this.itemXml.getString("item_name"), this.itemXml.getInt("quality"));
			}
			return result;
		}
	}
}
