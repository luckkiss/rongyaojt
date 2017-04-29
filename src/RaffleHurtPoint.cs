using System;
using UnityEngine;

public class RaffleHurtPoint : MonoBehaviour
{
	public GameObject m_Raffle;

	public GameObject m_RafWhole;

	public GameObject m_RafBoom;

	public void OnTriggerEnter(Collider other)
	{
		HitData component = other.gameObject.GetComponent<HitData>();
		bool flag = component == null;
		if (!flag)
		{
			this.m_RafWhole.SetActive(false);
			this.m_RafBoom.SetActive(true);
			base.GetComponent<Collider>().enabled = false;
			other.enabled = false;
			component.HitAndStop(-1, false);
			UnityEngine.Object.Destroy(this.m_Raffle, 3f);
		}
	}
}
