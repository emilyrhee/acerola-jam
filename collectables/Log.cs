using Godot;
using System;

public partial class Log : AnimatedSprite2D
{
    private RichTextLabel logLabel;

	public override void _Ready() {
        logLabel = GetNode<RichTextLabel>("../CanvasLayer/Logcount/Count");
	}

	public void _on_log_area_body_entered(Node2D body) {
		if (body.Name == "Player")
		{
            Global.logsCollected++;
            logLabel.Clear();
            logLabel.AddText(Global.logsCollected.ToString());  
			QueueFree();
		}
	}
	public override void _Process(double delta)
	{
		Play("float");
	}
}