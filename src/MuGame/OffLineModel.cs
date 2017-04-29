using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class OffLineModel : ModelBase<OffLineModel>
	{
		private List<List<float>> rateList = new List<List<float>>();

		public static int maxTime = 86400;

		public static int minTime = 3600;

		private int offLineTime = 0;

		public Action OnOffLineTimeChange = null;

		private int baseExp = 0;

		public Action OnBaseExpChange = null;

		private bool canGetExp = false;

		private SXML offLineXML;

		public int OffLineTime
		{
			get
			{
				return this.offLineTime;
			}
			set
			{
				bool flag = value > OffLineModel.maxTime;
				if (flag)
				{
					value = OffLineModel.maxTime;
				}
				this.offLineTime = value;
				bool flag2 = this.OnOffLineTimeChange != null;
				if (flag2)
				{
					this.OnOffLineTimeChange();
				}
			}
		}

		public int BaseExp
		{
			get
			{
				return this.baseExp;
			}
			set
			{
				this.baseExp = value;
				bool flag = this.OnBaseExpChange != null;
				if (flag)
				{
					this.OnBaseExpChange();
				}
				bool flag2 = a3_expbar.instance != null;
				if (flag2)
				{
					a3_expbar.instance.btnAutoOffLineExp.gameObject.SetActive(this.CanGetExp);
				}
			}
		}

		public bool CanGetExp
		{
			get
			{
				bool flag = this.BaseExp > 0;
				if (flag)
				{
					this.canGetExp = true;
				}
				else
				{
					this.canGetExp = false;
				}
				return this.canGetExp;
			}
		}

		public SXML OffLineXML
		{
			get
			{
				bool flag = this.offLineXML == null;
				if (flag)
				{
					this.offLineXML = XMLMgr.instance.GetSXML("offlineExp_a3", "");
				}
				return this.offLineXML;
			}
		}

		public int GetDoubleExp()
		{
			return this.BaseExp * 2;
		}

		public int GetThreefoldExp()
		{
			return this.BaseExp * 3;
		}

		public int GetQuadrupleExp()
		{
			return this.BaseExp * 4;
		}

		public float GetCost(int type)
		{
			float rateByType = this.GetRateByType(type);
			int num = (int)Math.Floor((double)((float)this.OffLineTime / 600f));
			float num2 = rateByType * (float)num;
			return (float)Math.Ceiling((double)num2);
		}

		public float GetRateByType(int type)
		{
			SXML node = this.OffLineXML.GetNode("rate_type", "type==" + type.ToString());
			return node.getFloat("cost_value");
		}

		public int GetSpendTypeByType(int type)
		{
			SXML node = this.OffLineXML.GetNode("rate_type", "type==" + type.ToString());
			return node.getInt("cost_type");
		}
	}
}
