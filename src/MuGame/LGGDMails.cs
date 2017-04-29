using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class LGGDMails : lgGDBase
	{
		protected Variant _mail_list = new Variant();

		protected int _glbmailrd = -1;

		private LGIUIMail _uiMail;

		private LGIUIMail uiMail
		{
			get
			{
				bool flag = this._uiMail == null;
				if (flag)
				{
					this._uiMail = (this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail);
				}
				return this._uiMail;
			}
		}

		public LGGDMails(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGDMails(m as gameManager);
		}

		public override void init()
		{
		}

		public Variant getMailList()
		{
			return this._mail_list;
		}

		public void set_glbMailList(Variant msgData)
		{
			bool flag = msgData.ContainsKey("glbmailrd");
			if (flag)
			{
				this._glbmailrd = msgData["glbmailrd"];
			}
			Variant variant = msgData["mails"];
			for (int i = 0; i < variant.Length; i++)
			{
				bool flag2 = this.mailInLocal(variant[i]["id"]);
				if (!flag2)
				{
					variant[i]["frmcid"] = 0;
					variant[i]["glbmail"] = 1;
					bool flag3 = variant[i]["id"]._int < this._glbmailrd;
					if (flag3)
					{
						variant[i]["flag"] = (variant[i]["flag"] | 1);
					}
					this._mail_list._arr.Add(variant[i]);
				}
			}
			LGIUIMail lGIUIMail = this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail;
			bool flag4 = lGIUIMail == null;
			if (!flag4)
			{
				lGIUIMail.refreshMailList(this._mail_list);
			}
		}

		public void set_ptMailList(Variant msgData)
		{
			Variant variant = msgData["mails"];
			for (int i = 0; i < variant.Length; i++)
			{
				bool flag = this.mailInLocal(variant[i]["id"]);
				if (!flag)
				{
					this._mail_list._arr.Add(variant[i]);
				}
			}
			LGIUIMail lGIUIMail = this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail;
			bool flag2 = lGIUIMail == null;
			if (!flag2)
			{
				lGIUIMail.refreshMailList(this._mail_list);
			}
		}

		public void get_glbMail(Variant msgData)
		{
			this._glbmailrd = msgData["id"]._int;
			for (int i = 0; i < this._mail_list.Length; i++)
			{
				bool flag = this._mail_list[i]["id"]._int == msgData["id"]._int;
				if (flag)
				{
					this._mail_list[i]["flag"] = (this._mail_list[i]["flag"] | 1);
					break;
				}
			}
			msgData["frmcid"] = 0;
			msgData["flag"] = (msgData["flag"] | 1);
			msgData["frmnm"] = LanguagePack.getLanguageText("chat", "sys");
			LGIUIMail lGIUIMail = this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail;
			bool flag2 = lGIUIMail == null;
			if (!flag2)
			{
				lGIUIMail.openMail(msgData);
			}
		}

		public void get_ptMail(Variant msgData)
		{
			for (int i = 0; i < this._mail_list.Length; i++)
			{
				bool flag = this._mail_list[i]["id"]._int == msgData["id"]._int;
				if (flag)
				{
					this._mail_list[i]["flag"] = msgData["flag"];
					break;
				}
			}
			LGIUIMail lGIUIMail = this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail;
			bool flag2 = lGIUIMail == null;
			if (!flag2)
			{
				lGIUIMail.openMail(msgData);
			}
		}

		public void got_newGlbMail(Variant msgData)
		{
			int mailid = 0;
			bool flag = msgData.ContainsKey("id");
			if (flag)
			{
				mailid = msgData["id"];
			}
			bool flag2 = this.mailInLocal(mailid);
			if (!flag2)
			{
				this._glbmailrd = msgData["id"]._int;
				msgData["frmcid"] = 0;
				msgData["flag"] = 0;
				msgData["glbmail"]["glbmail"] = 1;
				this._mail_list._arr.Add(msgData["glbmail"]);
				LGIUINotify lGIUINotify = this.g_mgr.g_uiM.getLGUI("LGUINotifyImpl") as LGIUINotify;
				lGIUINotify.notifyNewMail(msgData, true);
				LGIUIMail lGIUIMail = this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail;
				bool flag3 = lGIUIMail == null;
				if (!flag3)
				{
					lGIUIMail.refreshMailList(this._mail_list);
				}
			}
		}

		public void got_newPtMail(Variant msgData)
		{
			int mailid = 0;
			bool flag = msgData.ContainsKey("id");
			if (flag)
			{
				mailid = msgData["id"];
			}
			bool flag2 = this.mailInLocal(mailid);
			if (!flag2)
			{
				this._mail_list._arr.Add(msgData["mail"]);
				LGIUINotify lGIUINotify = this.g_mgr.g_uiM.getLGUI("LGUINotifyImpl") as LGIUINotify;
				lGIUINotify.notifyNewMail(msgData, true);
				LGIUIMail lGIUIMail = this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail;
				bool flag3 = lGIUIMail == null;
				if (!flag3)
				{
					lGIUIMail.refreshMailList(this._mail_list);
				}
			}
		}

		public void lockMail(Variant msgData)
		{
			for (int i = 0; i < this._mail_list.Length; i++)
			{
				bool flag = this._mail_list[i]["id"] == msgData["id"];
				if (flag)
				{
					this._mail_list[i]["flag"] = msgData["flag"];
					break;
				}
			}
			LGIUIMail lGIUIMail = this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail;
			bool flag2 = lGIUIMail == null;
			if (!flag2)
			{
				bool flag3 = (msgData["flag"] & 2) == 2;
				if (flag3)
				{
					lGIUIMail.lockMail(msgData["id"]._uint);
				}
				else
				{
					lGIUIMail.unlockMail(msgData["id"]._uint);
				}
			}
		}

		public void getMailItem(int mailID)
		{
			for (int i = 0; i < this._mail_list.Length; i++)
			{
				bool flag = this._mail_list[i]["id"]._int == mailID;
				if (flag)
				{
					this._mail_list[i]["flag"] = (this._mail_list[i]["flag"] & 3);
					this._mail_list[i].RemoveKey("itm");
					break;
				}
			}
			LGIUIMail lGIUIMail = this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail;
			bool flag2 = lGIUIMail == null;
			if (!flag2)
			{
				lGIUIMail.delMailItems((uint)mailID);
			}
		}

		public void delMailSuccess(int mailID)
		{
			for (int i = 0; i < this._mail_list.Length; i++)
			{
				bool flag = this._mail_list[i]["id"]._int == mailID;
				if (flag)
				{
					this._mail_list._arr.RemoveAt(i);
					break;
				}
			}
			LGIUIMail lGIUIMail = this.g_mgr.g_uiM.getLGUI("LGUIMailImpl") as LGIUIMail;
			bool flag2 = lGIUIMail == null;
			if (!flag2)
			{
				lGIUIMail.refreshMailList(this._mail_list);
			}
		}

		public void DelMailByCardTp(int tp)
		{
			for (int i = 0; i < this._mail_list.Length; i++)
			{
				bool flag = this._mail_list[i]["tp"]._int == tp;
				if (flag)
				{
					this._mail_list._arr.RemoveAt(i);
					break;
				}
			}
			bool flag2 = this.uiMail != null;
			if (flag2)
			{
				this.uiMail.refreshMailList(this._mail_list);
			}
		}

		public void ReadMailByCardTp(int tp)
		{
			for (int i = 0; i < this._mail_list.Length; i++)
			{
				Variant variant = this._mail_list[i];
				bool flag = variant["tp"]._int == tp;
				if (flag)
				{
					bool flag2 = (variant["flag"] & 1) != 1;
					if (flag2)
					{
						Variant variant2 = variant;
						variant2["flag"] = variant2["flag"] + 1;
					}
					break;
				}
			}
		}

		private bool mailInLocal(int mailid)
		{
			bool result;
			for (int i = 0; i < this._mail_list.Length; i++)
			{
				bool flag = this._mail_list[i]["id"]._int == mailid;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
	}
}
