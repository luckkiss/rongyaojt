using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGGDRmission : lgGDBase
	{
		private Variant _misSkillDescs = new Variant();

		private Variant _rmisDatas = new Variant();

		protected Variant _playerRmiss = new Variant();

		private Variant _appawdRmis;

		private Variant _misSkillCD = new Variant();

		private Variant _rmis_share = new Variant();

		private Variant _cdMaskBitmapData = new Variant();

		private int _freeInsure = 0;

		private bool _reSetPlaymis;

		private bool _reSetShare;

		public LGGDRmission(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGDRmission(m as gameManager);
		}

		public override void init()
		{
		}

		public void onRmisInfoRes(GameEvent e)
		{
			Variant data = e.data;
			this.rmis_res(data);
		}

		public void InitRmisData()
		{
			Variant rmiss = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_rmiss();
			bool flag = rmiss != null;
			if (flag)
			{
				for (int i = 0; i < rmiss.Count; i++)
				{
					Variant variant = rmiss[i];
					this.addDataToRmisData(variant["id"]._str, variant);
				}
			}
		}

		public void InitPlayerRmisData()
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			this._reSetPlaymis = true;
			this._reSetShare = true;
			foreach (string current in this._rmisDatas.Keys)
			{
				Variant variant = this._rmisDatas[current];
				this.requestRmisInfo(variant["id"]._int);
			}
			Variant playerAcceptedRmis = this.GetPlayerAcceptedRmis();
			for (int i = 0; i < playerAcceptedRmis.Count; i++)
			{
				this.requestRmisInfo(playerAcceptedRmis[i]._int);
			}
			this.GetAppawd();
			(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetRmisShareInfo();
		}

		public void PlayerInfoChanged()
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			foreach (string current in this._rmisDatas.Keys)
			{
				Variant variant = this._rmisDatas[current];
				int num = variant["id"];
				bool flag = this._playerRmiss[num.ToString()] == null;
				if (flag)
				{
				}
			}
		}

		public static bool IsArrayHasValue(Variant data, int value)
		{
			bool flag = data.Count <= 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (Variant current in data._arr)
				{
					bool flag2 = value == current._int;
					if (flag2)
					{
						result = true;
						return result;
					}
				}
				result = false;
			}
			return result;
		}

		public Variant GetPlayerAcceptedRmis()
		{
			Variant variant = new Variant();
			Variant misacept = (this.g_mgr as muLGClient).g_missionCT.misacept;
			bool flag = misacept.Count > 0;
			if (flag)
			{
				foreach (Variant current in misacept.Values)
				{
					Variant variant2 = current["configdata"];
					bool flag2 = variant2 == null;
					if (!flag2)
					{
						bool flag3 = variant2.ContainsKey("rmis");
						if (flag3)
						{
							variant.pushBack(variant2["rmis"]);
						}
					}
				}
			}
			return variant;
		}

		public Variant GetRmisActivities()
		{
			Variant variant = new Variant();
			foreach (Variant current in this._rmisDatas._arr)
			{
				bool flag = current.ContainsKey("part_type") && (current["part_type"] & 8) > 0;
				if (flag)
				{
					variant.pushBack(current);
				}
			}
			return variant;
		}

		public bool IsRmisCanAccept(int rmisId)
		{
			bool result = false;
			Variant variant = this._playerRmiss[rmisId.ToString()];
			bool flag = variant != null;
			if (flag)
			{
				Variant variant2 = this._rmisDatas[rmisId.ToString()];
				int num = 0;
				bool flag2 = variant2.ContainsKey("rmis_share");
				if (flag2)
				{
					Variant variant3 = this._rmis_share[variant2["rmis_share"]];
					bool flag3 = variant3 != null;
					if (flag3)
					{
						num = variant3["cnt"];
					}
					else
					{
						Variant rmisShare = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.GetRmisShare(variant2["rmis_share"]);
						bool flag4 = rmisShare != null;
						if (flag4)
						{
							num = rmisShare["dalycnt"];
						}
					}
				}
				else
				{
					num = variant["cnt"];
				}
				bool flag5 = num > 0;
				if (flag5)
				{
					Variant variant4 = this._rmisDatas[rmisId.ToString()];
					Variant variant5 = variant4["clan"];
					result = (variant5 == null);
				}
			}
			return result;
		}

		public int GetLeftFreeInsure()
		{
			return this._freeInsure;
		}

		public int GetPlayerRmisCurrQualMis(int rmisId)
		{
			int result = 0;
			Variant variant = this._playerRmiss[rmisId.ToString()];
			bool flag = variant != null;
			if (flag)
			{
				result = variant["misids"][0];
			}
			return result;
		}

		public Variant GetRmisQualIcon(int rmisid)
		{
			Variant variant = null;
			Variant variant2 = this._rmisDatas[rmisid.ToString()];
			bool flag = variant2 != null;
			if (flag)
			{
				variant = new Variant();
				foreach (Variant current in variant2["rqual"].Values)
				{
					foreach (Variant current2 in current["qual_grp"].Values)
					{
						variant.pushBack("rmis_icon_" + current2["qual"]);
					}
				}
			}
			return variant;
		}

		public Variant GetPlayerRmisInfo(int rmisid)
		{
			Variant variant = null;
			Variant variant2 = this._rmisDatas[rmisid];
			Variant variant3 = this._playerRmiss[rmisid];
			bool flag = variant2 != null && variant3 != null;
			if (flag)
			{
				Variant variant4 = variant2["qual_grp"][variant3["qual"]];
				int @int = variant2["type"]._int;
				if (@int != 1)
				{
					if (@int == 2)
					{
						variant = new Variant();
						variant["type"] = variant2["type"];
						variant["misid"] = variant3["misids"][0];
						variant["cnt"] = variant3["cnt"];
						variant["freecnt"] = variant3["freecnt"];
						variant["qual"] = variant4["qual"];
						variant["uprate"] = variant4["uprate"];
						variant["failcnt"] = variant4["failcnt"];
						variant["failper"] = variant4["failper"];
						variant["mincnt"] = variant4["mincnt"];
						variant["maxcnt"] = variant4["maxcnt"];
						variant["miscnt"] = variant4["miscnt"];
						variant["ryb"] = variant4["ryb"]._uint;
						variant["rgld"] = variant4["rgld"]._uint;
					}
				}
				else
				{
					variant = new Variant();
					variant["type"] = variant2["type"];
					variant["qual"] = variant3["qual"];
					variant["cnt"] = variant3["cnt"];
					variant["misid"] = variant3["misids"][0];
					variant["tm"] = variant3["rtm"];
					variant["yb"] = variant4["ryb"];
					variant["percent"] = variant4["uprate"];
					bool flag2 = variant3["failcnt"] > variant4["failcnt"];
					if (flag2)
					{
						variant["addper"] = (variant3["failcnt"] - variant4["failcnt"]) * variant4["failper"];
					}
					else
					{
						variant["addper"] = 0;
					}
				}
			}
			return variant;
		}

		public Variant GetRmisDesc(int rmisid)
		{
			return this._rmisDatas[rmisid.ToString()];
		}

		public Variant GetClanCanAcceptRmis(int clanlvl = 0)
		{
			Variant result = null;
			int num = 0;
			foreach (Variant current in this._rmisDatas._arr)
			{
				Variant variant = current["clan"];
				bool flag = variant != null;
				if (flag)
				{
					int num2 = variant["lvl"];
					bool flag2 = num2 == clanlvl;
					if (flag2)
					{
						result = current;
						break;
					}
					bool flag3 = num2 < clanlvl;
					if (flag3)
					{
						bool flag4 = num < num2;
						if (flag4)
						{
							result = current;
							num = num2;
						}
					}
					else
					{
						bool flag5 = clanlvl == 0;
						if (flag5)
						{
							bool flag6 = num > num2 || num == 0;
							if (flag6)
							{
								result = current;
								num = num2;
							}
						}
					}
				}
			}
			return result;
		}

		public Variant GetMisSkillDesc(int skillid)
		{
			Variant variant = this._misSkillDescs[skillid.ToString()];
			bool flag = variant == null;
			if (flag)
			{
				variant["name"] = "undefine";
				variant["desc"] = "";
			}
			return variant;
		}

		public Variant GetFlushToTargetCost(int rmisid, int tQual)
		{
			Variant variant = new Variant();
			Variant variant2 = this._rmisDatas[rmisid.ToString()];
			Variant variant3 = this._playerRmiss[rmisid.ToString()];
			bool flag = variant2 != null && variant3 != null;
			if (flag)
			{
				int num = variant3["qual"];
				bool flag2 = num < tQual;
				if (flag2)
				{
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					for (int i = num; i < tQual; i++)
					{
						Variant variant4 = variant2["qual_grp"][i];
						bool flag3 = variant4 == null;
						if (flag3)
						{
							break;
						}
						int num5 = variant4["ryb"];
						num2 += num5 * variant4["mincnt"]._int;
						int @int = variant4["maxcnt"]._int;
						num4 += @int;
						num3 += num5 * @int;
					}
					variant["min"] = num2;
					variant["max"] = num3;
					variant["count"] = num4;
				}
			}
			return variant;
		}

		private void addDataToRmisData(string rmisid, Variant data)
		{
			Variant variant = this._rmisDatas[rmisid];
			bool flag = variant == null;
			if (flag)
			{
				this._rmisDatas[rmisid] = data;
			}
		}

		public Variant GetQualString(int qual)
		{
			string text;
			switch (qual)
			{
			case 1:
				text = "white";
				break;
			case 2:
				text = "green4";
				break;
			case 3:
				text = "blue3";
				break;
			case 4:
				text = "purple";
				break;
			case 5:
				text = "orange";
				break;
			default:
				text = "white";
				break;
			}
			Variant variant = new Variant();
			variant["color"] = text;
			variant["colorStr"] = LanguagePack.getLanguageText("UI_Class_select_list", text);
			return variant;
		}

		public void rmis_res(Variant data)
		{
			bool flag = data == null;
			if (!flag)
			{
				int num = 0;
				bool flag2 = data.ContainsKey("id");
				if (flag2)
				{
					num = data["id"];
				}
				bool flag3 = false;
				Variant variant = this._rmisDatas[num.ToString()];
				Variant rmis_fin = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.rmis_fin;
				switch (data["tp"]._int)
				{
				case 1:
				{
					bool reSetPlaymis = this._reSetPlaymis;
					if (reSetPlaymis)
					{
						this._reSetPlaymis = false;
						this._playerRmiss = new Variant();
					}
					Variant variant2 = this._playerRmiss[num.ToString()];
					this._playerRmiss[num.ToString()] = data;
					bool flag4 = variant2 == null;
					if (flag4)
					{
						(this.g_mgr as muLGClient).g_missionCT.mission_change(0);
					}
					else
					{
						int @int = variant["type"]._int;
						if (@int != 1)
						{
							if (@int != 2)
							{
								break;
							}
						}
					}
					(this.g_mgr as muLGClient).g_missionCT.player_rmis_change(num);
					break;
				}
				case 2:
				{
					Variant variant2 = this._playerRmiss[num.ToString()];
					bool flag5 = variant2 != null;
					if (flag5)
					{
						foreach (string current in data.Keys)
						{
							bool flag6 = current != "rqual";
							if (flag6)
							{
								variant2[current] = data[current];
							}
						}
						foreach (Variant current2 in variant2["rqual"].Values)
						{
							Variant variant3 = data["rqual"];
							bool flag7 = current2["id"] == variant3["id"];
							if (flag7)
							{
								foreach (string current3 in variant3.Keys)
								{
									current2[current3] = variant3[current3];
								}
							}
						}
						int int2 = variant["type"]._int;
						if (int2 != 1)
						{
							if (int2 != 2)
							{
							}
						}
					}
					break;
				}
				case 8:
				{
					bool flag8 = data.ContainsKey("mis_append") && data["mis_append"].Count > 0;
					if (flag8)
					{
						foreach (Variant current4 in data["mis_append"]._arr)
						{
							int misid = current4["misid"];
							bool flag9 = this._appawdRmis == null;
							if (flag9)
							{
								this._appawdRmis = new Variant();
							}
							this._appawdRmis[misid.ToString()] = current4;
							(this.g_mgr as muLGClient).g_missionCT.UpdateRmis(misid);
						}
					}
					break;
				}
				case 9:
				{
					bool flag10 = this._playerRmiss[num.ToString()] == null;
					if (flag10)
					{
						this._playerRmiss[num.ToString()] = new Variant();
					}
					Variant variant2 = this._playerRmiss[num.ToString()];
					foreach (string current5 in data.Keys)
					{
						variant2[current5] = data[current5];
					}
					Variant variant4 = new Variant();
					variant4["id"] = num;
					variant4["fincnt"] = data["fincnt"];
					(this.g_mgr as muLGClient).g_missionCT.MisDataChange("rmis", variant4);
					int int3 = variant["type"]._int;
					if (int3 != 1)
					{
						if (int3 != 2)
						{
						}
					}
					break;
				}
				case 10:
				{
					bool flag11 = this._playerRmiss[num.ToString()] == null;
					if (flag11)
					{
						this._playerRmiss[num.ToString()] = new Variant();
					}
					Variant variant2 = this._playerRmiss[num.ToString()];
					foreach (Variant current6 in rmis_fin.Values)
					{
						bool flag12 = current6["rid"] == num;
						if (flag12)
						{
							Variant expr_552 = current6;
							Variant val = expr_552["fcnt"];
							expr_552["fcnt"] = val + 1;
							flag3 = true;
							break;
						}
					}
					bool flag13 = !flag3 && rmis_fin != null;
					if (flag13)
					{
						Variant variant5 = new Variant();
						variant5["rid"] = num;
						variant5["fcnt"] = 1;
						rmis_fin.pushBack(variant5);
					}
					variant2["dalyawd"] = data["dalyawd"];
					int int4 = variant["type"]._int;
					if (int4 != 1)
					{
						if (int4 != 2)
						{
						}
					}
					break;
				}
				case 11:
				{
					bool reSetShare = this._reSetShare;
					if (reSetShare)
					{
						this._reSetShare = false;
						this._rmis_share = new Variant();
					}
					foreach (Variant current7 in rmis_fin._arr)
					{
						bool flag14 = current7 != null && current7["rid"] == num;
						if (flag14)
						{
							Variant expr_688 = current7;
							Variant val = expr_688["fcnt"];
							expr_688["fcnt"] = val + 1;
							flag3 = true;
							break;
						}
					}
					bool flag15 = !flag3 && rmis_fin != null;
					if (flag15)
					{
						Variant variant5 = new Variant();
						variant5["rid"] = num;
						variant5["fcnt"] = 1;
						rmis_fin.pushBack(variant5);
					}
					Variant variant6 = data["rmis_share"];
					bool flag16 = variant6.Count > 0;
					if (flag16)
					{
						foreach (Variant current8 in variant6._arr)
						{
							this._rmis_share[current8["id"]] = current8;
							foreach (Variant current9 in this._rmisDatas.Values)
							{
								bool flag17 = current9.ContainsKey("rmis_share") && current9["rmis_share"] == current8["id"];
								if (flag17)
								{
									int int5 = current9["type"]._int;
									if (int5 != 1)
									{
										if (int5 != 2)
										{
										}
									}
									break;
								}
							}
						}
					}
					break;
				}
				}
			}
		}

		private void requestRmisInfo(int id)
		{
			bool flag = id != 0;
			if (flag)
			{
				(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetRmisInfo(id);
			}
		}

		public void RefreshRmis(int id, int toqual = 0)
		{
			bool flag = id != 0;
			if (flag)
			{
				(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.RefreshRmisInfo(id, toqual, 0);
			}
		}

		public void RefreshQualRmis(int id, int rqualid, int bqual)
		{
			bool flag = id != 0;
			if (flag)
			{
				(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.RefreshRmisBqual(id, rqualid, bqual);
			}
		}

		public void GetAppawd()
		{
			(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetAppawd();
		}

		public void OnekeyFinRmis(int rmisid, bool isdouble)
		{
			(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.OnekeyFinRmis(rmisid, isdouble);
		}

		public void GetDayAwd(int rmisid)
		{
			(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetDayAwd(rmisid);
		}

		public void AcceptRmis(int id)
		{
			Variant variant = this._playerRmiss[id.ToString()];
			bool flag = variant != null;
			if (flag)
			{
				int num = variant["misids"][0];
				bool flag2 = num != 0;
				if (flag2)
				{
					(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.AcceptMis(num);
				}
			}
		}

		public int GetRmisAppwdExp(int rmisid, int misid)
		{
			Variant rmisDesc = this.GetRmisDesc(rmisid);
			bool flag = rmisDesc != null && rmisDesc.ContainsKey("appawd");
			int result;
			if (flag)
			{
				Variant appawdRmis = this.GetAppawdRmis(misid);
				bool flag2 = appawdRmis != null;
				if (flag2)
				{
					Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_appawd(rmisDesc["appawd"]);
					bool flag3 = variant != null;
					if (flag3)
					{
						Variant variant2 = variant["qual_grp"];
						int @int = appawdRmis["qual"]._int;
						bool flag4 = @int > 0 && variant2 != null && variant2.Count >= @int - 1;
						if (flag4)
						{
							result = variant2[@int - 1]["awdper"];
							return result;
						}
					}
				}
			}
			result = 0;
			return result;
		}

		public Variant GetAppawdRmis(int misid)
		{
			return (this._appawdRmis != null) ? this._appawdRmis[misid.ToString()] : null;
		}

		public void onCompleteMis(int misid)
		{
			bool flag = this._appawdRmis != null;
			if (flag)
			{
				bool flag2 = this._appawdRmis[misid.ToString()] != null;
				if (flag2)
				{
					this._appawdRmis[misid.ToString()] = null;
				}
				Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
				Variant variant2 = this._playerRmiss[variant["rmis"].ToString()];
				bool flag3 = variant != null && variant2 != null;
				if (flag3)
				{
					Variant variant3 = this._rmisDatas[variant["rmis"]._str];
					bool flag4 = variant3.ContainsKey("rmis_share");
					if (flag4)
					{
						Variant variant4 = this._rmis_share[variant3["rmis_share"]];
						bool flag5 = variant4;
						if (flag5)
						{
							Variant expr_E6 = variant4;
							Variant val = expr_E6["fincnt"];
							expr_E6["fincnt"] = val + 1;
						}
					}
					else
					{
						Variant expr_12D = this._playerRmiss[variant["rmis"].ToString()];
						Variant val = expr_12D["fincnt"];
						expr_12D["fincnt"] = val + 1;
					}
					Variant variant5 = new Variant();
					variant5["id"] = variant["rmis"];
					(this.g_mgr as muLGClient).g_missionCT.MisDataChange("rmis", variant5);
				}
			}
		}

		public void onAcceptRmisMis(int misid)
		{
			Variant variant = this._rmisDatas[misid.ToString()];
			bool flag = variant.ContainsKey("rmis_share");
			if (flag)
			{
				Variant variant2 = this._rmis_share[variant["rmis_share"]];
				bool flag2 = variant2;
				if (flag2)
				{
					Variant expr_47 = variant2;
					Variant val = expr_47["cnt"];
					expr_47["cnt"] = val - 1;
				}
			}
		}
	}
}
