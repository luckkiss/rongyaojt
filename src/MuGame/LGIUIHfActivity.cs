using Cross;
using System;

namespace MuGame
{
	public interface LGIUIHfActivity
	{
		void refreshRankact(Variant ractdata);

		void refreshYbract(uint ractid, Variant ybractData);

		void refreshAwdact(uint ractid, Variant awdactData);

		void on_get_dbmkt_itm(Variant data);
	}
}
