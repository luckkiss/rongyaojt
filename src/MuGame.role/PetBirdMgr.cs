using System;
using UnityEngine;

namespace MuGame.role
{
	public class PetBirdMgr
	{
		private GameObject _birdPrefab = null;

		private GameObject _pathPrefab = null;

		private ProfessionRole _owner = null;

		private static PetBirdMgr _inst = null;

		public static PetBirdMgr Inst
		{
			get
			{
				bool flag = PetBirdMgr._inst != null;
				PetBirdMgr inst;
				if (flag)
				{
					inst = PetBirdMgr._inst;
				}
				else
				{
					PetBirdMgr._inst = new PetBirdMgr();
					PetBirdMgr._inst._init();
					inst = PetBirdMgr._inst;
				}
				return inst;
			}
		}

		private void OnPetStageChange()
		{
			Transform transform = SelfRole._inst.m_curModel.FindChild("birdstop");
			for (int i = 0; i < transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(transform.GetChild(i));
			}
			A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
			SXML node = instance.PetXML.GetNode("pet.stage", "stage==" + instance.Stage.ToString());
			string @string = node.getString("avatar");
			GameObject gameObject = Resources.Load<GameObject>("profession/" + @string);
			GameObject x = Resources.Load<GameObject>("profession/birdpath");
			bool flag = this._birdPrefab == null || x == null;
			if (!flag)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(this._birdPrefab, transform.position, Quaternion.identity) as GameObject;
				GameObject gameObject3 = UnityEngine.Object.Instantiate(this._pathPrefab, transform.position, Quaternion.identity) as GameObject;
				bool flag2 = gameObject2 == null || gameObject3 == null;
				if (!flag2)
				{
					gameObject3.transform.parent = transform;
					PetBird petBird = gameObject2.AddComponent<PetBird>();
					petBird.Path = gameObject3;
				}
			}
		}

		private void _init()
		{
		}
	}
}
