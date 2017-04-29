using Cross;
using System;

namespace MuGame
{
	public interface LGIUINobility
	{
		void UpdateNobInfo(Variant data);

		void levelChange();

		void OnItemChange(Variant item);

		void AddAchives(Variant achs);

		void RmvAchive(int achid);

		void ActiveAchive(int achid);

		void DeactiveAchive(int achid);

		void SelfAddEquip(Variant eqps);

		void SelfRmvEquip(Variant eqps);
	}
}
