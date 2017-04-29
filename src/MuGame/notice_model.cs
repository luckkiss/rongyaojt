using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class notice_model : ModelBase<notice_model>
	{
		public List<noticeDate> notice = new List<noticeDate>();

		public void xml_time()
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("notice.func", "");
			for (int i = 0; i < sXMLList.Count; i++)
			{
				noticeDate noticeDate = default(noticeDate);
				noticeDate.id = sXMLList[i].getInt("id");
				noticeDate.des = sXMLList[i].getString("des");
				noticeDate.func_id = sXMLList[i].getInt("func_id");
				noticeDate.last = sXMLList[i].getInt("last");
				noticeDate.icon = sXMLList[i].getInt("icon");
				noticeDate.zhuan = sXMLList[i].getInt("zhuan");
				noticeDate.level = sXMLList[i].getInt("level");
				noticeDate.time = new Dictionary<float, float>();
				List<SXML> nodeList = sXMLList[i].GetNodeList("time", "");
				for (int j = 0; j < nodeList.Count; j++)
				{
					string @string = nodeList[j].getString("t");
					string[] array = @string.Split(new char[]
					{
						','
					});
					float key = float.Parse(array[0]);
					float value = float.Parse(array[1]);
					noticeDate.time[key] = value;
				}
				this.notice.Add(noticeDate);
			}
		}
	}
}
