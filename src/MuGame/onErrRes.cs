using Cross;
using System;

namespace MuGame
{
	internal class onErrRes : TPKGMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 7u;
			}
		}

		protected override void _onProcess()
		{
		}
	}
}
