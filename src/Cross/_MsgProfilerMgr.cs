using System;
using System.Collections.Generic;

namespace Cross
{
	internal class _MsgProfilerMgr
	{
		public static _MsgProfilerMgr inst = new _MsgProfilerMgr();

		protected Dictionary<uint, Dictionary<uint, _MsgProfiler>> _profilerDict = new Dictionary<uint, Dictionary<uint, _MsgProfiler>>();

		public _MsgProfiler getMsgProfiler(uint type, uint msgID)
		{
			bool flag = !this._profilerDict.ContainsKey(type);
			Dictionary<uint, _MsgProfiler> dictionary;
			if (flag)
			{
				dictionary = new Dictionary<uint, _MsgProfiler>();
				this._profilerDict[type] = dictionary;
			}
			else
			{
				dictionary = this._profilerDict[type];
			}
			bool flag2 = !dictionary.ContainsKey(msgID);
			_MsgProfiler msgProfiler;
			if (flag2)
			{
				msgProfiler = new _MsgProfiler();
				msgProfiler.MsgID = msgID;
				msgProfiler.MsgType = type;
				dictionary[msgID] = msgProfiler;
			}
			else
			{
				msgProfiler = dictionary[msgID];
			}
			return msgProfiler;
		}

		public string dumpProfile(uint type)
		{
			string text = string.Empty;
			bool flag = !this._profilerDict.ContainsKey(type);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				Dictionary<uint, _MsgProfiler> dictionary = this._profilerDict[type];
				foreach (_MsgProfiler current in dictionary.Values)
				{
					text += current.dumpProfile();
				}
				result = text;
			}
			return result;
		}
	}
}
