using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Sandbox
{
	public class FootScore : Panel
	{
		public Label ScoreTeam1Lbl;
		public Panel Team1Pnl;

		public Label ScoreTeam2Lbl;
		public Panel Team2Pnl;

		public Panel TeamPnl;

		public Label TeamScoreSeparator;


		public FootScore()
        {
			TeamPnl = Add.Panel("TeamPnl");

			Team1Pnl = TeamPnl.Add.Panel("Team1Pnl");
			TeamScoreSeparator = TeamPnl.Add.Label("TeamScoreSeparator");
			Team2Pnl = TeamPnl.Add.Panel("Team2Pnl");

			ScoreTeam1Lbl = Team1Pnl.Add.Label("0", "Team1Lbl");
			ScoreTeam2Lbl = Team2Pnl.Add.Label("0", "Team2Lbl");

			TeamScoreSeparator.Text = "/";

			StyleSheet.Load("UI/footScoreStylesheet.scss");
		}

		public override void Tick()
        {
			var player = Local.Pawn;
			if (player == null) return;
			var p = (player as FootballPlayer);
			ScoreTeam1Lbl.Text = $"{p.ScoreTeam1}";
			ScoreTeam2Lbl.Text = $"{p.ScoreTeam2}";
		}
	}
}
