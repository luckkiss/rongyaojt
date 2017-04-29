using System;

namespace Cross
{
	public class MathUtil
	{
		public static float lerp(float v0, float v1, float t)
		{
			t = Math.Max(0f, Math.Min(1f, t));
			return (1f - t) * v0 + t * v1;
		}
	}
}
