using System;
using UnityEngine;

namespace MuGame
{
	public class GiftCardData
	{
		public int id;

		public string code;

		public GiftCardType cardType;

		public float creattimer = 0f;

		public void initTimer()
		{
			bool flag = this.creattimer == 0f;
			if (flag)
			{
				this.creattimer = Time.time;
			}
		}

		public void getItems()
		{
			BaseProxy<GiftCardProxy>.getInstance().sendFetchCard(this.code);
		}
	}
}
