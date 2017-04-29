using Cross;
using System;

namespace MuGame
{
	internal class ArenaRoom : BaseRoomItem
	{
		public override void onStart(Variant svr)
		{
			base.onStart(svr);
			InterfaceMgr.getInstance().open(InterfaceMgr.FLOAT_ARENA, null, false);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_HEROHEAD);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_LITEMINIMAP);
			InterfaceMgr.getInstance().close(InterfaceMgr.BROADCASTING);
			debug.Log("!!ArenaRoom start!!");
			cdtime.show(new Action(this.doBgein));
		}

		private void doBgein()
		{
		}

		public override void onEnd()
		{
			base.onEnd();
			bool flag = joystick.instance != null;
			if (flag)
			{
				joystick.instance.OnDragOut(null);
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.FLOAT_ARENA);
			debug.Log("!!ArenaRoom end!!");
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_HEROHEAD, null, false);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_LITEMINIMAP, null, false);
			InterfaceMgr.getInstance().open(InterfaceMgr.BROADCASTING, null, false);
		}
	}
}
