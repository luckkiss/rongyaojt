using Cross;
using System;

namespace GameFramework
{
	internal interface IMessageSend
	{
		void sendRPC(uint cmd, Variant msg);

		void sendTpkg(uint cmd, Variant msg);
	}
}
