using System;

namespace Cross
{
	public class GREffectParticles3D : GREntity3D, IGREffectParticles, IGREntity
	{
		protected IEffectParticles m_effect;

		public bool isPlaying
		{
			get
			{
				return this.m_effect.isPlaying;
			}
		}

		public IAssetParticles asset
		{
			set
			{
				this.m_effect.asset = value;
			}
		}

		public float duration
		{
			get
			{
				return this.m_effect.duration;
			}
			set
			{
				this.m_effect.duration = value;
			}
		}

		public bool loop
		{
			get
			{
				return this.m_effect.loop;
			}
			set
			{
				this.m_effect.loop = value;
			}
		}

		public GREffectParticles3D(string id, GRWorld3D world) : base(id, world)
		{
			this.m_effect = world.scene3d.createEffectParticles();
			this.m_rootObj.addChild(this.m_effect);
		}

		public override void load(Variant conf, IShader mtrl = null, Action onFin = null)
		{
			bool flag = conf == null;
			if (!flag)
			{
				bool flag2 = conf.ContainsKey("file");
				if (flag2)
				{
					this.m_effect.asset = os.asset.getAsset<IAssetParticles>(conf["file"]._str);
				}
			}
		}

		public void play()
		{
			this.m_effect.play(1f);
		}

		public void stop()
		{
			this.m_effect.stop();
		}

		public void addEventListener(float time, Action finFun)
		{
			this.m_effect.addEventListener(time, finFun);
		}

		public void removeEventListener(float time)
		{
			this.m_effect.removeEventListener(time);
		}
	}
}
