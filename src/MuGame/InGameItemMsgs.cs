using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameItemMsgs : MsgProcduresBase
	{
		public InGameItemMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameItemMsgs create(IClientBase m)
		{
			return new InGameItemMsgs(m);
		}

		public override void init()
		{
		}

		protected void regItemChangeRPC()
		{
			this.g_mgr.regRPCProcesser(75u, new NetManager.RPCProcCreator(on_item_change.create));
		}

		protected void regForgeRPC()
		{
			this.g_mgr.regRPCProcesser(72u, new NetManager.RPCProcCreator(on_eqp_forge_res.create));
		}

		protected void regMarketRPC()
		{
			this.g_mgr.regRPCProcesser(200u, new NetManager.RPCProcCreator(on_add_auc_itm_res.create));
			this.g_mgr.regRPCProcesser(201u, new NetManager.RPCProcCreator(on_rmv_auc_itm_res.create));
			this.g_mgr.regRPCProcesser(202u, new NetManager.RPCProcCreator(on_buy_auc_itm_res.create));
			this.g_mgr.regRPCProcesser(203u, new NetManager.RPCProcCreator(on_get_ply_auc_list_res.create));
			this.g_mgr.regRPCProcesser(204u, new NetManager.RPCProcCreator(on_fetch_auc_money_res.create));
			this.g_mgr.regRPCProcesser(205u, new NetManager.RPCProcCreator(on_get_auc_info_res.create));
		}

		public void add_auc_itm(Variant data)
		{
			bool flag = data == null;
			if (!flag)
			{
				bool flag2 = data["gld"] <= 0 && data["yb"] <= 0;
				if (!flag2)
				{
					bool flag3 = !data.ContainsKey("auctp");
					if (flag3)
					{
						data["auctp"] = 0;
					}
					bool flag4 = data.ContainsKey("yb_bid");
					if (flag4)
					{
						data.RemoveKey("gld_bid");
					}
					bool flag5 = data.ContainsKey("yb");
					if (flag5)
					{
						data.RemoveKey("gld");
					}
					base.sendRPC(200u, data);
				}
			}
		}

		public void rmv_auc_itm(uint itmid, int auctp = 0)
		{
			Variant variant = new Variant();
			variant["itmid"] = itmid;
			variant["auctp"] = auctp;
			base.sendRPC(201u, variant);
		}

		public void buy_auc_itm(uint cid, uint itmid, int auctp = 0)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["itmid"] = itmid;
			variant["auctp"] = auctp;
			base.sendRPC(202u, variant);
		}

		public void get_ply_auc_list(Variant obj)
		{
			bool flag = !obj.ContainsKey("auctp");
			if (flag)
			{
				obj["auctp"] = 0;
			}
			base.sendRPC(203u, obj);
		}

		public void fetch_auc_money()
		{
			Variant msg = new Variant();
			base.sendRPC(204u, msg);
		}

		public void get_auc_info(Variant data)
		{
			bool flag = 3 == data["tp"] && data["yb_bid"];
			if (flag)
			{
				data.RemoveKey("gld_bid");
			}
			base.sendRPC(205u, data);
		}

		public void trans_repo_itm(uint item_id, uint npcid, uint direct, int repotp = -1)
		{
			Variant variant = new Variant();
			variant["item_id"] = item_id;
			variant["npcid"] = npcid;
			variant["direct"] = direct;
			bool flag = repotp >= 0;
			if (flag)
			{
				variant["repotp"] = repotp;
			}
			base.sendRPC(33u, variant);
		}

		public void mod_pkg_spc(uint maxi_add, uint pkg_tp)
		{
			Variant variant = new Variant();
			variant["maxi_add"] = maxi_add;
			variant["pkg_tp"] = pkg_tp;
			base.sendRPC(37u, variant);
		}

		public void repair_eqp_dura(uint eqpid)
		{
			Variant variant = new Variant();
			variant["id"] = eqpid;
			variant["npcid"] = 0;
			base.sendRPC(39u, variant);
		}

		public void wh()
		{
		}

		public void eqp_frg_trans(uint frmid, uint toid)
		{
			Variant variant = new Variant();
			variant["frmid"] = frmid;
			variant["toid"] = toid;
			base.sendRPC(59u, variant);
		}

		public void sort_pkg(uint tp)
		{
			Variant variant = new Variant();
			variant["tp"] = tp;
			base.sendRPC(61u, variant);
		}

		public void buy_item(uint npcid, uint tpid, uint cnt, uint mktp, int gift_cid = -1)
		{
			Variant variant = new Variant();
			variant["npcid"] = npcid;
			variant["tpid"] = tpid;
			variant["cnt"] = cnt;
			variant["mktp"] = mktp;
			bool flag = gift_cid >= 0;
			if (flag)
			{
				variant["gift_cid"] = gift_cid;
			}
			base.sendRPC(62u, variant);
		}

		public void sell_item(uint npcid, uint id)
		{
			Variant variant = new Variant();
			variant["npcid"] = npcid;
			variant["id"] = id;
			base.sendRPC(63u, variant);
		}

		public void buy_sold_item(uint npcid, uint id)
		{
			Variant variant = new Variant();
			variant["npcid"] = npcid;
			variant["id"] = id;
			base.sendRPC(64u, variant);
		}

		public void get_items(uint sold)
		{
			Variant variant = new Variant();
			variant["sold"] = sold;
			base.sendRPC(65u, variant);
		}

		public void split_item(uint id, uint spcnt)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["spcnt"] = spcnt;
			base.sendRPC(66u, variant);
		}

		public void combine_item(uint idto, uint idfrm, uint frmcnt)
		{
			Variant variant = new Variant();
			variant["idto"] = idto;
			variant["idfrm"] = idfrm;
			variant["frmcnt"] = frmcnt;
			base.sendRPC(67u, variant);
		}

		public void use_item(uint id)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			base.sendRPC(68u, variant);
		}

		public void get_use_item(uint id, uint idx)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["idx"] = idx;
			base.sendRPC(68u, variant);
		}

		public void modify_item(uint id, uint req_id, int expire = -1)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["req_id"] = req_id;
			bool flag = expire >= 0;
			if (flag)
			{
				variant["expire"] = expire;
			}
			base.sendRPC(70u, variant);
		}

		public void delete_item(uint id)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			base.sendRPC(71u, variant);
		}

		public void eqp_forge(uint id, uint tar_lvl, int rateitem = 0)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["tp"] = 1;
			variant["tar_lvl"] = tar_lvl;
			variant["useprt"] = false;
			bool flag = rateitem != 0;
			if (flag)
			{
				variant["rateitem"] = rateitem;
				base.sendRPC(72u, variant);
			}
			else
			{
				base.sendRPC(72u, variant);
			}
		}

		public void HallowEnchant(uint id, uint elvl)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["elvl"] = elvl;
			variant["tp"] = 7;
			base.sendRPC(72u, variant);
		}

		public void eqp_modify_add_att(uint id)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["tp"] = 2;
			base.sendRPC(72u, variant);
		}

		public void eqp_super_forge(uint id, bool enhance, uint grpid)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["enhance"] = enhance;
			variant["grpid"] = grpid;
			variant["tp"] = 3;
			base.sendRPC(72u, variant);
		}

		public void clear_eqp_super_forge_att(uint id, uint grpid)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["grpid"] = grpid;
			variant["tp"] = 4;
			base.sendRPC(72u, variant);
		}

		public void eqp_active_att380(uint id)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["tp"] = 5;
			base.sendRPC(72u, variant);
		}

		public void eqp_veri_exatt(uint id)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["tp"] = 6;
			base.sendRPC(72u, variant);
		}

		public void embed_stone(uint eqpid, Variant stnids)
		{
			Variant variant = new Variant();
			variant["eqpid"] = eqpid;
			variant["stnids"] = stnids;
			base.sendRPC(73u, variant);
		}

		public void remove_stone(uint eqpid, uint stnid)
		{
			Variant variant = new Variant();
			variant["eqpid"] = eqpid;
			variant["stnid"] = stnid;
			base.sendRPC(74u, variant);
		}

		public void eqp_decomp(uint eqpid, bool restore = false)
		{
			Variant variant = new Variant();
			variant["eqpid"] = eqpid;
			variant["restore"] = restore;
			base.sendRPC(75u, variant);
		}

		public void eqp_up(uint eqpid)
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			variant["eqpid"] = eqpid;
			base.sendRPC(76u, variant);
		}

		public void pick_dpitem(uint id)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			base.sendRPC(77u, variant);
		}

		public void get_dbmkt_itm()
		{
			Variant msg = new Variant();
			base.sendRPC(94u, msg);
		}

		public void itm_merge(uint id, Variant cost_eqps, Variant cost_itms, Variant rate_itms = null)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			bool flag = cost_eqps != null && cost_eqps._arr.Count > 0;
			if (flag)
			{
				variant["cost_eqps"] = cost_eqps;
			}
			bool flag2 = cost_itms != null && cost_itms._arr.Count > 0;
			if (flag2)
			{
				variant["cost_itms"] = cost_itms;
			}
			bool flag3 = rate_itms != null && rate_itms._arr.Count > 0;
			if (flag3)
			{
				variant["rate_itms"] = rate_itms;
			}
			base.sendRPC(103u, variant);
		}

		public void GetShowItem(uint cid, uint id)
		{
			Variant variant = new Variant();
			variant["itmid"] = id;
			variant["cid"] = cid;
			base.sendRPC(104u, variant);
		}
	}
}
