using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public static float speed = 95.0f;
    public static float jumpVelocity = -200.0f;
    public float acceleration = 700.0f;
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    public static bool isNodding = false;
    private AnimatedSprite2D animatedSprite;
    private float defaultGravity;
    private bool isJumping = false;
    private Area2D stompArea;
    private Area2D deathArea;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        stompArea = GetNode<Area2D>("Area2D");
        deathArea = GetNode<Area2D>("DeathArea");
        defaultGravity = gravity;
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
    private void _on_area_2d_body_entered(Node2D node)
    {
        if (Velocity.Y > -1)
        {
            Vector2 newVelocity = Velocity;
            newVelocity.Y = jumpVelocity;
            Velocity = newVelocity;

            Tomato tomato = node as Tomato;
            tomato.stompSound.Play();
            tomato.shape.QueueFree();
            tomato.sprite.QueueFree();
        }
    }
    private void _on_death_area_body_entered(Node2D node)
    {
        CallDeferred("Die");
    }
    public static void Jump(ref Vector2 velocity)
    {
        velocity.Y = jumpVelocity;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Apply gravity
        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        // Jump logic
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            Jump(ref velocity);
            isJumping = true;
        }

        if (isJumping && Input.IsActionPressed("ui_accept"))
        {
            // Apply an upward force while the jump key is held
            velocity.Y += jumpVelocity * 0.04f;
        }

        if (Input.IsActionJustReleased("ui_accept"))
        {
            isJumping = false;
        }

        // Horizontal movement logic
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            // Apply acceleration
            velocity.X += direction.X * acceleration * (float)delta;
            // Limit the velocity to the player's speed
            velocity.X = Mathf.Clamp(velocity.X, -speed, speed);
        }
        else
        {
            velocity.X = 0; // no deceleration
        }

        Velocity = velocity;
        MoveAndSlide();

        Animate();

        if (isFalling())
            Die();
    }
}
