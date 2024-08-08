using Godot;
using System;
using System.Runtime.Intrinsics;

public partial class Tomato : CharacterBody2D
{
	[Export] private int speed = 30;

	[Export] private int leftBound;
	[Export] private int rightBound;

	private AudioStreamPlayer2D stompSound;
    private Area2D deathArea;
    private Area2D stompArea;
    private CollisionShape2D shape;
    private CollisionShape2D stompShape;
    private CollisionShape2D deathShape;

	private Sprite2D sprite;

	public override void _Ready() 
	{
		stompSound = GetNode<AudioStreamPlayer2D>("StompSound");
        deathShape = GetNode<CollisionShape2D>("DeathArea/DeathShape");
        deathArea = GetNode<Area2D>("DeathArea");
        stompArea = GetNode<Area2D>("StompDetector");
        stompShape = GetNode<CollisionShape2D>("StompDetector/StompShape");
        shape = GetNode<CollisionShape2D>("CollisionShape2D");
		sprite = GetNode<Sprite2D>("Sprite2D");
	}

    private void DisableShapes()
    {
        deathShape.Disabled = true;
        stompShape.Disabled = true;
    }

    public void _on_death_area_2d_body_entered(Node2D node)
    {
        if (node is Player player) // Check if the node is a Player
        {
            if (Global.logsCollected == 0)
            {
                player.CallDeferred("Die");
            }
            else if (Global.logsCollected > 0)
            {
                CallDeferred("DisableShapes");
                Global.logsCollected = 0;
                Log.UpdateLogLabel();
            }
        }
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

    public void _on_stomp_detector_body_entered(Node2D body)
    {
        if (body is Player player)
        {
            stompSound.Play();

            deathArea.QueueFree();
            stompArea.QueueFree();
            shape.QueueFree();
            sprite.QueueFree();

            Vector2 newVelocity = player.Velocity;
            newVelocity.Y = Player.jumpVelocity;
            player.Velocity = newVelocity;
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
