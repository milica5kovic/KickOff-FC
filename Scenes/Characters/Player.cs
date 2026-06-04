using Godot;
public partial class Player : CharacterBody2D
{

	public enum ControlScheme
	{
		CPU,
		P1,
		P2
	}
	[Export] public float speed;
	[Export] public ControlScheme Control { get; set; }
	[Export] public AnimationPlayer AnimationPlayer { get; set; }
	public override void _Process(double delta)
	{
		Vector2 direction = Input.GetVector("p1_left", "p1_right", "p1_up", "p1_down");
		Velocity = direction * speed;
		
		if(Velocity.Length() > 0.1f)
		{
			AnimationPlayer.Play("run");
		}
		else
		{
			AnimationPlayer.Play("Idle");
		}

		MoveAndSlide();
	}
	
}
