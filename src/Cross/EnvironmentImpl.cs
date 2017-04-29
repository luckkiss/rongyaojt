using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Cross
{
	public class EnvironmentImpl : IEnvironment
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly EnvironmentImpl.<>c <>9 = new EnvironmentImpl.<>c();

			public static Action<IURLReq, float> <>9__28_1;

			public static Action<IURLReq, string> <>9__28_2;

			public static Action<IURLReq, object> <>9__31_0;

			public static Action<IURLReq, float> <>9__31_1;

			public static Action<IURLReq, string> <>9__31_2;

			internal void <set_lightmap>b__28_1(IURLReq r, float progress)
			{
			}

			internal void <set_lightmap>b__28_2(IURLReq r, string err)
			{
			}

			internal void <set_lightprobes>b__31_0(IURLReq r, object data)
			{
				LightmapSettings.lightProbes = (data as LightProbes);
			}

			internal void <set_lightprobes>b__31_1(IURLReq r, float progress)
			{
			}

			internal void <set_lightprobes>b__31_2(IURLReq r, string err)
			{
			}
		}

		protected List<string> m_Lightmapres = new List<string>();

		protected List<LightmapData> m_lightmapDatas = new List<LightmapData>();

		protected string m_lightprobes;

		protected string m_skybox;

		public bool displayfog
		{
			get
			{
				return RenderSettings.fog;
			}
			set
			{
				RenderSettings.fog = value;
			}
		}

		public float fogden
		{
			get
			{
				return RenderSettings.fogDensity;
			}
			set
			{
				RenderSettings.fogDensity = value;
			}
		}

		public Vec4 fogcolor
		{
			get
			{
				return new Vec4(RenderSettings.fogColor.r, RenderSettings.fogColor.g, RenderSettings.fogColor.b, RenderSettings.fogColor.a);
			}
			set
			{
				RenderSettings.fogColor = new Color(value.x, value.y, value.z, value.w);
			}
		}

		public string fogmode
		{
			get
			{
				return string.Concat(RenderSettings.fogMode);
			}
			set
			{
				bool flag = value == "Exponential";
				if (flag)
				{
					RenderSettings.fogMode = FogMode.Exponential;
				}
				else
				{
					bool flag2 = value == "ExponentialSquared";
					if (flag2)
					{
						RenderSettings.fogMode = FogMode.ExponentialSquared;
					}
					else
					{
						RenderSettings.fogMode = FogMode.Linear;
					}
				}
			}
		}

		public float strdistance
		{
			get
			{
				return RenderSettings.fogStartDistance;
			}
			set
			{
				RenderSettings.fogStartDistance = value;
			}
		}

		public float enddistance
		{
			get
			{
				return RenderSettings.fogEndDistance;
			}
			set
			{
				RenderSettings.fogEndDistance = value;
			}
		}

		public Vec4 ambcolor
		{
			get
			{
				return new Vec4(RenderSettings.ambientLight.r, RenderSettings.ambientLight.g, RenderSettings.ambientLight.b, RenderSettings.ambientLight.a);
			}
			set
			{
				RenderSettings.ambientLight = new Color(value.x, value.y, value.z, value.w);
			}
		}

		public List<string> lightmap
		{
			get
			{
				return this.m_Lightmapres;
			}
			set
			{
				bool flag = value != null;
				if (flag)
				{
					this.m_Lightmapres.Clear();
					this.m_lightmapDatas.Clear();
					this.m_Lightmapres = value;
					bool async = os.asset.async;
					if (async)
					{
						int cur_loaded = 0;
						for (int i = 0; i < this.m_Lightmapres.Count; i++)
						{
							string text = this.m_Lightmapres[i];
							LightmapData lightmapData = new LightmapData();
							URLReqImpl arg_129_0 = new URLReqImpl
							{
								dataFormat = "assetbundle",
								url = (this.m_Lightmapres[i].IndexOf(".lmp") < 0) ? (this.m_Lightmapres[i] + ".lmp") : this.m_Lightmapres[i]
							};
							Action<IURLReq, object> arg_129_1 = delegate(IURLReq r, object data)
							{
								lightmapData.lightmapFar = (data as Texture2D);
								int cur_loaded = cur_loaded;
								cur_loaded++;
								bool flag2 = cur_loaded == this.m_Lightmapres.Count;
								if (flag2)
								{
									LightmapSettings.lightmaps = this.m_lightmapDatas.ToArray();
								}
							};
							Action<IURLReq, float> arg_129_2;
							if ((arg_129_2 = EnvironmentImpl.<>c.<>9__28_1) == null)
							{
								arg_129_2 = (EnvironmentImpl.<>c.<>9__28_1 = new Action<IURLReq, float>(EnvironmentImpl.<>c.<>9.<set_lightmap>b__28_1));
							}
							Action<IURLReq, string> arg_129_3;
							if ((arg_129_3 = EnvironmentImpl.<>c.<>9__28_2) == null)
							{
								arg_129_3 = (EnvironmentImpl.<>c.<>9__28_2 = new Action<IURLReq, string>(EnvironmentImpl.<>c.<>9.<set_lightmap>b__28_2));
							}
							arg_129_0.load(arg_129_1, arg_129_2, arg_129_3);
							this.m_lightmapDatas.Add(lightmapData);
						}
					}
					else
					{
						for (int j = 0; j < this.m_Lightmapres.Count; j++)
						{
							LightmapData lightmapData2 = new LightmapData();
							lightmapData2.lightmapFar = (Resources.Load(this.m_Lightmapres[j]) as Texture2D);
							this.m_lightmapDatas.Add(lightmapData2);
						}
						LightmapSettings.lightmaps = this.m_lightmapDatas.ToArray();
					}
				}
			}
		}

		public string lightprobes
		{
			get
			{
				return this.m_lightprobes;
			}
			set
			{
				bool flag = value != null;
				if (flag)
				{
					bool async = os.asset.async;
					if (async)
					{
						this.m_lightprobes = value;
						URLReqImpl arg_C8_0 = new URLReqImpl
						{
							dataFormat = "assetbundle",
							url = (this.m_lightprobes.IndexOf(".lpb") < 0) ? (this.m_lightprobes + ".lpb") : this.m_lightprobes
						};
						Action<IURLReq, object> arg_C8_1;
						if ((arg_C8_1 = EnvironmentImpl.<>c.<>9__31_0) == null)
						{
							arg_C8_1 = (EnvironmentImpl.<>c.<>9__31_0 = new Action<IURLReq, object>(EnvironmentImpl.<>c.<>9.<set_lightprobes>b__31_0));
						}
						Action<IURLReq, float> arg_C8_2;
						if ((arg_C8_2 = EnvironmentImpl.<>c.<>9__31_1) == null)
						{
							arg_C8_2 = (EnvironmentImpl.<>c.<>9__31_1 = new Action<IURLReq, float>(EnvironmentImpl.<>c.<>9.<set_lightprobes>b__31_1));
						}
						Action<IURLReq, string> arg_C8_3;
						if ((arg_C8_3 = EnvironmentImpl.<>c.<>9__31_2) == null)
						{
							arg_C8_3 = (EnvironmentImpl.<>c.<>9__31_2 = new Action<IURLReq, string>(EnvironmentImpl.<>c.<>9.<set_lightprobes>b__31_2));
						}
						arg_C8_0.load(arg_C8_1, arg_C8_2, arg_C8_3);
					}
					else
					{
						this.m_lightprobes = value;
						LightProbes lightProbes = new LightProbes();
						lightProbes = (Resources.Load(this.m_lightprobes) as LightProbes);
						LightmapSettings.lightProbes = lightProbes;
					}
				}
			}
		}

		public string skybox
		{
			get
			{
				return this.m_skybox;
			}
			set
			{
			}
		}
	}
}
