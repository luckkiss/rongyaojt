using System;
using UnityEngine;

public class HiddenItem : MonoBehaviour
{
	public bool useAni = false;

	public float hideSec = 5f;

	public void hide()
	{
		bool flag = this.useAni;
		if (flag)
		{
			Animator component = base.gameObject.GetComponent<Animator>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.SetTrigger("hide");
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			bool flag3 = this.hideSec == 0f;
			if (flag3)
			{
				this.dispose();
				return;
			}
		}
		bool flag4 = this.hideSec > 0f;
		if (flag4)
		{
			base.Invoke("dispose", this.hideSec);
		}
	}

	private void dispose()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
