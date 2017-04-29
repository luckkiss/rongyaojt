using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class AreaItem : Skin
	{
		public int _idx;

		public Action<int> _handle;

		public Button bt;

		public AreaItem(Transform trans, Action<int> handle, int idx) : base(trans)
		{
			this._idx = idx;
			this._handle = handle;
			this.init();
		}

		private void init()
		{
			this.bt = this.__mainTrans.GetComponent<Button>();
			this.bt.onClick.AddListener(new UnityAction(this.onClick));
			base.getComponentByPath<Text>("Text").text = string.Concat(new object[]
			{
				this._idx * 10 + 1,
				"-",
				this._idx * 10 + 10,
				"区"
			});
		}

		private void onClick()
		{
			bool flag = this._handle != null;
			if (flag)
			{
				this._handle(this._idx);
			}
		}

		public void dispose()
		{
			this._handle = null;
			this.bt.onClick.RemoveAllListeners();
		}
	}
}
