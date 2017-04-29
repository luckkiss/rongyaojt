using Cross;
using System;

namespace MuGame
{
	internal class DragonRoom : BaseRoomItem
	{
		public override void onStart(Variant svr)
		{
			base.onStart(svr);
			a3_insideui_fb.begintime = (double)muNetCleint.instance.CurServerTimeStamp;
			a3_insideui_fb.ShowInUI(a3_insideui_fb.e_room.DRAGON);
			Variant variant = new Variant();
			variant["curLevelId"] = MapModel.getInstance().curLevelId;
			InterfaceMgr.doCommandByLua("a3_litemap_btns.refreshByUIState", "ui/interfaces/floatui/a3_litemap_btns", new object[]
			{
				variant
			});
			InterfaceMgr.doCommandByLua("a3_litemap.refreshByUIState", "ui/interfaces/floatui/a3_litemap", new object[]
			{
				variant
			});
			a3_liteMinimap.instance.SetTaskInfoVisible(false);
		}

		public override void onEnd()
		{
			a3_insideui_fb.CloseInUI();
			base.onEnd();
			Variant variant = new Variant();
			variant["curLevelId"] = MapModel.getInstance().curLevelId;
			InterfaceMgr.doCommandByLua("a3_litemap_btns.refreshByUIState", "ui/interfaces/floatui/a3_litemap_btns", new object[]
			{
				variant
			});
			InterfaceMgr.doCommandByLua("a3_litemap.refreshByUIState", "ui/interfaces/floatui/a3_litemap", new object[]
			{
				variant
			});
			a3_liteMinimap.instance.SetTaskInfoVisible(true);
		}
	}
}
