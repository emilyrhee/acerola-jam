using Godot;
using System;

public partial class Log : AnimatedSprite2D
{
    private static RichTextLabel logLabel;

	public override void _Ready()
    {
        logLabel = GetNode<RichTextLabel>("../../CanvasLayer/Logcount/Count");
	}
    public static void UpdateLogLabel()
    {
        logLabel.Clear();
        logLabel.AddText(Global.logsCollected.ToString());
    }
	public void _on_log_area_body_entered(Node2D player)
    {
        Global.logsCollected++;
        UpdateLogLabel();
        QueueFree();
	}
    
	public override void _Process(double delta)
	{
		Play("float");
	}
}