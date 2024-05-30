using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public static float speed = Global.playerSpeed;
	public static float jumpVelocity = -250.0f;
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
	private bool isFalling()
	{
        return Position.Y > 140;
	}
    public void Die()
    {
        Global.logsCollected = 0;
        GetTree().ReloadCurrentScene();
    }
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = jumpVelocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
		    Jump(ref velocity);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		UpdateAnimatedSprite();

		if (isFalling())
            Die();
	}
}
