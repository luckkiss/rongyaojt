using System;
using UnityEngine;

namespace MuGame
{
	public class RoomDropItem : MonoBehaviour
	{
		public int id = 0;

		public int num = 0;

		private void Start()
		{
			BoxCollider boxCollider = base.GetComponent<BoxCollider>();
			bool flag = boxCollider == null;
			if (flag)
			{
				boxCollider = base.gameObject.AddComponent<BoxCollider>();
			}
			boxCollider.isTrigger = true;
			int lM_PT = EnumLayer.LM_PT;
			base.gameObject.layer = lM_PT;
		}

		public void OnTriggerEnter(Collider other)
		{
			int lM_SELFROLE = EnumLayer.LM_SELFROLE;
			bool flag = other.gameObject.layer == lM_SELFROLE;
			if (flag)
			{
				FightText.play(FightText.MONEY_TEXT, SelfRole._inst.getHeadPos(), 10, false, -1);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
