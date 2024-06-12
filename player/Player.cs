using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public static float speed = 95.0f;
    public static float jumpVelocity = -250.0f;
    public float acceleration = 700.0f; // Adjust the acceleration value as needed
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private AnimatedSprite2D animatedSprite;
    public static bool isNodding = false;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    private void Animate()
    {
        if (isNodding)
        {
            animatedSprite.Play("nod");
            return;
        }

        if (Velocity.Length() == 0)
        {
            animatedSprite.Stop();
            return;
        }

        if (Velocity.X != 0)
        {
            animatedSprite.FlipH = Velocity.X < 0;
        }

        animatedSprite.Play("walk");
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

    public static void Jump(ref Vector2 velocity)
    {
        velocity.Y = jumpVelocity;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;

        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            Jump(ref velocity);

        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            // Apply acceleration
            velocity.X += direction.X * acceleration * (float)delta;
            // Limit the velocity toGlobal.playerSpeed 
            velocity.X = Mathf.Clamp(velocity.X, -speed, speed);
        }
        else
        {
            // Decelerate if no input
            velocity.X = Mathf.MoveToward(Velocity.X, 0, acceleration * (float)delta);
        }
        Velocity = velocity;
        MoveAndSlide();

        Animate();

        if (isFalling())
            Die();
    }
}
