using Cross;
using System;

namespace MuGame
{
	internal class onRmisRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 0u;
			}
		}

		public static onRmisRes create()
		{
			return new onRmisRes();
		}

		protected override void _onProcess()
		{
		}
	}
}
