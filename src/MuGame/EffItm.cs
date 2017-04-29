using System;
using UnityEngine;

namespace MuGame
{
	public class EffItm
	{
		public BaseRole to;

		public BaseRole frm;

		public GameObject eff;

		public float sec;

		public void update(float s)
		{
			bool flag = this.to == null || this.frm == null || this.to.disposed || this.frm.disposed || this.to.isDead || this.frm.isDead;
			if (flag)
			{
				this.sec = -1f;
			}
			else
			{
				this.sec -= s;
				Vector3 position = this.to.m_curModel.position;
				Vector3 position2 = this.frm.m_curModel.transform.position;
				Quaternion localRotation = Quaternion.LookRotation(position - position2);
				this.eff.transform.localRotation = localRotation;
				Vector3 vector = position2;
				vector.y = this.frm.headOffset_half.y + vector.y;
				this.eff.transform.position = vector;
				this.eff.transform.localScale = new Vector3(1f, 1f, Vector3.Distance(position, position2));
			}
		}

		public void dispose()
		{
			UnityEngine.Object.Destroy(this.eff);
		}
	}
}
