using System;
using System.Collections.Generic;

namespace MuGame
{
	public struct a3_BagItemData : IComparable<a3_BagItemData>
	{
		public uint id;

		public uint tpid;

		public int num;

		public bool bnd;

		public bool isEquip;

		public bool isNew;

		public bool isSummon;

		public bool isLabeled;

		public bool ismark;

		public bool isrunestone;

		public a3_EquipData equipdata;

		public a3_ItemData confdata;

		public a3_SummonData summondata;

		public a3_AuctionData auctiondata;

		public a3_RunestoneData runestonedata;

		public int CompareTo(a3_BagItemData other)
		{
			bool flag = this.confdata.sortType > other.confdata.sortType;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = this.confdata.sortType < other.confdata.sortType;
				if (flag2)
				{
					result = -1;
				}
				else
				{
					bool flag3 = this.confdata.quality > other.confdata.quality;
					if (flag3)
					{
						result = -1;
					}
					else
					{
						bool flag4 = this.confdata.quality < other.confdata.quality;
						if (flag4)
						{
							result = 1;
						}
						else
						{
							bool flag5 = this.tpid > other.tpid;
							if (flag5)
							{
								result = 1;
							}
							else
							{
								bool flag6 = this.tpid < other.tpid;
								if (flag6)
								{
									result = -1;
								}
								else
								{
									bool flag7 = this.equipdata.stage > other.equipdata.stage;
									if (flag7)
									{
										result = -1;
									}
									else
									{
										bool flag8 = this.equipdata.stage < other.equipdata.stage;
										if (flag8)
										{
											result = 1;
										}
										else
										{
											bool flag9 = this.equipdata.intensify_lv > other.equipdata.intensify_lv;
											if (flag9)
											{
												result = -1;
											}
											else
											{
												bool flag10 = this.equipdata.intensify_lv < other.equipdata.intensify_lv;
												if (flag10)
												{
													result = 1;
												}
												else
												{
													bool flag11 = this.equipdata.add_level > other.equipdata.add_level;
													if (flag11)
													{
														result = -1;
													}
													else
													{
														bool flag12 = this.equipdata.add_level < other.equipdata.add_level;
														if (flag12)
														{
															result = 1;
														}
														else
														{
															Dictionary<int, int> expr_1B4 = this.equipdata.baoshi;
															int? num = (expr_1B4 != null) ? new int?(expr_1B4.Count) : null;
															Dictionary<int, int> expr_1DB = other.equipdata.baoshi;
															bool flag13 = num > ((expr_1DB != null) ? new int?(expr_1DB.Count) : null);
															if (flag13)
															{
																result = -1;
															}
															else
															{
																Dictionary<int, int> expr_22E = this.equipdata.baoshi;
																int? num2 = (expr_22E != null) ? new int?(expr_22E.Count) : null;
																Dictionary<int, int> expr_255 = other.equipdata.baoshi;
																bool flag14 = num2 < ((expr_255 != null) ? new int?(expr_255.Count) : null);
																if (flag14)
																{
																	result = 1;
																}
																else
																{
																	result = 0;
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}
