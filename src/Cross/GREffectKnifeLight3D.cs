using System;
using System.Collections.Generic;

namespace Cross
{
	public class GREffectKnifeLight3D : GREntity3D, IGREffectKnifeLight, IGREntity
	{
		protected IEffectKnifeLight m_effect;

		protected List<Vec2> _tempLV2 = new List<Vec2>();

		protected List<Vec3> _tempLV3 = new List<Vec3>();

		protected Vec3 _tempv3 = new Vec3();

		public IAssetMaterial asset
		{
			set
			{
				this.m_effect.asset = value;
			}
		}

		public float lifetTime
		{
			get
			{
				return this.m_effect.lifetTime;
			}
			set
			{
				this.m_effect.lifetTime = value;
			}
		}

		public float minVertexDistance
		{
			get
			{
				return this.m_effect.minVertexDistance;
			}
			set
			{
				this.m_effect.minVertexDistance = value;
			}
		}

		public int maxNumberOfPoints
		{
			get
			{
				return this.m_effect.maxNumberOfPoints;
			}
			set
			{
				this.m_effect.maxNumberOfPoints = value;
			}
		}

		public bool useForwardOverride
		{
			get
			{
				return this.m_effect.useForwardOverride;
			}
			set
			{
				this.m_effect.useForwardOverride = value;
			}
		}

		public bool stretchToFit
		{
			get
			{
				return this.m_effect.stretchToFit;
			}
			set
			{
				this.m_effect.stretchToFit = value;
			}
		}

		public bool forwardOverideRelative
		{
			get
			{
				return this.m_effect.forwardOverideRelative;
			}
			set
			{
				this.m_effect.forwardOverideRelative = value;
			}
		}

		public Vec3 forwardOverride
		{
			get
			{
				return this.m_effect.forwardOverride;
			}
			set
			{
				this.m_effect.forwardOverride = value;
			}
		}

		public List<Vec2> sizeOverLife
		{
			get
			{
				return this.m_effect.sizeOverLife;
			}
			set
			{
				this.m_effect.sizeOverLife = value;
			}
		}

		public List<Vec3> colorOverLife
		{
			get
			{
				return this.m_effect.colorOverLife;
			}
			set
			{
				this.m_effect.colorOverLife = value;
			}
		}

		public GREffectKnifeLight3D(string id, GRWorld3D world) : base(id, world)
		{
			this.m_effect = world.scene3d.createEffectKnifeLight();
			this.m_rootObj.addChild(this.m_effect);
		}

