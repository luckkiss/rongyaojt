using System;
using UnityEngine;

namespace GameFramework
{
	public class ScenceUtils
	{
		public static float getGroundHight(float x, float y)
		{
			NavMeshHit navMeshHit;
			bool flag = NavMesh.SamplePosition(new Vector3(x, 0f, y), out navMeshHit, 999f, 1 << NavMesh.GetNavMeshLayerFromName("Default"));
			float result;
			if (flag)
			{
				Vector3 position = navMeshHit.position;
				result = position.y;
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		public static float getParticleSystemLength(Transform transform)
		{
			ParticleSystem[] componentsInChildren = transform.GetComponentsInChildren<ParticleSystem>();
			float num = 0f;
			ParticleSystem[] array = componentsInChildren;
			float result;
			for (int i = 0; i < array.Length; i++)
			{
				ParticleSystem particleSystem = array[i];
				bool enableEmission = particleSystem.enableEmission;
				if (enableEmission)
				{
					bool loop = particleSystem.loop;
					if (loop)
					{
						result = -1f;
						return result;
					}
					bool flag = particleSystem.emissionRate <= 0f;
					float num2;
					if (flag)
					{
						num2 = particleSystem.startDelay + particleSystem.startLifetime;
					}
					else
					{
						num2 = particleSystem.startDelay + Mathf.Max(particleSystem.duration, particleSystem.startLifetime);
					}
					bool flag2 = num2 > num;
					if (flag2)
					{
						num = num2;
					}
				}
			}
			result = num;
			return result;
		}
	}
}
