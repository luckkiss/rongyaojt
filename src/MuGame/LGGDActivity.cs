using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGGDActivity : lgGDBase
	{
		private float _firstracttmt = -1f;

		private float _combracttm = -1f;

		private Variant _awdacts = new Variant();

		private Variant _rankData = new Variant();

		private Variant _ybractData = new Variant();

		private Variant _clanracts = new Variant();

		private Variant _festivalData = new Variant();

		private Variant _tcybCostLottData;

		private Variant _tcybLottData;

		private Variant _fetchRedPaperData;

		private LGIUILoginAward loginAward
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("mdlg_loginaward") as LGIUILoginAward;
			}
		}

		private LGIUILuckyDraw ui_luckydraw
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUILuckyDraw") as LGIUILuckyDraw;
			}
		}

		private LGIUIHfActivity ui_hfActivity
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUIHfActivity") as LGIUIHfActivity;
			}
		}

		public float firstracttmt
		{
			get
			{
				return this._firstracttmt;
			}
		}

		public float combracttm
		{
			get
			{
				return this._combracttm;
			}
		}

		public Variant tcybCostLottTm
		{
			get
			{
				return this._tcybCostLottData;
			}
		}

		public Variant tcybLottTm
		{
			get
			{
				return this._tcybLottData;
			}
		}

		public Variant fetchRedPaperTm
		{
			get
			{
				return this._fetchRedPaperData;
			}
		}

		private muNetCleint muNClt
		{
			get
			{
				return this.g_mgr.g_netM as muNetCleint;
			}
		}

		public LGGDActivity(gameManager m) : base(m)
		{
		}

		public static LGGDActivity create(IClientBase m)
		{
			return new LGGDActivity(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(45u, new Action<GameEvent>(this.onGetRactRes));
		}

		private void onGetRactRes(GameEvent e)
		{
			Variant data = e.data;
			this.get_ract_res(data);
		}

		public void get_ract_res(Variant data)
		{
		}

		private void set_ract_data(Variant data)
		{
		}

		public Variant GetRankractDataById(int ractid)
		{
			return this._rankData[ractid];
		}

		public Variant GetYbractDataById(int ractid)
		{
			return this._ybractData[ractid];
		}

		public Variant GetYbractData()
		{
			return this._ybractData;
		}

		public Variant GetAwdacts()
		{
			return this._awdacts;
		}

		public Variant get_act_awds_arr(uint ractid)
		{
			Variant result = new Variant();
			this._flush_normal_act_time(ractid);
			foreach (Variant current in this._awdacts["awdacts"]._arr)
			{
				bool flag = ractid == current["id"];
				if (flag)
				{
					result = current["tar"];
				}
			}
			return result;
		}

		public void UpdateFirstActTm(Variant data)
		{
		}

		public void UpdateCombActTm(Variant data)
		{
		}

		public void UpdateTcybCostLottTm(Variant data)
		{
			double val = data["tcyb_cost_lott_sttm"];
			double val2 = data["tcyb_cost_lott_edtm"];
			Variant variant = new Variant();
			variant["sttm"] = val;
			variant["edtm"] = val2;
			this._tcybCostLottData = variant;
		}

		public void UpdateTcybLottTm(Variant data)
		{
			double val = data["tcyb_lott_sttm"];
			double val2 = data["tcyb_lott_edtm"];
			Variant variant = new Variant();
			variant["sttm"] = val;
			variant["edtm"] = val2;
			this._tcybLottData = variant;
		}

		public void UpdateFetchRedPaperTm(Variant data)
		{
			double val = data["fetch_red_paper_sttm"];
			double val2 = data["fetch_red_paper_edtm"];
			Variant variant = new Variant();
			variant["sttm"] = val;
			variant["edtm"] = val2;
			this._fetchRedPaperData = variant;
		}

		public Variant GetFestivalByid(uint id)
		{
			bool flag = this._festivalData;
			Variant result;
			if (flag)
			{
				bool flag2 = this._festivalData[id];
				if (flag2)
				{
					result = this._festivalData[id];
					return result;
				}
			}
			result = null;
			return result;
		}

		public int GetFestivalLeast(uint ractid, uint id)
		{
			bool flag = this._festivalData[ractid];
			int result;
			if (flag)
			{
				Variant variant = this._festivalData[ractid];
				bool flag2 = variant["tar"];
				if (flag2)
				{
					bool flag3 = variant["tar"]["id"];
					if (flag3)
					{
						result = variant["tar"][id].Count;
						return result;
					}
				}
			}
			result = 0;
			return result;
		}

		public int GetCurFestivalid(int value)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			Variant variant3 = new Variant();
			bool flag = variant3 != null;
			int result;
			if (flag)
			{
				foreach (Variant current in variant2._arr)
				{
					bool flag2 = variant3._arr.IndexOf(current["id"]) == -1;
					if (!flag2)
					{
						result = current["id"];
						return result;
					}
				}
			}
			result = -1;
			return result;
		}

		public Variant GetRankInfo(int ractid)
		{
			Variant variant = this._rankData[ractid];
			bool flag = variant == null || (!variant["act_over"] && this.isInfoTimeOut(this._rankData[ractid]["expire_tm"]));
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._rankData[ractid];
			}
			return result;
		}

		public Variant GetAllRank()
		{
			return this._rankData;
		}

		private bool isInfoTimeOut(double tm)
		{
			double num = 0.0;
			return num >= tm * 1000.0;
		}

		public void get_firstracttm()
		{
			(this.g_mgr.g_netM as muNetCleint).igActivityMsgs.get_firstracttm();
		}

		public void _flush_normal_act_time(uint ractid)
		{
			this.muNClt.igActivityMsgs.flush_normal_act(ractid);
		}
	}
}
