using Godot;
using Utils;

public partial class Player : CharacterBody2D
{
	public enum ControlScheme { CPU, P1, P2 }
	public enum State { Moving, Tackling }

	[Export] public float speed;
	[Export] public Vector2 heading = Vector2.Right;
	[Export] public Keys Keys { get; set; }
	[Export] public ControlScheme Control { get; set; }
	[Export] public AnimationPlayer AnimationPlayer { get; set; }
	[Export] public Sprite2D PlayerSprite { get; set; }

	public PlayerState currentState;
	public PlayerStateFactory stateFactory;
	public bool IsTackling = false;

	public override void _Ready()
	{
		stateFactory = GetNode<PlayerStateFactory>("PlayerStateFactory");
		SwitchState(State.Moving);
	}

	public override void _PhysicsProcess(double delta)
	{
		SetHeading();
		FlipPlayer();
		MoveAndSlide();
	}

	public void SwitchState(State newState)
	{
		IsTackling = newState == State.Tackling;

		if (currentState != null)
		{
			currentState.StateTransitionRequested -= SwitchState;
			currentState.SetPhysicsProcess(false);
			currentState.SetProcess(false);
			currentState.SetProcessInput(false);
			currentState.QueueFree();
		}

		currentState = stateFactory.GetState(newState);
		currentState.Name = "PlayerStateMachine:" + newState.ToString();
		currentState.StateTransitionRequested += SwitchState;
		currentState.Setup(this);
		CallDeferred("add_child", currentState);
	}

	public void SetHeading()
	{
		if (Velocity.X > 0) heading = Vector2.Right;
		else if (Velocity.X < 0) heading = Vector2.Left;
	}

	public void FlipPlayer()
	{
		PlayerSprite.FlipH = heading == Vector2.Left;
	}
}
