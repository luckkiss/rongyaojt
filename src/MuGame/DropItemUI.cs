using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	public class DropItemUI : Skin
	{
		public INameObj _dropObj;

		private Text txtName;

		private TickItem showtime;

		private float times = 0f;

		private int i;

		private bool isDes = false;

		public DropItemUI(Transform trans) : base(trans)
		{
			this.iniUI();
		}

		private void iniUI()
		{
			base.transform.localScale = Vector3.one;
			this.txtName = base.getComponentByPath<Text>("txtName");
		}

		public void refresh(INameObj dropObj, string name)
		{
			this._dropObj = dropObj;
			bool flag = dropObj is DropItem;
			if (!flag)
			{
				this.txtName = null;
			}
			bool flag2 = this.txtName;
			if (flag2)
			{
				this.txtName.text = name;
				this.txtName.gameObject.SetActive(true);
			}
		}

		private void onUpdates(float s)
		{
			this.times += s;
			bool flag = this.times >= 1f;
			if (flag)
			{
				this.i--;
				bool flag2 = this.i == 0;
				if (flag2)
				{
					this.i = 0;
					bool flag3 = this.isDes;
					if (flag3)
					{
						return;
					}
					this.txtName.gameObject.SetActive(false);
					TickMgr.instance.removeTick(this.showtime);
					UnityEngine.Object.Destroy(base.gameObject);
					this.isDes = true;
					DropItemUIMgr.getInstance().removeDropItem(this);
					this.showtime = null;
				}
				this.times = 0f;
			}
		}

		public void refresShowName(int time)
		{
			bool flag = time <= 0;
			if (!flag)
			{
				this.showtime = new TickItem(new Action<float>(this.onUpdates));
				TickMgr.instance.addTick(this.showtime);
				this.i = time;
			}
		}

		public void clear()
		{
			bool flag = this.isDes;
			if (!flag)
			{
				this.txtName.gameObject.SetActive(false);
				TickMgr.instance.removeTick(this.showtime);
				this.showtime = null;
				UnityEngine.Object.Destroy(base.gameObject);
				this.isDes = true;
				DropItemUIMgr.getInstance().removeDropItem(this);
			}
		}

		public void update()
		{
			base.pos = this._dropObj.getHeadPos();
		}
	}
}
