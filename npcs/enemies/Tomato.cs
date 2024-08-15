using Godot;
using System;
using System.Runtime.Intrinsics;

public partial class Tomato : CharacterBody2D
{
	[Export] private int speed = 30;

	[Export] private int leftBound;
	[Export] private int rightBound;

	public AudioStreamPlayer2D stompSound;
    public CollisionShape2D shape;

	public Sprite2D sprite;

	public override void _Ready() 
	{
		stompSound = GetNode<AudioStreamPlayer2D>("StompSound");
        shape = GetNode<CollisionShape2D>("CollisionShape2D");
		sprite = GetNode<Sprite2D>("Sprite2D");
	}
	public void _on_stomp_sound_finished()
	{
		QueueFree();
	}
    
	[Export] private bool shouldMove = false;
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
    
	public override void _PhysicsProcess(double delta)
	{
		if (shouldMove == true)
		{
			Move();
		}
		MoveAndSlide();
	 }
}
