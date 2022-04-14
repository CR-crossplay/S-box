
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace Sandbox
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	public partial class FootballGame : Sandbox.Game
	{
		public Hud Hud;

		[Net, Change]
		public int ScoreTeam1 { get; set; }
		private void OnScoreTeam1Changed(int oldValue, int newValue)
        {
			(Local.Pawn as FootballPlayer).ScoreTeam1 = newValue;

        }

		[Net, Change]
		public int ScoreTeam2 { get; set; }
		private void OnScoreTeam2Changed(int oldValue, int newValue)
		{
			(Local.Pawn as FootballPlayer).ScoreTeam2 = newValue;

		}

		public void ScoreAGoal(int team)
        {
			if(team == 1) { ScoreTeam1++; }
            else { ScoreTeam2++; }
        }


		public FootballGame()
		{
			if(IsClient)
            {
				Hud = new Hud();

            }
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			// Create a pawn for this client to play with
			var pawn = new FootballPlayer();
			client.Pawn = pawn;
			pawn.ScoreTeam2 = ScoreTeam2;
			pawn.ScoreTeam1 = ScoreTeam1;
			pawn.Tags.Add("player");
			pawn.Respawn();
		}

		[ServerCmd("spawnball",Help = "fais apparaître une balle")]
		public static void SpawnBall()
        {
			var caller = ConsoleSystem.Caller.Pawn;
			var ball = new Ball()
			{
				Position = caller.Position + caller.Rotation.Forward * 50
			};
			ball.Tags.Add("ball");
        }
	}

}
