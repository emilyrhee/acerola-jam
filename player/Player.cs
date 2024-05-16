using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 100.0f;
	public const float JumpVelocity = -250.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private AnimatedSprite2D animatedSprite;
	public override void _Ready() {
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	private void UpdateAnimatedSprite() {		
		if (Velocity.Length() == 0) 
		{
			animatedSprite.Stop();
		} 
		else 
		{
			if (Velocity.X != 0) 
			{
				if (Velocity.X < 0) 
				{
					animatedSprite.FlipH = true;
				} 
				else 
				{
					animatedSprite.FlipH = false;
				}
			}
			animatedSprite.Play("walk");
		}
	}
	private void FallReset()
	{
		if (Position.Y > 120)
		Position = new Vector2(119, 80);
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		UpdateAnimatedSprite();
		FallReset();
	}
}
