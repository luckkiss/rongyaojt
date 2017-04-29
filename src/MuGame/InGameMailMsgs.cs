using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameMailMsgs : MsgProcduresBase
	{
		public InGameMailMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameMailMsgs create(IClientBase m)
		{
			return new InGameMailMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(152u, new NetManager.RPCProcCreator(on_get_mail_list.create));
			this.g_mgr.regRPCProcesser(153u, new NetManager.RPCProcCreator(on_get_mail.create));
			this.g_mgr.regRPCProcesser(154u, new NetManager.RPCProcCreator(on_got_new_mail.create));
			this.g_mgr.regRPCProcesser(155u, new NetManager.RPCProcCreator(on_send_mail_res.create));
			this.g_mgr.regRPCProcesser(157u, new NetManager.RPCProcCreator(on_get_mail_item_res.create));
			this.g_mgr.regRPCProcesser(158u, new NetManager.RPCProcCreator(on_del_mail_res.create));
		}

		public void get_mail_list(int begin, int cnt, uint glbmail = 0u)
		{
			Variant variant = new Variant();
			variant["begin"] = begin;
			variant["cnt"] = cnt;
			bool flag = glbmail == 1u;
			if (flag)
			{
				variant["glbmail"] = glbmail;
			}
			base.sendRPC(152u, variant);
		}

		public void get_mail(int mailid, uint glbmail = 0u)
		{
			Variant variant = new Variant();
			variant["mailid"] = mailid;
			bool flag = glbmail == 1u;
			if (flag)
			{
				variant["glbmail"] = glbmail;
			}
			base.sendRPC(153u, variant);
		}

		public void get_mail_item(int mailid)
		{
			Variant variant = new Variant();
			variant["mailid"] = mailid;
			base.sendRPC(154u, variant);
		}

		public void send_mail(int tocid, string title, string msg)
		{
			Variant variant = new Variant();
			variant["tocid"] = tocid;
			variant["title"] = title;
			variant["msg"] = msg;
			base.sendRPC(155u, variant);
		}

		public void lock_mail(int mailid, bool locked)
		{
			Variant variant = new Variant();
			variant["mailid"] = mailid;
			if (locked)
			{
				variant["lock"] = true;
			}
			else
			{
				variant["lock"] = false;
			}
			base.sendRPC(156u, variant);
		}

		public void del_mail(int mailid)
		{
			Variant variant = new Variant();
			variant["mailid"] = mailid;
			base.sendRPC(158u, variant);
		}
	}
}
