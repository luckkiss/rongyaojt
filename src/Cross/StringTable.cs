using System;
using System.Collections.Generic;

namespace Cross
{
	public class StringTable
	{
		protected Dictionary<string, string> _strTable;

		public StringTable()
		{
			this._strTable = new Dictionary<string, string>();
		}

		public string getString(string input)
		{
			bool flag = this._strTable.ContainsKey(input);
			string result;
			if (flag)
			{
				result = this._strTable[input];
			}
			else
			{
				result = input;
			}
			return result;
		}

		public void loadStrings(List<Variant> strAry)
		{
			foreach (Variant current in strAry)
			{
				this._strTable[current["key"]._str] = current["val"]._str;
			}
		}
	}
}
