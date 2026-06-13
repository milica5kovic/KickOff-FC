using Godot;
using Utils;

public partial class Player : CharacterBody2D
{
	public enum ControlScheme { CPU, P1, P2 }
	public enum State { Moving, Tackling, Recovering }

	[Export] public float speed;
	[Export] public Vector2 heading = Vector2.Right;
	[Export] public Keys Keys { get; set; }
	[Export] public ControlScheme Control { get; set; }
	[Export] public AnimationPlayer AnimationPlayer { get; set; }
	[Export] public Sprite2D PlayerSprite { get; set; }

	public PlayerState currentState;
	public PlayerStateFactory stateFactory;

	public override void _Ready()
	{
		stateFactory = GetNode<PlayerStateFactory>("PlayerStateFactory");
		SwitchState(State.Moving);
	}

	public override void _PhysicsProcess(double delta)
	{
		FlipPlayer();
		MoveAndSlide();
	}

	public void SwitchState(State newState)
	{
		if (currentState != null)
		{
			currentState.StateTransitionRequested -= SwitchState;
			currentState.QueueFree();
		}

		currentState = stateFactory.GetState(newState);
		currentState.Setup(this);
		currentState.StateTransitionRequested += SwitchState;
		currentState.Name = "PlayerStateMachine:" + newState.ToString();
		CallDeferred("add_child", currentState);
	}

	public void SetMovementAnimation()
	{
		if (Velocity.Length() > 0.1f)
			AnimationPlayer.Play("run");
		else
			AnimationPlayer.Play("Idle");
	}

	public void SetHeading()
	{
		if (Velocity.X > 0) heading = Vector2.Right;
		else if (Velocity.X < 0) heading = Vector2.Left;
	}

	public void FlipPlayer()
	{
		if (heading == Vector2.Right)
			PlayerSprite.FlipH = false;
		else if (heading == Vector2.Left)
			PlayerSprite.FlipH = true;
	}
}
