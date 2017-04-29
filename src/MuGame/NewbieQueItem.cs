using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class NewbieQueItem
	{
		public int id;

		private List<NewbieTeachItem> list = new List<NewbieTeachItem>();

		public NewbieQueItem(string str, int idid)
		{
			string[] array = str.Split(new char[]
			{
				';'
			});
			NewbieTeachItem newbieTeachItem = null;
			this.id = idid;
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = array[i] == "";
				if (!flag)
				{
					NewbieTeachItem newbieTeachItem2 = NewbieTeachItem.initWithStr(array[i]);
					newbieTeachItem2.idx = i;
					newbieTeachItem2.id = this.id;
					bool flag2 = newbieTeachItem != null;
					if (flag2)
					{
						newbieTeachItem.nextItem = newbieTeachItem2;
					}
					newbieTeachItem = newbieTeachItem2;
					this.list.Add(newbieTeachItem2);
				}
			}
			this.list[0].doit(false, false);
		}

		public NewbieQueItem(List<string> lStr, int idid)
		{
			NewbieTeachItem newbieTeachItem = null;
			this.id = idid;
			for (int i = 0; i < lStr.Count; i++)
			{
				bool flag = lStr[i] == "";
				if (!flag)
				{
					NewbieTeachItem newbieTeachItem2 = NewbieTeachItem.initWithStr(lStr[i]);
					newbieTeachItem2.idx = i;
					newbieTeachItem2.id = this.id;
					bool flag2 = newbieTeachItem != null;
					if (flag2)
					{
						newbieTeachItem.nextItem = newbieTeachItem2;
					}
					newbieTeachItem = newbieTeachItem2;
					this.list.Add(newbieTeachItem2);
				}
			}
			this.list[0].doit(false, false);
		}
	}
}
