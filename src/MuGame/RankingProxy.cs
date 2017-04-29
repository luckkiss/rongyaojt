using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class RankingProxy : BaseProxy<RankingProxy>
	{
		private List<RankData> rankList1 = new List<RankData>();

		private List<RankData> rankList2 = new List<RankData>();

		private List<RankData> rankList3 = new List<RankData>();

		private List<RankData> rankList4 = new List<RankData>();

		private List<RankData> rankList = new List<RankData>();

		public Dictionary<uint, List<RankData>> dic1 = new Dictionary<uint, List<RankData>>();

		public Dictionary<uint, List<RankData>> dic2 = new Dictionary<uint, List<RankData>>();

		public Dictionary<uint, List<RankData>> dic3 = new Dictionary<uint, List<RankData>>();

		public Dictionary<uint, List<RankData>> dic4 = new Dictionary<uint, List<RankData>>();

		public int type;

		private uint currentPage;

		public static uint ON_GET_RANK_INFO = 10u;

		public void sendMsg(uint type, uint page)
		{
			Variant variant = new Variant();
			variant["rank_type"] = type;
			variant["page"] = page;
			this.currentPage = page;
			this.sendRPC(254u, variant);
		}

		private void getInfo(Variant data)
		{
			bool flag = data["res"] <= 0;
			if (flag)
			{
				base.dispatchEvent(GameEvent.Create(RankingProxy.ON_GET_RANK_INFO, this, data, false));
			}
			else
			{
				this.type = data["res"];
				List<RankData> list = new List<RankData>();
				data["page"] = this.currentPage;
				bool flag2 = data["ranks"]._arr.Count != 0;
				if (flag2)
				{
					int num = 1;
					foreach (Variant current in data["ranks"]._arr)
					{
						RankData rankData = default(RankData);
						rankData.rank = current["rank"];
						rankData.cid = current["cid"];
						rankData.number = (int)((this.currentPage - 1u) * 5u + (uint)num);
						switch (this.type)
						{
						case 1:
						{
							rankData.name = current["name"];
							rankData.family_name = current["family_name"];
							bool flag3 = rankData.family_name == "";
							if (flag3)
							{
								rankData.family_name = "无";
							}
							rankData.combpt = current["combpt"];
							rankData.sex = current["sex"];
							list.Add(rankData);
							break;
						}
						case 2:
						{
							rankData.name = current["name"];
							rankData.family_name = current["family_name"];
							bool flag4 = rankData.family_name == "";
							if (flag4)
							{
								rankData.family_name = "无";
							}
							rankData.combpt = current["level"];
							rankData.sex = current["sex"];
							list.Add(rankData);
							break;
						}
						case 3:
						{
							rankData.name = current["name"];
							rankData.family_name = current["family_name"];
							bool flag5 = rankData.family_name == "";
							if (flag5)
							{
								rankData.family_name = "无";
							}
							rankData.combpt = current["hero_combpt"];
							rankData.sex = current["hero_id"];
							bool flag6 = current.ContainsKey("strengthen");
							if (flag6)
							{
								rankData.strengthen = current["strengthen"];
							}
							list.Add(rankData);
							break;
						}
						case 4:
						{
							rankData.name = current["leader_name"];
							rankData.family_name = current["name"];
							bool flag7 = rankData.family_name == "";
							if (flag7)
							{
								rankData.family_name = "无";
							}
							rankData.combpt = current["total_combpt"];
							rankData.member_num = current["member_num"];
							rankData.logoid = current["logo_id"];
							rankData.family_level = current["family_level"];
							list.Add(rankData);
							break;
						}
						}
						num++;
					}
					switch (this.type)
					{
					case 1:
					{
						bool flag8 = this.dic1.ContainsKey(this.currentPage);
						if (flag8)
						{
							this.dic1.Remove(this.currentPage);
						}
						this.dic1.Add(this.currentPage, list);
						break;
					}
					case 2:
					{
						bool flag9 = this.dic2.ContainsKey(this.currentPage);
						if (flag9)
						{
							this.dic2.Remove(this.currentPage);
						}
						this.dic2.Add(this.currentPage, list);
						break;
					}
					case 3:
					{
						bool flag10 = this.dic3.ContainsKey(this.currentPage);
						if (flag10)
						{
							this.dic3.Remove(this.currentPage);
						}
						this.dic3.Add(this.currentPage, list);
						break;
					}
					case 4:
					{
						bool flag11 = this.dic4.ContainsKey(this.currentPage);
						if (flag11)
						{
							this.dic4.Remove(this.currentPage);
						}
						this.dic4.Add(this.currentPage, list);
						break;
					}
					}
				}
				base.dispatchEvent(GameEvent.Create(RankingProxy.ON_GET_RANK_INFO, this, data, false));
			}
		}
	}
}
