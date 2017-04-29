using System;
using System.Collections.Generic;

namespace Cross
{
	public class GRMap3D : IGRMap
	{
		protected Variant m_conf;

		protected string m_id;

		protected GRWorld3D m_world;

		protected IContainer3D m_mapContainer;

		protected ISkAniMesh m_skmesh;

		protected Dictionary<string, IGREntity> m_entities = new Dictionary<string, IGREntity>();

		protected Dictionary<string, IGraphObject3D> m_obj = new Dictionary<string, IGraphObject3D>();

		protected Dictionary<string, string> m_file = new Dictionary<string, string>();

		protected Dictionary<string, string> m_tag = new Dictionary<string, string>();

		protected Dictionary<string, string> m_objname = new Dictionary<string, string>();

		protected Dictionary<string, List<IGraphObject3D>> m_allListObj = new Dictionary<string, List<IGraphObject3D>>();

		protected List<string> m_objTag = new List<string>();

		protected string m_boxname = "$";

		protected int m_num = 0;

		protected float pos_x = 0f;

		protected float pos_y = 0f;

		protected float pos_z = 0f;

		protected float rot_x = 0f;

		protected float rot_y = 0f;

		protected float rot_z = 0f;

		protected float sca_x = 0f;

		protected float sca_y = 0f;

		protected float sca_z = 0f;

		protected float cor_r = 0f;

		protected float cor_g = 0f;

		protected float cor_b = 0f;

		protected float cor_a = 0f;

		protected float fogcor_r = 0f;

		protected float fogcor_g = 0f;

		protected float fogcor_b = 0f;

		protected float fogcor_a = 0f;

		private static Variant s_FogConf;

		private static Variant s_AmbConf;

		public bool visible
		{
			get
			{
				return this.m_mapContainer.visible;
			}
			set
			{
				this.m_mapContainer.visible = value;
			}
		}

		public Dictionary<string, IGREntity> entities
		{
			get
			{
				return this.m_entities;
			}
		}

		public Dictionary<string, IGraphObject3D> obj
		{
			get
			{
				return this.m_obj;
			}
		}

		public Dictionary<string, string> file
		{
			get
			{
				return this.m_file;
			}
		}

		public Dictionary<string, string> objid
		{
			get
			{
				return this.m_objname;
			}
		}

		public Dictionary<string, string> tag
		{
			get
			{
				return this.m_tag;
			}
		}

		public Dictionary<string, List<IGraphObject3D>> allListObj
		{
			get
			{
				return this.m_allListObj;
			}
		}

		public IGRWorld world
		{
			get
			{
				return this.m_world;
			}
		}

		public IContainer3D mapContainer
		{
			get
			{
				return this.m_mapContainer;
			}
		}

		public Variant config
		{
			get
			{
				return this.m_conf;
			}
		}

		public string id
		{
			get
			{
				return this.m_id;
			}
		}

		public GRMap3D(string id, GRWorld3D world)
		{
			this.m_conf = null;
			this.m_id = id;
			this.m_world = world;
			this.m_mapContainer = this.m_world.scene3d.createContainer3D();
			this.m_mapContainer.visible = false;
		}

		public bool loadConfig(Variant conf, Action<GRMap3D> onFin = null, Action<GRMap3D, float> onProgress = null, Action<GRMap3D, string> onFail = null)
		{
			bool flag = conf == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.m_conf = conf;
				this.load(onFin, onProgress, onFail);
				result = true;
			}
			return result;
		}

		public void SetMapFogAndAnimbent()
		{
			this.resetMapFog();
			this.resetMapAnimbent();
		}

