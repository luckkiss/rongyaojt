using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class mailData
	{
		public int seconds;

		public int type;

		public int cid;

		public int id;

		public string time;

		public int frmcid;

		public string frmname;

		public int frmsex;

		public int flag;

		public string msg;

		public int clanc;

		public int money;

		public int yb;

		public int yinpiao;

		public string str;

		public string code = "--";

		public float acttm = 0f;

		public List<mailItemData> items = new List<mailItemData>();

		public bool isGet = false;
	}
}
