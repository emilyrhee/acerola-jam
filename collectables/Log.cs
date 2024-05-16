using Godot;
using System;

public partial class Log : AnimatedSprite2D
{
    private RichTextLabel logLabel;
    private int logCount = 0;
	public override void _Ready() {
		logLabel = GetNode<RichTextLabel>("../CanvasLayer/Logcount/Count");
        logLabel.AddText(logCount.ToString());
	}

	public void _on_log_area_body_entered(Node2D body) {
		if (body.Name == "Player")
		{
			logCount++;
            logLabel.Clear();
            logLabel.AddText(logCount.ToString());
			QueueFree();
		}
	}
	public override void _Process(double delta)
	{
		Play("float");
	}
}