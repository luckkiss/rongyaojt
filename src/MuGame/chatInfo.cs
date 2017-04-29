using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class chatInfo : LGDataBase
	{
		public chatInfo(muNetCleint m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new chatInfo(m as muNetCleint);
		}

		public override void init()
		{
			base.addEventListener(3066u, new Action<GameEvent>(this.sendTalk));
			base.g_mgr.addEventListener(161u, new Action<GameEvent>(this.sendTalkRes));
			base.g_mgr.addEventListener(160u, new Action<GameEvent>(this.getTalk));
		}

		public void sendTalk(GameEvent e)
		{
			Variant variant = new Variant();
			variant["msg"] = e.data["msg"];
			variant["tp"] = e.data["tp"];
			variant["cid"] = e.data["cid"];
			variant["vip"] = null;
			variant["tid"] = null;
			variant["clanid"] = null;
			variant["withtid"] = false;
		}

		public void sendTalkRes(GameEvent e)
		{
			Variant data = e.data;
			base.dispatchEvent(GameEvent.Create(3068u, this, data, false));
		}

		public void getTalk(GameEvent e)
		{
			Variant data = e.data;
			base.dispatchEvent(GameEvent.Create(3069u, this, data, false));
		}
	}
}
