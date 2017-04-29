using System;
using System.Collections.Generic;

namespace Cross
{
	public class ArrayUtil
	{
		public delegate int CompareFunction<T>(T t1, T t2);

		public static void arrayInsert<T>(IList<T> ary, T insertObj, ArrayUtil.CompareFunction<T> compFunc)
		{
		}

		public static int arrayRemove<T>(IList<T> ary, T rmObj)
		{
			int num = ary.IndexOf(rmObj);
			bool flag = num < 0;
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				ary.RemoveAt(num);
				result = num;
			}
			return result;
		}

		public static T arrayPop<T>(IList<T> ary)
		{
			T result = ary[ary.Count - 1];
			ary.RemoveAt(ary.Count - 1);
			return result;
		}

		public static T arrayPopFront<T>(IList<T> ary)
		{
			T result = ary[0];
			ary.RemoveAt(0);
			return result;
		}

		private static void _push_heap<T>(IList<T> queue, int hole, T val, ArrayUtil.CompareFunction<T> comp)
		{
			int num = (hole - 1) / 2;
			while (0 < hole && comp(queue[num], val) > 0)
			{
				queue[hole] = queue[num];
				hole = num;
				num = (hole - 1) / 2;
			}
			queue[hole] = val;
		}

		public static void push_priority_queue<T>(IList<T> queue, T val, ArrayUtil.CompareFunction<T> comp)
		{
			queue.Add(val);
			int num = queue.Count - 1;
			bool flag = num <= 0;
			if (!flag)
			{
				ArrayUtil._push_heap<T>(queue, num, val, comp);
			}
		}

		public static T pop_priority_queue<T>(IList<T> queue, ArrayUtil.CompareFunction<T> comp)
		{
			int num = queue.Count - 1;
			bool flag = num > 1;
			if (flag)
			{
				int num2 = 0;
				int i = 2 * num2 + 2;
				T val = queue[num];
				queue[num] = queue[0];
				while (i < num)
				{
					bool flag2 = comp(queue[i], queue[i - 1]) > 0;
					if (flag2)
					{
						i--;
					}
					queue[num2] = queue[i];
					num2 = i;
					i = 2 * i + 2;
				}
				bool flag3 = i == num;
				if (flag3)
				{
					queue[num2] = queue[num - 1];
					num2 = num - 1;
				}
				ArrayUtil._push_heap<T>(queue, num2, val, comp);
			}
			int index = queue.Count - 1;
			T result = queue[index];
			queue.RemoveAt(index);
			return result;
		}
	}
}
