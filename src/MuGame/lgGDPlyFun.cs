using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDPlyFun : lgGDBase
	{
		private LGIUIPlyFun _uiPlyFun;

		private LGIUIRideSkill _ui_rideskill;

		protected Variant _logindata;

		protected Variant _safedata;

		private string _newCode;

		private bool _autoUpgrade = false;

		private LGIUIMainUI _ui_main;

		protected Variant _monthInvestInfo = null;

		protected int _month_yb = 0;

		protected Variant _uplvlInvestInfo = null;

		protected int _uplvl_invest_yb = 0;

		private Action<Variant> _pkgBackFun;

		protected InGamePlyFunMsgs _plyfunMsg
		{
			get
			{
				return (this.g_mgr.g_netM as muNetCleint).igPlyFunMsgs;
			}
		}

		private LGIUIPlyFun uiPlyFun
		{
			get
			{
				bool flag = this._uiPlyFun == null;
				if (flag)
				{
					this._uiPlyFun = (this.g_mgr.g_uiM.getLGUI("UI_PLYFUN") as LGIUIPlyFun);
				}
				return this._uiPlyFun;
			}
		}

		private LGIUIRideSkill uiRideSkill
		{
			get
			{
				bool flag = this._ui_rideskill == null;
				if (flag)
				{
					this._ui_rideskill = (this.g_mgr.g_uiM.getLGUI("UI_RIDESKILL") as LGIUIRideSkill);
				}
				return this._ui_rideskill;
			}
		}

		private LGIUIMainUI _lgui_main
		{
			get
			{
				bool flag = this._ui_main == null;
				if (flag)
				{
					this._ui_main = (this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI);
				}
				return this._ui_main;
			}
		}

		public lgGDPlyFun(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDPlyFun(m as gameManager);
		}

		public override void init()
		{
		}

		public void resetPlayerPoint()
		{
			this._plyfunMsg.resetPlayerPoint();
		}

		public void UpgradeNoblv()
		{
			this._plyfunMsg.UpgradeNobility();
		}

		public void OnUpgradeNobRes(Variant data)
		{
			lgGDGeneral g_generalCT = (this.g_mgr.g_gameM as muLGClient).g_generalCT;
			g_generalCT.noblv = data["noblv"];
			g_generalCT.nobpt = data["nobpt"];
			(this.g_mgr.g_gameM as muLGClient).g_selfPlayer.updateNetData(data);
			(this.g_mgr.g_gameM as muLGClient).g_missionCT.player_change();
			LGIUINobility lGIUINobility = this.g_mgr.g_uiM.getLGUI("UI_NOBILITY") as LGIUINobility;
			bool flag = lGIUINobility != null;
			if (flag)
			{
				lGIUINobility.UpdateNobInfo(data);
			}
		}

		public void transfer()
		{
			this._plyfunMsg.transfer();
		}

		public void show_trans_icon()
		{
			bool flag = this.CanShowIcon();
			if (flag)
			{
				LGIUITransfer lGIUITransfer = this.g_mgr.g_uiM.getLGUI("mdlg_transfer") as LGIUITransfer;
				lGIUITransfer.show_trans_icon(true);
			}
		}

		public bool CanShowIcon()
		{
			lgSelfPlayer g_selfPlayer = (this.g_mgr.g_gameM as muLGClient).g_selfPlayer;
			int carr = g_selfPlayer.data["carr"];
			int num = g_selfPlayer.data["carrlvl"];
			bool flag = num == 3;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = num == 0;
				if (flag2)
				{
					Variant carrlvl = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.GetCarrlvl(carr);
					bool flag3 = carrlvl != null;
					if (flag3)
					{
						foreach (Variant current in carrlvl["carrlvl"]._arr)
						{
							bool flag4 = num + 1 == current["lvl"];
							if (flag4)
							{
								foreach (Variant current2 in current["attchk"]._arr)
								{
									bool flag5 = "level" == current2["name"];
									if (flag5)
									{
										int num2 = g_selfPlayer.data["level"];
										bool flag6 = num2 < current2["min"]._int;
										if (flag6)
										{
											result = false;
											return result;
										}
									}
									else
									{
										bool flag7 = "carrlvl" == current2["name"]._str;
										if (flag7)
										{
											bool flag8 = num != current2["equal"]._int;
											if (flag8)
											{
												result = false;
												return result;
											}
										}
									}
								}
								result = true;
								return result;
							}
						}
					}
				}
				result = true;
			}
			return result;
		}

		public void AutoTranfer()
		{
			bool flag = this.is_can_trans_job();
			if (flag)
			{
				this.transfer();
			}
		}

		public bool is_can_trans_job()
		{
			lgSelfPlayer g_selfPlayer = (this.g_mgr.g_gameM as muLGClient).g_selfPlayer;
			int carr = g_selfPlayer.data["carr"];
			int num = g_selfPlayer.data["carrlvl"];
			Variant carrlvl = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.GetCarrlvl(carr);
			bool flag = carrlvl != null;
			bool result;
			if (flag)
			{
				foreach (Variant current in carrlvl["carrlvl"]._arr)
				{
					bool flag2 = num + 1 == current["lvl"]._int;
					if (flag2)
					{
						foreach (Variant current2 in current["attchk"]._arr)
						{
							bool flag3 = "level" == current2["name"]._str;
							if (flag3)
							{
								int num2 = g_selfPlayer.data["level"];
								bool flag4 = num2 < current2["min"]._int;
								if (flag4)
								{
									result = false;
									return result;
								}
							}
							else
							{
								bool flag5 = "carrlvl" == current2["name"]._str;
								if (flag5)
								{
									bool flag6 = num != current2["equal"]._int;
									if (flag6)
									{
										result = false;
										return result;
									}
								}
								else
								{
									bool flag7 = "resetlvl" == current2["name"]._str;
									if (flag7)
									{
										int num3 = g_selfPlayer.data["resetlvl"];
										bool flag8 = num3 == current2["equal"]._int;
										if (flag8)
										{
											result = false;
											return result;
										}
									}
									else
									{
										bool flag9 = "finmis" == current2["name"]._str;
										if (flag9)
										{
											Variant missions = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_missions();
											bool flag10 = missions != null;
											if (flag10)
											{
												uint num4 = current2["equal"];
												Variant variant = missions[num4];
												bool flag11 = variant != null;
												if (flag11)
												{
													bool flag12 = (this.g_mgr.g_gameM as muLGClient).g_missionCT.is_mis_has_complete((int)num4);
													bool flag13 = !flag12;
													if (flag13)
													{
														result = false;
														return result;
													}
												}
											}
										}
									}
								}
							}
						}
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public Variant GetLoginInfo()
		{
			bool flag = this._logindata == null;
			if (flag)
			{
				this._plyfunMsg.RequestLoginData();
			}
			return this._logindata;
		}

		public Variant GetSafeData()
		{
			return this._safedata;
		}

		public bool IsOpenPasswordProtect()
		{
			return false;
		}

		public bool IsSafe()
		{
			return this._safedata != null;
		}

		public bool IsCommonIp(string cmp_ip)
		{
			bool flag = this._safedata != null && this._safedata.ContainsKey("safeips");
			bool result;
			if (flag)
			{
				Variant variant = GameTools.split(this._safedata["safeips"]._str, ",", 1u);
				bool flag2 = variant.Length > 0;
				if (flag2)
				{
					bool flag3 = (this.g_mgr.g_gameM as muLGClient).g_vipCT.isVip();
					if (flag3)
					{
						using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator = variant.Values.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string b = enumerator.Current;
								bool flag4 = cmp_ip == b;
								if (flag4)
								{
									result = true;
									return result;
								}
							}
						}
					}
					else
					{
						bool flag5 = cmp_ip == variant[0];
						if (flag5)
						{
							result = true;
							return result;
						}
					}
				}
			}
			result = false;
			return result;
		}

		public void PasswordSafeDataRes(Variant data)
		{
			switch (data["subtp"]._int)
			{
			case 1:
				this.loginDataRes(data);
				break;
			case 2:
				this.safeDataRes(data);
				break;
			case 3:
				this.unLockedPasswordRes(data);
				break;
			case 5:
				this.resetTelephoneNumRes(data);
				break;
			case 6:
				this.resetSafeNumRes(data);
				break;
			case 7:
				this.setCommonIpRes(data);
				break;
			case 8:
				this.requestSafeAwardRes(data);
				break;
			}
		}

		protected void loginDataRes(Variant data)
		{
		}

		protected void safeDataRes(Variant data)
		{
		}

		protected void unLockedPasswordRes(Variant data)
		{
		}

		protected void resetTelephoneNumRes(Variant data)
		{
			this._safedata["safeph"] = data["safeph"];
		}

		protected void resetSafeNumRes(Variant data)
		{
		}

		protected void setCommonIpRes(Variant data)
		{
		}

		protected void requestSafeAwardRes(Variant data)
		{
			this._safedata["gotawd"] = data["gotawd"];
		}

		public void RequestLoginData()
		{
			this._plyfunMsg.RequestLoginData();
		}

		public void RequestSafeData()
		{
			this._plyfunMsg.RequestSafeData();
		}

		public void UnLockedPassword(string scode)
		{
			this._plyfunMsg.UnLockedPassword(scode);
		}

		public void BindTelephoneNum(string scode, string phone = "", string tcode = "")
		{
			this._plyfunMsg.BindTelephoneNum(phone, tcode, scode);
		}

		public void ResetTelephoneNum(string tcode, string newph)
		{
			this._plyfunMsg.ResetTelephoneNum(tcode, newph);
		}

		public void ResetSafeNum(string tcode, string newcode = "")
		{
			this._newCode = newcode;
			this._plyfunMsg.ResetSafeNum(tcode, newcode);
		}

		public void SetCommonIp(string scode, bool isvip)
		{
			this._plyfunMsg.SetCommonIp(scode, isvip);
		}

		public void RequestSafeAward()
		{
			this._plyfunMsg.RequestSafeAward();
		}

		public void OnResetlvl(Variant data)
		{
			bool flag = data.ContainsKey("succ");
			if (flag)
			{
				Variant variant = new Variant();
				Variant variant2 = new Variant();
				variant2["exp"] = data["succ"]["exp"];
				variant["level"] = data["succ"]["level"];
				variant["pinfo"] = variant2;
				(this.g_mgr.g_gameM as muLGClient).g_selfPlayer.on_lvl_up(variant);
				(this.g_mgr.g_gameM as muLGClient).g_selfPlayer.data["resetlvl"] = data["succ"]["resetlvl"];
				(this.g_mgr.g_gameM as muLGClient).g_selfPlayer.SetResetName();
				(this.g_mgr.g_gameM as muLGClient).g_selfPlayer.modAttPt(data["succ"]["att_pt"]);
				(this.g_mgr.g_gameM as muLGClient).g_missionCT.acceptable_refault();
				LGIUISystemOpen lGIUISystemOpen = this.g_mgr.g_uiM.getLGUI("UI_SYSTEMOPEN") as LGIUISystemOpen;
				lGIUISystemOpen.OnResetlvl(data["succ"]["resetlvl"]);
				LGIUIShop lGIUIShop = this.g_mgr.g_uiM.getLGUI("shop") as LGIUIShop;
				lGIUIShop.OnResetlvl();
				LGIUITransfer lGIUITransfer = this.g_mgr.g_uiM.getLGUI("mdlg_transfer") as LGIUITransfer;
				lGIUITransfer.ReflushTransfer();
			}
			LGIUITranlive lGIUITranlive = this.g_mgr.g_uiM.getLGUI("mdlg_tranlive") as LGIUITranlive;
			lGIUITranlive.OnResetlvl(data);
		}

		public void RollptBack(Variant data)
		{
			Variant variant = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["roll_pt"];
			bool flag = false;
			int num = 0;
			foreach (Variant current in variant._arr)
			{
				bool flag2 = current["resetlvl"] == data["resetlvl"];
				if (flag2)
				{
					num = data["pt"]._int - current["pt"]._int;
					current["pt"] = data["pt"];
					flag = true;
					break;
				}
			}
			bool flag3 = !flag;
			if (flag3)
			{
				num = data["pt"];
				Variant variant2 = new Variant();
				variant2["resetlvl"] = data["resetlvl"];
				variant2["pt"] = data["pt"];
				variant._arr.Add(variant2);
			}
			num += (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["att_pt"];
			(this.g_mgr.g_gameM as muLGClient).g_selfPlayer.modAttPt(num);
			LGIUITranlive lGIUITranlive = this.g_mgr.g_uiM.getLGUI("mdlg_tranlive") as LGIUITranlive;
			lGIUITranlive.RollptBack(data);
		}

		public void ResetLvl()
		{
			this._plyfunMsg.ResetLvl();
		}

		public void rollReset(int lvl, int cost)
		{
			this._plyfunMsg.rollReset(lvl, cost);
		}

		public void UpgradeRideRes(Variant data)
		{
			Variant variant = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["mount"];
			bool flag = variant == null;
			if (!flag)
			{
				foreach (string current in data.Keys)
				{
					variant[current] = data[current];
				}
			}
			bool flag2 = data.ContainsKey("lvl");
			if (flag2)
			{
				this._autoUpgrade = false;
			}
			bool flag3 = data.ContainsKey("qual");
			if (flag3)
			{
			}
		}

		public void RideChange(Variant data)
		{
			bool flag = data.ContainsKey("cid");
			LGAvatar lGAvatar;
			if (flag)
			{
				lGAvatar = (this.g_mgr.g_gameM as muLGClient).g_mapCT.get_player_by_cid(data["cid"]);
			}
			else
			{
				lGAvatar = (this.g_mgr.g_gameM as muLGClient).g_selfPlayer;
			}
			bool flag2 = lGAvatar != null && lGAvatar.data.ContainsKey("mount");
			if (flag2)
			{
				lGAvatar.UpRide(data["ride"]);
				lGAvatar.data["mount"]["ride"] = data["ride"];
			}
		}

		public void SetAutoUp(bool flag)
		{
			this._autoUpgrade = flag;
		}

		public bool GetAutoUp()
		{
			return this._autoUpgrade;
		}

		public void UpgradeRide(int id, int cnt, bool exp = false)
		{
			this._plyfunMsg.UpgradeRide(id, cnt, exp);
		}

		public void UseRide(bool ride)
		{
			bool flag = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["mount"]["ride"];
			bool flag2 = flag != ride;
			if (flag2)
			{
				this._plyfunMsg.UseRide(ride);
			}
		}

		public void RideQualAttActiveRes(Variant msgData)
		{
			bool flag = false;
			Variant variant = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["mount_quals"];
			foreach (Variant current in variant._arr)
			{
				bool flag2 = current["qual"] == msgData["qual"];
				if (flag2)
				{
					flag = true;
					current["lvl"] = msgData["lvl"];
					break;
				}
			}
			bool flag3 = !flag;
			if (flag3)
			{
				Variant variant2 = new Variant();
				variant2["qual"] = msgData["qual"];
				variant2["lvl"] = msgData["lvl"];
				variant._arr.Add(variant2);
			}
		}

		public void RideQualAttActive(uint qual, uint attlvl)
		{
			this._plyfunMsg.ActiveRideQaulAtt(qual, attlvl);
		}

		public void SelectRideSkillRes(Variant msgData)
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			mainPlayerInfo["mount_skil_act"] = msgData["sid"];
			Variant variant = new Variant();
			variant["skid"] = msgData["sid"];
		}

		public void ChangePlayerName(string name)
		{
			this._plyfunMsg.ChangePlayerName(name);
		}

		public void UpdatePlayerName(Variant data)
		{
		}

		public void OnInvestBack(Variant msg)
		{
		}

		public bool IsMonthInvesting()
		{
			return this._month_yb > 0;
		}

		public bool IsUplvlInvesting()
		{
			return this._uplvl_invest_yb > 0;
		}

		public void GetMonthInvestInfo()
		{
			this._plyfunMsg.GetMonthInvestInfo();
		}

		public void SendMonthInvestMsg()
		{
			this._plyfunMsg.SendMonthInvestMsg();
		}

		public void GetMonthAward()
		{
			this._plyfunMsg.GetMonthInvestAward();
		}

		public void GetAcctAward()
		{
			this._plyfunMsg.GetAcctAward();
		}

		public void GetUplvlInvestInfo()
		{
			this._plyfunMsg.GetUplvlInvestInfo();
		}

		public void SendUplvlInvestMsg(string costyb)
		{
			this._plyfunMsg.SendUplvlInvestMsg(costyb);
		}

		public void GetUplvlInvestAward(uint level)
		{
			this._plyfunMsg.GetUplvlInvestAward(level);
		}

		public void GetInvestLogs(int log_tp)
		{
			this._plyfunMsg.GetInvestLogs(log_tp);
		}

		public void GetItemPkgs(Variant data, paramStruct obj)
		{
			bool flag = obj.fun != null;
			if (flag)
			{
				this._pkgBackFun = obj.fun;
			}
			this._plyfunMsg.GetItemPkgs(data["tpid"], data["cnt"], data["pkgs"]);
		}

		public void PkgsItemBack(Variant e)
		{
			bool flag = e && e["itm"];
			if (flag)
			{
				(this.g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_add_items(e["itm"], 0);
				bool flag2 = this._pkgBackFun != null;
				if (flag2)
				{
					this._pkgBackFun(e["itm"]);
					this._pkgBackFun = null;
				}
				Variant variant = e["itm"]["pkgs"];
				foreach (Variant current in variant._arr)
				{
					bool flag3 = current["name"]._str == "gold";
					if (flag3)
					{
						(this.g_mgr.g_gameM as muLGClient).g_generalCT.sub_gold(current["val"] * e["itm"]["cnt"]);
						(this.g_mgr.g_gameM as muLGClient).g_itemsCT.set_gold((uint)(this.g_mgr.g_gameM as muLGClient).g_generalCT.gold);
					}
				}
			}
		}

		public void SendRedPaper(Variant data)
		{
			string msg = "";
			bool flag = data && data["msg"];
			if (flag)
			{
				msg = data["msg"];
			}
			this._plyfunMsg.SendRedPaper(data["yb"], data["cnt"], msg);
		}

		public void GrabRedPaper(Variant data)
		{
			this._plyfunMsg.GrabRedPaper(data["cid"], data["red_paper_key"]);
		}

		public void GetRedPaperBack()
		{
			this._plyfunMsg.GetRedPaperBack();
		}
	}
}
