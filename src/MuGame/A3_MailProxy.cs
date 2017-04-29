using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MuGame
{
	internal class A3_MailProxy : BaseProxy<A3_MailProxy>
	{
		public static uint MAIL_NEW_MAIL = 0u;

		public static uint MAIL_NEW_MAIL_CONTENT = 1u;

		public static uint MAIL_GET_ATTACHMENT = 2u;

		public static uint MAIL_REMOVE_ONE = 3u;

		public static uint MAIL_GET_ALL = 4u;

		public static uint MAIL_DELETE_ALL = 5u;

		public A3_MailProxy()
		{
			this.addProxyListener(152u, new Action<Variant>(this.OnMail));
		}

		public void GetMails()
		{
			Variant variant = new Variant();
			variant["mail_cmd"] = 1;
			this.sendRPC(152u, variant);
		}

		public void GetMailContent(uint mailid)
		{
			Variant variant = new Variant();
			variant["mail_cmd"] = 2;
			variant["mailid"] = mailid;
			this.sendRPC(152u, variant);
		}

		public void GetMailAttachment(uint mailid)
		{
			Variant variant = new Variant();
			variant["mail_cmd"] = 3;
			variant["mailid"] = mailid;
			this.sendRPC(152u, variant);
		}

		public void RemoveMail(uint mailid)
		{
			Variant variant = new Variant();
			variant["mail_cmd"] = 4;
			variant["mailid"] = mailid;
			this.sendRPC(152u, variant);
		}

		public void GetAllAttachment()
		{
			Variant variant = new Variant();
			variant["mail_cmd"] = 5;
			this.sendRPC(152u, variant);
		}

		public void DeleteAll()
		{
			Variant variant = new Variant();
			variant["mail_cmd"] = 6;
			this.sendRPC(152u, variant);
		}

		public void OnMail(Variant data)
		{
			debug.Log("邮件信息：" + data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			switch (num)
			{
			case 1:
				this.OnGetMailList(data);
				break;
			case 2:
				this.OnGetOneMail(data);
				break;
			case 3:
				this.OnGetAttachment(data);
				break;
			case 4:
				this.OnRemoveMail(data);
				break;
			case 5:
				this.OnGetAll(data);
				break;
			case 6:
				this.OnDeleteAll(data);
				break;
			case 7:
				this.OnNewMail(data);
				break;
			}
		}

		private void OnGetMailList(Variant data)
		{
			A3_MailModel instance = ModelBase<A3_MailModel>.getInstance();
			instance.mail_simple.Clear();
			Variant variant = data["mails"];
			foreach (Variant current in variant._arr)
			{
				A3_MailSimple a3_MailSimple = new A3_MailSimple();
				a3_MailSimple.id = current["id"];
				a3_MailSimple.tm = current["tm"];
				a3_MailSimple.tp = ContMgr.getCont("mail_type_" + current["tp"], null);
				a3_MailSimple.got_itm = current["got_itm"];
				a3_MailSimple.flag = current["flag"];
				a3_MailSimple.title = this.ConvertString(current["title"], "mail_title_");
				a3_MailSimple.has_itm = current["has_itm"];
				instance.mail_simple[a3_MailSimple.id] = a3_MailSimple;
			}
		}

		private void OnGetOneMail(Variant data)
		{
			A3_MailModel instance = ModelBase<A3_MailModel>.getInstance();
			uint num = data["id"];
			bool flag = instance.mail_details.ContainsKey(num);
			if (flag)
			{
				instance.mail_details.Remove(num);
			}
			A3_MailDetail a3_MailDetail = new A3_MailDetail();
			a3_MailDetail.ms = instance.mail_simple[num];
			a3_MailDetail.ms.id = num;
			a3_MailDetail.ms.tp = ContMgr.getCont("mail_type_" + data["tp"], null);
			a3_MailDetail.ms.tm = data["tm"];
			a3_MailDetail.ms.got_itm = data["got_itm"];
			a3_MailDetail.ms.flag = true;
			a3_MailDetail.msg = this.ConvertString(data["msg"], "mail_content_");
			a3_MailDetail.itms = new List<a3_BagItemData>();
			bool flag2 = data.ContainsKey("itm");
			if (flag2)
			{
				Variant variant = data["itm"];
				bool flag3 = variant.ContainsKey("money");
				if (flag3)
				{
					a3_MailDetail.money = variant["money"];
				}
				bool flag4 = variant.ContainsKey("yb");
				if (flag4)
				{
					a3_MailDetail.yb = variant["yb"];
				}
				bool flag5 = variant.ContainsKey("bndyb");
				if (flag5)
				{
					a3_MailDetail.bndyb = variant["bndyb"];
				}
				bool flag6 = variant.ContainsKey("itm");
				if (flag6)
				{
					Variant variant2 = variant["itm"];
					foreach (Variant current in variant2._arr)
					{
						a3_BagItemData a3_BagItemData = default(a3_BagItemData);
						a3_BagItemData.tpid = current["tpid"];
						a3_BagItemData.num = current["cnt"];
						a3_BagItemData.confdata = ModelBase<a3_BagModel>.getInstance().getItemDataById(a3_BagItemData.tpid);
						a3_MailDetail.itms.Add(a3_BagItemData);
					}
				}
				bool flag7 = variant.ContainsKey("eqp");
				if (flag7)
				{
					Variant variant3 = variant["eqp"];
					foreach (Variant current2 in variant3._arr)
					{
						a3_BagItemData a3_BagItemData2 = default(a3_BagItemData);
						bool flag8 = current2.ContainsKey("tpid");
						if (flag8)
						{
							a3_BagItemData2.tpid = current2["tpid"];
						}
						bool flag9 = current2.ContainsKey("bnd");
						if (flag9)
						{
							a3_BagItemData2.bnd = current2["bnd"];
						}
						a3_BagItemData2 = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData2, current2);
						a3_BagItemData2.confdata = ModelBase<a3_BagModel>.getInstance().getItemDataById(a3_BagItemData2.tpid);
						a3_MailDetail.itms.Add(a3_BagItemData2);
					}
				}
			}
			instance.mail_details[num] = a3_MailDetail;
			base.dispatchEvent(GameEvent.Create(A3_MailProxy.MAIL_NEW_MAIL_CONTENT, this, num, false));
		}

		private void OnGetAttachment(Variant data)
		{
			uint num = data["id"];
			A3_MailModel instance = ModelBase<A3_MailModel>.getInstance();
			A3_MailSimple a3_MailSimple = instance.mail_simple[num];
			A3_MailDetail a3_MailDetail = instance.mail_details[num];
			a3_MailSimple.got_itm = true;
			a3_MailDetail.ms.got_itm = true;
			base.dispatchEvent(GameEvent.Create(A3_MailProxy.MAIL_GET_ATTACHMENT, this, num, false));
			BaseProxy<A3_MailProxy>.getInstance().RemoveMail(num);
		}

		private void OnRemoveMail(Variant data)
		{
			uint num = data["id"];
			A3_MailModel instance = ModelBase<A3_MailModel>.getInstance();
			instance.mail_simple.Remove(num);
			instance.mail_details.Remove(num);
			base.dispatchEvent(GameEvent.Create(A3_MailProxy.MAIL_REMOVE_ONE, this, num, false));
		}

		private void OnGetAll(Variant data)
		{
			A3_MailModel instance = ModelBase<A3_MailModel>.getInstance();
			Variant variant = data["ids"];
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					uint key = enumerator.Current;
					instance.mail_simple[key].got_itm = true;
					bool flag = instance.mail_details.ContainsKey(key);
					if (flag)
					{
						instance.mail_details[key].ms.got_itm = true;
					}
				}
			}
			bool flag2 = variant._arr.Count > 0 || data["itm"]["itm"]._arr.Count > 0;
			if (flag2)
			{
				flytxt.instance.fly(ContMgr.getCont("mail_hint_7", null), 0, default(Color), null);
			}
			base.dispatchEvent(GameEvent.Create(A3_MailProxy.MAIL_GET_ALL, this, null, false));
		}

		private void OnDeleteAll(Variant data)
		{
			A3_MailModel instance = ModelBase<A3_MailModel>.getInstance();
			Variant variant = data["ids"];
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					uint key = enumerator.Current;
					bool flag = instance.mail_simple.ContainsKey(key);
					if (flag)
					{
						instance.mail_simple.Remove(key);
					}
					bool flag2 = instance.mail_details.ContainsKey(key);
					if (flag2)
					{
						instance.mail_details[key].ms.got_itm = true;
					}
				}
			}
			base.dispatchEvent(GameEvent.Create(A3_MailProxy.MAIL_DELETE_ALL, this, data, false));
		}

		private void OnNewMail(Variant mail)
		{
			A3_MailModel instance = ModelBase<A3_MailModel>.getInstance();
			A3_MailSimple a3_MailSimple = new A3_MailSimple();
			a3_MailSimple.id = mail["id"];
			a3_MailSimple.tm = mail["tm"];
			a3_MailSimple.tp = ContMgr.getCont("mail_type_" + mail["tp"], null);
			a3_MailSimple.got_itm = false;
			a3_MailSimple.flag = mail["flag"];
			a3_MailSimple.title = this.ConvertString(mail["title"], "mail_title_");
			a3_MailSimple.has_itm = mail["has_itm"];
			bool flag = instance.mail_simple.ContainsKey(a3_MailSimple.id);
			if (flag)
			{
				instance.mail_simple.Remove(a3_MailSimple.id);
			}
			instance.mail_simple[a3_MailSimple.id] = a3_MailSimple;
			base.dispatchEvent(GameEvent.Create(A3_MailProxy.MAIL_NEW_MAIL, this, a3_MailSimple, false));
			a3_mail.expshowid = (int)a3_MailSimple.id;
		}

		private string ConvertString(string svrStr, string contPrefix)
		{
			string[] array = svrStr.Split(new char[]
			{
				'#'
			});
			bool flag = !array.Any<string>();
			string result;
			if (flag)
			{
				result = ContMgr.getCont(contPrefix + "0", null);
			}
			else
			{
				bool flag2 = array.Length == 1;
				if (flag2)
				{
					result = svrStr;
				}
				else
				{
					string str = array[0];
					List<string> list = new List<string>();
					for (int i = 1; i < array.Count<string>(); i++)
					{
						list.Add(array[i]);
					}
					result = ContMgr.getCont(contPrefix + str, list);
				}
			}
			return result;
		}
	}
}
