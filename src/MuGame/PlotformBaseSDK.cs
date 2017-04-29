using Cross;
using System;
using UnityEngine;

namespace MuGame
{
	public class PlotformBaseSDK : IPlotformSDK
	{
		public int isinited
		{
			get
			{
				return 1;
			}
		}

		public void FrameMove()
		{
		}

		public void Add_moreCmdInfo(string info, string jstr)
		{
			Debug.Log("Base SDK (Add_moreCmdInfo):" + info);
		}

		public void Call_Cmd(string cmd, string info = null, string jstr = null, bool waiting = true)
		{
			Debug.Log(string.Concat(new string[]
			{
				"Base SDK (Call_Cmd):",
				cmd,
				" ",
				info,
				" ",
				jstr
			}));
		}

		public void Cmd_CallBack(Variant v)
		{
			Debug.Log("Base SDK (Cmd_CallBack):" + v.dump());
		}
	}
}
