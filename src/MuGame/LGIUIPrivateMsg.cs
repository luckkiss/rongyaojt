using Cross;
using System;

namespace MuGame
{
	public interface LGIUIPrivateMsg
	{
		void onChatMsg(Variant data);

		bool isCurChat(uint cid);
	}
}
