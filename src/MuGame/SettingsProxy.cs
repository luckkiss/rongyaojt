using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class SettingsProxy : BaseProxy<SettingsProxy>
	{
		public static uint CHANGE_NAME = 1u;

		public Variant _variant = null;

		public SettingsProxy()
		{
			this.addProxyListener(208u, new Action<Variant>(this.getName));
		}

		public void getName(Variant data)
		{
			int num = data["res"];
			bool flag = num > 0;
			if (flag)
			{
				ModelBase<PlayerModel>.getInstance().name = data["name"];
			}
			base.dispatchEvent(GameEvent.Create(SettingsProxy.CHANGE_NAME, this, data, false));
		}

		public void SendModifName(string name)
		{
			Variant variant = new Variant();
			variant["name"] = name;
			this.sendRPC(208u, variant);
		}
	}
}
