using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class NavmeshUtils
	{
		public static List<int> listARE = new List<int>
		{
			0,
			NavmeshUtils.AgentLayerNameToValue("ARE1"),
			NavmeshUtils.AgentLayerNameToValue("ARE2"),
			NavmeshUtils.AgentLayerNameToValue("ARE3"),
			NavmeshUtils.AgentLayerNameToValue("ARE4"),
			NavmeshUtils.AgentLayerNameToValue("ARE5")
		};

		public static int allARE = NavmeshUtils.listARE[1] + NavmeshUtils.listARE[2] + NavmeshUtils.listARE[3] + NavmeshUtils.listARE[4] + NavmeshUtils.listARE[5];

		public static int AgentLayerNameToValue(string name)
		{
			int navMeshLayerFromName = NavMesh.GetNavMeshLayerFromName(name);
			return 1 << navMeshLayerFromName;
		}

		public static int AgentLayerNameToIndex(string name)
		{
			return NavMesh.GetNavMeshLayerFromName(name);
		}

		public static int GetAgentLayer(NavMeshAgent agent)
		{
			NavMeshHit navMeshHit;
			bool flag = NavMesh.SamplePosition(agent.transform.position, out navMeshHit, 1f, -1);
			return navMeshHit.mask;
		}

		public static Vector3 SampleNavMeshPosition(Vector3 logicPosition, out bool reachable)
		{
			NavMeshHit navMeshHit;
			reachable = NavMesh.SamplePosition(logicPosition, out navMeshHit, 1f, -1);
			return reachable ? navMeshHit.position : logicPosition;
		}

		public static bool IsNavMeshLayerOpen(NavMeshAgent agent, string layerName)
		{
			int navMeshLayerFromName = NavMesh.GetNavMeshLayerFromName(layerName);
			bool flag = navMeshLayerFromName == -1;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				int num = agent.walkableMask & 1 << navMeshLayerFromName;
				result = (num > 0);
			}
			return result;
		}
	}
}
