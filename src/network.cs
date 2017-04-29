using System;
using UnityEngine;

public class network : MonoBehaviour
{
	public static network _instance;

	private void start()
	{
		network._instance = this;
	}

	private void networkState()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			if (Application.internetReachability != NetworkReachability.ReachableViaCarrierDataNetwork)
			{
				if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
				{
				}
			}
		}
	}
}
