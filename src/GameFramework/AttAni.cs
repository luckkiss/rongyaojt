using Cross;
using System;
using System.Reflection;

namespace GameFramework
{
	internal class AttAni
	{
		private Variant _obj = null;

		private Variant _anis = new Variant();

		private bool _aniAttChanged = false;

		private Action<AttAni> _updateFun;

		private Action<AttAni> _finFun;

		private float _startTm = 0f;

		private float _totalTm = 0f;

		public Variant userdata = null;

		public Variant m_data
		{
			get
			{
				return this._obj;
			}
		}

		public AttAni(Variant obj)
		{
			bool flag = obj != null && obj._val is IUIBaseControl;
			if (flag)
			{
				this._obj = GameTools.createGroup(new object[]
				{
					"ctrl",
					obj._val
				});
			}
			else
			{
				this._obj = obj;
			}
		}

		public void Start(float tm)
		{
			this._startTm = tm;
			this.ResetObjAtt();
		}

		public void ResetObjAtt()
		{
			bool flag = this._anis.Count > 0;
			if (flag)
			{
				foreach (Variant current in this._anis.Values)
				{
					current["ccnt"] = 0;
					this.m_data[current["nm"]] = current["aniAtt"]["begin"];
				}
			}
		}

		public void AddAni(Func<Variant, double, double> tweenFun, string attnm, Variant aniAtt, int plycnt = 1, float delay = 0f)
		{
			bool flag = false;
			bool flag2 = this.m_data.ContainsKey("ctrl");
			if (flag2)
			{
				flag = true;
				Type type = this.m_data["ctrl"]._val.GetType();
				object[] properties = type.GetProperties();
				PropertyInfo property = type.GetProperty(attnm);
				bool flag3 = property != null && property.Name != attnm;
				if (flag3)
				{
					return;
				}
			}
			bool flag4 = !flag && !this.m_data.ContainsKey(attnm);
			if (!flag4)
			{
				this._anis[attnm] = GameTools.createGroup(new object[]
				{
					"nm",
					attnm,
					"tweenFun",
					tweenFun,
					"plycnt",
					plycnt,
					"ccnt",
					0,
					"delay",
					delay
				});
				this._anis[attnm]["aniAtt"] = aniAtt;
				this._aniAttChanged = true;
			}
		}

		public void AdjustAniAtt(string attnm, Variant att)
		{
			Variant variant = this._anis[attnm];
			bool flag = variant != null;
			if (flag)
			{
				foreach (string current in att.Keys)
				{
					bool flag2 = variant.ContainsKey(current);
					if (flag2)
					{
						this._anis[current] = att[current];
					}
				}
				this._aniAttChanged = true;
			}
		}

		public void RemoveAniAtt(string attnm)
		{
			bool flag = this._anis.ContainsKey(attnm);
			if (flag)
			{
				this._anis.RemoveKey(attnm);
				this._aniAttChanged = true;
			}
		}

		private void recalcTotalTm()
		{
			this._totalTm = 0f;
			foreach (Variant current in this._anis.Values)
			{
				bool flag = current["plycnt"]._int < 0;
				if (flag)
				{
					this._totalTm = -1f;
					break;
				}
				float num = (float)current["plycnt"]._int * current["aniAtt"]["duration"]._float + current["delay"]._float;
				bool flag2 = this._totalTm < num;
				if (flag2)
				{
					this._totalTm = num;
				}
			}
		}

		public void Release()
		{
			this._anis = new Variant();
			this._obj = null;
			this._finFun = null;
			this._updateFun = null;
		}

		public void SetFinFun(Action<AttAni> fin)
		{
			this._finFun = fin;
		}

		public void SetUpdateFun(Action<AttAni> fun)
		{
			this._updateFun = fun;
		}

		private void onAniFinish()
		{
			bool flag = this._finFun != null;
			if (flag)
			{
				this._finFun(this);
			}
		}

		public void Update(float currTm)
		{
			bool flag = this._startTm <= 0f;
			if (!flag)
			{
				bool aniAttChanged = this._aniAttChanged;
				if (aniAttChanged)
				{
					this.recalcTotalTm();
					this._aniAttChanged = false;
				}
				float num = currTm - this._startTm;
				bool flag2 = this._anis.Count > 0;
				if (flag2)
				{
					foreach (Variant current in this._anis.Values)
					{
						bool flag3 = current["delay"]._float > 0f && current["delay"]._float > num;
						if (!flag3)
						{
							float num2 = num - current["delay"]._float;
							float @float = current["aniAtt"]["duration"]._float;
							bool flag4 = current["plycnt"]._int > 0;
							if (flag4)
							{
								bool flag5 = current["plycnt"]._int <= current["ccnt"]._int;
								if (flag5)
								{
									continue;
								}
								bool flag6 = num2 >= @float;
								if (flag6)
								{
									current["ccnt"] = (int)(num2 / @float);
									bool flag7 = current["plycnt"]._int <= current["ccnt"]._int;
									if (flag7)
									{
										num2 = @float;
									}
									else
									{
										num2 -= (float)current["ccnt"]._int * @float;
									}
								}
							}
							else
							{
								bool flag8 = num2 > @float;
								if (flag8)
								{
									num2 -= (float)((int)(num2 / @float)) * @float;
								}
							}
							this.setValue(current["nm"], (current["tweenFun"]._val as Func<Variant, double, double>)(current["aniAtt"], (double)num2));
						}
					}
				}
				bool flag9 = this._updateFun != null;
				if (flag9)
				{
					this._updateFun(this);
				}
				bool flag10 = this._totalTm > 0f && num >= this._totalTm;
				if (flag10)
				{
					this._startTm = 0f;
					this.onAniFinish();
				}
			}
		}

		private void setValue(string attnm, double v)
		{
			bool flag = this.m_data.ContainsKey("ctrl");
			if (flag)
			{
				bool flag2 = attnm == "x";
				if (flag2)
				{
					(this.m_data["ctrl"]._val as IUIBaseControl).x = (float)v;
				}
				bool flag3 = attnm == "y";
				if (flag3)
				{
					(this.m_data["ctrl"]._val as IUIBaseControl).y = (float)v;
				}
				bool flag4 = attnm == "alpha";
				if (flag4)
				{
					(this.m_data["ctrl"]._val as IUIBaseControl).alpha = (float)v;
				}
				bool flag5 = attnm == "scale";
				if (flag5)
				{
				}
			}
			else
			{
				this.m_data[attnm] = v;
			}
		}
	}
}
