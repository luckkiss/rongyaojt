using MuGame;
using System;

namespace SimpleFramework.Manager
{
	public class InterfaceMgr : View
	{
		public static InterfaceMgr instacne;

		private void Awake()
		{
			InterfaceMgr.instacne = this;
			MuGame.InterfaceMgr.handleOpenByLua = new Action<string, object>(InterfaceMgr.instacne.open);
			MuGame.InterfaceMgr.doCommandByLua = new MuGame.InterfaceMgr.objDelegate1(InterfaceMgr.instacne.doCommand);
			MuGame.InterfaceMgr.doCommandByLua_discard = new MuGame.InterfaceMgr.objDelegate(InterfaceMgr.instacne.doCommand_discard);
		}

		public object[] doCommand(string id, string path, params object[] args)
		{
			return Util.CallMethod("InterfaceMgr", "doLua", new object[]
			{
				id,
				args,
				path
			});
		}

		public object[] doCommand_discard(string id, params object[] args)
		{
			return Util.CallMethod("InterfaceMgr", "doCommand", new object[]
			{
				id,
				args
			});
		}

		public void open(string name, object pram = null)
		{
			this.CallMethod("open", new object[]
			{
				name,
				pram
			});
		}

		public void close(string name)
		{
			this.CallMethod("close", new object[]
			{
				name
			});
		}

		private object[] CallMethod(string func, params object[] args)
		{
			return Util.CallMethod("InterfaceMgr", func, args);
		}
	}
}
