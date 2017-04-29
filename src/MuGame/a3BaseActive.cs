using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class a3BaseActive : Skin
	{
		public a3_active main
		{
			get;
			set;
		}

		public string pathStrName
		{
			get;
			set;
		}

		public a3BaseActive(Window win, string pathStr) : base(win.getTransformByPath(pathStr))
		{
			string[] array = pathStr.Split(new char[]
			{
				'/'
			});
			this.pathStrName = array[Mathf.Max(0, array.Length - 1)];
			this.main = (win as a3_active);
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
