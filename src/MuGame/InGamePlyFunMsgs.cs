using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGamePlyFunMsgs : MsgProcduresBase
	{
		private Variant tp = new Variant();

		public InGamePlyFunMsgs(IClientBase m) : base(m)
		{
		}

		public static InGamePlyFunMsgs create(IClientBase m)
		{
			return new InGamePlyFunMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(255u, new NetManager.RPCProcCreator(PlyFunMsg.create));
		}

		public void UpgradeNobility()
		{
			this.tp["tp"] = 10;
			base.sendRPC(255u, this.tp);
		}

		public void getMergeInfo()
		{
			this.tp["tp"] = 11;
			base.sendRPC(255u, this.tp);
		}

		public void GetChop()
		{
			this.tp["tp"] = 13;
			base.sendRPC(255u, this.tp);
		}

		public void GetNobilityPrize()
		{
			this.tp["tp"] = 14;
			base.sendRPC(255u, this.tp);
		}

		public void addPlayerPoint(uint strpt = 0u, uint conpt = 0u, uint intept = 0u, uint agipt = 0u, uint wispt = 0u)
		{
			Variant variant = new Variant();
			bool flag = strpt > 0u;
			if (flag)
			{
				variant["strpt"] = strpt;
			}
			bool flag2 = conpt > 0u;
			if (flag2)
			{
				variant["conpt"] = conpt;
			}
			bool flag3 = intept > 0u;
			if (flag3)
			{
				variant["intept"] = intept;
			}
			bool flag4 = agipt > 0u;
			if (flag4)
			{
				variant["agipt"] = agipt;
			}
			bool flag5 = wispt > 0u;
			if (flag5)
			{
				variant["wispt"] = wispt;
			}
			this.tp["tp"] = 3;
			this.tp["attadd"] = variant;
			base.sendRPC(255u, this.tp);
		}

		public void resetPlayerPoint()
		{
			this.tp["tp"] = 4;
			base.sendRPC(255u, this.tp);
		}

		public void transfer()
		{
			this.tp["tp"] = 2;
			base.sendRPC(255u, this.tp);
		}

		public void RequestLoginData()
		{
			this.tp["tp"] = 9;
			this.tp["subtp"] = 1;
			base.sendRPC(255u, this.tp);
		}

		public void RequestSafeData()
		{
			this.tp["tp"] = 9;
			this.tp["subtp"] = 2;
			base.sendRPC(255u, this.tp);
		}

		public void UnLockedPassword(string scode)
		{
			this.tp["tp"] = 9;
			this.tp["subtp"] = 3;
			this.tp["scode"] = scode;
			base.sendRPC(255u, this.tp);
		}

		public void BindTelephoneNum(string phone, string tcode, string scode)
		{
			this.tp["tp"] = 9;
			this.tp["subtp"] = 4;
			this.tp["phone"] = phone;
			this.tp["tcode"] = tcode;
			this.tp["scode"] = scode;
			base.sendRPC(255u, this.tp);
		}

		public void ResetSafeNum(string tcode, string newcode)
		{
			this.tp["tp"] = 9;
			this.tp["subtp"] = 6;
			this.tp["tcode"] = tcode;
			this.tp["newcode"] = newcode;
			base.sendRPC(255u, this.tp);
		}

		public void ResetTelephoneNum(string tcode, string newph)
		{
			this.tp["tp"] = 9;
			this.tp["subtp"] = 5;
			this.tp["tcode"] = tcode;
			this.tp["newph"] = newph;
			base.sendRPC(255u, this.tp);
		}

		public void SetCommonIp(string scode, bool isvip)
		{
			this.tp["tp"] = 9;
			this.tp["subtp"] = 7;
			this.tp["scode"] = scode;
			this.tp["isvip"] = isvip;
			base.sendRPC(255u, this.tp);
		}

		public void RequestSafeAward()
		{
			this.tp["tp"] = 9;
			this.tp["subtp"] = 8;
			base.sendRPC(255u, this.tp);
		}

		public void ResetLvl()
		{
			this.tp["tp"] = 5;
			base.sendRPC(255u, this.tp);
		}

		public void rollReset(int lvl, int cost)
		{
			this.tp["tp"] = 12;
			this.tp["resetlvl"] = lvl;
			this.tp["cost"] = cost;
			base.sendRPC(255u, this.tp);
		}

		public void GetPvipPowerAwd(uint awdid)
		{
			this.tp["tp"] = 4;
			this.tp["id"] = awdid;
			base.sendRPC(255u, this.tp);
		}

		public void UpgradeRide(int id, int cnt, bool exp = false)
		{
			Variant variant = new Variant();
			variant["tp"] = 15;
			bool flag = !exp;
			if (flag)
			{
				Variant variant2 = new Variant();
				variant2["id"] = id;
				variant2["cnt"] = cnt;
				variant["use"] = variant2;
			}
			else
			{
				variant["use"] = new Variant();
				variant["exp"] = exp;
			}
			base.sendRPC(255u, variant);
		}

		public void UseRide(bool ride)
		{
			this.tp["tp"] = 16;
			this.tp["ride"] = ride;
			base.sendRPC(255u, this.tp);
		}

		public void ChangePlayerName(string name)
		{
			this.tp["tp"] = 17;
			this.tp["name"] = name;
			base.sendRPC(255u, this.tp);
		}

		public void GetMonthInvestInfo()
		{
			this.tp["tp"] = 13;
			this.tp["subtp"] = 1;
			base.sendRPC(255u, this.tp);
		}

		public void SendMonthInvestMsg()
		{
			this.tp["tp"] = 13;
			this.tp["subtp"] = 2;
			base.sendRPC(255u, this.tp);
		}

		public void GetMonthInvestAward()
		{
			this.tp["tp"] = 13;
			this.tp["subtp"] = 3;
			this.tp["retcnt"] = "";
			base.sendRPC(255u, this.tp);
		}

		public void GetAcctAward()
		{
			this.tp["tp"] = 13;
			this.tp["subtp"] = 3;
			this.tp["addret"] = "";
			base.sendRPC(255u, this.tp);
		}

		public void GetUplvlInvestInfo()
		{
			this.tp["tp"] = 13;
			this.tp["subtp"] = 4;
			base.sendRPC(255u, this.tp);
		}

		public void SendUplvlInvestMsg(string costyb)
		{
			this.tp["tp"] = 13;
			this.tp["subtp"] = 5;
			this.tp["costyb"] = costyb;
			base.sendRPC(255u, this.tp);
		}

		public void GetUplvlInvestAward(uint level)
		{
			this.tp["tp"] = 13;
			this.tp["subtp"] = 6;
			this.tp["level"] = level;
			base.sendRPC(255u, this.tp);
		}

		public void GetInvestLogs(int log_tp)
		{
			this.tp["tp"] = 13;
			this.tp["subtp"] = 7;
			this.tp["log_tp"] = log_tp;
			base.sendRPC(255u, this.tp);
		}

		public void GetOnlineAwardsInfo()
		{
			this.tp["tp"] = 18;
			this.tp["subtp"] = 1;
			base.sendRPC(255u, this.tp);
		}

		public void GetOnlineAwards(int awdId)
		{
			this.tp["tp"] = 18;
			this.tp["subtp"] = 2;
			this.tp["id"] = awdId;
			base.sendRPC(255u, this.tp);
		}

		public void GetItemPkgs(uint tpid, uint cnt, Variant pkgs)
		{
			this.tp["tp"] = 19;
			this.tp["tpid"] = tpid;
			this.tp["cnt"] = cnt;
			this.tp["pkgs"] = pkgs;
			base.sendRPC(255u, this.tp);
		}

		public void ActiveRideQaulAtt(uint qual, uint lvl)
		{
			this.tp["tp"] = 20;
			this.tp["qual"] = qual;
			this.tp["lvl"] = lvl;
			base.sendRPC(255u, this.tp);
		}

		public void SelectRideSkill(uint sid)
		{
			this.tp["tp"] = 21;
			this.tp["sid"] = sid;
			base.sendRPC(255u, this.tp);
		}

		public void SendRedPaper(int total_yb, int red_paper_cnt, string msg = "")
		{
			this.tp["tp"] = 22;
			this.tp["total_yb"] = total_yb;
			this.tp["red_paper_cnt"] = red_paper_cnt;
			this.tp["msg"] = msg;
			base.sendRPC(255u, this.tp);
		}

		public void GrabRedPaper(uint cid, int red_paper_key)
		{
			this.tp["tp"] = 23;
			this.tp["snd_ply_cid"] = cid;
			this.tp["red_paper_key"] = red_paper_key;
			base.sendRPC(255u, this.tp);
		}

		public void GetRedPaperBack()
		{
			this.tp["tp"] = 24;
			base.sendRPC(255u, this.tp);
		}
	}
}
