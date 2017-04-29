using Cross;
using System;

namespace MuGame
{
	public interface LGIUISmelt
	{
		void GetSmeltInfoRes(Variant data);

		void GetAwdRes(Variant data);

		void RmvItems(Array items);

		void AddItems(Array items);
	}
}
