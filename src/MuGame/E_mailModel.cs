using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MuGame
{
	internal class E_mailModel : ModelBase<E_mailModel>
	{
		public static int toCid = 0;

		public List<mailData> familyMailDic = new List<mailData>();

		public List<mailData> ahMailDic = new List<mailData>();

		public List<mailData> systemMailDic = new List<mailData>();

		public List<mailData> gameMailDic = new List<mailData>();

		public Dictionary<int, List<mailData>> personalMailDic = new Dictionary<int, List<mailData>>();

		private string[] sep1 = new string[]
		{
			"#!#&"
		};

		private string[] sep2 = new string[]
		{
			"#)#&"
		};

		private const int _15days = 1296000;

		private DateTime nowTime = DateTime.Now;

		private int timeSmp;

		public List<mailData> perLocalStr = new List<mailData>();

		public List<mailData> famLocalStr = new List<mailData>();

		public Dictionary<mailData, GiftCardData> giftCardDataDic = new Dictionary<mailData, GiftCardData>();

		private bool isInited = false;

		public void init()
		{
			bool flag = this.isInited;
			if (!flag)
			{
				this.isInited = true;
				bool flag2 = HttpAppMgr.instance != null;
				if (flag2)
				{
					HttpAppMgr.instance.addEventListener(HttpAppMgr.EVENT_GET_GIFT_CARD, new Action<GameEvent>(this.getGiftCard));
					BaseProxy<E_mailProxy>.getInstance();
				}
				this.timeSmp = muNetCleint.instance.CurServerTimeStamp;
				debug.Log(this.timeSmp.ToString());
				this.perLocalStr.Clear();
				this.famLocalStr.Clear();
				string text = FileMgr.loadString(FileMgr.TYPE_MAIL, "per");
				string text2 = "";
				text2 = FileMgr.loadString(FileMgr.TYPE_MAIL, "fam");
				bool flag3 = text != "" && text != " ";
				if (flag3)
				{
					string[] array = text.Split(this.sep2, StringSplitOptions.None);
					try
					{
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string text3 = array2[i];
							string[] array3 = text3.Split(this.sep1, StringSplitOptions.None);
							debug.Log(array3[0] + 22);
							bool flag4 = array3[0] != "" && array3[0] != " ";
							if (flag4)
							{
								bool flag5 = int.Parse(array3[6]) + 1296000 > this.timeSmp;
								if (flag5)
								{
									mailData mailData = new mailData();
									mailData.frmcid = int.Parse(array3[0]);
									mailData.frmsex = int.Parse(array3[1]);
									mailData.cid = int.Parse(array3[2]);
									mailData.time = array3[3];
									mailData.frmname = array3[4];
									mailData.msg = array3[5];
									mailData.str = text3 + "#)#&";
									bool flag6 = ModelBase<E_mailModel>.getInstance().personalMailDic.ContainsKey(mailData.frmcid);
									if (flag6)
									{
										ModelBase<E_mailModel>.getInstance().personalMailDic[mailData.frmcid].Add(mailData);
									}
									else
									{
										List<mailData> list = new List<mailData>();
										list.Add(mailData);
										ModelBase<E_mailModel>.getInstance().personalMailDic.Add(mailData.frmcid, list);
									}
									this.perLocalStr.Add(mailData);
								}
							}
						}
						this.saveLocalData(this.perLocalStr, 4);
					}
					catch (Exception var_15_26F)
					{
						FileMgr.saveString(FileMgr.TYPE_MAIL, "per", " ");
						FileMgr.saveString(FileMgr.TYPE_MAIL, "fam", " ");
					}
				}
				bool flag7 = text2 != "" && text2 != " ";
				if (flag7)
				{
					string[] array4 = text2.Split(this.sep2, StringSplitOptions.None);
					string[] array5 = array4;
					for (int j = 0; j < array5.Length; j++)
					{
						string text4 = array5[j];
						string[] array6 = text4.Split(this.sep1, StringSplitOptions.None);
						bool flag8 = array6[0] != "" && array6[0] != " ";
						if (flag8)
						{
							bool flag9 = int.Parse(array6[6]) + 1296000 > this.timeSmp;
							if (flag9)
							{
								mailData mailData2 = new mailData();
								mailData2.frmcid = int.Parse(array6[0]);
								mailData2.frmsex = int.Parse(array6[1]);
								mailData2.cid = int.Parse(array6[2]);
								mailData2.time = array6[3];
								mailData2.frmname = array6[4];
								mailData2.msg = array6[5];
								mailData2.clanc = int.Parse(array6[7]);
								mailData2.str = text4 + "#)#&";
								this.familyMailDic.Add(mailData2);
								this.famLocalStr.Add(mailData2);
							}
						}
					}
					this.saveLocalData(this.famLocalStr, 3);
				}
				UIClient.instance.addEventListener(9001u, new Action<GameEvent>(this.onEndLoadItem));
			}
		}

		public void saveLocalData(List<mailData> list, int type)
		{
			int num = list.Count - 100;
			bool flag = num > 0;
			if (flag)
			{
				for (int i = 0; i < num; i++)
				{
					list.Remove(list[0]);
				}
			}
			string text = "";
			foreach (mailData current in list)
			{
				text += current.str;
			}
			if (type != 3)
			{
				if (type == 4)
				{
					FileMgr.saveString(FileMgr.TYPE_MAIL, "per", text);
				}
			}
			else
			{
				FileMgr.saveString(FileMgr.TYPE_MAIL, "fam", text);
			}
		}

		public void onSendMail(int type, int cid = 0, string name = null)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(type);
			bool flag = type == 4;
			if (flag)
			{
				arrayList.Add(cid);
				arrayList.Add(name);
			}
			InterfaceMgr.getInstance().open(InterfaceMgr.MAILPAPER, arrayList, false);
		}

		private int ConvertDateTimeInt(DateTime time)
		{
			DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return (int)(time - d).TotalSeconds;
		}

		public void getGiftCard(GameEvent e)
		{
			bool giftCardData = this.getGiftCardData();
			if (giftCardData)
			{
				bool flag = !BaseProxy<E_mailProxy>.getInstance().isNotice;
				if (flag)
				{
					BaseProxy<E_mailProxy>.getInstance().isNotice = true;
				}
			}
		}

		public bool getGiftCardData()
		{
			List<GiftCardData> giftCards = HttpAppMgr.instance.getGiftCards();
			this.giftCardDataDic.Clear();
			bool flag = false;
			bool flag2 = giftCards.Count == 0;
			bool result;
			if (flag2)
			{
				result = flag;
			}
			else
			{
				foreach (GiftCardData current in giftCards)
				{
					bool flag3 = current.cardType.functp == 4;
					if (flag3)
					{
						mailData mailData = new mailData();
						mailData.type = 1;
						mailData.code = current.code;
						mailData.acttm = (float)current.cardType.acttm;
						mailData.money = current.cardType.money;
						mailData.yb = current.cardType.golden;
						mailData.yinpiao = current.cardType.yinpiao;
						mailData.msg = current.cardType.desc;
						debug.Log(mailData.msg);
						bool flag4 = mailData.msg == "";
						if (flag4)
						{
							mailData.msg = current.cardType.name;
							debug.Log(mailData.msg + "名字");
						}
						mailData.seconds = current.cardType.acttm;
						mailData.time = BaseProxy<E_mailProxy>.getInstance().getTime(mailData.seconds.ToString());
						bool flag5 = current.cardType.lItem != null;
						if (flag5)
						{
							foreach (BaseItemData current2 in current.cardType.lItem)
							{
								mailItemData item = default(mailItemData);
								item.id = int.Parse(current2.id);
								item.count = current2.num;
								item.type = 1;
								mailData.items.Add(item);
							}
						}
						this.systemMailDic.Add(mailData);
						this.giftCardDataDic.Add(mailData, current);
						flag = true;
					}
				}
				result = flag;
			}
			return result;
		}

		private void onEndLoadItem(GameEvent e)
		{
		}
	}
}