		public void resetMapFog()
		{
			Variant variant = GRMap3D.s_FogConf;
			this.fogcor_r = variant["color"][0]["r"]._float;
			this.fogcor_g = variant["color"][0]["g"]._float;
			this.fogcor_b = variant["color"][0]["b"]._float;
			this.fogcor_a = variant["color"][0]["a"]._float;
			this.m_world.scene3d.env.displayfog = variant["display"]._bool;
			this.m_world.scene3d.env.fogden = variant["density"]._float;
			this.m_world.scene3d.env.fogcolor = new Vec4(this.fogcor_r, this.fogcor_g, this.fogcor_b, this.fogcor_a);
			this.m_world.scene3d.env.fogmode = variant["mode"]._str;
			this.m_world.scene3d.env.strdistance = variant["distBegin"]._float;
			this.m_world.scene3d.env.enddistance = variant["distEnd"]._float;
		}

		public void resetMapAnimbent()
		{
			Variant variant = GRMap3D.s_AmbConf;
			this.cor_r = variant["color"][0]["r"]._float;
			this.cor_g = variant["color"][0]["g"]._float;
			this.cor_b = variant["color"][0]["b"]._float;
			this.cor_a = variant["color"][0]["a"]._float;
			this.m_world.scene3d.env.ambcolor = new Vec4(this.cor_r, this.cor_g, this.cor_b, this.cor_a);
		}

		public void SetMapFog(bool displayfog, float fogden, Vec4 fogcolor, string fogmode, float strdistance, float enddistance)
		{
			this.m_world.scene3d.env.displayfog = displayfog;
			if (displayfog)
			{
				this.m_world.scene3d.env.fogden = fogden;
				this.m_world.scene3d.env.fogcolor = fogcolor;
				this.m_world.scene3d.env.fogmode = fogmode;
				this.m_world.scene3d.env.strdistance = strdistance;
				this.m_world.scene3d.env.enddistance = enddistance;
			}
		}

		public void SetAnimbent(Vec4 color)
		{
			this.m_world.scene3d.env.ambcolor = color;
		}

