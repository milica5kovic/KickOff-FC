using Godot;

public partial class TacklingState : PlayerState
{
	const int DurationTackle = 200;
	public ulong timeStartTackle = ulong.MaxValue;

	public override void OnEnter()
	{
		timeStartTackle = Time.GetTicksMsec();
		Player.AnimationPlayer.Play("Tackle");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (timeStartTackle == ulong.MaxValue) return;
		if (Time.GetTicksMsec() - timeStartTackle > DurationTackle)
		{
			EmitSignal(SignalName.StateTransitionRequested, (int)Player.State.Moving);
		}
	}
}
