using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	public class BaseAwardCenter : Skin
	{
		public BaseAwardCenter(Transform trans) : base(trans)
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
