using Godot;

public partial class PlayerState : Node
{
	[Signal] public delegate void StateTransitionRequestedEventHandler(Player.State newState);
	public Player Player;
	public AnimationPlayer AnimationPlayer;
	private bool _entered = false;

	public void Setup(Player player)
	{
		Player = player;
		AnimationPlayer = player.AnimationPlayer;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!_entered)
		{
			_entered = true;
			OnEnter();
		}
	}

	public virtual void OnEnter() { }
}
