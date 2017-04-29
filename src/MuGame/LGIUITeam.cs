using Cross;
using System;

namespace MuGame
{
	public interface LGIUITeam
	{
		void set_teammates(Variant plys);

		void add_teammate(Variant ply);

		void remove_teammate(uint cid);

		void set_join_reqs(Variant reqs);

		void add_join_req(Variant req);

		void remove_join_req(uint cid);

		void teammate_data_change(uint cid, Variant data);

		void set_team_leader(uint cid);

		void set_dir_join(bool b);

		void set_memb_inv(bool b);

		void FreshStates(uint iid);

		void publish_team(bool b);

		void get_publish_team(Variant pubedTeam);

		void ChangeTeamMateHp(Variant data);

		void ChangeTeamMateDp(Variant data);

		void changeAllTeamMate();

		void OnTeamChange();
	}
}
