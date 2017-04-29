using Cross;
using System;

namespace MuGame
{
	internal class onGetWorldCountRes : TPKGMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 6u;
			}
		}

		protected override void _onProcess()
		{
		}
	}
}
