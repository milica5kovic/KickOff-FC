using Godot;

public partial class PlayerStateRecovering : PlayerState
{
	const int DurationRecovery = 500;
	public ulong timeStartRecovering = ulong.MaxValue;

	public override void OnEnter()
	{
		timeStartRecovering = Time.GetTicksMsec();
		Player.Velocity = Vector2.Zero;
		Player.AnimationPlayer.Play("Recovery");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (timeStartRecovering == ulong.MaxValue) return;
		if (Time.GetTicksMsec() - timeStartRecovering > DurationRecovery)
			EmitSignal(SignalName.StateTransitionRequested, (int)Player.State.Moving);
	}
}
