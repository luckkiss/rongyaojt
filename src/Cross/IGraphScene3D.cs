using System;

namespace Cross
{
	public interface IGraphScene3D : IGraphScene
	{
		int layer
		{
			get;
			set;
		}

		bool visible
		{
			get;
			set;
		}

		IEnvironment env
		{
			get;
			set;
		}

		string name
		{
			get;
			set;
		}

		IContainer3D createContainer3D();

		IMesh createMesh();

		ICamera createCamera();

		IBillboard createBillboard();

		IEffectParticles createEffectParticles();

		IEffectKnifeLight createEffectKnifeLight();

		ISkAniMesh createSkAniMesh();

		ILightDir createLightDir();

		ILightPoint createLightPoint();

		ILightSpot createLightSpot();

		void deleteObject3D(string id);

		void deleteObject3D(IGraphObject3D object3D);

		void addContainer3D(IGraphObject3D cont);
	}
}
