using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class LGGDAward : lgGDBase
	{
		private Variant _awardInfo;

		private LGIUIWelfare _uiAward;

		private InGameAwardMsgs _awardMsg;

		private InGameRandshop _randShopMsg;

		protected LGIUIMainuiAttach _mainuiattach;

		protected LGIUIMainUI mainui;

		protected const uint MICROLOAD_AWD = 1u;

		private bool _hasGetMicroAwd = false;

		private Variant _btlawdInfo = new Variant();

		private Variant _inviteData = new Variant();

		private Variant _GetFeedAwardData = new Variant();

		private LGIUIMainuiAttach mainuiattach
		{
			get
			{
				bool flag = this._mainuiattach == null;
				if (flag)
				{
					this._mainuiattach = (this.g_mgr.g_uiM.getLGUI("LGUIMainuiAttach") as LGIUIMainuiAttach);
				}
				return this._mainuiattach;
			}
		}

		private LGIUIWelfare uiAward
		{
			get
			{
				bool flag = this._uiAward == null;
				if (flag)
				{
					this._uiAward = (this.g_mgr.g_uiM.getLGUI("mdlg_welfare") as LGIUIWelfare);
				}
				return this._uiAward;
			}
		}

		public LGGDAward(gameManager m) : base(m)
		{
			this._awardMsg = (this.g_mgr.g_netM as muNetCleint).igAwardMsgs;
			this._randShopMsg = (this.g_mgr.g_netM as muNetCleint).igRandShopMsgs;
			this.mainui = (this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI);
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGDAward(m as gameManager);
		}

		public override void init()
		{
		}

		public void AwardRes(Variant data)
		{
		}

		public void refreshGrowPack()
		{
		}

		public bool HasGetMicroAwd()
		{
			return this._hasGetMicroAwd;
		}

		public void GetMlineMisPrize(uint misid)
		{
			this._awardMsg.GetMlineMisPrize((int)misid);
		}

		public Variant getBtlawdInfo(uint ltpid)
		{
			bool flag = this._btlawdInfo[ltpid] == null;
			Variant result;
			if (flag)
			{
				this.GetBtlawdInfo((int)ltpid);
				result = null;
			}
			else
			{
				result = this._btlawdInfo[ltpid];
			}
			return result;
		}

		public Variant GetAwardInfo()
		{
			return this._awardInfo;
		}

		public int is_clogdawd_day_got(uint day)
		{
			bool flag = this._awardInfo == null;
			int result;
			if (flag)
			{
				result = -2;
			}
			else
			{
				Variant variant = this._awardInfo["day_awd"]["fetched"];
				using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						uint num = enumerator.Current;
						bool flag2 = day == num;
						if (flag2)
						{
							result = -1;
							return result;
						}
					}
				}
				bool flag3 = this._awardInfo["day_awd"]["day"] >= day;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					result = 1;
				}
			}
			return result;
		}

		public int daylogin()
		{
			int num = 0;
			bool flag = this._awardInfo != null;
			if (flag)
			{
				bool flag2 = this._awardInfo["day_awd"] != null;
				if (flag2)
				{
					num = this._awardInfo["day_awd"]["day"]._int;
				}
			}
			Variant variant = new Variant();
			int count = variant.Count;
			return (num > count) ? count : num;
		}

		public int has_award_day()
		{
			int num = 0;
			int result;
			for (int i = 1; i <= this.daylogin(); i++)
			{
				int num2 = this.is_clogdawd_day_got((uint)i);
				int num3 = this.is_viplogdawd_day_got((uint)i);
				int num4 = 0;
				bool flag = num3 == 0 && num4 > 0;
				bool flag2 = num2 == 0 | flag;
				if (flag2)
				{
					num = i;
					result = num;
					return result;
				}
			}
			result = num;
			return result;
		}

		public int can_get_loginaward()
		{
			int result = 0;
			Variant variant = new Variant();
			int num = 0;
			foreach (Variant current in variant._arr)
			{
				num++;
			}
			for (int i = 1; i <= num; i++)
			{
				int num2 = this.is_clogdawd_day_got((uint)i);
				int num3 = this.is_viplogdawd_day_got((uint)i);
				int num4 = 0;
				bool flag = (num3 == 0 || num3 == 1) && num4 > 0;
				bool flag2 = (num2 == 0 || num2 == 1) | flag;
				if (flag2)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		public int is_viplogdawd_day_got(uint day)
		{
			bool flag = this._awardInfo == null;
			int result;
			if (flag)
			{
				result = -2;
			}
			else
			{
				Variant variant = this._awardInfo["day_awd"]["fetched_vip"];
				using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						uint num = enumerator.Current;
						bool flag2 = day == num;
						if (flag2)
						{
							result = -1;
							return result;
						}
					}
				}
				bool flag3 = this._awardInfo["day_awd"]["day"] >= day;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					result = 1;
				}
			}
			return result;
		}

		private Variant clogdawd_next(uint day)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			bool flag = variant == null || variant.Count < 1;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = day < 1u;
				if (flag2)
				{
					result = variant[0];
				}
				else
				{
					for (int i = 0; i < variant.Count; i++)
					{
						variant2 = variant[i];
						bool flag3 = variant2 == null;
						if (!flag3)
						{
							bool flag4 = variant2[day] > day;
							if (flag4)
							{
								result = variant2;
								return result;
							}
						}
					}
					result = null;
				}
			}
			return result;
		}

		public bool is_dayGet_award_get_today()
		{
			bool flag = this._awardInfo == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				TZDate tZDate = new TZDate((double)(this._awardInfo["clogdawdtm"] * 1000));
				TZDate tZDate2 = tZDate;
				result = (tZDate2.getDate() == tZDate.getDate() && tZDate2.month == tZDate.month && tZDate2.fullYear == tZDate.fullYear);
			}
			return result;
		}

		public void SendGetAwardInfo()
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			this._awardMsg.GetAward(variant);
		}

		public void SendRegist()
		{
			Variant variant = new Variant();
			variant["tp"] = 16;
			variant["subtp"] = 2;
			this._awardMsg.GetAward(variant);
		}

		public void SendMendedRegist()
		{
			Variant variant = new Variant();
			variant["tp"] = 16;
			variant["subtp"] = 3;
			this._awardMsg.GetAward(variant);
		}

		public void SendGetRegistAward(int scnt)
		{
			Variant variant = new Variant();
			variant["tp"] = 16;
			variant["subtp"] = 1;
			variant["scnt"] = scnt;
			this._awardMsg.GetAward(variant);
		}

		public void GetMicroawd()
		{
			Variant variant = new Variant();
			variant["tp"] = 5;
			variant["id"] = 1;
			this._awardMsg.GetAward(variant);
		}

		public void C2S_get_clogdawd(uint day, bool vip = false)
		{
			if (vip)
			{
				Variant variant = new Variant();
				variant["tp"] = 6;
				variant["day"] = day;
				variant["vip"] = vip;
				this._awardMsg.GetAward(variant);
			}
			else
			{
				Variant variant2 = new Variant();
				variant2["tp"] = 6;
				variant2["day"] = day;
				this._awardMsg.GetAward(variant2);
			}
		}

		public void GetBtlawdInfo(int ltpid)
		{
			Variant variant = new Variant();
			variant["tp"] = 7;
			variant["ltpid"] = ltpid;
			this._awardMsg.GetAward(variant);
		}

		public void GetBtlawdAward(int ltpid)
		{
			Variant variant = new Variant();
			variant["tp"] = 8;
			variant["ltpid"] = ltpid;
			this._awardMsg.GetAward(variant);
		}

		public void GetCrossWarrAward()
		{
			Variant variant = new Variant();
			variant["tp"] = 21;
			this._awardMsg.GetAward(variant);
		}

		public void FetchArenaCwinAwd(uint awdid)
		{
			Variant variant = new Variant();
			variant["tp"] = 22;
			variant["awdid"] = awdid;
			this._awardMsg.GetAward(variant);
		}

		public void GetLevelMapAwd(uint mapid)
		{
			Variant variant = new Variant();
			variant["tp"] = 24;
			variant["mapid"] = mapid;
			this._awardMsg.GetAward(variant);
		}

		public void GetInviteFriendsAward()
		{
			Variant variant = new Variant();
			variant["tp"] = 31;
			this._awardMsg.sendPvipMsg(variant);
		}

		public void GetFeedAwardData()
		{
			Variant variant = new Variant();
			variant["tp"] = 5;
			this._awardMsg.sendPvipMsg(variant);
		}

		public void GetDoPerdayAward()
		{
			Variant variant = new Variant();
			variant["tp"] = 6;
			this._awardMsg.sendPvipMsg(variant);
		}

		public void GetDaysAward(int gmisid)
		{
			Variant variant = new Variant();
			variant["tp"] = 7;
			variant["gmisid"] = gmisid;
			this._awardMsg.sendPvipMsg(variant);
		}

		public void GetFriendGlowAward(int id)
		{
			Variant variant = new Variant();
			variant["tp"] = 4;
			variant["ivt_lvlid"] = id;
			this._awardMsg.sendPvipMsg(variant);
		}

		public Variant GetInviteFriendData()
		{
			bool flag = this._inviteData == null;
			if (flag)
			{
				this._inviteData = new Variant();
				this._randShopMsg.getInviteFriendData();
			}
			return this._inviteData;
		}

		protected void getInviteFriendData()
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			this._awardMsg.sendPvipMsg(variant);
		}

		public void on_itm_msg(Variant data)
		{
			switch (data["tp"]._int)
			{
			case 2:
				this.OnGetInviteFriendData(data);
				break;
			case 3:
				this.OnGetFeedAwardData(data);
				break;
			}
		}

		public void OnGetInviteFriendData(Variant data)
		{
			using (List<Variant>.Enumerator enumerator = data._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string key = enumerator.Current;
					this._inviteData[key] = data[key];
				}
			}
			LGIUIPvipShare lGIUIPvipShare = new Variant() as LGIUIPvipShare;
			lGIUIPvipShare.refleshdivtTileState(this._inviteData);
			lGIUIPvipShare.RefleshInviteFriend(this._inviteData);
		}

		public Variant GetFeedAwd()
		{
			bool flag = this._GetFeedAwardData == null;
			if (flag)
			{
				this._GetFeedAwardData = new Variant();
				this._randShopMsg.GetFeedAwardData();
			}
			return this._GetFeedAwardData;
		}

		public void OnGetFeedAwardData(Variant data)
		{
			bool flag = this._GetFeedAwardData == null;
			if (flag)
			{
				this._GetFeedAwardData = data;
			}
			else
			{
				bool flag2 = data["gmisfeeds"];
				if (flag2)
				{
					Variant variant = new Variant();
					Variant variant2 = data["gmisfeeds "];
					bool flag3 = this._GetFeedAwardData["gmisfeeds"] == null;
					if (flag3)
					{
						variant = new Variant();
						this._GetFeedAwardData["gmisfeeds "] = variant;
					}
					else
					{
						variant = this._GetFeedAwardData["gmisfeeds"];
					}
					bool flag4 = variant2 != null;
					if (flag4)
					{
						for (int i = 0; i < variant2.Count; i++)
						{
							bool flag5 = true;
							for (int j = 0; j < variant.Count; j++)
							{
								bool flag6 = variant[j] == variant2[i];
								if (flag6)
								{
									flag5 = false;
									break;
								}
							}
							bool flag7 = flag5;
							if (flag7)
							{
								variant.pushBack(variant2[i]);
							}
						}
					}
				}
				bool flag8 = data["dmisfeed"];
				if (flag8)
				{
					this._GetFeedAwardData["dmisfeed"] = data["dmisfeed"];
				}
			}
			LGIUIPvipShare lGIUIPvipShare = new Variant() as LGIUIPvipShare;
			lGIUIPvipShare.OnGetFeedAwardData(this._GetFeedAwardData);
		}

		public bool isGmisFeedsGot(uint gmisid)
		{
			bool flag = this._GetFeedAwardData == null || this._GetFeedAwardData["gmisfeeds"] == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant variant = this._GetFeedAwardData["gmisfeeds"];
				bool flag2 = variant == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < variant.Count; i++)
					{
						bool flag3 = variant[i] == gmisid;
						if (flag3)
						{
							result = true;
							return result;
						}
					}
					result = false;
				}
			}
			return result;
		}

		public bool isDmisFeedsGot()
		{
			bool flag = this._GetFeedAwardData == null || this._GetFeedAwardData["dmisfeed"] == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this._GetFeedAwardData["dmisfeed"] < 1;
				result = !flag2;
			}
			return result;
		}

		public void GetOlAwds(Variant msg)
		{
			int num = msg["subtp"];
			int num2 = num;
			if (num2 != 1)
			{
				if (num2 != 2)
				{
				}
			}
		}
	}
}
