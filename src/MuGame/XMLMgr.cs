using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class XMLMgr
	{
		public static XMLMgr instance = new XMLMgr();

		private Dictionary<string, SXML> m_allConf = new Dictionary<string, SXML>();

		public void init(ByteArray ba)
		{
			List<string> list = new List<string>();
			ba.position = 0;
			while (ba.bytesAvailable > 4)
			{
				int num = ba.readInt();
				bool flag = num == -1;
				if (flag)
				{
					break;
				}
				string item = ba.readUTF8Bytes(num);
				list.Add(item);
			}
			SXML.lStr = list;
			while (ba.bytesAvailable > 4)
			{
				int num2 = ba.readInt();
				bool flag2 = num2 == -1;
				if (flag2)
				{
					break;
				}
				string key = SXML.lStr[num2];
				SXML sXML = new SXML();
				int len = ba.readInt();
				ByteArray byteArray = new ByteArray();
				ba.readBytes(byteArray, ba.position, len);
				sXML.m_root = byteArray;
				this.m_allConf[key] = sXML;
			}
		}

		public List<SXML> GetSXMLList(string id, string filter = "")
		{
			string[] array = id.Split(new char[]
			{
				'.'
			});
			bool flag = array.Length < 1;
			List<SXML> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.m_allConf.ContainsKey(array[0]);
				if (flag2)
				{
					SXML sXML = this.m_allConf[array[0]];
					SXML sXML2 = sXML;
					sXML2.checkInited();
					List<SXML> list = null;
					bool flag3 = array.Length == 1;
					if (flag3)
					{
						list = new List<SXML>();
						list.Add(sXML2);
					}
					for (int i = 1; i < array.Length; i++)
					{
						bool flag4 = sXML2.m_dNode.ContainsKey(array[i]);
						if (!flag4)
						{
							result = null;
							return result;
						}
						list = sXML2.m_dNode[array[i]];
						sXML2 = list[0];
						sXML2.checkInited();
					}
					result = SXML.Filer(list, filter);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public SXML GetSXML(string id, string filter = "")
		{
			List<SXML> sXMLList = this.GetSXMLList(id, filter);
			bool flag = sXMLList == null || sXMLList.Count == 0;
			SXML result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = sXMLList[0];
			}
			return result;
		}

		public void AddXmlData(string key, ref string data)
		{
		}
	}
}
