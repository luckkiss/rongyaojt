using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class GiftCardsApp
	{
		public List<GiftCardData> lGiftCards = new List<GiftCardData>();

		public Dictionary<int, GiftCardData> dGiftCardData = new Dictionary<int, GiftCardData>();

		private List<GiftCardType> lType = new List<GiftCardType>();

		private Dictionary<int, GiftCardType> dTp = new Dictionary<int, GiftCardType>();

		private List<GiftCardType> cacheGetCode = new List<GiftCardType>();

		public RechangeTaskData rechangeTaskData;

		private float beginLoadingTimer = 0f;

		private GiftCardType curTransing;

		public GiftCardsApp()
		{
			this.getRechangeCard();
			BaseProxy<GiftCardProxy>.getInstance().sendLoadItemCardInfo(null);
		}

		public GiftCardType getTp(int tp)
		{
			bool flag = !this.dTp.ContainsKey(tp);
			GiftCardType result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.dTp[tp];
			}
			return result;
		}

		public void getRechangeCard()
		{
			bool flag = ModelBase<PlayerModel>.getInstance().lvl < 10u;
			if (!flag)
			{
				string param = string.Concat(new object[]
				{
					"sid=",
					Globle.curServerD.sid,
					"&uid=",
					ModelBase<PlayerModel>.getInstance().uid,
					"&cid=",
					ModelBase<PlayerModel>.getInstance().cid,
					"&cust=2"
				});
				HttpAppMgr.POSTSvrstr(Globle.curServerD.do_url + "?paycard", param, new Action<string>(this.onRechageHandle), true, "POST");
			}
		}

		public void onRechageHandle(string str)
		{
			debug.Log("收到充值礼包：" + str);
			bool flag = str == "";
			if (!flag)
			{
				JSONNode d = JSON.Parse(str);
				bool flag2 = this.rechangeTaskData == null;
				if (flag2)
				{
					this.rechangeTaskData = new RechangeTaskData();
				}
				this.rechangeTaskData.init(d);
			}
		}

		public void getFirstRechangeCard()
		{
			string param = string.Concat(new object[]
			{
				"sid=",
				Globle.curServerD.sid,
				"&uid=",
				ModelBase<PlayerModel>.getInstance().uid,
				"&cid=",
				ModelBase<PlayerModel>.getInstance().cid,
				"&tp=2"
			});
			HttpAppMgr.POSTSvrstr(Globle.curServerD.do_url + "?card", param, new Action<string>(this.onFirstRechange), true, "POST");
		}

		public void onFirstRechange(string str)
		{
			Debug.Log("受宠礼包信息:" + str);
		}

		public void getAllCode()
		{
			bool flag = this.cacheGetCode.Count > 0;
			if (!flag)
			{
				bool flag2 = this.lType.Count == 0;
				if (!flag2)
				{
					this.dGiftCardData.Clear();
					this.lGiftCards.Clear();
					foreach (GiftCardType current in this.lType)
					{
						this.cacheGetCode.Add(current);
					}
					this.beginLoadingTimer = Time.time;
					this.getCardsCode();
				}
			}
		}

		private void getCardsCode()
		{
			debug.Log(string.Concat(new object[]
			{
				"请求获得新卡:",
				this.cacheGetCode.Count,
				"   ",
				this.curTransing
			}));
			bool flag = this.cacheGetCode.Count == 0;
			if (flag)
			{
				foreach (GiftCardData current in this.lGiftCards)
				{
					debug.Log("判断有新:" + this.beginLoadingTimer + current.creattimer);
					bool flag2 = this.beginLoadingTimer <= current.creattimer;
					if (flag2)
					{
						HttpAppMgr.instance.onGetnewCard();
						bool flag3 = current.cardType.functp == 4;
						if (flag3)
						{
							ArrayList arrayList = new ArrayList();
							arrayList.Add(current);
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_GIFTCARD, arrayList, false);
						}
						break;
					}
				}
			}
			else
			{
				bool flag4 = this.curTransing != null;
				if (!flag4)
				{
					this.curTransing = this.cacheGetCode[0];
					this.cacheGetCode.RemoveAt(0);
					string text = string.Concat(new object[]
					{
						"sid=",
						Globle.curServerD.sid,
						"&uid=",
						ModelBase<PlayerModel>.getInstance().uid,
						"&cid=",
						ModelBase<PlayerModel>.getInstance().cid,
						"&tp="
					});
					debug.Log(string.Concat(new object[]
					{
						"请求激活码::::::::::::::::::",
						Globle.curServerD.do_url,
						"?card",
						text,
						this.curTransing.id
					}));
					HttpAppMgr.POSTSvrstr(Globle.curServerD.do_url + "?card", text + this.curTransing.id, new Action<string>(this.onHttpCallback), true, "POST");
				}
			}
		}

		public void getShortCardsCode(string code)
		{
			string text = string.Concat(new object[]
			{
				"sid=",
				Globle.curServerD.sid,
				"&cid=",
				ModelBase<PlayerModel>.getInstance().cid,
				"&shortcode="
			});
			debug.Log(string.Concat(new string[]
			{
				"请求激活码::::::::::::::::::",
				Globle.curServerD.do_url,
				"?card=2",
				text,
				code
			}));
			HttpAppMgr.POSTSvrstr(Globle.curServerD.do_url + "?card=2", text + code, new Action<string>(this.onHttpShortCars), true, "POST");
		}

		private void onHttpShortCars(string str)
		{
			bool flag = str == "";
			if (!flag)
			{
				Variant variant = JsonManager.StringToVariant(str, true);
				bool flag2 = variant["r"] == 1;
				if (flag2)
				{
					debug.Log("获得激活码：" + variant["res"] + " " + str);
					string code = variant["res"];
					HttpAppMgr.instance.sendInputGiftCode(code);
				}
				else
				{
					Globle.err_output(variant["r"]);
					debug.Log("激活码领取的错误码：" + variant["r"]);
				}
			}
		}

		public void onFirstRechangebbb(string str)
		{
			Debug.Log("受宠礼包信息:" + str);
		}

		public void addGiftType(GiftCardType type)
		{
			bool flag = this.dTp.ContainsKey(type.id);
			if (flag)
			{
				this.lType.Remove(this.dTp[type.id]);
			}
			this.lType.Add(type);
			this.dTp[type.id] = type;
		}

		private void onHttpCallback(string str)
		{
			bool flag = this.curTransing == null;
			if (flag)
			{
				this.curTransing = null;
				this.getCardsCode();
			}
			else
			{
				bool flag2 = str == "";
				if (flag2)
				{
					this.curTransing = null;
					this.getCardsCode();
				}
				else
				{
					Variant variant = JsonManager.StringToVariant(str, true);
					bool flag3 = variant["r"] == 1;
					if (flag3)
					{
						debug.Log(string.Concat(new object[]
						{
							"获得激活码：",
							this.curTransing.id,
							" ",
							str
						}));
						GiftCardData giftCardData = new GiftCardData();
						giftCardData.id = this.curTransing.id;
						giftCardData.code = variant["res"];
						giftCardData.cardType = this.curTransing;
						giftCardData.initTimer();
						bool flag4 = this.dGiftCardData.ContainsKey(giftCardData.id);
						if (flag4)
						{
							GiftCardData item = this.dGiftCardData[giftCardData.id];
							this.lGiftCards.Remove(item);
						}
						this.dGiftCardData[giftCardData.id] = giftCardData;
						this.lGiftCards.Add(giftCardData);
						debug.Log("lGiftCards.clount::" + this.lGiftCards.Count);
					}
					else
					{
						Globle.err_output(variant["r"]);
						debug.Log("激活码领取的错误码：" + variant["r"]);
					}
					this.curTransing = null;
					this.getCardsCode();
				}
			}
		}
	}
}
