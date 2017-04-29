using System;
using System.Collections.Generic;

namespace Cross
{
	public class PHWorld3D : IPHWorld
	{
		public delegate IPHEntity EntityCreator(string id);

		protected PhysicsManager m_physicsMrg;

		protected Dictionary<string, IPHEntity> m_entitys = new Dictionary<string, IPHEntity>();

		protected Dictionary<string, IPHMap> m_maps = new Dictionary<string, IPHMap>();

		protected PHMap3D m_phmap;

		protected string m_id;

		protected uint m_entityCounter = 0u;

		public string id
		{
			get
			{
				return this.m_id;
			}
		}

		public PhysicsManager phyciscsManager
		{
			get
			{
				return this.m_physicsMrg;
			}
		}

		public PHWorld3D(string id, PhysicsManager phyMgr)
		{
			this.m_phmap = null;
			this.m_id = "";
		}

		public IPHMap createMap(string id)
		{
			this.m_phmap = new PHMap3D(id, this);
			bool flag = !this.m_maps.ContainsKey(id);
			IPHMap result;
			if (flag)
			{
				this.m_maps.Add(id, this.m_phmap);
				result = this.m_phmap;
			}
			else
			{
				result = this.m_maps[id];
			}
			return result;
		}

		public IPHMap getMap(string id)
		{
			IPHMap result = null;
			this.m_maps.TryGetValue(id, out result);
			return result;
		}

		public void deleteMap(string id)
		{
			this.m_maps.Remove(id);
		}

		public IPHEntity createEntity(Define.PHEntityType type, string id)
		{
			bool flag = id == null;
			if (flag)
			{
				object arg_24_0 = "$";
				uint entityCounter = this.m_entityCounter;
				this.m_entityCounter = entityCounter + 1u;
				id = arg_24_0 + entityCounter;
			}
			IPHEntity result;
			if (type != Define.PHEntityType.HEIGHTMAP)
			{
				if (type != Define.PHEntityType.COLLIDER_MESH)
				{
					result = null;
					return result;
				}
				this.m_entitys[id] = new PHColliderMesh(id, this.m_physicsMrg);
			}
			else
			{
				this.m_entitys[id] = new PHHeightMap(id, this.m_physicsMrg);
			}
			result = this.m_entitys[id];
			return result;
		}

		public IPHEntity createEntity(Define.PHEntityType type)
		{
			object arg_22_0 = "$";
			uint entityCounter = this.m_entityCounter;
			this.m_entityCounter = entityCounter + 1u;
			string text = arg_22_0 + entityCounter;
			IPHEntity result;
			if (type != Define.PHEntityType.HEIGHTMAP)
			{
				if (type != Define.PHEntityType.COLLIDER_MESH)
				{
					result = null;
					return result;
				}
				this.m_entitys[text] = new PHColliderMesh(text, this.m_physicsMrg);
			}
			else
			{
				this.m_entitys[text] = new PHHeightMap(text, this.m_physicsMrg);
			}
			result = this.m_entitys[text];
			return result;
		}

		public IPHEntity getEntity(string id)
		{
			IPHEntity result = null;
			this.m_entitys.TryGetValue(id, out result);
			return result;
		}

		public void deleteEntity(string id)
		{
			this.m_entitys.Remove(id);
		}

		public void deleteEntity(IPHEntity ent)
		{
			bool flag = ent == null;
			if (!flag)
			{
				this.m_entitys.Remove(ent.id);
			}
		}
	}
}
