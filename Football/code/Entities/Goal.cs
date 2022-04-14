using Sandbox;
using Sandbox.Internal;

namespace Sandbox.Entities
{
	[Library("football_goal",Description = "Where to put the ball", Spawnable = true)]
	[Hammer.Solid]
	[Hammer.VisGroup(Hammer.VisGroup.Trigger)]
    public partial class Goal : ModelEntity
    {
		[Property(Title = "Team")]
		public int team { get; set; } = 1;

		public override void Spawn()
        {
			base.Spawn();
			SetupPhysicsFromModel(PhysicsMotionType.Static);
			CollisionGroup = CollisionGroup.Trigger;
			EnableSolidCollisions = false;
			EnableTouch = true;

			Transmit = TransmitType.Never;
        }

		public override void StartTouch(Entity other)
        {
			if(IsClient)
            {
				return;
            }
			if(other.Tags.Has("ball"))
            {
				Log.Info($"Team numéro {team} a marqué");
				(Game.Current as FootballGame).ScoreAGoal(team);
				other.Velocity = new Vector3(0, 0, 0);
				other.Position = new Vector3(0, 0, 5);
            }
		}
    }
}