		protected bool load(Action<GRMap3D> onFin, Action<GRMap3D, float> onProgress, Action<GRMap3D, string> onFail)
		{
			this.m_mapContainer = this.m_world.scene3d.createContainer3D();
			bool flag = this.m_conf == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Action<IAsset> <>9__0;
				Action<IAsset, float> <>9__1;
				Action<IAsset, string> <>9__2;
				foreach (string current in this.m_conf.Keys)
				{
					string text = current;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2125857213u)
					{
						if (num <= 1077287143u)
						{
							if (num != 132142556u)
							{
								if (num == 1077287143u)
								{
									if (!(text == "SpotLight"))
									{
									}
								}
							}
							else if (text == "Mesh")
							{
								this.m_obj.Clear();
								this.m_objTag.Clear();
								for (int i = 0; i < this.m_conf["Mesh"].Count; i++)
								{
									Variant variant = this.m_conf["Mesh"][i];
									bool flag2 = variant.ContainsKey("asset");
									if (flag2)
									{
										Variant variant2 = variant["asset"][0];
										bool flag3 = variant2.ContainsKey("file");
										if (flag3)
										{
											IMesh mesh = this.m_world.scene3d.createMesh();
											IMesh arg_518_0 = mesh;
											IAssetManager arg_513_0 = os.asset;
											string arg_513_1 = variant2["file"]._str;
											Action<IAsset> arg_513_2;
											if ((arg_513_2 = <>9__0) == null)
											{
												arg_513_2 = (<>9__0 = delegate(IAsset ast)
												{
													bool flag16 = onFin != null;
													if (flag16)
													{
														onFin(this);
													}
												});
											}
											Action<IAsset, float> arg_513_3;
											if ((arg_513_3 = <>9__1) == null)
											{
												arg_513_3 = (<>9__1 = delegate(IAsset ast, float progress)
												{
													bool flag16 = onProgress != null;
													if (flag16)
													{
														onProgress(this, progress);
													}
												});
											}
											Action<IAsset, string> arg_513_4;
											if ((arg_513_4 = <>9__2) == null)
											{
												arg_513_4 = (<>9__2 = delegate(IAsset ast, string err)
												{
													bool flag16 = onFail != null;
													if (flag16)
													{
														onFail(this, err);
													}
												});
											}
											arg_518_0.asset = arg_513_0.getAsset<IAssetMesh>(arg_513_1, arg_513_2, arg_513_3, arg_513_4);
											this.pos_x = variant["pos"][0]["x"]._float;
											this.pos_y = variant["pos"][0]["y"]._float;
											this.pos_z = variant["pos"][0]["z"]._float;
											this.rot_x = variant["rot"][0]["x"]._float;
											this.rot_y = variant["rot"][0]["y"]._float;
											this.rot_z = variant["rot"][0]["z"]._float;
											this.sca_x = variant["scale"][0]["x"]._float;
											this.sca_y = variant["scale"][0]["y"]._float;
											this.sca_z = variant["scale"][0]["z"]._float;
											mesh.pos = new Vec3(this.pos_x, this.pos_y, this.pos_z);
											mesh.rot = new Vec3(this.rot_x, this.rot_y, this.rot_z);
											mesh.scale = new Vec3(this.sca_x, this.sca_y, this.sca_z);
											this.m_mapContainer.addChild(mesh);
										}
									}
								}
							}
						}
						else if (num != 2022607796u)
						{
							if (num == 2125857213u)
							{
								if (text == "Ani")
								{
									for (int j = 0; j < this.m_conf["Ani"].Count; j++)
									{
										Variant variant3 = this.m_conf["Ani"][j];
										bool flag4 = variant3.ContainsKey("asset");
										if (flag4)
										{
											Variant variant4 = variant3["asset"][0];
											bool flag5 = variant4.ContainsKey("file");
											if (flag5)
											{
												this.m_skmesh = this.m_world.scene3d.createSkAniMesh();
												this.m_skmesh.asset = os.asset.getAsset<IAssetSkAniMesh>(variant4["file"]._str);
												this.pos_x = variant3["pos"][0]["x"]._float;
												this.pos_y = variant3["pos"][0]["y"]._float;
												this.pos_z = variant3["pos"][0]["z"]._float;
												this.rot_x = variant3["rot"][0]["x"]._float;
												this.rot_y = variant3["rot"][0]["y"]._float;
												this.rot_z = variant3["rot"][0]["z"]._float;
												this.sca_x = variant3["scale"][0]["x"]._float;
												this.sca_y = variant3["scale"][0]["y"]._float;
												this.sca_z = variant3["scale"][0]["z"]._float;
												this.m_skmesh.pos = new Vec3(this.pos_x, this.pos_y, this.pos_z);
												this.m_skmesh.rot = new Vec3(this.rot_x, this.rot_y, this.rot_z);
												this.m_skmesh.scale = new Vec3(this.sca_x, this.sca_y, this.sca_z);
												this.m_mapContainer.addChild(this.m_skmesh);
												this.m_file[variant3["id"]._str] = variant4["file"]._str;
											}
										}
										bool flag6 = variant3.ContainsKey("Anims");
										if (flag6)
										{
											for (int k = 0; k < variant3["Anims"].Count; k++)
											{
												Variant variant5 = variant3["Anims"][k];
												bool flag7 = variant5.ContainsKey("asset");
												if (flag7)
												{
													for (int l = 0; l < variant5["asset"].Count; l++)
													{
														Variant variant6 = variant5["asset"][l];
														bool flag8 = variant6.ContainsKey("file");
														if (flag8)
														{
															this.m_skmesh.addAnim(variant6["id"]._str, os.asset.getAsset<IAssetSkAnimation>(variant6["file"]._str));
															this.m_skmesh.play(variant6["id"]._str, true, 1f);
															this.m_file[variant6["id"]._str] = variant6["file"]._str;
														}
													}
												}
											}
										}
									}
								}
							}
						}
						else if (text == "env")
						{
							bool flag9 = this.m_conf["env"][0].ContainsKey("fog");
							if (flag9)
							{
								GRMap3D.s_FogConf = this.m_conf["env"][0]["fog"][0];
							}
							bool flag10 = this.m_conf["env"][0].ContainsKey("amblight");
							if (flag10)
							{
								GRMap3D.s_AmbConf = this.m_conf["env"][0]["amblight"][0];
							}
							bool flag11 = this.m_conf["env"][0].ContainsKey("LightProbes");
							if (flag11)
							{
							}
							bool flag12 = this.m_conf["env"][0].ContainsKey("Skybox");
							if (flag12)
							{
								bool flag13 = this.m_conf["env"][0]["Skybox"][0].ContainsKey("asset");
								if (flag13)
								{
									Variant variant7 = this.m_conf["env"][0]["Skybox"][0]["asset"][0];
									this.m_world.scene3d.env.skybox = variant7["file"]._str;
								}
							}
						}
					}
					else if (num <= 3134466500u)
					{
						if (num != 2193872363u)
						{
							if (num == 3134466500u)
							{
								if (text == "Particles")
								{
									for (int m = 0; m < this.m_conf["Particles"].Count; m++)
									{
										Variant variant8 = this.m_conf["Particles"][m];
										bool flag14 = variant8.ContainsKey("asset");
										if (flag14)
										{
											Variant variant9 = variant8["asset"][0];
											bool flag15 = variant9.ContainsKey("file");
											if (flag15)
											{
												IEffectParticles effectParticles = this.m_world.scene3d.createEffectParticles();
												effectParticles.asset = os.asset.getAsset<IAssetParticles>(variant9["file"]._str);
												this.pos_x = variant8["pos"][0]["x"]._float;
												this.pos_y = variant8["pos"][0]["y"]._float;
												this.pos_z = variant8["pos"][0]["z"]._float;
												this.rot_x = variant8["rot"][0]["x"]._float;
												this.rot_y = variant8["rot"][0]["y"]._float;
												this.rot_z = variant8["rot"][0]["z"]._float;
												this.sca_x = variant8["scale"][0]["x"]._float;
												this.sca_y = variant8["scale"][0]["y"]._float;
												this.sca_z = variant8["scale"][0]["z"]._float;
												effectParticles.pos = new Vec3(this.pos_x, this.pos_y, this.pos_z);
												effectParticles.rot = new Vec3(this.rot_x, this.rot_y, this.rot_z);
												effectParticles.scale = new Vec3(this.sca_x, this.sca_y, this.sca_z);
												bool @bool = variant8["loop"]._bool;
												if (@bool)
												{
													effectParticles.loop = true;
												}
												effectParticles.play(1f);
												this.m_obj[variant8["id"]._str] = effectParticles;
												this.m_file[variant8["id"]._str] = variant9["file"]._str;
												this.m_mapContainer.addChild(effectParticles);
											}
										}
									}
								}
							}
						}
						else if (text == "PtLight")
						{
							for (int n = 0; n < this.m_conf["PtLight"].Count; n++)
							{
								Variant variant10 = this.m_conf["PtLight"][n];
								ILightPoint lightPoint = this.m_world.scene3d.createLightPoint();
								this.cor_r = variant10["color"][0]["r"]._float;
								this.cor_g = variant10["color"][0]["g"]._float;
								this.cor_b = variant10["color"][0]["b"]._float;
								this.cor_a = variant10["color"][0]["a"]._float;
								this.pos_x = variant10["pos"][0]["x"]._float;
								this.pos_y = variant10["pos"][0]["y"]._float;
								this.pos_z = variant10["pos"][0]["z"]._float;
								lightPoint.color = new Vec4(this.cor_r, this.cor_g, this.cor_b, this.cor_a);
								lightPoint.pos = new Vec3(this.pos_x, this.pos_y, this.pos_z);
								lightPoint.intensity = variant10["intensity"][0]["val"]._float;
								lightPoint.range = variant10["range"][0]["val"]._float;
								this.m_obj[variant10["id"]._str] = lightPoint;
								this.m_mapContainer.addChild(lightPoint);
							}
						}
					}
					else if (num != 3873038640u)
					{
						if (num == 3880549230u)
						{
							if (text == "Camera")
							{
								for (int num2 = 0; num2 < this.m_conf["Camera"].Count; num2++)
								{
									Variant variant11 = this.m_conf["Camera"][num2];
									IGREntity iGREntity = this.m_world.createEntity(Define.GREntityType.CAMERA) as GRCamera3D;
									this.pos_x = variant11["pos"][0]["x"]._float;
									this.pos_y = variant11["pos"][0]["y"]._float;
									this.pos_z = variant11["pos"][0]["z"]._float;
									this.rot_x = variant11["rot"][0]["x"]._float;
									this.rot_y = variant11["rot"][0]["y"]._float;
									this.rot_z = variant11["rot"][0]["z"]._float;
									this.sca_x = variant11["scale"][0]["x"]._float;
									this.sca_y = variant11["scale"][0]["y"]._float;
									this.sca_z = variant11["scale"][0]["z"]._float;
									iGREntity.pos = new Vec3(this.pos_x, this.pos_y, this.pos_z);
									iGREntity.rot = new Vec3(this.rot_x, this.rot_y, this.rot_z);
									iGREntity.scale = new Vec3(this.sca_x, this.sca_y, this.sca_z);
									this.m_obj[variant11["id"]._str] = (iGREntity as IGraphObject3D);
									this.m_mapContainer.addChild(iGREntity as IGraphObject3D);
								}
							}
						}
					}
					else if (text == "DctLight")
					{
						for (int num3 = 0; num3 < this.m_conf["DctLight"].Count; num3++)
						{
							Variant variant12 = this.m_conf["DctLight"][num3];
							ILightDir lightDir = this.m_world.scene3d.createLightDir();
							this.cor_r = variant12["color"][0]["r"]._float;
							this.cor_g = variant12["color"][0]["g"]._float;
							this.cor_b = variant12["color"][0]["b"]._float;
							this.cor_a = variant12["color"][0]["a"]._float;
							this.pos_x = variant12["pos"][0]["x"]._float;
							this.pos_y = variant12["pos"][0]["y"]._float;
							this.pos_z = variant12["pos"][0]["z"]._float;
							this.rot_x = variant12["rot"][0]["x"]._float;
							this.rot_y = variant12["rot"][0]["y"]._float;
							this.rot_z = variant12["rot"][0]["z"]._float;
							lightDir.pos = new Vec3(this.pos_x, this.pos_y, this.pos_z);
							lightDir.rot = new Vec3(this.rot_x, this.rot_y, this.rot_z);
							lightDir.color = new Vec4(this.cor_r, this.cor_g, this.cor_b, this.cor_a);
							lightDir.intensity = variant12["intensity"][0]["val"]._float;
							this.m_obj[variant12["id"]._str] = lightDir;
							this.m_mapContainer.addChild(lightDir);
						}
					}
				}
				result = true;
			}
			return result;
		}

		public void refreshLightMap()
		{
			bool flag = this.m_conf["env"][0].ContainsKey("Lightmap");
			if (flag)
			{
				bool flag2 = this.m_conf["env"][0]["Lightmap"][0].ContainsKey("asset");
				if (flag2)
				{
					List<string> list = new List<string>();
					for (int i = 0; i < this.m_conf["env"][0]["Lightmap"][0]["asset"].Count; i++)
					{
						Variant variant = this.m_conf["env"][0]["Lightmap"][0]["asset"][i];
						string str = variant["file"]._str;
						list.Add(str);
					}
					this.m_world.scene3d.env.lightmap = list;
				}
			}
		}

		public void dispose()
		{
			this.m_mapContainer.dispose();
		}
	}
}
