using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class a3_friend : BaseShejiao
	{
		public a3_friend(Transform trans) : base(trans)
		{
			this.init();
		}

		public void init()
		{
			toggleGropFriendBase.mTransform = base.transform;
			toggleGropFriendBase.init();
		}

		public override void onShowed()
		{
			toggleGropFriendBase.mFriendList.onShow();
		}

		private void onBtnClose(GameObject gob)
		{
			foreach (KeyValuePair<uint, itemFriendData> current in friendList._instance.friendDataDic)
			{
				current.Value.root.gameObject.SetActive(false);
			}
			foreach (KeyValuePair<uint, itemFriendData> current2 in friendList._instance.BlackListDataDic)
			{
				current2.Value.root.gameObject.SetActive(false);
			}
			foreach (KeyValuePair<uint, itemFriendData> current3 in friendList._instance.EnemyListDataDic)
			{
				current3.Value.root.gameObject.SetActive(false);
			}
			foreach (KeyValuePair<uint, itemFriendData> current4 in friendList._instance.NearbyListDataDic)
			{
				current4.Value.root.gameObject.SetActive(false);
			}
			foreach (KeyValuePair<uint, itemFriendData> current5 in friendList._instance.RecommendListDataDic)
			{
				current5.Value.root.gameObject.SetActive(false);
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SHEJIAO);
		}
	}
}
