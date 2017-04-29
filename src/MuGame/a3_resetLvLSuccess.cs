using GameFramework;
using System;
using System.Collections;
using UnityEngine;

namespace MuGame
{
	internal class a3_resetLvLSuccess : FloatUi
	{
		public override void init()
		{
			base.StartCoroutine(this.timeGo());
		}

		private IEnumerator timeGo()
		{
			yield return new WaitForSeconds(2f);
			this.gameObject.SetActive(false);
			bool flag = ModelBase<PlayerModel>.getInstance().up_lvl > 0u && ModelBase<PlayerModel>.getInstance().pt_att > 0;
			if (flag)
			{
				UiEventCenter.getInstance().onAddPoint();
			}
			FunctionOpenMgr.instance.onLvUp((int)ModelBase<PlayerModel>.getInstance().up_lvl, (int)ModelBase<PlayerModel>.getInstance().lvl, true);
			yield break;
		}
	}
}
