using System;

namespace GameFramework
{
	public class Window3d : Baselayer
	{
		public override float type
		{
			get
			{
				return (float)Baselayer.LAYER_TYPE_WINDOW_3D;
			}
		}
	}
}
