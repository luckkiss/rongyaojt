using System;

namespace Cross
{
	public class RPCMsgProcesser : MsgProcesser
	{
		public override uint msgType
		{
			get
			{
				return 2u;
			}
		}
	}
}