		public override void load(Variant conf, IShader mtrl = null, Action onFin = null)
		{
			base.load(conf, null, null);
			bool flag = conf.ContainsKey("file");
			if (flag)
			{
				this.m_effect.asset = os.asset.getAsset<IAssetMaterial>(conf["file"]._str);
			}
			bool flag2 = conf.ContainsKey("liftime");
			if (flag2)
			{
				bool flag3 = conf["liftime"][0].ContainsKey("time");
				if (flag3)
				{
					this.m_effect.lifetTime = conf["liftime"][0]["time"]._float;
				}
			}
			bool flag4 = conf.ContainsKey("sizeliffe");
			if (flag4)
			{
				Vec2 vec = new Vec2();
				Vec2 vec2 = new Vec2();
				this._tempLV2.Clear();
				bool flag5 = conf["sizeliffe"][0].ContainsKey("startx");
				if (flag5)
				{
					vec.x = conf["sizeliffe"][0]["startx"]._float;
				}
				bool flag6 = conf["sizeliffe"][0].ContainsKey("starty");
				if (flag6)
				{
					vec.y = conf["sizeliffe"][0]["starty"]._float;
				}
				this._tempLV2.Add(vec);
				bool flag7 = conf["sizeliffe"][0].ContainsKey("overx");
				if (flag7)
				{
					vec2.x = conf["sizeliffe"][0]["overx"]._float;
				}
				bool flag8 = conf["sizeliffe"][0].ContainsKey("overy");
				if (flag8)
				{
					vec2.y = conf["sizeliffe"][0]["overy"]._float;
				}
				this._tempLV2.Add(vec2);
				this.m_effect.sizeOverLife = this._tempLV2;
			}
			bool flag9 = conf.ContainsKey("colorlife");
			if (flag9)
			{
				Vec3 vec3 = new Vec3();
				Vec3 vec4 = new Vec3();
				this._tempLV3.Clear();
				bool flag10 = conf["colorlife"][0].ContainsKey("startx");
				if (flag10)
				{
					vec3.x = conf["colorlife"][0]["startx"]._float;
				}
				bool flag11 = conf["colorlife"][0].ContainsKey("starty");
				if (flag11)
				{
					vec3.y = conf["colorlife"][0]["starty"]._float;
				}
				bool flag12 = conf["colorlife"][0].ContainsKey("startz");
				if (flag12)
				{
					vec3.z = conf["colorlife"][0]["startz"]._float;
				}
				this._tempLV3.Add(vec3);
				bool flag13 = conf["colorlife"][0].ContainsKey("overx");
				if (flag13)
				{
					vec4.x = conf["colorlife"][0]["overx"]._float;
				}
				bool flag14 = conf["colorlife"][0].ContainsKey("overy");
				if (flag14)
				{
					vec4.y = conf["colorlife"][0]["overy"]._float;
				}
				bool flag15 = conf["colorlife"][0].ContainsKey("overz");
				if (flag15)
				{
					vec4.z = conf["colorlife"][0]["overz"]._float;
				}
				this._tempLV3.Add(vec4);
				this.m_effect.colorOverLife = this._tempLV3;
			}
			bool flag16 = conf.ContainsKey("stf");
			if (flag16)
			{
				bool flag17 = conf["stf"][0].ContainsKey("stretch");
				if (flag17)
				{
					this.m_effect.stretchToFit = conf["stf"][0]["stretch"]._bool;
				}
			}
			bool flag18 = conf.ContainsKey("ufo");
			if (flag18)
			{
				bool flag19 = conf["ufo"][0].ContainsKey("ufo");
				if (flag19)
				{
					this.m_effect.useForwardOverride = conf["ufo"][0]["ufo"]._bool;
				}
			}
			bool flag20 = conf.ContainsKey("fo");
			if (flag20)
			{
				bool flag21 = conf["fo"][0].ContainsKey("fox");
				if (flag21)
				{
					this._tempv3.x = conf["fo"][0]["fox"]._float;
				}
				bool flag22 = conf["fo"][0].ContainsKey("foy");
				if (flag22)
				{
					this._tempv3.y = conf["fo"][0]["foy"]._float;
				}
				bool flag23 = conf["fo"][0].ContainsKey("foz");
				if (flag23)
				{
					this._tempv3.z = conf["fo"][0]["foz"]._float;
				}
				this.m_effect.forwardOverride = this._tempv3;
			}
			bool flag24 = conf.ContainsKey("for");
			if (flag24)
			{
				bool flag25 = conf["for"][0].ContainsKey("for");
				if (flag25)
				{
					this.m_effect.forwardOverideRelative = conf["for"][0]["for"]._bool;
				}
			}
			bool flag26 = conf.ContainsKey("mvd");
			if (flag26)
			{
				bool flag27 = conf["mvd"][0].ContainsKey("mvd");
				if (flag27)
				{
					this.m_effect.minVertexDistance = conf["mvd"][0]["mvd"]._float;
				}
			}
			bool flag28 = conf.ContainsKey("mno");
			if (flag28)
			{
				bool flag29 = conf["mno"][0].ContainsKey("mno");
				if (flag29)
				{
					this.m_effect.maxNumberOfPoints = conf["mno"][0]["mno"]._int;
				}
			}
		}

		public void play()
		{
			this.m_effect.play();
		}

		public void stop()
		{
			this.m_effect.stop();
		}
	}
}
