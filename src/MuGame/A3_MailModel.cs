using System;
using System.Collections.Generic;

namespace MuGame
{
	public class A3_MailModel : ModelBase<A3_MailModel>
	{
		public Dictionary<uint, A3_MailSimple> mail_simple = new Dictionary<uint, A3_MailSimple>();

		public Dictionary<uint, A3_MailDetail> mail_details = new Dictionary<uint, A3_MailDetail>();

		public bool HasItemInMails()
		{
			Dictionary<uint, A3_MailSimple>.Enumerator enumerator = this.mail_simple.GetEnumerator();
			bool result;
			while (enumerator.MoveNext())
			{
				KeyValuePair<uint, A3_MailSimple> current = enumerator.Current;
				bool arg_40_0;
				if (current.Value.has_itm)
				{
					current = enumerator.Current;
					arg_40_0 = !current.Value.got_itm;
				}
				else
				{
					arg_40_0 = false;
				}
				bool flag = arg_40_0;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public bool HasItemInMail(uint mailid)
		{
			bool flag = !this.mail_simple.ContainsKey(mailid);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.mail_simple[mailid].has_itm && !this.mail_simple[mailid].got_itm;
				result = flag2;
			}
			return result;
		}
	}
}
