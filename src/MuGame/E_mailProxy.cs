using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class E_mailProxy : BaseProxy<E_mailProxy>
	{
		private const uint C2S_READ_MAIL = 153u;

		private const uint c2s_get_mail_item = 154u;

		private const uint c2s_get_mail_list = 152u;

		private const uint c2s_send_mail = 155u;

		private const uint c2s_send_family_mail = 227u;

		private const uint c2s_delete_mail = 158u;

		private const uint c2s_fetch_itm_card = 22u;

		public static uint lis_sendMail_res = 155u;

		public static uint lis_unreadType = 153u;

		public static uint lis_deleteMail_res = 158u;

		public static uint lis_get_new_mail = 154u;

		public static uint lis_get_item = 157u;

		public List<int> newType = new List<int>();

		private bool isApplied = false;

		public bool isNotice = false;

		public E_mailProxy()
		{
			this.addProxyListener(152u, new Action<Variant>(this.onloadE_mail));
			this.addProxyListener(158u, new Action<Variant>(this.replayOnDelete));
			this.addProxyListener(154u, new Action<Variant>(this.getNewMail));
			this.addProxyListener(155u, new Action<Variant>(this.replayOnSend));
			this.addProxyListener(153u, new Action<Variant>(this.getUnreadType));
			this.addProxyListener(157u, new Action<Variant>(this.replayOnGetItem));
			this.sendLoadmail();
		}

		public void sendLoadmail()
		{
			debug.Log("发送152消息");
			this.sendRPC(152u, null);
		}

		public void readMail(uint type)
		{
			debug.Log("发送153消息");
			Variant variant = new Variant();
			variant["tp"] = type;
			this.sendRPC(153u, variant);
		}

		public void getItem(uint id)
		{
			debug.Log("发送154消息");
			Variant variant = new Variant();
			variant["mailid"] = id;
			this.sendRPC(154u, variant);
		}

		public void fetch_itm_card(int cid)
		{
			Variant variant = new Variant();
			variant["cardid"] = cid.ToString();
			this.sendRPC(22u, variant);
		}

		public void sendNewMail(int type, string msg, uint cid = 0u)
		{
			debug.Log("发送邮件155");
			Variant variant = new Variant();
			bool flag = type == 4;
			if (flag)
			{
				variant["tp"] = type;
				variant["tocid"] = cid;
				variant["msg"] = msg;
				this.sendRPC(155u, variant);
			}
			bool flag2 = type == 2 || type == 1;
			if (flag2)
			{
				variant["send_tp"] = type;
				variant["msg"] = msg;
				this.sendRPC(227u, variant);
			}
		}

		public void deleteMail(uint id)
		{
			debug.Log("发送158消息");
			Variant variant = new Variant();
			variant["mailid"] = id;
			this.sendRPC(158u, variant);
		}

		private void loadMailItems(Variant mail, mailData dta)
		{
			bool flag = mail.ContainsKey("itm");
			if (flag)
			{
				bool flag2 = mail["itm"].ContainsKey("money");
				if (flag2)
				{
					dta.money = mail["itm"]["money"];
				}
				bool flag3 = mail["itm"].ContainsKey("yb");
				if (flag3)
				{
					dta.yb = mail["itm"]["yb"];
				}
				bool flag4 = mail["itm"].ContainsKey("itm");
				if (flag4)
				{
					bool flag5 = mail["itm"]["itm"]._arr.Count != 0;
					if (flag5)
					{
						foreach (Variant current in mail["itm"]["itm"]._arr)
						{
							mailItemData item = default(mailItemData);
							item.id = current["item_id"];
							item.count = current["cnt"];
							item.type = 1;
							dta.items.Add(item);
						}
					}
				}
				bool flag6 = mail["itm"].ContainsKey("dress");
				if (flag6)
				{
					bool flag7 = mail["itm"]["dress"]._arr.Count != 0;
					if (flag7)
					{
						foreach (Variant current2 in mail["itm"]["dress"]._arr)
						{
							mailItemData item2 = default(mailItemData);
							item2.id = current2["id"];
							item2.count = current2["cnt"];
							item2.type = 2;
							dta.items.Add(item2);
						}
					}
				}
			}
		}

		private void onloadE_mail(Variant data)
		{
			this.isApplied = true;
			debug.Log("获取邮件列表");
			debug.Log(data.dump());
			foreach (Variant current in data["mails"]._arr)
			{
				mailData mailData = new mailData();
				mailData.type = current["tp"];
				mailData.id = current["id"];
				mailData.time = this.getTime(current["tm"]);
				mailData.seconds = current["tm"]._int32;
				mailData.flag = current["flag"];
				mailData.msg = KeyWord.filter(current["msg"]);
				this.loadMailItems(current, mailData);
				switch (mailData.type)
				{
				case 1:
					ModelBase<E_mailModel>.getInstance().systemMailDic.Add(mailData);
					break;
				case 2:
					ModelBase<E_mailModel>.getInstance().gameMailDic.Add(mailData);
					break;
				case 3:
				{
					bool flag = current["frmpinfo"].ContainsKey("cid");
					if (flag)
					{
						mailData.frmcid = current["frmpinfo"]["cid"];
					}
					bool flag2 = current["frmpinfo"].ContainsKey("name");
					if (flag2)
					{
						mailData.frmname = current["frmpinfo"]["name"];
					}
					bool flag3 = current["frmpinfo"].ContainsKey("sex");
					if (flag3)
					{
						mailData.frmsex = current["frmpinfo"]["sex"];
					}
					bool flag4 = current["frmpinfo"].ContainsKey("clanc");
					if (flag4)
					{
						mailData.clanc = current["frmpinfo"]["clanc"];
					}
					ModelBase<E_mailModel>.getInstance().familyMailDic.Add(mailData);
					bool flag5 = false;
					foreach (int current2 in this.newType)
					{
						bool flag6 = current2 == 3;
						if (flag6)
						{
							flag5 = true;
							break;
						}
					}
					bool flag7 = !flag5;
					if (flag7)
					{
						this.newType.Add(3);
					}
					string str = string.Concat(new object[]
					{
						mailData.frmcid.ToString(),
						"#!#&",
						mailData.frmsex.ToString(),
						"#!#&",
						mailData.cid.ToString(),
						"#!#&",
						mailData.time,
						"#!#&",
						mailData.frmname,
						"#!#&",
						mailData.msg,
						"#!#&",
						mailData.seconds,
						"#!#&",
						mailData.clanc,
						"#)#&"
					});
					mailData.str = str;
					ModelBase<E_mailModel>.getInstance().famLocalStr.Add(mailData);
					break;
				}
				case 4:
				{
					bool flag8 = current["frmpinfo"].ContainsKey("cid");
					if (flag8)
					{
						mailData.frmcid = current["frmpinfo"]["cid"];
					}
					bool flag9 = current["frmpinfo"].ContainsKey("name");
					if (flag9)
					{
						mailData.frmname = current["frmpinfo"]["name"];
					}
					bool flag10 = current["frmpinfo"].ContainsKey("sex");
					if (flag10)
					{
						mailData.frmsex = current["frmpinfo"]["sex"];
					}
					bool flag11 = current["frmpinfo"].ContainsKey("clanc");
					if (flag11)
					{
						mailData.clanc = current["frmpinfo"]["clanc"];
					}
					bool flag12 = ModelBase<E_mailModel>.getInstance().personalMailDic.ContainsKey(mailData.frmcid);
					if (flag12)
					{
						ModelBase<E_mailModel>.getInstance().personalMailDic[mailData.frmcid].Add(mailData);
					}
					else
					{
						List<mailData> list = new List<mailData>();
						list.Add(mailData);
						ModelBase<E_mailModel>.getInstance().personalMailDic.Add(mailData.frmcid, list);
						base.dispatchEvent(GameEvent.Create(E_mailProxy.lis_get_new_mail, this, null, false));
					}
					bool flag13 = false;
					foreach (int current3 in this.newType)
					{
						bool flag14 = current3 == mailData.frmcid;
						if (flag14)
						{
							flag13 = true;
							break;
						}
					}
					bool flag15 = !flag13;
					if (flag15)
					{
						this.newType.Add(mailData.frmcid);
					}
					string str2 = string.Concat(new object[]
					{
						mailData.frmcid.ToString(),
						"#!#&",
						mailData.frmsex.ToString(),
						"#!#&",
						mailData.cid.ToString(),
						"#!#&",
						mailData.time,
						"#!#&",
						mailData.frmname,
						"#!#&",
						mailData.msg,
						"#!#&",
						mailData.seconds,
						"#!#&",
						mailData.clanc,
						"#)#&"
					});
					mailData.str = str2;
					ModelBase<E_mailModel>.getInstance().perLocalStr.Add(mailData);
					break;
				}
				}
			}
			ModelBase<E_mailModel>.getInstance().saveLocalData(ModelBase<E_mailModel>.getInstance().famLocalStr, 3);
			ModelBase<E_mailModel>.getInstance().saveLocalData(ModelBase<E_mailModel>.getInstance().perLocalStr, 4);
		}

		private void getNewMail(Variant data)
		{
			bool flag = !this.isApplied;
			if (!flag)
			{
				debug.Log("获取新邮件");
				debug.Log(data.dump());
				Variant variant = new Variant();
				variant["tp"] = data["mail"]["tp"];
				bool flag2 = variant["tp"] == 4;
				if (flag2)
				{
					variant["secondType"] = data["mail"]["frmpinfo"]["cid"];
				}
				base.dispatchEvent(GameEvent.Create(E_mailProxy.lis_unreadType, this, variant, false));
				mailData mailData = new mailData();
				mailData.type = data["mail"]["tp"];
				mailData.id = data["mail"]["id"];
				mailData.time = this.getTime(data["mail"]["tm"]);
				mailData.seconds = data["mail"]["tm"]._int32;
				mailData.msg = KeyWord.filter(data["mail"]["msg"]);
				bool flag3 = data["mail"]["frmpinfo"].ContainsKey("cid");
				if (flag3)
				{
					mailData.frmcid = data["mail"]["frmpinfo"]["cid"];
				}
				bool flag4 = data["mail"]["frmpinfo"].ContainsKey("name");
				if (flag4)
				{
					mailData.frmname = data["mail"]["frmpinfo"]["name"];
					bool flag5 = mailData.type == 3 && (long)mailData.frmcid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid);
					if (flag5)
					{
						base.dispatchEvent(GameEvent.Create(E_mailProxy.lis_sendMail_res, this, variant, false));
					}
				}
				bool flag6 = data["mail"]["frmpinfo"].ContainsKey("sex");
				if (flag6)
				{
					mailData.frmsex = data["mail"]["frmpinfo"]["sex"];
				}
				bool flag7 = data["mail"]["frmpinfo"].ContainsKey("clanc");
				if (flag7)
				{
					mailData.clanc = data["mail"]["frmpinfo"]["clanc"];
				}
				this.loadMailItems(data["mail"], mailData);
				switch (mailData.type)
				{
				case 1:
				{
					ModelBase<E_mailModel>.getInstance().systemMailDic.Add(mailData);
					bool flag8 = !this.isNotice;
					if (flag8)
					{
						this.isNotice = true;
					}
					break;
				}
				case 2:
				{
					bool flag9 = false;
					foreach (int current in this.newType)
					{
						bool flag10 = current == 2;
						if (flag10)
						{
							flag9 = true;
							break;
						}
					}
					bool flag11 = !flag9;
					if (flag11)
					{
						this.newType.Add(2);
					}
					ModelBase<E_mailModel>.getInstance().gameMailDic.Add(mailData);
					bool flag12 = !this.isNotice;
					if (flag12)
					{
						this.isNotice = true;
					}
					break;
				}
				case 3:
				{
					ModelBase<E_mailModel>.getInstance().familyMailDic.Add(mailData);
					string str = string.Concat(new object[]
					{
						mailData.frmcid.ToString(),
						"#!#&",
						mailData.frmsex.ToString(),
						"#!#&",
						mailData.cid.ToString(),
						"#!#&",
						mailData.time,
						"#!#&",
						mailData.frmname,
						"#!#&",
						mailData.msg,
						"#!#&",
						mailData.seconds,
						"#!#&",
						mailData.clanc,
						"#)#&"
					});
					mailData.str = str;
					ModelBase<E_mailModel>.getInstance().famLocalStr.Add(mailData);
					ModelBase<E_mailModel>.getInstance().saveLocalData(ModelBase<E_mailModel>.getInstance().famLocalStr, 3);
					bool flag13 = false;
					foreach (int current2 in this.newType)
					{
						bool flag14 = current2 == 3;
						if (flag14)
						{
							flag13 = true;
							break;
						}
					}
					bool flag15 = !flag13;
					if (flag15)
					{
						this.newType.Add(3);
					}
					bool flag16 = !this.isNotice;
					if (flag16)
					{
						this.isNotice = true;
					}
					break;
				}
				case 4:
				{
					bool flag17 = ModelBase<E_mailModel>.getInstance().personalMailDic.ContainsKey(mailData.frmcid);
					if (flag17)
					{
						ModelBase<E_mailModel>.getInstance().personalMailDic[mailData.frmcid].Add(mailData);
					}
					else
					{
						List<mailData> list = new List<mailData>();
						list.Add(mailData);
						ModelBase<E_mailModel>.getInstance().personalMailDic.Add(mailData.frmcid, list);
						base.dispatchEvent(GameEvent.Create(E_mailProxy.lis_get_new_mail, this, null, false));
					}
					bool flag18 = false;
					foreach (int current3 in this.newType)
					{
						bool flag19 = current3 == mailData.frmcid;
						if (flag19)
						{
							flag18 = true;
							break;
						}
					}
					bool flag20 = !flag18;
					if (flag20)
					{
						this.newType.Add(mailData.frmcid);
					}
					bool flag21 = !this.isNotice;
					if (flag21)
					{
						this.isNotice = true;
					}
					string str2 = string.Concat(new object[]
					{
						mailData.frmcid.ToString(),
						"#!#&",
						mailData.frmsex.ToString(),
						"#!#&",
						mailData.cid.ToString(),
						"#!#&",
						mailData.time,
						"#!#&",
						mailData.frmname,
						"#!#&",
						mailData.msg,
						mailData.seconds,
						"#!#&",
						mailData.clanc,
						"#)#&"
					});
					mailData.str = str2;
					ModelBase<E_mailModel>.getInstance().perLocalStr.Add(mailData);
					ModelBase<E_mailModel>.getInstance().saveLocalData(ModelBase<E_mailModel>.getInstance().perLocalStr, 4);
					break;
				}
				}
			}
		}

		private void getUnreadType(Variant data)
		{
			debug.Log("收到未读邮件++++++++++++++++++" + data.dump());
			foreach (Variant current in data["new_mail"]._arr)
			{
				int num = current["tp"];
				int num2 = current["cnt"];
				bool flag = num2 != 0;
				if (flag)
				{
					this.isNotice = true;
					bool flag2 = num != 4;
					if (flag2)
					{
						this.newType.Add(num);
					}
				}
			}
		}

		private void replayOnSend(Variant data)
		{
			debug.Log("收到发送结果" + data.dump());
			base.dispatchEvent(GameEvent.Create(E_mailProxy.lis_sendMail_res, this, data, false));
		}

		private void replayOnDelete(Variant data)
		{
			debug.Log("收到删除结果" + data.dump());
			base.dispatchEvent(GameEvent.Create(E_mailProxy.lis_deleteMail_res, this, data, false));
		}

		private void replayOnGetItem(Variant data)
		{
			debug.Log("收取物品结果" + data.dump());
			base.dispatchEvent(GameEvent.Create(E_mailProxy.lis_get_item, this, data, false));
		}

		public string getTime(string _time)
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = long.Parse(_time + "0000000");
			TimeSpan value = new TimeSpan(ticks);
			DateTime dateTime2 = dateTime.Add(value);
			string text = dateTime2.ToShortDateString().ToString();
			string text2 = dateTime2.ToLongTimeString().ToString();
			string[] array = text.Split(new char[]
			{
				'/'
			});
			string[] array2 = text2.Split(new char[]
			{
				':',
				' '
			});
			string result = "";
			bool flag = array2.Length == 3;
			if (flag)
			{
				result = string.Concat(new string[]
				{
					array[2],
					"年",
					array[0],
					"月",
					array[1],
					"日 ",
					array2[0],
					"时",
					array2[1],
					"分"
				});
			}
			else
			{
				bool flag2 = array2.Length == 4;
				if (flag2)
				{
					bool flag3 = array2[3] == "PM";
					if (flag3)
					{
						result = string.Concat(new string[]
						{
							array[2],
							"年",
							array[0],
							"月",
							array[1],
							"日 ",
							(int.Parse(array2[0]) + 12).ToString(),
							"时",
							array2[1],
							"分"
						});
					}
					else
					{
						result = string.Concat(new string[]
						{
							array[2],
							"年",
							array[0],
							"月",
							array[1],
							"日 ",
							array2[0],
							"时",
							array2[1],
							"分"
						});
					}
				}
			}
			return result;
		}
	}
}
