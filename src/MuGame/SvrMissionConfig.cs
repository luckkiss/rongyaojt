using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class SvrMissionConfig : configParser
	{
		private Variant m_mis_misline_data;

		private Variant m_mis_npc_data;

		private Variant m_resolve = new Variant();

		private List<Variant> m_hasAttMline = null;

		private Variant autocomitArr = null;

		private Variant m_allChapterMis = null;

		private Variant m_sortAllChapterMis = null;

		public SvrMissionConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrMissionConfig(m as ClientConfig);
		}

		public Variant resolve()
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				for (int i = 0; i < this.m_conf["mission"].Count; i++)
				{
					this.m_resolve[this.m_conf["mission"][i]["id"]._int] = this.m_conf["mission"][i];
				}
				result = this.m_resolve;
			}
			return result;
		}

		public Variant get_mission_conf(int misid)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_conf["mission"][misid.ToString()];
			}
			return result;
		}

		public Variant get_mis_by_line(int mis_line)
		{
			return this.m_mis_misline_data[mis_line.ToString()];
		}

		public Variant get_missions()
		{
			return this.m_conf["mission"];
		}

		public string getMisName(int misid)
		{
			return LanguagePack.getLanguageText("misName", misid.ToString());
		}

		public string getMisDesc(int misid)
		{
			return LanguagePack.getLanguageText("misDesc", misid.ToString());
		}

		public string getMisGoalDesc(int misid)
		{
			return LanguagePack.getLanguageText("misGoalDesc", misid.ToString());
		}

		public Variant get_mis_by_npc(int npc_iid)
		{
			return this.m_mis_npc_data[npc_iid.ToString()];
		}

		public Variant get_rmiss()
		{
			return this.m_conf["rmis"];
		}

		protected override Variant _formatConfig(Variant conf)
		{
			this.m_mis_misline_data = new Variant();
			this.m_mis_npc_data = new Variant();
			bool flag = conf != null;
			if (flag)
			{
				bool flag2 = conf.ContainsKey("mission");
				if (flag2)
				{
					Variant variant = new Variant();
					Variant variant2 = conf["mission"];
					for (int i = 0; i < variant2.Count; i++)
					{
						Variant variant3 = variant2[i];
						bool flag3 = variant3.ContainsKey("accept");
						if (flag3)
						{
							variant3["accept"] = variant3["accept"][0];
							bool flag4 = variant3["accept"].ContainsKey("npc") && variant3["accept"]["npc"]._str == "";
							if (flag4)
							{
								variant3["accept"]["npc"] = "0";
							}
						}
						bool flag5 = variant3.ContainsKey("awards");
						if (flag5)
						{
							variant3["awards"] = variant3["awards"][0];
							bool flag6 = variant3["awards"].ContainsKey("npc") && variant3["awards"]["npc"]._str == "";
							if (flag6)
							{
								variant3["awards"]["npc"] = "0";
							}
						}
						bool flag7 = !this.m_mis_misline_data.ContainsKey(variant3["misline"]._str);
						if (flag7)
						{
							this.m_mis_misline_data[variant3["misline"]._str] = new Variant();
						}
						this.m_mis_misline_data[variant3["misline"]._str][variant3["id"]._str] = variant3;
						bool flag8 = !this.m_mis_npc_data.ContainsKey(variant3["accept"]["npc"]._str);
						if (flag8)
						{
							this.m_mis_npc_data[variant3["accept"]["npc"]._str] = new Variant();
						}
						this.m_mis_npc_data[variant3["accept"]["npc"]._str][variant3["id"]._str] = variant3;
						variant[variant3["id"]._str] = variant3;
					}
					conf["mission"] = variant;
				}
			}
			return conf;
		}

		protected override void onData()
		{
			this.format_mission();
		}

		protected void format_mission()
		{
		}

		public Variant GetRmisShare(int id)
		{
			Variant variant = this.m_conf["rmis_share"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current != null && current["id"] == id;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant get_autocomit_mis()
		{
			bool flag = this.autocomitArr == null;
			if (flag)
			{
				this.autocomitArr = new Variant();
				Variant variant = null;
				foreach (string current in this.m_mis_misline_data.Keys)
				{
					bool flag2 = current == "1";
					if (!flag2)
					{
						variant = this.m_mis_misline_data[current];
						bool flag3 = variant == null;
						if (!flag3)
						{
							foreach (string current2 in variant.Keys)
							{
								Variant variant2 = variant[current2];
								bool flag4 = variant2 == null;
								if (!flag4)
								{
									bool flag5 = !variant2.ContainsKey("dalyrep") || variant2["dalyrep"]._int <= 0 || !variant2.ContainsKey("autocomit_yb");
									if (!flag5)
									{
										Variant variant3 = variant2["accept"]["attchk"];
										bool flag6 = variant3 == null;
										if (!flag6)
										{
											int idx = 0;
											int val = 0;
											foreach (string current3 in variant3.Keys)
											{
												bool flag7 = variant3[current3] == null;
												if (!flag7)
												{
													bool flag8 = variant3[current3]["name"]._str != "level";
													if (!flag8)
													{
														bool flag9 = variant3[current3].ContainsKey("min");
														if (flag9)
														{
															idx = variant3[current3]["min"];
														}
														bool flag10 = variant3[current3].ContainsKey("max");
														if (flag10)
														{
															val = variant3[current3]["max"];
														}
													}
												}
											}
											Variant variant4 = new Variant();
											bool flag11 = this.autocomitArr[idx] == null;
											if (flag11)
											{
												Variant variant5 = new Variant();
												variant4["data"] = variant[current2];
												variant4["mx"] = val;
												variant5.pushBack(variant4);
												this.autocomitArr[idx] = variant5;
											}
											else
											{
												variant4["data"] = variant[current2];
												variant4["mx"] = val;
												this.autocomitArr[idx].pushBack(variant4);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return this.autocomitArr;
		}

		public Variant get_mis_appawd(int id)
		{
			Variant variant = this.m_conf["appawd"];
			bool flag = variant == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = variant[id.ToString()];
			}
			return result;
		}

		public Variant get_mis_appgoal(int id)
		{
			Variant variant = this.m_conf["appgoal"];
			bool flag = variant == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current != null && current["id"]._int == id;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
				result = variant[id.ToString()];
			}
			return result;
		}

		public Variant get_qamis(int id)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_conf["qamis"][id.ToString()];
			}
			return result;
		}

		public Variant get_question(int id)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_conf["question"][id.ToString()];
			}
			return result;
		}

		public Variant GetDmisById(int id)
		{
			bool flag = this.m_conf["dmis"] != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["dmis"][id.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetDmisAwd()
		{
			return this.m_conf["dmis_awd"];
		}

		public Variant GetMlineawd()
		{
			return this.m_conf["mlineawd"];
		}

		public List<Variant> GetMlineawdByAtt()
		{
			bool flag = this.m_hasAttMline == null;
			List<Variant> hasAttMline;
			if (flag)
			{
				this.m_hasAttMline = new List<Variant>();
				Variant mlineawd = this.GetMlineawd();
				for (int i = 0; i < mlineawd.Count; i++)
				{
					Variant variant = mlineawd[i];
					bool flag2 = variant.ContainsKey("att");
					if (flag2)
					{
						this.m_hasAttMline.Add(variant);
					}
				}
				hasAttMline = this.m_hasAttMline;
			}
			else
			{
				hasAttMline = this.m_hasAttMline;
			}
			return hasAttMline;
		}

		public Variant GetCurMlineAwd(int lastid)
		{
			Variant mlineawd = this.GetMlineawd();
			Variant variant = null;
			bool flag = lastid > 0;
			if (flag)
			{
				for (int i = 0; i < mlineawd.Count; i++)
				{
					variant = mlineawd[i];
					bool flag2 = lastid == variant["misid"]._int;
					if (flag2)
					{
						variant = mlineawd[i + 1];
						bool flag3 = variant != null;
						if (flag3)
						{
							break;
						}
					}
				}
			}
			else
			{
				variant = mlineawd[0];
			}
			return variant;
		}

		public int GetAwardChapter(int misid)
		{
			Variant mlineawd = this.GetMlineawd();
			int result;
			for (int i = 0; i < mlineawd.Count; i++)
			{
				Variant variant = mlineawd[i.ToString()];
				bool flag = misid == variant[misid.ToString()]._int;
				if (flag)
				{
					result = i;
					return result;
				}
			}
			result = 0;
			return result;
		}

		public Variant GetMlineawdByItm(int misid, int carr)
		{
			Variant mlineawd = this.GetMlineawd();
			Variant result;
			for (int i = 0; i < mlineawd.Count; i++)
			{
				Variant variant = mlineawd[i];
				bool flag = misid == variant[misid.ToString()]._int;
				if (flag)
				{
					Variant variant2 = variant["awards"]["award"];
					Variant variant4;
					for (int j = 0; j < variant2.Count; j++)
					{
						Variant variant3 = variant2[j];
						bool flag2 = variant3.ContainsKey(carr.ToString()) && variant3[carr.ToString()]._int == carr;
						if (flag2)
						{
							variant4 = variant3;
							variant4[misid.ToString()]._int = variant[misid.ToString()]._int;
							result = variant4;
							return result;
						}
					}
					variant4 = variant2[0];
					variant4[misid.ToString()]._int = variant[misid.ToString()]._int;
					result = variant4;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetChapterMisByChapter(int curChapter)
		{
			bool flag = this.m_allChapterMis == null;
			if (flag)
			{
				this.m_allChapterMis = new Variant();
				this.m_sortAllChapterMis = new Variant();
				Variant missions = this.get_missions();
				int num = 0;
				Variant mlineawd = this.GetMlineawd();
				Variant variant = mlineawd[num];
				bool flag2 = true;
				Variant variant2 = null;
				List<Variant> list = null;
				for (int i = 0; i < missions.Count; i++)
				{
					Variant variant3 = missions[i];
					bool flag3 = variant3 == null;
					if (!flag3)
					{
						bool flag4 = variant3.ContainsKey("misline") && variant3["misline"]._int == 1;
						if (flag4)
						{
							bool flag5 = variant3["id"]._int > variant["misid"]._int;
							if (flag5)
							{
								flag2 = true;
								num++;
							}
							bool flag6 = flag2;
							if (flag6)
							{
								flag2 = false;
								variant = mlineawd[num];
								bool flag7 = variant == null;
								if (flag7)
								{
									break;
								}
								bool flag8 = this.m_allChapterMis[num] == null;
								if (flag8)
								{
									this.m_allChapterMis[num] = new Variant();
								}
								bool flag9 = this.m_sortAllChapterMis[num] == null;
								if (flag9)
								{
									this.m_sortAllChapterMis[num] = new Variant();
								}
								variant2 = this.m_allChapterMis[num];
								list = new List<Variant>(this.m_sortAllChapterMis[num].Values);
							}
							variant2[variant3["id"]._str] = variant3;
							list.Add(variant3);
						}
					}
				}
			}
			return this.m_sortAllChapterMis[curChapter];
		}

		public bool IsChapterMis(int curChapter, int misid)
		{
			bool flag = this.m_allChapterMis[curChapter] == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.m_allChapterMis[curChapter][misid] == null;
				result = !flag2;
			}
			return result;
		}
	}
}
