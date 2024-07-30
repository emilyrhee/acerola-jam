using Godot;
using System;

public partial class Settings : Node2D
{
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            Visible = true;
        }
    }
	public override void _Process(double delta)
	{
	}
}
