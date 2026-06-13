using Godot;

public partial class TacklingState : PlayerState
{
	const int DurationPriorRecovery = 200;
	const float GroundFriction = 250.0f;

	private bool _isTackleComplete = false;
	private ulong _timeFinishTackle = 0;

	public override void OnEnter()
	{
		Player.AnimationPlayer.Play("Tackle");
		_isTackleComplete = false;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (!_isTackleComplete)
		{
			Player.Velocity = Player.Velocity.MoveToward(Vector2.Zero, (float)delta * GroundFriction);
			if (Player.Velocity == Vector2.Zero)
			{
				_isTackleComplete = true;
				_timeFinishTackle = Time.GetTicksMsec();
			}
		}
		else if (Time.GetTicksMsec() - _timeFinishTackle > DurationPriorRecovery)
		{
			EmitSignal(SignalName.StateTransitionRequested, (int)Player.State.Recovering);
		}
	}
}
