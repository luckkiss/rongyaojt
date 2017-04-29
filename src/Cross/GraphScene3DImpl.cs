using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class GraphScene3DImpl : GraphSceneImpl, IGraphScene3D, IGraphScene
	{
		public GameObject m_u3dObj = new GameObject();

		protected int m_layer = 0;

		protected bool m_visible = false;

		protected string m_name = "";

		protected Dictionary<string, GraphObject3DImpl> m_graphObj3D = new Dictionary<string, GraphObject3DImpl>();

		protected long m_id = 0L;

		protected EnvironmentImpl m_env;

		public IEnvironment env
		{
			get
			{
				return this.m_env;
			}
			set
			{
				this.m_env = (value as EnvironmentImpl);
			}
		}

		public int layer
		{
			get
			{
				return this.m_layer;
			}
			set
			{
				this.m_layer = value;
				this.m_u3dObj.transform.position = new Vector3(0f, 0f, (float)(this.m_layer * 100));
			}
		}

		public string name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
				this.m_u3dObj.name = this.m_name;
			}
		}

		public bool visible
		{
			get
			{
				return this.m_visible;
			}
			set
			{
				this.m_visible = value;
				this.m_u3dObj.SetActive(this.m_visible);
			}
		}

		public GraphScene3DImpl()
		{
			this.m_u3dObj.name = "3DScene";
			this.m_env = new EnvironmentImpl();
		}

		public void addContainer3D(IGraphObject3D cont)
		{
			(cont as GraphObject3DImpl).u3dObject.transform.parent = this.m_u3dObj.transform;
		}

		public IBillboard createBillboard()
		{
			BillboardImpl billboardImpl = new BillboardImpl();
			billboardImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			billboardImpl.id = "$" + this.m_id;
			this.m_graphObj3D[billboardImpl.id] = billboardImpl;
			this.m_id += 1L;
			return billboardImpl;
		}

		public IContainer3D createContainer3D()
		{
			Container3DImpl container3DImpl = new Container3DImpl();
			container3DImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			container3DImpl.id = "$" + this.m_id;
			this.m_graphObj3D[container3DImpl.id] = container3DImpl;
			this.m_id += 1L;
			return container3DImpl;
		}

		public IMesh createMesh()
		{
			MeshImpl meshImpl = new MeshImpl();
			meshImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			meshImpl.id = "$" + this.m_id;
			this.m_graphObj3D[meshImpl.id] = meshImpl;
			this.m_id += 1L;
			return meshImpl;
		}

		public ICamera createCamera()
		{
			CameraImpl cameraImpl = new CameraImpl();
			cameraImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			cameraImpl.id = "$" + this.m_id;
			this.m_graphObj3D[cameraImpl.id] = cameraImpl;
			this.m_id += 1L;
			return cameraImpl;
		}

		public IEffectParticles createEffectParticles()
		{
			EffectParticlesImpl effectParticlesImpl = new EffectParticlesImpl();
			effectParticlesImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			effectParticlesImpl.id = "$" + this.m_id;
			this.m_graphObj3D[effectParticlesImpl.id] = effectParticlesImpl;
			this.m_id += 1L;
			return effectParticlesImpl;
		}

		public IEffectKnifeLight createEffectKnifeLight()
		{
			EffectKnifeLightImpl effectKnifeLightImpl = new EffectKnifeLightImpl();
			effectKnifeLightImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			effectKnifeLightImpl.id = "$" + this.m_id;
			this.m_graphObj3D[effectKnifeLightImpl.id] = effectKnifeLightImpl;
			this.m_id += 1L;
			return effectKnifeLightImpl;
		}

		public ISkAniMesh createSkAniMesh()
		{
			SkAniMeshImpl skAniMeshImpl = new SkAniMeshImpl();
			skAniMeshImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			skAniMeshImpl.id = "$" + this.m_id;
			this.m_graphObj3D[skAniMeshImpl.id] = skAniMeshImpl;
			this.m_id += 1L;
			return skAniMeshImpl;
		}

		public ILightDir createLightDir()
		{
			LightDirImpl lightDirImpl = new LightDirImpl();
			lightDirImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			lightDirImpl.id = "$" + this.m_id;
			this.m_graphObj3D[lightDirImpl.id] = lightDirImpl;
			this.m_id += 1L;
			return lightDirImpl;
		}

		public ILightPoint createLightPoint()
		{
			LightPointImpl lightPointImpl = new LightPointImpl();
			lightPointImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			lightPointImpl.id = "$" + this.m_id;
			this.m_graphObj3D[lightPointImpl.id] = lightPointImpl;
			this.m_id += 1L;
			return lightPointImpl;
		}

		public ILightSpot createLightSpot()
		{
			LightSpotImpl lightSpotImpl = new LightSpotImpl();
			lightSpotImpl.u3dObject.transform.parent = this.m_u3dObj.transform;
			while (this.m_graphObj3D.ContainsKey("$" + this.m_id))
			{
				this.m_id += 1L;
			}
			lightSpotImpl.id = "$" + this.m_id;
			this.m_graphObj3D[lightSpotImpl.id] = lightSpotImpl;
			this.m_id += 1L;
			return lightSpotImpl;
		}

		public void deleteObject3D(string id)
		{
			this.m_graphObj3D[id].dispose();
			this.m_graphObj3D.Remove(id);
		}

		public void deleteObject3D(IGraphObject3D obj)
		{
			this.deleteObject3D((obj as GraphObject3DImpl).id);
		}
	}
}
