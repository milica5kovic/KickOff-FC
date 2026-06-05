using Godot;
using System.Collections.Generic;

namespace Utils
{
	public partial class Keys : Node
	{
		public enum Action
		{
			LEFT,
			RIGHT,
			UP,
			DOWN,
			SHOOT,
			PASS
		}

		Dictionary<Player.ControlScheme, Dictionary<Action, string>> actionsMap = new()
		{
			{
				Player.ControlScheme.P1, new Dictionary<Action, string>
				{
					{ Action.LEFT, "p1_left" },
					{ Action.RIGHT, "p1_right" },
					{ Action.UP, "p1_up" },
					{ Action.DOWN, "p1_down" },
					{ Action.SHOOT, "p1_shoot" },
					{ Action.PASS, "p1_pass" }
				}
			},
			{
				Player.ControlScheme.P2, new Dictionary<Action, string>
				{
					{ Action.LEFT, "p2_left" },
					{ Action.RIGHT, "p2_right" },
					{ Action.UP, "p2_up" },
					{ Action.DOWN, "p2_down" },
					{ Action.SHOOT, "p2_shoot" },
					{ Action.PASS, "p2_pass" }
				}
			}
		};
		public Vector2 GetInput(Player.ControlScheme scheme)
		{
			var map = actionsMap[scheme];
			return Input.GetVector(map[Action.LEFT], map[Action.RIGHT], map[Action.UP], map[Action.DOWN]);
		}

		public bool IsActionPressed(Player.ControlScheme scheme, Action action)
		{
			return Input.IsActionPressed(actionsMap[scheme][action]);
		}
	
		public bool IsActionJustPressed(Player.ControlScheme scheme, Action action)
		{
			return Input.IsActionJustPressed(actionsMap[scheme][action]);
		}
		public bool IsActionJustReleased(Player.ControlScheme scheme, Action action)
		{
			return Input.IsActionJustReleased(actionsMap[scheme][action]);
		}

		
	
	
	}

}
