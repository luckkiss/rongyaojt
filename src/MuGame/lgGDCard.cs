using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDCard : lgGDBase
	{
		public const int TYPE_FIRST_CHARGE = 0;

		public const int TYPE_DAY_CUMULATE = 1;

		public const int TYPE_KF_CUMULATE = 2;

		public const int TYPE_CUMULATE_CHARGE = 3;

		public const int TYPE_CONSUME_CUMULATE = 4;

		public const int TYPE_CONSUME_TIME_CUMULATE = 5;

		public const int TYPE_COMMUTE_DAY_CONSUME = 6;

		private Dictionary<int, Variant> _cardConfs = new Dictionary<int, Variant>();

		private Dictionary<int, Variant> _payCardConfs = new Dictionary<int, Variant>();

		private item_card _card = new item_card();

		private Variant _willEffectiveArr;

		private Variant _compensateCards;

		private bool _special_invalid = false;

		private Variant _special_cards = new Variant();

		private Variant _has_regetcards_array = new Variant();

		protected InGameCardMsgs _cardMsg = null;

		private LGIUIMainUI _lguimain = null;

		private Action _succFun;

		private Action _failFun;

		private Action _finFun;

		protected int _getCardDataFlag = 0;

		private LGIUIMainUI lguimain
		{
			get
			{
				bool flag = this._lguimain != null;
				if (flag)
				{
				}
				return this._lguimain;
			}
		}

		public lgGDCard(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDCard(m as gameManager);
		}

		public override void init()
		{
		}

		private bool is_special_card(uint functp)
		{
			return functp == 1u || functp == 2u || functp == 3u || functp == 4u;
		}

		public void processFun(double tm)
		{
			bool flag = this._willEffectiveArr == null || this._willEffectiveArr.Length <= 0;
			if (flag)
			{
			}
			Variant variant = new Variant();
			foreach (string current in this._willEffectiveArr.Keys)
			{
				Variant variant2 = this._willEffectiveArr[current];
				bool flag2 = variant2["acttm"] <= (this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L;
				if (flag2)
				{
					int idx = variant2["functp"];
					bool flag3 = variant == null;
					if (flag3)
					{
						variant = new Variant();
					}
					bool flag4 = variant[idx] == null;
					if (flag4)
					{
						variant[idx] = new Variant();
					}
					variant[idx].pushBack(variant2);
					this._willEffectiveArr.RemoveKey(current);
				}
			}
			bool flag5 = variant;
			if (flag5)
			{
				this.addCardMails(variant[4]);
			}
		}

		private bool canMail(Variant cardData)
		{
			bool result = true;
			bool flag = (cardData.ContainsKey("acttm") && cardData["acttm"] > (this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L) || (cardData.ContainsKey("fintm") && cardData["fintm"] > 0 && cardData["fintm"] < (this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L) || this.is_card_fetched(cardData["tp"]._uint) != 0.0;
			if (flag)
			{
				result = false;
			}
			return result;
		}

		private void addCardMails(Variant cards)
		{
			foreach (Variant current in cards.Values)
			{
				bool flag = this.canMail(current);
				if (flag)
				{
					this.addCardMail(current);
				}
			}
		}

		private void addCardMail(Variant cardData)
		{
			int @int = cardData["tp"]._int;
			bool flag = true;
			foreach (Variant current in this._compensateCards.Values)
			{
				bool flag2 = @int == current["tp"];
				if (flag2)
				{
					flag = false;
					break;
				}
			}
			bool flag3 = flag;
			if (flag3)
			{
				bool flag4 = this._compensateCards == null;
				if (flag4)
				{
					this._compensateCards = new Variant();
				}
				this._compensateCards[@int] = cardData;
				this.setCardMail(cardData);
			}
		}

		private void setCardMail(Variant card)
		{
			Variant variant = new Variant();
			variant["tp"] = card["tp"];
			bool flag = card["itm"] || card["eqp"] || card["yb"] || card["gld"];
			if (flag)
			{
				variant["itm"] = card;
				variant["flag"] = 4;
			}
			else
			{
				variant["flag"] = 1;
			}
			variant["acttm"] = Convert.ToInt32(card["acttm"]._int);
			variant["fintm"] = Convert.ToInt32(card["fintm"]._int);
			variant["lvl"] = Convert.ToInt32(card["lvl"]._int);
			bool flag2 = card.ContainsKey("desc");
			if (flag2)
			{
				variant["msg"] = card["desc"];
			}
			variant["title"] = card["name"];
			variant["frmcid"] = 0;
			variant["tm"] = Convert.ToInt64((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L);
		}

		private void resetCardsMail()
		{
			foreach (Variant current in this._compensateCards.Values)
			{
			}
			this._compensateCards = null;
		}

		public void SetPaycardData(Variant data)
		{
			foreach (Variant current in data.Values)
			{
				bool isInt = current.isInt32;
				if (!isInt)
				{
					bool isStr = current.isStr;
					if (!isStr)
					{
						bool flag = this._payCardConfs == null;
						if (flag)
						{
							this._payCardConfs = new Dictionary<int, Variant>();
						}
						this._payCardConfs[current["tp"]] = current;
					}
				}
			}
		}

		public void ResetTpawds()
		{
			this._cardConfs = null;
			this._willEffectiveArr = null;
			this.resetCardsMail();
			this.get_itmcards(null);
		}

		public Variant get_special_cards()
		{
			Variant variant = new Variant();
			bool special_invalid = this._special_invalid;
			if (special_invalid)
			{
				this._special_invalid = false;
				this._special_cards = new Variant();
				bool flag = this._cardConfs != null;
				if (flag)
				{
					foreach (Variant current in this._cardConfs.Values)
					{
						bool flag2 = this.is_special_card(current["functp"]);
						if (flag2)
						{
							this._special_cards.pushBack(current);
						}
					}
				}
			}
			return this._special_cards;
		}

		public Variant get_normal_cards()
		{
			Variant variant = new Variant();
			Variant cards = this._card.cards;
			bool flag = cards;
			if (flag)
			{
				Variant variant2 = new Variant();
				foreach (Variant current in cards.Values)
				{
					Variant variant3 = this.get_tpawd(current["tp"]._uint);
					bool flag2 = variant3 == null;
					if (flag2)
					{
						bool flag3 = !this._has_regetcards_array.ContainsKey(current["tp"]._int);
						if (flag3)
						{
							this._has_regetcards_array[current["tp"]._int] = true;
							variant2.pushBack(current["tp"]._int);
						}
					}
					else
					{
						bool flag4 = this.is_special_card(variant3["functp"]);
						if (!flag4)
						{
							Variant variant4 = new Variant();
							variant4["tpawd"] = variant3;
							variant4["cardid"] = current["cardid"];
							variant4["tp"] = current["tp"];
							variant._arr.Add(variant4);
						}
					}
				}
				bool flag5 = variant2.Count > 0;
				if (flag5)
				{
					this.get_itmcards(variant2);
				}
			}
			return variant;
		}

		public Variant get_tpawd(uint tp)
		{
			bool flag = this._cardConfs != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in this._cardConfs.Values)
				{
					bool flag2 = tp == current["tp"]._uint;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetPayconf(uint tp)
		{
			bool flag = this._payCardConfs != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in this._payCardConfs.Values)
				{
					bool flag2 = tp == current["tp"]._uint;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant getCardsByTp(uint tp)
		{
			Variant cards = this._card.cards;
			bool flag = cards && cards.Count > 0;
			Variant result;
			if (flag)
			{
				foreach (Variant current in cards.Values)
				{
					bool flag2 = current["tp"] == tp;
					if (flag2)
					{
						result = current["cards"];
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public void updateCard(Variant newcard)
		{
			Variant cards = this._card.cards;
			bool flag = true;
			bool flag2 = cards == null;
			if (flag2)
			{
				this._card.cards = new Variant();
			}
			else
			{
				uint num = 0u;
				while ((ulong)num < (ulong)((long)cards.Count))
				{
					bool flag3 = cards[num]["tp"] == newcard["tp"];
					if (flag3)
					{
						cards[num] = newcard;
						flag = false;
						break;
					}
					num += 1u;
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				this._card.cards.pushBack(newcard);
			}
		}

		private bool rmvCardid(uint tp, string cardid)
		{
			Variant cardsByTp = this.getCardsByTp(tp);
			bool flag = cardsByTp != null && cardsByTp.Count > 0;
			bool result;
			if (flag)
			{
				for (int i = 0; i < cardsByTp.Count; i++)
				{
					bool flag2 = cardid == cardsByTp[i];
					if (flag2)
					{
						cardsByTp._arr.RemoveAt(i);
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public double is_card_fetched(uint tp)
		{
			bool flag = this._card.fetched_cards != null;
			double result;
			if (flag)
			{
				foreach (Variant current in this._card.fetched_cards.Values)
				{
					bool flag2 = tp == current["tp"];
					if (flag2)
					{
						result = current["tm"];
						return result;
					}
				}
			}
			result = 0.0;
			return result;
		}

		public bool is_card_fetched_by_cardid(uint tp, string cardid)
		{
			Variant fetched_cards = this._card.fetched_cards;
			bool flag = fetched_cards;
			bool result;
			if (flag)
			{
				foreach (Variant current in fetched_cards.Values)
				{
					bool flag2 = tp == current["tp"];
					if (flag2)
					{
						using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator2 = current["cards"].Values.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string b = enumerator2.Current;
								bool flag3 = cardid == b;
								if (flag3)
								{
									result = true;
									return result;
								}
							}
						}
					}
				}
			}
			result = false;
			return result;
		}

		private void rmvFetchedCard(uint tp)
		{
			Variant fetched_cards = this._card.fetched_cards;
			bool flag = fetched_cards == null;
			if (flag)
			{
				this._card.fetched_cards = new Variant();
			}
			else
			{
				for (int i = 0; i < fetched_cards.Count; i++)
				{
					bool flag2 = tp == fetched_cards[i]["tp"];
					if (flag2)
					{
						fetched_cards._arr.RemoveAt(i);
						break;
					}
				}
			}
		}

		private void addFetchedCard(uint tp, Variant data)
		{
			Variant fetched_cards = this._card.fetched_cards;
			bool flag = fetched_cards == null;
			if (flag)
			{
				this._card.fetched_cards = new Variant();
				this._card.fetched_cards.pushBack(data);
			}
			else
			{
				bool flag2 = true;
				uint num = 0u;
				while ((ulong)num < (ulong)((long)fetched_cards.Count))
				{
					bool flag3 = data["tp"] == fetched_cards[num]["tp"];
					if (flag3)
					{
						fetched_cards[num] = data;
						flag2 = false;
					}
					num += 1u;
				}
				bool flag4 = flag2;
				if (flag4)
				{
					fetched_cards.pushBack(data);
				}
			}
		}

		public void item_card_res(Variant data)
		{
		}

		private void updateSpecialCards(Variant tpawds)
		{
			foreach (Variant current in tpawds.Values)
			{
				bool flag = 4 == current["functp"];
				if (flag)
				{
					bool flag2 = this._compensateCards == null;
					if (flag2)
					{
						this._compensateCards = new Variant();
					}
					bool flag3 = this.canMail(current);
					if (flag3)
					{
						this.addCardMail(current);
					}
				}
				bool flag4 = current.ContainsKey("acttm") && current["acttm"] > (this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L;
				if (flag4)
				{
					bool flag5 = this._willEffectiveArr == null;
					if (flag5)
					{
						this._willEffectiveArr = new Variant();
					}
					this._willEffectiveArr.pushBack(current);
				}
			}
		}

		public void fetch_itm_card(string cardid)
		{
			Variant variant = new Variant();
			variant["cardid"] = cardid;
			this._cardMsg.fetch_itm_card(variant);
		}

		public void get_itmcards(Variant awdtps = null)
		{
			bool flag = awdtps && awdtps.Count > 0;
			if (flag)
			{
				Variant variant = new Variant();
				variant["cardid"] = awdtps;
				this._cardMsg.get_itmcards(variant);
			}
			else
			{
				this._cardMsg.get_itmcards(null);
			}
		}

		public void FetchItmCard(string cardid)
		{
		}

		public void php_get_card_id(uint tp)
		{
		}

		public void CreateCard(string cardid, Action finFun, Action succFun = null, Action failFun = null)
		{
		}

		private void createCardRes(Variant data)
		{
			bool flag = data["r"] != 1;
			if (flag)
			{
				bool flag2 = this._failFun != null;
				if (flag2)
				{
					this._failFun();
					this._failFun = null;
				}
			}
			else
			{
				bool flag3 = this._succFun != null;
				if (flag3)
				{
					this._succFun();
					this._succFun = null;
				}
			}
			bool flag4 = this._finFun != null;
			if (flag4)
			{
				this._finFun();
				this._finFun = null;
			}
		}

		private void on_url_req_handler(Variant data)
		{
			bool flag = data["r"] != 1;
			if (!flag)
			{
				this.FetchItmCard(data["res"]);
			}
		}

		private void on_url_req_err()
		{
		}

		protected void _getPaycardInfoByPHP(string param, Action<Variant> fun)
		{
		}

		public void GetCardData(uint cust, int tcyb)
		{
		}

		public void refresh_tcyb()
		{
			this.GetCardData(0u, 1);
			this.GetCardData(0u, 2);
			this.GetCardData(0u, 3);
		}

		private void on_selfdefine_req(Variant data)
		{
		}

		private void on_dayChargeCumulate_req(Variant data)
		{
		}

		private void on_kfChargeCumulate_req(Variant data)
		{
		}

		private void on_ConsumeCumulate_req(Variant data)
		{
		}

		private void on_TimeConsumeCumulate_req(Variant data)
		{
		}

		private void on_DayConsumeCumulate_req(Variant data)
		{
		}
	}
}
