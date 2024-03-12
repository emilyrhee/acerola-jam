using Godot;
using System;

public partial class Tomato : CharacterBody2D
{
    public void _on_area_2d_body_entered(Node2D body) {
        if (body.Name == "Player")
        {
            body.Position = new Vector2(119, 80);
        }
    }
    public void _on_stomp_detector_body_entered(CharacterBody2D body) {
        if (body.Name == "Player")
        {
            QueueFree();

            Vector2 newVelocity = body.Velocity;
            const float jumpVelocity = Player.JumpVelocity;

            newVelocity.Y = jumpVelocity;
            body.Velocity = newVelocity;
        }
    }
}
