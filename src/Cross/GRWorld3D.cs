using System;
using System.Collections.Generic;

namespace Cross
{
	public class GRWorld3D : IGRWorld
	{
		private string m_id;

		protected GRCamera3D m_cam;

		protected uint m_objCounter = 0u;

		protected GraphManager m_graphMgr;

		private Dictionary<string, GRMap3D> m_maps;

		private Dictionary<string, IGREntity> m_entities;

		protected Dictionary<uint, List<IGREntity>> m_processEntities = new Dictionary<uint, List<IGREntity>>();

		protected GRMap3D m_curMap;

		protected IGraphScene2D m_scene2D;

		protected IGraphScene3D m_scene3D;

		public IGRMap curMap
		{
			get
			{
				return this.m_curMap;
			}
			set
			{
				bool flag = this.m_curMap != null;
				if (flag)
				{
					this.m_curMap.visible = false;
				}
				this.m_curMap = (value as GRMap3D);
				this.m_curMap.visible = true;
			}
		}

		public GraphManager graphManager
		{
			get
			{
				return this.m_graphMgr;
			}
		}

		public GRCamera3D cam
		{
			get
			{
				return this.m_cam;
			}
		}

		public IGraphScene3D scene3d
		{
			get
			{
				return this.m_scene3D;
			}
		}

		public GRWorld3D(string id, GraphManager grMgr)
		{
			this.m_id = id;
		}

		public IGRMap createMap(string id)
		{
			bool flag = !this.m_maps.ContainsKey(id);
			if (flag)
			{
				this.m_maps[id] = new GRMap3D(id, this);
			}
			this.m_curMap = this.m_maps[id];
			return this.m_maps[id];
		}

		public IGRMap getMap(string id)
		{
			bool flag = this.m_maps.ContainsKey(id);
			IGRMap result;
			if (flag)
			{
				result = this.m_maps[id];
			}
			else
			{
				result = this.m_curMap;
			}
			return result;
		}

		public bool deleteMap(string id)
		{
			bool flag = this.m_maps.ContainsKey(id);
			bool result;
			if (flag)
			{
				this.m_maps[id].dispose();
				this.m_maps.Remove(id);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool deleteMap(IGRMap map)
		{
			bool result;
			foreach (string current in this.m_maps.Keys)
			{
				bool flag = map == this.m_maps[current];
				if (flag)
				{
					this.deleteMap(current);
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public IGRAvatarPart createAvatar()
		{
			return new GRAvatarPart3D(this);
		}

		public IGREntity createEntity(Define.GREntityType type, string id)
		{
			bool flag = id == null;
			if (flag)
			{
				object arg_24_0 = "$";
				uint objCounter = this.m_objCounter;
				this.m_objCounter = objCounter + 1u;
				id = arg_24_0 + objCounter;
			}
			IGREntity result;
			switch (type)
			{
			case Define.GREntityType.STATIC_MESH:
				this.m_entities[id] = new GRStaticMesh3D(id, this);
				break;
			case Define.GREntityType.CHARACTER:
				this.m_entities[id] = new GRCharacter3D(id, this);
				break;
			case Define.GREntityType.EFFECT_PARTICLE:
				this.m_entities[id] = new GREffectParticles3D(id, this);
				break;
			case Define.GREntityType.CAMERA:
				this.m_entities[id] = new GRCamera3D(id, this);
				break;
			case Define.GREntityType.LIGHTDIR:
				this.m_entities[id] = new GRLightDir3D(id, this);
				break;
			case Define.GREntityType.LIGHTPOINT:
				this.m_entities[id] = new GRLightPoint3D(id, this);
				break;
			case Define.GREntityType.BILLBOARD:
				this.m_entities[id] = new GRBillboard(id, this);
				break;
			case Define.GREntityType.EFFECT_KNIFELIGHT:
				this.m_entities[id] = new GREffectKnifeLight3D(id, this);
				break;
			default:
				result = null;
				return result;
			}
			result = this.m_entities[id];
			return result;
		}

		public IGREntity createEntity(Define.GREntityType type)
		{
			object arg_22_0 = "$";
			uint objCounter = this.m_objCounter;
			this.m_objCounter = objCounter + 1u;
			string id = arg_22_0 + objCounter;
			return this.createEntity(type, id);
		}

		public IGREntity getEntity(string id)
		{
			IGREntity result = null;
			this.m_entities.TryGetValue(id, out result);
			return result;
		}

		public void deleteAll()
		{
			foreach (string current in this.m_entities.Keys)
			{
				this.m_entities[current].dispose();
			}
			this.m_entities.Clear();
			bool flag = this.m_scene3D != null;
			if (flag)
			{
				os.graph.deleteScene3D(this.m_scene3D);
			}
		}

		public bool deleteEntity(string id)
		{
			bool flag = this.m_entities.ContainsKey(id);
			bool result;
			if (flag)
			{
				this.m_entities[id].dispose();
				this.m_entities.Remove(id);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool deleteEntity(IGREntity ent)
		{
			bool flag = this.m_entities.ContainsValue(ent);
			bool result;
			if (flag)
			{
				foreach (string current in this.m_entities.Keys)
				{
					bool flag2 = ent == this.m_entities[current];
					if (flag2)
					{
						this.deleteEntity(current);
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public bool deleteMap()
		{
			this.m_curMap.dispose();
			this.m_curMap = null;
			this.deleteMap("map");
			return true;
		}
	}
}
