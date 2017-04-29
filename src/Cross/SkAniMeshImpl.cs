using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class SkAniMeshImpl : AniMeshImpl, ISkAniMesh, IAniMesh, IGraphObject3D, IGraphObject
	{
		public static int MTL_Main_Tex = -1;

		public static int MTL_Dead_Tex = -1;

		public QS_ACT_STATE m_curActState = QS_ACT_STATE.NONE;

		private Func<bool> m_ActActOver_CB = null;

		private string m_bPlayingActAction = null;

		private int m_PlayingActLoop = 1;

		private int m_nAnimPlayLevel = 0;

		protected Dictionary<string, string> m_animPath = new Dictionary<string, string>();

		private string m_strCurPlayAnim = null;

		private int m_nCurPlayLoop = 0;

		private SkinnedMeshRenderer[] m_curSMRs = null;

		protected AssetSkAniMeshImpl m_asset = null;

		protected GameObject m_animatorObj = null;

		protected Animation m_animator = null;

		protected Dictionary<IGraphObject3D, string> m_attach = new Dictionary<IGraphObject3D, string>();

		protected Dictionary<string, IAssetSkAnimation> m_animMap = new Dictionary<string, IAssetSkAnimation>();

		protected Dictionary<string, GameObject> m_skinMap = new Dictionary<string, GameObject>();

		protected Dictionary<string, ShaderImpl> m_skinMtrlMap = new Dictionary<string, ShaderImpl>();

		protected Dictionary<string, ShaderImpl> m_mtrlMap = new Dictionary<string, ShaderImpl>();

		protected Dictionary<string, bool> m_activeMap = new Dictionary<string, bool>();

		protected Dictionary<IAsset, IAsset> m_assetLoading = new Dictionary<IAsset, IAsset>();

		protected List<IAsset> m_tmpList = new List<IAsset>();

		protected bool m_playing = false;

		protected string m_curAnimName = "";

		protected float m_curSpeed = 1f;

		protected WrapMode m_wrapMode = WrapMode.ClampForever;

		protected float m_poSui;

		protected Action finFun;

		private Renderer[] rds;

		private bool m_enableLighting = true;

		protected Light m_light;

		protected bool m_Loop = false;

		private float m_fpsTime;

		private bool m_isPlay;

		private Dictionary<string, Dictionary<float, Action<string, float>>> m_EventFunc = new Dictionary<string, Dictionary<float, Action<string, float>>>();

		private Dictionary<string, Dictionary<float, float>> m_overdFun = new Dictionary<string, Dictionary<float, float>>();

		private string m_skainName = "";

		private Dictionary<float, float> m_overFunTime = new Dictionary<float, float>();

		public IAssetSkAniMesh asset
		{
			get
			{
				return this.m_asset;
			}
			set
			{
				bool flag = value == null;
				if (!flag)
				{
					bool flag2 = this.m_asset != null && this.m_assetLoading.ContainsKey(this.m_asset);
					if (flag2)
					{
						this.m_assetLoading.Remove(this.m_asset);
					}
					this.m_asset = (value as AssetSkAniMeshImpl);
					this.m_animatorObj = null;
					this.m_assetLoading[this.m_asset] = this.m_asset;
					this.m_activeMap = new Dictionary<string, bool>();
					bool flag3 = !this.m_asset.isReady;
					if (flag3)
					{
						this.m_asset.load();
					}
				}
			}
		}

		public Dictionary<string, IAssetSkAnimation>.KeyCollection animKeys
		{
			get
			{
				return this.m_animMap.Keys;
			}
		}

		public int numAnims
		{
			get
			{
				return this.m_animMap.Count;
			}
		}

		public IAssetSkAnimation curAnim
		{
			get
			{
				bool flag = this.m_curAnimName == "";
				IAssetSkAnimation result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_animMap[this.m_curAnimName];
				}
				return result;
			}
		}

		public string curAnimName
		{
			get
			{
				return this.m_curAnimName;
			}
		}

		public override int layer
		{
			get
			{
				return this.m_layer;
			}
			set
			{
				this.m_layer = value;
				base.setLayer(this.m_u3dObj, value);
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					this.rds = this.m_u3dObj.transform.GetComponentsInChildren<Renderer>();
					for (int i = 0; i < this.rds.Length; i++)
					{
					}
				}
			}
		}

		public bool enableLighting
		{
			get
			{
				return this.m_enableLighting;
			}
			set
			{
				this.m_enableLighting = value;
				bool enableLighting = this.m_enableLighting;
				if (enableLighting)
				{
					bool flag = this.m_light == null;
					if (flag)
					{
						GameObject gameObject = new GameObject();
						gameObject.transform.rotation = Quaternion.Euler(51f, 0f, 1.2f);
						gameObject.name = "light";
						this.m_light = gameObject.AddComponent<Light>();
						this.m_light.type = LightType.Directional;
						this.m_light.intensity = 0.3f;
						gameObject.transform.position = this.m_u3dObj.transform.position;
						gameObject.transform.parent = this.m_u3dObj.transform;
					}
					else
					{
						this.m_light.gameObject.SetActive(true);
					}
				}
				else
				{
					bool flag2 = this.m_light != null;
					if (flag2)
					{
						this.m_light.gameObject.SetActive(false);
					}
				}
			}
		}

		public void setDefaultFirstAnim(string anim)
		{
			bool flag = !this.m_animMap.ContainsKey(anim);
			if (flag)
			{
				bool flag2 = !this.m_animPath.ContainsKey(anim);
				if (!flag2)
				{
					IAssetSkAnimation asset = os.asset.getAsset<IAssetSkAnimation>(this.m_animPath[anim]);
					this.addAnim(anim, asset);
					this.m_strCurPlayAnim = anim;
				}
			}
		}

		public void setActActionOverCB(Func<bool> callback)
		{
			this.m_ActActOver_CB = callback;
		}

		public void pushAnim(string animName, string animPath)
		{
			this.m_animPath[animName] = animPath;
		}

		private void play_AnimClip(string anim, int loop)
		{
			bool flag = this.m_animator == null;
			if (!flag)
			{
				float fadeLength = 0.1f;
				bool flag2 = this.m_animator.GetClip(anim) == null;
				if (flag2)
				{
					bool flag3 = !this.m_animMap.ContainsKey(anim);
					if (flag3)
					{
						bool flag4 = !this.m_animPath.ContainsKey(anim);
						if (!flag4)
						{
							IAssetSkAnimation asset = os.asset.getAsset<IAssetSkAnimation>(this.m_animPath[anim]);
							this.addAnim(anim, asset);
							this.m_strCurPlayAnim = anim;
							this.m_nCurPlayLoop = loop;
						}
					}
					else
					{
						bool isReady = this.m_animMap[anim].isReady;
						if (isReady)
						{
							AssetSkAnimationImpl assetSkAnimationImpl = this.m_animMap[anim] as AssetSkAnimationImpl;
							this.m_animator.AddClip(assetSkAnimationImpl.anim, anim);
							bool flag5 = this.m_curAnimName == anim;
							if (flag5)
							{
								this.m_animator[this.m_curAnimName].normalizedTime = 0f;
							}
							else
							{
								this.m_animator.CrossFade(anim, fadeLength);
								this.m_curAnimName = anim;
							}
							this.m_strCurPlayAnim = null;
							bool flag6 = loop > 0;
							if (flag6)
							{
								this.m_PlayingActLoop = loop;
								this.m_bPlayingActAction = anim;
								this.m_nAnimPlayLevel = 1;
							}
						}
					}
				}
				else
				{
					bool flag7 = this.m_curAnimName == anim;
					if (flag7)
					{
						this.m_animator[this.m_curAnimName].normalizedTime = 0f;
					}
					else
					{
						this.m_animator.CrossFade(anim, fadeLength);
						this.m_curAnimName = anim;
					}
					this.m_strCurPlayAnim = null;
					bool flag8 = loop > 0;
					if (flag8)
					{
						this.m_PlayingActLoop = loop;
						this.m_bPlayingActAction = anim;
						this.m_nAnimPlayLevel = 1;
					}
				}
			}
		}

		private void play_ActState()
		{
			this.m_nAnimPlayLevel = 0;
			bool flag = this.m_curActState == QS_ACT_STATE.IDLE;
			if (flag)
			{
				this.play_AnimClip("idle", 0);
			}
			bool flag2 = this.m_curActState == QS_ACT_STATE.RUN;
			if (flag2)
			{
				this.play_AnimClip("run", 0);
			}
			bool flag3 = this.m_curActState == QS_ACT_STATE.MOUNT_IDLE;
			if (flag3)
			{
				this.play_AnimClip("mount_idle", 0);
			}
			bool flag4 = this.m_curActState == QS_ACT_STATE.MOUNT_RUN;
			if (flag4)
			{
				this.play_AnimClip("mount_run", 0);
			}
		}

		public void stateact_Change(QS_ACT_STATE sta)
		{
			bool flag = this.m_curActState == sta;
			if (!flag)
			{
				this.m_curActState = sta;
				bool flag2 = this.m_bPlayingActAction == null;
				if (flag2)
				{
					this.play_ActState();
				}
			}
		}

		public void action_Speed(float s)
		{
			bool flag = this.m_animator == null;
			if (!flag)
			{
				bool flag2 = this.m_curAnimName.Length > 0;
				if (flag2)
				{
					this.m_animator[this.m_curAnimName].speed = s;
				}
			}
		}

		public void action_Play(string aniName, int loop)
		{
			this.play_AnimClip(aniName, loop);
		}

		public void setMtlInt(int nameid, int ndata)
		{
			bool flag = this.m_animatorObj != null;
			if (flag)
			{
				bool flag2 = this.m_curSMRs == null;
				if (flag2)
				{
					this.m_curSMRs = this.m_animatorObj.GetComponentsInChildren<SkinnedMeshRenderer>();
				}
				for (int i = 0; i < this.m_curSMRs.Length; i++)
				{
					this.m_curSMRs[i].material.SetInt(nameid, ndata);
				}
			}
		}

		public void setMtlFloat(int nameid, float fdata)
		{
			bool flag = this.m_animatorObj != null;
			if (flag)
			{
				bool flag2 = this.m_curSMRs == null;
				if (flag2)
				{
					this.m_curSMRs = this.m_animatorObj.GetComponentsInChildren<SkinnedMeshRenderer>();
				}
				for (int i = 0; i < this.m_curSMRs.Length; i++)
				{
					this.m_curSMRs[i].material.SetFloat(nameid, fdata);
				}
			}
		}

		public void setMtlColor(int nameid, Color color)
		{
			bool flag = this.m_animatorObj != null;
			if (flag)
			{
				bool flag2 = this.m_curSMRs == null;
				if (flag2)
				{
					this.m_curSMRs = this.m_animatorObj.GetComponentsInChildren<SkinnedMeshRenderer>();
				}
				for (int i = 0; i < this.m_curSMRs.Length; i++)
				{
					this.m_curSMRs[i].material.SetColor(nameid, color);
				}
			}
		}

		public bool changeMtl(Material ml)
		{
			bool flag = this.m_animatorObj != null;
			bool result;
			if (flag)
			{
				bool flag2 = this.m_curSMRs == null;
				if (flag2)
				{
					this.m_curSMRs = this.m_animatorObj.GetComponentsInChildren<SkinnedMeshRenderer>();
				}
				for (int i = 0; i < this.m_curSMRs.Length; i++)
				{
					Material material = UnityEngine.Object.Instantiate(ml) as Material;
					Texture texture = this.m_curSMRs[i].material.GetTexture(SkAniMeshImpl.MTL_Main_Tex);
					material.SetTexture(SkAniMeshImpl.MTL_Dead_Tex, texture);
					this.m_curSMRs[i].material = material;
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool IsRunningAnim()
		{
			return this.m_curActState == QS_ACT_STATE.RUN && this.m_nAnimPlayLevel == 0;
		}

		public SkAniMeshImpl()
		{
			this.m_u3dObj.name = "SkMesh";
		}

		public void addAnim(string animName, IAssetSkAnimation anim)
		{
			bool flag = this.m_animMap.ContainsKey(animName);
			if (flag)
			{
				bool flag2 = this.m_animator != null;
				if (flag2)
				{
					this.m_animator.RemoveClip(animName);
				}
			}
			this.m_animMap[animName] = anim;
			this.m_assetLoading[anim] = anim;
		}

		public void removeAnim(IAssetSkAnimation anim)
		{
			foreach (string current in this.m_animMap.Keys)
			{
				bool flag = this.m_animMap[current] == anim;
				if (flag)
				{
					this.m_animMap.Remove(current);
					bool flag2 = this.m_animator != null;
					if (flag2)
					{
						this.m_animator.RemoveClip(current);
					}
					this.m_assetLoading.Remove(anim);
					break;
				}
			}
		}

		public void removeAnim(string animName)
		{
			bool flag = !this.m_animMap.ContainsKey(animName);
			if (!flag)
			{
				IAssetSkAnimation key = this.m_animMap[animName];
				this.m_animMap.Remove(animName);
				bool flag2 = this.m_animator != null;
				if (flag2)
				{
					this.m_animator.RemoveClip(animName);
				}
				this.m_assetLoading.Remove(key);
				bool flag3 = animName == this.m_curAnimName;
				if (flag3)
				{
					this.m_curAnimName = "";
					this._stopAnimator();
					this.m_animator.Rewind();
					this.m_playing = false;
				}
			}
		}

		public void clearAnim()
		{
			foreach (string current in this.m_animMap.Keys)
			{
				bool flag = this.m_animator != null;
				if (flag)
				{
					this.m_animator.RemoveClip(current);
				}
				this.m_assetLoading.Remove(this.m_animMap[current]);
			}
			this.m_animMap.Clear();
		}

		public IAssetSkAnimation getAnim(string animName)
		{
			bool flag = !this.m_animMap.ContainsKey(animName);
			IAssetSkAnimation result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_animMap[animName];
			}
			return result;
		}

		public void attachObject(string sBoneName, IGraphObject3D obj)
		{
			bool flag = obj == null;
			if (!flag)
			{
				bool flag2 = obj.parent != null;
				if (flag2)
				{
					obj.parent.removeChild(obj);
				}
				this.m_attach[obj] = sBoneName;
				GameObject gameObject = this._getBone(sBoneName);
				bool flag3 = gameObject != null;
				if (flag3)
				{
					(obj as GraphObject3DImpl).attachTo(gameObject);
				}
			}
		}

		public void detachObject(string sBoneName, IGraphObject3D obj)
		{
			bool flag = obj == null;
			if (!flag)
			{
				bool flag2 = this.m_attach.ContainsKey(obj) && this.m_attach[obj] == sBoneName;
				if (flag2)
				{
					(obj as GraphObject3DImpl).attachTo(null);
					this.m_attach.Remove(obj);
				}
			}
		}

		public void play(string animName, bool isLoop, float speed)
		{
			bool flag = !this.m_animMap.ContainsKey(animName);
			if (!flag)
			{
				Debug.Log("播放动画 " + animName);
				this.m_playing = true;
				this.m_curAnimName = animName;
				this.m_Loop = isLoop;
				this.m_wrapMode = (isLoop ? WrapMode.Loop : WrapMode.ClampForever);
				this.m_curSpeed = speed;
				this._syncPlayState(true);
			}
		}

		public void stop()
		{
			this.m_playing = false;
			bool flag = this.m_animator != null;
			if (flag)
			{
				this._stopAnimator();
				this.m_animator.Rewind();
			}
		}

		public void pause()
		{
			this.m_playing = false;
			bool flag = this.m_animator != null;
			if (flag)
			{
				this._stopAnimator();
			}
		}

		public void resume()
		{
			this.m_playing = true;
			bool flag = this.m_animator != null;
			if (flag)
			{
				this._playerAnimator("");
			}
		}

		public override void _updateRealVisible()
		{
			base._updateRealVisible();
			bool active = this.m_u3dObj.active;
			if (active)
			{
				this.m_bPlayingActAction = null;
				this.m_curActState = QS_ACT_STATE.NONE;
				this.stateact_Change(QS_ACT_STATE.IDLE);
			}
		}

		public void activateSubMesh(string sub, bool active)
		{
			bool flag = this.m_animatorObj != null;
			if (flag)
			{
				Transform transform = this.m_animatorObj.transform.Find(sub);
				bool flag2 = transform != null;
				if (flag2)
				{
					transform.gameObject.SetActive(active);
				}
			}
			this.m_activeMap[sub] = active;
		}

		public void setMaterial(string sub, IShader mtrl)
		{
			bool flag = this.m_animatorObj != null;
			if (flag)
			{
				bool flag2 = sub != null && sub != "";
				SkinnedMeshRenderer[] componentsInChildren;
				if (flag2)
				{
					Transform transform = this.m_animatorObj.transform.Find(sub);
					componentsInChildren = transform.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
					this.m_mtrlMap[sub] = (mtrl as ShaderImpl);
				}
				else
				{
					componentsInChildren = this.m_animatorObj.GetComponentsInChildren<SkinnedMeshRenderer>();
					this.m_mtrlMap = new Dictionary<string, ShaderImpl>();
					this.m_mtrlMap[""] = (mtrl as ShaderImpl);
				}
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					bool flag3 = (mtrl as ShaderImpl).u3dMaterial != null;
					if (flag3)
					{
						componentsInChildren[i].material = (mtrl as ShaderImpl).u3dMaterial;
					}
					else
					{
						(mtrl as ShaderImpl).apply(componentsInChildren[i].material);
					}
				}
			}
			else
			{
				bool flag4 = sub != null && sub != "";
				if (flag4)
				{
					this.m_mtrlMap[sub] = (mtrl as ShaderImpl);
				}
				else
				{
					this.m_mtrlMap = new Dictionary<string, ShaderImpl>();
					this.m_mtrlMap[""] = (mtrl as ShaderImpl);
				}
			}
		}

		public override void onPreRender()
		{
			bool flag = this.m_u3dObj == null;
			if (!flag)
			{
				bool flag2 = this.m_assetLoading.Count == 0;
				if (!flag2)
				{
					bool flag3 = this.m_tmpList.Count > 0;
					if (flag3)
					{
						this.m_tmpList.Clear();
					}
					foreach (IAsset current in this.m_assetLoading.Keys)
					{
						bool isReady = current.isReady;
						if (isReady)
						{
							bool flag4 = current == this.m_asset;
							if (flag4)
							{
								try
								{
									this.m_animatorObj = (UnityEngine.Object.Instantiate(this.m_asset.assetObj) as GameObject);
								}
								catch (Exception var_9_B0)
								{
									DebugTrace.add(Define.DebugTrace.DTT_ERR, "The GameObject is null");
								}
								bool flag5 = this.m_animatorObj == null;
								if (flag5)
								{
									return;
								}
								SkAniMeshBehaviour skAniMeshBehaviour = this.m_animatorObj.AddComponent<SkAniMeshBehaviour>();
								skAniMeshBehaviour.obj = this;
								this.m_animator = this.m_animatorObj.GetComponent<Animation>();
								bool flag6 = this.m_animator == null;
								if (flag6)
								{
									this.m_animator = this.m_animatorObj.AddComponent<Animation>();
								}
								this.m_animator.cullingType = AnimationCullingType.AlwaysAnimate;
								osImpl.linkU3dObj(this.m_u3dObj, this.m_animatorObj);
								this._syncPlayState(false);
								Dictionary<IGraphObject3D, string> dictionary = new Dictionary<IGraphObject3D, string>(this.m_attach);
								foreach (IGraphObject3D current2 in dictionary.Keys)
								{
									this.attachObject(this.m_attach[current2], current2);
								}
								this.m_tmpList.Add(current);
							}
							else
							{
								bool flag7 = current is IAssetSkAnimation;
								if (flag7)
								{
									bool flag8 = this.m_animator != null;
									if (flag8)
									{
										AssetSkAnimationImpl assetSkAnimationImpl = current as AssetSkAnimationImpl;
										this._syncPlayState(false);
										this.m_tmpList.Add(current);
									}
								}
								else
								{
									bool flag9 = current is IAssetSkAniMesh;
									if (flag9)
									{
										bool flag10 = this.m_animatorObj != null;
										if (flag10)
										{
											AssetSkAniMeshImpl assetSkAniMeshImpl = current as AssetSkAniMeshImpl;
											List<string> list = new List<string>();
											foreach (string current3 in this.m_skinMap.Keys)
											{
												bool flag11 = current3.IndexOf(assetSkAniMeshImpl.path) == 0 && this.m_skinMap[current3] == null;
												if (flag11)
												{
													list.Add(current3);
												}
											}
											foreach (string current4 in list)
											{
												bool flag12 = current4.IndexOf(assetSkAniMeshImpl.path) == 0 && this.m_skinMap[current4] == null;
												if (flag12)
												{
													ShaderImpl shaderImpl = this.m_skinMtrlMap.ContainsKey(current4) ? this.m_skinMtrlMap[current4] : null;
													Transform transform = null;
													string name = current4.Substring(current4.IndexOf('$') + 1);
													try
													{
														transform = assetSkAniMeshImpl.assetObj.transform.FindChild(name);
													}
													catch (Exception var_30_347)
													{
													}
													bool flag13 = transform;
													if (flag13)
													{
														GameObject gameObject = transform.gameObject;
														GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
														osImpl.linkU3dObj(this.m_animatorObj, gameObject2);
														this.m_skinMap[current4] = gameObject2;
														bool flag14 = gameObject.transform.GetComponent<SkinnedMeshRenderer>() != null;
														SkinnedMeshRenderer[] array;
														SkinnedMeshRenderer[] array2;
														if (flag14)
														{
															array = new SkinnedMeshRenderer[]
															{
																gameObject.transform.GetComponent<SkinnedMeshRenderer>()
															};
															array2 = new SkinnedMeshRenderer[]
															{
																gameObject2.transform.GetComponent<SkinnedMeshRenderer>()
															};
														}
														else
														{
															array = gameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
															array2 = gameObject2.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
														}
														for (int i = 0; i < array.Length; i++)
														{
															SkinnedMeshRenderer skinnedMeshRenderer = array[i];
															SkinnedMeshRenderer skinnedMeshRenderer2 = array2[i];
															bool flag15 = shaderImpl != null;
															if (flag15)
															{
																bool flag16 = shaderImpl.u3dMaterial != null;
																if (flag16)
																{
																	skinnedMeshRenderer2.material = shaderImpl.u3dMaterial;
																}
																else
																{
																	shaderImpl.apply(skinnedMeshRenderer2.material);
																}
															}
															bool flag17 = skinnedMeshRenderer.rootBone != null;
															if (flag17)
															{
																bool flag18 = this._getBone(skinnedMeshRenderer.rootBone.name) != null;
																if (flag18)
																{
																	skinnedMeshRenderer2.rootBone = this._getBone(skinnedMeshRenderer.rootBone.name).transform;
																}
															}
															List<Transform> list2 = new List<Transform>();
															Transform[] componentsInChildren = base.u3dObject.GetComponentsInChildren<Transform>();
															for (int j = 0; j < skinnedMeshRenderer.bones.Length; j++)
															{
																for (int k = 0; k < componentsInChildren.Length; k++)
																{
																	bool flag19 = componentsInChildren[k].name == skinnedMeshRenderer.bones[j].name;
																	if (flag19)
																	{
																		list2.Add(componentsInChildren[k]);
																		break;
																	}
																}
															}
															skinnedMeshRenderer2.bones = list2.ToArray();
														}
													}
												}
											}
										}
										this.m_tmpList.Add(current);
									}
								}
							}
						}
					}
					bool flag20 = this.m_tmpList.Count > 0;
					if (flag20)
					{
						foreach (IAsset current5 in this.m_tmpList)
						{
							this.m_assetLoading.Remove(current5);
						}
					}
				}
			}
		}

		public void attachSkin(IAssetSkAniMesh asset, string obj, IShader mtrl = null)
		{
			bool flag = asset == null;
			if (!flag)
			{
				string key = asset.path + "$" + obj;
				this.m_skinMap[key] = null;
				bool flag2 = mtrl != null;
				if (flag2)
				{
					this.m_skinMtrlMap[key] = (mtrl as ShaderImpl);
				}
				bool flag3 = !asset.isReady;
				if (flag3)
				{
					asset.load();
				}
				this.m_assetLoading[asset] = asset;
			}
		}

		public void detachSkin(IAssetSkAniMesh asset, string obj)
		{
			bool flag = asset == null;
			if (!flag)
			{
				string key = asset.path + "$" + obj;
				bool flag2 = !this.m_skinMap.ContainsKey(key);
				if (!flag2)
				{
					GameObject gameObject = this.m_skinMap[key];
					bool flag3 = gameObject;
					if (flag3)
					{
						UnityEngine.Object.Destroy(gameObject);
					}
					this.m_skinMap.Remove(key);
					this.m_skinMtrlMap.Remove(key);
				}
			}
		}

		public void onAnimPlayEnd()
		{
		}

		protected GameObject _getBone(string sBoneName)
		{
			bool flag = this.m_animatorObj == null;
			GameObject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Transform[] componentsInChildren = this.m_animatorObj.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					GameObject gameObject = componentsInChildren[i].gameObject;
					bool flag2 = gameObject.name == sBoneName;
					if (flag2)
					{
						result = gameObject;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public void addEventListener(string name, float time, Action<string, float> finFun)
		{
			bool flag = this.m_EventFunc.ContainsKey(name);
			if (flag)
			{
				this.m_EventFunc[name][time] = finFun;
			}
			else
			{
				Dictionary<float, Action<string, float>> dictionary = new Dictionary<float, Action<string, float>>();
				dictionary[time] = finFun;
				this.m_EventFunc[name] = dictionary;
			}
		}

		public void removeEventListener(string name, float time)
		{
			bool flag = this.m_EventFunc.ContainsKey(name);
			if (flag)
			{
				bool flag2 = this.m_EventFunc[name].ContainsKey(time);
				if (flag2)
				{
					this.m_EventFunc[name].Remove(time);
				}
			}
		}

		private void _playerAnimator(string name)
		{
			this.m_isPlay = true;
			this.m_fpsTime = 0f;
			this.m_overdFun.Clear();
			this.m_overFunTime.Clear();
			this.m_skainName = name;
			bool flag = name == "";
			if (flag)
			{
				this.m_animator.Play();
			}
			else
			{
				this.m_animator.Play(name);
			}
		}

		private void _stopAnimator()
		{
			this.m_isPlay = false;
			this.m_skainName = "";
			this.m_animator.Stop();
		}

		public override void onProcess(float tmSlice)
		{
			bool flag = this.m_strCurPlayAnim != null;
			if (flag)
			{
				bool flag2 = this.m_animator != null && this.m_animMap[this.m_strCurPlayAnim].isReady;
				if (flag2)
				{
					this.play_AnimClip(this.m_strCurPlayAnim, this.m_nCurPlayLoop);
					this.m_strCurPlayAnim = null;
				}
			}
			else
			{
				bool flag3 = this.m_bPlayingActAction != null;
				if (flag3)
				{
					bool flag4 = this.m_animator[this.m_bPlayingActAction].normalizedTime >= (float)this.m_PlayingActLoop;
					if (flag4)
					{
						this.m_bPlayingActAction = null;
						bool flag5 = this.m_ActActOver_CB == null || !this.m_ActActOver_CB();
						if (flag5)
						{
							this.play_ActState();
						}
					}
				}
			}
			base.onProcess(tmSlice);
			bool isPlay = this.m_isPlay;
			if (isPlay)
			{
				this.m_fpsTime += tmSlice;
				this.m_fpsTime = float.Parse(this.m_fpsTime.ToString("f3"));
				bool flag6 = this.m_EventFunc.ContainsKey(this.m_skainName);
				if (flag6)
				{
					foreach (float current in this.m_EventFunc[this.m_skainName].Keys)
					{
						bool flag7 = this.m_overdFun.ContainsKey(this.m_skainName) && this.m_overdFun[this.m_skainName].ContainsKey(current);
						if (!flag7)
						{
							bool flag8 = this.m_fpsTime >= current;
							if (flag8)
							{
								this.m_EventFunc[this.m_skainName][current](this.m_skainName, current);
								this.m_overFunTime[current] = current;
								this.m_overdFun[this.m_skainName] = this.m_overFunTime;
							}
						}
					}
				}
			}
		}

		protected void _syncPlayState(bool forceReplay = false)
		{
			this.layer = this.m_layer;
			bool flag = this.m_animatorObj != null;
			if (flag)
			{
				foreach (string current in this.m_mtrlMap.Keys)
				{
					this.setMaterial(current, this.m_mtrlMap[current]);
				}
				foreach (string current2 in this.m_activeMap.Keys)
				{
					Transform transform = this.m_animatorObj.transform.Find(current2);
					bool flag2 = transform != null;
					if (flag2)
					{
						transform.gameObject.SetActive(this.m_activeMap[current2]);
					}
				}
			}
			bool flag3 = this.m_animator != null;
			if (flag3)
			{
				bool flag4 = this.m_curAnimName != "" && this.m_playing && (!this.m_animator.IsPlaying(this.m_curAnimName) | forceReplay);
				if (flag4)
				{
					bool isReady = this.m_animMap[this.m_curAnimName].isReady;
					if (isReady)
					{
						AssetSkAnimationImpl assetSkAnimationImpl = this.m_animMap[this.m_curAnimName] as AssetSkAnimationImpl;
						bool flag5 = this.m_animator.GetClip(this.m_curAnimName) == null;
						if (flag5)
						{
							this.m_animator.AddClip(assetSkAnimationImpl.anim, this.m_curAnimName);
						}
						this.m_animator.GetClip(this.m_curAnimName).wrapMode = this.m_wrapMode;
						this.m_animator[this.m_curAnimName].speed = this.m_curSpeed;
						this._playerAnimator(this.m_curAnimName);
					}
				}
			}
		}
	}
}
