using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public abstract class GRBaseImpls : GameEventDispatcher, IObjectPlugin, IProcess
	{
		private string _controlId;

		protected GRClient g_mgr;

		protected LGGRBaseImpls m_ctrl;

		protected object m_gr;

		protected Dictionary<string, List<IGREffectParticles>> m_effectLoad = new Dictionary<string, List<IGREffectParticles>>();

		private bool _pause = false;

		private bool _destory = false;

		private string _processName = "";

		public string controlId
		{
			get
			{
				return this._controlId;
			}
			set
			{
				this._controlId = value;
			}
		}

		public bool destroy
		{
			get
			{
				return this._destory;
			}
			set
			{
				this._destory = value;
			}
		}

		public bool pause
		{
			get
			{
				return this._pause;
			}
			set
			{
				this._pause = value;
			}
		}

		public string processName
		{
			get
			{
				return this.controlId;
			}
			set
			{
				this._processName = value;
			}
		}

		public GRBaseImpls(clientBase m)
		{
			this.g_mgr = (m as GRClient);
		}

		public abstract void init();

		public abstract void dispose();

		protected abstract void onSetGraphImpl();

		protected abstract void onSetSceneCtrl();

		public void setGraphImpl(object gr)
		{
			this.m_gr = gr;
			this.onSetGraphImpl();
		}

		public void setSceneCtrl(LGGRBaseImpls ctrl)
		{
			this.m_ctrl = ctrl;
			this.onSetSceneCtrl();
		}

		public virtual void initLg(lgGDBase lgbase)
		{
		}

		protected IGREffectParticles addEffect(string effid, float x, float y, float z, bool single = true)
		{
			IGREffectParticles iGREffectParticles = this.createEffect(effid, single);
			bool flag = iGREffectParticles != null;
			if (flag)
			{
				iGREffectParticles.x = x;
				iGREffectParticles.y = y;
				iGREffectParticles.z = z;
			}
			return iGREffectParticles;
		}

		protected IGREffectParticles createEffect(string effid, bool single = true)
		{
			bool flag = !this.m_effectLoad.ContainsKey(effid);
			if (flag)
			{
				this.m_effectLoad[effid] = new List<IGREffectParticles>();
			}
			List<IGREffectParticles> list = this.m_effectLoad[effid];
			bool flag2 = single && list.Count > 0;
			IGREffectParticles iGREffectParticles;
			if (flag2)
			{
				iGREffectParticles = list[0];
				iGREffectParticles.stop();
			}
			else
			{
				iGREffectParticles = this.g_mgr.createEffect(effid);
				bool flag3 = iGREffectParticles != null;
				if (flag3)
				{
					list.Add(iGREffectParticles);
				}
			}
			return iGREffectParticles;
		}

		protected IGREffectParticles attachEffect(string effid, string attachID, bool single = true)
		{
			IGREffectParticles iGREffectParticles = this.createEffect(effid, single);
			bool flag = iGREffectParticles != null;
			if (flag)
			{
				IGRCharacter iGRCharacter = this.m_gr as IGRCharacter;
				bool flag2 = iGRCharacter != null;
				if (flag2)
				{
					iGRCharacter.attachEntity(attachID, iGREffectParticles);
				}
				else
				{
					DebugTrace.print("attachEffect should be IGRCharacter!");
				}
			}
			return iGREffectParticles;
		}

		protected void deleteEffects()
		{
			foreach (List<IGREffectParticles> current in this.m_effectLoad.Values)
			{
				foreach (IGREntity current2 in current)
				{
					current2.dispose();
					this.g_mgr.deleteEntity(current2);
				}
			}
		}

		protected IGREffectParticles attachEffect(string effid, bool single = true)
		{
			IGREffectParticles iGREffectParticles = this.createEffect(effid, single);
			bool flag = iGREffectParticles != null;
			if (flag)
			{
				GREntity3D gREntity3D = this.m_gr as GREntity3D;
				bool flag2 = gREntity3D != null;
				if (!flag2)
				{
					DebugTrace.print("attachEffect should be IGRCharacter!");
				}
			}
			return iGREffectParticles;
		}

		protected IGREffectParticles getSingleEffect(string effid)
		{
			IGREffectParticles result = null;
			bool flag = this.m_effectLoad.ContainsKey(effid);
			if (flag)
			{
				List<IGREffectParticles> list = this.m_effectLoad[effid];
				bool flag2 = list.Count > 0;
				if (flag2)
				{
					result = list[0];
				}
			}
			return result;
		}

		public virtual void updateProcess(float tmSlice)
		{
		}
	}
}
