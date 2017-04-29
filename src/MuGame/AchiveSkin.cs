using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class AchiveSkin : Skin
	{
		public a3_achievement main
		{
			get;
			set;
		}

		public AchiveSkin(Window win, Transform tran) : base(tran)
		{
			this.main = (win as a3_achievement);
			this.init();
		}

		public virtual void init()
		{
		}

		public virtual void onShowed()
		{
		}

		public virtual void onClosed()
		{
		}
	}
}
