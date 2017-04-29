using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class TalkMainData
	{
		public List<TalkDialogData> lDialog;

		private int curIdx;

		public string _name;

		public string _avatarid;

		public void init(string str, string avatarid, string name)
		{
			this._name = name;
			this._avatarid = avatarid;
			this.lDialog = new List<TalkDialogData>();
			string[] array = str.Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array.Length; i++)
			{
				TalkDialogData talkDialogData = new TalkDialogData();
				talkDialogData.init(array[i]);
				this.lDialog.Add(talkDialogData);
			}
		}

		public void beginTalk(LGAvatarNpc npc)
		{
			this.curIdx = 0;
		}

		public TalkDialogData doTalk()
		{
			bool flag = this.curIdx < this.lDialog.Count;
			TalkDialogData result;
			if (flag)
			{
				TalkDialogData talkDialogData = this.lDialog[this.curIdx];
				this.curIdx++;
				result = talkDialogData;
			}
			else
			{
				this.curIdx = 0;
				result = null;
			}
			return result;
		}
	}
}
