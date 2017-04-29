using System;
using System.Collections.Generic;

namespace MuGame
{
	public class A3_MailDetail
	{
		public A3_MailSimple ms;

		public string msg;

		public uint money;

		public uint yb;

		public uint bndyb;

		public List<a3_BagItemData> itms;

		public A3_MailDetail()
		{
			this.itms = new List<a3_BagItemData>();
		}
	}
}
