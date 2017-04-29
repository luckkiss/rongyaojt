using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Cross
{
	public class GRCharacter3D : GREntity3D, IGRCharacter, IGREntity
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly GRCharacter3D.<>c <>9 = new GRCharacter3D.<>c();

			public static Action<IAsset, float> <>9__19_1;

			internal void <load>b__19_1(IAsset a, float f)
			{
			}
		}

		protected ISkAniMesh m_skmesh;

		protected Dictionary<string, GRAvatarPart3D> m_bparts = new Dictionary<string, GRAvatarPart3D>();

		protected Dictionary<string, GREntity3D> m_battachs = new Dictionary<string, GREntity3D>();

		protected Dictionary<string, GREntity3D> m_weapon = new Dictionary<string, GREntity3D>();

		protected GRCharacter3D m_bmount = null;

		protected string m_bmountId = null;

		protected Dictionary<string, GRAvatarPart3D> m_avartars = new Dictionary<string, GRAvatarPart3D>();

		protected Dictionary<string, GREntity3D> m_attaches = new Dictionary<string, GREntity3D>();

		protected Dictionary<string, GRShader> m_mtrls = new Dictionary<string, GRShader>();

		protected GRCharacter3D m_mount = null;

		protected string m_mountPart = null;

		protected Dictionary<string, bool> m_aniLoops = new Dictionary<string, bool>();

		protected string m_curAniName = null;

		protected Dictionary<string, GREffectParticles3D> m_effectParticles = new Dictionary<string, GREffectParticles3D>();

		protected Dictionary<string, float> m_effectY = new Dictionary<string, float>();

		protected float m_chaHeight;

		public Func<bool> m_ActCharActOver_CB = null;

		public float chaHeight
		{
			get
			{
				return this.m_chaHeight;
			}
		}

		public GameObject gameObject
		{
			get
			{
				return this.m_skmesh.u3dObject;
			}
		}

		public string curAnimName
		{
			get
			{
				return this.m_skmesh.curAnimName;
			}
		}

		public ISkAniMesh skmesh
		{
			get
			{
				return this.m_skmesh;
			}
		}

		public bool shadow
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public override int layer
		{
			get
			{
				return this.m_skmesh.layer;
			}
			set
			{
				this.m_skmesh.layer = value;
			}
		}

		public GRCharacter3D(string id, GRWorld3D world) : base(id, world)
		{
			this.m_skmesh = this.m_world.scene3d.createSkAniMesh();
			this.m_rootObj.addChild(this.m_skmesh);
			this.m_skmesh.helper["$graphObj"] = this;
			this.m_rootObj.helper["$graphObj"] = this;
			this.m_skmesh.setActActionOverCB(new Func<bool>(this.actPlayOver));
		}

		private bool actPlayOver()
		{
			bool flag = this.m_ActCharActOver_CB != null;
			return flag && this.m_ActCharActOver_CB();
		}

		public override void load(Variant conf, IShader mtrl = null, Action onFin = null)
		{
			bool flag = conf == null;
			if (!flag)
			{
				List<IAsset> refAst = new List<IAsset>();
				this.m_skmesh.asset = os.asset.getAsset<IAssetSkAniMesh>(conf["file"]._str);
				refAst.Add(this.m_skmesh.asset);
				bool flag2 = mtrl != null;
				if (flag2)
				{
					this.m_skmesh.setMaterial("", mtrl);
				}
				bool flag3 = conf.ContainsKey("boundBox");
				if (flag3)
				{
					Variant variant = conf["boundBox"][0];
					string[] array = variant["center"]._str.Split(new char[]
					{
						','
					});
					string[] array2 = variant["extent"]._str.Split(new char[]
					{
						','
					});
					BoundBox boxCollider = new BoundBox(new Vec3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2])), new Vec3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2])));
					this.m_skmesh.boxCollider = boxCollider;
					bool flag4 = variant.ContainsKey("rigiType");
					if (flag4)
					{
						GameObject u3dObject = this.m_skmesh.u3dObject;
						bool flag5 = variant["rigiType"] == 1;
						if (flag5)
						{
							Rigidbody rigidbody = u3dObject.GetComponent<Rigidbody>();
							bool flag6 = rigidbody == null;
							if (flag6)
							{
								rigidbody = u3dObject.AddComponent<Rigidbody>();
							}
							rigidbody.mass = 10f;
							rigidbody.drag = 10f;
							rigidbody.sleepVelocity = 0.7f;
							rigidbody.useGravity = false;
							rigidbody.constraints = (RigidbodyConstraints)116;
						}
					}
				}
				bool flag7 = conf.ContainsKey("bpart");
				if (flag7)
				{
					this.m_bparts = new Dictionary<string, GRAvatarPart3D>();
					for (int i = 0; i < conf["bpart"].Count; i++)
					{
						Variant variant2 = conf["bpart"][i];
						GRAvatarPart3D gRAvatarPart3D = this.m_world.createAvatar() as GRAvatarPart3D;
						gRAvatarPart3D.load(variant2);
						this.m_bparts[variant2["part"]._str] = gRAvatarPart3D;
						bool flag8 = gRAvatarPart3D.asset == null;
						if (flag8)
						{
							this.m_skmesh.activateSubMesh(gRAvatarPart3D.obj, true);
							bool flag9 = gRAvatarPart3D.material != null;
							if (flag9)
							{
								this.m_skmesh.setMaterial(gRAvatarPart3D.obj, gRAvatarPart3D.material.graphMaterial);
							}
						}
						else
						{
							this.m_skmesh.attachSkin(gRAvatarPart3D.asset, gRAvatarPart3D.obj, (gRAvatarPart3D.material != null) ? gRAvatarPart3D.material.graphMaterial : null);
						}
					}
				}
				bool flag10 = conf.ContainsKey("battach");
				if (flag10)
				{
					this.m_battachs = new Dictionary<string, GREntity3D>();
					int j = 0;
					while (j < conf["battach"].Count)
					{
						Variant variant3 = conf["battach"][j];
						GREntity3D gREntity3D = null;
						bool flag11 = variant3.ContainsKey("cid");
						if (flag11)
						{
							Variant characterConf = GraphManager.singleton.getCharacterConf(variant3["cid"]._str);
							bool flag12 = characterConf == null;
							if (!flag12)
							{
								gREntity3D = (base.world.createEntity(Define.GREntityType.CHARACTER) as GRCharacter3D);
								gREntity3D.load(characterConf, null, null);
								goto IL_454;
							}
							DebugTrace.add(Define.DebugTrace.DTT_ERR, "applyAvatar attachto cid[" + variant3["cid"]._str + "] config not exist");
						}
						else
						{
							bool flag13 = variant3.ContainsKey("eid");
							if (!flag13)
							{
								goto IL_454;
							}
							Variant entityConf = GraphManager.singleton.getEntityConf(variant3["eid"]._str);
							bool flag14 = entityConf == null;
							if (!flag14)
							{
								gREntity3D = (base.world.createEntity(Define.GREntityType.STATIC_MESH) as GREntity3D);
								gREntity3D.load(entityConf, null, null);
								goto IL_454;
							}
							DebugTrace.add(Define.DebugTrace.DTT_ERR, "applyAvatar attachto entid[" + variant3["eid"] + "] config not exist");
						}
						IL_4C4:
						j++;
						continue;
						IL_454:
						bool flag15 = gREntity3D != null;
						if (flag15)
						{
							string str = variant3["attachto"]._str;
							this.m_battachs[str] = gREntity3D;
							bool flag16 = variant3["part"]._str.IndexOf("weapon") == 0;
							if (flag16)
							{
								this.m_weapon[str] = gREntity3D;
							}
							this.attachEntity(str, gREntity3D);
						}
						goto IL_4C4;
					}
				}
				bool flag17 = conf.ContainsKey("bmount");
				if (flag17)
				{
					Variant variant4 = conf["bmount"][0];
					Variant characterConf2 = GraphManager.singleton.getCharacterConf(variant4["cid"]._str);
					bool flag18 = characterConf2 != null;
					if (flag18)
					{
						this.m_bmount = (base.world.createEntity(Define.GREntityType.CHARACTER) as GRCharacter3D);
						this.m_bmount.load(characterConf2, null, null);
						this.m_bmountId = variant4["mountto"]._str;
						this.mountEntity(this.m_bmountId, this.m_bmount);
					}
					else
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, "applyAvatar mountto cid[" + variant4["cid"]._str + "] config not exist");
					}
				}
				bool flag19 = conf.ContainsKey("ani");
				if (flag19)
				{
					foreach (Variant current in conf["ani"].Values)
					{
						string text = null;
						bool flag20 = current.ContainsKey("name");
						if (flag20)
						{
							text = current["name"]._str;
						}
						bool flag21 = !current.ContainsKey("file");
						if (flag21)
						{
							DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new string[]
							{
								"file is lost in ani conf[ chaid: ",
								conf["id"]._str,
								" , aniName: ",
								text,
								" ]"
							}));
						}
						bool flag22 = text != null;
						if (flag22)
						{
							this.m_skmesh.pushAnim(text, current["file"]._str);
						}
						bool flag23 = text != null;
						if (flag23)
						{
							bool flag24 = current.ContainsKey("loop");
							if (flag24)
							{
								this.m_aniLoops[text] = (current["loop"]._str == "true");
							}
							else
							{
								this.m_aniLoops[text] = false;
							}
						}
					}
					this.m_skmesh.setDefaultFirstAnim("idle");
				}
				bool flag25 = conf.ContainsKey("attachEffect");
				if (flag25)
				{
					for (int k = 0; k < conf["attachEffect"].Count; k++)
					{
						Variant variant5 = conf["attachEffect"][k];
						IGREffectParticles iGREffectParticles = this.attachEffect(variant5["attachto"]._str, variant5["effID"]._str);
						iGREffectParticles.play();
					}
				}
				bool flag26 = conf.ContainsKey("scale");
				if (flag26)
				{
					float @float = conf["scale"]._float;
					this.m_skmesh.scale = new Vec3(@float, @float, @float);
				}
				bool flag27 = conf.ContainsKey("chaHeight");
				if (flag27)
				{
					this.m_chaHeight = conf["chaHeight"]._float;
				}
				this._onCharacterChange();
				int loadedAst = 0;
				Action<IAsset> <>9__0;
				Action<IAsset, string> <>9__2;
				for (int l = 0; l < refAst.Count; l++)
				{
					bool flag28 = refAst[l] != null;
					if (flag28)
					{
						IAsset arg_8A7_0 = refAst[l];
						Action<IAsset> arg_8A7_1;
						if ((arg_8A7_1 = <>9__0) == null)
						{
							arg_8A7_1 = (<>9__0 = delegate(IAsset a)
							{
								loadedAst++;
								bool flag29 = loadedAst == refAst.Count;
								if (flag29)
								{
									bool flag30 = onFin != null;
									if (flag30)
									{
										onFin();
									}
								}
							});
						}
						Action<IAsset, float> arg_8A7_2;
						if ((arg_8A7_2 = GRCharacter3D.<>c.<>9__19_1) == null)
						{
							arg_8A7_2 = (GRCharacter3D.<>c.<>9__19_1 = new Action<IAsset, float>(GRCharacter3D.<>c.<>9.<load>b__19_1));
						}
						Action<IAsset, string> arg_8A7_3;
						if ((arg_8A7_3 = <>9__2) == null)
						{
							arg_8A7_3 = (<>9__2 = delegate(IAsset a, string s)
							{
								loadedAst++;
								bool flag29 = loadedAst == refAst.Count;
								if (flag29)
								{
									bool flag30 = onFin != null;
									if (flag30)
									{
										onFin();
									}
								}
							});
						}
						arg_8A7_0.addCallbacks(arg_8A7_1, arg_8A7_2, arg_8A7_3);
					}
				}
			}
		}

		public Vec3 characterPos(Vec3 v)
		{
			return this.m_rootObj.characterPos(v);
		}

		public override void dispose()
		{
			foreach (GRAvatarPart3D current in this.m_avartars.Values)
			{
				this.m_skmesh.detachSkin(current.asset, current.obj);
				current.dispose();
			}
			this.m_avartars.Clear();
			foreach (string current2 in this.m_attaches.Keys)
			{
				GREntity3D gREntity3D = this.m_attaches[current2];
				this.m_skmesh.detachObject(current2, gREntity3D.graphObject as IGraphObject3D);
				gREntity3D.dispose();
			}
			this.m_attaches.Clear();
			bool flag = this.m_mount != null;
			if (flag)
			{
				this.m_mount.dettachEntity(base.id);
				this.m_mount.dispose();
				this.m_mount = null;
				this.m_mountPart = null;
			}
			base.dispose();
			this.m_loaded = false;
		}

		public void attachEntity(string attachID, IGREntity ent)
		{
			bool flag = ent == null;
			if (!flag)
			{
				this.m_attaches[attachID] = (ent as GREntity3D);
				this.m_skmesh.attachObject(attachID, ent.graphObject as IGraphObject3D);
				this._onCharacterChange();
			}
		}

		public IGREntity dettachEntity(string attachID)
		{
			bool flag = this.m_attaches.ContainsKey(attachID);
			IGREntity result;
			if (flag)
			{
				GREntity3D gREntity3D = this.m_attaches[attachID];
				this.m_skmesh.detachObject(attachID, gREntity3D.graphObject as IGraphObject3D);
				this.m_world.scene3d.addContainer3D(gREntity3D.graphObject as IGraphObject3D);
				this.m_attaches.Remove(attachID);
				this._onCharacterChange();
				result = gREntity3D;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public IGRCharacter mountEntity(string mountPart, IGRCharacter ent)
		{
			bool flag = ent == this.m_mount;
			IGRCharacter result;
			if (flag)
			{
				result = null;
			}
			else
			{
				GRCharacter3D mount = this.m_mount;
				bool flag2 = mount != null;
				if (flag2)
				{
					mount.m_skmesh.detachObject(mountPart, this.m_rootObj);
				}
				(ent as GRCharacter3D).m_skmesh.attachObject(this.m_mountPart, this.m_rootObj);
				this.m_mount = (ent as GRCharacter3D);
				this.m_mountPart = mountPart;
				this._onCharacterChange();
				result = mount;
			}
			return result;
		}

		public IGRCharacter unmountEntity()
		{
			GRCharacter3D mount = this.m_mount;
			bool flag = mount != null;
			if (flag)
			{
				mount.m_skmesh.detachObject(this.m_mountPart, this.m_rootObj);
			}
			this.m_mount = null;
			this.m_mountPart = null;
			return mount;
		}

		public virtual void applyAvatar(Variant conf, string mtrlId = null)
		{
			bool flag = conf == null;
			if (!flag)
			{
				bool flag2 = mtrlId != null;
				if (flag2)
				{
					conf["customMtrl"] = mtrlId;
				}
				GRAvatarPart3D gRAvatarPart3D = this.m_world.createAvatar() as GRAvatarPart3D;
				gRAvatarPart3D.load(conf);
				bool flag3 = conf.ContainsKey("attachto");
				if (flag3)
				{
					bool flag4 = gRAvatarPart3D.partId.IndexOf("weapon") == 0;
					if (flag4)
					{
						string str = conf["attachto"]._str;
						this.m_weapon[str] = (gRAvatarPart3D.entity as GREntity3D);
					}
				}
				this.changeAvatar(gRAvatarPart3D);
			}
		}

		public void changeAvatar(IGRAvatarPart ava)
		{
			bool flag = ava == null;
			if (!flag)
			{
				GRAvatarPart3D gRAvatarPart3D = ava as GRAvatarPart3D;
				bool flag2 = this.m_avartars.ContainsKey(gRAvatarPart3D.partId);
				if (flag2)
				{
					GRAvatarPart3D gRAvatarPart3D2 = this.m_avartars[gRAvatarPart3D.partId];
					this.removeAvatar(gRAvatarPart3D2.partId);
				}
				bool flag3 = gRAvatarPart3D.attachTo != null;
				if (flag3)
				{
					this.attachEntity(gRAvatarPart3D.attachTo, gRAvatarPart3D.entity);
				}
				else
				{
					bool flag4 = gRAvatarPart3D.mountTo != null;
					if (flag4)
					{
						this.mountEntity(gRAvatarPart3D.mountTo, gRAvatarPart3D.entity as IGRCharacter);
						bool flag5 = this.m_curAniName != null;
						if (flag5)
						{
							(gRAvatarPart3D.entity as GRCharacter3D).playAnimation(this.m_curAniName, 0);
						}
					}
					else
					{
						bool flag6 = gRAvatarPart3D.asset != null;
						if (flag6)
						{
							bool flag7 = this.m_bparts.ContainsKey(gRAvatarPart3D.partId);
							if (flag7)
							{
								GRAvatarPart3D gRAvatarPart3D3 = this.m_bparts[gRAvatarPart3D.partId];
								bool flag8 = gRAvatarPart3D3.asset == null;
								if (flag8)
								{
									this.m_skmesh.activateSubMesh(gRAvatarPart3D3.obj, false);
								}
								else
								{
									this.m_skmesh.detachSkin(gRAvatarPart3D3.asset, gRAvatarPart3D3.obj);
								}
							}
							this.m_skmesh.attachSkin(gRAvatarPart3D.asset, gRAvatarPart3D.obj, (gRAvatarPart3D.material != null) ? gRAvatarPart3D.material.graphMaterial : null);
						}
					}
				}
				this.m_avartars[gRAvatarPart3D.partId] = gRAvatarPart3D;
				this._onCharacterChange();
			}
		}

		public void removeAvatar(string partId)
		{
			bool flag = this.m_avartars.ContainsKey(partId);
			if (flag)
			{
				GRAvatarPart3D gRAvatarPart3D = this.m_avartars[partId];
				bool flag2 = gRAvatarPart3D.attachTo != null;
				if (flag2)
				{
					this.dettachEntity(gRAvatarPart3D.attachTo);
				}
				else
				{
					bool flag3 = gRAvatarPart3D.mountTo != null;
					if (flag3)
					{
						this.unmountEntity();
					}
					else
					{
						bool flag4 = this.m_bparts.ContainsKey(partId);
						if (flag4)
						{
							GRAvatarPart3D gRAvatarPart3D2 = this.m_bparts[partId];
							bool flag5 = gRAvatarPart3D2.asset == null;
							if (flag5)
							{
								this.m_skmesh.activateSubMesh(gRAvatarPart3D2.obj, true);
							}
							else
							{
								this.m_skmesh.attachSkin(gRAvatarPart3D2.asset, gRAvatarPart3D2.obj, (gRAvatarPart3D2.material != null) ? gRAvatarPart3D2.material.graphMaterial : null);
							}
						}
						this.m_skmesh.detachSkin(gRAvatarPart3D.asset, gRAvatarPart3D.obj);
					}
				}
				this.m_avartars.Remove(partId);
				gRAvatarPart3D.dispose();
				this._onCharacterChange();
			}
		}

		public IGRAvatarPart getAvatar(string partID)
		{
			GRAvatarPart3D result = null;
			this.m_avartars.TryGetValue(partID, out result);
			return result;
		}

		public IGREntity getWeapon(string attachtoID)
		{
			GREntity3D result = null;
			this.m_weapon.TryGetValue(attachtoID, out result);
			return result;
		}

		public IGREffectParticles attachEffect(string attachID, string effId)
		{
			GREffectParticles3D gREffectParticles3D = base.world.createEntity(Define.GREntityType.EFFECT_PARTICLE) as GREffectParticles3D;
			gREffectParticles3D.load(GraphManager.singleton.getEffectConf(effId), null, null);
			this.m_skmesh.attachObject(attachID, gREffectParticles3D.graphObject as IGraphObject3D);
			return gREffectParticles3D;
		}

		public void dettachEffect(IGREffectParticles eff)
		{
		}

		protected void _onCharacterChange()
		{
		}

		public void setMtlInt(int nameid, int ndata)
		{
			this.m_skmesh.setMtlInt(nameid, ndata);
		}

		public void setMtlFloat(int nameid, float fdata)
		{
			this.m_skmesh.setMtlFloat(nameid, fdata);
		}

		public void setMtlColor(int nameid, Color color)
		{
			this.m_skmesh.setMtlColor(nameid, color);
		}

		public bool changeMtl(Material ml)
		{
			return this.m_skmesh.changeMtl(ml);
		}

		public void anim_Speed(float s)
		{
			this.m_skmesh.action_Speed(s);
		}

		public bool IsRunningAnim()
		{
			return this.m_skmesh.IsRunningAnim();
		}

		public void playAnimation(string aniName, int loop)
		{
			bool flag = loop == 0;
			if (flag)
			{
				bool flag2 = aniName == "idle";
				if (flag2)
				{
					this.m_skmesh.stateact_Change(QS_ACT_STATE.IDLE);
				}
				else
				{
					bool flag3 = aniName == "run";
					if (flag3)
					{
						this.m_skmesh.stateact_Change(QS_ACT_STATE.RUN);
					}
					else
					{
						bool flag4 = aniName == "mount_idle";
						if (flag4)
						{
							this.m_skmesh.stateact_Change(QS_ACT_STATE.MOUNT_IDLE);
						}
						else
						{
							bool flag5 = aniName == "mount_run";
							if (flag5)
							{
								this.m_skmesh.stateact_Change(QS_ACT_STATE.MOUNT_RUN);
							}
							else
							{
								this.m_skmesh.action_Play(aniName, 1);
							}
						}
					}
				}
			}
			else
			{
				this.m_skmesh.action_Play(aniName, loop);
			}
		}

		public void pauseAnimation()
		{
			this.m_skmesh.pause();
		}

		public void resumeAnimation()
		{
			this.m_skmesh.resume();
		}

		public void stopAnimation()
		{
			this.m_skmesh.stop();
		}

		public void playEffect(string effectName, bool isLoop)
		{
			bool flag = effectName == null;
			if (!flag)
			{
				GREffectParticles3D gREffectParticles3D = null;
				this.m_effectParticles.TryGetValue(effectName, out gREffectParticles3D);
				bool flag2 = gREffectParticles3D == null;
				if (!flag2)
				{
					gREffectParticles3D.play();
				}
			}
		}

		public void stopEffect(string effectName)
		{
			bool flag = effectName == null;
			if (!flag)
			{
				GREffectParticles3D gREffectParticles3D = null;
				this.m_effectParticles.TryGetValue(effectName, out gREffectParticles3D);
				bool flag2 = gREffectParticles3D == null;
				if (!flag2)
				{
					gREffectParticles3D.stop();
				}
			}
		}

		public void stopAllEffects()
		{
			foreach (GREffectParticles3D current in this.m_effectParticles.Values)
			{
				current.stop();
			}
		}

		public override void addEventListener(Define.EventType eventType, Action<Event> cbFunc)
		{
			bool flag = cbFunc == null;
			if (!flag)
			{
				bool flag2 = this.m_eventFunc.ContainsKey(eventType);
				if (flag2)
				{
					Dictionary<Define.EventType, Action<Event>> eventFunc = this.m_eventFunc;
					eventFunc[eventType] = (Action<Event>)Delegate.Combine(eventFunc[eventType], cbFunc);
				}
				else
				{
					this.m_eventFunc[eventType] = cbFunc;
				}
			}
		}

		public override void removeEventListener(Define.EventType eventType, Action<Event> cbFunc)
		{
			bool flag = cbFunc == null;
			if (!flag)
			{
				bool flag2 = this.m_eventFunc.ContainsKey(eventType);
				if (flag2)
				{
					Dictionary<Define.EventType, Action<Event>> eventFunc = this.m_eventFunc;
					eventFunc[eventType] = (Action<Event>)Delegate.Remove(eventFunc[eventType], cbFunc);
				}
			}
		}

		public override void clearAllEventListeners()
		{
			this.m_eventFunc.Clear();
		}

		protected void onAnimPlayEnd(Event e)
		{
		}

		public bool hasAnim(string aniName)
		{
			return false;
		}

		public void showAvatar(bool b)
		{
			this.m_rootObj.visible = b;
		}

		public void addskmEventListener(Define.EventType eventType, Action<Event> cbFunc)
		{
			this.m_skmesh.addEventListener(eventType, cbFunc);
		}

		public void addEventListener(string name, float time, Action<string, float> finFun)
		{
			this.m_skmesh.addEventListener(name, time, finFun);
		}

		public void removeEventListener(string name, float time)
		{
			this.m_skmesh.removeEventListener(name, time);
		}
	}
}
