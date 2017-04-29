using System;

namespace GameFramework
{
	public class FightTextLayer : Baselayer
	{
		public override float type
		{
			get
			{
				return (float)Baselayer.LAYER_TYPE_FIGHT_TEXT;
			}
		}
	}
}
