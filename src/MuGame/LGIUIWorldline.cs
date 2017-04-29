using Cross;
using System;

namespace MuGame
{
	public interface LGIUIWorldline
	{
		void change_line_success_res(Variant data);

		void line_info_res(Variant data);

		void vip_expire();
	}
}
