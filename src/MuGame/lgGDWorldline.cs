using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class lgGDWorldline : lgGDBase
	{
		private string t_rmis_nchgLine;

		private uint VIP_LINE = 0u;

		private Action _changeLineCallback = null;

		private Variant _line_data;

		private bool _is_linedata_invalid = true;

		private const int LOADING = 1;

		private long _req_line_sttm = 0L;

		private int _last_req_tm = 0;

		private const int LOST_TM = 300000;

		private int _cur_line = 0;

		private InGameChangeLineMsgs _changelineMsg
		{
			get
			{
				return (this.g_mgr.g_netM as muNetCleint).igChangeLineMsgs;
			}
		}

		public int curline
		{
			get
			{
				return this._cur_line;
			}
			set
			{
				this._cur_line = value;
			}
		}

		private LGIUIWorldline worldline
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUIWorldlineImpl") as LGIUIWorldline;
			}
		}

		private LGIUIMainUI mainUI
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			}
		}

		public lgGDWorldline(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDWorldline(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(4u, new Action<GameEvent>(this.onLineChangeRes));
			this.g_mgr.g_netM.addEventListener(49u, new Action<GameEvent>(this.onLineInfoRes));
		}

		private void onLineChangeRes(GameEvent e)
		{
			this.line_change(e.data);
		}

		private void onLineInfoRes(GameEvent e)
		{
			this.line_info_res(e.data);
		}

		public void onProcess(float tmSlice)
		{
			this._linedata_invalid();
		}

		public void set_line(uint l, Action callBack = null)
		{
			bool flag = (this.g_mgr as muLGClient).g_missionCT.isPlayerAcceptedRmis(1u);
			if (flag)
			{
				bool flag2 = this.t_rmis_nchgLine == null;
				if (flag2)
				{
					this.t_rmis_nchgLine = LanguagePack.getLanguageText("change_line", "rmis_n_chgline");
				}
				this.mainUI.systemmsg(this.t_rmis_nchgLine, 4u);
			}
			else
			{
				bool flag3 = this.VIP_LINE > 0u && this.VIP_LINE == l;
				if (flag3)
				{
				}
				bool flag4 = (long)this._cur_line != (long)((ulong)l);
				if (flag4)
				{
					this._changeLineCallback = callBack;
					this._changelineMsg.select_line(l);
				}
			}
		}

		public Variant GetLineData()
		{
			bool flag = this._is_linedata_invalid && (this._line_data == null || this._line_data._int != 1);
			if (flag)
			{
				this.requestLineData();
			}
			return this._line_data;
		}

		public void UpdateLineData()
		{
			this._is_linedata_invalid = true;
			this.GetLineData();
		}

		private void _linedata_invalid()
		{
			long timer = GameTools.getTimer();
			bool flag = timer - this._req_line_sttm > 300000L;
			if (flag)
			{
				this._is_linedata_invalid = true;
				this._req_line_sttm = GameTools.getTimer();
			}
			TZDate tZDate = new TZDate((double)timer);
			TZDate tZDate2 = new TZDate((double)this._last_req_tm);
			bool flag2 = tZDate.date != tZDate2.date;
			if (flag2)
			{
				this._is_linedata_invalid = true;
				this.GetLineData();
			}
		}

		public void line_change(Variant data)
		{
			this._cur_line = data["line"];
			this.worldline.change_line_success_res(data);
		}

		public void line_info_res(Variant data)
		{
			bool flag = this._cur_line > data["nor_line"] - 1;
			if (flag)
			{
			}
			this._line_data = data;
			this._is_linedata_invalid = false;
			this.worldline.line_info_res(data);
		}

		private void requestLineData()
		{
			bool flag = this._line_data == null || this._line_data._int != 1;
			if (flag)
			{
				this._line_data = 1;
				this._changelineMsg.requestLineData();
			}
		}
	}
}
