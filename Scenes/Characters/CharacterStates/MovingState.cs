using Godot;
using Utils;

public partial class MovingState : PlayerState
{
	private bool _shootPressed = false;

	public override void _Input(InputEvent @event)
	{
		if (Player == null) return;
		if (Player.Control == Player.ControlScheme.CPU) return;
		if (@event.IsActionPressed(Player.Keys.actionsMap[Player.Control][Keys.Action.SHOOT]))
			_shootPressed = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (Player.Control == Player.ControlScheme.CPU)
			return;
		HandleHumanMovement();
		Player.SetMovementAnimation();
		Player.SetHeading();
	}

	private void HandleHumanMovement()
	{
		Player.Velocity = Player.Keys.GetInput(Player.Control) * Player.speed;
		if (_shootPressed)
		{
			_shootPressed = false;
			EmitSignal(SignalName.StateTransitionRequested, (int)Player.State.Tackling);
		}
	}
}
