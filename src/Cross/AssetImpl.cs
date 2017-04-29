using System;

namespace Cross
{
	public class AssetImpl : IAsset
	{
		protected bool m_loading;

		protected bool m_ready;

		protected bool m_loaded;

		protected string m_path;

		protected float m_autoDisposeTime;

		protected float m_lastVisitDelta;

		protected ulong m_autoDisposeCounter;

		protected Action<IAsset> m_onFins;

		protected Action<IAsset, float> m_onProgs;

		protected Action<IAsset, string> m_onFails;

		public float autoDisposeTime
		{
			get
			{
				return this.m_autoDisposeTime;
			}
			set
			{
				this.m_autoDisposeTime = value;
			}
		}

		public bool isReady
		{
			get
			{
				return this.m_ready;
			}
		}

		public string path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				bool flag = this.m_path == value;
				if (!flag)
				{
					this.m_loading = false;
					this.m_ready = false;
					this.m_loaded = false;
					this.m_path = ((value != null) ? value : "");
				}
			}
		}

		public ulong autoDisposeCounter
		{
			get
			{
				return this.m_autoDisposeCounter;
			}
			set
			{
				this.m_autoDisposeCounter = value;
			}
		}

		public AssetImpl()
		{
			this.m_loading = false;
			this.m_ready = false;
			this.m_loaded = false;
			this.m_path = "";
			this.m_autoDisposeTime = -1f;
			this.m_lastVisitDelta = 0f;
			this.m_autoDisposeCounter = 999999uL;
		}

		public virtual void dispose()
		{
			this.m_loading = false;
			this.m_ready = false;
			this.m_loaded = false;
		}

		public void visit()
		{
			this.m_lastVisitDelta = 0f;
		}

		public void onProcess(float delta)
		{
			bool flag = !this.m_ready;
			if (!flag)
			{
				bool flag2 = this.m_autoDisposeTime > 0f;
				if (flag2)
				{
					this.m_lastVisitDelta += delta;
					bool flag3 = this.m_lastVisitDelta > this.m_autoDisposeTime;
					if (flag3)
					{
						this.dispose();
						this.m_lastVisitDelta = 0f;
					}
				}
			}
		}

		public void addCallbacks(Action<IAsset> onFin, Action<IAsset, float> onProg, Action<IAsset, string> onFail)
		{
			bool flag = onFin != null;
			if (flag)
			{
				bool flag2 = this.m_onFins == null;
				if (flag2)
				{
					this.m_onFins = onFin;
				}
				else
				{
					this.m_onFins = (Action<IAsset>)Delegate.Combine(this.m_onFins, onFin);
				}
			}
			bool flag3 = onProg != null;
			if (flag3)
			{
				bool flag4 = this.m_onProgs == null;
				if (flag4)
				{
					this.m_onProgs = onProg;
				}
				else
				{
					this.m_onProgs = (Action<IAsset, float>)Delegate.Combine(this.m_onProgs, onProg);
				}
			}
			bool flag5 = onFail != null;
			if (flag5)
			{
				bool flag6 = this.m_onFails == null;
				if (flag6)
				{
					this.m_onFails = onFail;
				}
				else
				{
					this.m_onFails = (Action<IAsset, string>)Delegate.Combine(this.m_onFails, onFail);
				}
			}
			bool ready = this.m_ready;
			if (ready)
			{
				bool flag7 = this.m_onFins != null;
				if (flag7)
				{
					this.m_onFins(this);
				}
				this.m_onFins = null;
				this.m_onProgs = null;
				this.m_onFails = null;
			}
			else
			{
				bool loaded = this.m_loaded;
				if (loaded)
				{
					bool flag8 = this.m_onFails != null;
					if (flag8)
					{
						this.m_onFails(this, "");
					}
					this.m_onFins = null;
					this.m_onProgs = null;
					this.m_onFails = null;
				}
			}
		}

		public void load()
		{
			this.m_lastVisitDelta = 0f;
			this.loadImpl(!os.asset.async);
		}

		public virtual void loadImpl(bool bSync)
		{
			this.m_lastVisitDelta = 0f;
		}

		protected virtual void _dispatchOnFins()
		{
			bool flag = this.m_onFins != null;
			if (flag)
			{
				this.m_onFins(this);
				this.m_onFins = null;
				this.m_onFails = null;
			}
		}

		protected virtual void _dispatchOnFails(string err)
		{
			bool flag = this.m_onFails != null;
			if (flag)
			{
				this.m_onFails(this, err);
				this.m_onFins = null;
				this.m_onFails = null;
			}
		}
	}
}
