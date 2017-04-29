using Cross;
using System;

namespace MuGame
{
	internal class IconAddLightMgr
	{
		public static IconAddLightMgr _instance;

		private string path = "ui/interfaces/floatui/a3_litemap_btns";

		private string str = "a3_litemap_btns.";

		private string str1 = "a3_litemap.";

		public static IconAddLightMgr getInstance()
		{
			bool flag = IconAddLightMgr._instance == null;
			if (flag)
			{
				IconAddLightMgr._instance = new IconAddLightMgr();
			}
			return IconAddLightMgr._instance;
		}

		public void showOrHideFire(string way, Variant data)
		{
			InterfaceMgr.doCommandByLua(this.str + way, this.path, new object[]
			{
				data
			});
		}

		public void showOrHideFires(string way, Variant data)
		{
			InterfaceMgr.doCommandByLua(this.str1 + way, this.path, new object[]
			{
				data
			});
		}
	}
}
