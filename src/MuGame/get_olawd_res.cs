using Cross;
using System;

namespace MuGame
{
	internal class get_olawd_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 36u;
			}
		}

		public static get_olawd_res create()
		{
			return new get_olawd_res();
		}

		protected override void _onProcess()
		{
		}
	}
}
