using Cross;
using System;

namespace MuGame
{
	public class Algorithm
	{
		public delegate double AlgorithmTypeFun(Variant att, double tm);

		public static double TweenShake(Variant att, double tm)
		{
			double num = tm / att["att"]["duration"]._double;
			double num2 = num * 2.0;
			bool flag = num > 1.0;
			if (flag)
			{
				num -= (double)((int)num);
			}
			double num3 = (att["cnt"]._double * 2.0 - num2) / (att["cnt"]._double * 2.0);
			double num4 = att["change"]._double * num3 * num3;
			return att["begin"]._double + num4 * Math.Sin(6.2831853071795862 * num);
		}

		public static double TweenLine(Variant att, double tm)
		{
			return att["begin"]._double + tm / att["duration"]._double * att["change"]._double;
		}

		public static double TweenCubicEaseIn(Variant att, double tm)
		{
			double num = tm / att["duration"]._double;
			return att["begin"]._double + num * num * num * att["change"]._double;
		}

		public static double TweenCubicEaseOut(Variant att, double tm)
		{
			double num = tm / att["duration"]._double - 1.0;
			return att["begin"]._double + (num * num * num + 1.0) * att["change"]._double;
		}

		public static double TweenCubicEaseInOut(Variant att, double tm)
		{
			double num = tm / att["duration"]._double * 2.0;
			bool flag = num < 1.0;
			double result;
			if (flag)
			{
				result = att["begin"]._double + num * num * num * att["change"]._double / 2.0;
			}
			else
			{
				num -= 2.0;
				result = att["begin"]._double + (num * num * num + 2.0) * att["change"]._double / 2.0;
			}
			return result;
		}

		public static double TweenBounceEaseOut(Variant att, double tm)
		{
			double @double = att["duration"]._double;
			double double2 = att["begin"]._double;
			double double3 = att["change"]._double;
			bool flag = tm == 0.0;
			double result;
			if (flag)
			{
				result = double2;
			}
			else
			{
				double num;
				bool flag2 = (num = tm / @double) == 1.0;
				if (flag2)
				{
					result = double2 + double3;
				}
				else
				{
					double num2 = @double * 0.30000001192092896;
					double num3 = double3;
					double num4 = num2 / 4.0;
					result = num3 * Math.Pow(2.0, -10.0 * num) * Math.Sin((num * @double - num4) * 6.2831853071795862 / num2) + double3 + double2;
				}
			}
			return result;
		}

		public static double TweenQuadEaseIn(Variant att, double tm)
		{
			double num = tm / att["duration"]._double;
			return att["begin"]._double + num * num * att["change"]._double;
		}

		public static double TweenQuadEaseOut(Variant att, double tm)
		{
			double num = tm / att["duration"]._double;
			return att["begin"]._double + num * (2.0 - num) * att["change"]._double;
		}

		public static double TweenQuadEaseInOut(Variant att, double tm)
		{
			double num = tm / att["duration"]._double * 2.0;
			bool flag = num < 1.0;
			double result;
			if (flag)
			{
				result = att["begin"]._double + num * num * att["change"]._double / 2.0;
			}
			else
			{
				num -= 1.0;
				result = att["begin"]._double + num * (2.0 - num) * att["change"]._double / 2.0 + att["change"]._double / 2.0;
			}
			return result;
		}

		public static double TweenQuadCircular(Variant att, double tm)
		{
			double num = tm / att["duration"]._double;
			bool flag = num > 1.0;
			if (flag)
			{
				num -= (double)((int)num);
			}
			return att["begin"]._double + num * (5.0 * num - 2.0) / 3.0 * att["change"]._double;
		}

		public static double TweenQuadEaseInTF(Variant att, double tm)
		{
			double num = tm / att["duration"]._double;
			bool flag = num > 1.0;
			if (flag)
			{
				num -= (double)((int)tm);
			}
			num *= 2.0;
			bool flag2 = num > 1.0;
			if (flag2)
			{
				num = 2.0 - num;
			}
			return att["begin"]._double + num * num * att["change"]._double;
		}

		public static double TweenQuadEaseOutTF(Variant att, double tm)
		{
			double num = tm / att["duration"]._double;
			bool flag = num > 1.0;
			if (flag)
			{
				num -= (double)((int)num);
			}
			num *= 2.0;
			bool flag2 = num > 1.0;
			if (flag2)
			{
				num = 2.0 - num;
			}
			return att["begin"]._double + num * (2.0 - num) * att["change"]._double;
		}

		public static double TweenQuadEaseInOutTF(Variant att, double tm)
		{
			double num = tm / att["duration"]._double;
			bool flag = num > 1.0;
			if (flag)
			{
				num -= (double)((int)num);
			}
			num *= 2.0;
			bool flag2 = num > 1.0;
			if (flag2)
			{
				num = 2.0 - num;
			}
			num *= 2.0;
			bool flag3 = num < 1.0;
			double result;
			if (flag3)
			{
				result = att["begin"]._double + num * num * att["change"]._double / 2.0;
			}
			else
			{
				num -= 1.0;
				result = att["begin"]._double + num * (2.0 - num) * att["change"]._double / 2.0 + att["change"]._double / 2.0;
			}
			return result;
		}

		public static double TweenExpoEaseOut(Variant att, double tm)
		{
			bool flag = tm == att["duration"]._double;
			double result;
			if (flag)
			{
				result = att["begin"]._double + att["change"]._double;
			}
			else
			{
				double num = tm / att["duration"]._double;
				result = att["change"]._double * (-Math.Pow(2.0, -10.0 * num) + 1.0) + att["begin"]._double;
			}
			return result;
		}

		public static Algorithm.AlgorithmTypeFun GetTwennFun(string type = "Line")
		{
			bool flag = "Line" == type;
			Algorithm.AlgorithmTypeFun result;
			if (flag)
			{
				result = new Algorithm.AlgorithmTypeFun(Algorithm.TweenLine);
			}
			else
			{
				bool flag2 = "QuadraticIn" == type;
				if (flag2)
				{
					result = new Algorithm.AlgorithmTypeFun(Algorithm.TweenQuadEaseIn);
				}
				else
				{
					bool flag3 = "QuadraticOut" == type;
					if (flag3)
					{
						result = new Algorithm.AlgorithmTypeFun(Algorithm.TweenQuadEaseOut);
					}
					else
					{
						bool flag4 = "CubicIn" == type;
						if (flag4)
						{
							result = new Algorithm.AlgorithmTypeFun(Algorithm.TweenCubicEaseIn);
						}
						else
						{
							bool flag5 = "CubicOut" == type;
							if (flag5)
							{
								result = new Algorithm.AlgorithmTypeFun(Algorithm.TweenCubicEaseOut);
							}
							else
							{
								result = new Algorithm.AlgorithmTypeFun(Algorithm.TweenLine);
							}
						}
					}
				}
			}
			return result;
		}
	}
}
