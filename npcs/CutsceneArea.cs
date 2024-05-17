using Godot;
using System;

public partial class CutsceneArea : Area2D
{
    private RichTextLabel dialog;
    [Export] Font font;
	public override void _Ready()
	{
        dialog = GetNode<RichTextLabel>("Dialog");
        dialog.PushFont(font);
        dialog.PushFontSize(11);
	}
    public void _on_body_entered(Node2D body)
    {
        if (body.Name == "Player")
        {
            dialog.AddText("Hello there!");
            GD.Print("hello there!");
        }
    }
	public override void _Process(double delta)
	{
	}
}
