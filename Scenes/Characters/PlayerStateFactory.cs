using Godot;

public partial class PlayerStateFactory : Node
{
	public PlayerState GetState(Player.State state)
	{
		return state switch
		{
			Player.State.Moving => new MovingState(),
			Player.State.Tackling => new TacklingState(),
			Player.State.Recovering => new PlayerStateRecovering(),
			_ => null
		};
	}
}
