using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MuGame
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct PanelQuickTalk
	{
		public static Transform root;

		public static BaseButton btn01;

		public static BaseButton btn02;

		public static BaseButton btn03;
	}
}
