using System;
using UnityEngine;

public class Check_Bone : MonoBehaviour
{
	public GameObject mesh_a;

	public GameObject mesh_b;

	private void Start()
	{
		Transform[] bones = this.mesh_a.GetComponent<SkinnedMeshRenderer>().bones;
		Transform[] bones2 = this.mesh_b.GetComponent<SkinnedMeshRenderer>().bones;
		Debug.Log(string.Concat(new object[]
		{
			"a 骨骼数 ",
			bones.Length,
			"      b 骨骼数 ",
			bones2.Length
		}));
		for (int i = 0; i < bones.Length; i++)
		{
			bool flag = false;
			for (int j = 0; j < bones2.Length; j++)
			{
				if (bones[i].name == bones2[j].name)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Debug.Log("a name = " + bones[i].name);
			}
		}
		for (int k = 0; k < bones2.Length; k++)
		{
			bool flag2 = false;
			for (int l = 0; l < bones.Length; l++)
			{
				if (bones2[k].name == bones[l].name)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				Debug.Log("b name = " + bones2[k].name);
			}
		}
	}

	private void Update()
	{
	}
}
