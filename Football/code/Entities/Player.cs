using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox
{
	class FootballPlayer : Player
	{
		public int ScoreTeam1;
		public int ScoreTeam2;

		protected virtual float MaxPullDistance => 2000.0f;
		protected virtual float MaxPushDistance => 2000.0f;
		protected virtual float PullForce => 20.0f;
		protected virtual float PushForce => 1000.0f;
		public override void Respawn()
		{
			SetModel("models/citizen/citizen.vmdl");
			Controller = new WalkController();
			Animator = new StandardPlayerAnimator();
			CameraMode = new FirstPersonCamera();
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
			base.Respawn();
		}

		public override void Simulate(Client cl)
		{
			base.Simulate(cl);
			if (!IsServer) return;
			if (Input.Pressed(InputButton.Attack2))
			{
				var tr = Trace.Ray(EyePosition, EyePosition + EyeRotation.Forward * MaxPullDistance)
					.UseHitboxes()
					.Ignore(this)
					.Radius(1.0f)
					.Run();
				if (!tr.Hit || !tr.Body.IsValid() || !tr.Entity.IsValid() || tr.Entity.IsWorld) { return; }
				if (tr.Entity.PhysicsGroup == null) { return; }
				var modelEnt = tr.Entity as ModelEntity;
				if (!modelEnt.IsValid()) { return; }
				var body = tr.Body;
				body.Velocity = new Vector3(0, 0, 0);
			}
			if (Input.Pressed(InputButton.Attack1))
			{
				var tr = Trace.Ray(EyePosition, EyePosition + EyeRotation.Forward * MaxPullDistance)
					.UseHitboxes()
					.Ignore(this)
					.Radius(1.0f)
					.Run();
				if (!tr.Hit || !tr.Body.IsValid() || !tr.Entity.IsValid() || tr.Entity.IsWorld) { return; }
				if (tr.Entity.PhysicsGroup == null) { return; }
				var modelEnt = tr.Entity as ModelEntity;
				if (!modelEnt.IsValid()) { return; }
				var body = tr.Body;
				var pushScale = 1.0f - Math.Clamp(tr.Distance / MaxPushDistance, 0.0f,1.0f);
				body.ApplyImpulseAt(tr.EndPosition, EyeRotation.Forward * (body.Mass * (PushForce * pushScale)));
			}


		}
	}
}
