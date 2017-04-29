using Cross;
using System;

namespace MuGame
{
	public interface IPlotformSDK
	{
		int isinited
		{
			get;
		}

		void FrameMove();

		void Add_moreCmdInfo(string info, string jstr);

		void Call_Cmd(string cmd, string info = null, string jstr = null, bool waiting = true);

		void Cmd_CallBack(Variant v);
	}
}
