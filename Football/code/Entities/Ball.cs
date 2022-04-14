using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Sandbox
{
	[Library("ent_football_ball", Title = "Football ball", Spawnable = true)]
    public partial class Ball : Prop, IUse
    {
		public float MaxSpeed { get; set; } = 500.0f;
		public float SpeedMul { get; set; } = 1.05f;

		public override void Spawn()
        {
			base.Spawn();
			SetModel("models/citizen_props/beachball.vmdl");
			SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
			Scale = 1.0f;
        }

		protected override void OnPhysicsCollision(CollisionEventData eventData)
        {
			var speed = eventData.PreVelocity.Length;
			var direction = Vector3.Reflect(eventData.PreVelocity.Normal, eventData.Normal.Normal).Normal;
			Velocity = direction * MathF.Min(speed * SpeedMul, MaxSpeed);
        }

		public bool IsUsable(Entity user)
        {
			return true;
        }
		public bool OnUse(Entity user)
        {
			if(user is FootballPlayer player)
            {
				player.Health += 10;
				Delete();
            }
			return false;
        }
    }
}
