using Godot;
using System;
using System.Runtime.Intrinsics;

public partial class Tomato : CharacterBody2D
{
	[Export] private int speed = 30;
	[Export] private int leftBound;
	[Export] private int rightBound;
	private AudioStreamPlayer2D stompSound;
	private Sprite2D sprite;
	public override void _Ready() 
	{
		stompSound = GetNode<AudioStreamPlayer2D>("StompSound");
		sprite = GetNode<Sprite2D>("Sprite2D");
	}
	public void _on_area_2d_body_entered(Node2D body)
	{
		if (body.Name == "Player")
		{
			body.Position = new Vector2(119, 80);
		}
	}
	public void _on_stomp_sound_finished()
	{
		QueueFree();
	}
	[Export]
	private bool shouldMove = false;
	private bool isMovingLeft = false;
	public void Move()
	{
		Vector2 velocity = Velocity;
		if (isMovingLeft)
		{
			if (Position.X <= leftBound)
			{
				isMovingLeft = false;
				velocity.X = speed;
			}
			else
			{
				velocity.X = -speed;
			}
		}
		else
		{
			if (Position.X >= rightBound)
			{
				isMovingLeft = true;
				velocity.X = -speed;
			}
			else
			{
				velocity.X = speed;
			}
		}

		Velocity = velocity;
	}
	public void _on_stomp_detector_body_entered(CharacterBody2D body) {
		if (body.Name == "Player")
		{
			stompSound.Play();

			sprite.QueueFree();

			Vector2 newVelocity = body.Velocity;
			const float jumpVelocity = Player.JumpVelocity;

			newVelocity.Y = jumpVelocity;
			body.Velocity = newVelocity;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		if (shouldMove == true)
		{
			Move();
		}
		MoveAndSlide();
	 }
}
