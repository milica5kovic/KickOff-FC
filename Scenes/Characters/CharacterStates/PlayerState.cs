using Godot;

public partial class PlayerState : Node
{
	[Signal] public delegate void StateTransitionRequestedEventHandler(Player.State newState);
	public Player Player;
	public AnimationPlayer AnimationPlayer;

	public void Setup(Player player)
{
	Player = player;
	AnimationPlayer = player.AnimationPlayer;
	SetProcess(true);
	SetPhysicsProcess(true);
	OnEnter();
}

	public virtual void OnEnter() { }
}
