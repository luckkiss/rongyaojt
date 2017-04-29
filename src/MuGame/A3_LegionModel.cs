using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class A3_LegionModel : ModelBase<A3_LegionModel>
	{
		public A3_LegionData myLegion = default(A3_LegionData);

		public A3_LegionBuffData myLegionbuff = default(A3_LegionBuffData);

		public A3_LegionBuffData myLegionbuff_cost = default(A3_LegionBuffData);

		public List<A3_LegionData> list = new List<A3_LegionData>();

		public List<A3_LegionData> list2 = new List<A3_LegionData>();

		public Dictionary<int, A3_LegionMember> members = new Dictionary<int, A3_LegionMember>();

		public Dictionary<int, A3_LegionMember> applicant = new Dictionary<int, A3_LegionMember>();

		public Variant logdata;

		public bool CanAutoApply;

		private SXML itemsXMl;

		public int create_needlv;

		public int create_needzhuan;

		public int create_needmoney;

		public int donate;

		internal Variant mydonate;

		public int showtype = -1;

		public A3_LegionModel()
		{
			this.itemsXMl = XMLMgr.instance.GetSXML("clan", "");
			SXML node = this.itemsXMl.GetNode("create", "");
			this.create_needzhuan = node.getInt("zhuan");
			this.create_needlv = node.getInt("lvl");
			this.create_needmoney = node.getInt("money_cost");
		}

		public void AddMember(Variant data)
		{
			A3_LegionMember a3_LegionMember = default(A3_LegionMember);
			a3_LegionMember.cid = data["cid"];
			a3_LegionMember.donate = data["donate"];
			a3_LegionMember.clanc = data["clanc"];
			a3_LegionMember.name = data["name"];
			a3_LegionMember.lvl = data["lvl"];
			a3_LegionMember.zhuan = data["zhuan"];
			a3_LegionMember.carr = data["carr"];
			a3_LegionMember.combpt = data["combpt"];
			a3_LegionMember.huoyue = data["active"];
			bool flag = data.ContainsKey("lastlogoff");
			if (flag)
			{
				a3_LegionMember.lastlogoff = data["lastlogoff"];
			}
			this.members[a3_LegionMember.cid] = a3_LegionMember;
			bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().cid == (ulong)((long)a3_LegionMember.cid);
			if (flag2)
			{
				bool flag3 = a3_LegionMember.clanc > 1;
				if (flag3)
				{
					a3_legion expr_13F = a3_legion.mInstance;
					if (expr_13F != null)
					{
						expr_13F.transform.FindChild("s4/tabs/application").gameObject.SetActive(true);
					}
				}
				else
				{
					a3_legion expr_169 = a3_legion.mInstance;
					if (expr_169 != null)
					{
						expr_169.transform.FindChild("s4/tabs/application").gameObject.SetActive(false);
					}
				}
				this.donate = a3_LegionMember.donate;
			}
			bool flag4 = a3_legion_info.mInstance != null;
			if (flag4)
			{
				a3_legion_info.mInstance.buff_up();
			}
		}

		public void RefreshApplicant(Variant data)
		{
			Variant variant = data["info"];
			this.applicant.Clear();
			foreach (Variant current in variant._arr)
			{
				A3_LegionMember a3_LegionMember = default(A3_LegionMember);
				a3_LegionMember.cid = current["cid"];
				a3_LegionMember.name = current["name"];
				a3_LegionMember.lvl = current["lvl"];
				a3_LegionMember.zhuan = current["zhuan"];
				a3_LegionMember.combpt = current["combpt"];
				a3_LegionMember.carr = current["carr"];
				a3_LegionMember.tm = current["tm"];
				this.applicant[a3_LegionMember.cid] = a3_LegionMember;
			}
		}

		public void AddLog(Variant data)
		{
			bool flag = this.logdata == null;
			if (flag)
			{
				this.logdata = data;
			}
			else
			{
				Variant variant = data["clanlog_list"];
				foreach (Variant current in variant._arr)
				{
					this.logdata["clanlog_list"]._arr.Add(current);
				}
			}
		}

		public static string GetCarr(int i)
		{
			string result = null;
			switch (i)
			{
			case 2:
				result = ContMgr.getCont("comm_job2", null);
				break;
			case 3:
				result = ContMgr.getCont("comm_job3", null);
				break;
			case 5:
				result = ContMgr.getCont("comm_job5", null);
				break;
			}
			return result;
		}

		public int legion_weihu(int clan_lv)
		{
			SXML sXML = XMLMgr.instance.GetSXML("clan.clan_repair", "clan_lv==" + clan_lv);
			return sXML.getInt("repair_money");
		}

		public void SetLegionBuff(int lvl)
		{
			bool flag = this.myLegionbuff.buffs != null;
			if (flag)
			{
				this.myLegionbuff.buffs.Clear();
			}
			SXML node = this.itemsXMl.GetNode("clan_buff", "lvl==" + lvl);
			List<SXML> list = (node != null) ? node.GetNodeList("buff", "") : null;
			this.myLegionbuff.buffs = new Dictionary<int, int>();
			foreach (SXML current in list)
			{
				this.myLegionbuff.buffs[current.getInt("att_type")] = current.getInt("att_value");
			}
			this.myLegionbuff.cost_donate = node.getInt("cost_donate");
			this.myLegionbuff.cost_item = node.getInt("cost_item");
			this.myLegionbuff.cost_num = node.getInt("cost_num");
			bool flag2 = lvl < 12;
			if (flag2)
			{
				this.SetLegionBuff_cost(lvl + 1);
			}
		}

		public void SetLegionBuff_cost(int lvl)
		{
			bool flag = this.myLegionbuff_cost.buffs != null;
			if (flag)
			{
				this.myLegionbuff_cost.buffs.Clear();
			}
			SXML node = this.itemsXMl.GetNode("clan_buff", "lvl==" + lvl);
			List<SXML> list = (node != null) ? node.GetNodeList("buff", "") : null;
			this.myLegionbuff_cost.buffs = new Dictionary<int, int>();
			foreach (SXML current in list)
			{
				this.myLegionbuff_cost.buffs[current.getInt("att_type")] = current.getInt("att_value");
			}
			this.myLegionbuff_cost.cost_donate = node.getInt("cost_donate");
			this.myLegionbuff_cost.cost_item = node.getInt("cost_item");
			this.myLegionbuff_cost.cost_num = node.getInt("cost_num");
		}

		public void SetMyLegion(int lvl)
		{
			SXML node = this.itemsXMl.GetNode("clan", "clan_lvl==" + lvl);
			this.myLegion.member_max = node.getInt("member");
			this.myLegion.veteran = node.getInt("veteran");
			this.myLegion.elite = node.getInt("elite");
			this.myLegion.ordinary = node.getInt("ordinary");
			this.myLegion.gold_cost = node.getInt("money_cost");
			this.myLegion.exp_cost = node.getInt("exp_cost");
			List<SXML> nodeList = this.itemsXMl.GetNodeList("clan", "");
			this.myLegion.max_lvl = nodeList.Count;
		}

		public string GetClancToName(int clanc)
		{
			return ContMgr.getCont("legin_tag" + clanc, null);
		}
	}
}
