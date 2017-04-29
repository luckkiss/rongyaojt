using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class ServerItem : Skin
	{
		public GameObject iconClose;

		public GameObject iconTj;

		public GameObject iconNew;

		public ServerData serverData;

		public Action<ServerData> _handle;

		public Text txt;

		public Button _bt;

		public override bool visiable
		{
			get
			{
				return base.visiable;
			}
			set
			{
				base.visiable = value;
				bool flag = !base.visiable;
				if (flag)
				{
					this.serverData = null;
				}
			}
		}

		public ServerItem(Transform trans, Action<ServerData> handle) : base(trans)
		{
			this._handle = handle;
			this.init();
		}

		private void init()
		{
			this._bt = this.__mainTrans.GetComponent<Button>();
			this._bt.onClick.AddListener(new UnityAction(this.onClick));
			this.txt = base.getComponentByPath<Text>("Text");
			this.iconClose = base.getGameObjectByPath("iconClose");
			this.iconTj = base.getGameObjectByPath("iconTj");
			this.iconNew = base.getGameObjectByPath("iconNew");
			this.hideIcon();
		}

		public void hideIcon()
		{
			this.iconClose.SetActive(false);
			this.iconTj.SetActive(false);
			this.iconNew.SetActive(false);
		}

		public void setData(ServerData d)
		{
			bool flag = d == null;
			if (flag)
			{
				this.visiable = false;
			}
			else
			{
				this.visiable = true;
				this.serverData = d;
				this.txt.text = d.server_name;
				bool close = d.close;
				if (close)
				{
					this.iconClose.SetActive(true);
					this._bt.interactable = false;
				}
				else
				{
					bool srvnew = d.srvnew;
					if (srvnew)
					{
						this.iconNew.SetActive(true);
					}
					else
					{
						bool recomm = d.recomm;
						if (recomm)
						{
							this.iconTj.SetActive(true);
						}
					}
				}
			}
		}

		private void onClick()
		{
			bool flag = this._handle != null && this.serverData != null;
			if (flag)
			{
				this._handle(this.serverData);
			}
		}

		public void dispose()
		{
			this._bt.onClick.RemoveAllListeners();
			this._bt = null;
			this._handle = null;
		}
	}
}
