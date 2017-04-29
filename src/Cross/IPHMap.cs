using System;

namespace Cross
{
	public interface IPHMap
	{
		IPHWorld world
		{
			get;
		}

		bool load(Variant conf, Action onFin);
	}
}
