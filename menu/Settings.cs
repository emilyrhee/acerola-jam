using Godot;
using System;

public partial class Settings : Node2D
{
    public override void _Ready()
    {
        Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            Visible = !Visible;
        }
    }

    public override void _Process(double delta)
    {
    }
}
