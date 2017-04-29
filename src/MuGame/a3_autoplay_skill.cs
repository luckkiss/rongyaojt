using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_autoplay_skill : Window
	{
		public static a3_autoplay_skill Instance;

		public static Action<int, int> OnSkillChoose;

		public static int SkillSeat = -1;

		private GridLayoutGroup grid;

		public override void init()
		{
			a3_autoplay_skill.Instance = this;
			base.getEventTrigerByPath("ig_bg_bg").onClick = new EventTriggerListener.VoidDelegate(this.onClose);
			this.grid = base.getComponentByPath<GridLayoutGroup>("scroll_view/contain");
		}

		public override void onShowed()
		{
			GameObject original = Resources.Load<GameObject>("prefab/a3_autoplay_skill_icon");
			int num = 0;
			foreach (skill_a3Data current in ModelBase<Skill_a3Model>.getInstance().skilldic.Values)
			{
				bool flag = current.carr != ModelBase<PlayerModel>.getInstance().profession || current.skill_id == skillbar.NORNAL_SKILL_ID || current.now_lv == 0;
				if (!flag)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					bool flag2 = gameObject == null;
					if (!flag2)
					{
						int skid = current.skill_id;
						gameObject.transform.parent = this.grid.transform;
						gameObject.name = skid.ToString();
						gameObject.transform.FindChild("bg/mask/icon").GetComponent<Image>().sprite = (Resources.Load("icon/skill/" + skid.ToString(), typeof(Sprite)) as Sprite);
						gameObject.transform.FindChild("bg").GetComponent<Button>().onClick.AddListener(delegate
						{
							this.onClose(null);
							a3_autoplay_skill.OnSkillChoose(skid, a3_autoplay_skill.SkillSeat);
						});
						num++;
					}
				}
			}
			bool flag3 = num == 0;
			if (flag3)
			{
				flytxt.instance.fly("没有可以使用的技能，请学习技能", 0, default(Color), null);
			}
		}

		public override void onClosed()
		{
			for (int i = 0; i < this.grid.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.grid.transform.GetChild(i).gameObject);
			}
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_AUTOPLAY_SKILL);
		}
	}
}
