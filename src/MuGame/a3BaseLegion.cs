using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class a3BaseLegion : Skin
	{
		public a3_legion main
		{
			get;
			set;
		}

		public string pathStrName
		{
			get;
			set;
		}

		public a3BaseLegion(BaseShejiao win, string pathStr) : base(win.getTransformByPath("s4/" + pathStr))
		{
			string[] array = pathStr.Split(new char[]
			{
				'/'
			});
			this.pathStrName = array[Mathf.Max(0, array.Length - 1)];
			this.main = (win as a3_legion);
			this.init();
		}

		public virtual void init()
		{
		}

		public virtual void onShowed()
		{
		}

		public virtual void onClose()
		{
		}
	}
}
