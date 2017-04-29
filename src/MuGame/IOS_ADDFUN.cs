using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MuGame
{
	internal static class IOS_ADDFUN
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly IOS_ADDFUN.<>c <>9 = new IOS_ADDFUN.<>c();

			public static Comparison<KeyValuePair<int, TaskData>> <>9__0_0;

			public static Comparison<mailData> <>9__1_0;

			internal int <QSTDOrderBy>b__0_0(KeyValuePair<int, TaskData> first, KeyValuePair<int, TaskData> next)
			{
				int key = first.Key;
				int key2 = next.Key;
				return key.CompareTo(key2);
			}

			internal int <mailOderBy>b__1_0(mailData first, mailData next)
			{
				int seconds = first.seconds;
				int seconds2 = next.seconds;
				return seconds.CompareTo(seconds2);
			}
		}

		public static void QSTDOrderBy(this Dictionary<int, TaskData> dic)
		{
			List<KeyValuePair<int, TaskData>> list = new List<KeyValuePair<int, TaskData>>(dic);
			List<KeyValuePair<int, TaskData>> arg_28_0 = list;
			Comparison<KeyValuePair<int, TaskData>> arg_28_1;
			if ((arg_28_1 = IOS_ADDFUN.<>c.<>9__0_0) == null)
			{
				arg_28_1 = (IOS_ADDFUN.<>c.<>9__0_0 = new Comparison<KeyValuePair<int, TaskData>>(IOS_ADDFUN.<>c.<>9.<QSTDOrderBy>b__0_0));
			}
			arg_28_0.Sort(arg_28_1);
			dic.Clear();
			foreach (KeyValuePair<int, TaskData> current in list)
			{
				dic.Add(current.Key, current.Value);
			}
		}

		public static void mailOderBy(this List<mailData> list)
		{
			Comparison<mailData> arg_21_1;
			if ((arg_21_1 = IOS_ADDFUN.<>c.<>9__1_0) == null)
			{
				arg_21_1 = (IOS_ADDFUN.<>c.<>9__1_0 = new Comparison<mailData>(IOS_ADDFUN.<>c.<>9.<mailOderBy>b__1_0));
			}
			list.Sort(arg_21_1);
		}
	}
}
