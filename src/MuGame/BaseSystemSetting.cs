using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class BaseSystemSetting : Skin
	{
		public BaseSystemSetting(Transform trans) : base(trans)
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
