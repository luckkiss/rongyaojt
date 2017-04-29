using System;
using UnityEngine;

public class DestructAfterTime : MonoBehaviour
{
	public float time = 1f;

	private void Start()
	{
	}

	private void Update()
	{
		if (this.time > 0f)
		{
			this.time -= Time.deltaTime;
		}
		else
		{
			ParticleEmitter[] componentsInChildren = base.transform.GetComponentsInChildren<ParticleEmitter>();
			ParticleEmitter[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				ParticleEmitter particleEmitter = array[i];
				if (particleEmitter.maxEnergy == 0f && particleEmitter.minEnergy == 0f)
				{
					UnityEngine.Object.Destroy(particleEmitter.gameObject);
				}
				else
				{
					particleEmitter.emit = false;
					ParticleAnimator component = particleEmitter.GetComponent<ParticleAnimator>();
					if (component)
					{
						component.autodestruct = true;
					}
				}
			}
			if (base.transform.parent)
			{
				foreach (Transform transform in base.transform)
				{
					transform.parent = base.transform.parent;
				}
			}
			else
			{
				base.transform.DetachChildren();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
