using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameCardMsgs : MsgProcduresBase
	{
		public InGameCardMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameCardMsgs create(IClientBase m)
		{
			return new InGameCardMsgs(m);
		}

		public override void init()
		{
		}

		public void get_itmcards(Variant data)
		{
			base.sendRPC(23u, data);
		}

		public void fetch_itm_card(Variant data)
		{
			base.sendRPC(22u, data);
		}
	}
}
