using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class PlayerNameItem : Skin
	{
		public INameObj _avatar;

		private Text ptxt;

		private Text mtxt;

		public Text utext;

		private Text htxt;

		private Text sumtext;

		private Text mastertext;

		public RectTransform hp;

		private Image bg;

		private Image vipIcon;

		private RectTransform rectVipIcon;

		private GameObject serial;

		private bool hpshowed = false;

		private float wVip;

		private Text curName;

		private Image titleIcon;

		private Text baotu;

		private Text mapCount;

		private GameObject othermapCount;

		private GameObject hitback_state;

		private GameObject monsterHunter;

		public Text mhPlayerName;

		public Text cartxt;

		public Text legionName;

		private TickItem hitbacktime;

		private float times = 0f;

		private int i;

		private bool isself = false;

		public PlayerNameItem(Transform trans) : base(trans)
		{
			this.initUI();
		}

		public void clear()
		{
			this._avatar = null;
		}

		private void initUI()
		{
			this.vipIcon = base.getComponentByPath<Image>("uname/vipicon");
			this.bg = base.getComponentByPath<Image>("bg");
			this.ptxt = base.getComponentByPath<Text>("pname");
			this.mtxt = base.getComponentByPath<Text>("mname");
			this.utext = base.getComponentByPath<Text>("uname");
			this.htxt = base.getComponentByPath<Text>("hname");
			this.sumtext = base.getComponentByPath<Text>("sumname");
			this.mastertext = base.getComponentByPath<Text>("sumname/mastername");
			this.mhPlayerName = base.getComponentByPath<Text>("mhname/name");
			this.titleIcon = base.getComponentByPath<Image>("uname/title");
			this.mapCount = base.getComponentByPath<Text>("mapcount");
			this.othermapCount = base.getGameObjectByPath("othermapcount");
			this.baotu = base.getComponentByPath<Text>("baotu_guai");
			this.monsterHunter = base.getGameObjectByPath("mh_mark");
			this.hitback_state = base.getGameObjectByPath("hitback_state");
			this.serial = base.getGameObjectByPath("uname/serial");
			this.cartxt = base.getComponentByPath<Text>("carHP");
			this.legionName = base.getComponentByPath<Text>("carHP/legionname");
			this.hp = base.getComponentByPath<RectTransform>("hp");
			this.hp.localScale = new Vector3(0.1f, 1f, 1f);
			this.bg.gameObject.SetActive(false);
			this.hp.gameObject.SetActive(false);
			this.titleIcon.gameObject.SetActive(false);
			this.vipIcon.gameObject.SetActive(false);
			this.hitback_state.gameObject.SetActive(false);
			this.baotu.gameObject.SetActive(false);
			this.mapCount.gameObject.SetActive(false);
			this.othermapCount.SetActive(false);
			this.serial.SetActive(false);
			this.cartxt.gameObject.SetActive(false);
			this.rectVipIcon = this.vipIcon.GetComponent<RectTransform>();
			this.wVip = this.rectVipIcon.sizeDelta.x;
		}

		public void setName(string sum_name, string master_name)
		{
			this.sumtext.text = sum_name;
			this.mastertext.text = master_name;
		}

		public void setDartName(string dart_name)
		{
			this.legionName.text = dart_name;
		}

		public void setHP(string hp_per, bool show = false)
		{
			this.cartxt.gameObject.SetActive(true);
			if (show)
			{
				this.cartxt.text = hp_per + "%" + ContMgr.getCont("gameroom_wudi", null);
			}
			else
			{
				this.cartxt.text = hp_per + "%";
			}
		}

		public void refreicon()
		{
			this.baotu.gameObject.SetActive(false);
		}

		public void seticon_forDaobao(int num)
		{
			bool flag = num > 0;
			if (flag)
			{
				this.baotu.gameObject.SetActive(true);
				this.baotu.text = num.ToString();
			}
			else
			{
				this.baotu.gameObject.SetActive(false);
			}
		}

		public void show_mhMark(INameObj role, bool show)
		{
			this.mhPlayerName.text = string.Format("{0}\n{1}", (role as MonsterRole).roleName, ModelBase<PlayerModel>.getInstance().name);
			this.mhPlayerName.transform.parent.gameObject.SetActive(!show);
			this.monsterHunter.SetActive(!show);
		}

		public void refresh(INameObj avatar)
		{
			this._avatar = avatar;
			this.ptxt.text = (this.mtxt.text = (this.utext.text = (this.htxt.text = (this.sumtext.text = (this.mastertext.text = "")))));
			this.mapCount.gameObject.SetActive(false);
			this.othermapCount.SetActive(false);
			this.serial.SetActive(false);
			base.transform.SetAsFirstSibling();
			bool flag = avatar is ProfessionRole;
			if (flag)
			{
				base.transform.SetAsLastSibling();
				this.curName = this.utext;
			}
			else
			{
				bool flag2 = avatar is MonsterRole;
				if (flag2)
				{
					bool flag3 = avatar is MonsterPlayer;
					if (!flag3)
					{
						return;
					}
					this.curName = this.utext;
					this.refresNameColor(0);
				}
				else
				{
					bool flag4 = avatar is NpcRole;
					if (flag4)
					{
						this.curName = this.ptxt;
					}
					else
					{
						this.curName = null;
					}
				}
			}
			bool flag5 = this.curName;
			if (flag5)
			{
				this.curName.text = avatar.roleName;
			}
		}

		public void refresNameColor(int rednmstate)
		{
			bool flag = this.utext != null;
			if (flag)
			{
				switch (rednmstate)
				{
				case 0:
					this.utext.color = new Color(1f, 1f, 1f, 1f);
					break;
				case 1:
					this.utext.color = new Color(0.454901963f, 0.490196079f, 0.239215687f, 1f);
					break;
				case 2:
					this.utext.color = new Color(1f, 1f, 0f, 1f);
					break;
				case 3:
					this.utext.color = new Color(1f, 0f, 0f, 1f);
					break;
				}
			}
		}

		private void getColor()
		{
		}

		public void refresHitback(int time, bool ismyself = false)
		{
			bool flag = time <= 0;
			if (flag)
			{
				this.hitback_state.SetActive(false);
			}
			else
			{
				this.hitback_state.SetActive(true);
				bool flag2 = this.hitbacktime == null;
				if (flag2)
				{
					this.hitbacktime = new TickItem(new Action<float>(this.onUpdates));
					TickMgr.instance.addTick(this.hitbacktime);
				}
				this.i = time;
				if (ismyself)
				{
					this.isself = true;
				}
				else
				{
					this.isself = false;
				}
			}
		}

		private void onUpdates(float s)
		{
			this.times += s;
			bool flag = this.times >= 1f;
			if (flag)
			{
				this.i--;
				bool flag2 = this.isself;
				if (flag2)
				{
					ModelBase<PlayerModel>.getInstance().hitBack = (uint)this.i;
				}
				bool flag3 = this.i == 0;
				if (flag3)
				{
					this.hitback_state.SetActive(false);
					this.i = 0;
					TickMgr.instance.removeTick(this.hitbacktime);
					this.hitbacktime = null;
				}
				this.times = 0f;
			}
		}

		public void refreshVipLv(uint viplv)
		{
			bool flag = viplv == 0u;
			if (flag)
			{
				bool active = this.vipIcon.gameObject.active;
				if (active)
				{
					this.vipIcon.gameObject.SetActive(false);
				}
			}
			else
			{
				bool flag2 = this.curName == null;
				if (!flag2)
				{
					this.vipIcon.gameObject.SetActive(true);
					this.vipIcon.sprite = U3DAPI.U3DResLoad<Sprite>("icon/vip/" + viplv);
					this.vipIcon.transform.SetParent(this.curName.transform, false);
					Vector2 anchoredPosition = this.rectVipIcon.anchoredPosition;
					Vector2 sizeDelta = this.rectVipIcon.sizeDelta;
				}
			}
		}

		public void refreshTitle(int title_id)
		{
			bool flag = title_id == 0;
			if (flag)
			{
				this.titleIcon.gameObject.SetActive(false);
			}
			else
			{
				bool flag2 = this.curName == null;
				if (!flag2)
				{
					this.titleIcon.gameObject.SetActive(true);
					this.titleIcon.sprite = U3DAPI.U3DResLoad<Sprite>("icon/achievement/title_ui/t" + title_id);
					this.titleIcon.SetNativeSize();
				}
			}
		}

		public void refreshMapcount(int Count, bool ismine)
		{
			bool flag = Count <= 0;
			if (flag)
			{
				this.mapCount.gameObject.SetActive(false);
				this.othermapCount.SetActive(false);
			}
			else
			{
				bool flag2 = this.curName == null;
				if (!flag2)
				{
					if (ismine)
					{
						this.mapCount.gameObject.SetActive(true);
						this.mapCount.text = Count.ToString();
					}
					else
					{
						this.othermapCount.SetActive(true);
					}
				}
			}
		}

		public void refreshserial(int Count)
		{
			bool flag = this.curName == null;
			if (!flag)
			{
				bool flag2 = Count <= 3;
				if (flag2)
				{
					this.serial.SetActive(false);
				}
				else
				{
					bool flag3 = Count <= 10;
					if (flag3)
					{
						this.serial.transform.FindChild("count").GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
					}
					else
					{
						bool flag4 = Count <= 20;
						if (flag4)
						{
							this.serial.transform.FindChild("count").GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
						}
						else
						{
							this.serial.transform.FindChild("count").GetComponent<RectTransform>().localScale = new Vector2(1.2f, 1.2f);
						}
					}
					bool flag5 = Count >= 9999;
					if (flag5)
					{
						Count = 9999;
					}
					this.serial.SetActive(true);
					this.serial.transform.FindChild("count").GetComponent<Text>().text = Count.ToString();
				}
			}
		}

		public void refreshHp(int cur, int max, INameObj avatar = null)
		{
			bool flag = max <= 0;
			if (flag)
			{
				this.setHpVisible(false);
			}
			else
			{
				bool flag2 = cur >= max;
				if (flag2)
				{
					this.setHpVisible(false);
				}
				else
				{
					this.setHpVisible(true);
					this.hp.localScale = new Vector3((float)cur / (float)max, 1f, 1f);
				}
				bool flag3 = this.hp.localScale.x <= 0f;
				if (flag3)
				{
					this.bg.gameObject.SetActive(false);
					this.hp.gameObject.SetActive(false);
				}
				bool flag4 = avatar is MDC000;
				if (flag4)
				{
					this.setHpVisible(true);
					this.hp.localScale = new Vector3((float)cur / (float)max, 1f, 1f);
					bool flag5 = (int)((float)cur / (float)max * 100f) <= 20;
					if (flag5)
					{
						this.cartxt.text = "20%" + ContMgr.getCont("gameroom_wudi", null);
					}
					else
					{
						this.cartxt.text = ((int)((float)cur / (float)max * 100f)).ToString() + "%";
					}
				}
			}
		}

		public void update()
		{
			bool flag = this._avatar == null;
			if (!flag)
			{
				Vector3 headPos = this._avatar.getHeadPos();
				bool flag2 = headPos == Vector3.zero;
				if (flag2)
				{
					this.visiable = false;
				}
				else
				{
					this.visiable = true;
					base.pos = headPos;
				}
			}
		}

		public void setHpVisible(bool b)
		{
			bool flag = b == this.hpshowed;
			if (!flag)
			{
				this.hpshowed = b;
				if (b)
				{
					this.bg.gameObject.SetActive(true);
					this.hp.gameObject.SetActive(true);
				}
				else
				{
					this.bg.gameObject.SetActive(false);
					this.hp.gameObject.SetActive(false);
				}
			}
		}
	}
}
