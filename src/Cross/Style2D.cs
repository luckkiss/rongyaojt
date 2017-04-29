using System;

namespace Cross
{
	public class Style2D
	{
		public enum HAligment
		{
			ALIGN_LEFT,
			ALIGN_MIDDLE,
			ALIGN_RIGHT,
			ALIGN_CENTER,
			NONE
		}

		public enum VAligment
		{
			ALIGN_MIDDLE,
			ALIGN_TOP,
			ALIGN_BOTTOM,
			ALIGN_CENTER,
			NONE
		}

		protected Style2D.HAligment m_hAligment = Style2D.HAligment.NONE;

		protected Style2D.VAligment m_vAligment = Style2D.VAligment.NONE;

		protected float m_margin;

		protected float m_leftMargin;

		protected float m_rightMargin;

		protected float m_bottonMargin;

		protected float m_topMargin;

		public float margin
		{
			get
			{
				return this.m_margin;
			}
			set
			{
				this.m_margin = value;
			}
		}

		public float leftMargin
		{
			get
			{
				return this.m_leftMargin;
			}
			set
			{
				this.m_leftMargin = value;
			}
		}

		public float rightMargin
		{
			get
			{
				return this.m_rightMargin;
			}
			set
			{
				this.m_rightMargin = value;
			}
		}

		public float bottonMargin
		{
			get
			{
				return this.m_bottonMargin;
			}
			set
			{
				this.m_bottonMargin = value;
			}
		}

		public float topMargin
		{
			get
			{
				return this.m_topMargin;
			}
			set
			{
				this.m_topMargin = value;
			}
		}

		public Style2D.VAligment vAligment
		{
			get
			{
				return this.m_vAligment;
			}
			set
			{
				this.m_vAligment = value;
			}
		}

		public Style2D.HAligment hAligment
		{
			get
			{
				return this.m_hAligment;
			}
			set
			{
				this.m_hAligment = value;
			}
		}

		public static Style2D.HAligment str2HAligment(string hAlign)
		{
			bool flag = hAlign.Equals("left");
			Style2D.HAligment result;
			if (flag)
			{
				result = Style2D.HAligment.ALIGN_LEFT;
			}
			else
			{
				bool flag2 = hAlign.Equals("middle");
				if (flag2)
				{
					result = Style2D.HAligment.ALIGN_MIDDLE;
				}
				else
				{
					bool flag3 = hAlign.Equals("right");
					if (flag3)
					{
						result = Style2D.HAligment.ALIGN_RIGHT;
					}
					else
					{
						bool flag4 = hAlign.Equals("center");
						if (flag4)
						{
							result = Style2D.HAligment.ALIGN_MIDDLE;
						}
						else
						{
							result = Style2D.HAligment.NONE;
						}
					}
				}
			}
			return result;
		}

		public static Style2D.VAligment str2VAligment(string vAlign)
		{
			bool flag = vAlign.Equals("middle");
			Style2D.VAligment result;
			if (flag)
			{
				result = Style2D.VAligment.ALIGN_MIDDLE;
			}
			else
			{
				bool flag2 = vAlign.Equals("top");
				if (flag2)
				{
					result = Style2D.VAligment.ALIGN_TOP;
				}
				else
				{
					bool flag3 = vAlign.Equals("bottom");
					if (flag3)
					{
						result = Style2D.VAligment.ALIGN_BOTTOM;
					}
					else
					{
						bool flag4 = vAlign.Equals("center");
						if (flag4)
						{
							result = Style2D.VAligment.ALIGN_MIDDLE;
						}
						else
						{
							result = Style2D.VAligment.NONE;
						}
					}
				}
			}
			return result;
		}

		public bool applyStyleConf(Variant v)
		{
			bool flag = v == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = v.ContainsKey("prop");
				if (flag2)
				{
					Variant variant = v["prop"];
					bool flag3 = variant.ContainsKey("vAlign");
					if (flag3)
					{
						this.m_vAligment = this._vAligment(variant["vAlign"]._str);
					}
					bool flag4 = variant.ContainsKey("hAlign");
					if (flag4)
					{
						this.m_hAligment = this._hAligment(variant["hAlign"]._str);
					}
					bool flag5 = variant.ContainsKey("leftMargin");
					if (flag5)
					{
						this.m_leftMargin = variant["leftMargin"]._float;
					}
					bool flag6 = variant.ContainsKey("rightMargin");
					if (flag6)
					{
						this.m_rightMargin = variant["rightMargin"]._float;
					}
					bool flag7 = variant.ContainsKey("topMargin");
					if (flag7)
					{
						this.m_topMargin = variant["topMargin"]._float;
					}
					bool flag8 = variant.ContainsKey("bottomMargin");
					if (flag8)
					{
						this.m_bottonMargin = variant["bottomMargin"]._float;
					}
					bool flag9 = variant.ContainsKey("margin");
					if (flag9)
					{
						this.m_margin = variant["margin"]._float;
					}
				}
				bool flag10 = v.ContainsKey("filter");
				if (flag10)
				{
				}
				result = true;
			}
			return result;
		}

		private Style2D.VAligment _vAligment(string str)
		{
			Style2D.VAligment result;
			if (!(str == "top"))
			{
				if (!(str == "bott+om"))
				{
					if (!(str == "center"))
					{
						result = Style2D.VAligment.NONE;
					}
					else
					{
						result = Style2D.VAligment.ALIGN_MIDDLE;
					}
				}
				else
				{
					result = Style2D.VAligment.ALIGN_BOTTOM;
				}
			}
			else
			{
				result = Style2D.VAligment.ALIGN_TOP;
			}
			return result;
		}

		private Style2D.HAligment _hAligment(string str)
		{
			Style2D.HAligment result;
			if (!(str == "left"))
			{
				if (!(str == "right"))
				{
					if (!(str == "center"))
					{
						result = Style2D.HAligment.NONE;
					}
					else
					{
						result = Style2D.HAligment.ALIGN_MIDDLE;
					}
				}
				else
				{
					result = Style2D.HAligment.ALIGN_RIGHT;
				}
			}
			else
			{
				result = Style2D.HAligment.ALIGN_LEFT;
			}
			return result;
		}
	}
}
