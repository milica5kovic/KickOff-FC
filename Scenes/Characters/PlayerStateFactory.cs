using Godot;
using Godot.Collections;

public partial class PlayerStateFactory : Node
{
	public Dictionary<Player.State, PlayerState> states = new();

	public override void _Ready()
	{
		states = new Dictionary<Player.State, PlayerState>
		{
			{ Player.State.Moving, GetNode<MovingState>("MovingState") },
			{ Player.State.Tackling, GetNode<TacklingState>("TacklingState") }
		};

		foreach (var state in states.Values)
		{
			state.SetProcess(false);
			state.SetPhysicsProcess(false);
		}
	}
public PlayerState GetState(Player.State state)
{
	if (!states.ContainsKey(state))
	{
		GD.PushError($"State {state} doesn't exist!");
		return null;
	}

	return state switch
	{
		Player.State.Moving => new MovingState(),
		Player.State.Tackling => new TacklingState(),
		_ => null
	};
}
}
