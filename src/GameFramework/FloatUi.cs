using System;

namespace GameFramework
{
	public class FloatUi : Baselayer
	{
		public override float type
		{
			get
			{
				return (float)Baselayer.LAYER_TYPE_FLOATUI;
			}
		}
	}
}
