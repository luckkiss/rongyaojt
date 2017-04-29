using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class HttpAppMgr : GameEventDispatcher
	{
		public static uint EVENT_GET_GIFT_CARD = 0u;

		public static uint EVENT_GETREWARD_ITEMS = 1u;

		public GiftCardsApp giftCard;

		public static HttpAppMgr instance;

		public void initGift()
		{
			bool flag = this.giftCard == null;
			if (flag)
			{
				this.giftCard = new GiftCardsApp();
			}
		}

		public int getTodyPayedRmb()
		{
			bool flag = this.giftCard == null || this.giftCard.rechangeTaskData == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = this.giftCard.rechangeTaskData.moneyPayed;
			}
			return result;
		}

		public void sendInputGiftCode(string code)
		{
			bool flag = code.Length == 5;
			if (flag)
			{
				this.giftCard.getShortCardsCode(code);
			}
			else
			{
				BaseProxy<GiftCardProxy>.getInstance().sendFetchCard(code);
			}
		}

		public void onGetnewCard()
		{
			debug.Log("获得新礼品发event");
			base.dispatchEvent(GameEvent.Create(HttpAppMgr.EVENT_GET_GIFT_CARD, this, null, false));
			this.useGiftCards(this.giftCard.lGiftCards[0]);
		}

		public void onGetRewardItems(int tp)
		{
			GiftCardType tp2 = this.giftCard.getTp(tp);
			debug.Log("兑换玩成！：");
			base.dispatchEvent(GameEvent.Create(HttpAppMgr.EVENT_GETREWARD_ITEMS, this, tp2, false));
		}

		public List<GiftCardData> getGiftCards()
		{
			bool flag = this.giftCard == null;
			List<GiftCardData> result;
			if (flag)
			{
				result = new List<GiftCardData>();
			}
			else
			{
				result = this.giftCard.lGiftCards;
			}
			return result;
		}

		public void useGiftCards(GiftCardData card)
		{
			bool flag = card == null;
			if (!flag)
			{
				debug.Log("使用礼品卡:" + card.id);
				card.getItems();
				this.giftCard.lGiftCards.Remove(card);
			}
		}

		public static void POSTSvr(string query, string param, Action<Variant> cb, bool rcvJSONHandler = true, string method = "POST")
		{
			bool flag = query == null || query == "" || cb == null;
			if (!flag)
			{
				IURLReq iURLReq = os.net.CreateURLReq(null);
				iURLReq.url = query;
				iURLReq.contentType = "application/x-www-form-urlencoded";
				iURLReq.dataFormat = "text";
				string text = "";
				text += param;
				debug.Log(" POSTSvr query:" + query + "\n param:" + param);
				iURLReq.data = text;
				iURLReq.method = method;
				iURLReq.load(delegate(IURLReq r, object vari)
				{
					bool flag2 = vari == null;
					if (flag2)
					{
						DebugTrace.print(" POSTSvr urlReq.load vari Null!");
					}
					string text2 = vari as string;
					DebugTrace.print(" POSTSvr urlReq.loaded str[" + text2 + "]!");
					Variant variant = JsonManager.StringToVariant(text2, true);
					bool flag3 = cb != null;
					if (flag3)
					{
						cb(JsonManager.StringToVariant(text2, true));
					}
				}, null, null);
			}
		}

		public static void POSTSvrstr(string query, string param, Action<string> cb, bool rcvJSONHandler = true, string method = "POST")
		{
			bool flag = query == null || query == "" || cb == null;
			if (!flag)
			{
				IURLReq iURLReq = os.net.CreateURLReq(null);
				iURLReq.url = query;
				iURLReq.contentType = "application/x-www-form-urlencoded";
				iURLReq.dataFormat = "text";
				string text = "";
				text += param;
				debug.Log(" POSTSvr query:" + query + "\n param:" + param);
				iURLReq.data = text;
				iURLReq.method = method;
				iURLReq.load(delegate(IURLReq r, object vari)
				{
					bool flag2 = vari == null;
					if (flag2)
					{
						DebugTrace.print(" POSTSvr urlReq.load vari Null!");
					}
					string text2 = vari as string;
					DebugTrace.print(" POSTSvr urlReq.loaded str[" + text2 + "]!");
					Variant variant = JsonManager.StringToVariant(text2, true);
					bool flag3 = cb != null;
					if (flag3)
					{
						cb(text2);
					}
				}, null, null);
			}
		}

		public static void init()
		{
			bool flag = HttpAppMgr.instance == null;
			if (flag)
			{
				HttpAppMgr.instance = new HttpAppMgr();
			}
		}
	}
}
