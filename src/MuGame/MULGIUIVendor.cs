using Cross;
using System;

namespace MuGame
{
	public interface MULGIUIVendor
	{
		void GetPlyAuclistRes(Variant data);

		void AddVendorItemRes(Variant data);

		void RemoveVendorItemRes(Variant data);

		void BuyAucItemRes(Variant data);

		void AddPriceRes(Variant data);

		void GetAucinfoRes(Variant data);

		void FetchMoneyRes();
	}
}
