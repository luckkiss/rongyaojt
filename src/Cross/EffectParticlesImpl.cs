using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class EffectParticlesImpl : EffectImpl, IEffectParticles, IEffect, IGraphObject3D, IGraphObject
	{
		private AssetParticlesImpl m_asset;

		public GameObject m_effectObj;

		protected bool m_assetLoaded = false;

		protected bool m_loop = false;

		protected bool m_isOnce = true;

		protected bool m_isPlay = false;

		private float m_fpsTime;

		private Dictionary<float, Action> m_EventFunc = new Dictionary<float, Action>();

		private Dictionary<float, float> m_overdFun = new Dictionary<float, float>();

		public override int layer
		{
			get
			{
				return this.m_layer;
			}
			set
			{
				bool flag = value < 0 || value > 7;
				if (!flag)
				{
					bool flag2 = this.m_effectObj != null;
					if (flag2)
					{
					}
				}
			}
		}

		public IAssetParticles asset
		{
			get
			{
				return this.m_asset;
			}
			set
			{
				this.m_asset = (value as AssetParticlesImpl);
				this.m_assetLoaded = false;
				this.m_effectObj = null;
				bool isReady = this.m_asset.isReady;
				if (isReady)
				{
					this.onPreRender();
				}
				else
				{
					this.m_asset.addCallbacks(delegate(IAsset ast)
					{
						bool flag = this.m_u3dObj == null;
						if (!flag)
						{
							this.onPreRender();
							this.play(1f);
						}
					}, null, null);
				}
			}
		}

		public float duration
		{
			get
			{
				bool isReady = this.m_asset.isReady;
				float result;
				if (isReady)
				{
					result = this.m_effectObj.get_particleSystem().duration;
				}
				else
				{
					result = 0f;
				}
				return result;
			}
			set
			{
			}
		}

		public bool loop
		{
			get
			{
				return this.m_loop;
			}
			set
			{
				this.m_loop = value;
				this.m_isOnce = !this.m_loop;
				bool flag = this.m_effectObj == null;
				if (!flag)
				{
					bool flag2 = this.m_effectObj.GetComponent<ParticleSystem>() == null;
					if (flag2)
					{
						bool flag3 = this.m_effectObj.GetComponentsInChildren<ParticleSystem>().Length != 0;
						if (flag3)
						{
							ParticleSystem[] componentsInChildren = this.m_effectObj.GetComponentsInChildren<ParticleSystem>();
							for (int i = 0; i < componentsInChildren.Length; i++)
							{
								ParticleSystem particleSystem = componentsInChildren[i];
								particleSystem.loop = this.m_loop;
							}
						}
					}
					else
					{
						this.m_effectObj.GetComponent<ParticleSystem>().loop = this.m_loop;
					}
				}
			}
		}

		public bool isPlaying
		{
			get
			{
				bool flag = this.m_effectObj == null;
				return !flag && this.m_isPlay;
			}
		}

		public EffectParticlesImpl()
		{
			this.m_u3dObj.name = "Effect";
			this.m_u3dObj.AddComponent<MeshRenderer>();
		}

		public override void dispose()
		{
			base.dispose();
		}

		public void addEventListener(float time, Action finFun)
		{
			this.m_EventFunc[time] = finFun;
		}

		public void removeEventListener(float time)
		{
			this.m_EventFunc.Remove(time);
		}

		public override void onPreRender()
		{
			bool flag = this.m_asset == null;
			if (!flag)
			{
				this.m_asset.visit();
				bool assetLoaded = this.m_assetLoaded;
				if (!assetLoaded)
				{
					bool flag2 = this.m_u3dObj == null;
					if (!flag2)
					{
						bool flag3 = !this.m_asset.isReady;
						if (!flag3)
						{
							this.m_effectObj = (UnityEngine.Object.Instantiate(this.m_asset.assetObj) as GameObject);
							Vector3 localPosition = new Vector3(this.m_effectObj.transform.localPosition.x, this.m_effectObj.transform.localPosition.y, this.m_effectObj.transform.localPosition.z);
							Vector3 localEulerAngles = new Vector3(this.m_effectObj.transform.localEulerAngles.x, this.m_effectObj.transform.localEulerAngles.y, this.m_effectObj.transform.localEulerAngles.z);
							Vector3 localScale = new Vector3(this.m_effectObj.transform.localScale.x, this.m_effectObj.transform.localScale.y, this.m_effectObj.transform.localScale.z);
							this.m_effectObj.transform.parent = this.m_u3dObj.transform;
							this.m_effectObj.transform.localPosition = localPosition;
							this.m_effectObj.transform.localEulerAngles = localEulerAngles;
							this.m_effectObj.transform.localScale = localScale;
							Animation[] componentsInChildren = this.m_effectObj.GetComponentsInChildren<Animation>();
							for (int i = 0; i < componentsInChildren.Length; i++)
							{
								componentsInChildren[i].Play();
							}
							Animator[] componentsInChildren2 = this.m_effectObj.GetComponentsInChildren<Animator>();
							for (int j = 0; j < componentsInChildren2.Length; j++)
							{
								componentsInChildren2[j].speed = 0f;
							}
							bool flag4 = this.m_effectObj.GetComponent<ParticleSystem>() == null;
							if (flag4)
							{
								bool flag5 = this.m_effectObj.GetComponentsInChildren<ParticleSystem>().Length != 0;
								if (flag5)
								{
									ParticleSystem[] componentsInChildren3 = this.m_effectObj.GetComponentsInChildren<ParticleSystem>();
									for (int k = 0; k < componentsInChildren3.Length; k++)
									{
										ParticleSystem particleSystem = componentsInChildren3[k];
										particleSystem.loop = this.m_loop;
									}
								}
							}
							else
							{
								this.m_effectObj.GetComponent<ParticleSystem>().loop = this.m_loop;
							}
							LinkedList<GameObject> linkedList = new LinkedList<GameObject>();
							linkedList.AddLast(this.m_effectObj);
							while (linkedList.Count > 0)
							{
								GameObject value = linkedList.First.Value;
								linkedList.RemoveFirst();
								bool flag6 = value.get_particleSystem() != null;
								if (flag6)
								{
									value.get_particleSystem().Stop();
								}
								for (int l = 0; l < value.transform.childCount; l++)
								{
									linkedList.AddLast(value.transform.GetChild(l).gameObject);
								}
							}
							this.m_assetLoaded = true;
						}
					}
				}
			}
		}

		public void play(float speed = 1f)
		{
			this.m_fpsTime = 0f;
			this.m_overdFun.Clear();
			bool flag = this.m_asset == null || !this.m_asset.isReady;
			if (!flag)
			{
				this.m_isOnce = true;
				this.m_isPlay = true;
				bool flag2 = this.m_effectObj == null;
				if (flag2)
				{
					this.onPreRender();
					bool flag3 = this.m_effectObj == null;
					if (flag3)
					{
						return;
					}
				}
				LinkedList<GameObject> linkedList = new LinkedList<GameObject>();
				linkedList.AddLast(this.m_effectObj);
				while (linkedList.Count > 0)
				{
					GameObject value = linkedList.First.Value;
					linkedList.RemoveFirst();
					bool flag4 = value.get_particleSystem() != null;
					if (flag4)
					{
						value.get_particleSystem().Play();
					}
					for (int i = 0; i < value.transform.childCount; i++)
					{
						linkedList.AddLast(value.transform.GetChild(i).gameObject);
					}
				}
				Animation[] componentsInChildren = this.m_effectObj.GetComponentsInChildren<Animation>();
				bool flag5 = componentsInChildren != null;
				if (flag5)
				{
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						componentsInChildren[j].Play();
					}
				}
				Animator[] componentsInChildren2 = this.m_effectObj.GetComponentsInChildren<Animator>();
				bool flag6 = componentsInChildren2 != null;
				if (flag6)
				{
					for (int k = 0; k < componentsInChildren2.Length; k++)
					{
						componentsInChildren2[k].speed = speed;
						bool flag7 = componentsInChildren2[k].layerCount > 0;
						if (flag7)
						{
							componentsInChildren2[k].Play(0);
						}
					}
				}
			}
		}

		protected void getChild(Transform obj)
		{
			foreach (Transform transform in obj)
			{
				bool flag = transform != null;
				if (flag)
				{
					this.getChild(transform);
				}
				bool flag2 = transform.gameObject.GetComponent<ParticleSystem>() != null;
				if (flag2)
				{
					transform.gameObject.get_particleSystem().Play();
				}
			}
		}

		public void stop()
		{
			this.m_isPlay = false;
			bool flag = this.m_effectObj == null;
			if (!flag)
			{
				LinkedList<GameObject> linkedList = new LinkedList<GameObject>();
				linkedList.AddLast(this.m_effectObj);
				while (linkedList.Count > 0)
				{
					GameObject value = linkedList.First.Value;
					linkedList.RemoveFirst();
					bool flag2 = value.get_particleSystem() != null;
					if (flag2)
					{
						value.get_particleSystem().Stop();
					}
					for (int i = 0; i < value.transform.childCount; i++)
					{
						linkedList.AddLast(value.transform.GetChild(i).gameObject);
					}
				}
			}
		}

		public override void onProcess(float tmSlice)
		{
			base.onProcess(tmSlice);
			bool isPlay = this.m_isPlay;
			if (isPlay)
			{
				this.m_fpsTime += tmSlice;
				this.m_fpsTime = float.Parse(this.m_fpsTime.ToString("f3"));
				foreach (float current in this.m_EventFunc.Keys)
				{
					bool flag = this.m_overdFun.ContainsKey(current);
					if (flag)
					{
						break;
					}
					bool flag2 = this.m_fpsTime >= current;
					if (flag2)
					{
						this.m_EventFunc[current]();
						this.m_overdFun[current] = current;
					}
				}
			}
		}
	}
}
