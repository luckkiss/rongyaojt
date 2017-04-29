using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MuGame
{
	internal class welfareProxy : BaseProxy<welfareProxy>
	{
		public enum ActiveType
		{
			close,
			selfWelfareInfo,
			firstRechange,
			onLineTimeAward,
			accumulateLogin,
			lvlAward,
			accumulateRecharge,
			accumulateConsumption,
			dayRechargeAward
		}

		public static bool b_zhuan = false;

		private bool b_leijizhongzhi = false;

		private bool b_leijixiaofei = false;

		private bool b_leijichongzhi_today = false;

		public static uint SHOWFIRSTRECHARGE = 4701u;

		public static uint SHOWDAILYRECHARGE = 4702u;

		public static uint UPLEVELAWARD = 4703u;

		public static uint ACCUMULATERECHARGE = 4704u;

		public static uint ACCUMULATECONSUMPTION = 4705u;

		public static uint ACCUMULATETODAYRECHARGE = 4706u;

		public static uint SUCCESSGETFIRSTRECHARGEAWARD = 4707u;

		public static uint totalRecharge;

		public static uint totalXiaofei;

		public static uint firstRecharge;

		public static uint todayTotal_recharge;

		public List<Variant> dengjijiangli;

		public List<Variant> leijichongzhi;

		public List<Variant> leijixiaofei;

		public List<Variant> dailyGift;

		public bool _isShowEveryDataLogin = false;

		public void showIconLight()
		{
			bool flag = !welfareProxy.b_zhuan && !this.b_leijichongzhi_today && !this.b_leijixiaofei && !this.b_leijichongzhi_today;
			if (flag)
			{
				debug.Log("关图标");
				IconAddLightMgr.getInstance().showOrHideFire("close_Light_awardCenter", null);
			}
			else
			{
				debug.Log("亮图标");
				IconAddLightMgr.getInstance().showOrHideFire("open_Light_awardCenter", null);
			}
		}

		public welfareProxy()
		{
			this._isShowEveryDataLogin = false;
			this.addProxyListener(47u, new Action<Variant>(this.onActive));
		}

		private void onActive(Variant data)
		{
			debug.Log("奖励信息:" + data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				switch (num)
				{
				case 0:
					this.setClose(data);
					break;
				case 1:
					this.setWelfare(data);
					break;
				case 2:
					this.setGetFirstRechargeAward(data);
					break;
				case 3:
					this.setOnlineTime(data);
					break;
				case 4:
					this.setTotalLoginAward(data);
					break;
				case 5:
					this.setLvlAward(data);
					break;
				case 6:
					this.setAccumulateRecharge(data);
					break;
				case 7:
					this.setAccumulateConsumption(data);
					break;
				case 8:
					this.setDayRechargeAward(data);
					break;
				case 9:
					this.setTotalData(data);
					break;
				}
			}
		}

		private void setClose(Variant data)
		{
			int num = data["close"];
		}

		private void setWelfare(Variant data)
		{
			welfareProxy.totalRecharge = data["total_recharge"];
			welfareProxy.totalXiaofei = data["total_xiaofei"];
			welfareProxy.firstRecharge = data["first_recharge"];
			Variant variant = new Variant();
			variant["show"] = (welfareProxy.firstRecharge <= 0u);
			base.dispatchEvent(GameEvent.Create(welfareProxy.SHOWFIRSTRECHARGE, this, variant, false));
			Variant variant2 = data["zaixianshijian"];
			uint num = variant2["status_type"];
			uint num2 = variant2["end_tm"];
			uint num3 = variant2["status_count"];
			Variant variant3 = data["leijidenglu"];
			uint num4 = variant3["last_day"];
			uint num5 = variant3["day_count"];
			debug.Log(num4.ToString() + "--->>");
			bool flag = this.CheckTime(num4);
			bool flag2 = flag && !this._isShowEveryDataLogin;
			if (flag2)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(num4);
				arrayList.Add(num5);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EVERYDAYLOGIN, arrayList, false);
				this._isShowEveryDataLogin = true;
				bool flag3 = a3_expbar.instance != null;
				if (flag3)
				{
					a3_expbar.instance.getGameObjectByPath("operator/LightTips/everyDayLogin").SetActive(true);
				}
			}
			Variant variant4 = data["richongjiangli"];
			welfareProxy.todayTotal_recharge = variant4["total_recharge"];
			this.dailyGift = variant4["gift"]._arr;
			this.b_leijichongzhi_today = ModelBase<WelfareModel>.getInstance().for_jinrichongzhi(this.dailyGift);
			this.dengjijiangli = data["dengjijiangli"]._arr;
			welfareProxy.b_zhuan = ModelBase<WelfareModel>.getInstance().for_dengjilibao(this.dengjijiangli);
			this.leijichongzhi = data["leijichongzhi"]._arr;
			this.b_leijizhongzhi = ModelBase<WelfareModel>.getInstance().for_leijichongzhi(this.leijichongzhi);
			this.leijixiaofei = data["leijixiaofei"]._arr;
			this.b_leijixiaofei = ModelBase<WelfareModel>.getInstance().for_leixjixiaofei(this.leijixiaofei);
			this.showIconLight();
		}

		private bool CheckTime(uint last_day)
		{
			bool result = false;
			DateTime time = this.GetTime(last_day.ToString());
			TimeSpan timeSpan = DateTime.Now - time;
			bool flag = timeSpan.TotalSeconds > 0.0;
			if (flag)
			{
				bool flag2 = timeSpan.Days > 0;
				if (flag2)
				{
					result = true;
				}
				bool flag3 = DateTime.Now.Year == time.Year && DateTime.Now.Month == time.Month && DateTime.Now.Day - time.Day > 0;
				if (flag3)
				{
					result = true;
				}
				bool flag4 = DateTime.Now.Year == time.Year && DateTime.Now.Month > time.Month;
				if (flag4)
				{
					result = true;
				}
			}
			return result;
		}

		private void setGetFirstRechargeAward(Variant data)
		{
			base.dispatchEvent(GameEvent.Create(welfareProxy.SUCCESSGETFIRSTRECHARGEAWARD, this, data, false));
		}

		private void setOnlineTime(Variant data)
		{
			uint num = data["status_type"];
			uint num2 = data["end_tm"];
			uint num3 = data["status_count"];
		}

		private void setTotalLoginAward(Variant data)
		{
			uint val = data["last_day"];
			uint val2 = data["day_count"];
			Variant variant = new Variant();
			variant["last_day"] = val;
			variant["day_count"] = val2;
			variant["show"] = true;
			base.dispatchEvent(GameEvent.Create(welfareProxy.SHOWDAILYRECHARGE, this, variant, false));
		}

		private void setLvlAward(Variant data)
		{
			uint val = data["gift_id"];
			this.dengjijiangli.Add(val);
			welfareProxy.b_zhuan = ModelBase<WelfareModel>.getInstance().for_dengjilibao(this.dengjijiangli);
			this.showIconLight();
			base.dispatchEvent(GameEvent.Create(welfareProxy.UPLEVELAWARD, this, data, false));
		}

		private void setAccumulateRecharge(Variant data)
		{
			uint num = data["gift_id"];
			this.b_leijizhongzhi = ModelBase<WelfareModel>.getInstance().for_leijichongzhi(this.leijichongzhi);
			this.showIconLight();
			base.dispatchEvent(GameEvent.Create(welfareProxy.ACCUMULATERECHARGE, this, data, false));
		}

		private void setAccumulateConsumption(Variant data)
		{
			uint num = data["gift_id"];
			this.b_leijixiaofei = ModelBase<WelfareModel>.getInstance().for_leixjixiaofei(this.leijixiaofei);
			this.showIconLight();
			base.dispatchEvent(GameEvent.Create(welfareProxy.ACCUMULATECONSUMPTION, this, data, false));
		}

		private void setDayRechargeAward(Variant data)
		{
			uint num = data["gift_id"];
			this.b_leijichongzhi_today = ModelBase<WelfareModel>.getInstance().for_jinrichongzhi(this.dailyGift);
			this.showIconLight();
			base.dispatchEvent(GameEvent.Create(welfareProxy.ACCUMULATETODAYRECHARGE, this, data, false));
		}

		private void setTotalData(Variant data)
		{
			bool flag = data.ContainsKey("total_xiaofei");
			if (flag)
			{
				uint num = data["total_xiaofei"];
				this.b_leijixiaofei = ModelBase<WelfareModel>.getInstance().for_leixjixiaofei(this.leijixiaofei);
				this.showIconLight();
			}
			bool flag2 = data.ContainsKey("total_recharge");
			if (flag2)
			{
				welfareProxy.totalRecharge = data["total_recharge"];
				this.b_leijizhongzhi = ModelBase<WelfareModel>.getInstance().for_leijichongzhi(this.leijichongzhi);
				this.showIconLight();
			}
			bool flag3 = data.ContainsKey("richong");
			if (flag3)
			{
				uint num2 = data["richong"];
				this.b_leijichongzhi_today = ModelBase<WelfareModel>.getInstance().for_jinrichongzhi(this.dailyGift);
				this.showIconLight();
			}
		}

		private DateTime GetTime(string timeStamp)
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = long.Parse(timeStamp + "0000000");
			TimeSpan value = new TimeSpan(ticks);
			return dateTime.Add(value);
		}

		public void sendWelfare(welfareProxy.ActiveType at, uint id = 4294967295u)
		{
			Variant variant = new Variant();
			variant["welfare_type"] = (uint)at;
			bool flag = id != 4294967295u;
			if (flag)
			{
				variant["id"] = id;
			}
			this.sendRPC(47u, variant);
		}
	}
}
