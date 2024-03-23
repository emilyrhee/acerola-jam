using Godot;
using System;

public partial class Log : AnimatedSprite2D
{
    public void _on_log_area_body_entered(Node2D body) {
    	if (body.Name == "Player")
		{
            GD.Print("log collected");
            QueueFree();
		}
    }
	public override void _Process(double delta)
	{
        Play("float");
	}
}
