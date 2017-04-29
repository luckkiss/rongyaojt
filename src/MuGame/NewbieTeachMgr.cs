using System;
using System.Collections.Generic;

namespace MuGame
{
	public class NewbieTeachMgr
	{
		private List<NewbieQueItem> lQue = new List<NewbieQueItem>();

		private static NewbieTeachMgr _instance;

		public NewbieTeachMgr()
		{
			NewbieTeachItem.initCommand();
		}

		public void add(string str, int id)
		{
			NewbieQueItem item = new NewbieQueItem(str, id);
			this.lQue.Add(item);
		}

		public void add(List<string> lstr, int id)
		{
			NewbieQueItem item = new NewbieQueItem(lstr, id);
			this.lQue.Add(item);
		}

		public static NewbieTeachMgr getInstance()
		{
			bool flag = NewbieTeachMgr._instance == null;
			if (flag)
			{
				NewbieTeachMgr._instance = new NewbieTeachMgr();
			}
			return NewbieTeachMgr._instance;
		}
	}
}
