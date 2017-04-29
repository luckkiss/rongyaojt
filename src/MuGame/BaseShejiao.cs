using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class BaseShejiao : Skin
	{
		public BaseShejiao(Transform trans) : base(trans)
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
