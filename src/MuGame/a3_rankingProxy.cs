using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class a3_rankingProxy : BaseProxy<a3_rankingProxy>
	{
		public static uint EVENT_INFO = 0u;

		public a3_rankingProxy()
		{
			this.addProxyListener(254u, new Action<Variant>(this.getInfo));
		}

		public void send_Getinfo(uint type, uint page)
		{
			Variant variant = new Variant();
			variant["rank_type"] = type;
			variant["page"] = page;
			this.sendRPC(254u, variant);
		}

		public void send_Getinfo(uint type, uint page, uint self)
		{
			Variant variant = new Variant();
			variant["rank_type"] = type;
			variant["page"] = page;
			variant["self_rank"] = self;
			this.sendRPC(254u, variant);
		}

		public void getTime()
		{
			Variant variant = new Variant();
			variant["rank_type"] = 0;
			this.sendRPC(254u, variant);
		}

		private void getInfo(Variant data)
		{
			debug.Log("排行榜" + data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (!flag)
			{
				switch (num)
				{
				case 0:
				{
					float num2 = data["limit"];
					ModelBase<a3_rankingModel>.getInstance().runTime(num2);
					bool flag2 = a3_ranking.isshow;
					if (flag2)
					{
						a3_ranking.isshow.setTime(num2);
					}
					break;
				}
				case 1:
				{
					List<RankingData> list = new List<RankingData>();
					Variant variant = data["ranks"];
					foreach (Variant current in variant._arr)
					{
						list.Add(new RankingData
						{
							rank = current["rank"],
							cid = current["cid"],
							carr = current["carr"],
							combpt = current["combpt"],
							zhuan = current["zhuan"],
							lvl = current["lvl"],
							viplvl = current["vip"],
							name = current["name"]
						});
					}
					bool flag3 = a3_ranking.instan;
					if (flag3)
					{
						a3_ranking.instan.Getinfo_panel(list, num);
					}
					break;
				}
				case 2:
				{
					List<RankingData> list2 = new List<RankingData>();
					Variant variant2 = data["ranks"];
					foreach (Variant current2 in variant2._arr)
					{
						list2.Add(new RankingData
						{
							rank = current2["rank"],
							cid = current2["cid"],
							carr = current2["carr"],
							combpt = current2["combpt"],
							zhuan = current2["zhuan"],
							lvl = current2["lvl"],
							viplvl = current2["vip"],
							name = current2["name"]
						});
					}
					bool flag4 = a3_ranking.instan;
					if (flag4)
					{
						a3_ranking.instan.Getinfo_panel(list2, num);
					}
					break;
				}
				case 3:
				{
					List<RankingData> list3 = new List<RankingData>();
					Variant variant3 = data["ranks"];
					foreach (Variant current3 in variant3._arr)
					{
						list3.Add(new RankingData
						{
							rank = current3["rank"],
							cid = current3["cid"],
							carr = current3["carr"],
							flylvl = current3["level"],
							stage = current3["stage"],
							viplvl = current3["vip"],
							name = current3["name"]
						});
					}
					bool flag5 = a3_ranking.instan;
					if (flag5)
					{
						a3_ranking.instan.Getinfo_panel(list3, num);
					}
					break;
				}
				case 4:
				{
					List<RankingData> list4 = new List<RankingData>();
					Variant variant4 = data["ranks"];
					foreach (Variant current4 in variant4._arr)
					{
						list4.Add(new RankingData
						{
							rank = current4["rank"],
							jt_combpt = current4["combpt"],
							jt_id = current4["clanid"],
							jt_lvl = current4["lvl"],
							jt_name = current4["clname"]
						});
					}
					bool flag6 = a3_ranking.instan;
					if (flag6)
					{
						a3_ranking.instan.Getinfo_panel(list4, num);
					}
					break;
				}
				case 5:
				{
					List<RankingData> list5 = new List<RankingData>();
					Variant variant5 = data["ranks"];
					foreach (Variant current5 in variant5._arr)
					{
						list5.Add(new RankingData
						{
							rank = current5["rank"],
							cid = current5["cid"],
							carr = current5["carr"],
							name = current5["name"],
							viplvl = current5["vip"],
							zhs_combpt = current5["combpt"],
							zhs_id = current5["id"],
							zhs_lvl = current5["level"],
							zhs_tpid = current5["tpid"]
						});
					}
					bool flag7 = a3_ranking.instan;
					if (flag7)
					{
						a3_ranking.instan.Getinfo_panel(list5, num);
					}
					break;
				}
				}
				bool flag8 = data.ContainsKey("self_rank");
				if (flag8)
				{
					bool flag9 = a3_ranking.instan;
					if (flag9)
					{
						a3_ranking.instan.refresh_myRank(num, data["self_rank"]);
					}
				}
			}
		}
	}
}
