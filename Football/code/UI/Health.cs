using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Sandbox
{
    public class Health : Panel
    {
		public Label HealthLbl;
		public Panel HealthPnl;

		public Health()
        {
			HealthPnl = Add.Panel("HealthPnl");
			HealthLbl = HealthPnl.Add.Label("100", "HealthLbl");
			StyleSheet.Load("UI/healthStyle.scss");
        }

		public override void Tick()
        {
			var player = Local.Pawn;
			if (player == null) return;
			HealthLbl.Text = $"🩸{player.Health.CeilToInt()}";
        }
    }
}
