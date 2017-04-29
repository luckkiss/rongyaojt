using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MuGame.Qsmy.model
{
	internal class AutoPlayModel : ModelBase<AutoPlayModel>
	{
		private readonly int version = 3;

		public Action<int> onHpLowerChange = null;

		public Action<int> onMpLowerChange = null;

		public List<AutoPlayConfig4FB> autoplayCfg4FB = new List<AutoPlayConfig4FB>();

		private int nHpLower;

		private int nMpLower;

		private int[] skills = null;

		private SXML autoplayXml = null;

		public Dictionary<int, List<Vector3>> mapWayPoint = null;

		private int[] nhpdrugs = null;

		private int[] nmpdrugs = null;

		public int NHpLower
		{
			get
			{
				return this.nHpLower;
			}
			set
			{
				bool flag = this.nHpLower == value;
				if (!flag)
				{
					this.nHpLower = value;
					bool flag2 = this.onHpLowerChange != null;
					if (flag2)
					{
						this.onHpLowerChange(this.nHpLower);
					}
					InterfaceMgr.doCommandByLua("AutoPlayModel:getInstance().onHpLowerChange", "model/AutoPlayModel", new object[]
					{
						this.nHpLower
					});
				}
			}
		}

		public int NMpLower
		{
			get
			{
				return this.nMpLower;
			}
			set
			{
				bool flag = this.nMpLower == value;
				if (!flag)
				{
					this.nMpLower = value;
					bool flag2 = this.onMpLowerChange != null;
					if (flag2)
					{
						this.onMpLowerChange(this.nMpLower);
					}
					InterfaceMgr.doCommandByLua("AutoPlayModel:getInstance().onMpLowerChange", "model/AutoPlayModel", new object[]
					{
						this.nMpLower
					});
				}
			}
		}

		public int BuyDrug
		{
			get;
			set;
		}

		public int PickEqp
		{
			get;
			set;
		}

		public int PickMat
		{
			get;
			set;
		}

		public int PickEqp_cailiao
		{
			get;
			set;
		}

		public int PickPet_cailiao
		{
			get;
			set;
		}

		public int PickWing_cailiao
		{
			get;
			set;
		}

		public int PickSummon_cailiao
		{
			get;
			set;
		}

		public int PickDrugs
		{
			get;
			set;
		}

		public int PickGold
		{
			get;
			set;
		}

		public int PickOther
		{
			get;
			set;
		}

		public int EqpProc
		{
			get;
			set;
		}

		public int EqpType
		{
			get;
			set;
		}

		public int[] Skills
		{
			get
			{
				int[] arg_1A_0;
				if ((arg_1A_0 = this.skills) == null)
				{
					arg_1A_0 = (this.skills = new int[4]);
				}
				return arg_1A_0;
			}
		}

		public int Avoid
		{
			get;
			set;
		}

		public int AutoPK
		{
			get;
			set;
		}

		public int StoneRespawn
		{
			get;
			set;
		}

		public int GoldRespawn
		{
			get;
			set;
		}

		public int RespawnLimit
		{
			get;
			set;
		}

		public int RespawnUpBound
		{
			get;
			set;
		}

		public int RespawnLeft
		{
			get;
			set;
		}

		public SXML AutoplayXml
		{
			get
			{
				SXML arg_28_0;
				if ((arg_28_0 = this.autoplayXml) == null)
				{
					arg_28_0 = (this.autoplayXml = XMLMgr.instance.GetSXML("autoplay", ""));
				}
				return arg_28_0;
			}
		}

		public int[] Nhpdrugs
		{
			get
			{
				bool flag = this.nhpdrugs == null;
				int[] result;
				if (flag)
				{
					List<SXML> nodeList = this.AutoplayXml.GetNodeList("nhp", "");
					bool flag2 = nodeList == null || nodeList.Count == 0;
					if (flag2)
					{
						result = null;
						return result;
					}
					this.nhpdrugs = new int[nodeList.Count];
					for (int i = 0; i < nodeList.Count; i++)
					{
						this.nhpdrugs[i] = nodeList[i].getInt("id");
					}
				}
				result = this.nhpdrugs;
				return result;
			}
		}

		public int[] Nmpdrugs
		{
			get
			{
				bool flag = this.nmpdrugs == null;
				int[] result;
				if (flag)
				{
					List<SXML> nodeList = this.AutoplayXml.GetNodeList("nmp", "");
					bool flag2 = nodeList == null || nodeList.Count == 0;
					if (flag2)
					{
						result = null;
						return result;
					}
					this.nmpdrugs = new int[nodeList.Count];
					for (int i = 0; i < nodeList.Count; i++)
					{
						this.nmpdrugs[i] = nodeList[i].getInt("id");
					}
				}
				result = this.nmpdrugs;
				return result;
			}
		}

		public void Init()
		{
			this.ReadLocalCfg();
			this.ReadLocalData();
			this.InitMapWayPoint();
		}

		private void ReadLocalCfg()
		{
			List<SXML> nodeList = this.AutoplayXml.GetNodeList("conf_fb", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				float @float = nodeList[i].getFloat("mdis_fb");
				float float2 = nodeList[i].getFloat("pickdis_fb");
				float float3 = nodeList[i].getFloat("pkdis_fb");
				List<int> list = new List<int>();
				List<SXML> nodeList2 = nodeList[i].GetNodeList("map", "");
				for (int j = 0; j < nodeList2.Count; j++)
				{
					list.Add(nodeList2[j].getInt("map_id"));
				}
				this.autoplayCfg4FB.Add(new AutoPlayConfig4FB
				{
					Distance = @float,
					DistancePick = float2,
					DistancePK = float3,
					map = list
				});
			}
		}

		private void InitMapWayPoint()
		{
			bool flag = this.mapWayPoint == null;
			if (flag)
			{
				this.mapWayPoint = new Dictionary<int, List<Vector3>>();
			}
			List<SXML> nodeList = this.AutoplayXml.GetNodeList("map", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				List<SXML> nodeList2 = nodeList[i].GetNodeList("pos", "");
				int @int = nodeList[i].getInt("id");
				List<Vector3> list = new List<Vector3>();
				for (int j = 0; j < nodeList2.Count; j++)
				{
					list.Add(new Vector3
					{
						x = (float)nodeList2[j].getInt("x"),
						y = (float)nodeList2[j].getInt("y"),
						z = (float)nodeList2[j].getInt("z")
					});
				}
				this.mapWayPoint[@int] = list;
			}
		}

		private void SetDefault()
		{
			this.NHpLower = 80;
			this.NMpLower = 50;
			this.BuyDrug = 1;
			this.PickEqp = 31;
			this.PickMat = 31;
			this.EqpProc = 7;
			this.EqpType = 0;
			this.PickEqp_cailiao = 1;
			this.PickPet_cailiao = 1;
			this.PickWing_cailiao = 1;
			this.PickSummon_cailiao = 1;
			this.PickDrugs = 1;
			this.PickGold = 1;
			this.PickOther = 1;
			this.Skills[0] = 0;
			this.Skills[1] = 0;
			this.Skills[2] = 0;
			this.Skills[3] = 0;
			this.Avoid = 0;
			this.AutoPK = 0;
			this.StoneRespawn = 0;
			this.GoldRespawn = 0;
			this.RespawnLimit = 1;
			this.RespawnUpBound = 10;
		}

		public void ReadLocalData()
		{
			string text = FileMgr.loadString(FileMgr.TYPE_AUTO, "setting");
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				this.SetDefault();
			}
			else
			{
				try
				{
					string[] array = text.Split(new char[]
					{
						'|'
					});
					int num = 0;
					int num2 = int.Parse(array[num++]);
					bool flag2 = num2 != this.version;
					if (flag2)
					{
						throw new Exception("Autoplay local data version is not match!");
					}
					this.NHpLower = int.Parse(array[num++]);
					this.NMpLower = int.Parse(array[num++]);
					num++;
					this.BuyDrug = int.Parse(array[num++]);
					this.PickEqp = int.Parse(array[num++]);
					this.PickMat = int.Parse(array[num++]);
					this.EqpProc = int.Parse(array[num++]);
					this.PickEqp_cailiao = int.Parse(array[num++]);
					this.PickPet_cailiao = int.Parse(array[num++]);
					this.PickWing_cailiao = int.Parse(array[num++]);
					this.PickSummon_cailiao = int.Parse(array[num++]);
					this.PickDrugs = int.Parse(array[num++]);
					this.PickGold = int.Parse(array[num++]);
					this.PickOther = int.Parse(array[num++]);
					this.EqpType = int.Parse(array[num++]);
					this.Skills[0] = int.Parse(array[num++]);
					this.Skills[1] = int.Parse(array[num++]);
					this.Skills[2] = int.Parse(array[num++]);
					this.Skills[3] = int.Parse(array[num++]);
					num++;
					this.Avoid = int.Parse(array[num++]);
					this.AutoPK = int.Parse(array[num++]);
					this.StoneRespawn = int.Parse(array[num++]);
					this.GoldRespawn = int.Parse(array[num++]);
					this.RespawnLimit = int.Parse(array[num++]);
					this.RespawnUpBound = int.Parse(array[num++]);
				}
				catch (Exception ex)
				{
					Debug.Log(ex.Message);
					FileMgr.removeFile(FileMgr.TYPE_AUTO, "setting");
					this.SetDefault();
				}
			}
		}

		public void WriteLocalData()
		{
			string text = "";
			text = text + this.version.ToString() + "|";
			text = text + this.NHpLower.ToString() + "|";
			text = text + this.NMpLower.ToString() + "|";
			text = text + 0.ToString() + "|";
			text = text + this.BuyDrug.ToString() + "|";
			text = text + this.PickEqp.ToString() + "|";
			text = text + this.PickMat.ToString() + "|";
			text = text + this.EqpProc.ToString() + "|";
			text = text + this.PickEqp_cailiao.ToString() + "|";
			text = text + this.PickPet_cailiao.ToString() + "|";
			text = text + this.PickWing_cailiao.ToString() + "|";
			text = text + this.PickSummon_cailiao.ToString() + "|";
			text = text + this.PickDrugs.ToString() + "|";
			text = text + this.PickGold.ToString() + "|";
			text = text + this.PickOther.ToString() + "|";
			text = text + this.EqpType.ToString() + "|";
			text = text + this.Skills[0].ToString() + "|";
			text = text + this.Skills[1].ToString() + "|";
			text = text + this.Skills[2].ToString() + "|";
			text = text + this.Skills[3].ToString() + "|";
			text = text + 1.ToString() + "|";
			text = text + this.Avoid.ToString() + "|";
			text = text + this.AutoPK.ToString() + "|";
			text = text + this.StoneRespawn.ToString() + "|";
			text = text + this.GoldRespawn.ToString() + "|";
			text = text + this.RespawnLimit.ToString() + "|";
			text += this.RespawnUpBound.ToString();
			FileMgr.saveString(FileMgr.TYPE_AUTO, "setting", text);
		}

		public bool WillPick(uint tpid)
		{
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid);
			int quality = itemDataById.quality;
			int num = 1 << quality - 1;
			bool result = true;
			bool flag = itemDataById.item_type == 1;
			if (flag)
			{
				SXML sXML = XMLMgr.instance.GetSXML("autoplay", "");
				string[] source = sXML.GetNode("eqp_cailiao", "").getString("list").Split(new char[]
				{
					','
				});
				string[] source2 = sXML.GetNode("pet_cailiao", "").getString("list").Split(new char[]
				{
					','
				});
				string[] source3 = sXML.GetNode("wing_cailiao", "").getString("list").Split(new char[]
				{
					','
				});
				string[] source4 = sXML.GetNode("summon_cailiao", "").getString("list").Split(new char[]
				{
					','
				});
				string[] source5 = sXML.GetNode("drugs", "").getString("list").Split(new char[]
				{
					','
				});
				bool flag2 = source.Contains(tpid.ToString()) && this.PickEqp_cailiao != 1;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = source2.Contains(tpid.ToString()) && this.PickPet_cailiao != 1;
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = source3.Contains(tpid.ToString()) && this.PickWing_cailiao != 1;
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = source4.Contains(tpid.ToString()) && this.PickSummon_cailiao != 1;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = source5.Contains(tpid.ToString()) && this.PickDrugs != 1;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = this.PickOther != 1;
									if (flag7)
									{
										result = false;
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag8 = itemDataById.item_type == 2;
				if (flag8)
				{
					result = ((num & this.PickEqp) != 0);
				}
				else
				{
					bool flag9 = tpid == 0u;
					if (flag9)
					{
						bool flag10 = this.PickGold != 1;
						if (flag10)
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		public int GetSkillForUse()
		{
			int result;
			for (int i = 0; i < 5; i++)
			{
				bool flag = this.Skills[i] == 0;
				if (!flag)
				{
					int num = this.Skills[i];
					skill_a3Data skill_a3Data = null;
					ModelBase<Skill_a3Model>.getInstance().skilldic.TryGetValue(num, out skill_a3Data);
					bool flag2 = skill_a3Data == null;
					if (!flag2)
					{
						bool flag3 = skill_a3Data.cdTime > 0;
						if (!flag3)
						{
							long num2 = muNetCleint.instance.CurServerTimeStampMS + (long)((ulong)skill_a3Data.cd);
							bool flag4 = skill_a3Data.endCD < num2;
							if (flag4)
							{
								skill_a3Data.endCD = num2;
							}
							result = num;
							return result;
						}
					}
				}
			}
			result = skillbar.NORNAL_SKILL_ID;
			return result;
		}

		public List<int> GetAllHpDrugID()
		{
			List<SXML> nodeList = this.AutoplayXml.GetNodeList("nhp", "");
			List<int> list = new List<int>();
			for (int i = 0; i < nodeList.Count; i++)
			{
				int @int = nodeList[i].getInt("id");
				list.Add(@int);
			}
			return list;
		}

		public List<int> GetAllMpDrugID()
		{
			List<SXML> nodeList = this.AutoplayXml.GetNodeList("nmp", "");
			List<int> list = new List<int>();
			for (int i = 0; i < nodeList.Count; i++)
			{
				int @int = nodeList[i].getInt("id");
				list.Add(@int);
			}
			return list;
		}

		public List<Vector3> GetMapAutoPlayPosition(int mapid)
		{
			return new List<Vector3>();
		}
	}
}
