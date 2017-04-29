using Cross;
using System;
using UnityEngine;

namespace MuGame
{
	internal class FightAniUserTempSC : FightAniTempSC
	{
		public Action<GameObject, int> onAttackBeginHanle;

		public Action<GameObject, int> onAttackPointHandle;

		public Action<float, int, float> onAttackShakeHandle;

		public static GameObject goUser;

		public uint iid;

		private void Start()
		{
			AttackPointMgr.init();
			this.onAttackShakeHandle = new Action<float, int, float>(AttackPointMgr.instacne.onAttackShake);
			bool flag = base.gameObject.name == "player(Clone)";
			if (flag)
			{
				FightAniUserTempSC.goUser = base.gameObject;
			}
			Object3DBehaviour component = FightAniUserTempSC.goUser.transform.parent.GetComponent<Object3DBehaviour>();
		}

		public Vector3 getUserPos()
		{
			return base.transform.position;
		}

		public new void onAttackBegin(int num)
		{
			bool flag = this.onAttackBeginHanle != null;
			if (flag)
			{
				this.onAttackBeginHanle(base.gameObject, num);
			}
		}

		public new void onAttackPoint(int skillid)
		{
			bool flag = this.onAttackPointHandle != null;
			if (flag)
			{
				this.onAttackPointHandle(base.gameObject, skillid);
			}
		}

		public new void onAttackShake_time_num_str(string pram)
		{
			bool flag = this.onAttackShakeHandle != null;
			if (flag)
			{
				string[] array = pram.Split(new char[]
				{
					','
				});
				this.onAttackShakeHandle(float.Parse(array[0]), int.Parse(array[1]), float.Parse(array[2]));
			}
		}

		public new void onAttack_sound(int id)
		{
			MediaClient.instance.PlaySoundUrl("media/skill/" + id, false, null);
		}
	}
}
