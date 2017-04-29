using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class connInfo : LGDataBase
	{
		public uint uid = 100000068u;

		public Variant keyt = new Variant();

		public string server_ip = "10.1.8.45";

		public string token = "123";

		public int server_port = 60999;

		public uint server_id = 5u;

		public uint clnt = 0u;

		public string server_config_url = "http://10.1.8.76/do.php";

		public string mainConfFile = "main";

		private Variant _verinfo;

		private TickItem tick;

		private int tick_num = 0;

		public connInfo(muNetCleint m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new connInfo(m as muNetCleint);
		}

		public override void init()
		{
			base.g_mgr.addEventListener(3012u, new Action<GameEvent>(this.onConnect));
			base.g_mgr.addEventListener(3013u, new Action<GameEvent>(this.onError));
			base.g_mgr.addEventListener(3014u, new Action<GameEvent>(this.onVerfin));
			base.g_mgr.addEventListener(3015u, new Action<GameEvent>(this.onClose));
			base.g_mgr.addEventListener(3016u, new Action<GameEvent>(this.onConnfailed));
		}

		public Variant getVerInfo()
		{
			return this._verinfo;
		}

		private void onError(GameEvent e)
		{
			debug.Log("连接错误");
			bool flag = login.instance != null && LGOutGame.instance != null;
			if (flag)
			{
				Action handle = delegate
				{
					this.tick_num = 0;
					this.tick = new TickItem(new Action<float>(this.onUpdate));
					TickMgr.instance.addTick(this.tick);
				};
				login.instance.msg.show(true, "服务器连接错误，请重新连接", handle);
			}
		}

		private void onUpdate(float s)
		{
			this.tick_num++;
			bool flag = this.tick_num > 100;
			if (flag)
			{
				LGOutGame.instance.reStart();
				TickMgr.instance.removeTick(this.tick);
			}
		}

		private void onConnect(GameEvent e)
		{
			base.dispatchEvent(GameEvent.Create(3012u, this, null, false));
		}

		private void onVerfin(GameEvent e)
		{
			this._verinfo = base.g_mgr.g_netM.getServerVersionInfo();
			base.dispatchEvent(GameEvent.Create(3014u, this, null, false));
		}

		private void onClose(GameEvent e)
		{
		}

		private void onConnfailed(GameEvent e)
		{
		}

		public void reconect()
		{
			base.dispatchEvent(GameEvent.Createimmedi(3011u, this, null, false));
		}

		public void setInfo(Variant info)
		{
			bool flag = info == null;
			if (!flag)
			{
				Variant variant = info["outgamevar"];
				this.server_ip = variant["server_ip"]._str;
				this.uid = variant["uid"]._uint;
				this.token = variant["token"]._str;
				this.server_port = variant["server_port"]._int32;
				this.clnt = variant["clnt"]._uint;
				this.server_id = info["server_id"]._uint;
				this.server_config_url = info["server_config_url"]._str;
				this.mainConfFile = info["mainConfig"]._str;
				base.dispatchEvent(GameEvent.Createimmedi(3011u, this, null, false));
			}
		}
	}
}
