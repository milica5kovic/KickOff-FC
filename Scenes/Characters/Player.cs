using System;
using System.Numerics;
using Godot;
using Utils;
using Vector2 = Godot.Vector2;



public partial class Player : CharacterBody2D
{
	const int DurationTackle = 200;
	public enum ControlScheme
	{
		CPU,
		P1,
		P2
	}

	public enum State
	{
		Moving,
		Tackling,
		
	}
	
	[Export] public float speed;
	[Export] public Vector2 heading = Vector2.Right;
	[Export] public State state = State.Moving;
	public ulong timeStartTackling = Time.GetTicksMsec();
	[Export] public Keys Keys { get; set; }
	[Export] public ControlScheme Control { get; set; }
	[Export] public AnimationPlayer AnimationPlayer { get; set; }
	[Export] public Sprite2D PlayerSprite {get; set;}
	public override void _PhysicsProcess(double delta)
	{
		if (Control == ControlScheme.CPU)
		{
			return;
		}
		else
		{
			if (state == State.Moving)
			{
				HandleHumanMovement();
				if (Velocity.X != 0 && Keys.IsActionJustPressed(Control, Keys.Action.SHOOT))
				{
					state  = State.Tackling;
					timeStartTackling = Time.GetTicksMsec();
				}
				SetMovementAnimation();
			}
			else if (state == State.Tackling)
			{
				AnimationPlayer.Play("Tackle");
				if (Time.GetTicksMsec() - timeStartTackling > DurationTackle)
				{
					state = State.Moving;
				}
			}
			
		}
		
		SetHeading();
		FlipPlayer();
	}

	public void HandleHumanMovement()
	{
		Vector2 direction = Keys.GetInput(Control);
		Velocity = direction * speed;
		
	}

	public void SetMovementAnimation()
	{
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

	public void SetHeading()
	{
		if (Velocity.X > 0)
		{
			heading = Vector2.Right;
		}
		else if (Velocity.X < 0)
		{
			heading = Vector2.Left;
		}
	}
	public void FlipPlayer()
	{
		if (heading == Vector2.Right)
		{
			PlayerSprite.FlipH = false;
			
		}
		else if (heading == Vector2.Left)
		{
			PlayerSprite.FlipH = true;
		}
	}
}
