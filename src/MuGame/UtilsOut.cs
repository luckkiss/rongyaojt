using System;
using UnityEngine;

namespace MuGame
{
	internal class UtilsOut
	{
		public static void init()
		{
			gameST.REQ_SET_FAST_BLOOM = new Action<GameObject, float>(UtilsOut.setFastBloomParm);
		}

		public static void setFastBloomParm(GameObject go, float intensity)
		{
			FastBloom component = go.GetComponent<FastBloom>();
			if (component == null)
			{
				return;
			}
			if (intensity < 0f)
			{
				return;
			}
			if (intensity > 2.5f)
			{
				return;
			}
			component.intensity = intensity;
		}
	}
}
