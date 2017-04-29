using System;

namespace Cross
{
	public class GRAvatarPart3D : IGRAvatarPart
	{
		protected GRWorld3D m_world;

		protected IAssetSkAniMesh m_asset;

		protected string m_obj;

		protected GRShader m_mtrl;

		protected string m_part = "";

		protected string m_attachTo = null;

		protected string m_mountTo = null;

		protected GREntity3D m_entity = null;

		public GRShader material
		{
			get
			{
				return this.m_mtrl;
			}
		}

		public string partId
		{
			get
			{
				return this.m_part;
			}
		}

		public string attachTo
		{
			get
			{
				return this.m_attachTo;
			}
		}

		public string mountTo
		{
			get
			{
				return this.m_mountTo;
			}
		}

		public IGREntity entity
		{
			get
			{
				return this.m_entity;
			}
		}

		public string obj
		{
			get
			{
				return this.m_obj;
			}
		}

		public IAssetSkAniMesh asset
		{
			get
			{
				return this.m_asset;
			}
		}

		public GRAvatarPart3D(GRWorld3D world = null)
		{
			this.m_world = world;
		}

		public void load(Variant conf)
		{
			bool flag = conf == null;
			if (!flag)
			{
				bool flag2 = conf.ContainsKey("part");
				if (flag2)
				{
					this.m_part = conf["part"]._str;
				}
				bool flag3 = conf.ContainsKey("attachto");
				if (flag3)
				{
					this.m_attachTo = conf["attachto"]._str;
					string text = null;
					bool flag4 = conf.ContainsKey("customMtrl");
					if (flag4)
					{
						text = conf["customMtrl"]._str;
					}
					else
					{
						bool flag5 = conf.ContainsKey("mtrl");
						if (flag5)
						{
							text = conf["mtrl"]._str;
						}
					}
					bool flag6 = text != null;
					if (flag6)
					{
						Variant materialConf = GraphManager.singleton.getMaterialConf(text);
						bool flag7 = materialConf != null;
						if (flag7)
						{
							this.m_mtrl = new GRShader();
							this.m_mtrl.load(materialConf);
						}
						else
						{
							DebugTrace.add(Define.DebugTrace.DTT_ERR, "Material [" + conf["mtrl"]._str + "] is missed!");
						}
					}
					bool flag8 = conf.ContainsKey("cid");
					if (flag8)
					{
						Variant characterConf = GraphManager.singleton.getCharacterConf(conf["cid"]._str);
						bool flag9 = characterConf == null;
						if (flag9)
						{
							DebugTrace.add(Define.DebugTrace.DTT_ERR, "applyAvatar attachto cid[" + conf["cid"]._str + "] config not exist");
						}
						else
						{
							this.m_entity = (this.m_world.createEntity(Define.GREntityType.CHARACTER) as GREntity3D);
							bool flag10 = this.m_mtrl != null;
							if (flag10)
							{
								this.m_entity.load(characterConf, this.m_mtrl.graphMaterial, null);
							}
							else
							{
								this.m_entity.load(characterConf, null, null);
							}
						}
					}
					else
					{
						bool flag11 = conf.ContainsKey("eid");
						if (flag11)
						{
							Variant entityConf = GraphManager.singleton.getEntityConf(conf["eid"]._str);
							bool flag12 = entityConf == null;
							if (flag12)
							{
								DebugTrace.add(Define.DebugTrace.DTT_ERR, "applyAvatar attachto entid[" + conf["eid"]._str + "] config not exist");
							}
							else
							{
								this.m_entity = (this.m_world.createEntity(Define.GREntityType.STATIC_MESH) as GREntity3D);
								bool flag13 = this.m_mtrl != null;
								if (flag13)
								{
									this.m_entity.load(entityConf, this.m_mtrl.graphMaterial, null);
								}
								else
								{
									this.m_entity.load(entityConf, null, null);
								}
							}
						}
					}
				}
				else
				{
					bool flag14 = conf.ContainsKey("mountto");
					if (flag14)
					{
						this.m_mountTo = conf["mountto"];
						Variant characterConf2 = GraphManager.singleton.getCharacterConf(conf["cid"]);
						bool flag15 = characterConf2 == null;
						if (flag15)
						{
							DebugTrace.add(Define.DebugTrace.DTT_ERR, "applyAvatar mountto cid[" + conf["cid"]._str + "] config not exist");
						}
						else
						{
							this.m_entity = (this.m_world.createEntity(Define.GREntityType.CHARACTER) as GREntity3D);
							this.m_entity.load(characterConf2, null, null);
						}
					}
					else
					{
						bool flag16 = conf.ContainsKey("file");
						if (flag16)
						{
							this.m_asset = os.asset.getAsset<IAssetSkAniMesh>(conf["file"]._str);
						}
						else
						{
							this.m_asset = null;
						}
						bool flag17 = conf.ContainsKey("obj");
						if (flag17)
						{
							this.m_obj = conf["obj"]._str;
						}
						else
						{
							this.m_obj = conf["part"]._str;
						}
						string text2 = null;
						bool flag18 = conf.ContainsKey("customMtrl");
						if (flag18)
						{
							text2 = conf["customMtrl"]._str;
						}
						else
						{
							bool flag19 = conf.ContainsKey("mtrl");
							if (flag19)
							{
								text2 = conf["mtrl"]._str;
							}
						}
						bool flag20 = text2 != null;
						if (flag20)
						{
							Variant materialConf2 = GraphManager.singleton.getMaterialConf(text2);
							bool flag21 = materialConf2 != null;
							if (flag21)
							{
								this.m_mtrl = new GRShader();
								this.m_mtrl.load(materialConf2);
							}
							else
							{
								DebugTrace.add(Define.DebugTrace.DTT_ERR, "Material [" + conf["mtrl"]._str + "] is missed!");
							}
						}
					}
				}
			}
		}

		public void dispose()
		{
			bool flag = this.m_entity != null;
			if (flag)
			{
				this.m_world.deleteEntity(this.m_entity);
			}
			this.m_asset = null;
			this.m_part = null;
			this.m_mountTo = null;
			this.m_attachTo = null;
			this.m_obj = null;
			this.m_mtrl = null;
			this.m_world = null;
		}
	}
}
