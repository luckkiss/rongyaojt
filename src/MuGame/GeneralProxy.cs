using Cross;
using System;

namespace MuGame
{
	internal class GeneralProxy : BaseProxy<GeneralProxy>
	{
		public bool active_open = false;

		public uint active_left_tm = 0u;

		public GeneralProxy()
		{
			this.addProxyListener(151u, new Action<Variant>(this.onConfig));
			this.addProxyListener(99u, new Action<Variant>(this.onSomethingTodo));
			this.addProxyListener(107u, new Action<Variant>(this.onGodLightActive));
		}

		public void sendGetClientConfig()
		{
		}

		public void SendPKState(bool changePk)
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			variant["pk_state"] = (changePk ? 1 : 0);
		}

		private void onPkStateChange(Variant d)
		{
			bool flag = d.ContainsKey("pk_state");
			if (flag)
			{
				bool pkState = d["pk_state"] == 1;
				ModelBase<PlayerModel>.getInstance().pkState = pkState;
			}
		}

		private void onSomethingTodo(Variant data)
		{
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				bool flag2 = num == 1;
				if (flag2)
				{
					ModelBase<PlayerModel>.getInstance().isFirstRechange = true;
				}
			}
		}

		public void onConfig(Variant data)
		{
		}

		public void onGodLightActive(Variant data)
		{
			this.active_open = data["open"]._bool;
			this.active_left_tm = data["left_tm"];
			bool flag = a3_liteMinimap.instance;
			if (flag)
			{
				a3_liteMinimap.instance.showActiveIcon(this.active_open, this.active_left_tm);
			}
		}
	}
}
