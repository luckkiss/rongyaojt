using System;

namespace Cross
{
	public class TPKGMsgProcesser : MsgProcesser
	{
		public override uint msgType
		{
			get
			{
				return 1u;
			}
		}
	}
}
