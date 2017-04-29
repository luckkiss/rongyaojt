using System;

namespace MuGame
{
	internal class TalkDialogData
	{
		public bool isUser = false;

		public string cont = "--";

		public void init(string str)
		{
			string[] array = str.Split(new char[]
			{
				':'
			});
			bool flag = array.Length < 2;
			if (!flag)
			{
				this.isUser = (int.Parse(array[0]) == 0);
				this.cont = array[1];
			}
		}
	}
}
