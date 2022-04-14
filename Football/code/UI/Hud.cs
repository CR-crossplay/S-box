using Sandbox;
using Sandbox.UI;
namespace Sandbox
{
    public partial class Hud : RootPanel
    {
		public Hud()
        {
			AddChild<Health>();
			AddChild<FootScore>();
        }
    }
}
